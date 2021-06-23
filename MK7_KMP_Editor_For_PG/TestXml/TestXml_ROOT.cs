using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK7_KMP_Editor_For_PG_.TestXml
{
    [System.Xml.Serialization.XmlRoot("yaml")]
    public class TestXml_ROOT
    {
        [System.Xml.Serialization.XmlArray("Obj")]
        [System.Xml.Serialization.XmlArrayItem("value")]
        public List<OBJArray> Obj_Array { get; set; }
    }

    public class OBJArray
    {
        [System.Xml.Serialization.XmlAttribute("type")]
        public string Obj_Type { get; set; }

        [System.Xml.Serialization.XmlElement("Rotate")]
        public Rotate_Val RotateVal { get; set; }

        [System.Xml.Serialization.XmlElement("Scale")]
        public Scale_Val ScaleVal { get; set; }

        [System.Xml.Serialization.XmlElement("Translate")]
        public Translate_Val TranslateVal { get; set; }
    }

    public class Rotate_Val
    {
        [System.Xml.Serialization.XmlAttribute("X")]
        public string Rotate_X { get; set; }

        [System.Xml.Serialization.XmlAttribute("Y")]
        public string Rotate_Y { get; set; }

        [System.Xml.Serialization.XmlAttribute("Z")]
        public string Rotate_Z { get; set; }

    }

    public class Scale_Val
    {
        [System.Xml.Serialization.XmlAttribute("X")]
        public string Scale_X { get; set; }

        [System.Xml.Serialization.XmlAttribute("Y")]
        public string Scale_Y { get; set; }

        [System.Xml.Serialization.XmlAttribute("Z")]
        public string Scale_Z { get; set; }
    }

    public class Translate_Val
    {
        [System.Xml.Serialization.XmlAttribute("X")]
        public string Translate_X { get; set; }

        [System.Xml.Serialization.XmlAttribute("Y")]
        public string Translate_Y { get; set; }

        [System.Xml.Serialization.XmlAttribute("Z")]
        public string Translate_Z { get; set; }
    }
}
