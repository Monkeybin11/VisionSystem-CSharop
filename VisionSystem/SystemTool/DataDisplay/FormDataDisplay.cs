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
    public partial class FormDataDisplay : Form
    {
        FormBase frmBase;
        public FormDataDisplay(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogTextSetting textSetting = new DialogTextSetting();
            textSetting.Show();
        }
    }
}
