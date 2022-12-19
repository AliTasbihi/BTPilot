using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCreateWithJson.Component.BaseStructure;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class ElmArrowButton : BasicElement
    {
        public ElmConnector ConnectorStart { get; set; }
        public ElmConnector ConnectorEnd { get; set; }
        public object TransferData { get; internal set; }
        

        public string GetInfoConnectorStart
        {
            get
            {
                if (ConnectorStart == null) return "";
                return ConnectorStart.UniversalId;
            }
        }
        public string GetInfoConnectorEnd
        {
            get
            {
                if (ConnectorEnd == null) return "";
                return ConnectorEnd.UniversalId;
            }
        }

        public PointsArrowBezier screenPointsArrowBezier { get; set; }
    }
}
