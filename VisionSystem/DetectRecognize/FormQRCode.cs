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
using ZXing;
using ZXing.QrCode;

namespace VisionSystem
{
    public partial class FormQRCode : Form
    {
        FormBase frmBase;
 
        string selectInputImage = "";
        Mat imageShow;


        public FormQRCode(FormBase _frmBase)
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectInputImage = comboBox1.Text;//获取选择图像参数
            showPicture();
        }
        private void showPicture()
        {
            try
            {
                imageShow = frmBase.frmProcessManage.ImageChoose(selectInputImage);
                pictureBox1.Image = imageShow.Bitmap;
                pictureBox1.Refresh();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public bool readQRCode(out string QRText)
        {
            QRText = null;
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            try
            {
                Result result = reader.Decode(imageShow.Bitmap);
                QRText = result.Text;
                return true;
            }
            catch
            {
                return false;
            }
               
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string str="";
            dataGridView1.Rows.Clear();
            readQRCode(out str);
            int index =  dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = "1";
            dataGridView1.Rows[index].Cells[1].Value = str;

        }

        private void FormQRCode_VisibleChanged(object sender, EventArgs e)
        {
            ComboBoxUpdate();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Hide();
            SaveData();
        }
        private void SaveData()
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
