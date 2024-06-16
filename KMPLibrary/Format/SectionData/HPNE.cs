using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// HPNE (EnemyRoute, Path)
    /// </summary>
    public class HPNE
    {
        public char[] HPNEHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<HPNEValue> HPNEValue_List { get; set; }
        public class HPNEValue
        {
            public ushort HPNE_StartPoint { get; set; }
            public ushort HPNE_Length { get; set; }

            public HPNE_PreviewGroups HPNE_PreviewGroup { get; set; }
            public class HPNE_PreviewGroups
            {
                public ushort Prev0 { get; set; }
                public ushort Prev1 { get; set; }
                public ushort Prev2 { get; set; }
                public ushort Prev3 { get; set; }
                public ushort Prev4 { get; set; }
                public ushort Prev5 { get; set; }
                public ushort Prev6 { get; set; }
                public ushort Prev7 { get; set; }
                public ushort Prev8 { get; set; }
                public ushort Prev9 { get; set; }
                public ushort Prev10 { get; set; }
                public ushort Prev11 { get; set; }
                public ushort Prev12 { get; set; }
                public ushort Prev13 { get; set; }
                public ushort Prev14 { get; set; }
                public ushort Prev15 { get; set; }

                public void ReadPrevValue(BinaryReader br)
                {
                    Prev0 = br.ReadUInt16();
                    Prev1 = br.ReadUInt16();
                    Prev2 = br.ReadUInt16();
                    Prev3 = br.ReadUInt16();
                    Prev4 = br.ReadUInt16();
                    Prev5 = br.ReadUInt16();
                    Prev6 = br.ReadUInt16();
                    Prev7 = br.ReadUInt16();
                    Prev8 = br.ReadUInt16();
                    Prev9 = br.ReadUInt16();
                    Prev10 = br.ReadUInt16();
                    Prev11 = br.ReadUInt16();
                    Prev12 = br.ReadUInt16();
                    Prev13 = br.ReadUInt16();
                    Prev14 = br.ReadUInt16();
                    Prev15 = br.ReadUInt16();
                }

                public void WritePrevValue(BinaryWriter bw)
                {
                    bw.Write(Prev0);
                    bw.Write(Prev1);
                    bw.Write(Prev2);
                    bw.Write(Prev3);
                    bw.Write(Prev4);
                    bw.Write(Prev5);
                    bw.Write(Prev6);
                    bw.Write(Prev7);
                    bw.Write(Prev8);
                    bw.Write(Prev9);
                    bw.Write(Prev10);
                    bw.Write(Prev11);
                    bw.Write(Prev12);
                    bw.Write(Prev13);
                    bw.Write(Prev14);
                    bw.Write(Prev15);
                }

                public HPNE_PreviewGroups(ushort[] PrevGroupArray)
                {
                    Prev0 = PrevGroupArray[0];
                    Prev1 = PrevGroupArray[1];
                    Prev2 = PrevGroupArray[2];
                    Prev3 = PrevGroupArray[3];
                    Prev4 = PrevGroupArray[4];
                    Prev5 = PrevGroupArray[5];
                    Prev6 = PrevGroupArray[6];
                    Prev7 = PrevGroupArray[7];
                    Prev8 = PrevGroupArray[8];
                    Prev9 = PrevGroupArray[9];
                    Prev10 = PrevGroupArray[10];
                    Prev11 = PrevGroupArray[11];
                    Prev12 = PrevGroupArray[12];
                    Prev13 = PrevGroupArray[13];
                    Prev14 = PrevGroupArray[14];
                    Prev15 = PrevGroupArray[15];
                }

                public HPNE_PreviewGroups()
                {
                    Prev0 = 255;
                    Prev1 = 255;
                    Prev2 = 255;
                    Prev3 = 255;
                    Prev4 = 255;
                    Prev5 = 255;
                    Prev6 = 255;
                    Prev7 = 255;
                    Prev8 = 255;
                    Prev9 = 255;
                    Prev10 = 255;
                    Prev11 = 255;
                    Prev12 = 255;
                    Prev13 = 255;
                    Prev14 = 255;
                    Prev15 = 255;
                }
            }

            public HPNE_NextGroups HPNE_NextGroup { get; set; }
            public class HPNE_NextGroups
            {
                public ushort Next0 { get; set; }
                public ushort Next1 { get; set; }
                public ushort Next2 { get; set; }
                public ushort Next3 { get; set; }
                public ushort Next4 { get; set; }
                public ushort Next5 { get; set; }
                public ushort Next6 { get; set; }
                public ushort Next7 { get; set; }
                public ushort Next8 { get; set; }
                public ushort Next9 { get; set; }
                public ushort Next10 { get; set; }
                public ushort Next11 { get; set; }
                public ushort Next12 { get; set; }
                public ushort Next13 { get; set; }
                public ushort Next14 { get; set; }
                public ushort Next15 { get; set; }

                public void ReadNextValue(BinaryReader br)
                {
                    Next0 = br.ReadUInt16();
                    Next1 = br.ReadUInt16();
                    Next2 = br.ReadUInt16();
                    Next3 = br.ReadUInt16();
                    Next4 = br.ReadUInt16();
                    Next5 = br.ReadUInt16();
                    Next6 = br.ReadUInt16();
                    Next7 = br.ReadUInt16();
                    Next8 = br.ReadUInt16();
                    Next9 = br.ReadUInt16();
                    Next10 = br.ReadUInt16();
                    Next11 = br.ReadUInt16();
                    Next12 = br.ReadUInt16();
                    Next13 = br.ReadUInt16();
                    Next14 = br.ReadUInt16();
                    Next15 = br.ReadUInt16();
                }

                public void WriteNextValue(BinaryWriter bw)
                {
                    bw.Write(Next0);
                    bw.Write(Next1);
                    bw.Write(Next2);
                    bw.Write(Next3);
                    bw.Write(Next4);
                    bw.Write(Next5);
                    bw.Write(Next6);
                    bw.Write(Next7);
                    bw.Write(Next8);
                    bw.Write(Next9);
                    bw.Write(Next10);
                    bw.Write(Next11);
                    bw.Write(Next12);
                    bw.Write(Next13);
                    bw.Write(Next14);
                    bw.Write(Next15);
                }

                public HPNE_NextGroups(ushort[] NextGroupArray)
                {
                    Next0 = NextGroupArray[0];
                    Next1 = NextGroupArray[1];
                    Next2 = NextGroupArray[2];
                    Next3 = NextGroupArray[3];
                    Next4 = NextGroupArray[4];
                    Next5 = NextGroupArray[5];
                    Next6 = NextGroupArray[6];
                    Next7 = NextGroupArray[7];
                    Next8 = NextGroupArray[8];
                    Next9 = NextGroupArray[9];
                    Next10 = NextGroupArray[10];
                    Next11 = NextGroupArray[11];
                    Next12 = NextGroupArray[12];
                    Next13 = NextGroupArray[13];
                    Next14 = NextGroupArray[14];
                    Next15 = NextGroupArray[15];
                }

                public HPNE_NextGroups()
                {
                    Next0 = 255;
                    Next1 = 255;
                    Next2 = 255;
                    Next3 = 255;
                    Next4 = 255;
                    Next5 = 255;
                    Next6 = 255;
                    Next7 = 255;
                    Next8 = 255;
                    Next9 = 255;
                    Next10 = 255;
                    Next11 = 255;
                    Next12 = 255;
                    Next13 = 255;
                    Next14 = 255;
                    Next15 = 255;
                }
            }

            public ushort UnknownShortData1 { get; set; }
            public ushort UnknownShortData2 { get; set; }

            public void ReadHPNEValue(BinaryReader br)
            {
                HPNE_StartPoint = br.ReadUInt16();
                HPNE_Length = br.ReadUInt16();
                HPNE_PreviewGroup.ReadPrevValue(br);
                HPNE_NextGroup.ReadNextValue(br);
                UnknownShortData1 = br.ReadUInt16();
                UnknownShortData2 = br.ReadUInt16();
            }

            public void WriteHPNEValue(BinaryWriter bw)
            {
                bw.Write(HPNE_StartPoint);
                bw.Write(HPNE_Length);
                HPNE_PreviewGroup.WritePrevValue(bw);
                HPNE_NextGroup.WriteNextValue(bw);
                bw.Write(UnknownShortData1);
                bw.Write(UnknownShortData2);
            }

            public HPNEValue()
            {
                HPNE_StartPoint = 0;
                HPNE_Length = 0;
                HPNE_PreviewGroup = new HPNE_PreviewGroups();
                HPNE_NextGroup = new HPNE_NextGroups();
                UnknownShortData1 = 0;
                UnknownShortData2 = 0;
            }
        }

        public void ReadHPNE(BinaryReader br)
        {
            HPNEHeader = br.ReadChars(4);
            if (new string(HPNEHeader) != "HPNE") throw new Exception("Error : HPNE");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();
            for (int Count = 0; Count < NumOfEntries; Count++)
            {
                HPNEValue HPNE_Value = new HPNEValue();
                HPNE_Value.ReadHPNEValue(br);
                HPNEValue_List.Add(HPNE_Value);
            }
        }

        public void WriteHPNE(BinaryWriter bw)
        {
            bw.Write(HPNEHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);
            for (int Count = 0; Count < NumOfEntries; Count++) HPNEValue_List[Count].WriteHPNEValue(bw);
        }

        public HPNE(List<HPNEValue> HPNEValueList)
        {
            HPNEHeader = "HPNE".ToCharArray();
            NumOfEntries = Convert.ToUInt16(HPNEValueList.Count);
            AdditionalValue = 0;
            HPNEValue_List = HPNEValueList;
        }

        public HPNE()
        {
            HPNEHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            HPNEValue_List = new List<HPNEValue>();
        }
    }
}
