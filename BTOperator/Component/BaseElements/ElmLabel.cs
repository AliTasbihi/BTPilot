using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class ElmLabel : BasicElement
    {
        public MyTextSize MySize { get; set; } = MyTextSize.Medium;
        public string Title { get; set; } = "";
        public ContentAlignment Alinment { get; set; }
        //written by tasbihi
        public string Content { get; set; }
        public Color BackGround { get; set; } = Color.Transparent;
        public Color TextColor { get; set; } = Color.Black;

        private bool _isHeaderLabel = false;
        public bool IsHeaderLabel
        {
            get { return _isHeaderLabel; }
            set
            {
                _isHeaderLabel = value;
                if (_isHeaderLabel)
                    TheDoubleClick = HeaderDoubleClick;
                else
                    TheDoubleClick = null;
            }
        }
        public ElmLabel(object parent)
        {
            Parent = parent;
        }
        //todo:draw why it is heer
        public int Draw(Graphics graphics, int y, int borderWidth)
        {
            if (y == 0)
                y = borderWidth;
            var t = string.IsNullOrEmpty(Title) ? "gh" : Title;

            var measuredText = GraphicFunction.MeasureText(graphics, t, MySize);
            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width - 2 * borderWidth) : ElmWidth;
            var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredText.Height) + Padding.Top + Padding.Bottom : ElmHeight;
            var rec = new Rectangle(borderWidth, y, w1, h1);
            var recWPad = GlobalFunction.AddPadingToRec(rec, Padding, true);

            if (BackGround != Color.Transparent)
            {
                Brush bg = new SolidBrush(BackGround);
                graphics.FillRectangle(bg, rec);
            }
            BackgroundArea = GraphicFunction.DrawTextWithAlinment(graphics, Title, MySize, Alinment, recWPad, TextColor);

            return y + h1 + 1;
        }
        //todo:how its work click
        private void HeaderDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender is ElmLabel elmLabel)
            {
                var value = elmLabel.Title;
                if (InputBox.Show("عنوان را وارد کنید", "عنوان:", ref value, null) == DialogResult.OK)
                {
                    elmLabel.Title = value;
                }
            }
        }

    }
}
