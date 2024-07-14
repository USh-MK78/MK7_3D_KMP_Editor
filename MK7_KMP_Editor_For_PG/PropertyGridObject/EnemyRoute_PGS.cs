using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using KMPLibrary.Format.SectionData;
using static MK7_3D_KMP_Editor.PropertyGridObject.CustomPropertyGridClassConverter;

namespace MK7_3D_KMP_Editor.PropertyGridObject
{
    public class HPNE_TPNEData
    {
        public HPNE HPNE_Section;
        public TPNE TPNE_Section;

        public HPNE_TPNEData(HPNE HPNE, TPNE TPNE)
        {
            HPNE_Section = HPNE;
            TPNE_Section = TPNE;
        }
    }

    /// <summary>
    /// EnemyRoute (PropertyGrid)
    /// </summary>
    public class EnemyRoute_PGS
    {
        public List<HPNEValue> HPNEValue_List = new List<HPNEValue>();
        public List<HPNEValue> HPNEValueList { get => HPNEValue_List; set => HPNEValue_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class HPNEValue
        {
            [ReadOnly(true)]
            public int GroupID { get; set; }

            public bool IsViewportVisible { get; set; } = true;

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public HPNE_PreviewGroups HPNEPreviewGroups { get; set; } = new HPNE_PreviewGroups();
            public class HPNE_PreviewGroups
            {
                public ushort Prev0 { get; set; }
                public ushort Prev1 { get; set; }
                public ushort Prev2 { get; set; }
                public ushort Prev3 { get; set; }
                public ushort Prev4 { get; set; }
                public ushort Prev5 { get; set; }
                public ushort Prev6 { get; set; }
                public ushort Prev7 { get; set; }
                public ushort Prev8 { get; set; }
                public ushort Prev9 { get; set; }
                public ushort Prev10 { get; set; }
                public ushort Prev11 { get; set; }
                public ushort Prev12 { get; set; }
                public ushort Prev13 { get; set; }
                public ushort Prev14 { get; set; }
                public ushort Prev15 { get; set; }

                public ushort[] GetPrevGroupValueArray()
                {
                    return new ushort[] { Prev0, Prev1, Prev2, Prev3, Prev4, Prev5, Prev6, Prev7, Prev8, Prev9, Prev10, Prev11, Prev12, Prev13, Prev14, Prev15 };
                }

                public HPNE_PreviewGroups(ushort[] PrevGroupArray)
                {
                    Prev0 = PrevGroupArray[0];
                    Prev1 = PrevGroupArray[1];
                    Prev2 = PrevGroupArray[2];
                    Prev3 = PrevGroupArray[3];
                    Prev4 = PrevGroupArray[4];
                    Prev5 = PrevGroupArray[5];
                    Prev6 = PrevGroupArray[6];
                    Prev7 = PrevGroupArray[7];
                    Prev8 = PrevGroupArray[8];
                    Prev9 = PrevGroupArray[9];
                    Prev10 = PrevGroupArray[10];
                    Prev11 = PrevGroupArray[11];
                    Prev12 = PrevGroupArray[12];
                    Prev13 = PrevGroupArray[13];
                    Prev14 = PrevGroupArray[14];
                    Prev15 = PrevGroupArray[15];
                }

                public HPNE_PreviewGroups()
                {
                    Prev0 = 65535;
                    Prev1 = 65535;
                    Prev2 = 65535;
                    Prev3 = 65535;
                    Prev4 = 65535;
                    Prev5 = 65535;
                    Prev6 = 65535;
                    Prev7 = 65535;
                    Prev8 = 65535;
                    Prev9 = 65535;
                    Prev10 = 65535;
                    Prev11 = 65535;
                    Prev12 = 65535;
                    Prev13 = 65535;
                    Prev14 = 65535;
                    Prev15 = 65535;
                }

                public HPNE_PreviewGroups(HPNE.HPNEValue.HPNE_PreviewGroups HPNE_PreviewGroup)
                {
                    Prev0 = HPNE_PreviewGroup.Prev0;
                    Prev1 = HPNE_PreviewGroup.Prev1;
                    Prev2 = HPNE_PreviewGroup.Prev2;
                    Prev3 = HPNE_PreviewGroup.Prev3;
                    Prev4 = HPNE_PreviewGroup.Prev4;
                    Prev5 = HPNE_PreviewGroup.Prev5;
                    Prev6 = HPNE_PreviewGroup.Prev6;
                    Prev7 = HPNE_PreviewGroup.Prev7;
                    Prev8 = HPNE_PreviewGroup.Prev8;
                    Prev9 = HPNE_PreviewGroup.Prev9;
                    Prev10 = HPNE_PreviewGroup.Prev10;
                    Prev11 = HPNE_PreviewGroup.Prev11;
                    Prev12 = HPNE_PreviewGroup.Prev12;
                    Prev13 = HPNE_PreviewGroup.Prev13;
                    Prev14 = HPNE_PreviewGroup.Prev14;
                    Prev15 = HPNE_PreviewGroup.Prev15;
                }

                public HPNE_PreviewGroups(KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute.EnemyRoute_Group.ER_PreviousGroup Previous)
                {
                    Prev0 = Previous.Prev0;
                    Prev1 = Previous.Prev1;
                    Prev2 = Previous.Prev2;
                    Prev3 = Previous.Prev3;
                    Prev4 = Previous.Prev4;
                    Prev5 = Previous.Prev5;
                    Prev6 = Previous.Prev6;
                    Prev7 = Previous.Prev7;
                    Prev8 = Previous.Prev8;
                    Prev9 = Previous.Prev9;
                    Prev10 = Previous.Prev10;
                    Prev11 = Previous.Prev11;
                    Prev12 = Previous.Prev12;
                    Prev13 = Previous.Prev13;
                    Prev14 = Previous.Prev14;
                    Prev15 = Previous.Prev15;
                }

                public override string ToString()
                {
                    return "Preview";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public HPNE_NextGroups HPNENextGroups { get; set; } = new HPNE_NextGroups();
            public class HPNE_NextGroups
            {
                public ushort Next0 { get; set; }
                public ushort Next1 { get; set; }
                public ushort Next2 { get; set; }
                public ushort Next3 { get; set; }
                public ushort Next4 { get; set; }
                public ushort Next5 { get; set; }
                public ushort Next6 { get; set; }
                public ushort Next7 { get; set; }
                public ushort Next8 { get; set; }
                public ushort Next9 { get; set; }
                public ushort Next10 { get; set; }
                public ushort Next11 { get; set; }
                public ushort Next12 { get; set; }
                public ushort Next13 { get; set; }
                public ushort Next14 { get; set; }
                public ushort Next15 { get; set; }

                public ushort[] GetNextGroupValueArray()
                {
                    return new ushort[] { Next0, Next1, Next2, Next3, Next4, Next5, Next6, Next7, Next8, Next9, Next10, Next11, Next12, Next13, Next14, Next15 };
                }

                public HPNE_NextGroups(ushort[] NextGroupArray)
                {
                    Next0 = NextGroupArray[0];
                    Next1 = NextGroupArray[1];
                    Next2 = NextGroupArray[2];
                    Next3 = NextGroupArray[3];
                    Next4 = NextGroupArray[4];
                    Next5 = NextGroupArray[5];
                    Next6 = NextGroupArray[6];
                    Next7 = NextGroupArray[7];
                    Next8 = NextGroupArray[8];
                    Next9 = NextGroupArray[9];
                    Next10 = NextGroupArray[10];
                    Next11 = NextGroupArray[11];
                    Next12 = NextGroupArray[12];
                    Next13 = NextGroupArray[13];
                    Next14 = NextGroupArray[14];
                    Next15 = NextGroupArray[15];
                }

                public HPNE_NextGroups()
                {
                    Next0 = 65535;
                    Next1 = 65535;
                    Next2 = 65535;
                    Next3 = 65535;
                    Next4 = 65535;
                    Next5 = 65535;
                    Next6 = 65535;
                    Next7 = 65535;
                    Next8 = 65535;
                    Next9 = 65535;
                    Next10 = 65535;
                    Next11 = 65535;
                    Next12 = 65535;
                    Next13 = 65535;
                    Next14 = 65535;
                    Next15 = 65535;
                }

                public HPNE_NextGroups(HPNE.HPNEValue.HPNE_NextGroups HPNE_NextGroup)
                {
                    Next0 = HPNE_NextGroup.Next0;
                    Next1 = HPNE_NextGroup.Next1;
                    Next2 = HPNE_NextGroup.Next2;
                    Next3 = HPNE_NextGroup.Next3;
                    Next4 = HPNE_NextGroup.Next4;
                    Next5 = HPNE_NextGroup.Next5;
                    Next6 = HPNE_NextGroup.Next6;
                    Next7 = HPNE_NextGroup.Next7;
                    Next8 = HPNE_NextGroup.Next8;
                    Next9 = HPNE_NextGroup.Next9;
                    Next10 = HPNE_NextGroup.Next10;
                    Next11 = HPNE_NextGroup.Next11;
                    Next12 = HPNE_NextGroup.Next12;
                    Next13 = HPNE_NextGroup.Next13;
                    Next14 = HPNE_NextGroup.Next14;
                    Next15 = HPNE_NextGroup.Next15;
                }

                public HPNE_NextGroups(KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute.EnemyRoute_Group.ER_NextGroup Next)
                {
                    Next0 = Next.Next0;
                    Next1 = Next.Next1;
                    Next2 = Next.Next2;
                    Next3 = Next.Next3;
                    Next4 = Next.Next4;
                    Next5 = Next.Next5;
                    Next6 = Next.Next6;
                    Next7 = Next.Next7;
                    Next8 = Next.Next8;
                    Next9 = Next.Next9;
                    Next10 = Next.Next10;
                    Next11 = Next.Next11;
                    Next12 = Next.Next12;
                    Next13 = Next.Next13;
                    Next14 = Next.Next14;
                    Next15 = Next.Next15;
                }

                public override string ToString()
                {
                    return "Next";
                }
            }

            public ushort UnknwonData1 { get; set; }
            public ushort UnknownData2 { get; set; }

            public List<TPNEValue> TPNEValue_List = new List<TPNEValue>();
            [Browsable(false)]
            public List<TPNEValue> TPNEValueList { get => TPNEValue_List; set => TPNEValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class TPNEValue
            {
                [ReadOnly(true)]
                public int Group_ID { get; set; }

                [ReadOnly(true)]
                public int ID { get; set; }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Position Positions { get; set; } = new Position();
                public class Position
                {
                    private float _X;
                    public float X
                    {
                        get { return _X; }
                        set { _X = value; }
                    }

                    private float _Y;
                    public float Y
                    {
                        get { return _Y; }
                        set { _Y = value; }
                    }

                    private float _Z;
                    public float Z
                    {
                        get { return _Z; }
                        set { _Z = value; }
                    }

                    public Position()
                    {
                        _X = 0;
                        _Y = 0;
                        _Z = 0;
                    }

                    public Position(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
                    }

                    public Position(Vector3D vector3D)
                    {
                        _X = (float)vector3D.X;
                        _Y = (float)vector3D.Y;
                        _Z = (float)vector3D.Z;
                    }

                    public Vector3D GetVector3D()
                    {
                        double X = Convert.ToDouble(_X);
                        double Y = Convert.ToDouble(_Y);
                        double Z = Convert.ToDouble(_Z);

                        return new Vector3D(X, Y, Z);
                    }

                    public override string ToString()
                    {
                        return "Position";
                    }
                }

                public float Control { get; set; }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public MushSetting MushSettings { get; set; } = new MushSetting();
                public class MushSetting
                {
                    [ReadOnly(true)]
                    public TPNE.TPNEValue.MushSetting MushSettingEnum
                    {
                        get { return (TPNE.TPNEValue.MushSetting)Enum.ToObject(typeof(TPNE.TPNEValue.MushSetting), MushSettingValue); }
                    }

                    public ushort MushSettingValue { get; set; }

                    public override string ToString()
                    {
                        return "Mush Setting";
                    }
                }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public DriftSetting DriftSettings { get; set; } = new DriftSetting();
                public class DriftSetting
                {
                    [ReadOnly(true)]
                    public TPNE.TPNEValue.DriftSetting DriftSettingEnum
                    {
                        get { return (TPNE.TPNEValue.DriftSetting)Enum.ToObject(typeof(TPNE.TPNEValue.DriftSetting), DriftSettingValue); }
                    }

                    public byte DriftSettingValue { get; set; }

                    public override string ToString()
                    {
                        return "Drift Setting";
                    }
                }

                #region Flags(I'm using the code in "KMPExpander-master\KMPExpander\Class\SimpleKMPs\EnemyRoutes.cs" of "KMP Expander")
                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public FlagSetting FlagSettings { get; set; } = new FlagSetting();
                public class FlagSetting
                {
                    [Browsable(false)]
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

                    public override string ToString()
                    {
                        return "Flag Setting";
                    }
                }
                #endregion

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public PathFindOption PathFindOptions { get; set; } = new PathFindOption();
                public class PathFindOption
                {
                    [ReadOnly(true)]
                    public TPNE.TPNEValue.PathFindOption PathFindOptionEnum
                    {
                        get { return (TPNE.TPNEValue.PathFindOption)Enum.ToObject(typeof(TPNE.TPNEValue.PathFindOption), PathFindOptionValue); }
                    }

                    public short PathFindOptionValue { get; set; }

                    public override string ToString()
                    {
                        return "PathFindOption";
                    }
                }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public MaxSearch_YOffset MaxSearchYOffset { get; set; } = new MaxSearch_YOffset();
                public class MaxSearch_YOffset
                {
                    [ReadOnly(true)]
                    public TPNE.TPNEValue.MaxSearchYOffsetOption MaxSearchYOffsetOptionEnum
                    {
                        get { return (TPNE.TPNEValue.MaxSearchYOffsetOption)Enum.ToObject(typeof(TPNE.TPNEValue.MaxSearchYOffsetOption), MaxSearchYOffsetValue); }
                    }

                    public short MaxSearchYOffsetValue { get; set; }

                    public override string ToString()
                    {
                        return "MaxSearchYOffset";
                    }
                }

                public TPNEValue(Vector3D Pos, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    Positions = new Position(Pos);
                    Control = 1;
                    MushSettings.MushSettingValue = 0;
                    DriftSettings.DriftSettingValue = 0;
                    FlagSettings.Flags = 0x00;
                    PathFindOptions.PathFindOptionValue = 0;
                    MaxSearchYOffset.MaxSearchYOffsetValue = 0;
                }

                public TPNEValue(TPNE.TPNEValue TPNEValue, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    Positions = new Position(TPNEValue.TPNE_Position);
                    Control = TPNEValue.Control;
                    MushSettings.MushSettingValue = TPNEValue.MushSettingValue;
                    DriftSettings.DriftSettingValue = TPNEValue.DriftSettingValue;
                    FlagSettings.Flags = TPNEValue.Flags;
                    PathFindOptions.PathFindOptionValue = TPNEValue.PathFindOptionValue;
                    MaxSearchYOffset.MaxSearchYOffsetValue = TPNEValue.MaxSearchYOffsetValue;
                }

                public TPNEValue(KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point EnemyRoute_Point, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    Positions = new Position(EnemyRoute_Point.Position.ToVector3D());
                    Control = EnemyRoute_Point.Control;
                    MushSettings.MushSettingValue = EnemyRoute_Point.MushSetting;
                    DriftSettings.DriftSettingValue = EnemyRoute_Point.DriftSetting;
                    FlagSettings.Flags = EnemyRoute_Point.Flags;
                    PathFindOptions.PathFindOptionValue = EnemyRoute_Point.PathFindOption;
                    MaxSearchYOffset.MaxSearchYOffsetValue = EnemyRoute_Point.MaxSearchYOffset;
                }

                public TPNEValue(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData PointData, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    Positions = new Position(PointData.Position.ToVector3D());
                    Control = PointData.ScaleValue;
                    MushSettings.MushSettingValue = 0;
                    DriftSettings.DriftSettingValue = 0;
                    FlagSettings.Flags = 0;
                    PathFindOptions.PathFindOptionValue = 0;
                    MaxSearchYOffset.MaxSearchYOffsetValue = 0;
                }

                public override string ToString()
                {
                    return "EnemyRoute Point " + ID;
                }
            }

            public HPNEValue(int InputID)
            {
                GroupID = InputID;
                HPNENextGroups = new HPNE_NextGroups();
                HPNEPreviewGroups = new HPNE_PreviewGroups();
                UnknwonData1 = 0;
                UnknownData2 = 0;

                TPNEValueList = new List<TPNEValue>();
            }

            public HPNEValue(HPNE.HPNEValue HPNEValue, TPNE TPNE, int InputID)
            {
                GroupID = InputID;
                HPNEPreviewGroups = new HPNE_PreviewGroups(HPNEValue.HPNE_PreviewGroup);
                HPNENextGroups = new HPNE_NextGroups(HPNEValue.HPNE_NextGroup);
                UnknwonData1 = HPNEValue.UnknownShortData1;
                UnknownData2 = HPNEValue.UnknownShortData2;

                for (int i = 0; i < HPNEValue.HPNE_Length; i++)
                {
                    TPNEValueList.Add(new TPNEValue(TPNE.TPNEValue_List[i + HPNEValue.HPNE_StartPoint], InputID, i));
                }
            }

            public HPNEValue(KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute.EnemyRoute_Group EnemyRoute_Group, int InputID)
            {
                GroupID = InputID;
                HPNEPreviewGroups = new HPNE_PreviewGroups(EnemyRoute_Group.PreviousGroups);
                HPNENextGroups = new HPNE_NextGroups(EnemyRoute_Group.NextGroups);
                UnknwonData1 = EnemyRoute_Group.Unknown1;
                UnknownData2 = EnemyRoute_Group.Unknown2;

                for (int i = 0; i < EnemyRoute_Group.Points.Count; i++)
                {
                    TPNEValueList.Add(new TPNEValue(EnemyRoute_Group.Points[i], InputID, i));
                }
            }

            public HPNEValue(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData GroupData, int InputID)
            {
                GroupID = InputID;
                HPNEPreviewGroups = new HPNE_PreviewGroups();
                HPNENextGroups = new HPNE_NextGroups();
                UnknwonData1 = 0;
                UnknownData2 = 0;

                for (int i = 0; i < GroupData.Points.Count; i++)
                {
                    TPNEValueList.Add(new TPNEValue(GroupData.Points[i], InputID, i));
                }
            }

            public override string ToString()
            {
                return "Enemy Route " + GroupID;
            }
        }

        public EnemyRoute_PGS(HPNE HPNE, TPNE TPNE)
        {
            for (int i = 0; i < HPNE.NumOfEntries; i++)
            {
                HPNEValueList.Add(new HPNEValue(HPNE.HPNEValue_List[i], TPNE, i));
            }
        }

        public EnemyRoute_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute EnemyRoute)
        {
            for (int i = 0; i < EnemyRoute.Groups.Count; i++)
            {
                HPNEValueList.Add(new HPNEValue(EnemyRoute.Groups[i], i));
            }
        }

        public EnemyRoute_PGS(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute XXXXRoute)
        {
            for (int i = 0; i < XXXXRoute.Groups.Count; i++)
            {
                HPNEValueList.Add(new HPNEValue(XXXXRoute.Groups[i], i));
            }
        }

        public EnemyRoute_PGS()
        {
            HPNEValueList = new List<HPNEValue>();
        }

        public HPNE_TPNEData ToHPNE_TPNEData()
        {
            HPNE_TPNEData HPNE_TPNE_Data = null;

            if (HPNEValueList.Count != 0)
            {
                List<TPNE.TPNEValue> TPNE_Values_List = new List<TPNE.TPNEValue>();
                List<HPNE.HPNEValue> HPNE_Values_List = new List<HPNE.HPNEValue>();

                int StartPoint = 0;
                for (int HPNECount = 0; HPNECount < HPNEValueList.Count; HPNECount++)
                {
                    HPNE.HPNEValue HPNE_Values = new HPNE.HPNEValue
                    {
                        HPNE_StartPoint = Convert.ToUInt16(StartPoint),
                        HPNE_Length = Convert.ToUInt16(HPNEValueList[HPNECount].TPNEValueList.Count),
                        HPNE_PreviewGroup = new HPNE.HPNEValue.HPNE_PreviewGroups(HPNEValueList[HPNECount].HPNEPreviewGroups.GetPrevGroupValueArray()),
                        HPNE_NextGroup = new HPNE.HPNEValue.HPNE_NextGroups(HPNEValueList[HPNECount].HPNENextGroups.GetNextGroupValueArray()),
                        UnknownShortData1 = HPNEValueList[HPNECount].UnknwonData1,
                        UnknownShortData2 = HPNEValueList[HPNECount].UnknownData2
                    };
                    HPNE_Values_List.Add(HPNE_Values);

                    for (int TPNECount = 0; TPNECount < HPNEValueList[HPNECount].TPNEValueList.Count; TPNECount++)
                    {
                        TPNE.TPNEValue TPNE_Values = new TPNE.TPNEValue
                        {
                            TPNE_Position = HPNEValueList[HPNECount].TPNEValueList[TPNECount].Positions.GetVector3D(),
                            Control = HPNEValueList[HPNECount].TPNEValueList[TPNECount].Control,
                            MushSettingValue = HPNEValueList[HPNECount].TPNEValueList[TPNECount].MushSettings.MushSettingValue,
                            DriftSettingValue = HPNEValueList[HPNECount].TPNEValueList[TPNECount].DriftSettings.DriftSettingValue,
                            Flags = HPNEValueList[HPNECount].TPNEValueList[TPNECount].FlagSettings.Flags,
                            PathFindOptionValue = HPNEValueList[HPNECount].TPNEValueList[TPNECount].PathFindOptions.PathFindOptionValue,
                            MaxSearchYOffsetValue = HPNEValueList[HPNECount].TPNEValueList[TPNECount].MaxSearchYOffset.MaxSearchYOffsetValue
                        };

                        TPNE_Values_List.Add(TPNE_Values);

                        StartPoint++;
                    }
                }

                TPNE TPNE = new TPNE(TPNE_Values_List);
                HPNE HPNE = new HPNE(HPNE_Values_List);

                HPNE_TPNE_Data = new HPNE_TPNEData(HPNE, TPNE);
            }
            if (HPNEValueList.Count == 0)
            {
                TPNE TPNE = new TPNE(new List<TPNE.TPNEValue>());
                HPNE HPNE = new HPNE(new List<HPNE.HPNEValue>());

                HPNE_TPNE_Data = new HPNE_TPNEData(HPNE, TPNE);
            }

            return HPNE_TPNE_Data;
        }
    }
}
