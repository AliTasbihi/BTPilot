using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Utility;
using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancePanelLibrary.Component.BaseStructure;
using AdvancePanelLibrary.PlayerExecutiton;
using AutoCreateWithJson.Utility.Log;
using FlaUI.Core.Definitions;
using ComboBox = FlaUI.Core.AutomationElements.ComboBox;
using ListBox = FlaUI.Core.AutomationElements.ListBox;
using System.Xml.Linq;

namespace AdvancePanelLibrary.Component.BuildingBlocks.DesktopUI
{
    public class BldBlkSelectUIElement : BasicBuildingBlock
    {
        private AutomationElement[] foundElements;
        private int currentIndex;

        #region Connector Property
        //todo:##
        #region writeTasbihi

        private object IsNotFoundElementConnect(object sender)
        {
            var getOutPutArrowByElementName = OutPutArrowByElementName(nameNotFound);
            if (getOutPutArrowByElementName != null)
            {
                return getOutPutArrowByElementName.ConnectorEnd != null ? true : false;
            }

            return false;

        }
        private object GetFilter(object sender) => ElementByName(nameFilter);
        private object GetScroll(object sender) => ElementByName(ScName);
        private object GetOccure(object sender) => ElementByName(lblUseOccur);

        private object GetTextValue(object sender)
        {
            return GetConnectorPropertyEditBox(edTextValue, edTextValue);
        }
        private object GetMethod(object sender) => ElementByName(NameMethod);
        #endregion

        private object GetSelectCondition(object sender)
        {
            return GetConnectorPropertySelectElementCondition(sueSelectUIElement, sueSelectUIElement);
        }
        private object GetIsCaseSensitive(object sender)
        {
            return GetConnectorPropertyCheckBox(chkIsCaseSensitive, chkIsCaseSensitive);
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

        public BldBlkSelectUIElement()
        {
            Width = GraphicConstant.bluildingBlockWidth;
            AddHeaderLabel();
            AddSelectUIElement();
            AddMethod();
            AddFilter();
            AddtextValue();
            AddIsCaseSensitive();
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
        private const string edTextValue = "edTextValue";
        private void AddtextValue()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edTextValue;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.Title = "Value";
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Text = "";
            //
            edt.AddTwoConnector(Color.Blue, 1, 0, -17, outputDataFunction: GetTextValue);
            Children.Add(edt);
            Children.Add(new ElmSeparateLine());
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
        private const string ScName = "Scroll to find";
        private void AddScrollToFind()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 2, 10, 2);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Scroll to find";
            combo.Name = ScName;
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
        //todo:##
        //اضافه کردن اسم برای المان
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

        private const string chkIsCaseSensitive = "chkIsCaseSensitive";
        private void AddIsCaseSensitive()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkIsCaseSensitive;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(5, 0, 10, 0);
            chk.Title = "Is case sensitive";
            chk.AddTwoConnector(Color.Blue, 0, 1, outputDataFunction: GetIsCaseSensitive);
            Children.Add(chk);

