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
    /// <summary>
    /// Camera (PropertyGrid)
    /// </summary>
    public class Camera_PGS
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
                    _X = HTK_3DES.RadianToAngle(vector3D.X);
                    _Y = HTK_3DES.RadianToAngle(vector3D.Y);
                    _Z = HTK_3DES.RadianToAngle(vector3D.Z);
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

            public EMACValue(EMAC.EMACValue EMACValue, int InputID)
            {
                ID = InputID;
                CameraType = EMACValue.CameraType;
                NextCameraIndex = EMACValue.NextCameraIndex;
                EMAC_NextVideoIndex = EMACValue.EMAC_NextVideoIndex;
                EMAC_ITOP_CameraIndex = EMACValue.EMAC_ITOP_CameraIndex;
                SpeedSettings = new SpeedSetting(EMACValue.RouteSpeed, EMACValue.FOVSpeed, EMACValue.ViewpointSpeed);
                EMAC_StartFlag = EMACValue.EMAC_StartFlag;
                EMAC_VideoFlag = EMACValue.EMAC_VideoFlag;
                Positions = new Position(EMACValue.EMAC_Position);
                Rotations = new Rotation(EMACValue.EMAC_Rotation);
                FOVAngleSettings = new FOVAngleSetting(EMACValue.FOVAngle_Start, EMACValue.FOVAngle_End);
                Viewpoint_Destination = new ViewpointDestination(EMACValue.Viewpoint_Destination);
                Viewpoint_Start = new ViewpointStart(EMACValue.Viewpoint_Start);
                Camera_Active_Time = EMACValue.Camera_Active_Time;
            }

            public EMACValue(KMPLibrary.XMLConvert.KMPData.SectionData.Camera.Camera_Value Camera_Value, int InputID)
            {
                ID = InputID;
                CameraType = Camera_Value.CameraType;
                NextCameraIndex = Camera_Value.NextCameraIndex;
                EMAC_NextVideoIndex = Camera_Value.NextVideoIndex;
                EMAC_ITOP_CameraIndex = Camera_Value.Route_CameraIndex;
                SpeedSettings = new SpeedSetting(Camera_Value.SpeedSetting.RouteSpeed, Camera_Value.SpeedSetting.FOVSpeed, Camera_Value.SpeedSetting.ViewpointSpeed);
                EMAC_StartFlag = Camera_Value.StartFlag;
                EMAC_VideoFlag = Camera_Value.VideoFlag;
                Positions = new Position(Camera_Value.Position.ToVector3D());
                Rotations = new Rotation(Camera_Value.Rotation.ToVector3D());
                FOVAngleSettings = new FOVAngleSetting(Camera_Value.FOVAngleSettings.Start, Camera_Value.FOVAngleSettings.End);
                Viewpoint_Destination = new ViewpointDestination(Camera_Value.ViewpointDestination.ToVector3D());
                Viewpoint_Start = new ViewpointStart(Camera_Value.ViewpointStart.ToVector3D());
                Camera_Active_Time = Camera_Value.CameraActiveTime;
            }

            public override string ToString()
            {
                return "Camera " + ID;
            }
        }

        public Camera_PGS(EMAC EMAC_Section)
        {
            for (int i = 0; i < EMAC_Section.NumOfEntries; i++) EMACValueList.Add(new EMACValue(EMAC_Section.EMACValue_List[i], i));
        }

        public Camera_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.Camera Camera)
        {
            for (int i = 0; i < Camera.Values.Count; i++) EMACValueList.Add(new EMACValue(Camera.Values[i], i));
        }

        public Camera_PGS()
        {
            EMACValueList = new List<EMACValue>();
        }

        public EMAC ToEMAC()
        {
            List<EMAC.EMACValue> EMAC_Value_List = new List<EMAC.EMACValue>();

            for (int EMACCount = 0; EMACCount < EMACValueList.Count; EMACCount++)
            {
                double RX = HTK_3DES.AngleToRadian(EMACValueList[EMACCount].Rotations.X);
                double RY = HTK_3DES.AngleToRadian(EMACValueList[EMACCount].Rotations.Y);
                double RZ = HTK_3DES.AngleToRadian(EMACValueList[EMACCount].Rotations.Z);

                EMAC.EMACValue EMAC_Values = new EMAC.EMACValue
                {
                    CameraType = EMACValueList[EMACCount].CameraType,
                    NextCameraIndex =EMACValueList[EMACCount].NextCameraIndex,
                    EMAC_NextVideoIndex = EMACValueList[EMACCount].EMAC_NextVideoIndex,
                    EMAC_ITOP_CameraIndex = EMACValueList[EMACCount].EMAC_ITOP_CameraIndex,
                    RouteSpeed = EMACValueList[EMACCount].SpeedSettings.RouteSpeed,
                    FOVSpeed = EMACValueList[EMACCount].SpeedSettings.FOVSpeed,
                    ViewpointSpeed = EMACValueList[EMACCount].SpeedSettings.ViewpointSpeed,
                    EMAC_StartFlag = EMACValueList[EMACCount].EMAC_StartFlag,
                    EMAC_VideoFlag = EMACValueList[EMACCount].EMAC_VideoFlag,
                    EMAC_Position = EMACValueList[EMACCount].Positions.GetVector3D(),
                    EMAC_Rotation = new Vector3D(RX, RY, RZ),
                    FOVAngle_Start = EMACValueList[EMACCount].FOVAngleSettings.FOVAngle_Start,
                    FOVAngle_End = EMACValueList[EMACCount].FOVAngleSettings.FOVAngle_End,
                    Viewpoint_Start = EMACValueList[EMACCount].Viewpoint_Start.GetVector3D(),
                    Viewpoint_Destination = EMACValueList[EMACCount].Viewpoint_Destination.GetVector3D(),
                    Camera_Active_Time = EMACValueList[EMACCount].Camera_Active_Time
                };

                EMAC_Value_List.Add(EMAC_Values);
            }

            return new EMAC(EMAC_Value_List);
        }
    }
}
