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
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.UI;
using System.Drawing.Drawing2D;
using System.Threading;

namespace VisionSystem
{


    public partial class FormProcessManage : DockContent
    {
        public class ImageDefine
        {
            //图像定义
            public Mat acqImage;                  //采集图像
            public Mat arithmeticeImag;           //图像运算
            public Mat color2GrayImage;           //彩色转灰
            public Mat preImage;                  //预先处理
            public Mat mergingImage;              //图像合并
            public Mat colorExtractionImage;      //颜色抽取
            public Mat cutTransferImage;          //裁剪变换


            public Mat grayMatchImage;    //灰度匹配结果
        }

        public bool loop = false;
        public bool bmainThread = false;
        bool bRunResult = false;
        public ImageDefine imageDefine=new ImageDefine();  //实例化图像类
        public Point FormProcessPosition = new Point();  //记录窗口当前位置
       
        string[] tools;  //工具列表
        int count;       //选用的工具数量
        public int executeNum=0;  //执行次数，循环扫描本地图片时使用
        public string[] ImageOptions;   //图像存储界面comboBox选项

        string StrSelectTool = "";
        FormBase frmBase;
        public FormProcessManage(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }
        private void FormProcess_Load(object sender, EventArgs e)
        {


        }
        private void FormProcess_VisibleChanged(object sender, EventArgs e)
        {
            FormProcessPosition = this.Location;
            if (this.Visible)
            {
                frmBase.流程栏ToolStripMenuItem.Checked = true;
            }
            else
            {
                frmBase.流程栏ToolStripMenuItem.Checked = false;
            }
        }
        #region  TreeView到ListView 拖拽实现
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
        
