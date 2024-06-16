using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KMPLibrary.XMLConvert.IO
{
    public class XML_Exporter
    {
        public static void ExportAll(Format.KMP KMPData, string Path)
        {
            KMPData.KMP_XML KMP_XML = new KMPData.KMP_XML(KMPData);

            //Delete Namespaces
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPData.KMP_XML));
            System.IO.StreamWriter sw = new StreamWriter(Path + "_All.xml", false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, KMP_XML, xns);
            sw.Close();
        }

        public static void ExportSection(Format.KMP kMPPropertyGridSettings, string Path, KMPData.KMPXmlSetting.Section section)
        {
            #region DELETE
            //KMPData.KMP_XML KMP_Xml = new KMPData.KMP_XML
            //{
            //    startPositions = null,
            //    EnemyRoutes = null,
            //    ItemRoutes = null,
            //    Checkpoints = null,
            //    Objects = null,
            //    Routes = null,
            //    Areas = null,
            //    Cameras = null,
            //    JugemPoints = null,
            //    Stage_Info = null,
            //    #region Hide
            //    //Stage_Info = new TestXml.KMPXml.StageInfo
            //    //{
            //    //    Unknown1 = kMPPropertyGridSettings.IGTSSection.Unknown1,
            //    //    LapCount = kMPPropertyGridSettings.IGTSSection.LapCount,
            //    //    PolePosition = kMPPropertyGridSettings.IGTSSection.PolePosition,
            //    //    Unknown2 = kMPPropertyGridSettings.IGTSSection.Unknown2,
            //    //    Unknown3 = kMPPropertyGridSettings.IGTSSection.Unknown3,
            //    //    RGBAColor = new TestXml.KMPXml.StageInfo.RGBA
            //    //    {
            //    //        R = kMPPropertyGridSettings.IGTSSection.RGBAColor.R,
            //    //        G = kMPPropertyGridSettings.IGTSSection.RGBAColor.G,
            //    //        B = kMPPropertyGridSettings.IGTSSection.RGBAColor.B,
            //    //        A = kMPPropertyGridSettings.IGTSSection.RGBAColor.A,
            //    //        FlareAlpha = kMPPropertyGridSettings.IGTSSection.FlareAlpha
            //    //    }
            //    //},
            //    #endregion
            //    GlideRoutes = null
            //};
            #endregion

            KMPData.KMP_XML KMP_Xml = KMPData.KMP_XML.CreateNullDefault();

            if (section == KMPData.KMPXmlSetting.Section.KartPoint) KMP_Xml.startPositions = new KMPData.SectionData.StartPosition(kMPPropertyGridSettings.KMP_Section.TPTK);
            else if (section == KMPData.KMPXmlSetting.Section.EnemyRoutes) KMP_Xml.EnemyRoutes = new KMPData.SectionData.EnemyRoute(kMPPropertyGridSettings.KMP_Section.HPNE, kMPPropertyGridSettings.KMP_Section.TPNE);
            else if (section == KMPData.KMPXmlSetting.Section.ItemRoutes) KMP_Xml.ItemRoutes = new KMPData.SectionData.ItemRoute(kMPPropertyGridSettings.KMP_Section.HPTI, kMPPropertyGridSettings.KMP_Section.TPTI);
            else if (section == KMPData.KMPXmlSetting.Section.CheckPoint) KMP_Xml.Checkpoints = new KMPData.SectionData.Checkpoint(kMPPropertyGridSettings.KMP_Section.HPKC, kMPPropertyGridSettings.KMP_Section.TPKC);
            else if (section == KMPData.KMPXmlSetting.Section.Obj) KMP_Xml.Objects = new KMPData.SectionData.Object(kMPPropertyGridSettings.KMP_Section.JBOG);
            else if (section == KMPData.KMPXmlSetting.Section.Route) KMP_Xml.Routes = new KMPData.SectionData.Route(kMPPropertyGridSettings.KMP_Section.ITOP);
            else if (section == KMPData.KMPXmlSetting.Section.Area) KMP_Xml.Areas = new KMPData.SectionData.Area(kMPPropertyGridSettings.KMP_Section.AERA);
            else if (section == KMPData.KMPXmlSetting.Section.Camera) KMP_Xml.Cameras = new KMPData.SectionData.Camera(kMPPropertyGridSettings.KMP_Section.EMAC);
            else if (section == KMPData.KMPXmlSetting.Section.JugemPoint) KMP_Xml.JugemPoints = new KMPData.SectionData.JugemPoint(kMPPropertyGridSettings.KMP_Section.TPGJ);
            else if (section == KMPData.KMPXmlSetting.Section.GlideRoutes) KMP_Xml.GlideRoutes = new KMPData.SectionData.GlideRoute(kMPPropertyGridSettings.KMP_Section.HPLG, kMPPropertyGridSettings.KMP_Section.TPLG);

            //Delete Namespaces
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPData.KMP_XML));
            System.IO.StreamWriter sw = new StreamWriter(Path + "_" + section.ToString() + ".xml", false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, KMP_Xml, xns);
            sw.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KMPData"></param>
        /// <param name="Path"></param>
        /// <param name="routeType"></param>
        public static void ExportXXXXRoute(Format.KMP KMPData, string Path, XXXXRouteData.XXXXRouteXmlSetting.RouteType routeType)
        {
            XXXXRouteData.XXXXRoute_XML XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML();
            if (routeType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.EnemyRoute)
            {
                XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPNE, KMPData.KMP_Section.TPNE));
            }
            else if (routeType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.ItemRoute)
            {
                XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPTI, KMPData.KMP_Section.TPTI));
            }
            else if (routeType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.GlideRoute)
            {
                XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPLG, KMPData.KMP_Section.TPLG));
            }

            //Delete Namespaces
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(XXXXRouteData.XXXXRoute_XML));
            System.IO.StreamWriter sw = new StreamWriter(Path + "_PositionAndScaleOnly" + ".xml", false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, XXXXRoute_XML, xns);
            sw.Close();
        }

        //public static void ExportXXXXRoute(KMP_Main_PGS kMPPropertyGridSettings, string Path, XXXXRouteData.XXXXRouteXmlSetting.RouteType routeType)
        //{
        //    XXXXRouteData.XXXXRoute_XML XXXXRoute_Xml = new XXXXRouteData.XXXXRoute_XML
        //    {
        //        XXXXRoutes = new XXXXRouteData.XXXXRoute_XML.XXXXRoute
        //        {
        //            Groups = null
        //        }
        //    };

        //    if (routeType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.EnemyRoute)
        //    {
        //        List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData> groupDatas = new List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData>();

        //        foreach (var Groups in kMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValueList)
        //        {
        //            XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData groupData = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData
        //            {
        //                Points = null
        //            };

        //            List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData> pointDatas = new List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData>();

        //            foreach (var Points in Groups.TPNEValueList)
        //            {
        //                XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData pointData = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData
        //                {
        //                    Position = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData.PositionData
        //                    {
        //                        X = Points.Positions.X,
        //                        Y = Points.Positions.Y,
        //                        Z = Points.Positions.Z
        //                    },
        //                    ScaleValue = Points.Control
        //                };

        //                pointDatas.Add(pointData);
        //            }

        //            groupData.Points = pointDatas;

        //            groupDatas.Add(groupData);
        //        }

        //        XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
        //    }
        //    if (routeType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.ItemRoute)
        //    {
        //        List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData> groupDatas = new List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData>();

        //        foreach (var Groups in kMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValueList)
        //        {
        //            XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData groupData = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData
        //            {
        //                Points = null
        //            };

        //            List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData> pointDatas = new List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData>();

        //            foreach (var Points in Groups.TPTIValueList)
        //            {
        //                XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData pointData = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData
        //                {
        //                    Position = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData.PositionData
        //                    {
        //                        X = Points.TPTI_Positions.X,
        //                        Y = Points.TPTI_Positions.Y,
        //                        Z = Points.TPTI_Positions.Z
        //                    },
        //                    ScaleValue = Points.TPTI_PointSize
        //                };

        //                pointDatas.Add(pointData);
        //            }

        //            groupData.Points = pointDatas;

        //            groupDatas.Add(groupData);
        //        }

        //        XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
        //    }
        //    if (routeType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.GlideRoute)
        //    {
        //        List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData> groupDatas = new List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData>();

        //        foreach (var Groups in kMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValueList)
        //        {
        //            XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData groupData = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData
        //            {
        //                Points = null
        //            };

        //            List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData> pointDatas = new List<XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData>();

        //            foreach (var Points in Groups.TPLGValueList)
        //            {
        //                XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData pointData = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData
        //                {
        //                    Position = new XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData.PositionData
        //                    {
        //                        X = Points.Positions.X,
        //                        Y = Points.Positions.Y,
        //                        Z = Points.Positions.Z
        //                    },
        //                    ScaleValue = Points.TPLG_PointScaleValue
        //                };

        //                pointDatas.Add(pointData);
        //            }

        //            groupData.Points = pointDatas;

        //            groupDatas.Add(groupData);
        //        }

        //        XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
        //    }

        //    //Delete Namespaces
        //    var xns = new XmlSerializerNamespaces();
        //    xns.Add(string.Empty, string.Empty);

        //    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(XXXXRouteData.XXXXRoute_XML));
        //    System.IO.StreamWriter sw = new StreamWriter(Path + "_PositionAndScaleOnly" + ".xml", false, new System.Text.UTF8Encoding(false));
        //    serializer.Serialize(sw, XXXXRoute_Xml, xns);
        //    sw.Close();
        //}
    }
}
