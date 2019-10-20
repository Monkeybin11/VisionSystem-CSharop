using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CloudTerraceAssy
{
    public partial class DataManager
    {
        
        public DataManager(FormBase _formbase)
        {

        }
        public DataManager(String _modelName = "")
        {

        }
        #region 读写数据
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, String filePath);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(String section, String key, String val, String filePath);

        public bool  FindAndCreateDirectory(string DirectoryPath)
        {
            if (DirectoryPath.Length==0)
                return false;
            if (!Directory.Exists(DirectoryPath))
            {
                try
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                catch
                {
                    MessageBox.Show("Create directory fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        public bool DeleteDirectory(string DirectoryPath)
        {
            if (DirectoryPath.Length==0)
                return false;
            if (Directory.Exists(DirectoryPath))
            {
                try
                {
                    Directory.Delete(DirectoryPath);
                }
                catch
                {
                    MessageBox.Show("Delete directory fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        public bool FindAndCreateFile(string filePath)
        {
            if (filePath.Length==0)
                return false;
            FileInfo info=new FileInfo(filePath);
            if (!info.Exists)
            {
                try
                {
                    info.Create();
                }
                catch
                {
                    MessageBox.Show("Create file fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        public bool DeleteFile(string filePath)
        {
            if (filePath.Length==0)
                return false;
            FileInfo info = new FileInfo(filePath);
            if (!info.Exists)
            {
                try
                {
                    info.Delete();
                }
                catch
                {
                    MessageBox.Show("Delete file fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        public string GetModulePath()
        {
            string strDir="";
            strDir=Environment.CurrentDirectory;
            return strDir;
        }
        public double GetDouble(string szMain,string str,string szFileName)
        {
            string strret="";
            StringBuilder temp = new StringBuilder(10);
            GetPrivateProfileString(szMain, str, ("0.0"), temp, 10, szFileName);
            strret=temp.ToString();
            return Convert.ToDouble(strret);
            
        }
        public float GetFloat(string szMain,string str,string szFileName)
        {
	        string strret="";
            StringBuilder temp = new StringBuilder(10);
            GetPrivateProfileString(szMain,str,("0.0"),temp,10,szFileName);
            strret = temp.ToString();
            return (float)Convert.ToDouble(strret);
        }
        public int GetInt(string szMain, string str, string szFileName)
        {
            string strret = "";
            StringBuilder temp = new StringBuilder(10);
            GetPrivateProfileString(szMain, str, ("0"), temp, 10, szFileName);
            strret = temp.ToString();
            return Convert.ToInt32(strret);
        }
        public string GetString(string szMain, string str, string szFileName)
        {
            string strret = "";
            StringBuilder temp = new StringBuilder(10);
            GetPrivateProfileString(szMain, str, ("error"), temp, 256, szFileName);
            strret = temp.ToString();
            return strret;
        }
        public long SetValue(string szMain,string str,string sr,string szFileName)
        {
	        return(WritePrivateProfileString(szMain,sr,str,szFileName));
        }
        public long SetValue(string szMain, double value, string sr, string szFileName)
        {
            string str="";
            str = Convert.ToString(value);
            return (WritePrivateProfileString(szMain, sr, str, szFileName));
        }
        public long SetValue(string szMain, float value, string sr, string szFileName)
        {
            string str = "";
            str = Convert.ToString(value);
            return (WritePrivateProfileString(szMain, sr, str, szFileName));
        }
        public long SetValue(string szMain, int value, string sr, string szFileName)
        {
            string str = "";
            str = Convert.ToString(value);
            return (WritePrivateProfileString(szMain, sr, str, szFileName));
        }
        public long SetValue(string szMain, uint value, string sr, string szFileName)
        {
            string str = "";
            str = Convert.ToString(value);
            return (WritePrivateProfileString(szMain, sr, str, szFileName));
        }
        #endregion
    }
}
