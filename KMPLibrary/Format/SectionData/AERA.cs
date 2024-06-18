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
    /// AERA (Area Section)
    /// </summary>
    public class AERA
    {
        public char[] AERAHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<AERAValue> AERAValue_List { get; set; }
        public class AERAValue
        {
            public enum AreaMode
            {
                Box = 0,
                Cylinder = 1,
                Unknown
            }

            public AreaMode AreaModeType;
            public byte AreaModeValue
            {
                get
                {
                    return (byte)AreaModeType;
                }
                set
                {
                    AreaMode areaMode;
                    if (value > 1)
                    {
                        areaMode = AreaMode.Unknown;
                    }
                    else
                    {
                        areaMode = (AreaMode)value;
                    }

                    AreaModeType = areaMode;
                }
            }

            public byte AreaType { get; set; }
            public byte AERA_EMACIndex { get; set; }
            public byte Priority { get; set; }
            public Vector3D AERA_Position { get; set; }
            public Vector3D AERA_Rotation { get; set; }
            public Vector3D AERA_Scale { get; set; }
            public ushort AERA_Setting1 { get; set; }
            public ushort AERA_Setting2 { get; set; }
            public byte RouteID { get; set; }
            public byte EnemyID { get; set; }
            public ushort AERA_UnknownData1 { get; set; }

            public void ReadAERAValue(BinaryReader br)
            {
                AreaModeValue = br.ReadByte();
                AreaType = br.ReadByte();
                AERA_EMACIndex = br.ReadByte();
                Priority = br.ReadByte();
                AERA_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                AERA_Rotation = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                AERA_Scale = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                AERA_Setting1 = br.ReadUInt16();
                AERA_Setting2 = br.ReadUInt16();
                RouteID = br.ReadByte();
                EnemyID = br.ReadByte();
                AERA_UnknownData1 = br.ReadUInt16();
            }

            public void WriteAERAValue(BinaryWriter bw)
            {
                bw.Write(AreaModeValue);
                bw.Write(AreaType);
                bw.Write(AERA_EMACIndex);
                bw.Write(Priority);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Position)[2]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Rotation)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Rotation)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Rotation)[2]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Scale)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Scale)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(AERA_Scale)[2]);
                bw.Write(AERA_Setting1);
                bw.Write(AERA_Setting2);
                bw.Write(RouteID);
                bw.Write(EnemyID);
                bw.Write(AERA_UnknownData1);
            }

            public AERAValue()
            {
                AreaModeValue = 0x00;
                AreaType = 0x00;
                AERA_EMACIndex = 0x00;
                Priority = 0x00;
                AERA_Position = new Vector3D(0, 0, 0);
                AERA_Rotation = new Vector3D(0, 0, 0);
                AERA_Scale = new Vector3D(0, 0, 0);
                AERA_Setting1 = 0;
                AERA_Setting2 = 0;
                RouteID = 0x00;
                EnemyID = 0x00;
                AERA_UnknownData1 = 0;
            }
        }

        public void ReadAERA(BinaryReader br)
        {
            AERAHeader = br.ReadChars(4);
            if (new string(AERAHeader) != "AERA") throw new Exception("Error : AERA");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int AERACount = 0; AERACount < NumOfEntries; AERACount++)
            {
                AERAValue aERAValue = new AERAValue();
                aERAValue.ReadAERAValue(br);
                AERAValue_List.Add(aERAValue);
            }
        }

        public void WriteAERA(BinaryWriter bw)
        {
            bw.Write(AERAHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int Count = 0; Count < NumOfEntries; Count++)
            {
                AERAValue_List[Count].WriteAERAValue(bw);
            }
        }

        public AERA(List<AERAValue> AERAValueList, ushort AdditionalValue = 0)
        {
            AERAHeader = "AERA".ToCharArray();
            NumOfEntries = Convert.ToUInt16(AERAValueList.Count);
            this.AdditionalValue = AdditionalValue;
            AERAValue_List = AERAValueList;
        }

        public AERA()
        {
            AERAHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            AERAValue_List = new List<AERAValue>();
        }
    }
}
