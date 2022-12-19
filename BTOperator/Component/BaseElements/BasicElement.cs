using AutoCreateWithJson.Component.Controller;
using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class BasicElement : BasicLoadClickDoubleClickAction
    {
        public string UniversalId { set; get; }
        public string Name { get; internal set; }
        public bool ElmHasPosition { get; set; } = false;
        public int ElmLeft { get; set; }
        public int ElmTop { get; set; }
        public int ElmWidth { get; set; } = 0;
        public int ElmHeight { get; set; } = 0;
        public Padding Padding { get; set; } = new Padding(0);
        public Rectangle BackgroundArea { get; set; }
        public bool Enable { get; set; } = true;
        public bool Visible { get; set; } = true;

        public bool ReadOnly
        {
            get { return GetSelfConnectorCount(true, true, false) > 0; }
        }

        [Category("NoneSave")]
        public object? Parent { get; set; } = null;

        [Category("NoneSave")]
        public List<object> Children { get; set; } = new List<object>();
        public int IsNecessaryToView = 1;

        
        public bool IsShowingRunTime { get; set; } = false;

        public Action<object, MouseEventArgs> TheClick { get; set; }
        public Action<object, MouseEventArgs> TheDoubleClick { get; set; }

        private AdvancePanel? _advancePanel = null;
        public AdvancePanel? advancePanel
        {
            get
            {
                if (_advancePanel == null)
                {
                    var p = Parent;
                    while (p != null)
                    {
                        if (p is AdvancePanel)
                        {
                            _advancePanel = (AdvancePanel)p;
                            break;
                        }
                        if (p is BasicElement basicElement)
                            p = basicElement.Parent;
                        else if (p is BasicBuildingBlock basicBuildingBlock)
                            p = basicBuildingBlock.Parent;
                    }

                }
                return _advancePanel;
            }
        }

        private BasicBuildingBlock? _buildingBlock = null;

        public BasicElement()
        {
            UniversalId = Guid.NewGuid().ToString("B");
        }

        public BasicBuildingBlock? buildingBlock
        {
            get
            {
                if (_buildingBlock == null)
                {
                    var p = Parent;
                    while (p != null)
                    {
                        if (p is BasicBuildingBlock)
                        {
                            _buildingBlock = (BasicBuildingBlock)p;
                            break;
                        }
                        if (p is BasicElement basicElement)
                            p = basicElement.Parent;
                    }
                }
                return _buildingBlock;
            }
        }

        public bool IsThisParent(object mainParent)
        {
            if (mainParent == null)
                return false;
            if (this == mainParent)
                return true;
            if (Parent == mainParent)
                return true;
            if (Parent == null)
                return false;
            if (Parent is BasicBuildingBlock)
                return false;
            if (Parent is BasicElement objParent)
            {
                return objParent.IsThisParent(mainParent);
            }
            return false;
        }


        public void AddOneConnector(bool isInputType, Color color, int maxConnection, int offset = Int32.MinValue, Func<object, object> outputDataFunction = null)
        {
            if (offset <= -100000)
                offset = GraphicConstant.connectorOffset;
            var cntor = new ElmConnector();
            cntor.Name = isInputType ? $"{this.Name}ConnectorLinkIn" : $"{this.Name}ConnectorLinkOut";
            cntor.InputOutput = isInputType ? InputOutputType.Input : InputOutputType.Output;
            cntor.Color = color;
            cntor.MaxConnection = maxConnection;
            cntor.RelatedElement = this;
            cntor.Parent = this;
            cntor.Offset = offset;
            if (!isInputType && outputDataFunction != null)
                cntor.OutputDataFunction = outputDataFunction;
            Children.Add(cntor);
        }

        public void AddTwoConnector(Color color, int maxConnectionIn, int maxConnectionOut, int offset = Int32.MinValue, Func<object, object> outputDataFunction = null)
        {
            if (offset <= -100000)
                offset = GraphicConstant.connectorOffset;
            var cntor1 = new ElmConnector();
            cntor1.Name = $"{this.Name}ConnectorLinkIn";
            cntor1.InputOutput = InputOutputType.Input;
            cntor1.Color = color;
            cntor1.MaxConnection = maxConnectionIn;
            cntor1.RelatedElement = this;
            cntor1.Parent = this;
            cntor1.Offset = offset;
            Children.Add(cntor1);

            var cntor2 = new ElmConnector();
            cntor2.Name = $"{this.Name}ConnectorLinkOut";
            cntor2.InputOutput = InputOutputType.Output;
            cntor2.Color = color;
            cntor2.MaxConnection = maxConnectionOut;
            cntor2.RelatedElement = this;
            cntor2.Parent = this;
            cntor2.Offset = offset;
            if (outputDataFunction != null)
                cntor2.OutputDataFunction = outputDataFunction;
            Children.Add(cntor2);
        }
        //todo:?? its 
        public Rectangle? GetBoundaryClientToModel()
        {
            var myParent = Parent;
            while (myParent != null)
            {
                if (myParent is BasicBuildingBlock buildingBlock)
                {
                    return new Rectangle(buildingBlock.Left + BackgroundArea.Left,
                                         buildingBlock.Top + BackgroundArea.Top,
                                         BackgroundArea.Width,
                                         BackgroundArea.Height);
                }
                myParent = ((BasicElement)myParent).Parent;
            }
            return null;
        }

        // تعداد کانکشن های خود المان
        // در دو حالت : یکی تعداد اتصال های فعال
        // دوم فقط کانکشن های قابل اتصال
        public int GetSelfConnectorCount(bool onlyConnectedLink, bool inputConnection, bool outputConnection)
        {
            var count = 0;
            foreach (var child in Children)
            {
                if (child is ElmConnector elmConnector)
                {
                    if (!inputConnection && elmConnector.InputOutput == InputOutputType.Input)
                        continue;
                    if (!outputConnection && elmConnector.InputOutput == InputOutputType.Output)
                        continue;

                    if (onlyConnectedLink)
                    {
                        if (IsTestConnection(elmConnector))
                            count++;
                    }
                    else
                        count++;
                }
            }
            return count;

        }

        private bool IsTestConnection(ElmConnector elmConnector)
        {
            if (advancePanel == null)
                return false;
            foreach (var elmArrowButton in advancePanel.AllArrowButton)
            {
                if (elmArrowButton.ConnectorStart == elmConnector || elmArrowButton.ConnectorEnd == elmConnector)
                {
                    return true;
                }
            }
            return false;
        }

        // تعداد کانکشن های تمام زیر المان های  همین المان جاری
        // در دو حالت : یکی تعداد اتصال های فعال
        // دوم فقط کانکشن های قابل اتصال
        public int GetChildrenConnectorCount(bool onlyConnectedLink)
        {
            var count = 0;
            foreach (var child in Children)
            {
                if (child is not ElmConnector)
                {
                    if (child is BasicElement be)
                    {
                        count += be.GetSelfConnectorCount(onlyConnectedLink, true, true);
                    }
                }
            }
            return count;
        }
    }
}
