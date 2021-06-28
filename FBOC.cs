using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MK7_KMP_Editor_For_PG_
{
    [Serializable()]
    public class FBOC
    {
        public char[] fboc_Chunk;
        public short fboc_HeaderSize;
        public short OBJCount;
        public List<ObjFlowData> ObjFlow_Data { get; set; }
        public class ObjFlowData
        {
            public byte[] ObjID { get; set; } //オブジェクトID(0x2)
            public byte[] ColType { get; set; }  //衝突判定(0x2)
            public byte[] PathType { get; set; }  //パスタイプ(0x2)
            public short LOD { get; set; }  //LOD(0x2)
            public int LODHPoly { get; set; }  //LOD1(ハイポリモデルでの数値), 0x4
            public int LODLPoly { get; set; }  //LOD2(ローポリモデルの数値), 0x4
            public int LODDef { get; set; }  //LOD(デフォルトの数値), 0x4
            public byte[] ModelSetting { get; set; }  //モデルの設定(0x2)
            public short ObjX { get; set; }  //X(スケールさせない場合は[0E 00]となってY、Zに値は入らない)(0x2)
            public short ObjY { get; set; }  //Y(0x2)
            public short ObjZ { get; set; }  //Z(0x2)
            public byte[] Unknown1 { get; set; }  //何も無い?(0x4)
            public char[] ObjFlowName1 { get; set; }  //Object Name 1
            public char[] ObjFlowName2 { get; set; }  //Object Name 2
        }
    }
}
