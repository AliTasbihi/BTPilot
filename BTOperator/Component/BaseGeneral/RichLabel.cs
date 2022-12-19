using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseGeneral
{
    /// <summary>
    /// A Label that can contain formated text. 
    /// The formatting is done by enclosing a formatted chunk in curly braces. The first character is a format going from 0-7:
    /// 0 is a link ("{{0a link|www.google.com}}" or "{{www.google.com}}"), 
    /// 1 is bold ("{{1bold text}}"), 
    /// 2 is italic ("{{2italic text}}"), 
    /// 4 is alternative color ("{{4alternate text}}"), 
    /// 1 to 4 can be combined like enum flag ("{{7alternate bold and italic text}}"). 
    /// To insert a new line, use "\r\n". 
    /// 
    /// http://codereview.stackexchange.com/a/104751/49345
    /// </summary>
    [Serializable()]
    public class RichLabel : Label
    {
        private static int _globalCounter = 0;
        /// <summary>
        /// Alternate ForeColor that can be drawn when adding 4 to the formatting number. 
        /// </summary>
        public Color ForeColorAlt { get; set; }

        /// <summary>
        /// Splitters that allow to implement custom formattings. 
        /// </summary>
        public string[] Splitters { get; set; }

        /// <summary>
        /// Set this to true to force to calculate the width and height by the control itself. 
        /// This should always be true. If it does not work, rather use the legacy AutoSize. 
        /// Disable AutoSize and this property to disable auto size at all. 
        /// </summary>
        public bool CustomAutoSize { get; set; }

        /// <summary>
        /// Constructor. 
        /// </summary>
        public RichLabel()
        {
            Name = this.GetType().Name + (++_globalCounter);
            this.MouseClick += this.RichLabel_MouseClick;
            this.Splitters = new string[] { "{{", "}}" };
            this.ForeColor = SystemColors.ControlText;
            this.ForeColorAlt = SystemColors.HighlightText;
            this.AutoSize = this.AutoSize && !this.CustomAutoSize;
        }

        /// <summary>
        /// Internal rectangle collection with rectangles that lay over hyperlinks. 
        /// </summary>
        private Dictionary<Rectangle, string> links = new Dictionary<Rectangle, string>();

        /// <summary>
        /// Paints the formatted text into the control area and will be called on initialization and when the text has been changed. 
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush textBrush = null;
            Font textFont = null;
            this.links.Clear(); // clear old links

            // separate splitter pairs
            this.Text = this.Text.Replace(Splitters[1] + Splitters[0], Splitters[1] + "\v" + Splitters[0]);

            // the box to draw in
            int x = this.Padding.Left;
            int y = this.Padding.Top;
            int w = this.MinimumSize.Width;
            int h = this.MinimumSize.Height;
            string[] parts = this.Text.Split(Splitters, StringSplitOptions.None);
            foreach (string part in parts)
            {
                // test 1st character
                if (part != "")
                {
                    int o = (part[0] - '0');
                    // if 1-7: valid text format
                    if (o > 0 && o < 8)
                    {
                        textBrush = new SolidBrush(o < 4 ? ForeColor : ForeColorAlt);
                        FontStyle FS = ((o & 1) == 1) ? FontStyle.Bold : FontStyle.Regular;
                        FS |= ((o & 2) == 2) ? FontStyle.Italic : FontStyle.Regular;
                        textFont = new Font(this.Font.FontFamily, this.Font.Size, FS);
                        this.drawText(e, part.Substring(1).Trim(), textFont, textBrush, ref x, ref y, ref w, ref h);
                    }
                    else if (o == 0) //  0 = link
                    {
                        string urlText, urlLink;
                        if (part.Substring(1).Contains("|"))
                        {
                            string[] url = part.Substring(1).Split('|');
                            urlText = url[0];
                            urlLink = url[1];
                        }
                        else
                        {
                            urlText = urlLink = part.Substring(1);
                        }
                        List<Rectangle> linkRectangles;
                        textBrush = new SolidBrush(SystemColors.HotTrack);
                        textFont = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Underline);
                        this.drawText(e, urlText.Trim(), textFont, textBrush, ref x, ref y, ref w, ref h, out linkRectangles);
                        foreach (Rectangle rect in linkRectangles)
                        {
                            this.links.Add(rect, urlLink);
                        }
                    }
                    else // no formatting
                    {
                        textBrush = new SolidBrush(this.ForeColor);
                        this.drawText(e, part, this.Font, textBrush, ref x, ref y, ref w, ref h);
                    }
                }
            }
            // adjust label size
            if (this.CustomAutoSize)
            {
                this.Width = w + this.Padding.Right;
                this.Height = y + h + this.Padding.Bottom;
            }
            // clean up
            if (textBrush != null) textBrush.Dispose();
            if (textFont != null) textFont.Dispose();
        }

        /// <summary>
        /// Draws the text on the given position and respects the maximum width. 
        /// </summary>
        /// <param name="e">PaintEventArgs from the OnPaint method. </param>
        /// <param name="text">Text that should be drawn. </param>
        /// <param name="textFont">Text font that should be used. </param>
        /// <param name="colorBrush">Text brush that should be used. </param>
        /// <param name="x">X position where to draw. This will be updated. </param>
        /// <param name="y">Y position where to draw. This will be updated. </param>
        /// <param name="w">The total width of the control. This will be updated. </param>
        /// <param name="h">The total height of the control. This will be updated. </param>
        private void drawText(PaintEventArgs e, string text, Font textFont, Brush colorBrush, ref int x, ref int y, ref int w, ref int h)
        {
            List<Rectangle> dummy;
            this.drawText(e, text, textFont, colorBrush, ref x, ref y, ref w, ref h, out dummy);
        }

        /// <summary>
        /// Draws the text on the given position and respects the maximum width. 
        /// </summary>
        /// <param name="e">PaintEventArgs from the OnPaint method. </param>
        /// <param name="text">Text that should be drawn. </param>
        /// <param name="textFont">Text font that should be used. </param>
        /// <param name="colorBrush">Text brush that should be used. </param>
        /// <param name="x">X position where to draw. This will be updated. </param>
        /// <param name="y">Y position where to draw. This will be updated. </param>
        /// <param name="w">The total width of the control. This will be updated. </param>
        /// <param name="h">The total height of the control. This will be updated. </param>
        /// <param name="textRectangles">The returned rectangle collection of all text fragments. </param>
        private void drawText(PaintEventArgs e, string text, Font textFont, Brush colorBrush, ref int x, ref int y, ref int w, ref int h, out List<Rectangle> textRectangles)
        {
            textRectangles = new List<Rectangle>();
            string[][] linesWords = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Select(z => z.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
            for (int j = 0; j < linesWords.Length; j++)
            {
                if (j > 0)
                {
                    x = this.Padding.Left; // create a line break
                    y += h;
                }
                for (int k = 0; k < linesWords[j].Length; k++)
                {
                    string word = linesWords[j][k];
                    SizeF box = e.Graphics.MeasureString(word, textFont);
                    if (this.MaximumSize.Width > 0 && x + (int)box.Width > this.MaximumSize.Width - this.Padding.Right) // no more space for the word
                    {
                        x = this.Padding.Left; // create a line break
                        y += h;
                    }
                    e.Graphics.DrawString(word, textFont, colorBrush, new Point(x, y));
                    Rectangle rect = new Rectangle(x, y, (int)box.Width, (int)box.Height);
                    x += (int)(box.Width - (textFont.Bold ? word.Length * 0.2 : 0)); // bold letters are too large on the box calculating
                    w = Math.Max(w, x);
                    h = Math.Max(h, (int)box.Height - 1);
                    textRectangles.Add(rect);
                }
            }
        }

        /// <summary>
        /// Detects if a link was clicked. 
        /// Will be called when a click has been performed. 
        /// </summary>
        /// <param name="sender">Source control. </param>
        public void RichLabel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Rectangle r in links.Keys)
            {
                if (r.Contains(e.Location))
                {
                    Process.Start(links[r]);
                }
            }
        }

    }

}
