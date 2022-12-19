using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using AutoCreateWithJson.PlayerExecutiton;
using AutoCreateWithJson.Utility.Log;
using AutoCreateWithJson.Utility.SelectUIElement;
using FlaUI.Core.AutomationElements;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace AutoCreateWithJson.Component.BaseStructure
{
    public class ConditionForSelectElement
    {
        public List<LevelContion> LevelContions = new List<LevelContion>();

        private int _currentLevel;
        private Panel _currentPanel;

        public int CurrentLevel
        {
            get
            {
                return _currentLevel;
            }
            set
            {
                if (value < 1 || value > LevelContions.Count)
                {
                    DeActivePanelLevel(_currentLevel - 1);
                    _currentLevel = 0;
                    return;
                }
                DeActivePanelLevel(_currentLevel - 1);
                _currentLevel = value;
                ActivePanelLevel(_currentLevel - 1);
            }
        }
        public ConditionForSelectElement(int levelCount)
        {
            if (levelCount < 1)
                return;
            for (int l = 0; l < levelCount; l++)
            {
                var level = NewLevelCondition();
                level.Title = $"Level {l + 1}";
                var cond = level.NewCondition();
                cond.conditionType = OneCondition.ConditionType.Children;
                cond.childrenType = OneCondition.ChildrenType.Child;
            }

            CurrentLevel = 1;
            ActivePanelLevel(0);
        }

        public string SerializeToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(LevelContions.Count.ToString());
            for (int i = 0; i < LevelContions.Count; i++)
            {
                var level = LevelContions[i];
                sb.AppendLine(level.Title);
                sb.AppendLine(level.conditions.Count.ToString());
                for (int j = 0; j < level.conditions.Count; j++)
                {
                    var cond = level.conditions[j];
                    sb.AppendLine(cond.Title);
                    sb.AppendLine(cond.conditionType.ToString());
                    sb.AppendLine(cond.childrenType.ToString());
                    sb.AppendLine(cond.ChildrenNo.ToString());
                    sb.AppendLine(cond.parentheseType.ToString());
                    sb.AppendLine(cond.andOrType.ToString());
                    sb.AppendLine(cond.oprandType3Field.ToString());
                    sb.AppendLine(cond.category3Field);
                    sb.AppendLine(cond.fieldName3Field);
                    sb.AppendLine(cond.value3Field);
                }
            }
            return sb.ToString();
        }

        public void DeserializeFromString(string value)
        {
            if (value == "")
                return;
            string s = "";
            using (StringReader reader = new StringReader(value))
            {
                LevelContions = new List<LevelContion>();
                int levelCount = Convert.ToInt32(reader.ReadLine());
                for (int i = 0; i < levelCount; i++)
                {
                    var level = NewLevelCondition();
                    level.Title = reader.ReadLine();
                    int conditionCount = Convert.ToInt32(reader.ReadLine());
                    for (int j = 0; j < conditionCount; j++)
                    {
                        var cond = level.NewCondition();
                        cond.Title = reader.ReadLine();
                        Enum.TryParse(reader.ReadLine(), out cond.conditionType);
                        Enum.TryParse(reader.ReadLine(), out cond.childrenType);
                        cond.ChildrenNo = Convert.ToInt32(reader.ReadLine());
                        Enum.TryParse(reader.ReadLine(), out cond.parentheseType);
                        Enum.TryParse(reader.ReadLine(), out cond.andOrType);
                        Enum.TryParse(reader.ReadLine(), out cond.oprandType3Field);
                        cond.category3Field = reader.ReadLine();
                        cond.fieldName3Field = reader.ReadLine();
                        cond.value3Field = reader.ReadLine();
                    }
                }
            }
        }

        private void DeActivePanelLevel(int indexLevel)
        {
            if (indexLevel < 0 || indexLevel >= LevelContions.Count)
                return;
            var lc = LevelContions[indexLevel];
            if (lc.pnlLevel is null)
                return;
            lc.pnlLevel.Text = lc.Title;
            lc.pnlLevel.ForeColor = Color.Black;
            lc.pnlLevel.BackColor = SystemColors.Control;
        }

        private void ActivePanelLevel(int indexLevel)
        {
            if (indexLevel < 0 || indexLevel >= LevelContions.Count)
                return;
            var lc = LevelContions[indexLevel];
            if (lc.pnlLevel is null)
                return;
            lc.pnlLevel.Text = $"<<{lc.Title}>>";
            lc.pnlLevel.ForeColor = Color.Red;
            lc.pnlLevel.BackColor = Color.FromArgb(255, 255, 192);
        }

        public void DrawAllConditionToPanel(Panel panel)
        {
            if (panel is null)
                panel = _currentPanel;
            _currentPanel = panel;
            panel.AutoScroll = true;


            panel.Controls.Clear();
            var y = 5;
            foreach (var levelContion in LevelContions)
            {
                y = DrawLevelToPanel(panel, levelContion, y);
            }

            //panel.Height = y + 10;
            ActivePanelLevel(_currentLevel - 1);
        }

        private int DrawLevelToPanel(Panel panel, LevelContion levelContion, int yStartPanel)
        {
            var y = 25;
            var pnlLevel = new GroupBox();
            levelContion.pnlLevel = pnlLevel;
            pnlLevel.Text = levelContion.Title;
            pnlLevel.Location = new Point(5, yStartPanel);
            pnlLevel.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            pnlLevel.Width = 470;

            foreach (var condition in levelContion.conditions)
            {
                y = condition.DrawOneConditionToPanel(pnlLevel, condition, y);
            }

            pnlLevel.Height = y + 10;
            //pnlLevel.BorderStyle = BorderStyle.FixedSingle;

            panel.Controls.Add(pnlLevel);
            return yStartPanel + pnlLevel.Height + 10;
        }

        public LevelContion NewLevelCondition()
        {
            var lc = new LevelContion();
            LevelContions.Add(lc);
            return lc;
        }

        public LevelContion GetLevelCondition(int index)
        {
            return LevelContions[index];
        }

        public ConditionForSelectElement CloneObject()
        {
            var clone = new ConditionForSelectElement(LevelContions.Count);
            for (int i = 0; i < LevelContions.Count; i++)
            {
                var lvl = LevelContions[i];
                clone.LevelContions[i] = lvl.CloneObject();
            }

            return clone;
        }

        public void Clear()
        {
            for (int i = LevelContions.Count - 1; i >= 0; i--)
            {
                LevelContions[i].Clear();
                LevelContions.RemoveAt(i);
            }
        }
        //write Tasbihi
  
        public AutomationElement[] GetTargetElements(GlobalVariablePlayer globalVariablePlayer, TimeSpan timer)
        {
            try
            {
                double CounterForSeconds = 0;
                do
                {
                    AutomationElement parentElement = globalVariablePlayer.CurrentMainWindow;
                    
                    for (int levelIndex = 0; levelIndex < LevelContions.Count; levelIndex++)
                    {
                        var level = LevelContions[levelIndex];
                        var elems = level.GetElementInChildWithCondition(parentElement);
                        if (elems is null || LevelContions.Count == 0)
                            break;
                        if (elems.Length == 0)
                        {
                            parentElement=globalVariablePlayer.CurrentMainWindow;
                            continue;
                        }
                        if (levelIndex == LevelContions.Count - 1)
                            return elems;
                        parentElement = elems[0];
                    }
                    Thread.Sleep(500);
                    CounterForSeconds+=0.5;
                } while (CounterForSeconds<timer.TotalSeconds);
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

            return null;
        }
        public AutomationElement[] GetTargetElements(Window mainWindow)
        {
            try
            {
                AutomationElement parentElement = mainWindow;
                for (int levelIndex = 0; levelIndex < LevelContions.Count; levelIndex++)
                {
                    var level = LevelContions[levelIndex];
                    var elems = level.GetElementInChildWithCondition(parentElement);
                    if (elems is null || LevelContions.Count == 0)
                        return null;
                    if (levelIndex == LevelContions.Count - 1)
                        return elems;
                    parentElement = elems[0];
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }

        public string GetAllToString()
        {
            var sb = new StringBuilder();
            foreach (var levelContion in LevelContions)
            {
                sb.AppendLine($"Level : {levelContion.Title}");
                foreach (var condition in levelContion.conditions)
                {
                    sb.AppendLine($"  {condition.GetAllToString()}");
                }
                sb.AppendLine("");
            }

            return sb.ToString();
        }

        public class LevelContion
        {
            public List<OneCondition> conditions = new List<OneCondition>();
            public GroupBox pnlLevel;
            public string Title { get; set; }
            public OneCondition NewCondition()
            {
                var oc = new OneCondition();
                conditions.Add(oc);
                return oc;
            }

            public OneCondition GetCondition(int index)
            {
                return conditions[index];
            }

            public bool Find3Fields(string category, string fieldName)
            {
                foreach (var oneCondition in conditions)
                {
                    if (oneCondition.conditionType == OneCondition.ConditionType.Compare3Fields)
                    {
                        if (oneCondition.category3Field == category && oneCondition.fieldName3Field == fieldName)
                            return true;
                    }
                }
                return false;
            }

            public LevelContion CloneObject()
            {
                var clone = new LevelContion();
                clone.Title = Title;
                for (int i = 0; i < conditions.Count; i++)
                {
                    var cond = conditions[i];
                    clone.conditions.Add(cond.CloneObject());
                }

                return clone;
            }

            public void Clear()
            {
                pnlLevel = null;
                for (int i = conditions.Count - 1; i >= 0; i--)
                {
                    conditions[i].Clear();
                    conditions.RemoveAt(i);
                }
            }

            public AutomationElement[] GetElementInChildWithCondition(AutomationElement parentElement)
            {
                if (!GetChildrenTypeAtFirstItem(out var childrenType, out var childNo))
                    return null;
                AutomationElement[] children = null;
                if (childrenType == OneCondition.ChildrenType.Child || childrenType == OneCondition.ChildrenType.ChildNo)
                {
                    var s = parentElement.ControlType;
                    children = parentElement.FindAllChildren();
                }
                else if (childrenType == OneCondition.ChildrenType.Descended || childrenType == OneCondition.ChildrenType.DescendedNo)
                    children = parentElement.FindAllDescendants();

                if (children is null)
                    return null;
                for (int i = 1; i < conditions.Count; i++)
                {
                    children = GetChildrenWithCondtion(children, conditions[i]);
                }

                if (childNo > 0 && children is not null)
                {
                    if (childNo <= children.Length)
                        return new AutomationElement[] { children[childNo - 1] };
                }

                return children;
            }

            private AutomationElement[] GetChildrenWithCondtion(AutomationElement[] children, OneCondition oneCondition)
            {
                var res = new List<AutomationElement>();
                foreach (var element in children)
                {
                    var elementViewModel = new ElementViewModel(element);
                    var allItems = elementViewModel.AllItems;
                    if (ElementIsSuccessCondition(allItems, oneCondition))
                    {
                        res.Add(element);
                    }

                }


                return res.ToArray();
            }

            private bool ElementIsSuccessCondition(List<DetailElement> allItems, OneCondition oneCondition)
            {
                if (oneCondition.conditionType != OneCondition.ConditionType.Compare3Fields)
                    return false;
                foreach (var detailElement in allItems)
                {
                    if (string.Equals(detailElement.Title, oneCondition.category3Field, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var detailElementProperty in detailElement.Properties)
                        {
                            if (string.Equals(detailElementProperty.Key, oneCondition.fieldName3Field,
                                StringComparison.OrdinalIgnoreCase))
                            {
                                switch (oneCondition.oprandType3Field)
                                {
                                    case OneCondition.OprandType.Equals:
                                        if (detailElementProperty.Value.Equals(oneCondition.value3Field, StringComparison.OrdinalIgnoreCase))
                                            return true;
                                        return false;
                                    case OneCondition.OprandType.Contains:
                                        if (detailElementProperty.Value.IndexOf(oneCondition.value3Field, StringComparison.OrdinalIgnoreCase) >= 0)
                                            return true;
                                        return false;
                                    case OneCondition.OprandType.StartsWith:
                                        if (detailElementProperty.Value.StartsWith(oneCondition.value3Field, StringComparison.OrdinalIgnoreCase))
                                            return true;
                                        return false;
                                    case OneCondition.OprandType.EndsWith:
                                        if (detailElementProperty.Value.EndsWith(oneCondition.value3Field, StringComparison.OrdinalIgnoreCase))
                                            return true;
                                        return false;
                                    case OneCondition.OprandType.NotEqual:
                                        if (!detailElementProperty.Value.Equals(oneCondition.value3Field, StringComparison.OrdinalIgnoreCase))
                                            return true;
                                        return false;
                                    case OneCondition.OprandType.NotContain:
                                        if (detailElementProperty.Value.IndexOf(oneCondition.value3Field, StringComparison.OrdinalIgnoreCase) < 0)
                                            return true;
                                        return false;

                                    case OneCondition.OprandType.NotStartWith:
                                        if (!detailElementProperty.Value.StartsWith(oneCondition.value3Field, StringComparison.OrdinalIgnoreCase))
                                            return true;
                                        return false;

                                    default:
                                        return false;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            private bool GetChildrenTypeAtFirstItem(out OneCondition.ChildrenType outChildrenType, out int outChildNo)
            {
                outChildrenType = OneCondition.ChildrenType.Child;
                outChildNo = -1;
                if (conditions.Count == 0)
                    return false;
                if (conditions[0].conditionType != OneCondition.ConditionType.Children)
                    return false;

                outChildrenType = conditions[0].childrenType;
                outChildNo = conditions[0].ChildrenNo;
                return true;
            }
        }

        public class OneCondition
        {
            public string Title { get; set; }
            public ConditionType conditionType;
            public ChildrenType childrenType;
            public int ChildrenNo; // start from 1
            public ParentheseType parentheseType;

            public AndOrType andOrType;

            public string category3Field;
            public string fieldName3Field;
            public OprandType oprandType3Field;
            public string value3Field;

            private ComboBox _comboBoxChildrenType;
            private NumericUpDown _numericUpDownChildrenNo;

            private ComboBox _comboBoxAndOr;

            private ComboBox _comboBoxParentheses;


            private TextBox _textBoxCategory3Field;
            private TextBox _textBoxFieldName3Field;
            private ComboBox _comboBoxOprandType3Field;
            private TextBox _textBoxValue3Field;

            public enum ConditionType
            {
                Children,
                Parentheses,
                AndOr,
                Compare3Fields
            }
            public enum 
                ChildrenType
            {
                Child,
                ChildNo,
                Descended,
                DescendedNo
            }

            public enum ParentheseType
            {
                ParentheseOpen,
                ParentheseClose
            }

            public enum AndOrType
            {
                And,
                Or
            }

            public enum OprandType
            {
                Equals,
                Contains,
                StartsWith,
                EndsWith,
                NotEqual,
                NotContain,
                NotStartWith
            }

            public string GetAllToString()
            {
                var str = "";
                switch (conditionType)
                {
                    case ConditionType.Children:
                        switch (childrenType)
                        {
                            case ChildrenType.Child:
                                str = $"{conditionType} : {childrenType} ";
                                break;
                            case ChildrenType.ChildNo:
                                str = $"{conditionType} : {childrenType} - {ChildrenNo} ";
                                break;
                            case ChildrenType.Descended:
                                str = $"{conditionType} : {childrenType}";
                                break;
                            case ChildrenType.DescendedNo:
                                str = $"{conditionType} : {childrenType} - {ChildrenNo} ";
                                break;
                        }
                        break;
                    case ConditionType.Parentheses:
                        switch (parentheseType)
                        {
                            case ParentheseType.ParentheseOpen:
                                str = $"{conditionType} : (";
                                break;
                            case ParentheseType.ParentheseClose:
                                str = $"{conditionType} : )";
                                break;
                        }
                        break;

                    case ConditionType.AndOr:
                        switch (andOrType)
                        {
                            case AndOrType.And:
                                str = $"{conditionType} : AND";
                                break;
                            case AndOrType.Or:
                                str = $"{conditionType} : OR";
                                break;
                        }
                        break;

                    case ConditionType.Compare3Fields:
                        str = $"{conditionType} :  category<{category3Field}> fieldName<{fieldName3Field}> oprand<{oprandType3Field}> value<{value3Field}>";
                        break;
                }

                var strAddress = Environment.NewLine +
                                 GetAddress(_comboBoxChildrenType) + Environment.NewLine +
                                 GetAddress(_numericUpDownChildrenNo) + Environment.NewLine +
                                 GetAddress(_textBoxCategory3Field) + Environment.NewLine +
                                 GetAddress(_textBoxFieldName3Field) + Environment.NewLine +
                                 GetAddress(_comboBoxOprandType3Field) + Environment.NewLine +
                                 GetAddress(_textBoxValue3Field) + Environment.NewLine;
                return str + strAddress;
            }

            private string GetAddress(Control ctrl)
            {
                if (ctrl is null)
                    return "NULL";
                return String.Format("{0} ({0:X8})", ctrl.Handle.ToInt32());
            }

            // رسم کامپوننت های یک پنل شرطی
            public int DrawOneConditionToPanel(GroupBox panel, OneCondition condition, int yStartComponent)
            {
                var width = 10;
                var height = 10;
                var y = yStartComponent;
                SetAllComponentsOff();
                switch (conditionType)
                {
                    case ConditionType.Children:
                        if (_comboBoxChildrenType is null)
                        {
                            string[] itemList = { "Child", "Child No.", "Descended", "Descended No." };
                            _comboBoxChildrenType = CreateComponentComboBox(5, y, itemList);
                            _comboBoxChildrenType.DropDownStyle = ComboBoxStyle.DropDownList;
                            _comboBoxChildrenType.SelectedIndexChanged += new System.EventHandler(comboChildren_SelectedIndexChanged);
                        }
                        _comboBoxChildrenType.Visible = true;
                        panel.Controls.Add(_comboBoxChildrenType);
                        if (_numericUpDownChildrenNo is null)
                        {
                            _numericUpDownChildrenNo = CreateComponentNumericUpDown(_comboBoxChildrenType.Right + 10, y);
                            _numericUpDownChildrenNo.Minimum = 1;
                            _numericUpDownChildrenNo.Value = 1;
                            _numericUpDownChildrenNo.ValueChanged += new System.EventHandler(numericUpDownChildrenNo_ValueChanged);
                        }
                        _numericUpDownChildrenNo.Visible = true;
                        panel.Controls.Add(_numericUpDownChildrenNo);
                        _comboBoxChildrenType.SelectedIndex = (int)childrenType;

                        comboChildren_SelectedIndexChanged(null, null);
                        numericUpDownChildrenNo_ValueChanged(null, null);

                        height = Math.Max(_numericUpDownChildrenNo.Bottom, _comboBoxChildrenType.Bottom) + 5;
                        width = Math.Max(_numericUpDownChildrenNo.Right, _comboBoxChildrenType.Right) + 10;
                        break;

                    case ConditionType.Parentheses:
                        if (_comboBoxParentheses is null)
                        {
                            string[] itemList = { "(", ")" };
                            _comboBoxParentheses = CreateComponentComboBox(5, y, itemList);
                            _comboBoxParentheses.DropDownStyle = ComboBoxStyle.DropDownList;
                            _comboBoxParentheses.SelectedIndexChanged += new System.EventHandler(comboParentheses_SelectedIndexChanged);
                        }
                        _comboBoxParentheses.Visible = true;
                        _comboBoxParentheses.SelectedIndex = (int)parentheseType;
                        panel.Controls.Add(_comboBoxParentheses);

                        height = Math.Max(_comboBoxParentheses.Bottom, _comboBoxParentheses.Bottom) + 5;
                        width = Math.Max(_comboBoxParentheses.Right, _comboBoxParentheses.Right) + 10;

                        break;

                    case ConditionType.AndOr:
                        if (_comboBoxAndOr is null)
                        {
                            string[] itemList = { "And", "Or" };
                            _comboBoxAndOr = CreateComponentComboBox(5, y, itemList);
                            _comboBoxAndOr.DropDownStyle = ComboBoxStyle.DropDownList;
                            _comboBoxAndOr.SelectedIndexChanged += new System.EventHandler(comboBoxAndOr_SelectedIndexChanged);
                        }
                        _comboBoxAndOr.Visible = true;
                        _comboBoxAndOr.SelectedIndex = (int)andOrType;
                        panel.Controls.Add(_comboBoxAndOr);

                        height = Math.Max(_comboBoxAndOr.Bottom, _comboBoxAndOr.Bottom) + 5;
                        width = Math.Max(_comboBoxAndOr.Right, _comboBoxAndOr.Right) + 10;

                        break;

                    case ConditionType.Compare3Fields:
                        _textBoxCategory3Field ??= CreateComponentTextBox(5, y);
                        _textBoxCategory3Field.ReadOnly = true;
                        _textBoxCategory3Field.Visible = true;
                        _textBoxCategory3Field.Text = category3Field;
                        panel.Controls.Add(_textBoxCategory3Field);

                        _textBoxFieldName3Field ??= CreateComponentTextBox(_textBoxCategory3Field.Right + 10, y);
                        _textBoxFieldName3Field.ReadOnly = true;
                        _textBoxFieldName3Field.Visible = true;
                        _textBoxFieldName3Field.Text = fieldName3Field;
                        panel.Controls.Add(_textBoxFieldName3Field);

                        if (_comboBoxOprandType3Field is null)
                        {
                            string[] itemList = Enum.GetNames(typeof(OprandType));
                            _comboBoxOprandType3Field = CreateComponentComboBox(_textBoxFieldName3Field.Right + 10, y, itemList);
                            _comboBoxOprandType3Field.DropDownStyle = ComboBoxStyle.DropDownList;
                            _comboBoxOprandType3Field.SelectedIndexChanged += new System.EventHandler(oprandType3Field_SelectedIndexChanged);
                        }
                        _comboBoxOprandType3Field.SelectedIndex = (int)oprandType3Field;
                        _comboBoxOprandType3Field.Visible = true;
                        panel.Controls.Add(_comboBoxOprandType3Field);

                        if (_textBoxValue3Field == null)
                        {
                            _textBoxValue3Field = CreateComponentTextBox(_comboBoxOprandType3Field.Right + 10, y);
                            _textBoxValue3Field.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                            _textBoxValue3Field.TextChanged += new System.EventHandler(Value3Field_TextChanged);

                        }
                        _textBoxValue3Field.Visible = true;
                        _textBoxValue3Field.Text = value3Field;
                        panel.Controls.Add(_textBoxValue3Field);

                        height = Math.Max(_textBoxCategory3Field.Bottom, _comboBoxOprandType3Field.Bottom) + 5;
                        width = Math.Max(_textBoxCategory3Field.Right, _textBoxValue3Field.Right) + 10;
                        break;
                }
                panel.Width = Math.Max(panel.Width, width);
                panel.Height = height;

                return height;
            }

            public void New3Fields(string category, string fieldName, OprandType oprandType, string value)
            {
                conditionType = ConditionType.Compare3Fields;
                category3Field = category;
                fieldName3Field = fieldName;
                oprandType3Field = oprandType;
                value3Field = value;
            }
            public void NewParenthese(ParentheseType parentheType)
            {
                conditionType = ConditionType.Parentheses;
                parentheseType = parentheType;
            }

            public void NewAndOrType(AndOrType _andOrType)
            {
                conditionType = ConditionType.AndOr;
                andOrType = _andOrType;
            }

            private void SetAllComponentsOff()
            {
                SetComponentToOff(_comboBoxChildrenType);
                SetComponentToOff(_numericUpDownChildrenNo);
                SetComponentToOff(_textBoxCategory3Field);
                SetComponentToOff(_textBoxFieldName3Field);
                SetComponentToOff(_comboBoxOprandType3Field);
                SetComponentToOff(_textBoxValue3Field);
            }

            private void SetComponentToOff(Control ctrl)
            {
                if (ctrl != null)
                    ctrl.Visible = false;
            }

            private TextBox CreateComponentTextBox(int x, int y)
            {
                var txtBox = new TextBox();
                txtBox.Location = new Point(x, y);
                return txtBox;
            }
            private NumericUpDown CreateComponentNumericUpDown(int x, int y)
            {
                var numericUpDown = new NumericUpDown();
                numericUpDown.Location = new Point(x, y);
                return numericUpDown;
            }

            private ComboBox CreateComponentComboBox(int x, int y, string[] itemList)
            {
                var combo = new ComboBox();
                combo.Location = new Point(x, y);
                combo.Items.AddRange(itemList);
                return combo;
            }

            #region event component
            //ConditionType.Children
            private void comboChildren_SelectedIndexChanged(object sender, EventArgs e)
            {
                switch (_comboBoxChildrenType.SelectedIndex)
                {
                    case 0:// Child
                        childrenType = ChildrenType.Child;
                        _numericUpDownChildrenNo.Visible = false;

                        break;
                    case 1:// Child No.
                        childrenType = ChildrenType.ChildNo;
                        _numericUpDownChildrenNo.Visible = true;
                        break;
                    case 2:// Descended
                        childrenType = ChildrenType.Descended;
                        _numericUpDownChildrenNo.Visible = false;
                        break;
                    case 3:// Descended No.
                        childrenType = ChildrenType.DescendedNo;
                        _numericUpDownChildrenNo.Visible = true;
                        break;
                }
            }
            //ConditionType.Children
            private void numericUpDownChildrenNo_ValueChanged(object sender, EventArgs e)
            {
                ChildrenNo = (int)_numericUpDownChildrenNo.Value - 1;
            }

            //ConditionType.Parentheses
            private void comboParentheses_SelectedIndexChanged(object sender, EventArgs e)
            {
                parentheseType = (ParentheseType)_comboBoxParentheses.SelectedIndex;
            }

            private void comboBoxAndOr_SelectedIndexChanged(object sender, EventArgs e)
            {
                andOrType = (AndOrType)_comboBoxAndOr.SelectedIndex;
            }

            //ConditionType.Compare3Fields
            private void oprandType3Field_SelectedIndexChanged(object sender, EventArgs e)
            {
                oprandType3Field = (OprandType)_comboBoxOprandType3Field.SelectedIndex;
            }
            //ConditionType.Compare3Fields
            private void Value3Field_TextChanged(object sender, EventArgs e)
            {
                value3Field = _textBoxValue3Field.Text;
            }

            public OneCondition CloneObject()
            {
                var clone = new OneCondition();
                clone.Title = Title;
                clone.conditionType = conditionType;
                clone.childrenType = childrenType;
                clone.ChildrenNo = ChildrenNo;
                clone.parentheseType = parentheseType;
                clone.andOrType = andOrType;
                clone.category3Field = category3Field;
                clone.fieldName3Field = fieldName3Field;
                clone.oprandType3Field = oprandType3Field;
                clone.value3Field = value3Field;

                return clone;
            }
            #endregion


            public void Clear()
            {
                Title = "";
                _comboBoxChildrenType = null;
                _numericUpDownChildrenNo = null;
                _comboBoxAndOr = null;
                _comboBoxParentheses = null;
                _textBoxCategory3Field = null;
                _textBoxFieldName3Field = null;
                _comboBoxOprandType3Field = null;
                _textBoxValue3Field = null;
            }
        }

    }

}
