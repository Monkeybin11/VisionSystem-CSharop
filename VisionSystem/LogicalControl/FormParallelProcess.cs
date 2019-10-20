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
    public partial class FormParallelProcess : Form
    {
        FormBase frmBase;
        public FormParallelProcess(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }
    }
}
