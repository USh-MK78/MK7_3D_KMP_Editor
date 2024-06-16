//using HelixToolkit.Wpf;
//using MK7_KMP_Editor_For_PG_.PropertyGridObject;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Media.Media3D;
//using System.Xml.Serialization;

//namespace MK7_KMP_Editor_For_PG_
//{
//    //public class XMLExporter
//    //{
//    //    //public static void ExportAll(KMP_Main_PGS kMPPropertyGridSettings, string Path)
//    //    //{
//    //    //    //KMPLibrary.XMLConvert.KMPData.KMP_XML kMP_XML = new KMPLibrary.XMLConvert.KMPData.KMP_XML(kMPPropertyGridSettings)


//    //    //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml = new KMPLibrary.XMLConvert.KMPData.KMP_XML
//    //    //    {
//    //    //        startPositions = new KMPLibrary.XMLConvert.KMPData.SectionData.StartPosition(kMPPropertyGridSettings.TPTKSection),
//    //    //        EnemyRoutes = new KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute(kMPPropertyGridSettings.HPNE_TPNESection),
//    //    //        ItemRoutes = new KMPLibrary.XMLConvert.KMPData.SectionData.ItemRoute(kMPPropertyGridSettings.HPTI_TPTISection),
//    //    //        Checkpoints = new KMPLibrary.XMLConvert.KMPData.SectionData.Checkpoint(kMPPropertyGridSettings.HPKC_TPKCSection),
//    //    //        Objects = new KMPLibrary.XMLConvert.KMPData.SectionData.Object(kMPPropertyGridSettings.JBOGSection),
//    //    //        Routes = new KMPLibrary.XMLConvert.KMPData.SectionData.Route(kMPPropertyGridSettings.ITOPSection),
//    //    //        Areas = new KMPLibrary.XMLConvert.KMPData.SectionData.Area(kMPPropertyGridSettings.AERASection),
//    //    //        Cameras = new KMPLibrary.XMLConvert.KMPData.SectionData.Camera(kMPPropertyGridSettings.EMACSection),
//    //    //        JugemPoints = new KMPLibrary.XMLConvert.KMPData.SectionData.JugemPoint(kMPPropertyGridSettings.TPGJSection),
//    //    //        Stage_Info = new TestXml.KMPXml.StageInfo
//    //    //        {
//    //    //            Unknown1 = kMPPropertyGridSettings.IGTSSection.Unknown1,
//    //    //            LapCount = kMPPropertyGridSettings.IGTSSection.LapCount,
//    //    //            PolePosition = kMPPropertyGridSettings.IGTSSection.PolePosition,
//    //    //            Unknown2 = kMPPropertyGridSettings.IGTSSection.Unknown2,
//    //    //            Unknown3 = kMPPropertyGridSettings.IGTSSection.Unknown3,
//    //    //            RGBAColor = new TestXml.KMPXml.StageInfo.RGBA
//    //    //            {
//    //    //                R = kMPPropertyGridSettings.IGTSSection.RGBAColor.R,
//    //    //                G = kMPPropertyGridSettings.IGTSSection.RGBAColor.G,
//    //    //                B = kMPPropertyGridSettings.IGTSSection.RGBAColor.B,
//    //    //                A = kMPPropertyGridSettings.IGTSSection.RGBAColor.A,
//    //    //                FlareAlpha = kMPPropertyGridSettings.IGTSSection.FlareAlpha
//    //    //            }
//    //    //        },
//    //    //        GlideRoutes = new TestXml.KMPXml.GlideRoute(kMPPropertyGridSettings.HPLG_TPLGSection)
//    //    //    };

//    //    //    //Delete Namespaces
//    //    //    var xns = new XmlSerializerNamespaces();
//    //    //    xns.Add(string.Empty, string.Empty);

//    //    //    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.KMPXml));
//    //    //    System.IO.StreamWriter sw = new StreamWriter(Path + "_All.xml", false, new System.Text.UTF8Encoding(false));
//    //    //    serializer.Serialize(sw, KMP_Xml, xns);
//    //    //    sw.Close();
//    //    //}

