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
            public List<StartPosition_Value> StartPositionValues { get; set; } = new List<StartPosition_Value>();
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

                    public StartPosition_Position() { }

                    public StartPosition_Position(Vector3D vector3D)
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
                public StartPosition_Rotation Rotation { get; set; }
                public class StartPosition_Rotation
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public StartPosition_Rotation() { }

                    public StartPosition_Rotation(Vector3D vector3D)
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

                [System.Xml.Serialization.XmlAttribute("Player_Index")]
                public ushort Player_Index { get; set; }

                [System.Xml.Serialization.XmlAttribute("TPTK_UnkBytes")]
                public ushort TPTK_UnkBytes { get; set; }

                public StartPosition_Value() { }

                public StartPosition_Value(KMPPropertyGridSettings.TPTK_Section.TPTKValue StartPositionValue)
                {
                    Position = new StartPosition_Position(StartPositionValue.Position_Value.GetVector3D());
                    Rotation = new StartPosition_Rotation(StartPositionValue.Rotate_Value.GetVector3D());
                    Player_Index = StartPositionValue.Player_Index;
                    TPTK_UnkBytes = StartPositionValue.TPTK_UnkBytes;
                }
            }

            public StartPosition() { }

            public StartPosition(KMPPropertyGridSettings.TPTK_Section tPTK_Section)
            {
                foreach (var StartPositions in tPTK_Section.TPTKValueList) StartPositionValues.Add(new StartPosition_Value(StartPositions));
            }
        }

        [System.Xml.Serialization.XmlElement("EnemyRoute")]
        public EnemyRoute EnemyRoutes { get; set; }
        public class EnemyRoute
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<EnemyRoute_Group> Groups { get; set; } = new List<EnemyRoute_Group>();
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

                    public ER_PreviousGroup() { }

                    public ER_PreviousGroup(KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.HPNE_PreviewGroups PreviewGroups)
                    {
                        Prev0 = PreviewGroups.Prev0;
                        Prev1 = PreviewGroups.Prev1;
                        Prev2 = PreviewGroups.Prev2;
                        Prev3 = PreviewGroups.Prev3;
                        Prev4 = PreviewGroups.Prev4;
                        Prev5 = PreviewGroups.Prev5;
                        Prev6 = PreviewGroups.Prev6;
                        Prev7 = PreviewGroups.Prev7;
                        Prev8 = PreviewGroups.Prev8;
                        Prev9 = PreviewGroups.Prev9;
                        Prev10 = PreviewGroups.Prev10;
                        Prev11 = PreviewGroups.Prev11;
                        Prev12 = PreviewGroups.Prev12;
                        Prev13 = PreviewGroups.Prev13;
                        Prev14 = PreviewGroups.Prev14;
                        Prev15 = PreviewGroups.Prev15;
                    }
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

                    public ER_NextGroup() { }

                    public ER_NextGroup(KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.HPNE_NextGroups NextGroups)
                    {
                        Next0 = NextGroups.Next0;
                        Next1 = NextGroups.Next1;
                        Next2 = NextGroups.Next2;
                        Next3 = NextGroups.Next3;
                        Next4 = NextGroups.Next4;
                        Next5 = NextGroups.Next5;
                        Next6 = NextGroups.Next6;
                        Next7 = NextGroups.Next7;
                        Next8 = NextGroups.Next8;
                        Next9 = NextGroups.Next9;
                        Next10 = NextGroups.Next10;
                        Next11 = NextGroups.Next11;
                        Next12 = NextGroups.Next12;
                        Next13 = NextGroups.Next13;
                        Next14 = NextGroups.Next14;
                        Next15 = NextGroups.Next15;
                    }
                }

                [System.Xml.Serialization.XmlAttribute("Unknown1")]
                public uint Unknown1 { get; set; }

                [System.Xml.Serialization.XmlArray("Points")]
                [System.Xml.Serialization.XmlArrayItem("Point")]
                public List<EnemyRoute_Point> Points { get; set; } = new List<EnemyRoute_Point>();
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

                        public EnemyRoute_Position() { }

                        public EnemyRoute_Position(Vector3D vector3D)
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

                    public EnemyRoute_Point() { }

                    public EnemyRoute_Point(KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue EnemyPoint)
                    {
                        Position = new EnemyRoute_Position(EnemyPoint.Positions.GetVector3D());
                        Control = EnemyPoint.Control;
                        DriftSetting = EnemyPoint.DriftSettings.DriftSettingValue;
                        MushSetting = EnemyPoint.MushSettings.MushSettingValue;
                        Flags = EnemyPoint.FlagSettings.Flags;
                        PathFindOption = EnemyPoint.PathFindOptions.PathFindOptionValue;
                        MaxSearchYOffset = EnemyPoint.MaxSearchYOffset.MaxSearchYOffsetValue;
                    }
                }

                public EnemyRoute_Group() { }

                public EnemyRoute_Group(KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue hPNEValue)
                {
                    PreviousGroups = new ER_PreviousGroup(hPNEValue.HPNEPreviewGroups);
                    NextGroups = new ER_NextGroup(hPNEValue.HPNENextGroups);
                    Unknown1 = hPNEValue.HPNE_UnkBytes1;

                    foreach (var EnemyPoint in hPNEValue.TPNEValueList) Points.Add(new EnemyRoute_Point(EnemyPoint));
                }
            }

            public EnemyRoute() { }

            public EnemyRoute(KMPPropertyGridSettings.HPNE_TPNE_Section hPNE_TPNE_Section)
            {
                foreach (var Group in hPNE_TPNE_Section.HPNEValueList) Groups.Add(new EnemyRoute_Group(Group));
            }
        }

        [System.Xml.Serialization.XmlElement("ItemRoute")]
        public ItemRoute ItemRoutes { get; set; }
        public class ItemRoute
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<ItemRoute_Group> Groups { get; set; } = new List<ItemRoute_Group>();
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

                    public IR_PreviousGroup() { }

                    public IR_PreviousGroup(KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.HPTI_PreviewGroups PreviewGroups)
                    {
                        Prev0 = PreviewGroups.Prev0;
                        Prev1 = PreviewGroups.Prev1;
                        Prev2 = PreviewGroups.Prev2;
                        Prev3 = PreviewGroups.Prev3;
                        Prev4 = PreviewGroups.Prev4;
                        Prev5 = PreviewGroups.Prev5;
                    }
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

                    public IR_NextGroup() { }

                    public IR_NextGroup(KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.HPTI_NextGroups NextGroups)
                    {
                        Next0 = NextGroups.Next0;
                        Next1 = NextGroups.Next1;
                        Next2 = NextGroups.Next2;
                        Next3 = NextGroups.Next3;
                        Next4 = NextGroups.Next4;
                        Next5 = NextGroups.Next5;
                    }
                }

                [System.Xml.Serialization.XmlArray("Points")]
                [System.Xml.Serialization.XmlArrayItem("Point")]
                public List<ItemRoute_Point> Points { get; set; } = new List<ItemRoute_Point>();
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

                        public ItemRoute_Position() { }

                        public ItemRoute_Position(Vector3D vector3D)
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

                    [System.Xml.Serialization.XmlAttribute("GravityMode")]
                    public ushort GravityMode { get; set; }

                    [System.Xml.Serialization.XmlAttribute("PlayerScanRadius")]
                    public ushort PlayerScanRadius { get; set; }

                    [System.Xml.Serialization.XmlAttribute("PointSize")]
                    public float PointSize { get; set; }

                    public ItemRoute_Point() { }

                    public ItemRoute_Point(KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue ItemPoint)
                    {
                        GravityMode = ItemPoint.GravityModeSettings.GravityModeValue;
                        PlayerScanRadius = ItemPoint.PlayerScanRadiusSettings.PlayerScanRadiusValue;
                        PointSize = ItemPoint.TPTI_PointSize;
                        Position = new ItemRoute_Position(ItemPoint.TPTI_Positions.GetVector3D());
                    }
                }

                public ItemRoute_Group() { }

                public ItemRoute_Group(KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue ItemRouteGroups)
                {
                    PreviousGroups = new IR_PreviousGroup(ItemRouteGroups.HPTI_PreviewGroup);
                    NextGroups = new IR_NextGroup(ItemRouteGroups.HPTI_NextGroup);

                    foreach (var Point in ItemRouteGroups.TPTIValueList) Points.Add(new ItemRoute_Point(Point));
                }
            }

            public ItemRoute() { }

            public ItemRoute(KMPPropertyGridSettings.HPTI_TPTI_Section hPTI_TPTI_Section)
            {
                foreach (var Group in hPTI_TPTI_Section.HPTIValueList) Groups.Add(new ItemRoute_Group(Group));
            }
        }

        [System.Xml.Serialization.XmlElement("Checkpoint")]
        public Checkpoint Checkpoints { get; set; }
        public class Checkpoint
        {
            [System.Xml.Serialization.XmlElement("Groups")]
            public List<Checkpoint_Group> Groups { get; set; } = new List<Checkpoint_Group>();
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

                    public CP_PreviousGroup() { }

                    public CP_PreviousGroup(KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.HPKC_PreviewGroups PreviewGroups)
                    {
                        Prev0 = PreviewGroups.Prev0;
                        Prev1 = PreviewGroups.Prev1;
                        Prev2 = PreviewGroups.Prev2;
                        Prev3 = PreviewGroups.Prev3;
                        Prev4 = PreviewGroups.Prev4;
                        Prev5 = PreviewGroups.Prev5;
                    }
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

                    public CP_NextGroup() { }

                    public CP_NextGroup(KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.HPKC_NextGroups NextGroups)
                    {
                        Next0 = NextGroups.Next0;
                        Next1 = NextGroups.Next1;
                        Next2 = NextGroups.Next2;
                        Next3 = NextGroups.Next3;
                        Next4 = NextGroups.Next4;
                        Next5 = NextGroups.Next5;
                    }
                }

                [System.Xml.Serialization.XmlAttribute("UnkBytes1")]
                public ushort UnkBytes1 { get; set; }

                [System.Xml.Serialization.XmlArray("Points")]
                [System.Xml.Serialization.XmlArrayItem("Point")]
                public List<Checkpoint_Point> Points { get; set; } = new List<Checkpoint_Point>();
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

                        public Position2D_Left() { }

                        public Vector2 ToVector2()
                        {
                            return new Vector2(X, Y);
                        }

                        public Position2D_Left(KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue.Position2D_Left position2D_Left)
                        {
                            X = position2D_Left.X;
                            Y = position2D_Left.Y;
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

                        public Position2D_Right() { }

                        public Position2D_Right(KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue.Position2D_Right position2D_Right)
                        {
                            X = position2D_Right.X;
                            Y = position2D_Right.Y;
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

                    public Checkpoint_Point() { }

                    public Checkpoint_Point(KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue CheckpointPoint)
                    {
                        Position_2D_Left = new Position2D_Left(CheckpointPoint.Position_2D_Left);
                        Position_2D_Right = new Position2D_Right(CheckpointPoint.Position_2D_Right);
                        RespawnID = CheckpointPoint.TPKC_RespawnID;
                        Checkpoint_Type = CheckpointPoint.TPKC_Checkpoint_Type;
                        NextCheckPoint = CheckpointPoint.TPKC_NextCheckPoint;
                        PreviousCheckPoint = CheckpointPoint.TPKC_PreviousCheckPoint;
                        ClipID = CheckpointPoint.TPKC_ClipID;
                        Section = CheckpointPoint.TPKC_Section;
                        UnkBytes3 = CheckpointPoint.TPKC_UnkBytes3;
                        UnkBytes4 = CheckpointPoint.TPKC_UnkBytes4;
                    }
                }

                public Checkpoint_Group() { }

                public Checkpoint_Group(KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue CheckpointGroups)
                {
                    PreviousGroups = new CP_PreviousGroup(CheckpointGroups.HPKC_PreviewGroup);
                    NextGroups = new CP_NextGroup(CheckpointGroups.HPKC_NextGroup);
                    UnkBytes1 = CheckpointGroups.HPKC_UnkBytes1;

                    foreach (var Point in CheckpointGroups.TPKCValueList) Points.Add(new Checkpoint_Point(Point));
                }
            }

            public Checkpoint() { }

            public Checkpoint(KMPPropertyGridSettings.HPKC_TPKC_Section hPKC_TPKC_Section)
            {
                foreach (var Group in hPKC_TPKC_Section.HPKCValueList) Groups.Add(new Checkpoint_Group(Group));
            }
        }

        [System.Xml.Serialization.XmlElement("Object")]
        public Object Objects { get; set; }
        public class Object
        {
            [System.Xml.Serialization.XmlElement("Value")]
            public List<Object_Value> Object_Values { get; set; } = new List<Object_Value>();
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

                    public Object_Position() { }

                    public Object_Position(Vector3D vector3D)
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
                public Object_Rotation Rotation { get; set; }
                public class Object_Rotation
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Object_Rotation() { }

                    public Object_Rotation(Vector3D vector3D)
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

                    public Object_Scale() { }

                    public Object_Scale(Vector3D vector3D)
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

                    public SpecificSettings() { }

                    public SpecificSettings(KMPPropertyGridSettings.JBOG_Section.JBOGValue.JBOG_SpecificSetting JOBJ_Specific_Setting)
                    {
                        Value0 = JOBJ_Specific_Setting.Value0;
                        Value1 = JOBJ_Specific_Setting.Value1;
                        Value2 = JOBJ_Specific_Setting.Value2;
                        Value3 = JOBJ_Specific_Setting.Value3;
                        Value4 = JOBJ_Specific_Setting.Value4;
                        Value5 = JOBJ_Specific_Setting.Value5;
                        Value6 = JOBJ_Specific_Setting.Value6;
                        Value7 = JOBJ_Specific_Setting.Value7;
                    }
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

                public Object_Value() { }

                public Object_Value(KMPPropertyGridSettings.JBOG_Section.JBOGValue ObjectValues)
                {
                    Position = new Object_Position(ObjectValues.Positions.GetVector3D());
                    Rotation = new Object_Rotation(ObjectValues.Rotations.GetVector3D());
                    Scale = new Object_Scale(ObjectValues.Scales.GetVector3D());
                    SpecificSetting = new SpecificSettings(ObjectValues.JOBJ_Specific_Setting);
                    ObjectID = ObjectValues.ObjectID;
                    RouteIDIndex = ObjectValues.JBOG_ITOP_RouteIDIndex;
                    PresenceSetting = ObjectValues.JBOG_PresenceSetting;
                    UnkByte1 = ObjectValues.JBOG_UnkByte1;
                    UnkByte2 = ObjectValues.JBOG_UnkByte2;
                    UnkByte3 = ObjectValues.JBOG_UnkByte3;
                }
            }

            public Object() { }

            public Object(KMPPropertyGridSettings.JBOG_Section jBOG_Section)
            {
                foreach (var Object in jBOG_Section.JBOGValueList) Object_Values.Add(new Object_Value(Object));
            }
        }

        [System.Xml.Serialization.XmlElement("Route")]
        public Route Routes { get; set; }
        public class Route
        {
            [System.Xml.Serialization.XmlElement("Group")]
            public List<Route_Group> Groups { get; set; } = new List<Route_Group>();
            public class Route_Group
            {
                [System.Xml.Serialization.XmlAttribute("Roop")]
                public byte RoopSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("Smooth")]
                public byte SmoothSetting { get; set; }

                [System.Xml.Serialization.XmlElement("Point")]
                public List<Route_Point> Points { get; set; } = new List<Route_Point>();
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

                        public Route_Position() { }

                        public Route_Position(Vector3D vector3D)
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

                    public Route_Point() { }

                    public Route_Point(KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point RoutePoint)
                    {
                        Position = new Route_Position(RoutePoint.Positions.GetVector3D());
                        RouteSpeed = RoutePoint.ITOP_Point_RouteSpeed;
                        PointSetting2 = RoutePoint.ITOP_PointSetting2;
                    }
                }

                public Route_Group() { }

                public Route_Group(KMPPropertyGridSettings.ITOP_Section.ITOP_Route RouteGroup)
                {
                    RoopSetting = RouteGroup.ITOP_Roop;
                    SmoothSetting = RouteGroup.ITOP_Smooth;

                    foreach (var Point in RouteGroup.ITOP_PointList) Points.Add(new Route_Point(Point));
                }
            }

            public Route() { }

            public Route(KMPPropertyGridSettings.ITOP_Section iTOP_Section)
            {
                foreach (var Group in iTOP_Section.ITOP_RouteList) Groups.Add(new Route_Group(Group));
            }
        }

        [System.Xml.Serialization.XmlElement("Area")]
        public Area Areas { get; set; }
        public class Area
        {
            [System.Xml.Serialization.XmlElement("Value")]
            public List<Area_Value> Area_Values { get; set; } = new List<Area_Value>();
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

                    public Area_Position() { }

                    public Area_Position(Vector3D vector3D)
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
                public Area_Rotation Rotation { get; set; }
                public class Area_Rotation
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public Area_Rotation() { }

                    public Area_Rotation(Vector3D vector3D)
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

                    public Area_Scale() { }

                    public Area_Scale(Vector3D vector3D)
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

                public Area_Value() { }

                public Area_Value(KMPPropertyGridSettings.AERA_Section.AERAValue AreaValues)
                {
                    Position = new Area_Position(AreaValues.Positions.GetVector3D());
                    Rotation = new Area_Rotation(AreaValues.Rotations.GetVector3D());
                    Scale = new Area_Scale(AreaValues.Scales.GetVector3D());
                    AreaType = AreaValues.AreaType;
                    AreaMode = AreaValues.AreaModeSettings.AreaModeValue;
                    CameraIndex = AreaValues.AERA_EMACIndex;
                    Priority = AreaValues.Priority;
                    Setting1 = AreaValues.AERA_Setting1;
                    Setting2 = AreaValues.AERA_Setting2;
                    RouteID = AreaValues.RouteID;
                    EnemyID = AreaValues.EnemyID;
                    UnkByte4 = AreaValues.AERA_UnkByte4;
                }
            }

            public Area() { }

            public Area(KMPPropertyGridSettings.AERA_Section aERA_Section)
            {
                foreach (var AREAValue in aERA_Section.AERAValueList) Area_Values.Add(new Area_Value(AREAValue));
            }
        }

        [System.Xml.Serialization.XmlElement("Camera")]
        public Camera Cameras { get; set; }
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

                    public SpeedSettings(KMPPropertyGridSettings.EMAC_Section.EMACValue.SpeedSetting speedSetting)
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

                    public FOVAngleSetting(KMPPropertyGridSettings.EMAC_Section.EMACValue.FOVAngleSetting FOVAngleSetting)
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

                public Camera_Value(KMPPropertyGridSettings.EMAC_Section.EMACValue CameraValue)
                {
                    SpeedSetting = new SpeedSettings(CameraValue.SpeedSettings);
                    Position = new Camera_Position(CameraValue.Positions.GetVector3D());
                    Rotation = new Camera_Rotation(CameraValue.Rotations.GetVector3D());
                    FOVAngleSettings = new FOVAngleSetting(CameraValue.FOVAngleSettings);
                    ViewpointStart = new Viewpoint_Start(CameraValue.Viewpoint_Start.GetVector3D());
                    ViewpointDestination = new Viewpoint_Destination(CameraValue.Viewpoint_Destination.GetVector3D());
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

            public Camera(KMPPropertyGridSettings.EMAC_Section eMAC_Section)
            {
                foreach (var CameraValue in eMAC_Section.EMACValueList) Values.Add(new Camera_Value(CameraValue));
            }
        }

        [System.Xml.Serialization.XmlElement("JugemPoint")]
        public JugemPoint JugemPoints { get; set; }
        public class JugemPoint
        {
            [System.Xml.Serialization.XmlElement("Value")]
            public List<JugemPoint_Value> Values { get; set; } = new List<JugemPoint_Value>();
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

                    public JugemPoint_Position() { }

                    public JugemPoint_Position(Vector3D vector3D)
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
                public JugemPoint_Rotation Rotation { get; set; }
                public class JugemPoint_Rotation
                {
                    [System.Xml.Serialization.XmlAttribute("X")]
                    public float X { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Y")]
                    public float Y { get; set; }

                    [System.Xml.Serialization.XmlAttribute("Z")]
                    public float Z { get; set; }

                    public JugemPoint_Rotation() { }

                    public JugemPoint_Rotation(Vector3D vector3D)
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

                public JugemPoint_Value() { }

                public JugemPoint_Value(KMPPropertyGridSettings.TPGJ_Section.TPGJValue JugemPointValue)
                {
                    Position = new JugemPoint_Position(JugemPointValue.Positions.GetVector3D());
                    Rotation = new JugemPoint_Rotation(JugemPointValue.Rotations.GetVector3D());
                    RespawnID = JugemPointValue.TPGJ_RespawnID;
                    UnkBytes1 = JugemPointValue.TPGJ_UnkBytes1;
                }
            }

            public JugemPoint() { }

            public JugemPoint(KMPPropertyGridSettings.TPGJ_Section tPGJ_Section)
            {
                foreach (var JugemPoint in tPGJ_Section.TPGJValueList) Values.Add(new JugemPoint_Value(JugemPoint));
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
            public List<GlideRoute_Group> Groups { get; set; } = new List<GlideRoute_Group>();
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

                    public GR_PreviousGroup() { }

                    public GR_PreviousGroup(KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.HPLG_PreviewGroups PreviewGroups)
                    {
                        Prev0 = PreviewGroups.Prev0;
                        Prev1 = PreviewGroups.Prev1;
                        Prev2 = PreviewGroups.Prev2;
                        Prev3 = PreviewGroups.Prev3;
                        Prev4 = PreviewGroups.Prev4;
                        Prev5 = PreviewGroups.Prev5;
                    }
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

                    public GR_NextGroup() { }

                    public GR_NextGroup(KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.HPLG_NextGroups NextGroups)
                    {
                        Next0 = NextGroups.Next0;
                        Next1 = NextGroups.Next1;
                        Next2 = NextGroups.Next2;
                        Next3 = NextGroups.Next3;
                        Next4 = NextGroups.Next4;
                        Next5 = NextGroups.Next5;
                    }
                }

                [System.Xml.Serialization.XmlAttribute("RouteSetting")]
                public uint RouteSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("UnkBytes2")]
                public uint UnkBytes2 { get; set; }

                //[System.Xml.Serialization.XmlElement("Point")]
                [System.Xml.Serialization.XmlArray("Points")]
                [System.Xml.Serialization.XmlArrayItem("Point")]
                public List<GlideRoute_Point> Points { get; set; } = new List<GlideRoute_Point>();
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

                        public GlideRoute_Position() { }

                        public GlideRoute_Position(Vector3D vector3D)
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

                    public GlideRoute_Point() { }

                    public GlideRoute_Point(KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue GlidePoint)
                    {
                        Position = new GlideRoute_Position(GlidePoint.Positions.GetVector3D());
                        PointScale = GlidePoint.TPLG_PointScaleValue;
                        UnkBytes1 = GlidePoint.TPLG_UnkBytes1;
                        UnkBytes2 = GlidePoint.TPLG_UnkBytes2;
                    }
                }

                public GlideRoute_Group() { }

                public GlideRoute_Group(KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue GlideRouteGroups)
                {
                    PreviousGroups = new GR_PreviousGroup(GlideRouteGroups.HPLG_PreviewGroup);
                    NextGroups = new GR_NextGroup(GlideRouteGroups.HPLG_NextGroup);
                    RouteSetting = GlideRouteGroups.RouteSettings.RouteSettingValue;
                    UnkBytes2 = GlideRouteGroups.HPLG_UnkBytes2;

                    foreach (var Point in GlideRouteGroups.TPLGValueList) Points.Add(new GlideRoute_Point(Point));
                }
            }

            public GlideRoute() { }

            public GlideRoute(KMPPropertyGridSettings.HPLG_TPLG_Section hPLG_TPLG_Section)
            {
                foreach (var Group in hPLG_TPLG_Section.HPLGValueList) Groups.Add(new GlideRoute_Group(Group));
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