            try
            {
                TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                if (node != null)
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    Cursor = Cursors.No;
                    e.Effect = e.AllowedEffect;  //在listview控件内进行拖拽时
                }
            }
            catch { }
            finally { Cursor = Cursors.Default; }

         
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (frmBase.frmTool._selectednode != null & (e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    DragDropEffects dropEffect = listView1.DoDragDrop(frmBase.frmTool._selectednode, DragDropEffects.Move);
                }
            }
            catch { }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TreeNode node = (TreeNode)e.Data.GetData(typeof(TreeNode));
                if (node != null)
                {
                    if (node.Text != "条件分支" && node.Text != "选择分支" && node.Text != "循环工具")
                    {
                        string num = (listView1.Items.Count + 1).ToString();
                        ListViewItem lv = new ListViewItem(num);//第一列的记录为序号  
                        lv.SubItems.Add(node.Text);//添加第二列的内容为001  
                        lv.SubItems.Add("X");//添加第三列的内容  
                        lv.ImageIndex = node.ImageIndex;//指定图像的索引  
                        listView1.Items.Add(lv);
                    }
                    else
                    {
                        MultilineInsert(node.Text);
                    }

                }
                else    //在控件内拖放
                {
                    // 返回插入标记的索引值  
                    int index = listView1.InsertionMark.Index;
                    // 如果插入标记不可见，则退出.  
                    if (index == -1)
                    {
                        return;
                    }
                    // 如果插入标记在项目的右面，使目标索引值加一  
                    if (listView1.InsertionMark.AppearsAfterItem)
                    {
                        index++;
                    }

                    // 返回拖拽项  
                    ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                    //在目标索引位置插入一个拖拽项目的副本   
                    listView1.Items.Insert(index, (ListViewItem)item.Clone());
                    // 移除拖拽项目的原文件  
                    listView1.Items.Remove(item);
                }
                //   listView1.Items.Add("2",node.Text, node.ImageIndex);
            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Default;
                frmBase.frmTool._selectednode = null;
            }
        }

        private void MultilineInsert(string text)
        {
            switch (text)
            {
                case "条件分支":
                    {

                        string num1 = (listView1.Items.Count + 1).ToString();
                        ListViewItem lv1 = new ListViewItem(num1);//第一列的记录为序号  
                        lv1.SubItems.Add("如果");//添加第二列的内容为001  
                        lv1.SubItems.Add("X");//添加第三列的内容  
                                              // lv.ImageIndex = node.ImageIndex;//指定图像的索引  
                        listView1.Items.Add(lv1);

                        string num2 = (listView1.Items.Count + 2).ToString();
                        ListViewItem lv2 = new ListViewItem(num2);//第一列的记录为序号  
                        lv2.SubItems.Add("结束");//添加第二列的内容为001  
                        lv2.SubItems.Add("X");//添加第三列的内容  
                        // lv.ImageIndex = node.ImageIndex;//指定图像的索引  
                        listView1.Items.Add(lv2);



                    }
                    break;
                case "选择分支":
                    {
                        string num1 = (listView1.Items.Count + 1).ToString();
                        ListViewItem lv1 = new ListViewItem(num1);//第一列的记录为序号  
                        lv1.SubItems.Add("开始选择");//添加第二列的内容为001  
                        lv1.SubItems.Add("X");//添加第三列的内容  
                                              // lv.ImageIndex = node.ImageIndex;//指定图像的索引  
                        listView1.Items.Add(lv1);

                        string num2 = (listView1.Items.Count + 2).ToString();
                        ListViewItem lv2 = new ListViewItem(num2);//第一列的记录为序号  
                        lv2.SubItems.Add("结束选择");//添加第二列的内容为001  
                        lv2.SubItems.Add("X");//添加第三列的内容  
                        // lv.ImageIndex = node.ImageIndex;//指定图像的索引  
                        listView1.Items.Add(lv2);
                    }
                    break;
                case "循环工具":
                    {
                        string num1 = (listView1.Items.Count + 1).ToString();
                        ListViewItem lv1 = new ListViewItem(num1);//第一列的记录为序号  
                        lv1.SubItems.Add("开始循环");//添加第二列的内容为001  
                        lv1.SubItems.Add("X");//添加第三列的内容  
                                              // lv.ImageIndex = node.ImageIndex;//指定图像的索引  
                        listView1.Items.Add(lv1);

                        string num2 = (listView1.Items.Count + 2).ToString();
                        ListViewItem lv2 = new ListViewItem(num2);//第一列的记录为序号  
                        lv2.SubItems.Add("结束循环");//添加第二列的内容为001  
                        lv2.SubItems.Add("X");//添加第三列的内容  
                        // lv.ImageIndex = node.ImageIndex;//指定图像的索引  
                        listView1.Items.Add(lv2);
                    }
                    break;
                default:
                    break;

            }
            UpdateListView();
        }
        #endregion

        #region listView内部拖拽
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView1.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            // 获得鼠标坐标
            Point point = listView1.PointToClient(new Point(e.X, e.Y));
            // 返回离鼠标最近的项目的索引  
            int index = listView1.InsertionMark.NearestIndex(point);
            // 确定光标不在拖拽项目上  
            if (index > -1)
            {
                Rectangle itemBounds = listView1.GetItemRect(index);
                if (point.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    listView1.InsertionMark.AppearsAfterItem = true;
                }
                else
                {
                    listView1.InsertionMark.AppearsAfterItem = false;
                }


            }
            listView1.InsertionMark.Index = index;

        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {
            listView1.InsertionMark.Index = -1;
  
        }
        #endregion

        #region ListView右键属性 
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //按鼠标右键，弹出菜单    
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(PointToScreen(new  Point(e.X,e.Y+50)));
                Point point = listView1.PointToClient(new Point(e.X, e.Y));
                StrSelectTool = listView1.GetItemAt(e.X,e.Y).SubItems[1].Text;    
                               
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "属性")
            {
                ToolSetting(StrSelectTool);
            }
            if (e.ClickedItem.Text == "删除")
            {
                if (this.listView1.SelectedItems.Count > 0)                  //判断listview有被选中项
                {
                    int currentIndex = this.listView1.SelectedItems[0].Index;//取当前选中项的index
                    listView1.Items[currentIndex].Remove();
                    UpdateListView();
                }
            }
        }


        private void UpdateListView()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].SubItems[0].Text = (i + 1).ToString();
            }
        }
        #endregion

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point point = listView1.PointToClient(new Point(e.X, e.Y));
            StrSelectTool = listView1.GetItemAt(e.X, e.Y).SubItems[1].Text;
            ToolSetting(StrSelectTool);
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateListView();
        }

        #region 对话框打开管理
        private void ToolSetting(string strFlag)
        {
            switch (strFlag)
            {
                case "相机设置":
                    frmBase.directVideo.ShowDialog();
                    break;

                //////////////////////////图像处理/////////////////////////////
                case "采集图像":                   
                    frmBase.frmImageAcquisition.ShowDialog();
                    break;
                case "存储图像":
                    frmBase.frmSaveImage.ShowDialog();
                    break;
                case "图像运算":
                    frmBase.frmImageArithmetic.ShowDialog();
                    break;
                case "彩色转灰":
                    frmBase.frmColorToGray.ShowDialog();
                    break;
                case "预先处理":                
                    frmBase.frmPreProcessing.ShowDialog();
                    break;
                case "颜色抽取":
                    frmBase.frmColorExtraction.ShowDialog();
                    break;

                //////////////////////////标定校准/////////////////////////////
                case "坐标校准":
                    frmBase.frmCoordinateCalibration.ShowDialog();
                    break;
                case "图像校正":
                    frmBase.frmcameraCalibration.ShowDialog();
                    break;

                //////////////////////////检测识别/////////////////////////////
                case "像素统计":
                    frmBase.frmPixelStatistics.ShowDialog();
                    break;
                case "检测亮度":
                    frmBase.frmBrightnessDetect.ShowDialog();
                    break;
                case "灰度匹配":
                    frmBase.frmGrayMatch.ShowDialog();
                    break;
                case "轮廓匹配":
                    frmBase.frmContourMatch.ShowDialog();
                    break;
                case "图像对比":
                    frmBase.frmImageCompare.ShowDialog();
                    break;
                case "斑点分析":
                    frmBase.frmBlob.ShowDialog();
                    break;
                case "读取QR码":
                    frmBase.frmQRCode.ShowDialog();
                    break;


                //////////////////////////几何测量/////////////////////////////
                case "标记点":
                    frmBase.frmMarkPoint.ShowDialog();
                    break;
                case "点到点":
                    frmBase.frmPointToPoint.ShowDialog();
                    break;
                case "点到线":
                    frmBase.frmPointToLine.ShowDialog();
                    break;
                case "线到线":
                    frmBase.frmLineToLine.ShowDialog();
                    break;
                case "线到圆":
                    frmBase.frmLineToCircle.ShowDialog();
                    break;
                case "拟合线":
                    frmBase.frmFitLine.ShowDialog();
                    break;
                case "拟合圆":
                    frmBase.frmFitCircle.ShowDialog();
                    break;


                //////////////////////////文件通讯/////////////////////////////
                case "串口通讯":
                    frmBase.frmSerialPort.ShowDialog();
                    break;
                case "以太网":
                    frmBase.frmSocket.ShowDialog();
                    break;
                case "发送文本":
                    frmBase.frmSendText.ShowDialog();
                    break;
                case "接收文本":
                    frmBase.frmReceiveText.ShowDialog();
                    break;
                case "状态输入":
                    frmBase.frmInput.ShowDialog();
                    break;
                case "状态输出":
                    frmBase.frmOutput.ShowDialog();
                    break;
                case "接收数据":
                    frmBase.frmReceiveData.ShowDialog();
                    break;
                case "发送数据":
                    frmBase.frmSendData.ShowDialog();
                    break;
                case "光源控制":
                    frmBase.frmLightControl.ShowDialog();
                    break;

                ///////////////////////////////////////逻辑控制///////////////////////////////////////////
                case "等待延迟":
                    frmBase.frmDelay.ShowDialog();
                    break;
                case "条件分支":
                    frmBase.frmConditionalBranch.ShowDialog();
                    break;
                case "选择分支":
                    frmBase.frmSelectBranch.ShowDialog();
                    break;
                case "循环工具":
                    frmBase.frmCirculatoryTool.ShowDialog();
                    break;
                case "停止循环":
                    frmBase.frmStopCirculatory.ShowDialog();
                    break;
                case "执行流程":
                    frmBase.frmExecutingProcess.ShowDialog();
                    break;
                case "并行流程":
                    frmBase.frmParallelProcess.ShowDialog();
                    break;
                case "数据入列":
                    frmBase.frmDequeueData.ShowDialog();
                    break;
                case "数据出列":
                    frmBase.frmEnqueueData.ShowDialog();
                    break;
                case "清空队列":
                    frmBase.frmClearQueue.ShowDialog();
                    break;

                ///////////////////////////////////////系统工具///////////////////////////////////////////
                case "计算时间":
                    frmBase.frmComputingTime.ShowDialog();
                    break;
                case "用户变量":
                    frmBase.frmUserVariable.ShowDialog();
                    break;
                case "计算变量":
                    frmBase.frmComputeVariable.ShowDialog();
                    break;
                case "设置变量":
                    frmBase.frmSetVariable.ShowDialog();
                    break;
                case "数据记录":
                    frmBase.frmDataRecord.ShowDialog();
                    break;
                case "清除记录":
                    frmBase.frmClearRecord.ShowDialog();
                    break;
                case "定义数组":
                    frmBase.frmDefineArray.ShowDialog();               
                    break;
                case "数值显示":
                    frmBase.frmDataDisplay.ShowDialog();
                    break;
                case "数据判断":
                    frmBase.frmDataJudgment.ShowDialog();
                    break;
            }
        }
        #endregion

        private void timerProcessUpdate_Tick(object sender, EventArgs e)
        {
            if (bmainThread)   //程序正在运行时不更新
                return;
            count = listView1.Items.Count;
            tools = new string[count];
            string[] strArray=new string[count];
            int n = 0;
            //逐行扫描
            for (int i = 0; i < count; i++)
            {
                tools[i] = listView1.Items[i].SubItems[1].Text;
                if (tools[i].Contains("采集图像"))
                {
                    strArray[n] = "Task1." + tools[i];
                    n++;
                }
                if (tools[i].Contains("预先处理"))
                {
                    strArray[n] = "Task1." + tools[i];
                    n++;
                }
                if (tools[i].Contains("彩色转灰"))
                {
                    strArray[n] = "Task1." + tools[i];
                    n++;
                }
                if (tools[i].Contains("图像运算"))
                {
                    strArray[n] = "Task1." + tools[i];
                    n++;
                }
                if (tools[i].Contains("颜色抽取"))
                {
                    strArray[n] = "Task1." + tools[i];
                    n++;
                }
                if (tools[i].Contains("图像合并"))
                {
                    strArray[n] = "Task1." + tools[i];
                    n++;
                }
                if (tools[i].Contains("裁剪变换"))
                {
                    strArray[n] = "Task1." + tools[i];
                    n++;
                }
            }
            ImageOptions = new string[n];
            for (int i=0;i<n;i++)    
            {
                ImageOptions[i] = strArray[i];
            }

        }

        #region 流程处理主线程     
        public void ProcessRun()
        {
            if (!bmainThread)
            {
                Thread mainTask = new Thread(new ThreadStart(ProcessThread));
                mainTask.IsBackground = true;
                mainTask.Start();
                bmainThread = true;
            }
        }


          
        public void ProcessThread()
        {
              
            //逐行执行
            do
            {
                for (int i = 0; i < count; i++)
                {
                    if (!ToolExecute(tools[i]))   //如果中途失败，退出
                    {
                        bRunResult = false;
                        break;
                    }
                    else
                    {
                        if (i==count-1)
                        {
                            bRunResult = true;
                        }
                    }
                }
                if (frmBase.frmImageAcquisition.ChooseInputImageWay == 2)
                {
                    executeNum += 1;
                    if (executeNum >= frmBase.frmImageAcquisition.filesName.Length)
                        executeNum = 0;
                }
                if (frmBase.frmMainDraw.lb_OK_NG != null)
                {
                    if (bRunResult)
                        frmBase.controlSafeAccess.LabelChange(frmBase.frmMainDraw.lb_OK_NG, "OK");
                    else
                        frmBase.controlSafeAccess.LabelChange(frmBase.frmMainDraw.lb_OK_NG, "NG");
                }
            }
            while (loop);

            bmainThread = false;
        }

        private bool ToolExecute(string tool)
        {         
            bool res = false;
            switch(tool)
            {
                case "相机设置":
                    res = true;
                    break;
                case "存储图像":
                    res = frmBase.frmSaveImage.SaveImage(frmBase.dataStore.imageAcquisitionData.InputImagePath);
                    break;
                case "彩色转灰":
                    res = frmBase.frmColorToGray.Bgr2Gray();
                    break;
                case "预先处理":
                    res = frmBase.frmPreProcessing.Execute();
                    break;
                case "颜色抽取":
                    res = frmBase.frmColorExtraction.ColorExtract();
                    break;
                case "灰度匹配":
                    res = frmBase.frmGrayMatch.GrayMath();
                    break;

            }
            if(tool!=null)
            {
                if (tool.Contains("采集图像"))
                {
                    if (frmBase.frmImageAcquisition.ChooseInputImageWay == 1)  //本地图片
                        res = frmBase.frmImageAcquisition.ReadImage(frmBase.dataStore.imageAcquisitionData.InputImagePath);
                    if (frmBase.frmImageAcquisition.ChooseInputImageWay == 2)//本地目录图片
                    {                       
                        res = frmBase.frmImageAcquisition.ReadDirImage(frmBase.dataStore.imageAcquisitionData.InputImageDirPath);                     
                    }
                    if (frmBase.frmImageAcquisition.ChooseInputImageWay == 3)//相机采集
                    {
                        frmBase.directVideo.SingleFrame = true;
                        res = frmBase.directVideo.OpenVideo();
                        while(frmBase.directVideo.isGrab)
                        {
                            Thread.Sleep(10);//等待拍照采集结束
                        }
                    }
                   
                }
            }
                return res;
        }
   

        #endregion

        /// <summary>
        /// 根据comboBox选项选择图像变量
        /// </summary>
        /// <param name="strTool"></param>
        /// <returns></returns>
        public Mat ImageChoose(string strTool)
        {
            if (strTool.Contains("采集图像"))
                return imageDefine.acqImage;
            else if (strTool.Contains("彩色转灰"))
                return imageDefine.color2GrayImage;
            else if (strTool.Contains("预先处理"))
                return imageDefine.preImage;
            else if (strTool.Contains("图像运算"))
                return imageDefine.arithmeticeImag;
            else if (strTool.Contains("图像合并"))
                return imageDefine.mergingImage;
            else if (strTool.Contains("颜色抽取"))
                return imageDefine.colorExtractionImage;
            else if (strTool.Contains("裁剪变换"))
                return imageDefine.cutTransferImage;
            else
                return null;
        }

    }


}