//    //    //public static void ExportAll(KMPLibrary.Format.KMP kMPPropertyGridSettings, string Path)
//    //    //{
//    //    //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_XML = new KMPLibrary.XMLConvert.KMPData.KMP_XML(kMPPropertyGridSettings);

//    //    //    //Delete Namespaces
//    //    //    var xns = new XmlSerializerNamespaces();
//    //    //    xns.Add(string.Empty, string.Empty);

//    //    //    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPLibrary.XMLConvert.KMPData.KMP_XML));
//    //    //    System.IO.StreamWriter sw = new StreamWriter(Path + "_All.xml", false, new System.Text.UTF8Encoding(false));
//    //    //    serializer.Serialize(sw, KMP_XML, xns);
//    //    //    sw.Close();
//    //    //}

//    //    //public static void ExportSection(KMP_Main_PGS kMPPropertyGridSettings, string Path, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section section)
//    //    //{
//    //    //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml = new KMPLibrary.XMLConvert.KMPData.KMP_XML
//    //    //    {
//    //    //        startPositions = null,
//    //    //        EnemyRoutes = null,
//    //    //        ItemRoutes = null,
//    //    //        Checkpoints = null,
//    //    //        Objects = null,
//    //    //        Routes = null,
//    //    //        Areas = null,
//    //    //        Cameras = null,
//    //    //        JugemPoints = null,
//    //    //        #region Hide
//    //    //        //Stage_Info = new TestXml.KMPXml.StageInfo
//    //    //        //{
//    //    //        //    Unknown1 = kMPPropertyGridSettings.IGTSSection.Unknown1,
//    //    //        //    LapCount = kMPPropertyGridSettings.IGTSSection.LapCount,
//    //    //        //    PolePosition = kMPPropertyGridSettings.IGTSSection.PolePosition,
//    //    //        //    Unknown2 = kMPPropertyGridSettings.IGTSSection.Unknown2,
//    //    //        //    Unknown3 = kMPPropertyGridSettings.IGTSSection.Unknown3,
//    //    //        //    RGBAColor = new TestXml.KMPXml.StageInfo.RGBA
//    //    //        //    {
//    //    //        //        R = kMPPropertyGridSettings.IGTSSection.RGBAColor.R,
//    //    //        //        G = kMPPropertyGridSettings.IGTSSection.RGBAColor.G,
//    //    //        //        B = kMPPropertyGridSettings.IGTSSection.RGBAColor.B,
//    //    //        //        A = kMPPropertyGridSettings.IGTSSection.RGBAColor.A,
//    //    //        //        FlareAlpha = kMPPropertyGridSettings.IGTSSection.FlareAlpha
//    //    //        //    }
//    //    //        //},
//    //    //        #endregion
//    //    //        GlideRoutes = null
//    //    //    };

//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.KartPoint) KMP_Xml.startPositions = new KMPLibrary.XMLConvert.KMPData.SectionData.StartPosition(kMPPropertyGridSettings.TPTKSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.EnemyRoutes) KMP_Xml.EnemyRoutes = new KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute(kMPPropertyGridSettings.HPNE_TPNESection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.ItemRoutes) KMP_Xml.ItemRoutes = new TestXml.KMPXml.ItemRoute(kMPPropertyGridSettings.HPTI_TPTISection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.CheckPoint) KMP_Xml.Checkpoints = new TestXml.KMPXml.Checkpoint(kMPPropertyGridSettings.HPKC_TPKCSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Obj) KMP_Xml.Objects = new TestXml.KMPXml.Object(kMPPropertyGridSettings.JBOGSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Route) KMP_Xml.Routes = new TestXml.KMPXml.Route(kMPPropertyGridSettings.ITOPSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Area) KMP_Xml.Areas = new TestXml.KMPXml.Area(kMPPropertyGridSettings.AERASection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Camera) KMP_Xml.Cameras = new TestXml.KMPXml.Camera(kMPPropertyGridSettings.EMACSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.JugemPoint) KMP_Xml.JugemPoints = new TestXml.KMPXml.JugemPoint(kMPPropertyGridSettings.TPGJSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.GlideRoutes) KMP_Xml.GlideRoutes = new TestXml.KMPXml.GlideRoute(kMPPropertyGridSettings.HPLG_TPLGSection);

