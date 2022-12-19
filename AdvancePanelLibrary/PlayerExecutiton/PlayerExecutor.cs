using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.BaseStructure;
using AdvancePanelLibrary.Component.BuildingBlocks.StartAndStop;
using AdvancePanelLibrary.Component.Controller;
using AdvancePanelLibrary.Utility;
using AdvancePanelLibrary.Utility.Log;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.PlayerExecutiton
{
    public class PlayerExecutor
    {
        #region variables
        private UIA3Automation _automation;
        private AdvancePanel _panelAdvance;
        private BasicBuildingBlock? _startBuildingBlock;
        private BasicBuildingBlock? _currentCommand;
        private GlobalVariablePlayer _globalVariablePlayer;
        private ExecutorType _currentExecutorType;

        private bool isRunOfIndependentBuildingBlock=false;
        private List<BasicBuildingBlock> listOfIndependentBuildingBlock;
        #endregion


        public PlayerExecutor()
        {
            _currentCommand = null;
            _globalVariablePlayer = new GlobalVariablePlayer();
        }

        public string ConnectToAdvancePanel(AdvancePanel panel)
        {
            MyLog.WritelnBoth("ConnectToAdvancePanel");
            _panelAdvance = panel;
            try
            {
                if (_panelAdvance == null)
                    throw new Exception("هیچ اتصال فعالی  (پنل ادونسی) پیدا نشد");
                _startBuildingBlock = null;
                _currentCommand = null;
                foreach (var bld in _panelAdvance.AllBuildingBlock)
                {
                    if (bld is BldBlkStart bldBlkStart)
                    {
                        if (_currentCommand != null)
                            throw new Exception("تعداد نقطه شروع بیش از یک المان می باشد");
                        _startBuildingBlock = bldBlkStart;
                        return "";
                    }
                }

                if (_currentCommand == null)
                    throw new Exception("هیچ نقطه شروعی پیدا نشد");
                return "";
            }
            catch (Exception ex)
            {
                MyLog.WritelnBoth("Player Error", ex.Message.ToString());
                return ex.Message.ToString();
            }
        }


        public enum ExecutorType
        {
            None,
            FromStartToThisBuildingBlock,
            FromThisToEndBuildingBlock,
            FromStartToEnd,
            ContinueLastConfig,
        }
        public enum NextCommandResult
        {
            None,
            OkNextCommand,
            ErrorNotFoundNextCommand,
            NoEffectIsRunningStill
        }
        public NextCommandResult GotoNextCommand()
        {
            if (_currentCommand == null)
            {
                if (_startBuildingBlock == null)
                    //دستور بعدی شناسایی نشد
                    return NextCommandResult.ErrorNotFoundNextCommand;

                _currentCommand = _startBuildingBlock;
                _currentCommand.SetExecuteInit();
                return NextCommandResult.OkNextCommand;

            }

            if (_currentCommand.GetExecuteStatus() == BasicBuildingBlock.StatusOfExecutionEnum.Finish ||
                _currentCommand.GetExecuteStatus() == BasicBuildingBlock.StatusOfExecutionEnum.FinishWithError ||
                _currentCommand.GetExecuteStatus() == BasicBuildingBlock.StatusOfExecutionEnum.FinishLoopCompleted ||
                _currentCommand.GetExecuteStatus() == BasicBuildingBlock.StatusOfExecutionEnum.RunningLoop)
            {
                var nextCmd = _currentCommand.GetNextBuildingBlock();
                if (nextCmd == null)
                {
                    //دستور بعدی شناسایی نشد
                    return NextCommandResult.ErrorNotFoundNextCommand;
                }
                _currentCommand = nextCmd;
                _currentCommand.SetExecuteInit();
                return NextCommandResult.OkNextCommand;
            }

            if (_currentCommand.GetExecuteStatus() == BasicBuildingBlock.StatusOfExecutionEnum.Running)
            {
                return NextCommandResult.NoEffectIsRunningStill;
            }

            return NextCommandResult.None;
        }

        public ExecuteResult ExecuteCurrentCommand()
        {
            if (_currentCommand == null)
                return ExecuteResult.None;
            if (_currentCommand.GetExecuteStatus() == BasicBuildingBlock.StatusOfExecutionEnum.Finish)
                return ExecuteResult.Finish;
            var res = _currentCommand.ExecuteBuildingBlock(_globalVariablePlayer);
            return ExecuteResult.Finish;
        }


        public enum ExecuteResult
        {
            None,
            Running,
            Finish
        }

        public void ResetExecutor(BasicBuildingBlock? startCommand)
        {
            _currentCommand = startCommand;
        }
        #region Executor
        //************************************************
        // اجرای عمومی تمام دستورات با تمام پارامترهای لازم
        public (bool result, string message) ExecuteCommandTotalSupport(ExecutorType executorType, bool isSetInitCommand, BasicBuildingBlock? fromThisBuildingBlock = null, BasicBuildingBlock? toBuildingBlock = null)
        {
            try
            {
                ///  اگر متغیر گلوبال همیشگی ایجاد نشده باشد آنرا ایجاد می کنیم
                _panelAdvance.PermanentScopeVariables ??= new VariableNameValue();
                _panelAdvance.OnlyThisCaseScopeVariables ??= new VariableNameValue();

                if (_startBuildingBlock == null)
                    return new(false, "بلوک ابتدایی برای شروع پیدا نشد");
                if (isSetInitCommand)
                {
                    isRunOfIndependentBuildingBlock = false;
                    listOfIndependentBuildingBlock= new List<BasicBuildingBlock>();
                    _panelAdvance.ResetAllDebugIDs();
                    _panelAdvance.Invalidate();
                    if (executorType == ExecutorType.FromStartToThisBuildingBlock)
                        ResetExecutor(null);
                    else if (executorType == ExecutorType.FromThisToEndBuildingBlock)
                        ResetExecutor(fromThisBuildingBlock);
                    else if (executorType == ExecutorType.FromStartToEnd)
                        ResetExecutor(null);
                }

                do
                {
                    var resGetNext = GotoNextCommand();

                    if (resGetNext == NextCommandResult.ErrorNotFoundNextCommand || resGetNext == NextCommandResult.None)
                    {
                        // ثبت خطا در پیدا کردن دستور بعدی و پایان
                        return new(false, "خطا در پیدا کردن دستور بعدی و پایان");
                    }

                    if (_currentCommand == null)
                        throw new Exception("Current Command: NULL");
                    else
                        MyLog.WritelnBoth($"Current Command: {GlobalFunction.GetTypeLastClass(_currentCommand.GetType())}");

                    if (!isRunOfIndependentBuildingBlock)
                    {
                        isRunOfIndependentBuildingBlock= true;
                        // لیست بلوک هایی که بدون وابستگی هستند و باید به صورت اتوماتیک اجرا شوند
                        var listOfBld = _currentCommand.GetIndependentBuildingBlock(_currentCommand);
                        // اجرا از آخر به اول
                        if (listOfBld != null && listOfBld.Count>0)
                        {
                            for(var i= listOfBld.Count-1; i>-1; i--)
                            {
                                var bld = listOfBld[i];
                                // اجرای بلوک مستقل
                                var subRes = bld.ExecuteBuildingBlock(_globalVariablePlayer);
                            }
                        }
                        isRunOfIndependentBuildingBlock= false;
                    }

                    // اجرای دستور جاری
                    var resExec = ExecuteCurrentCommand();
                    if (resExec == ExecuteResult.None)
                    {
                        // ثبت خطا در اجرای برنامه و پایان
                        return new(false, "خطا در اجرای برنامه و پایان");
                    }

                    if (executorType == ExecutorType.FromStartToThisBuildingBlock)
                    {
                        if (_currentCommand == toBuildingBlock)
                        {
                            if (_currentCommand.StatusOfExecution == BasicBuildingBlock.StatusOfExecutionEnum.None)
                            {
                                // ثبت خطا در اجرای برنامه و پایان
                                return new(false, "خطا در اجرای برنامه و پایان");
                            }
                            if (_currentCommand.StatusOfExecution == BasicBuildingBlock.StatusOfExecutionEnum.Finish)
                            {
                                // اجرای موفقیت آمیز برنامه و رسیدن به المان مورد نظر
                                return new(true, "");
                            }
                        }
                    }
                    else if (executorType == ExecutorType.FromThisToEndBuildingBlock)
                    { 
                    }
                    else if (executorType == ExecutorType.FromStartToEnd)
                    {
                    }


                } while (true);
            }
            catch (Exception ex)
            {
                MyLog.WritelnBoth("Error(ExecuteCommandTotalSupport)", ex.Message.ToString());
                return new(false, ex.ToString());
            }
        }

        // اجرای بلوک های ساختمانی از ابتدا تا همین بلوک
        public (bool result, string message) ExecuteCommandFromStartToThisBuildingBlock(BasicBuildingBlock toBuildingBlock)
        {
            _currentExecutorType = ExecutorType.FromStartToThisBuildingBlock;
            return ExecuteCommandTotalSupport(_currentExecutorType, true, null,toBuildingBlock);
        }

        // اجرای بلوک های ساختمانی از همین نقطه تا آخرین بلوک
        public (bool result, string message) ExecuteCommandFromThisToEndBuildingBlock(BasicBuildingBlock fromThisBuildingBlock)
        {
            _currentExecutorType = ExecutorType.FromThisToEndBuildingBlock;
            return ExecuteCommandTotalSupport(_currentExecutorType, true, fromThisBuildingBlock);
        }

        // اجرای بلوک های ساختمانی از ابتدا تا آخر
        public (bool result, string message) ExecuteCommandFromStartToEnd()
        {
            _currentExecutorType = ExecutorType.FromStartToEnd;
            return ExecuteCommandTotalSupport(_currentExecutorType, true);
        }

        public BasicBuildingBlock? GetCurrentComman()
        {
            return _currentCommand;
        }

        public void SetCurrentComman(BasicBuildingBlock? currentCommand)
        {
            _currentCommand=currentCommand;
        }
        #endregion
    }

    public class GlobalVariablePlayer
    {
        public UIA3Automation automation = new UIA3Automation();
        public FlaUI.Core.Application currentApplication;

        public FlaUI.Core.AutomationElements.Window CurrentMainWindow
        {
            get
            {
                if (Process.GetProcesses().Any(x => x.Id == currentApplication.ProcessId))
                    return currentApplication.GetMainWindow(automation);
                else
                    return null;
            }
        }
    }

}
