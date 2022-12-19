using AutoCreateWithJson.Component.BaseElements;
using AutoCreateWithJson.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BuildingBlocks.DesktopUI
{
    public class BldBlkDragUIElement : BasicBuildingBlock
    {
        #region Connector Property  
        private object GetSelectDragFromCondition(object sender)
        {
            return GetConnectorPropertySelectElementCondition(sueSelectUIElementDragFrom, sueSelectUIElementDragFrom);
        }
        private object GetSelectDragToCondition(object sender)
        {
            return GetConnectorPropertySelectElementCondition(sueSelectUIElementDragTo, sueSelectUIElementDragTo);
        }
        private object GetFromWindows(object sender)
        {
            var arrow = InputArrowByElementName(lblFromWindows);
            return arrow != null ? arrow.TransferData : null;
        }
        private object GetToWindows(object sender)
        {
            var arrow = InputArrowByElementName(lblToWindows);
            return arrow != null ? arrow.TransferData : null;
        }
        private object GetDefaultTimeout(object sender)
        {
            return GetConnectorPropertyCheckBox(chkDefaultTimeout, chkDefaultTimeout);
        }
        private object GetTimeout(object sender)
        {
            return GetConnectorPropertyEditBox(edTimeout, edTimeout);
        }
        private object GetAwaitNoChanges(object sender)
        {
            return GetConnectorPropertyCheckBox(chkAwaitNoChanges, chkAwaitNoChanges);
        }
        private object GetAwaitTimeout(object sender)
        {
            return 0;
        }
        #endregion
        public BldBlkDragUIElement()
        {
            Width = GraphicConstant.bluildingBlockWidth;

            AddHeaderLabel();
            AddSelectUIElementDragFrom();
            DropDestination();
            AddSelectUIElementDragTo();
            AddNotFound();
            AddFromWindow();
            AddToWindow();
            AddSpeed();
            AddDefaultTimeout();
            AddTimeout();
            AddAwaitNoChanges();
            AddAwaitTimeout();
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

        private void AddAwaitTimeout()
        {
            var edt = new ElmEditBox(this);
            edt.Visible = false;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 2);
            edt.Title = "Await Timeout (sec)";
            edt.TitlePosition = ContentAlignment.MiddleLeft;
            edt.Text = "10";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetAwaitTimeout);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private const string chkAwaitNoChanges = "chkAwaitNoChanges";
        private void AddAwaitNoChanges()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkAwaitNoChanges;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(10, 2, 10, 2);
            chk.Title = "Await no changes";
            chk.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetAwaitNoChanges);
            Children.Add(chk);

            Children.Add(new ElmSeparateLine());
        }

        private const string edTimeout = "edTimeout";
        private void AddTimeout()
        {
            var edt = new ElmEditBox(this);
            edt.Name = edTimeout;
            edt.IsNecessaryToView = 0;
            edt.Padding = new Padding(10, 2, 12, 1);
            edt.Title = "Timeout (sec)";
            edt.TitlePosition = ContentAlignment.MiddleLeft;
            edt.Text = "10";
            edt.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetTimeout);
            Children.Add(edt);

            Children.Add(new ElmSeparateLine());
        }

        private const string chkDefaultTimeout = "chkDefaultTimeout";
        private void AddDefaultTimeout()
        {
            var chk = new ElmCheckBox(this);
            chk.Name = chkDefaultTimeout;
            chk.IsNecessaryToView = 0;
            chk.Padding = new Padding(5, 0, 10, 0);
            chk.Title = "Default timeout";
            chk.AddTwoConnector(Color.Blue, 0, 1, outputDataFunction: GetDefaultTimeout);
            Children.Add(chk);

            Children.Add(new ElmSeparateLine());
        }

        private void AddSpeed()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.MiddleLeft;
            combo.Title = "Speed";
            combo.Items.Add("Fast");
            combo.Items.Add("Medium");
            combo.Items.Add("Slow");
            combo.SelectedText = "Medium";
            Children.Add(combo);

            Children.Add(new ElmSeparateLine());
        }

        private const string lblToWindows = "lblToWindows";
        private void AddToWindow()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblToWindows;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "To Windows";
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetToWindows);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());

        }

        private const string lblFromWindows = "lblFromWindows";
        private void AddFromWindow()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = lblFromWindows;
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "From Windows";
            lbl.Alinment = ContentAlignment.MiddleLeft;
            lbl.AddTwoConnector(Color.Blue, 1, 0, outputDataFunction: GetFromWindows);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private void AddNotFound()
        {
            var lbl = new ElmLabel(this);
            lbl.IsNecessaryToView = 0;
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Not found";
            lbl.Alinment = ContentAlignment.MiddleRight;
            lbl.AddOneConnector(false, Color.Green, 0);
            Children.Add(lbl);
            Children.Add(new ElmSeparateLine());
        }

        private const string sueSelectUIElementDragTo = "sueSelectUIElementDragTo";
        private void AddSelectUIElementDragTo()
        {
            var lbl = new ElmLabel(this);
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "End Element";
            lbl.Alinment = ContentAlignment.MiddleLeft;
            Children.Add(lbl);

            var sue = new ElmSelectUIElement(this);
            sue.Name = sueSelectUIElementDragTo;
            sue.Title = "Select UI Element\r\nto drag to";
            sue.Padding = new Padding(15, 3, 15, 10);
            sue.ElmHeight = 70;
            sue.AddTwoConnector(Color.Blue, 1, 0, -21, outputDataFunction: GetSelectDragToCondition);
            Children.Add(sue);

            Children.Add(new ElmSeparateLine());
        }

        private void DropDestination()
        {
            var combo = new ElmComboBox(this);
            combo.IsNecessaryToView = 0;
            combo.Padding = new Padding(10, 0, 10, 0);
            combo.TitlePosition = ContentAlignment.TopLeft;
            combo.Title = "Drop destination";
            combo.Items.Add("UI element");
            combo.Items.Add("UI element 2");
            combo.SelectedText = "UI element";
            Children.Add(combo);

        }

        private const string sueSelectUIElementDragFrom = "sueSelectUIElementDragFrom";
        private void AddSelectUIElementDragFrom()
        {
            var lbl = new ElmLabel(this);
            lbl.Padding = new Padding(10, 0, 10, 0);
            lbl.Title = "Start Element";
            lbl.Alinment = ContentAlignment.MiddleLeft;
            Children.Add(lbl);

            var sue = new ElmSelectUIElement(this);
            sue.Name = sueSelectUIElementDragFrom;
            sue.Title = "Select UI Element\r\nto drag from";
            sue.Padding = new Padding(15, 3, 15, 10);
            sue.ElmHeight = 70;
            sue.AddTwoConnector(Color.Blue, 1, 0,-21, outputDataFunction: GetSelectDragFromCondition);
            Children.Add(sue);

            Children.Add(new ElmSeparateLine());
        }

        private void AddHeaderLabel()
        {
            var lbl = new ElmLabel(this);
            lbl.Name = "Header";
            lbl.Title = "Drag UI Element";
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
