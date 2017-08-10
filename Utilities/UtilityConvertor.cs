//********************************************************
//建立日期:2015.11.10
//作者:litao
//內容说明:　定义了常用的工具方法，用于简化代码。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace BinHong.Utilities
{
    /// <summary>
    /// Convertor实现的是把一种数据类型转换成另一种数据类型
    /// </summary>
    public class UtilityConvertor
    {
        /// <summary>
        /// 把对象转换成byte数组
        /// </summary>
        /// <param name="structObject">bytes数组</param>
        /// <returns>byte数组</returns>
        public static byte[] StructToBytes<T>(T structObject)
        {
            int size = Marshal.SizeOf(structObject);
            byte[] bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structObject, structPtr, true);
            Marshal.Copy(structPtr, bytes, 0, size);
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }

        /// <summary>
        /// 把一个byte数组转换成对象
        /// </summary>
        /// <param name="bytes">bytes数组</param>
        /// <returns>对象</returns>
        public static T BytesToStruct<T>(byte[] bytes)
        {
            Type type = typeof (T);
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length)
            {
                return default(T);
            }

            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, structPtr, size);
            T structObj = (T) Marshal.PtrToStructure(structPtr, type);
            Marshal.FreeHGlobal(structPtr);
            return structObj;
        }

        /// <summary>
        /// 把一个byte数组转换成对象
        /// </summary>
        public static T BytesToStruct<T>(byte[] bytes,int index)
        {
            Type type = typeof(T);
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length - index)
            {
                return default(T);
            }

            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, index, structPtr, size);
            T structObj = (T)Marshal.PtrToStructure(structPtr, type);
            Marshal.FreeHGlobal(structPtr);
            return structObj;
        }

        /// <summary>
        /// 把string转换为byte数组
        /// </summary>
        /// <param name="str">要转换的string</param>
        /// <returns>转换成的byte数组</returns>
        public static byte[] StringToByteArray(string str)
        {
            return UtilityEncoding.Gb2312.GetBytes(str);
        }

        /// <summary>
        /// byte数组转化成string类型
        /// </summary>
        /// <param name="bytes">要转化的数组</param>
        /// <param name="sweepInvalidData">是否清除第一个\0后面的数据</param>
        /// <returns>转化成的字符串</returns>
        public static string ByteArrayToString(Byte[] bytes, bool sweepInvalidData = true)
        {
            if (sweepInvalidData)
            {
                UtilityDataProcess.SweepInvalidateByte(ref bytes);
            }

            string outPutString = UtilityEncoding.Gb2312.GetString(bytes, 0, bytes.Length);
            return outPutString.TrimEnd('\0');
        }

        /// <summary>
        /// 字节数组转化为16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static StringBuilder ByteToHexStr(byte[] bytes, int startIndex, int length)
        {
            StringBuilder returnStr = new StringBuilder(); //初始化一个StringBuilder
            if (bytes != null)
            {
                for (int i = startIndex; i < (startIndex + length); i++)
                {
                    returnStr.Append(bytes[i].ToString("X2") + ",");
                    if (i == bytes.Length - 1)
                    {
                        returnStr.Append(bytes[i].ToString("X2"));
                    }
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 字节数组转化为16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static StringBuilder ByteToHexStr(byte[] bytes)
        {
            StringBuilder returnStr = new StringBuilder(); //初始化一个StringBuilder
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr.Append(bytes[i].ToString("X2") + ",");
                    if (i == bytes.Length - 1)
                    {
                        returnStr.Append(bytes[i].ToString("X2"));
                    }
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 字节数组转化为16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ValueByteToHexStrWithSpace(byte[] bytes)
        {
            StringBuilder returnStr = new StringBuilder(); //初始化一个StringBuilder
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr.Append(bytes[i].ToString("X2") + " ");
                }
            }
            return returnStr.ToString();
        }

        /// <summary>
        /// 符号的byte数组转换16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string SymbolByteToHexStrWithSpace(byte[] bytes)
        {
            var stringbuilder = new StringBuilder();
            for (int index = 0; index < bytes.Length; index++)
            {
                byte item = bytes[index];
                stringbuilder.Append((char) item);
                if (index%2 == 1)
                {
                    stringbuilder.Append(' ');
                }
            }
            return stringbuilder.ToString();
        }


        /// <summary>
        /// byte数组转化成string类型
        /// </summary>
        /// <param name="bytes">要转化的数组</param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="sweepInvalidData">是否清除第一个\0后面的数据</param>
        /// <returns>转化成的字符串</returns>
        public static string ByteArrayToString(Byte[] bytes, int startIndex, int length, bool sweepInvalidData = true)
        {
            if (sweepInvalidData)
            {
                UtilityDataProcess.SweepInvalidateByte(ref bytes, startIndex, length);
            }
            string outPutString = UtilityEncoding.Gb2312.GetString(bytes, startIndex, length);
            return outPutString.TrimEnd('\0');
        }

        /// <summary>
        ///从byte数组指定索引处读取指定长度的byte数据，把其转换成字符串，并且把其中第一个'\0'后的数据替换成空格
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="curBytesIndex">指定索引</param>
        /// <param name="dataLegth">指定长度</param>
        /// <returns>转换成的字符串</returns>
        public static string BytesToString(byte[] bytes, ref int curBytesIndex, int dataLegth)
        {
            UtilityDataProcess.SweepInvalidateByte(ref bytes, curBytesIndex, dataLegth);
            string curText = UtilityEncoding.Gb2312.GetString(bytes, curBytesIndex, dataLegth);
            //替换\0为空格
            curText = curText.Replace('\0', ' ');

            curBytesIndex += dataLegth;
            return curText;
        }

        /// <summary>
        /// 可逆的Byte数组转化到StringBuilder
        /// </summary>
        /// <returns></returns>
        public static StringBuilder ReversibleByteArrayToString(Byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length);
            foreach (byte bt in bytes)
            {
                stringBuilder.Append(bt.ToString("000"));
            }
            return stringBuilder;
        }

        /// <summary>
        /// 可逆的StringBuilder数组转化到Byte
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ReversibleStringByteToArray(string str)
        {
            byte[] outputBytes = new byte[str.Length/3];
            for (int i = 0; i < str.Length; i += 3)
            {
                outputBytes[i/3] = byte.Parse(str.Substring(i, 3));
            }
            return outputBytes;
        }

        /// <summary>
        /// 字节数组转换16进制.
        /// <remarks>
        /// 我认为Hex和byte[]一样,没有大小端的问题.由Hex(或byte[])转换为int等才会有大小端问题
        /// </remarks>
        /// </summary>
        public static string BytesToHex(byte[] bytes)
        {
            StringBuilder strBuilder = new StringBuilder(bytes.Length*2);
            foreach (byte bt in bytes)
            {
                strBuilder.AppendFormat("{0:X2}", bt);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// 16进制转换字节数组。
        /// <remarks>
        /// 我认为Hex和byte[]一样,没有大小端的问题.由Hex(或byte[])转换为int等才会有大小端问题
        /// </remarks>
        /// </summary>
        public static byte[] HexToBytes(string hexString)
        {
            int bytesLen = hexString.Length/2;
            byte[] bytes = new byte[bytesLen];
            for (int i = 0; i < bytesLen; i++)
            {
                byte btvalue = Convert.ToByte(hexString.Substring(i*2, 2), 16);
                bytes[i] = btvalue;
            }
            return bytes;
        }


        /// <summary>
        /// 字节数组转换小端int的String
        /// </summary>
        public static string BytesToLittleEndianIntString(byte[] bytes)
        {
            //我们设定MemoryUint中值以小端放置,如果本系统是大端就要转一下
            if (!BitConverter.IsLittleEndian)
            {
                bytes = bytes.Reverse().ToArray();
            }

            int value = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                value |= (bytes[i] & 0xFF) << (bytes.Length - 1 - i)*8;
            }

            return ((uint) value).ToString();
        }

        /// <summary>
        /// 从byte数组指定索引处读取2个字节的数据，把其转化为short.并且将值由网络字节顺序转换为主机字节顺序。
        /// 对于ushort也是调用这个方法。因为IPAddress.NetworkToHostOrder只有对short的操作，ushort还是要转换成short才能进行。
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="index">指定索引</param>
        /// <returns>转化为的ushort</returns>
        public static short BytesToHostOrderShort(byte[] bytes, ref int index)
        {
            return IPAddress.NetworkToHostOrder(BytesToShort(bytes, ref index));
        }

        /// <summary>
        /// 从byte数组指定索引处读取2个字节的数据，把其转换为short.
        /// 对于ushort也是调用这个方法。因为short和ushort的转换数据是不会丢失的。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static short BytesToShort(byte[] bytes, ref int index)
        {
            const int len = sizeof (short);
            short value = 0;
            if (index >= bytes.Length)
            {
                return 0;
            }

            if (index + len > bytes.Length)
            {
                int diffLen = bytes.Length - index;
                byte[] leftBytes = new byte[len];
                Array.Copy(bytes, index, leftBytes, 0, diffLen);

                value = BitConverter.ToInt16(leftBytes, 0);
            }
            else
            {
                value = BitConverter.ToInt16(bytes, index);
            }

            index += len;

            return value;
        }

        /// <summary>
        /// 从byte数组指定索引处读取4个字节的数据，把其转化为int.并且将值由网络字节顺序转换为主机字节顺序。
        /// 对于Uint也是调用这个方法。因为IPAddress.NetworkToHostOrder只有对int的操作，uint还是要转换成int才能进行。
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="index">指定索引</param>
        /// <returns>转化为的int</returns>
        public static int BytesToHostOrderInt(byte[] bytes, ref int index)
        {
            return IPAddress.NetworkToHostOrder(BytesToInt(bytes, ref index));
        }

        /// <summary>
        /// 从byte数组指定索引处读取4个字节的数据，把其转换为int.
        /// 对于Uint也是调用这个方法。因为int和uint的转换数据是不会丢失的。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int BytesToInt(byte[] bytes, ref int index)
        {
            const int len = sizeof (int);
            int value = 0;
            if (index >= bytes.Length)
            {
                return 0;
            }

            if (index + len > bytes.Length)
            {
                int diffLen = bytes.Length - index;
                byte[] leftBytes = new byte[len];
                Array.Copy(bytes, index, leftBytes, 0, diffLen);

                value = BitConverter.ToInt32(leftBytes, 0);
            }
            else
            {
                value = BitConverter.ToInt32(bytes, index);
            }

            index += len;

            return value;
        }

        /// <summary>
        /// 从byte数组指定索引处读取8个字节的数据，把其转化为long.并且将值由网络字节顺序转换为主机字节顺序。
        /// 对于ulong也是调用这个方法。因为IPAddress.NetworkToHostOrder只有对long的操作，ulong还是要转换成long才能进行。
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="index">指定索引</param>
        /// <returns>转化为的long</returns>
        public static long BytesToHostOrderLong(byte[] bytes, ref int index)
        {
            return IPAddress.NetworkToHostOrder(BytesToLong(bytes, ref index));
        }

        /// <summary>
        /// 从byte数组指定索引处读取8个字节的数据，把其转换为long.
        /// 对于uLong也是调用这个方法。因为Long和uLong的转换数据是不会丢失的
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static long BytesToLong(byte[] bytes, ref int index)
        {
            const int len = sizeof (long);
            long value = 0;
            if (index >= bytes.Length)
            {
                return 0;
            }

            if (index + len > bytes.Length)
            {
                int diffLen = bytes.Length - index;
                byte[] leftBytes = new byte[len];
                Array.Copy(bytes, index, leftBytes, 0, diffLen);

                value = BitConverter.ToInt64(leftBytes, 0);
            }
            else
            {
                value = BitConverter.ToInt64(bytes, index);
            }

            index += len;

            return value;
        }

        /// <summary>
        /// 把bytes中从指定索引index开始长度为Len的数据转换为主机顺序。
        /// </summary>
        public static byte[] NetworkToHostOrder(byte[] bytes, ref int index, int len)
        {
            byte[] newBytes = new byte[len];
            int endIndex = index + len - 1;
            for (int i = 0; i < len; i++)
            {
                newBytes[i] = bytes[endIndex - i];
            }
            index += len;

            return newBytes;
        }

        /// <summary>
        /// 把网络Bytes设置到HostBytes中，设置的过程中把网络字节转换成主机字节,并且把networkStartedIndex向前移动len。
        /// </summary>
        public static void SetNetworkBytesToHostBytes(byte[] hostBytes, int hostStartedIndex, byte[] networkBytes,
            ref int networkStartedIndex, int len, bool isBigEndian)
        {
            if (!isBigEndian)
            {
                Array.Copy(networkBytes, networkStartedIndex, hostBytes, hostStartedIndex, len);
            }
            else
            {
                int hostEndIndex = hostStartedIndex + len - 1;
                for (int i = 0; i < len; i++)
                {
                    int curNetworkIndex = networkStartedIndex + i;
                    int curHostIndex = hostEndIndex - i;
                    hostBytes[curHostIndex] = networkBytes[curNetworkIndex];
                }
            }

            networkStartedIndex += len;
        }

        /// <summary>
        /// 验证字符串是否是16进制数
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static bool MatchHex(string hexString)
        {
            const string matchStr = @"^(0(x|X))?(\d|[a-f]|[A-F]){1,8}$";
            return Regex.IsMatch(hexString, matchStr);
        }

        /// <summary>
        /// 验证字符串是否是数字
        /// </summary>
        /// <param name="intString"></param>
        /// <returns></returns>
        public static bool MatchNumber(string intString)
        {
            const string matchStr = @"^(\d)+$";
            return Regex.IsMatch(intString, matchStr);
        }

        /// <summary>
        /// 验证Ip字符串是否符合Ip的规则
        /// </summary>
        /// <param name="ipString"></param>
        /// <returns></returns>
        public static bool MatchIp(string ipString)
        {
            const string num = @"(2[0-4]\d|25[0-5]|[0-1]?\d?\d)";
            const string matchStr = "^" + num + "\\." + num + "\\." + num + "\\." + num + "$";
            return Regex.IsMatch(ipString, matchStr);
        }

        /// <summary>
        /// Ip的string转换成int形式.
        /// <remarks>
        /// 注意使用前请保证ip的String的格式正确
        /// </remarks>
        /// </summary>
        /// <param name="ipString"></param>
        /// <returns></returns>
        public static int IpStringToInt(string ipString)
        {
            char[] separator = new char[] {'.'};
            string[] items = ipString.Split(separator);
            return int.Parse(items[0]) << 24
                   | int.Parse(items[1]) << 16
                   | int.Parse(items[2]) << 8
                   | int.Parse(items[3]);
        }

        /// <summary>
        /// Ip的int转换成string形式.
        /// </summary>
        /// <param name="ipInt"></param>
        /// <returns></returns>
        public static string IpIntToString(int ipInt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((ipInt >> 24) & 0xFF).Append(".");
            sb.Append((ipInt >> 16) & 0xFF).Append(".");
            sb.Append((ipInt >> 8) & 0xFF).Append(".");
            sb.Append(ipInt & 0xFF);
            return sb.ToString();
        }

        /// <summary>
        /// 修正多个空白符成一个空白符
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string ModifyMutilSpaceToOneSpace(string line)
        {
            const char splitChar = ' ';

            //把多个连续空格修改成单个空格.
            bool isEmptySegment = false;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in line)
            {
                if (c == splitChar)
                {
                    isEmptySegment = true;
                }
                else
                {
                    if (isEmptySegment)
                    {
                        stringBuilder.Append(splitChar);
                        isEmptySegment = false;
                    }

                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 以控制台运行命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="initDir"></param>
        /// <returns></returns>
        public static string RunCommand(string command, string initDir)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WorkingDirectory = initDir;
            p.Start();
            return p.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 假设int以小端存储,计算出它的值
        /// </summary>
        public static int CalculateBySmallEndian(int intValue)
        {
            byte[] bytes = StructToBytes(intValue);
            int value = bytes[0] + bytes[1]*256 + bytes[2]*65536 + bytes[3]*16777216;
            return value;
        }

        /// <summary>
        /// 假设int以大端存储,计算出它的值
        /// </summary>
        public static int CalculateByBigEndian(int intValue)
        {
            byte[] bytes = StructToBytes(intValue);
            int value = bytes[3] + bytes[2] * 256 + bytes[1] * 65536 + bytes[0] * 16777216;
            return value;
        }
    }
}
