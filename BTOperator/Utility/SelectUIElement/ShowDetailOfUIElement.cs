using AutoCreateWithJson.Component.BaseStructure;
using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCreateWithJson.Utility.SelectUIElement
{
    public partial class ShowDetailOfUIElement : Form
    {
        public ShowDetailOfUIElement()
        {
            InitializeComponent();
        }

        private int initWidth = 0;
        private int initHeight = 0;
        private int curentY = 0;

        public bool ResultForm { get; private set; }

        public void Init(AutomationElement selectCurrentElement)
        {
            ResultForm = false;
            initWidth = pictureBox1.Width;
            initHeight = pictureBox1.Height;
            lbl_Autosize.Left = lbl_Stretch.Left;
            lbl_Autosize.Top = lbl_Stretch.Top;

            pictureBox1.Image = selectCurrentElement.Capture();
            curentY = pictureBox1.Top + pictureBox1.Height + 5;

            var _elementViewAllDetailWithParents = new ElementViewAllDetail(selectCurrentElement, null, false);
            _elementViewAllDetailWithParents.StartAnalyze();
            _elementViewAllDetailWithParents.DrawPropertyToPanel(pnl_Details, 0,0);
            lbl_Stretch_Click(null,null);
        }
        private void lbl_Stretch_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Width = initWidth;
            pictureBox1.Height = initHeight;
            lbl_Stretch.Visible = false;
            lbl_Autosize.Visible = true;
        }

        private void lbl_Autosize_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            lbl_Autosize.Visible = false;
            lbl_Stretch.Visible = true;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            ResultForm = false;
            Close();
        }

        private void btn_CloseAndSelect_Click(object sender, EventArgs e)
        {
            ResultForm = true;
            Close();
        }
    }
}
