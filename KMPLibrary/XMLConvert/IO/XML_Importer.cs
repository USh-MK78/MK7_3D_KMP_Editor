using KMPLibrary.Format;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMPLibrary.XMLConvert.KMPData.SectionData;

namespace KMPLibrary.XMLConvert.IO
{
    public class XML_Importer
    {
        public static T XMLImport<T>(string Path)
        {
            System.IO.FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer Serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)Serializer.Deserialize(fs);
        }

        #region DELETE
        //public static KMPData.KMP_XML ImportAll(string Path)
        //{
        //    //KMPPropertyGridSettings kMPPropertyGridSettings = new KMPPropertyGridSettings
        //    //{
        //    //    TPTKSection = null,
        //    //    HPNE_TPNESection = null,
        //    //    HPTI_TPTISection = null,
        //    //    HPKC_TPKCSection = null,
        //    //    JBOGSection = null,
        //    //    ITOPSection = null,
        //    //    AERASection = null,
        //    //    EMACSection = null,
        //    //    TPGJSection = null,
        //    //    IGTSSection = null,
        //    //    HPLG_TPLGSection = null
        //    //};

        //    //KMP_Main_PGS kMPPropertyGridSettings = new KMP_Main_PGS();

        //    return XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);



        //    #region DELETE
        //    //#region KartPoint
        //    //KMPPropertyGridSettings.TPTK_Section TPTK_Section = new KMPPropertyGridSettings.TPTK_Section(KMP_Xml_Model.startPositions);
        //    //Render.KMPRendering.KMPViewportRenderingXML.Render_StartPosition(UserCtrl, KMPViewportObject, KMP_Xml_Model.startPositions);
        //    //kMPPropertyGridSettings.TPTKSection = TPTK_Section;
        //    //#endregion

        //    //#region Enemy_Routes
        //    //KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section(KMP_Xml_Model.EnemyRoutes);
        //    //KMPs.KMPViewportRenderingXML.Render_EnemyRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
        //    //kMPPropertyGridSettings.HPNE_TPNESection = HPNE_TPNE_Section;
        //    //#endregion

        //    //#region Item Routes
        //    //KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section(KMP_Xml_Model.ItemRoutes);
        //    //KMPs.KMPViewportRenderingXML.Render_ItemRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
        //    //kMPPropertyGridSettings.HPTI_TPTISection = HPTI_TPTI_Section;
        //    //#endregion

        //    //#region CheckPoint
        //    //KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section(KMP_Xml_Model.Checkpoints);
        //    //KMPs.KMPViewportRenderingXML.Render_Checkpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(YOffsetValue));
        //    //kMPPropertyGridSettings.HPKC_TPKCSection = HPKC_TPKC_Section;
        //    //#endregion

        //    //#region OBJ
        //    //KMPPropertyGridSettings.JBOG_Section JBOG_Section = new KMPPropertyGridSettings.JBOG_Section(KMP_Xml_Model.Objects);
        //    //KMPs.KMPViewportRenderingXML.Render_Object(UserCtrl, KMPViewportObject, KMP_Xml_Model.Objects, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
        //    //kMPPropertyGridSettings.JBOGSection = JBOG_Section;
        //    //#endregion

        //    //#region Route
        //    //KMPPropertyGridSettings.ITOP_Section ITOP_Section = new KMPPropertyGridSettings.ITOP_Section(KMP_Xml_Model.Routes);
        //    //KMPs.KMPViewportRenderingXML.Render_Route(UserCtrl, KMPViewportObject, KMP_Xml_Model.Routes);
        //    //kMPPropertyGridSettings.ITOPSection = ITOP_Section;
        //    //#endregion

        //    //#region Area
        //    //KMPPropertyGridSettings.AERA_Section AERA_Section = new KMPPropertyGridSettings.AERA_Section(KMP_Xml_Model.Areas);
        //    //KMPs.KMPViewportRenderingXML.Render_Area(UserCtrl, KMPViewportObject, KMP_Xml_Model.Areas);
        //    //kMPPropertyGridSettings.AERASection = AERA_Section;
        //    //#endregion

