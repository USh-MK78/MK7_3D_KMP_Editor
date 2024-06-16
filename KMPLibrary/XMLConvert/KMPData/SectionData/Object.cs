using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class Object
    {
        [System.Xml.Serialization.XmlElement("Value")]
        public List<Object_Value> Object_Values { get; set; } = new List<Object_Value>();
        public class Object_Value
        {
            [System.Xml.Serialization.XmlElement("Position")]
            public Object_Position Position { get; set; }
            public class Object_Position
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Object_Position() { }

                public Object_Position(Vector3D vector3D)
                {
                    X = (float)vector3D.X;
                    Y = (float)vector3D.Y;
                    Z = (float)vector3D.Z;
                }

                public Vector3D ToVector3D()
                {
                    return new Vector3D(X, Y, Z);
                }
            }

            [System.Xml.Serialization.XmlElement("Rotation")]
            public Object_Rotation Rotation { get; set; }
            public class Object_Rotation
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Object_Rotation() { }

                public Object_Rotation(Vector3D vector3D)
                {
                    X = (float)vector3D.X;
                    Y = (float)vector3D.Y;
                    Z = (float)vector3D.Z;
                }

                public Vector3D ToVector3D()
                {
                    return new Vector3D(X, Y, Z);
                }
            }

            [System.Xml.Serialization.XmlElement("Scale")]
            public Object_Scale Scale { get; set; }
            public class Object_Scale
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Object_Scale() { }

                public Object_Scale(Vector3D vector3D)
                {
                    X = (float)vector3D.X;
                    Y = (float)vector3D.Y;
                    Z = (float)vector3D.Z;
                }

                public Vector3D ToVector3D()
                {
                    return new Vector3D(X, Y, Z);
                }
            }

            [System.Xml.Serialization.XmlElement("SpecificSetting")]
            public SpecificSettings SpecificSetting { get; set; }
            public class SpecificSettings
            {
                [System.Xml.Serialization.XmlAttribute("Value0")]
                public ushort Value0 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Value1")]
                public ushort Value1 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Value2")]
                public ushort Value2 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Value3")]
                public ushort Value3 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Value4")]
                public ushort Value4 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Value5")]
                public ushort Value5 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Value6")]
                public ushort Value6 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Value7")]
                public ushort Value7 { get; set; }

                public SpecificSettings() { }

                public SpecificSettings(Format.SectionData.JBOG.JBOGValue.JBOG_SpecificSetting JOBJ_Specific_Setting)
                {
                    Value0 = JOBJ_Specific_Setting.Value0;
                    Value1 = JOBJ_Specific_Setting.Value1;
                    Value2 = JOBJ_Specific_Setting.Value2;
                    Value3 = JOBJ_Specific_Setting.Value3;
                    Value4 = JOBJ_Specific_Setting.Value4;
                    Value5 = JOBJ_Specific_Setting.Value5;
                    Value6 = JOBJ_Specific_Setting.Value6;
                    Value7 = JOBJ_Specific_Setting.Value7;
                }
            }

            [System.Xml.Serialization.XmlAttribute("ObjectID")]
            public string ObjectID { get; set; }

            [System.Xml.Serialization.XmlAttribute("UnkByte1")]
            public string UnkByte1 { get; set; }

            [System.Xml.Serialization.XmlAttribute("RouteIDIndex")]
            public ushort RouteIDIndex { get; set; }

            [System.Xml.Serialization.XmlAttribute("PresenceSetting")]
            public ushort PresenceSetting { get; set; }

            [System.Xml.Serialization.XmlAttribute("UnkByte2")]
            public string UnkByte2 { get; set; }

            [System.Xml.Serialization.XmlAttribute("UnkByte3")]
            public ushort UnkByte3 { get; set; }

            public Object_Value() { }

            public Object_Value(Format.SectionData.JBOG.JBOGValue ObjectValues)
            {
                Position = new Object_Position(ObjectValues.JBOG_Position);
                Rotation = new Object_Rotation(ObjectValues.JBOG_Rotation);
                Scale = new Object_Scale(ObjectValues.JBOG_Scale);
                SpecificSetting = new SpecificSettings(ObjectValues.GOBJ_Specific_Setting);
                ObjectID = BitConverter.ToString(ObjectValues.ObjectID.Reverse().ToArray()).Replace("-", string.Empty);
                RouteIDIndex = ObjectValues.JBOG_ITOP_RouteIDIndex;
                PresenceSetting = ObjectValues.JBOG_PresenceSetting;
                UnkByte1 = BitConverter.ToString(ObjectValues.JBOG_UnkByte1.Reverse().ToArray()).Replace("-", string.Empty);
                UnkByte2 = BitConverter.ToString(ObjectValues.JBOG_UnkByte2.Reverse().ToArray()).Replace("-", string.Empty);
                UnkByte3 = ObjectValues.JBOG_UnkByte3;
            }
        }

        public Object() { }

        public Object(Format.SectionData.JBOG jBOG_Section)
        {
            foreach (var Object in jBOG_Section.JBOGValue_List) Object_Values.Add(new Object_Value(Object));
        }
    }

}
