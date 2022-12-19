using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class ElmConnector : BasicElement
    {
        public ElmConnector()
        {
            ElmWidth = GraphicConstant.connectorWidth;
            ElmHeight = GraphicConstant.connectorWidth; 
        }

        public InputOutputType InputOutput { get; set; }
        public object? RelatedElement { get; set; }
        public Color Color { get; set; }
        public int Offset { get; set; } = GraphicConstant.connectorOffset;
        public int MaxConnection { get; set; } = 0;
        public Func<object, object> OutputDataFunction { get; internal set; }

        public void Draw(Graphics bmpGfx, int  rtLeft, int rlTop)
        {
            var rec = new Rectangle(rtLeft, Offset+rlTop,  ElmWidth,  ElmHeight);
            BackgroundArea = rec;
            GraphicFunction.DrawFillRectangle(bmpGfx, rec, Color, Color.White);
        }
    }
}
