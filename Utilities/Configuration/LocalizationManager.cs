
//********************************************************
//建立日期:2016.03.15
//作者:litao
//內容说明:　本地化管理把配置以字典的方式存在内存中,方便快速提取。配置必须以xml形式配置。多语言（本地化）以此实现。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BinHong.Utilities
{
    /// <summary>
    /// 本地化管理
    /// </summary>
    public class LocalizationManager
    {
        /// <summary>
        /// 存放本地化字符的字典
        /// </summary>
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        //public string SelectedLanguage
        //{
        //    get { return (string)App.Instance.Parameter.GetParaData(ParameterNames.SelectedLanguage).Value; }
        //    set { App.Instance.Parameter.SetParamData(ParameterNames.SelectedLanguage, value); }
        //}

        /// <summary>
        /// 当前选择的语言
        /// </summary>
        public string SelectedLanguage { get; set; }

        /// <summary>
        /// 语言列表
        /// </summary>
        public List<string> LanguageList
        {
            get
            {
                return _languageList;
            }
        }
        private  readonly List<string>  _languageList = new List<string>();
        
        /// <summary>
        /// 读本地化文件。读指定的配置的文件
        /// </summary>
        /// <param name="filePath">指定的配置文件</param>
        private void ReadLocalizationFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            XmlDocument xmlDoc=new XmlDocument();
            xmlDoc.Load(filePath);

        }

        /// <summary>
        /// 读本地化文件。读指定的配置的文件
        /// </summary>
        /// <param name="filePaths">指定的配置文件</param>
        private void ReadLocalizationFiles(string filePaths)
        {
            string[] files=filePaths.Split('|');
            foreach (var file in files)
            {
                ReadLocalizationFile(file);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(string ownerDir)
        {
            try
            {
                string selectedLangFile = "";
                //读取Language.cfg，初始化语言列表。
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ownerDir + "Language.cfg");
                XmlNode node = xmlDoc.SelectSingleNode("LanguageList");
                if (node != null)
                {
                    if (SelectedLanguage == "UnKnown"
                        || SelectedLanguage == null)
                    {
                        string defaultLang = node.Attributes["Default"].Value;
                        SelectedLanguage = defaultLang;
                    }

                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        string langName = childNode.Attributes["Name"].Value;
                        LanguageList.Add(langName);
                        if (langName == SelectedLanguage)
                        {
                            selectedLangFile =ownerDir+"\\"+ childNode.Attributes["File"].Value;
                        }
                    }
                }

                //读取当前语言对应的文件，初始化_dictionary
                ReadLocalizationFiles(selectedLangFile);
            }
            catch (Exception ex)
            {
                string debugMsg = "";
#if DEBUG
                debugMsg = ex.Message + ex.StackTrace;
#endif
                RunningLog.Record(LogType.System, LogLevel.Error, debugMsg + "Language.cfg Load failed.");
            }
        }

        /// <summary>
        /// 根据指定的Key获取本地化字符串。根据指定的Key获取相应的值。如果没有这个KeyValue，就直接返回这个Key。
        /// </summary>
        /// <param name="key">指定的Key</param>
        /// <returns>相应的值。</returns>
        public string GetLocalizationString(string key)
        {
            if (_dictionary.ContainsKey(key))
            {
                return _dictionary[key];
            }
            return key;
        }
    }
}
