using System.Collections.Generic;

namespace AdvancePanelLibrary.Component.BaseStructure
{
    public class StrategyForSelectElement
    {
        public StrategyForSelectElement Parent { get; set; }
        public Dictionary<string, string> Condition { get; set; }
        public void AddToCondition(string propName, string propPropValue)
        {
            Condition ??= new Dictionary<string, string>();
            Condition.Add(propName,propPropValue);
        }
    }
}