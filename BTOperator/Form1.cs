using AutoCreateWithJson.Component;
using AutoCreateWithJson.Component.BaseElements;
using AutoCreateWithJson.Component.BuildingBlocks.DesktopUI;
using AutoCreateWithJson.Component.Controller;
using AutoCreateWithJson.PlayerExecutiton;
using AutoCreateWithJson.Utility;
using AutoCreateWithJson.Utility.Log;
using AutoCreateWithJson.Utility.Serialization;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;

namespace AutoCreateWithJson
{
    public partial class Form1 : Form
    {
        PlayerExecutor playerExecutor1;

        public Form1()
        {
            
            InitializeComponent();
            playerExecutor1 = new PlayerExecutor();
            playerExecutor1.ConnectToAdvancePanel(advancePanel1);
            advancePanel1.ConnectToPlayerExecutor(playerExecutor1);
            FillCategoryCombobox();
            MyLog.SetInitByForm(this, textBox1);
            PlayerFunctions.SetMainForm(this);
            textBox1.WordWrap = chk_Wrap.Checked;

            var obj = advancePanel1.AddBuildingBlock("BldBlkStartApplication");
            obj = advancePanel1.AddBuildingBlock("BldBlkReadExcel");
            // obj = advancePanel1.AddBuildingBlock("BldBlkCloseUIWindow");
            //lbl_LoadFromFile_Click(null, null);

            WindowState = FormWindowState.Maximized;
        }

        private void FillCategoryCombobox()
        {
            //tabComponents.TabPages.Clear();
            comboCategory.Items.Clear();
            var compPath = GlobalFunction.GetFilesDirectory("Component\\BuildingBlocks");
            var files = Directory.EnumerateDirectories(compPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                var folderName = Path.GetFileName(file);
                TabPage tp = new TabPage(folderName);
                tabComponents.TabPages.Add(tp);
                comboCategory.Items.Add(folderName);
            }
            comboCategory.SelectedIndex = 1;
        }

        private void AdvancePanel_MouseMove(object sender, MouseEventArgs e)
        {
            var pt = advancePanel1.transform.DisplayToModel(e.Location.X, e.Location.Y);
            var tpe = "-";
            var header = "-";
            if (advancePanel1.AllBuildingBlock.Count > 0)
            {
                var blk = advancePanel1.GetBuildingBlockWithDisplayPoint(new Point(e.Location.X, e.Location.Y));
                if (blk != null)
                {
                    var ty = blk.GetType().ToString();
                    tpe = ty.Split('.').Last();
                    if (blk is BasicBuildingBlock be)
                    {
                        header = be.GetHeader();
                    }
                }


            }


            this.Text = $"x:{e.Location.X} y:{e.Location.Y}  -  ({pt.X},{pt.Y})  - Block: <{tpe}>  Header:  <{header}>";

        }

        Point mouseDownPos = Point.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            var obj = advancePanel1.AddBuildingBlock(comboBuildingBlock.Text);
            if (obj != null)
                advancePanel1.Invalidate();
        }

        private void LoadInitData()
        {
            var path = GlobalFunction.GetFilesDirectory();

            AddToLog(path);
        }

        private void AddToLog(string txt)
        {
            textBox1.Text += txt + System.Environment.NewLine;
        }

