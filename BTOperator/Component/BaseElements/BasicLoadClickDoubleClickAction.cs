using AutoCreateWithJson.Utility.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class BasicLoadClickDoubleClickAction
    {
        private List<BasicElement> componnentTheClick = new List<BasicElement>();
        private List<Action<object, MouseEventArgs>> functionTheClick = new List<Action<object, MouseEventArgs>>();

        private List<BasicElement> componnentTheDoubleClick = new List<BasicElement>();
        private List<Action<object, MouseEventArgs>> functionTheDoubleClick = new List<Action<object, MouseEventArgs>>();


        public void AssignOnTheClickAndDoubleClickMethod()
        {
            AssignOnTheClickMethod();
            AssignOnTheDoubleClickMethod();
        }


        public void AddToListOfActionTheClick(BasicElement _componnentTheClick, Action<object, MouseEventArgs> _functionClick)
        {
            componnentTheClick.Add(_componnentTheClick);
            functionTheClick.Add(_functionClick);
        }

        private void AssignOnTheClickMethod()
        {
            for(int i = 0; i < componnentTheClick.Count; i++)
            {
                componnentTheClick[i].TheClick = functionTheClick[i];
            }
        }
 
        public void AddToListOfActionTheDoubleClick(BasicElement _componnentTheDoubleClick, Action<object, MouseEventArgs> _functionDoubleClick)
        {
            componnentTheDoubleClick.Add(_componnentTheDoubleClick);
            functionTheDoubleClick.Add(_functionDoubleClick);
        }

        private void AssignOnTheDoubleClickMethod()
        {
            for(int i = 0; i < componnentTheDoubleClick.Count; i++)
            {
                componnentTheDoubleClick[i].TheClick = functionTheDoubleClick[i];
            }
        }
 
    }
}