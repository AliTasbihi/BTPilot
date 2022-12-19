using AdvancePanelLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BaseElements
{
    public class ElmDropDown : BasicElement
    {
        public string Title { get; set; }

        public ElmDropDown(object parent)
        {
            Parent = parent;
            TheClick = ExpandItemClick;
        }

        public ContentAlignment Alignment { get; set; }
        public bool ExpandItems { get; set; }

        public int Draw(Graphics graphics, int y, int borderWidth)
        {
            var measuredText = GraphicFunction.MeasureText(graphics, Title, MyTextSize.Medium);
            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width) : ElmWidth;
            var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredText.Height) : ElmHeight;
            var rec = new Rectangle(0, y, w1, h1);
            var recPad = GlobalFunction.AddPadingToRec(rec, Padding, true);

            var recArrow = GraphicFunction.DrawArrowUpDown(graphics, recPad, ExpandItems, true, 5, Color.Black);
            var recWPad2 = GlobalFunction.AddPadingToRec(recPad, new(0, 0, 20, 0), true);
            var rectext = GraphicFunction.DrawTextWithAlinment(graphics, Title, MyTextSize.Medium, Alignment, recWPad2, Color.Black);

            var xMin = Math.Min(recArrow.Left, rectext.Left);
            var xMax = Math.Max(recArrow.Right, rectext.Right);
            var yMin = Math.Min(recArrow.Top, rectext.Top);
            var yMax = Math.Max(recArrow.Bottom, rectext.Bottom);
            BackgroundArea = new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);

            return y + h1 + 1;
        }

        public void ExpandItemClick(object sender, MouseEventArgs e)
        {
            // در صورتی که یکی از زیر المان های همین المان دارای کانکش فعال باشد 
            // آنگاه این المان نیاید بتواند جمع شود و باید به صورت بازشده باقی بماند
            if (ExpandItems && GetChildrenConnectorCount(true) > 0)
                return;
            ExpandItems = !ExpandItems;
        }

    }
}
