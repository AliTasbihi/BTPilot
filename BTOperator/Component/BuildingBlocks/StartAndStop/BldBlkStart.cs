using AutoCreateWithJson.Component.BaseElements;
using AutoCreateWithJson.PlayerExecutiton;
using AutoCreateWithJson.Utility;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BuildingBlocks.StartAndStop
{
    public class BldBlkStart : BasicBuildingBlock
    {
        
        public BldBlkStart()
        {
            Width = GraphicConstant.bluildingBlockWidth;
            AddHeaderLabel();
            AddAction();
          
        }

        private void AddAction()
        {
            var combo = new ElmComboBox(this);
            combo.Name = "comboAction";
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Title = "Action";
            combo.Items.Add("No action");
            combo.Items.Add("Close all windows");
            combo.Items.Add("Minimize all windows");
            combo.SelectedText = "No action";
            Children.Add(combo);

            Children.Add(new ElmSpace(8));
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Start";
            lbl.ElmHeight = 0;
            lbl.Padding = new Padding(10, 0, 3, 0);
            lbl.MySize = MyTextSize.Large;
            lbl.BackGround = Color.FromArgb(0, 170, 85);
            lbl.TextColor = Color.White;
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddOneConnector(false, Color.Green, 1);
            Children.Add(lbl);
        }


        ///////////////////////////////
        ///   EXECUTOR 
        ///
        ///
        //////////////////////////////////

        #region EXECUTOR
        public override void SetExecuteInit()
        {
            StatusOfExecution = StatusOfExecutionEnum.None;
        }
        
        public override bool ExecuteBuildingBlock(GlobalVariablePlayer globalVariablePlayer)
        {
            var comboAction = (ElmComboBox)ElementByName("ComboAction");
            if (comboAction.SelectedText== "No action")
            {
                StatusOfExecution = StatusOfExecutionEnum.Finish;
            }
            else if (comboAction.SelectedText== "Close all windows")
            {

            }
            else if (comboAction.SelectedText == "Minimize all windows")
            {
                PlayerFunctions.HideMainForm();
                Keyboard.Press(VirtualKeyShort.LWIN);
                Keyboard.Type(VirtualKeyShort.KEY_D);
                Keyboard.Release(VirtualKeyShort.LWIN);
                Thread.Sleep(1000);
                PlayerFunctions.ShowMainForm();
                StatusOfExecution = StatusOfExecutionEnum.Finish;
            }

            return true;
        }

        public override StatusOfExecutionEnum GetExecuteStatus()
        {
            return StatusOfExecution;
        }
        #endregion


    }
}
