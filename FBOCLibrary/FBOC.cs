using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBOCLibrary
{
    /// <summary>
    /// MK7 ObjFlowData (FBOC)
    /// </summary>
    public class FBOC
    {
        public char[] FBOC_Header { get; set; }
        public short FBOC_HeaderSize { get; set; }
        public short NumOfObjFlowData { get; set; }
        public List<ObjFlowData> ObjFlowDataList { get; set; }
        public class ObjFlowData
        {
            public string Name1 => new string(ObjFlowName1).Replace("\0", "");
            public string Name2 => new string(ObjFlowName2).Replace("\0", "");

            public byte[] ObjectID { get; set; } //オブジェクトID(0x2)
            public byte[] CollisionType { get; set; }  //衝突判定(0x2)
            public byte[] PathType { get; set; }  //パスタイプ(0x2)

            public LODSetting LOD_Setting { get; set; }
            public class LODSetting
            {
                public short LOD { get; set; }  //LOD(0x2)
                public int LODHighPoly { get; set; }  //LOD1(ハイポリモデルでの数値), 0x4
                public int LODLowPoly { get; set; }  //LOD2(ローポリモデルの数値), 0x4
                public int LODDefault { get; set; }  //LOD(デフォルトの数値), 0x4

                public void Read_LODSetting(BinaryReader br)
                {
                    LOD = br.ReadInt16();
                    LODHighPoly = br.ReadInt32();
                    LODLowPoly = br.ReadInt32();
                    LODDefault = br.ReadInt32();
                }

                public void Write_LODSetting(BinaryWriter bw)
                {
                    bw.Write(LOD);
                    bw.Write(LODHighPoly);
                    bw.Write(LODLowPoly);
                    bw.Write(LODDefault);
                }

                public LODSetting(short LODValue, int LOD_High, int LOD_Low, int LOD_Default)
                {
                    LOD = LODValue;
                    LODHighPoly = LOD_High;
                    LODLowPoly = LOD_Low;
                    LODDefault = LOD_Default;
                }

                public LODSetting()
                {
                    LOD = 0;
                    LODHighPoly = 0;
                    LODLowPoly = 0;
                    LODDefault = 0;
                }
            }

            public byte[] ModelSetting { get; set; }  //モデルの設定(0x2)

            public ObjFlowScaleSetting ObjFlowScale { get; set; }
            public class ObjFlowScaleSetting
            {
                public short X { get; set; }  //X(スケールさせない場合は[0E 00]となってY、Zに値は入らない)(0x2)
                public short Y { get; set; }  //Y(0x2)
                public short Z { get; set; }  //Z(0x2)

                public short[] GetArray()
                {
                    return new short[] { X, Y, Z };
                }

                //public static ObjFlowScaleSetting NoScaling()
                //{
                //    return new ObjFlowScaleSetting(1, 0, 0);
                //}

                public void Read_ObjFlowScale(BinaryReader br)
                {
                    X = br.ReadInt16();
                    Y = br.ReadInt16();
                    Z = br.ReadInt16();
                }

                public void Write_ObjFlowScale(BinaryWriter bw)
                {
                    bw.Write(X);
                    bw.Write(Y);
                    bw.Write(Z);
                }

                public ObjFlowScaleSetting(short X, short Y, short Z)
                {
                    this.X = X;
                    this.Y = Y;
                    this.Z = Z;
                }

                public ObjFlowScaleSetting()
                {
                    X = 0;
                    Y = 0;
                    Z = 0;
                }
            }

            public byte[] Unknown1 { get; set; }  //何も無い?(0x4)
            public char[] ObjFlowName1 { get; set; }  //Object Name 1 (64 byte)
            public char[] ObjFlowName2 { get; set; }  //Object Name 2 (64 byte)

            public void Read_ObjFlow(BinaryReader br)
            {
                ObjectID = br.ReadBytes(2);
                CollisionType = br.ReadBytes(2);
                PathType = br.ReadBytes(2);
                LOD_Setting.Read_LODSetting(br);
                ModelSetting = br.ReadBytes(2);
                ObjFlowScale.Read_ObjFlowScale(br);
                Unknown1 = br.ReadBytes(4);
                ObjFlowName1 = br.ReadChars(64);
                ObjFlowName2 = br.ReadChars(64);
            }

            public void Write_ObjFlow(BinaryWriter bw)
            {
                bw.Write(ObjectID);
                bw.Write(CollisionType);
                bw.Write(PathType);
                LOD_Setting.Write_LODSetting(bw);
                bw.Write(ModelSetting);
                ObjFlowScale.Write_ObjFlowScale(bw);
                bw.Write(Unknown1);
                bw.Write(ObjFlowName1);
                bw.Write(ObjFlowName2);
            }

            public ObjFlowData(byte[] ID, byte[] CollisionType, byte[] PathType, LODSetting LOD, byte[] ModelSetting, ObjFlowScaleSetting ScaleSetting, byte[] Unknown1, string Name1, string Name2 = "")
            {
                ObjectID = ID;
                this.CollisionType = CollisionType;
                this.PathType = PathType;
                LOD_Setting = LOD;
                this.ModelSetting = ModelSetting;
                ObjFlowScale = ScaleSetting;
                this.Unknown1 = Unknown1;
                ObjFlowName1 = Misc.ZEROPaddingedCharArray(Name1.ToCharArray());
                ObjFlowName2 = Misc.ZEROPaddingedCharArray(Name2.ToCharArray());
            }

            public ObjFlowData()
            {
                ObjectID = new byte[2];
                CollisionType = new byte[2];
                PathType = new byte[2];
                LOD_Setting = new LODSetting();
                ModelSetting = new byte[2];
                ObjFlowScale = new ObjFlowScaleSetting();
                Unknown1 = new byte[4];
                ObjFlowName1 = new char[64];
                ObjFlowName2 = new char[64];
            }
        }

        public void ReadFBOC(BinaryReader br)
        {
            FBOC_Header = br.ReadChars(4);
            if (new string(FBOC_Header) != "FBOC") throw new Exception("Invalid file.");
            FBOC_HeaderSize = br.ReadInt16();
            NumOfObjFlowData = br.ReadInt16();

            for (int Count = 0; Count < NumOfObjFlowData; Count++)
            {
                ObjFlowData objFlowData = new ObjFlowData();
                objFlowData.Read_ObjFlow(br);
                ObjFlowDataList.Add(objFlowData);
            }
        }

        public void WriteFBOC(BinaryWriter bw)
        {
            bw.Write(FBOC_Header);
            bw.Write(FBOC_HeaderSize);
            bw.Write(NumOfObjFlowData);

            for (int Count = 0; Count < NumOfObjFlowData; Count++)
            {
                ObjFlowDataList[Count].Write_ObjFlow(bw);
            }
        }

        public FBOC(short Count)
        {
            FBOC_Header = "FBOC".ToArray();
            FBOC_HeaderSize = 8; //Default
            NumOfObjFlowData = Count;
            ObjFlowDataList = new List<ObjFlowData>(Count);
        }

        public FBOC(List<ObjFlowData> ObjFlowDataList)
        {
            FBOC_Header = "FBOC".ToArray();
            FBOC_HeaderSize = 8; //Default
            NumOfObjFlowData = Convert.ToInt16(ObjFlowDataList.Count);
            this.ObjFlowDataList = ObjFlowDataList;
        }

        public FBOC()
        {
            FBOC_Header = "FBOC".ToArray();
            FBOC_HeaderSize = 8; //Default
            NumOfObjFlowData = 0;
            ObjFlowDataList = new List<ObjFlowData>();
        }
    }

    public class Misc
    {
        public static char[] ZEROPaddingedCharArray(char[] chars, int DefaultArrayLength = 64)
        {
            int ZEROPaddingLength = DefaultArrayLength - chars.Length;
            char[] ZEROPaddingCharArray = new char[ZEROPaddingLength];
            for (int Count = 0; Count < ZEROPaddingLength; Count++)
            {
                ZEROPaddingCharArray[Count] = '\0';
            }

            return chars.Concat(ZEROPaddingCharArray).ToArray();
        }
    }
}
