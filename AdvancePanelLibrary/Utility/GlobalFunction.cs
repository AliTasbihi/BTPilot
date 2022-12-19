using AdvancePanelLibrary.Component.BaseElements;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Utility
{
    public static class GlobalFunction
    {
        public static string Get2LevelUpDirectory(string path)
        {
            var d = new DirectoryInfo(path);
            if ((d.Parent != null) && (d.Parent.Parent != null) && (d.Parent.Parent.Parent != null))
            {
                return d.Parent.Parent.Parent.ToString();
            }
            return path;
        }

        public static string GetFilesDirectory(string folder = "Files")
        {
            var path1 = Directory.GetCurrentDirectory();
            var path = Path.Combine(path1, folder);
            if (Directory.Exists(path))
                return path;

            path1 = GlobalFunction.Get2LevelUpDirectory(Directory.GetCurrentDirectory());
            path = Path.Combine(path1, folder);
            if (Directory.Exists(path))
                return path;

            return "";
        }
        public static string FindFile(string fileName)
        {
            var fn = GetFilesDirectory("Files\\Cursors\\") + fileName;
            if (File.Exists(fn))
                return fn;

            return "";
        }

        public static IEnumerable<string> SplitCamelCase(string input)
        {
            return Regex.Split(input, @"([A-Z]?[a-z]+)").Where(str => !string.IsNullOrEmpty(str));
        }

        public static string BuildingBlockCategoryCompress(string category)
        {
            return category.Replace(" ", "");
        }
        public static string BuildingBlockCategoryDecompress(string compressCategory)
        {
            var sArray = SplitCamelCase(compressCategory);
            var sb = new StringBuilder();
            foreach (var s in sArray)
            {
                sb.Append(s + " ");
            }
            return sb.ToString().Trim();
        }
        public static string BuildingBlockComponentCompress(string buildingBlockName)
        {
            return "BldBlk" + buildingBlockName.Replace(" ", "");
        }
        public static string BuildingBlockComponentDecompress(string compressBuildingBlockName)
        {
            var sArray = SplitCamelCase(compressBuildingBlockName.Replace("BldBlk", ""));
            var sb = new StringBuilder();
            foreach (var s in sArray)
            {
                sb.Append(s + " ");
            }
            return sb.ToString().Trim();
        }

        public static Rectangle AddPadingToRec(Rectangle rec, Padding padding, bool toInsidePadding)
        {
            var factore = toInsidePadding ? 1 : -1;
            return new Rectangle(rec.X + padding.Left * factore,
                rec.Y + padding.Top * factore,
                rec.Width - (padding.Left + padding.Right) * factore,
                rec.Height - (padding.Top + padding.Bottom) * factore);

        }

        public static bool PointInnerRect(Rectangle rec, Point pt)
        {
            return pt.X >= rec.X && pt.Y >= rec.Y && pt.X <= rec.Right && pt.Y <= rec.Bottom;
        }

        public static string GetTypeLastClass(Type type)
        {
            var typAll = type.ToString();
            return typAll.Split('.').Last();
        }

        static float sign(Point p1, Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        public static bool PointInTriangle(Point pt, Point v1, Point v2, Point v3)
        {
            float d1, d2, d3;
            bool has_neg, has_pos;

            d1 = sign(pt, v1, v2);
            d2 = sign(pt, v2, v3);
            d3 = sign(pt, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }

        public static void AllFormsToMinimized(bool setTag)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.WindowState != FormWindowState.Minimized)
                {
                    var tag = 1500 + (int)form.WindowState;
                    form.WindowState = FormWindowState.Minimized;
                    if (setTag)
                        form.Tag = tag;
                }
            }
        }

        public static void AllFormsToNormal(bool onlyHaveTag)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (onlyHaveTag)
                {
                    var tag = (int)(form.Tag);
                    if (tag >= 1500)
                        form.WindowState = (FormWindowState)(tag - 1500);
                }
                else
                {
                    form.WindowState = FormWindowState.Normal;
                }
            }
        }

        public static object FromByteArrayToObject(byte[] data)
        {
            if (data == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return obj;
            }
        }

        public static double StringToFloat(string str, double defualtValue=0.0)
        {
           var successfullyParsed = double.TryParse(str, out var d);
            if (successfullyParsed)
                return d;
            return defualtValue;

        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public static RECT GetlocationOfTaskBar()
        {
            //Get the handle of the task bar
            IntPtr TaskBarHandle;
            TaskBarHandle = FindWindow("Shell_traywnd", "");

            RECT rct;

            //Get the taskbar window rect in screen coordinates
            GetWindowRect(TaskBarHandle, out rct);
            return rct;
        }

        public static string GetTitleOfElemet(object obj)
        {
            if (obj is ElmLabel elmLabel)
            {
                return elmLabel.Title;
            }
            else if (obj is ElmEditBox editBox)
            {
                return editBox.Title;
            }
            else if (obj is ElmCheckBox checkBox)
            {
                return checkBox.Title;
            }
            return "Unknown";
        }

        public static TimeSpan ConvertToTimeSpan(string timeText)
        {
            return TimeSpan.FromSeconds(int.Parse(timeText));
        }

    }
}
