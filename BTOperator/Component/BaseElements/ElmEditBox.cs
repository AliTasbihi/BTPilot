using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class ElmEditBox : BasicElement
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public ContentAlignment TitlePosition { get; set; }
        public bool AutoSize { get; set; } = false;
        public string TextWithAutoSize
        {
            set
            {
                Text = value;
                AutoSize = true;
            }
        }

        public ElmEditBox(object parent)
        {
            Parent = parent;
            TheClick = EditBoxClick;
        }

        public int Draw(Graphics graphics, int yTop, int borderWidth)
        {
            Rectangle recText, recEditbox;
            Rectangle recTextPad, recEditboxPad;
            var isTitle = (string.IsNullOrEmpty(Title)) ? false : true;
            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width) : ElmWidth;
            var tt = isTitle ? Title : "Thg";
            var vv = string.IsNullOrEmpty(Text) ? "Thg" : Text;
            SizeF measuredTitle = GraphicFunction.MeasureText(graphics, tt, MyTextSize.Medium);
            SizeF measuredValue;
            if (AutoSize)
                measuredValue = GraphicFunction.MeasureText(graphics, vv, MyTextSize.Medium, w1 - Padding.Left - Padding.Right - 2 * 5 - 2);
            else
                measuredValue = GraphicFunction.MeasureText(graphics, vv, MyTextSize.Medium);
            var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredTitle.Height) : ElmHeight;
            var h2 = (ElmHeight == 0) ? Convert.ToInt32(measuredValue.Height) : ElmHeight;
            if (isTitle)
            {
                var alg = ContentAlignment.TopLeft;
                if (TitlePosition == ContentAlignment.TopLeft)
                {
                    recText = (ElmHeight > 0) ? new Rectangle(0, yTop, w1, h1 / 2) : new Rectangle(0, yTop, w1, h1);
                    recTextPad = GlobalFunction.AddPadingToRec(recText, Padding, true);

                    recEditbox = (ElmHeight > 0) ? new Rectangle(0, recText.Bottom, w1, h1 / 2) : new Rectangle(0, recText.Bottom, w1, h2);
                    recEditboxPad = GlobalFunction.AddPadingToRec(recEditbox, Padding, true);
                }
                else
                {
                    alg = ContentAlignment.MiddleLeft;
                    recText = new Rectangle(0, yTop, w1 / 2, h1);
                    recTextPad = GlobalFunction.AddPadingToRec(recText, Padding, true);
                    var dx = Convert.ToInt32(recTextPad.Width - measuredTitle.Width);
                    if (dx > 0)
                    {
                        recText = new Rectangle(0, yTop, w1 / 2 - dx, h1);
                        recTextPad = GlobalFunction.AddPadingToRec(recText, Padding, true);
                    }
                    recEditbox = new Rectangle(recText.Right, yTop, w1 - recText.Width, h1);
                    recEditboxPad = GlobalFunction.AddPadingToRec(recEditbox, Padding, true);
                }
                GraphicFunction.DrawTextWithAlinment(graphics, Title, MyTextSize.Medium, alg, recTextPad, Color.Black);

            }
            else
            {
                if (AutoSize)
                {
                    recEditbox = new Rectangle(0, yTop, w1, h2);
                    recEditboxPad = GlobalFunction.AddPadingToRec(recEditbox, Padding, true);
                }
                else
                {
                    recEditbox = new Rectangle(0, yTop, w1, h1);
                    recEditboxPad = GlobalFunction.AddPadingToRec(recEditbox, Padding, true);
                }
            }

            GraphicFunction.DrawFillRectangle(graphics, recEditboxPad, Color.White, Color.Black);
            if (ReadOnly)
            {
                GraphicFunction.DrawTextEditbox(graphics, recEditboxPad, "Value from input", Color.White, Color.Black, AutoSize, ReadOnly);
            }
            else
            {
                GraphicFunction.DrawTextEditbox(graphics, recEditboxPad, Text, Color.White, Color.Black, AutoSize, ReadOnly);
            }


            BackgroundArea = recEditboxPad;

            return recEditbox.Bottom + 3;

        }

        private void EditBoxClick(object sender, MouseEventArgs e)
        {
            if (ReadOnly)
                return;
            var pt = advancePanel.PointToScreen(new Point(e.X, e.Y));
            var value = Text;
            if (InputBox.ShowWithPosition("Edit value", Title, pt, ref value, null) == DialogResult.OK)
            {
                Text = value;
            }
        }

    }
}
