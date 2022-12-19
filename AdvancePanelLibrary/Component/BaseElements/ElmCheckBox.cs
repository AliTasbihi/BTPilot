using AdvancePanelLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BaseElements
{
    public class ElmCheckBox : BasicElement
    {
        public string Title { get; set; }
        public ContentAlignment Alinment { get; set; }
        public bool Checked { get; set; } = false;

        public ElmCheckBox(object parent)
        {
            Parent = parent;
            TheClick = CheckBoxClick;
        }

        public int Draw(Graphics graphics, int yTop, int borderWidth)
        {
            var wChk = 15;
            Rectangle rec, recPad, recText;

            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width) : ElmWidth;
            w1 -= 2 * borderWidth;
            var tt = string.IsNullOrEmpty(Title) ? "Thg" : Title;
            var measuredText = GraphicFunction.MeasureText(graphics, tt, MyTextSize.Medium);
            var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredText.Height) : ElmHeight;
            rec = new Rectangle(borderWidth, yTop, w1, h1);
            recPad = GlobalFunction.AddPadingToRec(rec, Padding, true);

            var recChk = GraphicFunction.DrawCheckbox(graphics, recPad, Checked, false, wChk, Color.Black, ReadOnly);

            recText = new Rectangle(recPad.Left + 2 * wChk, recPad.Top, recPad.Width - 2 * wChk, recPad.Height);
            var recTxt = GraphicFunction.DrawTextWithAlinment(graphics, Title, MyTextSize.Medium, ContentAlignment.MiddleLeft, recText, Color.Black,false,ReadOnly);

            BackgroundArea = new Rectangle(recChk.Left, recChk.Top, recTxt.Right - recChk.Left, recTxt.Bottom - recChk.Top);

            return BackgroundArea.Bottom + 1;
        }
       
        private void CheckBoxClick(object sender, MouseEventArgs e)
        {
            if (ReadOnly)
                return;
            Checked = !Checked;
        }

    }
}
