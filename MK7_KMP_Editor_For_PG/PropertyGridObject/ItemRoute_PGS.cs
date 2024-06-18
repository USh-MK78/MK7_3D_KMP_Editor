using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using KMPLibrary.Format.SectionData;
using static MK7_3D_KMP_Editor.CustomPropertyGridClassConverter;

namespace MK7_3D_KMP_Editor.PropertyGridObject
{
    public class HPTI_TPTIData
    {
        public HPTI HPTI_Section;
        public TPTI TPTI_Section;

        public HPTI_TPTIData(HPTI HPTI, TPTI TPTI)
        {
            HPTI_Section = HPTI;
            TPTI_Section = TPTI;
        }
    }

    /// <summary>
    /// ItemRoute (PropertyGrid)
    /// </summary>
    public class ItemRoute_PGS
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

                public ushort[] GetPrevGroupArray()
                {
                    return new ushort[] { Prev0, Prev1, Prev2, Prev3, Prev4, Prev5 };
                }

                public HPTI_PreviewGroups(ushort[] PrevGroupArray)
                {
                    Prev0 = PrevGroupArray[0];
                    Prev1 = PrevGroupArray[1];
                    Prev2 = PrevGroupArray[2];
                    Prev3 = PrevGroupArray[3];
                    Prev4 = PrevGroupArray[4];
                    Prev5 = PrevGroupArray[5];
                }

                public HPTI_PreviewGroups()
                {
                    Prev0 = 65535;
                    Prev1 = 65535;
                    Prev2 = 65535;
                    Prev3 = 65535;
                    Prev4 = 65535;
                    Prev5 = 65535;
                }

                public HPTI_PreviewGroups(HPTI.HPTIValue.HPTI_PreviewGroups HPTI_PreviewGroup)
                {
                    Prev0 = HPTI_PreviewGroup.Prev0;
                    Prev1 = HPTI_PreviewGroup.Prev1;
                    Prev2 = HPTI_PreviewGroup.Prev2;
                    Prev3 = HPTI_PreviewGroup.Prev3;
                    Prev4 = HPTI_PreviewGroup.Prev4;
                    Prev5 = HPTI_PreviewGroup.Prev5;
                }

