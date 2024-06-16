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
    /// JBOG (Game Object)
    /// </summary>
    public class JBOG
    {
        public char[] JBOGHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<JBOGValue> JBOGValue_List { get; set; }
        public class JBOGValue
        {
            public byte[] ObjectID { get; set; }
            public byte[] JBOG_UnkByte1 { get; set; }
            public Vector3D JBOG_Position { get; set; }
            public Vector3D JBOG_Rotation { get; set; }
            public Vector3D JBOG_Scale { get; set; }
            public ushort JBOG_ITOP_RouteIDIndex { get; set; }
            public JBOG_SpecificSetting GOBJ_Specific_Setting { get; set; }
            public class JBOG_SpecificSetting
            {
                public ushort Value0 { get; set; }
                public ushort Value1 { get; set; }
                public ushort Value2 { get; set; }
                public ushort Value3 { get; set; }
                public ushort Value4 { get; set; }
                public ushort Value5 { get; set; }
                public ushort Value6 { get; set; }
                public ushort Value7 { get; set; }

                public void ReadSpecificSetting(BinaryReader br)
                {
                    Value0 = br.ReadUInt16();
                    Value1 = br.ReadUInt16();
                    Value2 = br.ReadUInt16();
                    Value3 = br.ReadUInt16();
                    Value4 = br.ReadUInt16();
                    Value5 = br.ReadUInt16();
                    Value6 = br.ReadUInt16();
                    Value7 = br.ReadUInt16();
                }

                public void WriteSpecificSetting(BinaryWriter bw)
                {
                    bw.Write(Value0);
                    bw.Write(Value1);
                    bw.Write(Value2);
                    bw.Write(Value3);
                    bw.Write(Value4);
                    bw.Write(Value5);
                    bw.Write(Value6);
                    bw.Write(Value7);
                }

                public ushort[] GetSpecificSettingArray()
                {
                    return new ushort[] { Value0, Value1, Value2, Value3, Value4, Value5, Value6, Value7 };
                }

                public JBOG_SpecificSetting(ushort[] SpecificSettingArray)
                {
                    Value0 = SpecificSettingArray[0];
                    Value1 = SpecificSettingArray[1];
                    Value2 = SpecificSettingArray[2];
                    Value3 = SpecificSettingArray[3];
                    Value4 = SpecificSettingArray[4];
                    Value5 = SpecificSettingArray[5];
                    Value6 = SpecificSettingArray[6];
                    Value7 = SpecificSettingArray[7];
                }

                public JBOG_SpecificSetting()
                {
                    Value0 = 255;
                    Value1 = 255;
                    Value2 = 255;
                    Value3 = 255;
                    Value4 = 255;
                    Value5 = 255;
                    Value6 = 255;
                    Value7 = 255;
                }
            }
            public ushort JBOG_PresenceSetting { get; set; }
            public byte[] JBOG_UnkByte2 { get; set; }
            public ushort JBOG_UnkByte3 { get; set; }

            public void ReadJBOGValue(BinaryReader br, uint Version)
            {
                ObjectID = br.ReadBytes(2);
                JBOG_UnkByte1 = br.ReadBytes(2);
                JBOG_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                JBOG_Rotation = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                JBOG_Scale = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                JBOG_ITOP_RouteIDIndex = br.ReadUInt16();
                GOBJ_Specific_Setting.ReadSpecificSetting(br);
                JBOG_PresenceSetting = br.ReadUInt16();

                if (Version == 3100)
                {
                    JBOG_UnkByte2 = br.ReadBytes(2);
                    JBOG_UnkByte3 = br.ReadUInt16();
                }
                else if (Version == 3000) return;
            }

            public void WriteJBOGValue(BinaryWriter bw, uint Version)
            {
                bw.Write(ObjectID);
                bw.Write(JBOG_UnkByte1);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Position)[2]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Rotation)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Rotation)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Rotation)[2]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Scale)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Scale)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(JBOG_Scale)[2]);
                bw.Write(JBOG_ITOP_RouteIDIndex);
                GOBJ_Specific_Setting.WriteSpecificSetting(bw);
                bw.Write(JBOG_PresenceSetting);

                if (Version == 3100)
                {
                    bw.Write(JBOG_UnkByte2);
                    bw.Write(JBOG_UnkByte3);
                }
                else if (Version == 3000) return;
            }

            public JBOGValue()
            {
                ObjectID = new byte[2];
                JBOG_UnkByte1 = new byte[2];
                JBOG_Position = new Vector3D(0, 0, 0);
                JBOG_Rotation = new Vector3D(0, 0, 0);
                JBOG_Scale = new Vector3D(0, 0, 0);
                JBOG_ITOP_RouteIDIndex = 0;
                GOBJ_Specific_Setting = new JBOG_SpecificSetting();
                JBOG_PresenceSetting = 0;
                JBOG_UnkByte2 = new byte[2];
                JBOG_UnkByte3 = 0;
            }
        }

        public void ReadJBOG(BinaryReader br, uint Version)
        {
            JBOGHeader = br.ReadChars(4);
            if (new string(JBOGHeader) != "JBOG") throw new Exception("Error : JBOG");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int JBOGCount = 0; JBOGCount < NumOfEntries; JBOGCount++)
            {
                JBOGValue JBOG_Value = new JBOGValue();
                JBOG_Value.ReadJBOGValue(br, Version);
                JBOGValue_List.Add(JBOG_Value);
            }
        }

        public void WriteJBOG(BinaryWriter bw, uint Version)
        {
            bw.Write(JBOGHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int Count = 0; Count < NumOfEntries; Count++) JBOGValue_List[Count].WriteJBOGValue(bw, Version);
        }

        public JBOG(List<JBOGValue> JBOGValueList, ushort AdditionalValue = 0)
        {
            JBOGHeader = "JBOG".ToCharArray();
            NumOfEntries = Convert.ToUInt16(JBOGValueList.Count);
            this.AdditionalValue = AdditionalValue;
            JBOGValue_List = JBOGValueList;
        }

        public JBOG()
        {
            JBOGHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            JBOGValue_List = new List<JBOGValue>();
        }
    }
}
