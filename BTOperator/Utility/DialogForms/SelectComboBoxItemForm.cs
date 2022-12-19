using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCreateWithJson.Utility.DialogForms
{
    public partial class SelectComboBoxItemForm : Form
    {
        private bool _formResult = false;

        public SelectComboBoxItemForm()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(combo_ItemsComboBox.Text))
            {
                MessageBox.Show("هیچ آیتمی انتخاب نشده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _formResult = true;
            Close();
        }

        public bool GetResultForm()
        {
            return _formResult;
        }

    }

    public static class SelectItemCombobox
    {
        public static bool Run(string caption, string valueDescription, List<string> itemList, Point clickPoint, ref string defaultValue)
        {
            using (var frmSelect = new SelectComboBoxItemForm())
            {
                frmSelect.Location=new Point(clickPoint.X- frmSelect.combo_ItemsComboBox.Left-20, clickPoint.Y- frmSelect.combo_ItemsComboBox.Top-40);
                frmSelect.Text = caption;
                frmSelect.lbl_DescriptionValue.Text = valueDescription;
                frmSelect.combo_ItemsComboBox.Items.Clear();
                frmSelect.combo_ItemsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (var item in itemList)
                {
                    frmSelect.combo_ItemsComboBox.Items.Add(item);
                }
                frmSelect.combo_ItemsComboBox.Text = defaultValue;

                frmSelect.ShowDialog();
                if (frmSelect.GetResultForm() == true)
                {
                    defaultValue = frmSelect.combo_ItemsComboBox.Text;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
