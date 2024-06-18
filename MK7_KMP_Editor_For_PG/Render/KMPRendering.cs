using HelixToolkit.Wpf;
using KMPLibrary.Format.SectionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using KMPLibrary.KMPHelper;

namespace MK7_3D_KMP_Editor.Render
{
    public class KMPRendering
    {
        public class KMPViewportObject
        {
            public List<ModelVisual3D> Area_MV3DList { get; set; }
            public List<ModelVisual3D> Camera_MV3DList { get; set; }
            public List<HTK_3DES.PathTools.Rail> EnemyRoute_Rail_List { get; set; }
            public List<HTK_3DES.PathTools.Rail> ItemRoute_Rail_List { get; set; }
            public List<HTK_3DES.PathTools.Rail> Routes_List { get; set; }
            public List<HTK_3DES.PathTools.Rail> GlideRoute_Rail_List { get; set; }
            public List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint> Checkpoint_Rail { get; set; }
            public List<ModelVisual3D> OBJ_MV3DList { get; set; }
            public List<ModelVisual3D> RespawnPoint_MV3DList { get; set; }
            public List<ModelVisual3D> StartPosition_MV3DList { get; set; }

            //new KMPViewportObject();
            public KMPViewportObject()
            {
                Area_MV3DList = new List<ModelVisual3D>();
                Camera_MV3DList = new List<ModelVisual3D>();
                EnemyRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                ItemRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                GlideRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                Routes_List = new List<HTK_3DES.PathTools.Rail>();
                Checkpoint_Rail = new List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint>();
                OBJ_MV3DList = new List<ModelVisual3D>();
                RespawnPoint_MV3DList = new List<ModelVisual3D>();
                StartPosition_MV3DList = new List<ModelVisual3D>();
            }
        }

        public class KMPViewportRendering
        {
            public static void Render_StartPosition(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TPTK TPTK)
            {
                for (int i = 0; i < TPTK.NumOfEntries; i++)
                {
                    #region Add Model(StartPosition)
                    ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + i + " " + -1);

                    HTK_3DES.Transform StartPosition_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = TPTK.TPTKValue_List[i].TPTK_Position,
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = TPTK.TPTKValue_List[i].TPTK_Rotation
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_StartPositionOBJ, StartPosition_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    kMPViewportObject.StartPosition_MV3DList.Add(dv3D_StartPositionOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_StartPositionOBJ);

                    HTK_3DES.GC_Dispose(dv3D_StartPositionOBJ);
                    #endregion
                }
            }

            public static void Render_EnemyRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, HPNE HPNE, TPNE TPNE)
            {
                for (int i = 0; i < HPNE.NumOfEntries; i++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int Count = 0; Count < HPNE.HPNEValue_List[i].HPNE_Length; Count++)
                    {
                        #region Add Model(EnemyRoutes)
                        ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + Count + " " + i);

                        HTK_3DES.Transform EnemyPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].TPNE_Position,
                            Scale3D = HTK_3DES.ScaleFactor(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Control, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add Rail => MV3DList
                        KMP_EnemyRoute_Rail.MV3D_List.Add(dv3D_EnemyPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_EnemyPathOBJ);
                        #endregion
                    }

