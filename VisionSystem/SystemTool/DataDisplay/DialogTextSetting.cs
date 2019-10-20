using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem
{
    public partial class DialogTextSetting : Form
    {
        public DialogTextSetting()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogTextLink textLink= new DialogTextLink();
            textLink.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogTextLink textLink = new DialogTextLink();
            textLink.Show();
        }       
    }
}
