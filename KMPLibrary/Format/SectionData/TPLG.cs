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
    /// TPLG (GlideRoute, Point)
    /// </summary>
    public class TPLG
    {
        public char[] TPLGHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<TPLGValue> TPLGValue_List { get; set; }
        public class TPLGValue
        {
            public Vector3D TPLG_Position { get; set; }
            public float TPLG_PointScaleValue { get; set; }
            public uint TPLG_UnknownData1 { get; set; }
            public uint TPLG_UnknownData2 { get; set; }

            public void ReadTPLGValue(BinaryReader br)
            {
                TPLG_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                TPLG_PointScaleValue = br.ReadSingle();
                TPLG_UnknownData1 = br.ReadUInt32();
                TPLG_UnknownData2 = br.ReadUInt32();
            }

            public void WriteTPLGValue(BinaryWriter bw)
            {
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPLG_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPLG_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPLG_Position)[2]);
                bw.Write(TPLG_PointScaleValue);
                bw.Write(TPLG_UnknownData1);
                bw.Write(TPLG_UnknownData2);
            }

            public TPLGValue(Vector3D Position, float PointScaleValue, uint UnknownData1, uint UnknownData2)
            {
                TPLG_Position = Position;
                TPLG_PointScaleValue = PointScaleValue;
                TPLG_UnknownData1 = UnknownData1;
                TPLG_UnknownData2 = UnknownData2;
            }

            public TPLGValue()
            {
                TPLG_Position = new Vector3D(0, 0, 0);
                TPLG_PointScaleValue = 0f;
                TPLG_UnknownData1 = 0;
                TPLG_UnknownData2 = 0;
            }
        }

        public void ReadTPLG(BinaryReader br)
        {
            TPLGHeader = br.ReadChars(4);
            if (new string(TPLGHeader) != "TPLG") throw new Exception("Error : TPLG");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int TPLGCount = 0; TPLGCount < NumOfEntries; TPLGCount++)
            {
                TPLGValue tPLGValue = new TPLGValue();
                tPLGValue.ReadTPLGValue(br);
                TPLGValue_List.Add(tPLGValue);
            }
        }

        public void WriteTPLG(BinaryWriter bw)
        {
            bw.Write(TPLGHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int Count = 0; Count < NumOfEntries; Count++)
            {
                TPLGValue_List[Count].WriteTPLGValue(bw);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TPLGValueList"></param>
        /// <param name="AdditionalValue">Default : 0</param>
        public TPLG(List<TPLGValue> TPLGValueList, ushort AdditionalValue = 0)
        {
            TPLGHeader = "TPLG".ToCharArray();
            NumOfEntries = Convert.ToUInt16(TPLGValueList.Count);
            this.AdditionalValue = AdditionalValue;
            TPLGValue_List = TPLGValueList;
        }

        public TPLG()
        {
            TPLGHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            TPLGValue_List = new List<TPLGValue>();
        }
    }
}