        //    //#region Camera
        //    //KMPPropertyGridSettings.EMAC_Section EMAC_Section = new KMPPropertyGridSettings.EMAC_Section(KMP_Xml_Model.Cameras);
        //    //KMPs.KMPViewportRenderingXML.Render_Camera(UserCtrl, KMPViewportObject, KMP_Xml_Model.Cameras);
        //    //kMPPropertyGridSettings.EMACSection = EMAC_Section;
        //    //#endregion

        //    //#region JugemPoint
        //    //KMPPropertyGridSettings.TPGJ_Section TPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section(KMP_Xml_Model.JugemPoints);
        //    //KMPs.KMPViewportRenderingXML.Render_Returnpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.JugemPoints);
        //    //kMPPropertyGridSettings.TPGJSection = TPGJ_Section;
        //    //#endregion

        //    ////TPNC : Unused Section
        //    ////TPSM : Unused Section

        //    //#region StageInfo
        //    //kMPPropertyGridSettings.IGTSSection = new KMPPropertyGridSettings.IGTS_Section(KMP_Xml_Model.Stage_Info);
        //    //#endregion

        //    ////SROC : Unused Section

        //    //#region GlideRoute
        //    //KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section(KMP_Xml_Model.GlideRoutes);
        //    //KMPs.KMPViewportRenderingXML.Render_GlideRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
        //    //kMPPropertyGridSettings.HPLG_TPLGSection = HPLG_TPLG_Section;
        //    //#endregion
        //    #endregion

        //    //return kMPPropertyGridSettings;
        //}

        //public static KartPoint_PGS ImportKartPosition(string Path)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);

        //    //return TPTK_Section;
        //}

        //public static EnemyRoute_PGS ImportEnemyRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    EnemyRoute_PGS HPNE_TPNE_Section = new EnemyRoute_PGS(KMP_Xml_Model.EnemyRoutes);

        //    return HPNE_TPNE_Section;
        //}

        //public static ItemRoute_PGS ImportItemRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    ItemRoute_PGS HPTI_TPTI_Section = new ItemRoute_PGS(KMP_Xml_Model.ItemRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_ItemRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
        //    return HPTI_TPTI_Section;
        //}

        //public static Checkpoint_PGS ImportCheckpoint(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl, string YOffsetValue)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    Checkpoint_PGS HPKC_TPKC_Section = new Checkpoint_PGS(KMP_Xml_Model.Checkpoints);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Checkpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(YOffsetValue));
        //    return HPKC_TPKC_Section;
        //}

        //public static KMPObject_PGS ImportObject(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    KMPObject_PGS JBOG_Section = new KMPObject_PGS(KMP_Xml_Model.Objects);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Object(UserCtrl, KMPViewportObject, KMP_Xml_Model.Objects, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
        //    return JBOG_Section;
        //}

        //public static Route_PGS ImportRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    Route_PGS ITOP_Section = new Route_PGS(KMP_Xml_Model.Routes);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Route(UserCtrl, KMPViewportObject, KMP_Xml_Model.Routes);
        //    return ITOP_Section;
        //}

        //public static Area_PGS ImportArea(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    Area_PGS AERA_Section = new Area_PGS(KMP_Xml_Model.Areas);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Area(UserCtrl, KMPViewportObject, KMP_Xml_Model.Areas);
        //    return AERA_Section;
        //}

        //public static Camera_PGS ImportCamera(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    Camera_PGS EMAC_Section = new Camera_PGS(KMP_Xml_Model.Cameras);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Camera(UserCtrl, KMPViewportObject, KMP_Xml_Model.Cameras);
        //    return EMAC_Section;
        //}

        //public static RespawnPoint_PGS ImportJugemPoint(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    RespawnPoint_PGS TPGJ_Section = new RespawnPoint_PGS(KMP_Xml_Model.JugemPoints);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Returnpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.JugemPoints);
        //    return TPGJ_Section;
        //}

        //public static GlideRoute_PGS ImportGlideRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    GlideRoute_PGS HPLG_TPLG_Section = new GlideRoute_PGS(KMP_Xml_Model.GlideRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_GlideRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
        //    return HPLG_TPLG_Section;
        //}

        //#region XXXX Route Importer
        //public static EnemyRoute_PGS ImportEnemyRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
        //    EnemyRoute_PGS HPNE_TPNE_Section = new EnemyRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_EnemyRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
        //    return HPNE_TPNE_Section;
        //}