            Children.Add(new ElmSeparateLine());
        }
        private const string nameFilter = "cmbFilter";
        private void AddFilter()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 2, 10, 2);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Filter";
            combo.Name = nameFilter;
            combo.Items.Add("No Filter");
            combo.Items.Add("Equals");
            combo.Items.Add("Contains");
            combo.Items.Add("Starts with");
            combo.Items.Add("End with");
            combo.SelectedText = "No Filter";
            Children.Add(combo);
            Children.Add(new ElmSeparateLine());
        }
        private const string NameMethod = "Method";
        private void AddMethod()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Name = NameMethod;
            combo.Title = "Method";
         
            combo.Items.Add("Select");
            combo.Items.Add("Add to selection");
            combo.Items.Add("Remove from selection");
            combo.SelectedText = "Select";
            Children.Add(combo);
            Children.Add(new ElmSeparateLine());
        }

        private const string sueSelectUIElement = "sueSelectUIElement";
        private void AddSelectUIElement()
        {
            var sue = new ElmSelectUIElement(this);
            sue.Name = sueSelectUIElement;
            sue.Title = "Select UI Element\r\nto select inside";
            sue.Padding = new Padding(15, 10, 15, 10);
            sue.ElmHeight = 70;
            sue.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetSelectCondition);
            Children.Add(sue);
            Children.Add(new ElmSeparateLine());
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Select UI Element";
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
        #region Executor
        public override void SetExecuteInit()
        {
            this.StatusOfExecution = StatusOfExecutionEnum.None;
        }

        private object sourceElement;
        public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        {
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
                        throw new Exception("این قسمت ننوشتی");
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

                bool resultSelect = Select(elements);
                if (resultSelect)
                {
                    StatusOfExecution = StatusOfExecutionEnum.Finish;
                    OccureLog.FinishExecutorBuildingBlock(this);
                    UpdateAllDataOfArrows();
                    return resultSelect;
                }
                else
                {
                    StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                    OccureLog.FinishWithErrorExecutorBuildingBlock(this);
                    return resultSelect;
                }


            }
            catch (Exception e)
            {
                OccureLog.ErrorInSelect(this, e);
                StatusOfExecution = StatusOfExecutionEnum.FinishWithError;
                return false;
            }


        }
        private delegate bool ActionMethod<T>(T item);
        private  ActionMethod<AutomationElement> action;
        private bool Select(AutomationElement[] targetElements)
        {
            try
            {
                var occure = (ElmComboBox)GetOccure(null);
                var getIndexOccure = Convert.ToInt32(occure.SelectedText);
                AutomationElement selectedElement = targetElements[getIndexOccure - 1];
                switch (selectedElement.ControlType)
                {
                    case ControlType.ComboBox:
                        action = SelectItemForComboBox;
                        break;
                    case ControlType.ListItem:
                        action = SelectItemForListBoxItem;
                        break;
                    case ControlType.List:
                        action = SelectItemForListBox;
                        break;

                }
                if (action != null)
                {
                    bool resultAction = action(selectedElement);
                    return resultAction;
                }
                else
                {
                    OccureLog.ErrorInSelect(this);
                    return false;
                }
            }
            catch (Exception e)
            {
                OccureLog.ErrorInSelect(this, e);
            }


            return false;
        }

        private bool SelectItemForListBox(AutomationElement automationElement)
        {
            try
            {
                var asListBox = automationElement.AsListBox();
              
                if (!CanSelect(asListBox))
                {
                    //todo:exception01
                    //OccureLog.ErrorInSelect(this);
                    return false;
                }

                var getScroll = (ElmComboBox)GetScroll(null);
                var getFilter = (ElmComboBox)GetFilter(null);
                var getMethod = (ElmComboBox)GetMethod(null);
                var textValue = (string)GetTextValue(null);
                if (getFilter.SelectedText == "No Filter")
                {
                    //todo:#need to rewrite for option that show we can select comobox
                    //todo:#need to rewirte for option that only when use linq select comobox item
                    if (asListBox.IsEnabled)
                    {
                        if (getMethod.SelectedText == "Select")
                        {
                            asListBox.Click();
                            return true;
                        }
                    }
                }

                else if (getFilter.SelectedText == "Equals")
                {
                    if (asListBox.IsEnabled)
                    {
                        if (((ElmComboBox)GetScroll(null)).SelectedText == "Infinity scroll")
                        {
                            var item = asListBox.Items.Where(q => q.Text.Equals(textValue, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                            item.ScrollIntoView();
                        }
                        if (getMethod.SelectedText == "Select")
                        {
                            asListBox.Select(textValue);
                            return true;
                        }
                        else if (getMethod.SelectedText == "Add to selection")
                        {
                            asListBox.AddToSelection(textValue);
                            return true;
                        }
                        else if (getMethod.SelectedText == "Remove from selection")
                        {
                            if (asListBox.SelectedItems.Any(q => q.Text == textValue))
                            {
                                asListBox.RemoveFromSelection(textValue);
                                return true;
                            }
                        }

                    }
                }
                else if (getFilter.SelectedText == "Contains")
                {
                    if (asListBox.IsEnabled)
                    {
                        if (((ElmComboBox)GetScroll(null)).SelectedText == "Infinity scroll")
                        {
                            var item = asListBox.Items.Where(q => q.Text.Contains(textValue, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                            item.ScrollIntoView();
                        }
                        if (getMethod.SelectedText == "Select")
                        {
                            var selectedItem = asListBox.Items
                                .Where(q => q.Text.Contains(textValue, StringComparison.CurrentCultureIgnoreCase))
                                .FirstOrDefault();
                            var listBoxItem = asListBox.Select(selectedItem.Text);
                        }
                        else if (getMethod.SelectedText == "Add to selection")
                        {
                            var selectedItem = asListBox.Items
                                .Where(q => q.Text.Contains(textValue, StringComparison.CurrentCultureIgnoreCase))
                                .FirstOrDefault();
                            asListBox.AddToSelection(selectedItem.Text);
                            return true;
                        }
                        else if (getMethod.SelectedText == "Remove from selection")
                        {
                            if (asListBox.SelectedItems.Any(q => q.Text == textValue))
                            {
                                var selectedItem = asListBox.Items
                                    .Where(q => q.Text.Contains(textValue, StringComparison.CurrentCultureIgnoreCase))
                                    .FirstOrDefault();
                                asListBox.RemoveFromSelection(selectedItem.Text);
                                return true;
                            }
                        }

                    }
                }
                else if (getFilter.SelectedText == "Starts with")
                {
                    if (asListBox.IsEnabled)
                    {
                        //option for scroll infinity
                        if (((ElmComboBox)GetScroll(null)).SelectedText == "Infinity scroll")
                        {
                            var item = asListBox.Items.Where(q => q.Text.StartsWith(textValue, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                            item.ScrollIntoView();
                        }
                        if (getMethod.SelectedText == "Select")
                        {
                            var selectedItem = asListBox.Items
                                .Where(q => q.Text.StartsWith(textValue, StringComparison.CurrentCultureIgnoreCase))
                                .FirstOrDefault();
                            asListBox.Select(selectedItem.Text);
                        }
                        else if (getMethod.SelectedText == "Add to selection")
                        {
                            var selectedItem = asListBox.Items
                                .Where(q => q.Text.StartsWith(textValue, StringComparison.CurrentCultureIgnoreCase))
                                .FirstOrDefault();
                            asListBox.AddToSelection(selectedItem.Text).ScrollIntoView();
                            return true;
                        }
                        else if (getMethod.SelectedText == "Remove from selection")
                        {
                            if (asListBox.SelectedItems.Any(q => q.Text == textValue))
                            {
                                var selectedItem = asListBox.Items
                                    .Where(q => q.Text.StartsWith(textValue, StringComparison.CurrentCultureIgnoreCase))
                                    .FirstOrDefault();
                                asListBox.RemoveFromSelection(selectedItem.Text);
                                return true;
                            }
                        }
                    }
                }
                else if (getFilter.SelectedText == "End with")
                {
                    if (asListBox.IsEnabled)
                    {
                        if (((ElmComboBox)GetScroll(null)).SelectedText == "Infinity scroll")
                        {
                            var item = asListBox.Items.Where(q => q.Text.EndsWith(textValue, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                            item.ScrollIntoView();
                        }
                        if (getMethod.SelectedText == "Select")
                        {
                            var selectedItem = asListBox.Items
                                .Where(q => q.Text.EndsWith(textValue, StringComparison.CurrentCultureIgnoreCase))
                                .FirstOrDefault();
                            asListBox.Select(selectedItem.Text);
                        }
                        else if (getMethod.SelectedText == "Add to selection")
                        {
                            var selectedItem = asListBox.Items
                                .Where(q => q.Text.EndsWith(textValue, StringComparison.CurrentCultureIgnoreCase))
                                .FirstOrDefault();
                            asListBox.AddToSelection(selectedItem.Text);
                            return true;
                        }
                        else if (getMethod.SelectedText == "Remove from selection")
                        {
                            if (asListBox.SelectedItems.Any(q => q.Text.EndsWith(textValue, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                var selectedItem = asListBox.Items
                                    .Where(q => q.Text.EndsWith(textValue, StringComparison.CurrentCultureIgnoreCase))
                                    .FirstOrDefault();
                                asListBox.RemoveFromSelection(selectedItem.Text);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //todo:erro exception 02
                //OccureLog.ErrorInSelect(this,e);
                return false;
            }

            return false;
        }
        private bool SelectItemForListBoxItem(AutomationElement automation)
        {
            try
            {
                var asListBoxItem = automation.AsListBoxItem();
                var methodComboBox = (ElmComboBox)GetMethod(null);
                var selectedTextMethod = methodComboBox.SelectedText;
                if (selectedTextMethod == "Select")
                {
                    if (asListBoxItem.IsEnabled)
                    {
                        asListBoxItem.Select();
                        return true;
                    }

                }
                else if (selectedTextMethod == "Add to selection")
                {
                    if (asListBoxItem.IsEnabled)
                    {
                        asListBoxItem.AddToSelection();
                        return true;
                    }
                }
                else if (selectedTextMethod == "Remove from selection")
                {
                    if (asListBoxItem.IsEnabled && asListBoxItem.IsSelected)
                    {
                        asListBoxItem.RemoveFromSelection();
                        return true;
                    }
                }

            }
            catch (Exception e)
            {
                //OccureLog.ErrorInSelect(this);
                return false;
            }

            return false;
        }

        private bool SelectItemForComboBox(AutomationElement element)
        {
            try
            {
                var selectedElement = element.AsComboBox();

                if (CanSelect(selectedElement) == false)
                {
                    return false;
                }

                var getFilter = (ElmComboBox)GetFilter(null);
                var textValue = (string)GetTextValue(null);
                if (getFilter.SelectedText == "No Filter")
                {
                    //todo:#need to rewrite for option that show we can select comobox
                    //todo:#need to rewirte for option that only when use linq select comobox item
                    if (true)
                    {
                        selectedElement.Expand();
                        return true;
                    }

                }

                else if (getFilter.SelectedText == "Equals")
                {
                    if (true)
                    {
                        selectedElement.Select(textValue);
                        selectedElement.Collapse();
                        return true;
                    }
                }
                else if (getFilter.SelectedText == "Contains")
                {
                    if (true)
                    {
                        var selectedComboBoxItem = selectedElement.Items
                            .Where(q => q.Text.Contains(textValue, StringComparison.CurrentCultureIgnoreCase))
                            .FirstOrDefault();
                        selectedElement.Select(selectedComboBoxItem.Text);
                        selectedElement.Collapse();
                        return true;
                    }
                }
                else if (getFilter.SelectedText == "Starts with")
                {
                    if (true)
                    {
                        var selectedComboBoxItem = selectedElement.Items
                            .Where(q => q.Text.StartsWith(textValue, StringComparison.CurrentCultureIgnoreCase))
                            .FirstOrDefault();
                        selectedElement.Select(selectedComboBoxItem.Text);
                        selectedElement.Collapse();
                        return true;
                    }
                }
                else if (getFilter.SelectedText == "End with")
                {
                    if (true)
                    {
                        var selectedComboBoxItem = selectedElement.Items
                            .Where(q => q.Text.EndsWith(textValue, StringComparison.CurrentCultureIgnoreCase))
                            .FirstOrDefault();
                        selectedElement.Select(selectedComboBoxItem.Text);
                        selectedElement.Collapse();
                        return true;
                    }
                }


            }
            catch (Exception e)
            {
                //OccureLog.ErrorInSelect(this,e);
                return false;

            }

            return false;
        }
        private bool CanSelect(object selectedElement)
        {
            var GetFilter = (ElmComboBox)this.GetFilter(null);
            var getTextValue = (string)this.GetTextValue(null);
            var type = selectedElement.GetType();
            if (type == typeof(ListBox))
            {
                var listBox = (ListBox)selectedElement;
                if (GetFilter.SelectedText == "No filter")
                {
                    return listBox.IsEnabled;
                }
                else if (GetFilter.SelectedText == "Equals")
                {
                    return listBox.Items.Any(q => q.Text.Equals(getTextValue, StringComparison.CurrentCultureIgnoreCase));
                }
                else if (GetFilter.SelectedText == "Contains")
                {
                    return listBox.Items.Any(q => q.Text.Contains(getTextValue, StringComparison.CurrentCultureIgnoreCase));
                }
                else if (GetFilter.SelectedText == "Starts with")
                {
                    return listBox.Items.Any(q => q.Text.StartsWith(getTextValue, StringComparison.CurrentCultureIgnoreCase));
                }
                else if (GetFilter.SelectedText == "End with")
                {
                    return listBox.Items.Any(q => q.Text.EndsWith(getTextValue, StringComparison.CurrentCultureIgnoreCase));
                }

            }
            else if (type == typeof(ComboBox))
            {
                var box = (ComboBox)selectedElement;
                if (GetFilter.SelectedText == "No filter")
                {
                    return box.IsEnabled;
                }
                else if (GetFilter.SelectedText == "Equals")
                {
                    return box.Items.Any(q => q.Text.Equals(getTextValue, StringComparison.CurrentCultureIgnoreCase));
                }
                else if (GetFilter.SelectedText == "Contains")
                {
                    return box.Items.Any(q => q.Text.Contains(getTextValue, StringComparison.CurrentCultureIgnoreCase));
                }
                else if (GetFilter.SelectedText == "Starts with")
                {
                    return box.Items.Any(q => q.Text.StartsWith(getTextValue, StringComparison.CurrentCultureIgnoreCase));
                }
                else if (GetFilter.SelectedText == "End with")
                {
                    return box.Items.Any(q => q.Text.EndsWith(getTextValue, StringComparison.CurrentCultureIgnoreCase));
                }
            }

            return false;
        }


        public override StatusOfExecutionEnum GetExecuteStatus()
        {
            return StatusOfExecution;
        }
        #endregion

    }
}
