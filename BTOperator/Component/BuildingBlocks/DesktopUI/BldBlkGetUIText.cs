using AutoCreateWithJson.Component.BaseElements;
using AutoCreateWithJson.Utility;
using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCreateWithJson.PlayerExecutiton;
using AutoCreateWithJson.Component.BaseStructure;
using AutoCreateWithJson.Utility.Log;
using FlaUI.Core.Patterns;
using FlaUI.Core;
using FlaUI.Core.Definitions;

namespace AutoCreateWithJson.Component.BuildingBlocks.DesktopUI
{
    public class BldBlkGetUIText : BasicBuildingBlock
    {
        private AutomationElement[] foundElements;
        private int currentIndex;

        #region Connector Property  
        private object GetSelectCondition(object sender)
        {
            return GetConnectorPropertySelectElementCondition(sueSelectUIElement, sueSelectUIElement);
        }
        private object GetAddTextFound(object sender)
        {
            return GetConnectorPropertyForLableContenet(nameTextFound, nameTextFound);
        }
        private object IsNotFoundElementConnect(object sender)
        {
            var getOutPutArrowByElementName = OutPutArrowByElementName(nameNotFound);
            if (getOutPutArrowByElementName != null)
            {
                return getOutPutArrowByElementName.ConnectorEnd != null ? true : false;
            }

            return false;
        }
        private object GetSpliteLines(object sender)
        {
            return GetConnectorPropertyCheckBox(chkSpliteLines, chkSpliteLines);
        }
        private object GetPositionFound(object sender)
        {
            return GetConnectorPropertyPositionFound(foundElements);
        }
        private object GetPositionFoundX(object sender)
        {
            return GetConnectorPropertyPositionFoundX(foundElements);
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
        private object GetIsCaseSensitive(object sender)
        {
            return GetConnectorPropertyCheckBox(chkIsCaseSensitive, chkIsCaseSensitive);
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

        public BldBlkGetUIText()
        {
            Width = GraphicConstant.bluildingBlockWidth;
            AddHeaderLabel();
            AddSelectUIElement();
            AddTextFound();
            AddNotFound();
            AddSpliteLines();
            AddDropDownPositionFound();
            AddDropDownAreaFound();
            AddSourceElement();
            AddFindFormat();
            AddIsCaseSensitive();
            AddFilter();
            AddUseOccur();
            AddCount();
            AddCurrentIndex();
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

        private void AddCurrentIndex()
        {
            var lbl = new ElmLabel(this);
            lbl.Visible = false;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Current index";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetCurrentIndex);
            Children.Add(lbl);
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

        private void AddUseOccur()
        {
            var combo = new ElmComboBox(this);
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
        }

        private void AddFilter()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 2, 10, 2);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Filter";
            combo.Items.Add("No Filter");
            combo.Items.Add("-");
            combo.SelectedText = "No Filter";
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private const string chkIsCaseSensitive = "chkIsCaseSensitive";
        private void AddIsCaseSensitive()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkSpliteLines;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(5, 0, 10, 0);
            chk.Title = "Is case sensitive";
            chk.AddTwoConnector(Color.Blue, 0, 1, outputDataFunction: GetIsCaseSensitive);
            Children.Add(chk);

            Children.Add(new ElmSeparateLine());
        }

        private void AddFindFormat()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 2, 10, 2);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Title = "Find format";
            combo.Items.Add("Text");
            combo.Items.Add("Integer");
            combo.Items.Add("Float");
            combo.SelectedText = "Text";
            combo.AddOneConnector(true, Color.Blue, 1, -17);
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
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

        private const string chkSpliteLines = "chkSpliteLines";
        private void AddSpliteLines()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkSpliteLines;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(5, 0, 10, 0);
            chk.Title = "Splite lines";
            chk.AddTwoConnector(Color.Blue, 0, 1, outputDataFunction: GetSpliteLines);
            Children.Add(chk);

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

