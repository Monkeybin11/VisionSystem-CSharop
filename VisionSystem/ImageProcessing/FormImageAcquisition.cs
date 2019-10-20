using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;

namespace VisionSystem
{
    public partial class FormImageAcquisition : Form
    {
        FormBase frmBase;
        public string[] cameraList=new string[8];
        public int ChooseInputImageWay=0;
        public string[] filesName;


        public FormImageAcquisition(FormBase _frmBase)
        {
            InitializeComponent();
            this.radioButton1.CheckedChanged += new EventHandler(this.radioBtn_CheckedChange);
            this.radioButton2.CheckedChanged += new EventHandler(this.radioBtn_CheckedChange);
            this.radioButton3.CheckedChanged += new EventHandler(this.radioBtn_CheckedChange);
            frmBase= _frmBase;
    }

        private void radioBtn_CheckedChange(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked)
            {
                return;
            }      
            switch (((RadioButton)sender).Text.ToString())
            {
                case "文件":
                    panel1.Enabled = true;
                    panel2.Enabled = false;
                    panel3.Enabled = false;
                    ChooseInputImageWay = 1;
                    break;
                case "目录":
                    panel1.Enabled = false;
                    panel2.Enabled = true;
                    panel3.Enabled = false;
                    ChooseInputImageWay = 2;
                    break;
                case "相机":
                    panel1.Enabled = false;
                    panel2.Enabled = false;
                    panel3.Enabled = true;
                    ChooseInputImageWay = 3;
                    break;
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBoxFileName.ReadOnly)
                textBoxFileName.ReadOnly = false;
            else
                textBoxFileName.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图像文件(*.bmp;*.jpg;*png)|*.bmp;*.jpg;*png|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
                textBox1.Text = strFileName;
                frmBase.dataStore.imageAcquisitionData.InputImagePath = strFileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择文件目录";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string sPath = folder.SelectedPath;
                textBox2.Text = sPath;
                frmBase.dataStore.imageAcquisitionData.InputImageDirPath = sPath;
            }
        }

        private void FormImageAcquisition_VisibleChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
                 textBox1.Text = frmBase.dataStore.imageAcquisitionData.InputImagePath;
            if (radioButton2.Checked)
                 textBox2.Text = frmBase.dataStore.imageAcquisitionData.InputImageDirPath;

            //相机列表更新
            comboCamera.Items.Clear();
            for (int i = 0; i < cameraList.Length; i++)
            {           
                if (cameraList[i] != null)
                    comboCamera.Items.Add(cameraList[i]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmBase.dataStore.SaveData();                      //保存设置
            if(radioButton1.Checked) 
                 ReadImage(textBox1.Text);                     //读取图像
            if (radioButton2.Checked)
                 ReadDirImage(textBox2.Text);                   //读取图像
            if (frmBase.frmProcessManage.imageDefine.acqImage != null)
                 frmBase.frmMain.pictureBox1.Image = frmBase.frmProcessManage.imageDefine.acqImage.Bitmap;   //图像变量
            this.Hide(); 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        //读取图像    
        public bool ReadImage(string ImagePath)
        {
            try
            {
              frmBase.frmProcessManage.imageDefine.acqImage = CvInvoke.Imread(ImagePath);
                if (frmBase.frmProcessManage.imageDefine.acqImage != null)
                {
                    frmBase.frmMain.pictureBox1.Image = frmBase.frmProcessManage.imageDefine.acqImage.Bitmap;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;

            }
        }
        int num = 1;

        public bool ReadDirImage(string ImageDirPath)
        {
            string fileName="";
            string extName = comboPictureType.Text;
            try
            {
                if (checkBoxFile.Checked)
                {
                    fileName = ImageDirPath + "\\" + textBoxFileName + "." + extName;
                }
                else
                {
                    string[] dir = Directory.GetDirectories(ImageDirPath); //文件夹列表   
                    DirectoryInfo fdir = new DirectoryInfo(ImageDirPath);
                    FileInfo[] file = fdir.GetFiles();
                    //FileInfo[] file = Directory.GetFiles(path); //文件列表   
                    if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                    {
                        filesName = new string[file.Length];
                        int i = 0;
                        foreach (FileInfo f in file) //显示当前目录所有文件   
                        {
                            //挑出指定格式
                            if (f.Name.Contains(extName))
                            {
                                filesName[i] = ImageDirPath + "\\" + f.Name;
                                i++;
                            }
                        }
                    }
                }          
             
                frmBase.frmProcessManage.imageDefine.acqImage = CvInvoke.Imread(filesName[frmBase.frmProcessManage.executeNum]);
                if (frmBase.frmProcessManage.imageDefine.acqImage != null)
                {
                     frmBase.frmMain.pictureBox1.Image = frmBase.frmProcessManage.imageDefine.acqImage.Bitmap;
                    //frmBase.frmMain.pictureBox1.Refresh();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;

            }
        }

     

        private void FormImageAcquisition_Load(object sender, EventArgs e)
        {
         
        }
    }
}
