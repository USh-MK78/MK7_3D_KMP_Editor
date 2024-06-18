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
    /// TPNE (EnemyRoute, Point)
    /// </summary>
    public class TPNE
    {
        public char[] TPNEHeader { get; set; } //0x4
        public ushort NumOfEntries { get; set; } //0x2
        public ushort AdditionalValue { get; set; } //0x2
        public List<TPNEValue> TPNEValue_List { get; set; }
        public class TPNEValue
        {
            #region Enum
            public enum MaxSearchYOffsetOption
            {
                Limited_offset_MinusOne = -1,
                No_limited_offset = 0,
                Limited_offset
            }

            public enum MushSetting
            {
                CanUseMushroom = 0,
                NeedsMushroom = 1,
                CannotUseMushroom = 2,
                Unknown
            }

            public enum DriftSetting
            {
                AllowDrift_AllowMiniturbo = 0,
                DisallowDrift_AllowMiniturbo = 1,
                DisallowDrift_DisallowMiniturbo = 2,
                Unknown
            }

            public enum PathFindOption
            {
                Taken_under_unknown_flag2 = -4,
                Taken_under_unknown_flag1 = -3,
                Bullet_cannot_find = -2,
                CPU_Racer_cannot_find = -1,
                No_restrictions = 0,
                Unknown
            }
            #endregion

            public Vector3D TPNE_Position { get; set; }
            public float Control { get; set; }

            public MushSetting MushSettingType;
            public ushort MushSettingValue
            {
                get
                {
                    return (ushort)MushSettingType;
                }
                set
                {
                    MushSetting mushSetting;
                    if (value > 2)
                    {
                        mushSetting = MushSetting.Unknown;
                    }
                    else
                    {
                        mushSetting = (MushSetting)value;
                    }

                    MushSettingType = mushSetting;
                }
            }

            public DriftSetting DriftSettingType;
            public byte DriftSettingValue
            {
                get
                {
                    return (byte)DriftSettingType;
                }
                set
                {
                    DriftSetting driftSetting;
                    if (value > 2)
                    {
                        driftSetting = DriftSetting.Unknown;
                    }
                    else
                    {
                        driftSetting = (DriftSetting)value;
                    }

                    DriftSettingType = driftSetting;
                }
            }

            #region RouteSetting(I'm using the code in "KMPExpander-master\KMPExpander\Class\SimpleKMPs\EnemyRoutes.cs" of "KMP Expander")
            public byte Flags { get; set; }
            public bool WideTurn
            {
                get
                {
                    return (Flags & 0x1) != 0;
                }
                set
                {
                    Flags = (byte)((Flags & ~(1 << 0)) | ((value ? 1 : 0) << 0));
                }
            }

            public bool NormalTurn
            {
                get
                {
                    return (Flags & 0x4) != 0;
                }
                set
                {
                    Flags = (byte)((Flags & ~(1 << 2)) | ((value ? 1 : 0) << 2));
                }
            }

            public bool SharpTurn
            {
                get
                {
                    return (Flags & 0x10) != 0;
                }
                set
                {
                    Flags = (byte)((Flags & ~(1 << 4)) | ((value ? 1 : 0) << 4));
                }
            }

            public bool TricksForbidden
            {
                get
                {
                    return (Flags & 0x8) != 0;
                }
                set
                {
                    Flags = (byte)((Flags & ~(1 << 3)) | ((value ? 1 : 0) << 3));
                }
            }

            public bool StickToRoute
            {
                get
                {
                    return (Flags & 0x40) != 0;
                }
                set
                {
                    Flags = (byte)((Flags & ~(1 << 6)) | ((value ? 1 : 0) << 6));
                }
            }

            public bool BouncyMushSection
            {
                get
                {
                    return (Flags & 0x20) != 0;
                }
                set
                {
                    Flags = (byte)((Flags & ~(1 << 5)) | ((value ? 1 : 0) << 5));
                }
            }

            public bool ForceDefaultSpeed
            {
                get
                {
                    return (Flags & 0x80) != 0;
                }
                set
                {
                    Flags = (byte)((Flags & ~(1 << 7)) | ((value ? 1 : 0) << 7));
                }
            }

            public bool NoPathSwitch
            {
                get
                {
                    return (Flags & 0x2) != 0;
                }
                set
                {
                    Flags = (byte)((Flags & ~(1 << 1)) | ((value ? 1 : 0) << 1));
                }
            }
            #endregion

            public PathFindOption PathFindOptionType;
            public short PathFindOptionValue
            {
                get
                {
                    return (short)PathFindOptionType;
                }
                set
                {
                    PathFindOption pathFindOption;
                    if (value > 0 || value < -4)
                    {
                        pathFindOption = PathFindOption.Unknown;
                    }
                    else
                    {
                        pathFindOption = (PathFindOption)value;
                    }

                    PathFindOptionType = pathFindOption;
                }
            }

            public MaxSearchYOffsetOption MaxSearchYOffsetType;
            public short MaxSearchYOffsetValue
            {
                get
                {
                    return (short)MaxSearchYOffsetType;
                }
                set
                {
                    MaxSearchYOffsetOption maxSearchYOffsetOption;
                    if (value < 0)
                    {
                        maxSearchYOffsetOption = MaxSearchYOffsetOption.Limited_offset_MinusOne;
                    }
                    else if (value > 0)
                    {
                        maxSearchYOffsetOption = MaxSearchYOffsetOption.Limited_offset;
                    }
                    else
                    {
                        maxSearchYOffsetOption = MaxSearchYOffsetOption.No_limited_offset;
                    }

                    MaxSearchYOffsetType = maxSearchYOffsetOption;
                }
            }

            public void ReadTPNEValue(BinaryReader br)
            {
                TPNE_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                Control = br.ReadSingle();
                MushSettingValue = br.ReadUInt16();
                DriftSettingValue = br.ReadByte();
                Flags = br.ReadByte();
                PathFindOptionValue = br.ReadInt16();
                MaxSearchYOffsetValue = br.ReadInt16();
            }

            public void WriteTPNEValue(BinaryWriter bw)
            {
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPNE_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPNE_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(TPNE_Position)[2]);
                bw.Write(Control);
                bw.Write(MushSettingValue);
                bw.Write(DriftSettingValue);
                bw.Write(Flags);
                bw.Write(PathFindOptionValue);
                bw.Write(MaxSearchYOffsetValue);
            }

            public TPNEValue(Vector3D Position, float Control, ushort MushSetting, byte DriftSetting, byte Flags, short PathFindOption, short MaxSearchYOffset)
            {
                TPNE_Position = Position;
                this.Control = Control;
                this.MushSettingValue = MushSetting;
                this.DriftSettingValue = DriftSetting;
                this.Flags = Flags;
                this.PathFindOptionValue = PathFindOption;
                this.MaxSearchYOffsetValue = MaxSearchYOffset;
            }

            public TPNEValue()
            {
                TPNE_Position = new Vector3D(0, 0, 0);
                Control = 0f;
                MushSettingValue = 0;
                DriftSettingValue = 0x00;
                Flags = 0x00;
                PathFindOptionValue = 0;
                MaxSearchYOffsetValue = 0;
            }
        }

        public void ReadTPNE(BinaryReader br)
        {
            TPNEHeader = br.ReadChars(4);
            if (new string(TPNEHeader) != "TPNE") throw new Exception("Error : TPNE");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int TPNECount = 0; TPNECount < NumOfEntries; TPNECount++)
            {
                TPNEValue TPNE_Value = new TPNEValue();
                TPNE_Value.ReadTPNEValue(br);
                TPNEValue_List.Add(TPNE_Value);
            }
        }

        public void WriteTPNE(BinaryWriter bw)
        {
            bw.Write(TPNEHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int Count = 0; Count < TPNEValue_List.Count; Count++)
            {
                TPNEValue_List[Count].WriteTPNEValue(bw);
            }
        }

        public TPNE(List<TPNEValue> TPNEValueList)
        {
            TPNEHeader = "TPNE".ToCharArray();
            NumOfEntries = Convert.ToUInt16(TPNEValueList.Count);
            AdditionalValue = 0;
            TPNEValue_List = TPNEValueList;
        }

        public TPNE()
        {
            TPNEHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            TPNEValue_List = new List<TPNEValue>();
        }
    }
}