//    //    //    //Delete Namespaces
//    //    //    var xns = new XmlSerializerNamespaces();
//    //    //    xns.Add(string.Empty, string.Empty);

//    //    //    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPLibrary.XMLConvert.KMPData.SectionData));
//    //    //    System.IO.StreamWriter sw = new StreamWriter(Path + "_" + section.ToString() + ".xml", false, new System.Text.UTF8Encoding(false));
//    //    //    serializer.Serialize(sw, KMP_Xml, xns);
//    //    //    sw.Close();
//    //    //}

//    //    //public static void ExportSection(KMPLibrary.Format.KMP kMPPropertyGridSettings, string Path, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section section)
//    //    //{
//    //    //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml = new KMPLibrary.XMLConvert.KMPData.KMP_XML
//    //    //    {
//    //    //        startPositions = null,
//    //    //        EnemyRoutes = null,
//    //    //        ItemRoutes = null,
//    //    //        Checkpoints = null,
//    //    //        Objects = null,
//    //    //        Routes = null,
//    //    //        Areas = null,
//    //    //        Cameras = null,
//    //    //        JugemPoints = null,
//    //    //        #region Hide
//    //    //        //Stage_Info = new TestXml.KMPXml.StageInfo
//    //    //        //{
//    //    //        //    Unknown1 = kMPPropertyGridSettings.IGTSSection.Unknown1,
//    //    //        //    LapCount = kMPPropertyGridSettings.IGTSSection.LapCount,
//    //    //        //    PolePosition = kMPPropertyGridSettings.IGTSSection.PolePosition,
//    //    //        //    Unknown2 = kMPPropertyGridSettings.IGTSSection.Unknown2,
//    //    //        //    Unknown3 = kMPPropertyGridSettings.IGTSSection.Unknown3,
//    //    //        //    RGBAColor = new TestXml.KMPXml.StageInfo.RGBA
//    //    //        //    {
//    //    //        //        R = kMPPropertyGridSettings.IGTSSection.RGBAColor.R,
//    //    //        //        G = kMPPropertyGridSettings.IGTSSection.RGBAColor.G,
//    //    //        //        B = kMPPropertyGridSettings.IGTSSection.RGBAColor.B,
//    //    //        //        A = kMPPropertyGridSettings.IGTSSection.RGBAColor.A,
//    //    //        //        FlareAlpha = kMPPropertyGridSettings.IGTSSection.FlareAlpha
//    //    //        //    }
//    //    //        //},
//    //    //        #endregion
//    //    //        GlideRoutes = null
//    //    //    };

//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.KartPoint) KMP_Xml.startPositions = new KMPLibrary.XMLConvert.KMPData.SectionData.StartPosition(kMPPropertyGridSettings.KMP_Section.TPTK);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.EnemyRoutes) KMP_Xml.EnemyRoutes = new KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute(kMPPropertyGridSettings.HPNE_TPNESection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.ItemRoutes) KMP_Xml.ItemRoutes = new TestXml.KMPXml.ItemRoute(kMPPropertyGridSettings.HPTI_TPTISection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.CheckPoint) KMP_Xml.Checkpoints = new TestXml.KMPXml.Checkpoint(kMPPropertyGridSettings.HPKC_TPKCSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Obj) KMP_Xml.Objects = new TestXml.KMPXml.Object(kMPPropertyGridSettings.JBOGSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Route) KMP_Xml.Routes = new TestXml.KMPXml.Route(kMPPropertyGridSettings.ITOPSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Area) KMP_Xml.Areas = new TestXml.KMPXml.Area(kMPPropertyGridSettings.AERASection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Camera) KMP_Xml.Cameras = new TestXml.KMPXml.Camera(kMPPropertyGridSettings.EMACSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.JugemPoint) KMP_Xml.JugemPoints = new TestXml.KMPXml.JugemPoint(kMPPropertyGridSettings.TPGJSection);
//    //    //    if (section == KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.GlideRoutes) KMP_Xml.GlideRoutes = new TestXml.KMPXml.GlideRoute(kMPPropertyGridSettings.HPLG_TPLGSection);

