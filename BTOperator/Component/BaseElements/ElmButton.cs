using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class ElmButton : BasicElement
    {
        public string Title { get; set; } = "Button";

        bool _isCollapseExpandMode = false;
        public bool IsCollapseExpandMode
        {
            get
            { return _isCollapseExpandMode; }
            set
            {
                if (value)
                {
                    TheClick = CollapseExpandClick;
                }
                else
                {
                    TheClick = null;
                }
                _isCollapseExpandMode = value;
            }
        }

        public ElmButton(object parent)
        {
            Parent = parent;
        }

        public int Draw(Graphics graphics, int yTop, int borderWidth)
        {
            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width - 2 * borderWidth) : ElmWidth;
            var measuredText = GraphicFunction.MeasureText(graphics, Title, MyTextSize.Medium);
            var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredText.Height) + Padding.Top + Padding.Bottom : ElmHeight;

            var x1 = ElmHasPosition ? ElmLeft : borderWidth;
            var y1 = ElmHasPosition ? yTop + ElmTop : yTop;

            var recText = new Rectangle(x1, y1, w1, h1);
            var recTextPad = GlobalFunction.AddPadingToRec(recText, Padding, true);

            BackgroundArea = recText;

            Brush bg = new SolidBrush(Color.FromArgb(156, 156, 156));
            graphics.FillRectangle(bg, recText);

            GraphicFunction.DrawTextWithAlinment(graphics, Title, MyTextSize.Medium, ContentAlignment.MiddleCenter, recTextPad, Color.White);

            return recText.Bottom + 1;
        }

        public void SetToCollapseExpandIfDif(bool isExpand)
        {
            if (!IsCollapseExpandMode)
                return;
            if (isExpand)
            {
                if (Title == GraphicConstant.textExpandButton)
                {
                    CollapseExpand(true);
                }
            }
            else
            {
                if (Title == GraphicConstant.textCollapseButton)
                {
                    CollapseExpand(true);
                }
            }
        }

        public void CollapseExpand(bool allowToChageStatus)
        {
            if (buildingBlock == null)
                return;
            if (advancePanel == null)
                return;
            var isCollapse = false;
            if (Title == GraphicConstant.textCollapseButton)
            {
                isCollapse = true;
                if (allowToChageStatus)
                    Title = GraphicConstant.textExpandButton;
                else
                    isCollapse = false;
            }
            else if (Title == GraphicConstant.textExpandButton)
            {
                isCollapse = false;
                if (allowToChageStatus)
                    Title = GraphicConstant.textCollapseButton;
                else
                    isCollapse = true;
            }
            foreach (var elm in buildingBlock.Children)
            {
                if (elm == this)
                    continue;
                if (elm is BasicElement basicElement)
                {
                    if (isCollapse) //Collapse
                    {
                        if (basicElement.IsNecessaryToView == 1)
                        {
                            basicElement.Enable = true;
                        }
                        else
                        {
                            var conSelf = basicElement.GetSelfConnectorCount(true,true,true);
                            var conChildren = basicElement.GetChildrenConnectorCount(true);
                            if (conSelf == 0 && conChildren == 0)
                            {
                                basicElement.Enable = false;
                            }
                            else if (conSelf > 0 && conChildren == 0)
                            {
                                if (basicElement is ElmDropDown elmDropDown)
                                {
                                    elmDropDown.ExpandItems = false;
                                    basicElement.Enable = true;
                                }

                            }
                        }
                    }
                    else // Expand
                    {
                        basicElement.Enable = true;
                    }
                }
            }
        }

        private void CollapseExpandClick(object sender, MouseEventArgs e)
        {
            CollapseExpand(true);

            advancePanel.Invalidate();

        }
    }
}
