using AutoCreateWithJson.Component.BaseElements;
using AutoCreateWithJson.PlayerExecutiton;
using AutoCreateWithJson.Utility;
using AutoCreateWithJson.Utility.Log;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = FlaUI.Core.Application;

namespace AutoCreateWithJson.Component.BuildingBlocks.DesktopUI
{
    public class BldBlkStartApplication : BasicBuildingBlock
    {
        public Application application;
        #region Connector Property  
        private object GetApplicationIn(object sender)
        {
            return GetConnectorPropertyEditBox(lblApplication, edApplication);
        }

        private object GetWorkingFolderIn(object sender)
        {
            return GetConnectorPropertyEditBox(lblWorkingFolder, edWorkingFolder);
        }

        private object GetArgumentsIn(object sender)
        {
            return GetConnectorPropertyEditBox(edArguments, edArguments);
        }

        private object GetUseAnyOpenedIn(object sender)
        {
            return GetConnectorPropertyCheckBox(chkUseAnyOpened, chkUseAnyOpened);
        }
        
        private object GetWindows(object sender)
        {
            return application;
        }
        #endregion

        public BldBlkStartApplication()
        {
            Width = GraphicConstant.bluildingBlockWidth;
            AddHeaderLabel();
            AddApplicationCapture();
            AddWorkingFolder();
            AddArguments();
            AddUseAnyOpened();
            AddErrorOccurred();
            AddWindows();
            AddCollapse();
            AssignOnTheClickAndDoubleClickMethod();
        }

        private void AddCollapse()
        {
            var btn = new ElmButton(this);
            btn.Title = GraphicConstant.textExpandButton;
            btn.IsCollapseExpandMode = true;
            btn.Padding = new Padding(1, 1, 1, 1);
            Children.Add(btn);
        }