//    //    //    //Delete Namespaces
//    //    //    var xns = new XmlSerializerNamespaces();
//    //    //    xns.Add(string.Empty, string.Empty);

//    //    //    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPLibrary.XMLConvert.KMPData.SectionData));
//    //    //    System.IO.StreamWriter sw = new StreamWriter(Path + "_" + section.ToString() + ".xml", false, new System.Text.UTF8Encoding(false));
//    //    //    serializer.Serialize(sw, KMP_Xml, xns);
//    //    //    sw.Close();
//    //    //}


//    //    //public static void ExportXXXXRoute(KMP_Main_PGS kMPPropertyGridSettings, string Path, TestXml.XXXXRouteXmlSetting.RouteType routeType)
//    //    //{
//    //    //    TestXml.XXXXRouteXml XXXXRoute_Xml = new TestXml.XXXXRouteXml
//    //    //    {
//    //    //        XXXXRoutes = new TestXml.XXXXRouteXml.XXXXRoute
//    //    //        {
//    //    //            Groups = null
//    //    //        }
//    //    //    };

//    //    //    if (routeType == TestXml.XXXXRouteXmlSetting.RouteType.EnemyRoute)
//    //    //    {
//    //    //        List<TestXml.XXXXRouteXml.XXXXRoute.GroupData> groupDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData>();

//    //    //        foreach (var Groups in kMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValueList)
//    //    //        {
//    //    //            TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData
//    //    //            {
//    //    //                Points = null
//    //    //            };

//    //    //            List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData> pointDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData>();

//    //    //            foreach (var Points in Groups.TPNEValueList)
//    //    //            {
//    //    //                TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData
//    //    //                {
//    //    //                    Position = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData.PositionData
//    //    //                    {
//    //    //                        X = Points.Positions.X,
//    //    //                        Y = Points.Positions.Y,
//    //    //                        Z = Points.Positions.Z
//    //    //                    },
//    //    //                    ScaleValue = Points.Control
//    //    //                };

//    //    //                pointDatas.Add(pointData);
//    //    //            }

//    //    //            groupData.Points = pointDatas;

//    //    //            groupDatas.Add(groupData);
//    //    //        }

//    //    //        XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
//    //    //    }
//    //    //    if (routeType == TestXml.XXXXRouteXmlSetting.RouteType.ItemRoute)
//    //    //    {
//    //    //        List<TestXml.XXXXRouteXml.XXXXRoute.GroupData> groupDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData>();

//    //    //        foreach (var Groups in kMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValueList)
//    //    //        {
//    //    //            TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData
//    //    //            {
//    //    //                Points = null
//    //    //            };

//    //    //            List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData> pointDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData>();

//    //    //            foreach (var Points in Groups.TPTIValueList)
//    //    //            {
//    //    //                TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData
//    //    //                {
//    //    //                    Position = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData.PositionData
//    //    //                    {
//    //    //                        X = Points.TPTI_Positions.X,
//    //    //                        Y = Points.TPTI_Positions.Y,
//    //    //                        Z = Points.TPTI_Positions.Z
//    //    //                    },
//    //    //                    ScaleValue = Points.TPTI_PointSize
//    //    //                };

//    //    //                pointDatas.Add(pointData);
//    //    //            }

//    //    //            groupData.Points = pointDatas;

//    //    //            groupDatas.Add(groupData);
//    //    //        }

