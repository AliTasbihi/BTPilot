using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component
{
    public static class Helpper
    {
        public static object? MagicallyCreateInstance(string className)
        {
            if (string.IsNullOrEmpty(className))
                return null;
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var type = assembly.GetTypes().First(t => t.Name == className);
                return Activator.CreateInstance(type);
            }
            catch (Exception)
            {
                return null;
            }
           
        }
    }
}
