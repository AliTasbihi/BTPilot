using AutoCreateWithJson.Component.BaseElements;
using AutoCreateWithJson.Component.BuildingBlocks.StartAndStop;
using AutoCreateWithJson.Component.Controller;
using AutoCreateWithJson.Utility;
using AutoCreateWithJson.Utility.Log;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.PlayerExecutiton
{
    public class PlayerExecutor
    {
        #region
        private UIA3Automation _automation;
        private AdvancePanel _panelAdvance;
        private BasicBuildingBlock? _startBuildingBlock;
        private BasicBuildingBlock? _currentCommand;
        private GlobalVariablePlayer _globalVariablePlayer;
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
                _currentCommand.GetExecuteStatus() == BasicBuildingBlock.StatusOfExecutionEnum.FinishWithErrorRunNotFound)
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

            if (_currentCommand == null ||
                _currentCommand.GetExecuteStatus() == BasicBuildingBlock.StatusOfExecutionEnum.Finish)
                return ExecuteResult.None;
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


        // اجرای بلوک های ساختمانی از ابتدا تا همین بلوک
        public (bool result, string message) ExecuteCommandFromStartToThisBuildingBlock(BasicBuildingBlock toBuildingBlock)
        {
            try
            {
                ResetExecutor(null);
                do
                {
                    var resGetNext = GotoNextCommand();

                    if (resGetNext == NextCommandResult.ErrorNotFoundNextCommand || resGetNext == NextCommandResult.None)
                    {
                        // ثبت خطا در پیدا کردن دستور بعدی و پایان
                        return new(false, "خطا در پیدا کردن دستور بعدی و پایان");
                    }

                    if (_currentCommand == null)
                        MyLog.WritelnBoth("Current Command:NULL");
                    else
                        MyLog.WritelnBoth($"Current Command: {GlobalFunction.GetTypeLastClass(_currentCommand.GetType())}");

                    var resExec = ExecuteCurrentCommand();
                    if (resExec == ExecuteResult.None)
                    {
                        // ثبت خطا در اجرای برنامه و پایان
                        return new(false, "خطا در اجرای برنامه و پایان");
                    }

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
                } while (true);
            }
            catch (Exception ex)
            {
                MyLog.WritelnBoth("ExecuteCommandFromStartToThisBuildingBlock", ex.Message.ToString());
                return new(false, ex.ToString());
            }
        }

        // اجرای بلوک های ساختمانی از همین نقطه تا آخرین بلوک
        public (bool result, string message) ExecuteCommandFromThisToEndBuildingBlock(BasicBuildingBlock fromElement)
        {
            try
            {
                if (_startBuildingBlock == null)
                    return new(false, "بلوک ابتدایی برای شروع پیدا نشد");

                ResetExecutor(fromElement);
                do
                {
                    var resExec = ExecuteCurrentCommand();
                    if (resExec == ExecuteResult.None)
                    {
                        // ثبت خطا در اجرای برنامه و پایان
                        return new(false, "خطا در اجرای برنامه و پایان");
                    }

                    var resGetNext = GotoNextCommand();
                    if (resGetNext == NextCommandResult.ErrorNotFoundNextCommand || resGetNext == NextCommandResult.None)
                    {
                        // ثبت خطا در پیدا کردن دستور بعدی و پایان
                        return new(false, "خطا در پیدا کردن دستور بعدی و پایان");
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                MyLog.WritelnBoth("ExecuteCommandFromThisToEndBuildingBlock", ex.Message.ToString());
                return new(false, ex.ToString());
            }
        }

    }

    public class GlobalVariablePlayer
    {
        public UIA3Automation automation = new UIA3Automation();
        public FlaUI.Core.Application currentApplication;

        public FlaUI.Core.AutomationElements.Window CurrentMainWindow
        {
            get
            {
                return currentApplication.GetMainWindow(automation);
            }
        }
    }

}
