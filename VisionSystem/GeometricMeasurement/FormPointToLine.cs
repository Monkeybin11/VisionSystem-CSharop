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
    public partial class FormPointToLine : Form
    {
        FormBase frmBase;
        public FormPointToLine(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }
    }
}
