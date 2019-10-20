using Emgu.CV.UI;
using System.Drawing;
using System.Windows.Forms;

namespace VisionSystem
{
    public class ControlSafeAccess
    {
        FormBase frmBase;

        public ControlSafeAccess(FormBase _frmBase)
        {
            frmBase = _frmBase;
        }

        //跨线程设置控件comboBox的值
        public delegate void SetcommbDelegate(ComboBox cb);
        public void commbSet(ComboBox cb)
        {
            if (cb.InvokeRequired)
            {
                SetcommbDelegate dt = new SetcommbDelegate(commbSet);
                cb.Invoke(dt, new object[] { cb });
            }
            else
            {
                cb.Text = "test";
            }
        }

        //跨线程读取控件ComboBox的值，并返回
        public delegate string GetcommbDelegate(ComboBox cb);
        public string commbGet(ComboBox cb)
        {
            if (cb.InvokeRequired)
            {
                //GetcommbDelegate dt = new GetcommbDelegate(commbGet);
                //IAsyncResult ia = cb.BeginInvoke(dt, new object[] { cb });
                //return (string)cb.EndInvoke(ia);  //这里需要利用EndInvoke来获取返回值
                return (string)cb.Invoke(new GetcommbDelegate(commbGet), cb);
            }
            else
            {
                return cb.Text;
            }
        }

        delegate void dTextBoxChange(TextBox Tb, string str);
        public void TextBoxChange(TextBox Tb, string str)
        {
            if (Tb.InvokeRequired)
            {
                dTextBoxChange call = new dTextBoxChange(TextBoxChange);
                Tb.Invoke(call, Tb, str);
            }
            else
            {
                Tb.Text = str;
            }
        }


        delegate void dButtonTextChange(Button bt, string str);
        public void ButtonTextChange(Button bt, string str)
        {
            if (bt.InvokeRequired)
            {
                dButtonTextChange call = new dButtonTextChange(ButtonTextChange);
                bt.Invoke(call, bt, str);
            }
            else
            {
                bt.Text = str;
            }
        }

        delegate void dButtonColorChange(Button bt, Color cl);
        public void ButtonColorChange(Button bt, Color cl)
        {
            if (bt.InvokeRequired)
            {
                dButtonTextChange call = new dButtonTextChange(ButtonTextChange);
                bt.Invoke(call, bt, cl);
            }
            else
            {
                bt.BackColor = cl;
            }
        }

        delegate void dImageBoxChange(ImageBox ib, Emgu.CV.IImage image);
        public void ImageBoxChange(ImageBox ib, Emgu.CV.IImage image)
        {
            if (ib.InvokeRequired)
            {
                dImageBoxChange call = new dImageBoxChange(ImageBoxChange);
                ib.Invoke(call, ib, image);
            }
            else
            {
                ib.Image = image;
            }
        }

        delegate void dImageBoxRefresh(ImageBox ib);
        public void ImageBoxRefresh(ImageBox ib)
        {
            if (ib.InvokeRequired)
            {
                dImageBoxRefresh call = new dImageBoxRefresh(ImageBoxRefresh);
                ib.Invoke(call, ib);
            }
            else
            {
                ib.Refresh();
            }
        }

        delegate void dPictureBox(PictureBox pb, Image image);
        public void PictureBoxChange(PictureBox pb, Image image)
        {
            if (pb.InvokeRequired)
            {
                dPictureBox call = new dPictureBox(PictureBoxChange);
                pb.Invoke(call, pb, image);
            }
            else
            {
                pb.Image = image;
            }
        }

        delegate void dPictureBoxRefresh(PictureBox pb);
        public void PictureBoxRefresh(PictureBox pb)
        {
            if (pb.InvokeRequired)
            {
                dPictureBoxRefresh call = new dPictureBoxRefresh(PictureBoxRefresh);
                pb.Invoke(call, pb);
            }
            else
            {
                pb.Refresh();
            }
        }

        delegate void dLabelChange(Label lb, string str);
        public void LabelChange(Label lb, string str)
        {
            if (lb.InvokeRequired)
            {
                dLabelChange call = new dLabelChange(LabelChange);
                lb.Invoke(call, lb, str);
            }
            else
            {
                lb.Text = str;
            }
        }

        delegate void dRichTextBoxChange(RichTextBox rtb, string str);
        public void RichTextBoxChange(RichTextBox rtb, string str)
        {
            if (rtb.InvokeRequired)
            {
                dRichTextBoxChange call = new dRichTextBoxChange(RichTextBoxChange);
                rtb.Invoke(call, rtb, str);
            }
            else
            {
                rtb.Text = str;
            }
        }

    }
}
