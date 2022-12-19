using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoCreateWithJson.Utility.PleaseWait
{
    public partial class ShowPleaseWaitForm : Form
    {
        private PointerToRunningStatus _pointerToRunningStatus;
        private int counter = 0;
       private bool dialogBoxIsOpened = false;
        private Thread thread;

        public ShowPleaseWaitForm(string title,PointerToRunningStatus pointerToRunningStatus)
        {
            InitializeComponent();
            this.Text = title;
            _pointerToRunningStatus = pointerToRunningStatus;
            _pointerToRunningStatus.ManagerForm = this;
            _pointerToRunningStatus.ConditionToExitReceived = false;
            _pointerToRunningStatus.IsRunning = false;
            _pointerToRunningStatus.IsRequestCancel = false;
            lbl_Progress.Text = "";
            lbl_Information.Text = "";
            progressBar1.Style = ProgressBarStyle.Marquee;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowPleaseWaitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_pointerToRunningStatus.IsRunning)
            {
                if (_pointerToRunningStatus.IsRequestCancel)
                {
                    counter++;
                    lbl_Information.Text = $"Close Counter : {counter}";
                    e.Cancel = true;
                    return;
                }

                dialogBoxIsOpened = true;
                DialogResult dialogResult = MessageBox.Show("آیا می خواهید عملیات لغو شود؟", "تایید لغو پردازش", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                dialogBoxIsOpened = false;
                if (dialogResult != DialogResult.Yes)
                {
                    counter = 1;
                    e.Cancel = true;
                    return;
                }
                _pointerToRunningStatus.ConditionToExitReceived = true;
                _pointerToRunningStatus.IsRequestCancel = true;
                e.Cancel = true;
                return;
            }
        }

        private void ShowPleaseWaitForm_Shown(object sender, EventArgs e)
        {
            if (_pointerToRunningStatus.RunProcedureOnTheThread is null)
            {
                Close();
                return;
            }
            _pointerToRunningStatus.IsRunning = true;
            thread = new Thread(RunWrapperMethod);
            thread.Start();
        }

        #region ProgressBar 
        public void InitProgressBar(int maxValue)
        {
            Invoke(new Action(() =>
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Maximum = maxValue;
            }));
            
        }
        public void ProgressBarSetPosition(int value)
        {
            Invoke(new Action(() =>
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = value;
                lbl_Progress.Text = $"{value} of {progressBar1.Maximum}";
            }));
            
        }
        #endregion

        private void RunWrapperMethod(object obj)
        {
            _pointerToRunningStatus.RunProcedureOnTheThread();
            _pointerToRunningStatus.IsRunning = false;
            while (dialogBoxIsOpened)
            {
                Thread.Sleep(100);
            }
            Invoke(new Action(() =>
            {
               _pointerToRunningStatus.ManagerForm.Close();
            }));
        }
    }

    public class PointerToRunningStatus
    {
        public bool ConditionToExitReceived;
        public bool IsRequestCancel;
        public bool IsRunning;
        public ShowPleaseWaitForm ManagerForm;
        public Action RunProcedureOnTheThread;
    }
}
