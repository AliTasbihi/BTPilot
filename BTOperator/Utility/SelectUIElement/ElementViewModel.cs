using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using FlaUI.UIA3.Identifiers;

namespace AutoCreateWithJson.Utility.SelectUIElement
{
    public class ElementViewModel : ObservableObject
    {
        public ElementViewModel(AutomationElement automationElement)
        {
            AutomationElement = automationElement;
        }

        public AutomationElement AutomationElement { get; }

        private List<DetailElement> _allItems;
        public List<DetailElement> AllItems
        {
            get
            {
                if (_allItems == null)
                {
                    _allItems = LoadDetails();
                }
                return _allItems;
            }
        }

        public List<DetailElement> LoadDetails()
        {
            var list = new List<DetailElement>();
            var cacheRequest = new CacheRequest();
            DetailElement oneGroup;

            var elementCached = AutomationElement.FindFirst(TreeScope.Element, TrueCondition.Default);
            if (elementCached != null)
            {
                // Element identification
                oneGroup = new DetailElement();
                oneGroup.Title = "identification";
                oneGroup.FromAutomationProperty("AutomationId", elementCached.Properties.AutomationId);
                oneGroup.FromAutomationProperty("Name", elementCached.Properties.Name);
                oneGroup.FromAutomationProperty("ClassName", elementCached.Properties.ClassName);
                oneGroup.FromAutomationProperty("ControlType", elementCached.Properties.ControlType);
                oneGroup.FromAutomationProperty("LocalizedControlType", elementCached.Properties.LocalizedControlType);
                oneGroup.FromAutomationPropertyString("FrameworkType", elementCached.FrameworkType.ToString());
                oneGroup.FromAutomationProperty("FrameworkId", elementCached.Properties.FrameworkId);
                oneGroup.FromAutomationProperty("ProcessId", elementCached.Properties.ProcessId);
                list.Add(oneGroup);

                // Element details
                oneGroup = new DetailElement();
                oneGroup.Title = "Details";
                oneGroup.FromAutomationProperty("IsEnabled", elementCached.Properties.IsEnabled);
                oneGroup.FromAutomationProperty("IsOffscreen", elementCached.Properties.IsOffscreen);
                oneGroup.FromAutomationProperty("BoundingRectangle", elementCached.Properties.BoundingRectangle);
                oneGroup.FromAutomationProperty("HelpText", elementCached.Properties.HelpText);
                oneGroup.FromAutomationProperty("IsPassword", elementCached.Properties.IsPassword);
                // Special handling for NativeWindowHandle
                var nativeWindowHandle = elementCached.Properties.NativeWindowHandle.ValueOrDefault;
                var nativeWindowHandleString = "Not Supported";
                if (nativeWindowHandle != default(IntPtr))
                {
                    nativeWindowHandleString = String.Format("{0} ({0:X8})", nativeWindowHandle.ToInt32());
                }
                oneGroup.FromAutomationPropertyString("NativeWindowHandle", nativeWindowHandleString);
                list.Add(oneGroup);
            }


            // Element identification
            oneGroup = new DetailElement();
            oneGroup.Title = "Pattern Support";
            // Pattern details
            var allSupportedPatterns = AutomationElement.GetSupportedPatterns();
            var allPatterns = AutomationElement.Automation.PatternLibrary.AllForCurrentFramework;
            foreach (var pattern in allPatterns)
            {
                var hasPattern = IfContainsPattern(allSupportedPatterns, pattern);

                oneGroup.FromAutomationPropertyString(pattern.Name + "Pattern", hasPattern ? "Yes" : "No");
            }
            list.Add(oneGroup);

            // GridItemPattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.GridItemPattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "GridItem Pattern";
                var pattern = AutomationElement.Patterns.GridItem.Pattern;
                oneGroup.FromAutomationProperty("Column", pattern.Column);
                oneGroup.FromAutomationProperty("ColumnSpan", pattern.ColumnSpan);
                oneGroup.FromAutomationProperty("Row", pattern.Row);
                oneGroup.FromAutomationProperty("RowSpan", pattern.RowSpan);
                oneGroup.FromAutomationProperty("ContainingGrid", pattern.ContainingGrid);
                list.Add(oneGroup);
            }

            // GridPattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.GridPattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "Grid Pattern";
                var pattern = AutomationElement.Patterns.Grid.Pattern;

                oneGroup.FromAutomationProperty("ColumnCount", pattern.ColumnCount);
                oneGroup.FromAutomationProperty("RowCount", pattern.RowCount);
                list.Add(oneGroup);
            }

            // LegacyIAccessiblePattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.LegacyIAccessiblePattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "LegacyIAccessible Pattern";
                var pattern = AutomationElement.Patterns.LegacyIAccessible.Pattern;
                oneGroup.FromAutomationProperty("Name", pattern.Name);
                oneGroup.FromAutomationPropertyString("State", AccessibilityTextResolver.GetStateText(pattern.State.ValueOrDefault));
                oneGroup.FromAutomationPropertyString("Role", AccessibilityTextResolver.GetRoleText(pattern.Role.ValueOrDefault));
                oneGroup.FromAutomationProperty("Value", pattern.Value);
                oneGroup.FromAutomationProperty("ChildId", pattern.ChildId);
                oneGroup.FromAutomationProperty("DefaultAction", pattern.DefaultAction);
                oneGroup.FromAutomationProperty("Description", pattern.Description);
                oneGroup.FromAutomationProperty("Help", pattern.Help);
                oneGroup.FromAutomationProperty("KeyboardShortcut", pattern.KeyboardShortcut);
                oneGroup.FromAutomationProperty("Selection", pattern.Selection);
                list.Add(oneGroup);
            }

            // RangeValuePattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.RangeValuePattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "RangeValue Pattern";
                var pattern = AutomationElement.Patterns.RangeValue.Pattern;
                oneGroup.FromAutomationProperty("IsReadOnly", pattern.IsReadOnly);
                oneGroup.FromAutomationProperty("SmallChange", pattern.SmallChange);
                oneGroup.FromAutomationProperty("LargeChange", pattern.LargeChange);
                oneGroup.FromAutomationProperty("Minimum", pattern.Minimum);
                oneGroup.FromAutomationProperty("Maximum", pattern.Maximum);
                oneGroup.FromAutomationProperty("Value", pattern.Value);
                list.Add(oneGroup);
            }

            // ScrollPattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.ScrollPattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "Scroll Pattern";
                var pattern = AutomationElement.Patterns.Scroll.Pattern;
                oneGroup.FromAutomationProperty("HorizontalScrollPercent", pattern.HorizontalScrollPercent);
                oneGroup.FromAutomationProperty("HorizontalViewSize", pattern.HorizontalViewSize);
                oneGroup.FromAutomationProperty("HorizontallyScrollable", pattern.HorizontallyScrollable);
                oneGroup.FromAutomationProperty("VerticalScrollPercent", pattern.VerticalScrollPercent);
                oneGroup.FromAutomationProperty("VerticalViewSize", pattern.VerticalViewSize);
                oneGroup.FromAutomationProperty("VerticallyScrollable", pattern.VerticallyScrollable);
                list.Add(oneGroup);
            }

            // SelectionItemPattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.SelectionItemPattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "SelectionItem Pattern";
                var pattern = AutomationElement.Patterns.SelectionItem.Pattern;
                oneGroup.FromAutomationProperty("IsSelected", pattern.IsSelected);
                oneGroup.FromAutomationProperty("SelectionContainer", pattern.SelectionContainer);
                list.Add(oneGroup);
            }

            // SelectionPattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.SelectionPattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "Selection Pattern";
                var pattern = AutomationElement.Patterns.Selection.Pattern;
                oneGroup.FromAutomationProperty("Selection", pattern.Selection);
                oneGroup.FromAutomationProperty("CanSelectMultiple", pattern.CanSelectMultiple);
                oneGroup.FromAutomationProperty("IsSelectionRequired", pattern.IsSelectionRequired);
                list.Add(oneGroup);
            }

            // TableItemPattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.TableItemPattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "TableItem Pattern";
                var pattern = AutomationElement.Patterns.TableItem.Pattern;
                oneGroup.FromAutomationProperty("ColumnHeaderItems", pattern.ColumnHeaderItems);
                oneGroup.FromAutomationProperty("RowHeaderItems", pattern.RowHeaderItems);
                list.Add(oneGroup);
            }

            // TablePattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.TablePattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "Table Pattern";
                var pattern = AutomationElement.Patterns.Table.Pattern;
                oneGroup.FromAutomationProperty("ColumnHeaderItems", pattern.ColumnHeaders);
                oneGroup.FromAutomationProperty("RowHeaderItems", pattern.RowHeaders);
                oneGroup.FromAutomationProperty("RowOrColumnMajor", pattern.RowOrColumnMajor);
                list.Add(oneGroup);
            }

            // TextPattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.TextPattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "Text Pattern";

                var pattern = AutomationElement.Patterns.Text.Pattern;
                var foreColor = GetTextAttribute<int>(pattern, TextAttributes.ForegroundColor, (x) =>
                {
                    return $"{System.Drawing.Color.FromArgb(x)} ({x})";
                });
                var backColor = GetTextAttribute<int>(pattern, TextAttributes.BackgroundColor, (x) =>
                {
                    return $"{System.Drawing.Color.FromArgb(x)} ({x})";
                });
                var fontName = GetTextAttribute<string>(pattern, TextAttributes.FontName, (x) =>
                {
                    return $"{x}";
                });
                var fontSize = GetTextAttribute<double>(pattern, TextAttributes.FontSize, (x) =>
                {
                    return $"{x}";
                });
                var fontWeight = GetTextAttribute<int>(pattern, TextAttributes.FontWeight, (x) =>
                {
                    return $"{x}";
                });

                oneGroup.FromAutomationPropertyString("ForeColor", foreColor);
                oneGroup.FromAutomationPropertyString("BackgroundColor", backColor);
                oneGroup.FromAutomationPropertyString("FontName", fontName);
                oneGroup.FromAutomationPropertyString("FontSize", fontSize);
                oneGroup.FromAutomationPropertyString("FontWeight", fontWeight);
                list.Add(oneGroup);
            }

            // TogglePattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.TogglePattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "Toggle Pattern";
                var pattern = AutomationElement.Patterns.Toggle.Pattern;
                oneGroup.FromAutomationProperty("ToggleState", pattern.ToggleState);
                list.Add(oneGroup);
            }

            // ValuePattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.ValuePattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "Value Pattern";
                var pattern = AutomationElement.Patterns.Value.Pattern;
                oneGroup.FromAutomationProperty("IsReadOnly", pattern.IsReadOnly);
                oneGroup.FromAutomationProperty("Value", pattern.Value);
                list.Add(oneGroup);
            }

            // WindowPattern
            if (IfContainsPattern(allSupportedPatterns, AutomationElement.Automation.PatternLibrary.WindowPattern))
            {
                oneGroup = new DetailElement();
                oneGroup.Title = "Window Pattern";
                var pattern = AutomationElement.Patterns.Window.Pattern;
                oneGroup.FromAutomationProperty("IsModal", pattern.IsModal);
                oneGroup.FromAutomationProperty("IsTopmost", pattern.IsTopmost);
                oneGroup.FromAutomationProperty("CanMinimize", pattern.CanMinimize);
                oneGroup.FromAutomationProperty("CanMaximize", pattern.CanMaximize);
                oneGroup.FromAutomationProperty("WindowVisualState", pattern.WindowVisualState);
                oneGroup.FromAutomationProperty("WindowInteractionState", pattern.WindowInteractionState);
                list.Add(oneGroup);
            }

            return list;
        }

        public DetailElement GetOneGroup(string title)
        {
            foreach (var detailElement in AllItems)
            {
                if (string.Equals(detailElement.Title, title, StringComparison.OrdinalIgnoreCase))
                {
                    return detailElement;
                }
            }

            return null;
        }

        private string GetTextAttribute<T>(FlaUI.Core.Patterns.ITextPattern pattern, FlaUI.Core.Identifiers.TextAttributeId textAttribute, Func<int, string> func)
        {
            var value = pattern.DocumentRange.GetAttributeValue(textAttribute);

            if (value == ((FlaUI.UIA3.UIA3Automation)AutomationElement.Automation).NativeAutomation.ReservedMixedAttributeValue)
            {
                return "Mixed";
            }
            else if (value == AutomationElement.Automation.NotSupportedValue)
            {
                return "Not supported";
            }
            else
            {
                try
                {
                    var converted = (int)value;
                    return func(converted);
                }
                catch
                {
                    return $"Conversion to ${typeof(T)} failed";
                }
            }
        }

        private bool IfContainsPattern(FlaUI.Core.Identifiers.PatternId[] allSupportedPatterns, FlaUI.Core.Identifiers.PatternId pattern)
        {
            foreach (var allSupportedPattern in allSupportedPatterns)
            {
                if (allSupportedPattern.Name == pattern.Name)
                    return true;
            }

            return false;
        }
    }

    public class DetailElement
    {
        public string Title { get; set; }
        public List<OneProperty> Properties { get; set; }

        public void FromAutomationProperty<T>(string _key, IAutomationProperty<T> _value)
        {
            if (Properties == null)
                Properties = new List<OneProperty>();
            Properties.Add(new OneProperty() { Key = _key, Value = _value.ToDisplayText(), Important = true });
        }
        public void FromAutomationPropertyString(string _key, string _value)
        {
            if (Properties == null)
                Properties = new List<OneProperty>();
            Properties.Add(new OneProperty() { Key = _key, Value = _value, Important = true });
        }
    }

    public class OneProperty
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Important { get; set; }
    }

    public static class SearchInDetailElement
    {
        public static string GetClassName(List<DetailElement> detailElements)
        {
            foreach (var detailElement in detailElements)
            {
                foreach (var detailElementProperty in detailElement.Properties)
                {
                    if (String.Equals(detailElementProperty.Key, "ClassName", StringComparison.OrdinalIgnoreCase))
                    {
                        return detailElementProperty.Value;
                    }
                }
            }
            return "";
        }
    }

}
