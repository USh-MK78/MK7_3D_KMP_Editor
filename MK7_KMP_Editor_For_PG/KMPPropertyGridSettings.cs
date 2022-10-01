using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Media.Media3D;
using System.Numerics;

namespace MK7_KMP_Editor_For_PG_
{
    public class KMPPropertyGridSettings
    {
        public TPTK_Section TPTKSection { get; set; }
        public class TPTK_Section
        {
            public List<TPTKValue> TPTKValue_List = new List<TPTKValue>();
            public List<TPTKValue> TPTKValueList { get => TPTKValue_List; set => TPTKValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class TPTKValue
            {
                [ReadOnly(true)]
                public int ID { get; set; }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Position Position_Value { get; set; } = new Position();
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

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Rotation Rotate_Value { get; set; } = new Rotation();
                public class Rotation
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

                    public Rotation()
                    {
                        _X = 0;
                        _Y = 0;
                        _Z = 0;
                    }

                    public Rotation(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
                    }

                    public Rotation(Vector3D vector3D)
                    {
                        _X = HTK_3DES.TSRSystem.RadianToAngle(vector3D.X);
                        _Y = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Y);
                        _Z = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Z);
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
                        return "Rotate";
                    }
                }

                public ushort Player_Index { get; set; }
                public ushort TPTK_UnkBytes { get; set; }

                public TPTKValue(Vector3D Pos, int InputID)
                {
                    ID = InputID;
                    Position_Value = new Position(Pos);
                    Rotate_Value = new Rotation();
                    Player_Index = 0;
                    TPTK_UnkBytes = 0;
                }

                public TPTKValue(KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue tPTKValue, int InputID)
                {
                    ID = InputID;
                    Position_Value = new Position(tPTKValue.TPTK_Position);
                    Rotate_Value = new Rotation(tPTKValue.TPTK_Rotation);
                    Player_Index = tPTKValue.Player_Index;
                    TPTK_UnkBytes = tPTKValue.TPTK_UnkBytes;
                }

                public TPTKValue(TestXml.KMPXml.StartPosition.StartPosition_Value startPosition_Value, int InputID)
                {
                    ID = InputID;
                    Position_Value = new Position(startPosition_Value.Position.ToVector3D());
                    Rotate_Value = new Rotation(startPosition_Value.Rotation.ToVector3D());
                    Player_Index = startPosition_Value.Player_Index;
                    TPTK_UnkBytes = startPosition_Value.TPTK_UnkBytes;
                }

                public override string ToString()
                {
                    return "Kart Point " + ID;
                }
            }

            public TPTK_Section()
            {
                TPTKValueList = new List<TPTKValue>();
            }

            public TPTK_Section(KMPs.KMPFormat.KMPSection.TPTK_Section tPTK_Section)
            {
                for (int i = 0; i < tPTK_Section.NumOfEntries; i++) TPTKValueList.Add(new TPTKValue(tPTK_Section.TPTKValue_List[i], i));
            }

