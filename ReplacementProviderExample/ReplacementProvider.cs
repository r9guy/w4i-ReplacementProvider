using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReplacementProviderExample
{
    public partial class ReplacementProvider : Form
    {
        private DataTable table;
        private DataTable Table {
            get { return table; }
            set {
                table = value;
                //dataGridView1.DataSource = null;
                //dataGridView1.DataSource = Table;
            }
        }
        public string oldCode { get { return textBox4.Text; } set { textBox4.Text = value; } }
        public string oldWidth { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string oldThickness { get { return textBox3.Text; } set { textBox3.Text = value; } }
        public string newCode { get { return textBox6.Text; } set { textBox6.Text = value; } }
        public string newName { get { return textBox5.Text; } set { textBox5.Text = value; } }
        public ReplacementProvider()
        {
            InitializeComponent();
            try
            {
                Mapping DataProvider = new Mapping();
                Table = DataProvider.GetDataTable();
                dataGridView1.DataSource = Table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Setup(object sender, EventArgs e)
        {
            Mapping conf = new Mapping();
            if (conf.ShowDialog() == DialogResult.OK)
            {
                ;
            }
        }

        private void yes_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void no_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
        public bool FindReplacementMaterial(string materialCode, double materialWidth, double materialThickness)
        {
            bool returnvalue = false;
            try
            {
                materialWidth = materialWidth * 10;
                materialThickness = materialThickness * 10;

                //compose the range which contains only matching material name
                var rowcounter = 0;
                int occurencerow = 0;
                double previousThickness = double.MaxValue;
                double previousWidth = double.MaxValue;
                foreach (DataRow row in Table.Rows)
                {
                    string group = (string)row.ItemArray[2];
                    double dataThickness;
                    double dataWidth;

                    if (group.ToUpper() == materialCode.ToUpper())
                    {
                        //materials with measurment unit SQM are defined by only thickness
                        if (materialWidth == 0)
                        {
                            try
                            {
                                dataThickness = Math.Round((double)row.ItemArray[4]);
                            }
                            catch
                            {
                                continue;
                            }
                            if (dataThickness >= materialThickness)
                                if (dataThickness < previousThickness)
                                {
                                    previousThickness = dataThickness;
                                    newCode = row.ItemArray[0].ToString();
                                    newName = row.ItemArray[1].ToString();
                                    occurencerow = rowcounter;
                                    returnvalue = true;
                                }
                        }

                        //materials with measurment unit LM are defined by 2 parameters thickness and width
                        else
                        {
                            try
                            {
                                dataThickness = Math.Round((double)row.ItemArray[4]);
                                dataWidth = Math.Round((double)row.ItemArray[3]);
                            }
                            catch
                            {
                                continue;
                            }
                            if (dataWidth >= materialWidth)
                                if (dataWidth < previousWidth)
                                {
                                    if (dataThickness >= materialThickness + decimal.ToDouble(numericUpDown1.Value))
                                        if (dataThickness < previousThickness)
                                        {
                                            previousWidth = dataWidth;
                                            previousThickness = dataThickness;
                                            newCode = row.ItemArray[0].ToString();
                                            newName = row.ItemArray[1].ToString();
                                            occurencerow = rowcounter;
                                            returnvalue = true;
                                        }

                                }
                        }
                    }


                    rowcounter++;
                }
                if (returnvalue)
                {
                    dataGridView1.Rows[occurencerow].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = occurencerow;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return returnvalue;

        }

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FindReplacementMaterial(textBox4.Text, Convert.ToDouble(textBox2.Text) / 10, Convert.ToDouble(textBox3.Text) / 10))
                    MessageBox.Show("Replacement not found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\nhave you forgot to input any of the fields?");
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            var selectedrow = dataGridView1.CurrentRow.Index;
            if (selectedrow >= 0)
            {
                var row = dataGridView1.CurrentRow;
                newName = (string)row.Cells[0].Value;
                newCode = (string)row.Cells[1].Value;

            }
        }

        private void reload_Click(object sender, EventArgs e)
        {
            Mapping DataProvider = new Mapping();
            Table = DataProvider.GetDataTable();
        }
    }
}
