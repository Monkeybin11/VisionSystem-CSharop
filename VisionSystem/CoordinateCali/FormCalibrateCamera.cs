using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Xml;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.Util;

namespace VisionSystem
{
    public partial class FormCalibrateCamera : Form
    {
        private delegate void SetTextCallback(Control control, string text);    //delegate declaration

        #region declaring global variables
        private MCvPoint3D32f[][] object_corner;                                //points in world coordinate
        private PointF[][] corner_count;                                        //points in image coordinate
        //相机内参，替代IntrinsicCameraParameters
        // private IntrinsicCameraParameters intrinsicParam = new IntrinsicCameraParameters(5);        //camera intrinsic       
        public Matrix<double> DistortionCoeffs=new Matrix<double>(5,1);//畸变矩阵，5*1矩阵
        public Matrix<double> IntrinsicMatrix=new Matrix<double>(3,3);//摄像机内参3*3矩阵
        //相机外参 ,替代ExtrinsicCameraParameters    
        // private ExtrinsicCameraParameters[] extrinsicParams;            //camera extrinsic                              
      //  public Matrix<double>[] ExtrinsicMatrixs;   
        public Mat[] RotationVectors;   //旋转向量M*3,M为市场个数
        public Mat[] TranslationVectors;//位移向量M*3矩阵
                                        
        private Matrix<float> mapx = new Matrix<float>(height, width);        //mapping matrix
        private Matrix<float> mapy = new Matrix<float>(height, width);
        private MCvTermCriteria criteria = new MCvTermCriteria(100, 1e-5);  //最优迭代终止条件设定

        private VideoCapture  capture1;
        private const int width = 640;      //camera resolution
        private const int height = 480;
        private Size imageSize = new Size(width, height);
        private Size patternSize;           //corner pattern
        private int nPoints;                //number of corners
        private int nImage;                 //number of images which use to calibrate
        private float square;               //the actual size of square (mm)
        private bool captureInProcess;      //the process sign of camera
        private bool isCalibrating;         //the sign of calibrating
        private bool isCalibrated;          //the sign of calibrated
        Image<Bgr, byte> imageFrame1;
        Image<Gray, byte> grayFrame1;
        private bool isCorners;             //the var is ture when there is corners file in local

        Thread newThread;                   //thread of calibrating
        #endregion

     
        public FormCalibrateCamera()
        {
            InitializeComponent();
            controlsInit();
        }
        private void controlsInit()
        {
            Corners_Nx.Text = "7";
            Corners_Ny.Text = "7";
            Square_Size.Text = "20";
            Image_Count.Text = "10";
            radio_camera.Checked = true;

            isCalibrating = false;
            isCalibrated = false;
            captureInProcess = false;
            isCorners = false;

            Start_Calibrate.Enabled = false;
            Exit_Calibrate.Enabled = false;
        }
        private void ProcessFrame(object sender, EventArgs arg)
        {
            imageFrame1 =new Image<Bgr, byte>(capture1.QueryFrame().Bitmap);
            grayFrame1 = imageFrame1.Convert<Gray, byte>();
            if (!isCalibrating)
            {
                pictureBox1.Image = grayFrame1.ToBitmap();
            }
            if (isCalibrated)   //重映射
            {
                CvInvoke.Remap(grayFrame1, grayFrame1, mapx, mapy,Inter.Linear,BorderType.Wrap, new MCvScalar(0));
                pictureBox1.Image = grayFrame1.ToBitmap();
            }
            
        }

