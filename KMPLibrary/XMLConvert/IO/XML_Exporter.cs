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

        /// <summary>
        /// Export all KMP data (XML) 
        /// </summary>
        /// <param name="KMPData"></param>
        /// <param name="Path">FilePath</param>
        public static void ExportAll(Format.KMP KMPData, string Path)
        {
            KMPData.KMP_XML KMP_XML = new KMPData.KMP_XML(KMPData);

            XmlSerializerNamespaces xns = EmptyXmlSerializerNamespaces();
            XMLExport<KMPData.KMP_XML>(Path + "_All.xml", KMP_XML, xns);

            #region DELETE
            ////Delete Namespaces
            //var xns = new XmlSerializerNamespaces();
            //xns.Add(string.Empty, string.Empty);

            //System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPData.KMP_XML));
            //System.IO.StreamWriter sw = new StreamWriter(Path + "_All.xml", false, new System.Text.UTF8Encoding(false));
            //serializer.Serialize(sw, KMP_XML, xns);
            //sw.Close();
            #endregion
        }

        /// <summary>
        /// Export KMP Section (XML)
        /// </summary>
        /// <param name="KMP">KMPData</param>
        /// <param name="Path">FilePath</param>
        /// <param name="SectionType">SectionType</param>
        public static void ExportSection(Format.KMP KMP, string Path, KMPData.KMPXmlSetting.Section SectionType)
        {
            KMPData.KMP_XML KMP_Xml = KMPData.KMP_XML.CreateNullDefault();

            if (SectionType == KMPData.KMPXmlSetting.Section.KartPoint) KMP_Xml.startPositions = new KMPData.SectionData.StartPosition(KMP.KMP_Section.TPTK);
            else if (SectionType == KMPData.KMPXmlSetting.Section.EnemyRoutes) KMP_Xml.EnemyRoutes = new KMPData.SectionData.EnemyRoute(KMP.KMP_Section.HPNE, KMP.KMP_Section.TPNE);
            else if (SectionType == KMPData.KMPXmlSetting.Section.ItemRoutes) KMP_Xml.ItemRoutes = new KMPData.SectionData.ItemRoute(KMP.KMP_Section.HPTI, KMP.KMP_Section.TPTI);
            else if (SectionType == KMPData.KMPXmlSetting.Section.CheckPoint) KMP_Xml.Checkpoints = new KMPData.SectionData.Checkpoint(KMP.KMP_Section.HPKC, KMP.KMP_Section.TPKC);
            else if (SectionType == KMPData.KMPXmlSetting.Section.Obj) KMP_Xml.Objects = new KMPData.SectionData.Object(KMP.KMP_Section.JBOG);
            else if (SectionType == KMPData.KMPXmlSetting.Section.Route) KMP_Xml.Routes = new KMPData.SectionData.Route(KMP.KMP_Section.ITOP);
            else if (SectionType == KMPData.KMPXmlSetting.Section.Area) KMP_Xml.Areas = new KMPData.SectionData.Area(KMP.KMP_Section.AERA);
            else if (SectionType == KMPData.KMPXmlSetting.Section.Camera) KMP_Xml.Cameras = new KMPData.SectionData.Camera(KMP.KMP_Section.EMAC);
            else if (SectionType == KMPData.KMPXmlSetting.Section.JugemPoint) KMP_Xml.JugemPoints = new KMPData.SectionData.JugemPoint(KMP.KMP_Section.TPGJ);
            else if (SectionType == KMPData.KMPXmlSetting.Section.GlideRoutes) KMP_Xml.GlideRoutes = new KMPData.SectionData.GlideRoute(KMP.KMP_Section.HPLG, KMP.KMP_Section.TPLG);

            XmlSerializerNamespaces xns = EmptyXmlSerializerNamespaces();
            XMLExport<KMPData.KMP_XML>(Path + "_" + SectionType.ToString() + ".xml", KMP_Xml, xns);

            #region DELETE
            ////Delete Namespaces
            //var xns = new XmlSerializerNamespaces();
            //xns.Add(string.Empty, string.Empty);

            //System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPData.KMP_XML));
            //System.IO.StreamWriter sw = new StreamWriter(Path + "_" + section.ToString() + ".xml", false, new System.Text.UTF8Encoding(false));
            //serializer.Serialize(sw, KMP_Xml, xns);
            //sw.Close();
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KMPData"></param>
        /// <param name="Path"></param>
        /// <param name="RouteType"></param>
        public static void ExportXXXXRoute(Format.KMP KMPData, string Path, XXXXRouteData.XXXXRouteXmlSetting.RouteType RouteType)
        {
            XXXXRouteData.XXXXRoute_XML XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML();
            if (RouteType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.EnemyRoute)
            {
                XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPNE, KMPData.KMP_Section.TPNE));
            }
            else if (RouteType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.ItemRoute)
            {
                XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPTI, KMPData.KMP_Section.TPTI));
            }
            else if (RouteType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.GlideRoute)
            {
                XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPLG, KMPData.KMP_Section.TPLG));
            }

            XmlSerializerNamespaces xns = EmptyXmlSerializerNamespaces();
            XMLExport<XXXXRouteData.XXXXRoute_XML>(Path + "_PositionAndScaleOnly" + ".xml", XXXXRoute_XML, xns);

            #region DELETE
            ////Delete Namespaces
            //var xns = new XmlSerializerNamespaces();
            //xns.Add(string.Empty, string.Empty);

            //System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(XXXXRouteData.XXXXRoute_XML));
            //System.IO.StreamWriter sw = new StreamWriter(Path + "_PositionAndScaleOnly" + ".xml", false, new System.Text.UTF8Encoding(false));
            //serializer.Serialize(sw, XXXXRoute_XML, xns);
            //sw.Close();
            #endregion
        }
    }
}
