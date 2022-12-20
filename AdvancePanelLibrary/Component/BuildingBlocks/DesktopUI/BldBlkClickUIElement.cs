using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.BaseStructure;
using AdvancePanelLibrary.PlayerExecutiton;
using AdvancePanelLibrary.Utility;
using AdvancePanelLibrary.Utility.Log;
using AdvancePanelLibrary.Utility.SelectUIElement;
using AutoCreateWithJson.Utility.Log;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Scrolling;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FlaUI.Core.Definitions;
using Button = FlaUI.Core.AutomationElements.Button;

namespace AdvancePanelLibrary.Component.BuildingBlocks.DesktopUI
{
    public class BldBlkClickUIElement : BasicBuildingBlock
    {
        private AutomationElement[] foundElements;
        private int currentIndex;

        #region Connector Property
        //todo:##
        #region WriteTasbihi
        private object GetNotFoundElement(object sender)
        {
            var getOutPutArrowByElementName = OutPutArrowByElementName(nameNotFound);
            if (getOutPutArrowByElementName != null)
            {
                return getOutPutArrowByElementName.ConnectorEnd != null ? true : false;
            }

            return false;
        }

        #endregion
        private object GetSelectCondition(object sender)
        {
            return GetConnectorPropertySelectElementCondition(sueSelectUIElement, sueSelectUIElement);
        }

        private object GetMethod(object sender)
        {
            return ElementByName(sueComboMethod);
        }
  
        private object GetButton(object sender)
        {
            return ElementByName(sueComboButton);
        }

        private object GetSpeed(object sender)
        {
            return ElementByName(sueSpeed);
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
            return GetConnectorPropertyPositionFoundX(foundElements, currentIndex);
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

        private object GetUseOccur(object sender)
        {
            return ElementByName(lblUseOccur);
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