        private const string nameTextFound = "text found";
        private void AddTextFound()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(3, 0, 3, 0);
            lbl.Title = "Text found";
            lbl.Name = nameTextFound;
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetAddTextFound);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private const string sueSelectUIElement = "sueSelectUIElement";
        private void AddSelectUIElement()
        {
            var sue = new ElmSelectUIElement(this);
            sue.Name = sueSelectUIElement;
            sue.Title = "Select UI Element\r\nto get text from";
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
            lbl.Title = "Get UI Text";
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

        public override void SetExecuteInit()
        {
            StatusOfExecution = StatusOfExecutionEnum.None;
        }

        public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        {
            try
            {
                OccureLog.StartExecutorBuildingBlock(this);
                SelectElementStoreable selectElementStoreable = (SelectElementStoreable)GetSelectCondition(null);
                var timeout = (string)GetTimeout(null);
                TimeSpan timeSpan = ConvertToTimeSpan(timeout);
                //todo:exception 03
                var targetElements = GetElements(globalVariablePlayer, selectElementStoreable, timeSpan);
                if (targetElements == null || targetElements.Length == 0)
                {
                    //for eleman not found
                    if ((bool)IsNotFoundElementConnect(null))
                    {
                        OccureLog.RunElementNotFound(this);
                        StatusOfExecution = StatusOfExecutionEnum.FinishWithErrorRunNotFound;
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

                bool resultClick = GetValues(targetElements.FirstOrDefault());
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

            return false;

        }

        private bool GetValues(AutomationElement? automationElement)
        {
            try
            {
               var elmLabel= (ElmLabel)ElementByName(nameTextFound);
               if (automationElement.Patterns.Value.IsSupported)
                {
                    var automationPattern = automationElement.Patterns.Value.PatternOrDefault;
                    var valueValueOrDefault = automationPattern.Value.ValueOrDefault;
                    elmLabel.Content=valueValueOrDefault;
                 
                    return true;
                }
                else if (automationElement.Patterns.Text.IsSupported)
                {
                    var textPatternOrDefault = automationElement.Patterns.Text.PatternOrDefault;
                    ITextRange text = textPatternOrDefault.DocumentRange.Clone();
                    elmLabel.Content = text.ToString();
                    return true;
                }
                else if (automationElement.Patterns.Text2.IsSupported)
                {
                    var textPatternOrDefault = automationElement.Patterns.Text2.PatternOrDefault;
                    var text = textPatternOrDefault.DocumentRange.Clone().ToString();
                    elmLabel.Content = text;
                    return true;
                }
                else if (automationElement.Patterns.TextEdit.IsSupported)
                {
                    var s = automationElement.Patterns.TextEdit.Pattern.DocumentRange.Clone().ToString();
                    elmLabel.Content = s;
                    return true;

                }
                else if (automationElement.Patterns.Grid.IsSupported)
                {
                    var gridPatternOrDefault = automationElement.Patterns.GridItem.PatternOrDefault.Column.ToDisplayText();
                    elmLabel.Content = gridPatternOrDefault;
                    return true;
                }
                else if (automationElement.Patterns.LegacyIAccessible.IsSupported)
                {
                    ILegacyIAccessiblePattern legacyIAccessiblePattern = automationElement.Patterns.LegacyIAccessible.PatternOrDefault;
                    var getValue = legacyIAccessiblePattern.Name.ValueOrDefault;
                    elmLabel.Content = getValue;
                    return true;
                }
                return TryGetValuesWithType(automationElement);
                bool TryGetValuesWithType(AutomationElement automationElement1)
                {
                    switch (automationElement.ControlType)
                    {
                        case ControlType.Edit:
                            elmLabel.Content = automationElement.AsTextBox().Text;
                            break;
                            return true;
                    }

                    return true ? elmLabel.Content != string.Empty : false;
                }
            }
            catch (Exception e)
            {
                OccureLog.ErrorToGetValue(this, e);
                return false;
            }

        }

        public override StatusOfExecutionEnum GetExecuteStatus()
        {
            return StatusOfExecution;
        }
    }   
}