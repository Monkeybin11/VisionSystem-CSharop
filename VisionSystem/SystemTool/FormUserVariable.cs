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
    public partial class FormUserVariable : Form
    {
        FormBase frmBase;
        public FormUserVariable(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }
    }
}
