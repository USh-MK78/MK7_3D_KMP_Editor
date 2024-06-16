using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class Route
    {
        [System.Xml.Serialization.XmlElement("Group")]
        public List<Route_Group> Groups { get; set; } = new List<Route_Group>();
        public class Route_Group
        {
            [System.Xml.Serialization.XmlAttribute("Loop")]
            public byte LoopSetting { get; set; }

            [System.Xml.Serialization.XmlAttribute("Smooth")]
            public byte SmoothSetting { get; set; }

            [System.Xml.Serialization.XmlElement("Point")]
            public List<Route_Point> Points { get; set; } = new List<Route_Point>();
            public class Route_Point
            {
                [System.Xml.Serialization.XmlAttribute("RouteSpeed")]
                public ushort RouteSpeed { get; set; }

                [System.Xml.Serialization.XmlAttribute("PointSetting2")]
                public ushort PointSetting2 { get; set; }

                [System.Xml.Serialization.XmlElement("Position")]
                public Route_Position Position { get; set; }
                public class Route_Position
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Route_Position() { }

                    public Route_Position(Vector3D vector3D)
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

                public Route_Point() { }

                public Route_Point(Format.SectionData.ITOP.ITOP_Route.ITOP_Point RoutePoint)
                {
                    Position = new Route_Position(RoutePoint.ITOP_Point_Position);
                    RouteSpeed = RoutePoint.ITOP_Point_RouteSpeed;
                    PointSetting2 = RoutePoint.ITOP_PointSetting2;
                }
            }

            public Route_Group() { }

            public Route_Group(Format.SectionData.ITOP.ITOP_Route RouteGroup)
            {
                LoopSetting = RouteGroup.ITOP_LoopSetting;
                SmoothSetting = RouteGroup.ITOP_SmoothSetting;

                foreach (var Point in RouteGroup.ITOP_Point_List) Points.Add(new Route_Point(Point));
            }
        }

        public Route() { }

        public Route(Format.SectionData.ITOP iTOP_Section)
        {
            foreach (var Group in iTOP_Section.ITOP_Route_List) Groups.Add(new Route_Group(Group));
        }
    }
}
