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
    public class HPLG_TPLGData
    {
        public HPLG HPLG_Section;
        public TPLG TPLG_Section;

        public HPLG_TPLGData(HPLG HPLG, TPLG TPLG)
        {
            HPLG_Section = HPLG;
            TPLG_Section = TPLG;
        }
    }

    /// <summary>
    /// Glide Route (PropertyGrid)
    /// </summary>
    public class GlideRoute_PGS
    {
        public List<HPLGValue> HPLGValue_List = new List<HPLGValue>();
        public List<HPLGValue> HPLGValueList { get => HPLGValue_List; set => HPLGValue_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class HPLGValue
        {
            [ReadOnly(true)]
            public int GroupID { get; set; }

            public bool IsViewportVisible { get; set; } = true;

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

                public byte[] GetPrevGroupArray()
                {
                    return new byte[] { Prev0, Prev1, Prev2, Prev3, Prev4, Prev5 };
                }

                public HPLG_PreviewGroups(byte[] PrevGroupArray)
                {
                    Prev0 = PrevGroupArray[0];
                    Prev1 = PrevGroupArray[1];
                    Prev2 = PrevGroupArray[2];
                    Prev3 = PrevGroupArray[3];
                    Prev4 = PrevGroupArray[4];
                    Prev5 = PrevGroupArray[5];
                }

                public HPLG_PreviewGroups()
                {
                    Prev0 = 255;
                    Prev1 = 255;
                    Prev2 = 255;
                    Prev3 = 255;
                    Prev4 = 255;
                    Prev5 = 255;
                }

                public HPLG_PreviewGroups(HPLG.HPLGValue.HPLG_PreviewGroups HPLG_PreviewGroup)
                {
                    Prev0 = HPLG_PreviewGroup.Prev0;
                    Prev1 = HPLG_PreviewGroup.Prev1;
                    Prev2 = HPLG_PreviewGroup.Prev2;
                    Prev3 = HPLG_PreviewGroup.Prev3;
                    Prev4 = HPLG_PreviewGroup.Prev4;
                    Prev5 = HPLG_PreviewGroup.Prev5;
                }

                public HPLG_PreviewGroups(KMPLibrary.XMLConvert.KMPData.SectionData.GlideRoute.GlideRoute_Group.GR_PreviousGroup Previous)
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
            public HPLG_NextGroups HPLG_NextGroup { get; set; } = new HPLG_NextGroups();
            public class HPLG_NextGroups
            {
                public byte Next0 { get; set; }
                public byte Next1 { get; set; }
                public byte Next2 { get; set; }
                public byte Next3 { get; set; }
                public byte Next4 { get; set; }
                public byte Next5 { get; set; }

                public byte[] GetNextGroupArray()
                {
                    return new byte[] { Next0, Next1, Next2, Next3, Next4, Next5 };
                }

                public HPLG_NextGroups(byte[] NextGroupArray)
                {
                    Next0 = NextGroupArray[0];
                    Next1 = NextGroupArray[1];
                    Next2 = NextGroupArray[2];
                    Next3 = NextGroupArray[3];
                    Next4 = NextGroupArray[4];
                    Next5 = NextGroupArray[5];
                }

                public HPLG_NextGroups()
                {
                    Next0 = 255;
                    Next1 = 255;
                    Next2 = 255;
                    Next3 = 255;
                    Next4 = 255;
                    Next5 = 255;
                }

                public HPLG_NextGroups(HPLG.HPLGValue.HPLG_NextGroups HPLG_NextGroup)
                {
                    Next0 = HPLG_NextGroup.Next0;
                    Next1 = HPLG_NextGroup.Next1;
                    Next2 = HPLG_NextGroup.Next2;
                    Next3 = HPLG_NextGroup.Next3;
                    Next4 = HPLG_NextGroup.Next4;
                    Next5 = HPLG_NextGroup.Next5;
                }

                public HPLG_NextGroups(KMPLibrary.XMLConvert.KMPData.SectionData.GlideRoute.GlideRoute_Group.GR_NextGroup Next)
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

            public uint HPLG_UnknownData2 { get; set; }

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

                public uint TPLG_UnknownData1 { get; set; }
                public uint TPLG_UnknownData2 { get; set; }

                public TPLGValue(Vector3D Pos, int GroupID, int InputID)
                {
                    this.GroupID = GroupID;
                    ID = InputID;
                    Positions = new Position(Pos);
                    TPLG_PointScaleValue = 1;
                    TPLG_UnknownData1 = 0;
                    TPLG_UnknownData2 = 0;
                }

                public TPLGValue(TPLG.TPLGValue TPLGValue, int GroupID, int InputID)
                {
                    this.GroupID = GroupID;
                    ID = InputID;
                    Positions = new Position(TPLGValue.TPLG_Position);
                    TPLG_PointScaleValue = TPLGValue.TPLG_PointScaleValue;
                    TPLG_UnknownData1 = TPLGValue.TPLG_UnknownData1;
                    TPLG_UnknownData2 = TPLGValue.TPLG_UnknownData2;
                }

                public TPLGValue(KMPLibrary.XMLConvert.KMPData.SectionData.GlideRoute.GlideRoute_Group.GlideRoute_Point GlideRoute_Point, int GroupID, int InputID)
                {
                    this.GroupID = GroupID;
                    ID = InputID;
                    Positions = new Position(GlideRoute_Point.Position.ToVector3D());
                    TPLG_PointScaleValue = GlideRoute_Point.PointScale;
                    TPLG_UnknownData1 = GlideRoute_Point.UnknownData1;
                    TPLG_UnknownData2 = GlideRoute_Point.UnknownData2;
                }

                public TPLGValue(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData.PointData PointData, int GroupID, int InputID)
                {
                    this.GroupID = GroupID;
                    ID = InputID;
                    Positions = new Position(PointData.Position.ToVector3D());
                    TPLG_PointScaleValue = PointData.ScaleValue;
                    TPLG_UnknownData1 = 0;
                    TPLG_UnknownData2 = 0;
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
                HPLG_UnknownData2 = 0;
                TPLGValueList = new List<TPLGValue>();
            }

            public HPLGValue(HPLG.HPLGValue HPLGValue, TPLG TPLG, int InputID)
            {
                GroupID = InputID;
                HPLG_PreviewGroup = new HPLG_PreviewGroups(HPLGValue.HPLG_PreviewGroup);
                HPLG_NextGroup = new HPLG_NextGroups(HPLGValue.HPLG_NextGroup);
                RouteSettings.RouteSettingValue = HPLGValue.RouteSetting;
                HPLG_UnknownData2 = HPLGValue.HPLG_UnknownData2;

                for (int i = 0; i < HPLGValue.HPLG_Length; i++)
                {
                    TPLGValueList.Add(new TPLGValue(TPLG.TPLGValue_List[i + HPLGValue.HPLG_StartPoint], InputID, i));
                }
            }

            public HPLGValue(KMPLibrary.XMLConvert.KMPData.SectionData.GlideRoute.GlideRoute_Group GlideRoute_Group, int InputID)
            {
                GroupID = InputID;
                HPLG_PreviewGroup = new HPLG_PreviewGroups(GlideRoute_Group.PreviousGroups);
                HPLG_NextGroup = new HPLG_NextGroups(GlideRoute_Group.NextGroups);
                RouteSettings.RouteSettingValue = GlideRoute_Group.RouteSetting;
                HPLG_UnknownData2 = GlideRoute_Group.UnknownData2;

                for (int i = 0; i < GlideRoute_Group.Points.Count; i++)
                {
                    TPLGValueList.Add(new TPLGValue(GlideRoute_Group.Points[i], InputID, i));
                }
            }

            public HPLGValue(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute.GroupData GroupData, int InputID)
            {
                GroupID = InputID;
                HPLG_PreviewGroup = new HPLG_PreviewGroups();
                HPLG_NextGroup = new HPLG_NextGroups();
                RouteSettings.RouteSettingValue = 0;
                HPLG_UnknownData2 = 0;

                for (int i = 0; i < GroupData.Points.Count; i++)
                {
                    TPLGValueList.Add(new TPLGValue(GroupData.Points[i], InputID, i));
                }
            }

            public override string ToString()
            {
                return "Glide Route " + GroupID;
            }
        }

        public GlideRoute_PGS(HPLG HPLG, TPLG TPLG)
        {
            for (int i = 0; i < HPLG.NumOfEntries; i++)
            {
                HPLGValueList.Add(new HPLGValue(HPLG.HPLGValue_List[i], TPLG, i));
            }
        }

        public GlideRoute_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.GlideRoute GlideRoute)
        {
            for (int i = 0; i < GlideRoute.Groups.Count; i++)
            {
                HPLGValueList.Add(new HPLGValue(GlideRoute.Groups[i], i));
            }
        }

        public GlideRoute_PGS(KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute XXXXRoute)
        {
            for (int i = 0; i < XXXXRoute.Groups.Count; i++)
            {
                HPLGValueList.Add(new HPLGValue(XXXXRoute.Groups[i], i));
            }
        }

        public GlideRoute_PGS()
        {
            HPLGValueList = new List<HPLGValue>();
        }

        public HPLG_TPLGData ToHPLG_TPLGData()
        {
            HPLG_TPLGData HPLG_TPLG_Data = null;

            if (HPLGValueList.Count != 0)
            {
                List<TPLG.TPLGValue> TPLG_Values_List = new List<TPLG.TPLGValue>();
                List<HPLG.HPLGValue> HPLG_Values_List = new List<HPLG.HPLGValue>();

                int StartPoint = 0;
                for (int HPLGCount = 0; HPLGCount < HPLGValueList.Count; HPLGCount++)
                {
                    HPLG.HPLGValue HPLG_Values = new HPLG.HPLGValue
                    {
                        HPLG_StartPoint = Convert.ToByte(StartPoint),
                        HPLG_Length = Convert.ToByte(HPLGValueList[HPLGCount].TPLGValueList.Count),
                        HPLG_PreviewGroup = new HPLG.HPLGValue.HPLG_PreviewGroups(HPLGValueList[HPLGCount].HPLG_PreviewGroup.GetPrevGroupArray()),
                        HPLG_NextGroup = new HPLG.HPLGValue.HPLG_NextGroups(HPLGValueList[HPLGCount].HPLG_NextGroup.GetNextGroupArray()),
                        RouteSetting = Convert.ToUInt32(HPLGValueList[HPLGCount].RouteSettings.RouteSettingValue),
                        HPLG_UnknownData2 = Convert.ToUInt32(HPLGValueList[HPLGCount].HPLG_UnknownData2)
                    };
                    HPLG_Values_List.Add(HPLG_Values);

                    for (int TPLGCount = 0; TPLGCount < HPLGValueList[HPLGCount].TPLGValueList.Count; TPLGCount++)
                    {
                        TPLG.TPLGValue TPLG_Values = new TPLG.TPLGValue
                        {
                            TPLG_Position = HPLGValueList[HPLGCount].TPLGValueList[TPLGCount].Positions.GetVector3D(),
                            TPLG_PointScaleValue = HPLGValueList[HPLGCount].TPLGValueList[TPLGCount].TPLG_PointScaleValue,
                            TPLG_UnknownData1 = HPLGValueList[HPLGCount].TPLGValueList[TPLGCount].TPLG_UnknownData1,
                            TPLG_UnknownData2 = HPLGValueList[HPLGCount].TPLGValueList[TPLGCount].TPLG_UnknownData2
                        };

                        TPLG_Values_List.Add(TPLG_Values);

                        StartPoint++;
                    }
                }

                TPLG TPLG = new TPLG(TPLG_Values_List);
                HPLG HPLG = new HPLG(HPLG_Values_List);

                HPLG_TPLG_Data = new HPLG_TPLGData(HPLG, TPLG);
            }
            if (HPLGValueList.Count == 0)
            {
                TPLG TPLG = new TPLG(new List<TPLG.TPLGValue>());
                HPLG HPLG = new HPLG(new List<HPLG.HPLGValue>());

                HPLG_TPLG_Data = new HPLG_TPLGData(HPLG, TPLG);
            }

            return HPLG_TPLG_Data;
        }
    }
}