        private void AddWindows()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Windows";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetWindows);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private void AddErrorOccurred()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Error occurred";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Green, 1);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private const string chkUseAnyOpened = "chkUseAnyOpened";
        private void AddUseAnyOpened()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkUseAnyOpened;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(5, 0, 10, 0);
            chk.Title = "Use any opened";
            chk.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetUseAnyOpenedIn);
            Children.Add(chk);

            Children.Add(new ElmSeparateLine());
        }

        private const string edArguments = "edArguments";
        private void AddArguments()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edArguments;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.Title = "Arguments";
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Text = "";
            edt.AddTwoConnector(Color.Blue, 1, 0, -17, outputDataFunction: GetArgumentsIn);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private const string lblWorkingFolder = "lblWorkingFolder";
        private const string btnWorkingFolder = "btnWorkingFolder";
        private const string edWorkingFolder = "edWorkingFolder";
        private void AddWorkingFolder()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblWorkingFolder;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 2, 10, 1);
            lbl.Title = "Working folder";
            lbl.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetWorkingFolderIn);
            Children.Add(lbl);

            var btn = new ElmButton(this);
            btn.Name = btnWorkingFolder;
            btn.IsNecessaryToView = 0;
            btn.Title = "Capture";
            btn.ElmHasPosition = true;
            btn.ElmLeft = 30;
            btn.ElmTop = 0;
            btn.ElmWidth = 80;
            btn.ElmHeight = 30;
            Children.Add(btn);
            AddToListOfActionTheClick(btn, SelectWorkingFolderClick);

            var edt = new ElmEditBox(this);
            edt.Name = edWorkingFolder;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.TitlePosition = ContentAlignment.TopLeft;
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private void SelectWorkingFolderClick(object sender, MouseEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var _edWorkingFolder = (ElmEditBox)ElementByName(edWorkingFolder);
                fbd.SelectedPath = _edWorkingFolder.Text;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _edWorkingFolder.TextWithAutoSize = fbd.SelectedPath;
                    advancePanel.Invalidate();
                }
            }

        }

        private const string lblApplication = "lblApplication";
        private const string btnApplication = "btnApplication";
        private const string edApplication = "edApplication";
        private void AddApplicationCapture()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblApplication;
            lbl.Padding = new Padding(10, 2, 10, 1);
            lbl.Title = "Application";
            lbl.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetApplicationIn);
            Children.Add(lbl);

            var btn = new ElmButton(this);
            btn.Name = btnApplication;
            btn.IsNecessaryToView = 0;
            btn.Title = "Capture";
            btn.ElmHasPosition = true;
            btn.ElmLeft = 30;
            btn.ElmTop = 0;
            btn.ElmWidth = 80;
            btn.ElmHeight = 30;
            Children.Add(btn);
            AddToListOfActionTheClick(btn, SelectApplicationClick);

            var edt = new ElmEditBox(this);
            edt.Name = edApplication;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.TitlePosition = ContentAlignment.TopLeft;
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private void SelectApplicationClick(object sender, MouseEventArgs e)
        {
            using (var openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Title = "Open File";
                openFileDialog1.Filter = "All files (*.*)|*.*";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var _edApplication = (ElmEditBox)ElementByName(edApplication);
                    _edApplication.TextWithAutoSize = openFileDialog1.FileName;
                    var _edWorkingFolder = (ElmEditBox)ElementByName(edWorkingFolder);
                    _edWorkingFolder.TextWithAutoSize = Path.GetDirectoryName(openFileDialog1.FileName);
                    advancePanel.Invalidate();
                }
            }

        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Start Application";
            lbl.ElmHeight = 0;
            lbl.Padding = new Padding(10, 0, 3, 0);
            lbl.MySize = MyTextSize.Large;
            lbl.BackGround = Color.FromArgb(131, 158, 177);
            lbl.TextColor = Color.White;
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddTwoConnector(Color.Green, 0, 1);
            Children.Add(lbl);

            lbl.IsHeaderLabel = true;
        }
        ///////////////////////////////
        ///   EXECUTOR 
        ///
        ///
        //////////////////////////////////

        #region EXECUTOR
        //for prepare
        public override void SetExecuteInit()
        {
            StatusOfExecution = StatusOfExecutionEnum.None;
        }

        public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        {
            MyLog.WritelnBoth("StartApplication");
             
            var res = false;
            var fn = (string)GetApplicationIn(null);
            if (File.Exists(fn))
            {
                try
                {
                    if ((bool)GetUseAnyOpenedIn(null))
                    {
                        var processes = PlayerFunctions.FindProcess(fn);
                        if (processes.Length > 0)
                        {
                            application = FlaUI.Core.Application.Attach(processes[0]);
                            res = application.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(2));
                        }
                    }
                    if (!res)
                    {
                        var arg = ((string)GetArgumentsIn(null)).Trim();
                        var wd = (string)GetWorkingFolderIn(null);
                        if (string.IsNullOrEmpty(wd))
                        {
                            wd = Directory.GetParent(fn).FullName;
                        }
                        
                        
                        var appProcInfo = new ProcessStartInfo
                        {
                            WindowStyle = ProcessWindowStyle.Maximized,
                            FileName = fn,
                            Arguments = arg,
                            WorkingDirectory = wd,
                            CreateNoWindow = false,
                            UseShellExecute = false
                        };
                        application = FlaUI.Core.Application.Launch(appProcInfo);
                        res = application.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(5));
                    }
               
                    var mainWindow = application.GetMainWindow(globalVariablePlayer.automation);
                    mainWindow.SetForeground();
                    globalVariablePlayer.currentApplication = application;
                }
                catch (Exception e)
                {
                    MyLog.WritelnBoth("error", e.Message);
                    res = false;
                }
            }

            StatusOfExecution = res ? StatusOfExecutionEnum.Finish : StatusOfExecutionEnum.FinishWithError;
            if (StatusOfExecution == StatusOfExecutionEnum.Finish)
            {
                UpdateAllDataOfArrows();
            }

            return res;

        }

        public override StatusOfExecutionEnum GetExecuteStatus()
        {
            return StatusOfExecution;
        }
        #endregion

    }
}