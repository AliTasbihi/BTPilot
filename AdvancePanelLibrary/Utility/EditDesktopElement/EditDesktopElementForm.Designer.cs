
namespace AdvancePanelLibrary.Utility.EditDesktopElement
{
    partial class EditDesktopElementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDesktopElementForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Structure = new System.Windows.Forms.TabPage();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.tv_ElemetStructure = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnl_ElemetStructure = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelCondition = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_SetClickPoint = new System.Windows.Forms.Label();
            this.img_Image = new System.Windows.Forms.PictureBox();
            this.combo_ValidationDelay = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_CaptureNewElement = new System.Windows.Forms.Button();
            this.ed_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tabControl1.SuspendLayout();
            this.tabPage_Structure.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Structure);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(428, 407);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_Structure
            // 
            this.tabPage_Structure.Controls.Add(this.splitter2);
            this.tabPage_Structure.Controls.Add(this.tv_ElemetStructure);
            this.tabPage_Structure.Controls.Add(this.pnl_ElemetStructure);
            this.tabPage_Structure.Location = new System.Drawing.Point(4, 24);
            this.tabPage_Structure.Name = "tabPage_Structure";
            this.tabPage_Structure.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Structure.Size = new System.Drawing.Size(420, 379);
            this.tabPage_Structure.TabIndex = 1;
            this.tabPage_Structure.Text = "Structure";
            this.tabPage_Structure.UseVisualStyleBackColor = true;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(154, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 373);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            // 
            // tv_ElemetStructure
            // 
            this.tv_ElemetStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_ElemetStructure.HideSelection = false;
            this.tv_ElemetStructure.ImageIndex = 0;
            this.tv_ElemetStructure.ImageList = this.imageList1;
            this.tv_ElemetStructure.Location = new System.Drawing.Point(3, 3);
            this.tv_ElemetStructure.Name = "tv_ElemetStructure";
            this.tv_ElemetStructure.SelectedImageIndex = 0;
            this.tv_ElemetStructure.Size = new System.Drawing.Size(154, 373);
            this.tv_ElemetStructure.TabIndex = 0;
            this.tv_ElemetStructure.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_ElemetStructure_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "06.bmp");
            this.imageList1.Images.SetKeyName(1, "03.bmp");
            this.imageList1.Images.SetKeyName(2, "05.bmp");
            this.imageList1.Images.SetKeyName(3, "01.bmp");
            this.imageList1.Images.SetKeyName(4, "02.bmp");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            // 
            // pnl_ElemetStructure
            // 
            this.pnl_ElemetStructure.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_ElemetStructure.Location = new System.Drawing.Point(157, 3);
            this.pnl_ElemetStructure.Name = "pnl_ElemetStructure";
            this.pnl_ElemetStructure.Size = new System.Drawing.Size(260, 373);
            this.pnl_ElemetStructure.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelCondition);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.combo_ValidationDelay);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_CaptureNewElement);
            this.panel1.Controls.Add(this.ed_Name);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_Cancel);
            this.panel1.Controls.Add(this.btn_Save);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(431, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 407);
            this.panel1.TabIndex = 1;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // panelCondition
            // 
            this.panelCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCondition.AutoScroll = true;
            this.panelCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCondition.Location = new System.Drawing.Point(8, 184);
            this.panelCondition.Name = "panelCondition";
            this.panelCondition.Size = new System.Drawing.Size(432, 171);
            this.panelCondition.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbl_SetClickPoint);
            this.panel2.Controls.Add(this.img_Image);
            this.panel2.Location = new System.Drawing.Point(8, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 89);
            this.panel2.TabIndex = 7;
            // 
            // lbl_SetClickPoint
            // 
            this.lbl_SetClickPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SetClickPoint.AutoSize = true;
            this.lbl_SetClickPoint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_SetClickPoint.Location = new System.Drawing.Point(323, 71);
            this.lbl_SetClickPoint.Name = "lbl_SetClickPoint";
            this.lbl_SetClickPoint.Size = new System.Drawing.Size(81, 15);
            this.lbl_SetClickPoint.TabIndex = 1;
            this.lbl_SetClickPoint.Text = "Set click point";
            this.lbl_SetClickPoint.Click += new System.EventHandler(this.lbl_SetClickPoint_Click);
            // 
            // img_Image
            // 
            this.img_Image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.img_Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.img_Image.Location = new System.Drawing.Point(34, 17);
            this.img_Image.Name = "img_Image";
            this.img_Image.Size = new System.Drawing.Size(370, 51);
            this.img_Image.TabIndex = 0;
            this.img_Image.TabStop = false;
            // 
            // combo_ValidationDelay
            // 
            this.combo_ValidationDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_ValidationDelay.FormattingEnabled = true;
            this.combo_ValidationDelay.Items.AddRange(new object[] {
            "None",
            "1 Sec",
            "3 Sec",
            "5 Sec",
            "10 Sec",
            "15 Sec",
            "20 Sec"});
            this.combo_ValidationDelay.Location = new System.Drawing.Point(126, 47);
            this.combo_ValidationDelay.Name = "combo_ValidationDelay";
            this.combo_ValidationDelay.Size = new System.Drawing.Size(121, 23);
            this.combo_ValidationDelay.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Validation delay:";
            // 
            // btn_CaptureNewElement
            // 
            this.btn_CaptureNewElement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_CaptureNewElement.Location = new System.Drawing.Point(13, 372);
            this.btn_CaptureNewElement.Name = "btn_CaptureNewElement";
            this.btn_CaptureNewElement.Size = new System.Drawing.Size(130, 23);
            this.btn_CaptureNewElement.TabIndex = 4;
            this.btn_CaptureNewElement.Text = "Capture New Element";
            this.btn_CaptureNewElement.UseVisualStyleBackColor = true;
            this.btn_CaptureNewElement.Click += new System.EventHandler(this.btn_CaptureNewElement_Click);
            // 
            // ed_Name
            // 
            this.ed_Name.Location = new System.Drawing.Point(67, 12);
            this.ed_Name.Name = "ed_Name";
            this.ed_Name.Size = new System.Drawing.Size(273, 23);
            this.ed_Name.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name: ";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(254, 372);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Location = new System.Drawing.Point(347, 372);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(91, 23);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(428, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 407);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // EditDesktopElementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 407);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "EditDesktopElementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditDesktopElementForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Structure.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Structure;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox combo_ValidationDelay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_CaptureNewElement;
        private System.Windows.Forms.TextBox ed_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox img_Image;
        private System.Windows.Forms.Panel pnl_ElemetStructure;
        private System.Windows.Forms.TreeView tv_ElemetStructure;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelCondition;
        private System.Windows.Forms.Label lbl_SetClickPoint;
    }
}