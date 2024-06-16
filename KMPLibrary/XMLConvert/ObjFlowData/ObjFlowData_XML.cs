using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.XMLConvert.ObjFlowData
{
    [System.Xml.Serialization.XmlRoot("KMPObjFlowData")]
    public class ObjFlowData_XML
    {
        [System.Xml.Serialization.XmlElement("ObjFlow")]
        public List<ObjFlow> ObjFlows { get; set; }
        public class ObjFlow
        {
            [System.Xml.Serialization.XmlAttribute("ObjectID")]
            public string ObjectID { get; set; }

            [System.Xml.Serialization.XmlAttribute("ObjectName")]
            public string ObjectName { get; set; }

            [System.Xml.Serialization.XmlAttribute("Path")]
            public string Path { get; set; }

            [System.Xml.Serialization.XmlAttribute("KCLFile")]
            public bool UseKCL { get; set; }

            [System.Xml.Serialization.XmlAttribute("ObjectType")]
            public string ObjectType { get; set; }

            [System.Xml.Serialization.XmlElement("Common")]
            public Common Commons { get; set; }
            public class Common
            {
                [System.Xml.Serialization.XmlAttribute("ColType")]
                public string ColType { get; set; }

                [System.Xml.Serialization.XmlAttribute("PathType")]
                public string PathType { get; set; }

                [System.Xml.Serialization.XmlAttribute("ModelSetting")]
                public string ModelSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("Unknown1")]
                public string Unknown1 { get; set; }
            }

            [System.Xml.Serialization.XmlElement("LODSetting")]
            public LOD_Setting LODSetting { get; set; }
            public class LOD_Setting
            {
                [System.Xml.Serialization.XmlAttribute("LOD")]
                public int LOD { get; set; }

                [System.Xml.Serialization.XmlAttribute("LODHPoly")]
                public int LODHPoly { get; set; }

                [System.Xml.Serialization.XmlAttribute("LODLPoly")]
                public int LODLPoly { get; set; }

                [System.Xml.Serialization.XmlAttribute("LODDef")]
                public int LODDef { get; set; }
            }

            [System.Xml.Serialization.XmlElement("Scale")]
            public Scale Scales { get; set; }
            public class Scale
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public int X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public int Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public int Z { get; set; }
            }

            [System.Xml.Serialization.XmlElement("Name")]
            public Name Names { get; set; }
            public class Name
            {
                [System.Xml.Serialization.XmlAttribute("Main")]
                public string Main { get; set; }

                [System.Xml.Serialization.XmlAttribute("Sub")]
                public string Sub { get; set; }
            }

            [System.Xml.Serialization.XmlElement("DefaultValues")]
            public Default_Values DefaultValues { get; set; }
            public class Default_Values
            {
                [System.Xml.Serialization.XmlElement("Value")]
                public List<Value> Values { get; set; }
                public class Value
                {
                    [System.Xml.Serialization.XmlAttribute("Default")]
                    public int DefaultObjectValue { get; set; }

                    [System.Xml.Serialization.XmlText]
                    public string Description { get; set; }
                }
            }
        }
    }
}
