using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// TPKC (Checkpoint, Point)
    /// </summary>
    public class TPKC
    {
        public char[] TPKCHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<TPKCValue> TPKCValue_List { get; set; }
        public class TPKCValue
        {
            public Vector2 TPKC_2DPosition_Left { get; set; }
            public Vector2 TPKC_2DPosition_Right { get; set; }
            public byte TPKC_RespawnID { get; set; }
            public byte TPKC_Checkpoint_Type { get; set; }
            public byte TPKC_PreviousCheckPoint { get; set; }
            public byte TPKC_NextCheckPoint { get; set; }
            public byte TPKC_ClipID { get; set; }
            public byte TPKC_Section { get; set; }
            public byte TPKC_UnknownData3 { get; set; }
            public byte TPKC_UnknownData4 { get; set; }

            public void ReadTPKCValue(BinaryReader br)
            {
                TPKC_2DPosition_Left = KMPHelper.Converter2D.ByteArrayToVector2D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4) });
                TPKC_2DPosition_Right = KMPHelper.Converter2D.ByteArrayToVector2D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4) });
                TPKC_RespawnID = br.ReadByte();
                TPKC_Checkpoint_Type = br.ReadByte();
                TPKC_PreviousCheckPoint = br.ReadByte();
                TPKC_NextCheckPoint = br.ReadByte();
                TPKC_ClipID = br.ReadByte();
                TPKC_Section = br.ReadByte();
                TPKC_UnknownData3 = br.ReadByte();
                TPKC_UnknownData4 = br.ReadByte();
            }

            public void WriteTPKCValue(BinaryWriter bw)
            {
                bw.Write(KMPHelper.Converter2D.Vector2ToByteArray(TPKC_2DPosition_Left)[0]);
                bw.Write(KMPHelper.Converter2D.Vector2ToByteArray(TPKC_2DPosition_Left)[1]);
                bw.Write(KMPHelper.Converter2D.Vector2ToByteArray(TPKC_2DPosition_Right)[0]);
                bw.Write(KMPHelper.Converter2D.Vector2ToByteArray(TPKC_2DPosition_Right)[1]);
                bw.Write(TPKC_RespawnID);
                bw.Write(TPKC_Checkpoint_Type);
                bw.Write(TPKC_PreviousCheckPoint);
                bw.Write(TPKC_NextCheckPoint);
                bw.Write(TPKC_ClipID);
                bw.Write(TPKC_Section);
                bw.Write(TPKC_UnknownData3);
                bw.Write(TPKC_UnknownData4);
            }

            public TPKCValue()
            {
                TPKC_2DPosition_Left = new Vector2(0, 0);
                TPKC_2DPosition_Right = new Vector2(0, 0);
                TPKC_RespawnID = 0x00;
                TPKC_Checkpoint_Type = 0x00;
                TPKC_PreviousCheckPoint = 0x00;
                TPKC_NextCheckPoint = 0x00;
                TPKC_ClipID = 0x00;
                TPKC_Section = 0x00;
                TPKC_UnknownData3 = 0x00;
                TPKC_UnknownData4 = 0x00;
            }
        }

        public void ReadTPKC(BinaryReader br)
        {
            TPKCHeader = br.ReadChars(4);
            if (new string(TPKCHeader) != "TPKC") throw new Exception("Error : TPKC");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();
            for (int Count = 0; Count < NumOfEntries; Count++)
            {
                TPKCValue TPKC_Value = new TPKCValue();
                TPKC_Value.ReadTPKCValue(br);
                TPKCValue_List.Add(TPKC_Value);
            }
        }

        public void WriteTPKC(BinaryWriter bw)
        {
            bw.Write(TPKCHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);
            for (int Count = 0; Count < TPKCValue_List.Count; Count++) TPKCValue_List[Count].WriteTPKCValue(bw);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TPKCValueList">List<TPKCValue></param>
        /// <param name="AdditionalValue">Default : 0</param>
        public TPKC(List<TPKCValue> TPKCValueList, ushort AdditionalValue = 0)
        {
            TPKCHeader = "TPKC".ToCharArray();
            NumOfEntries = Convert.ToUInt16(TPKCValueList.Count);
            this.AdditionalValue = AdditionalValue;
            TPKCValue_List = TPKCValueList;
        }

        public TPKC()
        {
            TPKCHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            TPKCValue_List = new List<TPKCValue>();
        }
    }
}
