using AutoCreateWithJson.Component.BaseGeneral;

namespace AutoCreateWithJson.Utility.SelectUIElement
{
    partial class SelectUIElemetForm
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
            this.btn_CaptureMode = new System.Windows.Forms.Button();
            this.richLabel1 = new RichLabel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.brn_Detail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_CaptureMode
            // 
            this.btn_CaptureMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CaptureMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.btn_CaptureMode.ForeColor = System.Drawing.Color.White;
            this.btn_CaptureMode.Location = new System.Drawing.Point(4, 2);
            this.btn_CaptureMode.Name = "btn_CaptureMode";
            this.btn_CaptureMode.Size = new System.Drawing.Size(247, 25);
            this.btn_CaptureMode.TabIndex = 0;
            this.btn_CaptureMode.Text = "Capture mode: OFF";
            this.btn_CaptureMode.UseVisualStyleBackColor = false;
            this.btn_CaptureMode.Click += new System.EventHandler(this.btn_CaptureMode_Click);
            // 
            // richLabel1
            // 
            this.richLabel1.AutoSize = true;
            this.richLabel1.CustomAutoSize = false;
            this.richLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.richLabel1.ForeColorAlt = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.richLabel1.Location = new System.Drawing.Point(10, 29);
            this.richLabel1.Name = "richLabel1";
            this.richLabel1.Size = new System.Drawing.Size(245, 30);
            this.richLabel1.Splitters = new string[] {
        "{{",
        "}}"};
            this.richLabel1.TabIndex = 1;
            this.richLabel1.Text = "{{5Type:}} xxxx {{5Name:}} xxxxxx\r\n{{4x:}} xxxx {{4y:}} xxx  {{4w:}} xxxx {{4w:}}" +
    " xxx";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(176, 67);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 21);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "OK";
            this.btn_OK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(95, 67);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 21);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // brn_Detail
            // 
            this.brn_Detail.Enabled = false;
            this.brn_Detail.Location = new System.Drawing.Point(10, 67);
            this.brn_Detail.Name = "brn_Detail";
            this.brn_Detail.Size = new System.Drawing.Size(75, 21);
            this.brn_Detail.TabIndex = 4;
            this.brn_Detail.Text = "Detail";
            this.brn_Detail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.brn_Detail.UseVisualStyleBackColor = true;
            this.brn_Detail.Click += new System.EventHandler(this.brn_Detail_Click);
            // 
            // SelectUIElemetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(258, 92);
            this.Controls.Add(this.brn_Detail);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.richLabel1);
            this.Controls.Add(this.btn_CaptureMode);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectUIElemetForm";
            this.Text = "Select UI Elemet Form (Hold Ctrl Key)";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectUIElemetForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_CaptureMode;
        private RichLabel richLabel1;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button brn_Detail;
    }
}