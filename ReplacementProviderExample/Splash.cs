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
    public partial class Splash : Form
    {
        public int maximum { set { progressBar1.Maximum = value; } }
        public int progress { set { progressBar1.Value = value; } }
        public Splash()
        {
            InitializeComponent();
        }
    }
}
