//********************************************************
//建立日期:2016.05.19
//作者:litao
//內容说明:　授权检测与激活

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using AuthorizationKey;
using BinHong.Utilities;

namespace BinHong.FlightViewerCore
{
    /// <summary>
    /// 激活器
    /// </summary>
    public class KeyAuthorization 
    {
        public KeyAuthorization(string dir)
        {
            _keyFilePath = dir + "fv.license";
        }

        /// <summary>
        /// Key文件的路径的
        /// </summary>
        private readonly string _keyFilePath;

        /// <summary>
        /// 截至日期
        /// </summary>
        public string DeadLine { get; private set; }

        /// <summary>
        /// 激活
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            if (!File.Exists(_keyFilePath))
            {
                return false;
            }
            string key = File.ReadAllText(_keyFilePath);
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            bool isSuccess = Check(key);
            //把当前时间写入到授权文件中。
            if (isSuccess)
            {
                string now = DateTime.Now.ToString("yyyyMMdd");
                KeyActivator activator = new KeyActivator();
                string originalKey = activator.GenerateNewKey(key, now);
                File.WriteAllText(_keyFilePath, originalKey);
            }
            
            return isSuccess;
        }

        private bool Check(string key)
        {
            string mac1 = "";
            string cpuId1 = "";
            string type1 = "";
            string deadLine1 = "";
            string produceDate1 = "";
            string md51 = "";
            string other = "";
            try
            {
                KeyActivator activator = new KeyActivator();
                List<string> contentList = activator.Activate(key);
                cpuId1 = contentList[0];
                mac1 = contentList[1];
                type1 = contentList[2];
                deadLine1 = contentList[3];
                produceDate1 = contentList[4];
                md51 = contentList[5];
                if (contentList.Count == 7)
                {
                    other = contentList[6];
                }
            }
            catch (Exception)
            {
                return false;
            }

            string md5Content1 = mac1 + cpuId1 + type1 + deadLine1 + produceDate1;
            SHA1CryptoServiceProvider sha1Cng = new SHA1CryptoServiceProvider();
            byte[] contentMd5Bytes1 = sha1Cng.ComputeHash(UtilityEncoding.Gb2312.GetBytes(md5Content1));
            string afreshMd5 = UtilityEncoding.Gb2312.GetString(contentMd5Bytes1);
            if (afreshMd5 != md51)
            {
                return false;
            }
            //读取截至日志
            DeadLine = deadLine1;
            IFormatProvider ifp = new CultureInfo("zh-CN", true);
            long deadLineTime = DateTime.ParseExact(DeadLine, "yyyyMMdd", ifp).Ticks;
            //读取生产日志
            string produceDate = produceDate1;
            long produceTime = DateTime.ParseExact(produceDate, "yyyyMMdd", ifp).Ticks;
            //读取授权文件中的上次启动时间。
            long lastStartedTime = produceTime;
            if (!string.IsNullOrWhiteSpace(other))
            {
                lastStartedTime = DateTime.ParseExact(other, "yyyyMMdd", ifp).Ticks;
            }
             
            //读取本机当前的时间。
            long curTime = DateTime.Now.Ticks;
            if (curTime < deadLineTime
                && produceTime < curTime
                && lastStartedTime < curTime
                && produceTime <= lastStartedTime)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 软件激活
        /// </summary>
        public bool Activate(string key)
        {
            if (!Check(key))
            {
                return false;
            }
            try
            {
                File.WriteAllText(_keyFilePath, key);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取当前注册的信息
        /// </summary>
        public void GetRegisterInfo(out string deadline, out string key)
        {
            deadline = "unregistered";
            key = "";
            if (!File.Exists(_keyFilePath))
            {
                return;
            }
            string content = File.ReadAllText(_keyFilePath);
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            string mac1 = "";
            string cpuId1 = "";
            string type1 = "";
            string deadLine1 = "";
            string nowDate1 = "";
            string md51 = "";
            try
            {
                KeyActivator activator = new KeyActivator();
                List<string> contentList = activator.Activate(content);
                key = activator.GetKey(content);
                cpuId1 = contentList[0];
                mac1 = contentList[1];
                type1 = contentList[2];
                deadLine1 = contentList[3];
                nowDate1 = contentList[4];
                md51 = contentList[5];
            }
            catch (Exception)
            {
                return;
            }
            deadline = deadLine1;
        }
    }
}
