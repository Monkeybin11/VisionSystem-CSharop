using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace VisionSystem
{
    public partial class FormBase : DockContent
    {
        public FormBase()
        {
            InitializeComponent();
            ClassCreate();
            InitSystem();
        }


        // Form属性
        public FormMain frmMain { get; set; }
        public FormProcessManage frmProcessManage { get; set; }
        public FormTool frmTool { get; set; }
        public FormData frmData { get; set; }
        public FormLog frmLog { get; set; }
        public FormMainDraw frmMainDraw { get; set; }

        //工具

     
        public DataStore dataStore { get; set; }


        //相机设置
        public DirectVideo directVideo { get; set; }

        //图像处理
        public FormImageAcquisition frmImageAcquisition { get; set; }
        public FormSaveImage frmSaveImage { get; set; }
        public FormImageArithmetic frmImageArithmetic { get; set; }
        public FormColorToGray frmColorToGray { get; set; }
        public FormPreProcessing frmPreProcessing { get; set; }
        public FormColorExtraction frmColorExtraction { get; set; }


        //标定校准
        public FormCoordinateCalibration frmCoordinateCalibration { get; set; }
        public FormCameraCalibration frmcameraCalibration { get; set; }


        //检测识别
        public FormPixelStatistics frmPixelStatistics { get; set; }
        public FormBrightnessDetect frmBrightnessDetect { get; set; }
        public FormGrayMatch frmGrayMatch { get; set; }
        public FormContourMatch frmContourMatch { get; set; }
        public FormImageCompare frmImageCompare { get; set; }
        public FormBlob frmBlob { get; set; }
        public FormQRCode frmQRCode { get; set; }


        //几何测量
        public FormMarkPoint frmMarkPoint { get; set; }
        public FormPointToPoint frmPointToPoint { get; set; }
        public FormPointToLine frmPointToLine { get; set; }
        public FormLineToLine frmLineToLine { get; set; }
        public FormLineToCircle frmLineToCircle { get; set; }
        public FormFitLine frmFitLine { get; set; }
        public FormFitCircle frmFitCircle { get; set; }

        //文本通讯
        public FormSerialPort frmSerialPort { get; set; }
        public FormSocket frmSocket { get; set; }
        public FormSendText frmSendText { get; set; }
        public FormReceiveText frmReceiveText { get; set; }
        public FormInput frmInput { get; set; }
        public FormOutput frmOutput { get; set; }
        public FormReceiveData frmReceiveData { get; set; }
        public FormSendData frmSendData { get; set; }
        public FormLightControl frmLightControl { get; set; }

        //逻辑控制
        public FormDelay frmDelay { get; set; }
    public FormConditionalBranch frmConditionalBranch { get; set; }
        public FormSelectBranch frmSelectBranch { get; set; }
        public FormCirculatoryTool frmCirculatoryTool { get; set; }
        public FormStopCirculatory frmStopCirculatory { get; set; }
        public FormExecutingProcess frmExecutingProcess { get; set; }
        public FormParallelProcess frmParallelProcess { get; set; }
        public FormDequeueData frmDequeueData { get; set; }
        public FormEnqueueData frmEnqueueData { get; set; }
        public FormClearQueue frmClearQueue { get; set; }

        //系统工具
        public FormComputingTime frmComputingTime { get; set; }
        public FormUserVariable frmUserVariable { get; set; }
        public FormComputeVariable frmComputeVariable { get; set; }
        public FormSetVariable frmSetVariable { get; set; }
        public FormDataRecord frmDataRecord { get; set; }
        public FormClearRecord frmClearRecord { get; set; }
        public FormDefineArray frmDefineArray { get; set; }
        public FormDataDisplay frmDataDisplay { get; set; }
        public FormDataJudgment frmDataJudgment { get; set; }


        //other
        public ControlSafeAccess controlSafeAccess { get; set; }

        //实例化类
        public void ClassCreate()
        {
            frmMain = new FormMain(this);
            frmProcessManage = new FormProcessManage(this);
            frmTool = new FormTool(this);
            frmData = new FormData(this);
            frmLog = new FormLog(this);
            frmMainDraw = new FormMainDraw(this);
            dataStore = new DataStore();

            //相机设置
            directVideo = new DirectVideo(this);

            //图像处理
            frmImageAcquisition = new FormImageAcquisition(this);
            frmSaveImage = new FormSaveImage(this);
            frmImageArithmetic = new FormImageArithmetic(this);
            frmColorToGray = new FormColorToGray(this);
            frmPreProcessing = new FormPreProcessing(this);
            frmColorExtraction = new FormColorExtraction(this);

            //标定校准
            frmCoordinateCalibration = new FormCoordinateCalibration(this);
            frmcameraCalibration = new FormCameraCalibration(this);

            //检测识别
            frmPixelStatistics = new FormPixelStatistics(this);
            frmBrightnessDetect = new FormBrightnessDetect(this);
            frmGrayMatch = new FormGrayMatch(this);
            frmContourMatch = new FormContourMatch(this);
            frmImageCompare = new FormImageCompare(this);
            frmBlob = new FormBlob(this);
            frmQRCode = new FormQRCode(this);

            //几何测量
            frmMarkPoint = new FormMarkPoint(this);
            frmPointToPoint = new FormPointToPoint(this);
            frmPointToLine = new FormPointToLine(this);
            frmLineToLine = new FormLineToLine(this);
            frmLineToCircle = new FormLineToCircle(this);
            frmFitLine = new FormFitLine(this);
            frmFitCircle = new FormFitCircle(this);

            //文本通讯
            frmSerialPort = new FormSerialPort(this);
            frmSocket = new FormSocket(this);
            frmSendText = new FormSendText(this);
            frmReceiveText = new FormReceiveText(this);
            frmInput = new FormInput(this);
            frmOutput = new FormOutput(this);
            frmReceiveData = new FormReceiveData(this);
            frmSendData = new FormSendData(this);
            frmLightControl = new FormLightControl(this);

            //逻辑控制
            frmDelay = new FormDelay(this);
            frmConditionalBranch = new FormConditionalBranch(this);
            frmSelectBranch = new FormSelectBranch(this);
            frmCirculatoryTool = new FormCirculatoryTool(this);
            frmStopCirculatory = new FormStopCirculatory(this);
            frmExecutingProcess = new FormExecutingProcess(this);
            frmParallelProcess = new FormParallelProcess(this);
            frmDequeueData = new FormDequeueData(this);
            frmEnqueueData = new FormEnqueueData(this);
            frmClearQueue = new FormClearQueue(this);

            //系统工具
            frmComputingTime = new FormComputingTime(this);
            frmUserVariable = new FormUserVariable(this);
            frmComputeVariable = new FormComputeVariable(this);
            frmSetVariable = new FormSetVariable(this);
            frmDataRecord = new FormDataRecord(this);
            frmClearRecord = new FormClearRecord(this);
            frmDefineArray = new FormDefineArray(this);
            frmDataDisplay = new FormDataDisplay(this);
            frmDataJudgment = new FormDataJudgment(this);


            //other
            controlSafeAccess = new ControlSafeAccess(this);
        }

        //初始化系统
        private void InitSystem()
        {
           
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                frmData.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float | DockAreas.DockBottom | DockAreas.DockTop;
                frmData.Show(dockPanel1, DockState.DockBottom);
                数据ToolStripMenuItem.Checked = true;

                frmLog.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float | DockAreas.DockBottom | DockAreas.DockTop;
                frmLog.Show(dockPanel1, DockState.DockBottom);
                日志ToolStripMenuItem.Checked = true;

                frmTool.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float | DockAreas.DockBottom | DockAreas.DockTop;
                frmTool.Show(dockPanel1, DockState.DockRight);
                工具箱ToolStripMenuItem.Checked = true;

                this.dockPanel1.DockRightPortion = (double)frmTool.Width / (double)this.dockPanel1.Width * 1.3;
                frmProcessManage.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.Float | DockAreas.DockBottom | DockAreas.DockTop;
                //  frmProcessManage.Show(dockPanel1, DockState.DockRight);
                frmProcessManage.Show(frmTool.Pane, DockAlignment.Right, 0.6);  //左右         
                流程栏ToolStripMenuItem.Checked = true;

                frmMain.DockAreas = DockAreas.Document;
                frmMain.Show(dockPanel1, DockState.Document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


         
        private void 停止流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

 

        private void 相机设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogSystemSetting dialogSystemSetting = new DialogSystemSetting();
            dialogSystemSetting.ShowDialog();
        }

        private void 密码设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordSetting passwordSetting = new PasswordSetting();
            passwordSetting.ShowDialog();
        }

        private void 项目设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectSetting projectSetting = new ProjectSetting();
            projectSetting.ShowDialog();
        }

        private void 界面设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {     
            frmMainDraw.ShowDialog();
        }

        private void 新增项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogAbout dialogAbout = new DialogAbout();
            dialogAbout.Show();
        }
        private void 工具箱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!frmTool.Visible)
            {
                frmTool.Location = frmTool.FormToolPosition;
                frmTool.Show();
                
            }
            else
            {
                frmTool.Hide();
                
            }
        }

        private void 流程栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!frmProcessManage.Visible)
            {
                frmProcessManage.Location = frmProcessManage.FormProcessPosition;
                frmProcessManage.Show();
                 
            }
            else
            {
                frmProcessManage.Hide();
             
            }
        }

        private void 数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!数据ToolStripMenuItem.Checked)
            {
                frmData.Location = frmData.formDataShowPositon;
                frmData.Show();           
            }
            else
            {
                frmData.Hide();       
            }
        }

        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!日志ToolStripMenuItem.Checked)
            {
                frmLog.Location = frmLog.formLogPosition;
                frmLog.Show();      
            }
            else
            {
                frmLog.Hide();      
            }
        }

 
  
        private void FormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }



        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            //扫描工具过程并执行
            frmProcessManage.loop = false;
            frmProcessManage.ProcessRun();
        }
        private void toolStripButtonLoop_Click(object sender, EventArgs e)
        {
            frmProcessManage.loop = true;
            frmProcessManage.ProcessRun();
        }
        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            frmProcessManage.loop = false;
            frmProcessManage.bmainThread = false;
            frmProcessManage.executeNum = 0;
        }

        ////利用反射复制控件
        //public static T Clone<T>(this T controlToClone)
        //    where T : Control
        //{
        //    PropertyInfo[] controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    T instance = Activator.CreateInstance<T>();

        //    foreach (PropertyInfo propInfo in controlProperties)
        //    {
        //        if (propInfo.CanWrite)
        //        {
        //            if (propInfo.Name != "WindowTarget")
        //                propInfo.SetValue(instance, propInfo.GetValue(controlToClone, null), null);
        //        }
        //    }

        //    return instance;
        //}
    }


}
