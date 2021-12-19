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
                    Prev15 = EnemyRouteGroups.HPNEPreviewGroups.Prev15,
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
                    Prev0 = ItemRouteGroups.HPTI_PreviewGroup.Prev0,
                    Prev1 = ItemRouteGroups.HPTI_PreviewGroup.Prev1,
                    Prev2 = ItemRouteGroups.HPTI_PreviewGroup.Prev2,
                    Prev3 = ItemRouteGroups.HPTI_PreviewGroup.Prev3,
                    Prev4 = ItemRouteGroups.HPTI_PreviewGroup.Prev4,
                    Prev5 = ItemRouteGroups.HPTI_PreviewGroup.Prev5,
                    Next0 = ItemRouteGroups.HPTI_NextGroup.Next0,
                    Next1 = ItemRouteGroups.HPTI_NextGroup.Next1,
                    Next2 = ItemRouteGroups.HPTI_NextGroup.Next2,
                    Next3 = ItemRouteGroups.HPTI_NextGroup.Next3,
                    Next4 = ItemRouteGroups.HPTI_NextGroup.Next4,
                    Next5 = ItemRouteGroups.HPTI_NextGroup.Next5,
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
                    Prev0 = CheckpointGroups.HPKC_PreviewGroup.Prev0,
                    Prev1 = CheckpointGroups.HPKC_PreviewGroup.Prev1,
                    Prev2 = CheckpointGroups.HPKC_PreviewGroup.Prev2,
                    Prev3 = CheckpointGroups.HPKC_PreviewGroup.Prev3,
                    Prev4 = CheckpointGroups.HPKC_PreviewGroup.Prev4,
                    Prev5 = CheckpointGroups.HPKC_PreviewGroup.Prev5,
                    Next0 = CheckpointGroups.HPKC_NextGroup.Next0,
                    Next1 = CheckpointGroups.HPKC_NextGroup.Next1,
                    Next2 = CheckpointGroups.HPKC_NextGroup.Next2,
                    Next3 = CheckpointGroups.HPKC_NextGroup.Next3,
                    Next4 = CheckpointGroups.HPKC_NextGroup.Next4,
                    Next5 = CheckpointGroups.HPKC_NextGroup.Next5,
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
                        UnkBytes1 = CheckpointPoint.TPKC_UnkBytes1,
                        UnkBytes2 = CheckpointPoint.TPKC_UnkBytes2,
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
                    RouteSetting1 = RouteGroups.ITOP_RouteSetting1,
                    RouteSetting2 = RouteGroups.ITOP_RouteSetting2,
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
                    UnkByte1 = AreaValues.AERA_UnkByte1,
                    UnkByte2 = AreaValues.AERA_UnkByte2,
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
                    UnkBytes1 = CameraValues.EMAC_UnkBytes1,
                    Route_CameraIndex = CameraValues.EMAC_ITOP_CameraIndex,
                    UnkBytes2 = CameraValues.EMAC_UnkBytes2,
                    UnkBytes3 = CameraValues.EMAC_UnkBytes3,
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
                    Prev0 = GlideRouteGroups.HPLG_PreviewGroup.Prev0,
                    Prev1 = GlideRouteGroups.HPLG_PreviewGroup.Prev1,
                    Prev2 = GlideRouteGroups.HPLG_PreviewGroup.Prev2,
                    Prev3 = GlideRouteGroups.HPLG_PreviewGroup.Prev3,
                    Prev4 = GlideRouteGroups.HPLG_PreviewGroup.Prev4,
                    Prev5 = GlideRouteGroups.HPLG_PreviewGroup.Prev5,
                    Next0 = GlideRouteGroups.HPLG_NextGroup.Next0,
                    Next1 = GlideRouteGroups.HPLG_NextGroup.Next1,
                    Next2 = GlideRouteGroups.HPLG_NextGroup.Next2,
                    Next3 = GlideRouteGroups.HPLG_NextGroup.Next3,
                    Next4 = GlideRouteGroups.HPLG_NextGroup.Next4,
                    Next5 = GlideRouteGroups.HPLG_NextGroup.Next5,
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
                        Prev15 = EnemyRouteGroups.HPNEPreviewGroups.Prev15,
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
                        Prev0 = ItemRouteGroups.HPTI_PreviewGroup.Prev0,
                        Prev1 = ItemRouteGroups.HPTI_PreviewGroup.Prev1,
                        Prev2 = ItemRouteGroups.HPTI_PreviewGroup.Prev2,
                        Prev3 = ItemRouteGroups.HPTI_PreviewGroup.Prev3,
                        Prev4 = ItemRouteGroups.HPTI_PreviewGroup.Prev4,
                        Prev5 = ItemRouteGroups.HPTI_PreviewGroup.Prev5,
                        Next0 = ItemRouteGroups.HPTI_NextGroup.Next0,
                        Next1 = ItemRouteGroups.HPTI_NextGroup.Next1,
                        Next2 = ItemRouteGroups.HPTI_NextGroup.Next2,
                        Next3 = ItemRouteGroups.HPTI_NextGroup.Next3,
                        Next4 = ItemRouteGroups.HPTI_NextGroup.Next4,
                        Next5 = ItemRouteGroups.HPTI_NextGroup.Next5,
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
                        Prev0 = CheckpointGroups.HPKC_PreviewGroup.Prev0,
                        Prev1 = CheckpointGroups.HPKC_PreviewGroup.Prev1,
                        Prev2 = CheckpointGroups.HPKC_PreviewGroup.Prev2,
                        Prev3 = CheckpointGroups.HPKC_PreviewGroup.Prev3,
                        Prev4 = CheckpointGroups.HPKC_PreviewGroup.Prev4,
                        Prev5 = CheckpointGroups.HPKC_PreviewGroup.Prev5,
                        Next0 = CheckpointGroups.HPKC_NextGroup.Next0,
                        Next1 = CheckpointGroups.HPKC_NextGroup.Next1,
                        Next2 = CheckpointGroups.HPKC_NextGroup.Next2,
                        Next3 = CheckpointGroups.HPKC_NextGroup.Next3,
                        Next4 = CheckpointGroups.HPKC_NextGroup.Next4,
                        Next5 = CheckpointGroups.HPKC_NextGroup.Next5,
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
                            UnkBytes1 = CheckpointPoint.TPKC_UnkBytes1,
                            UnkBytes2 = CheckpointPoint.TPKC_UnkBytes2,
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
                        RouteSetting1 = RouteGroups.ITOP_RouteSetting1,
                        RouteSetting2 = RouteGroups.ITOP_RouteSetting2,
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
                        UnkByte1 = AreaValues.AERA_UnkByte1,
                        UnkByte2 = AreaValues.AERA_UnkByte2,
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
                        UnkBytes1 = CameraValues.EMAC_UnkBytes1,
                        Route_CameraIndex = CameraValues.EMAC_ITOP_CameraIndex,
                        UnkBytes2 = CameraValues.EMAC_UnkBytes2,
                        UnkBytes3 = CameraValues.EMAC_UnkBytes3,
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
                        Prev0 = GlideRouteGroups.HPLG_PreviewGroup.Prev0,
                        Prev1 = GlideRouteGroups.HPLG_PreviewGroup.Prev1,
                        Prev2 = GlideRouteGroups.HPLG_PreviewGroup.Prev2,
                        Prev3 = GlideRouteGroups.HPLG_PreviewGroup.Prev3,
                        Prev4 = GlideRouteGroups.HPLG_PreviewGroup.Prev4,
                        Prev5 = GlideRouteGroups.HPLG_PreviewGroup.Prev5,
                        Next0 = GlideRouteGroups.HPLG_NextGroup.Next0,
                        Next1 = GlideRouteGroups.HPLG_NextGroup.Next1,
                        Next2 = GlideRouteGroups.HPLG_NextGroup.Next2,
                        Next3 = GlideRouteGroups.HPLG_NextGroup.Next3,
                        Next4 = GlideRouteGroups.HPLG_NextGroup.Next4,
                        Next5 = GlideRouteGroups.HPLG_NextGroup.Next5,
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
        public static KMPPropertyGridSettings ImportAll(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl, string YOffsetValue)
        {
            KMPs.KMPHelper.FlagConverter.EnemyRoute EnemyRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.EnemyRoute();
            KMPs.KMPHelper.FlagConverter.GlideRoute GlideRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.GlideRoute();

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

            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            #region KartPoint
            KMPPropertyGridSettings.TPTK_Section TPTK_Section = new KMPPropertyGridSettings.TPTK_Section
            {
                TPTKValueList = null
            };

            List<KMPPropertyGridSettings.TPTK_Section.TPTKValue> TPTKValues_List = new List<KMPPropertyGridSettings.TPTK_Section.TPTKValue>();

            foreach (var StartPosition in KMP_Xml_Model.startPositions.startPosition_Value.Select((value, index) => new { value, index }))
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

                #region Add Model(StartPosition)
                HTK_3DES.TSRSystem.Transform_Value StartPosition_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = StartPosition.value.Position.X,
                        Y = StartPosition.value.Position.Y,
                        Z = StartPosition.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = 10,
                        Y = 10,
                        Z = 10
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = StartPosition.value.Rotation.X,
                        Y = StartPosition.value.Rotation.Y,
                        Z = StartPosition.value.Rotation.Z
                    }
                };

                ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\StartPosition\\StartPosition.obj");

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + StartPosition.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(StartPosition_transform_Value, dv3D_StartPositionOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                KMPViewportObject.StartPosition_MV3DList.Add(dv3D_StartPositionOBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_StartPositionOBJ);

                HTK_3DES.TSRSystem.GC_Dispose(dv3D_StartPositionOBJ);
                #endregion
            }

            TPTK_Section.TPTKValueList = TPTKValues_List;

            kMPPropertyGridSettings.TPTKSection = TPTK_Section;
            #endregion

            #region Enemy_Routes
            KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section
            {
                HPNEValueList = null
            };

            List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> HPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>();

            foreach (var EnemyRoute in KMP_Xml_Model.EnemyRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

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

                    #region Add Model(EnemyRoutes)
                    HTK_3DES.TSRSystem.Transform_Value EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = EnemyPoint.value.Position.X,
                            Y = EnemyPoint.value.Position.Y,
                            Z = EnemyPoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = EnemyPoint.value.Control * 100,
                            Y = EnemyPoint.value.Control * 100,
                            Z = EnemyPoint.value.Control * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\EnemyPath\\EnemyPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + EnemyPoint.index + " " + EnemyRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(EnemyPoint_transform_Value, dv3D_EnemyPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add Rail => MV3DList
                    KMP_EnemyRoute_Rail.MV3D_List.Add(dv3D_EnemyPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_EnemyPathOBJ);
                    #endregion
                }

                hPNEValue.TPNEValueList = TPNEValues_List;

                HPNEValues_List.Add(hPNEValue);

                //Add point
                KMPViewportObject.EnemyRoute_Rail_List.Add(KMP_EnemyRoute_Rail);
            }

            HPNE_TPNE_Section.HPNEValueList = HPNEValues_List;

            kMPPropertyGridSettings.HPNE_TPNESection = HPNE_TPNE_Section;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.EnemyRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.EnemyRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.EnemyRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Orange);
            }
            #endregion

            #endregion

            #region Item Routes
            KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section
            {
                HPTIValueList = null
            };

            List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> HPTIValues_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>();

            foreach (var ItemRoute in KMP_Xml_Model.ItemRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

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

                    #region Add Model(ItemRoutes)
                    HTK_3DES.TSRSystem.Transform_Value ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = ItemPoint.value.Position.X,
                            Y = ItemPoint.value.Position.Y,
                            Z = ItemPoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = ItemPoint.value.PointSize * 100,
                            Y = ItemPoint.value.PointSize * 100,
                            Z = ItemPoint.value.PointSize * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\ItemPath\\ItemPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + ItemPoint.index + " " + ItemRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(ItemPoint_transform_Value, dv3D_ItemPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add Rail => MV3DList
                    KMP_ItemRoute_Rail.MV3D_List.Add(dv3D_ItemPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_ItemPathOBJ);
                    #endregion
                }

                hPTIValue.TPTIValueList = TPTIVales_List;

                HPTIValues_List.Add(hPTIValue);

                //Add point
                KMPViewportObject.ItemRoute_Rail_List.Add(KMP_ItemRoute_Rail);
            }

            HPTI_TPTI_Section.HPTIValueList = HPTIValues_List;

            kMPPropertyGridSettings.HPTI_TPTISection = HPTI_TPTI_Section;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.ItemRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.ItemRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.ItemRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Green);
            }
            #endregion

            #endregion

            #region CheckPoint
            //Checkpoint_List
            List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint> Checkpoints_List = new List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint>();

            KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section
            {
                HPKCValueList = null
            };

            List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue> HPKCValues_List = new List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue>();

            foreach (var Checkpoint_Group in KMP_Xml_Model.Checkpoints.Groups.Select((value, index) => new { value, index }))
            {
                //Checkpoint_Rails
                HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = new HTK_3DES.KMP_3DCheckpointSystem.Checkpoint
                {
                    Checkpoint_Left = new HTK_3DES.PathTools.Rail
                    {
                        LV3D_List = new List<LinesVisual3D>(),
                        TV3D_List = new List<TubeVisual3D>(),
                        MV3D_List = new List<ModelVisual3D>()
                    },
                    Checkpoint_Right = new HTK_3DES.PathTools.Rail
                    {
                        LV3D_List = new List<LinesVisual3D>(),
                        TV3D_List = new List<TubeVisual3D>(),
                        MV3D_List = new List<ModelVisual3D>()
                    },
                    Checkpoint_Line = new List<LinesVisual3D>(),
                    Checkpoint_Tube = new List<TubeVisual3D>(),
                    Checkpoint_SplitWallMDL = new List<ModelVisual3D>(),
                    SideWall_Left = new HTK_3DES.PathTools.SideWall
                    {
                        SideWallList = new List<ModelVisual3D>()
                    },
                    SideWall_Right = new HTK_3DES.PathTools.SideWall
                    {
                        SideWallList = new List<ModelVisual3D>()
                    }
                };

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
                        TPKC_UnkBytes1 = Checkpoint_Point.value.UnkBytes1,
                        TPKC_UnkBytes2 = Checkpoint_Point.value.UnkBytes2,
                        TPKC_UnkBytes3 = Checkpoint_Point.value.UnkBytes3,
                        TPKC_UnkBytes4 = Checkpoint_Point.value.UnkBytes4
                    };

                    TPKCValues_List.Add(tPKCValue);

                    #region Create
                    var P2D_Left = tPKCValue.Position_2D_Left;
                    Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                    Point3D P3DLeft = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DLeftToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                    P3DLeft.Y = Convert.ToDouble(YOffsetValue);

                    #region Transform(Left)
                    HTK_3DES.TSRSystem.Transform_Value P2DLeft_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = P3DLeft.X,
                            Y = P3DLeft.Y,
                            Z = P3DLeft.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 50,
                            Y = 50,
                            Z = 50
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Checkpoint\\LeftPoint\\Checkpoint_Left.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + Checkpoint_Point.index + " " + Checkpoint_Group.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(P2DLeft_transform_Value, dv3D_CheckpointLeftOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    checkpoint.Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                    HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointLeftOBJ);
                    #endregion

                    var P2D_Right = tPKCValue.Position_2D_Right;
                    Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                    Point3D P3DRight = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DRightToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                    P3DRight.Y = Convert.ToDouble(YOffsetValue);

                    #region Transform(Right)
                    HTK_3DES.TSRSystem.Transform_Value P2DRight_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = P3DRight.X,
                            Y = P3DRight.Y,
                            Z = P3DRight.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 50,
                            Y = 50,
                            Z = 50
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Checkpoint\\RightPoint\\Checkpoint_Right.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + Checkpoint_Point.index + " " + Checkpoint_Group.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(P2DRight_transform_Value, dv3D_CheckpointRightOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    checkpoint.Checkpoint_Right.MV3D_List.Add(dv3D_CheckpointRightOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointRightOBJ);

                    HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointRightOBJ);
                    #endregion

                    List<Point3D> point3Ds = new List<Point3D>();
                    point3Ds.Add(P3DLeft);
                    point3Ds.Add(P3DRight);

                    LinesVisual3D linesVisual3D = new LinesVisual3D
                    {
                        Points = new Point3DCollection(point3Ds),
                        Thickness = 1,
                        Color = Colors.Black
                    };

                    checkpoint.Checkpoint_Line.Add(linesVisual3D);
                    UserCtrl.MainViewPort.Children.Add(linesVisual3D);

                    #region SplitWall
                    Point3DCollection point3Ds1 = new Point3DCollection();
                    point3Ds1.Add(new Point3D(point3Ds[1].X, 0, point3Ds[1].Z));
                    point3Ds1.Add(point3Ds[1]);
                    point3Ds1.Add(new Point3D(point3Ds[0].X, 0, point3Ds[0].Z));
                    point3Ds1.Add(point3Ds[0]);

                    ModelVisual3D SplitWall = HTK_3DES.CustomModelCreateHelper.CustomRectanglePlane3D(point3Ds1, System.Windows.Media.Color.FromArgb(0xA0, 0xA0, 0x00, 0xA0), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                    HTK_3DES.TSRSystem.SetString_MV3D(SplitWall, "SplitWall -1 -1");
                    checkpoint.Checkpoint_SplitWallMDL.Add(SplitWall);
                    UserCtrl.MainViewPort.Children.Add(SplitWall);
                    #endregion
                    #endregion
                }

                hPKCValue.TPKCValueList = TPKCValues_List;

                HPKCValues_List.Add(hPKCValue);

                //Add Checkpoint
                Checkpoints_List.Add(checkpoint);
            }

            HPKC_TPKC_Section.HPKCValueList = HPKCValues_List;

            KMPViewportObject.Checkpoint_Rail = Checkpoints_List;

            kMPPropertyGridSettings.HPKC_TPKCSection = HPKC_TPKC_Section;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.Checkpoint_Rail.Count; i++)
            {
                List<Point3D> point3Ds_Left = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.MV3D_List);
                KMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.LV3D_List = HTK_3DES.PathTools.DrawPath_Line(UserCtrl, point3Ds_Left, 5, Colors.Green);

                KMPViewportObject.Checkpoint_Rail[i].SideWall_Left.SideWallList = HTK_3DES.PathTools.DrawPath_SideWall(UserCtrl, point3Ds_Left, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00));

                List<Point3D> point3Ds_Right = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.MV3D_List);
                KMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.LV3D_List = HTK_3DES.PathTools.DrawPath_Line(UserCtrl, point3Ds_Right, 5, Colors.Red);

                KMPViewportObject.Checkpoint_Rail[i].SideWall_Right.SideWallList = HTK_3DES.PathTools.DrawPath_SideWall(UserCtrl, point3Ds_Right, System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
            }
            #endregion

            #endregion

            #region OBJ
            KMPPropertyGridSettings.JBOG_section JBOG_Section = new KMPPropertyGridSettings.JBOG_section
            {
                JBOGValueList = null
            };

            List<KMPPropertyGridSettings.JBOG_section.JBOGValue> JBOGValues_List = new List<KMPPropertyGridSettings.JBOG_section.JBOGValue>();

            foreach (var Object in KMP_Xml_Model.Objects.Object_Values.Select((value, index) => new { value, index }))
            {
                KMPs.KMPHelper.ObjFlowReader.ObjFlowXmlToObject objFlowXmlToObject_FindName = KMPs.KMPHelper.ObjFlowReader.ReadObjFlowXml("ObjFlowData.xml");
                string Name = objFlowXmlToObject_FindName.ObjFlows.Find(x => x.ObjectID == Object.value.ObjectID).ObjectName;

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

                #region Add Model(OBJ)
                HTK_3DES.TSRSystem.Transform_Value OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = Object.value.Position.X,
                        Y = Object.value.Position.Y,
                        Z = Object.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = Object.value.Scale.X * 2,
                        Y = Object.value.Scale.Y * 2,
                        Z = Object.value.Scale.Z * 2
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = Object.value.Rotation.X,
                        Y = Object.value.Rotation.Y,
                        Z = Object.value.Rotation.Z
                    }
                };

                KMPs.KMPHelper.ObjFlowReader.ObjFlowXmlToObject objFlowXmlToObject = KMPs.KMPHelper.ObjFlowReader.ReadObjFlowXml("ObjFlowData.xml");
                string ObjectPath = objFlowXmlToObject.ObjFlows.Find(x => x.ObjectID == Object.value.ObjectID).Path;
                ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(ObjectPath);

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + Object.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(OBJ_transform_Value, dv3D_OBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                KMPViewportObject.OBJ_MV3DList.Add(dv3D_OBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_OBJ);
                #endregion
            }

            JBOG_Section.JBOGValueList = JBOGValues_List;

            kMPPropertyGridSettings.JBOGSection = JBOG_Section;
            #endregion

            #region Route
            KMPPropertyGridSettings.ITOP_Section ITOP_Section = new KMPPropertyGridSettings.ITOP_Section
            {
                ITOP_RouteList = null
            };

            List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route> ITOPRoutes_List = new List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route>();

            foreach (var Route_Group in KMP_Xml_Model.Routes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail Route_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

                KMPPropertyGridSettings.ITOP_Section.ITOP_Route ITOPRoute = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route
                {
                    GroupID = Route_Group.index,
                    ITOP_RouteSetting1 = Route_Group.value.RouteSetting1,
                    ITOP_RouteSetting2 = Route_Group.value.RouteSetting2,
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

                    #region Add Model(Routes)
                    HTK_3DES.TSRSystem.Transform_Value JugemPath_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = Route_Point.value.Position.X,
                            Y = Route_Point.value.Position.Y,
                            Z = Route_Point.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 10,
                            Y = 10,
                            Z = 10
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_RouteOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Routes\\Routes.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RouteOBJ, "Routes " + Route_Point.index + " " + Route_Group.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(JugemPath_transform_Value, dv3D_RouteOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //AddMDL
                    Route_Rail.MV3D_List.Add(dv3D_RouteOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_RouteOBJ);
                    #endregion
                }

                ITOPRoute.ITOP_PointList = ITOPPoints_List;
                ITOPRoutes_List.Add(ITOPRoute);

                KMPViewportObject.Routes_List.Add(Route_Rail);
            }

            ITOP_Section.ITOP_RouteList = ITOPRoutes_List;

            kMPPropertyGridSettings.ITOPSection = ITOP_Section;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.Routes_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.Routes_List[i].MV3D_List);
                KMPViewportObject.Routes_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Blue);
            }
            #endregion

            #endregion

            #region Area
            KMPPropertyGridSettings.AERA_Section AERA_Section = new KMPPropertyGridSettings.AERA_Section
            {
                AERAValueList = null
            };

            List<KMPPropertyGridSettings.AERA_Section.AERAValue> AERAValues_List = new List<KMPPropertyGridSettings.AERA_Section.AERAValue>();

            foreach (var Area in KMP_Xml_Model.Areas.Area_Values.Select((value, index) => new { value, index }))
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
                    AERA_UnkByte1 = Area.value.UnkByte1,
                    AERA_UnkByte2 = Area.value.UnkByte2,
                    RouteID = Area.value.RouteID,
                    EnemyID = Area.value.EnemyID,
                    AERA_UnkByte4 = Area.value.UnkByte4
                };

                AERAValues_List.Add(AERAValue);

                #region Add Model(Area)
                HTK_3DES.TSRSystem.Transform_Value Area_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = Area.value.Position.X,
                        Y = Area.value.Position.Y,
                        Z = Area.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = Area.value.Scale.X * 1000,
                        Y = Area.value.Scale.Y * 1000,
                        Z = Area.value.Scale.Z * 1000
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = Area.value.Rotation.X,
                        Y = Area.value.Rotation.Y,
                        Z = Area.value.Rotation.Z
                    }
                };

                ModelVisual3D dv3D_AreaOBJ = null;
                if (AERAValue.AreaModeSettings.AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Area\\Area_Box\\Area_Box.obj");
                if (AERAValue.AreaModeSettings.AreaModeValue == 1) dv3D_AreaOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Area\\Area_Cylinder\\Area_Cylinder.obj");
                if (AERAValue.AreaModeSettings.AreaModeValue > 1) dv3D_AreaOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Area\\Area_Box\\Area_Box.obj");

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + Area.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(Area_transform_Value, dv3D_AreaOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                //Area_MV3D_List.Add(dv3D_AreaOBJ);
                KMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_AreaOBJ);
                #endregion
            }

            AERA_Section.AERAValueList = AERAValues_List;

            kMPPropertyGridSettings.AERASection = AERA_Section;
            #endregion

            #region Camera
            KMPPropertyGridSettings.EMAC_Section EMAC_Section = new KMPPropertyGridSettings.EMAC_Section
            {
                EMACValueList = null
            };

            List<KMPPropertyGridSettings.EMAC_Section.EMACValue> EMACValues_List = new List<KMPPropertyGridSettings.EMAC_Section.EMACValue>();

            foreach (var Camera in KMP_Xml_Model.Cameras.Values.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.EMAC_Section.EMACValue EMACValue = new KMPPropertyGridSettings.EMAC_Section.EMACValue
                {
                    ID = Camera.index,
                    CameraType = Camera.value.CameraType,
                    NextCameraIndex = Camera.value.NextCameraIndex,
                    EMAC_UnkBytes1 = Camera.value.UnkBytes1,
                    EMAC_ITOP_CameraIndex = Camera.value.Route_CameraIndex,
                    SpeedSettings = new KMPPropertyGridSettings.EMAC_Section.EMACValue.SpeedSetting
                    {
                        RouteSpeed = Camera.value.SpeedSetting.RouteSpeed,
                        FOVSpeed = Camera.value.SpeedSetting.FOVSpeed,
                        ViewpointSpeed = Camera.value.SpeedSetting.ViewpointSpeed
                    },
                    EMAC_UnkBytes2 = Camera.value.UnkBytes2,
                    EMAC_UnkBytes3 = Camera.value.UnkBytes3,
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

                #region Add Model(Camera)
                HTK_3DES.TSRSystem.Transform_Value Camera_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = Camera.value.Position.X,
                        Y = Camera.value.Position.Y,
                        Z = Camera.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = 10,
                        Y = 10,
                        Z = 10
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = Camera.value.Rotation.X,
                        Y = Camera.value.Rotation.Y,
                        Z = Camera.value.Rotation.Z
                    }
                };

                ModelVisual3D dv3D_CameraOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Camera\\Camera.obj");

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CameraOBJ, "Camera " + Camera.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(Camera_transform_Value, dv3D_CameraOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                KMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_CameraOBJ);
                #endregion
            }

            EMAC_Section.EMACValueList = EMACValues_List;

            kMPPropertyGridSettings.EMACSection = EMAC_Section;
            #endregion

            #region JugemPoint
            KMPPropertyGridSettings.TPGJ_Section TPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section
            {
                TPGJValueList = null
            };

            List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue> TPGJValues_List = new List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue>();

            foreach (var JugemPoint in KMP_Xml_Model.JugemPoints.Values.Select((value, index) => new { value, index }))
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

                #region Add Model(RespawnPoint)
                HTK_3DES.TSRSystem.Transform_Value RespawnPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = JugemPoint.value.Position.X,
                        Y = JugemPoint.value.Position.Y,
                        Z = JugemPoint.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = 10,
                        Y = 10,
                        Z = 10
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = JugemPoint.value.Rotation.X,
                        Y = JugemPoint.value.Rotation.Y,
                        Z = JugemPoint.value.Rotation.Z
                    }
                };

                ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\RespawnPoint\\RespawnPoint.obj");

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + JugemPoint.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(RespawnPoint_transform_Value, dv3D_RespawnPointOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                //RespawnPoint_MV3D_List.Add(dv3D_RespawnPointOBJ);
                KMPViewportObject.RespawnPoint_MV3DList.Add(dv3D_RespawnPointOBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_RespawnPointOBJ);
                #endregion
            }

            TPGJ_Section.TPGJValueList = TPGJValues_List;

            kMPPropertyGridSettings.TPGJSection = TPGJ_Section;
            #endregion

            //TPNC : Unused Section
            //TPSM : Unused Section

            #region StageInfo
            KMPPropertyGridSettings.IGTS_Section IGTS_Section = new KMPPropertyGridSettings.IGTS_Section
            {
                Unknown1 = KMP_Xml_Model.Stage_Info.Unknown1,
                LapCount = KMP_Xml_Model.Stage_Info.LapCount,
                PolePosition = KMP_Xml_Model.Stage_Info.PolePosition,
                Unknown2 = KMP_Xml_Model.Stage_Info.Unknown2,
                Unknown3 = KMP_Xml_Model.Stage_Info.Unknown3,
                RGBAColor = new KMPPropertyGridSettings.IGTS_Section.RGBA
                {
                    R = KMP_Xml_Model.Stage_Info.RGBAColor.R,
                    G = KMP_Xml_Model.Stage_Info.RGBAColor.G,
                    B = KMP_Xml_Model.Stage_Info.RGBAColor.B,
                    A = KMP_Xml_Model.Stage_Info.RGBAColor.A
                },
                FlareAlpha = KMP_Xml_Model.Stage_Info.RGBAColor.FlareAlpha
            };

            kMPPropertyGridSettings.IGTSSection = IGTS_Section;
            #endregion

            //SROC : Unused Section

            #region GlideRoute
            KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section
            {
                HPLGValueList = null
            };

            List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> HPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>();

            foreach (var GlideRoute in KMP_Xml_Model.GlideRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

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

                    #region Add Model(GlideRoutes)
                    HTK_3DES.TSRSystem.Transform_Value GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = GlidePoint.value.Position.X,
                            Y = GlidePoint.value.Position.Y,
                            Z = GlidePoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = GlidePoint.value.PointScale * 100,
                            Y = GlidePoint.value.PointScale * 100,
                            Z = GlidePoint.value.PointScale * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\GliderPath\\GliderPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + GlidePoint.index + " " + GlideRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(GliderPoint_transform_Value, dv3D_GliderPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add model
                    GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    #endregion
                }

                HPLGValue.TPLGValueList = TPLGValues_List;

                HPLGValues_List.Add(HPLGValue);

                KMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
            }

            HPLG_TPLG_Section.HPLGValueList = HPLGValues_List;

            kMPPropertyGridSettings.HPLG_TPLGSection = HPLG_TPLG_Section;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.GlideRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.GlideRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.GlideRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.LightSkyBlue);
            }
            #endregion

            #endregion

            return kMPPropertyGridSettings;
        }

        public static KMPPropertyGridSettings.TPTK_Section ImportKartPosition(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.TPTK_Section TPTK_Section = new KMPPropertyGridSettings.TPTK_Section
            {
                TPTKValueList = null
            };

            List<KMPPropertyGridSettings.TPTK_Section.TPTKValue> TPTKValues_List = new List<KMPPropertyGridSettings.TPTK_Section.TPTKValue>();

            foreach (var StartPosition in KMP_Xml_Model.startPositions.startPosition_Value.Select((value, index) => new { value, index }))
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

                #region Add Model(StartPosition)
                HTK_3DES.TSRSystem.Transform_Value StartPosition_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = StartPosition.value.Position.X,
                        Y = StartPosition.value.Position.Y,
                        Z = StartPosition.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = 10,
                        Y = 10,
                        Z = 10
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = StartPosition.value.Rotation.X,
                        Y = StartPosition.value.Rotation.Y,
                        Z = StartPosition.value.Rotation.Z
                    }
                };

                ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\StartPosition\\StartPosition.obj");

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + StartPosition.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(StartPosition_transform_Value, dv3D_StartPositionOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                KMPViewportObject.StartPosition_MV3DList.Add(dv3D_StartPositionOBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_StartPositionOBJ);

                HTK_3DES.TSRSystem.GC_Dispose(dv3D_StartPositionOBJ);
                #endregion
            }

            TPTK_Section.TPTKValueList = TPTKValues_List;

            return TPTK_Section;
        }

        public static KMPPropertyGridSettings.HPNE_TPNE_Section ImportEnemyRoute(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            KMPs.KMPHelper.FlagConverter.EnemyRoute EnemyRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.EnemyRoute();

            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section
            {
                HPNEValueList = null
            };

            List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> HPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>();

            foreach (var EnemyRoute in KMP_Xml_Model.EnemyRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

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

                    #region Add Model(EnemyRoutes)
                    HTK_3DES.TSRSystem.Transform_Value EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = EnemyPoint.value.Position.X,
                            Y = EnemyPoint.value.Position.Y,
                            Z = EnemyPoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = EnemyPoint.value.Control * 100,
                            Y = EnemyPoint.value.Control * 100,
                            Z = EnemyPoint.value.Control * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\EnemyPath\\EnemyPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + EnemyPoint.index + " " + EnemyRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(EnemyPoint_transform_Value, dv3D_EnemyPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add Rail => MV3DList
                    KMP_EnemyRoute_Rail.MV3D_List.Add(dv3D_EnemyPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_EnemyPathOBJ);
                    #endregion
                }

                hPNEValue.TPNEValueList = TPNEValues_List;

                HPNEValues_List.Add(hPNEValue);

                //Add point
                KMPViewportObject.EnemyRoute_Rail_List.Add(KMP_EnemyRoute_Rail);
            }

            HPNE_TPNE_Section.HPNEValueList = HPNEValues_List;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.EnemyRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.EnemyRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.EnemyRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Orange);
            }
            #endregion

            return HPNE_TPNE_Section;
        }

        public static KMPPropertyGridSettings.HPTI_TPTI_Section ImportItemRoute(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section
            {
                HPTIValueList = null
            };

            List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> HPTIValues_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>();

            foreach (var ItemRoute in KMP_Xml_Model.ItemRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

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

                    #region Add Model(ItemRoutes)
                    HTK_3DES.TSRSystem.Transform_Value ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = ItemPoint.value.Position.X,
                            Y = ItemPoint.value.Position.Y,
                            Z = ItemPoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = ItemPoint.value.PointSize * 100,
                            Y = ItemPoint.value.PointSize * 100,
                            Z = ItemPoint.value.PointSize * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\ItemPath\\ItemPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + ItemPoint.index + " " + ItemRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(ItemPoint_transform_Value, dv3D_ItemPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add Rail => MV3DList
                    KMP_ItemRoute_Rail.MV3D_List.Add(dv3D_ItemPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_ItemPathOBJ);
                    #endregion
                }

                hPTIValue.TPTIValueList = TPTIVales_List;

                HPTIValues_List.Add(hPTIValue);

                //Add point
                KMPViewportObject.ItemRoute_Rail_List.Add(KMP_ItemRoute_Rail);
            }

            HPTI_TPTI_Section.HPTIValueList = HPTIValues_List;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.ItemRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.ItemRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.ItemRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Green);
            }
            #endregion

            return HPTI_TPTI_Section;
        }

        public static KMPPropertyGridSettings.HPKC_TPKC_Section ImportCheckpoint(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl, string YOffsetValue)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            //Checkpoint_List
            List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint> Checkpoints_List = new List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint>();

            KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section
            {
                HPKCValueList = null
            };

            List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue> HPKCValues_List = new List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue>();

            foreach (var Checkpoint_Group in KMP_Xml_Model.Checkpoints.Groups.Select((value, index) => new { value, index }))
            {
                //Checkpoint_Rails
                HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = new HTK_3DES.KMP_3DCheckpointSystem.Checkpoint
                {
                    Checkpoint_Left = new HTK_3DES.PathTools.Rail
                    {
                        LV3D_List = new List<LinesVisual3D>(),
                        TV3D_List = new List<TubeVisual3D>(),
                        MV3D_List = new List<ModelVisual3D>()
                    },
                    Checkpoint_Right = new HTK_3DES.PathTools.Rail
                    {
                        LV3D_List = new List<LinesVisual3D>(),
                        TV3D_List = new List<TubeVisual3D>(),
                        MV3D_List = new List<ModelVisual3D>()
                    },
                    Checkpoint_Line = new List<LinesVisual3D>(),
                    Checkpoint_Tube = new List<TubeVisual3D>(),
                    Checkpoint_SplitWallMDL = new List<ModelVisual3D>(),
                    SideWall_Left = new HTK_3DES.PathTools.SideWall
                    {
                        SideWallList = new List<ModelVisual3D>()
                    },
                    SideWall_Right = new HTK_3DES.PathTools.SideWall
                    {
                        SideWallList = new List<ModelVisual3D>()
                    }
                };

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
                        TPKC_UnkBytes1 = Checkpoint_Point.value.UnkBytes1,
                        TPKC_UnkBytes2 = Checkpoint_Point.value.UnkBytes2,
                        TPKC_UnkBytes3 = Checkpoint_Point.value.UnkBytes3,
                        TPKC_UnkBytes4 = Checkpoint_Point.value.UnkBytes4
                    };

                    TPKCValues_List.Add(tPKCValue);

                    #region Create
                    var P2D_Left = tPKCValue.Position_2D_Left;
                    Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                    Point3D P3DLeft = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DLeftToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                    P3DLeft.Y = Convert.ToDouble(YOffsetValue);

                    #region Transform(Left)
                    HTK_3DES.TSRSystem.Transform_Value P2DLeft_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = P3DLeft.X,
                            Y = P3DLeft.Y,
                            Z = P3DLeft.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 50,
                            Y = 50,
                            Z = 50
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Checkpoint\\LeftPoint\\Checkpoint_Left.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + Checkpoint_Point.index + " " + Checkpoint_Group.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(P2DLeft_transform_Value, dv3D_CheckpointLeftOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    checkpoint.Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                    HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointLeftOBJ);
                    #endregion

                    var P2D_Right = tPKCValue.Position_2D_Right;
                    Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                    Point3D P3DRight = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DRightToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                    P3DRight.Y = Convert.ToDouble(YOffsetValue);

                    #region Transform(Right)
                    HTK_3DES.TSRSystem.Transform_Value P2DRight_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = P3DRight.X,
                            Y = P3DRight.Y,
                            Z = P3DRight.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 50,
                            Y = 50,
                            Z = 50
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Checkpoint\\RightPoint\\Checkpoint_Right.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + Checkpoint_Point.index + " " + Checkpoint_Group.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(P2DRight_transform_Value, dv3D_CheckpointRightOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    checkpoint.Checkpoint_Right.MV3D_List.Add(dv3D_CheckpointRightOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointRightOBJ);

                    HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointRightOBJ);
                    #endregion

                    List<Point3D> point3Ds = new List<Point3D>();
                    point3Ds.Add(P3DLeft);
                    point3Ds.Add(P3DRight);

                    LinesVisual3D linesVisual3D = new LinesVisual3D
                    {
                        Points = new Point3DCollection(point3Ds),
                        Thickness = 1,
                        Color = Colors.Black
                    };

                    checkpoint.Checkpoint_Line.Add(linesVisual3D);
                    UserCtrl.MainViewPort.Children.Add(linesVisual3D);

                    #region SplitWall
                    Point3DCollection point3Ds1 = new Point3DCollection();
                    point3Ds1.Add(new Point3D(point3Ds[1].X, 0, point3Ds[1].Z));
                    point3Ds1.Add(point3Ds[1]);
                    point3Ds1.Add(new Point3D(point3Ds[0].X, 0, point3Ds[0].Z));
                    point3Ds1.Add(point3Ds[0]);

                    ModelVisual3D SplitWall = HTK_3DES.CustomModelCreateHelper.CustomRectanglePlane3D(point3Ds1, System.Windows.Media.Color.FromArgb(0xA0, 0xA0, 0x00, 0xA0), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                    HTK_3DES.TSRSystem.SetString_MV3D(SplitWall, "SplitWall -1 -1");
                    checkpoint.Checkpoint_SplitWallMDL.Add(SplitWall);
                    UserCtrl.MainViewPort.Children.Add(SplitWall);
                    #endregion
                    #endregion
                }

                hPKCValue.TPKCValueList = TPKCValues_List;

                HPKCValues_List.Add(hPKCValue);

                //Add Checkpoint
                Checkpoints_List.Add(checkpoint);
            }

            HPKC_TPKC_Section.HPKCValueList = HPKCValues_List;

            KMPViewportObject.Checkpoint_Rail = Checkpoints_List;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.Checkpoint_Rail.Count; i++)
            {
                List<Point3D> point3Ds_Left = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.MV3D_List);
                KMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.LV3D_List = HTK_3DES.PathTools.DrawPath_Line(UserCtrl, point3Ds_Left, 5, Colors.Green);

                KMPViewportObject.Checkpoint_Rail[i].SideWall_Left.SideWallList = HTK_3DES.PathTools.DrawPath_SideWall(UserCtrl, point3Ds_Left, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00));

                List<Point3D> point3Ds_Right = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.MV3D_List);
                KMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.LV3D_List = HTK_3DES.PathTools.DrawPath_Line(UserCtrl, point3Ds_Right, 5, Colors.Red);

                KMPViewportObject.Checkpoint_Rail[i].SideWall_Right.SideWallList = HTK_3DES.PathTools.DrawPath_SideWall(UserCtrl, point3Ds_Right, System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
            }
            #endregion

            return HPKC_TPKC_Section;
        }

        public static KMPPropertyGridSettings.JBOG_section ImportObject(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.JBOG_section JBOG_Section = new KMPPropertyGridSettings.JBOG_section
            {
                JBOGValueList = null
            };

            List<KMPPropertyGridSettings.JBOG_section.JBOGValue> JBOGValues_List = new List<KMPPropertyGridSettings.JBOG_section.JBOGValue>();

            foreach (var Object in KMP_Xml_Model.Objects.Object_Values.Select((value, index) => new { value, index }))
            {
                KMPs.KMPHelper.ObjFlowReader.ObjFlowXmlToObject objFlowXmlToObject_FindName = KMPs.KMPHelper.ObjFlowReader.ReadObjFlowXml("ObjFlowData.xml");
                string Name = objFlowXmlToObject_FindName.ObjFlows.Find(x => x.ObjectID == Object.value.ObjectID).ObjectName;

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

                #region Add Model(OBJ)
                HTK_3DES.TSRSystem.Transform_Value OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = Object.value.Position.X,
                        Y = Object.value.Position.Y,
                        Z = Object.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = Object.value.Scale.X * 2,
                        Y = Object.value.Scale.Y * 2,
                        Z = Object.value.Scale.Z * 2
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = Object.value.Rotation.X,
                        Y = Object.value.Rotation.Y,
                        Z = Object.value.Rotation.Z
                    }
                };

                KMPs.KMPHelper.ObjFlowReader.ObjFlowXmlToObject objFlowXmlToObject = KMPs.KMPHelper.ObjFlowReader.ReadObjFlowXml("ObjFlowData.xml");
                string ObjectPath = objFlowXmlToObject.ObjFlows.Find(x => x.ObjectID == Object.value.ObjectID).Path;
                ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(ObjectPath);

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + Object.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(OBJ_transform_Value, dv3D_OBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                KMPViewportObject.OBJ_MV3DList.Add(dv3D_OBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_OBJ);
                #endregion
            }

            JBOG_Section.JBOGValueList = JBOGValues_List;

            return JBOG_Section;
        }

        public static KMPPropertyGridSettings.ITOP_Section ImportRoute(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.ITOP_Section ITOP_Section = new KMPPropertyGridSettings.ITOP_Section
            {
                ITOP_RouteList = null
            };

            List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route> ITOPRoutes_List = new List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route>();

            foreach (var Route_Group in KMP_Xml_Model.Routes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail Route_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

                KMPPropertyGridSettings.ITOP_Section.ITOP_Route ITOPRoute = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route
                {
                    GroupID = Route_Group.index,
                    ITOP_RouteSetting1 = Route_Group.value.RouteSetting1,
                    ITOP_RouteSetting2 = Route_Group.value.RouteSetting2,
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

                    #region Add Model(Routes)
                    HTK_3DES.TSRSystem.Transform_Value JugemPath_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = Route_Point.value.Position.X,
                            Y = Route_Point.value.Position.Y,
                            Z = Route_Point.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 10,
                            Y = 10,
                            Z = 10
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_RouteOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Routes\\Routes.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RouteOBJ, "Routes " + Route_Point.index + " " + Route_Group.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(JugemPath_transform_Value, dv3D_RouteOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //AddMDL
                    Route_Rail.MV3D_List.Add(dv3D_RouteOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_RouteOBJ);
                    #endregion
                }

                ITOPRoute.ITOP_PointList = ITOPPoints_List;
                ITOPRoutes_List.Add(ITOPRoute);

                KMPViewportObject.Routes_List.Add(Route_Rail);
            }

            ITOP_Section.ITOP_RouteList = ITOPRoutes_List;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.Routes_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.Routes_List[i].MV3D_List);
                KMPViewportObject.Routes_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Blue);
            }
            #endregion

            return ITOP_Section;
        }

        public static KMPPropertyGridSettings.AERA_Section ImportArea(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.AERA_Section AERA_Section = new KMPPropertyGridSettings.AERA_Section
            {
                AERAValueList = null
            };

            List<KMPPropertyGridSettings.AERA_Section.AERAValue> AERAValues_List = new List<KMPPropertyGridSettings.AERA_Section.AERAValue>();

            foreach (var Area in KMP_Xml_Model.Areas.Area_Values.Select((value, index) => new { value, index }))
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
                    AERA_UnkByte1 = Area.value.UnkByte1,
                    AERA_UnkByte2 = Area.value.UnkByte2,
                    RouteID = Area.value.RouteID,
                    EnemyID = Area.value.EnemyID,
                    AERA_UnkByte4 = Area.value.UnkByte4
                };

                AERAValues_List.Add(AERAValue);

                #region Add Model(Area)
                HTK_3DES.TSRSystem.Transform_Value Area_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = Area.value.Position.X,
                        Y = Area.value.Position.Y,
                        Z = Area.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = Area.value.Scale.X * 1000,
                        Y = Area.value.Scale.Y * 1000,
                        Z = Area.value.Scale.Z * 1000
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = Area.value.Rotation.X,
                        Y = Area.value.Rotation.Y,
                        Z = Area.value.Rotation.Z
                    }
                };

                ModelVisual3D dv3D_AreaOBJ = null;
                if (AERAValue.AreaModeSettings.AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Area\\Area_Box\\Area_Box.obj");
                if (AERAValue.AreaModeSettings.AreaModeValue == 1) dv3D_AreaOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Area\\Area_Cylinder\\Area_Cylinder.obj");
                if (AERAValue.AreaModeSettings.AreaModeValue > 1) dv3D_AreaOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Area\\Area_Box\\Area_Box.obj");

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + Area.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(Area_transform_Value, dv3D_AreaOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                //Area_MV3D_List.Add(dv3D_AreaOBJ);
                KMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_AreaOBJ);
                #endregion
            }

            AERA_Section.AERAValueList = AERAValues_List;

            return AERA_Section;
        }

        public static KMPPropertyGridSettings.EMAC_Section ImportCamera(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.EMAC_Section EMAC_Section = new KMPPropertyGridSettings.EMAC_Section
            {
                EMACValueList = null
            };

            List<KMPPropertyGridSettings.EMAC_Section.EMACValue> EMACValues_List = new List<KMPPropertyGridSettings.EMAC_Section.EMACValue>();

            foreach (var Camera in KMP_Xml_Model.Cameras.Values.Select((value, index) => new { value, index }))
            {
                KMPPropertyGridSettings.EMAC_Section.EMACValue EMACValue = new KMPPropertyGridSettings.EMAC_Section.EMACValue
                {
                    ID = Camera.index,
                    CameraType = Camera.value.CameraType,
                    NextCameraIndex = Camera.value.NextCameraIndex,
                    EMAC_UnkBytes1 = Camera.value.UnkBytes1,
                    EMAC_ITOP_CameraIndex = Camera.value.Route_CameraIndex,
                    SpeedSettings = new KMPPropertyGridSettings.EMAC_Section.EMACValue.SpeedSetting
                    {
                        RouteSpeed = Camera.value.SpeedSetting.RouteSpeed,
                        FOVSpeed = Camera.value.SpeedSetting.FOVSpeed,
                        ViewpointSpeed = Camera.value.SpeedSetting.ViewpointSpeed
                    },
                    EMAC_UnkBytes2 = Camera.value.UnkBytes2,
                    EMAC_UnkBytes3 = Camera.value.UnkBytes3,
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

                #region Add Model(Camera)
                HTK_3DES.TSRSystem.Transform_Value Camera_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = Camera.value.Position.X,
                        Y = Camera.value.Position.Y,
                        Z = Camera.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = 10,
                        Y = 10,
                        Z = 10
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = Camera.value.Rotation.X,
                        Y = Camera.value.Rotation.Y,
                        Z = Camera.value.Rotation.Z
                    }
                };

                ModelVisual3D dv3D_CameraOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\Camera\\Camera.obj");

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CameraOBJ, "Camera " + Camera.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(Camera_transform_Value, dv3D_CameraOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                KMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_CameraOBJ);
                #endregion
            }

            EMAC_Section.EMACValueList = EMACValues_List;

            return EMAC_Section;
        }

        public static KMPPropertyGridSettings.TPGJ_Section ImportJugemPoint(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.TPGJ_Section TPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section
            {
                TPGJValueList = null
            };

            List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue> TPGJValues_List = new List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue>();

            foreach (var JugemPoint in KMP_Xml_Model.JugemPoints.Values.Select((value, index) => new { value, index }))
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

                #region Add Model(RespawnPoint)
                HTK_3DES.TSRSystem.Transform_Value RespawnPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                {
                    Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                    {
                        X = JugemPoint.value.Position.X,
                        Y = JugemPoint.value.Position.Y,
                        Z = JugemPoint.value.Position.Z
                    },
                    Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                    {
                        X = 10,
                        Y = 10,
                        Z = 10
                    },
                    Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                    {
                        X = JugemPoint.value.Rotation.X,
                        Y = JugemPoint.value.Rotation.Y,
                        Z = JugemPoint.value.Rotation.Z
                    }
                };

                ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\RespawnPoint\\RespawnPoint.obj");

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + JugemPoint.index + " " + -1);

                HTK_3DES.TransformMV3D.Transform_MV3D(RespawnPoint_transform_Value, dv3D_RespawnPointOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                //RespawnPoint_MV3D_List.Add(dv3D_RespawnPointOBJ);
                KMPViewportObject.RespawnPoint_MV3DList.Add(dv3D_RespawnPointOBJ);

                UserCtrl.MainViewPort.Children.Add(dv3D_RespawnPointOBJ);
                #endregion
            }

            TPGJ_Section.TPGJValueList = TPGJValues_List;

            return TPGJ_Section;
        }

        public static KMPPropertyGridSettings.HPLG_TPLG_Section ImportGlideRoute(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            KMPs.KMPHelper.FlagConverter.GlideRoute GlideRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.GlideRoute();

            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPXml));
            TestXml.KMPXml KMP_Xml_Model = (TestXml.KMPXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section
            {
                HPLGValueList = null
            };

            List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> HPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>();

            foreach (var GlideRoute in KMP_Xml_Model.GlideRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

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

                    #region Add Model(GlideRoutes)
                    HTK_3DES.TSRSystem.Transform_Value GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = GlidePoint.value.Position.X,
                            Y = GlidePoint.value.Position.Y,
                            Z = GlidePoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = GlidePoint.value.PointScale * 100,
                            Y = GlidePoint.value.PointScale * 100,
                            Z = GlidePoint.value.PointScale * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\GliderPath\\GliderPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + GlidePoint.index + " " + GlideRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(GliderPoint_transform_Value, dv3D_GliderPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add model
                    GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    #endregion
                }

                HPLGValue.TPLGValueList = TPLGValues_List;

                HPLGValues_List.Add(HPLGValue);

                KMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
            }

            HPLG_TPLG_Section.HPLGValueList = HPLGValues_List;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.GlideRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.GlideRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.GlideRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.LightSkyBlue);
            }
            #endregion

            return HPLG_TPLG_Section;
        }

        #region XXXX Route Importer
        public static KMPPropertyGridSettings.HPNE_TPNE_Section ImportEnemyRoutePositionAndScaleOnly(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            KMPs.KMPHelper.FlagConverter.EnemyRoute EnemyRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.EnemyRoute();

            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.XXXXRouteXml));
            TestXml.XXXXRouteXml XXXXRouteXml_Model = (TestXml.XXXXRouteXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section
            {
                HPNEValueList = null
            };

            List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue> HPNEValues_List = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>();

            foreach (var EnemyRoute in XXXXRouteXml_Model.XXXXRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

                KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue hPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue
                {
                    GroupID = EnemyRoute.index,
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

                    #region Add Model(EnemyRoutes)
                    HTK_3DES.TSRSystem.Transform_Value EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = EnemyPoint.value.Position.X,
                            Y = EnemyPoint.value.Position.Y,
                            Z = EnemyPoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = EnemyPoint.value.ScaleValue * 100,
                            Y = EnemyPoint.value.ScaleValue * 100,
                            Z = EnemyPoint.value.ScaleValue * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\EnemyPath\\EnemyPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + EnemyPoint.index + " " + EnemyRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(EnemyPoint_transform_Value, dv3D_EnemyPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add Rail => MV3DList
                    KMP_EnemyRoute_Rail.MV3D_List.Add(dv3D_EnemyPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_EnemyPathOBJ);
                    #endregion
                }

                hPNEValue.TPNEValueList = TPNEValues_List;

                HPNEValues_List.Add(hPNEValue);

                //Add point
                KMPViewportObject.EnemyRoute_Rail_List.Add(KMP_EnemyRoute_Rail);
            }

            HPNE_TPNE_Section.HPNEValueList = HPNEValues_List;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.EnemyRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.EnemyRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.EnemyRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Orange);
            }
            #endregion

            return HPNE_TPNE_Section;
        }

        public static KMPPropertyGridSettings.HPTI_TPTI_Section ImportItemRoutePositionAndScaleOnly(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.XXXXRouteXml));
            TestXml.XXXXRouteXml XXXXRouteXml_Model = (TestXml.XXXXRouteXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section
            {
                HPTIValueList = null
            };

            List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue> HPTIValues_List = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>();

            foreach (var ItemRoute in XXXXRouteXml_Model.XXXXRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

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

                    #region Add Model(ItemRoutes)
                    HTK_3DES.TSRSystem.Transform_Value ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = ItemPoint.value.Position.X,
                            Y = ItemPoint.value.Position.Y,
                            Z = ItemPoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = ItemPoint.value.ScaleValue * 100,
                            Y = ItemPoint.value.ScaleValue * 100,
                            Z = ItemPoint.value.ScaleValue * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\ItemPath\\ItemPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + ItemPoint.index + " " + ItemRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(ItemPoint_transform_Value, dv3D_ItemPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add Rail => MV3DList
                    KMP_ItemRoute_Rail.MV3D_List.Add(dv3D_ItemPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_ItemPathOBJ);
                    #endregion
                }

                hPTIValue.TPTIValueList = TPTIVales_List;

                HPTIValues_List.Add(hPTIValue);

                //Add point
                KMPViewportObject.ItemRoute_Rail_List.Add(KMP_ItemRoute_Rail);
            }

            HPTI_TPTI_Section.HPTIValueList = HPTIValues_List;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.ItemRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.ItemRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.ItemRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Green);
            }
            #endregion

            return HPTI_TPTI_Section;
        }

        public static KMPPropertyGridSettings.HPLG_TPLG_Section ImportGlideRoutePositionAndScaleOnly(KMPs.KMPViewportObject KMPViewportObject, string Path, UserControl1 UserCtrl)
        {
            KMPs.KMPHelper.FlagConverter.GlideRoute GlideRouteFlagConverter = new KMPs.KMPHelper.FlagConverter.GlideRoute();

            System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.XXXXRouteXml));
            TestXml.XXXXRouteXml XXXXRouteXml_Model = (TestXml.XXXXRouteXml)s1.Deserialize(fs1);

            KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section
            {
                HPLGValueList = null
            };

            List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue> HPLGValues_List = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>();

            foreach (var GlideRoute in XXXXRouteXml_Model.XXXXRoutes.Groups.Select((value, index) => new { value, index }))
            {
                //Rail
                HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail
                {
                    TV3D_List = new List<TubeVisual3D>(),
                    MV3D_List = new List<ModelVisual3D>()
                };

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

                    #region Add Model(GlideRoutes)
                    HTK_3DES.TSRSystem.Transform_Value GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = GlidePoint.value.Position.X,
                            Y = GlidePoint.value.Position.Y,
                            Z = GlidePoint.value.Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = GlidePoint.value.ScaleValue * 100,
                            Y = GlidePoint.value.ScaleValue * 100,
                            Z = GlidePoint.value.ScaleValue * 100
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = 0,
                            Y = 0,
                            Z = 0
                        }
                    };

                    ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.TSRSystem.OBJReader("KMP_OBJ\\GliderPath\\GliderPath.obj");

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + GlidePoint.index + " " + GlideRoute.index);

                    HTK_3DES.TransformMV3D.Transform_MV3D(GliderPoint_transform_Value, dv3D_GliderPathOBJ, HTK_3DES.TSRSystem.RotationSetting.Angle);

                    //Add model
                    GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    #endregion
                }

                HPLGValue.TPLGValueList = TPLGValues_List;

                HPLGValues_List.Add(HPLGValue);

                KMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
            }

            HPLG_TPLG_Section.HPLGValueList = HPLGValues_List;

            #region Add Rail
            for (int i = 0; i < KMPViewportObject.GlideRoute_Rail_List.Count; i++)
            {
                List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(KMPViewportObject.GlideRoute_Rail_List[i].MV3D_List);
                KMPViewportObject.GlideRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.LightSkyBlue);
            }
            #endregion

            return HPLG_TPLG_Section;
        }
        #endregion
    }
}