            public TPTK_Section(TestXml.KMPXml.StartPosition startPosition)
            {
                for (int i = 0; i < startPosition.StartPositionValues.Count; i++) TPTKValueList.Add(new TPTKValue(startPosition.StartPositionValues[i], i));
            }
        }

        public HPNE_TPNE_Section HPNE_TPNESection { get; set; }
        public class HPNE_TPNE_Section
        {
            public List<HPNEValue> HPNEValue_List = new List<HPNEValue>();
            public List<HPNEValue> HPNEValueList { get => HPNEValue_List; set => HPNEValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class HPNEValue
            {
                [ReadOnly(true)]
                public int GroupID { get; set; }

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

                    public HPNE_PreviewGroups(KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue.HPNE_PreviewGroups HPNE_PreviewGroup)
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

                    public HPNE_PreviewGroups(TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.ER_PreviousGroup previous)
                    {
                        Prev0 = previous.Prev0;
                        Prev1 = previous.Prev1;
                        Prev2 = previous.Prev2;
                        Prev3 = previous.Prev3;
                        Prev4 = previous.Prev4;
                        Prev5 = previous.Prev5;
                        Prev6 = previous.Prev6;
                        Prev7 = previous.Prev7;
                        Prev8 = previous.Prev8;
                        Prev9 = previous.Prev9;
                        Prev10 = previous.Prev10;
                        Prev11 = previous.Prev11;
                        Prev12 = previous.Prev12;
                        Prev13 = previous.Prev13;
                        Prev14 = previous.Prev14;
                        Prev15 = previous.Prev15;
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

                    public HPNE_NextGroups(KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue.HPNE_NextGroups HPNE_NextGroup)
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

                    public HPNE_NextGroups(TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.ER_NextGroup next)
                    {
                        Next0 = next.Next0;
                        Next1 = next.Next1;
                        Next2 = next.Next2;
                        Next3 = next.Next3;
                        Next4 = next.Next4;
                        Next5 = next.Next5;
                        Next6 = next.Next6;
                        Next7 = next.Next7;
                        Next8 = next.Next8;
                        Next9 = next.Next9;
                        Next10 = next.Next10;
                        Next11 = next.Next11;
                        Next12 = next.Next12;
                        Next13 = next.Next13;
                        Next14 = next.Next14;
                        Next15 = next.Next15;
                    }

                    public override string ToString()
                    {
                        return "Next";
                    }
                }

                public uint HPNE_UnkBytes1 { get; set; }

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
                        public KMPs.KMPHelper.KMPValueTypeConverter.EnemyRoute.MushSetting MushSettingEnum
                        {
                            get { return KMPs.KMPHelper.KMPValueTypeConverter.EnemyRoute.MushSettingType(MushSettingValue); }
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
                        public KMPs.KMPHelper.KMPValueTypeConverter.EnemyRoute.DriftSetting DriftSettingEnum
                        {
                            get { return KMPs.KMPHelper.KMPValueTypeConverter.EnemyRoute.DriftSettingType(DriftSettingValue); }
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
                        public KMPs.KMPHelper.KMPValueTypeConverter.EnemyRoute.PathFindOption PathFindOptionEnum
                        {
                            get { return KMPs.KMPHelper.KMPValueTypeConverter.EnemyRoute.PathFindOptionType(PathFindOptionValue); }
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
                        public KMPs.KMPHelper.KMPValueTypeConverter.EnemyRoute.MaxSearchYOffsetOption MaxSearchYOffsetOptionEnum
                        {
                            get { return KMPs.KMPHelper.KMPValueTypeConverter.EnemyRoute.MaxSearchYOffsetOptionType(MaxSearchYOffsetValue); }
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

                    public TPNEValue(KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue TPNEValue, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        Positions = new Position(TPNEValue.TPNE_Position);
                        Control = TPNEValue.Control;
                        MushSettings.MushSettingValue = TPNEValue.MushSetting;
                        DriftSettings.DriftSettingValue = TPNEValue.DriftSetting;
                        FlagSettings.Flags = TPNEValue.Flags;
                        PathFindOptions.PathFindOptionValue = TPNEValue.PathFindOption;
                        MaxSearchYOffset.MaxSearchYOffsetValue = TPNEValue.MaxSearchYOffset;
                    }

                    public TPNEValue(TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point enemyRoute_Point, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        Positions = new Position(enemyRoute_Point.Position.ToVector3D());
                        Control = enemyRoute_Point.Control;
                        MushSettings.MushSettingValue = enemyRoute_Point.MushSetting;
                        DriftSettings.DriftSettingValue = enemyRoute_Point.DriftSetting;
                        FlagSettings.Flags = enemyRoute_Point.Flags;
                        PathFindOptions.PathFindOptionValue = enemyRoute_Point.PathFindOption;
                        MaxSearchYOffset.MaxSearchYOffsetValue = enemyRoute_Point.MaxSearchYOffset;
                    }

                    public TPNEValue(TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        Positions = new Position(pointData.Position.ToVector3D());
                        Control = pointData.ScaleValue;
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
                    HPNE_UnkBytes1 = 65535;
                    TPNEValueList = new List<TPNEValue>();
                }

                public HPNEValue(KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue HPNEValue, KMPs.KMPFormat.KMPSection.TPNE_Section TPNE, int InputID)
                {
                    GroupID = InputID;
                    HPNEPreviewGroups = new HPNE_PreviewGroups(HPNEValue.HPNE_PreviewGroup);
                    HPNENextGroups = new HPNE_NextGroups(HPNEValue.HPNE_NextGroup);
                    HPNE_UnkBytes1 = HPNEValue.HPNE_UnkBytes1;
                    
                    for (int i = 0; i < HPNEValue.HPNE_Length; i++)
                    {
                        TPNEValueList.Add(new TPNEValue(TPNE.TPNEValue_List[i + HPNEValue.HPNE_StartPoint], InputID, i));
                    }
                }

                public HPNEValue(TestXml.KMPXml.EnemyRoute.EnemyRoute_Group enemyRoute_Group, int InputID)
                {
                    GroupID = InputID;
                    HPNEPreviewGroups = new HPNE_PreviewGroups(enemyRoute_Group.PreviousGroups);
                    HPNENextGroups = new HPNE_NextGroups(enemyRoute_Group.NextGroups);
                    HPNE_UnkBytes1 = enemyRoute_Group.Unknown1;

                    for (int i = 0; i < enemyRoute_Group.Points.Count; i++)
                    {
                        TPNEValueList.Add(new TPNEValue(enemyRoute_Group.Points[i], InputID, i));
                    }
                }

                public HPNEValue(TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData, int InputID)
                {
                    GroupID = InputID;
                    HPNEPreviewGroups = new HPNE_PreviewGroups();
                    HPNENextGroups = new HPNE_NextGroups();
                    HPNE_UnkBytes1 = 65535;

                    for (int i = 0; i < groupData.Points.Count; i++)
                    {
                        TPNEValueList.Add(new TPNEValue(groupData.Points[i], InputID, i));
                    }
                }

                public override string ToString()
                {
                    return "Enemy Route " + GroupID;
                }
            }

            public HPNE_TPNE_Section()
            {
                HPNEValueList = new List<HPNEValue>();
            }

            public HPNE_TPNE_Section(KMPs.KMPFormat.KMPSection.HPNE_Section HPNE, KMPs.KMPFormat.KMPSection.TPNE_Section TPNE)
            {
                for (int i = 0; i < HPNE.NumOfEntries; i++)
                {
                    HPNEValueList.Add(new HPNEValue(HPNE.HPNEValue_List[i], TPNE, i));
                }
            }

            public HPNE_TPNE_Section(TestXml.KMPXml.EnemyRoute enemyRoute)
            {
                for (int i = 0; i < enemyRoute.Groups.Count; i++)
                {
                    HPNEValueList.Add(new HPNEValue(enemyRoute.Groups[i], i));
                }
            }

            public HPNE_TPNE_Section(TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
            {
                for (int i = 0; i < xXXXRoute.Groups.Count; i++)
                {
                    HPNEValueList.Add(new HPNEValue(xXXXRoute.Groups[i], i));
                }
            }
        }

        public HPTI_TPTI_Section HPTI_TPTISection { get; set; }
        public class HPTI_TPTI_Section
        {
            public List<HPTIValue> HPTIValue_List = new List<HPTIValue>();
            public List<HPTIValue> HPTIValueList { get => HPTIValue_List; set => HPTIValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class HPTIValue
            {
                [ReadOnly(true)]
                public int GroupID { get; set; }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public HPTI_PreviewGroups HPTI_PreviewGroup { get; set; } = new HPTI_PreviewGroups();
                public class HPTI_PreviewGroups
                {
                    public ushort Prev0 { get; set; }
                    public ushort Prev1 { get; set; }
                    public ushort Prev2 { get; set; }
                    public ushort Prev3 { get; set; }
                    public ushort Prev4 { get; set; }
                    public ushort Prev5 { get; set; }

                    public HPTI_PreviewGroups()
                    {
                        Prev0 = 65535;
                        Prev1 = 65535;
                        Prev2 = 65535;
                        Prev3 = 65535;
                        Prev4 = 65535;
                        Prev5 = 65535;
                    }

                    public HPTI_PreviewGroups(KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue.HPTI_PreviewGroups HPTI_PreviewGroup)
                    {
                        Prev0 = HPTI_PreviewGroup.Prev0;
                        Prev1 = HPTI_PreviewGroup.Prev1;
                        Prev2 = HPTI_PreviewGroup.Prev2;
                        Prev3 = HPTI_PreviewGroup.Prev3;
                        Prev4 = HPTI_PreviewGroup.Prev4;
                        Prev5 = HPTI_PreviewGroup.Prev5;
                    }

                    public HPTI_PreviewGroups(TestXml.KMPXml.ItemRoute.ItemRoute_Group.IR_PreviousGroup previous)
                    {
                        Prev0 = previous.Prev0;
                        Prev1 = previous.Prev1;
                        Prev2 = previous.Prev2;
                        Prev3 = previous.Prev3;
                        Prev4 = previous.Prev4;
                        Prev5 = previous.Prev5;
                    }

                    public override string ToString()
                    {
                        return "Preview";
                    }
                }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public HPTI_NextGroups HPTI_NextGroup { get; set; } = new HPTI_NextGroups();
                public class HPTI_NextGroups
                {
                    public ushort Next0 { get; set; }
                    public ushort Next1 { get; set; }
                    public ushort Next2 { get; set; }
                    public ushort Next3 { get; set; }
                    public ushort Next4 { get; set; }
                    public ushort Next5 { get; set; }

                    public HPTI_NextGroups()
                    {
                        Next0 = 65535;
                        Next1 = 65535;
                        Next2 = 65535;
                        Next3 = 65535;
                        Next4 = 65535;
                        Next5 = 65535;
                    }

                    public HPTI_NextGroups(KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue.HPTI_NextGroups HPTI_NextGroup)
                    {
                        Next0 = HPTI_NextGroup.Next0;
                        Next1 = HPTI_NextGroup.Next1;
                        Next2 = HPTI_NextGroup.Next2;
                        Next3 = HPTI_NextGroup.Next3;
                        Next4 = HPTI_NextGroup.Next4;
                        Next5 = HPTI_NextGroup.Next5;
                    }

                    public HPTI_NextGroups(TestXml.KMPXml.ItemRoute.ItemRoute_Group.IR_NextGroup next)
                    {
                        Next0 = next.Next0;
                        Next1 = next.Next1;
                        Next2 = next.Next2;
                        Next3 = next.Next3;
                        Next4 = next.Next4;
                        Next5 = next.Next5;
                    }

                    public override string ToString()
                    {
                        return "Next";
                    }
                }

                public List<TPTIValue> TPTIValue_List = new List<TPTIValue>();
                [Browsable(false)]
                public List<TPTIValue> TPTIValueList { get => TPTIValue_List; set => TPTIValue_List = value; }
                [TypeConverter(typeof(CustomSortTypeConverter))]
                public class TPTIValue
                {
                    [ReadOnly(true)]
                    public int Group_ID { get; set; }

                    [ReadOnly(true)]
                    public int ID { get; set; }

                    [TypeConverter(typeof(ExpandableObjectConverter))]
                    public TPTI_Position TPTI_Positions { get; set; } = new TPTI_Position();
                    public class TPTI_Position
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

                        public TPTI_Position()
                        {
                            _X = 0;
                            _Y = 0;
                            _Z = 0;
                        }

                        public TPTI_Position(float X, float Y, float Z)
                        {
                            _X = X;
                            _Y = Y;
                            _Z = Z;
                        }

                        public TPTI_Position(Vector3D vector3D)
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

                    public float TPTI_PointSize { get; set; }

                    [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                    public GravityModeSetting GravityModeSettings { get; set; } = new GravityModeSetting();
                    public class GravityModeSetting
                    {
                        [ReadOnly(true)]
                        public KMPs.KMPHelper.KMPValueTypeConverter.ItemRoute.GravityMode GravityModeEnum
                        {
                            get { return KMPs.KMPHelper.KMPValueTypeConverter.ItemRoute.GravityModeType(GravityModeValue); }
                        }

                        public ushort GravityModeValue { get; set; }

                        public override string ToString()
                        {
                            return "Gravity Mode";
                        }
                    }

                    [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                    public PlayerScanRadiusSetting PlayerScanRadiusSettings { get; set; } = new PlayerScanRadiusSetting();
                    public class PlayerScanRadiusSetting
                    {
                        [ReadOnly(true)]
                        public KMPs.KMPHelper.KMPValueTypeConverter.ItemRoute.PlayerScanRadius PlayerScanRadiusEnum
                        {
                            get { return KMPs.KMPHelper.KMPValueTypeConverter.ItemRoute.PlayerScanRadiusType(PlayerScanRadiusValue); }
                        }

                        public ushort PlayerScanRadiusValue { get; set; }

                        public override string ToString()
                        {
                            return "PlayerScanRadius";
                        }
                    }

                    public TPTIValue(Vector3D Pos, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        TPTI_Positions = new TPTI_Position(Pos);
                        TPTI_PointSize = 1;
                        GravityModeSettings.GravityModeValue = 0;
                        PlayerScanRadiusSettings.PlayerScanRadiusValue = 0;
                    }

                    public TPTIValue(KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue TPTIValue, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        TPTI_Positions = new TPTI_Position(TPTIValue.TPTI_Position);
                        TPTI_PointSize = TPTIValue.TPTI_PointSize;
                        GravityModeSettings.GravityModeValue = TPTIValue.GravityMode;
                        PlayerScanRadiusSettings.PlayerScanRadiusValue = TPTIValue.PlayerScanRadius;
                    }

                    public TPTIValue(TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point itemRoute_Point, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        TPTI_Positions = new TPTI_Position(itemRoute_Point.Position.ToVector3D());
                        TPTI_PointSize = itemRoute_Point.PointSize;
                        GravityModeSettings.GravityModeValue = itemRoute_Point.GravityMode;
                        PlayerScanRadiusSettings.PlayerScanRadiusValue = itemRoute_Point.PlayerScanRadius;
                    }

                    public TPTIValue(TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        TPTI_Positions = new TPTI_Position(pointData.Position.ToVector3D());
                        TPTI_PointSize = pointData.ScaleValue;
                        GravityModeSettings.GravityModeValue = 0;
                        PlayerScanRadiusSettings.PlayerScanRadiusValue = 0;
                    }

                    public override string ToString()
                    {
                        return "ItemRoute Point " + ID;
                    }
                }

                public HPTIValue(int InputID)
                {
                    GroupID = InputID;
                    HPTI_PreviewGroup = new HPTI_PreviewGroups();
                    HPTI_NextGroup = new HPTI_NextGroups();
                    TPTIValueList = new List<TPTIValue>();
                }

                public HPTIValue(KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue HPTIValue, KMPs.KMPFormat.KMPSection.TPTI_Section TPTI, int InputID)
                {
                    GroupID = InputID;
                    HPTI_PreviewGroup = new HPTI_PreviewGroups(HPTIValue.HPTI_PreviewGroup);
                    HPTI_NextGroup = new HPTI_NextGroups(HPTIValue.HPTI_NextGroup);

                    for (int i = 0; i < HPTIValue.HPTI_Length; i++)
                    {
                        TPTIValueList.Add(new TPTIValue(TPTI.TPTIValue_List[i + HPTIValue.HPTI_StartPoint], InputID, i));
                    }
                }

                public HPTIValue(TestXml.KMPXml.ItemRoute.ItemRoute_Group itemRoute_Group, int InputID)
                {
                    GroupID = InputID;
                    HPTI_PreviewGroup = new HPTI_PreviewGroups(itemRoute_Group.PreviousGroups);
                    HPTI_NextGroup = new HPTI_NextGroups(itemRoute_Group.NextGroups);

                    for (int i = 0; i < itemRoute_Group.Points.Count; i++)
                    {
                        TPTIValueList.Add(new TPTIValue(itemRoute_Group.Points[i], InputID, i));
                    }
                }

                public HPTIValue(TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData, int InputID)
                {
                    GroupID = InputID;
                    HPTI_PreviewGroup = new HPTI_PreviewGroups();
                    HPTI_NextGroup = new HPTI_NextGroups();

                    for (int i = 0; i < groupData.Points.Count; i++)
                    {
                        TPTIValueList.Add(new TPTIValue(groupData.Points[i], InputID, i));
                    }
                }

                public override string ToString()
                {
                    return "ItemRoute " + GroupID;
                }
            }

            public HPTI_TPTI_Section()
            {
                HPTIValueList = new List<HPTIValue>();
            }

            public HPTI_TPTI_Section(KMPs.KMPFormat.KMPSection.HPTI_Section HPTI, KMPs.KMPFormat.KMPSection.TPTI_Section TPTI)
            {
                for (int i = 0; i < HPTI.NumOfEntries; i++)
                {
                    HPTIValueList.Add(new HPTIValue(HPTI.HPTIValue_List[i], TPTI, i));
                }
            }

            public HPTI_TPTI_Section(TestXml.KMPXml.ItemRoute itemRoute)
            {
                for (int i = 0; i < itemRoute.Groups.Count; i++)
                {
                    HPTIValueList.Add(new HPTIValue(itemRoute.Groups[i], i));
                }
            }

            public HPTI_TPTI_Section(TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
            {
                for (int i = 0; i < xXXXRoute.Groups.Count; i++)
                {
                    HPTIValueList.Add(new HPTIValue(xXXXRoute.Groups[i], i));
                }
            }
        }

        public HPKC_TPKC_Section HPKC_TPKCSection { get; set; }
        public class HPKC_TPKC_Section
        {
            public List<HPKCValue> HPKCValue_List = new List<HPKCValue>();
            public List<HPKCValue> HPKCValueList { get => HPKCValue_List; set => HPKCValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class HPKCValue
            {
                [ReadOnly(true)]
                public int GroupID { get; set; }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public HPKC_PreviewGroups HPKC_PreviewGroup { get; set; } = new HPKC_PreviewGroups();
                public class HPKC_PreviewGroups
                {
                    public byte Prev0 { get; set; }
                    public byte Prev1 { get; set; }
                    public byte Prev2 { get; set; }
                    public byte Prev3 { get; set; }
                    public byte Prev4 { get; set; }
                    public byte Prev5 { get; set; }

                    public HPKC_PreviewGroups()
                    {
                        Prev0 = 255;
                        Prev1 = 255;
                        Prev2 = 255;
                        Prev3 = 255;
                        Prev4 = 255;
                        Prev5 = 255;
                    }

                    public HPKC_PreviewGroups(KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue.HPKC_PreviewGroups HPKC_PreviewGroup)
                    {
                        Prev0 = HPKC_PreviewGroup.Prev0;
                        Prev1 = HPKC_PreviewGroup.Prev1;
                        Prev2 = HPKC_PreviewGroup.Prev2;
                        Prev3 = HPKC_PreviewGroup.Prev3;
                        Prev4 = HPKC_PreviewGroup.Prev4;
                        Prev5 = HPKC_PreviewGroup.Prev5;
                    }

                    public HPKC_PreviewGroups(TestXml.KMPXml.Checkpoint.Checkpoint_Group.CP_PreviousGroup previous)
                    {
                        Prev0 = previous.Prev0;
                        Prev1 = previous.Prev1;
                        Prev2 = previous.Prev2;
                        Prev3 = previous.Prev3;
                        Prev4 = previous.Prev4;
                        Prev5 = previous.Prev5;
                    }

                    public override string ToString()
                    {
                        return "Preview";
                    }
                }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public HPKC_NextGroups HPKC_NextGroup { get; set; } = new HPKC_NextGroups();
                public class HPKC_NextGroups
                {
                    public byte Next0 { get; set; }
                    public byte Next1 { get; set; }
                    public byte Next2 { get; set; }
                    public byte Next3 { get; set; }
                    public byte Next4 { get; set; }
                    public byte Next5 { get; set; }

                    public HPKC_NextGroups()
                    {
                        Next0 = 255;
                        Next1 = 255;
                        Next2 = 255;
                        Next3 = 255;
                        Next4 = 255;
                        Next5 = 255;
                    }

                    public HPKC_NextGroups(KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue.HPKC_NextGroups HPKC_NextGroup)
                    {
                        Next0 = HPKC_NextGroup.Next0;
                        Next1 = HPKC_NextGroup.Next1;
                        Next2 = HPKC_NextGroup.Next2;
                        Next3 = HPKC_NextGroup.Next3;
                        Next4 = HPKC_NextGroup.Next4;
                        Next5 = HPKC_NextGroup.Next5;
                    }

                    public HPKC_NextGroups(TestXml.KMPXml.Checkpoint.Checkpoint_Group.CP_NextGroup next)
                    {
                        Next0 = next.Next0;
                        Next1 = next.Next1;
                        Next2 = next.Next2;
                        Next3 = next.Next3;
                        Next4 = next.Next4;
                        Next5 = next.Next5;
                    }

                    public override string ToString()
                    {
                        return "Next";
                    }
                }

                public ushort HPKC_UnkBytes1 { get; set; }

                public List<TPKCValue> TPKCValue_List = new List<TPKCValue>();
                [Browsable(false)]
                public List<TPKCValue> TPKCValueList { get => TPKCValue_List; set => TPKCValue_List = value; }
                [TypeConverter(typeof(CustomSortTypeConverter))]
                public class TPKCValue
                {
                    [ReadOnly(true)]
                    public int Group_ID { get; set; }

                    [ReadOnly(true)]
                    public int ID { get; set; }

                    [TypeConverter(typeof(ExpandableObjectConverter))]
                    public Position2D_Left Position_2D_Left { get; set; } = new Position2D_Left();
                    public class Position2D_Left
                    {
                        public float X { get; set; }
                        public float Y { get; set; }

                        public Position2D_Left()
                        {
                            X = 0;
                            Y = 0;
                        }

                        public Position2D_Left(float Left_X, float Left_Y)
                        {
                            X = Left_X;
                            Y = Left_Y;
                        }

                        public Position2D_Left(Vector2 vector2)
                        {
                            X = (float)vector2.X;
                            Y = (float)vector2.Y;
                        }

                        public Vector2 GetVector2()
                        {
                            return new Vector2(this.X, this.Y);
                        }

                        public override string ToString()
                        {
                            return "Position2D Left";
                        }
                    }

                    [TypeConverter(typeof(ExpandableObjectConverter))]
                    public Position2D_Right Position_2D_Right { get; set; } = new Position2D_Right();
                    public class Position2D_Right
                    {
                        public float X { get; set; }
                        public float Y { get; set; }

                        public Position2D_Right()
                        {
                            X = 0;
                            Y = 0;
                        }

                        public Position2D_Right(float Right_X, float Right_Y)
                        {
                            X = Right_X;
                            Y = Right_Y;
                        }

                        public Position2D_Right(Vector2 vector2)
                        {
                            X = (float)vector2.X;
                            Y = (float)vector2.Y;
                        }

                        public Vector2 GetVector2()
                        {
                            return new Vector2(this.X, this.Y);
                        }

                        public override string ToString()
                        {
                            return "Position2D Right";
                        }
                    }

                    public byte TPKC_RespawnID { get; set; }
                    public byte TPKC_Checkpoint_Type { get; set; }
                    public byte TPKC_PreviousCheckPoint { get; set; }
                    public byte TPKC_NextCheckPoint { get; set; }
                    public byte TPKC_ClipID { get; set; }
                    public byte TPKC_Section { get; set; }
                    public byte TPKC_UnkBytes3 { get; set; }
                    public byte TPKC_UnkBytes4 { get; set; }

                    public TPKCValue(Vector2 LeftPos, Vector2 RightPos, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        Position_2D_Left = new Position2D_Left(LeftPos);
                        Position_2D_Right = new Position2D_Right(RightPos);
                        TPKC_RespawnID = 0xFF;
                        TPKC_Checkpoint_Type = 0;
                        TPKC_NextCheckPoint = 0xFF;
                        TPKC_PreviousCheckPoint = 0xFF;
                        TPKC_ClipID = 255;
                        TPKC_Section = 0;
                        TPKC_UnkBytes3 = 0;
                        TPKC_UnkBytes4 = 0;
                    }

                    public TPKCValue(KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue TPKCValue, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        Position_2D_Left = new Position2D_Left(TPKCValue.TPKC_2DPosition_Left);
                        Position_2D_Right = new Position2D_Right(TPKCValue.TPKC_2DPosition_Right);
                        TPKC_RespawnID = TPKCValue.TPKC_RespawnID;
                        TPKC_Checkpoint_Type = TPKCValue.TPKC_Checkpoint_Type;
                        TPKC_NextCheckPoint = TPKCValue.TPKC_NextCheckPoint;
                        TPKC_PreviousCheckPoint = TPKCValue.TPKC_PreviousCheckPoint;
                        TPKC_ClipID = TPKCValue.TPKC_ClipID;
                        TPKC_Section = TPKCValue.TPKC_Section;
                        TPKC_UnkBytes3 = TPKCValue.TPKC_UnkBytes3;
                        TPKC_UnkBytes4 = TPKCValue.TPKC_UnkBytes4;
                    }

                    public TPKCValue(TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point checkpoint_Point, int GroupID, int InputID)
                    {
                        Group_ID = GroupID;
                        ID = InputID;
                        Position_2D_Left = new Position2D_Left(checkpoint_Point.Position_2D_Left.ToVector2());
                        Position_2D_Right = new Position2D_Right(checkpoint_Point.Position_2D_Right.ToVector2());
                        TPKC_RespawnID = checkpoint_Point.RespawnID;
                        TPKC_Checkpoint_Type = checkpoint_Point.Checkpoint_Type;
                        TPKC_NextCheckPoint = checkpoint_Point.NextCheckPoint;
                        TPKC_PreviousCheckPoint = checkpoint_Point.PreviousCheckPoint;
                        TPKC_ClipID = checkpoint_Point.ClipID;
                        TPKC_Section = checkpoint_Point.Section;
                        TPKC_UnkBytes3 = checkpoint_Point.UnkBytes3;
                        TPKC_UnkBytes4 = checkpoint_Point.UnkBytes4;
                    }

                    public override string ToString()
                    {
                        return "CheckPoint Point " + ID;
                    }
                }

                public HPKCValue(int InputID)
                {
                    GroupID = InputID;
                    HPKC_PreviewGroup = new HPKC_PreviewGroups();
                    HPKC_NextGroup = new HPKC_NextGroups();
                    HPKC_UnkBytes1 = 0;
                    TPKCValueList = new List<TPKCValue>();
                }

                public HPKCValue(KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue HPKCValue, KMPs.KMPFormat.KMPSection.TPKC_Section TPKC, int InputID)
                {
                    GroupID = InputID;
                    HPKC_PreviewGroup = new HPKC_PreviewGroups(HPKCValue.HPKC_PreviewGroup);
                    HPKC_NextGroup = new HPKC_NextGroups(HPKCValue.HPKC_NextGroup);
                    HPKC_UnkBytes1 = HPKCValue.HPKC_UnkBytes1;

                    for (int i = 0; i < HPKCValue.HPKC_Length; i++)
                    {
                        TPKCValueList.Add(new TPKCValue(TPKC.TPKCValue_List[i + HPKCValue.HPKC_StartPoint], InputID, i));
                    }
                }

                public HPKCValue(TestXml.KMPXml.Checkpoint.Checkpoint_Group checkpoint_Group, int InputID)
                {
                    GroupID = InputID;
                    HPKC_PreviewGroup = new HPKC_PreviewGroups(checkpoint_Group.PreviousGroups);
                    HPKC_NextGroup = new HPKC_NextGroups(checkpoint_Group.NextGroups);
                    HPKC_UnkBytes1 = checkpoint_Group.UnkBytes1;

                    for (int i = 0; i < checkpoint_Group.Points.Count; i++)
                    {
                        TPKCValueList.Add(new TPKCValue(checkpoint_Group.Points[i], InputID, i));
                    }
                }

                public override string ToString()
                {
                    return "CheckPoint " + GroupID;
                }
            }

            public HPKC_TPKC_Section()
            {
                HPKCValueList = new List<HPKCValue>();
            }

            public HPKC_TPKC_Section(KMPs.KMPFormat.KMPSection.HPKC_Section HPKC, KMPs.KMPFormat.KMPSection.TPKC_Section TPKC)
            {
                for (int i = 0; i < HPKC.NumOfEntries; i++)
                {
                    HPKCValueList.Add(new HPKCValue(HPKC.HPKCValue_List[i], TPKC, i));
                }
            }

            public HPKC_TPKC_Section(TestXml.KMPXml.Checkpoint checkpoint)
            {
                for (int i = 0; i < checkpoint.Groups.Count; i++)
                {
                    HPKCValueList.Add(new HPKCValue(checkpoint.Groups[i], i));
                }
            }
        }

        public JBOG_Section JBOGSection { get; set; }
        public class JBOG_Section
        {
            public List<JBOGValue> JBOGValue_List = new List<JBOGValue>();
            public List<JBOGValue> JBOGValueList { get => JBOGValue_List; set => JBOGValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class JBOGValue
            {
                [ReadOnly(true)]
                public int ID { get; set; }

                [ReadOnly(true)]
                public string ObjectName { get; set; }
                public string ObjectID { get; set; }
                public string JBOG_UnkByte1 { get; set; }

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

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Rotation Rotations { get; set; } = new Rotation();
                public class Rotation
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

                    public Rotation()
                    {
                        _X = 0;
                        _Y = 0;
                        _Z = 0;
                    }

                    public Rotation(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
                    }

                    public Rotation(Vector3D vector3D)
                    {
                        _X = HTK_3DES.TSRSystem.RadianToAngle(vector3D.X);
                        _Y = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Y);
                        _Z = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Z);
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
                        return "Rotation";
                    }
                }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Scale Scales { get; set; } = new Scale();
                public class Scale
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

                    public Scale()
                    {
                        _X = 1;
                        _Y = 1;
                        _Z = 1;
                    }

                    public Scale(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
                    }

                    public Scale(Vector3D vector3D)
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
                        return "Scale";
                    }
                }

                public ushort JBOG_ITOP_RouteIDIndex { get; set; }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public JBOG_SpecificSetting JOBJ_Specific_Setting { get; set; } = new JBOG_SpecificSetting();
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

                    public JBOG_SpecificSetting()
                    {
                        Value0 = 0;
                        Value1 = 0;
                        Value2 = 0;
                        Value3 = 0;
                        Value4 = 0;
                        Value5 = 0;
                        Value6 = 0;
                        Value7 = 0;
                    }

                    public JBOG_SpecificSetting(KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue.JBOG_SpecificSetting jBOG_SpecificSetting)
                    {
                        Value0 = jBOG_SpecificSetting.Value0;
                        Value1 = jBOG_SpecificSetting.Value1;
                        Value2 = jBOG_SpecificSetting.Value2;
                        Value3 = jBOG_SpecificSetting.Value3;
                        Value4 = jBOG_SpecificSetting.Value4;
                        Value5 = jBOG_SpecificSetting.Value5;
                        Value6 = jBOG_SpecificSetting.Value6;
                        Value7 = jBOG_SpecificSetting.Value7;
                    }

                    public JBOG_SpecificSetting(TestXml.KMPXml.Object.Object_Value.SpecificSettings SpecificSettings)
                    {
                        Value0 = SpecificSettings.Value0;
                        Value1 = SpecificSettings.Value1;
                        Value2 = SpecificSettings.Value2;
                        Value3 = SpecificSettings.Value3;
                        Value4 = SpecificSettings.Value4;
                        Value5 = SpecificSettings.Value5;
                        Value6 = SpecificSettings.Value6;
                        Value7 = SpecificSettings.Value7;
                    }

                    public override string ToString()
                    {
                        return "Obj Params";
                    }
                }
                public ushort JBOG_PresenceSetting { get; set; }
                public string JBOG_UnkByte2 { get; set; }
                public ushort JBOG_UnkByte3 { get; set; }

                public JBOGValue(string Name, string ObjID, Vector3D Pos,int InputID)
                {
                    ID = InputID;
                    ObjectName = Name;
                    ObjectID = ObjID;
                    JBOG_ITOP_RouteIDIndex = 65535;
                    JBOG_PresenceSetting = 7;
                    JBOG_UnkByte1 = "0000";
                    JBOG_UnkByte2 = "FFFF";
                    JBOG_UnkByte3 = 0;
                    Positions = new Position(Pos);
                    Scales = new Scale();
                    Rotations = new Rotation();
                    JOBJ_Specific_Setting = new JBOG_SpecificSetting();
                }

                public JBOGValue(KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue jBOGValue, List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDB, int InputID)
                {
                    string Name = ObjFlowDB.Find(x => x.ObjectID == BitConverter.ToString(jBOGValue.ObjectID.Reverse().ToArray()).Replace("-", string.Empty)).ObjectName;

                    ID = InputID;
                    ObjectName = Name;
                    ObjectID = BitConverter.ToString(jBOGValue.ObjectID.Reverse().ToArray()).Replace("-", string.Empty);
                    JBOG_ITOP_RouteIDIndex = jBOGValue.JBOG_ITOP_RouteIDIndex;
                    JBOG_PresenceSetting = jBOGValue.JBOG_PresenceSetting;
                    JBOG_UnkByte1 = BitConverter.ToString(jBOGValue.JBOG_UnkByte1.Reverse().ToArray()).Replace("-", string.Empty);
                    JBOG_UnkByte2 = BitConverter.ToString(jBOGValue.JBOG_UnkByte2.Reverse().ToArray()).Replace("-", string.Empty);
                    JBOG_UnkByte3 = jBOGValue.JBOG_UnkByte3;
                    Positions = new Position(jBOGValue.JBOG_Position);
                    Scales = new Scale(jBOGValue.JBOG_Scale);
                    Rotations = new Rotation(jBOGValue.JBOG_Rotation);
                    JOBJ_Specific_Setting = new JBOG_SpecificSetting(jBOGValue.GOBJ_Specific_Setting);
                }

                public JBOGValue(TestXml.KMPXml.Object.Object_Value object_Value, int InputID)
                {
                    List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDB_FindName = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml");
                    string Name = ObjFlowDB_FindName.Find(x => x.ObjectID == object_Value.ObjectID).ObjectName;

                    ID = InputID;
                    ObjectID = object_Value.ObjectID;
                    JBOG_ITOP_RouteIDIndex = object_Value.RouteIDIndex;
                    JBOG_PresenceSetting = object_Value.PresenceSetting;
                    JBOG_UnkByte1 = object_Value.UnkByte1;
                    JBOG_UnkByte2 = object_Value.UnkByte2;
                    JBOG_UnkByte3 = object_Value.UnkByte3;
                    Positions = new Position(object_Value.Position.ToVector3D());
                    Scales = new Scale(object_Value.Scale.ToVector3D());
                    Rotations = new Rotation(object_Value.Rotation.ToVector3D());
                    JOBJ_Specific_Setting = new JBOG_SpecificSetting(object_Value.SpecificSetting);
                }

                public override string ToString()
                {
                    return "Object " + ID + " [" + "OBJID : " + ObjectID + "]";
                }
            }

            public JBOG_Section()
            {
                JBOGValueList = new List<JBOGValue>();
            }

            public JBOG_Section(KMPs.KMPFormat.KMPSection.JBOG_Section jBOG_Section, List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDB)
            {
                for (int i = 0; i < jBOG_Section.NumOfEntries; i++) JBOGValueList.Add(new JBOGValue(jBOG_Section.JBOGValue_List[i], ObjFlowDB, i));
            }

            public JBOG_Section(TestXml.KMPXml.Object obj)
            {
                for (int i = 0; i < obj.Object_Values.Count; i++) JBOGValueList.Add(new JBOGValue(obj.Object_Values[i], i));
            }
        }

        public ITOP_Section ITOPSection { get; set; }
        public class ITOP_Section
        {
            public List<ITOP_Route> ITOP_Route_List = new List<ITOP_Route>();
            public List<ITOP_Route> ITOP_RouteList { get => ITOP_Route_List; set => ITOP_Route_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class ITOP_Route
            {
                [ReadOnly(true)]
                public int GroupID { get; set; }

                public byte ITOP_Roop { get; set; }
                public byte ITOP_Smooth { get; set; }

                public List<ITOP_Point> ITOP_Point_List = new List<ITOP_Point>();
                [Browsable(false)]
                public List<ITOP_Point> ITOP_PointList { get => ITOP_Point_List; set => ITOP_Point_List = value; }
                [TypeConverter(typeof(CustomSortTypeConverter))]
                public class ITOP_Point
                {
                    [ReadOnly(true)]
                    public int GroupID { get; set; }

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

                    public ushort ITOP_Point_RouteSpeed { get; set; }
                    public ushort ITOP_PointSetting2 { get; set; }

                    public ITOP_Point(Vector3D Pos, int Group_ID, int InputID)
                    {
                        GroupID = Group_ID;
                        ID = InputID;
                        Positions = new Position(Pos);
                        ITOP_Point_RouteSpeed = 0;
                        ITOP_PointSetting2 = 0;
                    }

                    public ITOP_Point(KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point iTOP_Point, int Group_ID, int InputID)
                    {
                        GroupID = Group_ID;
                        ID = InputID;
                        Positions = new Position(iTOP_Point.ITOP_Point_Position);
                        ITOP_Point_RouteSpeed = iTOP_Point.ITOP_Point_RouteSpeed;
                        ITOP_PointSetting2 = iTOP_Point.ITOP_PointSetting2;
                    }

                    public ITOP_Point(TestXml.KMPXml.Route.Route_Group.Route_Point route_Point, int Group_ID, int InputID)
                    {
                        GroupID = Group_ID;
                        ID = InputID;
                        Positions = new Position(route_Point.Position.ToVector3D());
                        ITOP_Point_RouteSpeed = route_Point.RouteSpeed;
                        ITOP_PointSetting2 = route_Point.PointSetting2;
                    }

                    public override string ToString()
                    {
                        return "Point " + ID;
                    }
                }

                public ITOP_Route(int InputID)
                {
                    GroupID = InputID;
                    ITOP_Roop = 0;
                    ITOP_Smooth = 0;
                    ITOP_Point_List = new List<ITOP_Point>();
                }

                public ITOP_Route(KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route iTOP_Route, int InputID)
                {
                    GroupID = InputID;
                    ITOP_Roop = iTOP_Route.ITOP_RoopSetting;
                    ITOP_Smooth = iTOP_Route.ITOP_SmoothSetting;

                    for (int i = 0; i < iTOP_Route.ITOP_Route_NumOfPoint; i++)
                    {
                        ITOP_Point_List.Add(new ITOP_Point(iTOP_Route.ITOP_Point_List[i], InputID, i));
                    }
                }

                public ITOP_Route(TestXml.KMPXml.Route.Route_Group route_Group, int InputID)
                {
                    GroupID = InputID;
                    ITOP_Roop = route_Group.RoopSetting;
                    ITOP_Smooth = route_Group.SmoothSetting;

                    for (int i = 0; i < route_Group.Points.Count; i++)
                    {
                        ITOP_Point_List.Add(new ITOP_Point(route_Group.Points[i], InputID, i));
                    }
                }

                public override string ToString()
                {
                    return "Route " + GroupID;
                }
            }

            public ITOP_Section()
            {
                ITOP_RouteList = new List<ITOP_Route>();
            }

            public ITOP_Section(KMPs.KMPFormat.KMPSection.ITOP_Section iTOP_Section)
            {
                for (int i = 0; i < iTOP_Section.ITOP_Route_List.Count; i++)
                {
                    ITOP_Route_List.Add(new ITOP_Route(iTOP_Section.ITOP_Route_List[i], i));
                }
            }

            public ITOP_Section(TestXml.KMPXml.Route route)
            {
                for (int i = 0; i < route.Groups.Count; i++)
                {
                    ITOP_RouteList.Add(new ITOP_Route(route.Groups[i], i));
                }
            }
        }

        public AERA_Section AERASection { get; set; }
        public class AERA_Section
        {
            public List<AERAValue> AERAValue_List = new List<AERAValue>();
            public List<AERAValue> AERAValueList { get => AERAValue_List; set => AERAValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class AERAValue
            {
                [ReadOnly(true)]
                public int ID { get; set; }

                public byte AreaType { get; set; }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public AreaModeSetting AreaModeSettings { get; set; } = new AreaModeSetting();
                public class AreaModeSetting
                {
                    [ReadOnly(true)]
                    public KMPs.KMPHelper.KMPValueTypeConverter.Area.AreaMode AreaTypeEnum
                    {
                        get { return KMPs.KMPHelper.KMPValueTypeConverter.Area.AreaModes(AreaModeValue); }
                    }

                    public byte AreaModeValue { get; set; }

                    public override string ToString()
                    {
                        return "Area Mode";
                    }
                }

                public byte AERA_EMACIndex { get; set; }
                public byte Priority { get; set; }

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

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Rotation Rotations { get; set; } = new Rotation();
                public class Rotation
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

                    public Rotation()
                    {
                        _X = 0;
                        _Y = 0;
                        _Z = 0;
                    }

                    public Rotation(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
                    }

                    public Rotation(Vector3D vector3D)
                    {
                        _X = HTK_3DES.TSRSystem.RadianToAngle(vector3D.X);
                        _Y = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Y);
                        _Z = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Z);
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
                        return "Rotation";
                    }
                }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Scale Scales { get; set; } = new Scale();
                public class Scale
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

                    public Scale()
                    {
                        _X = 1;
                        _Y = 1;
                        _Z = 1;
                    }

                    public Scale(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
                    }

                    public Scale(Vector3D vector3D)
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
                        return "Scale";
                    }
                }

                public ushort AERA_Setting1 { get; set; }
                public ushort AERA_Setting2 { get; set; }
                public byte RouteID { get; set; }
                public byte EnemyID { get; set; }
                public ushort AERA_UnkByte4 { get; set; }

                public AERAValue(Vector3D Pos, int InputID)
                {
                    ID = InputID;
                    AreaType = 0;
                    AreaModeSettings.AreaModeValue = 0;
                    AERA_EMACIndex = 0;
                    Priority = 0;
                    Positions = new Position(Pos);
                    Rotations = new Rotation();
                    Scales = new Scale();
                    AERA_Setting1 = 0;
                    AERA_Setting2 = 0;
                    RouteID = 0;
                    EnemyID = 0;
                    AERA_UnkByte4 = 0;
                }

                public AERAValue(KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue aERAValue, int InputID)
                {
                    ID = InputID;
                    AreaType = aERAValue.AreaType;
                    AreaModeSettings.AreaModeValue = aERAValue.AreaMode;
                    AERA_EMACIndex = aERAValue.AERA_EMACIndex;
                    Priority = aERAValue.Priority;
                    Positions = new Position(aERAValue.AERA_Position);
                    Rotations = new Rotation(aERAValue.AERA_Rotation);
                    Scales = new Scale(aERAValue.AERA_Scale);
                    AERA_Setting1 = aERAValue.AERA_Setting1;
                    AERA_Setting2 = aERAValue.AERA_Setting2;
                    RouteID = aERAValue.RouteID;
                    EnemyID = aERAValue.EnemyID;
                    AERA_UnkByte4 = aERAValue.AERA_UnkByte4;
                }

                public AERAValue(TestXml.KMPXml.Area.Area_Value area_Value, int InputID)
                {
                    ID = InputID;
                    AreaType = area_Value.AreaType;
                    AreaModeSettings.AreaModeValue = area_Value.AreaMode;
                    AERA_EMACIndex = area_Value.CameraIndex;
                    Priority = area_Value.Priority;
                    Positions = new Position(area_Value.Position.ToVector3D());
                    Rotations = new Rotation(area_Value.Rotation.ToVector3D());
                    Scales = new Scale(area_Value.Scale.ToVector3D());
                    AERA_Setting1 = area_Value.Setting1;
                    AERA_Setting2 = area_Value.Setting2;
                    RouteID = area_Value.RouteID;
                    EnemyID = area_Value.EnemyID;
                    AERA_UnkByte4 = area_Value.UnkByte4;
                }

                public override string ToString()
                {
                    return "Area " + ID;
                }
            }

            public AERA_Section()
            {
                AERAValueList = new List<AERAValue>();
            }

            public AERA_Section(KMPs.KMPFormat.KMPSection.AERA_Section aERA_Section)
            {
                for (int i = 0; i < aERA_Section.NumOfEntries; i++) AERAValueList.Add(new AERAValue(aERA_Section.AERAValue_List[i], i));
            }

            public AERA_Section(TestXml.KMPXml.Area area)
            {
                for (int i = 0; i < area.Area_Values.Count; i++) AERAValueList.Add(new AERAValue(area.Area_Values[i], i));
            }
        }

        public EMAC_Section EMACSection { get; set; }
        public class EMAC_Section
        {
            public List<EMACValue> EMACValue_List = new List<EMACValue>();
            public List<EMACValue> EMACValueList { get => EMACValue_List; set => EMACValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class EMACValue
            {
                [ReadOnly(true)]
                public int ID { get; set; }

                public byte CameraType { get; set; }
                public byte NextCameraIndex { get; set; }
                public byte EMAC_NextVideoIndex { get; set; }
                public byte EMAC_ITOP_CameraIndex { get; set; }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public SpeedSetting SpeedSettings { get; set; } = new SpeedSetting();
                public class SpeedSetting
                {
                    public ushort RouteSpeed { get; set; }
                    public ushort FOVSpeed { get; set; }
                    public ushort ViewpointSpeed { get; set; }

                    public SpeedSetting()
                    {
                        RouteSpeed = 0;
                        FOVSpeed = 0;
                        ViewpointSpeed = 0;
                    }

                    public SpeedSetting(ushort RouteSpd, ushort FOVSpd, ushort ViewpointSpd)
                    {
                        RouteSpeed = RouteSpd;
                        FOVSpeed = FOVSpd;
                        ViewpointSpeed = ViewpointSpd;
                    }

                    public override string ToString()
                    {
                        return "Speed";
                    }
                }

                public byte EMAC_StartFlag { get; set; }
                public byte EMAC_VideoFlag { get; set; }

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

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Rotation Rotations { get; set; } = new Rotation();
                public class Rotation
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

                    public Rotation()
                    {
                        _X = 0;
                        _Y = 0;
                        _Z = 0;
                    }

                    public Rotation(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
                    }

                    public Rotation(Vector3D vector3D)
                    {
                        _X = HTK_3DES.TSRSystem.RadianToAngle(vector3D.X);
                        _Y = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Y);
                        _Z = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Z);
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
                        return "Rotation";
                    }
                }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public FOVAngleSetting FOVAngleSettings { get; set; } = new FOVAngleSetting();
                public class FOVAngleSetting
                {
                    public float FOVAngle_Start { get; set; }
                    public float FOVAngle_End { get; set; }

                    public FOVAngleSetting()
                    {
                        FOVAngle_Start = 0;
                        FOVAngle_End = 0;
                    }

                    public FOVAngleSetting(float Start, float End)
                    {
                        FOVAngle_Start = Start;
                        FOVAngle_End = End;
                    }

                    public override string ToString()
                    {
                        return "FOV Angle";
                    }
                }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public ViewpointStart Viewpoint_Start { get; set; } = new ViewpointStart();
                public class ViewpointStart
                {
                    public float X { get; set; }
                    public float Y { get; set; }
                    public float Z { get; set; }

                    public ViewpointStart()
                    {
                        X = 0;
                        Y = 0;
                        Z = 0;
                    }

                    public ViewpointStart(float VPS_X, float VPS_Y, float VPS_Z)
                    {
                        X = VPS_X;
                        Y = VPS_Y;
                        Z = VPS_Z;
                    }

                    public ViewpointStart(Vector3D vector3D)
                    {
                        X = (float)vector3D.X;
                        Y = (float)vector3D.Y;
                        Z = (float)vector3D.Z;
                    }

                    public Vector3D GetVector3D()
                    {
                        double X_ = Convert.ToDouble(X);
                        double Y_ = Convert.ToDouble(Y);
                        double Z_ = Convert.ToDouble(Z);

                        return new Vector3D(X_, Y_, Z_);
                    }

                    public override string ToString()
                    {
                        return "Viewpoint Start";
                    }
                }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public ViewpointDestination Viewpoint_Destination { get; set; } = new ViewpointDestination();
                public class ViewpointDestination
                {
                    public float X { get; set; }
                    public float Y { get; set; }
                    public float Z { get; set; }

                    public ViewpointDestination()
                    {
                        X = 0;
                        Y = 0;
                        Z = 0;
                    }

                    public ViewpointDestination(float VPD_X, float VPD_Y, float VPD_Z)
                    {
                        X = VPD_X;
                        Y = VPD_Y;
                        Z = VPD_Z;
                    }

                    public ViewpointDestination(Vector3D vector3D)
                    {
                        X = (float)vector3D.X;
                        Y = (float)vector3D.Y;
                        Z = (float)vector3D.Z;
                    }

                    public Vector3D GetVector3D()
                    {
                        double X_ = Convert.ToDouble(X);
                        double Y_ = Convert.ToDouble(Y);
                        double Z_ = Convert.ToDouble(Z);

                        return new Vector3D(X_, Y_, Z_);
                    }

                    public override string ToString()
                    {
                        return "Viewpoint Destination";
                    }
                }

                public float Camera_Active_Time { get; set; }

                public EMACValue(Vector3D Pos, int InputID)
                {
                    ID = InputID;
                    CameraType = 0;
                    NextCameraIndex = 0;
                    EMAC_NextVideoIndex = 0;
                    EMAC_ITOP_CameraIndex = 0;
                    SpeedSettings = new SpeedSetting();
                    EMAC_StartFlag = 0;
                    EMAC_VideoFlag = 0;
                    Positions = new Position(Pos);
                    Rotations = new Rotation();
                    FOVAngleSettings = new FOVAngleSetting();
                    Viewpoint_Destination = new ViewpointDestination();
                    Viewpoint_Start = new ViewpointStart();
                    Camera_Active_Time = 0;
                }

                public EMACValue(KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue eMACValue, int InputID)
                {
                    ID = InputID;
                    CameraType = eMACValue.CameraType;
                    NextCameraIndex = eMACValue.NextCameraIndex;
                    EMAC_NextVideoIndex = eMACValue.EMAC_NextVideoIndex;
                    EMAC_ITOP_CameraIndex = eMACValue.EMAC_ITOP_CameraIndex;
                    SpeedSettings = new SpeedSetting(eMACValue.RouteSpeed, eMACValue.FOVSpeed, eMACValue.ViewpointSpeed);
                    EMAC_StartFlag = eMACValue.EMAC_StartFlag;
                    EMAC_VideoFlag = eMACValue.EMAC_VideoFlag;
                    Positions = new Position(eMACValue.EMAC_Position);
                    Rotations = new Rotation(eMACValue.EMAC_Rotation);
                    FOVAngleSettings = new FOVAngleSetting(eMACValue.FOVAngle_Start, eMACValue.FOVAngle_End);
                    Viewpoint_Destination = new ViewpointDestination(eMACValue.Viewpoint_Destination);
                    Viewpoint_Start = new ViewpointStart(eMACValue.Viewpoint_Start);
                    Camera_Active_Time = eMACValue.Camera_Active_Time;
                }

                public EMACValue(TestXml.KMPXml.Camera.Camera_Value camera_Value, int InputID)
                {
                    ID = InputID;
                    CameraType = camera_Value.CameraType;
                    NextCameraIndex = camera_Value.NextCameraIndex;
                    EMAC_NextVideoIndex = camera_Value.NextVideoIndex;
                    EMAC_ITOP_CameraIndex = camera_Value.Route_CameraIndex;
                    SpeedSettings = new SpeedSetting(camera_Value.SpeedSetting.RouteSpeed, camera_Value.SpeedSetting.FOVSpeed, camera_Value.SpeedSetting.ViewpointSpeed);
                    EMAC_StartFlag = camera_Value.StartFlag;
                    EMAC_VideoFlag = camera_Value.VideoFlag;
                    Positions = new Position(camera_Value.Position.ToVector3D());
                    Rotations = new Rotation(camera_Value.Rotation.ToVector3D());
                    FOVAngleSettings = new FOVAngleSetting(camera_Value.FOVAngleSettings.Start, camera_Value.FOVAngleSettings.End);
                    Viewpoint_Destination = new ViewpointDestination(camera_Value.ViewpointDestination.ToVector3D());
                    Viewpoint_Start = new ViewpointStart(camera_Value.ViewpointStart.ToVector3D());
                    Camera_Active_Time = camera_Value.CameraActiveTime;
                }

                public override string ToString()
                {
                    return "Camera " + ID;
                }
            }

            public EMAC_Section()
            {
                EMACValueList = new List<EMACValue>();
            }

            public EMAC_Section(KMPs.KMPFormat.KMPSection.EMAC_Section eMAC_Section)
            {
                for (int i = 0; i < eMAC_Section.NumOfEntries; i++) EMACValueList.Add(new EMACValue(eMAC_Section.EMACValue_List[i], i));
            }

            public EMAC_Section(TestXml.KMPXml.Camera camera)
            {
                for (int i = 0; i < camera.Values.Count; i++) EMACValueList.Add(new EMACValue(camera.Values[i], i));
            }
        }

        public TPGJ_Section TPGJSection { get; set; }
        public class TPGJ_Section
        {
            public List<TPGJValue> TPGJValue_List = new List<TPGJValue>();
            public List<TPGJValue> TPGJValueList { get => TPGJValue_List; set => TPGJValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class TPGJValue
            {
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

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Rotation Rotations { get; set; } = new Rotation();
                public class Rotation
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

                    public Rotation()
                    {
                        _X = 0;
                        _Y = 0;
                        _Z = 0;
                    }

                    public Rotation(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
                    }

                    public Rotation(Vector3D vector3D)
                    {
                        _X = HTK_3DES.TSRSystem.RadianToAngle(vector3D.X);
                        _Y = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Y);
                        _Z = HTK_3DES.TSRSystem.RadianToAngle(vector3D.Z);
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
                        return "Rotation";
                    }
                }

                public ushort TPGJ_RespawnID { get; set; }
                public ushort TPGJ_UnkBytes1 { get; set; }

                public TPGJValue(Vector3D Pos, int InputID)
                {
                    ID = InputID;
                    TPGJ_RespawnID = 65535;
                    Positions = new Position(Pos);
                    Rotations = new Rotation();
                    TPGJ_UnkBytes1 = 0;
                }

                public TPGJValue(KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue tPGJValue, int InputID)
                {
                    ID = InputID;
                    TPGJ_RespawnID = tPGJValue.TPGJ_RespawnID;
                    Positions = new Position(tPGJValue.TPGJ_Position);
                    Rotations = new Rotation(tPGJValue.TPGJ_Rotation);
                    TPGJ_UnkBytes1 = tPGJValue.TPGJ_UnkBytes1;
                }

                public TPGJValue(TestXml.KMPXml.JugemPoint.JugemPoint_Value jugemPoint_Value, int InputID)
                {
                    ID = InputID;
                    TPGJ_RespawnID = jugemPoint_Value.RespawnID;
                    Positions = new Position(jugemPoint_Value.Position.ToVector3D());
                    Rotations = new Rotation(jugemPoint_Value.Rotation.ToVector3D());
                    TPGJ_UnkBytes1 = jugemPoint_Value.UnkBytes1;
                }

                public override string ToString()
                {
                    return "Jugem Point " + ID;
                }
            }

            public TPGJ_Section()
            {
                TPGJValueList = new List<TPGJValue>();
            }

            public TPGJ_Section(KMPs.KMPFormat.KMPSection.TPGJ_Section tPGJ_Section)
            {
                for (int i = 0; i < tPGJ_Section.NumOfEntries; i++) TPGJValueList.Add(new TPGJValue(tPGJ_Section.TPGJValue_List[i], i));
            }

            public TPGJ_Section(TestXml.KMPXml.JugemPoint jugemPoint)
            {
                for (int i = 0; i < jugemPoint.Values.Count; i++) TPGJValueList.Add(new TPGJValue(jugemPoint.Values[i], i));
            }
        }

        //TPNC = null

        //TPSM = null

        public IGTS_Section IGTSSection { get; set; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class IGTS_Section
        {
            public uint Unknown1 { get; set; }
            public byte LapCount { get; set; }
            public byte PolePosition { get; set; }
            public byte Unknown2 { get; set; }
            public byte Unknown3 { get; set; }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public RGBA RGBAColor { get; set; }
            public class RGBA
            {
                public byte R { get; set; }
                public byte G { get; set; }
                public byte B { get; set; }
                public byte A { get; set; }

                public RGBA()
                {
                    R = 0;
                    G = 0;
                    B = 0;
                    A = 0;
                }

                public RGBA(byte In_R, byte In_G, byte In_B, byte In_A)
                {
                    R = In_R;
                    G = In_G;
                    B = In_B;
                    A = In_A;
                }

                public override string ToString()
                {
                    return "RGBA Color";
                }
            }

            public uint FlareAlpha { get; set; }

            public IGTS_Section()
            {
                Unknown1 = 0;
                LapCount = 3;
                PolePosition = 0;
                Unknown2 = 0;
                Unknown3 = 0;
                RGBAColor = new RGBA(255, 255, 255, 0);
                FlareAlpha = 75;
            }

            public IGTS_Section(KMPs.KMPFormat.KMPSection.IGTS_Section iGTS_Section)
            {
                Unknown1 = iGTS_Section.Unknown1;
                LapCount = iGTS_Section.LapCount;
                PolePosition = iGTS_Section.PolePosition;
                Unknown2 = iGTS_Section.Unknown2;
                Unknown3 = iGTS_Section.Unknown3;
                RGBAColor = new RGBA(iGTS_Section.RGBAColor.R, iGTS_Section.RGBAColor.G, iGTS_Section.RGBAColor.B, iGTS_Section.RGBAColor.A);
                FlareAlpha = iGTS_Section.FlareAlpha;
            }

            public IGTS_Section(TestXml.KMPXml.StageInfo stageInfo)
            {
                Unknown1 = stageInfo.Unknown1;
                LapCount = stageInfo.LapCount;
                PolePosition = stageInfo.PolePosition;
                Unknown2 = stageInfo.Unknown2;
                Unknown3 = stageInfo.Unknown3;
                RGBAColor = new RGBA(stageInfo.RGBAColor.R, stageInfo.RGBAColor.G, stageInfo.RGBAColor.B, stageInfo.RGBAColor.A);
                FlareAlpha = stageInfo.RGBAColor.FlareAlpha;
            }
        }

        //SROC = null

        public HPLG_TPLG_Section HPLG_TPLGSection { get; set; }
        public class HPLG_TPLG_Section
        {
            public List<HPLGValue> HPLGValue_List = new List<HPLGValue>();
            public List<HPLGValue> HPLGValueList { get => HPLGValue_List; set => HPLGValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class HPLGValue
            {
                [ReadOnly(true)]
                public int GroupID { get; set; }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public HPLG_PreviewGroups HPLG_PreviewGroup { get; set; } = new HPLG_PreviewGroups();
                public class HPLG_PreviewGroups
                {
                    public byte Prev0 { get; set; }
                    public byte Prev1 { get; set; }
                    public byte Prev2 { get; set; }
                    public byte Prev3 { get; set; }
                    public byte Prev4 { get; set; }
                    public byte Prev5 { get; set; }

                    public HPLG_PreviewGroups()
                    {
                        Prev0 = 255;
                        Prev1 = 255;
                        Prev2 = 255;
                        Prev3 = 255;
                        Prev4 = 255;
                        Prev5 = 255;
                    }

                    public HPLG_PreviewGroups(KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue.HPLG_PreviewGroups HPLG_PreviewGroup)
                    {
                        Prev0 = HPLG_PreviewGroup.Prev0;
                        Prev1 = HPLG_PreviewGroup.Prev1;
                        Prev2 = HPLG_PreviewGroup.Prev2;
                        Prev3 = HPLG_PreviewGroup.Prev3;
                        Prev4 = HPLG_PreviewGroup.Prev4;
                        Prev5 = HPLG_PreviewGroup.Prev5;
                    }

                    public HPLG_PreviewGroups(TestXml.KMPXml.GlideRoute.GlideRoute_Group.GR_PreviousGroup previous)
                    {
                        Prev0 = previous.Prev0;
                        Prev1 = previous.Prev1;
                        Prev2 = previous.Prev2;
                        Prev3 = previous.Prev3;
                        Prev4 = previous.Prev4;
                        Prev5 = previous.Prev5;
                    }

                    public override string ToString()
                    {
                        return "Preview";
                    }
                }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public HPLG_NextGroups HPLG_NextGroup { get; set; } = new HPLG_NextGroups();
                public class HPLG_NextGroups
                {
                    public byte Next0 { get; set; }
                    public byte Next1 { get; set; }
                    public byte Next2 { get; set; }
                    public byte Next3 { get; set; }
                    public byte Next4 { get; set; }
                    public byte Next5 { get; set; }

                    public HPLG_NextGroups()
                    {
                        Next0 = 255;
                        Next1 = 255;
                        Next2 = 255;
                        Next3 = 255;
                        Next4 = 255;
                        Next5 = 255;
                    }

                    public HPLG_NextGroups(KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue.HPLG_NextGroups HPLG_NextGroup)
                    {
                        Next0 = HPLG_NextGroup.Next0;
                        Next1 = HPLG_NextGroup.Next1;
                        Next2 = HPLG_NextGroup.Next2;
                        Next3 = HPLG_NextGroup.Next3;
                        Next4 = HPLG_NextGroup.Next4;
                        Next5 = HPLG_NextGroup.Next5;
                    }

                    public HPLG_NextGroups(TestXml.KMPXml.GlideRoute.GlideRoute_Group.GR_NextGroup next)
                    {
                        Next0 = next.Next0;
                        Next1 = next.Next1;
                        Next2 = next.Next2;
                        Next3 = next.Next3;
                        Next4 = next.Next4;
                        Next5 = next.Next5;
                    }

                    public override string ToString()
                    {
                        return "Next";
                    }
                }

                #region RouteSetting(I'm using the code in "KMPExpander-master\KMPExpander\Class\SimpleKMPs\GliderRoutes.cs" of "KMP Expander")
                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public RouteSetting RouteSettings { get; set; } = new RouteSetting();
                public class RouteSetting
                {
                    [Browsable(false)]
                    public uint RouteSettingValue { get; set; }

                    public bool ForceToRoute
                    {
                        get
                        {
                            return (RouteSettingValue & 0xFF) != 0;
                        }
                        set
                        {
                            RouteSettingValue = (RouteSettingValue & ~0xFFu) | (value ? 1u : 0u);
                        }
                    }

                    public bool CannonSection
                    {
                        get
                        {
                            return (RouteSettingValue & 0xFF00) != 0;
                        }
                        set
                        {
                            RouteSettingValue = (RouteSettingValue & ~0xFF00u) | (value ? 1u : 0u) << 8;
                        }
                    }

                    public bool PreventRaising
                    {
                        get
                        {
                            return (RouteSettingValue & 0xFF0000) != 0;
                        }
                        set
                        {
                            RouteSettingValue = (RouteSettingValue & ~0xFF0000u) | (value ? 1u : 0u) << 16;
                        }
                    }

                    public override string ToString()
                    {
                        return "Route Setting";
                    }
                }
                #endregion

                public uint HPLG_UnkBytes2 { get; set; }

                public List<TPLGValue> TPLGValue_List = new List<TPLGValue>();
                [Browsable(false)]
                public List<TPLGValue> TPLGValueList { get => TPLGValue_List; set => TPLGValue_List = value; }
                [TypeConverter(typeof(CustomSortTypeConverter))]
                public class TPLGValue
                {
                    [ReadOnly(true)]
                    public int GroupID { get; set; }

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

                    public float TPLG_PointScaleValue { get; set; }

                    public uint TPLG_UnkBytes1 { get; set; }
                    public uint TPLG_UnkBytes2 { get; set; }

                    public TPLGValue(Vector3D Pos, int GroupID, int InputID)
                    {
                        this.GroupID = GroupID;
                        ID = InputID;
                        Positions = new Position(Pos);
                        TPLG_PointScaleValue = 1;
                        TPLG_UnkBytes1 = 0;
                        TPLG_UnkBytes2 = 0;
                    }

                    public TPLGValue(KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue TPLGValue, int GroupID, int InputID)
                    {
                        this.GroupID = GroupID;
                        ID = InputID;
                        Positions = new Position(TPLGValue.TPLG_Position);
                        TPLG_PointScaleValue = TPLGValue.TPLG_PointScaleValue;
                        TPLG_UnkBytes1 = TPLGValue.TPLG_UnkBytes1;
                        TPLG_UnkBytes2 = TPLGValue.TPLG_UnkBytes2;
                    }

                    public TPLGValue(TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point glideRoute_Point, int GroupID, int InputID)
                    {
                        this.GroupID = GroupID;
                        ID = InputID;
                        Positions = new Position(glideRoute_Point.Position.ToVector3D());
                        TPLG_PointScaleValue = glideRoute_Point.PointScale;
                        TPLG_UnkBytes1 = glideRoute_Point.UnkBytes1;
                        TPLG_UnkBytes2 = glideRoute_Point.UnkBytes2;
                    }

                    public TPLGValue(TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData, int GroupID, int InputID)
                    {
                        this.GroupID = GroupID;
                        ID = InputID;
                        Positions = new Position(pointData.Position.ToVector3D());
                        TPLG_PointScaleValue = pointData.ScaleValue;
                        TPLG_UnkBytes1 = 0;
                        TPLG_UnkBytes2 = 0;
                    }

                    public override string ToString()
                    {
                        return "Glide Point " + ID;
                    }
                }

                public HPLGValue(int InputID)
                {
                    GroupID = InputID;
                    HPLG_PreviewGroup = new HPLG_PreviewGroups();
                    HPLG_NextGroup = new HPLG_NextGroups();
                    RouteSettings.RouteSettingValue = 0;
                    HPLG_UnkBytes2 = 0;
                    TPLGValueList = new List<TPLGValue>();
                }

                public HPLGValue(KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue HPLGValue, KMPs.KMPFormat.KMPSection.TPLG_Section TPLG, int InputID)
                {
                    GroupID = InputID;
                    HPLG_PreviewGroup = new HPLG_PreviewGroups(HPLGValue.HPLG_PreviewGroup);
                    HPLG_NextGroup = new HPLG_NextGroups(HPLGValue.HPLG_NextGroup);
                    RouteSettings.RouteSettingValue = HPLGValue.RouteSetting;
                    HPLG_UnkBytes2 = HPLGValue.HPLG_UnkBytes2;

                    for (int i = 0; i < HPLGValue.HPLG_Length; i++)
                    {
                        TPLGValueList.Add(new TPLGValue(TPLG.TPLGValue_List[i + HPLGValue.HPLG_StartPoint], InputID, i));
                    }
                }

                public HPLGValue(TestXml.KMPXml.GlideRoute.GlideRoute_Group glideRoute_Group, int InputID)
                {
                    GroupID = InputID;
                    HPLG_PreviewGroup = new HPLG_PreviewGroups(glideRoute_Group.PreviousGroups);
                    HPLG_NextGroup = new HPLG_NextGroups(glideRoute_Group.NextGroups);
                    RouteSettings.RouteSettingValue = glideRoute_Group.RouteSetting;
                    HPLG_UnkBytes2 = glideRoute_Group.UnkBytes2;

                    for (int i = 0; i < glideRoute_Group.Points.Count; i++)
                    {
                        TPLGValueList.Add(new TPLGValue(glideRoute_Group.Points[i], InputID, i));
                    }
                }

                public HPLGValue(TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData, int InputID)
                {
                    GroupID = InputID;
                    HPLG_PreviewGroup = new HPLG_PreviewGroups();
                    HPLG_NextGroup = new HPLG_NextGroups();
                    RouteSettings.RouteSettingValue = 0;
                    HPLG_UnkBytes2 = 0;

                    for (int i = 0; i < groupData.Points.Count; i++)
                    {
                        TPLGValueList.Add(new TPLGValue(groupData.Points[i], InputID, i));
                    }
                }

                public override string ToString()
                {
                    return "Glide Route " + GroupID;
                }
            }

            public HPLG_TPLG_Section()
            {
                HPLGValueList = new List<HPLGValue>();
            }

            public HPLG_TPLG_Section(KMPs.KMPFormat.KMPSection.HPLG_Section HPLG, KMPs.KMPFormat.KMPSection.TPLG_Section TPLG)
            {
                for (int i = 0; i < HPLG.NumOfEntries; i++)
                {
                    HPLGValueList.Add(new HPLGValue(HPLG.HPLGValue_List[i], TPLG, i));
                }
            }

            public HPLG_TPLG_Section(TestXml.KMPXml.GlideRoute glideRoute)
            {
                for (int i = 0; i < glideRoute.Groups.Count; i++)
                {
                    HPLGValueList.Add(new HPLGValue(glideRoute.Groups[i], i));
                }
            }

            public HPLG_TPLG_Section(TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
            {
                for (int i = 0; i < xXXXRoute.Groups.Count; i++)
                {
                    HPLGValueList.Add(new HPLGValue(xXXXRoute.Groups[i], i));
                }
            }
        }
    }

    public class PropertyGridClassToBinaryConverter
    {
        public static KMPs.KMPFormat.KMPSection.TPTK_Section ToTPTK_Section(KMPPropertyGridSettings.TPTK_Section TPTK_Section)
        {
            KMPs.KMPFormat.KMPSection.TPTK_Section TPTK = new KMPs.KMPFormat.KMPSection.TPTK_Section
            {
                TPTKHeader = new char[] { 'T', 'P', 'T', 'K' },
                NumOfEntries = Convert.ToUInt16(TPTK_Section.TPTKValueList.Count),
                AdditionalValue = 0,
                TPTKValue_List = null
            };

            List<KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue> TPTK_Value_List = new List<KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue>();

            for (int Count = 0; Count < TPTK_Section.TPTKValueList.Count; Count++)
            {
                double RX = HTK_3DES.TSRSystem.AngleToRadian(TPTK_Section.TPTKValueList[Count].Rotate_Value.X);
                double RY = HTK_3DES.TSRSystem.AngleToRadian(TPTK_Section.TPTKValueList[Count].Rotate_Value.Y);
                double RZ = HTK_3DES.TSRSystem.AngleToRadian(TPTK_Section.TPTKValueList[Count].Rotate_Value.Z);

                KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue TPTK_Values = new KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue
                {
                    TPTK_Position = TPTK_Section.TPTKValueList[Count].Position_Value.GetVector3D(),
                    TPTK_Rotation = new Vector3D(RX, RY, RZ),
                    Player_Index = Convert.ToUInt16(TPTK_Section.TPTKValueList[Count].Player_Index),
                    TPTK_UnkBytes = Convert.ToUInt16(TPTK_Section.TPTKValueList[Count].TPTK_UnkBytes)
                };

                TPTK_Value_List.Add(TPTK_Values);
            }

            TPTK.TPTKValue_List = TPTK_Value_List;

            return TPTK;
        }

        public class HPNE_TPNESection
        {
            public class HPNE_TPNEData
            {
                public KMPs.KMPFormat.KMPSection.HPNE_Section HPNE_Section;
                public KMPs.KMPFormat.KMPSection.TPNE_Section TPNE_Section;

                public HPNE_TPNEData(KMPs.KMPFormat.KMPSection.HPNE_Section HPNE, KMPs.KMPFormat.KMPSection.TPNE_Section TPNE)
                {
                    HPNE_Section = HPNE;
                    TPNE_Section = TPNE;
                }
            }

            public static HPNE_TPNEData ToHPNE_TPNE_Section(KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section)
            {
                HPNE_TPNEData hPNE_TPNEData = null;

                if (HPNE_TPNE_Section.HPNEValueList.Count != 0)
                {
                    List<KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue> TPNE_Values_List = new List<KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue>();
                    List<KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue> HPNE_Values_List = new List<KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue>();

                    int StartPoint = 0;
                    for (int HPNECount = 0; HPNECount < HPNE_TPNE_Section.HPNEValueList.Count; HPNECount++)
                    {
                        KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue HPNE_Values = new KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue
                        {
                            HPNE_StartPoint = Convert.ToUInt16(StartPoint),
                            HPNE_Length = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList.Count),
                            HPNE_PreviewGroup = new KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue.HPNE_PreviewGroups
                            {
                                Prev0 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev0),
                                Prev1 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev1),
                                Prev2 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev2),
                                Prev3 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev3),
                                Prev4 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev4),
                                Prev5 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev5),
                                Prev6 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev6),
                                Prev7 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev7),
                                Prev8 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev8),
                                Prev9 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev9),
                                Prev10 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev10),
                                Prev11 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev11),
                                Prev12 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev12),
                                Prev13 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev13),
                                Prev14 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev14),
                                Prev15 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNEPreviewGroups.Prev15)
                            },
                            HPNE_NextGroup = new KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue.HPNE_NextGroups
                            {
                                Next0 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next0),
                                Next1 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next1),
                                Next2 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next2),
                                Next3 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next3),
                                Next4 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next4),
                                Next5 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next5),
                                Next6 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next6),
                                Next7 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next7),
                                Next8 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next8),
                                Next9 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next9),
                                Next10 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next10),
                                Next11 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next11),
                                Next12 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next12),
                                Next13 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next13),
                                Next14 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next14),
                                Next15 = Convert.ToUInt16(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNENextGroups.Next15)
                            },
                            HPNE_UnkBytes1 = Convert.ToUInt32(HPNE_TPNE_Section.HPNEValueList[HPNECount].HPNE_UnkBytes1)
                        };

                        HPNE_Values_List.Add(HPNE_Values);

                        for (int TPNECount = 0; TPNECount < HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList.Count; TPNECount++)
                        {
                            KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue TPNE_Values = new KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue
                            {
                                TPNE_Position = HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList[TPNECount].Positions.GetVector3D(),
                                Control = Convert.ToSingle(HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList[TPNECount].Control),
                                MushSetting = HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList[TPNECount].MushSettings.MushSettingValue,
                                DriftSetting = Convert.ToByte(HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList[TPNECount].DriftSettings.DriftSettingValue),
                                Flags = Convert.ToByte(HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList[TPNECount].FlagSettings.Flags),
                                PathFindOption = HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList[TPNECount].PathFindOptions.PathFindOptionValue,
                                MaxSearchYOffset = HPNE_TPNE_Section.HPNEValueList[HPNECount].TPNEValueList[TPNECount].MaxSearchYOffset.MaxSearchYOffsetValue
                            };

                            TPNE_Values_List.Add(TPNE_Values);

                            StartPoint++;
                        }
                    }

                    KMPs.KMPFormat.KMPSection.TPNE_Section TPNE = new KMPs.KMPFormat.KMPSection.TPNE_Section
                    {
                        TPNEHeader = new char[] { 'T', 'P', 'N', 'E' },
                        NumOfEntries = Convert.ToUInt16(TPNE_Values_List.Count),
                        AdditionalValue = 0,
                        TPNEValue_List = TPNE_Values_List
                    };

                    KMPs.KMPFormat.KMPSection.HPNE_Section HPNE = new KMPs.KMPFormat.KMPSection.HPNE_Section
                    {
                        HPNEHeader = new char[] { 'H', 'P', 'N', 'E' },
                        NumOfEntries = Convert.ToUInt16(HPNE_Values_List.Count),
                        AdditionalValue = 0,
                        HPNEValue_List = HPNE_Values_List
                    };

                    hPNE_TPNEData = new HPNE_TPNEData(HPNE, TPNE);
                }
                if (HPNE_TPNE_Section.HPNEValueList.Count == 0)
                {
                    KMPs.KMPFormat.KMPSection.TPNE_Section TPNE = new KMPs.KMPFormat.KMPSection.TPNE_Section
                    {
                        TPNEHeader = new char[] { 'T', 'P', 'N', 'E' },
                        NumOfEntries = 0,
                        AdditionalValue = 0,
                        TPNEValue_List = new List<KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue>()
                    };

                    KMPs.KMPFormat.KMPSection.HPNE_Section HPNE = new KMPs.KMPFormat.KMPSection.HPNE_Section
                    {
                        HPNEHeader = new char[] { 'H', 'P', 'N', 'E' },
                        NumOfEntries = 0,
                        AdditionalValue = 0,
                        HPNEValue_List = new List<KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue>()
                    };

                    hPNE_TPNEData = new HPNE_TPNEData(HPNE, TPNE);
                }

                return hPNE_TPNEData;
            }
        }

        public class HPTI_TPTISection
        {
            public class HPTI_TPTIData
            {
                public KMPs.KMPFormat.KMPSection.HPTI_Section HPTI_Section;
                public KMPs.KMPFormat.KMPSection.TPTI_Section TPTI_Section;

                public HPTI_TPTIData(KMPs.KMPFormat.KMPSection.HPTI_Section HPTI, KMPs.KMPFormat.KMPSection.TPTI_Section TPTI)
                {
                    HPTI_Section = HPTI;
                    TPTI_Section = TPTI;
                }
            }

            public static HPTI_TPTIData ToHPTI_TPTI_Section(KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section)
            {
                HPTI_TPTIData hPTI_TPTIData = null;

                if (HPTI_TPTI_Section.HPTIValueList.Count != 0)
                {
                    List<KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue> TPTI_Values_List = new List<KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue>();
                    List<KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue> HPTI_Values_List = new List<KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue>();

                    int StartPoint = 0;
                    for (int HPTICount = 0; HPTICount < HPTI_TPTI_Section.HPTIValueList.Count; HPTICount++)
                    {
                        KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue HPTI_Values = new KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue
                        {
                            HPTI_StartPoint = Convert.ToUInt16(StartPoint),
                            HPTI_Length = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].TPTIValueList.Count),
                            HPTI_PreviewGroup = new KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue.HPTI_PreviewGroups
                            {
                                Prev0 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_PreviewGroup.Prev0),
                                Prev1 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_PreviewGroup.Prev1),
                                Prev2 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_PreviewGroup.Prev2),
                                Prev3 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_PreviewGroup.Prev3),
                                Prev4 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_PreviewGroup.Prev4),
                                Prev5 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_PreviewGroup.Prev5),
                            },
                            HPTI_NextGroup = new KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue.HPTI_NextGroups
                            {
                                Next0 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_NextGroup.Next0),
                                Next1 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_NextGroup.Next1),
                                Next2 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_NextGroup.Next2),
                                Next3 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_NextGroup.Next3),
                                Next4 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_NextGroup.Next4),
                                Next5 = Convert.ToUInt16(HPTI_TPTI_Section.HPTIValueList[HPTICount].HPTI_NextGroup.Next5),
                            }
                        };

                        HPTI_Values_List.Add(HPTI_Values);

                        for (int TPTICount = 0; TPTICount < HPTI_TPTI_Section.HPTIValueList[HPTICount].TPTIValueList.Count; TPTICount++)
                        {
                            KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue TPTI_Values = new KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue
                            {
                                TPTI_Position = HPTI_TPTI_Section.HPTIValueList[HPTICount].TPTIValueList[TPTICount].TPTI_Positions.GetVector3D(),
                                TPTI_PointSize = Convert.ToSingle(HPTI_TPTI_Section.HPTIValueList[HPTICount].TPTIValueList[TPTICount].TPTI_PointSize),
                                GravityMode = HPTI_TPTI_Section.HPTIValueList[HPTICount].TPTIValueList[TPTICount].GravityModeSettings.GravityModeValue,
                                PlayerScanRadius = HPTI_TPTI_Section.HPTIValueList[HPTICount].TPTIValueList[TPTICount].PlayerScanRadiusSettings.PlayerScanRadiusValue
                            };

                            TPTI_Values_List.Add(TPTI_Values);

                            StartPoint++;
                        }
                    }

                    KMPs.KMPFormat.KMPSection.TPTI_Section TPTI = new KMPs.KMPFormat.KMPSection.TPTI_Section
                    {
                        TPTIHeader = new char[] { 'T', 'P', 'T', 'I' },
                        NumOfEntries = Convert.ToUInt16(TPTI_Values_List.Count),
                        AdditionalValue = 0,
                        TPTIValue_List = TPTI_Values_List
                    };

                    KMPs.KMPFormat.KMPSection.HPTI_Section HPTI = new KMPs.KMPFormat.KMPSection.HPTI_Section
                    {
                        HPTIHeader = new char[] { 'H', 'P', 'T', 'I' },
                        NumOfEntries = Convert.ToUInt16(HPTI_Values_List.Count),
                        AdditionalValue = 0,
                        HPTIValue_List = HPTI_Values_List
                    };

                    hPTI_TPTIData = new HPTI_TPTIData(HPTI, TPTI);
                }
                if (HPTI_TPTI_Section.HPTIValueList.Count == 0)
                {
                    KMPs.KMPFormat.KMPSection.TPTI_Section TPTI = new KMPs.KMPFormat.KMPSection.TPTI_Section
                    {
                        TPTIHeader = new char[] { 'T', 'P', 'T', 'I' },
                        NumOfEntries = 0,
                        AdditionalValue = 0,
                        TPTIValue_List = new List<KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue>()
                    };

                    KMPs.KMPFormat.KMPSection.HPTI_Section HPTI = new KMPs.KMPFormat.KMPSection.HPTI_Section
                    {
                        HPTIHeader = new char[] { 'H', 'P', 'T', 'I' },
                        NumOfEntries = 0,
                        AdditionalValue = 0,
                        HPTIValue_List = new List<KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue>()
                    };

                    hPTI_TPTIData = new HPTI_TPTIData(HPTI, TPTI);
                }

                return hPTI_TPTIData;
            }
        }

        public class HPKC_TPKCSection
        {
            public class HPKC_TPKCData
            {
                public KMPs.KMPFormat.KMPSection.HPKC_Section HPKC_Section;
                public KMPs.KMPFormat.KMPSection.TPKC_Section TPKC_Section;

                public HPKC_TPKCData(KMPs.KMPFormat.KMPSection.HPKC_Section HPKC, KMPs.KMPFormat.KMPSection.TPKC_Section TPKC)
                {
                    HPKC_Section = HPKC;
                    TPKC_Section = TPKC;
                }
            }

            public static HPKC_TPKCData ToHPKC_TPKC_Section(KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section)
            {
                HPKC_TPKCData hPKC_TPKCData = null;

                if (HPKC_TPKC_Section.HPKCValueList.Count != 0)
                {
                    List<KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue> TPKC_Values_List = new List<KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue>();
                    List<KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue> HPKC_Values_List = new List<KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue>();

                    int StartPoint = 0;
                    for (int HPKCCount = 0; HPKCCount < HPKC_TPKC_Section.HPKCValueList.Count; HPKCCount++)
                    {
                        KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue HPKC_Values = new KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue
                        {
                            HPKC_StartPoint = Convert.ToByte(StartPoint),
                            HPKC_Length = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList.Count),
                            HPKC_PreviewGroup = new KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue.HPKC_PreviewGroups
                            {
                                Prev0 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev0),
                                Prev1 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev1),
                                Prev2 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev2),
                                Prev3 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev3),
                                Prev4 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev4),
                                Prev5 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev5),
                            },
                            HPKC_NextGroup = new KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue.HPKC_NextGroups
                            {
                                Next0 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_NextGroup.Next0),
                                Next1 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_NextGroup.Next1),
                                Next2 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_NextGroup.Next2),
                                Next3 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_NextGroup.Next3),
                                Next4 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_NextGroup.Next4),
                                Next5 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].HPKC_NextGroup.Next5),
                            }
                        };

                        HPKC_Values_List.Add(HPKC_Values);

                        for (int TPKCCount = 0; TPKCCount < HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList.Count; TPKCCount++)
                        {
                            KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue TPKC_Values = new KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue
                            {
                                TPKC_2DPosition_Left = HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].Position_2D_Left.GetVector2(),
                                TPKC_2DPosition_Right = HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].Position_2D_Right.GetVector2(),

                                TPKC_RespawnID = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_RespawnID),
                                TPKC_Checkpoint_Type = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_Checkpoint_Type),
                                TPKC_PreviousCheckPoint = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_PreviousCheckPoint),
                                TPKC_NextCheckPoint = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_NextCheckPoint),
                                TPKC_ClipID = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_ClipID),
                                TPKC_Section = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_Section),
                                TPKC_UnkBytes3 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_UnkBytes3),
                                TPKC_UnkBytes4 = Convert.ToByte(HPKC_TPKC_Section.HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_UnkBytes4)
                            };

                            TPKC_Values_List.Add(TPKC_Values);

                            StartPoint++;
                        }
                    }

                    KMPs.KMPFormat.KMPSection.TPKC_Section TPKC = new KMPs.KMPFormat.KMPSection.TPKC_Section
                    {
                        TPKCHeader = new char[] { 'T', 'P', 'K', 'C' },
                        NumOfEntries = Convert.ToUInt16(TPKC_Values_List.Count),
                        AdditionalValue = 0,
                        TPKCValue_List = TPKC_Values_List
                    };

                    KMPs.KMPFormat.KMPSection.HPKC_Section HPKC = new KMPs.KMPFormat.KMPSection.HPKC_Section
                    {
                        HPKCHeader = new char[] { 'H', 'P', 'K', 'C' },
                        NumOfEntries = Convert.ToUInt16(HPKC_Values_List.Count),
                        AdditionalValue = 0,
                        HPKCValue_List = HPKC_Values_List
                    };

                    hPKC_TPKCData = new HPKC_TPKCData(HPKC, TPKC);
                }
                if (HPKC_TPKC_Section.HPKCValueList.Count == 0)
                {
                    KMPs.KMPFormat.KMPSection.TPKC_Section TPKC = new KMPs.KMPFormat.KMPSection.TPKC_Section
                    {
                        TPKCHeader = new char[] { 'T', 'P', 'K', 'C' },
                        NumOfEntries = 0,
                        AdditionalValue = 0,
                        TPKCValue_List = new List<KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue>()
                    };

                    KMPs.KMPFormat.KMPSection.HPKC_Section HPKC = new KMPs.KMPFormat.KMPSection.HPKC_Section
                    {
                        HPKCHeader = new char[] { 'H', 'P', 'K', 'C' },
                        NumOfEntries = 0,
                        AdditionalValue = 0,
                        HPKCValue_List = new List<KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue>()
                    };

                    hPKC_TPKCData = new HPKC_TPKCData(HPKC, TPKC);
                }

                return hPKC_TPKCData;
            }
        }

        public static KMPs.KMPFormat.KMPSection.JBOG_Section ToJBOG_Section(KMPPropertyGridSettings.JBOG_Section JBOG_Section, uint KMP_Version)
        {
            KMPs.KMPFormat.KMPSection.JBOG_Section JBOG = new KMPs.KMPFormat.KMPSection.JBOG_Section
            {
                JBOGHeader = new char[] { 'J', 'B', 'O', 'G' },
                NumOfEntries = Convert.ToUInt16(JBOG_Section.JBOGValueList.Count),
                AdditionalValue = 0,
                JBOGValue_List = null
            };

            List<KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue> JBOG_Value_List = new List<KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue>();

            for (int Count = 0; Count < JBOG_Section.JBOGValueList.Count; Count++)
            {
                double RX = HTK_3DES.TSRSystem.AngleToRadian(JBOG_Section.JBOGValueList[Count].Rotations.X);
                double RY = HTK_3DES.TSRSystem.AngleToRadian(JBOG_Section.JBOGValueList[Count].Rotations.Y);
                double RZ = HTK_3DES.TSRSystem.AngleToRadian(JBOG_Section.JBOGValueList[Count].Rotations.Z);

                KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue JBOG_Values = new KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue();

                JBOG_Values.ObjectID = KMPs.KMPHelper.Byte2StringConverter.OBJIDStrToByteArray(JBOG_Section.JBOGValueList[Count].ObjectID);
                JBOG_Values.JBOG_UnkByte1 = KMPs.KMPHelper.Byte2StringConverter.OBJIDStrToByteArray(JBOG_Section.JBOGValueList[Count].JBOG_UnkByte1);
                JBOG_Values.JBOG_Position = JBOG_Section.JBOGValueList[Count].Positions.GetVector3D();
                JBOG_Values.JBOG_Rotation = new Vector3D(RX, RY, RZ);
                JBOG_Values.JBOG_Scale = JBOG_Section.JBOGValueList[Count].Scales.GetVector3D();
                JBOG_Values.JBOG_ITOP_RouteIDIndex = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JBOG_ITOP_RouteIDIndex);
                JBOG_Values.GOBJ_Specific_Setting = new KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue.JBOG_SpecificSetting
                {
                    Value0 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value0),
                    Value1 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value1),
                    Value2 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value2),
                    Value3 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value3),
                    Value4 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value4),
                    Value5 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value5),
                    Value6 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value6),
                    Value7 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value7)
                };
                JBOG_Values.JBOG_PresenceSetting = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JBOG_PresenceSetting);

                if (KMP_Version == 3100)
                {
                    JBOG_Values.JBOG_UnkByte2 = KMPs.KMPHelper.Byte2StringConverter.OBJIDStrToByteArray(JBOG_Section.JBOGValueList[Count].JBOG_UnkByte2);
                    JBOG_Values.JBOG_UnkByte3 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JBOG_UnkByte3);
                }
                if (KMP_Version == 3000)
                {
                    JBOG_Values.JBOG_UnkByte2 = new byte[] { 0x00, 0x00 };
                    JBOG_Values.JBOG_UnkByte3 = new ushort();
                }

                JBOG_Value_List.Add(JBOG_Values);
            }

            JBOG.JBOGValue_List = JBOG_Value_List;

            return JBOG;
        }

        public static KMPs.KMPFormat.KMPSection.ITOP_Section ToITOP_Section(KMPPropertyGridSettings.ITOP_Section ITOP_Section)
        {
            KMPs.KMPFormat.KMPSection.ITOP_Section ITOP = new KMPs.KMPFormat.KMPSection.ITOP_Section
            {
                ITOPHeader = new char[] { 'I', 'T', 'O', 'P' },
                ITOP_NumberOfRoute = Convert.ToUInt16(ITOP_Section.ITOP_RouteList.Count),
                ITOP_NumberOfPoint = Convert.ToUInt16(ITOP_Section.ITOP_RouteList.Select(x => x.ITOP_PointList.Count).Sum()),
                ITOP_Route_List = null
            };

            List<KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route> ITOP_Route_List = new List<KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route>();

            for (int ITOPRouteCount = 0; ITOPRouteCount < ITOP.ITOP_NumberOfRoute; ITOPRouteCount++)
            {
                KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route ITOP_Routes = new KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route
                {
                    ITOP_Route_NumOfPoint = Convert.ToUInt16(ITOP_Section.ITOP_RouteList[ITOPRouteCount].ITOP_PointList.Count),
                    ITOP_RoopSetting = Convert.ToByte(ITOP_Section.ITOP_RouteList[ITOPRouteCount].ITOP_Roop),
                    ITOP_SmoothSetting = Convert.ToByte(ITOP_Section.ITOP_RouteList[ITOPRouteCount].ITOP_Smooth),
                    ITOP_Point_List = null
                };

                List<KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point> ITOP_Point_List = new List<KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point>();

                for (int ITOP_PointCount = 0; ITOP_PointCount < ITOP_Routes.ITOP_Route_NumOfPoint; ITOP_PointCount++)
                {
                    KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point ITOP_Points = new KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point
                    {
                        ITOP_Point_Position = ITOP_Section.ITOP_RouteList[ITOPRouteCount].ITOP_PointList[ITOP_PointCount].Positions.GetVector3D(),
                        ITOP_Point_RouteSpeed = Convert.ToUInt16(ITOP_Section.ITOP_RouteList[ITOPRouteCount].ITOP_PointList[ITOP_PointCount].ITOP_Point_RouteSpeed),
                        ITOP_PointSetting2 = Convert.ToUInt16(ITOP_Section.ITOP_RouteList[ITOPRouteCount].ITOP_PointList[ITOP_PointCount].ITOP_PointSetting2)
                    };

                    ITOP_Point_List.Add(ITOP_Points);
                }

                ITOP_Routes.ITOP_Point_List = ITOP_Point_List;

                ITOP_Route_List.Add(ITOP_Routes);
            }

            ITOP.ITOP_Route_List = ITOP_Route_List;

            return ITOP;
        }

        public static KMPs.KMPFormat.KMPSection.AERA_Section ToAERA_section(KMPPropertyGridSettings.AERA_Section AERA_Section)
        {
            KMPs.KMPFormat.KMPSection.AERA_Section AERA = new KMPs.KMPFormat.KMPSection.AERA_Section
            {
                AERAHeader = new char[] { 'A', 'E', 'R', 'A' },
                NumOfEntries = Convert.ToUInt16(AERA_Section.AERAValueList.Count),
                AdditionalValue = 0,
                AERAValue_List = null
            };

            List<KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue> AERA_Value_List = new List<KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue>();

            for (int Count = 0; Count < AERA_Section.AERAValueList.Count; Count++)
            {
                double RX = HTK_3DES.TSRSystem.AngleToRadian(AERA_Section.AERAValueList[Count].Rotations.X);
                double RY = HTK_3DES.TSRSystem.AngleToRadian(AERA_Section.AERAValueList[Count].Rotations.Y);
                double RZ = HTK_3DES.TSRSystem.AngleToRadian(AERA_Section.AERAValueList[Count].Rotations.Z);

                KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue AERA_Values = new KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue
                {
                    AreaMode = Convert.ToByte(AERA_Section.AERAValueList[Count].AreaModeSettings.AreaModeValue),
                    AreaType = Convert.ToByte(AERA_Section.AERAValueList[Count].AreaType),
                    AERA_EMACIndex = Convert.ToByte(AERA_Section.AERAValueList[Count].AERA_EMACIndex),
                    Priority = Convert.ToByte(AERA_Section.AERAValueList[Count].Priority),
                    AERA_Position = AERA_Section.AERAValueList[Count].Positions.GetVector3D(),
                    AERA_Rotation = new Vector3D(RX, RY, RZ),
                    AERA_Scale = AERA_Section.AERAValueList[Count].Scales.GetVector3D(),
                    AERA_Setting1 = Convert.ToUInt16(AERA_Section.AERAValueList[Count].AERA_Setting1),
                    AERA_Setting2 = Convert.ToUInt16(AERA_Section.AERAValueList[Count].AERA_Setting2),
                    RouteID = Convert.ToByte(AERA_Section.AERAValueList[Count].RouteID),
                    EnemyID = Convert.ToByte(AERA_Section.AERAValueList[Count].EnemyID),
                    AERA_UnkByte4 = Convert.ToUInt16(AERA_Section.AERAValueList[Count].AERA_UnkByte4)
                };

                AERA_Value_List.Add(AERA_Values);
            }

            AERA.AERAValue_List = AERA_Value_List;

            return AERA;
        }

        public static KMPs.KMPFormat.KMPSection.EMAC_Section ToEMAC_Section(KMPPropertyGridSettings.EMAC_Section EMAC_Section)
        {
            KMPs.KMPFormat.KMPSection.EMAC_Section EMAC = new KMPs.KMPFormat.KMPSection.EMAC_Section
            {
                EMACHeader = new char[] { 'E', 'M', 'A', 'C' },
                NumOfEntries = Convert.ToUInt16(EMAC_Section.EMACValueList.Count),
                AdditionalValue = 65535, //0xFFFF
                EMACValue_List = null
            };

            List<KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue> EMAC_Value_List = new List<KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue>();

            for (int EMACCount = 0; EMACCount < EMAC_Section.EMACValueList.Count; EMACCount++)
            {
                double RX = HTK_3DES.TSRSystem.AngleToRadian(EMAC_Section.EMACValueList[EMACCount].Rotations.X);
                double RY = HTK_3DES.TSRSystem.AngleToRadian(EMAC_Section.EMACValueList[EMACCount].Rotations.Y);
                double RZ = HTK_3DES.TSRSystem.AngleToRadian(EMAC_Section.EMACValueList[EMACCount].Rotations.Z);

                KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue EMAC_Values = new KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue
                {
                    CameraType = Convert.ToByte(EMAC_Section.EMACValueList[EMACCount].CameraType),
                    NextCameraIndex = Convert.ToByte(EMAC_Section.EMACValueList[EMACCount].NextCameraIndex),
                    EMAC_NextVideoIndex = Convert.ToByte(EMAC_Section.EMACValueList[EMACCount].EMAC_NextVideoIndex),
                    EMAC_ITOP_CameraIndex = Convert.ToByte(EMAC_Section.EMACValueList[EMACCount].EMAC_ITOP_CameraIndex),
                    RouteSpeed = Convert.ToUInt16(EMAC_Section.EMACValueList[EMACCount].SpeedSettings.RouteSpeed),
                    FOVSpeed = Convert.ToUInt16(EMAC_Section.EMACValueList[EMACCount].SpeedSettings.FOVSpeed),
                    ViewpointSpeed = Convert.ToUInt16(EMAC_Section.EMACValueList[EMACCount].SpeedSettings.ViewpointSpeed),
                    EMAC_StartFlag = Convert.ToByte(EMAC_Section.EMACValueList[EMACCount].EMAC_StartFlag),
                    EMAC_VideoFlag = Convert.ToByte(EMAC_Section.EMACValueList[EMACCount].EMAC_VideoFlag),
                    EMAC_Position = EMAC_Section.EMACValueList[EMACCount].Positions.GetVector3D(),
                    EMAC_Rotation = new Vector3D(RX, RY, RZ),
                    FOVAngle_Start = Convert.ToSingle(EMAC_Section.EMACValueList[EMACCount].FOVAngleSettings.FOVAngle_Start),
                    FOVAngle_End = Convert.ToSingle(EMAC_Section.EMACValueList[EMACCount].FOVAngleSettings.FOVAngle_End),
                    Viewpoint_Start = EMAC_Section.EMACValueList[EMACCount].Viewpoint_Start.GetVector3D(),
                    Viewpoint_Destination = EMAC_Section.EMACValueList[EMACCount].Viewpoint_Destination.GetVector3D(),
                    Camera_Active_Time = Convert.ToSingle(EMAC_Section.EMACValueList[EMACCount].Camera_Active_Time)
                };

                EMAC_Value_List.Add(EMAC_Values);
            }

            EMAC.EMACValue_List = EMAC_Value_List;

            return EMAC;
        }

        public static KMPs.KMPFormat.KMPSection.TPGJ_Section ToTPGJ_Section(KMPPropertyGridSettings.TPGJ_Section TPGJ_Section)
        {
            KMPs.KMPFormat.KMPSection.TPGJ_Section TPGJ = new KMPs.KMPFormat.KMPSection.TPGJ_Section
            {
                TPGJHeader = new char[] { 'T', 'P', 'G', 'J' },
                NumOfEntries = Convert.ToUInt16(TPGJ_Section.TPGJValueList.Count),
                AdditionalValue = 0,
                TPGJValue_List = null
            };

            List<KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue> TPGJ_Value_List = new List<KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue>();

            for (int TPGJCount = 0; TPGJCount < TPGJ_Section.TPGJValueList.Count; TPGJCount++)
            {
                double RX = HTK_3DES.TSRSystem.AngleToRadian(TPGJ_Section.TPGJValueList[TPGJCount].Rotations.X);
                double RY = HTK_3DES.TSRSystem.AngleToRadian(TPGJ_Section.TPGJValueList[TPGJCount].Rotations.Y);
                double RZ = HTK_3DES.TSRSystem.AngleToRadian(TPGJ_Section.TPGJValueList[TPGJCount].Rotations.Z);

                KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue TPGJ_Values = new KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue
                {
                    TPGJ_Position = TPGJ_Section.TPGJValueList[TPGJCount].Positions.GetVector3D(),
                    TPGJ_Rotation = new Vector3D(RX, RY, RZ),
                    TPGJ_RespawnID = Convert.ToUInt16(TPGJ_Section.TPGJValueList[TPGJCount].TPGJ_RespawnID),
                    TPGJ_UnkBytes1 = Convert.ToUInt16(TPGJ_Section.TPGJValueList[TPGJCount].TPGJ_UnkBytes1),
                };

                TPGJ_Value_List.Add(TPGJ_Values);
            }

            TPGJ.TPGJValue_List = TPGJ_Value_List;

            return TPGJ;
        }

        public static KMPs.KMPFormat.KMPSection.TPNC_Section ToTPNC_Section()
        {
            //TPNC(Unused Section)
            KMPs.KMPFormat.KMPSection.TPNC_Section TPNC = new KMPs.KMPFormat.KMPSection.TPNC_Section
            {
                TPNCHeader = new char[] { 'T', 'P', 'N', 'C' },
                NumOfEntries = 0,
                AdditionalValue = 0
            };

            return TPNC;
        }

        public static KMPs.KMPFormat.KMPSection.TPSM_Section ToTPSM_Section()
        {
            //TPSM(Unused Section)
            KMPs.KMPFormat.KMPSection.TPSM_Section TPSM = new KMPs.KMPFormat.KMPSection.TPSM_Section
            {
                TPSMHeader = new char[] { 'T', 'P', 'S', 'M' },
                NumOfEntries = 0,
                AdditionalValue = 0
            };

            return TPSM;
        }

        public static KMPs.KMPFormat.KMPSection.IGTS_Section ToIGTS_Section(KMPPropertyGridSettings.IGTS_Section IGTS_Section)
        {
            KMPs.KMPFormat.KMPSection.IGTS_Section IGTS = new KMPs.KMPFormat.KMPSection.IGTS_Section
            {
                IGTSHeader = new char[] { 'I', 'G', 'T', 'S' },
                Unknown1 = IGTS_Section.Unknown1,
                LapCount = IGTS_Section.LapCount,
                PolePosition = IGTS_Section.PolePosition,
                Unknown2 = IGTS_Section.Unknown2,
                Unknown3 = IGTS_Section.Unknown3,
                RGBAColor = new KMPs.KMPFormat.KMPSection.IGTS_Section.RGBA
                {
                    R = IGTS_Section.RGBAColor.R,
                    G = IGTS_Section.RGBAColor.G,
                    B = IGTS_Section.RGBAColor.B,
                    A = IGTS_Section.RGBAColor.A
                },
                FlareAlpha = IGTS_Section.FlareAlpha
            };

            return IGTS;
        }

        public static KMPs.KMPFormat.KMPSection.SROC_Section ToSROC_Section()
        {
            //SROC(Unused Section)
            KMPs.KMPFormat.KMPSection.SROC_Section SROC = new KMPs.KMPFormat.KMPSection.SROC_Section
            {
                SROCHeader = new char[] { 'S', 'R', 'O', 'C' },
                NumOfEntries = 0,
                AdditionalValue = 0
            };

            return SROC;
        }

        public class HPLG_TPLGSection
        {
            public class HPLG_TPLGData
            {
                public KMPs.KMPFormat.KMPSection.HPLG_Section HPLG_Section;
                public KMPs.KMPFormat.KMPSection.TPLG_Section TPLG_Section;

                public HPLG_TPLGData(KMPs.KMPFormat.KMPSection.HPLG_Section HPLG, KMPs.KMPFormat.KMPSection.TPLG_Section TPLG)
                {
                    HPLG_Section = HPLG;
                    TPLG_Section = TPLG;
                }
            }

            public static HPLG_TPLGData ToHPLG_TPLG_Section(KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section)
            {
                HPLG_TPLGData hPLG_TPLGData = null;

                if (HPLG_TPLG_Section.HPLGValueList.Count != 0)
                {
                    List<KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue> TPLG_Values_List = new List<KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue>();
                    List<KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue> HPLG_Values_List = new List<KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue>();

                    int StartPoint = 0;
                    for (int HPLGCount = 0; HPLGCount < HPLG_TPLG_Section.HPLGValueList.Count; HPLGCount++)
                    {
                        KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue HPLG_Values = new KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue
                        {
                            HPLG_StartPoint = Convert.ToByte(StartPoint),
                            HPLG_Length = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].TPLGValueList.Count),
                            HPLG_PreviewGroup = new KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue.HPLG_PreviewGroups
                            {
                                Prev0 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_PreviewGroup.Prev0),
                                Prev1 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_PreviewGroup.Prev1),
                                Prev2 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_PreviewGroup.Prev2),
                                Prev3 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_PreviewGroup.Prev3),
                                Prev4 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_PreviewGroup.Prev4),
                                Prev5 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_PreviewGroup.Prev5),
                            },
                            HPLG_NextGroup = new KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue.HPLG_NextGroups
                            {
                                Next0 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_NextGroup.Next0),
                                Next1 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_NextGroup.Next1),
                                Next2 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_NextGroup.Next2),
                                Next3 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_NextGroup.Next3),
                                Next4 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_NextGroup.Next4),
                                Next5 = Convert.ToByte(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_NextGroup.Next5),
                            },
                            RouteSetting = Convert.ToUInt32(HPLG_TPLG_Section.HPLGValueList[HPLGCount].RouteSettings.RouteSettingValue),
                            HPLG_UnkBytes2 = Convert.ToUInt32(HPLG_TPLG_Section.HPLGValueList[HPLGCount].HPLG_UnkBytes2)
                        };

                        HPLG_Values_List.Add(HPLG_Values);

                        for (int TPLGCount = 0; TPLGCount < HPLG_TPLG_Section.HPLGValueList[HPLGCount].TPLGValueList.Count; TPLGCount++)
                        {
                            KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue TPLG_Values = new KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue
                            {
                                TPLG_Position = HPLG_TPLG_Section.HPLGValueList[HPLGCount].TPLGValueList[TPLGCount].Positions.GetVector3D(),
                                TPLG_PointScaleValue = Convert.ToSingle(HPLG_TPLG_Section.HPLGValueList[HPLGCount].TPLGValueList[TPLGCount].TPLG_PointScaleValue),
                                TPLG_UnkBytes1 = HPLG_TPLG_Section.HPLGValueList[HPLGCount].TPLGValueList[TPLGCount].TPLG_UnkBytes1,
                                TPLG_UnkBytes2 = Convert.ToUInt16(HPLG_TPLG_Section.HPLGValueList[HPLGCount].TPLGValueList[TPLGCount].TPLG_UnkBytes2)
                            };

                            TPLG_Values_List.Add(TPLG_Values);

                            StartPoint++;
                        }
                    }

                    KMPs.KMPFormat.KMPSection.TPLG_Section TPLG = new KMPs.KMPFormat.KMPSection.TPLG_Section
                    {
                        TPLGHeader = new char[] { 'T', 'P', 'L', 'G' },
                        NumOfEntries = Convert.ToUInt16(TPLG_Values_List.Count),
                        AdditionalValue = 0,
                        TPLGValue_List = TPLG_Values_List
                    };

                    KMPs.KMPFormat.KMPSection.HPLG_Section HPLG = new KMPs.KMPFormat.KMPSection.HPLG_Section
                    {
                        HPLGHeader = new char[] { 'H', 'P', 'L', 'G' },
                        NumOfEntries = Convert.ToUInt16(HPLG_Values_List.Count),
                        AdditionalValue = 0,
                        HPLGValue_List = HPLG_Values_List
                    };

                    hPLG_TPLGData = new HPLG_TPLGData(HPLG, TPLG);
                }
                if (HPLG_TPLG_Section.HPLGValueList.Count == 0)
                {
                    KMPs.KMPFormat.KMPSection.TPLG_Section TPLG = new KMPs.KMPFormat.KMPSection.TPLG_Section
                    {
                        TPLGHeader = new char[] { 'T', 'P', 'L', 'G' },
                        NumOfEntries = 0,
                        AdditionalValue = 0,
                        TPLGValue_List = new List<KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue>()
                    };

                    KMPs.KMPFormat.KMPSection.HPLG_Section HPLG = new KMPs.KMPFormat.KMPSection.HPLG_Section
                    {
                        HPLGHeader = new char[] { 'H', 'P', 'L', 'G' },
                        NumOfEntries = 0,
                        AdditionalValue = 0,
                        HPLGValue_List = new List<KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue>()
                    };

                    hPLG_TPLGData = new HPLG_TPLGData(HPLG, TPLG);
                }

                return hPLG_TPLGData;
            }
        }
    }

    public class ObjFlowXmlPropertyGridSetting
    {
        public List<ObjFlow> ObjFlows_List = new List<ObjFlow>();
        public List<ObjFlow> ObjFlowsList { get => ObjFlows_List; set => ObjFlows_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class ObjFlow
        {
			public string ObjectID { get; set; }
			public string ObjectName { get; set; }
			public string Path { get; set; }
            public bool UseKCL { get; set; }
            public string ObjectType { get; set; }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Common Commons { get; set; } = new Common();
            public class Common
            {
                public string ColType { get; set; }
                public string PathType { get; set; }
                public string ModelSetting { get; set; }
                public string Unknown1 { get; set; }

                public override string ToString()
                {
                    return "Common";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public LOD_Setting LODSetting { get; set; } = new LOD_Setting();
            public class LOD_Setting
            {
                public int LOD { get; set; }
                public int LODHPoly { get; set; }
                public int LODLPoly { get; set; }
                public int LODDef { get; set; }

                public override string ToString()
                {
                    return "LODSetting";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Scale Scales { get; set; } = new Scale();
            public class Scale
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

                public Scale()
                {
                    _X = 0;
                    _Y = 0;
                    _Z = 0;
                }

                public override string ToString()
                {
                    return "Scale";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Name Names { get; set; } = new Name();
            public class Name
            {
                public string Main { get; set; }
                public string Sub { get; set; }

                public override string ToString()
                {
                    return "Name";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Default_Values DefaultValues { get; set; } = new Default_Values();
            public class Default_Values
            {
                public List<Value> Values_List = new List<Value>();
                public List<Value> ValuesList { get => Values_List; set => Values_List = value; }
                [TypeConverter(typeof(CustomSortTypeConverter))]
                public class Value
                {
                    public int DefaultObjectValue { get; set; }
                    public string Description { get; set; }

                    public override string ToString()
                    {
                        return "Value";
                    }
                }

                public override string ToString()
                {
                    return "DefaultValue";
                }
            }

            public override string ToString()
            {
                return ObjectName + " [" + ObjectID + "]";
            }
        }
    }

    public class CustomSortTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection PDC = TypeDescriptor.GetProperties(value, attributes);

            Type type = value.GetType();

            List<string> list = type.GetProperties().Select(x => x.Name).ToList();

            return PDC.Sort(list.ToArray());
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    public class CustomExpandableObjectSortTypeConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection PDC = TypeDescriptor.GetProperties(value, attributes);

            Type type = value.GetType();

            List<string> list = type.GetProperties().Select(x => x.Name).ToList();

            return PDC.Sort(list.ToArray());
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
