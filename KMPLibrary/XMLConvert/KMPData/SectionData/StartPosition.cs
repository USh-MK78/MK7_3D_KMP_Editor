using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

using KMPLibrary.Format.SectionData;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{

    public class StartPosition
    {
        [System.Xml.Serialization.XmlElement("Value")]
        public List<StartPosition_Value> StartPositionValues { get; set; } = new List<StartPosition_Value>();
        public class StartPosition_Value
        {
            [System.Xml.Serialization.XmlElement("Position")]
            public StartPosition_Position Position { get; set; }
            public class StartPosition_Position
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public StartPosition_Position() { }

                public StartPosition_Position(Vector3D vector3D)
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
            public StartPosition_Rotation Rotation { get; set; }
            public class StartPosition_Rotation
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public StartPosition_Rotation() { }

                public StartPosition_Rotation(Vector3D vector3D)
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

            [System.Xml.Serialization.XmlAttribute("Player_Index")]
            public ushort Player_Index { get; set; }

            [System.Xml.Serialization.XmlAttribute("TPTK_UnkBytes")]
            public ushort TPTK_UnkBytes { get; set; }

            public StartPosition_Value() { }

            public StartPosition_Value(TPTK.TPTKValue StartPositionValue)
            {
                Position = new StartPosition_Position(StartPositionValue.TPTK_Position);
                Rotation = new StartPosition_Rotation(StartPositionValue.TPTK_Rotation);
                Player_Index = StartPositionValue.Player_Index;
                TPTK_UnkBytes = StartPositionValue.TPTK_UnknownData;
            }
        }

        public StartPosition() { }

        public StartPosition(TPTK tPTK_Section)
        {
            foreach (var StartPositions in tPTK_Section.TPTKValue_List) StartPositionValues.Add(new StartPosition_Value(StartPositions));
        }
    }
}
