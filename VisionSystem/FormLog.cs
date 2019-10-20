using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VisionSystem
{
    public partial class FormLog : DockContent
    {
        FormBase frmBase;
        public Point formLogPosition = new Point();
        public FormLog(FormBase _frmBase )
        {
            InitializeComponent();
            frmBase = _frmBase;
        }

  
        private void FormLog_VisibleChanged(object sender, EventArgs e)
        {
            formLogPosition = this.Location;
            if (this.Visible)
            {
                frmBase.日志ToolStripMenuItem.Checked = true;
            }
            else
            {
                frmBase.日志ToolStripMenuItem.Checked = false;
            }
        }
    }
}
