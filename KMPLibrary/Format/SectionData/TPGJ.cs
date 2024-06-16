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
    /// TPGJ (Jugem Point (Respawn Point))
    /// </summary>
    public class TPGJ
    {
        public char[] TPGJHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<TPGJValue> TPGJValue_List { get; set; }
        public class TPGJValue
        {
            public Vector3D TPGJ_Position { get; set; }
            public Vector3D TPGJ_Rotation { get; set; }
            public ushort TPGJ_RespawnID { get; set; }
            public ushort TPGJ_UnknownData1 { get; set; }

            public void ReadTPGJValue(BinaryReader br)
            {
                TPGJ_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                TPGJ_Rotation = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                TPGJ_RespawnID = br.ReadUInt16();
                TPGJ_UnknownData1 = br.ReadUInt16();
            }

            public void WriteTPGJValue(BinaryWriter bw)
            {
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPGJ_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPGJ_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPGJ_Position)[2]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPGJ_Rotation)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPGJ_Rotation)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPGJ_Rotation)[2]);
                bw.Write(TPGJ_RespawnID);
                bw.Write(TPGJ_UnknownData1);
            }

            public TPGJValue(Vector3D Position, Vector3D Rotation, ushort RespawnID, ushort UnknownData1)
            {
                TPGJ_Position = Position;
                TPGJ_Rotation = Rotation;
                TPGJ_RespawnID = RespawnID;
                TPGJ_UnknownData1 = UnknownData1;
            }

            public TPGJValue()
            {
                TPGJ_Position = new Vector3D(0, 0, 0);
                TPGJ_Rotation = new Vector3D(0, 0, 0);
                TPGJ_RespawnID = 0;
                TPGJ_UnknownData1 = 0;
            }
        }

        public void ReadTPGJ(BinaryReader br)
        {
            TPGJHeader = br.ReadChars(4);
            if (new string(TPGJHeader) != "TPGJ") throw new Exception("Error : TPGJ");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int TPGJCount = 0; TPGJCount < NumOfEntries; TPGJCount++)
            {
                TPGJValue tPGJValue = new TPGJValue();
                tPGJValue.ReadTPGJValue(br);
                TPGJValue_List.Add(tPGJValue);
            }
        }

        public void WriteTPGJ(BinaryWriter bw)
        {
            bw.Write(TPGJHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int Count = 0; Count < NumOfEntries; Count++)
            {
                TPGJValue_List[Count].WriteTPGJValue(bw);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TPGJValueList"></param>
        /// <param name="AdditionalValue">Defalut : 0</param>
        public TPGJ(List<TPGJValue> TPGJValueList, ushort AdditionalValue = 0)
        {
            TPGJHeader = "TPGJ".ToCharArray();
            NumOfEntries = Convert.ToUInt16(TPGJValueList.Count);
            this.AdditionalValue = AdditionalValue;
            TPGJValue_List = TPGJValueList;
        }

        public TPGJ()
        {
            TPGJHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            TPGJValue_List = new List<TPGJValue>();
        }
    }
}
