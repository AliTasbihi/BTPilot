using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.BuildingBlocks.DesktopUI
{
    public class BldBlkUpdateUIWindow : BasicBuildingBlock
    {
        public BldBlkUpdateUIWindow()
        {
            Width = GraphicConstant.bluildingBlockWidth;
            AddHeaderLabel();
            AddCloseMethod();
            AddWindow();
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


        private void AddWindow()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Window";
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddOneConnector(true, Color.Blue, 1);
            Children.Add(lbl);

            Children.Add(new ElmSeparateLine());
        }

        private void AddCloseMethod()
        {
            var combo = new ElmComboBox(this);
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Title = "Update method";
            combo.Items.Add("Maximize");
            combo.Items.Add("Minimize");
            combo.Items.Add("-");
            combo.SelectedText = "Maximize";
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Update UI Window";
            lbl.ElmHeight = 0;
            lbl.Padding = new Padding(10, 0, 3, 0);
            lbl.MySize = MyTextSize.Large;
            lbl.BackGround = Color.FromArgb(131, 158, 177);
            lbl.TextColor = Color.White;
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddTwoConnector(Color.Green, 0, 1);
            Children.Add(lbl);

            lbl.IsHeaderLabel = true;
        }

    }
}
