using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.VideoStab;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Ocl;
using DirectShowLib;
using System.Windows.Input;
using System.Threading;

namespace VisionSystem
{
    public partial class DirectVideo : Form
    {
        VideoCapture capt;
        FormBase frmBase;
        public bool SingleFrame = false;
        Thread GetFrameThread;
        public  bool isGrab = false;

        public DirectVideo(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;

           
        }
        private void FormCamera_Load(object sender, EventArgs e)
        {
            buttonOpen.Enabled = true;
            buttonClose.Enabled = false;

            //Get the information about the installed cameras and add the combobox items 
            DsDevice[] ds = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            for (int i = 0; i < ds.Length; i++)
            {
                comboBox1.Items.Add(ds[i].Name);
                frmBase.frmImageAcquisition.cameraList[i] = ds[i].Name;
            }
            if (ds.Length >= 1)
                comboBox1.SelectedIndex = 0;
        }
        private void FormCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CameraPara cameraPara = new CameraPara();
            cameraPara.Show();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenVideo();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
           CloseVideo();
        }

        public bool OpenVideo()
        {
            try
            {
                CloseVideo();
                if (capt == null && comboBox1.SelectedIndex != -1)
                    capt = new VideoCapture(comboBox1.SelectedIndex);
                
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
            if (capt != null)
            {
                buttonOpen.Enabled = false;
                buttonClose.Enabled = true;
                //   Application.Idle += new EventHandler(GetFrame);
                if (!isGrab)
                {
                    GetFrameThread = new Thread(new ThreadStart(GetFrame));
                    GetFrameThread.IsBackground = true;
                    GetFrameThread.Start();
                    isGrab = true;
                }
                return true;
            }
            else
                return false;
        }

        public void CloseVideo()
        {
            if (capt != null)
            {
                buttonOpen.Enabled = true;
                buttonClose.Enabled = false;
                //    Application.Idle -= new EventHandler(GetFrame);
               GetFrameThread.Abort();
                isGrab = false;
            }
        }
 

        private void GetFrame()
        {
            do
            {
                frmBase.frmProcessManage.imageDefine.acqImage = capt.QueryFrame();
                if (this.Visible)
                {
                    frmBase.controlSafeAccess.PictureBoxChange(pictureBox1,frmBase.frmProcessManage.imageDefine.acqImage.Bitmap);
                   
                }
                frmBase.controlSafeAccess.PictureBoxChange(frmBase.frmMain.pictureBox1, frmBase.frmProcessManage.imageDefine.acqImage.Bitmap);           
            }
            while (!SingleFrame);
            isGrab = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CloseVideo();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