//    //    //        XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
//    //    //    }
//    //    //    if (routeType == TestXml.XXXXRouteXmlSetting.RouteType.GlideRoute)
//    //    //    {
//    //    //        List<TestXml.XXXXRouteXml.XXXXRoute.GroupData> groupDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData>();

//    //    //        foreach (var Groups in kMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValueList)
//    //    //        {
//    //    //            TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData
//    //    //            {
//    //    //                Points = null
//    //    //            };

//    //    //            List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData> pointDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData>();

//    //    //            foreach (var Points in Groups.TPLGValueList)
//    //    //            {
//    //    //                TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData
//    //    //                {
//    //    //                    Position = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData.PositionData
//    //    //                    {
//    //    //                        X = Points.Positions.X,
//    //    //                        Y = Points.Positions.Y,
//    //    //                        Z = Points.Positions.Z
//    //    //                    },
//    //    //                    ScaleValue = Points.TPLG_PointScaleValue
//    //    //                };

//    //    //                pointDatas.Add(pointData);
//    //    //            }

//    //    //            groupData.Points = pointDatas;

//    //    //            groupDatas.Add(groupData);
//    //    //        }

//    //    //        XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
//    //    //    }

//    //    //    //Delete Namespaces
//    //    //    var xns = new XmlSerializerNamespaces();
//    //    //    xns.Add(string.Empty, string.Empty);

//    //    //    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.XXXXRouteXml));
//    //    //    System.IO.StreamWriter sw = new StreamWriter(Path + "_PositionAndScaleOnly" + ".xml", false, new System.Text.UTF8Encoding(false));
//    //    //    serializer.Serialize(sw, XXXXRoute_Xml, xns);
//    //    //    sw.Close();
//    //    //}
//    //}

//    //public class XMLImporter
//    //{
//    //    public static T XMLImport<T>(string Path)
//    //    {
//    //        System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
//    //        System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(T));
//    //        return (T)s1.Deserialize(fs1);
//    //    }

//    //    public static KMP_Main_PGS ImportAll(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl, string YOffsetValue)
//    //    {
//    //        //KMPPropertyGridSettings kMPPropertyGridSettings = new KMPPropertyGridSettings
//    //        //{
//    //        //    TPTKSection = null,
//    //        //    HPNE_TPNESection = null,
//    //        //    HPTI_TPTISection = null,
//    //        //    HPKC_TPKCSection = null,
//    //        //    JBOGSection = null,
//    //        //    ITOPSection = null,
//    //        //    AERASection = null,
//    //        //    EMACSection = null,
//    //        //    TPGJSection = null,
//    //        //    IGTSSection = null,
//    //        //    HPLG_TPLGSection = null
//    //        //};

//    //        //KMP_Main_PGS kMPPropertyGridSettings = new KMP_Main_PGS();

//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        KMP_Main_PGS kMPPropertyGridSettings = new KMP_Main_PGS(KMP_Xml_Model);

//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_StartPosition(UserCtrl, KMPViewportObject, KMP_Xml_Model.startPositions);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_EnemyRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_ItemRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Checkpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(YOffsetValue));
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Object(UserCtrl, KMPViewportObject, KMP_Xml_Model.Objects, Render.KMPRendering.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Route(UserCtrl, KMPViewportObject, KMP_Xml_Model.Routes);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Area(UserCtrl, KMPViewportObject, KMP_Xml_Model.Areas);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Camera(UserCtrl, KMPViewportObject, KMP_Xml_Model.Cameras);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Returnpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.JugemPoints);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_GlideRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.GlideRoutes);

//    //        #region DELETE
//    //        //#region KartPoint
//    //        //KMPPropertyGridSettings.TPTK_Section TPTK_Section = new KMPPropertyGridSettings.TPTK_Section(KMP_Xml_Model.startPositions);
//    //        //Render.KMPRendering.KMPViewportRenderingXML.Render_StartPosition(UserCtrl, KMPViewportObject, KMP_Xml_Model.startPositions);
//    //        //kMPPropertyGridSettings.TPTKSection = TPTK_Section;
//    //        //#endregion

