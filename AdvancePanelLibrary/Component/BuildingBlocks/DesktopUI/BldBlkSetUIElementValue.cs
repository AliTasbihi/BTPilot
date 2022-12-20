using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.BaseStructure;
using AdvancePanelLibrary.PlayerExecutiton;
using AdvancePanelLibrary.Utility;
using AdvancePanelLibrary.Utility.Log;
using AutoCreateWithJson.Utility.Log;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;

namespace AdvancePanelLibrary.Component.BuildingBlocks.DesktopUI
{
    public class BldBlkSetUIElementValue : BasicBuildingBlock
    {
        private AutomationElement[] foundElements;
        private int currentIndex;
        //todo:##
        //کانکتر پراپرتی ها اضافه شده

        #region addConnectroPropertyTasbihi

        private object IsNotFoundElementConnect(object sender)
        {
            var getOutPutArrowByElementName = OutPutArrowByElementName(nameNotFound);
            if (getOutPutArrowByElementName != null)
            {
                return getOutPutArrowByElementName.ConnectorEnd != null ? true : false;
            }

            return false;
        }
        private object GetOccure(object sender) => ElementByName(lblUseOccur);

        #endregion

        #region Connector Property  
        private object GetSelectCondition(object sender)
        {
            return GetConnectorPropertySelectElementCondition(sueSelectUIElement, sueSelectUIElement);
        }
        private object GetTextValue(object sender)
        {
            return GetConnectorPropertyEditBox(edTextValue, edTextValue);
        }
        private object GetFoundElement(object sender)
        {
            return GetConnectorPropertyFoundElement(foundElements, currentIndex);
        }
        private object GetPositionFound(object sender)
        {
            return GetConnectorPropertyPositionFound(foundElements);
        }
        private object GetPositionFoundX(object sender)
        {
            return GetConnectorPropertyPositionFoundX(foundElements,currentIndex);
        }
        private object GetPositionFoundY(object sender)
        {
            return GetConnectorPropertyPositionFoundY(foundElements);
        }
        private object GetAreaFound(object sender)
        {
            return GetConnectorPropertyAreaFound(foundElements);
        }
        private object GetAreaFoundX(object sender)
        {
            return GetConnectorPropertyAreaFoundX(foundElements);
        }
        private object GetAreaFoundY(object sender)
        {
            return GetConnectorPropertyAreaFoundY(foundElements);
        }
        private object GetAreaFoundWidth(object sender)
        {
            return GetConnectorPropertyAreaFoundWidth(foundElements);
        }
        private object GetAreaFoundHeight(object sender)
        {
            return GetConnectorPropertyAreaFoundHeight(foundElements);
        }
        private object GetSourceElement(object sender)
        {
            var arrow = InputArrowByElementName(lblSourceElement);
            return arrow != null ? arrow.TransferData : null;
        }
        private object GetCount(object sender)
        {
            return foundElements != null ? foundElements.Length : 0;
        }
        private object GetCurrentIndex(object sender)
        {
            return currentIndex;
        }
        private object GetDefaultTimeout(object sender)
        {
            return GetConnectorPropertyCheckBox(chkDefaultTimeout, chkDefaultTimeout);
        }
        private object GetTimeout(object sender)
        {
            return GetConnectorPropertyEditBox(edTimeout, edTimeout);
        }
        private object GetAwaitNoChanges(object sender)
        {
            return GetConnectorPropertyCheckBox(chkAwaitNoChanges, chkAwaitNoChanges);
        }
        private object GetAwaitTimeout(object sender)
        {
            return 0;
        }
        #endregion

