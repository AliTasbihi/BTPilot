using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoCreateWithJson.Utility.SelectUIElement;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using Application = FlaUI.Core.Application;
using MessageBox = System.Windows.MessageBox;

namespace AutoCreateWithJson.Component.BaseStructure
{
    /// <summary>
    /// این کلاس یک المان اتومیشن را می گیرد
    /// و تمام اطلاعات تا بالاترین المان موجود در ویندوز را بدست می آورد
    /// و همچنین تمام خصوصیات موجود در المان های  در مسیر به سمت بالا را بدست می آورد
    /// و برای هر کدام از آنها مختصات لیبل را محاسبه می کند و در حافظه نگه میدارد
    ///
    /// همچنین قابلیت نگهداری آیدی آیکون درختوار را نیز دارد
    /// همچنین قابلیت رسم تمام خصوصیات بر روی پنل را دارد
    /// 
    /// </summary>
    public class ElementViewAllDetail
    {
        public static readonly int Node_Normal = 0;
        public static readonly int Node_NormalActive = 1;
        public static readonly int Node_Home = 2;
        public static readonly int Node_HomeActive = 3;
        public static readonly int Node_Item = 4;
        public static readonly int Node_ItemActive = 5;
        public static readonly int Node_Select = 6;
        public static readonly int Node_SelectActive = 7;

        public AutomationElement SelectedElement;
        private bool _analyzeUpToRootElement;
        private List<FlaUI.Core.Application> _allRunningApplication;
        private ElementTreeDetail ElementTreeDetails;
        private const int DefualtHeightLabel = 12;

        public ElementViewAllDetail(AutomationElement selectedElement, List<FlaUI.Core.Application> allRunningApplication, bool analyzeUpToRootElement)
        {
            SelectedElement = selectedElement;
            _allRunningApplication = allRunningApplication;
            _analyzeUpToRootElement = analyzeUpToRootElement;

        }

        public bool StartAnalyze()
        {
            if (_analyzeUpToRootElement)
            {
                // لیست تمام المان ها از المان انتخاب شده تا بالاترین المان موجود در ویندوز را بدست می آورد
                var allCommonElementsUpToParent =
                    _analyzeUpToRootElement ? GetListOfElementUptoParent(SelectedElement) : null;

                // با توجه به المان انتخاب شده برنامه اجرایی مرتبط با آن را پیدا می کند
                var runningApp = FindRunningApplicationForThisSelectedElement(SelectedElement);
                if (runningApp.app is not null)
                {
                    DrawTreeOfElement(allCommonElementsUpToParent, runningApp.app, SelectedElement);
                    return true;
                }
                return false;
            }

            DrawTreeOfElement(null, null, SelectedElement);
            return true;
        }

        // لیست تمام المان ها از المان انتخاب شده تا بالاترین المان موجود در ویندوز را بدست می آورد
        private List<AutomationElement> GetListOfElementUptoParent(AutomationElement element)
        {
            var li = new List<AutomationElement>();
            var elm = element;
            while (elm != null)
            {
                li.Add(elm);
                elm = elm.Parent;
            }

            return li;
        }

        // با توجه به المان انتخاب شده برنامه اجرایی مرتبط با آن را پیدا می کند
        private (Application app, AutomationElement element) FindRunningApplicationForThisSelectedElement(AutomationElement element)
        {
            while (element != null)
            {
                foreach (var runApp in _allRunningApplication)
                {
                    if (runApp.ProcessId == element.Properties.ProcessId)
                        return new(runApp, element);
                }

                element = element.Parent;
            }

            return new(null, null);
        }

        #region رسم درختواره
        // Tree View رسم دیتاها در درختواره 
        public void DrawDataToTreeView(TreeView tv_ElemetStructure)
        {
            tv_ElemetStructure.BeginUpdate();
            tv_ElemetStructure.Nodes.Clear();
            if (ElementTreeDetails is not null)
            {
                var node = AddOneItemToTreeView(tv_ElemetStructure.Nodes, ElementTreeDetails);
                AddChildrnItemToTreeView(node.Nodes, ElementTreeDetails.SubElementTreeDetails);
            }
            tv_ElemetStructure.EndUpdate();
        }

