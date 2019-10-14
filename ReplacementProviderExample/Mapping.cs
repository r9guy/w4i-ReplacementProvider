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
    public partial class Mapping : Form
    {
        private BindingList<string> sheets = new BindingList<string>();
        private BindingList<string> fields = new BindingList<string>();
        public string activeDocument { get {return textBox1.Text; } set { textBox1.Text = value; } }
        public string newName{get;set; }
        public string newCode { get; set; }

        
        public Mapping()
        {
            InitializeComponent();
            
            updateviews();
        }

        ~Mapping()
        {
            /*try { 
                ActiveWorkbook.Close(SaveChanges:false);
                ExcelAppInstance.Quit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void ChoseExcelFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = activeDocument;
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                activeDocument = openFileDialog1.FileName;
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Properties.Settings.Default.Save();
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        public System.Data.DataTable GetDataTable()
        {
            System.Data.DataTable table = new System.Data.DataTable();
            Microsoft.Office.Interop.Excel.Application ExecApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook ActiveWorkbook = ExecApp.Workbooks.Open(activeDocument, ReadOnly: true);
            Splash progressBar = new Splash();
            progressBar.Show();
            try
            {
                table.Columns.Add("Code", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Group", typeof(string));
                table.Columns.Add("Width", typeof(double));
                table.Columns.Add("Thickness", typeof(double));

                var rowcounter = 0;
                var numrows = ((Worksheet)ActiveWorkbook.Worksheets[1]).UsedRange.Rows.Count;
                progressBar.maximum = numrows;
                foreach (Range excel_row in ((Worksheet)ActiveWorkbook.Worksheets[1]).UsedRange.Rows)
                {
                    progressBar.progress = rowcounter;
                    rowcounter++;
                    //skip few rows header
                    if (rowcounter <= 5)
                        continue;
                    var datarow = table.NewRow();
                    for (int i = 0; i < datarow.ItemArray.Count(); i++)
                    {
                        try
                        {
                            switch (i)
                            {
                                case 0: datarow[i] = excel_row.Columns["A"].Text; break;//Code
                                case 1: datarow[i] = excel_row.Columns["B"].Text; break;//Name
                                case 2: datarow[i] = excel_row.Columns["C"].Text; break;//group
                                case 3: datarow[i] = excel_row.Columns["BS"].Value2; break;//width
                                case 4: datarow[i] = excel_row.Columns["BT"].Value2; break;//thickness
                                default:
                                    //normally shall not come here
                                    break;
                            }
                        }
                        catch
                        {
                            //in case if excel row contains empty cells
                        }
                    }
                    table.Rows.Add(datarow);
                }

                sheets = RefreshSheets(ActiveWorkbook);
                fields = RefreshFields(ActiveWorkbook.Sheets[1]);
            }
            catch
            {

            }
            finally
            {
                progressBar.Close();
                ActiveWorkbook.Close(SaveChanges:false);
                ExecApp.Quit();
            }
            return table;
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            //ActiveWorkbook=ExcelAppInstance.Workbooks.Open(activeDocument);
            //sheets=RefreshSheets(ActiveWorkbook);

            
            updateviews();
        }

        private BindingList<string> RefreshSheets(Workbook wb)
        {
            BindingList<string> sheets = new BindingList<string>();
            try
            {
                foreach (Worksheet worksheet in wb.Worksheets)
                    sheets.Add(worksheet.Name);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sheets;

        }
        private BindingList<string> RefreshFields(Worksheet sheet)
        {
            BindingList<string> fields = new BindingList<string>(); ;
            try
            {
                foreach (Range column in sheet.UsedRange.Columns)
                    fields.Add(column.Rows[1].Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fields;
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectionName = listBox1.SelectedItem.ToString();

                //Worksheet selectedSheet = ActiveWorkbook.Worksheets[selectionName];

                //fields = RefreshFields(selectedSheet);
                updateviews();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void updateviews()
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = fields;
            listBox1.DataSource = null;
            listBox1.DataSource = sheets;
        }
    }
}
