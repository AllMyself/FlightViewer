using System.IO;
using BinHong.Utilities;
using System.Collections.Generic;
using System;
using System.Text;

namespace BinHong.FlightViewerCore
{
    public class DataProcessModule : IComponent
    {
        private readonly Channe429Receive _receive429;

        private FileStream _fileStream;
        string path;

        public DataProcessModule(Channe429Receive receive429)
        {
            Owner = receive429;

            _receive429 = receive429;
            string mainDir = App.Instance.ApplicationDirectory + "\\ReceiveData\\";
            if (!File.Exists(mainDir))
            {
                Directory.CreateDirectory(mainDir);
            }
            string deviceDir = mainDir + ((AbstractDevice)(receive429.Owner)).Path + "\\";
            if (!File.Exists(deviceDir))
            {
                Directory.CreateDirectory(deviceDir);
            }
            path = deviceDir + receive429.Name;
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //_fileStream = new FileStream(deviceDir + receive429.Name, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        }
        //存数据
        public void Save(RxpA429 rxpA429, int rxpNum)
        {
            if (rxpNum > 0)
            {
                _fileStream = new FileStream(path, FileMode.Append);
                string str = rxpA429.data.ToString();
                Encoding encode = Encoding.UTF8;
                byte[] data = encode.GetBytes(str);
                _fileStream.Write(data, 0, data.Length);
                _fileStream.Flush();
                _fileStream.Close();
            }
        }
        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="percent">百分比</param>
        /// <returns></returns>
        public List<string> GetData(int position)
        {
            _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sReader = new StreamReader(_fileStream);
            List<string> list = new List<string>();
            List<string> returnList = new List<string>();
            string str;
            int count = 0;
            while ((str = sReader.ReadLine()) != null)
            {
                count++;
                list.Add(str);
            }
            sReader.Close();
            _fileStream.Close();
            int startLocation;
            int endLocation;
            if (position - 50 <= 0)
            {
                startLocation = 0;
            }
            else
            {
                startLocation = position - 50;
            }
            if (position + 50 >= count)
            {
                endLocation = count;
            }
            else
            {
                endLocation = position + 50;
            }
            for (int i = startLocation; i <= endLocation; i++)
            {
                if (list.Count > 0)
                {
                    returnList.Add(list[i]);
                }
            }
            return returnList;
        }
        public IOwner Owner { get; private set; }

        public void Dispose()
        {
            _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            _fileStream.Close();
        }
    }
}
