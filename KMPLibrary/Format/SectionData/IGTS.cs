using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// IGTS (KMP Stage Info)
    /// </summary>
    public class IGTS
    {
        public char[] IGTSHeader { get; set; }

        public uint Unknown1 { get; set; }
        public byte LapCount { get; set; }
        public byte PolePosition { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }

        public RGBA RGBAColor { get; set; }
        public class RGBA
        {
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }
            public byte A { get; set; }

            public void ReadRGBA(BinaryReader br)
            {
                R = br.ReadByte();
                G = br.ReadByte();
                B = br.ReadByte();
                A = br.ReadByte();
            }

            public void WriteRGBA(BinaryWriter bw)
            {
                bw.Write(R);
                bw.Write(G);
                bw.Write(B);
                bw.Write(A);
            }

            public RGBA(byte ColorR = 0xFF, byte ColorG = 0xFF, byte ColorB = 0xFF, byte ColorA = 0xFF)
            {
                R = ColorR;
                G = ColorG;
                B = ColorB;
                A = ColorA;
            }

            public RGBA()
            {
                R = 255;
                G = 255;
                B = 255;
                A = 255;
            }
        }

        public uint FlareAlpha { get; set; }

        public void ReadIGTS(BinaryReader br)
        {
            IGTSHeader = br.ReadChars(4);
            if (new string(IGTSHeader) != "IGTS") throw new Exception("Error : IGTS");

            Unknown1 = br.ReadUInt32();
            LapCount = br.ReadByte();
            PolePosition = br.ReadByte();
            Unknown2 = br.ReadByte();
            Unknown3 = br.ReadByte();
            RGBAColor.ReadRGBA(br);
            FlareAlpha = br.ReadUInt32();
        }

        public void WriteIGTS(BinaryWriter bw)
        {
            bw.Write(IGTSHeader);
            bw.Write(Unknown1);
            bw.Write(LapCount);
            bw.Write(PolePosition);
            bw.Write(Unknown2);
            bw.Write(Unknown3);
            RGBAColor.WriteRGBA(bw);
            bw.Write(FlareAlpha);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Unknown1"></param>
        /// <param name="LapCount">Default : 3</param>
        /// <param name="PolePosition"></param>
        /// <param name="Unknown2"></param>
        /// <param name="Unknown3"></param>
        /// <param name="Color">Default : [R:255, G:255, B:255, A:0]</param>
        /// <param name="FlareAlpha">Default : 75</param>
        public IGTS(uint Unknown1, byte LapCount, byte PolePosition, byte Unknown2, byte Unknown3, RGBA Color, uint FlareAlpha)
        {
            IGTSHeader = "IGTS".ToCharArray();
            this.Unknown1 = Unknown1;
            this.LapCount = LapCount;
            this.PolePosition = PolePosition;
            this.Unknown2 = Unknown2;
            this.Unknown3 = Unknown3;
            RGBAColor = Color;
            this.FlareAlpha = FlareAlpha;
        }

        public IGTS()
        {
            IGTSHeader = new char[4];
            Unknown1 = 0;
            LapCount = 0x00;
            PolePosition = 0x00;
            Unknown2 = 0x00;
            Unknown3 = 0x00;
            RGBAColor = new RGBA();
            FlareAlpha = 0;
        }
    }
}
