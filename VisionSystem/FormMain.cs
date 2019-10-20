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
    public partial class FormMain : DockContent
    {
        FormBase frmBase;
        public PickBox pickBox = new PickBox();

        public FormMain(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;


            //加载主界面
            if(true)
            {
                pictureBox1.Dock = DockStyle.Fill;
            }

        }

        //增加控件时创建
        public void AddWireControl(Control control)
        {
            pickBox.WireControl(control);
        }


        //移除控件选中小矩形光标,移除拖动修改事件
        public void RemoveWire()
        {
            pickBox.Remove();
            foreach (Control c in this.Controls)
            {
                pickBox.ClearEvent(c, "Click");
            }  
        }
    

        //打开编辑界面时创建拖动修改事件
        public void WireControl()
        {
            foreach (Control c in this.Controls)
            {
                pickBox.WireControl(c);
            }
   
        }

    }
}