        public int GetCounOfLevel()
        {
            return _levelCounterStart_1 - 1;
        }

        private TreeNode AddOneItemToTreeView(TreeNodeCollection nodes, ElementTreeDetail elementTreeDetails)
        {
            var node = nodes.Add(elementTreeDetails.Title);
            node.ImageIndex = elementTreeDetails.ImageIndex;
            node.SelectedImageIndex = elementTreeDetails.SelectedImageIndex;
            node.Tag = elementTreeDetails;
            return node;
        }

        private void AddChildrnItemToTreeView(TreeNodeCollection nodes, List<ElementTreeDetail> subElementTreeDetails)
        {
            foreach (var subElementTreeDetail in subElementTreeDetails)
            {
                var node = AddOneItemToTreeView(nodes, subElementTreeDetail);
                if (subElementTreeDetail.SubElementTreeDetails.Count > 0)
                    AddChildrnItemToTreeView(node.Nodes, subElementTreeDetail.SubElementTreeDetails);
            }
        }
        #endregion

        public void DrawPropertyToPanel(Panel panel, int shiftX, int shiftY)
        {
            ElementTreeDetails.DrawPropertyToPanel(panel, shiftX, shiftY);
        }

        private int _counterOfFindProgram = 0;
        private int _levelCounterStart_1 = 0;
        private int _homeLevelCounter = 0;

        private void DrawTreeOfElement(List<AutomationElement> allCommonElementsUpToParent, Application firstApplicationFromTop, AutomationElement lastElementOnBottom)
        {
            if (!_analyzeUpToRootElement)
            {
                // فقط خود المان نیاز هست و والدین آن نیاز نیست
                ElementTreeDetails = new ElementTreeDetail();
                GetAllDataOfElement_DataAndComponentPosition(ElementTreeDetails, lastElementOnBottom, false, false, false);
                return;

            }
            if (allCommonElementsUpToParent is null)
            {
                MessageBox.Show("درختواره المان ها خالی می باشد");
                return;
            }
            if (firstApplicationFromTop is null)
            {
                MessageBox.Show("هیچ برنامه ایی مرتبط با المان انتخاب شده پیدا نشد");
                return;
            }
            ElementTreeDetails = new ElementTreeDetail();

            var topElement = allCommonElementsUpToParent.Last();
            var isFirstElementOfApplication = topElement.Properties.ProcessId == firstApplicationFromTop.ProcessId;
            var isLastElementOfBottom = topElement.Equals(lastElementOnBottom);
            _counterOfFindProgram = 0;
            _levelCounterStart_1 = 0;
            _homeLevelCounter = 0;
            GetAllDataOfElement_DataAndComponentPosition(ElementTreeDetails, topElement, isFirstElementOfApplication, isLastElementOfBottom, isFirstElementOfApplication);

            GetAllDataOfChildrenElement_DataAndComponentPosition(ElementTreeDetails, topElement.FindAllChildren(), allCommonElementsUpToParent, firstApplicationFromTop, lastElementOnBottom);

        }

        private void GetAllDataOfChildrenElement_DataAndComponentPosition(ElementTreeDetail parentElementTreeDetails, AutomationElement[] childrenOfElement, List<AutomationElement> listOfElementUptoParent, Application firstApplicationFromTop, AutomationElement lastElementOnBottom)
        {
            ElementTreeDetail selectNode = null;
            AutomationElement selectElement = null;
            foreach (var child in childrenOfElement)
            {
                var isFirstElementOfApplication = child.Properties.ProcessId == firstApplicationFromTop.ProcessId;
                var isLastElementOfBottom = child.Equals(lastElementOnBottom);
                ElementTreeDetail nodeTreeDetail = parentElementTreeDetails.AddNewNode();
                var isMainRouteElement = false;
                foreach (var elm in listOfElementUptoParent)
                {
                    if (child.Equals(elm))
                    {
                        selectNode = nodeTreeDetail;
                        //selectNodes = node.Nodes;
                        selectElement = child;
                        isMainRouteElement = true;
                        break;
                    }
                }
                GetAllDataOfElement_DataAndComponentPosition(nodeTreeDetail, child, isFirstElementOfApplication, isLastElementOfBottom, isMainRouteElement);
            }

            if (selectNode != null && selectElement != null)
            {
                _levelCounterStart_1++;
                GetAllDataOfChildrenElement_DataAndComponentPosition(selectNode, selectElement.FindAllChildren(),
                    listOfElementUptoParent, firstApplicationFromTop, lastElementOnBottom);
            }

        }

