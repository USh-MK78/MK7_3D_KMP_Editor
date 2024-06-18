using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class Area
    {
        [System.Xml.Serialization.XmlElement("Value")]
        public List<Area_Value> Area_Values { get; set; } = new List<Area_Value>();
        public class Area_Value
        {
            [System.Xml.Serialization.XmlAttribute("AreaType")]
            public byte AreaType { get; set; }

            [System.Xml.Serialization.XmlAttribute("AreaMode")]
            public byte AreaMode { get; set; }

            [System.Xml.Serialization.XmlAttribute("CameraIndex")]
            public byte CameraIndex { get; set; }

            [System.Xml.Serialization.XmlAttribute("Priority")]
            public byte Priority { get; set; }

            [System.Xml.Serialization.XmlAttribute("Setting1")]
            public ushort Setting1 { get; set; }

            [System.Xml.Serialization.XmlAttribute("Setting2")]
            public ushort Setting2 { get; set; }

            [System.Xml.Serialization.XmlAttribute("RouteID")]
            public byte RouteID { get; set; }

            [System.Xml.Serialization.XmlAttribute("EnemyID")]
            public byte EnemyID { get; set; }

            [System.Xml.Serialization.XmlAttribute("UnknownData1")]
            public ushort UnknownData1 { get; set; }

            [System.Xml.Serialization.XmlElement("Position")]
            public Area_Position Position { get; set; }
            public class Area_Position
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Area_Position() { }

                public Area_Position(Vector3D vector3D)
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
            public Area_Rotation Rotation { get; set; }
            public class Area_Rotation
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Area_Rotation() { }

                public Area_Rotation(Vector3D vector3D)
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
            public Area_Scale Scale { get; set; }
            public class Area_Scale
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Area_Scale() { }

                public Area_Scale(Vector3D vector3D)
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

            public Area_Value() { }

            public Area_Value(Format.SectionData.AERA.AERAValue AreaValues)
            {
                Position = new Area_Position(AreaValues.AERA_Position);
                Rotation = new Area_Rotation(AreaValues.AERA_Rotation);
                Scale = new Area_Scale(AreaValues.AERA_Scale);
                AreaType = AreaValues.AreaType;
                AreaMode = AreaValues.AreaModeValue;
                CameraIndex = AreaValues.AERA_EMACIndex;
                Priority = AreaValues.Priority;
                Setting1 = AreaValues.AERA_Setting1;
                Setting2 = AreaValues.AERA_Setting2;
                RouteID = AreaValues.RouteID;
                EnemyID = AreaValues.EnemyID;
                UnknownData1 = AreaValues.AERA_UnknownData1;
            }
        }

        public Area() { }

        public Area(Format.SectionData.AERA AERA_Section)
        {
            foreach (var AREAValue in AERA_Section.AERAValue_List) Area_Values.Add(new Area_Value(AREAValue));
        }
    }
}
