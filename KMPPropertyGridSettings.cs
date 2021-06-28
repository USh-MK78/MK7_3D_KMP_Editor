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

namespace MK7_KMP_Editor_For_PG_
{
    public class KMPPropertyGridSettings
    {
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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
                    private string _X = "";
                    [Editor(typeof(CustomRotationEditor), typeof(UITypeEditor))]
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    [Editor(typeof(CustomRotationEditor), typeof(UITypeEditor))]
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    [Editor(typeof(CustomRotationEditor), typeof(UITypeEditor))]
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
                    }

                    public override string ToString()
                    {
                        return "Rotate";
                    }
                }

                public ushort Player_Index { get; set; }
                public ushort TPTK_UnkBytes { get; set; }

                public override string ToString()
                {
                    return "Kart Point " + ID;
                }
            }
        }

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
                        private string _X = "";
                        public string X
                        {
                            get { return _X; }
                            set => _X = value == "" || value == null ? "0" : value;
                        }

                        private string _Y = "";
                        public string Y
                        {
                            get { return _Y; }
                            set => _Y = value == "" || value == null ? "0" : value;
                        }

                        private string _Z = "";
                        public string Z
                        {
                            get { return _Z; }
                            set => _Z = value == "" || value == null ? "0" : value;
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

                        public bool UnknownFlag
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

                    public override string ToString()
                    {
                        return "EnemyRoute Point " + ID;
                    }
                }

                public override string ToString()
                {
                    return "Enemy Route " + GroupID;
                }
            }
        }

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
                        private string _X = "";
                        public string X
                        {
                            get { return _X; }
                            set => _X = value == "" || value == null ? "0" : value;
                        }

                        private string _Y = "";
                        public string Y
                        {
                            get { return _Y; }
                            set => _Y = value == "" || value == null ? "0" : value;
                        }

                        private string _Z = "";
                        public string Z
                        {
                            get { return _Z; }
                            set => _Z = value == "" || value == null ? "0" : value;
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

                    public override string ToString()
                    {
                        return "ItemRoute Point " + ID;
                    }
                }

                public override string ToString()
                {
                    return "ItemRoute " + GroupID;
                }
            }
        }

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
                        private string _X = "";
                        public string X
                        {
                            get { return _X; }
                            set => _X = value == "" || value == null ? "0" : value;
                        }

                        private string _Y = "";
                        public string Y
                        {
                            get { return _Y; }
                            set => _Y = value == "" || value == null ? "0" : value;
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
                        private string _X = "";
                        public string X
                        {
                            get { return _X; }
                            set => _X = value == "" || value == null ? "0" : value;
                        }

                        private string _Y = "";
                        public string Y
                        {
                            get { return _Y; }
                            set => _Y = value == "" || value == null ? "0" : value;
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
                    public byte TPKC_UnkBytes1 { get; set; }
                    public byte TPKC_UnkBytes2 { get; set; }
                    public byte TPKC_UnkBytes3 { get; set; }
                    public byte TPKC_UnkBytes4 { get; set; }

                    public override string ToString()
                    {
                        return "CheckPoint Point " + ID;
                    }
                }

                public override string ToString()
                {
                    return "CheckPoint " + GroupID;
                }
            }
        }

        public class JBOG_section
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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

                    public override string ToString()
                    {
                        return "Obj Params";
                    }
                }
                public ushort JBOG_PresenceSetting { get; set; }
                public string JBOG_UnkByte2 { get; set; }
                public ushort JBOG_UnkByte3 { get; set; }

                public override string ToString()
                {
                    return "Object " + ID + " [" + "OBJID : " + ObjectID + "]";
                }
            }
        }

        public class ITOP_Section
        {
            public List<ITOP_Route> ITOP_Route_List = new List<ITOP_Route>();
            public List<ITOP_Route> ITOP_RouteList { get => ITOP_Route_List; set => ITOP_Route_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class ITOP_Route
            {
                [ReadOnly(true)]
                public int GroupID { get; set; }

                public byte ITOP_RouteSetting1 { get; set; }
                public byte ITOP_RouteSetting2 { get; set; }

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
                        private string _X = "";
                        public string X
                        {
                            get { return _X; }
                            set => _X = value == "" || value == null ? "0" : value;
                        }

                        private string _Y = "";
                        public string Y
                        {
                            get { return _Y; }
                            set => _Y = value == "" || value == null ? "0" : value;
                        }

                        private string _Z = "";
                        public string Z
                        {
                            get { return _Z; }
                            set => _Z = value == "" || value == null ? "0" : value;
                        }

                        public override string ToString()
                        {
                            return "Position";
                        }
                    }

                    public ushort ITOP_Point_RouteSpeed { get; set; }
                    public ushort ITOP_PointSetting2 { get; set; }

                    public override string ToString()
                    {
                        return "Point " + ID;
                    }
                }

                public override string ToString()
                {
                    return "Route " + GroupID;
                }
            }
        }

        public class AERA_Section
        {
            public List<AERAValue> AERAValue_List = new List<AERAValue>();
            public List<AERAValue> AERAValueList { get => AERAValue_List; set => AERAValue_List = value; }
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
                    }

                    public override string ToString()
                    {
                        return "Scale";
                    }
                }

                public ushort AERA_UnkByte1 { get; set; }
                public ushort AERA_UnkByte2 { get; set; }
                public byte RouteID { get; set; }
                public byte EnemyID { get; set; }
                public ushort AERA_UnkByte4 { get; set; }

                public override string ToString()
                {
                    return "Area " + ID;
                }
            }
        }

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
                public byte EMAC_UnkBytes1 { get; set; }
                public byte EMAC_ITOP_CameraIndex { get; set; }

                [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
                public SpeedSetting SpeedSettings { get; set; } = new SpeedSetting();
                public class SpeedSetting
                {
                    public ushort RouteSpeed { get; set; }
                    public ushort FOVSpeed { get; set; }
                    public ushort ViewpointSpeed { get; set; }

                    public override string ToString()
                    {
                        return "Speed";
                    }
                }

                public byte EMAC_UnkBytes2 { get; set; }
                public byte EMAC_UnkBytes3 { get; set; }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Position Positions { get; set; } = new Position();
                public class Position
                {
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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

                    public override string ToString()
                    {
                        return "FOV Angle";
                    }
                }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public ViewpointStart Viewpoint_Start { get; set; } = new ViewpointStart();
                public class ViewpointStart
                {
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
                    }

                    public override string ToString()
                    {
                        return "Viewpoint Destination";
                    }
                }

                public float Camera_Active_Time { get; set; }

                public override string ToString()
                {
                    return "Camera " + ID;
                }
            }
        }

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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
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
                    private string _X = "";
                    public string X
                    {
                        get { return _X; }
                        set => _X = value == "" || value == null ? "0" : value;
                    }

                    private string _Y = "";
                    public string Y
                    {
                        get { return _Y; }
                        set => _Y = value == "" || value == null ? "0" : value;
                    }

                    private string _Z = "";
                    public string Z
                    {
                        get { return _Z; }
                        set => _Z = value == "" || value == null ? "0" : value;
                    }

                    public override string ToString()
                    {
                        return "Rotation";
                    }
                }

                public ushort TPGJ_RespawnID { get; set; }
                public ushort TPGJ_UnkBytes1 { get; set; }

                public override string ToString()
                {
                    return "Jugem Point " + ID;
                }
            }
        }

        //TPNC = null

        //TPSM = null

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

                public override string ToString()
                {
                    return "RGBA Color";
                }
            }

            public uint FlareAlpha { get; set; }
        }

        //SROC = null

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
                        private string _X = "";
                        public string X
                        {
                            get { return _X; }
                            set => _X = value == "" || value == null ? "0" : value;
                        }

                        private string _Y = "";
                        public string Y
                        {
                            get { return _Y; }
                            set => _Y = value == "" || value == null ? "0" : value;
                        }

                        private string _Z = "";
                        public string Z
                        {
                            get { return _Z; }
                            set => _Z = value == "" || value == null ? "0" : value;
                        }

                        public override string ToString()
                        {
                            return "Position";
                        }
                    }

                    public float TPLG_PointScaleValue { get; set; }

                    public uint TPLG_UnkBytes1 { get; set; }
                    public uint TPLG_UnkBytes2 { get; set; }

                    public override string ToString()
                    {
                        return "Glide Point " + ID;
                    }
                }

                public override string ToString()
                {
                    return "Glide Route " + GroupID;
                }
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
                public string ObjID { get; set; }
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
                //public int X { get; set; }
                //public int Y { get; set; }
                //public int Z { get; set; }

                private string _X = "";
                public string X
                {
                    get { return _X; }
                    set => _X = value == "" || value == null ? "0" : value;
                }

                private string _Y = "";
                public string Y
                {
                    get { return _Y; }
                    set => _Y = value == "" || value == null ? "0" : value;
                }

                private string _Z = "";
                public string Z
                {
                    get { return _Z; }
                    set => _Z = value == "" || value == null ? "0" : value;
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

    //public class ValueRange
    //{
    //    public int _Value;
    //    public int Value
    //    {
    //        set => _Value = Math.Min(65536, Math.Max(-2, value));
    //        get => _Value;
    //    }
    //}

    public class CustomRotationEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _WinFormEditorService;

        //編集時にドロップダウンスタイルで表示
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _WinFormEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            TrackBar trackBar_Rotation = new TrackBar
            {
                Location = new System.Drawing.Point(5, 15),
                Minimum = -3142,
                Maximum = 3142,
                SmallChange = 0,
                Orientation = Orientation.Horizontal,
                TickFrequency = 1,
                LargeChange = 1,
                TickStyle = TickStyle.BottomRight,
                Value = Convert.ToInt32(Convert.ToDouble(value.ToString()) * 1000)
            };

            Label label_Radian = new Label
            {
                Location = new System.Drawing.Point(5, 60),
                Text = "Radian : " + value.ToString() + " \r\nAngle : " + Math.Round(Convert.ToSingle(value.ToString()) * (180 / Math.PI), 0, MidpointRounding.AwayFromZero)
            };

            //Add controls to the GroupBox to combine multiple controls into a single control.
            GroupBox groupBox = new GroupBox();
            groupBox.Width = 100;
            groupBox.Height = 100;
            groupBox.Controls.Add(trackBar_Rotation);
            groupBox.Controls.Add(label_Radian);

            trackBar_Rotation.Scroll += (object sender, EventArgs e) =>
            {
                string output = (float.Parse(trackBar_Rotation.Value.ToString()) / 1000).ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append(output);

                label_Radian.Text = "Radian : " + sb.ToString() + " \r\nAngle : " + Math.Round(Convert.ToSingle(sb.ToString()) * (180 / Math.PI), 0, MidpointRounding.AwayFromZero);
                label_Radian.Update();

                value = sb.ToString();
            };

            _WinFormEditorService.DropDownControl(groupBox);

            return value;
        }
    }
}
