using AutoCreateWithJson.Utility;
using AutoCreateWithJson.Utility.DialogForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    internal class ElmComboBox : BasicElement
    {
        public bool HasTitle { get; set; } = true;
        public string Title { get; set; }

        public List<string> Items { get; set; } = new List<string>();
        public string SelectedText { get; set; } = "";
        public ContentAlignment TitlePosition { get; set; } = ContentAlignment.TopLeft;

        public ElmComboBox(object parent)
        {
            Parent = parent;
            TheClick = SelectItemByClick;
        }

        public int Draw(Graphics graphics, int yTop, int borderWidth)
        {
            Rectangle recText, recCombo;
            Rectangle recTextPad, recComboPad;
            var isTitle = (string.IsNullOrEmpty(Title) || !HasTitle) ? false : HasTitle;
            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width) : ElmWidth;
            var tt = isTitle ? Title : "Thg";
            var measuredText = GraphicFunction.MeasureText(graphics, tt, MyTextSize.Medium);

            if (isTitle)
            {
                var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredText.Height) : ElmHeight;
                var alg = ContentAlignment.TopLeft;
                if (TitlePosition == ContentAlignment.TopLeft)
                {
                    recText = (ElmHeight > 0) ? new Rectangle(0, yTop, w1, h1 / 2) : new Rectangle(0, yTop, w1, h1);
                    recTextPad = GlobalFunction.AddPadingToRec(recText, Padding, true);
                    recCombo = (ElmHeight > 0) ? new Rectangle(0, recText.Bottom, w1, h1 / 2) : new Rectangle(0, recText.Bottom, w1, h1);
                    recComboPad = GlobalFunction.AddPadingToRec(recCombo, Padding, true);
                }
                else
                {
                    alg = ContentAlignment.MiddleLeft;
                    recText = new Rectangle(0, yTop, w1 / 2, h1);
                    recTextPad = GlobalFunction.AddPadingToRec(recText, Padding, true);
                    var dx = Convert.ToInt32(recTextPad.Width - measuredText.Width);
                    if (dx > 0)
                    {
                        recText = new Rectangle(0, yTop, w1 / 2 - dx, h1);
                        recTextPad = GlobalFunction.AddPadingToRec(recText, Padding, true);
                    }
                    recCombo = new Rectangle(recText.Right, yTop, w1 - recText.Width, h1);
                    recComboPad = GlobalFunction.AddPadingToRec(recCombo, Padding, true);
                }
                GraphicFunction.DrawTextWithAlinment(graphics, Title, MyTextSize.Medium, alg, recTextPad, Color.Black);

            }
            else
            {
                var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredText.Height) : ElmHeight;
                recCombo = new Rectangle(0, yTop, w1, h1);
                recComboPad = GlobalFunction.AddPadingToRec(recCombo, Padding, true);
            }

            GraphicFunction.DrawFillRectangle(graphics, recComboPad, Color.White, Color.Black);

            if (ReadOnly)
            {
                GraphicFunction.DrawTextCombobox(graphics, recComboPad, "Value from input", Color.White, Color.Black, ReadOnly);
            }
            else
            {
                GraphicFunction.DrawTextCombobox(graphics, recComboPad, SelectedText, Color.White, Color.Black, ReadOnly);
            }

            

            BackgroundArea = recComboPad;

            return recCombo.Bottom + 3;
        }

        public void SelectItemByClick(object sender, MouseEventArgs e)
        {
            if (ReadOnly)
                return;
            var value = SelectedText;
            var pt=advancePanel.PointToScreen(new Point(e.X, e.Y));
            if (SelectItemCombobox.Run("Change Value", Title,Items,pt,ref value)) 
            {
                SelectedText = value;
            }
        }

    }
}
