using System;
using System.Net;

namespace BinHong.Utilities
{
    /// <summary>
    /// DataProcess定义的是对某些数据进行处理的方式
    /// </summary>
    public class UtilityDataProcess
    {
        //因为板卡上传的数据中，在处理后，有效数据后面马上跟0，但是再后面一般会有其他值，这些值虽然不影响显示，但会影响某些处理
        /// <summary>
        /// 这个方法作用是把byte[]中第一个0前面的数据保留，第一个0后面的就全部设置成0。byte[]的大小不变化
        /// </summary>
        /// <param name="bytes">要处理的byte数组</param>
        public static void SweepInvalidateByte(ref byte[] bytes)
        {
            int len = bytes.Length;
            bool isEnd = false;
            for (int i = 0; i < len; i++)
            {
                if (isEnd)
                {
                    bytes[i] = 0;
                }
                else
                {
                    if (bytes[i] == 0)
                    {
                        isEnd = true;
                    }
                }
            }
        }

        //因为板卡上传的数据中，在处理后，有效数据后面马上跟0，但是再后面一般会有其他值，这些值虽然不影响显示，但会影响某些处理
        /// <summary>
        /// 这个方法作用是把byte[]从startIndex开始的长度为length的数据中第一个0前面的数据保留，
        /// 第一个0后面的就全部设置成0。byte[]的大小不变化
        /// </summary>
        /// <param name="bytes">要处理的byte数组</param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        public static void SweepInvalidateByte(ref byte[] bytes, int startIndex, int length)
        {
            int len = startIndex + length;
            bool isEnd = false;
            for (int i = startIndex; i < len; i++)
            {
                if (isEnd)
                {
                    bytes[i] = 0;
                }
                else
                {
                    if (bytes[i] == 0)
                    {
                        isEnd = true;
                    }
                }
            }
        }

        /// <summary>
        /// 获取指定string的字节数
        /// </summary>
        /// <param name="str">指定string</param>
        /// <returns>字节数</returns>
        public static int GetStringSize(string str)
        {
            return UtilityEncoding.Gb2312.GetByteCount(str);
        }

        /// <summary>
        /// 转换简单结构体对象的字节顺序。
        /// 因为网上传输过来的结构体的值的顺序是网络字节顺序，需要将短值由网络字节顺序转换为主机字节顺序。
        /// 注意：对于byte[]数组变量会保持原来顺序的。//todo 是否需要支持？
        /// </summary>
        /// <param name="structObject">要转换的对象</param>
        public static void ReverseSimpleStructEndian<T>(ref T structObject) where T : class
        {
            if (structObject == null)
            {
                return;
            }
            Type structObjectType = structObject.GetType();
            //如果structObject是一个数组，structObject的GetFields()就为空。分别处理
            if (structObjectType.IsArray)
            {
                Array array = structObject as Array;
                for (int index = 0; index < array.Length; index++)
                {
                    object fieldValue = array.GetValue(index);

                    //如果是byte数组就直接break,不用进入递归。经测试，break后整个函数执行时间为0~1ms.不break
                    //执行时间是20~50ms.
                    if (fieldValue is byte)
                    {
                        break;
                    }
                    ReverseSimpleStructEndian(ref fieldValue);
                    array.SetValue(fieldValue, index);
                }
                structObject = array as T;
            }

            foreach (var field in structObjectType.GetFields())
            {
                object fieldValue = field.GetValue(structObject);
                if (fieldValue == null)
                {
                    continue;
                }
                Type fieldType = fieldValue.GetType();

                TypeCode typeCode = Type.GetTypeCode(fieldType);

                if (typeCode != TypeCode.Object)
                {
                    switch (typeCode)
                    {
                        case TypeCode.Boolean:
                            Console.WriteLine("C# Boolean not Support in this function");
                            return;
                        case TypeCode.SByte:
                            break;
                        case TypeCode.Byte:
                            break;
                        case TypeCode.Int16:
                            field.SetValue(structObject, IPAddress.NetworkToHostOrder(Convert.ToInt16(fieldValue)));
                            break;
                        case TypeCode.UInt16:
                            //fieldValue一定要使用与它对应的转换器。
                            //无符号数会大于有符号数，可以直接强制转换成有符号数，数据流不会在转换过程变化，eg：1111111，转化成无符号后还是为1111111。
                            //NetworkToHostOrder只支持有符号数。这里，我们不能把Convert.ToUInt16(fieldValue)强转成int，因为这样会凭空添加好多0
                            //转化后0全部跑到后面，就不能还原成原始数据了。
                            short shortValue = (short)Convert.ToUInt16(fieldValue);
                            field.SetValue(structObject, (UInt16)IPAddress.NetworkToHostOrder(shortValue));
                            break;
                        case TypeCode.Int32:
                            field.SetValue(structObject, IPAddress.NetworkToHostOrder(Convert.ToInt32(fieldValue)));
                            break;
                        case TypeCode.UInt32:
                            field.SetValue(structObject, (UInt32)IPAddress.NetworkToHostOrder((int)Convert.ToUInt32(fieldValue)));
                            break;
                        case TypeCode.Int64:
                            field.SetValue(structObject, IPAddress.NetworkToHostOrder(Convert.ToInt64(fieldValue)));
                            break;
                        case TypeCode.UInt64:
                            //todo 这里有问题。UInt64超过Int64的最大值，会导致ToInt64转换异常。
                            field.SetValue(structObject, (UInt64)IPAddress.NetworkToHostOrder(Convert.ToInt64(fieldValue)));
                            break;
                        case TypeCode.Single:
                            Console.WriteLine("C# String not Support in this function");
                            return;
                        case TypeCode.Double:
                            Console.WriteLine("C# String not Support in this function");
                            return;
                        case TypeCode.String:
                            Console.WriteLine("C# String not Support in this function");
                            return;
                        case TypeCode.Char:
                            Console.WriteLine("You should avoid to use C# Char in cross-platform environment.");
                            return;
                        default:
                            break;
                    }
                }
                else
                {
                    ReverseSimpleStructEndian(ref fieldValue);
                }
            }
        }
    }
}
