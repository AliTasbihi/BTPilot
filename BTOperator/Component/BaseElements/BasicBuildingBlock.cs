using AutoCreateWithJson.Component.BaseStructure;
using AutoCreateWithJson.Component.Controller;
using AutoCreateWithJson.PlayerExecutiton;
using AutoCreateWithJson.Utility;
using AutoCreateWithJson.Utility.Log;
using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlaUI.Core;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class BasicBuildingBlock : BasicLoadClickDoubleClickAction
    {
        public string UniversalId { set; get; }
        public string Name { get; internal set; }

        [Category("NoneSave")] public List<object> Children { get; set; } = new List<object>();

        [Category("NoneSave")] public List<object> ClickableComponents { get; set; } = new List<object>();
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle Rect
        {
            get
            {
                if (Height == 0)
                    return new Rectangle(Left, Top, Width, 90);
                else
                    return new Rectangle(Left, Top, Width, Height);
            }
        }

        [Category("NoneSave")] public object? Parent { get; set; } = null;
        //wrtie Tasbihi
        private string GetNameNotFound
        {
            get
            {
                foreach (BasicElement child in Children)
                {
                    if (child.Name != null)
                    {
                        if (child.Name.Contains("not found", StringComparison.CurrentCultureIgnoreCase))
                        {
                            var name = child.Name;
                            return name;
                        }
                    }

                }

                return null;
            }
        }

        private AdvancePanel? _advancePanel = null;

        [Category("NoneSave")]
        public AdvancePanel? advancePanel
        {
            get
            {
                if (_advancePanel == null)
                {
                    var p = Parent;
                    while (p != null)
                    {
                        if (p is AdvancePanel)
                        {
                            _advancePanel = (AdvancePanel)p;
                            break;
                        }

                        if (p is BasicElement basicElement)
                            p = basicElement.Parent;
                        else if (p is BasicBuildingBlock basicBuildingBlock)
                            p = basicBuildingBlock.Parent;
                    }

                }

                return _advancePanel;
            }
        }


        Graphics? _graphics;
        Transform? _transform;
        bool _isActiveBuildingBlock;

        object? _lastElementDraw = null;

        int borderWidth = 2;

        public BasicBuildingBlock()
        {
            UniversalId = Guid.NewGuid().ToString("B");
        }

        public object ElementByName(string elementName)
        {
            foreach (BasicElement element in Children)
            {
                if (String.Compare(element.Name, elementName, StringComparison.OrdinalIgnoreCase) == 0)
                    return element;
                if (element.Children.Count > 0)
                {
                    var chld = FindInChildrenByName(element.Children, elementName);
                    if (chld != null)
                        return chld;
                }
                else
                {
                   
                }
            }

            return null;

            throw new Exception($"المان زیر پیدا نشد\r\n{elementName}");
        }

        public ElmArrowButton InputArrowByElementName(string elementName)
        {
            var elm = ElementByName($"{elementName}ConnectorLinkIn");
            if (elm == null) return null;
            if (elm is ElmConnector cntr)
            {
                var arrows = advancePanel.GetArrowsWithEnd(cntr);
                return arrows.Count > 0 ? arrows[0] : null;
            }

            return null;
        }

        //write Tasbihi
        public ElmArrowButton OutPutArrowByElementName(string elementName)
        {
            var elm = ElementByName($"{elementName}ConnectorLinkOut");
            if (elm == null) return null;
            if (elm is ElmConnector cntr)
            {
                var arrows = advancePanel.GetArrowsWithStart(cntr);
                return arrows.Count > 0 ? arrows[0] : null;
            }

            return null;
            {

            }
        }

        public List<ElmArrowButton> GetAllArrowsConnected(List<object>? children)
        {
            var arrow = new List<ElmArrowButton>();
            if (children == null)
            {
                children = Children;
            }

            foreach (BasicElement elm in children)
            {
                if (elm is ElmConnector cntr)
                {
                    var ars = advancePanel.GetArrowsWithEnd(cntr);
                    if (ars.Count > 0)
                        arrow.AddRange(ars);
                    ars = advancePanel.GetArrowsWithStart(cntr);
                    if (ars.Count > 0)
                        arrow.AddRange(ars);
                }
                else if (elm.Children.Count > 0)
                {
                    var ars = GetAllArrowsConnected(elm.Children);
                    if (ars.Count > 0)
                        arrow.AddRange(ars);
                }
            }

            return arrow;

        }

        private BasicElement FindInChildrenByName(List<object> children, string elementName)
        {
            foreach (BasicElement child in children)
            {

                if (String.Compare(child.Name, elementName, StringComparison.OrdinalIgnoreCase) == 0)
                    return child;
                if (child.Children.Count > 0)
                {
                    var chld = FindInChildrenByName(child.Children, elementName);
                    if (chld != null)
                        return chld;
                }
            }

            return null;
        }

        public void DrawAllElementes(Graphics graphics, Transform transform, bool isActiveBuildingBlock)
        {
            _graphics = graphics;
            _transform = transform;
            _isActiveBuildingBlock = isActiveBuildingBlock;

            var borderColor = GetColorFromHeaderColor();

            Bitmap bmp = new Bitmap(Width, 4500);
            Graphics bmpGfx = Graphics.FromImage(bmp);

            if (isActiveBuildingBlock)
            {
                GraphicFunction.DrawFillRectangle(bmpGfx, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    GraphicConstant.colorBackgroundBuildingBlock, GraphicConstant.colorActiveBuildingBlock,
                    2 * borderWidth);
            }
            else
            {
                GraphicFunction.DrawFillRectangle(bmpGfx, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    GraphicConstant.colorBackgroundBuildingBlock, borderColor, 2 * borderWidth);
            }

            IfHaveCollapseExpandButtonThenApply();

            ClickableComponents.Clear();
            _lastElementDraw = null;
            var y = 0;
            foreach (var elem in Children)
            {
                if (elem is BasicElement be)
                {
                    be.IsShowingRunTime = false;
                    if (!be.Enable)
                        continue;
                    if (be.Parent is not null)
                    {
                        if (be.Parent is BasicElement bee)
                        {
                            if (!bee.Enable)
                                continue;
                        }

                        if (be.Parent is ElmDropDown drp)
                        {
                            if (!drp.ExpandItems)
                                continue;
                        }

                    }
                }
                else
                    continue;


                if (elem is ElmSeparateLine space)
                {
                    if (LastElementDrawIsSpace())
                    {
                        continue;
                    }
                }

                ////////
                // رسم یک المان
                var res = DrawOneElement(bmpGfx, elem, y);
                if (!res.result)
                    continue;
                y = res.y;
                ////////


                // آخرین المانی که رسم شده است ذخیره می شود
                if (elem is not ElmConnector)
                    _lastElementDraw = elem;
                Height = y;
            }

            Height = y;

            if (isActiveBuildingBlock)
            {
                GraphicFunction.DrawLine(bmpGfx, 0, y, bmp.Width, y, GraphicConstant.colorActiveBuildingBlock,
                    2 * borderWidth);
            }
            else
            {
                GraphicFunction.DrawLine(bmpGfx, 0, y, bmp.Width, y, borderColor, 2 * borderWidth);
            }

            //var ty = GlobalFunction.GetTypeLastClass(this.GetType());
            //var fn = $"c:\\00\\00\\0-{ty}.png";
            //GraphicFunction.CopyRegionIntoImageFile(bmp, new Rectangle(0, 0, Width, Height), fn);

            var bmpResize = GraphicFunction.ResizeImage(bmp, Width, Height, _transform.ModelToDisplayValue(Width),
                _transform.ModelToDisplayValue(Height));
            graphics.DrawImage(bmpResize, _transform.ModelToDisplay(Left, Top));
        }

        private void IfHaveCollapseExpandButtonThenApply()
        {
            foreach (var elem in Children)
            {
                if (elem is ElmButton btn)
                {
                    if (btn.IsCollapseExpandMode)
                    {
                        btn.CollapseExpand(false);
                        return;
                    }
                }
            }
        }

        private (bool result, int y) DrawOneElement(Graphics bmpGfx, object elem, int y)
        {
            if (!NeedToDrawIncludeParent(elem))
                return (false, y);

            var isClickableArea = true;


            //// Start Elements
            if (elem is ElmButton btn)
            {
                y = btn.Draw(bmpGfx, y, borderWidth);
            }
            else if (elem is ElmCheckBox chk)
            {
                y = chk.Draw(bmpGfx, y, borderWidth);
            }
            else if (elem is ElmComboBox combo)
            {
                y = combo.Draw(bmpGfx, y, borderWidth);
            }
            else if (elem is ElmDropDown drpbox)
            {
                y = drpbox.Draw(bmpGfx, y, borderWidth);
            }
            else if (elem is ElmMultiConnector elmMulti)
            {
                y = elmMulti.Draw(bmpGfx, y, borderWidth);
            }
            else if (elem is ElmEditBox edbox)
            {
                y = edbox.Draw(bmpGfx, y, borderWidth);
            }
            else if (elem is ElmLabel lbl)
            {
                y = lbl.Draw(bmpGfx, y, borderWidth);
            }
            else if (elem is ElmSelectUIElement selelem)
            {
                y = selelem.Draw(bmpGfx, y, borderWidth);
            }
            else if (elem is ElmConnector contr)
            {
                var rlTop = 0;
                var rtLeft = 0;
                if (contr.RelatedElement is BasicElement sbe)
                {
                    if (sbe != null)
                    {
                        if (!sbe.Enable || !sbe.Visible)
                            return (false, y);
                        rlTop = sbe.BackgroundArea.Top;
                    }

                    if (contr.InputOutput == InputOutputType.Output)
                        rtLeft = Convert.ToInt32(bmpGfx.VisibleClipBounds.Width - contr.ElmWidth);
                }
                else
                {
                    rlTop = contr.Offset = 0;
                }

                contr.Draw(bmpGfx, rtLeft, rlTop);
            }
            else if (elem is ElmSeparateLine spl)
            {
                y = spl.Draw(bmpGfx, y, borderWidth);
                isClickableArea = false;
            }
            else if (elem is ElmSpace spc)
            {
                y = spc.Draw(bmpGfx, y, borderWidth);
                isClickableArea = false;
            }

            else
            {
                System.Diagnostics.Debug.WriteLine(elem.GetType().ToString());
                return (false, y);
            }

            if (isClickableArea)
                ClickableComponents.Add(elem);

            ((BasicElement)elem).IsShowingRunTime = true;
            // Draw Childern
            if (elem is BasicElement be)
            {
                foreach (var el in be.Children)
                {
                    var res = DrawOneElement(bmpGfx, el, y);
                    if (!res.result)
                        continue;
                    y = res.y;
                }
            }

            return (true, y);
        }

        private bool NeedToDrawIncludeParent(object elem)
        {
            if (elem is BasicElement be)
            {
                if (!be.Enable || !be.Visible)
                    return false;
                if (be.Parent == null)
                    return true;
                if (be.Parent is BasicElement basicElement)
                {
                    if (!basicElement.IsShowingRunTime)
                        return false;
                }

                if (be.Parent is ElmDropDown elmDropDown)
                {
                    if (elem is ElmConnector elmConnector)
                    {
                        return true;
                    }

                    return elmDropDown.Enable && elmDropDown.Visible && elmDropDown.ExpandItems;
                }

                return true;
            }

            return false;
        }

        private Color GetColorFromHeaderColor()
        {
            foreach (var elem in Children)
            {
                if (elem is ElmLabel lbl)
                {
                    return lbl.BackGround;
                }
            }

            return Color.Black;
        }

        public object? GetElementByModelPosition(Point ptModel)
        {
            var ptLocal = new Point(ptModel.X - Left, ptModel.Y - Top);
            for (int i = ClickableComponents.Count - 1; i >= 0; i--)
            {
                if (ClickableComponents[i] is BasicElement basicElement)
                {
                    if (basicElement.BackgroundArea.IsPointInside(ptLocal))
                        return ClickableComponents[i];
                }
            }

            return null;
        }

        public string GetHeader()
        {
            var elm = Children.First();
            if (elm is ElmLabel lbl)
            {
                return lbl.Title;
            }

            return "";
        }

        private bool LastElementDrawIsSpace()
        {
            if (_lastElementDraw is ElmSeparateLine)
                return true;
            return false;
        }

        public string GetAllScriptPositionRaw(bool detail, int spaceNo)
        {
            var space = new StringBuilder().Insert(0, " ", spaceNo).ToString();
            var sb = new StringBuilder();

            sb.AppendLine($"{space}UniversalId:{UniversalId}");
            sb.AppendLine($"{space}Left:{Left}");
            sb.AppendLine($"{space}Top:{Top}");
            sb.AppendLine($"{space}Width:{Width}");
            sb.AppendLine($"{space}Height:{Height}");
            sb.AppendLine($"{space}Element Count:{Children.Count}");
            if (detail)
            {
                var c = 0;
                foreach (var elem in Children)
                {
                    c++;
                    GetDetailOfBasicElement(sb, (BasicElement)elem, space);

                }
            }

            return sb.ToString();
        }

        public string? GetAllScriptClickableArea(int spaceNo)
        {
            var space = new StringBuilder().Insert(0, " ", spaceNo).ToString();
            var sb = new StringBuilder();

            if (ClickableComponents.Count == 0)
                return "";
            var c = 0;
            foreach (var elem in ClickableComponents)
            {
                if (elem is BasicElement basicElement)
                {
                    c++;
                    var ty = GlobalFunction.GetTypeLastClass(basicElement.GetType());
                    sb.AppendLine(
                        $"{space} {c}-{ty}:[{basicElement.BackgroundArea.Left},{basicElement.BackgroundArea.Top},{basicElement.BackgroundArea.Right},{basicElement.BackgroundArea.Bottom}]");
                }


            }

            return sb.ToString();
        }

        private void GetDetailOfBasicElement(StringBuilder sb, BasicElement basicElement, string space)
        {
            var ty = GlobalFunction.GetTypeLastClass(basicElement.GetType());
            sb.AppendLine($"{space} -UniversalId:{basicElement.UniversalId}");
            sb.AppendLine(
                $"{space} -Area:[{basicElement.BackgroundArea.Left},{basicElement.BackgroundArea.Top},{basicElement.BackgroundArea.Right},{basicElement.BackgroundArea.Bottom}]");
            sb.AppendLine($"{space}   {ty}");
            if (basicElement.Children.Count == 0)
                return;
            sb.AppendLine($"{space}    Children:{basicElement.Children.Count}");
            foreach (var child in basicElement.Children)
            {
                //var ty2 = GlobalFunction.GetTypeLastClass(child.GetType());
                //sb.AppendLine($"{space}       {ty2}");
                GetDetailOfBasicElement(sb, (BasicElement)child, space + "   ");
            }
        }

        public string GetAllScriptPositionWithEffect(Transform transform, int spaceNo)
        {
            _transform = transform;
            var space = new StringBuilder().Insert(0, " ", spaceNo).ToString();
            var sb = new StringBuilder();

            sb.AppendLine($"{space}Left:{_transform.ModelToDisplayX(Left)}");
            sb.AppendLine($"{space}Top:{_transform.ModelToDisplayY(Top)}");
            sb.AppendLine($"{space}Width:{_transform.ModelToDisplayValue(Width)}");
            sb.AppendLine($"{space}Height:{_transform.ModelToDisplayValue(Height)}");
            sb.AppendLine($"{space}Element Count:{Children.Count}");


            return sb.ToString();
        }

        public void CollapseExpand(bool isExpand)
        {
            foreach (BasicElement elm in Children)
            {
                if (elm is ElmButton btn)
                {
                    if (btn.IsCollapseExpandMode)
                    {
                        btn.SetToCollapseExpandIfDif(isExpand);
                    }
                }
            }
        }


        ///////////////////////////////
        //   EXECUTOR 
        //
        //
        ////////////////////////////////// 
        public enum StatusOfExecutionEnum
        {
            None,
            Start,
            Running,
            Finish,
            FinishWithError,
            FinishWithErrorRunNotFound
        }

        public StatusOfExecutionEnum StatusOfExecution { set; get; }

        public virtual void SetExecuteInit()
        {
            var ty = GlobalFunction.GetTypeLastClass(GetType()) + ".SetExecuteInit";
            MyLog.WritelnBoth(ty, "این قسمت باید کدنویسی شود که هنوز نشده است");
        }

        public virtual bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        {
            var ty = GlobalFunction.GetTypeLastClass(GetType()) + ".ExecuteBuildingBlock";
            MyLog.WritelnBoth(ty, "این قسمت باید کدنویسی شود که هنوز نشده است");
            return true;
        }

        public virtual StatusOfExecutionEnum GetExecuteStatus()
        {
            var ty = GlobalFunction.GetTypeLastClass(GetType()) + ".GetExecuteStatus";
            MyLog.WritelnBoth(ty, "این قسمت باید کدنویسی شود که هنوز نشده است");
            return StatusOfExecutionEnum.None;
        }

        public BasicBuildingBlock GetNextBuildingBlock()
        {
            if (StatusOfExecution == StatusOfExecutionEnum.Finish)
            {
                var elm = ElementByName("HeaderConnectorLinkOut");
                if (elm == null) return null;
                if (elm is ElmConnector cntr)
                {
                    var arrows = advancePanel.GetArrowsWithStart(cntr);
                    if (arrows.Count == 1)
                    {
                        var elmTo = arrows[0].ConnectorEnd;
                        return elmTo.buildingBlock;
                    }
                }

                return null;
            }
            //write tasbihi
            else if (StatusOfExecution == StatusOfExecutionEnum.FinishWithErrorRunNotFound)
            {
                var testGetNameNotFound = this.GetNameNotFound;
                var elm = ElementByName($"{GetNameNotFound}ConnectorLinkOut");
                if (elm == null) return null;
                if (elm is ElmConnector cntr)
                {
                    var arrows = advancePanel.GetArrowsWithStart(cntr);
                    if (arrows.Count == 1)
                    {
                        var elmTo = arrows[0].ConnectorEnd;
                        return elmTo.buildingBlock;
                    }
                }

                return null;
            }
            //
            else if (StatusOfExecution == StatusOfExecutionEnum.FinishWithError)
            {

                return null;
            }
            else
            {
                return null;
            }
        }

        #region Arrow Transfer Data and Connectors

        //todo:??this section outputdatafunction is null what can i do?
        //todo:??why use delegate for outputDataFunction
        public void UpdateAllDataOfArrows()
        {
            var outputArrows = advancePanel.GetAllOutputArrowsBuildingBlock(this);
            foreach (var arrow in outputArrows)
            {
                if (arrow.ConnectorStart.OutputDataFunction is not null)
                {
                    arrow.TransferData = arrow.ConnectorStart.OutputDataFunction(arrow);
                }
            }
        }
        //todo:##modified by tasbihi
        public string GetConnectorPropertyEditBox(string elementNameForArrow, string elementNameForData)
        {
            var arrow = InputArrowByElementName(elementNameForArrow);
            if (arrow == null)
            {
                return ((ElmEditBox)ElementByName(elementNameForData)).Text;
            }
            else if(arrow!=null&&arrow.TransferData!=null)
            {
                return (string)arrow.TransferData;
            }
            else
            {
                return (string) arrow.ConnectorStart.OutputDataFunction.Invoke(null);
            }
        }
       //todo:## written by tasbihi
        public string GetConnectorPropertyForLableContenet(string elementNameForArrow, string elementNameForData)
        {
            var arrow = InputArrowByElementName(elementNameForArrow);
            if (arrow == null)
            {
                var elementByName = ElementByName(elementNameForData);
                return ((ElmLabel)elementByName).Content;
            }
            else
            {
                return (string)arrow.TransferData;
            }
        }

        public string GetConnectorPropertyComboBox(string elementNameForArrow, string elementNameForData)
        {
            var arrow = InputArrowByElementName(elementNameForArrow);
            if (arrow == null)
            {
                return ((ElmComboBox)ElementByName(elementNameForData)).SelectedText;
            }
            else
            {
                return (string)arrow.TransferData;
            }
        }

        public bool GetConnectorPropertyCheckBox(string elementNameForArrow, string elementNameForData)
        {
            var arrow = InputArrowByElementName(elementNameForArrow);
            if (arrow == null)
            {
                return ((ElmCheckBox)ElementByName(elementNameForData)).Checked;
            }
            else
            {
                return (bool)arrow.TransferData;
            }
        }

        public SelectElementStoreable GetConnectorPropertySelectElementCondition(string elementNameForArrow,
            string elementNameForData)
        {
            var arrow = InputArrowByElementName(elementNameForArrow);
            if (arrow == null)
            {
                var sue = (ElmSelectUIElement)ElementByName(elementNameForData);
                if (sue != null)
                    return sue.SelectElementStoreable;
                return null;
            }
            else
            {
                return (SelectElementStoreable)arrow.TransferData;
            }
        }

        public object GetConnectorPropertyFoundElement(AutomationElement[] foundElements)
        {
            if (foundElements != null && foundElements.Length > 0)
                return foundElements[0];
            return null;
        }

        public object GetConnectorPropertyPositionFound(AutomationElement[] foundElements)
        {
            if (foundElements == null || foundElements.Length == 0)
                return null;
            if (foundElements[0].GetType().HasProperty("BoundingRectangle"))
            {
                return new Point(foundElements[0].BoundingRectangle.Left, foundElements[0].BoundingRectangle.Top);
            }

            return null;
        }

        public object GetConnectorPropertyPositionFoundX(AutomationElement[] foundElements)
        {
            if (foundElements == null || foundElements.Length == 0)
                return null;
            if (foundElements[0].GetType().HasProperty("BoundingRectangle"))
            {
                return foundElements[0].BoundingRectangle.Left;
            }

            return null;
        }

        public object GetConnectorPropertyPositionFoundY(AutomationElement[] foundElements)
        {
            if (foundElements == null || foundElements.Length == 0)
                return null;
            if (foundElements[0].GetType().HasProperty("BoundingRectangle"))
            {
                return foundElements[0].BoundingRectangle.Top;
            }

            return null;
        }

        public object GetConnectorPropertyAreaFound(AutomationElement[] foundElements)
        {
            if (foundElements == null || foundElements.Length == 0)
                return null;
            if (foundElements[0].GetType().HasProperty("BoundingRectangle"))
            {
                return new Rectangle(foundElements[0].BoundingRectangle.Left, foundElements[0].BoundingRectangle.Top,
                    foundElements[0].BoundingRectangle.Width, foundElements[0].BoundingRectangle.Height);
            }

            return null;
        }

        public object GetConnectorPropertyAreaFoundX(AutomationElement[] foundElements)
        {
            if (foundElements == null || foundElements.Length == 0)
                return null;
            if (foundElements[0].GetType().HasProperty("BoundingRectangle"))
            {
                return foundElements[0].BoundingRectangle.Left;
            }

            return null;
        }

        public object GetConnectorPropertyAreaFoundY(AutomationElement[] foundElements)
        {
            if (foundElements == null || foundElements.Length == 0)
                return null;
            if (foundElements[0].GetType().HasProperty("BoundingRectangle"))
            {
                return foundElements[0].BoundingRectangle.Top;
            }

            return null;
        }

        public object GetConnectorPropertyAreaFoundWidth(AutomationElement[] foundElements)
        {
            if (foundElements == null || foundElements.Length == 0)
                return null;
            if (foundElements[0].GetType().HasProperty("BoundingRectangle"))
            {
                return foundElements[0].BoundingRectangle.Width;
            }

            return null;
        }

        public object GetConnectorPropertyAreaFoundHeight(AutomationElement[] foundElements)
        {
            if (foundElements == null || foundElements.Length == 0)
                return null;
            if (foundElements[0].GetType().HasProperty("BoundingRectangle"))
            {
                return foundElements[0].BoundingRectangle.Height;
            }

            return null;
        }

        //write tasbihi
        //getElements for Building block
        public AutomationElement[] GetElements(GlobalVariablePlayer globalVariablePlayer,
            SelectElementStoreable selectElementStoreable,TimeSpan timeOut)
        {
            try
            {
                if (selectElementStoreable != null)
                {

                    var conditionElement = selectElementStoreable.ConditionForSelectElement;
                  //  var automationElements = conditionElement.GetTargetElements(globalVariablePlayer.CurrentMainWindow);

                    AutomationElement[] targetElements =
                        conditionElement.GetTargetElements(globalVariablePlayer,timeOut);
                    if (targetElements == null || targetElements.Length == 0)
                    {
                        OccureLog.ErrorToFindTargetElement(this);
                        return null;
                    }
                    else
                    {
                        return targetElements;
                    }
                }
                else
                {
                    OccureLog.ErrorToFindTargetElement(this);
                    return null;
                }
            }
            catch (Exception e)
            {
                OccureLog.ErrorToFindTargetElement(this, e);
                return null;
            }

            #endregion
        }
        //write aliTasbihi
        //ConvertToTimeSpan
        public TimeSpan ConvertToTimeSpan(string timeText)=> TimeSpan.FromSeconds(int.Parse(timeText));


    }
}
