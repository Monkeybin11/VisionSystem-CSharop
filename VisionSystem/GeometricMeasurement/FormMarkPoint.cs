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
    public partial class FormMarkPoint : Form
    {
        FormBase frmBase;
        public FormMarkPoint(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
