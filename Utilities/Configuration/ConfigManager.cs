
//********************************************************
//建立日期:2016.03.15
//作者:litao
//內容说明:　配置管理主要工作是读取配置到内存。
//          这些配置以字典的方式存在内存中,方便快速提取。其中包含多语言（本地化）。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace BinHong.Utilities
{
    /// <summary>
    /// 配置管理
    /// </summary>
    public class ConfigManager
    {
        private readonly LocalizationManager _localizationManager=new LocalizationManager();

        /// <summary>
        /// 普通配置
        /// </summary>
        private readonly Dictionary<string, object> _commonConfigs = new Dictionary<string, object>(); 

        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionNo { get; set; }

        /// <summary>
        /// 读取版本配置
        /// </summary>
        private void ReadVersionConfig(string versionFile)
        {
            VersionNo = "0.0.1";

            try
            {
                //读取versionFile
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(versionFile);
                XmlNode versionNode = xmlDoc.SelectSingleNode("Version");
                if (versionNode != null)
                {
                    bool canAddBuildTime = false;
                    if (versionNode.Attributes != null)
                    {
                        canAddBuildTime = versionNode.Attributes["Type"].Value == "Test";
                    }
                    VersionNo = versionNode.Attributes["No"].Value;
                    if (canAddBuildTime)
                    {
                        XmlNode buildTimeNode = versionNode.SelectSingleNode("BuildTime");
                        string buildTime = buildTimeNode.Attributes["Time"].Value;
                        if (!string.IsNullOrEmpty(buildTime))
                        {
                            VersionNo = VersionNo + "(" + buildTime + ")";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string debugMsg = "";
#if DEBUG
                debugMsg = ex.Message + ex.StackTrace;
#endif
                RunningLog.Record(LogType.System, LogLevel.Error, debugMsg + "Version.cfg Load failed.");
            }
        }

        /// <summary>
        /// 读取普通配置
        /// </summary>
        private void ReadCommonConfig(string configFile)
        {
            try
            {
                //读取configFile
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(configFile);
                RecurseRead(xmlDoc.ChildNodes[1].ChildNodes, xmlDoc.ChildNodes[1].Attributes["Name"].Value);
            }
            catch (Exception ex)
            {
                string debugMsg = "";
#if DEBUG
                debugMsg = ex.Message + ex.StackTrace;
#endif
                RunningLog.Record(LogType.System, LogLevel.Error, debugMsg + "Common.cfg Load failed.");
            }
        }

        /// <summary>
        /// 循环读取
        /// </summary>
        private void RecurseRead(XmlNodeList nodeList,string parentName)
        {
            foreach (XmlNode childNode in nodeList)
            {
                if (childNode.Name == "CommonPara")
                {
                    try
                    {
                        string name = childNode.Attributes["Name"].Value;
                        string typeString = childNode.Attributes["Type"].Value;
                        string valueString = childNode.Attributes["Value"].Value;

                        Type type = Type.GetType(typeString);
                        object value = Convert.ChangeType(valueString, type);
                        _commonConfigs.Add(parentName+"_"+name, value);
                    }
                    catch (Exception ex)
                    {
                        string debugMsg = "";
#if DEBUG
                        debugMsg = ex.Message + ex.StackTrace;
#endif
                        RunningLog.Record(LogType.System, LogLevel.Error, debugMsg + "Common.cfg Load failed.");
                    }
                }
                else if (childNode.Name == "ParameterCategory")
                {
                    RecurseRead(childNode.ChildNodes, parentName + "_" + childNode.Attributes["Name"].Value);
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(string applicationDirectory)
        {
            //本地化的初始化
            _localizationManager.Initialize(applicationDirectory + "Config\\LangConfig\\");

            //版本信息中添加编译时间的功能。
            ReadVersionConfig(applicationDirectory + "Config\\Version.cfg");
            
            //读取普通配置
            ReadCommonConfig(applicationDirectory + "Config\\Common.cfg");
        }

        /// <summary>
        /// 获取指定名称的参数
        /// </summary>
        public object GetParameter(string name)
        {
            if (!_commonConfigs.ContainsKey(name))
            {
                return null;
            }
            return _commonConfigs[name];
        }

        /// <summary>
        /// 获取某一参数类型下的所有参数名列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetParameterCategory(string name)
        {
            List<string> keys=_commonConfigs.Keys.ToList();
            return keys.Where(item => item.Contains(name)).ToList();
        }
    }
}
