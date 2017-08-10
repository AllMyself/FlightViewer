using System;
using System.Collections.Generic;

namespace BinHong.Utilities
{
    //todo 不知道Phone这名字取得怎么样
    /// <summary>
    /// 电话。主要用于绑定数据和界面元素
    /// </summary>
    public class Phone
    {
        private readonly string _address;
        private string _remoteAddress;

        public Phone(string address)
        {
            _address = address;
        }

        /// <summary>
        /// callFrontend字典
        /// </summary>
        private static readonly Dictionary<string, Action<string, object>> CallEventDic = new Dictionary<string, Action<string, object>>();

        /// <summary>
        /// 呼叫某人
        /// </summary>
        /// <param name="address">呼叫的电话地址</param>
        /// <param name="path">呼叫的属性路径</param>
        /// <param name="value">呼叫的内容</param>
        public static void Call(string address, string path, object value)
        {
            if (CallEventDic.ContainsKey(address))
            {
                CallEventDic[address].Invoke(path, value);
            }
        }

        /// <summary>
        /// 注册地址以及起响应
        /// </summary>
        public static void RegisterAddressAndAction(string address, Action<string, object> action)
        {
            if (!CallEventDic.ContainsKey(address))
            {
                CallEventDic.Add(address, action);
            }
        }

        /// <summary>
        /// 移除地址以及起响应
        /// </summary>
        /// <param name="address"></param>
        public static void RemoveAddressAndAction(string address)
        {
            if (CallEventDic.ContainsKey(address))
            {
                CallEventDic.Remove(address);
            }
        }

        /// <summary>
        /// 连接两个电话
        /// </summary>
        public static void ConnectTwoPhone(Phone phoneA, Phone phoneB)
        {
            phoneA._remoteAddress = phoneB._address;
            phoneB._remoteAddress = phoneB._address;
        }
    }

    /// <summary>
    /// 电话预定义信息
    /// </summary>
    public static class PhonePredefinedMsg
    {
        /// <summary>
        /// 显示ShowErrorMsg信息
        /// </summary>
        public readonly static string ShowErrorMsg = "ShowErrorMsg";

        /// <summary>
        /// 显示ShowWarningMsg信息
        /// </summary>
        public readonly static string ShowWarningMsg = "ShowWarningMsg";

        /// <summary>
        /// 显示ShowInfoMsg信息
        /// </summary>
        public readonly static string ShowInfoMsg = "ShowInfoMsg";

    }
}
