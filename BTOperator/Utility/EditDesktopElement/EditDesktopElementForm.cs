using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using AutoCreateWithJson.Component.BaseStructure;
using AutoCreateWithJson.Component.BuildingBlocks.DesktopUI;
using AutoCreateWithJson.Utility.EditImageCollection;
using AutoCreateWithJson.Utility.PleaseWait;
using AutoCreateWithJson.Utility.SelectUIElement;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;

using Application = FlaUI.Core.Application;
using Button = System.Windows.Forms.Button;
using Label = System.Windows.Forms.Label;
using MessageBox = System.Windows.Forms.MessageBox;

namespace AutoCreateWithJson.Utility.EditDesktopElement
{
    public partial class EditDesktopElementForm : Form
    {
        private LinkedList<object> _allBulidingBlockControls;
        private List<FlaUI.Core.Application> _allRunningApplication;
        //private ConditionForSelectElement conditionForSelectElement;
        private ElementViewAllDetail _elementViewAllDetailWithParents;
        private AutomationElement selectElementLive;
        private SelectElementStoreable _selectElementStoreable;

        public EditDesktopElementForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        public void InitForm(LinkedList<object> allBulidingBlockControls,
            SelectElementStoreable selectElementStoreable,
            AutomationElement newSelectElement)
        {
            _allBulidingBlockControls = allBulidingBlockControls;
            _selectElementStoreable = selectElementStoreable.CloneObject();
            if (_selectElementStoreable.ConditionForSelectElement is not null)
                _selectElementStoreable.ConditionForSelectElement.DrawAllConditionToPanel(panelCondition);
            img_Image.Image = _selectElementStoreable.DrawToPictureBox(new Rectangle(0, 0, img_Image.Width, img_Image.Height));
            ed_Name.Text = _selectElementStoreable.ElementName;
            Text = "Edit Desktop Element: " + _selectElementStoreable.ElementName;
            combo_ValidationDelay.SelectedIndex = ConvertIntToComboIndex(_selectElementStoreable.ValidationDelay);

            _allRunningApplication = new List<Application>();
            foreach (var bulidingBlock in _allBulidingBlockControls)
            {
                if (bulidingBlock is BldBlkStartApplication sApp)
                {
                    if (sApp.application != null)
                    {
                        _allRunningApplication.Add(sApp.application);
                    }
                }
            }
            if (newSelectElement is not null)
                CalculateAndShowElementDetails(newSelectElement);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_CaptureNewElement_Click(object sender, EventArgs e)
        {
            GlobalFunction.AllFormsToMinimized(true);
            using (var frm = new SelectUIElemetForm())
            {
                frm.ShowDialog();
                if (frm.GetResultForm())
                {
                    selectElementLive = frm.GetResultSelectedElement();
                    CalculateAndShowElementDetails(selectElementLive);
                }
            }
            GlobalFunction.AllFormsToNormal(true);
        }

        private void CalculateAndShowElementDetails(AutomationElement selectElementLive)
        {
            var bmp = selectElementLive.Capture();
            var pointerToRunningStatus = new PointerToRunningStatus();
            using (var frmPlzW = new ShowPleaseWaitForm("محاسبات", pointerToRunningStatus))
            {
                pointerToRunningStatus.RunProcedureOnTheThread = new Action(() =>
                {
                    _selectElementStoreable.AddElementImage(bmp, new System.Drawing.Point(bmp.Width / 2, bmp.Height / 2));
                    _elementViewAllDetailWithParents = new ElementViewAllDetail(selectElementLive, _allRunningApplication, true);
                    if (_elementViewAllDetailWithParents.StartAnalyze())
                    {
                        _selectElementStoreable.ConditionForSelectElement = new ConditionForSelectElement(_elementViewAllDetailWithParents.GetCounOfLevel());
                    }


                    //NewMethod(pointerToRunningStatus, selectElementLive);
                });
                frmPlzW.ShowDialog();
            }
            _elementViewAllDetailWithParents.DrawDataToTreeView(tv_ElemetStructure);
            tabControl1.SelectedTab = tabPage_Structure;
            tv_ElemetStructure.ExpandAll();
            _selectElementStoreable.ConditionForSelectElement.DrawAllConditionToPanel(panelCondition);
            img_Image.Image = _selectElementStoreable.DrawToPictureBox(new Rectangle(0, 0, img_Image.Width, img_Image.Height));
            tv_ElemetStructure_AfterSelect(null, null);
        }

        private void ClickForAddConditionToDetectStructure(object sender, EventArgs e)
        {
            if (sender is Label btn)
            {
                var tag = (string)btn.Tag;
                var data = tag.Split("\t");
                if (data.Length == 5)
                {
                    if (!Int32.TryParse(data[1], out int levelCounter))
                        return;

                    _selectElementStoreable.ConditionForSelectElement.CurrentLevel = levelCounter;
                    var level = _selectElementStoreable.ConditionForSelectElement.GetLevelCondition(levelCounter - 1);
                    if (!level.Find3Fields(data[2], data[3]))
                    {
                        var oneCondition = level.NewCondition();
                        oneCondition.New3Fields(data[2], data[3],
                            ConditionForSelectElement.OneCondition.OprandType.Equals, data[4]);
                        _selectElementStoreable.ConditionForSelectElement.DrawAllConditionToPanel(null);
                    }
                }
                else
                {
                    MessageBox.Show(tag);
                }
            }
        }

        private void MouseEnterForAddConditionToDetectStructure(object sender, EventArgs e)
        {
            if (sender is Label lbl)
            {
                lbl.BackColor = Color.Blue;
                lbl.ForeColor = Color.White;
            }
        }

        private void MouseLeaveForAddConditionToDetectStructure(object sender, EventArgs e)
        {
            if (sender is Label lbl)
            {
                lbl.BackColor = Color.AntiqueWhite;
                lbl.ForeColor = Color.Black;
            }
        }

        private void tv_ElemetStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tv_ElemetStructure.SelectedNode != null)
            {
                if (tv_ElemetStructure.SelectedNode.Tag is ElementTreeDetail elementTreeDetail)
                {
                    pnl_ElemetStructure.Controls.Clear();
                    if (tv_ElemetStructure.SelectedNode.ImageIndex == ElementViewAllDetail.Node_Home ||
                        tv_ElemetStructure.SelectedNode.ImageIndex == ElementViewAllDetail.Node_Item ||
                        tv_ElemetStructure.SelectedNode.ImageIndex == ElementViewAllDetail.Node_Select)
                    {
                        _selectElementStoreable.ConditionForSelectElement.CurrentLevel = elementTreeDetail.IsMainRouteElement ? elementTreeDetail.LevelNumber : -1;
                        elementTreeDetail.DrawPropertyToPanel(pnl_ElemetStructure, 0, 0, ClickForAddConditionToDetectStructure, MouseEnterForAddConditionToDetectStructure, MouseLeaveForAddConditionToDetectStructure);
                    }
                    else
                    {
                        _selectElementStoreable.ConditionForSelectElement.CurrentLevel = -1;
                        elementTreeDetail.DrawPropertyToPanel(pnl_ElemetStructure, 0, 0);
                    }
                }
                else
                {
                    _selectElementStoreable.ConditionForSelectElement.CurrentLevel = -1;
                }
            }
        }

