using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class ElmSpace : BasicElement
    {
        public int Dx { get; }
        public ElmSpace(int dx=4)
        {
            Dx = dx;
        }

        public int Draw(Graphics graphics, int y, int borderWidth)
        {
            return y + Dx + 1;
        }
    }
}