//    //        //#region Enemy_Routes
//    //        //KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section(KMP_Xml_Model.EnemyRoutes);
//    //        //KMPs.KMPViewportRenderingXML.Render_EnemyRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
//    //        //kMPPropertyGridSettings.HPNE_TPNESection = HPNE_TPNE_Section;
//    //        //#endregion

//    //        //#region Item Routes
//    //        //KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section(KMP_Xml_Model.ItemRoutes);
//    //        //KMPs.KMPViewportRenderingXML.Render_ItemRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
//    //        //kMPPropertyGridSettings.HPTI_TPTISection = HPTI_TPTI_Section;
//    //        //#endregion

//    //        //#region CheckPoint
//    //        //KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section(KMP_Xml_Model.Checkpoints);
//    //        //KMPs.KMPViewportRenderingXML.Render_Checkpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(YOffsetValue));
//    //        //kMPPropertyGridSettings.HPKC_TPKCSection = HPKC_TPKC_Section;
//    //        //#endregion

//    //        //#region OBJ
//    //        //KMPPropertyGridSettings.JBOG_Section JBOG_Section = new KMPPropertyGridSettings.JBOG_Section(KMP_Xml_Model.Objects);
//    //        //KMPs.KMPViewportRenderingXML.Render_Object(UserCtrl, KMPViewportObject, KMP_Xml_Model.Objects, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
//    //        //kMPPropertyGridSettings.JBOGSection = JBOG_Section;
//    //        //#endregion

//    //        //#region Route
//    //        //KMPPropertyGridSettings.ITOP_Section ITOP_Section = new KMPPropertyGridSettings.ITOP_Section(KMP_Xml_Model.Routes);
//    //        //KMPs.KMPViewportRenderingXML.Render_Route(UserCtrl, KMPViewportObject, KMP_Xml_Model.Routes);
//    //        //kMPPropertyGridSettings.ITOPSection = ITOP_Section;
//    //        //#endregion

//    //        //#region Area
//    //        //KMPPropertyGridSettings.AERA_Section AERA_Section = new KMPPropertyGridSettings.AERA_Section(KMP_Xml_Model.Areas);
//    //        //KMPs.KMPViewportRenderingXML.Render_Area(UserCtrl, KMPViewportObject, KMP_Xml_Model.Areas);
//    //        //kMPPropertyGridSettings.AERASection = AERA_Section;
//    //        //#endregion

//    //        //#region Camera
//    //        //KMPPropertyGridSettings.EMAC_Section EMAC_Section = new KMPPropertyGridSettings.EMAC_Section(KMP_Xml_Model.Cameras);
//    //        //KMPs.KMPViewportRenderingXML.Render_Camera(UserCtrl, KMPViewportObject, KMP_Xml_Model.Cameras);
//    //        //kMPPropertyGridSettings.EMACSection = EMAC_Section;
//    //        //#endregion

//    //        //#region JugemPoint
//    //        //KMPPropertyGridSettings.TPGJ_Section TPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section(KMP_Xml_Model.JugemPoints);
//    //        //KMPs.KMPViewportRenderingXML.Render_Returnpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.JugemPoints);
//    //        //kMPPropertyGridSettings.TPGJSection = TPGJ_Section;
//    //        //#endregion

//    //        ////TPNC : Unused Section
//    //        ////TPSM : Unused Section

//    //        //#region StageInfo
//    //        //kMPPropertyGridSettings.IGTSSection = new KMPPropertyGridSettings.IGTS_Section(KMP_Xml_Model.Stage_Info);
//    //        //#endregion

//    //        ////SROC : Unused Section

//    //        //#region GlideRoute
//    //        //KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section(KMP_Xml_Model.GlideRoutes);
//    //        //KMPs.KMPViewportRenderingXML.Render_GlideRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
//    //        //kMPPropertyGridSettings.HPLG_TPLGSection = HPLG_TPLG_Section;
//    //        //#endregion
//    //        #endregion