        public BldBlkClickUIElement()
        {
            Width = GraphicConstant.bluildingBlockWidth;

            AddHeaderLabel();
            AddSelectUIElement();
            AddMethodMouseClick();
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
        private const string nameNotFound = "Not found";
        private void AddNotFound()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Not found";
            lbl.Name = nameNotFound;
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

        private const string sueComboMethod = "sueComboMethod";
        private const string sueComboButton = "sueComboButton";
        private const string sueSpeed = "sueSpeed";

        private void AddMethodMouseClick()
        {
            var combo = new ElmComboBox(this);
            combo.Name = sueComboMethod;
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Title = "Method";
            combo.Items.Add("Click");
            combo.Items.Add("Invoke");
            combo.SelectedText = "Click";
            Children.Add(combo);

            combo = new ElmComboBox(this);
            combo.Name = sueComboButton;
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Button";
            combo.Items.Add("Left");
            combo.Items.Add("Right");
            combo.Items.Add("Middle");
            combo.Items.Add("Double Left");
            combo.Items.Add("Double Right");
            combo.Items.Add("Double Middle");
            combo.SelectedText = "Left";
            Children.Add(combo);

            combo = new ElmComboBox(this);
            combo.Name = sueSpeed;
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Speed";
            combo.Items.Add("Slow");
            combo.Items.Add("Medium");
            combo.Items.Add("Fast");
            combo.Items.Add("Instantaneous");
            combo.SelectedText = "Medium";
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private const string sueSelectUIElement = "sueSelectUIElement";

        private void AddSelectUIElement()
        {
            var sue = new ElmSelectUIElement(this);
            sue.Name = sueSelectUIElement;
            sue.Title = "Select UI Element\r\nto click / invoke";
            sue.Padding = new Padding(15, 10, 15, 10);
            sue.ElmHeight = 70;
            sue.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetSelectCondition);
            Children.Add(sue);
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Click UI Element";
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

        //#region EXECUTOR

        //public override void SetExecuteInit()
        //{
        //    StatusOfExecution = StatusOfExecutionEnum.None;
        //}

        //public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        //{
        //    try
        //    {
        //        MyLog.WritelnBoth($"Block: {GlobalFunction.BuildingBlockComponentDecompress(GlobalFunction.GetTypeLastClass(GetType()))} ({this.DebugID})"); 
        //        foundElements = null;
        //        var timeout = (string)GetTimeout(null);
        //        TimeSpan timeSpan = GlobalFunction.ConvertToTimeSpan(timeout);
        //        var selectElementStoreable = (SelectElementStoreable)GetSelectCondition(null);

        //        var sourceElement = (AutomationElement)GetSourceElement(null);
        //        var elements = GetElementsWithConditionTimeout(globalVariablePlayer, sourceElement, selectElementStoreable, timeSpan);

        //        if (elements is null || elements.Length == 0)
        //        {
        //            StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
        //            return false;
        //        }
        //        foundElements = elements;
        //        currentIndex = 0;
        //        var occure = (ElmComboBox)GetUseOccur(null);
        //        if (occure.SelectedText == "All")
        //        {
        //            for (var i = 0; i < foundElements.Length; i++)
        //            {
        //                currentIndex = i;
        //                ElementClick(foundElements[i]);
        //            }
        //        }
        //        else
        //        {
        //            var getIndexOccure = Convert.ToInt32(occure.SelectedText);
        //            ElementClick(foundElements[getIndexOccure - 1]);
        //        }

        //        StatusOfExecution = StatusOfExecutionEnum.Finish;

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        MyLog.WritelnBoth("FinishWithError", e.Message);
        //        StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
        //        return false;
        //    }
        //    finally
        //    {
        //        if (IsSuccessfullyStatusOfExecution())
        //        {
        //            UpdateAllDataOfArrows();
        //        }
        //    }
        //}

        //public override StatusOfExecutionEnum GetExecuteStatus()
        //{
        //    return StatusOfExecution;
        //}
        //#endregion

        //public void ElementClick(AutomationElement automationElement)
        //{
        //    var method = ((ElmComboBox)GetMethod(null)).SelectedText;
        //    var mouseClick = ((ElmComboBox)GetButton(null)).SelectedText;

        //    var elementViewModel = new ElementViewModel(automationElement);
        //    var allItems = elementViewModel.AllItems;
        //    var className = SearchInDetailElement.GetClassName(allItems);
        //    if (className.Equals("Button", StringComparison.OrdinalIgnoreCase))
        //    {
        //        var btn = automationElement.AsButton();
        //        if (method == "Invoke")
        //        {
        //            btn.Invoke();
        //        }
        //        else if (method == "Click")
        //        {
        //            btn.Click();
        //        }
        //    }
        //    else if (className.Equals("RadioButton", StringComparison.OrdinalIgnoreCase))
        //    {
        //        var rb = automationElement.AsRadioButton();
        //        rb.IsChecked = true;
        //    }
        //    else if (className.Equals("DataGridCell", StringComparison.OrdinalIgnoreCase))
        //    {
        //        var cell = automationElement.AsGridCell();
        //        switch (mouseClick)
        //        {
        //            case "Left":
        //                cell.Click();
        //                break;
        //            case "Right":
        //                cell.RightClick();
        //                break;
        //            case "Middle":
        //                break;
        //            case "Double Left":
        //                cell.DoubleClick();
        //                break;
        //            case "Double Right":
        //                cell.RightDoubleClick();
        //                break;
        //            case "Double Middle":
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            switch (mouseClick)
        //            {
        //                case "Left":
        //                    automationElement.Click();
        //                    break;
        //                case "Right":
        //                    automationElement.RightClick();
        //                    break;
        //                case "Middle":
        //                    throw new Exception("Not Support");
        //                    break;
        //                case "Double Left":
        //                    //Mouse.DoubleClick(MouseButton.Left);
        //                    automationElement.DoubleClick();
        //                    break;
        //                case "Double Right":
        //                    automationElement.RightDoubleClick();
        //                    break;
        //                case "Double Middle":
        //                    throw new Exception("Not Support");
        //                    break;
        //            }
        //        }
        //        catch 
        //        {
        //            var p = automationElement.GetClickablePoint();
        //            Mouse.MoveTo(p.X, p.Y);
        //            switch (mouseClick)
        //            {
        //                case "Left":
        //                    Mouse.Click(MouseButton.Left);
        //                    break;
        //                case "Right":
        //                    Mouse.Click(MouseButton.Right);
        //                    break;
        //                case "Middle":
        //                    Mouse.Click(MouseButton.Middle);
        //                    break;
        //                case "Double Left":
        //                    Mouse.DoubleClick(MouseButton.Left);
        //                    break;
        //                case "Double Right":
        //                    Mouse.DoubleClick(MouseButton.Right);
        //                    break;
        //                case "Double Middle":
        //                    Mouse.DoubleClick(MouseButton.Middle);
        //                    break;
        //            }

        //        }
        //    }
        //}
        //todo:##
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
                    if ((bool)GetNotFoundElement(null))
                    {
                        OccureLog.RunElementNotFound(this);
                        throw new Exception("dont write");
                        //StatusOfExecution = StatusOfExecutionEnum.FinishWithErrorRunNotFound;
                        return true;
                    }
                    else
                    {
                        //
                        OccureLog.FinishWithErrorExecutorBuildingBlock(this);
                        StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                        return false;
                    }
                }
                bool resultClick = Click(elements);
                if (resultClick)
                {
                    StatusOfExecution = StatusOfExecutionEnum.Finish;
                    OccureLog.FinishExecutorBuildingBlock(this);
                    return resultClick;
                }
                else
                {
                    StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                    OccureLog.FinishWithErrorExecutorBuildingBlock(this);
                    return resultClick;
                }

            }
            catch (Exception e)
            {
                OccureLog.FinishWithErrorExecutorBuildingBlock(this, e);
                StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                return false;
            }


