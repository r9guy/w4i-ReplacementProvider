using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace ReplacementProviderExample
{
    public partial class Config : Form
    {
        private BindingList<string> sheets = new BindingList<string>();
        //private List<Tuple<string, List<string>>> fields;
        private BindingList<string> fields = new BindingList<string>();
        private Microsoft.Office.Interop.Excel.Application ExcelAppInstance = new Microsoft.Office.Interop.Excel.Application();
        private Microsoft.Office.Interop.Excel.Workbook ActiveWorkbook;
        private DialogResult result;
        public Dictionary<string, string> Mapping { get; set; }

        public Config()
        {
            InitializeComponent();
            RefreshSheets();
        }

        ~Config()
        {
            ActiveWorkbook.Close();
            if (ExcelAppInstance.Workbooks.Count == 0)
                ExcelAppInstance.Quit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = textBox1.Text;
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                RefreshSheets();
                
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            result=DialogResult.OK;
            Properties.Settings.Default.Save();
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            result=DialogResult.Cancel;
            Close();
        }

        private void RefreshSheets()
        {
            
            if (ActiveWorkbook != null )
                if (ExcelAppInstance.Workbooks.Count != 0)
                    ActiveWorkbook.Close();
            ActiveWorkbook = ExcelAppInstance.Workbooks.Open(textBox1.Text);
            label4.Text = ActiveWorkbook.Name;
            ExcelAppInstance.Visible = true;
            sheets.Clear();
            foreach (Worksheet worksheet in ActiveWorkbook.Worksheets)
            {
                sheets.Add(worksheet.Name);
                RefreshFields(worksheet);
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshSheets();
        }

        private void Config_Load(object sender, EventArgs e)
        {

            comboBox1.DataSource = fields;
            listBox1.DataSource = sheets;
        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private BindingList<string> RefreshFields(Worksheet sheet)
        {
            BindingList<string> fields = new BindingList<string>(); ;
            try
            {
                Range row = sheet.UsedRange.Rows[1];
                foreach (Range column in row.Columns)
                {
                    fields.Add(column.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fields;
        }

        private void Config_Shown(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectionName = listBox1.SelectedItem.ToString();
                Worksheet selectedSheet = ActiveWorkbook.Worksheets[selectionName];

                fields = RefreshFields(selectedSheet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //listView1.DataSource = null;
            //listView1.DataSource = Mapping;

        }
    }
}
