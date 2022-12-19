using AdvancePanelLibrary.Component.BaseElements;
using AdvancePanelLibrary.Component.BaseStructure;
using AdvancePanelLibrary.Component.BuildingBlocks.DesktopUI;
using AdvancePanelLibrary.PlayerExecutiton;
using AdvancePanelLibrary.Utility;
using AdvancePanelLibrary.Utility.Log;
using AdvancePanelLibrary.Utility.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AdvancePanelLibrary.Component.Controller
{
    public class AdvancePanel : Control
    {
        #region Public Variables

        [Category("NoneSave")]
        public LinkedList<object> AllBuildingBlock { get; set; } = new LinkedList<object>();

        [Category("NoneSave")]
        public List<ElmArrowButton> AllArrowButton { get; set; } = new List<ElmArrowButton>();

        public Transform transform = new Transform();
        public PlayerExecutor playerExecutor { get; set; }
        public Action<AdvancePanel> InforAction { get; set; }

        public bool AllowInteractiveWithUI
        {
            get
            {
                return _allowInteractiveWithUI;
            }
            set
            {
                _allowInteractiveWithUI = value;
            }
        }

        DisplayModeEnum _displayMode;
        public DisplayModeEnum DisplayMode
        {
            get
            {
                return _displayMode;
            }
            set
            {
                switch (value)
                {
                    case DisplayModeEnum.ZoomWindow:
                        this.Cursor = defaultCursors.Cross;
                        break;

                    case DisplayModeEnum.MoveObject:
                        this.Cursor = Cursors.SizeAll;
                        break;

                    case DisplayModeEnum.Pan:
                        this.Cursor = defaultCursors.Hand1;
                        break;
                    default:
                        this.Cursor = oldCursor;
                        break;
                }
                _displayMode = value;
            }
        }

        [Category("NoneSave")]
        public VariableNameValue PermanentScopeVariables { get; set; }

        [Category("NoneSave")]
        public VariableNameValue OnlyThisCaseScopeVariables { get; set; }
        #endregion

        #region Private Variables
        private HScrollBar hScrollBar1 = new HScrollBar();
        private VScrollBar vScrollBar1 = new VScrollBar();
        private DefaultCursors defaultCursors = new DefaultCursors();
        private Cursor oldCursor;

        private object? ActiveBuildingBlock { get; set; } = null;
        private Bitmap? BmpBackGround;

        private Pen penDash;

        private object? _lastBuildingBlockSelected = null;
        private object? _lastSelectElement = null;
        private ElmArrowButton? _lastArrowButtonSelected = null;
        private DateTime _lastTimeForMouseDown = DateTime.Now;
        private bool _finishedDisplayMode = true;
        private Bitmap _orginBitmap;
        private Rectangle? _blockBuildingRec;
        private bool _isMouseDown = false;
        private Point _lastMousePosition;
        private int _lastDx;
        private int _lastDy;
        private bool _allowInteractiveWithUI;

        // popup menu
        private ContextMenuStrip _contextMenuStripBuildingBlock;
        private ContextMenuStrip _contextMenuStripArrow;
        #endregion

        public AdvancePanel()
        {
            bool allowInteractiveWithUI = true;
            _allowInteractiveWithUI = allowInteractiveWithUI;
            DoubleBuffered = true;
            oldCursor = this.Cursor;
            DisplayMode = DisplayModeEnum.None;

            Controls.Add(hScrollBar1);
            Controls.Add(vScrollBar1);

            penDash = new Pen(Color.FromArgb(125, Color.Red));
            penDash.Width = 3.0F;
            penDash.DashCap = DashCap.Round;
            penDash.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };

            _contextMenuStripBuildingBlock = new System.Windows.Forms.ContextMenuStrip();
            var menuItem = _contextMenuStripBuildingBlock.Items.Add("Run flow from here");
            menuItem.Click += new System.EventHandler(runFlowFromHereToolStripMenuItem_Click);
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Run flow to here");
            menuItem.Click += new System.EventHandler(runFlowToHereToolStripMenuItem_Click);
            _contextMenuStripBuildingBlock.Items.Add("-");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Add Building Block");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Rename");
            _contextMenuStripBuildingBlock.Items.Add("-");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Cut");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Copy");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Paste");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Delete");
            menuItem.Click += new System.EventHandler(DeleteBuldingBlockToolStripMenuItem_Click);
            _contextMenuStripBuildingBlock.Items.Add("-");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Create Sub-flow");
            _contextMenuStripBuildingBlock.Items.Add("-");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Expand All Building Blocks");
            menuItem.Click += new System.EventHandler(ExpandAllBuildingBlocksToolStripMenuItem_Click);
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Collapse All Building Blocks");
            menuItem.Click += new System.EventHandler(CollapseAllBuildingBlocksToolStripMenuItem_Click);
            _contextMenuStripBuildingBlock.Items.Add("-");
            menuItem = _contextMenuStripBuildingBlock.Items.Add("Edit flow settings");

            _contextMenuStripArrow = new System.Windows.Forms.ContextMenuStrip();
            menuItem = _contextMenuStripArrow.Items.Add("Property");
            menuItem.Click += new System.EventHandler(propertyToolStripMenuItem_Click);
            menuItem = _contextMenuStripArrow.Items.Add("Delete Connection");
            menuItem.Click += new System.EventHandler(deleteConnectionToolStripMenuItem_Click);
        }

        #region Variables and Values
        public void SetPermanentVariables(VariableNameValue permanentVariables)
        {
            PermanentScopeVariables = permanentVariables;
        }
        #endregion

        #region Context Popup menu
        // اجرای فلوچارت از ابتدا تا این نقطه
        private void runFlowToHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_lastBuildingBlockSelected == null)
            {
                MyLog.WritelnBoth("Error", "خطا در اتصال به اجرا کننده");
                MessageBox.Show("خطا در اتصال به اجرا کننده");
                return;
            }

            MyLog.WritelnBoth($"FlowToHere: {GlobalFunction.GetTypeLastClass(_lastBuildingBlockSelected.GetType())}");
            var msg = playerExecutor.ConnectToAdvancePanel(this);
            if (!string.IsNullOrEmpty(msg))
            {
                MyLog.WritelnBoth("Error", msg);
                MessageBox.Show(msg);
                return;
            }
            var res = playerExecutor.ExecuteCommandFromStartToThisBuildingBlock((BasicBuildingBlock)_lastBuildingBlockSelected);
            if (!res.result)
            {
                MyLog.WritelnBoth("Error", res.message);
                MessageBox.Show(res.message);
            }
        }

        // اجرای فلوچارت از همین نقطه به بعد
        private void runFlowFromHereToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_lastBuildingBlockSelected == null)
            {
                MyLog.WritelnBoth("Error", "هیچ بلوکی برای اجرا مشخص نشده است");
                MessageBox.Show("هیچ بلوکی برای اجرا مشخص نشده است");
                return;
            }

            MyLog.WritelnBoth($"FlowFromHere: {GlobalFunction.GetTypeLastClass(_lastBuildingBlockSelected.GetType())}");
            var res = playerExecutor.ExecuteCommandFromThisToEndBuildingBlock((BasicBuildingBlock)_lastBuildingBlockSelected);
            if (!res.result)
            {
                MyLog.WritelnBoth("Error", res.message);
                MessageBox.Show(res.message);
            }
        }

        // پاک کردن بیلدینگ بلاک انتخاب شده
        private void DeleteBuldingBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_lastBuildingBlockSelected == null)
            {
                return;
            }
            RemoveBuildingBlock((BasicBuildingBlock)_lastBuildingBlockSelected);
            _lastBuildingBlockSelected = null;
            Invalidate();
        }


        private void ExpandAllBuildingBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (BasicBuildingBlock bld in AllBuildingBlock)
            {
                bld.CollapseExpand(true);
            }
            Invalidate();
        }

        private void CollapseAllBuildingBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (BasicBuildingBlock bld in AllBuildingBlock)
            {
                bld.CollapseExpand(false);
            }
            Invalidate();
        }

        private void propertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Property Click");
        }

        // پاک کردن کانکشن ارتباطی
        private void deleteConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("آیا می خواهید این کانکشن پاک شود؟", "تاییدیه پاک کردن",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (_lastArrowButtonSelected != null)
                {
                    AllArrowButton.Remove(_lastArrowButtonSelected);
                    Invalidate();
                }
            }

        }
        #endregion

        #region Player Executor
        public void ConnectToPlayerExecutor(PlayerExecutor _playerExecutor)
        {
            playerExecutor = _playerExecutor;
        }
        public void PlayerExecuteAll()
        {
            if (playerExecutor == null)
            {
                playerExecutor = new PlayerExecutor();
                playerExecutor.ConnectToAdvancePanel(this);
                ConnectToPlayerExecutor(playerExecutor);
            }
            playerExecutor.ExecuteCommandFromStartToEnd();
        }
        #endregion

        #region Arrow Button List
        public void DeleteArrows(List<ElmArrowButton> arrows)
        {
            for (int i = AllArrowButton.Count - 1; i >= 0; i--)
            {
                var arrow = AllArrowButton[i];
                foreach (var ar in arrows)
                {
                    if (arrow == ar)
                        AllArrowButton.Remove(arrow);
                }

            }
        }

        // لیست کانکشن هایی که با این کانکتور شروع می شوند
        public List<ElmArrowButton> GetArrowsWithStart(ElmConnector cntr)
        {
            var res = new List<ElmArrowButton>();
            foreach (var arrow in AllArrowButton)
            {
                if (arrow.ConnectorStart == cntr)
                {
                    res.Add(arrow);
                }
            }
            return res;
        }

        // لیست کانکشن هایی که به این کانکتور ختم می شوند
        public List<ElmArrowButton> GetArrowsWithEnd(ElmConnector cntr)
        {
            var res = new List<ElmArrowButton>();
            foreach (var arrow in AllArrowButton)
            {
                if (arrow.ConnectorEnd == cntr)
                {
                    res.Add(arrow);
                }
            }
            return res;
        }

        // لیست تمام کانکشن هایی که به این المان (و تمام زیر المان های آن) مربوط می شوند
        public List<ElmArrowButton> GetAllArrowsElement(BasicElement basicElement)
        {
            var res = new List<ElmArrowButton>();
            foreach (var child in basicElement.Children)
            {
                if (child is ElmConnector cntr)
                {
                    if (cntr.InputOutput == InputOutputType.Input)
                    {
                        res.AddRange(GetArrowsWithEnd(cntr));
                    }
                    else
                    {
                        res.AddRange(GetArrowsWithStart(cntr));
                    }
                }
                else if (child is BasicElement be)
                {
                    res.AddRange(GetAllArrowsElement(be));
                }

            }
            return res;
        }

        // لیست تمام کانکشن هایی که از این بلوک ساختمانی خارج می شوند
        public List<ElmArrowButton> GetAllOutputArrowsBuildingBlock(BasicBuildingBlock basicBuilding)
        {
            var res = new List<ElmArrowButton>();
            foreach (var arrow in AllArrowButton)
            {
                if (arrow.ConnectorStart.IsThisParent(basicBuilding))
                {
                    res.Add(arrow);
                }
            }
            return res;
        }

        // لیست تمام کانکشن هایی که به این بلوک ساختمانی وارد می شوند
        public List<ElmArrowButton> GetAllInputArrowsBuildingBlock(BasicBuildingBlock basicBuilding)
        {
            var res = new List<ElmArrowButton>();
            foreach (var arrow in AllArrowButton)
            {
                if (arrow.ConnectorEnd.IsThisParent(basicBuilding))
                {
                    res.Add(arrow);
                }
            }
            return res;
        }

        public bool IsFindArrowWithConnections(ElmConnector cntr1, ElmConnector cntr2)
        {
            foreach (var arrow in AllArrowButton)
            {
                if ((arrow.ConnectorStart == cntr1 && arrow.ConnectorEnd == cntr2) ||
                    (arrow.ConnectorStart == cntr2 && arrow.ConnectorEnd == cntr1))
                {
                    return true;
                }
            }
            return false;
        }

        public ElmArrowButton AddLinkArrowButtonConnection(ElmConnector? connectorSource, ElmConnector? connectorTarget)
        {
            //  چک می کنیم که آیا اتصال درست هست یا نه؟
            // اتصالات کانکتور شروع و خاتمه لازم هست تغییر کنند یا نه؟؟

            if (connectorSource == null || connectorTarget == null)
                return null;

            if (connectorSource.InputOutput != InputOutputType.Output)
                return null;
            if (connectorTarget.InputOutput != InputOutputType.Input)
                return null;

            if (IsFindArrowWithConnections(connectorSource, connectorTarget))
            {
                MessageBox.Show("بین این دو المان قبلا یک کانکشن برقرار شده است",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;

            }
            var maxSrc = connectorSource.MaxConnection == 0 ? 1000 : connectorSource.MaxConnection;
            var maxDst = connectorTarget.MaxConnection == 0 ? 1000 : connectorTarget.MaxConnection;

            var countSrcConnStart = GetArrowsWithStart(connectorSource).Count;
            var countDstConnEnd = GetArrowsWithEnd(connectorTarget).Count;
            if (countSrcConnStart < maxSrc && countDstConnEnd < maxDst)
            {
                var elmArrowButton = new ElmArrowButton();
                elmArrowButton.ConnectorStart = connectorSource;
                elmArrowButton.ConnectorEnd = connectorTarget;

                AllArrowButton.Add(elmArrowButton);
                return elmArrowButton;
            }
            else
            {
                if (countSrcConnStart >= maxSrc)
                {
                    MessageBox.Show("امکان اضافه کردن لینک جدید ندارید. محدودیت تعداد اتصال در کانکشن شروع می باشد",
                        "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (countDstConnEnd >= maxDst)
                {
                    MessageBox.Show("امکان اضافه کردن لینک جدید ندارید. محدودیت تعداد اتصال در کانکشن انتهایی می باشد",
                        "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return null;
            }
        }

        public ElmArrowButton AddLinkArrowButtonConnectionWithGuid(string guidConnectorSource, string guidConnectorTarget)
        {
            var connectorSource = GetElementFromBuildingBlockByGuid(guidConnectorSource);
            if (connectorSource == null)
                return null;
            var connectorTarget = GetElementFromBuildingBlockByGuid(guidConnectorTarget);
            if (connectorTarget == null)
                return null;

            return AddLinkArrowButtonConnection((ElmConnector)connectorSource, (ElmConnector)connectorTarget);
        }

        private object GetElementFromBuildingBlockByGuid(string guidConnector)
        {
            foreach (BasicBuildingBlock bld in AllBuildingBlock)
            {
                foreach (BasicElement elm in bld.Children)
                {
                    if (elm.UniversalId == guidConnector)
                    {
                        return elm;
                    }
                    if (elm.Children.Count > 0)
                    {
                        var gElm = GetElementByGuid(elm.Children, guidConnector);
                        if (gElm != null)
                        {
                            return gElm;
                        }
                    }
                }
            }
            return null;
        }

        private object GetElementByGuid(List<object> children, string guidConnector)
        {
            foreach (BasicElement elm in children)
            {
                if (elm.UniversalId == guidConnector)
                {
                    return elm;
                }
                if (elm.Children.Count > 0)
                {
                    var gElm = GetElementByGuid(elm.Children, guidConnector);
                    if (gElm != null)
                    {
                        return gElm;
                    }
                }
            }
            return null;
        }

        public void ResetAllDebugIDs()
        {
            var c = 0;
            foreach (var block in AllBuildingBlock)
            {
                if (block is BasicBuildingBlock be)
                {
                    c++;
                    be.DebugID = c;

                }
            }
        }

        #endregion

        #region Modification Building Block
        public BasicBuildingBlock? GetActiveBuildingBlock()
        {
            foreach (var bld in AllBuildingBlock)
            {
                if (bld == ActiveBuildingBlock)
                {
                    return bld as BasicBuildingBlock;
                }
            }
            return null;
        }
        public object GetBuildingBlockWithModelPoint(Point pointModel)
        {
            foreach (var block in AllBuildingBlock)
            {
                if (block is BasicBuildingBlock be)
                {
                    if (GlobalFunction.PointInnerRect(be.Rect, pointModel))
                    {
                        return block;
                    }
                }
            }
            return null;
        }

        public void ClearAll()
        {
            while (AllBuildingBlock.Count > 0)
            {
                var o = AllBuildingBlock.Last();
                AllBuildingBlock.Remove(o);

            }
            while (AllArrowButton.Count > 0)
            {
                var o = AllArrowButton.Last();
                AllArrowButton.Remove(o);
            }
            Invalidate();

        }

        public object GetBuildingBlockWithDisplayPoint(Point pointModelisplay)
        {
            var pointModel = transform.DisplayToModel(pointModelisplay.X, pointModelisplay.Y);
            foreach (var block in AllBuildingBlock)
            {
                if (block is BasicBuildingBlock be)
                {
                    if (GlobalFunction.PointInnerRect(be.Rect, pointModel))
                    {
                        return block;
                    }
                }
            }
            return null;
        }

        private object GetBuildingBlockByModelPosition(Point pointModel)
        {
            foreach (var block in AllBuildingBlock)
            {
                if (block is BasicBuildingBlock be)
                {
                    if (GlobalFunction.PointInnerRect(be.Rect, pointModel))
                    {
                        return block;
                    }
                }
            }
            return null;
        }

        #endregion

        #region  Our OnPaint for All BuildingBlock and All Arrows
        private void DrawAllBuildingBlock(Graphics graphics)
        {
            // رنگ زمینه بوم پنل ادونس رو رنگ یکدست میزنیم
            Brush bg = new SolidBrush(Color.White);
            graphics.FillRectangle(bg, 0, 0, graphics.VisibleClipBounds.Width, graphics.VisibleClipBounds.Height);

            // به ازای تک تک بلوک های ساختمانی بررسی می کنیم که نیاز به رسم شدن دارند یا نه؟
            for (var c = AllBuildingBlock.Count - 1; c >= 0; c--)
            {
                var comp = AllBuildingBlock.ElementAt(c);
                if (comp is BasicBuildingBlock be)
                {
                    bool isActiveBuildingBlock = false;
                    if (ActiveBuildingBlock != null && (be.UniversalId == (ActiveBuildingBlock as BasicBuildingBlock)?.UniversalId))
                        isActiveBuildingBlock = true;

                    if (IsNeedToDraw(be.Rect))
                    {
                        // Goto BasicBuildingBlock
                        // تمام المان های یک بلوک ساختمانی را رسم می کنیم
                        be.DrawAllElementes(graphics, transform, isActiveBuildingBlock);
                    }

                }
            }

            // تمام لینک ها رو رسم می کنیم
            foreach (var link in AllArrowButton)
            {
                var areaStart0 = link.ConnectorStart.GetBoundaryClientToModel();
                if (areaStart0 == null) continue;
                var areaStart = (Rectangle)areaStart0;

                var areaEnd0 = link.ConnectorEnd.GetBoundaryClientToModel();
                if (areaEnd0 == null) continue;
                var areaEnd = (Rectangle)areaEnd0;


                var color = link.ConnectorStart.Color;
                var pt1 = transform.ModelToDisplay(areaStart.Right, (areaStart.Top + areaStart.Bottom) / 2);
                var pt2 = transform.ModelToDisplay(areaEnd.Left, (areaEnd.Top + areaEnd.Bottom) / 2);
                link.screenPointsArrowBezier = GraphicFunction.CalculateBezierPoints(pt1.X, pt1.Y, pt2.X, pt2.Y);
                using (var brush = new SolidBrush(color))
                using (var pen = new Pen(color, GraphicConstant.arrowLinkWidth))
                {
                    graphics.FillPolygon(brush, link.screenPointsArrowBezier.rightArrowPoints);
                    if (link.screenPointsArrowBezier.bezierCount == 1)
                    {
                        graphics.DrawPath(pen, link.screenPointsArrowBezier.gpBezier1);
                        //graphics.DrawBezier(pen, link.pointsArrowBezier.bezierPoints1[0], link.pointsArrowBezier.bezierPoints1[1], link.pointsArrowBezier.bezierPoints1[2], link.pointsArrowBezier.bezierPoints1[3]);
                    }
                    else if (link.screenPointsArrowBezier.bezierCount == 2)
                    {
                        graphics.DrawPath(pen, link.screenPointsArrowBezier.gpBezier1);
                        graphics.DrawPath(pen, link.screenPointsArrowBezier.gpBezier2);
                        //graphics.DrawBezier(pen, link.pointsArrowBezier.bezierPoints1[0], link.pointsArrowBezier.bezierPoints1[1], link.pointsArrowBezier.bezierPoints1[2], link.pointsArrowBezier.bezierPoints1[3]);
                        //graphics.DrawBezier(pen, link.pointsArrowBezier.bezierPoints2[0], link.pointsArrowBezier.bezierPoints2[1], link.pointsArrowBezier.bezierPoints2[2], link.pointsArrowBezier.bezierPoints2[3]);
                    }
                }
            }
        }

        private bool IsNeedToDraw(Rectangle rect)
        {
            var rec = new Rectangle(transform.ModelToDisplay(rect.Left, rect.Top),
                transform.ModelToDisplayValue(rect.Width, rect.Height)
                );

            var rsec = Rectangle.Intersect(rec, transform.DisplayArea);
            return rsec.Width > 0;
        }

        public string GetAllScriptPosition(bool detail)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Display=> Width:{transform.DisplayArea.Width}  Height:{transform.DisplayArea.Height}");
            sb.AppendLine($"ShiftX:{transform.ShiftX}  ShiftY:{transform.ShiftY}");
            sb.AppendLine($"Xmin:{transform.Xmin}  Ymin:{transform.Ymin}");
            sb.AppendLine($"Scale:{transform.Scale:0.##}");
            sb.AppendLine($"Building Block Count:{AllBuildingBlock.Count}");
            var c = 0;
            foreach (var bldblk in AllBuildingBlock)
            {
                c++;
                sb.AppendLine($"{c}-({((BasicBuildingBlock)bldblk).DebugID}) {GlobalFunction.GetTypeLastClass(bldblk.GetType())}");
                if (bldblk is BasicBuildingBlock be)
                {
                    sb.AppendLine(be.GetAllScriptPositionRaw(detail, 2));
                }
            }
            sb.AppendLine("");
            sb.AppendLine($"Arrow Button Count:{AllArrowButton.Count}");
            c = 0;
            foreach (var link in AllArrowButton)
            {
                c++;
                sb.AppendLine($"{c}- {GlobalFunction.GetTypeLastClass(link.GetType())}");
                var recStart = link.ConnectorStart.GetBoundaryClientToModel();
                var recEnd = link.ConnectorEnd.GetBoundaryClientToModel();
                sb.AppendLine($"  start:{recStart.ToString()}");
                sb.AppendLine($"  end:{recEnd.ToString()}");

            }
            return sb.ToString();
        }

        public void HighlightElement(BasicBuildingBlock bld, BasicElement el)
        {
            if (bld != null)
            {
                using (Graphics g = CreateGraphics())
                {
                    g.DrawImage(_orginBitmap, 0, 0);
                    var r0 = new Rectangle(el.BackgroundArea.Left + bld.Left, el.BackgroundArea.Top + bld.Top, el.BackgroundArea.Width, el.BackgroundArea.Height);
                    var r = GlobalFunction.AddPadingToRec(transform.ModelToDisplay(r0), new Padding(6), false);

                    GraphicFunction.DrawRectangleWithLine(g, new Pen(Color.Green, 4), r);

                    r = transform.ModelToDisplay(bld.Rect);
                    GraphicFunction.DrawRectangleWithLine(g, GraphicConstant.penRed2, r);
                }

            }
        }

        public string GetClickableArea()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Display=> Width:{transform.DisplayArea.Width}  Height:{transform.DisplayArea.Height}");
            sb.AppendLine($"ShiftX:{transform.ShiftX}  ShiftY:{transform.ShiftY}");
            sb.AppendLine($"Xmin:{transform.Xmin}  Ymin:{transform.Ymin}");
            sb.AppendLine($"Scale:{transform.Scale:0.##}");
            sb.AppendLine($"Building Block Count:{AllBuildingBlock.Count}");
            var c = 0;
            foreach (var bldblk in AllBuildingBlock)
            {
                c++;
                sb.AppendLine($"{c}- {GlobalFunction.GetTypeLastClass(bldblk.GetType())}");
                if (bldblk is BasicBuildingBlock be)
                {
                    sb.AppendLine(be.GetAllScriptClickableArea(2));

                }
            }

            return sb.ToString();

        }

        public void DrawClickableArea()
        {
            using (Graphics g = CreateGraphics())
            {
                g.DrawImage(_orginBitmap, 0, 0);
                foreach (var bldblk in AllBuildingBlock)
                {
                    if (bldblk is BasicBuildingBlock be)
                    {
                        foreach (var elm in be.ClickableComponents)
                        {
                            if (elm is BasicElement el)
                            {
                                var r0 = new Rectangle(el.BackgroundArea.Left + be.Left, el.BackgroundArea.Top + be.Top, el.BackgroundArea.Width, el.BackgroundArea.Height);
                                var r = transform.ModelToDisplay(r0);

                                GraphicFunction.DrawRectangleWithLine(g, GraphicConstant.penRed1, r);

                            }
                        }

                    }
                }
            }
        }

        private Bitmap? GetBmpBackGround(Rectangle displayArea)
        {
            if (displayArea.Width <= 0 || displayArea.Height <= 0)
                return null;
            Bitmap bmp = new Bitmap(displayArea.Width, displayArea.Height);
            Graphics bmpGfx = Graphics.FromImage(bmp);
            bmpGfx.FillRectangle(Brushes.LightGray, displayArea);

            return bmp;
        }
        #endregion

        #region Events (OnPaint OnMouse... OnResize ...)
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;
            base.OnPaint(e);
            //System.Diagnostics.Debug.WriteLine($"<OnPaint> finishedDisplayMode:{_finishedDisplayMode}  DisplayMode:{DisplayMode}");

            if (!_finishedDisplayMode && DisplayMode == DisplayModeEnum.ZoomWindow)
            {
                if (_lastDx == 0 && _lastDy == 0)
                    return;
                var x1 = Math.Min(_lastMousePosition.X, _lastMousePosition.X + _lastDx);
                var y1 = Math.Min(_lastMousePosition.Y, _lastMousePosition.Y + _lastDy);
                var x2 = Math.Max(_lastMousePosition.X, _lastMousePosition.X + _lastDx);
                var y2 = Math.Max(_lastMousePosition.Y, _lastMousePosition.Y + _lastDy);

                e.Graphics.DrawImage(_orginBitmap, 0, 0);
                e.Graphics.DrawRectangle(penDash, x1, y1, x2 - x1, y2 - y1);
                //System.Diagnostics.Debug.WriteLine($"<OnPaint> ==>DisplayModeEnum.ZoomWindow");

            }
            else if (!_finishedDisplayMode && DisplayMode == DisplayModeEnum.MoveObject)
            {
                if (_lastDx == 0 && _lastDy == 0)
                    return;
                if (_blockBuildingRec == null)
                    return;
                e.Graphics.DrawImage(_orginBitmap, 0, 0);
                GraphicFunction.DrawAlphaFillRectangleWithCrop(e.Graphics, (Rectangle)_blockBuildingRec, _lastMousePosition.X, _lastMousePosition.Y, _lastMousePosition.X + _lastDx, _lastMousePosition.Y + _lastDy, transform.DisplayArea, Color.Blue, 128);
                //System.Diagnostics.Debug.WriteLine($"<OnPaint> ==>DisplayModeEnum.MoveObject");
            }
            else if (!_finishedDisplayMode && DisplayMode == DisplayModeEnum.Connector)
            {
                if (_lastDx == 0 && _lastDy == 0)
                    return;
                var x2 = _lastMousePosition.X + _lastDx;
                var y2 = _lastMousePosition.Y + _lastDy;
                //System.Diagnostics.Debug.WriteLine($"<OnPaint> ==>::{_lastMousePosition.X},{_lastMousePosition.Y}, {x2}, {y2}");
                var arrowSpline = GraphicFunction.CalculateBezierPoints(_lastMousePosition.X, _lastMousePosition.Y, x2, y2);
                e.Graphics.DrawImage(_orginBitmap, 0, 0);
                e.Graphics.FillPolygon(GraphicConstant.brushActive, arrowSpline.rightArrowPoints);
                if (arrowSpline.bezierCount == 1)
                {
                    //System.Diagnostics.Debug.WriteLine($"<OnPaint> ==>({arrowSpline.bezierPoints1[0].X},{arrowSpline.bezierPoints1[0].Y}) " +
                    //    $"({arrowSpline.bezierPoints1[1].X},{arrowSpline.bezierPoints1[1].Y}) " +
                    //    $"({arrowSpline.bezierPoints1[2].X},{arrowSpline.bezierPoints1[2].Y}) " +
                    //    $"({arrowSpline.bezierPoints1[3].X},{arrowSpline.bezierPoints1[3].Y}) "
                    //    );
                    e.Graphics.DrawBezier(GraphicConstant.penActive, arrowSpline.bezierPoints1[0], arrowSpline.bezierPoints1[1], arrowSpline.bezierPoints1[2], arrowSpline.bezierPoints1[3]);
                }
                else if (arrowSpline.bezierCount == 2)
                {
                    e.Graphics.DrawBezier(GraphicConstant.penActive, arrowSpline.bezierPoints1[0], arrowSpline.bezierPoints1[1], arrowSpline.bezierPoints1[2], arrowSpline.bezierPoints1[3]);
                    e.Graphics.DrawBezier(GraphicConstant.penActive, arrowSpline.bezierPoints2[0], arrowSpline.bezierPoints2[1], arrowSpline.bezierPoints2[2], arrowSpline.bezierPoints2[3]);
                }
            }
            else if (!_finishedDisplayMode && DisplayMode == DisplayModeEnum.Pan)
            {
                if (_lastDx == 0 && _lastDy == 0)
                    return;
                e.Graphics.DrawImage(_orginBitmap, 0, 0);
                foreach (var b in AllBuildingBlock)
                {
                    var bld = (BasicBuildingBlock)b;
                    var rec = transform.ModelToDisplay(bld.Rect);
                    GraphicFunction.DrawAlphaFillRectangleWithCrop(e.Graphics, rec, _lastMousePosition.X, _lastMousePosition.Y, _lastMousePosition.X + _lastDx, _lastMousePosition.Y + _lastDy, transform.DisplayArea, Color.YellowGreen, 150);
                }
            }
            else
            {
                Bitmap bmp = new Bitmap(BmpBackGround!);
                Graphics bmpGfx = Graphics.FromImage(bmp);

                DrawAllBuildingBlock(bmpGfx);

                e.Graphics.DrawImage(bmp, transform.DisplayArea.Location);
                var format = bmp.PixelFormat;
                var cloneRect = new RectangleF(0, 0, bmp.Width, bmp.Height);
                _orginBitmap = bmp.Clone(cloneRect, format);
                //System.Diagnostics.Debug.WriteLine($"<OnPaint> ***OnPaint***");
            }
        }

        private Rectangle CalculateNewDisplayClientArea()
        {
            return new Rectangle(0, 0, this.Width - vScrollBar1.Width - 1, this.Height - hScrollBar1.Height - 1);
        }

        protected override void OnResize(EventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;
            base.OnResize(e);

            transform.DisplayArea = CalculateNewDisplayClientArea();
            BmpBackGround = GetBmpBackGround(transform.DisplayArea);

            hScrollBar1.Left = 0;
            hScrollBar1.Top = transform.DisplayArea.Height + 1;
            hScrollBar1.Width = transform.DisplayArea.Width;

            vScrollBar1.Left = transform.DisplayArea.Width + 1;
            vScrollBar1.Top = 0;
            vScrollBar1.Height = transform.DisplayArea.Height;

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;
            //System.Diagnostics.Debug.WriteLine($"OnMouse Down {DateTime.Now}");
            _lastMousePosition = new Point(e.X, e.Y);
            _lastDx = 0;
            _lastDy = 0;
            _finishedDisplayMode = false;
            _lastBuildingBlockSelected = null;
            _lastSelectElement = null;
            _lastArrowButtonSelected = null;
            _blockBuildingRec = null;
            _panStart = false;

            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                var ptModel = transform.DisplayToModel(e.X, e.Y);
                var blockClick = GetBuildingBlockByModelPosition(ptModel);
                if (blockClick is BasicBuildingBlock buildingBlock)
                {
                    _lastBuildingBlockSelected = blockClick;
                    _lastSelectElement = buildingBlock.GetElementByModelPosition(ptModel);
                    _lastTimeForMouseDown = DateTime.Now;
                    _blockBuildingRec = transform.ModelToDisplay(buildingBlock.Rect);
                    if (DisplayMode == DisplayModeEnum.None)
                    {
                        if (_lastSelectElement is ElmConnector elmConnector)
                        {
                            if (elmConnector.InputOutput == InputOutputType.Output)
                            {
                                DisplayMode = DisplayModeEnum.Connector;
                            }
                        }
                        else if (_lastSelectElement == null)
                        {
                            DisplayMode = DisplayModeEnum.MoveObject;
                        }
                        else if (_lastSelectElement is BasicElement elm)
                        {
                            if (elm.TheClick == null)
                            {
                                DisplayMode = DisplayModeEnum.MoveObject;
                            }
                        }
                    }
                }
                else
                {
                    _blockBuildingRec = null;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                _isMouseDown = true;
                DisplayMode = DisplayModeEnum.None;
            }
            else
                _isMouseDown = false;

            base.OnMouseDown(e);
        }

        bool _panStart = false;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;

            var dx = e.X - _lastMousePosition.X;
            var dy = e.Y - _lastMousePosition.Y;

            if (DisplayMode != DisplayModeEnum.Pan && _isMouseDown && e.Button == MouseButtons.Right && Math.Abs(dx) > 3 && Math.Abs(dy) > 3)
                DisplayMode = DisplayModeEnum.Pan;

            var isNeedInvalidate = false;
            if (_isMouseDown)
            {
                if (dx != 0 || dy != 0)
                {
                    switch (DisplayMode)
                    {
                        case DisplayModeEnum.ZoomWindow:
                            _lastDx = dx;
                            _lastDy = dy;
                            isNeedInvalidate = true;
                            break;
                        case DisplayModeEnum.Pan:
                            if (_panStart || (Math.Abs(dx) > 3 && Math.Abs(dy) > 3))
                            {
                                _lastDx = dx;
                                _lastDy = dy;
                                isNeedInvalidate = true;
                                _panStart = true;
                            }
                            break;
                        case DisplayModeEnum.MoveObject:
                            if (_blockBuildingRec != null)
                            {
                                _lastDx = dx;
                                _lastDy = dy;
                                isNeedInvalidate = true;
                            }
                            break;
                        case DisplayModeEnum.Connector:
                            if (_lastSelectElement is ElmConnector elmConnectorSource)
                            {
                                _lastDx = dx;
                                _lastDy = dy;
                                isNeedInvalidate = true;
                            }
                            break;
                    }


                }
            }
            base.OnMouseMove(e);

            //System.Diagnostics.Debug.WriteLine($"<OnMouseMove> finishedDisplayMode:{_finishedDisplayMode}  DisplayMode:{DisplayMode} isNeedInvalidate:{isNeedInvalidate}");

            if (isNeedInvalidate)
                Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;
            _panStart = false;
            var isNeedInvalidate = false;
            switch (DisplayMode)
            {
                case DisplayModeEnum.ZoomWindow:
                    var x1 = Math.Min(_lastMousePosition.X, _lastMousePosition.X + _lastDx);
                    var y1 = Math.Min(_lastMousePosition.Y, _lastMousePosition.Y + _lastDy);
                    var x2 = Math.Max(_lastMousePosition.X, _lastMousePosition.X + _lastDx);
                    var y2 = Math.Max(_lastMousePosition.Y, _lastMousePosition.Y + _lastDy);
                    ZoomWindow(x1, y1, x2, y2);
                    DisplayMode = DisplayModeEnum.None;
                    isNeedInvalidate = true;
                    break;
                case DisplayModeEnum.Pan:
                    transform.ShiftX += _lastDx;
                    transform.ShiftY += _lastDy;
                    isNeedInvalidate = true;
                    DisplayMode = DisplayModeEnum.None;
                    break;
                case DisplayModeEnum.MoveObject:
                    if (_lastBuildingBlockSelected is BasicBuildingBlock buildingBlock)
                    {
                        if (Math.Abs(_lastDx) > 8 || Math.Abs(_lastDy) > 8)
                        {
                            var x = transform.ModelToDisplayX(buildingBlock.Left) + _lastDx;
                            var y = transform.ModelToDisplayY(buildingBlock.Top) + _lastDy;
                            buildingBlock.Left = transform.DisplayToModelX(x);
                            buildingBlock.Top = transform.DisplayToModelY(y);
                        }

                        AllBuildingBlock.Remove(buildingBlock);
                        AllBuildingBlock.AddFirst(buildingBlock);

                        ActiveBuildingBlock = buildingBlock;
                    }
                    DisplayMode = DisplayModeEnum.None;
                    _finishedDisplayMode = true;
                    isNeedInvalidate = true;
                    break;

                case DisplayModeEnum.Connector:
                    if (_lastSelectElement is ElmConnector elmConnectorSource)
                    {
                        if (elmConnectorSource.GetBoundaryClientToModel() != null)
                        {
                            var ptModel = transform.DisplayToModel(e.X, e.Y);
                            var blockClick = GetBuildingBlockByModelPosition(ptModel);
                            if (blockClick is BasicBuildingBlock buildingBlockTarget)
                            {
                                var elm = buildingBlockTarget.GetElementByModelPosition(ptModel);
                                if (elm is ElmConnector elmConnectorTarget)
                                {
                                    if (elmConnectorTarget.GetBoundaryClientToModel() != null)
                                    {
                                        AddLinkArrowButtonConnection(elmConnectorSource, elmConnectorTarget);
                                    }
                                    else
                                        MessageBox.Show("Target Connector: Parent is null");
                                }
                            }
                        }
                        else
                            MessageBox.Show("Source Connector: Parent is null");

                    }
                    _finishedDisplayMode = true;
                    DisplayMode = DisplayModeEnum.None;
                    isNeedInvalidate = true;
                    break;
            }
            _isMouseDown = false;

            if (e.Button == MouseButtons.Right && !isNeedInvalidate)
            {
                var ptModel = transform.DisplayToModel(e.X, e.Y);
                var blockClick = GetBuildingBlockByModelPosition(ptModel);
                if (blockClick is not null)
                {
                    DisplayMode = DisplayModeEnum.None;
                    _lastBuildingBlockSelected = blockClick;
                    _contextMenuStripBuildingBlock.Show(this, new Point(e.X, e.Y));
                }
                else
                {
                    _lastArrowButtonSelected = null;
                    foreach (var arrow in AllArrowButton)
                    {
                        var inside = GlobalFunction.PointInTriangle(new Point(e.X, e.Y),
                           arrow.screenPointsArrowBezier.rightArrowPoints[0],
                           arrow.screenPointsArrowBezier.rightArrowPoints[1],
                           arrow.screenPointsArrowBezier.rightArrowPoints[2]);
                        if (inside)
                        {
                            _lastArrowButtonSelected = arrow;
                            break;
                        }
                        else if (arrow.screenPointsArrowBezier.bezierCount == 1)
                        {
                            if (arrow.screenPointsArrowBezier.gpBezier1.IsOutlineVisible(e.X, e.Y, GraphicConstant.penActive))
                            {
                                _lastArrowButtonSelected = arrow;
                                break;
                            }
                        }
                        else if (arrow.screenPointsArrowBezier.bezierCount == 2)
                        {
                            if (arrow.screenPointsArrowBezier.gpBezier1.IsOutlineVisible(e.X, e.Y, GraphicConstant.penActive))
                            {
                                _lastArrowButtonSelected = arrow;
                                break;
                            }
                            if (arrow.screenPointsArrowBezier.gpBezier2.IsOutlineVisible(e.X, e.Y, GraphicConstant.penActive))
                            {
                                _lastArrowButtonSelected = arrow;
                                break;
                            }
                        }
                    }
                    if (_lastArrowButtonSelected != null)
                    {
                        DisplayMode = DisplayModeEnum.None;
                        _contextMenuStripArrow.Show(this, new Point(e.X, e.Y));
                    }
                }
            }

            base.OnMouseUp(e);
            if (isNeedInvalidate)
                Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;

            //System.Diagnostics.Debug.WriteLine($"OnMouse Click {DateTime.Now}");
            var isNeedInvalidate = false;
            if (e.Button == MouseButtons.Left)
            {
                var ptModel = transform.DisplayToModel(e.X, e.Y);
                var blockClick = GetBuildingBlockByModelPosition(ptModel);
                if (blockClick is BasicBuildingBlock buildingBlock)
                {

                    if (AllBuildingBlock.First() != blockClick)
                    {
                        AllBuildingBlock.Remove(blockClick);
                        AllBuildingBlock.AddFirst(blockClick);
                    }
                    if (ActiveBuildingBlock != blockClick)
                    {
                        ActiveBuildingBlock = blockClick;
                        isNeedInvalidate = true;
                    }
                    var elementClick = buildingBlock.GetElementByModelPosition(ptModel);
                    if (elementClick == null || elementClick != _lastSelectElement)
                        return;
                    TimeSpan span = DateTime.Now - _lastTimeForMouseDown;
                    if (span.TotalSeconds <= 0.5)
                    {
                        if (elementClick is BasicElement basicElement)
                        {
                            if (basicElement.TheClick != null)
                            {
                                basicElement.TheClick(basicElement, e);
                                isNeedInvalidate = true;
                            }
                        }
                    }
                }
            }

            base.OnMouseClick(e);

            if (isNeedInvalidate)
                Invalidate();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;

            //System.Diagnostics.Debug.WriteLine($"OnMouse Double Click {DateTime.Now}");

            if (e.Button == MouseButtons.Left)
            {
                var ptModel = transform.DisplayToModel(e.X, e.Y);
                var blockClick = GetBuildingBlockByModelPosition(ptModel);
                if (blockClick is BasicBuildingBlock buildingBlock)
                {

                    if (AllBuildingBlock.First() != blockClick)
                    {
                        AllBuildingBlock.Remove(blockClick);
                        AllBuildingBlock.AddFirst(blockClick);
                    }
                    if (ActiveBuildingBlock != blockClick)
                    {
                        ActiveBuildingBlock = blockClick;
                        Invalidate();
                    }
                    var elementClick = buildingBlock.GetElementByModelPosition(ptModel);
                    if (elementClick == null || elementClick != _lastSelectElement)
                        return;
                    TimeSpan span = DateTime.Now - _lastTimeForMouseDown;
                    if (span.TotalSeconds > 0.5)
                        return;
                    if (elementClick is BasicElement basicElement)
                    {
                        if (basicElement.TheDoubleClick != null)
                        {
                            basicElement.TheDoubleClick(basicElement, e);
                        }
                    }


                }


            }
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;
            base.OnMouseEnter(e);
            //Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;
            base.OnMouseLeave(e);
            //Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;
            //System.Diagnostics.Debug.WriteLine($"OnClick {DateTime.Now}");
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (!AllowInteractiveWithUI)
                return;
            //System.Diagnostics.Debug.WriteLine($"OnDoubleClick {DateTime.Now}");
            base.OnDoubleClick(e);
        }
        #endregion

        #region Insert Remove Building Block
        private object? AddBuildingBlock(object obj)
        {
            ((BasicBuildingBlock)obj).Parent = this;


            Point pt = GetNewInsertPostion();
            if (obj is BasicBuildingBlock be)
            {
                be.Left = pt.X;
                be.Top = pt.Y;
            }

            AllBuildingBlock.AddFirst(obj);

            ActiveBuildingBlock = obj;
            return obj;
        }

        public object? AddBuildingBlock(string typeStr)
        {
            var obj = Helpper.MagicallyCreateInstance(typeStr);
            if (obj != null)
                return AddBuildingBlock(obj);
            return null;
        }

        public object? AddBuildingBlock(Type type)
        {
            object? obj = Activator.CreateInstance(type);
            if (obj == null)
                return null;
            return AddBuildingBlock(obj);
        }

        public void RemoveLastBuildingBlock()
        {
            if (AllBuildingBlock.Count == 0)
                return;

            var obj = AllBuildingBlock.First();
            RemoveBuildingBlock((BasicBuildingBlock)obj);
            _lastBuildingBlockSelected = null;
            Invalidate();
        }

        // پاک کردن بیلدینگ بلوک با تمام کانکشن های ارتباطی
        public void RemoveBuildingBlock(BasicBuildingBlock buildingBlock)
        {
            var obj = AllBuildingBlock.Find(buildingBlock);
            if (obj == null)
                return;
            // لیست تمام کانکشن های ارتباطی
            var arrows = buildingBlock.GetAllArrowsConnected(null);
            // پاک کردن کانکشن های ارتباطی پیدا شده
            foreach (var arrow in arrows)
                AllArrowButton.Remove(arrow);
            // پاک کردن بیلدینگ بلوک
            AllBuildingBlock.Remove(obj);
            Invalidate();
        }

        private Point GetNewInsertPostion()
        {
            var p = new Point(10, 10);
            foreach (var item in AllBuildingBlock)
            {
                if (item is BasicBuildingBlock be)
                {
                    var x = be.Left + be.Width + 60;
                    if (x > p.X)
                        p.X = x;
                }
            }
            return p;
        }
        #endregion

        #region Zoom
        public void ZoomAll()
        {
            if ((transform.DisplayArea.Width == 0) || (transform.DisplayArea.Height == 0))
                return;
            var wh = CalculateWidthHeightWithDefault();
            transform.Xmin = wh.Left;
            transform.Ymin = wh.Top;
            double sx = transform.DisplayArea.Width / (wh.Width * 1.20);
            double sy = transform.DisplayArea.Height / (wh.Height * 1.20);
            transform.Scale = sx < sy ? sx : sy;

            transform.ShiftX = Convert.ToInt32((transform.DisplayArea.Width - wh.Width * transform.Scale) / 2);
            transform.ShiftY = Convert.ToInt32((transform.DisplayArea.Height - wh.Height * transform.Scale) / 2);

            if (AllBuildingBlock.Count == 0)
            {
                transform.Scale = 1;
                transform.ShiftX = 0;
                transform.ShiftY = 0;
            }
            Invalidate();

        }
        public void ZoomActual()
        {
            if ((transform.DisplayArea.Width == 0) || (transform.DisplayArea.Height == 0))
                return;
            var wh = CalculateWidthHeightWithDefault();
            transform.Xmin = wh.Left;
            transform.Ymin = wh.Top;
            transform.Scale = 1.00;

            transform.ShiftX = 0;
            transform.ShiftY = 0;

            if (AllBuildingBlock.Count == 0)
            {
                transform.Scale = 1;
                transform.ShiftX = 0;
                transform.ShiftY = 0;
            }
            Invalidate();


        }

        public void ZoomExtent()
        {
            if ((transform.DisplayArea.Width == 0) || (transform.DisplayArea.Height == 0))
                return;
            var wh = CalculateWidthHeightWithDefault();
            transform.Xmin = wh.Left;
            transform.Ymin = wh.Top;
            double sx = transform.DisplayArea.Width / (wh.Width * 1.02);
            double sy = transform.DisplayArea.Height / (wh.Height * 1.02);
            transform.Scale = sx < sy ? sx : sy;

            transform.ShiftX = Convert.ToInt32((transform.DisplayArea.Width - wh.Width * transform.Scale) / 2);
            transform.ShiftY = Convert.ToInt32((transform.DisplayArea.Height - wh.Height * transform.Scale) / 2);

            if (AllBuildingBlock.Count == 0)
            {
                transform.Scale = 1;
                transform.ShiftX = 0;
                transform.ShiftY = 0;
            }

            Invalidate();
        }

        public void ZoomWindow(int _x1, int _y1, int _x2, int _y2)
        {
            if (Math.Abs(_x1 - _x2) < 10 || Math.Abs(_y1 - _y2) < 10)
                return;
            if ((transform.DisplayArea.Width == 0) || (transform.DisplayArea.Height == 0))
                return;
            var x1 = Math.Min(_x1, _x2);
            var x2 = Math.Max(_x1, _x2);
            var y1 = Math.Min(_y1, _y2);
            var y2 = Math.Max(_y1, _y2);
            Point pt1 = transform.DisplayToModel(x1, y1);
            Point pt2 = transform.DisplayToModel(x2, y2);
            var wh = new Rectangle(pt1.X, pt1.Y, (pt2.X - pt1.X), (pt2.Y - pt1.Y));
            if ((wh.Width == 0) || (wh.Height == 0))
                return;
            double sx = transform.DisplayArea.Width / (wh.Width * 1.02);
            double sy = transform.DisplayArea.Height / (wh.Height * 1.02);
            var scale = sx < sy ? sx : sy;
            if (scale > 200 || scale < (1 / 200))
                return;
            transform.Scale = scale;
            transform.Xmin = wh.Left;
            transform.Ymin = wh.Top;

            transform.ShiftX = Convert.ToInt32((transform.DisplayArea.Width - wh.Width * transform.Scale) / 2);
            transform.ShiftY = Convert.ToInt32((transform.DisplayArea.Height - wh.Height * transform.Scale) / 2);

            Invalidate();

        }

        public Rectangle CalculateMinMaxWidthHeight()
        {
            var Xmin = Int32.MaxValue / 4;
            var Ymin = Int32.MaxValue / 4;
            var Xmax = -Int32.MaxValue / 4;
            var Ymax = -Int32.MaxValue / 4;
            foreach (var comp in AllBuildingBlock)
            {
                if (comp is BasicBuildingBlock be)
                {
                    if (be.Left < Xmin) Xmin = be.Left;
                    if (be.Top < Ymin) Ymin = be.Top;

                    var right = be.Left + be.Width;
                    var bottom = be.Top + be.Height;
                    if (right > Xmax) Xmax = right;
                    if (bottom > Ymax) Ymax = bottom;
                }
            }
            var width = Xmax - Xmin;
            var height = Ymax - Ymin;

            if (width <= 0 || height <= 0)
                return new Rectangle(0, 0, 0, 0);

            return new Rectangle(Xmin, Ymin, width, height);

        }

        public Rectangle CalculateWidthHeightWithDefault()
        {
            var rec = CalculateMinMaxWidthHeight();
            if ((rec.Width == 0) || (rec.Height == 0))
            {
                return new Rectangle(0, 0, 10, 10);
            }
            return rec;
        }
        #endregion

        #region Save Load
        public void LoadControlFromSerializeFile(string filePath, bool v)
        {
            throw new NotImplementedException();
        }

        public void SerializeAllControlToFile(string filePath)
        {
            /*var sfc = new MyCustomSerializeFormControl(this, false);
            var serialized = ObjectToByteArray(sfc);
            File.WriteAllBytes(filePath, serialized);*/
        }

        static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        #endregion
    }

    public class Transform
    {
        public int ShiftX = 0;
        public int ShiftY = 0;
        public double Scale = 1.0;

        public int Xmin = 0;
        public int Ymin = 0;

        public Rectangle DisplayArea { get; set; }

        public int ModelToDisplayX(int x)
        {
            return Convert.ToInt32((x - Xmin) * Scale + ShiftX);
        }
        public int ModelToDisplayY(int y)
        {
            return Convert.ToInt32((y - Ymin) * Scale + ShiftY);
        }
        public int ModelToDisplayValue(int v)
        {
            return Convert.ToInt32(v * Scale);
        }
        public Size ModelToDisplayValue(int v1, int v2)
        {
            return new Size(Convert.ToInt32(v1 * Scale), Convert.ToInt32(v2 * Scale));
        }
        public Point ModelToDisplay(int x, int y)
        {
            return new Point(ModelToDisplayX(x), ModelToDisplayY(y));
        }
        public Rectangle ModelToDisplay(Rectangle rect)
        {
            var p1 = ModelToDisplay(rect.Left, rect.Top);
            var p2 = ModelToDisplay(rect.Right, rect.Bottom);
            return new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
        }


        public int DisplayToModelX(int x)
        {
            return Convert.ToInt32((x - ShiftX) / Scale + Xmin);
        }
        public int DisplayToModelY(int y)
        {
            return Convert.ToInt32((y - ShiftY) / Scale + Ymin);
        }

        public Point DisplayToModel(int x, int y)
        {
            return new Point(DisplayToModelX(x), DisplayToModelY(y));
        }

    }

    public enum DisplayModeEnum
    {
        None,
        ZoomWindow,
        ZoomIn,
        ZoomOut,
        Pan,
        MoveObject,
        Connector

    }

}
