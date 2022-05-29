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

                public override string ToString()
                {
                    return "Kart Point " + ID;
                }
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

        public JBOG_section JBOGSection { get; set; }
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
                        _X = 0;
                        _Y = 0;
                        _Z = 0;
                    }

                    public Scale(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
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
                        _X = 0;
                        _Y = 0;
                        _Z = 0;
                    }

                    public Scale(float X, float Y, float Z)
                    {
                        _X = X;
                        _Y = Y;
                        _Z = Z;
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

                public override string ToString()
                {
                    return "Area " + ID;
                }
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

                public override string ToString()
                {
                    return "Camera " + ID;
                }
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

                public override string ToString()
                {
                    return "Jugem Point " + ID;
                }
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

                public override string ToString()
                {
                    return "RGBA Color";
                }
            }

            public uint FlareAlpha { get; set; }
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

    public class PropertyGridClassConverter
    {
        public static List<KMPPropertyGridSettings.TPTK_Section.TPTKValue> ToTPTKValueList(KMPs.KMPFormat.KMPSection.TPTK_Section TPTK)
        {
            List<KMPPropertyGridSettings.TPTK_Section.TPTKValue> TPTKValues_List = new List<KMPPropertyGridSettings.TPTK_Section.TPTKValue>();

            for (int i = 0; i < TPTK.NumOfEntries; i++)
            {
                KMPPropertyGridSettings.TPTK_Section.TPTKValue tPTKValue = new KMPPropertyGridSettings.TPTK_Section.TPTKValue
                {
                    ID = i,
                    Position_Value = new KMPPropertyGridSettings.TPTK_Section.TPTKValue.Position
                    {
                        X = (float)TPTK.TPTKValue_List[i].TPTK_Position.X,
                        Y = (float)TPTK.TPTKValue_List[i].TPTK_Position.Y,
                        Z = (float)TPTK.TPTKValue_List[i].TPTK_Position.Z
                    },
                    Rotate_Value = new KMPPropertyGridSettings.TPTK_Section.TPTKValue.Rotation
                    {
                        X = HTK_3DES.TSRSystem.RadianToAngle(TPTK.TPTKValue_List[i].TPTK_Rotation.X),
                        Y = HTK_3DES.TSRSystem.RadianToAngle(TPTK.TPTKValue_List[i].TPTK_Rotation.Y),
                        Z = HTK_3DES.TSRSystem.RadianToAngle(TPTK.TPTKValue_List[i].TPTK_Rotation.Z)
                    },
                    Player_Index = TPTK.TPTKValue_List[i].Player_Index,
                    TPTK_UnkBytes = TPTK.TPTKValue_List[i].TPTK_UnkBytes
                };

                TPTKValues_List.Add(tPTKValue);
            }

            return TPTKValues_List;
        }

        public static List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> ToHPNEValueList(KMPs.KMPFormat.KMPSection.HPNE_Section HPNE, KMPs.KMPFormat.KMPSection.TPNE_Section TPNE)
        {
            KMPs.KMPHelper.FlagConverter.EnemyRoute EnemyRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.EnemyRoute();

            List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> HPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>();

            for (int i = 0; i < HPNE.NumOfEntries; i++)
            {
                KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue hPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue
                {
                    GroupID = i,
                    HPNEPreviewGroups = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.HPNE_PreviewGroups
                    {
                        Prev0 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev0,
                        Prev1 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev1,
                        Prev2 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev2,
                        Prev3 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev3,
                        Prev4 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev4,
                        Prev5 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev5,
                        Prev6 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev6,
                        Prev7 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev7,
                        Prev8 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev8,
                        Prev9 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev9,
                        Prev10 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev10,
                        Prev11 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev11,
                        Prev12 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev12,
                        Prev13 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev13,
                        Prev14 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev14,
                        Prev15 = HPNE.HPNEValue_List[i].HPNE_PreviewGroup.Prev15,
                    },
                    HPNENextGroups = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.HPNE_NextGroups
                    {
                        Next0 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next0,
                        Next1 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next1,
                        Next2 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next2,
                        Next3 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next3,
                        Next4 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next4,
                        Next5 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next5,
                        Next6 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next6,
                        Next7 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next7,
                        Next8 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next8,
                        Next9 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next9,
                        Next10 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next10,
                        Next11 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next11,
                        Next12 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next12,
                        Next13 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next13,
                        Next14 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next14,
                        Next15 = HPNE.HPNEValue_List[i].HPNE_NextGroup.Next15,
                    },
                    HPNE_UnkBytes1 = HPNE.HPNEValue_List[i].HPNE_UnkBytes1,
                    TPNEValueList = null
                };

                List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue> TPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue>();

                for (int Count = 0; Count < HPNE.HPNEValue_List[i].HPNE_Length; Count++)
                {
                    KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue tPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue
                    {
                        Group_ID = i,
                        ID = Count,
                        Positions = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.Position
                        {
                            X = (float)TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].TPNE_Position.X,
                            Y = (float)TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].TPNE_Position.Y,
                            Z = (float)TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].TPNE_Position.Z
                        },
                        Control = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Control,
                        MushSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.MushSetting
                        {
                            MushSettingValue = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].MushSetting
                        },
                        DriftSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.DriftSetting
                        {
                            DriftSettingValue = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].DriftSetting
                        },
                        FlagSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.FlagSetting
                        {
                            WideTurn = EnemyRouteFlagConverter.ConvertFlags(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.WideTurn),
                            NormalTurn = EnemyRouteFlagConverter.ConvertFlags(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.NormalTurn),
                            SharpTurn = EnemyRouteFlagConverter.ConvertFlags(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.SharpTurn),
                            TricksForbidden = EnemyRouteFlagConverter.ConvertFlags(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.TricksForbidden),
                            StickToRoute = EnemyRouteFlagConverter.ConvertFlags(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.StickToRoute),
                            BouncyMushSection = EnemyRouteFlagConverter.ConvertFlags(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.BouncyMushSection),
                            ForceDefaultSpeed = EnemyRouteFlagConverter.ConvertFlags(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.ForceDefaultSpeed),
                            NoPathSwitch = EnemyRouteFlagConverter.ConvertFlags(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.NoPathSwitch),
                        },
                        PathFindOptions = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.PathFindOption
                        {
                            PathFindOptionValue = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].PathFindOption
                        },
                        MaxSearchYOffset = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.MaxSearch_YOffset
                        {
                            MaxSearchYOffsetValue = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].MaxSearchYOffset
                        }
                    };

                    TPNEValues_List.Add(tPNEValue);
                }

                hPNEValue.TPNEValueList = TPNEValues_List;

                HPNEValues_List.Add(hPNEValue);
            }

            return HPNEValues_List;
        }

        public static List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> ToHPTIValueList(KMPs.KMPFormat.KMPSection.HPTI_Section HPTI, KMPs.KMPFormat.KMPSection.TPTI_Section TPTI)
        {
            List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> HPTIValues_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>();

            for (int HPTICount = 0; HPTICount < HPTI.NumOfEntries; HPTICount++)
            {
                KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue hPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue
                {
                    GroupID = HPTICount,
                    HPTI_PreviewGroup = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.HPTI_PreviewGroups
                    {
                        Prev0 = HPTI.HPTIValue_List[HPTICount].HPTI_PreviewGroup.Prev0,
                        Prev1 = HPTI.HPTIValue_List[HPTICount].HPTI_PreviewGroup.Prev1,
                        Prev2 = HPTI.HPTIValue_List[HPTICount].HPTI_PreviewGroup.Prev2,
                        Prev3 = HPTI.HPTIValue_List[HPTICount].HPTI_PreviewGroup.Prev3,
                        Prev4 = HPTI.HPTIValue_List[HPTICount].HPTI_PreviewGroup.Prev4,
                        Prev5 = HPTI.HPTIValue_List[HPTICount].HPTI_PreviewGroup.Prev5
                    },
                    HPTI_NextGroup = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.HPTI_NextGroups
                    {
                        Next0 = HPTI.HPTIValue_List[HPTICount].HPTI_NextGroup.Next0,
                        Next1 = HPTI.HPTIValue_List[HPTICount].HPTI_NextGroup.Next1,
                        Next2 = HPTI.HPTIValue_List[HPTICount].HPTI_NextGroup.Next2,
                        Next3 = HPTI.HPTIValue_List[HPTICount].HPTI_NextGroup.Next3,
                        Next4 = HPTI.HPTIValue_List[HPTICount].HPTI_NextGroup.Next4,
                        Next5 = HPTI.HPTIValue_List[HPTICount].HPTI_NextGroup.Next5
                    },
                    TPTIValueList = null
                };

                List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue> TPTIVales_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue>();

                for (int Count = 0; Count < HPTI.HPTIValue_List[HPTICount].HPTI_Length; Count++)
                {
                    KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue tPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue
                    {
                        Group_ID = HPTICount,
                        ID = Count,
                        TPTI_Positions = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.TPTI_Position
                        {
                            X = (float)TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_Position.X,
                            Y = (float)TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_Position.Y,
                            Z = (float)TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_Position.Z
                        },
                        TPTI_PointSize = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_PointSize,
                        GravityModeSettings = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.GravityModeSetting
                        {
                            GravityModeValue = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].GravityMode
                        },
                        PlayerScanRadiusSettings = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.PlayerScanRadiusSetting
                        {
                            PlayerScanRadiusValue = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].PlayerScanRadius
                        }
                    };

                    TPTIVales_List.Add(tPTIValue);
                }

                hPTIValue.TPTIValueList = TPTIVales_List;

                HPTIValues_List.Add(hPTIValue);
            }

            return HPTIValues_List;
        }

        public static List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue> ToHPKCValueList(KMPs.KMPFormat.KMPSection.HPKC_Section HPKC, KMPs.KMPFormat.KMPSection.TPKC_Section TPKC)
        {
            List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue> HPKCValues_List = new List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue>();

            for (int HPKCCount = 0; HPKCCount < HPKC.NumOfEntries; HPKCCount++)
            {
                KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue hPKCValue = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue
                {
                    GroupID = HPKCCount,
                    HPKC_PreviewGroup = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.HPKC_PreviewGroups
                    {
                        Prev0 = HPKC.HPKCValue_List[HPKCCount].HPKC_PreviewGroup.Prev0,
                        Prev1 = HPKC.HPKCValue_List[HPKCCount].HPKC_PreviewGroup.Prev1,
                        Prev2 = HPKC.HPKCValue_List[HPKCCount].HPKC_PreviewGroup.Prev2,
                        Prev3 = HPKC.HPKCValue_List[HPKCCount].HPKC_PreviewGroup.Prev3,
                        Prev4 = HPKC.HPKCValue_List[HPKCCount].HPKC_PreviewGroup.Prev4,
                        Prev5 = HPKC.HPKCValue_List[HPKCCount].HPKC_PreviewGroup.Prev5
                    },
                    HPKC_NextGroup = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.HPKC_NextGroups
                    {
                        Next0 = HPKC.HPKCValue_List[HPKCCount].HPKC_NextGroup.Next0,
                        Next1 = HPKC.HPKCValue_List[HPKCCount].HPKC_NextGroup.Next1,
                        Next2 = HPKC.HPKCValue_List[HPKCCount].HPKC_NextGroup.Next2,
                        Next3 = HPKC.HPKCValue_List[HPKCCount].HPKC_NextGroup.Next3,
                        Next4 = HPKC.HPKCValue_List[HPKCCount].HPKC_NextGroup.Next4,
                        Next5 = HPKC.HPKCValue_List[HPKCCount].HPKC_NextGroup.Next5
                    },
                    HPKC_UnkBytes1 = HPKC.HPKCValue_List[HPKCCount].HPKC_UnkBytes1,
                    TPKCValueList = null
                };

                List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue> TPKCValues_List = new List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue>();

                for (int Count = 0; Count < HPKC.HPKCValue_List[HPKCCount].HPKC_Length; Count++)
                {
                    KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue tPKCValue = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue
                    {
                        Group_ID = HPKCCount,
                        ID = Count,
                        Position_2D_Left = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue.Position2D_Left
                        {
                            X = (float)TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Left.X,
                            Y = (float)TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Left.Y
                        },
                        Position_2D_Right = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue.Position2D_Right
                        {
                            X = (float)TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Right.X,
                            Y = (float)TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Right.Y
                        },
                        TPKC_RespawnID = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_RespawnID,
                        TPKC_Checkpoint_Type = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_Checkpoint_Type,
                        TPKC_PreviousCheckPoint = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_PreviousCheckPoint,
                        TPKC_NextCheckPoint = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_NextCheckPoint,
                        TPKC_ClipID = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_ClipID,
                        TPKC_Section = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_Section,
                        TPKC_UnkBytes3 = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_UnkBytes3,
                        TPKC_UnkBytes4 = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_UnkBytes4
                    };

                    TPKCValues_List.Add(tPKCValue);
                }

                hPKCValue.TPKCValueList = TPKCValues_List;

                HPKCValues_List.Add(hPKCValue);
            }

            return HPKCValues_List;
        }

        public static List<KMPPropertyGridSettings.JBOG_section.JBOGValue> ToJBOGValueList(KMPs.KMPFormat.KMPSection.JBOG_Section JBOG, List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDB)
        {
            List<KMPPropertyGridSettings.JBOG_section.JBOGValue> JBOGValues_List = new List<KMPPropertyGridSettings.JBOG_section.JBOGValue>();

            for (int Count = 0; Count < JBOG.NumOfEntries; Count++)
            {
                string Name = ObjFlowDB.Find(x => x.ObjectID == BitConverter.ToString(JBOG.JBOGValue_List[Count].ObjectID.Reverse().ToArray()).Replace("-", string.Empty)).ObjectName;

                KMPPropertyGridSettings.JBOG_section.JBOGValue jBOGValue = new KMPPropertyGridSettings.JBOG_section.JBOGValue
                {
                    ID = Count,
                    ObjectName = Name,
                    ObjectID = BitConverter.ToString(JBOG.JBOGValue_List[Count].ObjectID.Reverse().ToArray()).Replace("-", string.Empty),
                    JBOG_UnkByte1 = BitConverter.ToString(JBOG.JBOGValue_List[Count].JBOG_UnkByte1.Reverse().ToArray()).Replace("-", string.Empty),
                    Positions = new KMPPropertyGridSettings.JBOG_section.JBOGValue.Position
                    {
                        X = (float)JBOG.JBOGValue_List[Count].JBOG_Position.X,
                        Y = (float)JBOG.JBOGValue_List[Count].JBOG_Position.Y,
                        Z = (float)JBOG.JBOGValue_List[Count].JBOG_Position.Z
                    },
                    Rotations = new KMPPropertyGridSettings.JBOG_section.JBOGValue.Rotation
                    {
                        X = HTK_3DES.TSRSystem.RadianToAngle(JBOG.JBOGValue_List[Count].JBOG_Rotation.X),
                        Y = HTK_3DES.TSRSystem.RadianToAngle(JBOG.JBOGValue_List[Count].JBOG_Rotation.Y),
                        Z = HTK_3DES.TSRSystem.RadianToAngle(JBOG.JBOGValue_List[Count].JBOG_Rotation.Z)
                    },
                    Scales = new KMPPropertyGridSettings.JBOG_section.JBOGValue.Scale
                    {
                        X = (float)JBOG.JBOGValue_List[Count].JBOG_Scale.X,
                        Y = (float)JBOG.JBOGValue_List[Count].JBOG_Scale.Y,
                        Z = (float)JBOG.JBOGValue_List[Count].JBOG_Scale.Z
                    },
                    JBOG_ITOP_RouteIDIndex = JBOG.JBOGValue_List[Count].JBOG_ITOP_RouteIDIndex,
                    JOBJ_Specific_Setting = new KMPPropertyGridSettings.JBOG_section.JBOGValue.JBOG_SpecificSetting
                    {
                        Value0 = JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value0,
                        Value1 = JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value1,
                        Value2 = JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value2,
                        Value3 = JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value3,
                        Value4 = JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value4,
                        Value5 = JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value5,
                        Value6 = JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value6,
                        Value7 = JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value7
                    },
                    JBOG_PresenceSetting = JBOG.JBOGValue_List[Count].JBOG_PresenceSetting,
                    JBOG_UnkByte2 = BitConverter.ToString(JBOG.JBOGValue_List[Count].JBOG_UnkByte2.Reverse().ToArray()).Replace("-", string.Empty),
                    JBOG_UnkByte3 = JBOG.JBOGValue_List[Count].JBOG_UnkByte3
                };

                JBOGValues_List.Add(jBOGValue);
            }

            return JBOGValues_List;
        }

        public static List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route> ToITOPValueList(KMPs.KMPFormat.KMPSection.ITOP_Section ITOP)
        {
            List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route> ITOPRoutes_List = new List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route>();

            for (int ITOP_RoutesCount = 0; ITOP_RoutesCount < ITOP.ITOP_NumberOfRoute; ITOP_RoutesCount++)
            {
                KMPPropertyGridSettings.ITOP_Section.ITOP_Route ITOPRoute = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route
                {
                    GroupID = ITOP_RoutesCount,
                    ITOP_Roop = ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_RoopSetting,
                    ITOP_Smooth = ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_SmoothSetting,
                    ITOP_PointList = null
                };

                List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point> ITOPPoints_List = new List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point>();

                for (int ITOP_PointsCount = 0; ITOP_PointsCount < ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Route_NumOfPoint; ITOP_PointsCount++)
                {
                    KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point ITOPPoint = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point
                    {
                        GroupID = ITOP_RoutesCount,
                        ID = ITOP_PointsCount,
                        Positions = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point.Position
                        {
                            X = (float)ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position.X,
                            Y = (float)ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position.Y,
                            Z = (float)ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position.Z
                        },
                        ITOP_Point_RouteSpeed = ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_RouteSpeed,
                        ITOP_PointSetting2 = ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_PointSetting2
                    };

                    ITOPPoints_List.Add(ITOPPoint);

                }

                ITOPRoute.ITOP_PointList = ITOPPoints_List;
                ITOPRoutes_List.Add(ITOPRoute);
            }

            return ITOPRoutes_List;
        }

        public static List<KMPPropertyGridSettings.AERA_Section.AERAValue> ToAERAValueList(KMPs.KMPFormat.KMPSection.AERA_Section AERA)
        {
            List<KMPPropertyGridSettings.AERA_Section.AERAValue> AERAValues_List = new List<KMPPropertyGridSettings.AERA_Section.AERAValue>();

            for (int AERACount = 0; AERACount < AERA.NumOfEntries; AERACount++)
            {
                KMPPropertyGridSettings.AERA_Section.AERAValue AERAValue = new KMPPropertyGridSettings.AERA_Section.AERAValue
                {
                    ID = AERACount,
                    AreaModeSettings = new KMPPropertyGridSettings.AERA_Section.AERAValue.AreaModeSetting
                    {
                        AreaModeValue = AERA.AERAValue_List[AERACount].AreaMode
                    },
                    AreaType = AERA.AERAValue_List[AERACount].AreaType,
                    AERA_EMACIndex = AERA.AERAValue_List[AERACount].AERA_EMACIndex,
                    Priority = AERA.AERAValue_List[AERACount].Priority,
                    Positions = new KMPPropertyGridSettings.AERA_Section.AERAValue.Position
                    {
                        X = (float)AERA.AERAValue_List[AERACount].AERA_Position.X,
                        Y = (float)AERA.AERAValue_List[AERACount].AERA_Position.Y,
                        Z = (float)AERA.AERAValue_List[AERACount].AERA_Position.Z
                    },
                    Rotations = new KMPPropertyGridSettings.AERA_Section.AERAValue.Rotation
                    {
                        X = HTK_3DES.TSRSystem.RadianToAngle(AERA.AERAValue_List[AERACount].AERA_Rotation.X),
                        Y = HTK_3DES.TSRSystem.RadianToAngle(AERA.AERAValue_List[AERACount].AERA_Rotation.Y),
                        Z = HTK_3DES.TSRSystem.RadianToAngle(AERA.AERAValue_List[AERACount].AERA_Rotation.Z)
                    },
                    Scales = new KMPPropertyGridSettings.AERA_Section.AERAValue.Scale
                    {
                        X = (float)AERA.AERAValue_List[AERACount].AERA_Scale.X,
                        Y = (float)AERA.AERAValue_List[AERACount].AERA_Scale.Y,
                        Z = (float)AERA.AERAValue_List[AERACount].AERA_Scale.Z
                    },
                    AERA_Setting1 = AERA.AERAValue_List[AERACount].AERA_Setting1,
                    AERA_Setting2 = AERA.AERAValue_List[AERACount].AERA_Setting2,
                    RouteID = AERA.AERAValue_List[AERACount].RouteID,
                    EnemyID = AERA.AERAValue_List[AERACount].EnemyID,
                    AERA_UnkByte4 = AERA.AERAValue_List[AERACount].AERA_UnkByte4
                };

                AERAValues_List.Add(AERAValue);
            }

            return AERAValues_List;
        }

        public static List<KMPPropertyGridSettings.EMAC_Section.EMACValue> ToEMACValueList(KMPs.KMPFormat.KMPSection.EMAC_Section EMAC)
        {
            List<KMPPropertyGridSettings.EMAC_Section.EMACValue> EMACValues_List = new List<KMPPropertyGridSettings.EMAC_Section.EMACValue>();

            for (int EMACCount = 0; EMACCount < EMAC.NumOfEntries; EMACCount++)
            {
                KMPPropertyGridSettings.EMAC_Section.EMACValue EMACValue = new KMPPropertyGridSettings.EMAC_Section.EMACValue
                {
                    ID = EMACCount,
                    CameraType = EMAC.EMACValue_List[EMACCount].CameraType,
                    NextCameraIndex = EMAC.EMACValue_List[EMACCount].NextCameraIndex,
                    EMAC_NextVideoIndex = EMAC.EMACValue_List[EMACCount].EMAC_NextVideoIndex,
                    EMAC_ITOP_CameraIndex = EMAC.EMACValue_List[EMACCount].EMAC_ITOP_CameraIndex,
                    SpeedSettings = new KMPPropertyGridSettings.EMAC_Section.EMACValue.SpeedSetting
                    {
                        RouteSpeed = EMAC.EMACValue_List[EMACCount].RouteSpeed,
                        FOVSpeed = EMAC.EMACValue_List[EMACCount].FOVSpeed,
                        ViewpointSpeed = EMAC.EMACValue_List[EMACCount].ViewpointSpeed
                    },
                    EMAC_StartFlag = EMAC.EMACValue_List[EMACCount].EMAC_StartFlag,
                    EMAC_VideoFlag = EMAC.EMACValue_List[EMACCount].EMAC_VideoFlag,
                    Positions = new KMPPropertyGridSettings.EMAC_Section.EMACValue.Position
                    {
                        X = (float)EMAC.EMACValue_List[EMACCount].EMAC_Position.X,
                        Y = (float)EMAC.EMACValue_List[EMACCount].EMAC_Position.Y,
                        Z = (float)EMAC.EMACValue_List[EMACCount].EMAC_Position.Z
                    },
                    Rotations = new KMPPropertyGridSettings.EMAC_Section.EMACValue.Rotation
                    {
                        X = HTK_3DES.TSRSystem.RadianToAngle(EMAC.EMACValue_List[EMACCount].EMAC_Rotation.X),
                        Y = HTK_3DES.TSRSystem.RadianToAngle(EMAC.EMACValue_List[EMACCount].EMAC_Rotation.Y),
                        Z = HTK_3DES.TSRSystem.RadianToAngle(EMAC.EMACValue_List[EMACCount].EMAC_Rotation.Z)
                    },
                    FOVAngleSettings = new KMPPropertyGridSettings.EMAC_Section.EMACValue.FOVAngleSetting
                    {
                        FOVAngle_Start = EMAC.EMACValue_List[EMACCount].FOVAngle_Start,
                        FOVAngle_End = EMAC.EMACValue_List[EMACCount].FOVAngle_End
                    },
                    Viewpoint_Start = new KMPPropertyGridSettings.EMAC_Section.EMACValue.ViewpointStart
                    {
                        X = (float)EMAC.EMACValue_List[EMACCount].Viewpoint_Start.X,
                        Y = (float)EMAC.EMACValue_List[EMACCount].Viewpoint_Start.Y,
                        Z = (float)EMAC.EMACValue_List[EMACCount].Viewpoint_Start.Z
                    },
                    Viewpoint_Destination = new KMPPropertyGridSettings.EMAC_Section.EMACValue.ViewpointDestination
                    {
                        X = (float)EMAC.EMACValue_List[EMACCount].Viewpoint_Destination.X,
                        Y = (float)EMAC.EMACValue_List[EMACCount].Viewpoint_Destination.Y,
                        Z = (float)EMAC.EMACValue_List[EMACCount].Viewpoint_Destination.Z
                    },
                    Camera_Active_Time = EMAC.EMACValue_List[EMACCount].Camera_Active_Time
                };

                EMACValues_List.Add(EMACValue);
            }

            return EMACValues_List;
        }

        public static List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue> ToTPGJValueList(KMPs.KMPFormat.KMPSection.TPGJ_Section TPGJ)
        {
            List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue> TPGJValues_List = new List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue>();

            for (int TPGJCount = 0; TPGJCount < TPGJ.NumOfEntries; TPGJCount++)
            {
                KMPPropertyGridSettings.TPGJ_Section.TPGJValue TPGJValue = new KMPPropertyGridSettings.TPGJ_Section.TPGJValue
                {
                    ID = TPGJCount,
                    TPGJ_RespawnID = TPGJ.TPGJValue_List[TPGJCount].TPGJ_RespawnID,
                    Positions = new KMPPropertyGridSettings.TPGJ_Section.TPGJValue.Position
                    {
                        X = (float)TPGJ.TPGJValue_List[TPGJCount].TPGJ_Position.X,
                        Y = (float)TPGJ.TPGJValue_List[TPGJCount].TPGJ_Position.Y,
                        Z = (float)TPGJ.TPGJValue_List[TPGJCount].TPGJ_Position.Z
                    },
                    Rotations = new KMPPropertyGridSettings.TPGJ_Section.TPGJValue.Rotation
                    {
                        X = HTK_3DES.TSRSystem.RadianToAngle(TPGJ.TPGJValue_List[TPGJCount].TPGJ_Rotation.X),
                        Y = HTK_3DES.TSRSystem.RadianToAngle(TPGJ.TPGJValue_List[TPGJCount].TPGJ_Rotation.Y),
                        Z = HTK_3DES.TSRSystem.RadianToAngle(TPGJ.TPGJValue_List[TPGJCount].TPGJ_Rotation.Z)
                    },
                    TPGJ_UnkBytes1 = TPGJ.TPGJValue_List[TPGJCount].TPGJ_UnkBytes1
                };

                TPGJValues_List.Add(TPGJValue);
            }

            return TPGJValues_List;
        }

        public static KMPPropertyGridSettings.IGTS_Section ToIGTSValue(KMPs.KMPFormat.KMPSection.IGTS_Section IGTS)
        {
            KMPPropertyGridSettings.IGTS_Section IGTS_Section = new KMPPropertyGridSettings.IGTS_Section
            {
                Unknown1 = IGTS.Unknown1,
                LapCount = IGTS.LapCount,
                PolePosition = IGTS.PolePosition,
                Unknown2 = IGTS.Unknown2,
                Unknown3 = IGTS.Unknown3,
                RGBAColor = new KMPPropertyGridSettings.IGTS_Section.RGBA
                {
                    R = IGTS.RGBAColor.R,
                    G = IGTS.RGBAColor.G,
                    B = IGTS.RGBAColor.B,
                    A = IGTS.RGBAColor.A
                },
                FlareAlpha = IGTS.FlareAlpha
            };

            return IGTS_Section;
        }

        public static List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> ToHPLGValueList(KMPs.KMPFormat.KMPSection.HPLG_Section HPLG, KMPs.KMPFormat.KMPSection.TPLG_Section TPLG)
        {
            KMPs.KMPHelper.FlagConverter.GlideRoute GlideRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.GlideRoute();

            List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> HPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>();

            for (int i = 0; i < HPLG.NumOfEntries; i++)
            {
                KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue HPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue
                {
                    GroupID = i,
                    HPLG_PreviewGroup = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.HPLG_PreviewGroups
                    {
                        Prev0 = HPLG.HPLGValue_List[i].HPLG_PreviewGroup.Prev0,
                        Prev1 = HPLG.HPLGValue_List[i].HPLG_PreviewGroup.Prev1,
                        Prev2 = HPLG.HPLGValue_List[i].HPLG_PreviewGroup.Prev2,
                        Prev3 = HPLG.HPLGValue_List[i].HPLG_PreviewGroup.Prev3,
                        Prev4 = HPLG.HPLGValue_List[i].HPLG_PreviewGroup.Prev4,
                        Prev5 = HPLG.HPLGValue_List[i].HPLG_PreviewGroup.Prev5
                    },
                    HPLG_NextGroup = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.HPLG_NextGroups
                    {
                        Next0 = HPLG.HPLGValue_List[i].HPLG_NextGroup.Next0,
                        Next1 = HPLG.HPLGValue_List[i].HPLG_NextGroup.Next1,
                        Next2 = HPLG.HPLGValue_List[i].HPLG_NextGroup.Next2,
                        Next3 = HPLG.HPLGValue_List[i].HPLG_NextGroup.Next3,
                        Next4 = HPLG.HPLGValue_List[i].HPLG_NextGroup.Next4,
                        Next5 = HPLG.HPLGValue_List[i].HPLG_NextGroup.Next5
                    },
                    RouteSettings = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.RouteSetting
                    {
                        ForceToRoute = GlideRouteFlagConverter.ConvertFlags(HPLG.HPLGValue_List[i].RouteSetting, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.ForceToRoute),
                        CannonSection = GlideRouteFlagConverter.ConvertFlags(HPLG.HPLGValue_List[i].RouteSetting, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.CannonSection),
                        PreventRaising = GlideRouteFlagConverter.ConvertFlags(HPLG.HPLGValue_List[i].RouteSetting, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.PreventRaising),
                    },
                    HPLG_UnkBytes2 = HPLG.HPLGValue_List[i].HPLG_UnkBytes2,
                    TPLGValueList = null
                };

                List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue> TPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue>();

                for (int Count = 0; Count < HPLG.HPLGValue_List[i].HPLG_Length; Count++)
                {
                    KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue TPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue
                    {
                        GroupID = i,
                        ID = Count,
                        Positions = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue.Position
                        {
                            X = (float)TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_Position.X,
                            Y = (float)TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_Position.Y,
                            Z = (float)TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_Position.Z
                        },
                        TPLG_PointScaleValue = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_PointScaleValue,
                        TPLG_UnkBytes1 = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_UnkBytes1,
                        TPLG_UnkBytes2 = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_UnkBytes2
                    };

                    TPLGValues_List.Add(TPLGValue);
                }

                HPLGValue.TPLGValueList = TPLGValues_List;

                HPLGValues_List.Add(HPLGValue);
            }

            return HPLGValues_List;
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

        public static KMPs.KMPFormat.KMPSection.JBOG_Section ToJBOG_Section(KMPPropertyGridSettings.JBOG_section JBOG_Section)
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

                KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue JBOG_Values = new KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue
                {
                    ObjectID = KMPs.KMPHelper.Byte2StringConverter.OBJIDStrToByteArray(JBOG_Section.JBOGValueList[Count].ObjectID),
                    JBOG_UnkByte1 = KMPs.KMPHelper.Byte2StringConverter.OBJIDStrToByteArray(JBOG_Section.JBOGValueList[Count].JBOG_UnkByte1),
                    JBOG_Position = JBOG_Section.JBOGValueList[Count].Positions.GetVector3D(),
                    JBOG_Rotation = new Vector3D(RX, RY, RZ),
                    JBOG_Scale = JBOG_Section.JBOGValueList[Count].Scales.GetVector3D(),
                    JBOG_ITOP_RouteIDIndex = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JBOG_ITOP_RouteIDIndex),
                    JOBJ_Specific_Setting = new KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue.JBOG_SpecificSetting
                    {
                        Value0 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value0),
                        Value1 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value1),
                        Value2 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value2),
                        Value3 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value3),
                        Value4 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value4),
                        Value5 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value5),
                        Value6 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value6),
                        Value7 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JOBJ_Specific_Setting.Value7)
                    },
                    JBOG_PresenceSetting = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JBOG_PresenceSetting),
                    JBOG_UnkByte2 = KMPs.KMPHelper.Byte2StringConverter.OBJIDStrToByteArray(JBOG_Section.JBOGValueList[Count].JBOG_UnkByte2),
                    JBOG_UnkByte3 = Convert.ToUInt16(JBOG_Section.JBOGValueList[Count].JBOG_UnkByte3)
                };

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

    public class PropertyGridClassConverterXML
    {
        public static List<KMPPropertyGridSettings.TPTK_Section.TPTKValue> ToTPTKValueList(TestXml.KMPXml.StartPosition TPTK)
        {
            List<KMPPropertyGridSettings.TPTK_Section.TPTKValue> TPTKValues_List = new List<KMPPropertyGridSettings.TPTK_Section.TPTKValue>();

            foreach (var StartPosition in TPTK.startPosition_Value.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.TPTK_Section.TPTKValue tPTKValue = new KMPPropertyGridSettings.TPTK_Section.TPTKValue
                {
                    ID = StartPosition.index,
                    Position_Value = new KMPPropertyGridSettings.TPTK_Section.TPTKValue.Position
                    {
                        X = StartPosition.value.Position.X,
                        Y = StartPosition.value.Position.Y,
                        Z = StartPosition.value.Position.Z
                    },
                    Rotate_Value = new KMPPropertyGridSettings.TPTK_Section.TPTKValue.Rotation
                    {
                        X = StartPosition.value.Rotation.X,
                        Y = StartPosition.value.Rotation.Y,
                        Z = StartPosition.value.Rotation.Z
                    },
                    Player_Index = StartPosition.value.Player_Index,
                    TPTK_UnkBytes = StartPosition.value.TPTK_UnkBytes
                };

                TPTKValues_List.Add(tPTKValue);

            }

            return TPTKValues_List;
        }

        public static List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> ToHPNEValueList(TestXml.KMPXml.EnemyRoute enemyRoute)
        {
            KMPs.KMPHelper.FlagConverter.EnemyRoute EnemyRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.EnemyRoute();

            List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> HPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>();

            foreach (var EnemyRoute in enemyRoute.Groups.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue hPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue
                {
                    GroupID = EnemyRoute.index,
                    HPNEPreviewGroups = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.HPNE_PreviewGroups
                    {
                        Prev0 = EnemyRoute.value.Prev0,
                        Prev1 = EnemyRoute.value.Prev1,
                        Prev2 = EnemyRoute.value.Prev2,
                        Prev3 = EnemyRoute.value.Prev3,
                        Prev4 = EnemyRoute.value.Prev4,
                        Prev5 = EnemyRoute.value.Prev5,
                        Prev6 = EnemyRoute.value.Prev6,
                        Prev7 = EnemyRoute.value.Prev7,
                        Prev8 = EnemyRoute.value.Prev8,
                        Prev9 = EnemyRoute.value.Prev9,
                        Prev10 = EnemyRoute.value.Prev10,
                        Prev11 = EnemyRoute.value.Prev11,
                        Prev12 = EnemyRoute.value.Prev12,
                        Prev13 = EnemyRoute.value.Prev13,
                        Prev14 = EnemyRoute.value.Prev14,
                        Prev15 = EnemyRoute.value.Prev15,
                    },
                    HPNENextGroups = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.HPNE_NextGroups
                    {
                        Next0 = EnemyRoute.value.Next0,
                        Next1 = EnemyRoute.value.Next1,
                        Next2 = EnemyRoute.value.Next2,
                        Next3 = EnemyRoute.value.Next3,
                        Next4 = EnemyRoute.value.Next4,
                        Next5 = EnemyRoute.value.Next5,
                        Next6 = EnemyRoute.value.Next6,
                        Next7 = EnemyRoute.value.Next7,
                        Next8 = EnemyRoute.value.Next8,
                        Next9 = EnemyRoute.value.Next9,
                        Next10 = EnemyRoute.value.Next10,
                        Next11 = EnemyRoute.value.Next11,
                        Next12 = EnemyRoute.value.Next12,
                        Next13 = EnemyRoute.value.Next13,
                        Next14 = EnemyRoute.value.Next14,
                        Next15 = EnemyRoute.value.Next15,
                    },
                    HPNE_UnkBytes1 = EnemyRoute.value.Unknown1,
                    TPNEValueList = null
                };

                List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue> TPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue>();

                foreach (var EnemyPoint in EnemyRoute.value.Points.Select((value, index) => new { value, index }))
                {
                    KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue tPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue
                    {
                        Group_ID = EnemyRoute.index,
                        ID = EnemyPoint.index,
                        Positions = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.Position
                        {
                            X = EnemyPoint.value.Position.X,
                            Y = EnemyPoint.value.Position.Y,
                            Z = EnemyPoint.value.Position.Z
                        },
                        Control = EnemyPoint.value.Control,
                        MushSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.MushSetting
                        {
                            MushSettingValue = EnemyPoint.value.MushSetting
                        },
                        DriftSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.DriftSetting
                        {
                            DriftSettingValue = EnemyPoint.value.DriftSetting
                        },
                        FlagSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.FlagSetting
                        {
                            WideTurn = EnemyRouteFlagConverter.ConvertFlags(EnemyPoint.value.Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.WideTurn),
                            NormalTurn = EnemyRouteFlagConverter.ConvertFlags(EnemyPoint.value.Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.NormalTurn),
                            SharpTurn = EnemyRouteFlagConverter.ConvertFlags(EnemyPoint.value.Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.SharpTurn),
                            TricksForbidden = EnemyRouteFlagConverter.ConvertFlags(EnemyPoint.value.Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.TricksForbidden),
                            StickToRoute = EnemyRouteFlagConverter.ConvertFlags(EnemyPoint.value.Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.StickToRoute),
                            BouncyMushSection = EnemyRouteFlagConverter.ConvertFlags(EnemyPoint.value.Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.BouncyMushSection),
                            ForceDefaultSpeed = EnemyRouteFlagConverter.ConvertFlags(EnemyPoint.value.Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.ForceDefaultSpeed),
                            NoPathSwitch = EnemyRouteFlagConverter.ConvertFlags(EnemyPoint.value.Flags, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.NoPathSwitch),
                        },
                        PathFindOptions = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.PathFindOption
                        {
                            PathFindOptionValue = EnemyPoint.value.PathFindOption
                        },
                        MaxSearchYOffset = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.MaxSearch_YOffset
                        {
                            MaxSearchYOffsetValue = EnemyPoint.value.MaxSearchYOffset
                        }
                    };

                    TPNEValues_List.Add(tPNEValue);
                }

                hPNEValue.TPNEValueList = TPNEValues_List;

                HPNEValues_List.Add(hPNEValue);
            }

            return HPNEValues_List;
        }

        public static List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> ToHPTIValueList(TestXml.KMPXml.ItemRoute itemRoute)
        {
            List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> HPTIValues_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>();

            foreach (var ItemRoute in itemRoute.Groups.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue hPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue
                {
                    GroupID = ItemRoute.index,
                    HPTI_PreviewGroup = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.HPTI_PreviewGroups
                    {
                        Prev0 = ItemRoute.value.Prev0,
                        Prev1 = ItemRoute.value.Prev1,
                        Prev2 = ItemRoute.value.Prev2,
                        Prev3 = ItemRoute.value.Prev3,
                        Prev4 = ItemRoute.value.Prev4,
                        Prev5 = ItemRoute.value.Prev5
                    },
                    HPTI_NextGroup = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.HPTI_NextGroups
                    {
                        Next0 = ItemRoute.value.Next0,
                        Next1 = ItemRoute.value.Next1,
                        Next2 = ItemRoute.value.Next2,
                        Next3 = ItemRoute.value.Next3,
                        Next4 = ItemRoute.value.Next4,
                        Next5 = ItemRoute.value.Next5
                    },
                    TPTIValueList = null
                };

                List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue> TPTIVales_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue>();

                foreach (var ItemPoint in ItemRoute.value.Points.Select((value, index) => new { value, index }))
                {
                    KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue tPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue
                    {
                        Group_ID = ItemRoute.index,
                        ID = ItemPoint.index,
                        TPTI_Positions = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.TPTI_Position
                        {
                            X = ItemPoint.value.Position.X,
                            Y = ItemPoint.value.Position.Y,
                            Z = ItemPoint.value.Position.Z
                        },
                        TPTI_PointSize = ItemPoint.value.PointSize,
                        GravityModeSettings = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.GravityModeSetting
                        {
                            GravityModeValue = ItemPoint.value.GravityMode
                        },
                        PlayerScanRadiusSettings = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.PlayerScanRadiusSetting
                        {
                            PlayerScanRadiusValue = ItemPoint.value.PlayerScanRadius
                        }
                    };

                    TPTIVales_List.Add(tPTIValue);
                }

                hPTIValue.TPTIValueList = TPTIVales_List;

                HPTIValues_List.Add(hPTIValue);
            }

            return HPTIValues_List;
        }

        public static List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue> ToHPKCValueList(TestXml.KMPXml.Checkpoint checkpoint)
        {
            List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue> HPKCValues_List = new List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue>();

            foreach (var Checkpoint_Group in checkpoint.Groups.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue hPKCValue = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue
                {
                    GroupID = Checkpoint_Group.index,
                    HPKC_PreviewGroup = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.HPKC_PreviewGroups
                    {
                        Prev0 = Checkpoint_Group.value.Prev0,
                        Prev1 = Checkpoint_Group.value.Prev1,
                        Prev2 = Checkpoint_Group.value.Prev2,
                        Prev3 = Checkpoint_Group.value.Prev3,
                        Prev4 = Checkpoint_Group.value.Prev4,
                        Prev5 = Checkpoint_Group.value.Prev5
                    },
                    HPKC_NextGroup = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.HPKC_NextGroups
                    {
                        Next0 = Checkpoint_Group.value.Next0,
                        Next1 = Checkpoint_Group.value.Next1,
                        Next2 = Checkpoint_Group.value.Next2,
                        Next3 = Checkpoint_Group.value.Next3,
                        Next4 = Checkpoint_Group.value.Next4,
                        Next5 = Checkpoint_Group.value.Next5
                    },
                    HPKC_UnkBytes1 = Checkpoint_Group.value.UnkBytes1,
                    TPKCValueList = null
                };

                List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue> TPKCValues_List = new List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue>();

                foreach (var Checkpoint_Point in Checkpoint_Group.value.Points.Select((value, index) => new { value, index }))
                {
                    KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue tPKCValue = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue
                    {
                        Group_ID = Checkpoint_Group.index,
                        ID = Checkpoint_Point.index,
                        Position_2D_Left = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue.Position2D_Left
                        {
                            X = Checkpoint_Point.value.Position_2D_Left.X,
                            Y = Checkpoint_Point.value.Position_2D_Left.Y
                        },
                        Position_2D_Right = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue.Position2D_Right
                        {
                            X = Checkpoint_Point.value.Position_2D_Right.X,
                            Y = Checkpoint_Point.value.Position_2D_Right.Y
                        },
                        TPKC_RespawnID = Checkpoint_Point.value.RespawnID,
                        TPKC_Checkpoint_Type = Checkpoint_Point.value.Checkpoint_Type,
                        TPKC_PreviousCheckPoint = Checkpoint_Point.value.PreviousCheckPoint,
                        TPKC_NextCheckPoint = Checkpoint_Point.value.NextCheckPoint,
                        TPKC_ClipID = Checkpoint_Point.value.UnkBytes1,
                        TPKC_Section = Checkpoint_Point.value.UnkBytes2,
                        TPKC_UnkBytes3 = Checkpoint_Point.value.UnkBytes3,
                        TPKC_UnkBytes4 = Checkpoint_Point.value.UnkBytes4
                    };

                    TPKCValues_List.Add(tPKCValue);
                }

                hPKCValue.TPKCValueList = TPKCValues_List;

                HPKCValues_List.Add(hPKCValue);
            }

            return HPKCValues_List;
        }

        public static List<KMPPropertyGridSettings.JBOG_section.JBOGValue> ToJBOGValueList(TestXml.KMPXml.Object Objects)
        {
            List<KMPPropertyGridSettings.JBOG_section.JBOGValue> JBOGValues_List = new List<KMPPropertyGridSettings.JBOG_section.JBOGValue>();

            foreach (var Object in Objects.Object_Values.Select((value, index) => new { value, index }))
            {
                List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDB_FindName = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml");
                string Name = ObjFlowDB_FindName.Find(x => x.ObjectID == Object.value.ObjectID).ObjectName;

                KMPPropertyGridSettings.JBOG_section.JBOGValue jBOGValue = new KMPPropertyGridSettings.JBOG_section.JBOGValue
                {
                    ID = Object.index,
                    ObjectName = Name,
                    ObjectID = Object.value.ObjectID,
                    JBOG_UnkByte1 = Object.value.UnkByte1,
                    Positions = new KMPPropertyGridSettings.JBOG_section.JBOGValue.Position
                    {
                        X = Object.value.Position.X,
                        Y = Object.value.Position.Y,
                        Z = Object.value.Position.Z
                    },
                    Rotations = new KMPPropertyGridSettings.JBOG_section.JBOGValue.Rotation
                    {
                        X = Object.value.Rotation.X,
                        Y = Object.value.Rotation.Y,
                        Z = Object.value.Rotation.Z
                    },
                    Scales = new KMPPropertyGridSettings.JBOG_section.JBOGValue.Scale
                    {
                        X = Object.value.Scale.X,
                        Y = Object.value.Scale.Y,
                        Z = Object.value.Scale.Z
                    },
                    JBOG_ITOP_RouteIDIndex = Object.value.RouteIDIndex,
                    JOBJ_Specific_Setting = new KMPPropertyGridSettings.JBOG_section.JBOGValue.JBOG_SpecificSetting
                    {
                        Value0 = Object.value.SpecificSetting.Value0,
                        Value1 = Object.value.SpecificSetting.Value1,
                        Value2 = Object.value.SpecificSetting.Value2,
                        Value3 = Object.value.SpecificSetting.Value3,
                        Value4 = Object.value.SpecificSetting.Value4,
                        Value5 = Object.value.SpecificSetting.Value5,
                        Value6 = Object.value.SpecificSetting.Value6,
                        Value7 = Object.value.SpecificSetting.Value7
                    },
                    JBOG_PresenceSetting = Object.value.PresenceSetting,
                    JBOG_UnkByte2 = Object.value.UnkByte2,
                    JBOG_UnkByte3 = Object.value.UnkByte3
                };

                JBOGValues_List.Add(jBOGValue);
            }

            return JBOGValues_List;
        }

        public static List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route> ToITOPValueList(TestXml.KMPXml.Route route)
        {
            List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route> ITOPRoutes_List = new List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route>();

            foreach (var Route_Group in route.Groups.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.ITOP_Section.ITOP_Route ITOPRoute = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route
                {
                    GroupID = Route_Group.index,
                    ITOP_Roop = Route_Group.value.RouteSetting1,
                    ITOP_Smooth = Route_Group.value.RouteSetting2,
                    ITOP_PointList = null
                };

                List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point> ITOPPoints_List = new List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point>();

                foreach (var Route_Point in Route_Group.value.Points.Select((value, index) => new { value, index }))
                {
                    KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point ITOPPoint = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point
                    {
                        GroupID = Route_Group.index,
                        ID = Route_Point.index,
                        Positions = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point.Position
                        {
                            X = Route_Point.value.Position.X,
                            Y = Route_Point.value.Position.Y,
                            Z = Route_Point.value.Position.Z
                        },
                        ITOP_Point_RouteSpeed = Route_Point.value.RouteSpeed,
                        ITOP_PointSetting2 = Route_Point.value.PointSetting2
                    };

                    ITOPPoints_List.Add(ITOPPoint);
                }

                ITOPRoute.ITOP_PointList = ITOPPoints_List;
                ITOPRoutes_List.Add(ITOPRoute);
            }

            return ITOPRoutes_List;
        }

        public static List<KMPPropertyGridSettings.AERA_Section.AERAValue> ToAERAValueList(TestXml.KMPXml.Area area)
        {
            List<KMPPropertyGridSettings.AERA_Section.AERAValue> AERAValues_List = new List<KMPPropertyGridSettings.AERA_Section.AERAValue>();

            foreach (var Area in area.Area_Values.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.AERA_Section.AERAValue AERAValue = new KMPPropertyGridSettings.AERA_Section.AERAValue
                {
                    ID = Area.index,
                    AreaModeSettings = new KMPPropertyGridSettings.AERA_Section.AERAValue.AreaModeSetting
                    {
                        AreaModeValue = Area.value.AreaMode
                    },
                    AreaType = Area.value.AreaType,
                    AERA_EMACIndex = Area.value.CameraIndex,
                    Priority = Area.value.Priority,
                    Positions = new KMPPropertyGridSettings.AERA_Section.AERAValue.Position
                    {
                        X = Area.value.Position.X,
                        Y = Area.value.Position.Y,
                        Z = Area.value.Position.Z
                    },
                    Rotations = new KMPPropertyGridSettings.AERA_Section.AERAValue.Rotation
                    {
                        X = Area.value.Rotation.X,
                        Y = Area.value.Rotation.Y,
                        Z = Area.value.Rotation.Z
                    },
                    Scales = new KMPPropertyGridSettings.AERA_Section.AERAValue.Scale
                    {
                        X = Area.value.Scale.X,
                        Y = Area.value.Scale.Y,
                        Z = Area.value.Scale.Z
                    },
                    AERA_Setting1 = Area.value.UnkByte1,
                    AERA_Setting2 = Area.value.UnkByte2,
                    RouteID = Area.value.RouteID,
                    EnemyID = Area.value.EnemyID,
                    AERA_UnkByte4 = Area.value.UnkByte4
                };

                AERAValues_List.Add(AERAValue);
            }

            return AERAValues_List;
        }

        public static List<KMPPropertyGridSettings.EMAC_Section.EMACValue> ToEMACValueList(TestXml.KMPXml.Camera camera)
        {
            List<KMPPropertyGridSettings.EMAC_Section.EMACValue> EMACValues_List = new List<KMPPropertyGridSettings.EMAC_Section.EMACValue>();

            foreach (var Camera in camera.Values.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.EMAC_Section.EMACValue EMACValue = new KMPPropertyGridSettings.EMAC_Section.EMACValue
                {
                    ID = Camera.index,
                    CameraType = Camera.value.CameraType,
                    NextCameraIndex = Camera.value.NextCameraIndex,
                    EMAC_NextVideoIndex = Camera.value.UnkBytes1,
                    EMAC_ITOP_CameraIndex = Camera.value.Route_CameraIndex,
                    SpeedSettings = new KMPPropertyGridSettings.EMAC_Section.EMACValue.SpeedSetting
                    {
                        RouteSpeed = Camera.value.SpeedSetting.RouteSpeed,
                        FOVSpeed = Camera.value.SpeedSetting.FOVSpeed,
                        ViewpointSpeed = Camera.value.SpeedSetting.ViewpointSpeed
                    },
                    EMAC_StartFlag = Camera.value.UnkBytes2,
                    EMAC_VideoFlag = Camera.value.UnkBytes3,
                    Positions = new KMPPropertyGridSettings.EMAC_Section.EMACValue.Position
                    {
                        X = Camera.value.Position.X,
                        Y = Camera.value.Position.Y,
                        Z = Camera.value.Position.Z
                    },
                    Rotations = new KMPPropertyGridSettings.EMAC_Section.EMACValue.Rotation
                    {
                        X = Camera.value.Rotation.X,
                        Y = Camera.value.Rotation.Y,
                        Z = Camera.value.Rotation.Z
                    },
                    FOVAngleSettings = new KMPPropertyGridSettings.EMAC_Section.EMACValue.FOVAngleSetting
                    {
                        FOVAngle_Start = Camera.value.FOVAngleSettings.Start,
                        FOVAngle_End = Camera.value.FOVAngleSettings.End
                    },
                    Viewpoint_Start = new KMPPropertyGridSettings.EMAC_Section.EMACValue.ViewpointStart
                    {
                        X = Camera.value.ViewpointStart.X,
                        Y = Camera.value.ViewpointStart.Y,
                        Z = Camera.value.ViewpointStart.Z
                    },
                    Viewpoint_Destination = new KMPPropertyGridSettings.EMAC_Section.EMACValue.ViewpointDestination
                    {
                        X = Camera.value.ViewpointDestination.X,
                        Y = Camera.value.ViewpointDestination.Y,
                        Z = Camera.value.ViewpointDestination.Z
                    },
                    Camera_Active_Time = Camera.value.CameraActiveTime
                };

                EMACValues_List.Add(EMACValue);
            }

            return EMACValues_List;
        }

        public static List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue> ToTPGJValueList(TestXml.KMPXml.JugemPoint jugemPoint)
        {
            List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue> TPGJValues_List = new List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue>();

            foreach (var JugemPoint in jugemPoint.Values.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.TPGJ_Section.TPGJValue TPGJValue = new KMPPropertyGridSettings.TPGJ_Section.TPGJValue
                {
                    ID = JugemPoint.index,
                    TPGJ_RespawnID = JugemPoint.value.RespawnID,
                    Positions = new KMPPropertyGridSettings.TPGJ_Section.TPGJValue.Position
                    {
                        X = JugemPoint.value.Position.X,
                        Y = JugemPoint.value.Position.Y,
                        Z = JugemPoint.value.Position.Z
                    },
                    Rotations = new KMPPropertyGridSettings.TPGJ_Section.TPGJValue.Rotation
                    {
                        X = JugemPoint.value.Rotation.X,
                        Y = JugemPoint.value.Rotation.Y,
                        Z = JugemPoint.value.Rotation.Z
                    },
                    TPGJ_UnkBytes1 = JugemPoint.value.UnkBytes1
                };

                TPGJValues_List.Add(TPGJValue);
            }

            return TPGJValues_List;
        }

        public static KMPPropertyGridSettings.IGTS_Section ToIGTSValue(TestXml.KMPXml.StageInfo stageInfo)
        {
            KMPPropertyGridSettings.IGTS_Section IGTS_Section = new KMPPropertyGridSettings.IGTS_Section
            {
                Unknown1 = stageInfo.Unknown1,
                LapCount = stageInfo.LapCount,
                PolePosition = stageInfo.PolePosition,
                Unknown2 = stageInfo.Unknown2,
                Unknown3 = stageInfo.Unknown3,
                RGBAColor = new KMPPropertyGridSettings.IGTS_Section.RGBA
                {
                    R = stageInfo.RGBAColor.R,
                    G = stageInfo.RGBAColor.G,
                    B = stageInfo.RGBAColor.B,
                    A = stageInfo.RGBAColor.A
                },
                FlareAlpha = stageInfo.RGBAColor.FlareAlpha
            };

            return IGTS_Section;
        }

        public static List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> ToHPLGValueList(TestXml.KMPXml.GlideRoute glideRoute)
        {
            KMPs.KMPHelper.FlagConverter.GlideRoute GlideRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.GlideRoute();

            List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> HPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>();

            foreach (var GlideRoute in glideRoute.Groups.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue HPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue
                {
                    GroupID = GlideRoute.index,
                    HPLG_PreviewGroup = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.HPLG_PreviewGroups
                    {
                        Prev0 = GlideRoute.value.Prev0,
                        Prev1 = GlideRoute.value.Prev1,
                        Prev2 = GlideRoute.value.Prev2,
                        Prev3 = GlideRoute.value.Prev3,
                        Prev4 = GlideRoute.value.Prev4,
                        Prev5 = GlideRoute.value.Prev5
                    },
                    HPLG_NextGroup = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.HPLG_NextGroups
                    {
                        Next0 = GlideRoute.value.Next0,
                        Next1 = GlideRoute.value.Next1,
                        Next2 = GlideRoute.value.Next2,
                        Next3 = GlideRoute.value.Next3,
                        Next4 = GlideRoute.value.Next4,
                        Next5 = GlideRoute.value.Next5
                    },
                    RouteSettings = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.RouteSetting
                    {
                        ForceToRoute = GlideRouteFlagConverter.ConvertFlags(GlideRoute.value.RouteSetting, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.ForceToRoute),
                        CannonSection = GlideRouteFlagConverter.ConvertFlags(GlideRoute.value.RouteSetting, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.CannonSection),
                        PreventRaising = GlideRouteFlagConverter.ConvertFlags(GlideRoute.value.RouteSetting, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.PreventRaising),
                    },
                    HPLG_UnkBytes2 = GlideRoute.value.UnkBytes2,
                    TPLGValueList = null
                };

                List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue> TPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue>();

                foreach (var GlidePoint in GlideRoute.value.Points.Select((value, index) => new { value, index }))
                {
                    KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue TPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue
                    {
                        GroupID = GlideRoute.index,
                        ID = GlidePoint.index,
                        Positions = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue.Position
                        {
                            X = GlidePoint.value.Position.X,
                            Y = GlidePoint.value.Position.Y,
                            Z = GlidePoint.value.Position.Z
                        },
                        TPLG_PointScaleValue = GlidePoint.value.PointScale,
                        TPLG_UnkBytes1 = GlidePoint.value.UnkBytes1,
                        TPLG_UnkBytes2 = GlidePoint.value.UnkBytes2
                    };

                    TPLGValues_List.Add(TPLGValue);
                }

                HPLGValue.TPLGValueList = TPLGValues_List;

                HPLGValues_List.Add(HPLGValue);

            }

            return HPLGValues_List;
        }
    }

    public class PropertyGridClassConverterXML_XXXXRoute
    {
        public static List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> ToHPNEValueList(TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
        {
            KMPs.KMPHelper.FlagConverter.EnemyRoute EnemyRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.EnemyRoute();

            List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> HPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>();

            foreach (var XXXXRoute in xXXXRoute.Groups.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue hPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue
                {
                    GroupID = XXXXRoute.index,
                    HPNEPreviewGroups = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.HPNE_PreviewGroups
                    {
                        Prev0 = 255,
                        Prev1 = 255,
                        Prev2 = 255,
                        Prev3 = 255,
                        Prev4 = 255,
                        Prev5 = 255,
                        Prev6 = 255,
                        Prev7 = 255,
                        Prev8 = 255,
                        Prev9 = 255,
                        Prev10 = 255,
                        Prev11 = 255,
                        Prev12 = 255,
                        Prev13 = 255,
                        Prev14 = 255,
                        Prev15 = 255
                    },
                    HPNENextGroups = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.HPNE_NextGroups
                    {
                        Next0 = 255,
                        Next1 = 255,
                        Next2 = 255,
                        Next3 = 255,
                        Next4 = 255,
                        Next5 = 255,
                        Next6 = 255,
                        Next7 = 255,
                        Next8 = 255,
                        Next9 = 255,
                        Next10 = 255,
                        Next11 = 255,
                        Next12 = 255,
                        Next13 = 255,
                        Next14 = 255,
                        Next15 = 255
                    },
                    HPNE_UnkBytes1 = 0,
                    TPNEValueList = null
                };

                List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue> TPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue>();

                foreach (var EnemyPoint in XXXXRoute.value.Points.Select((value, index) => new { value, index }))
                {
                    KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue tPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue
                    {
                        Group_ID = XXXXRoute.index,
                        ID = EnemyPoint.index,
                        Positions = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.Position
                        {
                            X = EnemyPoint.value.Position.X,
                            Y = EnemyPoint.value.Position.Y,
                            Z = EnemyPoint.value.Position.Z
                        },
                        Control = EnemyPoint.value.ScaleValue,
                        MushSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.MushSetting
                        {
                            MushSettingValue = 0
                        },
                        DriftSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.DriftSetting
                        {
                            DriftSettingValue = 0
                        },
                        FlagSettings = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.FlagSetting
                        {
                            WideTurn = EnemyRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.WideTurn),
                            NormalTurn = EnemyRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.NormalTurn),
                            SharpTurn = EnemyRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.SharpTurn),
                            TricksForbidden = EnemyRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.TricksForbidden),
                            StickToRoute = EnemyRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.StickToRoute),
                            BouncyMushSection = EnemyRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.BouncyMushSection),
                            ForceDefaultSpeed = EnemyRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.ForceDefaultSpeed),
                            NoPathSwitch = EnemyRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.EnemyRoute.FlagType.NoPathSwitch),
                        },
                        PathFindOptions = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.PathFindOption
                        {
                            PathFindOptionValue = 0
                        },
                        MaxSearchYOffset = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue.MaxSearch_YOffset
                        {
                            MaxSearchYOffsetValue = 0
                        }
                    };

                    TPNEValues_List.Add(tPNEValue);
                }

                hPNEValue.TPNEValueList = TPNEValues_List;

                HPNEValues_List.Add(hPNEValue);
            }

            return HPNEValues_List;
        }

        public static List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> ToHPTIValueList(TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
        {
            List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> HPTIValues_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>();

            foreach (var ItemRoute in xXXXRoute.Groups.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue hPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue
                {
                    GroupID = ItemRoute.index,
                    HPTI_PreviewGroup = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.HPTI_PreviewGroups
                    {
                        Prev0 = 255,
                        Prev1 = 255,
                        Prev2 = 255,
                        Prev3 = 255,
                        Prev4 = 255,
                        Prev5 = 255
                    },
                    HPTI_NextGroup = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.HPTI_NextGroups
                    {
                        Next0 = 255,
                        Next1 = 255,
                        Next2 = 255,
                        Next3 = 255,
                        Next4 = 255,
                        Next5 = 255
                    },
                    TPTIValueList = null
                };

                List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue> TPTIVales_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue>();

                foreach (var ItemPoint in ItemRoute.value.Points.Select((value, index) => new { value, index }))
                {
                    KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue tPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue
                    {
                        Group_ID = ItemRoute.index,
                        ID = ItemPoint.index,
                        TPTI_Positions = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.TPTI_Position
                        {
                            X = ItemPoint.value.Position.X,
                            Y = ItemPoint.value.Position.Y,
                            Z = ItemPoint.value.Position.Z
                        },
                        TPTI_PointSize = ItemPoint.value.ScaleValue,
                        GravityModeSettings = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.GravityModeSetting
                        {
                            GravityModeValue = 0
                        },
                        PlayerScanRadiusSettings = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue.PlayerScanRadiusSetting
                        {
                            PlayerScanRadiusValue = 0
                        }
                    };

                    TPTIVales_List.Add(tPTIValue);
                }

                hPTIValue.TPTIValueList = TPTIVales_List;

                HPTIValues_List.Add(hPTIValue);
            }

            return HPTIValues_List;
        }

        public static List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> ToHPLGValueList(TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
        {
            KMPs.KMPHelper.FlagConverter.GlideRoute GlideRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.GlideRoute();

            List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> HPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>();

            foreach (var GlideRoute in xXXXRoute.Groups.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue HPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue
                {
                    GroupID = GlideRoute.index,
                    HPLG_PreviewGroup = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.HPLG_PreviewGroups
                    {
                        Prev0 = 255,
                        Prev1 = 255,
                        Prev2 = 255,
                        Prev3 = 255,
                        Prev4 = 255,
                        Prev5 = 255
                    },
                    HPLG_NextGroup = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.HPLG_NextGroups
                    {
                        Next0 = 255,
                        Next1 = 255,
                        Next2 = 255,
                        Next3 = 255,
                        Next4 = 255,
                        Next5 = 255
                    },
                    RouteSettings = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.RouteSetting
                    {
                        ForceToRoute = GlideRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.ForceToRoute),
                        CannonSection = GlideRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.CannonSection),
                        PreventRaising = GlideRouteFlagConverter.ConvertFlags(0, KMPs.KMPHelper.FlagConverter.GlideRoute.FlagType.PreventRaising),
                    },
                    HPLG_UnkBytes2 = 0,
                    TPLGValueList = null
                };

                List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue> TPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue>();

                foreach (var GlidePoint in GlideRoute.value.Points.Select((value, index) => new { value, index }))
                {
                    KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue TPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue
                    {
                        GroupID = GlideRoute.index,
                        ID = GlidePoint.index,
                        Positions = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue.Position
                        {
                            X = GlidePoint.value.Position.X,
                            Y = GlidePoint.value.Position.Y,
                            Z = GlidePoint.value.Position.Z
                        },
                        TPLG_PointScaleValue = GlidePoint.value.ScaleValue,
                        TPLG_UnkBytes1 = 0,
                        TPLG_UnkBytes2 = 0
                    };

                    TPLGValues_List.Add(TPLGValue);
                }

                HPLGValue.TPLGValueList = TPLGValues_List;

                HPLGValues_List.Add(HPLGValue);

            }

            return HPLGValues_List;
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
