using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace MK7_KMP_Editor_For_PG_
{
    public class XMLExporter
    {
        public static void ExportAll(KMPPropertyGridSettings kMPPropertyGridSettings, string Path)
        {
            #region StartPosition
            List<TestXml.KMPXml.StartPosition.StartPosition_Value> startPositions = new List<TestXml.KMPXml.StartPosition.StartPosition_Value>();

            foreach (var StartPositions in kMPPropertyGridSettings.TPTKSection.TPTKValueList)
            {
                TestXml.KMPXml.StartPosition.StartPosition_Value startPosition_Value = new TestXml.KMPXml.StartPosition.StartPosition_Value
                {
                    Position = new TestXml.KMPXml.StartPosition.StartPosition_Value.StartPosition_Position
                    {
                        X = StartPositions.Position_Value.X,
                        Y = StartPositions.Position_Value.Y,
                        Z = StartPositions.Position_Value.Z
                    },
                    Rotation = new TestXml.KMPXml.StartPosition.StartPosition_Value.StartPosition_Rotation
                    {
                        X = StartPositions.Rotate_Value.X,
                        Y = StartPositions.Rotate_Value.Y,
                        Z = StartPositions.Rotate_Value.Z
                    },
                    Player_Index = StartPositions.Player_Index,
                    TPTK_UnkBytes = StartPositions.TPTK_UnkBytes
                };

                startPositions.Add(startPosition_Value);
            }
            #endregion

            #region EnemyRoute
            List<TestXml.KMPXml.EnemyRoute.EnemyRoute_Group> EnemyRoute_Groups = new List<TestXml.KMPXml.EnemyRoute.EnemyRoute_Group>();

            foreach (var EnemyRouteGroups in kMPPropertyGridSettings.HPNE_TPNESection.HPNEValueList)
            {
                TestXml.KMPXml.EnemyRoute.EnemyRoute_Group EnemyRoute_group = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group
                {
                    PreviousGroups = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.ER_PreviousGroup
                    {
                        Prev0 = EnemyRouteGroups.HPNEPreviewGroups.Prev0,
                        Prev1 = EnemyRouteGroups.HPNEPreviewGroups.Prev1,
                        Prev2 = EnemyRouteGroups.HPNEPreviewGroups.Prev2,
                        Prev3 = EnemyRouteGroups.HPNEPreviewGroups.Prev3,
                        Prev4 = EnemyRouteGroups.HPNEPreviewGroups.Prev4,
                        Prev5 = EnemyRouteGroups.HPNEPreviewGroups.Prev5,
                        Prev6 = EnemyRouteGroups.HPNEPreviewGroups.Prev6,
                        Prev7 = EnemyRouteGroups.HPNEPreviewGroups.Prev7,
                        Prev8 = EnemyRouteGroups.HPNEPreviewGroups.Prev8,
                        Prev9 = EnemyRouteGroups.HPNEPreviewGroups.Prev9,
                        Prev10 = EnemyRouteGroups.HPNEPreviewGroups.Prev10,
                        Prev11 = EnemyRouteGroups.HPNEPreviewGroups.Prev11,
                        Prev12 = EnemyRouteGroups.HPNEPreviewGroups.Prev12,
                        Prev13 = EnemyRouteGroups.HPNEPreviewGroups.Prev13,
                        Prev14 = EnemyRouteGroups.HPNEPreviewGroups.Prev14,
                        Prev15 = EnemyRouteGroups.HPNEPreviewGroups.Prev15
                    },
                    NextGroups = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.ER_NextGroup
                    {
                        Next0 = EnemyRouteGroups.HPNENextGroups.Next0,
                        Next1 = EnemyRouteGroups.HPNENextGroups.Next1,
                        Next2 = EnemyRouteGroups.HPNENextGroups.Next2,
                        Next3 = EnemyRouteGroups.HPNENextGroups.Next3,
                        Next4 = EnemyRouteGroups.HPNENextGroups.Next4,
                        Next5 = EnemyRouteGroups.HPNENextGroups.Next5,
                        Next6 = EnemyRouteGroups.HPNENextGroups.Next6,
                        Next7 = EnemyRouteGroups.HPNENextGroups.Next7,
                        Next8 = EnemyRouteGroups.HPNENextGroups.Next8,
                        Next9 = EnemyRouteGroups.HPNENextGroups.Next9,
                        Next10 = EnemyRouteGroups.HPNENextGroups.Next10,
                        Next11 = EnemyRouteGroups.HPNENextGroups.Next11,
                        Next12 = EnemyRouteGroups.HPNENextGroups.Next12,
                        Next13 = EnemyRouteGroups.HPNENextGroups.Next13,
                        Next14 = EnemyRouteGroups.HPNENextGroups.Next14,
                        Next15 = EnemyRouteGroups.HPNENextGroups.Next15,
                    },
                    Unknown1 = EnemyRouteGroups.HPNE_UnkBytes1,
                    Points = null
                };

                List<TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point> EnemyRoute_points = new List<TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point>();

                foreach (var EnemyPoint in EnemyRouteGroups.TPNEValueList)
                {
                    TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point EnemyRoute_point = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point
                    {
                        Position = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point.EnemyRoute_Position
                        {
                            X = EnemyPoint.Positions.X,
                            Y = EnemyPoint.Positions.Y,
                            Z = EnemyPoint.Positions.Z
                        },
                        Control = EnemyPoint.Control,
                        DriftSetting = EnemyPoint.DriftSettings.DriftSettingValue,
                        MushSetting = EnemyPoint.MushSettings.MushSettingValue,
                        Flags = EnemyPoint.FlagSettings.Flags,
                        PathFindOption = EnemyPoint.PathFindOptions.PathFindOptionValue,
                        MaxSearchYOffset = EnemyPoint.MaxSearchYOffset.MaxSearchYOffsetValue
                    };

                    EnemyRoute_points.Add(EnemyRoute_point);
                }

                EnemyRoute_group.Points = EnemyRoute_points;

                EnemyRoute_Groups.Add(EnemyRoute_group);
            }
            #endregion

            #region ItemRoute
            List<TestXml.KMPXml.ItemRoute.ItemRoute_Group> ItemRoute_Groups = new List<TestXml.KMPXml.ItemRoute.ItemRoute_Group>();

            foreach (var ItemRouteGroups in kMPPropertyGridSettings.HPTI_TPTISection.HPTIValueList)
            {
                TestXml.KMPXml.ItemRoute.ItemRoute_Group itemRoute_Group = new TestXml.KMPXml.ItemRoute.ItemRoute_Group
                {
                    PreviousGroups = new TestXml.KMPXml.ItemRoute.ItemRoute_Group.IR_PreviousGroup
                    {
                        Prev0 = ItemRouteGroups.HPTI_PreviewGroup.Prev0,
                        Prev1 = ItemRouteGroups.HPTI_PreviewGroup.Prev1,
                        Prev2 = ItemRouteGroups.HPTI_PreviewGroup.Prev2,
                        Prev3 = ItemRouteGroups.HPTI_PreviewGroup.Prev3,
                        Prev4 = ItemRouteGroups.HPTI_PreviewGroup.Prev4,
                        Prev5 = ItemRouteGroups.HPTI_PreviewGroup.Prev5
                    },
                    NextGroups = new TestXml.KMPXml.ItemRoute.ItemRoute_Group.IR_NextGroup
                    {
                        Next0 = ItemRouteGroups.HPTI_NextGroup.Next0,
                        Next1 = ItemRouteGroups.HPTI_NextGroup.Next1,
                        Next2 = ItemRouteGroups.HPTI_NextGroup.Next2,
                        Next3 = ItemRouteGroups.HPTI_NextGroup.Next3,
                        Next4 = ItemRouteGroups.HPTI_NextGroup.Next4,
                        Next5 = ItemRouteGroups.HPTI_NextGroup.Next5
                    },
                    Points = null
                };

                List<TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point> itemRoute_Points = new List<TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point>();

                foreach (var ItemPoint in ItemRouteGroups.TPTIValueList)
                {
                    TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point itemRoute_Point = new TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point
                    {
                        GravityMode = ItemPoint.GravityModeSettings.GravityModeValue,
                        PlayerScanRadius = ItemPoint.PlayerScanRadiusSettings.PlayerScanRadiusValue,
                        PointSize = ItemPoint.TPTI_PointSize,
                        Position = new TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point.ItemRoute_Position
                        {
                            X = ItemPoint.TPTI_Positions.X,
                            Y = ItemPoint.TPTI_Positions.Y,
                            Z = ItemPoint.TPTI_Positions.Z
                        }
                    };

                    itemRoute_Points.Add(itemRoute_Point);
                }

                itemRoute_Group.Points = itemRoute_Points;

                ItemRoute_Groups.Add(itemRoute_Group);
            }
            #endregion

            #region Checkpoint
            List<TestXml.KMPXml.Checkpoint.Checkpoint_Group> checkpoint_Groups = new List<TestXml.KMPXml.Checkpoint.Checkpoint_Group>();

            foreach (var CheckpointGroups in kMPPropertyGridSettings.HPKC_TPKCSection.HPKCValueList)
            {
                TestXml.KMPXml.Checkpoint.Checkpoint_Group checkpoint_Group = new TestXml.KMPXml.Checkpoint.Checkpoint_Group
                {
                    PreviousGroups = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.CP_PreviousGroup
                    {
                        Prev0 = CheckpointGroups.HPKC_PreviewGroup.Prev0,
                        Prev1 = CheckpointGroups.HPKC_PreviewGroup.Prev1,
                        Prev2 = CheckpointGroups.HPKC_PreviewGroup.Prev2,
                        Prev3 = CheckpointGroups.HPKC_PreviewGroup.Prev3,
                        Prev4 = CheckpointGroups.HPKC_PreviewGroup.Prev4,
                        Prev5 = CheckpointGroups.HPKC_PreviewGroup.Prev5
                    },
                    NextGroups = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.CP_NextGroup
                    {
                        Next0 = CheckpointGroups.HPKC_NextGroup.Next0,
                        Next1 = CheckpointGroups.HPKC_NextGroup.Next1,
                        Next2 = CheckpointGroups.HPKC_NextGroup.Next2,
                        Next3 = CheckpointGroups.HPKC_NextGroup.Next3,
                        Next4 = CheckpointGroups.HPKC_NextGroup.Next4,
                        Next5 = CheckpointGroups.HPKC_NextGroup.Next5
                    },
                    UnkBytes1 = CheckpointGroups.HPKC_UnkBytes1,
                    Points = null
                };

                List<TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point> checkpoint_Points = new List<TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point>();

                foreach (var CheckpointPoint in CheckpointGroups.TPKCValueList)
                {
                    TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point checkpoint_Point = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point
                    {
                        Position_2D_Left = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point.Position2D_Left
                        {
                            X = CheckpointPoint.Position_2D_Left.X,
                            Y = CheckpointPoint.Position_2D_Left.Y
                        },
                        Position_2D_Right = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point.Position2D_Right
                        {
                            X = CheckpointPoint.Position_2D_Right.X,
                            Y = CheckpointPoint.Position_2D_Right.Y
                        },
                        RespawnID = CheckpointPoint.TPKC_RespawnID,
                        Checkpoint_Type = CheckpointPoint.TPKC_Checkpoint_Type,
                        NextCheckPoint = CheckpointPoint.TPKC_NextCheckPoint,
                        PreviousCheckPoint = CheckpointPoint.TPKC_PreviousCheckPoint,
                        ClipID = CheckpointPoint.TPKC_ClipID,
                        Section = CheckpointPoint.TPKC_Section,
                        UnkBytes3 = CheckpointPoint.TPKC_UnkBytes3,
                        UnkBytes4 = CheckpointPoint.TPKC_UnkBytes4
                    };

                    checkpoint_Points.Add(checkpoint_Point);
                }

                checkpoint_Group.Points = checkpoint_Points;

                checkpoint_Groups.Add(checkpoint_Group);
            }
            #endregion

            #region Object
            List<TestXml.KMPXml.Object.Object_Value> object_Values = new List<TestXml.KMPXml.Object.Object_Value>();

            foreach (var ObjectValues in kMPPropertyGridSettings.JBOGSection.JBOGValueList)
            {
                TestXml.KMPXml.Object.Object_Value object_Value = new TestXml.KMPXml.Object.Object_Value
                {
                    Position = new TestXml.KMPXml.Object.Object_Value.Object_Position
                    {
                        X = ObjectValues.Positions.X,
                        Y = ObjectValues.Positions.Y,
                        Z = ObjectValues.Positions.Z
                    },
                    Rotation = new TestXml.KMPXml.Object.Object_Value.Object_Rotation
                    {
                        X = ObjectValues.Rotations.X,
                        Y = ObjectValues.Rotations.Y,
                        Z = ObjectValues.Rotations.Z
                    },
                    Scale = new TestXml.KMPXml.Object.Object_Value.Object_Scale
                    {
                        X = ObjectValues.Scales.X,
                        Y = ObjectValues.Scales.Y,
                        Z = ObjectValues.Scales.Z
                    },
                    SpecificSetting = new TestXml.KMPXml.Object.Object_Value.SpecificSettings
                    {
                        Value0 = ObjectValues.JOBJ_Specific_Setting.Value0,
                        Value1 = ObjectValues.JOBJ_Specific_Setting.Value1,
                        Value2 = ObjectValues.JOBJ_Specific_Setting.Value2,
                        Value3 = ObjectValues.JOBJ_Specific_Setting.Value3,
                        Value4 = ObjectValues.JOBJ_Specific_Setting.Value4,
                        Value5 = ObjectValues.JOBJ_Specific_Setting.Value5,
                        Value6 = ObjectValues.JOBJ_Specific_Setting.Value6,
                        Value7 = ObjectValues.JOBJ_Specific_Setting.Value7,
                    },
                    ObjectID = ObjectValues.ObjectID,
                    RouteIDIndex = ObjectValues.JBOG_ITOP_RouteIDIndex,
                    PresenceSetting = ObjectValues.JBOG_PresenceSetting,
                    UnkByte1 = ObjectValues.JBOG_UnkByte1,
                    UnkByte2 = ObjectValues.JBOG_UnkByte2,
                    UnkByte3 = ObjectValues.JBOG_UnkByte3
                };

                object_Values.Add(object_Value);
            }
            #endregion

            #region Route
            List<TestXml.KMPXml.Route.Route_Group> route_Groups = new List<TestXml.KMPXml.Route.Route_Group>();

            foreach (var RouteGroups in kMPPropertyGridSettings.ITOPSection.ITOP_RouteList)
            {
                TestXml.KMPXml.Route.Route_Group route_Group = new TestXml.KMPXml.Route.Route_Group
                {
                    RoopSetting = RouteGroups.ITOP_Roop,
                    SmoothSetting = RouteGroups.ITOP_Smooth,
                    Points = null
                };

                List<TestXml.KMPXml.Route.Route_Group.Route_Point> route_Points = new List<TestXml.KMPXml.Route.Route_Group.Route_Point>();

                foreach (var RoutePoint in RouteGroups.ITOP_PointList)
                {
                    TestXml.KMPXml.Route.Route_Group.Route_Point route_Point = new TestXml.KMPXml.Route.Route_Group.Route_Point
                    {
                        Position = new TestXml.KMPXml.Route.Route_Group.Route_Point.Route_Position
                        {
                            X = RoutePoint.Positions.X,
                            Y = RoutePoint.Positions.Y,
                            Z = RoutePoint.Positions.Z
                        },
                        RouteSpeed = RoutePoint.ITOP_Point_RouteSpeed,
                        PointSetting2 = RoutePoint.ITOP_PointSetting2
                    };

                    route_Points.Add(route_Point);
                }

                route_Group.Points = route_Points;

                route_Groups.Add(route_Group);
            }
            #endregion

            #region Area
            List<TestXml.KMPXml.Area.Area_Value> area_Values = new List<TestXml.KMPXml.Area.Area_Value>();

            foreach (var AreaValues in kMPPropertyGridSettings.AERASection.AERAValueList)
            {
                TestXml.KMPXml.Area.Area_Value area_Value = new TestXml.KMPXml.Area.Area_Value
                {
                    Position = new TestXml.KMPXml.Area.Area_Value.Area_Position
                    {
                        X = AreaValues.Positions.X,
                        Y = AreaValues.Positions.Y,
                        Z = AreaValues.Positions.Z
                    },
                    Rotation = new TestXml.KMPXml.Area.Area_Value.Area_Rotation
                    {
                        X = AreaValues.Rotations.X,
                        Y = AreaValues.Rotations.Y,
                        Z = AreaValues.Rotations.Z
                    },
                    Scale = new TestXml.KMPXml.Area.Area_Value.Area_Scale
                    {
                        X = AreaValues.Scales.X,
                        Y = AreaValues.Scales.Y,
                        Z = AreaValues.Scales.Z
                    },
                    AreaType = AreaValues.AreaType,
                    AreaMode = AreaValues.AreaModeSettings.AreaModeValue,
                    CameraIndex = AreaValues.AERA_EMACIndex,
                    Priority = AreaValues.Priority,
                    Setting1 = AreaValues.AERA_Setting1,
                    Setting2 = AreaValues.AERA_Setting2,
                    RouteID = AreaValues.RouteID,
                    EnemyID = AreaValues.EnemyID,
                    UnkByte4 = AreaValues.AERA_UnkByte4
                };

                area_Values.Add(area_Value);
            }
            #endregion

            #region Camera
            List<TestXml.KMPXml.Camera.Camera_Value> camera_Values = new List<TestXml.KMPXml.Camera.Camera_Value>();

            foreach (var CameraValues in kMPPropertyGridSettings.EMACSection.EMACValueList)
            {
                TestXml.KMPXml.Camera.Camera_Value camera_Value = new TestXml.KMPXml.Camera.Camera_Value
                {
                    SpeedSetting = new TestXml.KMPXml.Camera.Camera_Value.SpeedSettings
                    {
                        RouteSpeed = CameraValues.SpeedSettings.RouteSpeed,
                        FOVSpeed = CameraValues.SpeedSettings.FOVSpeed,
                        ViewpointSpeed = CameraValues.SpeedSettings.ViewpointSpeed
                    },
                    Position = new TestXml.KMPXml.Camera.Camera_Value.Camera_Position
                    {
                        X = CameraValues.Positions.X,
                        Y = CameraValues.Positions.Y,
                        Z = CameraValues.Positions.Z
                    },
                    Rotation = new TestXml.KMPXml.Camera.Camera_Value.Camera_Rotation
                    {
                        X = CameraValues.Rotations.X,
                        Y = CameraValues.Rotations.Y,
                        Z = CameraValues.Rotations.Z
                    },
                    FOVAngleSettings = new TestXml.KMPXml.Camera.Camera_Value.FOVAngleSetting
                    {
                        Start = CameraValues.FOVAngleSettings.FOVAngle_Start,
                        End = CameraValues.FOVAngleSettings.FOVAngle_End
                    },
                    ViewpointStart = new TestXml.KMPXml.Camera.Camera_Value.Viewpoint_Start
                    {
                        X = CameraValues.Viewpoint_Start.X,
                        Y = CameraValues.Viewpoint_Start.Y,
                        Z = CameraValues.Viewpoint_Start.Z
                    },
                    ViewpointDestination = new TestXml.KMPXml.Camera.Camera_Value.Viewpoint_Destination
                    {
                        X = CameraValues.Viewpoint_Destination.X,
                        Y = CameraValues.Viewpoint_Destination.Y,
                        Z = CameraValues.Viewpoint_Destination.Z
                    },
                    CameraType = CameraValues.CameraType,
                    NextCameraIndex = CameraValues.NextCameraIndex,
                    NextVideoIndex = CameraValues.EMAC_NextVideoIndex,
                    Route_CameraIndex = CameraValues.EMAC_ITOP_CameraIndex,
                    StartFlag = CameraValues.EMAC_StartFlag,
                    VideoFlag = CameraValues.EMAC_VideoFlag,
                    CameraActiveTime = CameraValues.Camera_Active_Time
                };

                camera_Values.Add(camera_Value);
            }
            #endregion

            #region Jugempoint(Respawn)
            List<TestXml.KMPXml.JugemPoint.JugemPoint_Value> jugemPoint_Values = new List<TestXml.KMPXml.JugemPoint.JugemPoint_Value>();

            foreach (var JugempointValues in kMPPropertyGridSettings.TPGJSection.TPGJValueList)
            {
                TestXml.KMPXml.JugemPoint.JugemPoint_Value jugemPoint_Value = new TestXml.KMPXml.JugemPoint.JugemPoint_Value
                {
                    Position = new TestXml.KMPXml.JugemPoint.JugemPoint_Value.JugemPoint_Position
                    {
                        X = JugempointValues.Positions.X,
                        Y = JugempointValues.Positions.Y,
                        Z = JugempointValues.Positions.Z
                    },
                    Rotation = new TestXml.KMPXml.JugemPoint.JugemPoint_Value.JugemPoint_Rotation
                    {
                        X = JugempointValues.Rotations.X,
                        Y = JugempointValues.Rotations.Y,
                        Z = JugempointValues.Rotations.Z
                    },
                    RespawnID = JugempointValues.TPGJ_RespawnID,
                    UnkBytes1 = JugempointValues.TPGJ_UnkBytes1
                };

                jugemPoint_Values.Add(jugemPoint_Value);
            }
            #endregion

            #region GlideRoute
            List<TestXml.KMPXml.GlideRoute.GlideRoute_Group> glideRoute_Groups = new List<TestXml.KMPXml.GlideRoute.GlideRoute_Group>();

            foreach (var GlideRouteGroups in kMPPropertyGridSettings.HPLG_TPLGSection.HPLGValueList)
            {
                TestXml.KMPXml.GlideRoute.GlideRoute_Group glideRoute_Group = new TestXml.KMPXml.GlideRoute.GlideRoute_Group
                {
                    PreviousGroups = new TestXml.KMPXml.GlideRoute.GlideRoute_Group.GR_PreviousGroup
                    {
                        Prev0 = GlideRouteGroups.HPLG_PreviewGroup.Prev0,
                        Prev1 = GlideRouteGroups.HPLG_PreviewGroup.Prev1,
                        Prev2 = GlideRouteGroups.HPLG_PreviewGroup.Prev2,
                        Prev3 = GlideRouteGroups.HPLG_PreviewGroup.Prev3,
                        Prev4 = GlideRouteGroups.HPLG_PreviewGroup.Prev4,
                        Prev5 = GlideRouteGroups.HPLG_PreviewGroup.Prev5
                    },
                    NextGroups = new TestXml.KMPXml.GlideRoute.GlideRoute_Group.GR_NextGroup
                    {
                        Next0 = GlideRouteGroups.HPLG_NextGroup.Next0,
                        Next1 = GlideRouteGroups.HPLG_NextGroup.Next1,
                        Next2 = GlideRouteGroups.HPLG_NextGroup.Next2,
                        Next3 = GlideRouteGroups.HPLG_NextGroup.Next3,
                        Next4 = GlideRouteGroups.HPLG_NextGroup.Next4,
                        Next5 = GlideRouteGroups.HPLG_NextGroup.Next5
                    },
                    RouteSetting = GlideRouteGroups.RouteSettings.RouteSettingValue,
                    UnkBytes2 = GlideRouteGroups.HPLG_UnkBytes2,
                    Points = null
                };

                List<TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point> glideRoute_Points = new List<TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point>();  

                foreach (var GlidePoint in GlideRouteGroups.TPLGValueList)
                {
                    TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point glideRoute_Point = new TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point
                    {
                        Position = new TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point.GlideRoute_Position
                        {
                            X = GlidePoint.Positions.X,
                            Y = GlidePoint.Positions.Y,
                            Z = GlidePoint.Positions.Z
                        },
                        PointScale = GlidePoint.TPLG_PointScaleValue,
                        UnkBytes1 = GlidePoint.TPLG_UnkBytes1,
                        UnkBytes2 = GlidePoint.TPLG_UnkBytes2
                    };

                    glideRoute_Points.Add(glideRoute_Point);
                }

                glideRoute_Group.Points = glideRoute_Points;

                glideRoute_Groups.Add(glideRoute_Group);
            }
            #endregion

            TestXml.KMPXml KMP_Xml = new TestXml.KMPXml
            {
                startPositions = new TestXml.KMPXml.StartPosition
                {
                    startPosition_Value = startPositions
                },
                EnemyRoutes = new TestXml.KMPXml.EnemyRoute
                {
                    Groups = EnemyRoute_Groups
                },
                ItemRoutes = new TestXml.KMPXml.ItemRoute
                {
                    Groups = ItemRoute_Groups
                },
                Checkpoints = new TestXml.KMPXml.Checkpoint
                {
                    Groups = checkpoint_Groups
                },
                Objects = new TestXml.KMPXml.Object
                {
                    Object_Values = object_Values
                },
                Routes = new TestXml.KMPXml.Route
                {
                    Groups = route_Groups
                },
                Areas = new TestXml.KMPXml.Area
                {
                    Area_Values = area_Values
                },
                Cameras = new TestXml.KMPXml.Camera
                {
                    Values = camera_Values
                },
                JugemPoints = new TestXml.KMPXml.JugemPoint
                {
                    Values = jugemPoint_Values
                },
                Stage_Info = new TestXml.KMPXml.StageInfo
                {
                    Unknown1 = kMPPropertyGridSettings.IGTSSection.Unknown1,
                    LapCount = kMPPropertyGridSettings.IGTSSection.LapCount,
                    PolePosition = kMPPropertyGridSettings.IGTSSection.PolePosition,
                    Unknown2 = kMPPropertyGridSettings.IGTSSection.Unknown2,
                    Unknown3 = kMPPropertyGridSettings.IGTSSection.Unknown3,
                    RGBAColor = new TestXml.KMPXml.StageInfo.RGBA
                    {
                        R = kMPPropertyGridSettings.IGTSSection.RGBAColor.R,
                        G = kMPPropertyGridSettings.IGTSSection.RGBAColor.G,
                        B = kMPPropertyGridSettings.IGTSSection.RGBAColor.B,
                        A = kMPPropertyGridSettings.IGTSSection.RGBAColor.A,
                        FlareAlpha = kMPPropertyGridSettings.IGTSSection.FlareAlpha
                    }
                },
                GlideRoutes = new TestXml.KMPXml.GlideRoute
                {
                    Groups = glideRoute_Groups
                }
            };

            //Delete Namespaces
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.KMPXml));
            System.IO.StreamWriter sw = new StreamWriter(Path + "_All.xml", false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, KMP_Xml, xns);
            sw.Close();
        }

        public static void ExportSection(KMPPropertyGridSettings kMPPropertyGridSettings, string Path, TestXml.KMPXmlSetting.Section section)
        {
            TestXml.KMPXml KMP_Xml = new TestXml.KMPXml
            {
                startPositions = null,
                EnemyRoutes = null,
                ItemRoutes = null,
                Checkpoints = null,
                Objects = null,
                Routes = null,
                Areas = null,
                Cameras = null,
                JugemPoints = null,
                #region Hide
                //Stage_Info = new TestXml.KMPXml.StageInfo
                //{
                //    Unknown1 = kMPPropertyGridSettings.IGTSSection.Unknown1,
                //    LapCount = kMPPropertyGridSettings.IGTSSection.LapCount,
                //    PolePosition = kMPPropertyGridSettings.IGTSSection.PolePosition,
                //    Unknown2 = kMPPropertyGridSettings.IGTSSection.Unknown2,
                //    Unknown3 = kMPPropertyGridSettings.IGTSSection.Unknown3,
                //    RGBAColor = new TestXml.KMPXml.StageInfo.RGBA
                //    {
                //        R = kMPPropertyGridSettings.IGTSSection.RGBAColor.R,
                //        G = kMPPropertyGridSettings.IGTSSection.RGBAColor.G,
                //        B = kMPPropertyGridSettings.IGTSSection.RGBAColor.B,
                //        A = kMPPropertyGridSettings.IGTSSection.RGBAColor.A,
                //        FlareAlpha = kMPPropertyGridSettings.IGTSSection.FlareAlpha
                //    }
                //},
                #endregion
                GlideRoutes = null
            };

            if (section == TestXml.KMPXmlSetting.Section.KartPoint)
            {
                List<TestXml.KMPXml.StartPosition.StartPosition_Value> startPositions = new List<TestXml.KMPXml.StartPosition.StartPosition_Value>();

                foreach (var StartPositions in kMPPropertyGridSettings.TPTKSection.TPTKValueList)
                {
                    TestXml.KMPXml.StartPosition.StartPosition_Value startPosition_Value = new TestXml.KMPXml.StartPosition.StartPosition_Value
                    {
                        Position = new TestXml.KMPXml.StartPosition.StartPosition_Value.StartPosition_Position
                        {
                            X = StartPositions.Position_Value.X,
                            Y = StartPositions.Position_Value.Y,
                            Z = StartPositions.Position_Value.Z
                        },
                        Rotation = new TestXml.KMPXml.StartPosition.StartPosition_Value.StartPosition_Rotation
                        {
                            X = StartPositions.Rotate_Value.X,
                            Y = StartPositions.Rotate_Value.Y,
                            Z = StartPositions.Rotate_Value.Z
                        },
                        Player_Index = StartPositions.Player_Index,
                        TPTK_UnkBytes = StartPositions.TPTK_UnkBytes
                    };

                    startPositions.Add(startPosition_Value);
                }

                TestXml.KMPXml.StartPosition startPosition = new TestXml.KMPXml.StartPosition
                {
                    startPosition_Value = startPositions
                };

                KMP_Xml.startPositions = startPosition;
            }
            if (section == TestXml.KMPXmlSetting.Section.EnemyRoutes)
            {
                List<TestXml.KMPXml.EnemyRoute.EnemyRoute_Group> EnemyRoute_Groups = new List<TestXml.KMPXml.EnemyRoute.EnemyRoute_Group>();

                foreach (var EnemyRouteGroups in kMPPropertyGridSettings.HPNE_TPNESection.HPNEValueList)
                {
                    TestXml.KMPXml.EnemyRoute.EnemyRoute_Group EnemyRoute_group = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group
                    {
                        PreviousGroups = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.ER_PreviousGroup
                        {
                            Prev0 = EnemyRouteGroups.HPNEPreviewGroups.Prev0,
                            Prev1 = EnemyRouteGroups.HPNEPreviewGroups.Prev1,
                            Prev2 = EnemyRouteGroups.HPNEPreviewGroups.Prev2,
                            Prev3 = EnemyRouteGroups.HPNEPreviewGroups.Prev3,
                            Prev4 = EnemyRouteGroups.HPNEPreviewGroups.Prev4,
                            Prev5 = EnemyRouteGroups.HPNEPreviewGroups.Prev5,
                            Prev6 = EnemyRouteGroups.HPNEPreviewGroups.Prev6,
                            Prev7 = EnemyRouteGroups.HPNEPreviewGroups.Prev7,
                            Prev8 = EnemyRouteGroups.HPNEPreviewGroups.Prev8,
                            Prev9 = EnemyRouteGroups.HPNEPreviewGroups.Prev9,
                            Prev10 = EnemyRouteGroups.HPNEPreviewGroups.Prev10,
                            Prev11 = EnemyRouteGroups.HPNEPreviewGroups.Prev11,
                            Prev12 = EnemyRouteGroups.HPNEPreviewGroups.Prev12,
                            Prev13 = EnemyRouteGroups.HPNEPreviewGroups.Prev13,
                            Prev14 = EnemyRouteGroups.HPNEPreviewGroups.Prev14,
                            Prev15 = EnemyRouteGroups.HPNEPreviewGroups.Prev15
                        },
                        NextGroups = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.ER_NextGroup
                        {
                            Next0 = EnemyRouteGroups.HPNENextGroups.Next0,
                            Next1 = EnemyRouteGroups.HPNENextGroups.Next1,
                            Next2 = EnemyRouteGroups.HPNENextGroups.Next2,
                            Next3 = EnemyRouteGroups.HPNENextGroups.Next3,
                            Next4 = EnemyRouteGroups.HPNENextGroups.Next4,
                            Next5 = EnemyRouteGroups.HPNENextGroups.Next5,
                            Next6 = EnemyRouteGroups.HPNENextGroups.Next6,
                            Next7 = EnemyRouteGroups.HPNENextGroups.Next7,
                            Next8 = EnemyRouteGroups.HPNENextGroups.Next8,
                            Next9 = EnemyRouteGroups.HPNENextGroups.Next9,
                            Next10 = EnemyRouteGroups.HPNENextGroups.Next10,
                            Next11 = EnemyRouteGroups.HPNENextGroups.Next11,
                            Next12 = EnemyRouteGroups.HPNENextGroups.Next12,
                            Next13 = EnemyRouteGroups.HPNENextGroups.Next13,
                            Next14 = EnemyRouteGroups.HPNENextGroups.Next14,
                            Next15 = EnemyRouteGroups.HPNENextGroups.Next15
                        },
                        Unknown1 = EnemyRouteGroups.HPNE_UnkBytes1,
                        Points = null
                    };

                    List<TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point> EnemyRoute_points = new List<TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point>();

                    foreach (var EnemyPoint in EnemyRouteGroups.TPNEValueList)
                    {
                        TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point EnemyRoute_point = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point
                        {
                            Position = new TestXml.KMPXml.EnemyRoute.EnemyRoute_Group.EnemyRoute_Point.EnemyRoute_Position
                            {
                                X = EnemyPoint.Positions.X,
                                Y = EnemyPoint.Positions.Y,
                                Z = EnemyPoint.Positions.Z
                            },
                            Control = EnemyPoint.Control,
                            DriftSetting = EnemyPoint.DriftSettings.DriftSettingValue,
                            MushSetting = EnemyPoint.MushSettings.MushSettingValue,
                            Flags = EnemyPoint.FlagSettings.Flags,
                            PathFindOption = EnemyPoint.PathFindOptions.PathFindOptionValue,
                            MaxSearchYOffset = EnemyPoint.MaxSearchYOffset.MaxSearchYOffsetValue
                        };

                        EnemyRoute_points.Add(EnemyRoute_point);
                    }

                    EnemyRoute_group.Points = EnemyRoute_points;

                    EnemyRoute_Groups.Add(EnemyRoute_group);
                }

                TestXml.KMPXml.EnemyRoute EnemyRoute = new TestXml.KMPXml.EnemyRoute
                {
                    Groups = EnemyRoute_Groups
                };

                KMP_Xml.EnemyRoutes = EnemyRoute;
            }
            if (section == TestXml.KMPXmlSetting.Section.ItemRoutes)
            {
                List<TestXml.KMPXml.ItemRoute.ItemRoute_Group> ItemRoute_Groups = new List<TestXml.KMPXml.ItemRoute.ItemRoute_Group>();

                foreach (var ItemRouteGroups in kMPPropertyGridSettings.HPTI_TPTISection.HPTIValueList)
                {
                    TestXml.KMPXml.ItemRoute.ItemRoute_Group itemRoute_Group = new TestXml.KMPXml.ItemRoute.ItemRoute_Group
                    {
                        PreviousGroups = new TestXml.KMPXml.ItemRoute.ItemRoute_Group.IR_PreviousGroup
                        {
                            Prev0 = ItemRouteGroups.HPTI_PreviewGroup.Prev0,
                            Prev1 = ItemRouteGroups.HPTI_PreviewGroup.Prev1,
                            Prev2 = ItemRouteGroups.HPTI_PreviewGroup.Prev2,
                            Prev3 = ItemRouteGroups.HPTI_PreviewGroup.Prev3,
                            Prev4 = ItemRouteGroups.HPTI_PreviewGroup.Prev4,
                            Prev5 = ItemRouteGroups.HPTI_PreviewGroup.Prev5
                        },
                        NextGroups = new TestXml.KMPXml.ItemRoute.ItemRoute_Group.IR_NextGroup
                        {
                            Next0 = ItemRouteGroups.HPTI_NextGroup.Next0,
                            Next1 = ItemRouteGroups.HPTI_NextGroup.Next1,
                            Next2 = ItemRouteGroups.HPTI_NextGroup.Next2,
                            Next3 = ItemRouteGroups.HPTI_NextGroup.Next3,
                            Next4 = ItemRouteGroups.HPTI_NextGroup.Next4,
                            Next5 = ItemRouteGroups.HPTI_NextGroup.Next5
                        },
                        Points = null
                    };

                    List<TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point> itemRoute_Points = new List<TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point>();

                    foreach (var ItemPoint in ItemRouteGroups.TPTIValueList)
                    {
                        TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point itemRoute_Point = new TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point
                        {
                            GravityMode = ItemPoint.GravityModeSettings.GravityModeValue,
                            PlayerScanRadius = ItemPoint.PlayerScanRadiusSettings.PlayerScanRadiusValue,
                            PointSize = ItemPoint.TPTI_PointSize,
                            Position = new TestXml.KMPXml.ItemRoute.ItemRoute_Group.ItemRoute_Point.ItemRoute_Position
                            {
                                X = ItemPoint.TPTI_Positions.X,
                                Y = ItemPoint.TPTI_Positions.Y,
                                Z = ItemPoint.TPTI_Positions.Z
                            }
                        };

                        itemRoute_Points.Add(itemRoute_Point);
                    }

                    itemRoute_Group.Points = itemRoute_Points;

                    ItemRoute_Groups.Add(itemRoute_Group);
                }

                TestXml.KMPXml.ItemRoute ItemRoute = new TestXml.KMPXml.ItemRoute
                {
                    Groups = ItemRoute_Groups
                };

                KMP_Xml.ItemRoutes = ItemRoute;
            }
            if (section == TestXml.KMPXmlSetting.Section.CheckPoint)
            {
                List<TestXml.KMPXml.Checkpoint.Checkpoint_Group> checkpoint_Groups = new List<TestXml.KMPXml.Checkpoint.Checkpoint_Group>();

                foreach (var CheckpointGroups in kMPPropertyGridSettings.HPKC_TPKCSection.HPKCValueList)
                {
                    TestXml.KMPXml.Checkpoint.Checkpoint_Group checkpoint_Group = new TestXml.KMPXml.Checkpoint.Checkpoint_Group
                    {
                        PreviousGroups = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.CP_PreviousGroup
                        {
                            Prev0 = CheckpointGroups.HPKC_PreviewGroup.Prev0,
                            Prev1 = CheckpointGroups.HPKC_PreviewGroup.Prev1,
                            Prev2 = CheckpointGroups.HPKC_PreviewGroup.Prev2,
                            Prev3 = CheckpointGroups.HPKC_PreviewGroup.Prev3,
                            Prev4 = CheckpointGroups.HPKC_PreviewGroup.Prev4,
                            Prev5 = CheckpointGroups.HPKC_PreviewGroup.Prev5
                        },
                        NextGroups = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.CP_NextGroup
                        {
                            Next0 = CheckpointGroups.HPKC_NextGroup.Next0,
                            Next1 = CheckpointGroups.HPKC_NextGroup.Next1,
                            Next2 = CheckpointGroups.HPKC_NextGroup.Next2,
                            Next3 = CheckpointGroups.HPKC_NextGroup.Next3,
                            Next4 = CheckpointGroups.HPKC_NextGroup.Next4,
                            Next5 = CheckpointGroups.HPKC_NextGroup.Next5
                        },
                        UnkBytes1 = CheckpointGroups.HPKC_UnkBytes1,
                        Points = null
                    };

                    List<TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point> checkpoint_Points = new List<TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point>();

                    foreach (var CheckpointPoint in CheckpointGroups.TPKCValueList)
                    {
                        TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point checkpoint_Point = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point
                        {
                            Position_2D_Left = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point.Position2D_Left
                            {
                                X = CheckpointPoint.Position_2D_Left.X,
                                Y = CheckpointPoint.Position_2D_Left.Y
                            },
                            Position_2D_Right = new TestXml.KMPXml.Checkpoint.Checkpoint_Group.Checkpoint_Point.Position2D_Right
                            {
                                X = CheckpointPoint.Position_2D_Right.X,
                                Y = CheckpointPoint.Position_2D_Right.Y
                            },
                            RespawnID = CheckpointPoint.TPKC_RespawnID,
                            Checkpoint_Type = CheckpointPoint.TPKC_Checkpoint_Type,
                            NextCheckPoint = CheckpointPoint.TPKC_NextCheckPoint,
                            PreviousCheckPoint = CheckpointPoint.TPKC_PreviousCheckPoint,
                            ClipID = CheckpointPoint.TPKC_ClipID,
                            Section = CheckpointPoint.TPKC_Section,
                            UnkBytes3 = CheckpointPoint.TPKC_UnkBytes3,
                            UnkBytes4 = CheckpointPoint.TPKC_UnkBytes4
                        };

                        checkpoint_Points.Add(checkpoint_Point);
                    }

                    checkpoint_Group.Points = checkpoint_Points;

                    checkpoint_Groups.Add(checkpoint_Group);
                }

                TestXml.KMPXml.Checkpoint Checkpoint = new TestXml.KMPXml.Checkpoint
                {
                    Groups = checkpoint_Groups
                };

                KMP_Xml.Checkpoints = Checkpoint;
            }
            if (section == TestXml.KMPXmlSetting.Section.Obj)
            {
                List<TestXml.KMPXml.Object.Object_Value> object_Values = new List<TestXml.KMPXml.Object.Object_Value>();

                foreach (var ObjectValues in kMPPropertyGridSettings.JBOGSection.JBOGValueList)
                {
                    TestXml.KMPXml.Object.Object_Value object_Value = new TestXml.KMPXml.Object.Object_Value
                    {
                        Position = new TestXml.KMPXml.Object.Object_Value.Object_Position
                        {
                            X = ObjectValues.Positions.X,
                            Y = ObjectValues.Positions.Y,
                            Z = ObjectValues.Positions.Z
                        },
                        Rotation = new TestXml.KMPXml.Object.Object_Value.Object_Rotation
                        {
                            X = ObjectValues.Rotations.X,
                            Y = ObjectValues.Rotations.Y,
                            Z = ObjectValues.Rotations.Z
                        },
                        Scale = new TestXml.KMPXml.Object.Object_Value.Object_Scale
                        {
                            X = ObjectValues.Scales.X,
                            Y = ObjectValues.Scales.Y,
                            Z = ObjectValues.Scales.Z
                        },
                        SpecificSetting = new TestXml.KMPXml.Object.Object_Value.SpecificSettings
                        {
                            Value0 = ObjectValues.JOBJ_Specific_Setting.Value0,
                            Value1 = ObjectValues.JOBJ_Specific_Setting.Value1,
                            Value2 = ObjectValues.JOBJ_Specific_Setting.Value2,
                            Value3 = ObjectValues.JOBJ_Specific_Setting.Value3,
                            Value4 = ObjectValues.JOBJ_Specific_Setting.Value4,
                            Value5 = ObjectValues.JOBJ_Specific_Setting.Value5,
                            Value6 = ObjectValues.JOBJ_Specific_Setting.Value6,
                            Value7 = ObjectValues.JOBJ_Specific_Setting.Value7,
                        },
                        ObjectID = ObjectValues.ObjectID,
                        RouteIDIndex = ObjectValues.JBOG_ITOP_RouteIDIndex,
                        PresenceSetting = ObjectValues.JBOG_PresenceSetting,
                        UnkByte1 = ObjectValues.JBOG_UnkByte1,
                        UnkByte2 = ObjectValues.JBOG_UnkByte2,
                        UnkByte3 = ObjectValues.JBOG_UnkByte3
                    };

                    object_Values.Add(object_Value);
                }

                TestXml.KMPXml.Object Object = new TestXml.KMPXml.Object
                {
                    Object_Values = object_Values
                };

                KMP_Xml.Objects = Object;
            }
            if (section == TestXml.KMPXmlSetting.Section.Route)
            {
                List<TestXml.KMPXml.Route.Route_Group> route_Groups = new List<TestXml.KMPXml.Route.Route_Group>();

                foreach (var RouteGroups in kMPPropertyGridSettings.ITOPSection.ITOP_RouteList)
                {
                    TestXml.KMPXml.Route.Route_Group route_Group = new TestXml.KMPXml.Route.Route_Group
                    {
                        RoopSetting = RouteGroups.ITOP_Roop,
                        SmoothSetting = RouteGroups.ITOP_Smooth,
                        Points = null
                    };

                    List<TestXml.KMPXml.Route.Route_Group.Route_Point> route_Points = new List<TestXml.KMPXml.Route.Route_Group.Route_Point>();

                    foreach (var RoutePoint in RouteGroups.ITOP_PointList)
                    {
                        TestXml.KMPXml.Route.Route_Group.Route_Point route_Point = new TestXml.KMPXml.Route.Route_Group.Route_Point
                        {
                            Position = new TestXml.KMPXml.Route.Route_Group.Route_Point.Route_Position
                            {
                                X = RoutePoint.Positions.X,
                                Y = RoutePoint.Positions.Y,
                                Z = RoutePoint.Positions.Z
                            },
                            RouteSpeed = RoutePoint.ITOP_Point_RouteSpeed,
                            PointSetting2 = RoutePoint.ITOP_PointSetting2
                        };

                        route_Points.Add(route_Point);
                    }

                    route_Group.Points = route_Points;

                    route_Groups.Add(route_Group);
                }

                TestXml.KMPXml.Route Route = new TestXml.KMPXml.Route
                {
                    Groups = route_Groups
                };

                KMP_Xml.Routes = Route;
            }
            if (section == TestXml.KMPXmlSetting.Section.Area)
            {
                List<TestXml.KMPXml.Area.Area_Value> area_Values = new List<TestXml.KMPXml.Area.Area_Value>();

                foreach (var AreaValues in kMPPropertyGridSettings.AERASection.AERAValueList)
                {
                    TestXml.KMPXml.Area.Area_Value area_Value = new TestXml.KMPXml.Area.Area_Value
                    {
                        Position = new TestXml.KMPXml.Area.Area_Value.Area_Position
                        {
                            X = AreaValues.Positions.X,
                            Y = AreaValues.Positions.Y,
                            Z = AreaValues.Positions.Z
                        },
                        Rotation = new TestXml.KMPXml.Area.Area_Value.Area_Rotation
                        {
                            X = AreaValues.Rotations.X,
                            Y = AreaValues.Rotations.Y,
                            Z = AreaValues.Rotations.Z
                        },
                        Scale = new TestXml.KMPXml.Area.Area_Value.Area_Scale
                        {
                            X = AreaValues.Scales.X,
                            Y = AreaValues.Scales.Y,
                            Z = AreaValues.Scales.Z
                        },
                        AreaType = AreaValues.AreaType,
                        AreaMode = AreaValues.AreaModeSettings.AreaModeValue,
                        CameraIndex = AreaValues.AERA_EMACIndex,
                        Priority = AreaValues.Priority,
                        Setting1 = AreaValues.AERA_Setting1,
                        Setting2 = AreaValues.AERA_Setting2,
                        RouteID = AreaValues.RouteID,
                        EnemyID = AreaValues.EnemyID,
                        UnkByte4 = AreaValues.AERA_UnkByte4
                    };

                    area_Values.Add(area_Value);
                }

                TestXml.KMPXml.Area Area = new TestXml.KMPXml.Area
                {
                    Area_Values = area_Values
                };

                KMP_Xml.Areas = Area;
            }
            if (section == TestXml.KMPXmlSetting.Section.Camera)
            {
                List<TestXml.KMPXml.Camera.Camera_Value> camera_Values = new List<TestXml.KMPXml.Camera.Camera_Value>();

                foreach (var CameraValues in kMPPropertyGridSettings.EMACSection.EMACValueList)
                {
                    TestXml.KMPXml.Camera.Camera_Value camera_Value = new TestXml.KMPXml.Camera.Camera_Value
                    {
                        SpeedSetting = new TestXml.KMPXml.Camera.Camera_Value.SpeedSettings
                        {
                            RouteSpeed = CameraValues.SpeedSettings.RouteSpeed,
                            FOVSpeed = CameraValues.SpeedSettings.FOVSpeed,
                            ViewpointSpeed = CameraValues.SpeedSettings.ViewpointSpeed
                        },
                        Position = new TestXml.KMPXml.Camera.Camera_Value.Camera_Position
                        {
                            X = CameraValues.Positions.X,
                            Y = CameraValues.Positions.Y,
                            Z = CameraValues.Positions.Z
                        },
                        Rotation = new TestXml.KMPXml.Camera.Camera_Value.Camera_Rotation
                        {
                            X = CameraValues.Rotations.X,
                            Y = CameraValues.Rotations.Y,
                            Z = CameraValues.Rotations.Z
                        },
                        FOVAngleSettings = new TestXml.KMPXml.Camera.Camera_Value.FOVAngleSetting
                        {
                            Start = CameraValues.FOVAngleSettings.FOVAngle_Start,
                            End = CameraValues.FOVAngleSettings.FOVAngle_End
                        },
                        ViewpointStart = new TestXml.KMPXml.Camera.Camera_Value.Viewpoint_Start
                        {
                            X = CameraValues.Viewpoint_Start.X,
                            Y = CameraValues.Viewpoint_Start.Y,
                            Z = CameraValues.Viewpoint_Start.Z
                        },
                        ViewpointDestination = new TestXml.KMPXml.Camera.Camera_Value.Viewpoint_Destination
                        {
                            X = CameraValues.Viewpoint_Destination.X,
                            Y = CameraValues.Viewpoint_Destination.Y,
                            Z = CameraValues.Viewpoint_Destination.Z
                        },
                        CameraType = CameraValues.CameraType,
                        NextCameraIndex = CameraValues.NextCameraIndex,
                        NextVideoIndex = CameraValues.EMAC_NextVideoIndex,
                        Route_CameraIndex = CameraValues.EMAC_ITOP_CameraIndex,
                        StartFlag = CameraValues.EMAC_StartFlag,
                        VideoFlag = CameraValues.EMAC_VideoFlag,
                        CameraActiveTime = CameraValues.Camera_Active_Time
                    };

                    camera_Values.Add(camera_Value);
                }

                TestXml.KMPXml.Camera Camera = new TestXml.KMPXml.Camera
                {
                    Values = camera_Values
                };

                KMP_Xml.Cameras = Camera;
            }
            if (section == TestXml.KMPXmlSetting.Section.JugemPoint)
            {
                List<TestXml.KMPXml.JugemPoint.JugemPoint_Value> jugemPoint_Values = new List<TestXml.KMPXml.JugemPoint.JugemPoint_Value>();

                foreach (var JugempointValues in kMPPropertyGridSettings.TPGJSection.TPGJValueList)
                {
                    TestXml.KMPXml.JugemPoint.JugemPoint_Value jugemPoint_Value = new TestXml.KMPXml.JugemPoint.JugemPoint_Value
                    {
                        Position = new TestXml.KMPXml.JugemPoint.JugemPoint_Value.JugemPoint_Position
                        {
                            X = JugempointValues.Positions.X,
                            Y = JugempointValues.Positions.Y,
                            Z = JugempointValues.Positions.Z
                        },
                        Rotation = new TestXml.KMPXml.JugemPoint.JugemPoint_Value.JugemPoint_Rotation
                        {
                            X = JugempointValues.Rotations.X,
                            Y = JugempointValues.Rotations.Y,
                            Z = JugempointValues.Rotations.Z
                        },
                        RespawnID = JugempointValues.TPGJ_RespawnID,
                        UnkBytes1 = JugempointValues.TPGJ_UnkBytes1
                    };

                    jugemPoint_Values.Add(jugemPoint_Value);
                }

                TestXml.KMPXml.JugemPoint JugemPoint = new TestXml.KMPXml.JugemPoint
                {
                    Values = jugemPoint_Values
                };

                KMP_Xml.JugemPoints = JugemPoint;
            }
            if (section == TestXml.KMPXmlSetting.Section.GlideRoutes)
            {
                List<TestXml.KMPXml.GlideRoute.GlideRoute_Group> glideRoute_Groups = new List<TestXml.KMPXml.GlideRoute.GlideRoute_Group>();

                foreach (var GlideRouteGroups in kMPPropertyGridSettings.HPLG_TPLGSection.HPLGValueList)
                {
                    TestXml.KMPXml.GlideRoute.GlideRoute_Group glideRoute_Group = new TestXml.KMPXml.GlideRoute.GlideRoute_Group
                    {
                        PreviousGroups = new TestXml.KMPXml.GlideRoute.GlideRoute_Group.GR_PreviousGroup
                        {
                            Prev0 = GlideRouteGroups.HPLG_PreviewGroup.Prev0,
                            Prev1 = GlideRouteGroups.HPLG_PreviewGroup.Prev1,
                            Prev2 = GlideRouteGroups.HPLG_PreviewGroup.Prev2,
                            Prev3 = GlideRouteGroups.HPLG_PreviewGroup.Prev3,
                            Prev4 = GlideRouteGroups.HPLG_PreviewGroup.Prev4,
                            Prev5 = GlideRouteGroups.HPLG_PreviewGroup.Prev5
                        },
                        NextGroups = new TestXml.KMPXml.GlideRoute.GlideRoute_Group.GR_NextGroup
                        {
                            Next0 = GlideRouteGroups.HPLG_NextGroup.Next0,
                            Next1 = GlideRouteGroups.HPLG_NextGroup.Next1,
                            Next2 = GlideRouteGroups.HPLG_NextGroup.Next2,
                            Next3 = GlideRouteGroups.HPLG_NextGroup.Next3,
                            Next4 = GlideRouteGroups.HPLG_NextGroup.Next4,
                            Next5 = GlideRouteGroups.HPLG_NextGroup.Next5
                        },
                        RouteSetting = GlideRouteGroups.RouteSettings.RouteSettingValue,
                        UnkBytes2 = GlideRouteGroups.HPLG_UnkBytes2,
                        Points = null
                    };

                    List<TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point> glideRoute_Points = new List<TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point>();

                    foreach (var GlidePoint in GlideRouteGroups.TPLGValueList)
                    {
                        TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point glideRoute_Point = new TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point
                        {
                            Position = new TestXml.KMPXml.GlideRoute.GlideRoute_Group.GlideRoute_Point.GlideRoute_Position
                            {
                                X = GlidePoint.Positions.X,
                                Y = GlidePoint.Positions.Y,
                                Z = GlidePoint.Positions.Z
                            },
                            PointScale = GlidePoint.TPLG_PointScaleValue,
                            UnkBytes1 = GlidePoint.TPLG_UnkBytes1,
                            UnkBytes2 = GlidePoint.TPLG_UnkBytes2
                        };

                        glideRoute_Points.Add(glideRoute_Point);
                    }

                    glideRoute_Group.Points = glideRoute_Points;

                    glideRoute_Groups.Add(glideRoute_Group);
                }

                TestXml.KMPXml.GlideRoute GlideRoute = new TestXml.KMPXml.GlideRoute
                {
                    Groups = glideRoute_Groups
                };

                KMP_Xml.GlideRoutes = GlideRoute;
            }

            //Delete Namespaces
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.KMPXml));
            System.IO.StreamWriter sw = new StreamWriter(Path + "_" + section.ToString() + ".xml", false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, KMP_Xml, xns);
            sw.Close();
        }

        public static void ExportXXXXRoute(KMPPropertyGridSettings kMPPropertyGridSettings, string Path, TestXml.XXXXRouteXmlSetting.RouteType routeType)
        {
            TestXml.XXXXRouteXml XXXXRoute_Xml = new TestXml.XXXXRouteXml
            {
                XXXXRoutes = new TestXml.XXXXRouteXml.XXXXRoute
                {
                    Groups = null
                }
            };

            if (routeType == TestXml.XXXXRouteXmlSetting.RouteType.EnemyRoute)
            {
                List<TestXml.XXXXRouteXml.XXXXRoute.GroupData> groupDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData>();

                foreach (var Groups in kMPPropertyGridSettings.HPNE_TPNESection.HPNEValueList)
                {
                    TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData
                    {
                        Points = null
                    };

                    List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData> pointDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData>();

                    foreach (var Points in Groups.TPNEValueList)
                    {
                        TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData
                        {
                            Position = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData.PositionData
                            {
                                X = Points.Positions.X,
                                Y = Points.Positions.Y,
                                Z = Points.Positions.Z
                            },
                            ScaleValue = Points.Control
                        };

                        pointDatas.Add(pointData);
                    }

                    groupData.Points = pointDatas;

                    groupDatas.Add(groupData);
                }

                XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
            }
            if (routeType == TestXml.XXXXRouteXmlSetting.RouteType.ItemRoute)
            {
                List<TestXml.XXXXRouteXml.XXXXRoute.GroupData> groupDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData>();

                foreach (var Groups in kMPPropertyGridSettings.HPTI_TPTISection.HPTIValueList)
                {
                    TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData
                    {
                        Points = null
                    };

                    List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData> pointDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData>();

                    foreach (var Points in Groups.TPTIValueList)
                    {
                        TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData
                        {
                            Position = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData.PositionData
                            {
                                X = Points.TPTI_Positions.X,
                                Y = Points.TPTI_Positions.Y,
                                Z = Points.TPTI_Positions.Z
                            },
                            ScaleValue = Points.TPTI_PointSize
                        };

                        pointDatas.Add(pointData);
                    }

                    groupData.Points = pointDatas;

                    groupDatas.Add(groupData);
                }

                XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
            }
            if (routeType == TestXml.XXXXRouteXmlSetting.RouteType.GlideRoute)
            {
                List<TestXml.XXXXRouteXml.XXXXRoute.GroupData> groupDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData>();

                foreach (var Groups in kMPPropertyGridSettings.HPLG_TPLGSection.HPLGValueList)
                {
                    TestXml.XXXXRouteXml.XXXXRoute.GroupData groupData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData
                    {
                        Points = null
                    };

                    List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData> pointDatas = new List<TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData>();

                    foreach (var Points in Groups.TPLGValueList)
                    {
                        TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData pointData = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData
                        {
                            Position = new TestXml.XXXXRouteXml.XXXXRoute.GroupData.PointData.PositionData
                            {
                                X = Points.Positions.X,
                                Y = Points.Positions.Y,
                                Z = Points.Positions.Z
                            },
                            ScaleValue = Points.TPLG_PointScaleValue
                        };

                        pointDatas.Add(pointData);
                    }

                    groupData.Points = pointDatas;

                    groupDatas.Add(groupData);
                }

                XXXXRoute_Xml.XXXXRoutes.Groups = groupDatas;
            }

            //Delete Namespaces
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);

            System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.XXXXRouteXml));
            System.IO.StreamWriter sw = new StreamWriter(Path + "_PositionAndScaleOnly" + ".xml", false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, XXXXRoute_Xml, xns);
            sw.Close();
        }
    }

    public class XMLImporter
    {
        public static T XMLImport<T>(string Path)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)s1.Deserialize(fs1);
        }

        public static KMPPropertyGridSettings ImportAll(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl, string YOffsetValue)
        {
            KMPPropertyGridSettings kMPPropertyGridSettings = new KMPPropertyGridSettings
            {
                TPTKSection = null,
                HPNE_TPNESection = null,
                HPTI_TPTISection = null,
                HPKC_TPKCSection = null,
                JBOGSection = null,
                ITOPSection = null,
                AERASection = null,
                EMACSection = null,
                TPGJSection = null,
                IGTSSection = null,
                HPLG_TPLGSection = null
            };

            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);

            #region KartPoint
            KMPPropertyGridSettings.TPTK_Section TPTK_Section = new KMPPropertyGridSettings.TPTK_Section(KMP_Xml_Model.startPositions);
            KMPs.KMPViewportRenderingXML.Render_StartPosition(UserCtrl, KMPViewportObject, KMP_Xml_Model.startPositions);
            kMPPropertyGridSettings.TPTKSection = TPTK_Section;
            #endregion

            #region Enemy_Routes
            KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section(KMP_Xml_Model.EnemyRoutes);
            KMPs.KMPViewportRenderingXML.Render_EnemyRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
            kMPPropertyGridSettings.HPNE_TPNESection = HPNE_TPNE_Section;
            #endregion

            #region Item Routes
            KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section(KMP_Xml_Model.ItemRoutes);
            KMPs.KMPViewportRenderingXML.Render_ItemRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
            kMPPropertyGridSettings.HPTI_TPTISection = HPTI_TPTI_Section;
            #endregion

            #region CheckPoint
            KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section(KMP_Xml_Model.Checkpoints);
            KMPs.KMPViewportRenderingXML.Render_Checkpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(YOffsetValue));
            kMPPropertyGridSettings.HPKC_TPKCSection = HPKC_TPKC_Section;
            #endregion

            #region OBJ
            KMPPropertyGridSettings.JBOG_Section JBOG_Section = new KMPPropertyGridSettings.JBOG_Section(KMP_Xml_Model.Objects);
            KMPs.KMPViewportRenderingXML.Render_Object(UserCtrl, KMPViewportObject, KMP_Xml_Model.Objects, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
            kMPPropertyGridSettings.JBOGSection = JBOG_Section;
            #endregion

            #region Route
            KMPPropertyGridSettings.ITOP_Section ITOP_Section = new KMPPropertyGridSettings.ITOP_Section(KMP_Xml_Model.Routes);
            KMPs.KMPViewportRenderingXML.Render_Route(UserCtrl, KMPViewportObject, KMP_Xml_Model.Routes);
            kMPPropertyGridSettings.ITOPSection = ITOP_Section;
            #endregion

            #region Area
            KMPPropertyGridSettings.AERA_Section AERA_Section = new KMPPropertyGridSettings.AERA_Section(KMP_Xml_Model.Areas);
            KMPs.KMPViewportRenderingXML.Render_Area(UserCtrl, KMPViewportObject, KMP_Xml_Model.Areas);
            kMPPropertyGridSettings.AERASection = AERA_Section;
            #endregion

            #region Camera
            KMPPropertyGridSettings.EMAC_Section EMAC_Section = new KMPPropertyGridSettings.EMAC_Section(KMP_Xml_Model.Cameras);
            KMPs.KMPViewportRenderingXML.Render_Camera(UserCtrl, KMPViewportObject, KMP_Xml_Model.Cameras);
            kMPPropertyGridSettings.EMACSection = EMAC_Section;
            #endregion

            #region JugemPoint
            KMPPropertyGridSettings.TPGJ_Section TPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section(KMP_Xml_Model.JugemPoints);
            KMPs.KMPViewportRenderingXML.Render_Returnpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.JugemPoints);
            kMPPropertyGridSettings.TPGJSection = TPGJ_Section;
            #endregion

            //TPNC : Unused Section
            //TPSM : Unused Section

            #region StageInfo
            kMPPropertyGridSettings.IGTSSection = new KMPPropertyGridSettings.IGTS_Section(KMP_Xml_Model.Stage_Info);
            #endregion

            //SROC : Unused Section

            #region GlideRoute
            KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section(KMP_Xml_Model.GlideRoutes);
            KMPs.KMPViewportRenderingXML.Render_GlideRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
            kMPPropertyGridSettings.HPLG_TPLGSection = HPLG_TPLG_Section;
            #endregion

            return kMPPropertyGridSettings;
        }

        public static KMPPropertyGridSettings.TPTK_Section ImportKartPosition(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.TPTK_Section TPTK_Section = new KMPPropertyGridSettings.TPTK_Section(KMP_Xml_Model.startPositions);
            KMPs.KMPViewportRenderingXML.Render_StartPosition(UserCtrl, KMPViewportObject, KMP_Xml_Model.startPositions);
            return TPTK_Section;
        }

        public static KMPPropertyGridSettings.HPNE_TPNE_Section ImportEnemyRoute(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section(KMP_Xml_Model.EnemyRoutes);
            KMPs.KMPViewportRenderingXML.Render_EnemyRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
            return HPNE_TPNE_Section;
        }

        public static KMPPropertyGridSettings.HPTI_TPTI_Section ImportItemRoute(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section(KMP_Xml_Model.ItemRoutes);
            KMPs.KMPViewportRenderingXML.Render_ItemRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
            return HPTI_TPTI_Section;
        }

        public static KMPPropertyGridSettings.HPKC_TPKC_Section ImportCheckpoint(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl, string YOffsetValue)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section(KMP_Xml_Model.Checkpoints);
            KMPs.KMPViewportRenderingXML.Render_Checkpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(YOffsetValue));
            return HPKC_TPKC_Section;
        }

        public static KMPPropertyGridSettings.JBOG_Section ImportObject(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.JBOG_Section JBOG_Section = new KMPPropertyGridSettings.JBOG_Section(KMP_Xml_Model.Objects);
            KMPs.KMPViewportRenderingXML.Render_Object(UserCtrl, KMPViewportObject, KMP_Xml_Model.Objects, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
            return JBOG_Section;
        }

        public static KMPPropertyGridSettings.ITOP_Section ImportRoute(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.ITOP_Section ITOP_Section = new KMPPropertyGridSettings.ITOP_Section(KMP_Xml_Model.Routes);
            KMPs.KMPViewportRenderingXML.Render_Route(UserCtrl, KMPViewportObject, KMP_Xml_Model.Routes);
            return ITOP_Section;
        }

        public static KMPPropertyGridSettings.AERA_Section ImportArea(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.AERA_Section AERA_Section = new KMPPropertyGridSettings.AERA_Section(KMP_Xml_Model.Areas);
            KMPs.KMPViewportRenderingXML.Render_Area(UserCtrl, KMPViewportObject, KMP_Xml_Model.Areas);
            return AERA_Section;
        }

        public static KMPPropertyGridSettings.EMAC_Section ImportCamera(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.EMAC_Section EMAC_Section = new KMPPropertyGridSettings.EMAC_Section(KMP_Xml_Model.Cameras);
            KMPs.KMPViewportRenderingXML.Render_Camera(UserCtrl, KMPViewportObject, KMP_Xml_Model.Cameras);
            return EMAC_Section;
        }

        public static KMPPropertyGridSettings.TPGJ_Section ImportJugemPoint(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.TPGJ_Section TPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section(KMP_Xml_Model.JugemPoints);
            KMPs.KMPViewportRenderingXML.Render_Returnpoint(UserCtrl, KMPViewportObject, KMP_Xml_Model.JugemPoints);
            return TPGJ_Section;
        }

        public static KMPPropertyGridSettings.HPLG_TPLG_Section ImportGlideRoute(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.KMPXml KMP_Xml_Model = XMLImport<TestXml.KMPXml>(Path);
            KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section(KMP_Xml_Model.GlideRoutes);
            KMPs.KMPViewportRenderingXML.Render_GlideRoute(UserCtrl, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
            return HPLG_TPLG_Section;
        }

        #region XXXX Route Importer
        public static KMPPropertyGridSettings.HPNE_TPNE_Section ImportEnemyRoutePositionAndScaleOnly(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
            KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section(XXXXRouteXml_Model.XXXXRoutes);
            KMPs.KMPViewportRenderingXML_XXXXRoute.Render_EnemyRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
            return HPNE_TPNE_Section;
        }

        public static KMPPropertyGridSettings.HPTI_TPTI_Section ImportItemRoutePositionAndScaleOnly(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
            KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section(XXXXRouteXml_Model.XXXXRoutes);
            KMPs.KMPViewportRenderingXML_XXXXRoute.Render_ItemRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
            return HPTI_TPTI_Section;
        }

        public static KMPPropertyGridSettings.HPLG_TPLG_Section ImportGlideRoutePositionAndScaleOnly(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            TestXml.XXXXRouteXml XXXXRouteXml_Model = XMLImport<TestXml.XXXXRouteXml>(Path);
            KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section(XXXXRouteXml_Model.XXXXRoutes);
            KMPs.KMPViewportRenderingXML_XXXXRoute.Render_GlideRoute(UserCtrl, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
            return HPLG_TPLG_Section;
        }
        #endregion
    }
}
