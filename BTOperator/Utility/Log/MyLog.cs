using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Utility.Log
{
    public static class MyLog
    {
        public static TextBox? _textBox = null;
        public static Form? _form = null;
        public static bool ActiveLog = true;


        public static void SetInitByForm(Form form, TextBox textBox)
        {
            _textBox = textBox;
            _form = form;
        }
        public static void WritelnForm(string txt)
        {
            if (_textBox == null)
                return;
            if (_form != null)
            {
                if (_form.InvokeRequired)
                {
                    _form.Invoke(new Action<string>(WritelnForm), new object[] { txt });
                    return;
                }
            }
            _textBox.AppendText(txt + Environment.NewLine);
        }

        public static void ClearFile()
        {
            var filename = FullPathFilename();
            var dir = Path.GetDirectoryName(filename);
            if (dir == null)
                return;
            StreamWriter logLoop = new StreamWriter(filename, false);
            logLoop.Close();
        }

        public static string FullPathFilename()
        {
            try
            {
                string AppPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                return Path.Combine(AppPath, "Logs_" + DateTime.Now.Date.ToString("yyyy_MM"), DateTime.Now.Date.ToString("yyyy_MM_dd") + ".txt");
            }
            catch
            {
                return @"C:\Log_On_Error_Time.txt";
            }
        }
        public static void WritelnFile(string Title, string Text, bool isWriteInConsol = true)
        {
            if (!ActiveLog)
                return;
            var filename = FullPathFilename();
            var dir = Path.GetDirectoryName(filename);
            if (dir == null)
                return;
            Directory.CreateDirectory(dir);
            StreamWriter logLoop = new StreamWriter(filename, true);
            logLoop.WriteLine("Date >>>>> " + DateTime.Now.ToString());
            if (string.IsNullOrEmpty(Title))
                logLoop.WriteLine(Text);
            else
                logLoop.WriteLine(Title + " >>>>> " + Text);
            logLoop.WriteLine("================================================");
            logLoop.Close();

            if (isWriteInConsol)
                WritelnConsol(Title, Text);
        }




        public static void WritelnBoth(string Title, string Text)
        {
            if (!ActiveLog)
                return;
            var txt = string.IsNullOrEmpty(Text) ? Title : Title + "==>" + Text;
            WritelnForm(txt);
            WritelnFile(Title, Text);
        }
        public static void WritelnBoth(string Text)
        {
            if (!ActiveLog)
                return;
            WritelnForm(Text);
            WritelnFile("", Text);
        }

        public static void WritelnConsol(string Title, string Text)
        {
            if (!ActiveLog)
                return;
            System.Diagnostics.Debug.WriteLine(Title + " >>>>> " + Text);
        }


        public static void ForceWritelnBoth(string Title, string Text)
        {
            var _ctiveLog = ActiveLog;
            ActiveLog = true;
            var txt = string.IsNullOrEmpty(Text) ? Title : Title + "==>" + Text;
            WritelnForm(txt);
            WritelnFile(Title, Text);
            ActiveLog = _ctiveLog;
        }
        public static void ForceWritelnBoth(string Text)
        {
            var _ctiveLog = ActiveLog;
            ActiveLog = true;
            WritelnForm(Text);
            WritelnFile("", Text);
            ActiveLog = _ctiveLog;
        }

    }

}
