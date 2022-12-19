using AutoCreateWithJson.Component.BaseStructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

using Point = System.Drawing.Point;

namespace AutoCreateWithJson.Utility.EditImageCollection
{
    public partial class EditImageCollectionForm : Form
    {
        private SelectElementStoreable tempSelectElementStoreable;
        public EditImageCollectionForm()
        {
            InitializeComponent();
        }

        public void Init(SelectElementStoreable selectElementStoreable)
        {
            listBox1.Items.Clear();
            tempSelectElementStoreable = new SelectElementStoreable();
            for (int i = 0; i < selectElementStoreable.ImageCount; i++)
            {
                var bmp = selectElementStoreable.GetImage(i);
                var pt = selectElementStoreable.GetPoint(i);
                tempSelectElementStoreable.AddElementImage(bmp,pt);
                listBox1.Items.Add($"Image {i + 1}");
            }

            lbl_Count.Text = $"Count: {selectElementStoreable.ImageCount}";
            if (selectElementStoreable.ImageCount > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            if (index<0 || index>tempSelectElementStoreable.ImageCount-1)
                return;
           var bmp= tempSelectElementStoreable.GetImage(index);
           var pt = tempSelectElementStoreable.GetPoint(index);

           DrawImageToPictureBox(bmp,pt);
           
        }

        private void DrawImageToPictureBox(Bitmap bmp, Point pt)
        {
            lbl_Width.Text = $"Width: {bmp.Width}";
            lbl_Height.Text = $"Height: {bmp.Height}";
            lbl_X.Text = $"Click Point X: {pt.X}";
            lbl_Y.Text = $"Click Point Y: {pt.Y}";

            var res = CalculateImage(bmp, pictureBox1.Width, pictureBox1.Height, pt);

            pictureBox1.Image = res.bmp;
            imageBoundray = res.boundray;
            imageScale = res.scale;
        }

        private Rect imageBoundray;
        private double imageScale;
        private (Bitmap bmp, Rect boundray, double scale) CalculateImage(Bitmap elementBmp, int _width, int _height, Point pt)
        {
            var margin = 30;
            var width = _width - 2 * margin;
            var height = _height - 2 * margin;
            var scaleX = width / (double)elementBmp.Width;
            var scaleY = height / (double)elementBmp.Height;
            var scale = scaleX < scaleY ? scaleX : scaleY;

            var scaleWidth = (int)(elementBmp.Width * scale);
            var scaleHeight = (int)(elementBmp.Height * scale);


            var boundray = new Rect();
            var bmp = new Bitmap(_width, _height);
            var x = (width - scaleWidth) / 2;
            var y = (height - scaleHeight) / 2 ;
            var ptx = (int) (pt.X * scale);
            var pty = (int) (pt.Y * scale);

            using (Graphics g = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.White))
            using (var pen = new Pen(Color.Red, 1))
            {
                pen.DashStyle = DashStyle.Custom;
                pen.DashPattern = new float[] { 2, 2 };
                g.FillRectangle(brush, 0,0, _width, _height);
                g.DrawImage(elementBmp, x+ margin, y+ margin, scaleWidth, scaleHeight);
                // دو خط عمودی محور مختصات نقطه کلیک شده
                g.DrawLine(pen, 0, margin +y+ pty, _width- 1, margin+y + pty);
                g.DrawLine(pen, margin+x + ptx, 0, margin+x + ptx, _height-1);
            }
            boundray.X = margin+x;
            boundray.Y = margin+y;
            boundray.Width = scaleWidth;
            boundray.Height = scaleHeight;
            return new(bmp, boundray, scale);
        }


        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool isMouseDown = false;
        private Point lastPoint = new Point(0, 0);
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            NewPositionOfMouseRefresh(e.X, e.Y);
        }

        private void NewPositionOfMouseRefresh(int X, int Y)
        {
            lastPoint.X = X;
            lastPoint.Y = Y;
            if (X >= imageBoundray.Left && X <= imageBoundray.Right &&
                Y >= imageBoundray.Top && Y < imageBoundray.Bottom)
            {
                var x = X - imageBoundray.Left;
                var y = Y - imageBoundray.Top;
                var index = listBox1.SelectedIndex;
                var bmp = tempSelectElementStoreable.GetImage(index);
                var pt = new Point((int)(x / imageScale), (int)(y / imageScale));

                DrawImageToPictureBox(bmp, pt);
                tempSelectElementStoreable.SetPoint(index, pt);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown)
                return;
            if (lastPoint.X == e.X && lastPoint.Y == e.Y)
                return;
            NewPositionOfMouseRefresh(e.X, e.Y);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Close();
        }

        public Point GetImagePoint(int index)
        {
            return tempSelectElementStoreable.GetPoint(index);
        }
    }
}
