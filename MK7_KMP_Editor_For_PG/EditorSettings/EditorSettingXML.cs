using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK7_3D_KMP_Editor.EditorSettings
{
    [System.Xml.Serialization.XmlRoot("EditorSetting")]
    public class EditorSettingXML
    {
        [System.Xml.Serialization.XmlElement("FilePath")]
        public FilePath FilePathSetting { get; set; } = new FilePath();
        public class FilePath
        {
            [System.Xml.Serialization.XmlElement("DefaultKMP")]
            public string DefaultKMPFileName { get; set; } = "Untitled";

            [System.Xml.Serialization.XmlElement("DefaultXML")]
            public string DefaultXMLFileName { get; set; } = "UntitledXML";

            [System.Xml.Serialization.XmlElement("DefaultDirectory")]
            public string DefaultDirectory { get; set; } = Environment.CurrentDirectory;

            public FilePath(string DefaultKMPFileName, string DefaultXMLFileName, string DefaultDirectory)
            {
                this.DefaultKMPFileName = DefaultKMPFileName;
                this.DefaultXMLFileName = DefaultXMLFileName;

                this.DefaultDirectory = DefaultDirectory;
            }

            public FilePath() { }
        }

        [System.Xml.Serialization.XmlElement("General")]
        public General GeneralSetting { get; set; } = new General();
        public class General
        {
            [System.Xml.Serialization.XmlElement("DefaultObjectID")]
            public string DefaultValueObjectID { get; set; } = "0005";

            /// <summary>
            /// General
            /// </summary>
            /// <param name="DefaultValueObjectID">DefaultObjectID</param>
            public General(string DefaultValueObjectID)
            {
                this.DefaultValueObjectID = DefaultValueObjectID;
            }

            public General() { }
        }

        public EditorSettingXML(FilePath FilePathSetting, General GeneralSetting)
        {
            this.FilePathSetting = FilePathSetting;
            this.GeneralSetting = GeneralSetting;
        }

        public EditorSettingXML() { }
    }
}