        /// <summary>
        /// camera Start and Stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            #region if capture is not created, create it now
            if (null == capture1)
            {
                try
                {
                    capture1 = new VideoCapture(0);
                    capture1.SetCaptureProperty(CapProp.FrameWidth, width);
                    capture1.SetCaptureProperty(CapProp.FrameHeight, height);
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
            #endregion

            if (null != capture1)
            {
                if (captureInProcess)
                {
                    button1.Text = "Start";
                    Application.Idle -= ProcessFrame;
                    Start_Calibrate.Enabled = false;
                    Exit_Calibrate.Enabled = false;
                }
                else
                {
                    button1.Text = "Stop";
                    Application.Idle += ProcessFrame;
                    Start_Calibrate.Enabled = true;
                    Exit_Calibrate.Enabled = true;
                }
                captureInProcess = !captureInProcess;
            }
        }

        /// <summary>
        /// Release sourse
        /// </summary>
        private void ReleaseData()
        {
            if (null != capture1)
            {
                capture1.Dispose();
            }
        }
        /// <summary>
        /// Initialize arrays
        /// </summary>
        public void cameraInitialize()
        {
            object_corner = new MCvPoint3D32f[nImage][];
            corner_count = new PointF[nImage][];
 
            //ExtrinsicMatrixs = new Matrix<double> [nImage];
            RotationVectors = new Mat[nImage];
            TranslationVectors = new Mat[nImage];

            for (int i = 0; i < nImage; i++)
            {
                object_corner[i] = new MCvPoint3D32f[nPoints];
                corner_count[i] = new PointF[nPoints];
            }
        }

        /// <summary>
        /// points in world coordinate
        /// </summary>
        /// <param name="corners3D">coordinate value</param>
        /// <param name="chessBoardSize">size of chessboard</param>
        /// <param name="nImages">number of images</param>
        /// <param name="squareSize">actual size of square</param>
        private void objectCorners3D(MCvPoint3D32f[][] corners3D, Size chessBoardSize, int nImages, float squareSize)
        {
            int currentImage, currentRow, currentCol;
            for (currentImage = 0; currentImage < nImages; currentImage++)
            {
                for (currentRow = 0; currentRow < chessBoardSize.Height; currentRow++)
                {
                    for (currentCol = 0; currentCol < chessBoardSize.Width; currentCol++)
                    {
                        int nPoint = currentRow * chessBoardSize.Width + currentCol;
                        corners3D[currentImage][nPoint].X = (float)currentCol * squareSize;
                        corners3D[currentImage][nPoint].Y = (float)currentRow * squareSize;
                        corners3D[currentImage][nPoint].Z = (float)0.0f;
                    }
                }
            }
        }

        /// <summary>
        /// corners detection
        /// </summary>
        /// <param name="chessboardImage">chessboard image</param>
        /// <param name="cornersDetected">corners value in image coordinate</param>
        /// <returns>
        /// return true if success
        /// </returns>
        private bool findCorners(ref Image<Gray, byte> chessboardImage, out PointF[] cornersDetected)
        {
            VectorOfPointF cornersPoint = new VectorOfPointF() ;
            cornersDetected = new PointF[nPoints];
            //【1】角点检测
             CvInvoke.FindChessboardCorners(chessboardImage, patternSize , cornersPoint, CalibCbType.AdaptiveThresh | CalibCbType.NormalizeImage);
            //1.输入的图像矩阵，必须是8 - bit灰度图或者彩色图像，在图像传入函数之前，一般经过灰度处理，还有滤波操作。 
            //2.内角点的size，表示方式是定义Size PatSize(m, n)，将PatSize作为参数传入。这里是内角点的行列数，不包括边缘角点行列数；行数和列数不要相同，这样的话函数会辨别出标定板的方向，如果行列数相同，那么函数每次画出来的角点起始位置会变化，不利于标定。 
            //3.存储角点的数组，一般用vector < vector < point2f >>
            //4.标志位，有默认值。 AdaptiveThresh：该函数的默认方式是根据图像的平均亮度值进行图像二值化，设立此标志位的含义是采用变化的阈值进行自适应二值化； 
            //NormalizeImage：在二值化之前，调用EqualizeHist()函数进行图像归一化处理；
            //FilterQuads：二值化完成后，函数开始定位图像中的四边形（这里不应该称之为正方形，因为存在畸变），这个标志设立后，函数开始使用面积、周长等参数来筛选方块，从而使得角点检测更准确更严格。 
            //FastCheck：快速检测选项，对于检测角点极可能不成功检测的情况，这个标志位可以使函数效率提升。 

            if (null == cornersPoint)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < cornersPoint.Size; i++)
                {
                    cornersDetected[i] = cornersPoint[i];
                }
                //【2】精确化角点位置
                CvInvoke.CornerSubPix(chessboardImage, cornersPoint,new Size(5, 5), new Size(-1,-1), criteria);
                //image：输入图像
                //corners：输入角点的初始坐标以及精准化后的坐标用于输出。
                //winSize：搜索窗口边长的一半，例如如果winSize = Size(5, 5)，则一个大小为的搜索窗口将被使用。
                //zeroZone：搜索区域中间的dead region边长的一半，有时用于避免自相关矩阵的奇异性。如果值设为(-1, -1)则表示没有这个区域。
                //criteria：角点精准化迭代过程的终止条件。也就是当迭代次数超过criteria.maxCount，或者角点位置变化小于criteria.epsilon时，停止迭代过程。
                //【3】绘制被成功标定的角点
                CvInvoke.DrawChessboardCorners(chessboardImage, patternSize, cornersPoint, false);             
                //image——输入的目标图像，必须是8位彩色图像
                //patternSize——内心的角落每棋盘的行和列的数目角，也就是标定板角点的行数和列数
                //corners——检测到的角阵列，也就是用cvFindCornerSubPix函数检测到的角点的坐标（cvFindCornerSubPix的第二个参数）
                //count——角数，每张标定图所有角点的数目
                //patternWasFound——指出是否已找到所有的角点（该值为0表示不能找到所有的角点，不为0则表示能够找出所有的角点），该参数值由cvFindChessboardCorners函数的返回值给出
                return true;
            }
        }

      

