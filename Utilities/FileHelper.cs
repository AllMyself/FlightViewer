using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BinHong.Utilities
{
    public class FileHelper
    {
        public static string pathSend = @"\logs\" + DateTime.Today.ToString("yyyy-MM-dd") + "SendLog.txt";
        public static string pathReceive = @"\logs\" + DateTime.Today.ToString("yyyy-MM-dd") + "ReceiveLog.txt";
        public static void WriteLogForSend(string str)
        {
            WriteFile(pathSend, str);
        }
        public static void WriteLogForReceive(string str)
        {
            WriteFile(pathReceive, str);
        }
        /// <summary>  
        ///  写入文件  
        /// </summary>  
        /// <param name="filePath">文件名</param>  
        /// <param name="content">文件内容</param>  
        public static void WriteFile(string filePath, string content)
        {
            try
            {
                string path = System.Environment.CurrentDirectory;
                bool exist = Directory.Exists(path + @"\logs");
                if (!exist)
                {
                    Directory.CreateDirectory(path + @"\logs");
                }
                exist = File.Exists(path + filePath);
                if (!exist)
                {
                    File.Create(path + filePath).Close();
                }
                var fs = new FileStream(path + filePath, FileMode.Append);
                Encoding encode = Encoding.UTF8;
                //获得字节数组  
                content = DateTime.Now.ToString() + ":" + content + "\r\n";
                byte[] data = encode.GetBytes(content);
                //开始写入  
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流  
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ReceiveDataUse(string path, string context)
        {
            try
            {
                var fs = new FileStream(path, FileMode.Append);
                Encoding encode = Encoding.UTF8;
                //获得字节数组  
                context = DateTime.Now.ToString() + ":" + context + "\r\n";
                byte[] data = encode.GetBytes(context);
                //开始写入  
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流  
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
