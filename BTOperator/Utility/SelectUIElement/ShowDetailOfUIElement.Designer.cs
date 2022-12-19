
namespace AutoCreateWithJson.Utility.SelectUIElement
{
    partial class ShowDetailOfUIElement
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
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_CloseAndSelect = new System.Windows.Forms.Button();
            this.pnl_Details = new System.Windows.Forms.Panel();
            this.lbl_Autosize = new System.Windows.Forms.Label();
            this.lbl_Stretch = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Location = new System.Drawing.Point(367, 406);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(139, 23);
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "Close && Continue";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_CloseAndSelect
            // 
            this.btn_CloseAndSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CloseAndSelect.Location = new System.Drawing.Point(188, 406);
            this.btn_CloseAndSelect.Name = "btn_CloseAndSelect";
            this.btn_CloseAndSelect.Size = new System.Drawing.Size(148, 23);
            this.btn_CloseAndSelect.TabIndex = 2;
            this.btn_CloseAndSelect.Text = "Close && Select Elemant";
            this.btn_CloseAndSelect.UseVisualStyleBackColor = true;
            this.btn_CloseAndSelect.Click += new System.EventHandler(this.btn_CloseAndSelect_Click);
            // 
            // pnl_Details
            // 
            this.pnl_Details.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Details.Location = new System.Drawing.Point(6, 125);
            this.pnl_Details.Name = "pnl_Details";
            this.pnl_Details.Size = new System.Drawing.Size(506, 275);
            this.pnl_Details.TabIndex = 3;
            // 
            // lbl_Autosize
            // 
            this.lbl_Autosize.AutoSize = true;
            this.lbl_Autosize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Autosize.Location = new System.Drawing.Point(98, 9);
            this.lbl_Autosize.Name = "lbl_Autosize";
            this.lbl_Autosize.Size = new System.Drawing.Size(52, 15);
            this.lbl_Autosize.TabIndex = 6;
            this.lbl_Autosize.Text = "Autosize";
            this.lbl_Autosize.Click += new System.EventHandler(this.lbl_Autosize_Click);
            // 
            // lbl_Stretch
            // 
            this.lbl_Stretch.AutoSize = true;
            this.lbl_Stretch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Stretch.Location = new System.Drawing.Point(12, 9);
            this.lbl_Stretch.Name = "lbl_Stretch";
            this.lbl_Stretch.Size = new System.Drawing.Size(44, 15);
            this.lbl_Stretch.TabIndex = 5;
            this.lbl_Stretch.Text = "Stretch";
            this.lbl_Stretch.Click += new System.EventHandler(this.lbl_Stretch_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(151, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // ShowDetailOfUIElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Close;
            this.ClientSize = new System.Drawing.Size(518, 441);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_Autosize);
            this.Controls.Add(this.lbl_Stretch);
            this.Controls.Add(this.pnl_Details);
            this.Controls.Add(this.btn_CloseAndSelect);
            this.Controls.Add(this.btn_Close);
            this.Name = "ShowDetailOfUIElement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowDetailOfUIElement";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_CloseAndSelect;
        private System.Windows.Forms.Panel pnl_Details;
        private System.Windows.Forms.Label lbl_Autosize;
        private System.Windows.Forms.Label lbl_Stretch;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}