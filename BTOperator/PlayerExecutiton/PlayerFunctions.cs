using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.PlayerExecutiton
{
    public class PlayerFunctions
    {
        private static Form _mainForm;

        public static void SetMainForm(Form frm)
        {
            _mainForm = frm;
        }

        public static void HideMainForm()
        {
            _mainForm.Hide();
        }
        public static void ShowMainForm()
        {
            _mainForm.Show();
        }

        public static Process[] FindProcess(string executable)
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(executable));
        }
    }

}
