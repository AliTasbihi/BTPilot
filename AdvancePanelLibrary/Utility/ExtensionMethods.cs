using FlaUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Utility
{
    public static class ExtensionMethods
    {
        public static bool IsPointInside(this Rectangle rec, Point pt)
        {
            return (pt.X >= rec.Left && pt.X <= rec.Right) &&
                (pt.Y >= rec.Top && pt.Y <= rec.Bottom);
        }
        public static string ToString(this Rectangle rec)
        {
            return $"[{rec.Left},{rec.Top},{rec.Right},{rec.Bottom}]";
        }

        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }

        public static string ToDisplayText<T>(this IAutomationProperty<T> automationProperty)
        {
            try
            {
                var success = automationProperty.TryGetValue(out T value);
                return success ? (value == null ? String.Empty : value.ToString()) : "Not Supported";
            }
            catch (Exception ex)
            {
                return $"Exception getting value ({ex.HResult})";
            }
        }

    }

}
