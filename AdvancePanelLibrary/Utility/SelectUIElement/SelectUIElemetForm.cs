using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA2;
using FlaUI.UIA3;

namespace AdvancePanelLibrary.Utility.SelectUIElement
{
    public partial class SelectUIElemetForm : Form
    {
        private bool _formResult = false;
        private bool _isCaptureMode = false;
        private HoverMode _hoverMode;
        private AutomationBase _automation;
        private AutomationElement _rootElement;
        private ITreeWalker _treeWalker;
        private AutomationElement _selectCurrentElement = null;



        public bool CaptureMode
        {
            get
            {
                return _isCaptureMode;
            }
            set
            {
                _isCaptureMode = value;
                if (_isCaptureMode)
                {
                    _hoverMode.Start();
                    btn_CaptureMode.Text = "Capture mode: ON";
                    btn_CaptureMode.BackColor = Color.FromArgb(67, 158, 22);
                }
                else
                {
                    _hoverMode.Stop();
                    btn_CaptureMode.Text = "Capture mode: OFF";
                    btn_CaptureMode.BackColor = Color.FromArgb(158, 22, 22);

                }
            }
        }
        public SelectUIElemetForm()
        {
            InitializeComponent();
            InitForm(AutomationType.UIA3);
            CaptureMode = true;
        }

        private void InitForm(AutomationType selectedAutomationType)
        {
            StartPosition = FormStartPosition.Manual;
            var rec = GlobalFunction.GetlocationOfTaskBar();
            var x = rec.Right - this.Width;
            var y = rec.Top - this.Height;
            this.Location = new Point(x, y);

            ///https://www.youtube.com/watch?v=RNleTMMuJsw
            /// 
            _automation = selectedAutomationType == AutomationType.UIA2 ? (AutomationBase)new UIA2Automation() : new UIA3Automation();
            _rootElement = _automation.GetDesktop();

            // Initialize TreeWalker
            _treeWalker = _automation.TreeWalkerFactory.GetControlViewWalker();

            // Initialize hover
            _hoverMode = new HoverMode(_automation);
            _hoverMode.ElementHovered += ElementToSelectChanged;

            ShowInformationOfElement(null);
        }

        public bool GetResultForm()
        {
            return _formResult;
        }
        public AutomationElement GetResultSelectedElement()
        {
            return  _selectCurrentElement;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (_selectCurrentElement != null)
            {
                _formResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("هیچ المانی انتخاب نشده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_CaptureMode_Click(object sender, EventArgs e)
        {
            CaptureMode = !CaptureMode;
        }

        private void ElementToSelectChanged(AutomationElement obj)
        {
            ShowInformationOfElement(obj);
            var c = 0;

            // Build a stack from the root to the hovered item
            var pathToRoot = new Stack<AutomationElement>();
            while (obj != null)
            {
                // Break on circular relationship (should not happen?)
                if (pathToRoot.Contains(obj) || obj.Equals(_rootElement)) { break; }

                pathToRoot.Push(obj);
                try
                {
                    c++;
                    obj = _treeWalker.GetParent(obj);
                }
                catch (Exception ex)
                {
                    // TODO: Log
                }
            }

        }

        private void ShowInformationOfElement(AutomationElement obj)
        {
            _selectCurrentElement = obj;
            string _type = "\t";
            string _name = "\t";
            string _x = "\t";
            string _y = "\t";
            string _w = "\t";
            string _h = "\t";
            if (obj != null)
            {
                if (obj.GetType().HasProperty("ControlType"))
                    try { _type = obj.ControlType.ToString(); } catch (Exception e) { }
                else if (obj.GetType().HasProperty("ClassName"))
                    try { _type = obj.ClassName.ToString(); } catch (Exception e) { }

                if (obj.GetType().HasProperty("Name"))
                    try { _name = obj.Name.ToString(); } catch (Exception e) { }
                else if (obj.GetType().HasProperty("ClassName"))
                    _name = obj.ClassName.ToString();

                if (obj.GetType().HasProperty("BoundingRectangle"))
                {
                    _x = obj.BoundingRectangle.Left.ToString();
                    _y = obj.BoundingRectangle.Top.ToString();
                    _w = obj.BoundingRectangle.Width.ToString();
                    _h = obj.BoundingRectangle.Height.ToString();
                }
            }
            richLabel1.Text = "{{5Type:}} " + _type +
                              "  {{5Name:}} " + _name + Environment.NewLine +
                              "{{4x:}} " + _x + "  {{4y:}} " + _y +
                              "  {{4w:}} " + _w + "{{4h:}} " + _h + Environment.NewLine + "_";

            brn_Detail.Enabled = _selectCurrentElement != null;
        }

        private void SelectUIElemetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CaptureMode = false;
        }

        private void brn_Detail_Click(object sender, EventArgs e)
        {
            var _state = CaptureMode;
            TopMost = false;
            CaptureMode = false;
            using (var frm = new ShowDetailOfUIElement())
            {
                frm.Init(_selectCurrentElement);
                frm.ShowDialog();
                if (frm.ResultForm == true)
                {
                    _formResult = true;
                    Close();
                    return;
                }
            }
            TopMost = true;
            if (_state)
                CaptureMode = _state;
        }
    }
}
