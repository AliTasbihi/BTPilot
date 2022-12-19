namespace AutoAdvPanelTest
{
    partial class AdvMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvMainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Data = new System.Windows.Forms.Button();
            this.lbl_Clear = new System.Windows.Forms.Label();
            this.comboBuildingBlock = new System.Windows.Forms.ComboBox();
            this.lbl_ClearBuildingBlock = new System.Windows.Forms.Label();
            this.comboCategory = new System.Windows.Forms.ComboBox();
            this.lbl_ClearAddBuildingBlock = new System.Windows.Forms.Label();
            this.lbl_AddAllBuildingBlock = new System.Windows.Forms.Label();
            this.chk_Detail = new System.Windows.Forms.CheckBox();
            this.tabComponents = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.btn_ZoomWindow = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btn_ZoomExtent = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_Move = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button8 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chk_Wrap = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_Example = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.advancePanel1 = new AdvancePanelLibrary.Component.Controller.AdvancePanel();
            this.tabComponents.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(337, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Bulding Block";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBox1.Location = new System.Drawing.Point(704, 81);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(273, 231);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // btn_Data
            // 
            this.btn_Data.Location = new System.Drawing.Point(484, 10);
            this.btn_Data.Name = "btn_Data";
            this.btn_Data.Size = new System.Drawing.Size(75, 23);
            this.btn_Data.TabIndex = 3;
            this.btn_Data.Text = "Show Data";
            this.btn_Data.UseVisualStyleBackColor = true;
            this.btn_Data.Click += new System.EventHandler(this.btn_Data_Click);
            // 
            // lbl_Clear
            // 
            this.lbl_Clear.AutoSize = true;
            this.lbl_Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Clear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.lbl_Clear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_Clear.Location = new System.Drawing.Point(623, 10);
            this.lbl_Clear.Name = "lbl_Clear";
            this.lbl_Clear.Size = new System.Drawing.Size(123, 15);
            this.lbl_Clear.TabIndex = 4;
            this.lbl_Clear.Text = "Clear All Components";
            this.lbl_Clear.Click += new System.EventHandler(this.lbl_Clear_Click);
            // 
            // comboBuildingBlock
            // 
            this.comboBuildingBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBuildingBlock.FormattingEnabled = true;
            this.comboBuildingBlock.Items.AddRange(new object[] {
            "BldBlkClickUIElement",
            "BldBlkCloseUIWindow",
            "BldBlkCollapseUIElement",
            "BldBlkDragUIElement"});
            this.comboBuildingBlock.Location = new System.Drawing.Point(173, 11);
            this.comboBuildingBlock.Name = "comboBuildingBlock";
            this.comboBuildingBlock.Size = new System.Drawing.Size(158, 23);
            this.comboBuildingBlock.TabIndex = 5;
            // 
            // lbl_ClearBuildingBlock
            // 
            this.lbl_ClearBuildingBlock.AutoSize = true;
            this.lbl_ClearBuildingBlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_ClearBuildingBlock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.lbl_ClearBuildingBlock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_ClearBuildingBlock.Location = new System.Drawing.Point(13, 44);
            this.lbl_ClearBuildingBlock.Name = "lbl_ClearBuildingBlock";
            this.lbl_ClearBuildingBlock.Size = new System.Drawing.Size(113, 15);
            this.lbl_ClearBuildingBlock.TabIndex = 13;
            this.lbl_ClearBuildingBlock.Text = "Clear Building Block";
            this.lbl_ClearBuildingBlock.Click += new System.EventHandler(this.lbl_ClearBuildingBlock_Click);
            // 
            // comboCategory
            // 
            this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCategory.FormattingEnabled = true;
            this.comboCategory.Items.AddRange(new object[] {
            "BldBlkClickUIElement",
            "BldBlkCloseUIWindow",
            "BldBlkCollapseUIElement",
            "BldBlkDragUIElement"});
            this.comboCategory.Location = new System.Drawing.Point(13, 11);
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.Size = new System.Drawing.Size(146, 23);
            this.comboCategory.TabIndex = 14;
            this.comboCategory.SelectedValueChanged += new System.EventHandler(this.comboCategory_SelectedValueChanged);
            // 
            // lbl_ClearAddBuildingBlock
            // 
            this.lbl_ClearAddBuildingBlock.AutoSize = true;
            this.lbl_ClearAddBuildingBlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_ClearAddBuildingBlock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.lbl_ClearAddBuildingBlock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_ClearAddBuildingBlock.Location = new System.Drawing.Point(344, 43);
            this.lbl_ClearAddBuildingBlock.Name = "lbl_ClearAddBuildingBlock";
            this.lbl_ClearAddBuildingBlock.Size = new System.Drawing.Size(141, 15);
            this.lbl_ClearAddBuildingBlock.TabIndex = 15;
            this.lbl_ClearAddBuildingBlock.Text = "Clear & Add Building Block";
            this.lbl_ClearAddBuildingBlock.Click += new System.EventHandler(this.lbl_ClearAddBuildingBlock_Click);
            // 
            // lbl_AddAllBuildingBlock
            // 
            this.lbl_AddAllBuildingBlock.AutoSize = true;
            this.lbl_AddAllBuildingBlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_AddAllBuildingBlock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.lbl_AddAllBuildingBlock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_AddAllBuildingBlock.Location = new System.Drawing.Point(344, 58);
            this.lbl_AddAllBuildingBlock.Name = "lbl_AddAllBuildingBlock";
            this.lbl_AddAllBuildingBlock.Size = new System.Drawing.Size(125, 15);
            this.lbl_AddAllBuildingBlock.TabIndex = 16;
            this.lbl_AddAllBuildingBlock.Text = "Add All Building Block";
            this.lbl_AddAllBuildingBlock.Click += new System.EventHandler(this.lbl_AddAllBuildingBlock_Click);
            // 
            // chk_Detail
            // 
            this.chk_Detail.AutoSize = true;
            this.chk_Detail.Location = new System.Drawing.Point(561, 12);
            this.chk_Detail.Name = "chk_Detail";
            this.chk_Detail.Size = new System.Drawing.Size(56, 19);
            this.chk_Detail.TabIndex = 17;
            this.chk_Detail.Text = "Detail";
            this.chk_Detail.UseVisualStyleBackColor = true;
            // 
            // tabComponents
            // 
            this.tabComponents.Controls.Add(this.tabPage1);
            this.tabComponents.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabComponents.Location = new System.Drawing.Point(0, 312);
            this.tabComponents.Name = "tabComponents";
            this.tabComponents.SelectedIndex = 0;
            this.tabComponents.Size = new System.Drawing.Size(977, 100);
            this.tabComponents.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button6);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button9);
            this.tabPage1.Controls.Add(this.btn_ZoomWindow);
            this.tabPage1.Controls.Add(this.button11);
            this.tabPage1.Controls.Add(this.button12);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.btn_ZoomExtent);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.btn_Move);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(969, 72);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(852, 35);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 21;
            this.button6.Text = "Connector";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(834, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "Clickable Area";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(209, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "Zoom Actual";
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.ZoomActual;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(218, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 42);
            this.button2.TabIndex = 18;
            this.toolTip1.SetToolTip(this.button2, "Pan");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(728, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "Zoom Previous";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(648, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "Zoom Window";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(582, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "Zoom Out";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(511, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "Zoom In";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(427, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "Zoom Center";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(363, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "Zoom E";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(286, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Zoom All";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(127, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Pan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(20, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Move";
            // 
            // button9
            // 
            this.button9.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.ZoomPrevious;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.Location = new System.Drawing.Point(743, 6);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(44, 42);
            this.button9.TabIndex = 8;
            this.toolTip1.SetToolTip(this.button9, "Zoom Previous");
            this.button9.UseVisualStyleBackColor = true;
            // 
            // btn_ZoomWindow
            // 
            this.btn_ZoomWindow.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.ZoomWindow;
            this.btn_ZoomWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ZoomWindow.Location = new System.Drawing.Point(662, 6);
            this.btn_ZoomWindow.Name = "btn_ZoomWindow";
            this.btn_ZoomWindow.Size = new System.Drawing.Size(44, 42);
            this.btn_ZoomWindow.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btn_ZoomWindow, "Zoom Window");
            this.btn_ZoomWindow.UseVisualStyleBackColor = true;
            this.btn_ZoomWindow.Click += new System.EventHandler(this.btn_ZoomWindow_Click);
            // 
            // button11
            // 
            this.button11.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.ZoomOut;
            this.button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button11.Location = new System.Drawing.Point(587, 6);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(44, 42);
            this.button11.TabIndex = 6;
            this.toolTip1.SetToolTip(this.button11, "Zoom Out");
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.Transparent;
            this.button12.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.ZoomIn;
            this.button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button12.Location = new System.Drawing.Point(512, 6);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(44, 42);
            this.button12.TabIndex = 5;
            this.toolTip1.SetToolTip(this.button12, "Zoom In");
            this.button12.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.ZoomCenter;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.Location = new System.Drawing.Point(437, 6);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(44, 42);
            this.button7.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button7, "Zoom Center");
            this.button7.UseVisualStyleBackColor = true;
            // 
            // btn_ZoomExtent
            // 
            this.btn_ZoomExtent.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.ZoomExtents;
            this.btn_ZoomExtent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ZoomExtent.Location = new System.Drawing.Point(362, 6);
            this.btn_ZoomExtent.Name = "btn_ZoomExtent";
            this.btn_ZoomExtent.Size = new System.Drawing.Size(44, 42);
            this.btn_ZoomExtent.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_ZoomExtent, "Zoom Extents");
            this.btn_ZoomExtent.UseVisualStyleBackColor = true;
            this.btn_ZoomExtent.Click += new System.EventHandler(this.btn_ZoomExtent_Click);
            // 
            // button5
            // 
            this.button5.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.ZoomAll;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Location = new System.Drawing.Point(287, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(44, 42);
            this.button5.TabIndex = 2;
            this.toolTip1.SetToolTip(this.button5, "Zoom All");
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnZoomAll_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.Pan;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Location = new System.Drawing.Point(116, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(44, 42);
            this.button4.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button4, "Pan");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_Move
            // 
            this.btn_Move.BackColor = System.Drawing.Color.Transparent;
            this.btn_Move.BackgroundImage = global::AutoAdvPanelTest.Properties.Resources.Move;
            this.btn_Move.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Move.Location = new System.Drawing.Point(13, 6);
            this.btn_Move.Name = "btn_Move";
            this.btn_Move.Size = new System.Drawing.Size(44, 42);
            this.btn_Move.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btn_Move, "Move");
            this.btn_Move.UseVisualStyleBackColor = false;
            this.btn_Move.Click += new System.EventHandler(this.btn_Move_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(531, 50);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(179, 23);
            this.button8.TabIndex = 23;
            this.button8.Text = "بررسی وضعیت کامپوننت جاری";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.chk_Wrap);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lbl_Example);
            this.panel1.Controls.Add(this.comboCategory);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.comboBuildingBlock);
            this.panel1.Controls.Add(this.lbl_ClearBuildingBlock);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.lbl_ClearAddBuildingBlock);
            this.panel1.Controls.Add(this.lbl_AddAllBuildingBlock);
            this.panel1.Controls.Add(this.chk_Detail);
            this.panel1.Controls.Add(this.btn_Data);
            this.panel1.Controls.Add(this.lbl_Clear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(977, 81);
            this.panel1.TabIndex = 25;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label15.Location = new System.Drawing.Point(681, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(272, 15);
            this.label15.TabIndex = 32;
            this.label15.Text = "Backgroud Load and Execute(with Global Variable)";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label14.Location = new System.Drawing.Point(210, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 15);
            this.label14.TabIndex = 31;
            this.label14.Text = "Add n Building Block";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(565, 27);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 30;
            this.button10.Text = "Clear ADV";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label13.Location = new System.Drawing.Point(886, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 15);
            this.label13.TabIndex = 29;
            this.label13.Text = "Save To FIle";
            this.label13.Click += new System.EventHandler(this.lbl_SaveToFile_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label12.Location = new System.Drawing.Point(777, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 15);
            this.label12.TabIndex = 28;
            this.label12.Text = "Load From File";
            this.label12.Click += new System.EventHandler(this.lbl_LoadFromFile_Click);
            // 
            // chk_Wrap
            // 
            this.chk_Wrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_Wrap.AutoSize = true;
            this.chk_Wrap.Location = new System.Drawing.Point(917, 55);
            this.chk_Wrap.Name = "chk_Wrap";
            this.chk_Wrap.Size = new System.Drawing.Size(54, 19);
            this.chk_Wrap.TabIndex = 27;
            this.chk_Wrap.Text = "Wrap";
            this.chk_Wrap.UseVisualStyleBackColor = true;
            this.chk_Wrap.CheckedChanged += new System.EventHandler(this.chk_Wrap_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label11.Location = new System.Drawing.Point(747, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 15);
            this.label11.TabIndex = 26;
            this.label11.Text = "Clear Log";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // lbl_Example
            // 
            this.lbl_Example.AutoSize = true;
            this.lbl_Example.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Example.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.lbl_Example.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_Example.Location = new System.Drawing.Point(162, 43);
            this.lbl_Example.Name = "lbl_Example";
            this.lbl_Example.Size = new System.Drawing.Size(52, 15);
            this.lbl_Example.TabIndex = 25;
            this.lbl_Example.Text = "Example";
            this.lbl_Example.Click += new System.EventHandler(this.lbl_Example_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(701, 81);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 231);
            this.splitter1.TabIndex = 26;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.advancePanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(701, 231);
            this.panel2.TabIndex = 27;
            // 
            // advancePanel1
            // 
            this.advancePanel1.AllBuildingBlock = ((System.Collections.Generic.LinkedList<object>)(resources.GetObject("advancePanel1.AllBuildingBlock")));
            this.advancePanel1.AllowInteractiveWithUI = true;
            this.advancePanel1.DisplayMode = AdvancePanelLibrary.Component.Controller.DisplayModeEnum.None;
            this.advancePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancePanel1.InforAction = null;
            this.advancePanel1.Location = new System.Drawing.Point(0, 0);
            this.advancePanel1.Name = "advancePanel1";
            this.advancePanel1.playerExecutor = null;
            this.advancePanel1.Size = new System.Drawing.Size(701, 231);
            this.advancePanel1.TabIndex = 23;
            this.advancePanel1.Text = "advancePanel1";
            // 
            // AdvMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 412);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabComponents);
            this.Name = "AdvMainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabComponents.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Button btn_Data;
        private Label lbl_Clear;
        private ComboBox comboBuildingBlock;
        private Label lbl_ClearBuildingBlock;
        private ComboBox comboCategory;
        private Label lbl_ClearAddBuildingBlock;
        private Label lbl_AddAllBuildingBlock;
        private CheckBox chk_Detail;
        private TabControl tabComponents;
        private TabPage tabPage1;
        private Button btn_Move;
        private Button button4;
        private Button button9;
        private Button btn_ZoomWindow;
        private Button button11;
        private Button button12;
        private Button button7;
        private Button btn_ZoomExtent;
        private Button button5;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label3;
        private Label label2;
        private Label label1;
        private ToolTip toolTip1;
        private Label label10;
        private Button button2;
        private Button button3;
        private Button button6;
        private Button button8;
        private AdvancePanelLibrary.Component.Controller.AdvancePanel advancePanel2;
        private Panel panel1;
        private Label lbl_Example;
        private Label label11;
        private CheckBox chk_Wrap;
        private Splitter splitter1;
        private Label label13;
        private Label label12;
        private Button button10;
        private Panel panel2;
        private AdvancePanelLibrary.Component.Controller.AdvancePanel advancePanel1;
        private Label label14;
        private Label label15;
    }
}