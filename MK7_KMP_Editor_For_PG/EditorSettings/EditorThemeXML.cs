using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK7_3D_KMP_Editor.EditorSettings
{
    [System.Xml.Serialization.XmlRoot("Theme")]
    public class EditorThemeXML
    {
        public Color BaseColor { get; set; }
        public Color TextColor { get; set; }

        public Color ViewportBackGroundColor { get; set; } = SystemColors.Control;
    }
}
