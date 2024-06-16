using KMPLibrary.Format.SectionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class EnemyRoute
    {
        [System.Xml.Serialization.XmlElement("Group")]
        public List<EnemyRoute_Group> Groups { get; set; } = new List<EnemyRoute_Group>();
        public class EnemyRoute_Group
        {
            [System.Xml.Serialization.XmlElement("Previous")]
            public ER_PreviousGroup PreviousGroups { get; set; }
            public class ER_PreviousGroup
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

                [System.Xml.Serialization.XmlAttribute("Prev6")]
                public ushort Prev6 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev7")]
                public ushort Prev7 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev8")]
                public ushort Prev8 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev9")]
                public ushort Prev9 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev10")]
                public ushort Prev10 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev11")]
                public ushort Prev11 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev12")]
                public ushort Prev12 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev13")]
                public ushort Prev13 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev14")]
                public ushort Prev14 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev15")]
                public ushort Prev15 { get; set; }

                public ER_PreviousGroup() { }

                public ER_PreviousGroup(HPNE.HPNEValue.HPNE_PreviewGroups PreviewGroups)
                {
                    Prev0 = PreviewGroups.Prev0;
                    Prev1 = PreviewGroups.Prev1;
                    Prev2 = PreviewGroups.Prev2;
                    Prev3 = PreviewGroups.Prev3;
                    Prev4 = PreviewGroups.Prev4;
                    Prev5 = PreviewGroups.Prev5;
                    Prev6 = PreviewGroups.Prev6;
                    Prev7 = PreviewGroups.Prev7;
                    Prev8 = PreviewGroups.Prev8;
                    Prev9 = PreviewGroups.Prev9;
                    Prev10 = PreviewGroups.Prev10;
                    Prev11 = PreviewGroups.Prev11;
                    Prev12 = PreviewGroups.Prev12;
                    Prev13 = PreviewGroups.Prev13;
                    Prev14 = PreviewGroups.Prev14;
                    Prev15 = PreviewGroups.Prev15;
                }
            }

            [System.Xml.Serialization.XmlElement("Next")]
            public ER_NextGroup NextGroups { get; set; }
            public class ER_NextGroup
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

                [System.Xml.Serialization.XmlAttribute("Next6")]
                public ushort Next6 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next7")]
                public ushort Next7 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next8")]
                public ushort Next8 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next9")]
                public ushort Next9 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next10")]
                public ushort Next10 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next11")]
                public ushort Next11 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next12")]
                public ushort Next12 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next13")]
                public ushort Next13 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next14")]
                public ushort Next14 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next15")]
                public ushort Next15 { get; set; }

                public ER_NextGroup() { }

                public ER_NextGroup(HPNE.HPNEValue.HPNE_NextGroups NextGroups)
                {
                    Next0 = NextGroups.Next0;
                    Next1 = NextGroups.Next1;
                    Next2 = NextGroups.Next2;
                    Next3 = NextGroups.Next3;
                    Next4 = NextGroups.Next4;
                    Next5 = NextGroups.Next5;
                    Next6 = NextGroups.Next6;
                    Next7 = NextGroups.Next7;
                    Next8 = NextGroups.Next8;
                    Next9 = NextGroups.Next9;
                    Next10 = NextGroups.Next10;
                    Next11 = NextGroups.Next11;
                    Next12 = NextGroups.Next12;
                    Next13 = NextGroups.Next13;
                    Next14 = NextGroups.Next14;
                    Next15 = NextGroups.Next15;
                }
            }

            [System.Xml.Serialization.XmlAttribute("Unknown1")]
            public ushort Unknown1 { get; set; }

            [System.Xml.Serialization.XmlAttribute("Unknown2")]
            public ushort Unknown2 { get; set; }

            [System.Xml.Serialization.XmlArray("Points")]
            [System.Xml.Serialization.XmlArrayItem("Point")]
            public List<EnemyRoute_Point> Points { get; set; } = new List<EnemyRoute_Point>();
            public class EnemyRoute_Point
            {
                [System.Xml.Serialization.XmlElement("Position")]
                public EnemyRoute_Position Position { get; set; }
                public class EnemyRoute_Position
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public EnemyRoute_Position() { }

                    public EnemyRoute_Position(Vector3D vector3D)
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

                [System.Xml.Serialization.XmlAttribute("MushSetting")]
                public ushort MushSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("DriftSetting")]
                public byte DriftSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("Flags")]
                public byte Flags { get; set; }

                [System.Xml.Serialization.XmlAttribute("Control")]
                public float Control { get; set; }

                [System.Xml.Serialization.XmlAttribute("PathFind")]
                public short PathFindOption { get; set; }

                [System.Xml.Serialization.XmlAttribute("MaxSearchYOffset")]
                public short MaxSearchYOffset { get; set; }

                public EnemyRoute_Point(TPNE.TPNEValue EnemyPoint)
                {
                    Position = new EnemyRoute_Position(EnemyPoint.TPNE_Position);
                    Control = EnemyPoint.Control;
                    DriftSetting = EnemyPoint.DriftSettingValue;
                    MushSetting = EnemyPoint.MushSettingValue;
                    Flags = EnemyPoint.Flags;
                    PathFindOption = EnemyPoint.PathFindOptionValue;
                    MaxSearchYOffset = EnemyPoint.MaxSearchYOffsetValue;
                }

                public EnemyRoute_Point() { }
            }

            public EnemyRoute_Group(HPNE.HPNEValue HPNEValue, TPNE TPNE)
            {
                PreviousGroups = new ER_PreviousGroup(HPNEValue.HPNE_PreviewGroup);
                NextGroups = new ER_NextGroup(HPNEValue.HPNE_NextGroup);
                Unknown1 = HPNEValue.UnknownShortData1;
                Unknown2 = HPNEValue.UnknownShortData2;

                for (int i = 0; i < HPNEValue.HPNE_Length; i++)
                {
                    Points.Add(new EnemyRoute_Point(TPNE.TPNEValue_List[i + HPNEValue.HPNE_StartPoint]));
                }
            }

            public EnemyRoute_Group() { }
        }

        public EnemyRoute(HPNE HPNE, TPNE TPNE)
        {
            for (int i = 0; i < HPNE.NumOfEntries; i++)
            {
                Groups.Add(new EnemyRoute_Group(HPNE.HPNEValue_List[i], TPNE));
            }
        }

        public EnemyRoute() { }
    }
}
