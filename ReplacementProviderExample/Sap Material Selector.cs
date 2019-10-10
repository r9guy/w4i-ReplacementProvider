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
    public partial class Sap_Material_Selector : Form 
    {
        Config conf=new Config();
        private DialogResult result;

        public Sap_Material_Selector()
        {
            InitializeComponent();
        }

        private void Setup(object sender, EventArgs e)
        {
            conf.Mapping = new Dictionary<string,string>();
            conf.Mapping.Add("material.name", "");
            conf.Mapping.Add("material.code", "");
            if(conf.ShowDialog()==DialogResult.OK)
            {
                ;
            }
        }

        private void yes_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            this.Close();
        }

        private void no_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            this.Close();
        }
        public  new DialogResult  ShowDialog()
        {
            base.ShowDialog();
            return result;
        }
    }
}
