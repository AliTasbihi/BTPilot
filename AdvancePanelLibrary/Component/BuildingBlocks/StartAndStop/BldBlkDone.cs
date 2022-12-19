using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BuildingBlocks.StartAndStop
{
    public class BldBlkDone : BasicBuildingBlock
    {
        public BldBlkDone()
        {
            Width = GraphicConstant.bluildingBlockWidth;
            AddHeaderLabel();
            AddDoneMessage();
            AddTextFieldAddField();
            AddCollapse();
        }

        private void AddCollapse()
        {
            var btn = new ElmButton(this);
            btn.Title = GraphicConstant.textExpandButton;
            btn.IsCollapseExpandMode = true;
            btn.Padding = new Padding(1, 1, 1, 1);
            Children.Add(btn);
        }

        private void AddTextFieldAddField()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 2, 10, 1);
            lbl.Title = "Text field";
            Children.Add(lbl);


            var btn = new ElmButton(this);
            btn.IsNecessaryToView = 0;
            btn.Title = "+ Add field";
            btn.ElmHasPosition = true;
            btn.ElmLeft = 30;
            btn.ElmTop = 0;

            btn.ElmWidth = 80;
            btn.ElmHeight = 30;
            Children.Add(btn);

            Children.Add(new ElmSeparateLine());
        }


        private void AddDoneMessage()
        {
            var edt = new ElmEditBox(this);
            edt.Padding = new Padding(10, 0, 10, 0);
            edt.TitlePosition = ContentAlignment.TopLeft;
            edt.Title = "Stop message";
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Done";
            lbl.ElmHeight = 0;
            lbl.Padding = new Padding(10, 0, 3, 0);
            lbl.MySize = MyTextSize.Large;
            lbl.BackGround = Color.FromArgb(255, 187, 0);
            lbl.TextColor = Color.White;
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddOneConnector(true, Color.Green, 0);
            Children.Add(lbl);

            lbl.IsHeaderLabel = true;

        }

    }
}