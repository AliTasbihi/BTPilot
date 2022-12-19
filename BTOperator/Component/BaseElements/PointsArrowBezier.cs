using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class PointsArrowBezier
    {
        public int bezierCount = 1;
        public Point[] rightArrowPoints = new Point[3];
        public Point[] bezierPoints1 = new Point[4];
        public Point[] bezierPoints2 = new Point[4];

        public GraphicsPath gpBezier1;
        public GraphicsPath gpBezier2;  
    }
}
