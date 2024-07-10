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
    /// <summary>
    /// Route (PropertyGrid)
    /// </summary>
    public class Route_PGS
    {
        public List<ITOP_Route> ITOP_Route_List = new List<ITOP_Route>();
        public List<ITOP_Route> ITOP_RouteList { get => ITOP_Route_List; set => ITOP_Route_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class ITOP_Route
        {
            [ReadOnly(true)]
            public int GroupID { get; set; }

            public bool IsViewportVisible { get; set; } = true;

            public byte ITOP_Loop { get; set; }
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

                public ITOP_Point(ITOP.ITOP_Route.ITOP_Point ITOP_Point, int Group_ID, int InputID)
                {
                    GroupID = Group_ID;
                    ID = InputID;
                    Positions = new Position(ITOP_Point.ITOP_Point_Position);
                    ITOP_Point_RouteSpeed = ITOP_Point.ITOP_Point_RouteSpeed;
                    ITOP_PointSetting2 = ITOP_Point.ITOP_PointSetting2;
                }

                public ITOP_Point(KMPLibrary.XMLConvert.KMPData.SectionData.Route.Route_Group.Route_Point Route_Point, int Group_ID, int InputID)
                {
                    GroupID = Group_ID;
                    ID = InputID;
                    Positions = new Position(Route_Point.Position.ToVector3D());
                    ITOP_Point_RouteSpeed = Route_Point.RouteSpeed;
                    ITOP_PointSetting2 = Route_Point.PointSetting2;
                }

                public override string ToString()
                {
                    return "Point " + ID;
                }
            }

            public ITOP_Route(int InputID)
            {
                GroupID = InputID;
                ITOP_Loop = 0;
                ITOP_Smooth = 0;
                ITOP_Point_List = new List<ITOP_Point>();
            }

            public ITOP_Route(ITOP.ITOP_Route ITOP_Route, int InputID)
            {
                GroupID = InputID;
                ITOP_Loop = ITOP_Route.ITOP_LoopSetting;
                ITOP_Smooth = ITOP_Route.ITOP_SmoothSetting;

                for (int i = 0; i < ITOP_Route.ITOP_Route_NumOfPoint; i++)
                {
                    ITOP_Point_List.Add(new ITOP_Point(ITOP_Route.ITOP_Point_List[i], InputID, i));
                }
            }

            public ITOP_Route(KMPLibrary.XMLConvert.KMPData.SectionData.Route.Route_Group Route_Group, int InputID)
            {
                GroupID = InputID;
                ITOP_Loop = Route_Group.LoopSetting;
                ITOP_Smooth = Route_Group.SmoothSetting;

                for (int i = 0; i < Route_Group.Points.Count; i++)
                {
                    ITOP_Point_List.Add(new ITOP_Point(Route_Group.Points[i], InputID, i));
                }
            }

            public override string ToString()
            {
                return "Route " + GroupID;
            }
        }

        public Route_PGS(ITOP ITOP_Section)
        {
            for (int i = 0; i < ITOP_Section.ITOP_Route_List.Count; i++)
            {
                ITOP_Route_List.Add(new ITOP_Route(ITOP_Section.ITOP_Route_List[i], i));
            }
        }

        public Route_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.Route Route)
        {
            for (int i = 0; i < Route.Groups.Count; i++)
            {
                ITOP_RouteList.Add(new ITOP_Route(Route.Groups[i], i));
            }
        }

        public Route_PGS()
        {
            ITOP_RouteList = new List<ITOP_Route>();
        }

        public ITOP ToITOP()
        {
            List<ITOP.ITOP_Route> ITOP_Route_List = new List<ITOP.ITOP_Route>();

            for (int ITOPRouteCount = 0; ITOPRouteCount < ITOP_RouteList.Count; ITOPRouteCount++)
            {
                List<ITOP.ITOP_Route.ITOP_Point> ITOP_Point_List = new List<ITOP.ITOP_Route.ITOP_Point>();

                for (int ITOP_PointCount = 0; ITOP_PointCount < ITOP_RouteList[ITOPRouteCount].ITOP_PointList.Count; ITOP_PointCount++)
                {
                    ITOP.ITOP_Route.ITOP_Point ITOP_Points = new ITOP.ITOP_Route.ITOP_Point
                    {
                        ITOP_Point_Position = ITOP_RouteList[ITOPRouteCount].ITOP_PointList[ITOP_PointCount].Positions.GetVector3D(),
                        ITOP_Point_RouteSpeed = ITOP_RouteList[ITOPRouteCount].ITOP_PointList[ITOP_PointCount].ITOP_Point_RouteSpeed,
                        ITOP_PointSetting2 = ITOP_RouteList[ITOPRouteCount].ITOP_PointList[ITOP_PointCount].ITOP_PointSetting2
                    };

                    ITOP_Point_List.Add(ITOP_Points);
                }

                ITOP.ITOP_Route ITOP_Route = new ITOP.ITOP_Route(ITOP_RouteList[ITOPRouteCount].ITOP_Loop, ITOP_RouteList[ITOPRouteCount].ITOP_Smooth, ITOP_Point_List);

                ITOP_Route_List.Add(ITOP_Route);
            }

            return new ITOP(ITOP_Route_List);
        }
    }
}
