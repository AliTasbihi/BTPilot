using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class ElmSeparateLine : BasicElement
    {
        public int Draw(Graphics graphics, int y, int borderWidth)
        {
            var dx = 6;
            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width) : ElmWidth;
            graphics.DrawLine(GraphicConstant.penBez1, dx, y, w1-2*dx, y);
            return y + 1;
        }
    }
}
