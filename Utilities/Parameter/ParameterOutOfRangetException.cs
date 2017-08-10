//********************************************************
//建立日期:2015.11.28
//作者:litao
//內容说明:　定义参数输入超出范围的异常

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Runtime.Serialization;
using System.Security;

namespace BinHong.Utilities
{
    /// <summary>
    /// 参数超出范围异常
    /// </summary>
    [Serializable]
    public class ParameterOutOfRangetException : ArgumentOutOfRangeException
    {
        private string _max;
        private string _min;
        private string _value;
        private readonly string _message;

        public ParameterOutOfRangetException()
        {
            this._message = string.Empty;
            this._min = "0.0";
            this._max = "0.0";
            this._value = "0.0";
        }

        public ParameterOutOfRangetException(object value, string min, string max)
        {
            this._message = string.Format("值{0}不在{1}~{2}范围", value, min, max);
        }

        public ParameterOutOfRangetException(object value, string min, string max, string message)
        {
            this._message = string.Format("{0} 值{1}不在{2}~{3}范围", message, value, min, max);
        }

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public string Min
        {
            get { return this._min; }
            set { this._min = value; }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public string Max
        {
            get { return this._max; }
            set { this._max = value; }
        }

        /// <summary>
        /// 当前值
        /// </summary>
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        /// <summary>
        /// Exception
        /// </summary>
        public override string Message
        {
            get { return this._message; }
        }
    }
}
