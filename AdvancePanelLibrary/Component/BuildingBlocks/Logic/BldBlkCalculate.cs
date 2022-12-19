using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.BaseStructure;
using AdvancePanelLibrary.PlayerExecutiton;
using AdvancePanelLibrary.Utility;
using AdvancePanelLibrary.Utility.Log;
using FlaUI.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BuildingBlocks.Logic
{
    public class BldBlkCalculate : BasicBuildingBlock
    {
        double resultNumber = 0;
        #region Connector Property  
        private object GetValueA(object sender)
        {
            return GetConnectorPropertyEditBox(edValueA, edValueA);
        }
        private object GetValueB(object sender)
        {
            return GetConnectorPropertyEditBox(edValueB, edValueB);
        }
        private object GetResultNumber(object sender)
        {
            return resultNumber;
        }
        private object GetRoundResult(object sender)
        {
            return GetConnectorPropertyCheckBox(chkRoundResult, chkRoundResult);
        }
        #endregion

        public BldBlkCalculate()
        {
            Width = GraphicConstant.bluildingBlockWidth;

            AddHeaderLabel();
            AddValueA();
            AddValueB();
            AddCalculationMethod();
            AddResultNumber();
            AddRoundResult();
            AddCollapse();
        }

        private void AddCollapse()
        {
            var btn = new ElmButton(this);
            btn.Title = GraphicConstant.textExpandButton;
            btn.IsCollapseExpandMode = true;
            btn.Padding = new Padding(1, 1, 1, 1);
            Children.Add(btn);
        }

        private const string chkRoundResult = "chkRoundResult";
        private const string edRoundDecimals = "edRoundDecimals";
        private void AddRoundResult()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkRoundResult;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(5, 0, 10, 0);
            chk.Title = "Round result";
            chk.AddTwoConnector(Color.Blue, 0, 1, outputDataFunction: GetRoundResult);
            Children.Add(chk);

            chk.TheClick += ChkRoundResultClick;


            var edt = new ElmEditBox(this);
            edt.Name = edRoundDecimals;
            edt.Visible = false;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.Title = "Round decimals";
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Text = "2";
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }
        private void ChkRoundResultClick(object sender, MouseEventArgs e)
        {
            var chk = sender as ElmCheckBox;
            //chk.TheClick(sender, e);
            var ed = (ElmEditBox)ElementByName(edRoundDecimals);
            ed.Visible = chk.Checked;
        }


        private const string lblResultNumber = "lblResultNumber";
        private void AddResultNumber()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblResultNumber;
            lbl.IsNecessaryToView = 1;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Result number";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetResultNumber);
            Children.Add(lbl);

            Children.Add(new ElmSeparateLine());
        }


        private const string comboCalculationMethod = "comboCalculationMethod";
        private void AddCalculationMethod()
        {
            var combo = new ElmComboBox(this);
            combo.Name = comboCalculationMethod;
            combo.IsNecessaryToView = 1;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Title = "Calculation method";
            combo.Items.Add("A + B");
            combo.Items.Add("A - B");
            combo.Items.Add("A * B");
            combo.Items.Add("A / B");
            combo.SelectedText = "A + B";
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private const string edValueB = "edValueB";
        private void AddValueB()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edValueB;
            edt.IsNecessaryToView = 1;
            edt.Padding = new Padding(12, 2, 12, 2);
            edt.Title = "Value B";
            edt.TitlePosition = ContentAlignment.MiddleLeft;
            edt.Text = "";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetValueB);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private const string edValueA = "edValueA";
        private void AddValueA()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edValueA;
            edt.IsNecessaryToView = 1;
            edt.Padding = new Padding(12, 2, 12, 2);
            edt.Title = "Value A";
            edt.TitlePosition = ContentAlignment.MiddleLeft;
            edt.Text = "";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetValueA);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Calculate";
            lbl.ElmHeight = 0;
            lbl.Padding = new Padding(10, 0, 3, 0);
            lbl.MySize = MyTextSize.Large;
            lbl.BackGround = Color.FromArgb(0, 183, 195);
            lbl.TextColor = Color.White;
            lbl.Alinment = ContentAlignment.MiddleLeft;
            //lbl.AddTwoConnector(Color.Green, 0, 1);
            Children.Add(lbl);

            lbl.IsHeaderLabel = true;
        }



        ///////////////////////////////
        ///   EXECUTOR 
        ///
        ///
        //////////////////////////////////

        #region EXECUTOR
        public override void SetExecuteInit()
        {
            StatusOfExecution = StatusOfExecutionEnum.None;
        }

        public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        {
            try
            {
                MyLog.WritelnBoth($"Block: {GlobalFunction.BuildingBlockComponentDecompress(GlobalFunction.GetTypeLastClass(GetType()))} ({this.DebugID})");
                var valueA = (string)GetValueA(null);
                var valueB = (string)GetValueB(null);
                var method = ((ElmComboBox)ElementByName(comboCalculationMethod)).SelectedText;

                resultNumber = 0;
                if (method == "A + B")
                {
                    resultNumber = GlobalFunction.StringToFloat(valueA) + GlobalFunction.StringToFloat(valueB);
                }
                else if (method == "A - B")
                {
                    resultNumber = GlobalFunction.StringToFloat(valueA) - GlobalFunction.StringToFloat(valueB);
                }
                else if (method == "A * B")
                {
                    resultNumber = GlobalFunction.StringToFloat(valueA) * GlobalFunction.StringToFloat(valueB);
                }
                else if (method == "A / B")
                {
                    var b = GlobalFunction.StringToFloat(valueB);
                    if (b != 0)
                        resultNumber = GlobalFunction.StringToFloat(valueA) + b;
                    else
                        resultNumber = 0;
                }
                else
                {
                    MyLog.WritelnBoth("Error", "Not Found Method");
                    StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                    return false;
                }

                MyLog.WritelnBoth($"Result: {resultNumber}");
                StatusOfExecution = StatusOfExecutionEnum.Finish;
                return true;
            }
            catch (Exception e)
            {
                MyLog.WritelnBoth("FinishWithError", e.Message);
                StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                return false;
            }
            finally
            {
                if (IsSuccessfullyStatusOfExecution())
                {
                    UpdateAllDataOfArrows();
                }
            }
        }

        public override StatusOfExecutionEnum GetExecuteStatus()
        {
            return StatusOfExecution;
        }
        #endregion

    }
}
