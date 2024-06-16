using KMPLibrary.Format.SectionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.XXXXRouteData
{
    [System.Xml.Serialization.XmlRoot("KMPXml")]
    public class XXXXRoute_XML
    {
        [System.Xml.Serialization.XmlElement("XXXXRoute")]
        public XXXXRoute XXXXRoutes { get; set; }
        public class XXXXRoute
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<GroupData> Groups { get; set; } = new List<GroupData>();
            public class GroupData
            {
                [System.Xml.Serialization.XmlElement("Point")]
                public List<PointData> Points { get; set; } = new List<PointData>();
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

                        public Vector3D ToVector3D()
                        {
                            return new Vector3D(X, Y, Z);
                        }

                        public PositionData(Vector3D Position)
                        {
                            X = (float)Position.X;
                            Y = (float)Position.Y;
                            Z = (float)Position.Z;
                        }

                        public PositionData() { }
                    }

                    [System.Xml.Serialization.XmlAttribute("ScaleValue")]
                    public float ScaleValue { get; set; }

                    public PointData(TPNE.TPNEValue TPNEValue)
                    {
                        Position = new PositionData(TPNEValue.TPNE_Position);
                        ScaleValue = TPNEValue.Control;
                    }

                    public PointData(TPTI.TPTIValue TPTIValue)
                    {
                        Position = new PositionData(TPTIValue.TPTI_Position);
                        ScaleValue = TPTIValue.TPTI_PointSize;
                    }

                    public PointData(TPLG.TPLGValue TPLGValue)
                    {
                        Position = new PositionData(TPLGValue.TPLG_Position);
                        ScaleValue = TPLGValue.TPLG_PointScaleValue;
                    }

                    public PointData() { }
                }

                public GroupData(HPNE.HPNEValue HPNEValue, TPNE TPNE)
                {
                    for (int i = 0; i < HPNEValue.HPNE_Length; i++)
                    {
                        Points.Add(new PointData(TPNE.TPNEValue_List[i + HPNEValue.HPNE_StartPoint]));
                    }
                }

                public GroupData(HPTI.HPTIValue HPTIValue, TPTI TPTI)
                {
                    for (int i = 0; i < HPTIValue.HPTI_Length; i++)
                    {
                        Points.Add(new PointData(TPTI.TPTIValue_List[i + HPTIValue.HPTI_StartPoint]));
                    }
                }

                public GroupData(HPLG.HPLGValue HPLGValue, TPLG TPLG)
                {
                    for (int i = 0; i < HPLGValue.HPLG_Length; i++)
                    {
                        Points.Add(new PointData(TPLG.TPLGValue_List[i + HPLGValue.HPLG_StartPoint]));
                    }
                }

                public GroupData() { }
            }

            /// <summary>
            /// Enemy Route
            /// </summary>
            /// <param name="HPNE"></param>
            /// <param name="TPNE"></param>
            public XXXXRoute(HPNE HPNE, TPNE TPNE)
            {
                for (int i = 0; i < HPNE.NumOfEntries; i++)
                {
                    Groups.Add(new GroupData(HPNE.HPNEValue_List[i], TPNE));
                }
            }

            /// <summary>
            /// Item Route
            /// </summary>
            /// <param name="HPTI"></param>
            /// <param name="TPTI"></param>
            public XXXXRoute(HPTI HPTI, TPTI TPTI)
            {
                for (int i = 0; i < HPTI.NumOfEntries; i++)
                {
                    Groups.Add(new GroupData(HPTI.HPTIValue_List[i], TPTI));
                }
            }

            /// <summary>
            /// Glide Route
            /// </summary>
            /// <param name="HPLG"></param>
            /// <param name="TPLG"></param>
            public XXXXRoute(HPLG HPLG, TPLG TPLG)
            {
                for (int i = 0; i < HPLG.NumOfEntries; i++)
                {
                    Groups.Add(new GroupData(HPLG.HPLGValue_List[i], TPLG));
                }
            }

            public XXXXRoute() { }
        }

        public XXXXRoute_XML(XXXXRoute route)
        {
            XXXXRoutes = route;

            //if (routeType == XXXXRouteXmlSetting.RouteType.EnemyRoute)
            //{
            //    XXXXRoutes = new XXXXRoute()
            //}
        }

        public XXXXRoute_XML() { }
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
