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
    public partial class FormTool : DockContent
    {
        public Point FormToolPosition=new Point();
        public TreeNode _selectednode;
        FormBase frmBase;
        
        public FormTool(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }

        /// <summary>
        /// 添加MouseDown是因为,MouseDown先执行AfterSelect，所以在没有AfterSelect时，MouseDown要先选中被选取项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    _selectednode = treeView1.GetNodeAt(e.X, e.Y);
                    treeView1.SelectedNode = _selectednode;
                }
            }
            catch { }
        }

        private void FillListView(TreeNodeCollection nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                ListViewItem lvwItem = new ListViewItem(nodes[i].Text, nodes[i].ImageIndex);
                lvwItem.Tag = nodes[i].Tag;
                frmBase.frmProcessManage.listView1.Items.Add(lvwItem);
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                _selectednode = e.Node;
            }
            catch { }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.DoDragDrop(_selectednode, DragDropEffects.Move);
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

  

        private void FormTool_VisibleChanged(object sender, EventArgs e)
        {

            FormToolPosition = this.Location;
            if(this.Visible)
            {
                frmBase.工具箱ToolStripMenuItem.Checked = true;

            }
            else
            {
                frmBase.工具箱ToolStripMenuItem.Checked = false;

            }
        }
    }
}
