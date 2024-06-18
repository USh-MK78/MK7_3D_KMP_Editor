using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData
{
    [System.Xml.Serialization.XmlRoot("KMPXml")]
    public class KMP_XML
    {
        [System.Xml.Serialization.XmlElement("StartPosition")]
        public SectionData.StartPosition startPositions { get; set; }

        [System.Xml.Serialization.XmlElement("EnemyRoute")]
        public SectionData.EnemyRoute EnemyRoutes { get; set; }

        [System.Xml.Serialization.XmlElement("ItemRoute")]
        public SectionData.ItemRoute ItemRoutes { get; set; }

        [System.Xml.Serialization.XmlElement("Checkpoint")]
        public SectionData.Checkpoint Checkpoints { get; set; }

        [System.Xml.Serialization.XmlElement("Object")]
        public SectionData.Object Objects { get; set; }

        [System.Xml.Serialization.XmlElement("Route")]
        public SectionData.Route Routes { get; set; }

        [System.Xml.Serialization.XmlElement("Area")]
        public SectionData.Area Areas { get; set; }

        [System.Xml.Serialization.XmlElement("Camera")]
        public SectionData.Camera Cameras { get; set; }

        [System.Xml.Serialization.XmlElement("JugemPoint")]
        public SectionData.JugemPoint JugemPoints { get; set; }

        [System.Xml.Serialization.XmlElement("StageInfo")]
        public SectionData.StageInfo Stage_Info { get; set; }

        [System.Xml.Serialization.XmlElement("GlideRoute")]
        public SectionData.GlideRoute GlideRoutes { get; set; }

        public static KMP_XML CreateNullDefault()
        {
            KMP_XML KMP_Xml = new KMP_XML
            {
                startPositions = null,
                EnemyRoutes = null,
                ItemRoutes = null,
                Checkpoints = null,
                Objects = null,
                Routes = null,
                Areas = null,
                Cameras = null,
                JugemPoints = null,
                Stage_Info = null,
                GlideRoutes = null
            };

            return KMP_Xml;
        }

        public KMP_XML(Format.KMP kMP)
        {
            startPositions = new SectionData.StartPosition(kMP.KMP_Section.TPTK);
            EnemyRoutes = new SectionData.EnemyRoute(kMP.KMP_Section.HPNE, kMP.KMP_Section.TPNE);
            ItemRoutes = new SectionData.ItemRoute(kMP.KMP_Section.HPTI, kMP.KMP_Section.TPTI);
            Checkpoints = new SectionData.Checkpoint(kMP.KMP_Section.HPKC, kMP.KMP_Section.TPKC);
            Objects = new SectionData.Object(kMP.KMP_Section.JBOG);
            Routes = new SectionData.Route(kMP.KMP_Section.ITOP);
            Areas = new SectionData.Area(kMP.KMP_Section.AERA);
            Cameras = new SectionData.Camera(kMP.KMP_Section.EMAC);
            JugemPoints = new SectionData.JugemPoint(kMP.KMP_Section.TPGJ);
            Stage_Info = new SectionData.StageInfo(kMP.KMP_Section.IGTS);
            GlideRoutes = new SectionData.GlideRoute(kMP.KMP_Section.HPLG, kMP.KMP_Section.TPLG);
        }

        public KMP_XML()
        {
            startPositions = new SectionData.StartPosition();
            EnemyRoutes = new SectionData.EnemyRoute();
            ItemRoutes = new SectionData.ItemRoute();
            Checkpoints = new SectionData.Checkpoint();
            Objects = new SectionData.Object();
            Routes = new SectionData.Route();
            Areas = new SectionData.Area();
            Cameras = new SectionData.Camera();
            JugemPoints = new SectionData.JugemPoint();
            Stage_Info = new SectionData.StageInfo();
            GlideRoutes = new SectionData.GlideRoute();
        }
    }

    public class KMPXmlSetting
    {
        public enum Section
        {
            KartPoint,
            EnemyRoutes,
            ItemRoutes,
            CheckPoint,
            Obj,
            Route,
            Area,
            Camera,
            JugemPoint,
            GlideRoutes
        }
    }
}
