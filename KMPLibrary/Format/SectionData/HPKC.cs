using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// HPKC (Checkpoint, Point)
    /// </summary>
    public class HPKC
    {
        public char[] HPKCHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<HPKCValue> HPKCValue_List { get; set; }
        public class HPKCValue
        {
            public byte HPKC_StartPoint { get; set; }
            public byte HPKC_Length { get; set; }

            public HPKC_PreviewGroups HPKC_PreviewGroup { get; set; }
            public class HPKC_PreviewGroups
            {
                public byte Prev0 { get; set; }
                public byte Prev1 { get; set; }
                public byte Prev2 { get; set; }
                public byte Prev3 { get; set; }
                public byte Prev4 { get; set; }
                public byte Prev5 { get; set; }

                public void ReadHPKCPrevGroups(BinaryReader br)
                {
                    Prev0 = br.ReadByte();
                    Prev1 = br.ReadByte();
                    Prev2 = br.ReadByte();
                    Prev3 = br.ReadByte();
                    Prev4 = br.ReadByte();
                    Prev5 = br.ReadByte();
                }

                public void WriteHPKCPrevGroups(BinaryWriter bw)
                {
                    bw.Write(Prev0);
                    bw.Write(Prev1);
                    bw.Write(Prev2);
                    bw.Write(Prev3);
                    bw.Write(Prev4);
                    bw.Write(Prev5);
                }

                public byte[] GetPrevGroupArray()
                {
                    return new byte[] { Prev0, Prev1, Prev2, Prev3, Prev4, Prev5 };
                }

                public HPKC_PreviewGroups(byte[] PrevGroupArray)
                {
                    Prev0 = PrevGroupArray[0];
                    Prev1 = PrevGroupArray[1];
                    Prev2 = PrevGroupArray[2];
                    Prev3 = PrevGroupArray[3];
                    Prev4 = PrevGroupArray[4];
                    Prev5 = PrevGroupArray[5];
                }

                public HPKC_PreviewGroups()
                {
                    Prev0 = 0x00;
                    Prev1 = 0x00;
                    Prev2 = 0x00;
                    Prev3 = 0x00;
                    Prev4 = 0x00;
                    Prev5 = 0x00;
                }
            }

            public HPKC_NextGroups HPKC_NextGroup { get; set; }
            public class HPKC_NextGroups
            {
                public byte Next0 { get; set; }
                public byte Next1 { get; set; }
                public byte Next2 { get; set; }
                public byte Next3 { get; set; }
                public byte Next4 { get; set; }
                public byte Next5 { get; set; }

                public void ReadHPKCNextGroup(BinaryReader br)
                {
                    Next0 = br.ReadByte();
                    Next1 = br.ReadByte();
                    Next2 = br.ReadByte();
                    Next3 = br.ReadByte();
                    Next4 = br.ReadByte();
                    Next5 = br.ReadByte();
                }

                public void WriteHPKCNextGroup(BinaryWriter bw)
                {
                    bw.Write(Next0);
                    bw.Write(Next1);
                    bw.Write(Next2);
                    bw.Write(Next3);
                    bw.Write(Next4);
                    bw.Write(Next5);
                }

                public byte[] GetNextGroupArray()
                {
                    return new byte[] { Next0, Next1, Next2, Next3, Next4, Next5 };
                }

                public HPKC_NextGroups(byte[] NextGroupArray)
                {
                    Next0 = NextGroupArray[0];
                    Next1 = NextGroupArray[1];
                    Next2 = NextGroupArray[2];
                    Next3 = NextGroupArray[3];
                    Next4 = NextGroupArray[4];
                    Next5 = NextGroupArray[5];
                }

                public HPKC_NextGroups()
                {
                    Next0 = 0x00;
                    Next1 = 0x00;
                    Next2 = 0x00;
                    Next3 = 0x00;
                    Next4 = 0x00;
                    Next5 = 0x00;
                }
            }

            public ushort HPKC_UnknownShortData1 { get; set; }

            public void ReadHPKCValue(BinaryReader br)
            {
                HPKC_StartPoint = br.ReadByte();
                HPKC_Length = br.ReadByte();
                HPKC_PreviewGroup.ReadHPKCPrevGroups(br);
                HPKC_NextGroup.ReadHPKCNextGroup(br);
                HPKC_UnknownShortData1 = br.ReadUInt16();
            }

            public void WriteHPKCValue(BinaryWriter bw)
            {
                bw.Write(HPKC_StartPoint);
                bw.Write(HPKC_Length);
                HPKC_PreviewGroup.WriteHPKCPrevGroups(bw);
                HPKC_NextGroup.WriteHPKCNextGroup(bw);
                bw.Write(HPKC_UnknownShortData1);
            }

            public HPKCValue()
            {
                HPKC_StartPoint = 0x00;
                HPKC_Length = 0x00;
                HPKC_PreviewGroup = new HPKC_PreviewGroups();
                HPKC_NextGroup = new HPKC_NextGroups();
                HPKC_UnknownShortData1 = 0;
            }
        }

        public void ReadHPKC(BinaryReader br)
        {
            HPKCHeader = br.ReadChars(4);
            if (new string(HPKCHeader) != "HPKC") throw new Exception("Error : HPKC");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int HPKCCount = 0; HPKCCount < NumOfEntries; HPKCCount++)
            {
                HPKCValue HPKC_Value = new HPKCValue();
                HPKC_Value.ReadHPKCValue(br);
                HPKCValue_List.Add(HPKC_Value);
            }
        }

        public void WriteHPKC(BinaryWriter bw)
        {
            bw.Write(HPKCHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int Count = 0; Count < NumOfEntries; Count++) HPKCValue_List[Count].WriteHPKCValue(bw);
        }

        public HPKC(List<HPKCValue> HPKCValueList, ushort AdditionalValue = 0)
        {
            HPKCHeader = "HPKC".ToCharArray();
            NumOfEntries = Convert.ToUInt16(HPKCValueList.Count);
            this.AdditionalValue = AdditionalValue;
            HPKCValue_List = HPKCValueList;
        }

        public HPKC()
        {
            HPKCHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            HPKCValue_List = new List<HPKCValue>();
        }

        //public HPKC()
        //{
        //    HPKCHeader = "HPKC".ToCharArray();
        //    NumOfEntries = 0;
        //    AdditionalValue = 0;
        //    HPKCValue_List = new List<HPKCValue>();
        //}
    }
}
