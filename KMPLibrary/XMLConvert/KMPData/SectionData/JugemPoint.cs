using KMPLibrary.Format.SectionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class JugemPoint
    {
        [System.Xml.Serialization.XmlElement("Value")]
        public List<JugemPoint_Value> Values { get; set; } = new List<JugemPoint_Value>();
        public class JugemPoint_Value
        {
            [System.Xml.Serialization.XmlAttribute("RespawnID")]
            public ushort RespawnID { get; set; }

            [System.Xml.Serialization.XmlAttribute("UnkBytes1")]
            public ushort UnkBytes1 { get; set; }

            [System.Xml.Serialization.XmlElement("Position")]
            public JugemPoint_Position Position { get; set; }
            public class JugemPoint_Position
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public JugemPoint_Position() { }

                public JugemPoint_Position(Vector3D vector3D)
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
            public JugemPoint_Rotation Rotation { get; set; }
            public class JugemPoint_Rotation
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public JugemPoint_Rotation() { }

                public JugemPoint_Rotation(Vector3D vector3D)
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

            public JugemPoint_Value() { }

            public JugemPoint_Value(TPGJ.TPGJValue JugemPointValue)
            {
                Position = new JugemPoint_Position(JugemPointValue.TPGJ_Position);
                Rotation = new JugemPoint_Rotation(JugemPointValue.TPGJ_Rotation);
                RespawnID = JugemPointValue.TPGJ_RespawnID;
                UnkBytes1 = JugemPointValue.TPGJ_UnknownData1;
            }
        }

        public JugemPoint() { }

        public JugemPoint(TPGJ tPGJ_Section)
        {
            foreach (var JugemPoint in tPGJ_Section.TPGJValue_List) Values.Add(new JugemPoint_Value(JugemPoint));
        }
    }

}