        /// <summary>
        /// negative image for observation
        /// </summary>
        /// <param name="bitmap">original image</param>
        /// <returns>negative image</returns>
        private Bitmap snap(Bitmap bitmap)
        {
            Bitmap bmp = new Bitmap(bitmap);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                 bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            for (int i = 0; i < bytes; i += 4)
            {
                rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = (byte)(255 - rgbValues[i]);
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            bmp.UnlockBits(bmpData);
            return bmp;
        }

        /// <summary>
        /// save corner file include objectPoints and imagePoints
        /// </summary>
        private void saveCorners()
        {
            //One xml file, for objectCorners in world coordinate,
            //and detected corners for chessboard coordinate.

            XmlDocument doc = new XmlDocument();
            XmlNode decl = doc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            doc.AppendChild(decl);

            XmlElement root = doc.CreateElement("", "Corners_Storage", "");
            doc.AppendChild(root);
            for (int i = 0; i < nImage; i++)
            {
                string image = "image" + (i + 1).ToString();
                XmlElement imageIndex = doc.CreateElement("", image, "");
                for (int n = 0; n < nPoints; n++)
                {
                    int point = i * nPoints + n;
                    string node = "node" + (point + 1).ToString();
                    XmlElement nodeIndex = doc.CreateElement("", node, "");
                    XmlElement data_1 = doc.CreateElement("data");
                    XmlElement data_2 = doc.CreateElement("data");
                    XmlElement data_3 = doc.CreateElement("data");
                    XmlElement data_4 = doc.CreateElement("data");
                    XmlElement data_5 = doc.CreateElement("data");

                    XmlText data1 = doc.CreateTextNode(Convert.ToString(object_corner[i][n].X));
                    XmlText data2 = doc.CreateTextNode(Convert.ToString(object_corner[i][n].Y));
                    XmlText data3 = doc.CreateTextNode(Convert.ToString(object_corner[i][n].Z));
                    XmlText data4 = doc.CreateTextNode(Convert.ToString(corner_count[i][n].X));
                    XmlText data5 = doc.CreateTextNode(Convert.ToString(corner_count[i][n].Y));

                    nodeIndex.AppendChild(data_1);
                    nodeIndex.AppendChild(data_2);
                    nodeIndex.AppendChild(data_3);
                    nodeIndex.AppendChild(data_4);
                    nodeIndex.AppendChild(data_5);
                    data_1.AppendChild(data1);
                    data_2.AppendChild(data2);
                    data_3.AppendChild(data3);
                    data_4.AppendChild(data4);
                    data_5.AppendChild(data5);
                    imageIndex.InsertAfter(nodeIndex, imageIndex.LastChild);
                }
                root.InsertAfter(imageIndex, root.LastChild);
            }

            try
            {
                doc.Save("DataCorners.xml");
                MessageBox.Show("Save Corners successful.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// save camera intrinsic
        /// </summary>
        private void saveIntrinsic()
        {
            XmlDocument doc = new XmlDocument();
            XmlWriterSettings xmlSetting = new XmlWriterSettings();
            xmlSetting.Encoding = new UTF8Encoding(false);
            xmlSetting.Indent = true;
            XmlWriter writer;

            writer = XmlWriter.Create("Intrinsic.xml", xmlSetting);
            writer.WriteStartElement("opencv_storage");
            writer.WriteStartElement("matrix_left");
            writer.WriteStartAttribute("type_id");
            writer.WriteValue("opencv-matrix");
            writer.WriteEndAttribute();
            for (int i = 0; i < IntrinsicMatrix.Width * IntrinsicMatrix.Height; i++)
            {
                writer.WriteStartElement("data");
                writer.WriteValue(IntrinsicMatrix[i / 3, i % 3]);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            //intrinsicParam.IntrinsicMatrix.WriteXml(writer);
            //writer.WriteEndElement();
            writer.Flush();
            //writer.WriteStartElement("Distoration");
            //intrinsicParam.DistortionCoeffs.WriteXml(writer);
            //writer.WriteEndElement();
            writer.WriteStartElement("distortion_left");
            writer.WriteStartAttribute("type_id");
            writer.WriteValue("opencv-matrix");
            writer.WriteEndAttribute();
            for (int i = 0; i < DistortionCoeffs.Rows; i++)
            {
                writer.WriteStartElement("data");
                writer.WriteValue(DistortionCoeffs[i, 0]);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();

            doc.Save(writer);
            writer.Close();

            MessageBox.Show("Save Intrinsic successful.");
        }

        /// <summary>
        /// camera calibrate
        /// </summary>
        /// <param name="camera"></param>
        public void  cameraCalibration(out Matrix<double> _intrinsicMatrix, out Matrix<double> _distortionCoeffs)
        {
            //【1】获取角点数据
            string text = this.Text;
            if (!isCorners)
            {
                cameraInitialize();

                objectCorners3D(object_corner, patternSize, nImage, square);

                int nCount = 0;
                int nFrame = 0;
                while (nCount < nImage)
                {
                    if (!radio_camera.Checked)
                    {
                        string filename = Environment.CurrentDirectory + "\\新建文件夹\\" + "chess" + (nCount + 1).ToString() + ".bmp";
                        // string filename =   (nCount + 1).ToString() + ".jpg";
                        grayFrame1 = new Image<Gray, byte>(filename);
                    }
                    pictureBox1.Image = grayFrame1.ToBitmap();

                    if (findCorners(ref grayFrame1, out corner_count[nCount]))
                    {
                        if (nFrame++ > 5)
                        {
                            nCount++;
                            nFrame = 0;
                        }
                        if (nCount > 0 && nFrame < 1)
                        {
                      //      pictureBox1.Image =  snap(grayFrame1.ToBitmap());
                            SetText(this, nCount.ToString() + "th image");
                            Thread.Sleep(1000);
                        }
                    }
                }
                SetText(this, text);
                saveCorners();
            }

            //【2】摄像机标定，计算相机内参（IntrinsicMatrix：1/dx、1/dy、r、u0、v0、f）、外参（旋转RotationVectors（ω、δ、 θ）、平移TranslationVectors（（Tx、Ty、Tz））） 
            //  double reprojection_error = singleCameraCalibration(object_corner, corner_count, imageSize, IntrinsicMatrix, DistortionCoeffs, RotationVectors, TranslationVectors);
            double reprojection_error = CvInvoke.CalibrateCamera(object_corner, corner_count, imageSize, IntrinsicMatrix, DistortionCoeffs,
             CalibType.FixK3, criteria, out RotationVectors, out TranslationVectors);
            //objectPoints ：世界坐标系中的点。在使用时，应该输入vector < vector < Point3f > >。             
            //imagePoints ：其对应的图像点。和objectPoints一样，应该输入vector<vector<Point2f>> 型的变量。
            //imageSize ：图像的大小，在计算相机的内参数和畸变矩阵需要用到；
            //cameraMatrix ：内参数矩阵。输入一个Mat cameraMatrix即可。
            //distCoeffs ：畸变矩阵。输入一个Mat distCoeffs即可。\
            //CalibType:
            //tremCriteria:迭代优化算法的终止准则
            //rotationVectors ：旋转向量；应该输入一个Mat的vector，即vector<Mat> rvecs因为每个vector< Point3f > 会得到一个rvecs。
            //translationVectors ：位移向量；和rvecs一样，也应该为vector tvecs。

            MessageBox.Show("重映射误差reprojection error: " + reprojection_error.ToString() + "\n" + "Camera Calibrated.");
      
            //【3】畸变矫正，计算畸变参数DistortionCoeffs（k1,k2,k3径向畸变系数，p1,p2是切向畸变系数）
            CvInvoke.Undistort(mapx, mapy, IntrinsicMatrix, DistortionCoeffs);
  
            //【4】标定结果评价,计算反投影，得到标定误差
            Matrix<double> rvec = new Matrix<double>(3, 1);
            Matrix<double> tvec = new Matrix<double>(3, 1);
            PointF[] points = CvInvoke.ProjectPoints(object_corner[0], rvec, tvec, IntrinsicMatrix, DistortionCoeffs);
            //【5】保存标定参数
            saveIntrinsic();
            _intrinsicMatrix = IntrinsicMatrix;
            _distortionCoeffs = DistortionCoeffs;
        }

        /// <summary>
        /// valid the control value which initialize camera param
        /// </summary>
        /// <returns>
        /// return false if invalid
        /// </returns>
        private bool validControls()
        {
            if ("" == Corners_Nx.Text || "" == Corners_Ny.Text ||
                "" == Square_Size.Text || "" == Image_Count.Text)
                return false;
            else
                return true;
        }

        /// <summary>
        /// start new thread for calibrate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Calibrate_Click(object sender, EventArgs e)
        {
            newThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.startCalibrate));
            newThread.Priority = ThreadPriority.Normal;
            newThread.IsBackground = false;
            newThread.Start();
        }

        private void startCalibrate()
        {
            if (validControls())
            {
                patternSize = new Size(Convert.ToInt16(Corners_Nx.Text), Convert.ToInt16(Corners_Ny.Text));
                nPoints = patternSize.Height * patternSize.Width;
                nImage = Convert.ToInt16(Image_Count.Text);
                square = (float)Convert.ToDouble(Square_Size.Text);

                isCalibrating = true;

                //开始标定
                cameraCalibration(out IntrinsicMatrix,out DistortionCoeffs);

                SetText(textBox1, IntrinsicMatrix[0, 0].ToString());
                SetText(textBox2, IntrinsicMatrix[1, 1].ToString());
                SetText(textBox3, IntrinsicMatrix[0, 2].ToString());
                SetText(textBox4, IntrinsicMatrix[1, 2].ToString());

                SetText(textBox5, DistortionCoeffs[0, 0].ToString());
                SetText(textBox6, DistortionCoeffs[1, 0].ToString());
                SetText(textBox7, DistortionCoeffs[4, 0].ToString());
                SetText(textBox8, DistortionCoeffs[2, 0].ToString());
                SetText(textBox9, DistortionCoeffs[3, 0].ToString());

                isCalibrating = false;
            }
            else
                MessageBox.Show("Please Input ChessBoard Params");
        }


        /// <summary>
        /// set value of textBox.Text, using control in different threads needs InvokeRequired attribute
        /// </summary>
        /// <param name="textBox">textBox name</param>
        /// <param name="text">string which will set in textBox</param>
        private void SetText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
        }

        /// <summary>
        /// exit calibrate and abort thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Calibrate_Click(object sender, EventArgs e)
        {
            isCalibrated = false;
            isCalibrating = false;
            if (null != newThread)
            {
                MessageBox.Show("Exit Calibrate");
                newThread.Abort();
            }
        }

        /// <summary>
        /// read corners if there is a corner file in local
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read_Corners_Click(object sender, EventArgs e)
        {
            if (validControls())
            {
                patternSize = new Size(Convert.ToInt16(Corners_Nx.Text), Convert.ToInt16(Corners_Ny.Text));
                nPoints = patternSize.Height * patternSize.Width;
                nImage = Convert.ToInt16(Image_Count.Text);
                square = (float)Convert.ToDouble(Square_Size.Text);

                cameraInitialize();

                XmlDocument doc = new XmlDocument();
                XmlReader reader;
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                reader = XmlReader.Create("DataCorners.xml", settings);
                doc.Load(reader);
                XmlNode xmlNode = doc.SelectSingleNode("Corners_Storage");
                if (null == xmlNode)
                {
                    MessageBox.Show("Load Corners Error.");
                    return;
                }
                else
                {
                    XmlNodeList imageList = xmlNode.ChildNodes;
                    int i = 0;
                    foreach (XmlNode imageIndex in imageList)
                    {
                        XmlNodeList nodeList = imageIndex.ChildNodes;
                        int j = 0;
                        foreach (XmlNode data in nodeList)
                        {
                            object_corner[i][j].X = Convert.ToSingle(data.ChildNodes[0].InnerText);
                            object_corner[i][j].Y = Convert.ToSingle(data.ChildNodes[1].InnerText);
                            object_corner[i][j].Z = Convert.ToSingle(data.ChildNodes[2].InnerText);
                            corner_count[i][j].X = Convert.ToSingle(data.ChildNodes[3].InnerText);
                            corner_count[i][j].Y = Convert.ToSingle(data.ChildNodes[4].InnerText);

                            j++;
                        }
                        i++;
                    }
                    MessageBox.Show("Load Corners successful.");
                }
                reader.Close();

                isCorners = true;

                Start_Calibrate.Enabled = true;
                Exit_Calibrate.Enabled = true;
            }
            else
                MessageBox.Show("Please Input ChessBoard Params");
        }

        /// <summary>
        /// mapping matrix
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectify_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.xml|*.xml";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                string str = dlg.FileName.Trim();
                reader = XmlReader.Create(str, settings);
                doc.Load(reader);
            }
            else
                return;

            //root node
            XmlNode xmlNode = doc.SelectSingleNode("opencv_storage");
            //chile node
            XmlNodeList xmlList = xmlNode.ChildNodes;
            foreach (XmlNode xmlnode in xmlList)
            {
                XmlNodeList xn = xmlnode.ChildNodes;
                //intrinsic and distortion param
                if ("matrix_left" == xmlnode.Name)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        IntrinsicMatrix[i / 3, i % 3] = Convert.ToDouble(xn.Item(i).InnerText);
                    }
                }
                else if ("distortion_left" == xmlnode.Name)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        DistortionCoeffs[i, 0] = Convert.ToDouble(xn.Item(i).InnerText);
                    }
                }
            }

            CvInvoke.Undistort(IntrinsicMatrix,DistortionCoeffs, mapx, mapy);

            isCalibrating = true;
            isCalibrated = true;

            SetText(textBox1, IntrinsicMatrix[0, 0].ToString());
            SetText(textBox2, IntrinsicMatrix[1, 1].ToString());
            SetText(textBox3, IntrinsicMatrix[0, 2].ToString());
            SetText(textBox4, IntrinsicMatrix[1, 2].ToString());

            SetText(textBox5, DistortionCoeffs[0, 0].ToString());
            SetText(textBox6, DistortionCoeffs[1, 0].ToString());
            SetText(textBox7, DistortionCoeffs[4, 0].ToString());
            SetText(textBox8, DistortionCoeffs[2, 0].ToString());
            SetText(textBox9, DistortionCoeffs[3, 0].ToString());
        }
    }
}
