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
    public partial class FormColorToGray : Form
    {
        FormBase frmBase;

        string grayWay = "平均值";
        string selectInputImage = "";
        Mat imageShow;



        public FormColorToGray(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }




        private void FormColorToGray_VisibleChanged(object sender, EventArgs e)
        {
            ComboBoxUpdate();

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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectInputImage = comboBox1.Text;//获取选择图像参数
            imageShow = frmBase.frmProcessManage.ImageChoose(selectInputImage);
            pictureBox1.Image = imageShow.Bitmap;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            grayWay = comboBox2.Text;  //彩色转灰方式参数
            showPicture();
        }

        //彩色转灰    
        public bool Bgr2Gray()
        {
            try
            {
                Mat mat = frmBase.frmProcessManage.ImageChoose(selectInputImage);  //获取选择图像
                Mat[] bgrMat  = mat.Split(); 
                switch (grayWay)
                {
                    case "蓝色通道":                     
                        frmBase.frmProcessManage.imageDefine.color2GrayImage = bgrMat[0];
                        break;
                    case "绿色通道": 
                        frmBase.frmProcessManage.imageDefine.color2GrayImage = bgrMat[1];
                        break;
                    case "红色通道":             
                        frmBase.frmProcessManage.imageDefine.color2GrayImage = bgrMat[2];
                        break;
                    case "灰度图":
                        CvInvoke.CvtColor(mat, frmBase.frmProcessManage.imageDefine.color2GrayImage, ColorConversion.Rgb2Gray);
                        break;
                }            
                return true;
            }
            catch(Exception e)
            {                
                return false;            
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            showPicture();
        }

        private void showPicture()
        {
             
            Bgr2Gray();
            imageShow = frmBase.frmProcessManage.imageDefine.color2GrayImage;
            if (checkBox1.Checked && imageShow != null)
            {
                pictureBox1.Image = imageShow.Bitmap;
                pictureBox1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SaveData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SaveData()
        {

        }
    }
}
