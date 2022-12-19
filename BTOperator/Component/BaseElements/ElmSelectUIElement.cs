using AutoCreateWithJson.Component.BaseStructure;
using AutoCreateWithJson.Component.BuildingBlocks.DesktopUI;
using AutoCreateWithJson.Utility;
using AutoCreateWithJson.Utility.EditDesktopElement;
using AutoCreateWithJson.Utility.EditImageCollection;
using AutoCreateWithJson.Utility.SelectUIElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreateWithJson.Component.BaseElements
{
    public class ElmSelectUIElement : BasicElement
    {
        public string Title { get; set; }
        public bool TitleVisible { get; set; } = true;
        public Color BackGround = Color.FromArgb(242, 242, 242);
        public Color TextColor = Color.Gray;
        public Bitmap ElementPicture ;

        private ContextMenuStrip _contextMenuStrip_UIElement;
        private ContextMenuStrip _contextMenuStrip_Image;

        public enum KindOfSelectElementType
        {
            windowsUIElement,
            imageElement
        }
        public KindOfSelectElementType SelectElementSelectType { get; set; }

        public SelectElementStoreable SelectElementStoreable = new SelectElementStoreable();
        public string SelectElementStorageSerialize
        {
            get
            {
                if (SelectElementStoreable is null)
                    return "";
                return SelectElementStoreable.SerializeToString();
            }
            set
            {
                SelectElementStoreable.DeserializeFromString(value);
                DrawImageToPictureBox();
            }
        }

        public int GetCountSelected
        {
            get
            {
                if (SelectElementSelectType == KindOfSelectElementType.windowsUIElement)
                    return SelectElementStoreable.ElementCount;
                else if (SelectElementSelectType == KindOfSelectElementType.imageElement)
                    return SelectElementStoreable.ImageCount;
                return 0;
            }
        }
        private void DrawImageToPictureBox()
        {
            if (GetCountSelected == 0)
            {
                TitleVisible = true;
            }
            else
            {
                TitleVisible = false;
                ElementPicture=SelectElementStoreable.DrawToPictureBox(BackgroundArea);
            }
        }

        public ElmSelectUIElement(object parent)
        {
            Parent = parent;
            TheClick = SelectUIElement_Click;

            _contextMenuStrip_UIElement = new System.Windows.Forms.ContextMenuStrip();
            var menuItem = _contextMenuStrip_UIElement.Items.Add("Set click point");
            menuItem.Click += new System.EventHandler(setClickPointToolStripMenuItem_Click);
            menuItem = _contextMenuStrip_UIElement.Items.Add("Capture new element");
            menuItem.Click += new System.EventHandler(captureNewElementToolStripMenuItem_Click);
            menuItem = _contextMenuStrip_UIElement.Items.Add("Edit element");
            menuItem.Click += new System.EventHandler(editElementToolStripMenuItem_Click);
            menuItem = _contextMenuStrip_UIElement.Items.Add("Clear element");
            menuItem.Click += new System.EventHandler(clearElementToolStripMenuItem_Click);


            _contextMenuStrip_Image = new System.Windows.Forms.ContextMenuStrip();
            menuItem = _contextMenuStrip_Image.Items.Add("Capture and add image to collection");
            menuItem.Click += new System.EventHandler(captureAndAddImageToCollectionToolStripMenuItem_Click);
            menuItem = _contextMenuStrip_Image.Items.Add("Edit collection");
            menuItem.Click += new System.EventHandler(editCollectionToolStripMenuItem_Click);
            menuItem = _contextMenuStrip_Image.Items.Add("Clear collection");
            menuItem.Click += new System.EventHandler(clearCollectionToolStripMenuItem_Click);

        }

        private void setClickPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new EditImageCollectionForm())
            {
                frm.Init(SelectElementStoreable);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < SelectElementStoreable.ElementCount; i++)
                    {
                        var pt = frm.GetImagePoint(i);
                        SelectElementStoreable.SetPoint(i, pt);
                    }
                }

            }
        }

        private void captureNewElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectNewElementByMouse();
        }

        private void editElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new EditDesktopElementForm())
            {
                frm.InitForm(advancePanel.AllBuildingBlock, SelectElementStoreable, null);
                var resFrom = frm.ShowDialog();
                if (resFrom == DialogResult.OK)
                {
                    SelectElementStoreable = frm.GetResultForm();
                    ElementPicture = SelectElementStoreable.DrawToPictureBox(BackgroundArea);
                }
            }

        }

        private void clearElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectElementStoreable.Clear();
            ElementPicture = null;
            DrawImageToPictureBox();
            advancePanel.Invalidate();
        }

        private void captureAndAddImageToCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void editCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void clearCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        int c = 0;
        private void SelectUIElement_Click(object sender, EventArgs e)
        {
            if (ReadOnly)
            {
                return;
            }
            var p0 = advancePanel.transform.ModelToDisplay(buildingBlock.Left + BackgroundArea.Right, buildingBlock.Top + BackgroundArea.Top);
            var p = advancePanel.PointToScreen(p0);

            if (SelectElementSelectType == KindOfSelectElementType.windowsUIElement)
            {
                if (GetCountSelected == 0)
                {
                    SelectNewElementByMouse();
                }
                else
                {
                    // if have Image then show popup menu
                    _contextMenuStrip_UIElement.Show(p);
                }
            }
            else if (SelectElementSelectType == KindOfSelectElementType.imageElement)
            {
                // Select One Element
                _contextMenuStrip_Image.Show(p);
            }
        }

        private void SelectNewElementByMouse()
        {
            GlobalFunction.AllFormsToMinimized(true);
            using (var frmSelect = new SelectUIElemetForm())
            {
                frmSelect.ShowDialog();
                if (frmSelect.GetResultForm() == true)
                {
                    var elm = frmSelect.GetResultSelectedElement();
                    using (var frm = new EditDesktopElementForm())
                    {
                        //var allBulidingBlockControlsWithUniqueId = BaseFunctions.GetAllBulidingBlockControlsByFindPanelAdvance(this);
                        //frm.InitForm(allBulidingBlockControlsWithUniqueId, SelectElementStoreable, elm);
                        frm.InitForm(advancePanel.AllBuildingBlock, SelectElementStoreable, elm);
                        var resFrom = frm.ShowDialog();
                        if (resFrom == DialogResult.OK)
                        {
                            SelectElementStoreable = frm.GetResultForm();
                            DrawImageToPictureBox();
                        }
                    }
                }
            }
            GlobalFunction.AllFormsToNormal(true);
        }

        public int Draw(Graphics graphics, int y, int borderWidth)
        {
            var measuredText = GraphicFunction.MeasureText(graphics, Title, MyTextSize.Medium);
            var w1 = (ElmWidth == 0) ? Convert.ToInt32(graphics.VisibleClipBounds.Width) : ElmWidth;
            w1 -= 2 * borderWidth;
            var h1 = (ElmHeight == 0) ? Convert.ToInt32(measuredText.Height) : ElmHeight;
            var rec = new Rectangle(borderWidth, y, w1, h1);
            var recWPad = GlobalFunction.AddPadingToRec(rec, Padding, true);

            BackgroundArea = recWPad;

            Brush bg = new SolidBrush(BackGround);
            graphics.FillRectangle(bg, rec);
            GraphicFunction.DrawFillRectangle(graphics, recWPad, Color.White, Color.Black);
            if (TitleVisible)
                GraphicFunction.DrawTextWithAlinment(graphics, Title, MyTextSize.Medium, ContentAlignment.MiddleCenter, recWPad, TextColor);
            else
                graphics.DrawImage(ElementPicture, BackgroundArea.Location);
            if (ReadOnly)
            {
                graphics.DrawLine(GraphicConstant.penDeactive, BackgroundArea.Left, BackgroundArea.Top, BackgroundArea.Right, BackgroundArea.Bottom);
            }
            return y + h1 + 1;
        }
    }
}
