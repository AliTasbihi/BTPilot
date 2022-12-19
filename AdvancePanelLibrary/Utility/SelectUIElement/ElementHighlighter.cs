using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancePanelLibrary.Utility.Log;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Exceptions;
using static AdvancePanelLibrary.Component.BaseElements.BasicBuildingBlock;

namespace AdvancePanelLibrary.Utility.SelectUIElement
{
    public static class ElementHighlighter
    {
        private static AutomationElement lastAutomationElement=null;
        private static DateTime lastDateTime = DateTime.Now;
        public static void HighlightElement(AutomationElement automationElement)
        {
            try
            {
                var dt = DateTime.Now - lastDateTime;
                if (lastAutomationElement is null || !automationElement.Equals(lastAutomationElement) || dt.Seconds>2)
                {
                    Task.Run(() => automationElement.DrawHighlight(false, Color.Coral, TimeSpan.FromSeconds(0.3)));
                    lastAutomationElement = automationElement;
                    lastDateTime=DateTime.Now;
                }
            }
            catch (PropertyNotSupportedException ex)
            {
                MyLog.WritelnBoth("HighlightElementException", ex.Message);
            }
            catch (Exception e)
            {
                MyLog.WritelnBoth("HighlightElementError", e.Message);
            }
        }
        public static void HighlightElement2(AutomationElement automationElement)
        {
            try
            {
                var dt = DateTime.Now - lastDateTime;
                if (lastAutomationElement is null || !automationElement.Equals(lastAutomationElement) || dt.Seconds>2)
                {
                    //automationElement.Automation.OverlayManager.ShowBlocking()
                    Task.Run(() => automationElement.DrawHighlight(false, Color.Coral, TimeSpan.FromSeconds(4)));
                    //automationElement.DrawHighlight(false, Color.Coral, TimeSpan.FromSeconds(4));
                    lastAutomationElement = automationElement;
                    lastDateTime=DateTime.Now;
                }
            }
            catch (PropertyNotSupportedException ex)
            {
                MyLog.WritelnBoth("HighlightElementException", ex.Message);
            }
            catch (Exception e)
            {
                MyLog.WritelnBoth("HighlightElementError", e.Message);
            }
        }
    }
}