        public SelectElementStoreable GetResultForm()
        {
            return _selectElementStoreable;
        }

        private void lbl_SetClickPoint_Click(object sender, EventArgs e)
        {
            using (var frm = new EditImageCollectionForm())
            {
                frm.Init(_selectElementStoreable);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < _selectElementStoreable.ElementCount; i++)
                    {
                        var pt = frm.GetImagePoint(i);
                        _selectElementStoreable.SetPoint(i, pt);
                    }

                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("آیا تغییرات ذخیره شود؟", "ذخیره تغییرات",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    _selectElementStoreable.ElementName = ed_Name.Text.Trim();
                    _selectElementStoreable.ValidationDelay = ConvertToInt(combo_ValidationDelay.Text);
                    this.DialogResult = DialogResult.OK;
                    Close();
                    break;
                case DialogResult.No:
                    this.DialogResult = DialogResult.No;
                    Close();
                    break;
                default:

                    break;

            }
        }

        private int ConvertToInt(string txt)
        {
            string b = string.Empty;
            for (int i = 0; i < txt.Length; i++)
            {
                if (Char.IsDigit(txt[i]))
                    b += txt[i];
            }
            if (b.Length > 0)
                return int.Parse(b);
            return 0;
        }
        private int ConvertIntToComboIndex(int second)
        {
            switch (second)
            {
                case < 1:
                    return 0;
                case 1: return 1;
                case 3: return 2;
                case 5: return 3;
                case 10: return 4;
                case 15: return 5;
                case 20: return 6;
                default:
                    return 0;
            }
        }
    }

}
