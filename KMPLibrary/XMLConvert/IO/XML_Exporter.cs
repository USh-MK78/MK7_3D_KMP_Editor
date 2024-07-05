using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KMPLibrary.XMLConvert.IO
{
    public class XML_Exporter
    {
        /// <summary>
        /// XmlNamespaces
        /// </summary>
        public class XMLNameSpace
        {
            public string prefix { get; set; }
            public string ns { get; set; }

            public XMLNameSpace EmptyXmlNamespace()
            {
                return new XMLNameSpace(string.Empty, string.Empty);
            }

            public XMLNameSpace(string prefix, string ns)
            {
                this.prefix = prefix;
                this.ns = ns;
            }
        }

        /// <summary>
        /// Delete Namespaces
        /// </summary>
        /// <returns></returns>
        public static XmlSerializerNamespaces EmptyXmlSerializerNamespaces()
        {
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            return xns;
        }

        /// <summary>
        /// Create XmlSerializerNamespaces
        /// </summary>
        /// <param name="NamespaceArray">XMLNameSpace[]</param>
        /// <returns>XmlSerializerNamespaces</returns>
        public XmlSerializerNamespaces CreateXmlSerializerNamespaces(XMLNameSpace[] NamespaceArray)
        {
            var xns = new XmlSerializerNamespaces();

            foreach (var items in NamespaceArray)
            {
                xns.Add(items.prefix, items.ns);
            }

            xns.Add(string.Empty, string.Empty);
            return xns;
        }

        /// <summary>
        /// Export XML
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="Path">File Path</param>
        /// <param name="XMLData">XMLData</param>
        /// <param name="xns">XmlSerializerNamespaces (Empty => EmptyXmlSerializerNamespaces())</param>
        public static void XMLExport<T>(string Path, T XMLData, XmlSerializerNamespaces xns)
        {
            System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(T));
            System.IO.StreamWriter sw = new StreamWriter(Path, false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, XMLData, xns);
            sw.Close();
        }
    }
}
