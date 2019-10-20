using Emgu.CV;
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
    public partial class FormColorExtraction : Form
    {
        string selectInputImage;
        Mat imageShow;

        FormBase frmBase;
        public FormColorExtraction(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
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

        private void FormColorExtraction_VisibleChanged(object sender, EventArgs e)
        {
            ComboBoxUpdate();
            label5.Text = trackBar1.Value.ToString();
            label6.Text = trackBar2.Value.ToString();
            label7.Text = trackBar3.Value.ToString();
            label8.Text = trackBar4.Value.ToString();
            label9.Text = trackBar5.Value.ToString();
            label10.Text = trackBar6.Value.ToString();
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
                if (frmBase.frmProcessManage.imageDefine.colorExtractionImage == null)
                {
                    imageShow = frmBase.frmProcessManage.ImageChoose(selectInputImage);
                }
                else
                    imageShow = frmBase.frmProcessManage.imageDefine.colorExtractionImage;
                if (imageShow != null)
                {
                    pictureBox1.Image = imageShow.Bitmap;
                    pictureBox1.Refresh();
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ColorExtract();
                showPicture();
            }
            label5.Text = trackBar1.Value.ToString();

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ColorExtract();
                showPicture();
            }
            label6.Text = trackBar2.Value.ToString();

        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ColorExtract();
                showPicture();
            }
            label7.Text = trackBar3.Value.ToString();

        }
        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ColorExtract();
                showPicture();
            }
            label8.Text = trackBar4.Value.ToString();
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ColorExtract();
                showPicture();
            }
            label9.Text = trackBar5.Value.ToString();

        }

        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ColorExtract();
                showPicture();
            }
            label10.Text = trackBar6.Value.ToString();

        }
        public bool ColorExtract()
        {
            try
            {
                Image<Bgr, byte> srcImage = new Image<Bgr, byte>(frmBase.frmProcessManage.ImageChoose(selectInputImage).Bitmap);  //获取选择图像
                double blue_min = trackBar1.Value;
                double blue_max = trackBar2.Value;
                double green_min = trackBar3.Value;
                double green_max = trackBar4.Value;
                double red_min = trackBar5.Value;
                double red_max = trackBar6.Value;
                Bgr min = new Bgr(blue_min, green_min, red_min);//黄色的最小值，允许一定的误差。
                Bgr max = new Bgr(blue_max, green_max, red_max);//黄色的最大值，允许一定的误差。
                Image<Gray, byte> result = srcImage.InRange(min, max);//进行颜色提取。
                frmBase.frmProcessManage.imageDefine.colorExtractionImage = result.Mat;
                return true;
            }
            catch(Exception ex)
            {
                return false;
              
               
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SaveData();
        }
        public void SaveData()
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