//    //        return kMPPropertyGridSettings;
//    //    }

//    //    public static KartPoint_PGS ImportKartPosition(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        KartPoint_PGS TPTK_Section = new KartPoint_PGS(KMP_Xml_Model.startPositions);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_StartPosition(UserCtrl, KMPViewportObject, KMP_Xml_Model.startPositions);
//    //        return TPTK_Section;
//    //    }

//    //    public static EnemyRoute_PGS ImportEnemyRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        EnemyRoute_PGS HPNE_TPNE_Section = new EnemyRoute_PGS(KMP_Xml_Model.EnemyRoutes);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_EnemyRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
//    //        return HPNE_TPNE_Section;
//    //    }

//    //    public static ItemRoute_PGS ImportItemRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        ItemRoute_PGS HPTI_TPTI_Section = new ItemRoute_PGS(KMP_Xml_Model.ItemRoutes);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_ItemRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
//    //        return HPTI_TPTI_Section;
//    //    }

//    //    public static Checkpoint_PGS ImportCheckpoint(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl, string YOffsetValue)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        Checkpoint_PGS HPKC_TPKC_Section = new Checkpoint_PGS(KMP_Xml_Model.Checkpoints);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Checkpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(YOffsetValue));
//    //        return HPKC_TPKC_Section;
//    //    }

//    //    public static KMPObject_PGS ImportObject(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        KMPObject_PGS JBOG_Section = new KMPObject_PGS(KMP_Xml_Model.Objects);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Object(UserCtrl, KMPViewportObject, KMP_Xml_Model.Objects, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
//    //        return JBOG_Section;
//    //    }

//    //    public static Route_PGS ImportRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        Route_PGS ITOP_Section = new Route_PGS(KMP_Xml_Model.Routes);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Route(UserCtrl, KMPViewportObject, KMP_Xml_Model.Routes);
//    //        return ITOP_Section;
//    //    }

//    //    public static Area_PGS ImportArea(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        Area_PGS AERA_Section = new Area_PGS(KMP_Xml_Model.Areas);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Area(UserCtrl, KMPViewportObject, KMP_Xml_Model.Areas);
//    //        return AERA_Section;
//    //    }

//    //    public static Camera_PGS ImportCamera(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        Camera_PGS EMAC_Section = new Camera_PGS(KMP_Xml_Model.Cameras);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Camera(UserCtrl, KMPViewportObject, KMP_Xml_Model.Cameras);
//    //        return EMAC_Section;
//    //    }

//    //    public static RespawnPoint_PGS ImportJugemPoint(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        RespawnPoint_PGS TPGJ_Section = new RespawnPoint_PGS(KMP_Xml_Model.JugemPoints);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_Returnpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.JugemPoints);
//    //        return TPGJ_Section;
//    //    }

//    //    public static GlideRoute_PGS ImportGlideRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
//    //        GlideRoute_PGS HPLG_TPLG_Section = new GlideRoute_PGS(KMP_Xml_Model.GlideRoutes);
//    //        Render.KMPRendering.KMPViewportRenderingXML.Render_GlideRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
//    //        return HPLG_TPLG_Section;
//    //    }

//    //    #region XXXX Route Importer
//    //    public static EnemyRoute_PGS ImportEnemyRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
//    //        EnemyRoute_PGS HPNE_TPNE_Section = new EnemyRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
//    //        Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_EnemyRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
//    //        return HPNE_TPNE_Section;
//    //    }

//    //    public static ItemRoute_PGS ImportItemRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
//    //        ItemRoute_PGS HPTI_TPTI_Section = new ItemRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
//    //        Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_ItemRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
//    //        return HPTI_TPTI_Section;
//    //    }

//    //    public static GlideRoute_PGS ImportGlideRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
//    //    {
//    //        TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
//    //        GlideRoute_PGS HPLG_TPLG_Section = new GlideRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
//    //        Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_GlideRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
//    //        return HPLG_TPLG_Section;
//    //    }
//    //    #endregion
//    //}
//}
