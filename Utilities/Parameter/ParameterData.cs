
//********************************************************
//建立日期:2015.11.28
//作者:litao
//內容说明:　定义一个参数对象。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;

namespace BinHong.Utilities
{
    /// <summary>
    /// 参数数据
    /// T值的类型
    /// </summary>
    public class ParameterData
    {
        private const char SplitChar = (char)1;

        /// <summary>
        /// 参数路径
        /// </summary>
        public string Path;

        /// <summary>
        /// 参数名
        /// </summary>
        public string Name;

        /// <summary>
        /// 参数描述。
        /// </summary>
        public string Description;

        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return _value; }
            set
            {
                object newValue=null;
                try
                {
                    newValue = Convert.ChangeType(value, ValueType);
                }
                catch (Exception e)
                {
                    if (ConvertFailed!=null)
                    {
                        ConvertFailed(this, value.ToString());
                        return;
                    }
                    throw new Exception(e.Message);
                }
                    
                if (_value != newValue)
                {
                    if (Verify != null)
                    {
                        if (!Verify(newValue))//验证没有通过
                        {
                            return;
                        }
                    }
                    _value = newValue;
                    //值被修改后
                    if (AfterModify != null)
                    {
                        AfterModify(this);
                    }
                }
            }
        }
        private object _value;

        /// <summary>
        /// 值的Type
        /// </summary>
        public Type ValueType;

        /// <summary>
        /// 参数范围验证
        /// </summary>
        public Func<object, bool> Verify;

        /// <summary>
        /// 修改之后的行为
        /// </summary>
        public static Action<ParameterData> AfterModify;

        /// <summary>
        /// 值转换失败行为
        /// </summary>
        public static Action<ParameterData,string> ConvertFailed;

        /// <summary>
        /// 起效时间
        /// </summary>
        public OnsetTime OnsetTime;

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", Path, SplitChar, ValueType, SplitChar, Value,
                SplitChar,Name,SplitChar, Description);
        }

        /// <summary>
        /// 把字符串转换成ParameterData对象。字符串中只包含Path，ValueType，Value，Name，Description等信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ParameterData Parse(string str)
        {
            string[] items = str.Split(SplitChar);
            if (items.Length != 5)
            {
                return null;
            }
            try
            {
                ParameterData param = new ParameterData();
                param.Path = items[0];
                param.ValueType = Type.GetType(items[1]);
                param.Value = items[2];
                param.Name = items[3];
                param.Description = items[4];
                return param;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 起效时间
    /// </summary>
    public enum OnsetTime
    {
        Now,
        Reboot
    }
}
