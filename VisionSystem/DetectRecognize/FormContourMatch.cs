using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace VisionSystem
{
    public partial class FormContourMatch : Form
    {
        FormBase frmBase;
        string grayWay = "平均值";
        string selectInputImage = "";
        Mat imageShow;
        public FormContourMatch(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }

        private void FormContourMatch_VisibleChanged(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
    
}
