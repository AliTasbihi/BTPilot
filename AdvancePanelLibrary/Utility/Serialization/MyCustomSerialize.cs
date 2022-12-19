using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.Controller;
using AdvancePanelLibrary.Utility.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Utility.Serialization
{
    [Serializable()]
    public class MyCustomSerialize
    {
        public string CtrlName { get; set; }
        public string CtrlTypeName { get; set; }
        public string PartialNamespace { get; set; }

        public Hashtable PropertyList { get; set; } = new Hashtable();
        public List<MyCustomSerialize> Children1 { get; set; } = new List<MyCustomSerialize>();
        public List<MyCustomSerialize> Children2 { get; set; } = new List<MyCustomSerialize>();

        public bool IsAdvancePanel = false;
        public bool IsBasicBuildingBlock = false;
        public bool IsBasicElement = false;

        public MyCustomSerialize(object obj)
        {
            if (obj == null)
                return;
            IsAdvancePanel = obj is AdvancePanel;
            AdvancePanel? advancePanel = IsAdvancePanel ? obj as AdvancePanel : null;

            IsBasicBuildingBlock = obj is BasicBuildingBlock;
            BasicBuildingBlock? buildingBlock = IsBasicBuildingBlock ? obj as BasicBuildingBlock : null;

            IsBasicElement = obj is BasicElement;
            BasicElement? basicElement = IsBasicElement ? obj as BasicElement : null;

            CtrlName = "";
            CtrlTypeName = obj.GetType().Name;
            PartialNamespace = "";

            MyLog.WritelnBoth($"----{CtrlTypeName}");

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
            foreach (PropertyDescriptor myProperty in properties)
            {
                if (IsNeedProperty(myProperty, IsAdvancePanel, IsBasicBuildingBlock, IsBasicElement))
                {
                    var val = myProperty.GetValue(obj);
                    PropertyList.Add(myProperty.Name, val);
                }
                else
                {
                    //if (!IsAdvancePanel)
                    //    MyLog.ForceWritelnBoth(CtrlTypeName, myProperty.Name);
                }
            }

            if (IsAdvancePanel && advancePanel != null)
            {
                CtrlName = advancePanel.Name;
                PartialNamespace = advancePanel.GetType().Namespace;
                foreach (var bld in advancePanel.AllBuildingBlock)
                    Children1.Add(new MyCustomSerialize(bld));
                foreach (var arrow in advancePanel.AllArrowButton)
                    Children2.Add(new MyCustomSerialize(arrow));

            }
            else if (IsBasicBuildingBlock && buildingBlock != null)
            {
                CtrlName = buildingBlock.Name;
                PartialNamespace = buildingBlock.GetType().Namespace;
                foreach (var elm in buildingBlock.Children)
                {
                    Children1.Add(new MyCustomSerialize(elm));
                }
            }
            else if (IsBasicElement && basicElement != null)
            {
                CtrlName = basicElement.Name;
                PartialNamespace = basicElement.GetType().Namespace;
                foreach (var elm in basicElement.Children)
                {
                    Children1.Add(new MyCustomSerialize(elm));
                }
            }
        }

        private bool IsNeedProperty(PropertyDescriptor myProperty, bool isAdvancePanel, bool isBasicBuildingBlock, bool isBasicElement)
        {
            if (myProperty.Category.Equals("NoneSave", StringComparison.OrdinalIgnoreCase))
                return false;
            var propertyName = myProperty.Name;
            if (isAdvancePanel)
            {
                if (GeneralPeroperty(propertyName))
                    return true;
                if (CheckPropertyName(propertyName, "Dock", "Anchor"))
                    return true;
            }
            else if (isBasicBuildingBlock)
            {
                if (GeneralPeroperty(propertyName))
                    return true;
                if (CheckPropertyName(propertyName, "xx", "xx"))
                    return true;
            }
            else if (isBasicElement)
            {
                if (GeneralPeroperty(propertyName))
                    return true;
                if (CheckPropertyName(propertyName, "MySize", "Title", "Alinment", "BackGround", "TextColor",
                    "GetInfoConnectorStart", "GetInfoConnectorEnd", "IsHeaderLabel", "InputOutput", "TitleVisible",
                    "TitleVisible", "SelectElementSelectType", "SelectElementStorageSerialize", "IsCollapseExpandMode",
                    "Checked", "HasTitle", "TitlePosition", "Alignment", "ExpandItems", "AutoSize", "Text", "SelectedText"))
                    return true;
            }
            return false;
        }

        private bool CheckPropertyName(string propertyName, params string[] values)
        {
            foreach (var value in values)
            {
                if (propertyName.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private bool GeneralPeroperty(string propertyName)
        {
            return CheckPropertyName(propertyName, "Name", "Left", "Top",
                "Width", "Height", "Enable", "Visible", "Padding", "ElmHasPosition",
                "ElmLeft", "ElmTop", "ElmWidth", "ElmHeight", "BackgroundArea",
                 "IsNecessaryToView", "Color", "Offset", "MaxConnection", "InputOutputType",
                 "UniversalId");
        }
    }

    public class MyDeSerializeFactory
    {
        public static void CreateInstance(MyCustomSerialize model, AdvancePanel advancePanel)
        {
            if (advancePanel == null)
                return;
            if (model != null && !model.IsAdvancePanel)
                return;
            advancePanel.ClearAll();

            SetPropertyToObject(advancePanel, model.PropertyList);

            foreach (var modelBld in model.Children1)
            {
                CreateBuildingBlock(advancePanel, modelBld);
            }
            foreach (var modelArrow in model.Children2)
            {
                CreateArrowButton(advancePanel, modelArrow);
            }
        }

        private static void CreateArrowButton(AdvancePanel advancePanel, MyCustomSerialize modelArrow)
        {
            var s = modelArrow.PropertyList["GetInfoConnectorStart"].ToString();
            var e = modelArrow.PropertyList["GetInfoConnectorEnd"].ToString();

            ElmArrowButton arrow = advancePanel.AddLinkArrowButtonConnectionWithGuid(s, e);
            SetPropertyToObject(arrow, modelArrow.PropertyList);

        }

        private static void CreateBuildingBlock(AdvancePanel advancePanel, MyCustomSerialize modelBuildingBlock)
        {
            if (modelBuildingBlock.IsBasicBuildingBlock)
            {
                var buildingBlock = advancePanel.AddBuildingBlock(modelBuildingBlock.CtrlTypeName) as BasicBuildingBlock;
                SetPropertyForChildrenOfElement(buildingBlock.Children, modelBuildingBlock.Children1);
                SetPropertyToObject(buildingBlock, modelBuildingBlock.PropertyList);
                buildingBlock.AssignOnTheClickAndDoubleClickMethod();
            }
        }

        private static void SetPropertyForChildrenOfElement(List<object> elementChildren, List<MyCustomSerialize> modelChildren)
        {
            if (elementChildren.Count == modelChildren.Count)
            {
                for (var i = 0; i < elementChildren.Count; i++)
                {
                    var basicElement = (BasicElement)elementChildren[i];
                    if (basicElement.Children.Count > 0)
                    {
                        SetPropertyForChildrenOfElement(basicElement.Children, modelChildren[i].Children1);
                    }
                    SetPropertyToObject(basicElement, modelChildren[i].PropertyList);
                }
            }
        }

        private static void SetPropertyToObject(object obj, Hashtable modelPropertyList)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
            foreach (PropertyDescriptor myProperty in properties)
            {
                if (modelPropertyList.Contains(myProperty.Name))
                {
                    var o = modelPropertyList[myProperty.Name];
                    if (o == null)
                        continue;
                    myProperty.SetValue(obj, o);
                }
            }
        }
    }
}