                public HPTI_PreviewGroups(KMPLibrary.XMLConvert.KMPData.SectionData.ItemRoute.ItemRoute_Group.IR_PreviousGroup Previous)
                {
                    Prev0 = Previous.Prev0;
                    Prev1 = Previous.Prev1;
                    Prev2 = Previous.Prev2;
                    Prev3 = Previous.Prev3;
                    Prev4 = Previous.Prev4;
                    Prev5 = Previous.Prev5;
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

                public ushort[] GetNextGroupArray()
                {
                    return new ushort[] { Next0, Next1, Next2, Next3, Next4, Next5 };
                }

                public HPTI_NextGroups(ushort[] NextGroupArray)
                {
                    Next0 = NextGroupArray[0];
                    Next1 = NextGroupArray[1];
                    Next2 = NextGroupArray[2];
                    Next3 = NextGroupArray[3];
                    Next4 = NextGroupArray[4];
                    Next5 = NextGroupArray[5];
                }

                public HPTI_NextGroups()
                {
                    Next0 = 65535;
                    Next1 = 65535;
                    Next2 = 65535;
                    Next3 = 65535;
                    Next4 = 65535;
                    Next5 = 65535;
                }

                public HPTI_NextGroups(HPTI.HPTIValue.HPTI_NextGroups HPTI_NextGroup)
                {
                    Next0 = HPTI_NextGroup.Next0;
                    Next1 = HPTI_NextGroup.Next1;
                    Next2 = HPTI_NextGroup.Next2;
                    Next3 = HPTI_NextGroup.Next3;
                    Next4 = HPTI_NextGroup.Next4;
                    Next5 = HPTI_NextGroup.Next5;
                }

                public HPTI_NextGroups(KMPLibrary.XMLConvert.KMPData.SectionData.ItemRoute.ItemRoute_Group.IR_NextGroup Next)
                {
                    Next0 = Next.Next0;
                    Next1 = Next.Next1;
                    Next2 = Next.Next2;
                    Next3 = Next.Next3;
                    Next4 = Next.Next4;
                    Next5 = Next.Next5;
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
                    public TPTI.TPTIValue.GravityMode GravityModeEnum
                    {
                        get { return (TPTI.TPTIValue.GravityMode)Enum.ToObject(typeof(TPTI.TPTIValue.GravityMode), GravityModeValue); }
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
                    public TPTI.TPTIValue.PlayerScanRadius PlayerScanRadiusEnum
                    {
                        get { return (TPTI.TPTIValue.PlayerScanRadius)Enum.ToObject(typeof(TPTI.TPTIValue.PlayerScanRadius), PlayerScanRadiusValue); }
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

                public TPTIValue(TPTI.TPTIValue TPTIValue, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    TPTI_Positions = new TPTI_Position(TPTIValue.TPTI_Position);
                    TPTI_PointSize = TPTIValue.TPTI_PointSize;
                    GravityModeSettings.GravityModeValue = TPTIValue.GravityModeValue;
                    PlayerScanRadiusSettings.PlayerScanRadiusValue = TPTIValue.PlayerScanRadiusValue;
                }

                public TPTIValue(KMPLibrary.XMLConvert.KMPData.SectionData.ItemRoute.ItemRoute_Group.ItemRoute_Point ItemRoute_Point, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    TPTI_Positions = new TPTI_Position(ItemRoute_Point.Position.ToVector3D());
                    TPTI_PointSize = ItemRoute_Point.PointSize;
                    GravityModeSettings.GravityModeValue = ItemRoute_Point.GravityMode;
                    PlayerScanRadiusSettings.PlayerScanRadiusValue = ItemRoute_Point.PlayerScanRadius;
                }

                public TPTIValue(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData PointData, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    TPTI_Positions = new TPTI_Position(PointData.Position.ToVector3D());
                    TPTI_PointSize = PointData.ScaleValue;
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

            public HPTIValue(HPTI.HPTIValue HPTIValue, TPTI TPTI, int InputID)
            {
                GroupID = InputID;
                HPTI_PreviewGroup = new HPTI_PreviewGroups(HPTIValue.HPTI_PreviewGroup);
                HPTI_NextGroup = new HPTI_NextGroups(HPTIValue.HPTI_NextGroup);

                for (int i = 0; i < HPTIValue.HPTI_Length; i++)
                {
                    TPTIValueList.Add(new TPTIValue(TPTI.TPTIValue_List[i + HPTIValue.HPTI_StartPoint], InputID, i));
                }
            }

            public HPTIValue(KMPLibrary.XMLConvert.KMPData.SectionData.ItemRoute.ItemRoute_Group ItemRoute_Group, int InputID)
            {
                GroupID = InputID;
                HPTI_PreviewGroup = new HPTI_PreviewGroups(ItemRoute_Group.PreviousGroups);
                HPTI_NextGroup = new HPTI_NextGroups(ItemRoute_Group.NextGroups);

                for (int i = 0; i < ItemRoute_Group.Points.Count; i++)
                {
                    TPTIValueList.Add(new TPTIValue(ItemRoute_Group.Points[i], InputID, i));
                }
            }

            public HPTIValue(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData GroupData, int InputID)
            {
                GroupID = InputID;
                HPTI_PreviewGroup = new HPTI_PreviewGroups();
                HPTI_NextGroup = new HPTI_NextGroups();

                for (int i = 0; i < GroupData.Points.Count; i++)
                {
                    TPTIValueList.Add(new TPTIValue(GroupData.Points[i], InputID, i));
                }
            }

            public override string ToString()
            {
                return "ItemRoute " + GroupID;
            }
        }

        public ItemRoute_PGS(HPTI HPTI, TPTI TPTI)
        {
            for (int i = 0; i < HPTI.NumOfEntries; i++)
            {
                HPTIValueList.Add(new HPTIValue(HPTI.HPTIValue_List[i], TPTI, i));
            }
        }

        public ItemRoute_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.ItemRoute ItemRoute)
        {
            for (int i = 0; i < ItemRoute.Groups.Count; i++)
            {
                HPTIValueList.Add(new HPTIValue(ItemRoute.Groups[i], i));
            }
        }

        public ItemRoute_PGS(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute XXXXRoute)
        {
            for (int i = 0; i < XXXXRoute.Groups.Count; i++)
            {
                HPTIValueList.Add(new HPTIValue(XXXXRoute.Groups[i], i));
            }
        }

        public ItemRoute_PGS()
        {
            HPTIValueList = new List<HPTIValue>();
        }

        public HPTI_TPTIData ToHPTI_TPTIData()
        {
            HPTI_TPTIData HPTI_TPTI_Data = null;

            if (HPTIValueList.Count != 0)
            {
                List<TPTI.TPTIValue> TPTI_Values_List = new List<TPTI.TPTIValue>();
                List<HPTI.HPTIValue> HPTI_Values_List = new List<HPTI.HPTIValue>();

                int StartPoint = 0;
                for (int HPTICount = 0; HPTICount < HPTIValueList.Count; HPTICount++)
                {
                    HPTI.HPTIValue HPTI_Values = new HPTI.HPTIValue
                    {
                        HPTI_StartPoint = Convert.ToUInt16(StartPoint),
                        HPTI_Length = Convert.ToUInt16(HPTIValueList[HPTICount].TPTIValueList.Count),
                        HPTI_PreviewGroup = new HPTI.HPTIValue.HPTI_PreviewGroups(HPTIValueList[HPTICount].HPTI_PreviewGroup.GetPrevGroupArray()),
                        HPTI_NextGroup = new HPTI.HPTIValue.HPTI_NextGroups(HPTIValueList[HPTICount].HPTI_NextGroup.GetNextGroupArray())
                    };
                    HPTI_Values_List.Add(HPTI_Values);

                    for (int TPTICount = 0; TPTICount < HPTIValueList[HPTICount].TPTIValueList.Count; TPTICount++)
                    {
                        TPTI.TPTIValue TPTI_Values = new TPTI.TPTIValue
                        {
                            TPTI_Position = HPTIValueList[HPTICount].TPTIValueList[TPTICount].TPTI_Positions.GetVector3D(),
                            TPTI_PointSize = Convert.ToSingle(HPTIValueList[HPTICount].TPTIValueList[TPTICount].TPTI_PointSize),
                            GravityModeValue = HPTIValueList[HPTICount].TPTIValueList[TPTICount].GravityModeSettings.GravityModeValue,
                            PlayerScanRadiusValue = HPTIValueList[HPTICount].TPTIValueList[TPTICount].PlayerScanRadiusSettings.PlayerScanRadiusValue
                        };

                        TPTI_Values_List.Add(TPTI_Values);

                        StartPoint++;
                    }
                }

                TPTI TPTI = new TPTI(TPTI_Values_List);
                HPTI HPTI = new HPTI(HPTI_Values_List);

                HPTI_TPTI_Data = new HPTI_TPTIData(HPTI, TPTI);
            }
            if (HPTIValueList.Count == 0)
            {
                TPTI TPTI = new TPTI(new List<TPTI.TPTIValue>());
                HPTI HPTI = new HPTI(new List<HPTI.HPTIValue>());

                HPTI_TPTI_Data = new HPTI_TPTIData(HPTI, TPTI);
            }

            return HPTI_TPTI_Data;
        }
    }
}