        private void GetAllDataOfElement_DataAndComponentPosition(ElementTreeDetail elementTreeDetails, AutomationElement element, bool isFirstElementOfApplication, bool isLastElementOfBottom, bool isMainRouteElement)
        {
            var elementViewModel = new ElementViewModel(element);
            var allItems = elementViewModel.AllItems;
            var curentY = 10;
            elementTreeDetails.Title = GetTitleForElement(allItems);
            elementTreeDetails.LevelNumber = _levelCounterStart_1;
            elementTreeDetails.IsMainRouteElement = isMainRouteElement;
            elementTreeDetails.Element = element;
            foreach (var detailElement in allItems)
            {
                AddTitleElementForPanel(elementTreeDetails, detailElement.Title, ref curentY);
                foreach (var detailElementProperty in detailElement.Properties)
                {
                    AddSubElementForPanel(elementTreeDetails, detailElement.Title, _levelCounterStart_1, detailElementProperty.Key, detailElementProperty.Value, ref curentY);
                }
            }

            elementTreeDetails.ImageIndex = Node_Normal;
            elementTreeDetails.SelectedImageIndex = Node_NormalActive;
            var levelIndicator = -1;
            if (isFirstElementOfApplication)
            {
                _counterOfFindProgram++;
                if (_counterOfFindProgram == 1)
                {
                    elementTreeDetails.ImageIndex = Node_Home;
                    elementTreeDetails.SelectedImageIndex = Node_HomeActive;
                    levelIndicator = 0;
                    _homeLevelCounter = _levelCounterStart_1;
                }
                else
                {
                    levelIndicator = _levelCounterStart_1 - _homeLevelCounter;
                    if (isLastElementOfBottom)
                    {
                        elementTreeDetails.ImageIndex = Node_Select;
                        elementTreeDetails.SelectedImageIndex = Node_SelectActive;
                    }
                    else
                    {
                        elementTreeDetails.ImageIndex = Node_Item;
                        elementTreeDetails.SelectedImageIndex = Node_ItemActive;
                    }
                }

            }

            elementTreeDetails.LevelNumber = levelIndicator;
        }

        private void AddSubElementForPanel(ElementTreeDetail elementTreeDetails, string title, int levelNumber, string name, string value, ref int curentY)
        {
            ElementTreeDetail.OneComponent oneComponent = new ElementTreeDetail.OneComponent();
            oneComponent.IsTitle = false;
            oneComponent.HasAddButtom = levelNumber > 0;
            oneComponent.PosX2 = 15;
            oneComponent.PosX3 = 160;
            oneComponent.PosY = curentY;
            oneComponent.Name = name;
            oneComponent.Value = value;
            if (oneComponent.HasAddButtom)
                oneComponent.TagData = $"condition\t{levelNumber}\t{title}\t{name}\t{value}"; ;
            elementTreeDetails.PanelComponents.Add(oneComponent);
            curentY += DefualtHeightLabel + 3;
        }

        private void AddTitleElementForPanel(ElementTreeDetail elementTreeDetails, string title, ref int curentY)
        {
            ElementTreeDetail.OneComponent oneComponent = new ElementTreeDetail.OneComponent();
            oneComponent.Title = title;
            oneComponent.IsTitle = true;
            oneComponent.HasAddButtom = false;
            oneComponent.PosX1 = 2;
            oneComponent.PosY = curentY;
            elementTreeDetails.PanelComponents.Add(oneComponent);
            curentY += DefualtHeightLabel + 3;
        }