            //MyLog.WritelnFile("error",globalVariablePlayer.ToString());
            //MyLog.WritelnBoth("BldBlkClickUIElement");
            //foundElements = null;
            //var cse = ((SelectElementStoreable)GetSelectCondition(null)).ConditionForSelectElement;
            //var elements = cse.GetTargetElements(globalVariablePlayer.CurrentMainWindow);
            //if (elements is null || elements.Length == 0)
            //{
            //    StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
            //    return false;
            //}
            //foundElements = elements;
            //var elementViewModel = new ElementViewModel(foundElements[0]);
            //var allItems = elementViewModel.AllItems;
            //var className = SearchInDetailElement.GetClassName(allItems);
            //if (className.Equals("Button", StringComparison.OrdinalIgnoreCase))
            //{
            //    var btn = elements[0].AsButton();
            //    btn.Invoke();

            //}
            //else if (className.Equals("RadioButton", StringComparison.OrdinalIgnoreCase))
            //{
            //    var rb = elements[0].AsRadioButton();
            //    rb.IsChecked = true;
            //}
            //else if (className.Equals("DataGridCell", StringComparison.OrdinalIgnoreCase))
            //{
            //    var cell = elements[0].AsGridCell();
            //    var mouseClick = ((ElmEditBox)ElementByName(sueComboButton)).Text;
            //    switch (mouseClick)
            //    {
            //        case "Left":
            //            cell.Click();
            //            break;
            //        case "Right":
            //            cell.RightClick();
            //            break;
            //        case "Middle":
            //            break;
            //        case "Double Left":
            //            cell.DoubleClick();
            //            break;
            //        case "Double Right":
            //            cell.RightDoubleClick();
            //            break;
            //        case "Double Middle":
            //            break;
            //    }

            //}


