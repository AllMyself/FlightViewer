using System;
using System.Reflection;

namespace BinHong.Utilities
{
    /// <summary>
    /// 对象生成器
    /// </summary>
    public class ObjectGenerator
    {
        /// <summary>
        /// 采用反射机制创建一个对象
        /// </summary>
        /// <param name="dllName"></param>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        public static object Create(string dllName,string fullClassName)
        {
            //采用反射机制生成主窗口
            Assembly assembly = Assembly.LoadFrom(dllName);
            Type type = assembly.GetType(fullClassName);
            return Activator.CreateInstance(type);
        }
    }
}
