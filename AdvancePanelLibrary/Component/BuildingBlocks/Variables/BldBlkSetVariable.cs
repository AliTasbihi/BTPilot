using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.BaseStructure;
using AdvancePanelLibrary.PlayerExecutiton;
using AdvancePanelLibrary.Utility;
using AdvancePanelLibrary.Utility.Log;
using AdvancePanelLibrary.Utility.SelectUIElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BuildingBlocks.Variables
{
    public class BldBlkSetVariable : BasicBuildingBlock
    {
        #region Connector Property  
        private object GetVariableName(object sender)
        {
            return GetConnectorPropertyEditBox(edVariableName, edVariableName);
        }
        private object GetValue(object sender)
        {
            return GetConnectorPropertyEditBox(edValue, edValue);
        }
        #endregion

        public BldBlkSetVariable()
        {
            Width = GraphicConstant.bluildingBlockWidth;

            AddHeaderLabel();
            AddVariableName();
            AddValue();
            AddTextField();
            AddScope();
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

        private void AddTextField()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 2, 10, 1);
            lbl.Title = "Text field";
            Children.Add(lbl);


            var btn = new ElmButton(this);
            btn.IsNecessaryToView = 0;
            btn.Title = "+ Add field";
            btn.ElmHasPosition = true;
            btn.ElmLeft = 30;
            btn.ElmTop = 0;

            btn.ElmWidth = 80;
            btn.ElmHeight = 30;
            Children.Add(btn);

            Children.Add(new ElmSeparateLine());
        }


        private const string edValue = "edValue";
        private void AddValue()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edValue;
            edt.IsNecessaryToView = 1;
            edt.Padding = new Padding(12, 2, 12, 2);
            edt.Title = "Variable name";
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Text = "";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetValue);
            Children.Add(edt);

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
            lbl.Title = "Set Variable";
            lbl.ElmHeight = 0;
            lbl.Padding = new Padding(10, 0, 3, 0);
            lbl.MySize = MyTextSize.Large;
            lbl.BackGround = Color.FromArgb(132, 114, 70);
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
        public override void SetExecuteInit()
        {
            StatusOfExecution = StatusOfExecutionEnum.None;
        }

        public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        {
            MyLog.WritelnBoth($"Block: {GlobalFunction.BuildingBlockComponentDecompress(GlobalFunction.GetTypeLastClass(GetType()))} ({this.DebugID})");
            var variableName = (string)GetVariableName(null);
            var variableValue = (string)GetValue(null);
            var scope = ((ElmComboBox)ElementByName(comboScope)).SelectedText;

            if (scope.Equals("Only this case", StringComparison.OrdinalIgnoreCase))
            {
                advancePanel.OnlyThisCaseScopeVariables.Add(variableName, variableValue);
            }
            else if (scope.Equals("Schedule", StringComparison.OrdinalIgnoreCase))
            {
                advancePanel.PermanentScopeVariables.Add(variableName, variableValue);
            }
            else if (scope.Equals("Permanent", StringComparison.OrdinalIgnoreCase))
            {
                advancePanel.PermanentScopeVariables.Add(variableName, variableValue);
            }
            else
            {
                MyLog.WritelnBoth("Error", "Not found Scope");
                StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                return false;
            }


            StatusOfExecution = StatusOfExecutionEnum.Finish;
            if (IsSuccessfullyStatusOfExecution())
            {
                UpdateAllDataOfArrows();
            }
            return true;
        }

        public override StatusOfExecutionEnum GetExecuteStatus()
        {
            return StatusOfExecution;
        }
        #endregion


    }
}
