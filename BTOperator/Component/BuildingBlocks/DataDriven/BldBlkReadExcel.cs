using AutoCreateWithJson.Component.BaseElements;
using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BuildingBlocks.DataDriven
{
    public class BldBlkReadExcel : BasicBuildingBlock
    {
        public BldBlkReadExcel()
        {
            Width = GraphicConstant.bluildingBlockWidth;
            AddHeaderLabel();
            AddSourceType();
            AddPathToFile(true);
            AddRangeDefine();
            AddMethod();
            AddRowIndex();
            AddIterate();

            AssignOnTheClickAndDoubleClickMethod();
        }

        private const string lblRowIndex = "lblRowIndex";
        private const string lblCompleted = "lblCompleted";
        private void AddIterate()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblRowIndex;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Row index";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false,Color.Blue,0);
            Children.Add(lbl);

            Children.Add(new ElmSeparateLine());

            lbl = new ElmLabel(this);
            lbl.Name = lblCompleted;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Completed";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Green, 1, 0);
            Children.Add(lbl);

            Children.Add(new ElmSeparateLine());

        }

        private const string edRowIndex = "edRowIndex";
        private void AddRowIndex()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edRowIndex;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.Title = "Row index";
            edt.TitlePosition = ContentAlignment.MiddleLeft;
            edt.Text = "1";
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());

        }
        private void AddMethod()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 2, 10, 2);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Method";
            combo.Items.Add("First row");
            combo.Items.Add("Row index");
            combo.Items.Add("Iterate");
            combo.SelectedText = "First row";
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private const string btnRangeDefine = "btnRangeDefine";
        private const string mcntrRangeDefine = "mcntrRangeDefine";
        private void AddRangeDefine()
        {
            var lbl = new ElmLabel(this);
            lbl.Padding = new Padding(10, 2, 10, 1);
            lbl.Title = "Range";
            lbl.AddTwoConnector(Color.Blue, 1, 0);
            Children.Add(lbl);

            var btn = new ElmButton(this);
            btn.Name = btnRangeDefine;
            btn.IsNecessaryToView = 0;
            btn.Title = "Define";
            btn.ElmHasPosition = true;
            btn.ElmLeft = 30;
            btn.ElmTop = 0;
            btn.ElmWidth = 80;
            btn.ElmHeight = 30;
            Children.Add(btn);
            AddToListOfActionTheClick(btn, SelectExcelRangeClick);

            var mcntr = new ElmMultiConnector(this);
            mcntr.Name = mcntrRangeDefine;
            Children.Add(mcntr);

            Children.Add(new ElmSeparateLine());

        }

        private void SelectExcelRangeClick(object sender, MouseEventArgs e)
        {
            var mcntr = (ElmMultiConnector)ElementByName(mcntrRangeDefine);
            mcntr.SetConnectorOutput(new string[3] { "dfd", "sdfsd", "dsff423" });
        }

        private const string comboSourceType = "comboSourceType";
        private void AddSourceType()
        {
            var combo = new ElmComboBox(this);
            combo.Name = comboSourceType;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Title = "Source type";
            combo.Items.Add("Data file");
            combo.Items.Add("Local path");
            combo.SelectedText = "Local path";
            Children.Add(combo);

            AddToListOfActionTheClick(combo, SourceTypeClick);
            Children.Add(new ElmSeparateLine());
        }

        private void SourceTypeClick(object sender, MouseEventArgs e)
        {
            var combo = (ElmComboBox)ElementByName(comboSourceType);
            var val1 = combo.SelectedText;
            combo.SelectItemByClick(sender, e);
            if (val1 != combo.SelectedText)
            {
                if (combo.SelectedText == "Data file")
                {
                    ComponentDataFileVisible(true);
                    ComponenLocalPathVisible(false);

                }
                else if (combo.SelectedText == "Local path")
                {
                    ComponentDataFileVisible(false);
                    ComponenLocalPathVisible(true);
                }
            }
        }

        private void ComponentDataFileVisible(bool visible)
        {

        }
        private void ComponenLocalPathVisible(bool visible)
        {
            ((ElmLabel)ElementByName(lblPathToFile)).Visible = visible;
            ((ElmButton)ElementByName(btnPathToFile)).Visible = visible;
            ((ElmEditBox)ElementByName(edPathToFile)).Visible = visible;
        }

        private const string lblPathToFile = "lblPathToFile";
        private const string btnPathToFile = "btnPathToFile";
        private const string edPathToFile = "edPathToFile";
        private void AddPathToFile(bool visible)
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblPathToFile;
            lbl.Padding = new Padding(10, 2, 10, 1);
            lbl.Title = "Path to file";
            lbl.AddTwoConnector(Color.Blue, 1, 0);
            lbl.Visible = visible;
            Children.Add(lbl);

            var btn = new ElmButton(this);
            btn.Name = btnPathToFile;
            btn.IsNecessaryToView = 0;
            btn.Title = "...";
            btn.ElmHasPosition = true;
            btn.ElmLeft = 30;
            btn.ElmTop = 0;
            btn.ElmWidth = 80;
            btn.ElmHeight = 30;
            btn.Visible = visible;
            Children.Add(btn);
            AddToListOfActionTheClick(btn, SelectApplicationClick);

            var edt = new ElmEditBox(this);
            edt.Name = edPathToFile;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Visible = visible;
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private void SelectApplicationClick(object sender, MouseEventArgs e)
        {
            using (var openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Title = "Open File";
                openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var _edApplication = (ElmEditBox)ElementByName(edPathToFile);
                    _edApplication.TextWithAutoSize = openFileDialog1.FileName;
                    advancePanel.Invalidate();
                }
            }

        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Read Excel";
            lbl.ElmHeight = 0;
            lbl.Padding = new Padding(10, 0, 3, 0);
            lbl.MySize = MyTextSize.Large;
            lbl.BackGround = Color.FromArgb(85, 122, 166);
            lbl.TextColor = Color.White;
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddTwoConnector(Color.Green, 0, 1);
            Children.Add(lbl);

            lbl.IsHeaderLabel = true;
        }


    }
}
