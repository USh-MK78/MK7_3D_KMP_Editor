using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK7_KMP_Editor_For_PG_.TestXml
{
    [System.Xml.Serialization.XmlRoot("KMPXml")]
    public class XXXXRouteXml
    {
        [System.Xml.Serialization.XmlElement("XXXXRoute")]
        public XXXXRoute XXXXRoutes { get; set; }
        public class XXXXRoute
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<GroupData> Groups { get; set; }
            public class GroupData
            {
                [System.Xml.Serialization.XmlElement("Point")]
                public List<PointData> Points { get; set; }
                public class PointData
                {
                    [System.Xml.Serialization.XmlElement("Position")]
                    public PositionData Position { get; set; }
                    public class PositionData
                    {
                        [System.Xml.Serialization.XmlAttribute("X")]
                        public float X { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Y")]
                        public float Y { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Z")]
                        public float Z { get; set; }
                    }

                    [System.Xml.Serialization.XmlAttribute("ScaleValue")]
                    public float ScaleValue { get; set; }
                }
            }
        }
    }

    public class XXXXRouteXmlSetting
    {
        public enum RouteType
        {
            EnemyRoute,
            ItemRoute,
            GlideRoute
        }
    }
}