        private void btn_Data_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Text = advancePanel1.GetAllScriptPosition(chk_Detail.Checked);
            //textBox1.Text = advancePanel1.GetClickableArea();
        }

        private void lbl_Clear_Click(object sender, EventArgs e)
        {
            advancePanel1.ClearAll();
            FillCategoryCombobox();
            advancePanel1.Invalidate();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
        //todo:##modified tasbihi
        private void Form1_Load(object sender, EventArgs e)
        {


            comboBuildingBlock.SelectedIndex = 0;
           advancePanel1.ClearAll();
            if (Directory.Exists(path)&&Directory.GetFiles(path).Length!=0)
            {
                var fileInfos = Directory.GetFiles(path).Select(q=>new FileInfo(q));
                var orderByTime = fileInfos.OrderBy(q=>q.LastWriteTime);
                var fileLastAccsses = orderByTime.LastOrDefault();
                if (fileLastAccsses!=null)
                {
                    var bytes = File.ReadAllBytes(fileLastAccsses.FullName);
                    var mcs = FromByteArray(bytes) as MyCustomSerialize;
                    MyDeSerializeFactory.CreateInstance(mcs, advancePanel1);
                }
                //foreach (var strFile in Directory.GetFiles(path))
                //{
                //    var lastAccses = stack.Where(q => new FileInfo(q).LastWriteTime < new FileInfo(strFile).LastWriteTime).FirstOrDefault();
                //    if (lastAccses!=string.Empty)
                //    {
                //        stack.Push(strFile);
                //    }
                //}
                //string lastSaveFile = Directory.GetFiles(path).Where(q=>new FileInfo(q).LastAccessTime==lastWriteTime)
                //    .FirstOrDefault();

            }
        }

        private void lbl_ClearBuildingBlock_Click(object sender, EventArgs e)
        {
            advancePanel1.RemoveLastBuildingBlock();
            advancePanel1.Invalidate();
        }

        private void comboCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBuildingBlock.Items.Clear();
            var compPath = GlobalFunction.GetFilesDirectory("Component\\BuildingBlocks\\" + comboCategory.Text);
            var files = Directory.EnumerateFiles(compPath, "*.cs", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {               
                var fileName = Path.GetFileNameWithoutExtension(file);
                comboBuildingBlock.Items.Add(fileName);
            }
            comboBuildingBlock.SelectedIndex = 0;

        }

        private void lbl_ClearAddBuildingBlock_Click(object sender, EventArgs e)
        {
            while (advancePanel1.AllBuildingBlock.Count > 0)
            {
                var o = advancePanel1.AllBuildingBlock.Last();
                advancePanel1.AllBuildingBlock.Remove(o);

            }
            var obj = Helpper.MagicallyCreateInstance(comboBuildingBlock.Text);
            if (obj != null)
            {
                advancePanel1.AddBuildingBlock(obj.GetType());
                advancePanel1.Invalidate();
            }
            if (comboBuildingBlock.SelectedIndex < comboBuildingBlock.Items.Count - 1)
            {
                comboBuildingBlock.SelectedIndex += 1;
            }
            else
            {
                comboBuildingBlock.SelectedIndex = 0;
            }
        }

        private void lbl_AddAllBuildingBlock_Click(object sender, EventArgs e)
        {
            if (comboBuildingBlock.SelectedIndex == comboBuildingBlock.Items.Count - 1)
                comboBuildingBlock.SelectedIndex = 0;

            while (comboBuildingBlock.Items.Count > 0 && comboBuildingBlock.SelectedIndex < comboBuildingBlock.Items.Count)
            {
                var obj = Helpper.MagicallyCreateInstance(comboBuildingBlock.Text);
                if (obj != null)
                {
                    if (obj is BasicBuildingBlock be)
                    {
                        if (be.Width > 0)
                            advancePanel1.AddBuildingBlock(obj.GetType());
                    }
                }

                if (comboBuildingBlock.SelectedIndex < comboBuildingBlock.Items.Count - 1)
                {
                    comboBuildingBlock.SelectedIndex += 1;
                }
                else
                {
                    break;
                }
            }
            advancePanel1.Invalidate();

        }

        private void btn_Move_Click(object sender, EventArgs e)
        {
            if (advancePanel1.DisplayMode != DisplayModeEnum.MoveObject)
                advancePanel1.DisplayMode = DisplayModeEnum.MoveObject;
            else
                advancePanel1.DisplayMode = DisplayModeEnum.None;
        }

        private void btnZoomAll_Click(object sender, EventArgs e)
        {
            advancePanel1.ZoomAll();
        }

        private void btn_ZoomExtent_Click(object sender, EventArgs e)
        {
            advancePanel1.ZoomExtent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            advancePanel1.DisplayMode = DisplayModeEnum.Pan;
        }

        private void btn_ZoomWindow_Click(object sender, EventArgs e)
        {
            advancePanel1.DisplayMode = DisplayModeEnum.ZoomWindow;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            advancePanel1.ZoomActual();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            advancePanel1.DrawClickableArea();
            textBox1.Text = advancePanel1.GetClickableArea();
        }

        private void advancePanel1_MouseMove(object sender, MouseEventArgs e)
        {
            var pt = advancePanel1.transform.DisplayToModel(e.Location.X, e.Location.Y);
            var tpe = "-";
            var header = "-";
            if (advancePanel1.AllBuildingBlock.Count > 0)
            {
                var blk = advancePanel1.GetBuildingBlockWithDisplayPoint(new Point(e.Location.X, e.Location.Y));
                if (blk != null)
                {
                    var ty = blk.GetType().ToString();
                    tpe = ty.Split('.').Last();
                    if (blk is BasicBuildingBlock be)
                    {
                        header = be.GetHeader();
                    }
                }
            }

            this.Text = $"x:{e.Location.X} y:{e.Location.Y}  -  ({pt.X},{pt.Y})  - Block: <{tpe}>  Header:  <{header}>";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            advancePanel1.DisplayMode = DisplayModeEnum.Connector;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var aciveBld = advancePanel1.GetActiveBuildingBlock();
            if (aciveBld == null)
            {
                MessageBox.Show("هیچ بلوکی اکتیو نیست");
                return;
            }
            Graphics g = advancePanel1.CreateGraphics();
            string error = CheckErrorOfChildren(aciveBld.Children, aciveBld, g, advancePanel1);
            textBox1.Text = error;
            return;
        }

        private string CheckErrorOfChildren(List<object> children, BasicBuildingBlock bld, Graphics g, AdvancePanel advancePanel)
        {
            var sb = new StringBuilder();
            foreach (var elm in children)
            {
                if (elm is ElmSeparateLine || elm is ElmSpace)
                    continue;
                var parentTyp = GlobalFunction.GetTypeLastClass((elm as BasicElement).Parent.GetType());
                var parentText = GlobalFunction.GetTitleOfElemet((elm as BasicElement).Parent);
                if (elm is BasicElement basicElement)
                {
                    if (basicElement.Parent == null)
                    {
                        advancePanel1.HighlightElement(bld, basicElement);
                        sb.AppendLine($"Parent is null-{parentTyp}-{parentText}");
                    }
                }
                if (elm is ElmConnector el)
                {
                    if((elm as BasicElement).Parent is ElmLabel elmLabel)
                    {
                        if (elmLabel.IsHeaderLabel)
                            continue;
                    }
                    if (el.InputOutput == InputOutputType.Output)
                    {
                        if (el.OutputDataFunction == null)
                        {
                            //advancePanel1.HighlightElement(aciveBld, connector);
                            sb.AppendLine($"OutputDataFunction is null-{parentTyp}-{parentText}");
                            var r0 = new Rectangle(el.BackgroundArea.Left + bld.Left, el.BackgroundArea.Top + bld.Top, el.BackgroundArea.Width, el.BackgroundArea.Height);
                            var r = advancePanel.transform.ModelToDisplay(r0);
                            GraphicFunction.DrawRectangleWithLine(g, GraphicConstant.penRed2, r);
                        }
                    }
                }
                if (elm is BasicElement be)
                {
                    if (be.Children.Count > 0)
                    {
                        var e = CheckErrorOfChildren(be.Children, bld, g, advancePanel);
                        if (e.Length > 0)
                            sb.AppendLine(e);
                    }

                }
            }
            return sb.ToString();
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
        }

        private void lbl_Example_Click(object sender, EventArgs e)
        {
            var obj = advancePanel1.AddBuildingBlock("BldBlkStart");
            obj = advancePanel1.AddBuildingBlock("BldBlkStartApplication");
            obj = advancePanel1.AddBuildingBlock("BldBlkSetUIElementValue");
            obj = advancePanel1.AddBuildingBlock("BldBlkSetUIElementValue");
            obj = advancePanel1.AddBuildingBlock("BldBlkClickUIElement");
            advancePanel1.Invalidate();
        }

        private void chk_Wrap_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.WordWrap = chk_Wrap.Checked;
        }

        private const string FilePath = @"C:\00\TEMP\Autonation1.wzp";
        private const string FilePath2 = @"C:\00\0.bin";
        private const string FilePath3 = @"C:\00\0.bin";
        private const string newFolder = "BTSave";
        private string path = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            newFolder);
        //todo:##modified tasbihi
        private void lbl_SaveToFile_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = path;
            saveFileDialog.Filter = "BIN Files (*.bin)|*.bin";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = saveFileDialog.FileName;
                MyLog.ClearFile();
                //MyLog.ActiveLog = false;
                //var mcsd = new MyCustomSaveData(advancePanel1);
                var mcs = new MyCustomSerialize(advancePanel1);
                byte[] serialized;
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    // bf.Serialize(ms, mcsd);
                    bf.Serialize(ms, mcs);
                    serialized = ms.ToArray();
                }
                File.WriteAllBytes(filePath, serialized);

            }
        }
        //todo:## modified tasbihi  
        private void lbl_LoadFromFile_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = path;
            fileDialog.Filter = "BIN Files (*.bin)|*.bin";
            if (fileDialog.ShowDialog()==DialogResult.OK)
            {
                var bytes = File.ReadAllBytes(fileDialog.FileName);
                var mcs = FromByteArray(bytes) as MyCustomSerialize;
                MyDeSerializeFactory.CreateInstance(mcs, advancePanel1);
            }
         
            /*
            var jsonReadAllText = File.ReadAllText("c:\\00\\1.json");
            var serializer = new DataContractJsonSerializer(typeof(MyCustomSerialize)); 
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonReadAllText));
            var mcs = (MyCustomSerialize)serializer.ReadObject(ms);
           

            // advancePanel1.ClearAll();

            //textBox1.Text = GetObjects(mcs,0);

            MyDeSerializeFactory.CreateInstance(mcs, advancePanel1);
 */


        }
        static object FromByteArray(byte[] data)
        {
            if (data == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return obj;
            }
        }

        private string GetObjects(MyCustomSerialize customSerialize, int spaceNo)
        {
            var space = "";
            for (int i = 0; i < spaceNo; i++)
                space += " ";
            var sb = new StringBuilder();
            var name = string.IsNullOrEmpty(customSerialize.CtrlName) ? customSerialize.CtrlTypeName : customSerialize.CtrlName;
            sb.AppendLine($"{space}{name}");
            foreach (var custom in customSerialize.Children1)
            {
                sb.AppendLine(GetObjects(custom, spaceNo + 2));
                sb.AppendLine("");
            }

            foreach (var custom in customSerialize.Children2)
            {
                sb.AppendLine(GetObjects(custom, spaceNo + 2));
                sb.AppendLine("");
            }


            return sb.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            advancePanel1.Dispose();

            advancePanel1 = new AdvancePanel();
            advancePanel1.Parent = panel2;
        }

        private void label14_Click(object sender, EventArgs e)
        {
            advancePanel1.ClearAll();

            comboBuildingBlock.SelectedIndex = 0;
            var current = 10;
            var indexTo = 16;

            label14.Text = $"Add ({current}..{indexTo} Building Block";
            while (current < comboBuildingBlock.Items.Count && current <= indexTo)
            {
                comboBuildingBlock.SelectedIndex = current;
                var obj = Helpper.MagicallyCreateInstance(comboBuildingBlock.Text);
                if (obj != null)
                {
                    if (obj is BasicBuildingBlock be)
                    {
                        if (be.Width > 0)
                            advancePanel1.AddBuildingBlock(obj.GetType());
                    }
                }
                current++;
            }
            foreach (BasicBuildingBlock bld in advancePanel1.AllBuildingBlock)
            {
                bld.CollapseExpand(true);
            }
            advancePanel1.Invalidate();
        }
    }
}