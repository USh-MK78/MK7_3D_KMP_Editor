using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// HPLG (GlideRoute, Path)
    /// </summary>
    public class HPLG
    {
        public char[] HPLGHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<HPLGValue> HPLGValue_List { get; set; }
        public class HPLGValue
        {
            public byte HPLG_StartPoint { get; set; }
            public byte HPLG_Length { get; set; }

            public HPLG_PreviewGroups HPLG_PreviewGroup { get; set; }
            public class HPLG_PreviewGroups
            {
                public byte Prev0 { get; set; }
                public byte Prev1 { get; set; }
                public byte Prev2 { get; set; }
                public byte Prev3 { get; set; }
                public byte Prev4 { get; set; }
                public byte Prev5 { get; set; }

                public void ReadHPLGPrevGroups(BinaryReader br)
                {
                    Prev0 = br.ReadByte();
                    Prev1 = br.ReadByte();
                    Prev2 = br.ReadByte();
                    Prev3 = br.ReadByte();
                    Prev4 = br.ReadByte();
                    Prev5 = br.ReadByte();
                }

                public void WriteHPLGPrevGroups(BinaryWriter bw)
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

                public HPLG_PreviewGroups(byte[] PrevGroupArray)
                {
                    Prev0 = PrevGroupArray[0];
                    Prev1 = PrevGroupArray[1];
                    Prev2 = PrevGroupArray[2];
                    Prev3 = PrevGroupArray[3];
                    Prev4 = PrevGroupArray[4];
                    Prev5 = PrevGroupArray[5];
                }

                public HPLG_PreviewGroups()
                {
                    Prev0 = 0xFF;
                    Prev1 = 0xFF;
                    Prev2 = 0xFF;
                    Prev3 = 0xFF;
                    Prev4 = 0xFF;
                    Prev5 = 0xFF;
                }
            }

            public HPLG_NextGroups HPLG_NextGroup { get; set; }
            public class HPLG_NextGroups
            {
                public byte Next0 { get; set; }
                public byte Next1 { get; set; }
                public byte Next2 { get; set; }
                public byte Next3 { get; set; }
                public byte Next4 { get; set; }
                public byte Next5 { get; set; }

                public void ReadHPLGNextGroups(BinaryReader br)
                {
                    Next0 = br.ReadByte();
                    Next1 = br.ReadByte();
                    Next2 = br.ReadByte();
                    Next3 = br.ReadByte();
                    Next4 = br.ReadByte();
                    Next5 = br.ReadByte();
                }

                public void WriteHPLGNextGroups(BinaryWriter bw)
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

                public HPLG_NextGroups(byte[] NextGroupArray)
                {
                    Next0 = NextGroupArray[0];
                    Next1 = NextGroupArray[1];
                    Next2 = NextGroupArray[2];
                    Next3 = NextGroupArray[3];
                    Next4 = NextGroupArray[4];
                    Next5 = NextGroupArray[5];
                }

                public HPLG_NextGroups()
                {
                    Next0 = 0xFF;
                    Next1 = 0xFF;
                    Next2 = 0xFF;
                    Next3 = 0xFF;
                    Next4 = 0xFF;
                    Next5 = 0xFF;
                }
            }

            #region RouteSetting(I'm using the code in "KMPExpander-master\KMPExpander\Class\SimpleKMPs\GliderRoutes.cs" of "KMP Expander")
            public uint RouteSetting { get; set; }
            public bool ForceToRoute
            {
                get
                {
                    return (RouteSetting & 0xFF) != 0;
                }
                set
                {
                    RouteSetting = (RouteSetting & ~0xFFu) | (value ? 1u : 0u);
                }
            }

            public bool CannonSection
            {
                get
                {
                    return (RouteSetting & 0xFF00) != 0;
                }
                set
                {
                    RouteSetting = (RouteSetting & ~0xFF00u) | (value ? 1u : 0u) << 8;
                }
            }

            public bool PreventRaising
            {
                get
                {
                    return (RouteSetting & 0xFF0000) != 0;
                }
                set
                {
                    RouteSetting = (RouteSetting & ~0xFF0000u) | (value ? 1u : 0u) << 16;
                }
            }
            #endregion

            public uint HPLG_UnknownData2 { get; set; }

            public void ReadHPLGValue(BinaryReader br)
            {
                HPLG_StartPoint = br.ReadByte();
                HPLG_Length = br.ReadByte();
                HPLG_PreviewGroup.ReadHPLGPrevGroups(br);
                HPLG_NextGroup.ReadHPLGNextGroups(br);
                RouteSetting = br.ReadUInt32();
                HPLG_UnknownData2 = br.ReadUInt32();
            }

            public void WriteHPLGValue(BinaryWriter bw)
            {
                bw.Write(HPLG_StartPoint);
                bw.Write(HPLG_Length);
                HPLG_PreviewGroup.WriteHPLGPrevGroups(bw);
                HPLG_NextGroup.WriteHPLGNextGroups(bw);
                bw.Write(RouteSetting);
                bw.Write(HPLG_UnknownData2);
            }

            public HPLGValue()
            {
                HPLG_StartPoint = 0x00;
                HPLG_Length = 0x00;
                HPLG_PreviewGroup = new HPLG_PreviewGroups();
                HPLG_NextGroup = new HPLG_NextGroups();
                RouteSetting = 0;
                HPLG_UnknownData2 = 0;
            }
        }

        public void ReadHPLG(BinaryReader br)
        {
            HPLGHeader = br.ReadChars(4);
            if (new string(HPLGHeader) != "HPLG") throw new Exception("Error : HPLG");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int HPLGCount = 0; HPLGCount < NumOfEntries; HPLGCount++)
            {
                HPLGValue hPLGValue = new HPLGValue();
                hPLGValue.ReadHPLGValue(br);
                HPLGValue_List.Add(hPLGValue);
            }
        }

        public void WriteHPLG(BinaryWriter bw)
        {
            bw.Write(HPLGHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int Count = 0; Count < NumOfEntries; Count++)
            {
                HPLGValue_List[Count].WriteHPLGValue(bw);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HPLGValueList"></param>
        /// <param name="AdditionalValue">Default : 0</param>
        public HPLG(List<HPLGValue> HPLGValueList, ushort AdditionalValue = 0)
        {
            HPLGHeader = "HPLG".ToCharArray();
            NumOfEntries = Convert.ToUInt16(HPLGValueList.Count);
            this.AdditionalValue = AdditionalValue;
            HPLGValue_List = HPLGValueList;
        }

        public HPLG()
        {
            HPLGHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            HPLGValue_List = new List<HPLGValue>();
        }

        //public HPLG()
        //{
        //    HPLGHeader = "HPLG".ToCharArray();
        //    NumOfEntries = 0;
        //    AdditionalValue = 0;
        //    HPLGValue_List = new List<HPLGValue>();
        //}
    }

}
