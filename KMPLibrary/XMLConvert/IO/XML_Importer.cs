using KMPLibrary.Format;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMPLibrary.XMLConvert.KMPData.SectionData;

namespace KMPLibrary.XMLConvert.IO
{
    public class XML_Importer
    {
        /// <summary>
        /// Read XML
        /// </summary>
        /// <typeparam name="T">Class (XML)</typeparam>
        /// <param name="Path">FilePath</param>
        /// <returns>T</returns>
        public static T XMLImport<T>(string Path)
        {
            System.IO.FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer Serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)Serializer.Deserialize(fs);
        }
    }
}
