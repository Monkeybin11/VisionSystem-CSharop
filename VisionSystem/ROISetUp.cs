using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace VisionSystem
{
    public class ROISetUp
    {
        #region 绘制矩形
        private Rectangle m_MouseRect = Rectangle.Empty;
        private bool m_MouseIsDown = false;
        int intwidth = 0;
        int intheigh = 0;
        float w = 0;//宽度比例
        float h = 0;//高度比例   
        private Control control;

        public Rectangle rect=new Rectangle();
        public bool bDrawOver=false;

        private delegate void SelectRectangel(object sneder, Rectangle e);
        private event SelectRectangel SetRectangel;

        public ROISetUp(PictureBox pb)
        {
            pb.Click += new EventHandler(this.SelectControl);
            control = pb;
        }

        public void RoiSet(PictureBox pb)
        {
           
        }

        private void SelectControl(object sender, EventArgs e)
        {
            if (control is Control)
            {
                //Remove event any pre-existing event handlers appended by this class
                control.MouseDown -= new MouseEventHandler(this.pb_MouseDown);
                control.MouseMove -= new MouseEventHandler(this.pb_MouseMove);
                control.MouseUp -= new MouseEventHandler(this.pb_MouseUp);
                control = null;
            }
            control = (Control)sender;
            //Add event handlers for moving the selected control around
            control.MouseDown += new MouseEventHandler(this.pb_MouseDown);
            control.MouseMove += new MouseEventHandler(this.pb_MouseMove);
            control.MouseUp += new MouseEventHandler(this.pb_MouseUp);
        }

 
        public void ClearEvent()
        {
            control.MouseDown -= new MouseEventHandler(this.pb_MouseDown);
            control.MouseMove -= new MouseEventHandler(this.pb_MouseMove);
            control.MouseUp -= new MouseEventHandler(this.pb_MouseUp);
            control.Refresh();
            control = null;

        }

        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            control.Refresh();
            bDrawOver = false;
            m_MouseIsDown = true;
            DrawStart(new Point(e.X, e.Y));
        }

        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MouseIsDown)
                ResizeToRectangle(new Point(e.X, e.Y));
        }

        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            w = (float)intwidth / (float)control.Width;
            h = (float)intheigh / (float)control.Height;
            Cursor.Clip = Rectangle.Empty;
            m_MouseIsDown = false;
            DrawRectangle();
            if (m_MouseRect.X == 0 || m_MouseRect.Y == 0 || m_MouseRect.Width == 0 || m_MouseRect.Height == 0)
            {
                //如果区域为0 就不执行委托 
            }
            else
            {
                if (SetRectangel != null)
                    SetRectangel(control, m_MouseRect);
            }
            DrawRectangle();
            ControlPaint.DrawReversibleFrame(control.RectangleToScreen(m_MouseRect), Color.Red, FrameStyle.Thick);
            //rect.X = (int)(m_MouseRect.X * ((float)2592 / control.Width));
            //rect.Y = (int)(m_MouseRect.Y * ((float)1944 / control.Height));
            //rect.Width = (int)(m_MouseRect.Width * ((float)2592 / control.Width));
            //rect.Height = (int)(m_MouseRect.Height * ((float)1944 / control.Height));
            
            rect.X = m_MouseRect.X;
            rect.Y = m_MouseRect.Y;
            rect.Width = m_MouseRect.Width;
            rect.Height = m_MouseRect.Height;

            DrawRectangle();
            bDrawOver = true;
        }

        /// <summary> 
        /// 刷新绘制 
        /// </summary> 
        /// <param name="p"></param> 
        private void ResizeToRectangle(Point p_Point)
        {
            DrawRectangle();
            m_MouseRect.Width = p_Point.X - m_MouseRect.Left;
            m_MouseRect.Height = p_Point.Y - m_MouseRect.Top;
            DrawRectangle();
        }

        /// <summary> 
        /// 绘制区域 
        /// </summary> 
        public void DrawRectangle()
        {
            Rectangle _Rect = control.RectangleToScreen(m_MouseRect);
            ControlPaint.DrawReversibleFrame(_Rect, Color.Blue, FrameStyle.Thick);
        }

        /// <summary> 
        /// 开始绘制 并且设置鼠标区域 
        /// </summary> 
        /// <param name="StartPoint">开始位置</param> 
        private void DrawStart(Point p_Point)
        {
            Cursor.Clip = control.RectangleToScreen(new Rectangle(0, 0, control.Width, control.Height));
            m_MouseRect = new Rectangle(p_Point.X, p_Point.Y, 0, 0);

        }
        #endregion
    }
}
