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
    /// TPTI (ItemRoute, Point)
    /// </summary>
    public class TPTI
    {
        public char[] TPTIHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<TPTIValue> TPTIValue_List { get; set; }
        public class TPTIValue
        {
            #region Enum
            public enum GravityMode
            {
                Affected_By_Gravity = 0,
                Unaffected_By_Gravity = 1,
                Cannon_Section = 2,
                Unknown
            }

            public enum PlayerScanRadius
            {
                Small = 0,
                Big = 1,
                Unknown
            }
            #endregion

            public Vector3D TPTI_Position { get; set; }
            public float TPTI_PointSize { get; set; }

            public GravityMode GravityModeType;
            public ushort GravityModeValue
            {
                get
                {
                    return (ushort)GravityModeType;
                }
                set
                {
                    GravityMode gravityMode;
                    if (value > 2)
                    {
                        gravityMode = GravityMode.Unknown;
                    }
                    else
                    {
                        gravityMode = (GravityMode)value;
                    }

                    GravityModeType = gravityMode;
                }
            }

            public PlayerScanRadius PlayerScanRadiusType;
            public ushort PlayerScanRadiusValue
            {
                get
                {
                    return (ushort)PlayerScanRadiusType;
                }
                set
                {
                    PlayerScanRadius playerScanRadius;
                    if (value > 1)
                    {
                        playerScanRadius = PlayerScanRadius.Unknown;
                    }
                    else
                    {
                        playerScanRadius = (PlayerScanRadius)value;
                    }

                    PlayerScanRadiusType = playerScanRadius;
                }
            }

            public void ReadTPTIValue(BinaryReader br)
            {
                TPTI_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                TPTI_PointSize = br.ReadSingle();
                GravityModeValue = br.ReadUInt16();
                PlayerScanRadiusValue = br.ReadUInt16();
            }

            public void WriteTPTIValue(BinaryWriter bw)
            {
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTI_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTI_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPTI_Position)[2]);
                bw.Write(TPTI_PointSize);
                bw.Write(GravityModeValue);
                bw.Write(PlayerScanRadiusValue);
            }

            public TPTIValue()
            {
                TPTI_Position = new Vector3D(0, 0, 0);
                TPTI_PointSize = 0;
                GravityModeValue = 0;
                PlayerScanRadiusValue = 0;
            }
        }

        public void ReadTPTI(BinaryReader br)
        {
            TPTIHeader = br.ReadChars(4);
            if (new string(TPTIHeader) != "TPTI") throw new Exception("Error : TPTI");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();
            for (int TPTICount = 0; TPTICount < NumOfEntries; TPTICount++)
            {
                TPTIValue TPTI_Value = new TPTIValue();
                TPTI_Value.ReadTPTIValue(br);
                TPTIValue_List.Add(TPTI_Value);
            }
        }

        public void WriteTPTI(BinaryWriter bw)
        {
            bw.Write(TPTIHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);
            for (int TPTICount = 0; TPTICount < NumOfEntries; TPTICount++) TPTIValue_List[TPTICount].WriteTPTIValue(bw);
        }

        public TPTI(List<TPTIValue> TPTIValueList, ushort AdditionalValue = 0)
        {
            TPTIHeader = "TPTI".ToCharArray();
            NumOfEntries = Convert.ToUInt16(TPTIValueList.Count);
            this.AdditionalValue = AdditionalValue;
            TPTIValue_List = TPTIValueList;
        }

        public TPTI()
        {
            TPTIHeader = "TPTI".ToCharArray();
            NumOfEntries = 0;
            AdditionalValue = 0;
            TPTIValue_List = new List<TPTIValue>();
        }
    }
}
