using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BaseStructure
{
    public class VariableNameValue
    {
        public List<OneVarNameValue> Variables = new List<OneVarNameValue>();
        public OneVarNameValue Add(string name, object value)
        {
           var v= Variables.SingleOrDefault(n => n.VarName == name);
            if (v != null)
            {
                v.VarValueObject = value;
            }
            else
            {
                v = new OneVarNameValue(name, value);
                Variables.Add(v);
            }
            return v;
        }
        public void Remove(string name)
        {
            var v = Variables.SingleOrDefault(n => n.VarName == name);
            if (v != null)
                Variables.Remove(v);
        }
        public OneVarNameValue Get(string name)
        {
            return Variables.SingleOrDefault(n => n.VarName == name);
        }
    }

    public class OneVarNameValue
    {
        public OneVarNameValue(string name, object value)
        {
            VarName = name;
            VarValueObject = value;
        }

        public string VarName { get; set; }
        public object VarValueObject { get; set; }


    }
}
