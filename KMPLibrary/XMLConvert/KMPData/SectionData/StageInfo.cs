using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMPLibrary.Format.SectionData;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class StageInfo
    {
        [System.Xml.Serialization.XmlAttribute("Unknown1")]
        public uint Unknown1 { get; set; }

        [System.Xml.Serialization.XmlAttribute("LapCount")]
        public byte LapCount { get; set; }

        [System.Xml.Serialization.XmlAttribute("PolePosition")]
        public byte PolePosition { get; set; }

        [System.Xml.Serialization.XmlAttribute("Unknown2")]
        public byte Unknown2 { get; set; }

        [System.Xml.Serialization.XmlAttribute("Unknown3")]
        public byte Unknown3 { get; set; }

        [System.Xml.Serialization.XmlElement("RBAColor")]
        public RGBA RGBAColor { get; set; }
        public class RGBA
        {
            [System.Xml.Serialization.XmlAttribute("R")]
            public byte R { get; set; }

            [System.Xml.Serialization.XmlAttribute("G")]
            public byte G { get; set; }

            [System.Xml.Serialization.XmlAttribute("B")]
            public byte B { get; set; }

            [System.Xml.Serialization.XmlAttribute("A")]
            public byte A { get; set; }

            [System.Xml.Serialization.XmlAttribute("FlareAlpha")]
            public uint FlareAlpha { get; set; }

            public RGBA(IGTS.RGBA RGBA, uint FlareAlphaValue)
            {
                R = RGBA.R;
                G = RGBA.G;
                B = RGBA.B;
                A = RGBA.A;

                FlareAlpha = FlareAlphaValue;
            }

            public RGBA() { }
        }

        public StageInfo(IGTS IGTS)
        {
            Unknown1 = IGTS.Unknown1;
            LapCount = IGTS.LapCount;
            PolePosition = IGTS.PolePosition;
            Unknown2 = IGTS.Unknown2;
            Unknown3 = IGTS.Unknown3;

            RGBAColor = new RGBA(IGTS.RGBAColor, IGTS.FlareAlpha);
        }

        public StageInfo() { }
    }
}
