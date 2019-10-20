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
    public partial class FormGrayMatch : Form
    {
        FormBase frmBase;
        string grayWay = "平均值";
        string selectInputImage = "";
        Mat imageShow;
        Mat imageRoi;
        Mat tempalteImage;

        ROISetUp roiSetUp = null;

        public Rectangle rectRoi = new Rectangle();//图像ROI
        public Rectangle templateRegion = new Rectangle();//截取模板区域


        public FormGrayMatch(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;

            //隐藏 tabControl头部
            this.tabControlArea.Region = new Region(new RectangleF(this.tabPageRectangle.Left, this.tabPageRectangle.Top, this.tabPageRectangle.Width, this.tabPageRectangle.Height));
            HideTabPage();
        }
        public void HideTabPage()
        {
            //修改TabPage所有背景颜色，并隐藏tabpage
            foreach (TabPage c in this.tabControlArea.TabPages)
            {
                c.BackColor = Color.Gainsboro;
                c.Parent = null;
            }          
        }
        private void FormGrayMatch_VisibleChanged(object sender, EventArgs e)
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
            imageShow = frmBase.frmProcessManage.ImageChoose(selectInputImage);
       
            if (imageShow != null)
                pictureBox1.Image = imageShow.Bitmap;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HideTabPage();
            this.tabPageRectangle.Parent = this.tabControlArea;
            this.tabControlArea.SelectedTab = tabPageRectangle;

            if (roiSetUp == null)
            {
                roiSetUp = new ROISetUp(pictureBox1);
                roiSetUp.DrawRectangle();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (roiSetUp != null)
            {
                roiSetUp.ClearEvent();
                roiSetUp = null;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            roiSetUp = new ROISetUp(pictureBox1);
            roiSetUp.DrawRectangle();
        }

 
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                imageRoi = imageShow;
                tempalteImage = new Mat(imageRoi, templateRegion);   //ROI图像      
                pictureBox2.Image = tempalteImage.Bitmap;
                pictureBox2.Refresh();
                tempalteImage.Save("tempalteImage.bmp");
         
            }
            catch(Exception ex)
            { }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (roiSetUp != null)
            {
                roiSetUp.ClearEvent();
                roiSetUp = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (roiSetUp != null)
            {
                if (roiSetUp.bDrawOver)
                {
                    //鼠标坐标转换为在图像上的坐标
                    roiSetUp.rect.X = (int)(roiSetUp.rect.X*((float)imageShow.Width / pictureBox1.Width));
                    roiSetUp.rect.Y = (int)(roiSetUp.rect.Y*((float)imageShow.Height / pictureBox1.Height));
                    roiSetUp.rect.Width = (int)(roiSetUp.rect.Width * ((float)imageShow.Width / pictureBox1.Width));
                    roiSetUp.rect.Height = (int)(roiSetUp.rect.Height * ((float)imageShow.Height / pictureBox1.Height));

                    if (tabControl1.SelectedTab==tabPage1)  //在基本设置页时
                    {
                       

                        rectRoi = new Rectangle(roiSetUp.rect.Location, roiSetUp.rect.Size);
                        tbLeftX.Text = rectRoi.Location.X.ToString();
                        tbLeftY.Text = rectRoi.Location.Y.ToString();
                        tbWidth.Text = rectRoi.Width.ToString();
                        tbHeight.Text = rectRoi.Height.ToString();
                    }
                    if (tabControl1.SelectedTab == tabPage2)  //在参数设置页时
                    {
                        templateRegion = new Rectangle(roiSetUp.rect.Location, roiSetUp.rect.Size);
                    }
                    roiSetUp.bDrawOver = false;
                }
            }
        }


        public bool GrayMath()
        {
            try
            {
                Mat srcImage = frmBase.frmProcessManage.ImageChoose(selectInputImage);
                imageShow = srcImage;
                CvInvoke.Imwrite("photo\\src_image.bmp", srcImage);        
                 
                if (!rectRoi.IsEmpty)   //当设置了ROI时
                    srcImage = new Mat(srcImage, rectRoi); //ROI区域设置  
                Mat resultImage = new Mat(new Size(srcImage.Width - tempalteImage.Width + 1, srcImage.Height - tempalteImage.Height + 1), Emgu.CV.CvEnum.DepthType.Cv32F, 1);
                CvInvoke.MatchTemplate(srcImage, tempalteImage, resultImage, TemplateMatchingType.Sqdiff);
                //归一化结果                           
              //  CvInvoke.Normalize(resultImage, resultImage, 1, 0, NormType.MinMax);//把数据进行最大值255最小值0进行归一化
                double max = 0, min = 0;
                Point max_loc = new Point();
                Point min_loc = new Point();
                CvInvoke.MinMaxLoc(resultImage, ref min, ref max, ref min_loc, ref max_loc);//获得极值信息                   
                frmBase.frmProcessManage.imageDefine.grayMatchImage = srcImage;

                //平方差匹配法Sqdiff,SqdiffNormed，最好的匹配为0，值越大，匹配越差
                //相关匹配法，采用乘法操作，数值越大表明匹配程度越好,相关系数匹配法，1：完美的匹配，-1:最差的匹配
                if (min <0.5) 
                {
                    //在原图画出模板位置
                    CvInvoke.Rectangle(imageShow, new Rectangle(new Point(min_loc.X + rectRoi.X, min_loc.Y + rectRoi.Y), tempalteImage.Size), new MCvScalar(0, 0, 255), 3);//在找到位置画出矩形                                                                                                                                                                         //显示图像           
                    frmBase.controlSafeAccess.PictureBoxChange(frmBase.frmMain.pictureBox1, imageShow.Bitmap);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        

        private void button15_Click(object sender, EventArgs e)
        {
            GrayMath();
        }

        private void button14_Click(object sender, EventArgs e)
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
