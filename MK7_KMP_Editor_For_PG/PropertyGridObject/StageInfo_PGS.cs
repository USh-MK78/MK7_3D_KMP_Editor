using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMPLibrary.Format.SectionData;
using static MK7_3D_KMP_Editor.CustomPropertyGridClassConverter;

namespace MK7_3D_KMP_Editor.PropertyGridObject
{
    /// <summary>
    /// StageInfo (PropertyGrid)
    /// </summary>
    [TypeConverter(typeof(CustomSortTypeConverter))]
    public class StageInfo_PGS
    {
        public uint Unknown1 { get; set; }
        public byte LapCount { get; set; }
        public byte PolePosition { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }

        [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
        public RGBA RGBAColor { get; set; }
        public class RGBA
        {
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }
            public byte A { get; set; }

            public RGBA()
            {
                R = 0;
                G = 0;
                B = 0;
                A = 0;
            }

            public RGBA(byte In_R, byte In_G, byte In_B, byte In_A)
            {
                R = In_R;
                G = In_G;
                B = In_B;
                A = In_A;
            }

            public override string ToString()
            {
                return "RGBA Color";
            }
        }

        public uint FlareAlpha { get; set; }

        public StageInfo_PGS(IGTS IGTS_Section)
        {
            Unknown1 = IGTS_Section.Unknown1;
            LapCount = IGTS_Section.LapCount;
            PolePosition = IGTS_Section.PolePosition;
            Unknown2 = IGTS_Section.Unknown2;
            Unknown3 = IGTS_Section.Unknown3;
            RGBAColor = new RGBA(IGTS_Section.RGBAColor.R, IGTS_Section.RGBAColor.G, IGTS_Section.RGBAColor.B, IGTS_Section.RGBAColor.A);
            FlareAlpha = IGTS_Section.FlareAlpha;
        }

        public StageInfo_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.StageInfo StageInfo)
        {
            Unknown1 = StageInfo.Unknown1;
            LapCount = StageInfo.LapCount;
            PolePosition = StageInfo.PolePosition;
            Unknown2 = StageInfo.Unknown2;
            Unknown3 = StageInfo.Unknown3;
            RGBAColor = new RGBA(StageInfo.RGBAColor.R, StageInfo.RGBAColor.G, StageInfo.RGBAColor.B, StageInfo.RGBAColor.A);
            FlareAlpha = StageInfo.RGBAColor.FlareAlpha;
        }

        public StageInfo_PGS()
        {
            Unknown1 = 0;
            LapCount = 3;
            PolePosition = 0;
            Unknown2 = 0;
            Unknown3 = 0;
            RGBAColor = new RGBA(255, 255, 255, 0);
            FlareAlpha = 75;
        }

        public IGTS ToIGTS()
        {
            IGTS.RGBA RGBA = new IGTS.RGBA(RGBAColor.R, RGBAColor.G, RGBAColor.B, RGBAColor.A);
            return new IGTS(Unknown1, LapCount, PolePosition, Unknown2, Unknown3, RGBA, FlareAlpha);
        }
    }
}
