using KMPLibrary.Format.SectionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class ItemRoute
    {
        [System.Xml.Serialization.XmlElement("Group")]
        public List<ItemRoute_Group> Groups { get; set; } = new List<ItemRoute_Group>();
        public class ItemRoute_Group
        {
            [System.Xml.Serialization.XmlElement("Previous")]
            public IR_PreviousGroup PreviousGroups { get; set; }
            public class IR_PreviousGroup
            {
                [System.Xml.Serialization.XmlAttribute("Prev0")]
                public ushort Prev0 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev1")]
                public ushort Prev1 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev2")]
                public ushort Prev2 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev3")]
                public ushort Prev3 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev4")]
                public ushort Prev4 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev5")]
                public ushort Prev5 { get; set; }

                public IR_PreviousGroup(HPTI.HPTIValue.HPTI_PreviewGroups PreviewGroups)
                {
                    Prev0 = PreviewGroups.Prev0;
                    Prev1 = PreviewGroups.Prev1;
                    Prev2 = PreviewGroups.Prev2;
                    Prev3 = PreviewGroups.Prev3;
                    Prev4 = PreviewGroups.Prev4;
                    Prev5 = PreviewGroups.Prev5;
                }

                public IR_PreviousGroup() { }
            }

            [System.Xml.Serialization.XmlElement("Next")]
            public IR_NextGroup NextGroups { get; set; }
            public class IR_NextGroup
            {
                [System.Xml.Serialization.XmlAttribute("Next0")]
                public ushort Next0 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next1")]
                public ushort Next1 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next2")]
                public ushort Next2 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next3")]
                public ushort Next3 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next4")]
                public ushort Next4 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next5")]
                public ushort Next5 { get; set; }

                public IR_NextGroup(HPTI.HPTIValue.HPTI_NextGroups NextGroups)
                {
                    Next0 = NextGroups.Next0;
                    Next1 = NextGroups.Next1;
                    Next2 = NextGroups.Next2;
                    Next3 = NextGroups.Next3;
                    Next4 = NextGroups.Next4;
                    Next5 = NextGroups.Next5;
                }

                public IR_NextGroup() { }
            }

            [System.Xml.Serialization.XmlArray("Points")]
            [System.Xml.Serialization.XmlArrayItem("Point")]
            public List<ItemRoute_Point> Points { get; set; } = new List<ItemRoute_Point>();
            public class ItemRoute_Point
            {
                [System.Xml.Serialization.XmlElement("Position")]
                public ItemRoute_Position Position { get; set; }
                public class ItemRoute_Position
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public ItemRoute_Position() { }

                    public ItemRoute_Position(Vector3D vector3D)
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

                [System.Xml.Serialization.XmlAttribute("GravityMode")]
                public ushort GravityMode { get; set; }

                [System.Xml.Serialization.XmlAttribute("PlayerScanRadius")]
                public ushort PlayerScanRadius { get; set; }

                [System.Xml.Serialization.XmlAttribute("PointSize")]
                public float PointSize { get; set; }

                public ItemRoute_Point(TPTI.TPTIValue ItemPoint)
                {
                    GravityMode = ItemPoint.GravityModeValue;
                    PlayerScanRadius = ItemPoint.PlayerScanRadiusValue;
                    PointSize = ItemPoint.TPTI_PointSize;
                    Position = new ItemRoute_Position(ItemPoint.TPTI_Position);
                }

                public ItemRoute_Point() { }
            }

            public ItemRoute_Group(HPTI.HPTIValue HPTIValue, TPTI TPTI)
            {
                PreviousGroups = new IR_PreviousGroup(HPTIValue.HPTI_PreviewGroup);
                NextGroups = new IR_NextGroup(HPTIValue.HPTI_NextGroup);

                for (int i = 0; i < HPTIValue.HPTI_Length; i++)
                {
                    Points.Add(new ItemRoute_Point(TPTI.TPTIValue_List[i + HPTIValue.HPTI_StartPoint]));
                }
            }

            public ItemRoute_Group() { }
        }

        public ItemRoute(HPTI HPTI, TPTI TPTI)
        {
            for (int i = 0; i < HPTI.NumOfEntries; i++)
            {
                Groups.Add(new ItemRoute_Group(HPTI.HPTIValue_List[i], TPTI));
            }
        }

        public ItemRoute() { }
    }
}