        public BldBlkSetUIElementValue()
        {
            Width = GraphicConstant.bluildingBlockWidth;
            AddHeaderLabel();
            AddSelectUIElement();
            AddMethod();
            AddTextValue();
            AddTextFieldAddField();
            AddType();
            AddFoundElement();
            AddNotFound();
            AddDropDownPositionFound();
            AddDropDownAreaFound();
            AddSourceElement();
            AddUseOccur();
            AddCurrentIndex();
            AddCompleted();
            AddCount();
            AddDefaultTimeout();
            AddTimeout();
            AddScrollToFind();
            AddAwaitNoChanges();
            AddAwaitTimeout();
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

        private void AddAwaitTimeout()
        {
            var edt = new ElmEditBox(this);
            edt.Visible = false;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 2);
            edt.Title = "Await Timeout (sec)";
            edt.TitlePosition = ContentAlignment.MiddleLeft;
            edt.Text = "10";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetAwaitTimeout);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private const string chkAwaitNoChanges = "chkAwaitNoChanges";
        private void AddAwaitNoChanges()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkAwaitNoChanges;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(10, 2, 10, 2);
            chk.Title = "Await no changes";
            chk.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetAwaitNoChanges);
            Children.Add(chk);

            Children.Add(new ElmSeparateLine());
        }

        private void AddScrollToFind()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 2, 10, 2);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Scroll to find";
            combo.Items.Add("None");
            combo.Items.Add("Infinity scroll");
            combo.SelectedText = "None";
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private const string edTimeout = "edTimeout";
        private void AddTimeout()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edTimeout;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.Title = "Timeout (sec)";
            edt.TitlePosition = ContentAlignment.MiddleLeft;
            edt.Text = "10";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetTimeout);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private const string chkDefaultTimeout = "chkDefaultTimeout";
        private void AddDefaultTimeout()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkDefaultTimeout;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(5, 0, 10, 0);
            chk.Title = "Default timeout";
            chk.AddTwoConnector(Color.Blue, 0, 1, outputDataFunction: GetDefaultTimeout);
            Children.Add(chk);

            Children.Add(new ElmSeparateLine());
        }

        private void AddCount()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Count";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetCount);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private const string lblCompleted = "lblCompleted";
        private void AddCompleted()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblCompleted;
            lbl.Visible = false;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Completed";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Green, 0, outputDataFunction: GetCurrentIndex);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }
        private const string lblCurrentIndex = "lblCurrentIndex";
        private void AddCurrentIndex()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblCurrentIndex;
            lbl.Visible = false;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Current index";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetCurrentIndex);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private const string lblUseOccur = "lblUseOccur";
        private void AddUseOccur()
        {
            var combo = new ElmComboBox(this);
            combo.Name = lblUseOccur;
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 2, 10, 2);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Use occure.";
            combo.Items.Add("1");
            combo.Items.Add("2");
            combo.Items.Add("3");
            combo.Items.Add("4");
            combo.Items.Add("5");
            combo.Items.Add("All");
            combo.SelectedText = "1";
            Children.Add(combo);
            Children.Add(new ElmSeparateLine());

            combo.TheClick = ComboUseOccurClick;
        }

        private void ComboUseOccurClick(object sender, MouseEventArgs e)
        {
            var combo = sender as ElmComboBox;
            combo.SelectItemByClick(sender, e);
            var currentIndex = (ElmLabel)ElementByName(lblCurrentIndex);
            var completed = (ElmLabel)ElementByName(lblCompleted);
            if (combo.SelectedText == "All")
            {
                currentIndex.Visible = true;
                completed.Visible = true;
            }
            else
            {
                currentIndex.Visible = false;
                completed.Visible = false;
            }
        }

        private const string lblSourceElement = "lblSourceElement";
        private void AddSourceElement()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblSourceElement;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Source element";
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetSourceElement);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private void AddDropDownAreaFound()
        {
            var drp = new ElmDropDown(this);
            drp.IsNecessaryToView = 0;
            drp.Padding = new Padding(10, 0, 10, 0);
            drp.Title = "Area found";
            drp.Alignment = ContentAlignment.MiddleRight;
            drp.ExpandItems = false;
            drp.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetAreaFound);
            Children.Add(drp);

            var lbl = new ElmLabel(drp);
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "X";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.Parent = drp;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetAreaFoundX);
            drp.Children.Add(lbl);

            lbl = new ElmLabel(drp);
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Y";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.Parent = drp;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetAreaFoundY);
            drp.Children.Add(lbl);

            lbl = new ElmLabel(drp);
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Width";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.Parent = drp;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetAreaFoundWidth);
            drp.Children.Add(lbl);

            lbl = new ElmLabel(drp);
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Height";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.Parent = drp;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetAreaFoundHeight);
            drp.Children.Add(lbl);

            Children.Add(new ElmSeparateLine());

        }

        private void AddDropDownPositionFound()
        {
            var drp = new ElmDropDown(this);
            drp.IsNecessaryToView = 0;
            drp.Padding = new Padding(10, 0, 10, 0);
            drp.Title = "Position found";
            drp.Alignment = ContentAlignment.MiddleRight;
            drp.ExpandItems = false;
            drp.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetPositionFound);
            Children.Add(drp);

            var lbl = new ElmLabel(drp);
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "X";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.Parent = drp;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetPositionFoundX);
            drp.Children.Add(lbl);

            lbl = new ElmLabel(drp);
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Y";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetPositionFoundY);
            lbl.Parent = drp;
            drp.Children.Add(lbl);

            Children.Add(new ElmSeparateLine());
        }
        private const string nameNotFound = "Notfound";
        private void AddNotFound()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Name = nameNotFound;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Not found";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Green, 0);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private const string lblFoundElement = "lblFoundElement";
        private void AddFoundElement()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblFoundElement;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Found element";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetFoundElement);
            Children.Add(lbl);

            Children.Add(new ElmSeparateLine());
        }

        private void AddType()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 8, 10, 2);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Type";
            combo.Items.Add("Text");
            combo.Items.Add("-");
            combo.Items.Add("-*");
            combo.SelectedText = "Text";
            combo.ElmHeight = 30;
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private void AddTextFieldAddField()
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

        private const string edTextValue = "edTextValue";
        private void AddTextValue()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edTextValue;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.Title = "Text value";
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Text = "";
            edt.AddTwoConnector(Color.Blue, 1, 0, -17, outputDataFunction: GetTextValue);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private void AddMethod()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Title = "Method";
            combo.Items.Add("Set Value");
            combo.Items.Add("-");
            combo.Items.Add("-");
            combo.SelectedText = "Set Value";
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private const string sueSelectUIElement = "sueSelectUIElement";
        private void AddSelectUIElement()
        {
            var sue = new ElmSelectUIElement(this);
            sue.Name = sueSelectUIElement;
            sue.Title = "Select UI Element\r\nto set value on";
            sue.Padding = new Padding(15, 10, 15, 10);
            sue.ElmHeight = 70;
            sue.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetSelectCondition);
            Children.Add(sue);
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Set UI Element Value";
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
        //public override void SetExecuteInit()
        //{
        //    StatusOfExecution = StatusOfExecutionEnum.None;
        //}

        //public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        //{
        //    MyLog.WritelnBoth($"Block: {GlobalFunction.BuildingBlockComponentDecompress(GlobalFunction.GetTypeLastClass(GetType()))} ({this.DebugID})");
        //    //var cse = ((ElmSelectUIElement)ElementByName(sueSelectUIElement)).SelectElementStoreable.ConditionForSelectElement;
        //    //var elements = cse.GetTargetElements(globalVariablePlayer.CurrentMainWindow);
        //    var timeout = (string)GetTimeout(null);
        //    TimeSpan timeSpan = GlobalFunction.ConvertToTimeSpan(timeout);
        //    var selectElementStoreable = (SelectElementStoreable)GetSelectCondition(null);

        //    var sourceElement = (AutomationElement)GetSourceElement(null);
        //    var elements = GetElementsWithConditionTimeout(globalVariablePlayer, sourceElement, selectElementStoreable, timeSpan);

        //    if (elements is null || elements.Length == 0)
        //    {
        //        StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
        //        return false;
        //    }
        //    foundElements = elements;
        //    currentIndex = 0;

        //    globalVariablePlayer.CurrentMainWindow.SetForeground();
        //    var txtBox = elements[0].AsTextBox();
        //    txtBox.Text = (string)GetTextValue(null);


        //    StatusOfExecution = StatusOfExecutionEnum.Finish;
        //    if (IsSuccessfullyStatusOfExecution())
        //    {
        //        UpdateAllDataOfArrows();
        //    }

        //    return true;

        //}

        //public override StatusOfExecutionEnum GetExecuteStatus()
        //{
        //    return StatusOfExecution;
        //}
        #endregion
        //todo:##
        //روش دیگر برای اکسکیوتر
        #region write Tasbihi
        #region EXECUTOR
        public override void SetExecuteInit()
        {
            StatusOfExecution = StatusOfExecutionEnum.None;
        }

        public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        {
            OccureLog.StartExecutorBuildingBlock(this);
            try
            {
                var timeout = (string)GetTimeout(null);
                TimeSpan timeSpan = GlobalFunction.ConvertToTimeSpan(timeout);
                var selectElementStoreable = (SelectElementStoreable)GetSelectCondition(null);
                var sourceElement = (AutomationElement)GetSourceElement(null);
                var elements = GetElementsWithConditionTimeout(globalVariablePlayer, sourceElement, selectElementStoreable, timeSpan);
                if (elements == null || elements.Length == 0)
                {
                    //for eleman not found
                    if ((bool)IsNotFoundElementConnect(null))
                    {
                        OccureLog.RunElementNotFound(this);
                        throw new Exception("write this text");
                        //StatusOfExecution = StatusOfExecutionEnum.FinishWithErrorRunNotFound;
                        return true;
                    }
                    else
                    {
                        //
                        OccureLog.FinishWithErrorExecutorBuildingBlock(this);
                        StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                        return true;
                    }
                }

                bool resultSet = Set(elements);
                if (resultSet)
                {
                    StatusOfExecution = StatusOfExecutionEnum.Finish;
                    OccureLog.FinishExecutorBuildingBlock(this);
                    UpdateAllDataOfArrows();
                    return resultSet;
                }
                else
                {
                    StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                    OccureLog.FinishWithErrorExecutorBuildingBlock(this);
                    return resultSet;
                }

            }
            catch (Exception e)
            {
                StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                OccureLog.FinishWithErrorExecutorBuildingBlock(this);
                return false;
            }


            //MyLog.WritelnBoth("SetUIElementValue");
            //var cse = ((ElmSelectUIElement)ElementByName(sueSelectUIElement)).SelectElementStoreable.ConditionForSelectElement;
            //var elements = cse.GetTargetElements(globalVariablePlayer.CurrentMainWindow);
            //if (elements is null || elements.Length == 0)
            //{
            //    StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
            //    return false;
            //}

            //globalVariablePlayer.CurrentMainWindow.SetForeground();
            //var txtBox = elements[0].AsTextBox();
            //txtBox.Text = (string)GetTextValue(null);


        }

        private delegate bool ActionMethod<T>(T item);
        private ActionMethod<AutomationElement> action;
        private bool Set(AutomationElement[] automationElements1)
        {
            var occure = (ElmComboBox)GetOccure(null);
            var getIndexOccure = Convert.ToInt32(occure.SelectedText);
            AutomationElement selectedElement = automationElements1[getIndexOccure - 1];
            try
            {
                switch (selectedElement.ControlType)
                {
                    case ControlType.Text:
                        action = WriteForTextBox;
                        break;
                    case ControlType.Edit:
                        action = WriteForEditBox;
                        //Write(selectedElement);
                        break;
                    case ControlType.DataGrid:
                        action = Write;
                        break;
                    case ControlType.Calendar:
                        action = Write;
                        break;

                }
                //
                if (action != null)
                {
                  return  action(selectedElement);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                OccureLog.ErrorInSetValue(this, e);
                return false;
            }

        }

        private bool WriteForEditBox(AutomationElement selectedElement)
        {
            var textValue = (string)GetTextValue(null);
            if (textValue != string.Empty && textValue != null)
            {
                try
                {
                    if (selectedElement.IsEnabled)
                    {
                        selectedElement.Focus();
                        using (Keyboard.Pressing(VirtualKeyShort.CONTROL))
                        {
                            Keyboard.Press(VirtualKeyShort.KEY_A);
                        }
                        Keyboard.Release(VirtualKeyShort.CONTROL);
                        Keyboard.Press(VirtualKeyShort.BACK);
                        if (textValue != String.Empty || textValue != null)
                        {
                            Keyboard.Type(textValue);
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    
                    OccureLog.ErrorInSetValue(this, e);
                    return false;
                }
            }

            return true;
        }

        private bool WriteForTextBox(AutomationElement automation)
        {
            
            var asTextBox = automation.AsTextBox();
            var textValue = (string)GetTextValue(null);
            if (textValue != string.Empty && textValue != null)
            {
                try
                {
                    if (asTextBox.IsReadOnly == false)
                    {
                        asTextBox.Focus();
                        using (Keyboard.Pressing(VirtualKeyShort.CONTROL))
                        {
                            Keyboard.Press(VirtualKeyShort.KEY_A);
                        }
                        Keyboard.Release(VirtualKeyShort.CONTROL);
                        Keyboard.Press(VirtualKeyShort.BACK);
                        if (textValue != String.Empty || textValue != null)
                        {
                            Keyboard.Type(textValue);
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    
                    OccureLog.ErrorInSetValue(this, e);
                    return false;
                }

                return false;
            }

            return true;
        }

        private bool Write(AutomationElement automationElement)
        {
            var textValue = (string)GetTextValue(null);
            if (textValue != string.Empty && textValue != null)
            {
                try
                {

                    automationElement.Focus();
                    Keyboard.Type(textValue);
                    return true;

                }
                catch (Exception e)
                {
                    
                    OccureLog.ErrorInSetValue(this, e);
                    return false;
                }

            }

            return true;


        }
        //todo:??why return come back to override and dont work
        //private AutomationElement[] GetElements(GlobalVariablePlayer globalVariablePlayer)
        //{
        //    //todo:##write filter and time manager for find target eleman
        //    try
        //    {
        //        //todo$$:error coreCode for select get condtion
        //        var condition = ((SelectElementStoreable)GetSelectCondition(null));
        //        if (condition != null)
        //        {
        //            var conditionConditionForSelectElement = condition.ConditionForSelectElement;
        //            if (conditionConditionForSelectElement == null)
        //            {
        //                OccureLog.ErrorToFindTargetElement(this);
        //                return null;
        //            }
        //            else
        //            {

        //                var targetElements =
        //                    conditionConditionForSelectElement.GetTargetElements(globalVariablePlayer.CurrentMainWindow);
        //                if (targetElements == null || targetElements.Length == 0)
        //                {
        //                    OccureLog.ErrorToFindTargetElement(this);
        //                    return null;
        //                }
        //                else
        //                {

        //                    return targetElements;
        //                }
        //            }

        //        }
        //        else
        //        {
        //            OccureLog.ErrorToFindTargetElement(this);
        //            return null;
        //        }
        //        //ConditionForSelectElement conditionForSelectElement = cse.SelectElementStoreable.ConditionForSelectElement;
        //        ////nead filter for time scroll position found area found
        //        //AutomationElement[] targetElements = conditionForSelectElement.GetTargetElements(globalVariablePlayer.CurrentMainWindow);

        //    }
        //    catch (Exception e)
        //    {
        //        OccureLog.ErrorToFindTargetElement(this, e);
        //        return null;
        //    }

        //}

        public override StatusOfExecutionEnum GetExecuteStatus()
        {
            return StatusOfExecution;
        }
        #endregion



        #endregion
    }
}