            //StatusOfExecution = StatusOfExecutionEnum.Finish;
            //if (StatusOfExecution == StatusOfExecutionEnum.Finish)
            //{
            //    UpdateAllDataOfArrows();
            //}
            //return true;
        }
        //result thread 
        private delegate bool ActionMethod<T>(T item);
        private ActionMethod<AutomationElement> action;
        private bool Click(AutomationElement[] automationElements1)
        {
            try
            {
                var occure = (ElmComboBox)GetUseOccur(null);
                var getIndexOccure = Convert.ToInt32(occure.SelectedText);
                AutomationElement selectedElement = automationElements1[getIndexOccure - 1];
                var selectedComboBox = (ElmComboBox)GetMethod(null);
                string selectedItemText = selectedComboBox.SelectedText;
                if (string.Equals(selectedItemText, "Click", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (selectedElement.ControlType)
                    {
                        case ControlType.Button:
                            action = ClickButton;
                            break;
                        case ControlType.CheckBox:
                            action = ClickButton;
                            break;
                        case ControlType.Custom:
                            action = ClickButton;
                            break;
                        case ControlType.RadioButton:
                            action = ClickButton;
                            break;
                        case ControlType.HeaderItem:
                            action = ClickButton;
                            break;
                        case ControlType.ScrollBar:
                            action = ClickForVerticalScroll;
                            break;
                    }
                }
                else if (string.Equals(selectedItemText, "Invoke", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (selectedElement.ControlType)
                    {
                        case ControlType.Button:
                            action = ClickInvoke;
                            break;
                    }

                }
                if (action != null)
                {
                   return action(selectedElement);
                }
                else
                {
                    OccureLog.ErrorInClick(this);
                    return false;
                }
            }
            catch (Exception e)
            {
                OccureLog.ErrorInClick(this, e);
                return false;
            }
        }

        private bool ClickForVerticalScroll(AutomationElement automationElement)
        {
            try
            {
                var asVerticalScrollBar = automationElement.AsVerticalScrollBar();
                asVerticalScrollBar.ScrollDown();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
          
        }

        private bool ClickInvoke(AutomationElement automationElement)
        {
            try
            {
                var asButton = automationElement.AsButton();
                asButton.Invoke();
                return true;
            }
            catch (Exception e)
            {
                OccureLog.ErrorInClick(this, e);
                return false;
            }

        }

        private bool ClickButton(AutomationElement automationElement)
        {
            try
            {
                ElmComboBox comboBox = (ElmComboBox)GetButton(null);
                string selectedText = comboBox.SelectedText;
                bool resultFocus = CanFocus(automationElement);
                if (resultFocus)
                {
                    automationElement.Focus();
                }
                switch (selectedText)
                {
                    case "Left":
                        automationElement.Click();
                        break;
                    case "Right":
                        automationElement.RightClick();
                        break;
                    case "Middle":
                        break;
                    case "Double Left":
                        automationElement.DoubleClick();
                        break;
                    case "Double Right":
                        automationElement.RightDoubleClick();
                        break;
                    case "Double Middle":
                        break;
                }

                return true;
            }
            catch (Exception e)
            {
                OccureLog.ErrorInClick(this, e);
                return false;
            }
        }

        private bool CanFocus(AutomationElement automationElement)
        {
            if (automationElement.ControlType == ControlType.HeaderItem)
            {
                return false;
            }

            return true;
        }

        private AutomationElement[] automationElements;
        //private AutomationElement[] GetElements(GlobalVariablePlayer globalVariablePlayer)
        //{
        //    try
        //    {
        //        var selectElementStoreable = (SelectElementStoreable)GetSelectCondition(null);
        //        if (selectElementStoreable != null)
        //        {
        //            var conditionElement = selectElementStoreable.ConditionForSelectElement;
        //            AutomationElement[] targetElements = conditionElement.GetTargetElements(globalVariablePlayer.CurrentMainWindow);
        //            if (targetElements == null || targetElements.Length == 0)
        //            {
        //                OccureLog.ErrorToFindTargetElement(this);
        //                return null;

        //            }
        //            else
        //            {
        //                return targetElements;
        //            }
        //        }
        //        else
        //        {
        //            OccureLog.ErrorToFindTargetElement(this);
        //            return null;
        //        }

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

    }

}


