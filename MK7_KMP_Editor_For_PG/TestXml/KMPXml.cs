using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MK7_KMP_Editor_For_PG_.TestXml
{
    [System.Xml.Serialization.XmlRoot("KMPXml")]
    public class KMPXml
    {
        [System.Xml.Serialization.XmlElement("StartPosition")]
        public StartPosition startPositions { get; set; }
        public class StartPosition
        {
            [System.Xml.Serialization.XmlElement("Value")]
            public List<StartPosition_Value> startPosition_Value { get; set; }
            public class StartPosition_Value
            {
                [System.Xml.Serialization.XmlElement("Position")]
                public StartPosition_Position Position { get; set; }
                public class StartPosition_Position
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }

                [System.Xml.Serialization.XmlElement("Rotation")]
                public StartPosition_Rotation Rotation { get; set; }
                public class StartPosition_Rotation
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }

                [System.Xml.Serialization.XmlAttribute("Player_Index")]
                public ushort Player_Index { get; set; }

                [System.Xml.Serialization.XmlAttribute("TPTK_UnkBytes")]
                public ushort TPTK_UnkBytes { get; set; }
            }
        }

        [System.Xml.Serialization.XmlElement("EnemyRoute")]
        public EnemyRoute EnemyRoutes { get; set; }
        public class EnemyRoute
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<EnemyRoute_Group> Groups { get; set; }
            public class EnemyRoute_Group
            {
                [System.Xml.Serialization.XmlElement("Previous")]
                public ER_PreviousGroup PreviousGroups { get; set; }
                public class ER_PreviousGroup
                {
                    [System.Xml.Serialization.XmlAttribute("Prev0")]
                    public ushort Prev0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev1")]
                    public ushort Prev1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev2")]
                    public ushort Prev2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev3")]
                    public ushort Prev3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev4")]
                    public ushort Prev4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev5")]
                    public ushort Prev5 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev6")]
                    public ushort Prev6 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev7")]
                    public ushort Prev7 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev8")]
                    public ushort Prev8 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev9")]
                    public ushort Prev9 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev10")]
                    public ushort Prev10 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev11")]
                    public ushort Prev11 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev12")]
                    public ushort Prev12 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev13")]
                    public ushort Prev13 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev14")]
                    public ushort Prev14 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev15")]
                    public ushort Prev15 { get; set; }
                }

                [System.Xml.Serialization.XmlElement("Next")]
                public ER_NextGroup NextGroups { get; set; }
                public class ER_NextGroup
                {
                    [System.Xml.Serialization.XmlAttribute("Next0")]
                    public ushort Next0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next1")]
                    public ushort Next1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next2")]
                    public ushort Next2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next3")]
                    public ushort Next3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next4")]
                    public ushort Next4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next5")]
                    public ushort Next5 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next6")]
                    public ushort Next6 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next7")]
                    public ushort Next7 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next8")]
                    public ushort Next8 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next9")]
                    public ushort Next9 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next10")]
                    public ushort Next10 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next11")]
                    public ushort Next11 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next12")]
                    public ushort Next12 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next13")]
                    public ushort Next13 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next14")]
                    public ushort Next14 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next15")]
                    public ushort Next15 { get; set; }
                }

                [System.Xml.Serialization.XmlAttribute("Unknown1")]
                public uint Unknown1 { get; set; }

                [System.Xml.Serialization.XmlElement("Point")]
                public List<EnemyRoute_Point> Points { get; set; }
                public class EnemyRoute_Point
                {
                    [System.Xml.Serialization.XmlElement("Position")]
                    public EnemyRoute_Position Position { get; set; }
                    public class EnemyRoute_Position
                    {
                        [System.Xml.Serialization.XmlAttribute("X")]
                        public float X { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Y")]
                        public float Y { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Z")]
                        public float Z { get; set; }

                        public Vector3D ToVector3D()
                        {
                            return new Vector3D(X, Y, Z);
                        }
                    }

                    [System.Xml.Serialization.XmlAttribute("MushSetting")]
                    public ushort MushSetting { get; set; }

                    [System.Xml.Serialization.XmlAttribute("DriftSetting")]
                    public byte DriftSetting { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Flags")]
                    public byte Flags { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Control")]
                    public float Control { get; set; }

                    [System.Xml.Serialization.XmlAttribute("PathFind")]
                    public short PathFindOption { get; set; }

                    [System.Xml.Serialization.XmlAttribute("MaxSearchYOffset")]
                    public short MaxSearchYOffset { get; set; }
                }
            }
        }

        [System.Xml.Serialization.XmlElement("ItemRoute")]
        public ItemRoute ItemRoutes { get; set; }
        public class ItemRoute
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<ItemRoute_Group> Groups { get; set; }
            public class ItemRoute_Group
            {
                [System.Xml.Serialization.XmlElement("Previous")]
                public IR_PreviousGroup PreviousGroups { get; set; }
                public class IR_PreviousGroup
                {
                    [System.Xml.Serialization.XmlAttribute("Prev0")]
                    public ushort Prev0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev1")]
                    public ushort Prev1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev2")]
                    public ushort Prev2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev3")]
                    public ushort Prev3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev4")]
                    public ushort Prev4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev5")]
                    public ushort Prev5 { get; set; }
                }

                [System.Xml.Serialization.XmlElement("Next")]
                public IR_NextGroup NextGroups { get; set; }
                public class IR_NextGroup
                {
                    [System.Xml.Serialization.XmlAttribute("Next0")]
                    public ushort Next0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next1")]
                    public ushort Next1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next2")]
                    public ushort Next2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next3")]
                    public ushort Next3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next4")]
                    public ushort Next4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next5")]
                    public ushort Next5 { get; set; }
                }

                [System.Xml.Serialization.XmlElement("Point")]
                public List<ItemRoute_Point> Points { get; set; }
                public class ItemRoute_Point
                {
                    [System.Xml.Serialization.XmlElement("Position")]
                    public ItemRoute_Position Position { get; set; }
                    public class ItemRoute_Position
                    {
                        [System.Xml.Serialization.XmlAttribute("X")]
                        public float X { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Y")]
                        public float Y { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Z")]
                        public float Z { get; set; }

                        public Vector3D ToVector3D()
                        {
                            return new Vector3D(X, Y, Z);
                        }
                    }

                    [System.Xml.Serialization.XmlAttribute("GravityMode")]
                    public ushort GravityMode { get; set; }

                    [System.Xml.Serialization.XmlAttribute("PlayerScanRadius")]
                    public ushort PlayerScanRadius { get; set; }

                    [System.Xml.Serialization.XmlAttribute("PointSize")]
                    public float PointSize { get; set; }
                }
            }
        }

        [System.Xml.Serialization.XmlElement("Checkpoint")]
        public Checkpoint Checkpoints { get; set; }
        public class Checkpoint
        {
            [System.Xml.Serialization.XmlElement("Groups")]
            public List<Checkpoint_Group> Groups { get; set; }
            public class Checkpoint_Group
            {
                [System.Xml.Serialization.XmlElement("Previous")]
                public CP_PreviousGroup PreviousGroups { get; set; }
                public class CP_PreviousGroup
                {
                    [System.Xml.Serialization.XmlAttribute("Prev0")]
                    public byte Prev0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev1")]
                    public byte Prev1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev2")]
                    public byte Prev2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev3")]
                    public byte Prev3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev4")]
                    public byte Prev4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev5")]
                    public byte Prev5 { get; set; }
                }

                [System.Xml.Serialization.XmlElement("Next")]
                public CP_NextGroup NextGroups { get; set; }
                public class CP_NextGroup
                {
                    [System.Xml.Serialization.XmlAttribute("Next0")]
                    public byte Next0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next1")]
                    public byte Next1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next2")]
                    public byte Next2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next3")]
                    public byte Next3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next4")]
                    public byte Next4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next5")]
                    public byte Next5 { get; set; }
                }

                [System.Xml.Serialization.XmlAttribute("UnkBytes1")]
                public ushort UnkBytes1 { get; set; }

                [System.Xml.Serialization.XmlElement("Point")]
                public List<Checkpoint_Point> Points { get; set; }
                public class Checkpoint_Point
                {
                    [System.Xml.Serialization.XmlElement("Position2DLeft")]
                    public Position2D_Left Position_2D_Left { get; set; }
                    public class Position2D_Left
                    {
                        [System.Xml.Serialization.XmlAttribute("X")]
                        public float X { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Y")]
                        public float Y { get; set; }

                        public Vector2 ToVector2()
                        {
                            return new Vector2(X, Y);
                        }
                    }

                    [System.Xml.Serialization.XmlElement("Position2DRight")]
                    public Position2D_Right Position_2D_Right { get; set; }
                    public class Position2D_Right
                    {
                        [System.Xml.Serialization.XmlAttribute("X")]
                        public float X { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Y")]
                        public float Y { get; set; }

                        public Vector2 ToVector2()
                        {
                            return new Vector2(X, Y);
                        }
                    }

                    [System.Xml.Serialization.XmlAttribute("RespawnID")]
                    public byte RespawnID { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Checkpoint_Type")]
                    public byte Checkpoint_Type { get; set; }

                    [System.Xml.Serialization.XmlAttribute("PreviousCheckPoint")]
                    public byte PreviousCheckPoint { get; set; }

                    [System.Xml.Serialization.XmlAttribute("NextCheckPoint")]
                    public byte NextCheckPoint { get; set; }

                    [System.Xml.Serialization.XmlAttribute("ClipID")]
                    public byte ClipID { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Section")]
                    public byte Section { get; set; }

                    [System.Xml.Serialization.XmlAttribute("UnkBytes3")]
                    public byte UnkBytes3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("UnkBytes4")]
                    public byte UnkBytes4 { get; set; }
                }
            }
        }

        [System.Xml.Serialization.XmlElement("Object")]
        public Object Objects { get; set; }
        public class Object
        {
            [System.Xml.Serialization.XmlElement("Value")]
            public List<Object_Value> Object_Values { get; set; }
            public class Object_Value
            {
                [System.Xml.Serialization.XmlElement("Position")]
                public Object_Position Position { get; set; }
                public class Object_Position
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }

                [System.Xml.Serialization.XmlElement("Rotation")]
                public Object_Rotation Rotation { get; set; }
                public class Object_Rotation
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }

                [System.Xml.Serialization.XmlElement("Scale")]
                public Object_Scale Scale { get; set; }
                public class Object_Scale
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }

                [System.Xml.Serialization.XmlElement("SpecificSetting")]
                public SpecificSettings SpecificSetting { get; set; }
                public class SpecificSettings
                {
                    [System.Xml.Serialization.XmlAttribute("Value0")]
                    public ushort Value0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Value1")]
                    public ushort Value1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Value2")]
                    public ushort Value2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Value3")]
                    public ushort Value3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Value4")]
                    public ushort Value4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Value5")]
                    public ushort Value5 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Value6")]
                    public ushort Value6 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Value7")]
                    public ushort Value7 { get; set; }
                }

                [System.Xml.Serialization.XmlAttribute("ObjectID")]
                public string ObjectID { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnkByte1")]
                public string UnkByte1 { get; set; }

                [System.Xml.Serialization.XmlAttribute("RouteIDIndex")]
                public ushort RouteIDIndex { get; set; }

                [System.Xml.Serialization.XmlAttribute("PresenceSetting")]
                public ushort PresenceSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnkByte2")]
                public string UnkByte2 { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnkByte3")]
                public ushort UnkByte3 { get; set; }
            }
        }

        [System.Xml.Serialization.XmlElement("Route")]
        public Route Routes { get; set; }
        public class Route
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<Route_Group> Groups { get; set; }
            public class Route_Group
            {
                [System.Xml.Serialization.XmlAttribute("Roop")]
                public byte RoopSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("Smooth")]
                public byte SmoothSetting { get; set; }

                [System.Xml.Serialization.XmlElement("Point")]
                public List<Route_Point> Points { get; set; }
                public class Route_Point
                {
                    [System.Xml.Serialization.XmlAttribute("RouteSpeed")]
                    public ushort RouteSpeed { get; set; }

                    [System.Xml.Serialization.XmlAttribute("PointSetting2")]
                    public ushort PointSetting2 { get; set; }

                    [System.Xml.Serialization.XmlElement("Position")]
                    public Route_Position Position { get; set; }
                    public class Route_Position
                    {
                        [System.Xml.Serialization.XmlAttribute("X")]
                        public float X { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Y")]
                        public float Y { get; set; }

                        [System.Xml.Serialization.XmlAttribute("Z")]
                        public float Z { get; set; }

                        public Vector3D ToVector3D()
                        {
                            return new Vector3D(X, Y, Z);
                        }
                    }
                }
            }
        }

        [System.Xml.Serialization.XmlElement("Area")]
        public Area Areas { get; set; }
        public class Area
        {
            [System.Xml.Serialization.XmlElement("Value")]
            public List<Area_Value> Area_Values { get; set; }
            public class Area_Value
            {
                [System.Xml.Serialization.XmlAttribute("AreaType")]
                public byte AreaType { get; set; }

                [System.Xml.Serialization.XmlAttribute("AreaMode")]
                public byte AreaMode { get; set; }

                [System.Xml.Serialization.XmlAttribute("CameraIndex")]
                public byte CameraIndex { get; set; }

                [System.Xml.Serialization.XmlAttribute("Priority")]
                public byte Priority { get; set; }

                [System.Xml.Serialization.XmlAttribute("Setting1")]
                public ushort Setting1 { get; set; }

                [System.Xml.Serialization.XmlAttribute("Setting2")]
                public ushort Setting2 { get; set; }

                [System.Xml.Serialization.XmlAttribute("RouteID")]
                public byte RouteID { get; set; }

                [System.Xml.Serialization.XmlAttribute("EnemyID")]
                public byte EnemyID { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnkByte4")]
                public ushort UnkByte4 { get; set; }

                [System.Xml.Serialization.XmlElement("Position")]
                public Area_Position Position { get; set; }
                public class Area_Position
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }

                [System.Xml.Serialization.XmlElement("Rotation")]
                public Area_Rotation Rotation { get; set; }
                public class Area_Rotation
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }

                [System.Xml.Serialization.XmlElement("Scale")]
                public Area_Scale Scale { get; set; }
                public class Area_Scale
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }
            }
        }

        [System.Xml.Serialization.XmlElement("Camera")]
        public Camera Cameras { get; set; }
        public class Camera
        {
            [System.Xml.Serialization.XmlElement("Value")]
            public List<Camera_Value> Values { get; set; }
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

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }
            }
        }

        [System.Xml.Serialization.XmlElement("JugemPoint")]
        public JugemPoint JugemPoints { get; set; }
        public class JugemPoint
        {
            [System.Xml.Serialization.XmlElement("Value")]
            public List<JugemPoint_Value> Values { get; set; }
            public class JugemPoint_Value
            {
                [System.Xml.Serialization.XmlAttribute("RespawnID")]
                public ushort RespawnID { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnkBytes1")]
                public ushort UnkBytes1 { get; set; }

                [System.Xml.Serialization.XmlElement("Position")]
                public JugemPoint_Position Position { get; set; }
                public class JugemPoint_Position
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }

                [System.Xml.Serialization.XmlElement("Rotation")]
                public JugemPoint_Rotation Rotation { get; set; }
                public class JugemPoint_Rotation
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Vector3D ToVector3D()
                    {
                        return new Vector3D(X, Y, Z);
                    }
                }
            }
        }

        [System.Xml.Serialization.XmlElement("StageInfo")]
        public StageInfo Stage_Info { get; set; }
        public class StageInfo
        {
            [System.Xml.Serialization.XmlAttribute("Unknown1")]
            public uint Unknown1 { get; set; }

            [System.Xml.Serialization.XmlAttribute("LapCount")]
            public byte LapCount { get; set; }

            [System.Xml.Serialization.XmlAttribute("PolePosition")]
            public byte PolePosition { get; set; }

            [System.Xml.Serialization.XmlAttribute("Unknown2")]
            public byte Unknown2 { get; set; }

            [System.Xml.Serialization.XmlAttribute("Unknown3")]
            public byte Unknown3 { get; set; }

            [System.Xml.Serialization.XmlElement("RBAColor")]
            public RGBA RGBAColor { get; set; }
            public class RGBA
            {
                [System.Xml.Serialization.XmlAttribute("R")]
                public byte R { get; set; }

                [System.Xml.Serialization.XmlAttribute("G")]
                public byte G { get; set; }

                [System.Xml.Serialization.XmlAttribute("B")]
                public byte B { get; set; }

                [System.Xml.Serialization.XmlAttribute("A")]
                public byte A { get; set; }

                [System.Xml.Serialization.XmlAttribute("FlareAlpha")]
                public uint FlareAlpha { get; set; }
            }
        }

        [System.Xml.Serialization.XmlElement("GlideRoute")]
        public GlideRoute GlideRoutes { get; set; }
        public class GlideRoute
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<GlideRoute_Group> Groups { get; set; }
            public class GlideRoute_Group
            {
                [System.Xml.Serialization.XmlElement("Previous")]
                public GR_PreviousGroup PreviousGroups { get; set; }
                public class GR_PreviousGroup
                {
                    [System.Xml.Serialization.XmlAttribute("Prev0")]
                    public byte Prev0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev1")]
                    public byte Prev1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev2")]
                    public byte Prev2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev3")]
                    public byte Prev3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev4")]
                    public byte Prev4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Prev5")]
                    public byte Prev5 { get; set; }
                }

                [System.Xml.Serialization.XmlElement("Next")]
                public GR_NextGroup NextGroups { get; set; }
                public class GR_NextGroup
                {
                    [System.Xml.Serialization.XmlAttribute("Next0")]
                    public byte Next0 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next1")]
                    public byte Next1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next2")]
                    public byte Next2 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next3")]
                    public byte Next3 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next4")]
                    public byte Next4 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Next5")]
                    public byte Next5 { get; set; }
                }

                [System.Xml.Serialization.XmlAttribute("RouteSetting")]
                public uint RouteSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnkBytes2")]
                public uint UnkBytes2 { get; set; }

                [System.Xml.Serialization.XmlElement("Point")]
                public List<GlideRoute_Point> Points { get; set; }
                public class GlideRoute_Point
                {
                    [System.Xml.Serialization.XmlAttribute("PointScale")]
                    public float PointScale { get; set; }

                    [System.Xml.Serialization.XmlAttribute("UnkBytes1")]
                    public uint UnkBytes1 { get; set; }

                    [System.Xml.Serialization.XmlAttribute("UnkBytes2")]
                    public uint UnkBytes2 { get; set; }

                    [System.Xml.Serialization.XmlElement("Position")]
                    public GlideRoute_Position Position { get; set; }
                    public class GlideRoute_Position
                    {
                        [System.Xml.Serialization.XmlElement("X")]
                        public float X { get; set; }

                        [System.Xml.Serialization.XmlElement("Y")]
                        public float Y { get; set; }

                        [System.Xml.Serialization.XmlElement("Z")]
                        public float Z { get; set; }

                        public Vector3D ToVector3D()
                        {
                            return new Vector3D(X, Y, Z);
                        }
                    }
                }
            }
        }
    }

    public class KMPXmlSetting
    {
        public enum Section
        {
            KartPoint, 
            EnemyRoutes, 
            ItemRoutes, 
            CheckPoint, 
            Obj, 
            Route, 
            Area, 
            Camera, 
            JugemPoint, 
            GlideRoutes
        }
    }
}
