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
                string str = Convert.ToString(rxpA429.data, 2);
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
        public List<string> GetData(string data, string sdi, string ssm, int pageIndex, out int pageCount)
        {
            _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sReader = new StreamReader(_fileStream);
            List<string> list = new List<string>();
            List<string> returnList = new List<string>();
            string str;
            int count = 0;
            while ((str = sReader.ReadLine()) != null)
            {
                bool isContainData = false;
                bool isContainSdi = false;
                bool isContainSsm = false;
                if (string.IsNullOrEmpty(data))
                {
                    isContainData = str.Contains(data);
                }
                if (string.IsNullOrEmpty(sdi))
                {
                    isContainSdi = str.Contains(sdi);
                }
                if (string.IsNullOrEmpty(ssm))
                {
                    isContainSsm = str.Contains(ssm);
                }
                if (isContainData && isContainSdi && isContainSsm)
                {
                    count++;
                    list.Add(str);
                }
                else if (isContainData && isContainSdi)
                {
                    count++;
                    list.Add(str);
                }
                else if (isContainData && isContainSsm)
                {
                    count++;
                    list.Add(str);
                }
                else if (isContainSdi && isContainSsm)
                {
                    count++;
                    list.Add(str);
                }
                else if (isContainData)
                {
                    count++;
                    list.Add(str);
                }
                else if (isContainSdi)
                {
                    count++;
                    list.Add(str);
                }
                else if (isContainSsm)
                {
                    count++;
                    list.Add(str);
                }
            }
            pageCount = count / 50;
            sReader.Close();
            _fileStream.Close();
            int startIndex = 0;
            int endIndex = 0;
            if (pageIndex - 1 <= 0)
            {
                startIndex = 0;
            }
            else
            {
                startIndex = pageIndex - 1;
            }
            if (pageIndex + 1 > pageCount)
            {
                endIndex = pageCount;
            }
            else
            {
                endIndex = pageIndex;
            }
            for (int i = startIndex * 50; i < endIndex * 50; i++)
            {
                if (list.Count > 0 && !string.IsNullOrEmpty(list[i]))
                {
                    returnList.Add(list[i]);
                }
            }
            #region 无用


            //int startLocation;
            //int endLocation;
            //if (position - 50 <= 0)
            //{
            //    startLocation = 0;
            //}
            //else
            //{
            //    startLocation = position - 50;
            //}
            //if (position + 50 >= count)
            //{
            //    endLocation = count;
            //}
            //else
            //{
            //    endLocation = position + 50;
            //}
            //for (int i = startLocation; i <= endLocation; i++)
            //{
            //    if (list.Count > 0)
            //    {
            //        returnList.Add(list[i]);
            //    }
            //}

            #endregion
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
