using System.Xml;

namespace BinHong.Utilities
{
    public static class XmlAttributeCollectionExtend
    {
        /// <summary>
        /// 是否包含了指定名称的属性
        /// </summary>
        /// <param name="xmlAttributeCollection"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static bool Contain(this XmlAttributeCollection xmlAttributeCollection, string attributeName)
        {
            foreach (XmlAttribute itemAttribute in xmlAttributeCollection)
            {
                if (itemAttribute.Name == attributeName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
