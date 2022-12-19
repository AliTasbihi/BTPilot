using AdvancePanelLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BaseElements
{
    public class ElmMultiConnector : BasicElement
    {
        public string Title { get; set; }
        public ElmMultiConnector(object parent)
        {
            Parent = parent;
        }
        private object GetDataConnector(object sender)
        {
            return null;
        }

        public void SetConnectorOutput(string[] connectorTitles)
        {
            if (string.IsNullOrEmpty(Name))
                return;

            for (int i = Children.Count - 1; i >= 0; i--)
            {
                var child = (BasicElement)Children[i];
                var arrows = advancePanel.GetAllArrowsElement(child);
                advancePanel.DeleteArrows(arrows);
                Children.Remove(child);
            }
            for (var i = 0; i < connectorTitles.Length; i++)
            {
                var title = connectorTitles[i];
                var lbl = new ElmLabel(this);
                lbl.Name = $"{Name}Item_{i + 1}_";
                lbl.Padding = new Padding(10, 0, 10, 0);
                lbl.Title = title;
                lbl.Alinment = ContentAlignment.MiddleRight;
                lbl.AddOneConnector(false, Color.Blue, 0, outputDataFunction: GetDataConnector);
                lbl.Parent = this;
                this.Children.Add(lbl);
            }
        }

        public int Draw(Graphics graphics, int y, int borderWidth)
        {
            var measuredText = GraphicFunction.MeasureText(graphics, Title, MyTextSize.Medium);
            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width) : ElmWidth;
            var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredText.Height) : ElmHeight;
            var rec = new Rectangle(0, y, w1, h1);
            var recPad = GlobalFunction.AddPadingToRec(rec, Padding, true);

            var recWPad2 = GlobalFunction.AddPadingToRec(recPad, new(0, 0, 20, 0), true);
            var rectext = GraphicFunction.DrawTextWithAlinment(graphics, Title, MyTextSize.Medium, ContentAlignment.MiddleRight, recWPad2, Color.Black);

            BackgroundArea = rectext;

            return y + h1 + 1;
        }
    }
}
