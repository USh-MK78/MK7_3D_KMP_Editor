using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        Flags = EnemyPoint.FlagSettings.Flags
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
    }
}
