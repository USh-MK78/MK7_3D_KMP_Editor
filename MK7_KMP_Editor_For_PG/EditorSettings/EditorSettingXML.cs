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

        public EditorSettingXML(FilePath FilePathSetting)
        {
            this.FilePathSetting = FilePathSetting;
        }

        public EditorSettingXML() { }
    }
}
