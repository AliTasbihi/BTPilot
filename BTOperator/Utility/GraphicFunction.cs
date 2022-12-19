using AutoCreateWithJson.Component.BaseElements;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Utility
{
    public static class GraphicFunction
    {
        public static Bitmap ResizeImage(Bitmap bmp, int srcWidth, int srcHight, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destBmp = new Bitmap(width, height);

            destBmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

            using (var graphics = Graphics.FromImage(destBmp))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    //graphics.DrawImage(bmp, destRect, 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, wrapMode);
                    graphics.DrawImage(bmp, destRect, 0, 0, srcWidth, srcHight, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destBmp;
        }

        public static Rectangle DrawTextWithAlinment(Graphics graphics, string title, MyTextSize mySize, ContentAlignment alinment, Rectangle rec, Color textColor, bool isWapping = false, bool isReadOnly = false)
        {
            if (isReadOnly)
            {
                textColor = Color.FromArgb(161, 159, 162);
            }
            Font font;
            switch (mySize)
            {
                case MyTextSize.Small:
                    font = GraphicConstant.textFontSmall;
                    break;
                case MyTextSize.Medium:
                    font = GraphicConstant.textFontMedium;
                    break;
                case MyTextSize.Large:
                    font = GraphicConstant.textFontLarge;
                    break;
                case MyTextSize.ExtraLarge:
                    font = GraphicConstant.textFontExtraLarge;
                    break;
                case MyTextSize.DoubleExtraLarge:
                    font = GraphicConstant.textFontDoubleExtraLarge;
                    break;
                default:
                    font = GraphicConstant.textFontMedium;
                    break;
            }

            Brush fg = new SolidBrush(textColor);
            if (isWapping)
            {
                graphics.DrawString(title, font, fg, rec);
                return rec;
            }
 

                var measuredText = GraphicFunction.MeasureText(graphics, title, mySize);
            var ptInsert = new Point(0, 0);
            switch (alinment)
            {
                case ContentAlignment.TopLeft:

                    break;
                case ContentAlignment.TopCenter:
                    ptInsert.X = Convert.ToInt32((rec.Width - measuredText.Width) / 2);
                    break;
                case ContentAlignment.TopRight:
                    ptInsert.X = Convert.ToInt32(rec.Width - measuredText.Width);
                    break;
                case ContentAlignment.MiddleLeft:
                    ptInsert.Y = Convert.ToInt32((rec.Height - measuredText.Height) / 2);
                    break;
                case ContentAlignment.MiddleCenter:
                    ptInsert.X = Convert.ToInt32((rec.Width - measuredText.Width) / 2);
                    ptInsert.Y = Convert.ToInt32((rec.Height - measuredText.Height) / 2);
                    break;
                case ContentAlignment.MiddleRight:
                    ptInsert.X = Convert.ToInt32(rec.Width - measuredText.Width);
                    ptInsert.Y = Convert.ToInt32((rec.Height - measuredText.Height) / 2);
                    break;
                case ContentAlignment.BottomLeft:
                    ptInsert.Y = Convert.ToInt32(rec.Height - measuredText.Height);
                    break;
                case ContentAlignment.BottomCenter:
                    ptInsert.X = Convert.ToInt32((rec.Width - measuredText.Width) / 2);
                    ptInsert.Y = Convert.ToInt32(rec.Height - measuredText.Height);
                    break;
                case ContentAlignment.BottomRight:
                    ptInsert.X = Convert.ToInt32(rec.Width - measuredText.Width);
                    ptInsert.Y = Convert.ToInt32(rec.Height - measuredText.Height);
                    break;
            }

            var res = new Rectangle(rec.Left + ptInsert.X, rec.Top + ptInsert.Y + 2, (Int32)measuredText.Width, (Int32)measuredText.Height);
            graphics.DrawString(title, font, fg, res.Left, res.Top);
            return res;
        }

        public static void DrawLine(Graphics bmpGfx, int x1, int y1, int x2, int y2, Color penColor, int penWidth = 1)
        {
            Pen pn = new Pen(penColor, penWidth);
            bmpGfx.DrawLine(pn, x1, y1, x2, y2);
        }
        public static void DrawFillRectangle(Graphics bmpGfx, Rectangle rec, Color backGroundColor, Color borderColor, int penWidth = 1)
        {
            Brush bg = new SolidBrush(backGroundColor);
            bmpGfx.FillRectangle(bg, rec);
            Pen pn = new Pen(borderColor, penWidth);
            bmpGfx.DrawLine(pn, rec.Left, rec.Top, rec.Right, rec.Top);
            bmpGfx.DrawLine(pn, rec.Right, rec.Top, rec.Right, rec.Bottom);
            bmpGfx.DrawLine(pn, rec.Right, rec.Bottom, rec.Left, rec.Bottom);
            bmpGfx.DrawLine(pn, rec.Left, rec.Bottom, rec.Left, rec.Top);
        }

        private static void DrawTextComboboxEditbox(Graphics bmpGfx, Rectangle rec, string selectedText, Color backGroundColor, Color borderColor, bool isCombobox, bool isEditbox, bool isWapping, bool isReadOnly)
        {
            var textColor = Color.Black;
            if (isReadOnly)
            {
                backGroundColor = Color.FromArgb(231, 231, 230);
                borderColor = Color.FromArgb(207, 207, 206);
                textColor = Color.FromArgb(161, 159, 162);
            }
            Brush bg = new SolidBrush(backGroundColor);
            bmpGfx.FillRectangle(bg, rec);
            Pen pn = new Pen(borderColor, 1);
            bmpGfx.DrawLine(pn, rec.Left, rec.Top, rec.Right, rec.Top);
            bmpGfx.DrawLine(pn, rec.Right, rec.Top, rec.Right, rec.Bottom);
            bmpGfx.DrawLine(pn, rec.Right, rec.Bottom, rec.Left, rec.Bottom);
            bmpGfx.DrawLine(pn, rec.Left, rec.Bottom, rec.Left, rec.Top);

            if (isCombobox)
                DrawArrowUpDown(bmpGfx, rec, false, true, 7, borderColor);

            if (!string.IsNullOrEmpty(selectedText))
            {
                DrawTextWithAlinment(bmpGfx, selectedText, MyTextSize.Medium, ContentAlignment.TopLeft,
                      new Rectangle(rec.Left + 2, rec.Top + 1, rec.Width - 24, rec.Height), textColor, isWapping);
            }
        }
        public static void DrawTextCombobox(Graphics bmpGfx, Rectangle rec, string selectedText, Color backGroundColor, Color borderColor, bool isReadOnly = false)
        {
            DrawTextComboboxEditbox(bmpGfx, rec, selectedText, backGroundColor, borderColor, true, false, false, isReadOnly);
        }
        public static void DrawTextEditbox(Graphics bmpGfx, Rectangle rec, string text, Color backGroundColor, Color borderColor, bool isWapping, bool isReadOnly)
        {
            DrawTextComboboxEditbox(bmpGfx, rec, text, backGroundColor, borderColor, false, true, isWapping, isReadOnly);
        }

        public static Rectangle DrawArrowUpDown(Graphics graphics, Rectangle rec, bool isArrowUp, bool isRightSide, int size, Color penColor)
        {
            var dy = (rec.Height - size) / 2;
            var pn = new Pen(penColor, 2);
            Point[] points = new Point[3];
            if (isRightSide)
            {
                if (isArrowUp)
                {
                    points[0] = new Point(rec.Right - size, rec.Top + dy + size);
                    points[1] = new Point(rec.Right - 2 * size, rec.Top + dy);
                    points[2] = new Point(rec.Right - 3 * size, rec.Top + dy + size);
                }
                else
                {
                    points[0] = new Point(rec.Right - size, rec.Top + dy);
                    points[1] = new Point(rec.Right - 2 * size, rec.Top + dy + size);
                    points[2] = new Point(rec.Right - 3 * size, rec.Top + dy);
                }
            }
            else
            {
                if (isArrowUp)
                {
                    points[0] = new Point(rec.Left + size, rec.Top + dy + size);
                    points[1] = new Point(rec.Left + 2 * size, rec.Top + dy);
                    points[2] = new Point(rec.Left + 3 * size, rec.Top + dy + size);
                }
                else
                {
                    points[0] = new Point(rec.Left + size, rec.Top + dy);
                    points[1] = new Point(rec.Left + 2 * size, rec.Top + dy + size);
                    points[2] = new Point(rec.Left + 3 * size, rec.Top + dy);
                }
            }
            graphics.DrawLines(pn, points);

            var xMin = Math.Min(points[0].X, points[2].X);
            var xMax = Math.Max(points[0].X, points[2].X);
            var yMin = Math.Min(points[1].Y, points[2].Y);
            var yMax = Math.Max(points[1].Y, points[2].Y);
            return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        public static Rectangle DrawCheckbox(Graphics graphics, Rectangle rec, bool isChecked, bool isRightSide, int size, Color penColor, bool isReadOnly)
        {
            if (isReadOnly)
            {
                penColor = Color.FromArgb(161, 159, 162);
            }
            var dy = (rec.Height - size) / 2;
            var pn = new Pen(penColor, 1);
            Point[] points = new Point[5];
            if (isRightSide)
            {
                points[0] = new Point(rec.Right - size / 2, rec.Top + dy);
                points[1] = new Point(rec.Right - size / 2, rec.Top + dy + size);
                points[2] = new Point(rec.Right - 3 * size / 2, rec.Top + dy + size);
                points[3] = new Point(rec.Right - 3 * size / 2, rec.Top + dy);
                points[4] = points[0];

            }
            else
            {
                points[0] = new Point(rec.Left + size / 2, rec.Top + dy);
                points[1] = new Point(rec.Left + size / 2, rec.Top + dy + size);
                points[2] = new Point(rec.Left + 3 * size / 2, rec.Top + dy + size);
                points[3] = new Point(rec.Left + 3 * size / 2, rec.Top + dy);
                points[4] = points[0];

            }
            graphics.DrawLines(pn, points);

            if (isChecked)
            {
                graphics.DrawLine(pn, points[0], points[2]);
                graphics.DrawLine(pn, points[1], points[3]);
            }
            return new Rectangle(points[0].X, points[0].Y, points[3].X - points[0].X, points[3].Y - points[0].Y);
        }

        public static SizeF MeasureText(Graphics graphics, string title, MyTextSize mySize, int MaxWidthForWrapping = 0)
        {
            var Padding1 = 5;
            SizeF measuredText;
            var rect = new RectangleF(0, 0, MaxWidthForWrapping, 1000);
            var isWrapping = MaxWidthForWrapping > 0;
            switch (mySize)
            {
                case MyTextSize.Small:
                    if (isWrapping)
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontSmall, rect.Size);
                    else
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontSmall);
                    break;
                case MyTextSize.Medium:
                    if (isWrapping)
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontMedium, rect.Size);
                    else
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontMedium);
                    break;
                case MyTextSize.Large:
                    if (isWrapping)
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontLarge, rect.Size);
                    else
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontLarge);
                    break;
                case MyTextSize.ExtraLarge:
                    if (isWrapping)
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontExtraLarge, rect.Size);
                    else
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontExtraLarge);
                    break;
                case MyTextSize.DoubleExtraLarge:
                    if (isWrapping)
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontDoubleExtraLarge, rect.Size);
                    else
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontDoubleExtraLarge);
                    break;
                default:
                    if (isWrapping)
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontMedium, rect.Size);
                    else
                        measuredText = graphics.MeasureString(title, GraphicConstant.textFontMedium);
                    break;
            }
            return new SizeF(measuredText.Width + Padding1, measuredText.Height + Padding1);
        }

        public static void DrawAlphaFillRectangleWithCrop(Graphics graphics, Rectangle rec, int ptFromX, int ptFromY, int ptToX, int ptToY, Rectangle displayArea, Color color, int alpha)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen pen = new Pen(Color.FromArgb(alpha, color.R, color.G, color.B));
            pen.Width = 3.0F;
            pen.DashCap = DashCap.Round;
            pen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };

            Brush brush = new SolidBrush(Color.FromArgb(alpha, color.R, color.G, color.B));
            graphics.DrawLine(pen, ptFromX, ptFromY, ptToX, ptToY);

            var dx = ptToX - ptFromX;
            var dy = ptToY - ptFromY;

            var x1 = rec.Left + dx;
            var y1 = rec.Top + dy;
            var x2 = x1 + rec.Width;
            var y2 = y1 + rec.Height;

            x1 = (x1 < displayArea.Left) ? displayArea.Left : x1;
            y1 = (y1 < displayArea.Top) ? displayArea.Top : y1;
            x2 = (x2 > displayArea.Right) ? displayArea.Right : x2;
            y2 = (y2 > displayArea.Bottom) ? displayArea.Bottom : y2;

            graphics.FillRectangle(brush, x1, y1, x2 - x1, y2 - y1);

            graphics.DrawLine(pen, x1, y1, x2, y1);
            graphics.DrawLine(pen, x2, y1, x2, y2);
            graphics.DrawLine(pen, x2, y2, x1, y2);
            graphics.DrawLine(pen, x1, y2, x1, y1);


            pen.Dispose();
        }

        public static void DrawRectangleWithLine(Graphics graphics, Pen pen, Rectangle rec)
        {
            graphics.DrawLine(pen, rec.Left, rec.Top, rec.Right, rec.Top);
            graphics.DrawLine(pen, rec.Right, rec.Top, rec.Right, rec.Bottom);
            graphics.DrawLine(pen, rec.Right, rec.Bottom, rec.Left, rec.Bottom);
            graphics.DrawLine(pen, rec.Left, rec.Bottom, rec.Left, rec.Top);
        }

        public static PointsArrowBezier CalculateBezierPoints(int x1, int y1, int x2, int y2)
        {
            var res = new PointsArrowBezier();

            var minDx = GraphicConstant.bluildingBlockWidth / 3;
            var minDy = GraphicConstant.bluildingBlockWidth / 5;

            var arrowWidth = 10;
            var arrowHalfHeight = 6;
            var penWidth = 3;

            var x3 = x2 - arrowWidth;
            var y3 = y2;

            var dx = x3 - x1;
            var dy = y3 - y1;

            res.rightArrowPoints[0].X = x2;
            res.rightArrowPoints[0].Y = y2;

            res.rightArrowPoints[1].X = x3;
            res.rightArrowPoints[1].Y = y2 - arrowHalfHeight;

            res.rightArrowPoints[2].X = x3;
            res.rightArrowPoints[2].Y = y2 + arrowHalfHeight;

            if (x2 - x1 >= -2)
            {
                res.bezierCount = 1;
                if (Math.Abs(dy) < 15)
                    minDx = dx;
                var xMid = (x1 + x3) / 2;
                var yMid = (y1 + y3) / 2;

                // Bezier Points
                res.bezierPoints1[0].X = x1;
                res.bezierPoints1[0].Y = y1;

                res.bezierPoints1[1].X = Math.Max(xMid, x1 + minDx);
                res.bezierPoints1[1].Y = y1;

                res.bezierPoints1[2].X = Math.Min(xMid, x3 - minDx);
                res.bezierPoints1[2].Y = y3;

                res.bezierPoints1[3].X = x3;
                res.bezierPoints1[3].Y = y3;

                res.gpBezier1 = new GraphicsPath();
                res.gpBezier1.AddBezier(res.bezierPoints1[0], res.bezierPoints1[1], res.bezierPoints1[2], res.bezierPoints1[3]);
            }
            else
            {
                res.bezierCount = 2;
                int a1x = x1 + minDx;
                int a1y = y1;
                int a4x = x3 - minDx;
                int a4y = y3;
                int a2x = a1x;
                int a2y = 0;
                int a3x = a4x;
                int a3y = 0;
                int amidx = 0;
                int amidy = 0;
                if (y1 - y2 > minDx)
                {
                    a2y = a1y - minDx;
                    a3y = a4y + minDx;
                }
                else if (Math.Abs(y1 - y2) <= minDx)
                {
                    a2y = a1y - minDx;
                    a3y = a4y - minDx;
                }
                else
                {
                    a2y = a1y + minDx;
                    a3y = a4y - minDx;
                }

                amidx = (a2x + a3x) / 2;
                amidy = (a2y + a3y) / 2;

                res.bezierPoints1[0].X = x1;
                res.bezierPoints1[0].Y = y1;
                res.bezierPoints1[1].X = a1x;
                res.bezierPoints1[1].Y = a1y;
                res.bezierPoints1[2].X = a2x;
                res.bezierPoints1[2].Y = a2y;
                res.bezierPoints1[3].X = amidx;
                res.bezierPoints1[3].Y = amidy;

                res.bezierPoints2[0].X = x3;
                res.bezierPoints2[0].Y = y3;
                res.bezierPoints2[1].X = a4x;
                res.bezierPoints2[1].Y = a4y;
                res.bezierPoints2[2].X = a3x;
                res.bezierPoints2[2].Y = a3y;
                res.bezierPoints2[3].X = amidx;
                res.bezierPoints2[3].Y = amidy;

                res.gpBezier1 = new GraphicsPath();
                res.gpBezier1.AddBezier(res.bezierPoints1[0], res.bezierPoints1[1], res.bezierPoints1[2], res.bezierPoints1[3]);
                res.gpBezier2 = new GraphicsPath();
                res.gpBezier2.AddBezier(res.bezierPoints2[0], res.bezierPoints2[1], res.bezierPoints2[2], res.bezierPoints2[3]);
            }


            return res;
        }

        public static void CopyRegionIntoImageFile(Bitmap srcBitmap, Rectangle srcRegion, string filename)
        {
            using (Bitmap destBitmap = new Bitmap(srcRegion.Width, srcRegion.Height))
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, srcRegion, srcRegion, GraphicsUnit.Pixel);
                destBitmap.Save(filename, ImageFormat.Png);
            }
        }
    }
}
