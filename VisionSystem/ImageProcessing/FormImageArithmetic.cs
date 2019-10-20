using Emgu.CV;
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
    public partial class FormImageArithmetic : Form
    {
        FormBase frmBase;
        string[] selectInputImage=new string[2];
        Mat imageShow;

        public FormImageArithmetic(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }

        private void FormImageArithmetic_VisibleChanged(object sender, EventArgs e)
        {
            ComboBoxUpdate();
            comboBox3.SelectedIndex = 0;
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

            this.comboBox2.Items.Clear();
            this.comboBox2.Items.AddRange(frmBase.frmProcessManage.ImageOptions);
        }

        private void showPicture()
        {
            try
            {
                if (frmBase.frmProcessManage.imageDefine.arithmeticeImag == null)
                {
                    if (comboBox3.Text == "输入图像1")
                        imageShow = frmBase.frmProcessManage.ImageChoose(selectInputImage[0]);

                    else if (comboBox3.Text == "输入图像2")
                        imageShow = frmBase.frmProcessManage.ImageChoose(selectInputImage[1]);
                }
                else
                {
                    if (comboBox3.Text == "输入图像3")
                        imageShow = frmBase.frmProcessManage.imageDefine.arithmeticeImag;
                }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectInputImage[0] = comboBox1.Text;//获取选择图像参数
            showPicture();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectInputImage[1] = comboBox2.Text;//获取选择图像参数
            showPicture();
        }
    }
}
