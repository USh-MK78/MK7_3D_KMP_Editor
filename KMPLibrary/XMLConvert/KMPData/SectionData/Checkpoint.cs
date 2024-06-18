using KMPLibrary.Format.SectionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class Checkpoint
    {
        [System.Xml.Serialization.XmlElement("Groups")]
        public List<Checkpoint_Group> Groups { get; set; } = new List<Checkpoint_Group>();
        public class Checkpoint_Group
        {
            [System.Xml.Serialization.XmlElement("Previous")]
            public CP_PreviousGroup PreviousGroups { get; set; }
            public class CP_PreviousGroup
            {
                [System.Xml.Serialization.XmlAttribute("Prev0")]
                public byte Prev0 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev1")]
                public byte Prev1 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev2")]
                public byte Prev2 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev3")]
                public byte Prev3 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev4")]
                public byte Prev4 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Prev5")]
                public byte Prev5 { get; set; }

                public CP_PreviousGroup(HPKC.HPKCValue.HPKC_PreviewGroups PreviewGroups)
                {
                    Prev0 = PreviewGroups.Prev0;
                    Prev1 = PreviewGroups.Prev1;
                    Prev2 = PreviewGroups.Prev2;
                    Prev3 = PreviewGroups.Prev3;
                    Prev4 = PreviewGroups.Prev4;
                    Prev5 = PreviewGroups.Prev5;
                }

                public CP_PreviousGroup() { }
            }

            [System.Xml.Serialization.XmlElement("Next")]
            public CP_NextGroup NextGroups { get; set; }
            public class CP_NextGroup
            {
                [System.Xml.Serialization.XmlAttribute("Next0")]
                public byte Next0 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next1")]
                public byte Next1 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next2")]
                public byte Next2 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next3")]
                public byte Next3 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next4")]
                public byte Next4 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Next5")]
                public byte Next5 { get; set; }

                public CP_NextGroup(HPKC.HPKCValue.HPKC_NextGroups NextGroups)
                {
                    Next0 = NextGroups.Next0;
                    Next1 = NextGroups.Next1;
                    Next2 = NextGroups.Next2;
                    Next3 = NextGroups.Next3;
                    Next4 = NextGroups.Next4;
                    Next5 = NextGroups.Next5;
                }

                public CP_NextGroup() { }
            }

            [System.Xml.Serialization.XmlAttribute("UnknownData1")]
            public ushort UnknownData1 { get; set; }

            [System.Xml.Serialization.XmlArray("Points")]
            [System.Xml.Serialization.XmlArrayItem("Point")]
            public List<Checkpoint_Point> Points { get; set; } = new List<Checkpoint_Point>();
            public class Checkpoint_Point
            {
                [System.Xml.Serialization.XmlElement("Position2DLeft")]
                public Position2D_Left Position_2D_Left { get; set; }
                public class Position2D_Left
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    public Vector2 ToVector2()
                    {
                        return new Vector2(X, Y);
                    }

                    public Position2D_Left(float X, float Y)
                    {
                        this.X = X;
                        this.Y = Y;
                    }

                    public Position2D_Left() { }
                }

                [System.Xml.Serialization.XmlElement("Position2DRight")]
                public Position2D_Right Position_2D_Right { get; set; }
                public class Position2D_Right
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    public Vector2 ToVector2()
                    {
                        return new Vector2(X, Y);
                    }

                    public Position2D_Right(float X, float Y)
                    {
                        this.X = X;
                        this.Y = Y;
                    }

                    public Position2D_Right() { }
                }

                [System.Xml.Serialization.XmlAttribute("RespawnID")]
                public byte RespawnID { get; set; }

                [System.Xml.Serialization.XmlAttribute("Checkpoint_Type")]
                public byte Checkpoint_Type { get; set; }

                [System.Xml.Serialization.XmlAttribute("PreviousCheckPoint")]
                public byte PreviousCheckPoint { get; set; }

                [System.Xml.Serialization.XmlAttribute("NextCheckPoint")]
                public byte NextCheckPoint { get; set; }

                [System.Xml.Serialization.XmlAttribute("ClipID")]
                public byte ClipID { get; set; }

                [System.Xml.Serialization.XmlAttribute("Section")]
                public byte Section { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnknownData3")]
                public byte UnknownData3 { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnknownData4")]
                public byte UnknownData4 { get; set; }

                public Checkpoint_Point(TPKC.TPKCValue TPKCValue)
                {
                    Position_2D_Left = new Position2D_Left(TPKCValue.TPKC_2DPosition_Left.X, TPKCValue.TPKC_2DPosition_Left.Y);
                    Position_2D_Right = new Position2D_Right(TPKCValue.TPKC_2DPosition_Right.X, TPKCValue.TPKC_2DPosition_Right.Y);
                    RespawnID = TPKCValue.TPKC_RespawnID;
                    Checkpoint_Type = TPKCValue.TPKC_Checkpoint_Type;
                    NextCheckPoint = TPKCValue.TPKC_NextCheckPoint;
                    PreviousCheckPoint = TPKCValue.TPKC_PreviousCheckPoint;
                    ClipID = TPKCValue.TPKC_ClipID;
                    Section = TPKCValue.TPKC_Section;
                    UnknownData3 = TPKCValue.TPKC_UnknownData3;
                    UnknownData4 = TPKCValue.TPKC_UnknownData4;
                }

                public Checkpoint_Point() { }
            }

            public Checkpoint_Group(HPKC.HPKCValue HPKCValue, TPKC TPKC)
            {
                PreviousGroups = new CP_PreviousGroup(HPKCValue.HPKC_PreviewGroup);
                NextGroups = new CP_NextGroup(HPKCValue.HPKC_NextGroup);
                UnknownData1 = HPKCValue.HPKC_UnknownShortData1;

                for (int i = 0; i < HPKCValue.HPKC_Length; i++)
                {
                    Points.Add(new Checkpoint_Point(TPKC.TPKCValue_List[i + HPKCValue.HPKC_StartPoint]));
                }
            }

            public Checkpoint_Group() { }
        }

        public Checkpoint(HPKC HPKC, TPKC TPKC)
        {
            for (int i = 0; i < HPKC.NumOfEntries; i++)
            {
                Groups.Add(new Checkpoint_Group(HPKC.HPKCValue_List[i], TPKC));
            }
        }

        public Checkpoint() { }
    }
}
