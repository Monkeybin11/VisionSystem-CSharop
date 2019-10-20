using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
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
    public partial class FormPreProcessing : Form
    {
        FormBase frmBase;

        string selectInputImage = "";
        Mat imageShow;
        int blurSize = 3;
        int gaussianBlurSize = 3;
        int boxFilterSize = 3;
        int medianBlurSize = 3;
        int bilateralFilterSize = 3;

        int threshold = 120;
        int maxValue = 255;


        ROISetUp roiSetUp = null;
        public  Rectangle roi = new Rectangle();


        public FormPreProcessing(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;         
            //隐藏 tabControl头部
            this.tabControlArea.Region = new Region(new RectangleF(this.tabPageRectangle.Left, this.tabPageRectangle.Top, this.tabPageRectangle.Width, this.tabPageRectangle.Height));
            this.tabControlPara.Region = new Region(new RectangleF(this.pageBlur.Left, this.pageBlur.Top, this.pageBlur.Width, this.pageBlur.Height));                     
            //隐藏所有TabPage，显示需要的TabPage
            HideTabPage();
        }

        //以上代码完成标签的隐藏，但还存在一个问题，就是 Ctrl +Tab 可以切换TabControl中的页，可以通过捕捉按键消息屏蔽 组合键
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Tab | Keys.Control):
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void HideTabPage()
        {
            //修改TabPage所有背景颜色，并隐藏tabpage
            foreach (TabPage c in this.tabControlArea.TabPages)
            {
                c.BackColor = Color.Gainsboro;
                c.Parent = null;
            }         
            foreach (TabPage c in this.tabControlPara.TabPages)
            {
                c.BackColor = Color.Gainsboro;
                c.Parent = null;
            }
        }



        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{             
            //     contextMenuStrip1.Hide();
            //}
            //else
            //{
            //    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y+10);
            //}
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectInputImage = comboBox1.Text;//获取选择图像参数
            showPicture();
        }
        private void showPicture()
        {
            try
            {
                if (frmBase.frmProcessManage.imageDefine.preImage == null)
                {
                    imageShow = frmBase.frmProcessManage.ImageChoose(selectInputImage);            
                }
                else
                    imageShow = frmBase.frmProcessManage.imageDefine.preImage;
                if (imageShow != null)
                    pictureBox1.Image = imageShow.Bitmap;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }


        private void ComboBoxUpdate()
        {
            //必须不为空的数组 添加至combox
            foreach (string str in frmBase.frmProcessManage.ImageOptions)
            {
                if (str == null)
                    return;
            }
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(frmBase.frmProcessManage.ImageOptions);
        }
        private void FormPreProcessing_VisibleChanged(object sender, EventArgs e)
        {
            ComboBoxUpdate();
            label6.Text = trackBarMinThreshold.Value.ToString();
            label7.Text = trackBarMaxThreshold.Value.ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(PointToScreen(new Point(this.buttonAdd.Location.X, this.buttonAdd.Location.Y + 50)));

        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void 腐蚀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "腐蚀";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];

        }

        private void 膨胀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "膨胀";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 开运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "开运算";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 闭运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "闭运算";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }
        private void 梯度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "梯度";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }
        private void 黑帽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "黑帽";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 顶帽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "顶帽";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 均值滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "均值滤波";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }
        private void 高斯滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "高斯滤波";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }
        private void 双边滤波ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "双边滤波";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 双边滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "双边滤波";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 中值滤波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "中值滤波";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 二值化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "二值化";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 均衡化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "均衡化";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

 
        private void 锐化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "锐化";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void 模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] strErodePara = new string[2];
            strErodePara[0] = "模糊";
            strErodePara[1] = "0,0,0,0,0";
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = strErodePara[0];
            dataGridView1.Rows[index].Cells[1].Value = strErodePara[1];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HideTabPage();
            int RowIndex = dataGridView1.CurrentCell.RowIndex;  //当前单元格所在行
            string selectText = "";
            if (RowIndex >= 0)
            {
                selectText = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
            }
            if (selectText != "")
            {
                switch (selectText)
                {
                    case "均值滤波":
                        this.pageBlur.Parent = this.tabControlPara;
                        this.tabControlPara.SelectedTab = pageBlur;
                        break;
                    case "高斯滤波":
                        this.pageGaussianBlur.Parent = this.tabControlPara;
                        this.tabControlPara.SelectedTab = pageGaussianBlur;
                        break;
                    case "方框滤波":
                        this.pageBoxFilter.Parent = this.tabControlPara;
                        this.tabControlPara.SelectedTab = pageBoxFilter;
                        break;
                    case "中值滤波":
                        this.pageMedianBlur.Parent = this.tabControlPara;
                        this.tabControlPara.SelectedTab = pageMedianBlur;
                        break;
                    case "双边滤波":
                        this.pageBilateralFilter.Parent = this.tabControlPara;
                        this.tabControlPara.SelectedTab = pageBilateralFilter;
                        break;


                    case "二值化":
                        this.pageBianry.Parent = this.tabControlPara;
                        this.tabControlPara.SelectedTab = pageBianry;
                        break;
                }

            
            }
        }
       

        public void PreProcess(string str, Mat src, Mat dst)
        {
            switch (str)
            {
                case "均值滤波":
                    CvInvoke.Blur(src, dst, new Size(blurSize, blurSize), new Point(-1, -1));
                    break;
                case "高斯滤波":
                    CvInvoke.GaussianBlur(src, dst, new Size(gaussianBlurSize, gaussianBlurSize), 0, 0);
                    break;
                case "方框滤波":
                    CvInvoke.BoxFilter(src, dst,DepthType.Default, new Size(boxFilterSize, boxFilterSize), new Point(-1, -1));
                    break;
                case "中值滤波":
                    CvInvoke.MedianBlur(src, dst, medianBlurSize);
                    break;
                case "双边滤波":
                    CvInvoke.BilateralFilter(src, dst, bilateralFilterSize, bilateralFilterSize * 2, bilateralFilterSize / 2);
                    break;
                case "腐蚀":            
                    Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));
                    CvInvoke.MorphologyEx(src, dst, MorphOp.Erode, element, new Point(1, 1), 1, BorderType.Default, new MCvScalar(255, 0, 0, 255));
                    break;
                case "膨胀":
                    Mat element1 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));
                    CvInvoke.MorphologyEx(src, dst, MorphOp.Dilate, element1, new Point(1, 1), 1, BorderType.Default, new MCvScalar(255, 0, 0, 255));
                    break;
                case "开运算":
                    Mat element2 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));
                    CvInvoke.MorphologyEx(src, dst, MorphOp.Open, element2, new Point(1, 1), 1, BorderType.Default, new MCvScalar(255, 0, 0, 255));
                    break;
                case "闭运算":
                    Mat element3 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));
                    CvInvoke.MorphologyEx(src, dst, MorphOp.Close, element3, new Point(1, 1), 1, BorderType.Default, new MCvScalar(255, 0, 0, 255));
                    break;
                case "梯度":
                    Mat element4 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));
                    CvInvoke.MorphologyEx(src, dst, MorphOp.Gradient, element4, new Point(1, 1), 1, BorderType.Default, new MCvScalar(255, 0, 0, 255));
                    break;
                case "顶帽":
                    Mat element5 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));
                    CvInvoke.MorphologyEx(src, dst, MorphOp.Tophat, element5, new Point(1, 1), 1, BorderType.Default, new MCvScalar(255, 0, 0, 255));
                    break;
                case "黑帽":
                    Mat element6 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(5, 5), new Point(2, 2));
                    CvInvoke.MorphologyEx(src, dst, MorphOp.Blackhat, element6, new Point(1, 1), 1, BorderType.Default, new MCvScalar(255, 0, 0, 255));
                    break;
                case "二值化":
                    CvInvoke.Threshold(src, dst,threshold,maxValue,ThresholdType.Binary);
                    break;
                case "均衡化":
                 //   Mat Histimg = new Mat(CvInvoke.cvGetSize(src), DepthType.Cv8U, 1);
                    CvInvoke.EqualizeHist(src, dst);   //均衡化         
                    break;
                case "模糊":
                    // dst=src.SmoothMedian (3);
                    break;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            blurSize =(int)nudBlurSize.Value;
            gaussianBlurSize= (int)nudGaussianBlurSize.Value;


            threshold= (int)trackBarMinThreshold.Value;
            maxValue= (int)trackBarMaxThreshold.Value;

            Execute();
        }


        public bool Execute()
        {

            frmBase.frmProcessManage.imageDefine.preImage = null;
            Mat srcMat;
            try
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        if (dr.Index==0)
                            srcMat = frmBase.frmProcessManage.ImageChoose(selectInputImage);
                        else
                            srcMat = frmBase.frmProcessManage.imageDefine.preImage;
                        frmBase.frmProcessManage.imageDefine.preImage = new Mat(srcMat.Size, srcMat.Depth, srcMat.NumberOfChannels);
                        PreProcess(dr.Cells[0].Value.ToString(), srcMat, frmBase.frmProcessManage.imageDefine.preImage);
                    }
                }
                showPicture();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                //选中的行数  
                int iCount = dataGridView1.SelectedRows.Count;
                if (iCount < 1)
                {
                    MessageBox.Show("Delete Data Fail!", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return;
                }
                if (DialogResult.Yes == MessageBox.Show("是否删除选中的数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)  //循环遍历所有行  
                    {
                        if (this.dataGridView1.Rows[i].Selected)//当前行处于选中状态，则将其删除  
                            this.dataGridView1.Rows.RemoveAt(i);
                    }
                    //删除任意行数据后，应该刷新dataGridView表格，使索引值从上至下按大小顺序排序  
                    //for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    //{
                    //    dataGridView1.Rows[i].Cells[0].Value = i + 1;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //方形ROI设置
        private void button6_Click(object sender, EventArgs e)
        {          
            HideTabPage();      
            this.tabPageRectangle.Parent = this.tabControlArea;
            this.tabControlArea.SelectedTab = tabPageRectangle;

            if (roiSetUp == null)
            {
                roiSetUp = new ROISetUp(pictureBox1);
                roiSetUp.DrawRectangle();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (roiSetUp != null)
            {
                roiSetUp.ClearEvent();             
                roiSetUp = null;
            }
        }


   
    

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label6.Text = trackBarMinThreshold.Value.ToString();
        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label7.Text = trackBarMaxThreshold.Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(roiSetUp!=null)
            {
                if (roiSetUp.bDrawOver)
                {
                    roi = new Rectangle(roiSetUp.rect.Location, roiSetUp.rect.Size);
                    tbLeftX.Text = roi.Location.X.ToString();
                    tbLeftY.Text = roi.Location.Y.ToString();
                    tbWidth.Text = roi.Width.ToString();
                    tbHeight.Text = roi.Height.ToString();
                    roiSetUp.bDrawOver = false;
                }
            }
        }
    }
}
