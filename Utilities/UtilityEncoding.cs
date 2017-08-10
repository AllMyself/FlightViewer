using System.Text;

namespace BinHong.Utilities
{
    /// <summary>
    /// 编码
    /// </summary>
    public class UtilityEncoding
    {
        public static Encoding Gb2312
        {
            get
            {
                if (_gb2312 == null)
                {
                    _gb2312 = Encoding.GetEncoding("GB2312");
                }
                return _gb2312;
            }
        }

        private static Encoding _gb2312 = null;

        public static Encoding ASCII
        { get { return Encoding.ASCII; } }


        /// <summary>
        /// 根据字节数组的内容判断它可能使用的编码，用这种编码把字节数组转换成字符
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static string GetText(byte[] buff)
        {
            string strReslut;
            if (buff.Length > 3)
            {
                if (buff[0] == 239 && buff[1] == 187 && buff[2] == 191)
                {// utf-8
                    strReslut = Encoding.UTF8.GetString(buff);
                }
                else if (buff[0] == 254 && buff[1] == 255)
                {// big endian unicode
                    strReslut = Encoding.BigEndianUnicode.GetString(buff);
                }
                else if (buff[0] == 255 && buff[1] == 254)
                {// unicode
                    strReslut = Encoding.Unicode.GetString(buff);
                }
                else if (IsUtf8(buff))
                {// utf-8
                    strReslut = Encoding.UTF8.GetString(buff);
                }
                else
                {
                    //默认以gb2312处理
                    strReslut = Gb2312.GetString(buff);
                }
            }
            else
            {
                strReslut = Encoding.UTF8.GetString(buff);
            }
            return strReslut;
        }

        // 110XXXXX, 10XXXXXX
        // 1110XXXX, 10XXXXXX, 10XXXXXX
        // 11110XXX, 10XXXXXX, 10XXXXXX, 10XXXXXX
        private static bool IsUtf8(byte[] buff)
        {
            for (int i = 0; i < buff.Length; i++)
            {
                if ((buff[i] & 0xE0) == 0xC0)    // 110x xxxx 10xx xxxx
                {
                    if ((buff[i + 1] & 0x80) != 0x80)
                    {
                        return false;
                    }
                }
                else if ((buff[i] & 0xF0) == 0xE0)  // 1110 xxxx 10xx xxxx 10xx xxxx
                {
                    if ((buff[i + 1] & 0x80) != 0x80 || (buff[i + 2] & 0x80) != 0x80)
                    {
                        return false;
                    }
                }
                else if ((buff[i] & 0xF8) == 0xF0)  // 1111 0xxx 10xx xxxx 10xx xxxx 10xx xxxx
                {
                    if ((buff[i + 1] & 0x80) != 0x80 || (buff[i + 2] & 0x80) != 0x80 || (buff[i + 3] & 0x80) != 0x80)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
