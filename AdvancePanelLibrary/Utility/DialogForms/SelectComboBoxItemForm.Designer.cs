namespace AdvancePanelLibrary.Utility.DialogForms
{
    partial class SelectComboBoxItemForm
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lbl_DescriptionValue = new System.Windows.Forms.Label();
            this.combo_ItemsComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(78, 74);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 21);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(159, 74);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 21);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "OK";
            this.btn_OK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // lbl_DescriptionValue
            // 
            this.lbl_DescriptionValue.AutoSize = true;
            this.lbl_DescriptionValue.Location = new System.Drawing.Point(17, 9);
            this.lbl_DescriptionValue.Name = "lbl_DescriptionValue";
            this.lbl_DescriptionValue.Size = new System.Drawing.Size(98, 15);
            this.lbl_DescriptionValue.TabIndex = 6;
            this.lbl_DescriptionValue.Text = "Description Value";
            // 
            // combo_ItemsComboBox
            // 
            this.combo_ItemsComboBox.FormattingEnabled = true;
            this.combo_ItemsComboBox.Location = new System.Drawing.Point(17, 33);
            this.combo_ItemsComboBox.Name = "combo_ItemsComboBox";
            this.combo_ItemsComboBox.Size = new System.Drawing.Size(272, 23);
            this.combo_ItemsComboBox.TabIndex = 7;
            // 
            // SelectComboBoxItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(301, 107);
            this.Controls.Add(this.combo_ItemsComboBox);
            this.Controls.Add(this.lbl_DescriptionValue);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectComboBoxItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Caption";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_Cancel;
        private Button btn_OK;
        public Label lbl_DescriptionValue;
        public ComboBox combo_ItemsComboBox;
    }
}