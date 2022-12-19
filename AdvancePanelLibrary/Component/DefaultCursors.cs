using AdvancePanelLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component
{
    public class DefaultCursors
    {
        public Cursor? Arrow1;
        public Cursor? Arrow2;
        public Cursor? Arrow3;
        public Cursor? Cross;
        public Cursor? Move;
        public Cursor? Hand1;
        public Cursor? Hand2;
        public Cursor? Hand3;
        public Cursor? Pen;
        public Cursor? ZoomIn;
        public Cursor? ZoomOut;

        public DefaultCursors()
        {
            Arrow1 = LoadFromFile(GlobalFunction.FindFile("arrow_l.cur"));
            Arrow2 = LoadFromFile(GlobalFunction.FindFile("arrow_il.cur"));
            Arrow3 = LoadFromFile(GlobalFunction.FindFile("3dgarro.cur"));
            Cross = LoadFromFile(GlobalFunction.FindFile("cross.cur"));
            Move = LoadFromFile(GlobalFunction.FindFile("cross.cur"));
            Hand1 = LoadFromFile(GlobalFunction.FindFile("hmove1.cur"));
            Hand2 = LoadFromFile(GlobalFunction.FindFile("hmove2.cur"));
            Hand3 = LoadFromFile(GlobalFunction.FindFile("hmove3.cur"));
            Pen = LoadFromFile(GlobalFunction.FindFile("pen_il.cur"));
            ZoomIn = LoadFromFile(GlobalFunction.FindFile("Zoom_In.cur"));
            ZoomOut = LoadFromFile(GlobalFunction.FindFile("Zoom_Out.cur"));

        }

        private Cursor? LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
                return new Cursor(fileName);
            return null;
        }


    }
}
