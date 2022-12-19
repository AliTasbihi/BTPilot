using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.BaseStructure;
using AdvancePanelLibrary.PlayerExecutiton;
using AdvancePanelLibrary.Utility;
using AdvancePanelLibrary.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BuildingBlocks.Variables
{
    public class BldBlkGetVariable : BasicBuildingBlock
    {
        private object variableValue;

        #region Connector Property  
        private object GetVariableName(object sender)
        {
            return GetConnectorPropertyEditBox(edVariableName, edVariableName);
        }
        private object GetResultValue(object sender)
        {
            return variableValue;
        }
        private object GetDefualtValue(object sender)
        {
            return GetConnectorPropertyEditBox(edDefualtValue, edDefualtValue);
        }
        #endregion

        public BldBlkGetVariable()
        {
            Width = GraphicConstant.bluildingBlockWidth;

            AddHeaderLabel();
            AddVariableName();
            AddResultValue();
            AddScope();
            AddDefualtValue();
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

        private const string edDefualtValue = "edDefualtValue";
        private void AddDefualtValue()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edDefualtValue;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(12, 2, 12, 2);
            edt.Title = "Defualt value";
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Text = "";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetDefualtValue);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private const string comboScope = "comboScope";
        private void AddScope()
        {
            var combo = new ElmComboBox(this);
            combo.Name = comboScope;
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 8, 10, 2);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Scope";
            combo.Items.Add("Only this case");
            combo.Items.Add("Schedule");
            combo.Items.Add("Permanent");
            combo.SelectedText = "Only this case";
            combo.ElmHeight = 30;
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }


        private const string lblResultValue = "lblResultValue";
        private void AddResultValue()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblResultValue;
            lbl.IsNecessaryToView = 1;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Result value";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetResultValue);
            Children.Add(lbl);

            Children.Add(new ElmSeparateLine());
        }

        private const string edVariableName = "edVariableName";
        private void AddVariableName()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edVariableName;
            edt.IsNecessaryToView = 1;
            edt.Padding = new Padding(12, 2, 12, 2);
            edt.Title = "Variable name";
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Text = "";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetVariableName);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Get Variable";
            lbl.ElmHeight = 0;
            lbl.Padding = new Padding(10, 0, 3, 0);
            lbl.MySize = MyTextSize.Large;
            lbl.BackGround = Color.FromArgb(132, 114, 70);
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
                var variableName = (string)GetVariableName(null);
                var defualtValue = (string)GetDefualtValue(null);
                var scope = ((ElmComboBox)ElementByName(comboScope)).SelectedText;

                OneVarNameValue varValue = null;
                if (scope.Equals("Only this case", StringComparison.OrdinalIgnoreCase))
                {
                    varValue = advancePanel.OnlyThisCaseScopeVariables.Get(variableName);
                    if (varValue == null)
                        varValue = advancePanel.OnlyThisCaseScopeVariables.Add(variableName, defualtValue);
                }
                else if (scope.Equals("Schedule", StringComparison.OrdinalIgnoreCase))
                {
                    varValue = advancePanel.PermanentScopeVariables.Get(variableName);
                    if (varValue == null)
                        varValue = advancePanel.PermanentScopeVariables.Add(variableName, defualtValue);
                }
                else if (scope.Equals("Permanent", StringComparison.OrdinalIgnoreCase))
                {
                    varValue = advancePanel.PermanentScopeVariables.Get(variableName);
                    if (varValue == null)
                        varValue = advancePanel.PermanentScopeVariables.Add(variableName, defualtValue);
                }
                else
                {
                    MyLog.WritelnBoth("Error", "Not Found Scope");
                    StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                    return false;
                }

                if (varValue== null)
                {
                    MyLog.WritelnBoth("Error", "Not Found Variable Name");
                    StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                    return false;
                }
                if (varValue.VarValueObject == null)
                    variableValue = defualtValue;
                else
                    variableValue = varValue.VarValueObject;

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
