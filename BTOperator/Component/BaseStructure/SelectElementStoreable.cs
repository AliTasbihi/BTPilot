using AutoCreateWithJson.Utility;
using AutoCreateWithJson.Utility.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseStructure
{
    public class SelectElementStoreable
    {
        public string ElementName = "";
        public int ValidationDelay = 0;

        private List<Bitmap> Images = new List<Bitmap>();
        private List<Point> ClickPoints = new List<Point>();
        public ConditionForSelectElement ConditionForSelectElement;
        private Rectangle lastRectangle;
        public int Count
        {
            get { return Images.Count; }
        }
        public int ElementCount
        {
            get { return Images.Count; }
        }
        public int ImageCount
        {
            get { return Images.Count; }
        }

        public string SerializeToString()
        {
            CustomArrayBase64 customArrayBase64 = new CustomArrayBase64("");
            customArrayBase64.AddString(ElementName);
            customArrayBase64.AddInteger(ValidationDelay);
            customArrayBase64.AddRectangle(lastRectangle);
            customArrayBase64.AddInteger(Images.Count);
            for (int i = 0; i < Images.Count; i++)
            {
                customArrayBase64.AddImage(Images[i]);
                customArrayBase64.AddPoint(ClickPoints[i]);

            }
            if (ConditionForSelectElement is not null)
                customArrayBase64.AddString(ConditionForSelectElement.SerializeToString());

            return customArrayBase64.GetAllAsString();
        }

        public void DeserializeFromString(string value)
        {
            Clear();
            if (value == "")
                return;
            CustomArrayBase64 customArrayBase64 = new CustomArrayBase64(value);
            ElementName = customArrayBase64.GetString();
            ValidationDelay = customArrayBase64.GetInteger();
            lastRectangle = customArrayBase64.GetRectangle();
            int count = customArrayBase64.GetInteger();
            for (int i = 0; i < count; i++)
            {
                Bitmap bmp = customArrayBase64.GetImage();
                Point pt = customArrayBase64.GetPoint();
                AddElementImage(bmp, pt);
            }
            ConditionForSelectElement = new ConditionForSelectElement(0);
            ConditionForSelectElement.DeserializeFromString(customArrayBase64.GetStringWithoutRaise());
        }


        public Bitmap DrawToPictureBox(Rectangle _rect)
        {
            if (_rect.Width == 0 || _rect.Height == 0)
            {
                if (lastRectangle == null)
                    return null;
                if (lastRectangle.Width == 0 || lastRectangle.Height == 0)
                    return null;
            }
            else
                lastRectangle = _rect;

            var bmp = GetEmptyImage(lastRectangle.Width, lastRectangle.Height);
            if (ImageCount == 0)
            {
                return bmp;
            }
            var firstBmp = Images[0];

            var dx1 = ImageCount > 1 ? 8 : 0;
            var dx2 = ImageCount > 1 ? 1 : 0;

            var scaleX = (lastRectangle.Width - dx1) / (double)firstBmp.Width;
            var scaleY = (lastRectangle.Height - dx1) / (double)firstBmp.Height;
            var scale = scaleX < scaleY ? scaleX : scaleY;

            var scaleWidth = (int)(firstBmp.Width * scale);
            var scaleHeight = (int)(firstBmp.Height * scale);
            using (Graphics g = Graphics.FromImage(bmp))
            using (var brush1 = new SolidBrush(Color.Red))
            using (var brush2 = new SolidBrush(Color.White))
            using (var penWhite = new Pen(Color.White, 1))
            using (var penDark = new Pen(Color.FromArgb(241, 240, 238), 1))
            {
                var x = (lastRectangle.Width - scaleWidth) / 2 + dx2;
                var y = (lastRectangle.Height - scaleHeight) / 2 + dx2;

                if (ImageCount > 1)
                {
                    for (int i = 6; i >= 0; i -= 2)
                    {
                        g.FillRectangle(brush1, x + i, y + i, scaleWidth, scaleHeight);
                        if (i % 2 == 0)
                            g.DrawRectangle(penDark, x + i, y + i, scaleWidth, scaleHeight);
                        else
                            g.DrawRectangle(penWhite, x + i, y + i, scaleWidth, scaleHeight);
                    }
                }
                g.DrawImage(firstBmp, x, y, scaleWidth, scaleHeight);
                if (ImageCount > 1)
                {
                    g.FillRectangle(brush1, x, y, 10, 15);
                    g.DrawString($"{ImageCount }", GraphicConstant.textFontSmall, brush2, new Point(x, y));
                }
            }

            return bmp;
        }

        private Bitmap GetEmptyImage(int width, int height)
        {
            var delta = 10;
            var bmp = new Bitmap(width, height);
            var count1 = 0;
            var color1 = Color.FromArgb(61, 61, 61);
            var color2 = Color.FromArgb(51, 51, 51);
            using (Graphics g = Graphics.FromImage(bmp))
            using (SolidBrush brush1 = new SolidBrush(color1))
            using (SolidBrush brush2 = new SolidBrush(color2))
            {
                for (int x = 0; x <= width / delta; x++)
                {
                    count1++;
                    var count2 = count1;
                    for (int y = 0; y <= height / delta; y++)
                    {
                        var xx = x * delta;
                        var yy = y * delta;
                        var rec = new Rectangle(xx, yy, delta, delta);
                        g.FillRectangle(count2 % 2 == 0 ? brush1 : brush2, rec);
                        count2++;
                    }
                }
            }
            return bmp;
        }

        public void AddElementImage(Bitmap bmp, Point point)
        {
            Images.Clear();
            ClickPoints.Clear();
            Images.Add(bmp);
            ClickPoints.Add(point);
        }

        public void Clear()
        {
            Images.Clear();
            ClickPoints.Clear();
            if (ConditionForSelectElement != null)
                ConditionForSelectElement.Clear();
        }

        public Bitmap GetImage(int index)
        {
            return Images[index];
        }

        public Point GetPoint(int index)
        {
            return ClickPoints[index];
        }

        public void SetPoint(int index, Point pt)
        {
            ClickPoints[index] = pt;
        }

        public SelectElementStoreable CloneObject()
        {
            var clone = new SelectElementStoreable();
            for (int i = 0; i < ImageCount; i++)
            {
                clone.AddElementImage(Images[i], ClickPoints[i]);
            }

            clone.ElementName = this.ElementName;
            clone.ValidationDelay = this.ValidationDelay;
            if (ConditionForSelectElement is not null)
                clone.ConditionForSelectElement = ConditionForSelectElement.CloneObject();

            return clone;
        }
    }

}
