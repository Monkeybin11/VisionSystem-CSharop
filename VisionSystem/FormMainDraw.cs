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
    public partial class FormMainDraw   : DockContent
    {
        FormBase frmBase;
        Button btn;
        public TextBox tb;
        public Label lb;
        public Label lb_OK_NG;

        public PictureBox pb;


        public FormMainDraw(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;  
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

      

        private void FormMainDraw_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                frmBase.frmMain.WireControl();
                frmBase.dockPanel1.Controls.Remove(frmBase.frmMain);
                frmBase.frmMain.TopLevel = false;
                frmBase.frmMain.Size = panelInterface.Size;
                panelInterface.Controls.Add(frmBase.frmMain);
                frmBase.frmMain.Show();

                frmBase.frmMain.pictureBox1.Dock = DockStyle.None;
            }
            else
            {
                frmBase.frmMain.RemoveWire();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
            updateInterface();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            updateInterface();
        }

        private void updateInterface()
        {          
          //  frmBase.frmMain.Size = panelInterface.Size;
            panelInterface.Controls.Remove(frmBase.frmMain);
            frmBase.frmMain.TopLevel = true;
            frmBase.frmMain.Hide();
            
            frmBase.frmMain.DockAreas = DockAreas.Document;
            frmBase.frmMain.Show(frmBase.dockPanel1, DockState.Document);          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (pb == null)
            {
                pb = new PictureBox();
                pb.Parent = this;
                pb.Location = new Point(500, 100);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (btn == null)
            {
                btn = new Button();
                btn.Location = new Point(730, 150);
                btn.Size = new Size(75, 35);
                btn.Text = "Run";
                btn.UseVisualStyleBackColor = true;
                frmBase.frmMain.Controls.Add(btn);
                frmBase.frmMain.AddWireControl(btn);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (lb == null)
            {
                lb = new Label();
                lb.Location = new Point(730, 250);
                lb.Size = new Size(75, 35);
                lb.Font = new Font("宋体", 20);
                lb.Text = "OK/NG";
                frmBase.frmMain.Controls.Add(lb);
                frmBase.frmMain.AddWireControl(lb);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tb == null)
            {
                tb = new TextBox();
                tb.Location = new Point(730, 200);
                tb.Size = new Size(75, 35);
                tb.Text = "Text";
                frmBase.frmMain.Controls.Add(tb);
                frmBase.frmMain.AddWireControl(tb);
            }
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (lb_OK_NG == null)
            {
                lb_OK_NG = new Label();
                lb_OK_NG.Location = new Point(730, 300);
                lb_OK_NG.Size = new Size(80, 80);
                lb_OK_NG.Font = new Font("宋体", 60);
                lb_OK_NG.ForeColor = Color.Green;
                lb_OK_NG.Text = "OK";
                frmBase.frmMain.Controls.Add(lb_OK_NG);
                frmBase.frmMain.AddWireControl(lb_OK_NG);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            frmBase.frmMain.pickBox.Remove();
        }

  
    }
}
