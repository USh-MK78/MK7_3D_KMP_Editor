using KMPLibrary.Format;
using KMPLibrary.KMPHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK7_3D_KMP_Editor.PropertyGridObject
{
    /// <summary>
    /// PropertyGrid (All)
    /// </summary>
    public class KMP_Main_PGS
    {
        public KartPoint_PGS TPTK_Section { get; set; }
        public EnemyRoute_PGS HPNE_TPNE_Section { get; set; }
        public ItemRoute_PGS HPTI_TPTI_Section { get; set; }
        public Checkpoint_PGS HPKC_TPKC_Section { get; set; }
        public KMPObject_PGS JBOG_Section { get; set; }
        public Route_PGS ITOP_Section { get; set; }
        public Area_PGS AERA_Section { get; set; }
        public Camera_PGS EMAC_Section { get; set; }
        public RespawnPoint_PGS TPGJ_Section { get; set; }

        //TPNC = null
        //TPSM = null

        public StageInfo_PGS IGTS_Section { get; set; }

        //SROC = null

        public GlideRoute_PGS HPLG_TPLG_Section { get; set; }

        public KMP_Main_PGS(KMP KMP)
        {
            TPTK_Section = new KartPoint_PGS(KMP.KMP_Section.TPTK);
            HPNE_TPNE_Section = new EnemyRoute_PGS(KMP.KMP_Section.HPNE, KMP.KMP_Section.TPNE);
            HPTI_TPTI_Section = new ItemRoute_PGS(KMP.KMP_Section.HPTI, KMP.KMP_Section.TPTI);
            HPKC_TPKC_Section = new Checkpoint_PGS(KMP.KMP_Section.HPKC, KMP.KMP_Section.TPKC);
            JBOG_Section = new KMPObject_PGS(KMP.KMP_Section.JBOG, ObjFlowConverter.Xml.ReadObjFlowXml("ObjFlowData.xml"));
            ITOP_Section = new Route_PGS(KMP.KMP_Section.ITOP);
            AERA_Section = new Area_PGS(KMP.KMP_Section.AERA);
            EMAC_Section = new Camera_PGS(KMP.KMP_Section.EMAC);
            TPGJ_Section = new RespawnPoint_PGS(KMP.KMP_Section.TPGJ);

            //TPNC : Unused Section
            //TPSM : Unused Section

            IGTS_Section = new StageInfo_PGS(KMP.KMP_Section.IGTS);

            //SROC : Unused Section

            HPLG_TPLG_Section = new GlideRoute_PGS(KMP.KMP_Section.HPLG, KMP.KMP_Section.TPLG);
        }

        public KMP_Main_PGS(KMPLibrary.XMLConvert.KMPData.KMP_XML KMP)
        {
            TPTK_Section = new KartPoint_PGS(KMP.startPositions);
            HPNE_TPNE_Section = new EnemyRoute_PGS(KMP.EnemyRoutes);
            HPTI_TPTI_Section = new ItemRoute_PGS(KMP.ItemRoutes);
            HPKC_TPKC_Section = new Checkpoint_PGS(KMP.Checkpoints);
            //JBOG_Section = new KMPObject_PGS(KMP.Objects, Render.KMPRendering.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
            JBOG_Section = new KMPObject_PGS(KMP.Objects);
            ITOP_Section = new Route_PGS(KMP.Routes);
            AERA_Section = new Area_PGS(KMP.Areas);
            EMAC_Section = new Camera_PGS(KMP.Cameras);
            TPGJ_Section = new RespawnPoint_PGS(KMP.JugemPoints);

            //TPNC : Unused Section
            //TPSM : Unused Section

            IGTS_Section = new StageInfo_PGS(KMP.Stage_Info);

            //SROC : Unused Section

            HPLG_TPLG_Section = new GlideRoute_PGS(KMP.GlideRoutes);
        }

        public KMP_Main_PGS()
        {
            TPTK_Section = new KartPoint_PGS();
            HPNE_TPNE_Section = new EnemyRoute_PGS();
            HPTI_TPTI_Section = new ItemRoute_PGS();
            HPKC_TPKC_Section = new Checkpoint_PGS();
            JBOG_Section = new KMPObject_PGS();
            ITOP_Section = new Route_PGS();
            AERA_Section = new Area_PGS();
            EMAC_Section = new Camera_PGS();
            TPGJ_Section = new RespawnPoint_PGS();
            IGTS_Section = new StageInfo_PGS();
            HPLG_TPLG_Section = new GlideRoute_PGS();
        }

        public KMP ToKMP(uint Version)
        {
            KMP.KMPSection KMPSection = new KMP.KMPSection
            {
                TPTK = TPTK_Section.ToTPTK(),
                TPNE = HPNE_TPNE_Section.ToHPNE_TPNEData().TPNE_Section,
                HPNE = HPNE_TPNE_Section.ToHPNE_TPNEData().HPNE_Section,
                TPTI = HPTI_TPTI_Section.ToHPTI_TPTIData().TPTI_Section,
                HPTI = HPTI_TPTI_Section.ToHPTI_TPTIData().HPTI_Section,
                TPKC = HPKC_TPKC_Section.ToHPKC_TPKCData().TPKC_Section,
                HPKC = HPKC_TPKC_Section.ToHPKC_TPKCData().HPKC_Section,
                JBOG = JBOG_Section.ToJBOG(Version),
                ITOP = ITOP_Section.ToITOP(),
                AERA = AERA_Section.ToAERA(),
                EMAC = EMAC_Section.ToEMAC(),
                TPGJ = TPGJ_Section.ToTPGJ(),
                TPNC = KMPLibrary.Format.SectionData.TPNC.ToTPNC_Section(),
                TPSM = KMPLibrary.Format.SectionData.TPSM.ToTPSM_Section(),
                IGTS = IGTS_Section.ToIGTS(),
                SROC = KMPLibrary.Format.SectionData.SROC.ToSROC_Section(),
                TPLG = HPLG_TPLG_Section.ToHPLG_TPLGData().TPLG_Section,
                HPLG = HPLG_TPLG_Section.ToHPLG_TPLGData().HPLG_Section,
            };

            return new KMP(KMPSection, Version);



            //KMP kMPFormat = new KMP(Version);
            //kMPFormat.KMP_Section.TPTK = TPTK_Section.ToTPTK();
            //kMPFormat.KMP_Section.TPNE = HPNE_TPNE_Section.ToHPNE_TPNEData().TPNE_Section;
            //kMPFormat.KMP_Section.HPNE = HPNE_TPNE_Section.ToHPNE_TPNEData().HPNE_Section;
            //kMPFormat.KMP_Section.TPTI = HPTI_TPTI_Section.ToHPTI_TPTIData().TPTI_Section;
            //kMPFormat.KMP_Section.HPTI = HPTI_TPTI_Section.ToHPTI_TPTIData().HPTI_Section;
            //kMPFormat.KMP_Section.TPKC = HPKC_TPKC_Section.ToHPKC_TPKCData().TPKC_Section;
            //kMPFormat.KMP_Section.HPKC = HPKC_TPKC_Section.ToHPKC_TPKCData().HPKC_Section;
            //kMPFormat.KMP_Section.JBOG = JBOG_Section.ToJBOG(kMPFormat.VersionNumber);
            //kMPFormat.KMP_Section.ITOP = ITOP_Section.ToITOP();
            //kMPFormat.KMP_Section.AERA = AERA_Section.ToAERA();
            //kMPFormat.KMP_Section.EMAC = EMAC_Section.ToEMAC();
            //kMPFormat.KMP_Section.TPGJ = TPGJ_Section.ToTPGJ();
            //kMPFormat.KMP_Section.TPNC = KMPLibrary.Format.SectionData.TPNC.ToTPNC_Section();
            //kMPFormat.KMP_Section.TPSM = KMPLibrary.Format.SectionData.TPSM.ToTPSM_Section();
            //kMPFormat.KMP_Section.IGTS = IGTS_Section.ToIGTS();
            //kMPFormat.KMP_Section.SROC = KMPLibrary.Format.SectionData.SROC.ToSROC_Section();
            //kMPFormat.KMP_Section.TPLG = HPLG_TPLG_Section.ToHPLG_TPLGData().TPLG_Section;
            //kMPFormat.KMP_Section.HPLG = HPLG_TPLG_Section.ToHPLG_TPLGData().HPLG_Section;
            //return kMPFormat;
        }
    }
}