        private string GetTitleForElement(List<DetailElement> allItems)
        {
            var name = GetPropertyFromDetailElement(allItems, "identification", "Name");
            var className = GetPropertyFromDetailElement(allItems, "identification", "ClassName");
            var localizedControlType = GetPropertyFromDetailElement(allItems, "identification", "LocalizedControlType");
            var title = name;
            if (name == "")
                title = className;
            if (localizedControlType != "")
                title = "\"" + title + "\" " + localizedControlType;
            return title;
        }

        private string GetPropertyFromDetailElement(List<DetailElement> allItems, string title, string key)
        {
            foreach (var detailElement in allItems)
            {
                if (detailElement.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var detailElementProperty in detailElement.Properties)
                    {
                        if (detailElementProperty.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                        {
                            if (detailElementProperty.Value.Equals("Not Supported", StringComparison.OrdinalIgnoreCase))
                                return "";
                            return detailElementProperty.Value;
                        }
                    }
                }
            }

            return "";
        }

    }

    public class ElementTreeDetail
    {
        public List<ElementTreeDetail> SubElementTreeDetails = new List<ElementTreeDetail>();

        public int LevelNumber = -1;
        public bool IsMainRouteElement = false;
        public string Title;
        public int ImageIndex = -1;
        public int SelectedImageIndex = -1;
        public AutomationElement Element;
        public List<OneComponent> PanelComponents = new List<OneComponent>();

        public ElementTreeDetail AddNewNode()
        {
            var elementTreeDetail = new ElementTreeDetail();
            SubElementTreeDetails.Add(elementTreeDetail);
            return elementTreeDetail;
        }

        public class OneComponent
        {
            public bool IsTitle;
            public bool HasAddButtom;
            public string Title;
            public string Name;
            public string Value;
            public int PosX1;
            public int PosX2;
            public int PosX3;
            public int PosY;
            public string TagData;
        }

        public void DrawPropertyToPanel(Panel panel,
            int shiftX, int shiftY,
            Action<object, EventArgs> clickForAddConditionToDetectStructure = null,
            Action<object, EventArgs> mouseEnterForAddConditionToDetectStructure = null,
            Action<object, EventArgs> mouseLeaveForAddConditionToDetectStructure = null)
        {
            panel.Controls.Clear();
            panel.AutoScroll = true;
            foreach (var oneComponent in PanelComponents)
            {

                if (oneComponent.IsTitle)
                {
                    var lbl = CreateLabel(oneComponent.Title, oneComponent.PosX1 + shiftX, oneComponent.PosY + shiftY, oneComponent.IsTitle, false);
                    panel.Controls.Add(lbl);
                }
                else
                {
                    if (oneComponent.HasAddButtom && IsMainRouteElement)
                    {
                        var lblBtn = CreateLabel("+", oneComponent.PosX1 + shiftX, oneComponent.PosY + shiftY, false, false);
                        lblBtn.Tag = oneComponent.TagData;
                        if (LevelNumber > 0)
                        {
                            lblBtn.Click += new System.EventHandler(clickForAddConditionToDetectStructure);
                            lblBtn.MouseEnter += new System.EventHandler(mouseEnterForAddConditionToDetectStructure);
                            lblBtn.MouseLeave += new System.EventHandler(mouseLeaveForAddConditionToDetectStructure);
                        }

                        panel.Controls.Add(lblBtn);
                    }
                    var lbl = CreateLabel(oneComponent.Name, oneComponent.PosX2 + shiftX, oneComponent.PosY + shiftY, false, false);
                    panel.Controls.Add(lbl);
                    lbl = CreateLabel(oneComponent.Value, oneComponent.PosX3 + shiftX, oneComponent.PosY + shiftY, false, true);
                    panel.Controls.Add(lbl);
                }
            }
        }

        private System.Windows.Forms.Label CreateLabel(string text, int x, int y, bool isBold, bool isValue)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.Location = new Point(x, y);
            lbl.Text = text;
            lbl.AutoSize = true;
            if (isBold)
            {
                lbl.ForeColor = Color.Blue;
                lbl.Font = new Font(lbl.Font, FontStyle.Bold);
            }
            else if (isValue)
            {
                if (text.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    lbl.ForeColor = Color.Green;
                }
                else
                {
                    lbl.ForeColor = Color.Red;
                }
            }

            return lbl;
        }
    }

}
