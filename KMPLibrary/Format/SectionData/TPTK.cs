using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// TPTK (Kart Point)
    /// </summary>
    public class TPTK
    {
        public char[] TPTKHeader { get; set; } //0x4
        public ushort NumOfEntries { get; set; } //0x2
        public ushort AdditionalValue { get; set; } //0x2
        public List<TPTKValue> TPTKValue_List { get; set; }
        public class TPTKValue
        {
            public Vector3D TPTK_Position { get; set; }
            public Vector3D TPTK_Rotation { get; set; }
            public ushort Player_Index { get; set; } //0x2
            public ushort TPTK_UnknownData { get; set; } //0x2

            public void ReadTPTKValue(BinaryReader br)
            {
                TPTK_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                TPTK_Rotation = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                Player_Index = br.ReadUInt16();
                TPTK_UnknownData = br.ReadUInt16();
            }

            public void WriteTPTKValue(BinaryWriter bw)
            {
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTK_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTK_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTK_Position)[2]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTK_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTK_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTK_Position)[2]);
                bw.Write(Player_Index);
                bw.Write(TPTK_UnknownData);
            }

            /// <summary>
            /// KartPoint Data
            /// </summary>
            /// <param name="Position">Position Data</param>
            /// <param name="Rotation">Rotation Data</param>
            /// <param name="PlayerIndex">PlayerIndex</param>
            /// <param name="UnknownData"></param>
            public TPTKValue(Vector3D Position, Vector3D Rotation, ushort PlayerIndex, ushort UnknownData)
            {
                TPTK_Position = Position;
                TPTK_Rotation = Rotation;
                Player_Index = PlayerIndex;
                TPTK_UnknownData = UnknownData;
            }

            public TPTKValue()
            {
                TPTK_Position = new Vector3D(0, 0, 0);
                TPTK_Rotation = new Vector3D(0, 0, 0);
                Player_Index = 0;
                TPTK_UnknownData = 0;
            }
        }

        public void ReadTPTK(BinaryReader br)
        {
            TPTKHeader = br.ReadChars(4);
            if (new string(TPTKHeader) != "TPTK") throw new Exception("Error : TPTK");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int TPTKCount = 0; TPTKCount < NumOfEntries; TPTKCount++)
            {
                TPTKValue TPTK_Value = new TPTKValue();
                TPTK_Value.ReadTPTKValue(br);
                TPTKValue_List.Add(TPTK_Value);
            }
        }

        public void WriteTPTK(BinaryWriter bw)
        {
            bw.Write(TPTKHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int TPTKCount = 0; TPTKCount < NumOfEntries; TPTKCount++) TPTKValue_List[TPTKCount].WriteTPTKValue(bw);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TPTKValueList"></param>
        public TPTK(List<TPTKValue> TPTKValueList, ushort AdditionalValue = 0)
        {
            TPTKHeader = "TPTK".ToCharArray();
            NumOfEntries = Convert.ToUInt16(TPTKValueList.Count);
            this.AdditionalValue = AdditionalValue;
            TPTKValue_List = TPTKValueList;
        }

        public TPTK()
        {
            TPTKHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            TPTKValue_List = new List<TPTKValue>();
        }
    }
}