        //public static ItemRoute_PGS ImportItemRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{

        //    return HPTI_TPTI_Section;
        //}

        //public static GlideRoute_PGS ImportGlideRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
        //    GlideRoute_PGS HPLG_TPLG_Section = new GlideRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_GlideRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
        //    return HPLG_TPLG_Section;
        //}
        //#endregion

        //public static KartPoint_PGS ImportKartPosition(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    KartPoint_PGS TPTK_Section = new KartPoint_PGS(KMP_Xml_Model.startPositions);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_StartPosition(UserCtrl, KMPViewportObject, KMP_Xml_Model.startPositions);
        //    return TPTK_Section;
        //}

        //public static EnemyRoute_PGS ImportEnemyRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    EnemyRoute_PGS HPNE_TPNE_Section = new EnemyRoute_PGS(KMP_Xml_Model.EnemyRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_EnemyRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
        //    return HPNE_TPNE_Section;
        //}

        //public static ItemRoute_PGS ImportItemRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    ItemRoute_PGS HPTI_TPTI_Section = new ItemRoute_PGS(KMP_Xml_Model.ItemRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_ItemRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
        //    return HPTI_TPTI_Section;
        //}

        //public static Checkpoint_PGS ImportCheckpoint(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl, string YOffsetValue)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    Checkpoint_PGS HPKC_TPKC_Section = new Checkpoint_PGS(KMP_Xml_Model.Checkpoints);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Checkpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(YOffsetValue));
        //    return HPKC_TPKC_Section;
        //}

        //public static KMPObject_PGS ImportObject(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    KMPObject_PGS JBOG_Section = new KMPObject_PGS(KMP_Xml_Model.Objects);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Object(UserCtrl, KMPViewportObject, KMP_Xml_Model.Objects, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
        //    return JBOG_Section;
        //}

        //public static Route_PGS ImportRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    Route_PGS ITOP_Section = new Route_PGS(KMP_Xml_Model.Routes);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Route(UserCtrl, KMPViewportObject, KMP_Xml_Model.Routes);
        //    return ITOP_Section;
        //}

        //public static Area_PGS ImportArea(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    Area_PGS AERA_Section = new Area_PGS(KMP_Xml_Model.Areas);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Area(UserCtrl, KMPViewportObject, KMP_Xml_Model.Areas);
        //    return AERA_Section;
        //}

        //public static Camera_PGS ImportCamera(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    Camera_PGS EMAC_Section = new Camera_PGS(KMP_Xml_Model.Cameras);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Camera(UserCtrl, KMPViewportObject, KMP_Xml_Model.Cameras);
        //    return EMAC_Section;
        //}

        //public static RespawnPoint_PGS ImportJugemPoint(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    RespawnPoint_PGS TPGJ_Section = new RespawnPoint_PGS(KMP_Xml_Model.JugemPoints);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_Returnpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.JugemPoints);
        //    return TPGJ_Section;
        //}

        //public static GlideRoute_PGS ImportGlideRoute(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    KMPLibrary.XMLConvert.KMPData.KMP_XML KMP_Xml_Model = XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Path);
        //    GlideRoute_PGS HPLG_TPLG_Section = new GlideRoute_PGS(KMP_Xml_Model.GlideRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML.Render_GlideRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
        //    return HPLG_TPLG_Section;
        //}

        //#region XXXX Route Importer
        //public static EnemyRoute_PGS ImportEnemyRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
        //    EnemyRoute_PGS HPNE_TPNE_Section = new EnemyRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_EnemyRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
        //    return HPNE_TPNE_Section;
        //}

        //public static ItemRoute_PGS ImportItemRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
        //    ItemRoute_PGS HPTI_TPTI_Section = new ItemRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_ItemRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
        //    return HPTI_TPTI_Section;
        //}

        //public static GlideRoute_PGS ImportGlideRoutePositionAndScaleOnly(Render.KMPRendering.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        //{
        //    TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
        //    GlideRoute_PGS HPLG_TPLG_Section = new GlideRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
        //    Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_GlideRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
        //    return HPLG_TPLG_Section;
        //}
        //#endregion
        #endregion
    }
}
