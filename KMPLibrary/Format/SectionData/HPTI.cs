using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// HPTI (ItemRoute, Path)
    /// </summary>
    public class HPTI
    {
        public char[] HPTIHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<HPTIValue> HPTIValue_List { get; set; }
        public class HPTIValue
        {
            public ushort HPTI_StartPoint { get; set; }
            public ushort HPTI_Length { get; set; }

            public HPTI_PreviewGroups HPTI_PreviewGroup { get; set; }
            public class HPTI_PreviewGroups
            {
                public ushort Prev0 { get; set; }
                public ushort Prev1 { get; set; }
                public ushort Prev2 { get; set; }
                public ushort Prev3 { get; set; }
                public ushort Prev4 { get; set; }
                public ushort Prev5 { get; set; }

                public void ReadHPTIPrevGroups(BinaryReader br)
                {
                    Prev0 = br.ReadUInt16();
                    Prev1 = br.ReadUInt16();
                    Prev2 = br.ReadUInt16();
                    Prev3 = br.ReadUInt16();
                    Prev4 = br.ReadUInt16();
                    Prev5 = br.ReadUInt16();
                }

                public void WriteHPTIPrevGroups(BinaryWriter bw)
                {
                    bw.Write(Prev0);
                    bw.Write(Prev1);
                    bw.Write(Prev2);
                    bw.Write(Prev3);
                    bw.Write(Prev4);
                    bw.Write(Prev5);
                }

                public ushort[] GetPrevGroupArray()
                {
                    return new ushort[] { Prev0, Prev1, Prev2, Prev3, Prev4, Prev5 };
                }

                public HPTI_PreviewGroups(ushort[] PrevGroupArray)
                {
                    Prev0 = PrevGroupArray[0];
                    Prev1 = PrevGroupArray[1];
                    Prev2 = PrevGroupArray[2];
                    Prev3 = PrevGroupArray[3];
                    Prev4 = PrevGroupArray[4];
                    Prev5 = PrevGroupArray[5];
                }

                public HPTI_PreviewGroups()
                {
                    Prev0 = 0;
                    Prev1 = 0;
                    Prev2 = 0;
                    Prev3 = 0;
                    Prev4 = 0;
                    Prev5 = 0;
                }
            }

            public HPTI_NextGroups HPTI_NextGroup { get; set; }
            public class HPTI_NextGroups
            {
                public ushort Next0 { get; set; }
                public ushort Next1 { get; set; }
                public ushort Next2 { get; set; }
                public ushort Next3 { get; set; }
                public ushort Next4 { get; set; }
                public ushort Next5 { get; set; }

                public void ReadHPTINextGroups(BinaryReader br)
                {
                    Next0 = br.ReadUInt16();
                    Next1 = br.ReadUInt16();
                    Next2 = br.ReadUInt16();
                    Next3 = br.ReadUInt16();
                    Next4 = br.ReadUInt16();
                    Next5 = br.ReadUInt16();
                }

                public void WriteHPTINextGroups(BinaryWriter bw)
                {
                    bw.Write(Next0);
                    bw.Write(Next1);
                    bw.Write(Next2);
                    bw.Write(Next3);
                    bw.Write(Next4);
                    bw.Write(Next5);
                }

                public ushort[] GetNextGroupArray()
                {
                    return new ushort[] { Next0, Next1, Next2, Next3, Next4, Next5 };
                }

                public HPTI_NextGroups(ushort[] NextGroupArray)
                {
                    Next0 = NextGroupArray[0];
                    Next1 = NextGroupArray[1];
                    Next2 = NextGroupArray[2];
                    Next3 = NextGroupArray[3];
                    Next4 = NextGroupArray[4];
                    Next5 = NextGroupArray[5];
                }

                public HPTI_NextGroups()
                {
                    Next0 = 0;
                    Next1 = 0;
                    Next2 = 0;
                    Next3 = 0;
                    Next4 = 0;
                    Next5 = 0;
                }
            }

            public void ReadHPTIValue(BinaryReader br)
            {
                HPTI_StartPoint = br.ReadUInt16();
                HPTI_Length = br.ReadUInt16();
                HPTI_PreviewGroup.ReadHPTIPrevGroups(br);
                HPTI_NextGroup.ReadHPTINextGroups(br);
            }

            public void WriteTPTIValue(BinaryWriter bw)
            {
                bw.Write(HPTI_StartPoint);
                bw.Write(HPTI_Length);
                HPTI_PreviewGroup.WriteHPTIPrevGroups(bw);
                HPTI_NextGroup.WriteHPTINextGroups(bw);
            }

            public HPTIValue()
            {
                HPTI_StartPoint = 0;
                HPTI_Length = 0;
                HPTI_PreviewGroup = new HPTI_PreviewGroups();
                HPTI_NextGroup = new HPTI_NextGroups();
            }
        }

        public void ReadHPTI(BinaryReader br)
        {
            HPTIHeader = br.ReadChars(4);
            if (new string(HPTIHeader) != "HPTI") throw new Exception("Error : HPTI");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();
            for (int Count = 0; Count < NumOfEntries; Count++)
            {
                HPTIValue HPTI_Value = new HPTIValue();
                HPTI_Value.ReadHPTIValue(br);
                HPTIValue_List.Add(HPTI_Value);
            }
        }

        public void WriteHPTI(BinaryWriter bw)
        {
            bw.Write(HPTIHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);
            for (int Count = 0; Count < NumOfEntries; Count++) HPTIValue_List[Count].WriteTPTIValue(bw);
        }

        public HPTI(List<HPTIValue> HPTIValueList, ushort AdditionalValue = 0)
        {
            HPTIHeader = "HPTI".ToCharArray();
            NumOfEntries = Convert.ToUInt16(HPTIValueList.Count);
            this.AdditionalValue = AdditionalValue;
            HPTIValue_List = HPTIValueList;
        }

        public HPTI()
        {
            HPTIHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            HPTIValue_List = new List<HPTIValue>();
        }
    }
}
