using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem
{
    public partial class FormSaveImage : Form
    {
        FormBase frmBase;
        public FormSaveImage(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择文件目录";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string sPath = folder.SelectedPath;
                textBox1.Text = sPath;
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
        private void comboBox1_VisibleChanged(object sender, EventArgs e)
        {
            ComboBoxUpdate();
        }
        private void button5_Click(object sender, EventArgs e)
        {


            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //保存图像    
        public bool SaveImage(string ImagePath)
        {
            string imagePath = textBox1.Text;      
            string imageName = textBox2.Text;
            if (!Directory.Exists(imagePath))
                Directory.CreateDirectory(imagePath);
            Mat savedMat=new Mat();
            if(comboBox1.Text.Contains("采集图像"))
                 savedMat =  frmBase.frmProcessManage.imageDefine.acqImage;
            if (comboBox1.Text.Contains("彩色转灰"))
                savedMat = frmBase.frmProcessManage.imageDefine.color2GrayImage;
            if (comboBox1.Text.Contains("预先处理"))
                savedMat = frmBase.frmProcessManage.imageDefine.preImage;
            try
            {
                return CvInvoke.Imwrite(imagePath+"\\" + imageName + ".jpg", savedMat);
            }
            catch
            {
                return false;
            }
        }
    }
}
