using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionSystem
{
    public struct ImageAcquisitionData
    {
        public string InputImagePath;
        public string InputImageDirPath;
        public bool AcqureWayOption ;
        public ImageAcquisitionData(string str="")
        {
            InputImagePath = "";
            InputImageDirPath = "";
            AcqureWayOption = true;
        }


    }

    public class DataStore: DataManager
    {
        string strFilePath;
        public ImageAcquisitionData imageAcquisitionData;

        public  DataStore (string _modelName = "")
            : base(_modelName)
        {
            InitCalibrationDataa();
        }
        public void End()
        {
            // cali_data.Clear();
        }
        public void InitCalibrationDataa()
        {
            string strDirectoryPath = GetModulePath() + "\\数据";
            strFilePath = strDirectoryPath + "\\1.dat";

            if (FindAndCreateDirectory(strDirectoryPath))
            {
                if (!FindAndCreateFile(strFilePath))
                    return;
            }
            else
                return;
        }
   
        public void SaveData()
        {
             SetValue("图像处理", "InputImagePath", imageAcquisitionData.InputImagePath, strFilePath);
             SetValue("图像处理", "InputImageDirPath", imageAcquisitionData.InputImageDirPath, strFilePath);
        }
    }
}
