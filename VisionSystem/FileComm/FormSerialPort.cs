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
    public partial class FormSerialPort : Form
    {

        FormBase frmBase;
        public FormSerialPort(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

     
    }
}
