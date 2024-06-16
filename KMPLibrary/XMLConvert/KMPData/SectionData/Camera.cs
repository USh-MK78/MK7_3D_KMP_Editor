using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.XMLConvert.KMPData.SectionData
{
    public class Camera
    {
        [System.Xml.Serialization.XmlElement("Value")]
        public List<Camera_Value> Values { get; set; } = new List<Camera_Value>();
        public class Camera_Value
        {
            [System.Xml.Serialization.XmlAttribute("CameraType")]
            public byte CameraType { get; set; }

            [System.Xml.Serialization.XmlAttribute("NextCameraIndex")]
            public byte NextCameraIndex { get; set; }

            [System.Xml.Serialization.XmlAttribute("NextVideoIndex")]
            public byte NextVideoIndex { get; set; }

            [System.Xml.Serialization.XmlAttribute("Route_CameraIndex")]
            public byte Route_CameraIndex { get; set; }

            [System.Xml.Serialization.XmlAttribute("StartFlag")]
            public byte StartFlag { get; set; }

            [System.Xml.Serialization.XmlAttribute("VideoFlag")]
            public byte VideoFlag { get; set; }

            [System.Xml.Serialization.XmlAttribute("CameraActiveTime")]
            public float CameraActiveTime { get; set; }

            [System.Xml.Serialization.XmlElement("SpeedSetting")]
            public SpeedSettings SpeedSetting { get; set; }
            public class SpeedSettings
            {
                [System.Xml.Serialization.XmlAttribute("RouteSpeed")]
                public ushort RouteSpeed { get; set; }

                [System.Xml.Serialization.XmlAttribute("FOVSpeed")]
                public ushort FOVSpeed { get; set; }

                [System.Xml.Serialization.XmlAttribute("ViewpointSpeed")]
                public ushort ViewpointSpeed { get; set; }

                public SpeedSettings() { }

                public SpeedSettings(Format.SectionData.EMAC.EMACValue speedSetting)
                {
                    RouteSpeed = speedSetting.RouteSpeed;
                    FOVSpeed = speedSetting.FOVSpeed;
                    ViewpointSpeed = speedSetting.ViewpointSpeed;
                }
            }

            [System.Xml.Serialization.XmlElement("Position")]
            public Camera_Position Position { get; set; }
            public class Camera_Position
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Camera_Position() { }

                public Camera_Position(Vector3D vector3D)
                {
                    X = (float)vector3D.X;
                    Y = (float)vector3D.Y;
                    Z = (float)vector3D.Z;
                }

                public Vector3D ToVector3D()
                {
                    return new Vector3D(X, Y, Z);
                }
            }

            [System.Xml.Serialization.XmlElement("Rotation")]
            public Camera_Rotation Rotation { get; set; }
            public class Camera_Rotation
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                Camera_Rotation() { }

                public Camera_Rotation(Vector3D vector3D)
                {
                    X = (float)vector3D.X;
                    Y = (float)vector3D.Y;
                    Z = (float)vector3D.Z;
                }

                public Vector3D ToVector3D()
                {
                    return new Vector3D(X, Y, Z);
                }
            }

            [System.Xml.Serialization.XmlElement("FOVAngle")]
            public FOVAngleSetting FOVAngleSettings { get; set; }
            public class FOVAngleSetting
            {
                [System.Xml.Serialization.XmlAttribute("Start")]
                public float Start { get; set; }

                [System.Xml.Serialization.XmlAttribute("End")]
                public float End { get; set; }

                public FOVAngleSetting() { }

                public FOVAngleSetting(Format.SectionData.EMAC.EMACValue FOVAngleSetting)
                {
                    Start = FOVAngleSetting.FOVAngle_Start;
                    End = FOVAngleSetting.FOVAngle_End;
                }
            }

            [System.Xml.Serialization.XmlElement("ViewpointStart")]
            public Viewpoint_Start ViewpointStart { get; set; }
            public class Viewpoint_Start
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Viewpoint_Start() { }

                public Viewpoint_Start(Vector3D vector3D)
                {
                    X = (float)vector3D.X;
                    Y = (float)vector3D.Y;
                    Z = (float)vector3D.Z;
                }

                public Vector3D ToVector3D()
                {
                    return new Vector3D(X, Y, Z);
                }
            }

            [System.Xml.Serialization.XmlElement("ViewpointDestination")]
            public Viewpoint_Destination ViewpointDestination { get; set; }
            public class Viewpoint_Destination
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public float X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public float Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public float Z { get; set; }

                public Viewpoint_Destination() { }

                public Viewpoint_Destination(Vector3D vector3D)
                {
                    X = (float)vector3D.X;
                    Y = (float)vector3D.Y;
                    Z = (float)vector3D.Z;
                }

                public Vector3D ToVector3D()
                {
                    return new Vector3D(X, Y, Z);
                }
            }

            public Camera_Value() { }

            public Camera_Value(Format.SectionData.EMAC.EMACValue CameraValue)
            {
                SpeedSetting = new SpeedSettings(CameraValue);
                Position = new Camera_Position(CameraValue.EMAC_Position);
                Rotation = new Camera_Rotation(CameraValue.EMAC_Rotation);
                FOVAngleSettings = new FOVAngleSetting(CameraValue);
                ViewpointStart = new Viewpoint_Start(CameraValue.Viewpoint_Start);
                ViewpointDestination = new Viewpoint_Destination(CameraValue.Viewpoint_Destination);
                CameraType = CameraValue.CameraType;
                NextCameraIndex = CameraValue.NextCameraIndex;
                NextVideoIndex = CameraValue.EMAC_NextVideoIndex;
                Route_CameraIndex = CameraValue.EMAC_ITOP_CameraIndex;
                StartFlag = CameraValue.EMAC_StartFlag;
                VideoFlag = CameraValue.EMAC_VideoFlag;
                CameraActiveTime = CameraValue.Camera_Active_Time;
            }
        }

        public Camera() { }

        public Camera(Format.SectionData.EMAC eMAC_Section)
        {
            foreach (var CameraValue in eMAC_Section.EMACValue_List) Values.Add(new Camera_Value(CameraValue));
        }
    }

}
