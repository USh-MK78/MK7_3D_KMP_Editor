using KMPLibrary.Format.SectionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class GlideRoute
    {
        [System.Xml.Serialization.XmlElement("Group")]
        public List<GlideRoute_Group> Groups { get; set; } = new List<GlideRoute_Group>();
        public class GlideRoute_Group
        {
            [System.Xml.Serialization.XmlElement("Previous")]
            public GR_PreviousGroup PreviousGroups { get; set; }
            public class GR_PreviousGroup
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

                public GR_PreviousGroup(HPLG.HPLGValue.HPLG_PreviewGroups PreviewGroups)
                {
                    Prev0 = PreviewGroups.Prev0;
                    Prev1 = PreviewGroups.Prev1;
                    Prev2 = PreviewGroups.Prev2;
                    Prev3 = PreviewGroups.Prev3;
                    Prev4 = PreviewGroups.Prev4;
                    Prev5 = PreviewGroups.Prev5;
                }

                public GR_PreviousGroup() { }
            }

            [System.Xml.Serialization.XmlElement("Next")]
            public GR_NextGroup NextGroups { get; set; }
            public class GR_NextGroup
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

                public GR_NextGroup(HPLG.HPLGValue.HPLG_NextGroups NextGroups)
                {
                    Next0 = NextGroups.Next0;
                    Next1 = NextGroups.Next1;
                    Next2 = NextGroups.Next2;
                    Next3 = NextGroups.Next3;
                    Next4 = NextGroups.Next4;
                    Next5 = NextGroups.Next5;
                }

                public GR_NextGroup() { }
            }

            [System.Xml.Serialization.XmlAttribute("RouteSetting")]
            public uint RouteSetting { get; set; }

            [System.Xml.Serialization.XmlAttribute("UnknownData2")]
            public uint UnknownData2 { get; set; }

            [System.Xml.Serialization.XmlArray("Points")]
            [System.Xml.Serialization.XmlArrayItem("Point")]
            public List<GlideRoute_Point> Points { get; set; } = new List<GlideRoute_Point>();
            public class GlideRoute_Point
            {
                [System.Xml.Serialization.XmlAttribute("PointScale")]
                public float PointScale { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnknownData1")]
                public uint UnknownData1 { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnknownData2")]
                public uint UnknownData2 { get; set; }

                [System.Xml.Serialization.XmlElement("Position")]
                public GlideRoute_Position Position { get; set; }
                public class GlideRoute_Position
                {
                    [System.Xml.Serialization.XmlElement("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlElement("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlElement("Z")]
                    public float Z { get; set; }

                    public GlideRoute_Position(Vector3D vector3D)
                    {
                        X = (float)vector3D.X;
                        Y = (float)vector3D.Y;
                        Z = (float)vector3D.Z;
                    }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }

                    public GlideRoute_Position() { }
                }

                public GlideRoute_Point(TPLG.TPLGValue TPLGValue)
                {
                    Position = new GlideRoute_Position(TPLGValue.TPLG_Position);
                    PointScale = TPLGValue.TPLG_PointScaleValue;
                    UnknownData1 = TPLGValue.TPLG_UnknownData1;
                    UnknownData2 = TPLGValue.TPLG_UnknownData2;
                }

                public GlideRoute_Point() { }
            }

            public GlideRoute_Group(HPLG.HPLGValue HPLGValue, TPLG TPLG)
            {
                PreviousGroups = new GR_PreviousGroup(HPLGValue.HPLG_PreviewGroup);
                NextGroups = new GR_NextGroup(HPLGValue.HPLG_NextGroup);
                RouteSetting = HPLGValue.RouteSetting;
                UnknownData2 = HPLGValue.HPLG_UnknownData2;

                for (int i = 0; i < HPLGValue.HPLG_Length; i++)
                {
                    Points.Add(new GlideRoute_Point(TPLG.TPLGValue_List[i + HPLGValue.HPLG_StartPoint]));
                }
            }

            public GlideRoute_Group() { }
        }

        public GlideRoute(HPLG HPLG, TPLG TPLG)
        {
            for (int i = 0; i < HPLG.NumOfEntries; i++)
            {
                Groups.Add(new GlideRoute_Group(HPLG.HPLGValue_List[i], TPLG));
            }
        }

        public GlideRoute() { }
    }
}
