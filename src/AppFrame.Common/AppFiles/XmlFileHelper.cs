using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppFrame.Common.AppFiles
{
    public static class XmlFileHelper
    {
        /// <summary>
        /// 加载XML文件
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <param name="groupNodeName">参数组名称</param>
        /// <returns></returns>
        public static IEnumerable<XElement> LoadXml(string xmlPath, string groupNodeName)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentException($"“{nameof(xmlPath)}”不能为 null 或空白。", nameof(xmlPath));
            }

            if (string.IsNullOrWhiteSpace(groupNodeName))
            {
                throw new ArgumentException($"“{nameof(groupNodeName)}”不能为 null 或空白。", nameof(groupNodeName));
            }
            if (!File.Exists(xmlPath)) throw new Exception($"{xmlPath}文件不存在");
            var xel = XElement.Load(xmlPath);
            return xel.Descendants(groupNodeName).Elements();
        }
        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="elements"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="groupNodeName"></param>
        /// <param name="parameterNodeName"></param>
        /// <returns></returns>
        public static bool CreateXml(string xmlPath, List<XElement> elements, string rootNodeName, string categoryNodeName, string groupNodeName, string parameterNodeName)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentException($"“{nameof(xmlPath)}”不能为 null 或空白。", nameof(xmlPath));
            }

            if (string.IsNullOrWhiteSpace(rootNodeName))
            {
                throw new ArgumentException($"“{nameof(rootNodeName)}”不能为 null 或空白。", nameof(rootNodeName));
            }

            if (string.IsNullOrWhiteSpace(categoryNodeName))
            {
                throw new ArgumentException($"“{nameof(categoryNodeName)}”不能为 null 或空白。", nameof(categoryNodeName));
            }

            if (string.IsNullOrWhiteSpace(groupNodeName))
            {
                throw new ArgumentException($"“{nameof(groupNodeName)}”不能为 null 或空白。", nameof(groupNodeName));
            }

            if (string.IsNullOrWhiteSpace(parameterNodeName))
            {
                throw new ArgumentException($"“{nameof(parameterNodeName)}”不能为 null 或空白。", nameof(parameterNodeName));
            }

            //创建XML
            XDocument xdoc = null;
            if (!File.Exists(xmlPath))
                xdoc = new XDocument { Declaration = new XDeclaration("1.0", "utf-8", "yes") };
            else
                xdoc = XDocument.Load(xmlPath);
            //根节点
            var rootXle = xdoc.Element(rootNodeName);
            if (rootXle == null)
            {
                rootXle = new XElement(rootNodeName);
                xdoc.Add(rootXle);
            }
            //参数类别节点
            var categoryXle = rootXle.Element(categoryNodeName);
            if (categoryXle == null)
            {
                categoryXle = new XElement(categoryNodeName);
                rootXle.Add(categoryXle);
            }
            //参数组节点
            var groupXle = categoryXle.Element(groupNodeName);
            if (groupXle == null)
            {
                groupXle = new XElement(groupNodeName);
                categoryXle.Add(groupXle);
            }
            //参数节点
            XElement parameterXle = null;
            parameterXle = groupXle.Element(parameterNodeName);
            if (parameterXle == null)
            {
                parameterXle = new XElement(parameterNodeName);
                groupXle.Add(parameterXle);
            }
            foreach (var item in elements)
            {
                //具体参数
                if (parameterXle.Element(item.Name) == null)
                    parameterXle.Add(item);
                else
                    parameterXle.Element(item.Name).Value = item.Value;
            }
            xdoc.Save(xmlPath);
            return true;
        }
        /// <summary>
        /// 删除指定的节点
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="groupNodeName"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static bool RemoveNode(string xmlPath, string groupNodeName, string nodeName)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentException($"“{nameof(xmlPath)}”不能为 null 或空白。", nameof(xmlPath));
            }

            if (string.IsNullOrWhiteSpace(groupNodeName))
            {
                throw new ArgumentException($"“{nameof(groupNodeName)}”不能为 null 或空白。", nameof(groupNodeName));
            }

            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException($"“{nameof(nodeName)}”不能为 null 或空白。", nameof(nodeName));
            }
            XDocument xdoc = null;
            if (!File.Exists(xmlPath))
                throw new Exception($"{xmlPath}文件不存在");
            xdoc = XDocument.Load(xmlPath);
            var node = xdoc.Descendants(groupNodeName).Elements().Where(e => e.Name.LocalName.Equals(nodeName));
            node.Remove();
            xdoc.Save(xmlPath);
            return true;
        }
        /// <summary>
        /// 重命名节点
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="groupNodeName"></param>
        /// <param name="oldNodeName"></param>
        /// <param name="newNodeName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static bool RenameNode(string xmlPath, string groupNodeName, string oldNodeName, string newNodeName)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentException($"“{nameof(xmlPath)}”不能为 null 或空白。", nameof(xmlPath));
            }

            if (string.IsNullOrWhiteSpace(groupNodeName))
            {
                throw new ArgumentException($"“{nameof(groupNodeName)}”不能为 null 或空白。", nameof(groupNodeName));
            }

            if (string.IsNullOrWhiteSpace(oldNodeName))
            {
                throw new ArgumentException($"“{nameof(oldNodeName)}”不能为 null 或空白。", nameof(oldNodeName));
            }

            if (string.IsNullOrWhiteSpace(newNodeName))
            {
                throw new ArgumentException($"“{nameof(newNodeName)}”不能为 null 或空白。", nameof(newNodeName));
            }

            XDocument xdoc = null;
            if (!File.Exists(xmlPath))
                throw new Exception($"{xmlPath}文件不存在");
            xdoc = XDocument.Load(xmlPath);
            var res0 = xdoc.Descendants(groupNodeName);
            var res = res0.Elements().FirstOrDefault(e => e.Name.LocalName.Equals(oldNodeName));
            res.Name = newNodeName;          
            xdoc.Save(xmlPath);
            return true;
        }
    }
}
