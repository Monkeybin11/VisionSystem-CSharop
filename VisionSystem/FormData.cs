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
    public partial class FormData : DockContent
    {
        FormBase frmBase;
        public Point formDataShowPositon = new Point();

        public FormData(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }

   
        private void FormData_VisibleChanged(object sender, EventArgs e)
        {
            formDataShowPositon = this.Location;
            if (this.Visible)
            {
                frmBase.数据ToolStripMenuItem.Checked = true;
            }
            else
            {
                frmBase.数据ToolStripMenuItem.Checked = false;
            }
        }
    }
}