                    //Add point
                    kMPViewportObject.EnemyRoute_Rail_List.Add(KMP_EnemyRoute_Rail);
                }

                //Add Rail
                for (int i = 0; i < kMPViewportObject.EnemyRoute_Rail_List.Count; i++) kMPViewportObject.EnemyRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.Orange);
            }

            public static void Render_ItemRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, HPTI HPTI, TPTI TPTI)
            {
                for (int HPTICount = 0; HPTICount < HPTI.NumOfEntries; HPTICount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int Count = 0; Count < HPTI.HPTIValue_List[HPTICount].HPTI_Length; Count++)
                    {
                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + Count + " " + HPTICount);

                        HTK_3DES.Transform ItemPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_Position,
                            Scale3D = HTK_3DES.ScaleFactor(TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_PointSize, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add Rail => MV3DList
                        KMP_ItemRoute_Rail.MV3D_List.Add(dv3D_ItemPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_ItemPathOBJ);
                    }

                    //Add point
                    kMPViewportObject.ItemRoute_Rail_List.Add(KMP_ItemRoute_Rail);
                }

                //Add Rail
                for (int i = 0; i < kMPViewportObject.ItemRoute_Rail_List.Count; i++) kMPViewportObject.ItemRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.Green);
            }

            public static void Render_Checkpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, HPKC HPKC, TPKC TPKC, double CheckpointYOffset)
            {
                for (int HPKCCount = 0; HPKCCount < HPKC.NumOfEntries; HPKCCount++)
                {
                    //Checkpoint_Rails
                    HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = new HTK_3DES.KMP_3DCheckpointSystem.Checkpoint();

                    for (int Count = 0; Count < HPKC.HPKCValue_List[HPKCCount].HPKC_Length; Count++)
                    {
                        #region Create
                        var P2D_Left = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Left;
                        Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                        Point3D P3DLeft = Converter2D.Vector2DTo3D(P2DLeftToVector2, Converter2D.Axis_Up.Y).ToPoint3D();
                        P3DLeft.Y = CheckpointYOffset;

                        #region Transform(Left)
                        ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + Count + " " + HPKCCount);

                        HTK_3DES.Transform P2DLeft_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = P3DLeft.ToVector3D(),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D_P2DLeft = new HTK_3DES.TSRSystem3D(dv3D_CheckpointLeftOBJ, P2DLeft_transform_Value);
                        tSRSystem3D_P2DLeft.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        checkpoint.Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                        HTK_3DES.GC_Dispose(dv3D_CheckpointLeftOBJ);
                        #endregion

                        var P2D_Right = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Right;
                        Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                        Point3D P3DRight = Converter2D.Vector2DTo3D(P2DRightToVector2, Converter2D.Axis_Up.Y).ToPoint3D();
                        P3DRight.Y = CheckpointYOffset;

                        #region Transform(Right)
                        ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + Count + " " + HPKCCount);

                        HTK_3DES.Transform P2DRight_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = P3DRight.ToVector3D(),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D_P2DRight = new HTK_3DES.TSRSystem3D(dv3D_CheckpointRightOBJ, P2DRight_transform_Value);
                        tSRSystem3D_P2DRight.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        checkpoint.Checkpoint_Right.MV3D_List.Add(dv3D_CheckpointRightOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointRightOBJ);

                        HTK_3DES.GC_Dispose(dv3D_CheckpointRightOBJ);
                        #endregion

                        List<Point3D> point3Ds = new List<Point3D>();
                        point3Ds.Add(P3DLeft);
                        point3Ds.Add(P3DRight);

                        #region SplitLine
                        LinesVisual3D linesVisual3D = new LinesVisual3D
                        {
                            Points = new Point3DCollection(point3Ds),
                            Thickness = 1,
                            Color = Colors.Black
                        };

                        checkpoint.Checkpoint_Line.Add(linesVisual3D);
                        UserCtrl.MainViewPort.Children.Add(linesVisual3D);
                        #endregion

                        #region SplitWall
                        Point3DCollection point3Ds1 = new Point3DCollection();
                        point3Ds1.Add(new Point3D(point3Ds[1].X, 0, point3Ds[1].Z));
                        point3Ds1.Add(point3Ds[1]);
                        point3Ds1.Add(new Point3D(point3Ds[0].X, 0, point3Ds[0].Z));
                        point3Ds1.Add(point3Ds[0]);

                        ModelVisual3D SplitWall = HTK_3DES.CustomModelCreateHelper.CustomRectanglePlane3D(point3Ds1, System.Windows.Media.Color.FromArgb(0xA0, 0xA0, 0x00, 0xA0), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                        HTK_3DES.SetString_MV3D(SplitWall, "SplitWall -1 -1");
                        checkpoint.Checkpoint_SplitWallMDL.Add(SplitWall);
                        UserCtrl.MainViewPort.Children.Add(SplitWall);
                        #endregion
                        #endregion
                    }

                    //Add Checkpoint
                    kMPViewportObject.Checkpoint_Rail.Add(checkpoint);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.Checkpoint_Rail.Count; i++)
                {
                    kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.DrawPath_Line(UserCtrl, 5, Colors.Green);
                    kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.DrawPath_Line(UserCtrl, 5, Colors.Red);
                    kMPViewportObject.Checkpoint_Rail[i].DrawPath_SideWall(UserCtrl, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                }
                #endregion
            }

            public static void Render_Object(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, JBOG JBOG, List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> objFlowDataXml)
            {
                for (int Count = 0; Count < JBOG.NumOfEntries; Count++)
                {
                    string Path = objFlowDataXml.Find(x => x.ObjectID == BitConverter.ToString(JBOG.JBOGValue_List[Count].ObjectID.Reverse().ToArray()).Replace("-", string.Empty)).Path;
                    ModelVisual3D dv3D_OBJ = HTK_3DES.OBJReader(Path);

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_OBJ, "OBJ " + Count + " " + -1);

                    HTK_3DES.Transform OBJ_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = JBOG.JBOGValue_List[Count].JBOG_Position,
                        Scale3D = HTK_3DES.ScaleFactor(JBOG.JBOGValue_List[Count].JBOG_Scale, 2),
                        Rotate3D = JBOG.JBOGValue_List[Count].JBOG_Rotation
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    kMPViewportObject.OBJ_MV3DList.Add(dv3D_OBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_OBJ);
                }
            }

            public static void Render_Route(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, ITOP ITOP)
            {
                for (int ITOP_RoutesCount = 0; ITOP_RoutesCount < ITOP.ITOP_NumberOfRoute; ITOP_RoutesCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail Route_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int ITOP_PointsCount = 0; ITOP_PointsCount < ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Route_NumOfPoint; ITOP_PointsCount++)
                    {
                        ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_RouteOBJ, "Routes " + ITOP_PointsCount + " " + ITOP_RoutesCount);

                        HTK_3DES.Transform Route_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position,
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_RouteOBJ, Route_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //AddMDL
                        Route_Rail.MV3D_List.Add(dv3D_RouteOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_RouteOBJ);
                    }

                    kMPViewportObject.Routes_List.Add(Route_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.Routes_List.Count; i++)
                {
                    kMPViewportObject.Routes_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.Blue);
                }
                #endregion
            }

            public static void Render_Area(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, AERA AERA)
            {
                for (int AERACount = 0; AERACount < AERA.NumOfEntries; AERACount++)
                {
                    ModelVisual3D dv3D_AreaOBJ = null;
                    if (AERA.AERAValue_List[AERACount].AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (AERA.AERAValue_List[AERACount].AreaModeValue == 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomCylinderVisual3D(Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (AERA.AERAValue_List[AERACount].AreaModeValue > 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_AreaOBJ, "Area " + AERACount + " " + -1);

                    HTK_3DES.Transform Area_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = AERA.AERAValue_List[AERACount].AERA_Position,
                        Scale3D = HTK_3DES.ScaleFactor(AERA.AERAValue_List[AERACount].AERA_Scale, 2000),
                        Rotate3D = AERA.AERAValue_List[AERACount].AERA_Rotation
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //Area_MV3D_List.Add(dv3D_AreaOBJ);
                    kMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_AreaOBJ);
                }
            }

            public static void Render_Camera(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, EMAC EMAC)
            {
                for (int EMACCount = 0; EMACCount < EMAC.NumOfEntries; EMACCount++)
                {
                    ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_CameraOBJ, "Camera " + EMACCount + " " + -1);

                    HTK_3DES.Transform Camera_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = EMAC.EMACValue_List[EMACCount].EMAC_Position,
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = EMAC.EMACValue_List[EMACCount].EMAC_Rotation
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_CameraOBJ, Camera_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                    kMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CameraOBJ);
                }
            }

            public static void Render_Returnpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TPGJ TPGJ)
            {
                for (int TPGJCount = 0; TPGJCount < TPGJ.NumOfEntries; TPGJCount++)
                {
                    ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + TPGJCount + " " + -1);

                    HTK_3DES.Transform GliderPoint_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Position,
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Rotation
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_RespawnPointOBJ, GliderPoint_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //RespawnPoint_MV3D_List.Add(dv3D_RespawnPointOBJ);
                    kMPViewportObject.RespawnPoint_MV3DList.Add(dv3D_RespawnPointOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_RespawnPointOBJ);
                }
            }

            public static void Render_GlideRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, HPLG HPLG, TPLG TPLG)
            {
                for (int i = 0; i < HPLG.NumOfEntries; i++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int Count = 0; Count < HPLG.HPLGValue_List[i].HPLG_Length; Count++)
                    {
                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + Count + " " + i);

                        HTK_3DES.Transform GliderPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_Position,
                            Scale3D = HTK_3DES.ScaleFactor(TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_PointScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add model
                        GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    }

                    kMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.GlideRoute_Rail_List.Count; i++)
                {
                    kMPViewportObject.GlideRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.LightSkyBlue);
                }
                #endregion
            }
        }

        public class KMPViewportRenderingXML
        {
            public static void Render_StartPosition(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.StartPosition startPosition)
            {
                for (int ValueCount = 0; ValueCount < startPosition.StartPositionValues.Count; ValueCount++)
                {
                    ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + ValueCount + " " + -1);

                    HTK_3DES.Transform StartPosition_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = startPosition.StartPositionValues[ValueCount].Position.ToVector3D(),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = startPosition.StartPositionValues[ValueCount].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_StartPositionOBJ, StartPosition_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    kMPViewportObject.StartPosition_MV3DList.Add(dv3D_StartPositionOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_StartPositionOBJ);

                    HTK_3DES.GC_Dispose(dv3D_StartPositionOBJ);
                }
            }

            public static void Render_EnemyRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.EnemyRoute enemyRoute)
            {
                for (int GroupCount = 0; GroupCount < enemyRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < enemyRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        #region Add Model(EnemyRoutes)
                        ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + PointCount + " " + GroupCount);

                        HTK_3DES.Transform EnemyPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = enemyRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(enemyRoute.Groups[GroupCount].Points[PointCount].Control, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add Rail => MV3DList
                        KMP_EnemyRoute_Rail.MV3D_List.Add(dv3D_EnemyPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_EnemyPathOBJ);
                        #endregion
                    }

                    //Add point
                    kMPViewportObject.EnemyRoute_Rail_List.Add(KMP_EnemyRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.EnemyRoute_Rail_List.Count; i++)
                {
                    kMPViewportObject.EnemyRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.Orange);
                }
                #endregion
            }

            public static void Render_ItemRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.ItemRoute itemRoute)
            {
                for (int GroupCount = 0; GroupCount < itemRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < itemRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + PointCount + " " + GroupCount);

                        HTK_3DES.Transform ItemPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = itemRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(itemRoute.Groups[GroupCount].Points[PointCount].PointSize, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add Rail => MV3DList
                        KMP_ItemRoute_Rail.MV3D_List.Add(dv3D_ItemPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_ItemPathOBJ);
                    }

                    //Add point
                    kMPViewportObject.ItemRoute_Rail_List.Add(KMP_ItemRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.ItemRoute_Rail_List.Count; i++)
                {
                    kMPViewportObject.ItemRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.Green);
                }
                #endregion
            }

            public static void Render_Checkpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.Checkpoint Checkpoint, double CheckpointYOffset)
            {
                for (int GroupCount = 0; GroupCount < Checkpoint.Groups.Count; GroupCount++)
                {
                    //Checkpoint_Rails
                    HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = new HTK_3DES.KMP_3DCheckpointSystem.Checkpoint();

                    for (int PointCount = 0; PointCount < Checkpoint.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        #region Create
                        var P2D_Left = Checkpoint.Groups[GroupCount].Points[PointCount].Position_2D_Left;
                        Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                        Point3D P3DLeft = Converter2D.Vector2DTo3D(P2DLeftToVector2, Converter2D.Axis_Up.Y).ToPoint3D();
                        P3DLeft.Y = CheckpointYOffset;

                        #region Transform(Left)
                        ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + PointCount + " " + GroupCount);

                        HTK_3DES.Transform P2DLeft_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = P3DLeft.ToVector3D(),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D_P2DLeft = new HTK_3DES.TSRSystem3D(dv3D_CheckpointLeftOBJ, P2DLeft_transform_Value);
                        tSRSystem3D_P2DLeft.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        checkpoint.Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                        HTK_3DES.GC_Dispose(dv3D_CheckpointLeftOBJ);
                        #endregion

                        var P2D_Right = Checkpoint.Groups[GroupCount].Points[PointCount].Position_2D_Right;
                        Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                        Point3D P3DRight = Converter2D.Vector2DTo3D(P2DRightToVector2, Converter2D.Axis_Up.Y).ToPoint3D();
                        P3DRight.Y = CheckpointYOffset;

                        #region Transform(Right)
                        ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + PointCount + " " + GroupCount);

                        HTK_3DES.Transform P2DRight_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = P3DRight.ToVector3D(),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D_P2DRight = new HTK_3DES.TSRSystem3D(dv3D_CheckpointRightOBJ, P2DRight_transform_Value);
                        tSRSystem3D_P2DRight.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        checkpoint.Checkpoint_Right.MV3D_List.Add(dv3D_CheckpointRightOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointRightOBJ);

                        HTK_3DES.GC_Dispose(dv3D_CheckpointRightOBJ);
                        #endregion

                        List<Point3D> point3Ds = new List<Point3D>();
                        point3Ds.Add(P3DLeft);
                        point3Ds.Add(P3DRight);

                        #region SplitLine
                        LinesVisual3D linesVisual3D = new LinesVisual3D
                        {
                            Points = new Point3DCollection(point3Ds),
                            Thickness = 1,
                            Color = Colors.Black
                        };

                        checkpoint.Checkpoint_Line.Add(linesVisual3D);
                        UserCtrl.MainViewPort.Children.Add(linesVisual3D);
                        #endregion

                        #region SplitWall
                        Point3DCollection point3Ds1 = new Point3DCollection();
                        point3Ds1.Add(new Point3D(point3Ds[1].X, 0, point3Ds[1].Z));
                        point3Ds1.Add(point3Ds[1]);
                        point3Ds1.Add(new Point3D(point3Ds[0].X, 0, point3Ds[0].Z));
                        point3Ds1.Add(point3Ds[0]);

                        ModelVisual3D SplitWall = HTK_3DES.CustomModelCreateHelper.CustomRectanglePlane3D(point3Ds1, System.Windows.Media.Color.FromArgb(0xA0, 0xA0, 0x00, 0xA0), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                        HTK_3DES.SetString_MV3D(SplitWall, "SplitWall -1 -1");
                        checkpoint.Checkpoint_SplitWallMDL.Add(SplitWall);
                        UserCtrl.MainViewPort.Children.Add(SplitWall);
                        #endregion
                        #endregion
                    }

                    //Add Checkpoint
                    kMPViewportObject.Checkpoint_Rail.Add(checkpoint);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.Checkpoint_Rail.Count; i++)
                {
                    kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.DrawPath_Line(UserCtrl, 5, Colors.Green);
                    kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.DrawPath_Line(UserCtrl, 5, Colors.Red);
                    kMPViewportObject.Checkpoint_Rail[i].DrawPath_SideWall(UserCtrl, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                }
                #endregion
            }

            public static void Render_Object(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.Object Object, List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> objFlowDataXml)
            {
                for (int ValueCount = 0; ValueCount < Object.Object_Values.Count; ValueCount++)
                {
                    string ObjectPath = objFlowDataXml.Find(x => x.ObjectID == Object.Object_Values[ValueCount].ObjectID).Path;
                    ModelVisual3D dv3D_OBJ = HTK_3DES.OBJReader(ObjectPath);

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_OBJ, "OBJ " + ValueCount + " " + -1);

                    HTK_3DES.Transform OBJ_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = Object.Object_Values[ValueCount].Position.ToVector3D(),
                        Scale3D = HTK_3DES.ScaleFactor(Object.Object_Values[ValueCount].Scale.ToVector3D(), 2),
                        Rotate3D = Object.Object_Values[ValueCount].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    kMPViewportObject.OBJ_MV3DList.Add(dv3D_OBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_OBJ);
                }
            }

            public static void Render_Route(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.Route route)
            {
                for (int GroupCount = 0; GroupCount < route.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail Route_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointsCount = 0; PointsCount < route.Groups[GroupCount].Points.Count; PointsCount++)
                    {
                        ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_RouteOBJ, "Routes " + PointsCount + " " + GroupCount);

                        HTK_3DES.Transform Route_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = route.Groups[GroupCount].Points[PointsCount].Position.ToVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_RouteOBJ, Route_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //AddMDL
                        Route_Rail.MV3D_List.Add(dv3D_RouteOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_RouteOBJ);
                    }

                    kMPViewportObject.Routes_List.Add(Route_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.Routes_List.Count; i++)
                {
                    kMPViewportObject.Routes_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.Blue);
                }
                #endregion
            }

            public static void Render_Area(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.Area area)
            {
                for (int ValueCount = 0; ValueCount < area.Area_Values.Count; ValueCount++)
                {
                    ModelVisual3D dv3D_AreaOBJ = null;
                    if (area.Area_Values[ValueCount].AreaMode == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (area.Area_Values[ValueCount].AreaMode == 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomCylinderVisual3D(Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (area.Area_Values[ValueCount].AreaMode > 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_AreaOBJ, "Area " + ValueCount + " " + -1);

                    HTK_3DES.Transform Area_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = area.Area_Values[ValueCount].Position.ToVector3D(),
                        Scale3D = HTK_3DES.ScaleFactor(area.Area_Values[ValueCount].Scale.ToVector3D(), 2000),
                        Rotate3D = area.Area_Values[ValueCount].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //Area_MV3D_List.Add(dv3D_AreaOBJ);
                    kMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_AreaOBJ);
                }
            }

            public static void Render_Camera(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.Camera camera)
            {
                for (int ValueCount = 0; ValueCount < camera.Values.Count; ValueCount++)
                {
                    ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_CameraOBJ, "Camera " + ValueCount + " " + -1);

                    HTK_3DES.Transform Camera_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = camera.Values[ValueCount].Position.ToVector3D(),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = camera.Values[ValueCount].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_CameraOBJ, Camera_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                    kMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CameraOBJ);
                }
            }

            public static void Render_Returnpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.JugemPoint jugemPoint)
            {
                for (int ValueCount = 0; ValueCount < jugemPoint.Values.Count; ValueCount++)
                {
                    ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + ValueCount + " " + -1);

                    HTK_3DES.Transform RespawnPoint_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = jugemPoint.Values[ValueCount].Position.ToVector3D(),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = jugemPoint.Values[ValueCount].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_RespawnPointOBJ, RespawnPoint_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //RespawnPoint_MV3D_List.Add(dv3D_RespawnPointOBJ);
                    kMPViewportObject.RespawnPoint_MV3DList.Add(dv3D_RespawnPointOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_RespawnPointOBJ);
                }
            }

            public static void Render_GlideRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.KMPData.SectionData.GlideRoute glideRoute)
            {
                for (int GroupCount = 0; GroupCount < glideRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < glideRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + PointCount + " " + GroupCount);

                        HTK_3DES.Transform GliderPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = glideRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(glideRoute.Groups[GroupCount].Points[PointCount].PointScale, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add model
                        GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    }

                    kMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.GlideRoute_Rail_List.Count; i++)
                {
                    kMPViewportObject.GlideRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.LightSkyBlue);
                }
                #endregion
            }
        }

        public class KMPViewportRenderingXML_XXXXRoute
        {
            public static void Render_EnemyRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute xXXXRoute)
            {
                for (int i = 0; i < xXXXRoute.Groups.Count; i++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int Count = 0; Count < xXXXRoute.Groups[i].Points.Count; Count++)
                    {
                        #region Add Model(EnemyRoutes)
                        ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + Count + " " + i);

                        HTK_3DES.Transform EnemyPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = xXXXRoute.Groups[i].Points[Count].Position.ToVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(xXXXRoute.Groups[i].Points[Count].ScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add Rail => MV3DList
                        KMP_EnemyRoute_Rail.MV3D_List.Add(dv3D_EnemyPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_EnemyPathOBJ);
                        #endregion
                    }

                    //Add point
                    kMPViewportObject.EnemyRoute_Rail_List.Add(KMP_EnemyRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.EnemyRoute_Rail_List.Count; i++)
                {
                    kMPViewportObject.EnemyRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.Orange);
                }
                #endregion
            }

            public static void Render_ItemRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute xXXXRoute)
            {
                for (int GroupCount = 0; GroupCount < xXXXRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < xXXXRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + PointCount + " " + GroupCount);

                        HTK_3DES.Transform ItemPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add Rail => MV3DList
                        KMP_ItemRoute_Rail.MV3D_List.Add(dv3D_ItemPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_ItemPathOBJ);
                    }

                    //Add point
                    kMPViewportObject.ItemRoute_Rail_List.Add(KMP_ItemRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.ItemRoute_Rail_List.Count; i++)
                {
                    kMPViewportObject.ItemRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.Green);
                }
                #endregion
            }

            public static void Render_GlideRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPLibrary.XMLConvert.XXXXRouteData.XXXXRoute_XML.XXXXRoute xXXXRoute)
            {
                for (int GroupCount = 0; GroupCount < xXXXRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < xXXXRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + PointCount + " " + GroupCount);

                        HTK_3DES.Transform GliderPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //Add model
                        GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    }

                    kMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.GlideRoute_Rail_List.Count; i++)
                {
                    kMPViewportObject.GlideRoute_Rail_List[i].DrawPath_Tube(UserCtrl, 10.0, Colors.LightSkyBlue);
                }
                #endregion
            }
        }
    }
}
