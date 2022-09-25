using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.IO;
using System.Data;
using System.Collections;
using System.Xml.Serialization;
using HelixToolkit.Wpf;
using System.Windows.Media;

namespace MK7_KMP_Editor_For_PG_
{
    public class KMPs
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
            public static void Render_StartPosition(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.TPTK_Section TPTK)
            {
                for (int i = 0; i < TPTK.NumOfEntries; i++)
                {
                    #region Add Model(StartPosition)
                    ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + i + " " + -1);

                    HTK_3DES.TSRSystem.Transform StartPosition_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = TPTK.TPTKValue_List[i].TPTK_Position,
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = TPTK.TPTKValue_List[i].TPTK_Rotation
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_StartPositionOBJ, StartPosition_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    kMPViewportObject.StartPosition_MV3DList.Add(dv3D_StartPositionOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_StartPositionOBJ);

                    HTK_3DES.TSRSystem.GC_Dispose(dv3D_StartPositionOBJ);
                    #endregion
                }
            }

            public static void Render_EnemyRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.HPNE_Section HPNE, KMPs.KMPFormat.KMPSection.TPNE_Section TPNE)
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
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + Count + " " + i);

                        HTK_3DES.TSRSystem.Transform EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].TPNE_Position,
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Control, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

            public static void Render_ItemRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.HPTI_Section HPTI, KMPs.KMPFormat.KMPSection.TPTI_Section TPTI)
            {
                for (int HPTICount = 0; HPTICount < HPTI.NumOfEntries; HPTICount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int Count = 0; Count < HPTI.HPTIValue_List[HPTICount].HPTI_Length; Count++)
                    {
                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + Count + " " + HPTICount);

                        HTK_3DES.TSRSystem.Transform ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_Position,
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_PointSize, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

            public static void Render_Checkpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.HPKC_Section HPKC, KMPs.KMPFormat.KMPSection.TPKC_Section TPKC, double CheckpointYOffset)
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
                        Point3D P3DLeft = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DLeftToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                        P3DLeft.Y = CheckpointYOffset;

                        #region Transform(Left)
                        ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + Count + " " + HPKCCount);

                        HTK_3DES.TSRSystem.Transform P2DLeft_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = P3DLeft.ToVector3D(),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_P2DLeft = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CheckpointLeftOBJ, P2DLeft_transform_Value);
                        tSRSystem3D_P2DLeft.TestTransform3D();

                        checkpoint.Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                        HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointLeftOBJ);
                        #endregion

                        var P2D_Right = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Right;
                        Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                        Point3D P3DRight = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DRightToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                        P3DRight.Y = CheckpointYOffset;

                        #region Transform(Right)
                        ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + Count + " " + HPKCCount);

                        HTK_3DES.TSRSystem.Transform P2DRight_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = P3DRight.ToVector3D(),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_P2DRight = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CheckpointRightOBJ, P2DRight_transform_Value);
                        tSRSystem3D_P2DRight.TestTransform3D();

                        checkpoint.Checkpoint_Right.MV3D_List.Add(dv3D_CheckpointRightOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointRightOBJ);

                        HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointRightOBJ);
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
                        HTK_3DES.TSRSystem.SetString_MV3D(SplitWall, "SplitWall -1 -1");
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

            public static void Render_Object(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.JBOG_Section JBOG, List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> objFlowDataXml)
            {
                for (int Count = 0; Count < JBOG.NumOfEntries; Count++)
                {
                    string Path = objFlowDataXml.Find(x => x.ObjectID == BitConverter.ToString(JBOG.JBOGValue_List[Count].ObjectID.Reverse().ToArray()).Replace("-", string.Empty)).Path;
                    ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(Path);

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + Count + " " + -1);

                    HTK_3DES.TSRSystem.Transform OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = JBOG.JBOGValue_List[Count].JBOG_Position,
                        Scale3D = HTK_3DES.TSRSystem.ScaleFactor(JBOG.JBOGValue_List[Count].JBOG_Scale, 2),
                        Rotate3D = JBOG.JBOGValue_List[Count].JBOG_Rotation
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    kMPViewportObject.OBJ_MV3DList.Add(dv3D_OBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_OBJ);
                }
            }

            public static void Render_Route(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.ITOP_Section ITOP)
            {
                for (int ITOP_RoutesCount = 0; ITOP_RoutesCount < ITOP.ITOP_NumberOfRoute; ITOP_RoutesCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail Route_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int ITOP_PointsCount = 0; ITOP_PointsCount < ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Route_NumOfPoint; ITOP_PointsCount++)
                    {
                        ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RouteOBJ, "Routes " + ITOP_PointsCount + " " + ITOP_RoutesCount);

                        HTK_3DES.TSRSystem.Transform Route_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position,
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_RouteOBJ, Route_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

            public static void Render_Area(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.AERA_Section AERA)
            {
                for (int AERACount = 0; AERACount < AERA.NumOfEntries; AERACount++)
                {
                    ModelVisual3D dv3D_AreaOBJ = null;
                    if (AERA.AERAValue_List[AERACount].AreaMode == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (AERA.AERAValue_List[AERACount].AreaMode == 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomCylinderVisual3D(Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (AERA.AERAValue_List[AERACount].AreaMode > 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + AERACount + " " + -1);

                    HTK_3DES.TSRSystem.Transform Area_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = AERA.AERAValue_List[AERACount].AERA_Position,
                        Scale3D = HTK_3DES.TSRSystem.ScaleFactor(AERA.AERAValue_List[AERACount].AERA_Scale, 2000),
                        Rotate3D = AERA.AERAValue_List[AERACount].AERA_Rotation
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    //Area_MV3D_List.Add(dv3D_AreaOBJ);
                    kMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_AreaOBJ);
                }
            }

            public static void Render_Camera(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.EMAC_Section EMAC)
            {
                for (int EMACCount = 0; EMACCount < EMAC.NumOfEntries; EMACCount++)
                {
                    ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CameraOBJ, "Camera " + EMACCount + " " + -1);

                    HTK_3DES.TSRSystem.Transform Camera_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = EMAC.EMACValue_List[EMACCount].EMAC_Position,
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = EMAC.EMACValue_List[EMACCount].EMAC_Rotation
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CameraOBJ, Camera_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                    kMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CameraOBJ);
                }
            }

            public static void Render_Returnpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.TPGJ_Section TPGJ)
            {
                for (int TPGJCount = 0; TPGJCount < TPGJ.NumOfEntries; TPGJCount++)
                {
                    ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + TPGJCount + " " + -1);

                    HTK_3DES.TSRSystem.Transform GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Position,
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Rotation
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_RespawnPointOBJ, GliderPoint_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    //RespawnPoint_MV3D_List.Add(dv3D_RespawnPointOBJ);
                    kMPViewportObject.RespawnPoint_MV3DList.Add(dv3D_RespawnPointOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_RespawnPointOBJ);
                }
            }

            public static void Render_GlideRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.HPLG_Section HPLG, KMPs.KMPFormat.KMPSection.TPLG_Section TPLG)
            {
                for (int i = 0; i < HPLG.NumOfEntries; i++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int Count = 0; Count < HPLG.HPLGValue_List[i].HPLG_Length; Count++)
                    {
                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + Count + " " + i);

                        HTK_3DES.TSRSystem.Transform GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_Position,
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_PointScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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
            public static void Render_StartPosition(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.StartPosition startPosition)
            {
                for (int i = 0; i < startPosition.startPosition_Value.Count; i++)
                {
                    ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + i + " " + -1);

                    HTK_3DES.TSRSystem.Transform StartPosition_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = startPosition.startPosition_Value[i].Position.ToVector3D(),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = startPosition.startPosition_Value[i].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_StartPositionOBJ, StartPosition_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    kMPViewportObject.StartPosition_MV3DList.Add(dv3D_StartPositionOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_StartPositionOBJ);

                    HTK_3DES.TSRSystem.GC_Dispose(dv3D_StartPositionOBJ);
                }
            }

            public static void Render_EnemyRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.EnemyRoute enemyRoute)
            {
                for (int i = 0; i < enemyRoute.Groups.Count; i++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int Count = 0; Count < enemyRoute.Groups[i].Points.Count; Count++)
                    {
                        #region Add Model(EnemyRoutes)
                        ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + Count + " " + i);

                        HTK_3DES.TSRSystem.Transform EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = enemyRoute.Groups[i].Points[Count].Position.ToVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = enemyRoute.Groups[i].Points[Count].Position.ToVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

            public static void Render_ItemRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.ItemRoute itemRoute)
            {
                for (int GroupCount = 0; GroupCount < itemRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < itemRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.Transform ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = itemRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(itemRoute.Groups[GroupCount].Points[PointCount].PointSize, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

            public static void Render_Checkpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Checkpoint Checkpoint, double CheckpointYOffset)
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
                        Point3D P3DLeft = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DLeftToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                        P3DLeft.Y = CheckpointYOffset;

                        #region Transform(Left)
                        ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.Transform P2DLeft_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = P3DLeft.ToVector3D(),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_P2DLeft = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CheckpointLeftOBJ, P2DLeft_transform_Value);
                        tSRSystem3D_P2DLeft.TestTransform3D();

                        checkpoint.Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                        HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointLeftOBJ);
                        #endregion

                        var P2D_Right = Checkpoint.Groups[GroupCount].Points[PointCount].Position_2D_Right;
                        Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                        Point3D P3DRight = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DRightToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                        P3DRight.Y = CheckpointYOffset;

                        #region Transform(Right)
                        ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.Transform P2DRight_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = P3DRight.ToVector3D(),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_P2DRight = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CheckpointRightOBJ, P2DRight_transform_Value);
                        tSRSystem3D_P2DRight.TestTransform3D();

                        checkpoint.Checkpoint_Right.MV3D_List.Add(dv3D_CheckpointRightOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointRightOBJ);

                        HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointRightOBJ);
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
                        HTK_3DES.TSRSystem.SetString_MV3D(SplitWall, "SplitWall -1 -1");
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

            public static void Render_Object(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Object Object, List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> objFlowDataXml)
            {
                for (int Count = 0; Count < Object.Object_Values.Count; Count++)
                {
                    string ObjectPath = objFlowDataXml.Find(x => x.ObjectID == Object.Object_Values[Count].ObjectID).Path;
                    ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(ObjectPath);

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + Count + " " + -1);

                    HTK_3DES.TSRSystem.Transform OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = Object.Object_Values[Count].Position.ToVector3D(),
                        Scale3D = HTK_3DES.TSRSystem.ScaleFactor(Object.Object_Values[Count].Scale.ToVector3D(), 2),
                        Rotate3D = Object.Object_Values[Count].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    kMPViewportObject.OBJ_MV3DList.Add(dv3D_OBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_OBJ);
                }
            }

            public static void Render_Route(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Route route)
            {
                for (int GroupCount = 0; GroupCount < route.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail Route_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointsCount = 0; PointsCount < route.Groups[GroupCount].Points.Count; PointsCount++)
                    {
                        ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RouteOBJ, "Routes " + PointsCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.Transform Route_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = route.Groups[GroupCount].Points[PointsCount].Position.ToVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_RouteOBJ, Route_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

            public static void Render_Area(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Area area)
            {
                for (int Count = 0; Count < area.Area_Values.Count; Count++)
                {
                    ModelVisual3D dv3D_AreaOBJ = null;
                    if (area.Area_Values[Count].AreaMode == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (area.Area_Values[Count].AreaMode == 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomCylinderVisual3D(Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (area.Area_Values[Count].AreaMode > 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + Count + " " + -1);

                    HTK_3DES.TSRSystem.Transform Area_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = area.Area_Values[Count].Position.ToVector3D(),
                        Scale3D = HTK_3DES.TSRSystem.ScaleFactor(area.Area_Values[Count].Scale.ToVector3D(), 2000),
                        Rotate3D = area.Area_Values[Count].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    //Area_MV3D_List.Add(dv3D_AreaOBJ);
                    kMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_AreaOBJ);
                }
            }

            public static void Render_Camera(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Camera camera)
            {
                for (int Count = 0; Count < camera.Values.Count; Count++)
                {
                    ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CameraOBJ, "Camera " + Count + " " + -1);

                    HTK_3DES.TSRSystem.Transform Camera_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = camera.Values[Count].Position.ToVector3D(),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = camera.Values[Count].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CameraOBJ, Camera_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                    kMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CameraOBJ);
                }
            }

            public static void Render_Returnpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.JugemPoint jugemPoint)
            {
                for (int Count = 0; Count < jugemPoint.Values.Count; Count++)
                {
                    ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + Count + " " + -1);

                    HTK_3DES.TSRSystem.Transform RespawnPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = jugemPoint.Values[Count].Position.ToVector3D(),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = jugemPoint.Values[Count].Rotation.ToVector3D()
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_RespawnPointOBJ, RespawnPoint_transform_Value);
                    tSRSystem3D.TestTransform3D();

                    //RespawnPoint_MV3D_List.Add(dv3D_RespawnPointOBJ);
                    kMPViewportObject.RespawnPoint_MV3DList.Add(dv3D_RespawnPointOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_RespawnPointOBJ);
                }
            }

            public static void Render_GlideRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.GlideRoute glideRoute)
            {
                for (int GroupCount = 0; GroupCount < glideRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < glideRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.Transform GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = glideRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(glideRoute.Groups[GroupCount].Points[PointCount].PointScale, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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
            public static void Render_EnemyRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
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
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + Count + " " + i);

                        HTK_3DES.TSRSystem.Transform EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = xXXXRoute.Groups[i].Points[Count].Position.ToVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(xXXXRoute.Groups[i].Points[Count].ScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

            public static void Render_ItemRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
            {
                for (int GroupCount = 0; GroupCount < xXXXRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < xXXXRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.Transform ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

            public static void Render_GlideRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.XXXXRouteXml.XXXXRoute xXXXRoute)
            {
                for (int GroupCount = 0; GroupCount < xXXXRoute.Groups.Count; GroupCount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int PointCount = 0; PointCount < xXXXRoute.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.Transform GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.ToVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

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

        public class KMPFormat
        {
            public char[] DMDCHeader { get; set; } //0x4
            public uint FileSize { get; set; } //0x4
            public ushort SectionCount { get; set; } //0x2
            public ushort DMDCHeaderSize { get; set; } //0x2
            public uint VersionNumber { get; set; } //0x4
            public KMPSection KMP_Section { get; set; }
            public class KMPSection
            {
                public uint TPTK_Offset { get; set; }
                public uint TPNE_Offset { get; set; }
                public uint HPNE_Offset { get; set; }
                public uint TPTI_Offset { get; set; }
                public uint HPTI_Offset { get; set; }
                public uint TPKC_Offset { get; set; }
                public uint HPKC_Offset { get; set; }
                public uint JBOG_Offset { get; set; }
                public uint ITOP_Offset { get; set; }
                public uint AERA_Offset { get; set; }
                public uint EMAC_Offset { get; set; }
                public uint TPGJ_Offset { get; set; }
                public uint TPNC_Offset { get; set; }
                public uint TPSM_Offset { get; set; }
                public uint IGTS_Offset { get; set; }
                public uint SROC_Offset { get; set; }
                public uint TPLG_Offset { get; set; }
                public uint HPLG_Offset { get; set; }

                public TPTK_Section TPTK { get; set; }
                public class TPTK_Section
                {
                    public char[] TPTKHeader { get; set; } //0x4
                    public ushort NumOfEntries { get; set; } //0x2
                    public ushort AdditionalValue { get; set; } //0x2
                    public List<TPTKValue> TPTKValue_List { get; set; }
                    public class TPTKValue
                    {
                        public Vector3D TPTK_Position { get; set; }
                        public Vector3D TPTK_Rotation { get; set; }
                        public ushort Player_Index { get; set; } //0x2
                        public ushort TPTK_UnkBytes { get; set; } //0x2

                        public void ReadTPTKValue(BinaryReader br)
                        {
                            TPTK_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            TPTK_Rotation = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            Player_Index = br.ReadUInt16();
                            TPTK_UnkBytes = br.ReadUInt16();
                        }

                        public void WriteTPTKValue(BinaryWriter bw)
                        {
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK_Position)[2]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK_Position)[2]);
                            bw.Write(Player_Index);
                            bw.Write(TPTK_UnkBytes);
                        }

                        public TPTKValue()
                        {
                            TPTK_Position = new Vector3D(0, 0, 0);
                            TPTK_Rotation = new Vector3D(0, 0, 0);
                            Player_Index = 0;
                            TPTK_UnkBytes = 0;
                        }
                    }

                    public void ReadTPTK(BinaryReader br)
                    {
                        TPTKHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int TPTKCount = 0; TPTKCount < NumOfEntries; TPTKCount++)
                        {
                            TPTKValue TPTK_Value = new TPTKValue();
                            TPTK_Value.ReadTPTKValue(br);
                            TPTKValue_List.Add(TPTK_Value);
                        }
                    }

                    public void WriteTPTK(BinaryWriter bw)
                    {
                        bw.Write(TPTKHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int TPTKCount = 0; TPTKCount < NumOfEntries; TPTKCount++) TPTKValue_List[TPTKCount].WriteTPTKValue(bw);
                    }

                    public TPTK_Section()
                    {
                        TPTKHeader = "TPTK".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        TPTKValue_List = new List<TPTKValue>();
                    }
                }

                public TPNE_Section TPNE { get; set; }
                public class TPNE_Section
                {
                    public char[] TPNEHeader { get; set; } //0x4
                    public ushort NumOfEntries { get; set; } //0x2
                    public ushort AdditionalValue { get; set; } //0x2
                    public List<TPNEValue> TPNEValue_List { get; set; }
                    public class TPNEValue
                    {
                        public Vector3D TPNE_Position { get; set; }
                        public float Control { get; set; }
                        public ushort MushSetting { get; set; }
                        public byte DriftSetting { get; set; }
                        public byte Flags { get; set; }
                        public short PathFindOption { get; set; }
                        public short MaxSearchYOffset { get; set; }

                        public void ReadTPNEValue(BinaryReader br)
                        {
                            TPNE_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            Control = br.ReadSingle();
                            MushSetting = br.ReadUInt16();
                            DriftSetting = br.ReadByte();
                            Flags = br.ReadByte();
                            PathFindOption = br.ReadInt16();
                            MaxSearchYOffset = br.ReadInt16();
                        }

                        public void WriteTPNEValue(BinaryWriter bw)
                        {
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE_Position)[2]);
                            bw.Write(Control);
                            bw.Write(MushSetting);
                            bw.Write(DriftSetting);
                            bw.Write(Flags);
                            bw.Write(PathFindOption);
                            bw.Write(MaxSearchYOffset);
                        }

                        public TPNEValue()
                        {
                            TPNE_Position = new Vector3D(0, 0, 0);
                            Control = 0f;
                            MushSetting = 0;
                            DriftSetting = 0x00;
                            Flags = 0x00;
                            PathFindOption = 0;
                            MaxSearchYOffset = 0;
                        } 
                    }

                    public void ReadTPNE(BinaryReader br)
                    {
                        TPNEHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int TPNECount = 0; TPNECount < NumOfEntries; TPNECount++)
                        {
                            TPNEValue TPNE_Value = new TPNEValue();
                            TPNE_Value.ReadTPNEValue(br);
                            TPNEValue_List.Add(TPNE_Value);
                        }
                    }

                    public void WriteTPNE(BinaryWriter bw)
                    {
                        bw.Write(TPNEHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int Count = 0; Count < TPNEValue_List.Count; Count++)
                        {
                            TPNEValue_List[Count].WriteTPNEValue(bw);
                        }
                    }

                    public TPNE_Section()
                    {
                        TPNEHeader = "TPNE".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        TPNEValue_List = new List<TPNEValue>();
                    }
                }

                public HPNE_Section HPNE { get; set; }
                public class HPNE_Section
                {
                    public char[] HPNEHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<HPNEValue> HPNEValue_List { get; set; }
                    public class HPNEValue
                    {
                        public ushort HPNE_StartPoint { get; set; }
                        public ushort HPNE_Length { get; set; }

                        public HPNE_PreviewGroups HPNE_PreviewGroup { get; set; }
                        public class HPNE_PreviewGroups
                        {
                            public ushort Prev0 { get; set; }
                            public ushort Prev1 { get; set; }
                            public ushort Prev2 { get; set; }
                            public ushort Prev3 { get; set; }
                            public ushort Prev4 { get; set; }
                            public ushort Prev5 { get; set; }
                            public ushort Prev6 { get; set; }
                            public ushort Prev7 { get; set; }
                            public ushort Prev8 { get; set; }
                            public ushort Prev9 { get; set; }
                            public ushort Prev10 { get; set; }
                            public ushort Prev11 { get; set; }
                            public ushort Prev12 { get; set; }
                            public ushort Prev13 { get; set; }
                            public ushort Prev14 { get; set; }
                            public ushort Prev15 { get; set; }

                            public void ReadHPNEPrevValue(BinaryReader br)
                            {
                                Prev0 = br.ReadUInt16();
                                Prev1 = br.ReadUInt16();
                                Prev2 = br.ReadUInt16();
                                Prev3 = br.ReadUInt16();
                                Prev4 = br.ReadUInt16();
                                Prev5 = br.ReadUInt16();
                                Prev6 = br.ReadUInt16();
                                Prev7 = br.ReadUInt16();
                                Prev8 = br.ReadUInt16();
                                Prev9 = br.ReadUInt16();
                                Prev10 = br.ReadUInt16();
                                Prev11 = br.ReadUInt16();
                                Prev12 = br.ReadUInt16();
                                Prev13 = br.ReadUInt16();
                                Prev14 = br.ReadUInt16();
                                Prev15 = br.ReadUInt16();
                            }

                            public void WritePrevValue(BinaryWriter bw)
                            {
                                bw.Write(Prev0);
                                bw.Write(Prev1);
                                bw.Write(Prev2);
                                bw.Write(Prev3);
                                bw.Write(Prev4);
                                bw.Write(Prev5);
                                bw.Write(Prev6);
                                bw.Write(Prev7);
                                bw.Write(Prev8);
                                bw.Write(Prev9);
                                bw.Write(Prev10);
                                bw.Write(Prev11);
                                bw.Write(Prev12);
                                bw.Write(Prev13);
                                bw.Write(Prev14);
                                bw.Write(Prev15);
                            }

                            public HPNE_PreviewGroups()
                            {
                                Prev0 = 255;
                                Prev1 = 255;
                                Prev2 = 255;
                                Prev3 = 255;
                                Prev4 = 255;
                                Prev5 = 255;
                                Prev6 = 255;
                                Prev7 = 255;
                                Prev8 = 255;
                                Prev9 = 255;
                                Prev10 = 255;
                                Prev11 = 255;
                                Prev12 = 255;
                                Prev13 = 255;
                                Prev14 = 255;
                                Prev15 = 255;
                            }
                        }

                        public HPNE_NextGroups HPNE_NextGroup { get; set; }
                        public class HPNE_NextGroups
                        {
                            public ushort Next0 { get; set; }
                            public ushort Next1 { get; set; }
                            public ushort Next2 { get; set; }
                            public ushort Next3 { get; set; }
                            public ushort Next4 { get; set; }
                            public ushort Next5 { get; set; }
                            public ushort Next6 { get; set; }
                            public ushort Next7 { get; set; }
                            public ushort Next8 { get; set; }
                            public ushort Next9 { get; set; }
                            public ushort Next10 { get; set; }
                            public ushort Next11 { get; set; }
                            public ushort Next12 { get; set; }
                            public ushort Next13 { get; set; }
                            public ushort Next14 { get; set; }
                            public ushort Next15 { get; set; }

                            public void ReadNextValue(BinaryReader br)
                            {
                                Next0 = br.ReadUInt16();
                                Next1 = br.ReadUInt16();
                                Next2 = br.ReadUInt16();
                                Next3 = br.ReadUInt16();
                                Next4 = br.ReadUInt16();
                                Next5 = br.ReadUInt16();
                                Next6 = br.ReadUInt16();
                                Next7 = br.ReadUInt16();
                                Next8 = br.ReadUInt16();
                                Next9 = br.ReadUInt16();
                                Next10 = br.ReadUInt16();
                                Next11 = br.ReadUInt16();
                                Next12 = br.ReadUInt16();
                                Next13 = br.ReadUInt16();
                                Next14 = br.ReadUInt16();
                                Next15 = br.ReadUInt16();
                            }

                            public void WriteNextValue(BinaryWriter bw)
                            {
                                bw.Write(Next0);
                                bw.Write(Next1);
                                bw.Write(Next2);
                                bw.Write(Next3);
                                bw.Write(Next4);
                                bw.Write(Next5);
                                bw.Write(Next6);
                                bw.Write(Next7);
                                bw.Write(Next8);
                                bw.Write(Next9);
                                bw.Write(Next10);
                                bw.Write(Next11);
                                bw.Write(Next12);
                                bw.Write(Next13);
                                bw.Write(Next14);
                                bw.Write(Next15);
                            }

                            public HPNE_NextGroups()
                            {
                                Next0 = 255;
                                Next1 = 255;
                                Next2 = 255;
                                Next3 = 255;
                                Next4 = 255;
                                Next5 = 255;
                                Next6 = 255;
                                Next7 = 255;
                                Next8 = 255;
                                Next9 = 255;
                                Next10 = 255;
                                Next11 = 255;
                                Next12 = 255;
                                Next13 = 255;
                                Next14 = 255;
                                Next15 = 255;
                            }
                        }

                        public uint HPNE_UnkBytes1 { get; set; }

                        public void ReadHPNEValue(BinaryReader br)
                        {
                            HPNE_StartPoint = br.ReadUInt16();
                            HPNE_Length = br.ReadUInt16();
                            HPNE_PreviewGroup.ReadHPNEPrevValue(br);
                            HPNE_NextGroup.ReadNextValue(br);
                            HPNE_UnkBytes1 = br.ReadUInt16();
                        }

                        public void WriteHPNEValue(BinaryWriter bw)
                        {
                            bw.Write(HPNE_StartPoint);
                            bw.Write(HPNE_Length);
                            HPNE_PreviewGroup.WritePrevValue(bw);
                            HPNE_NextGroup.WriteNextValue(bw);
                            bw.Write(HPNE_UnkBytes1);
                        }

                        public HPNEValue()
                        {
                            HPNE_StartPoint = 0;
                            HPNE_Length = 0;
                            HPNE_PreviewGroup = new HPNE_PreviewGroups();
                            HPNE_NextGroup = new HPNE_NextGroups();
                            HPNE_UnkBytes1 = 0;
                        }
                    }

                    public void ReadHPNE(BinaryReader br)
                    {
                        HPNEHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();
                        for (int Count = 0; Count < NumOfEntries; Count++)
                        {
                            HPNEValue HPNE_Value = new HPNEValue();
                            HPNE_Value.ReadHPNEValue(br);
                            HPNEValue_List.Add(HPNE_Value);
                        }
                    }

                    public void WriteHPNE(BinaryWriter bw)
                    {
                        bw.Write(HPNEHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);
                        for (int Count = 0; Count < NumOfEntries; Count++) HPNEValue_List[Count].WriteHPNEValue(bw);
                    }

                    public HPNE_Section()
                    {
                        HPNEHeader = "HPNE".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        HPNEValue_List = new List<HPNEValue>();
                    }
                }

                public TPTI_Section TPTI { get; set; }
                public class TPTI_Section
                {
                    public char[] TPTIHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<TPTIValue> TPTIValue_List { get; set; }
                    public class TPTIValue
                    {
                        public Vector3D TPTI_Position { get; set; }
                        public float TPTI_PointSize { get; set; }
                        public ushort GravityMode { get; set; }
                        public ushort PlayerScanRadius { get; set; }

                        public void ReadTPTIValue(BinaryReader br)
                        {
                            TPTI_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            TPTI_PointSize = br.ReadSingle();
                            GravityMode = br.ReadUInt16();
                            PlayerScanRadius = br.ReadUInt16();
                        }

                        public void WriteTPTIValue(BinaryWriter bw)
                        {
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI_Position)[2]);
                            bw.Write(TPTI_PointSize);
                            bw.Write(GravityMode);
                            bw.Write(PlayerScanRadius);
                        }

                        public TPTIValue()
                        {
                            TPTI_Position = new Vector3D(0, 0, 0);
                            TPTI_PointSize = 0;
                            GravityMode = 0;
                            PlayerScanRadius = 0;
                        }
                    }

                    public void ReadTPTI(BinaryReader br)
                    {
                        TPTIHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();
                        for (int TPTICount = 0; TPTICount < NumOfEntries; TPTICount++)
                        {
                            TPTIValue TPTI_Value = new TPTIValue();
                            TPTI_Value.ReadTPTIValue(br);
                            TPTIValue_List.Add(TPTI_Value);
                        }
                    }

                    public void WriteTPTI(BinaryWriter bw)
                    {
                        bw.Write(TPTIHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);
                        for (int TPTICount = 0; TPTICount < NumOfEntries; TPTICount++) TPTIValue_List[TPTICount].WriteTPTIValue(bw);
                    }

                    public TPTI_Section()
                    {
                        TPTIHeader = "TPTI".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        TPTIValue_List = new List<TPTIValue>();
                    }
                }

                public HPTI_Section HPTI { get; set; }
                public class HPTI_Section
                {
                    public char[] HPTIHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<HPTIValue> HPTIValue_List { get; set; }
                    public class HPTIValue
                    {
                        public ushort HPTI_StartPoint { get; set; }
                        public ushort HPTI_Length { get; set; }

                        public HPTI_PreviewGroups HPTI_PreviewGroup { get; set; }
                        public class HPTI_PreviewGroups
                        {
                            public ushort Prev0 { get; set; }
                            public ushort Prev1 { get; set; }
                            public ushort Prev2 { get; set; }
                            public ushort Prev3 { get; set; }
                            public ushort Prev4 { get; set; }
                            public ushort Prev5 { get; set; }

                            public void ReadHPTIPrevGroups(BinaryReader br)
                            {
                                Prev0 = br.ReadUInt16();
                                Prev1 = br.ReadUInt16();
                                Prev2 = br.ReadUInt16();
                                Prev3 = br.ReadUInt16();
                                Prev4 = br.ReadUInt16();
                                Prev5 = br.ReadUInt16();
                            }

                            public void WriteHPTIPrevGroups(BinaryWriter bw)
                            {
                                bw.Write(Prev0);
                                bw.Write(Prev1);
                                bw.Write(Prev2);
                                bw.Write(Prev3);
                                bw.Write(Prev4);
                                bw.Write(Prev5);
                            }

                            public HPTI_PreviewGroups()
                            {
                                Prev0 = 0;
                                Prev1 = 0;
                                Prev2 = 0;
                                Prev3 = 0;
                                Prev4 = 0;
                                Prev5 = 0;
                            }
                        }

                        public HPTI_NextGroups HPTI_NextGroup { get; set; }
                        public class HPTI_NextGroups
                        {
                            public ushort Next0 { get; set; }
                            public ushort Next1 { get; set; }
                            public ushort Next2 { get; set; }
                            public ushort Next3 { get; set; }
                            public ushort Next4 { get; set; }
                            public ushort Next5 { get; set; }

                            public void ReadHPTINextGroups(BinaryReader br)
                            {
                                Next0 = br.ReadUInt16();
                                Next1 = br.ReadUInt16();
                                Next2 = br.ReadUInt16();
                                Next3 = br.ReadUInt16();
                                Next4 = br.ReadUInt16();
                                Next5 = br.ReadUInt16();
                            }

                            public void WriteHPTINextGroups(BinaryWriter bw)
                            {
                                bw.Write(Next0);
                                bw.Write(Next1);
                                bw.Write(Next2);
                                bw.Write(Next3);
                                bw.Write(Next4);
                                bw.Write(Next5);
                            }

                            public HPTI_NextGroups()
                            {
                                Next0 = 0;
                                Next1 = 0;
                                Next2 = 0;
                                Next3 = 0;
                                Next4 = 0;
                                Next5 = 0;
                            }
                        }

                        public void ReadHPTIValue(BinaryReader br)
                        {
                            HPTI_StartPoint = br.ReadUInt16();
                            HPTI_Length = br.ReadUInt16();
                            HPTI_PreviewGroup.ReadHPTIPrevGroups(br);
                            HPTI_NextGroup.ReadHPTINextGroups(br);
                        }

                        public void WriteTPTIValue(BinaryWriter bw)
                        {
                            bw.Write(HPTI_StartPoint);
                            bw.Write(HPTI_Length);
                            HPTI_PreviewGroup.WriteHPTIPrevGroups(bw);
                            HPTI_NextGroup.WriteHPTINextGroups(bw);
                        }

                        public HPTIValue()
                        {
                            HPTI_StartPoint = 0;
                            HPTI_Length = 0;
                            HPTI_PreviewGroup = new HPTI_PreviewGroups();
                            HPTI_NextGroup = new HPTI_NextGroups();
                        }
                    }

                    public void ReadHPTI(BinaryReader br)
                    {
                        HPTIHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();
                        for (int Count = 0; Count < NumOfEntries; Count++)
                        {
                            HPTIValue HPTI_Value = new HPTIValue();
                            HPTI_Value.ReadHPTIValue(br);
                            HPTIValue_List.Add(HPTI_Value);
                        }
                    }

                    public void WriteHPTI(BinaryWriter bw)
                    {
                        bw.Write(HPTIHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);
                        for (int Count = 0; Count < NumOfEntries; Count++) HPTIValue_List[Count].WriteTPTIValue(bw);
                    }

                    public HPTI_Section()
                    {
                        HPTIHeader = "HPTI".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        HPTIValue_List = new List<HPTIValue>();
                    }
                }

                public TPKC_Section TPKC { get; set; }
                public class TPKC_Section
                {
                    public char[] TPKCHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<TPKCValue> TPKCValue_List { get; set; }
                    public class TPKCValue
                    {
                        public Vector2 TPKC_2DPosition_Left { get; set; }
                        public Vector2 TPKC_2DPosition_Right { get; set; }
                        public byte TPKC_RespawnID { get; set; }
                        public byte TPKC_Checkpoint_Type { get; set; }
                        public byte TPKC_PreviousCheckPoint { get; set; }
                        public byte TPKC_NextCheckPoint { get; set; }
                        public byte TPKC_ClipID { get; set; }
                        public byte TPKC_Section { get; set; }
                        public byte TPKC_UnkBytes3 { get; set; }
                        public byte TPKC_UnkBytes4 { get; set; }

                        public void ReadTPKCValue(BinaryReader br)
                        {
                            TPKC_2DPosition_Left = KMPs.KMPHelper.Vector3DTo2DConverter.ByteArrayToVector2D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4) });
                            TPKC_2DPosition_Right = KMPs.KMPHelper.Vector3DTo2DConverter.ByteArrayToVector2D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4) });
                            TPKC_RespawnID = br.ReadByte();
                            TPKC_Checkpoint_Type = br.ReadByte();
                            TPKC_PreviousCheckPoint = br.ReadByte();
                            TPKC_NextCheckPoint = br.ReadByte();
                            TPKC_ClipID = br.ReadByte();
                            TPKC_Section = br.ReadByte();
                            TPKC_UnkBytes3 = br.ReadByte();
                            TPKC_UnkBytes4 = br.ReadByte();
                        }

                        public void WriteTPKCValue(BinaryWriter bw)
                        {
                            bw.Write(KMPHelper.Vector3DTo2DConverter.Vector2ToByteArray(TPKC_2DPosition_Left)[0]);
                            bw.Write(KMPHelper.Vector3DTo2DConverter.Vector2ToByteArray(TPKC_2DPosition_Left)[1]);
                            bw.Write(KMPHelper.Vector3DTo2DConverter.Vector2ToByteArray(TPKC_2DPosition_Right)[0]);
                            bw.Write(KMPHelper.Vector3DTo2DConverter.Vector2ToByteArray(TPKC_2DPosition_Right)[1]);
                            bw.Write(TPKC_RespawnID);
                            bw.Write(TPKC_Checkpoint_Type);
                            bw.Write(TPKC_PreviousCheckPoint);
                            bw.Write(TPKC_NextCheckPoint);
                            bw.Write(TPKC_ClipID);
                            bw.Write(TPKC_Section);
                            bw.Write(TPKC_UnkBytes3);
                            bw.Write(TPKC_UnkBytes4);
                        }

                        public TPKCValue()
                        {
                            TPKC_2DPosition_Left = new Vector2(0, 0);
                            TPKC_2DPosition_Right = new Vector2(0, 0);
                            TPKC_RespawnID = 0x00;
                            TPKC_Checkpoint_Type = 0x00;
                            TPKC_PreviousCheckPoint = 0x00;
                            TPKC_NextCheckPoint = 0x00;
                            TPKC_ClipID = 0x00;
                            TPKC_Section = 0x00;
                            TPKC_UnkBytes3 = 0x00;
                            TPKC_UnkBytes4 = 0x00;
                        }
                    }

                    public void ReadTPKC(BinaryReader br)
                    {
                        TPKCHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();
                        for (int Count = 0; Count < NumOfEntries; Count++)
                        {
                            TPKCValue TPKC_Value = new TPKCValue();
                            TPKC_Value.ReadTPKCValue(br);
                            TPKCValue_List.Add(TPKC_Value);
                        }
                    }

                    public void WriteTPKC(BinaryWriter bw)
                    {
                        bw.Write(TPKCHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);
                        for (int Count = 0; Count < TPKCValue_List.Count; Count++) TPKCValue_List[Count].WriteTPKCValue(bw);
                    }

                    public TPKC_Section()
                    {
                        TPKCHeader = "TPKC".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        TPKCValue_List = new List<TPKCValue>();
                    }
                }

                public HPKC_Section HPKC { get; set; }
                public class HPKC_Section
                {
                    public char[] HPKCHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<HPKCValue> HPKCValue_List { get; set; }
                    public class HPKCValue
                    {
                        public byte HPKC_StartPoint { get; set; }
                        public byte HPKC_Length { get; set; }

                        public HPKC_PreviewGroups HPKC_PreviewGroup { get; set; }
                        public class HPKC_PreviewGroups
                        {
                            public byte Prev0 { get; set; }
                            public byte Prev1 { get; set; }
                            public byte Prev2 { get; set; }
                            public byte Prev3 { get; set; }
                            public byte Prev4 { get; set; }
                            public byte Prev5 { get; set; }

                            public void ReadHPKCPrevGroups(BinaryReader br)
                            {
                                Prev0 = br.ReadByte();
                                Prev1 = br.ReadByte();
                                Prev2 = br.ReadByte();
                                Prev3 = br.ReadByte();
                                Prev4 = br.ReadByte();
                                Prev5 = br.ReadByte();
                            }

                            public void WriteHPKCPrevGroups(BinaryWriter bw)
                            {
                                bw.Write(Prev0);
                                bw.Write(Prev1);
                                bw.Write(Prev2);
                                bw.Write(Prev3);
                                bw.Write(Prev4);
                                bw.Write(Prev5);
                            }

                            public HPKC_PreviewGroups()
                            {
                                Prev0 = 0x00;
                                Prev1 = 0x00;
                                Prev2 = 0x00;
                                Prev3 = 0x00;
                                Prev4 = 0x00;
                                Prev5 = 0x00;
                            }
                        }

                        public HPKC_NextGroups HPKC_NextGroup { get; set; }
                        public class HPKC_NextGroups
                        {
                            public byte Next0 { get; set; }
                            public byte Next1 { get; set; }
                            public byte Next2 { get; set; }
                            public byte Next3 { get; set; }
                            public byte Next4 { get; set; }
                            public byte Next5 { get; set; }

                            public void ReadHPKCNextGroup(BinaryReader br)
                            {
                                Next0 = br.ReadByte();
                                Next1 = br.ReadByte();
                                Next2 = br.ReadByte();
                                Next3 = br.ReadByte();
                                Next4 = br.ReadByte();
                                Next5 = br.ReadByte();
                            }

                            public void WriteHPKCNextGroup(BinaryWriter bw)
                            {
                                bw.Write(Next0);
                                bw.Write(Next1);
                                bw.Write(Next2);
                                bw.Write(Next3);
                                bw.Write(Next4);
                                bw.Write(Next5);
                            }

                            public HPKC_NextGroups()
                            {
                                Next0 = 0x00;
                                Next1 = 0x00;
                                Next2 = 0x00;
                                Next3 = 0x00;
                                Next4 = 0x00;
                                Next5 = 0x00;
                            }
                        }

                        public ushort HPKC_UnkBytes1 { get; set; }

                        public void ReadHPKCValue(BinaryReader br)
                        {
                            HPKC_StartPoint = br.ReadByte();
                            HPKC_Length = br.ReadByte();
                            HPKC_PreviewGroup.ReadHPKCPrevGroups(br);
                            HPKC_NextGroup.ReadHPKCNextGroup(br);
                            HPKC_UnkBytes1 = br.ReadUInt16();
                        }

                        public void WriteHPKCValue(BinaryWriter bw)
                        {
                            bw.Write(HPKC_StartPoint);
                            bw.Write(HPKC_Length);
                            HPKC_PreviewGroup.WriteHPKCPrevGroups(bw);
                            HPKC_NextGroup.WriteHPKCNextGroup(bw);
                            bw.Write(HPKC_UnkBytes1);
                        }

                        public HPKCValue()
                        {
                            HPKC_StartPoint = 0x00;
                            HPKC_Length = 0x00;
                            HPKC_PreviewGroup = new HPKC_PreviewGroups();
                            HPKC_NextGroup = new HPKC_NextGroups();
                            HPKC_UnkBytes1 = 0;
                        }
                    }

                    public void ReadHPKC(BinaryReader br)
                    {
                        HPKCHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int HPKCCount = 0; HPKCCount < NumOfEntries; HPKCCount++)
                        {
                            HPKCValue HPKC_Value = new HPKCValue();
                            HPKC_Value.ReadHPKCValue(br);
                            HPKCValue_List.Add(HPKC_Value);
                        }
                    }

                    public void WriteHPKC(BinaryWriter bw)
                    {
                        bw.Write(HPKCHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int Count = 0; Count < NumOfEntries; Count++) HPKCValue_List[Count].WriteHPKCValue(bw);
                    }

                    public HPKC_Section()
                    {
                        HPKCHeader = "HPKC".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        HPKCValue_List = new List<HPKCValue>();
                    }
                }

                public JBOG_Section JBOG { get; set; }
                public class JBOG_Section
                {
                    public char[] JBOGHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<JBOGValue> JBOGValue_List { get; set; }
                    public class JBOGValue
                    {
                        public byte[] ObjectID { get; set; }
                        public byte[] JBOG_UnkByte1 { get; set; }
                        public Vector3D JBOG_Position { get; set; }
                        public Vector3D JBOG_Rotation { get; set; }
                        public Vector3D JBOG_Scale { get; set; }
                        public ushort JBOG_ITOP_RouteIDIndex { get; set; }
                        public JBOG_SpecificSetting GOBJ_Specific_Setting { get; set; }
                        public class JBOG_SpecificSetting
                        {
                            public ushort Value0 { get; set; }
                            public ushort Value1 { get; set; }
                            public ushort Value2 { get; set; }
                            public ushort Value3 { get; set; }
                            public ushort Value4 { get; set; }
                            public ushort Value5 { get; set; }
                            public ushort Value6 { get; set; }
                            public ushort Value7 { get; set; }

                            public void ReadSpecificSetting(BinaryReader br)
                            {
                                Value0 = br.ReadUInt16();
                                Value1 = br.ReadUInt16();
                                Value2 = br.ReadUInt16();
                                Value3 = br.ReadUInt16();
                                Value4 = br.ReadUInt16();
                                Value5 = br.ReadUInt16();
                                Value6 = br.ReadUInt16();
                                Value7 = br.ReadUInt16();
                            }

                            public void WriteSpecificSetting(BinaryWriter bw)
                            {
                                bw.Write(Value0);
                                bw.Write(Value1);
                                bw.Write(Value2);
                                bw.Write(Value3);
                                bw.Write(Value4);
                                bw.Write(Value5);
                                bw.Write(Value6);
                                bw.Write(Value7);
                            }

                            public JBOG_SpecificSetting()
                            {
                                Value0 = 255;
                                Value1 = 255;
                                Value2 = 255;
                                Value3 = 255;
                                Value4 = 255;
                                Value5 = 255;
                                Value6 = 255;
                                Value7 = 255;
                            }
                        }
                        public ushort JBOG_PresenceSetting { get; set; }
                        public byte[] JBOG_UnkByte2 { get; set; }
                        public ushort JBOG_UnkByte3 { get; set; }

                        public void ReadJBOGValue(BinaryReader br, uint Version)
                        {
                            ObjectID = br.ReadBytes(2);
                            JBOG_UnkByte1 = br.ReadBytes(2);
                            JBOG_Position = KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            JBOG_Rotation = KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            JBOG_Scale = KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            JBOG_ITOP_RouteIDIndex = br.ReadUInt16();
                            GOBJ_Specific_Setting.ReadSpecificSetting(br);
                            JBOG_PresenceSetting = br.ReadUInt16();

                            if (Version == 3100) 
                            {
                                JBOG_UnkByte2 = br.ReadBytes(2);
                                JBOG_UnkByte3 = br.ReadUInt16();
                            }
                            if (Version == 3000) return;
                        }

                        public void WriteJBOGValue(BinaryWriter bw, uint Version)
                        {
                            bw.Write(ObjectID);
                            bw.Write(JBOG_UnkByte1);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Position)[2]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Rotation)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Rotation)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Rotation)[2]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Scale)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Scale)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG_Scale)[2]);
                            bw.Write(JBOG_ITOP_RouteIDIndex);
                            GOBJ_Specific_Setting.WriteSpecificSetting(bw);
                            bw.Write(JBOG_PresenceSetting);

                            if (Version == 3100)
                            {
                                bw.Write(JBOG_UnkByte2);
                                bw.Write(JBOG_UnkByte3);
                            }
                            if (Version == 3000) return;
                        }

                        public JBOGValue()
                        {
                            ObjectID = new List<byte>().ToArray();
                            JBOG_UnkByte1 = new List<byte>().ToArray();
                            JBOG_Position = new Vector3D(0, 0, 0);
                            JBOG_Rotation = new Vector3D(0, 0, 0);
                            JBOG_Scale = new Vector3D(0, 0, 0);
                            JBOG_ITOP_RouteIDIndex = 0;
                            GOBJ_Specific_Setting = new JBOG_SpecificSetting();
                            JBOG_PresenceSetting = 0;
                            JBOG_UnkByte2 = new List<byte>().ToArray();
                            JBOG_UnkByte3 = 0;
                        }
                    }

                    public void ReadJBOG(BinaryReader br, uint Version)
                    {
                        JBOGHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int JBOGCount = 0; JBOGCount < NumOfEntries; JBOGCount++)
                        {
                            JBOGValue JBOG_Value = new JBOGValue();
                            JBOG_Value.ReadJBOGValue(br, Version);
                            JBOGValue_List.Add(JBOG_Value);
                        }
                    }

                    public void WriteJBOG(BinaryWriter bw, uint Version)
                    {
                        bw.Write(JBOGHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int Count = 0; Count < NumOfEntries; Count++) JBOGValue_List[Count].WriteJBOGValue(bw, Version);
                    }

                    public JBOG_Section()
                    {
                        JBOGHeader = "JBOG".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        JBOGValue_List = new List<JBOGValue>();
                    }
                }

                public ITOP_Section ITOP { get; set; }
                public class ITOP_Section
                {
                    public char[] ITOPHeader { get; set; }
                    public ushort ITOP_NumberOfRoute { get; set; }
                    public ushort ITOP_NumberOfPoint { get; set; }
                    public List<ITOP_Route> ITOP_Route_List { get; set; }
                    public class ITOP_Route
                    {
                        public ushort ITOP_Route_NumOfPoint { get; set; }
                        public byte ITOP_RoopSetting { get; set; }
                        public byte ITOP_SmoothSetting { get; set; }
                        public List<ITOP_Point> ITOP_Point_List { get; set; }
                        public class ITOP_Point
                        {
                            public Vector3D ITOP_Point_Position { get; set; }
                            public ushort ITOP_Point_RouteSpeed { get; set; }
                            public ushort ITOP_PointSetting2 { get; set; }

                            public void ReadITOP_Point(BinaryReader br)
                            {
                                ITOP_Point_Position = KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                                ITOP_Point_RouteSpeed = br.ReadUInt16();
                                ITOP_PointSetting2 = br.ReadUInt16();
                            }

                            public void WriteITOP_Point(BinaryWriter bw)
                            {
                                bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP_Point_Position)[0]);
                                bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP_Point_Position)[1]);
                                bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP_Point_Position)[2]);
                                bw.Write(ITOP_Point_RouteSpeed);
                                bw.Write(ITOP_PointSetting2);
                            }

                            public ITOP_Point()
                            {
                                ITOP_Point_Position = new Vector3D(0, 0, 0);
                                ITOP_Point_RouteSpeed = 0;
                                ITOP_PointSetting2 = 0;
                            }
                        }

                        public void ReadITOPRoute(BinaryReader br)
                        {
                            ITOP_Route_NumOfPoint = br.ReadUInt16();
                            ITOP_RoopSetting = br.ReadByte();
                            ITOP_SmoothSetting = br.ReadByte();

                            for (int ITOP_PointCount = 0; ITOP_PointCount < ITOP_Route_NumOfPoint; ITOP_PointCount++)
                            {
                                ITOP_Point iTOP_Point = new ITOP_Point();
                                iTOP_Point.ReadITOP_Point(br);
                                ITOP_Point_List.Add(iTOP_Point);
                            }
                        }

                        public void WriteITOPRoute(BinaryWriter bw)
                        {
                            bw.Write(ITOP_Route_NumOfPoint);
                            bw.Write(ITOP_RoopSetting);
                            bw.Write(ITOP_SmoothSetting);

                            for (int ITOP_PointsCount = 0; ITOP_PointsCount < ITOP_Route_NumOfPoint; ITOP_PointsCount++)
                            {
                                ITOP_Point_List[ITOP_PointsCount].WriteITOP_Point(bw);
                            }
                        }

                        public ITOP_Route()
                        {
                            ITOP_Route_NumOfPoint = 0;
                            ITOP_RoopSetting = 0x00;
                            ITOP_SmoothSetting = 0x00;
                            ITOP_Point_List = new List<ITOP_Point>();
                        }
                    }

                    public void ReadITOP(BinaryReader br)
                    {
                        ITOPHeader = br.ReadChars(4);
                        ITOP_NumberOfRoute = br.ReadUInt16();
                        ITOP_NumberOfPoint = br.ReadUInt16();

                        for (int ITOPRouteCount = 0; ITOPRouteCount < ITOP_NumberOfRoute; ITOPRouteCount++)
                        {
                            ITOP_Route iTOP_Route = new ITOP_Route();
                            iTOP_Route.ReadITOPRoute(br);
                            ITOP_Route_List.Add(iTOP_Route);
                        }
                    }

                    public void WriteITOP(BinaryWriter bw)
                    {
                        bw.Write(ITOPHeader);
                        bw.Write(ITOP_NumberOfRoute);
                        bw.Write(ITOP_NumberOfPoint);

                        for (int ITOP_RoutesCount = 0; ITOP_RoutesCount < ITOP_NumberOfRoute; ITOP_RoutesCount++)
                        {
                            ITOP_Route_List[ITOP_RoutesCount].WriteITOPRoute(bw);
                        }
                    }

                    public ITOP_Section()
                    {
                        ITOPHeader = "ITOP".ToCharArray();
                        ITOP_NumberOfRoute = 0;
                        ITOP_NumberOfPoint = 0;
                        ITOP_Route_List = new List<ITOP_Route>();
                    }
                }

                public AERA_Section AERA { get; set; }
                public class AERA_Section
                {
                    public char[] AERAHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<AERAValue> AERAValue_List { get; set; }
                    public class AERAValue
                    {
                        public byte AreaMode { get; set; }
                        public byte AreaType { get; set; }
                        public byte AERA_EMACIndex { get; set; }
                        public byte Priority { get; set; }
                        public Vector3D AERA_Position { get; set; }
                        public Vector3D AERA_Rotation { get; set; }
                        public Vector3D AERA_Scale { get; set; }
                        public ushort AERA_Setting1 { get; set; }
                        public ushort AERA_Setting2 { get; set; }
                        public byte RouteID { get; set; }
                        public byte EnemyID { get; set; }
                        public ushort AERA_UnkByte4 { get; set; }

                        public void ReadAERAValue(BinaryReader br)
                        {
                            AreaMode = br.ReadByte();
                            AreaType = br.ReadByte();
                            AERA_EMACIndex = br.ReadByte();
                            Priority = br.ReadByte();
                            AERA_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            AERA_Rotation = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            AERA_Scale = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            AERA_Setting1 = br.ReadUInt16();
                            AERA_Setting2 = br.ReadUInt16();
                            RouteID = br.ReadByte();
                            EnemyID = br.ReadByte();
                            AERA_UnkByte4 = br.ReadUInt16();
                        }

                        public void WriteAERAValue(BinaryWriter bw)
                        {
                            bw.Write(AreaMode);
                            bw.Write(AreaType);
                            bw.Write(AERA_EMACIndex);
                            bw.Write(Priority);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Position)[2]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Rotation)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Rotation)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Rotation)[2]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Scale)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Scale)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA_Scale)[2]);
                            bw.Write(AERA_Setting1);
                            bw.Write(AERA_Setting2);
                            bw.Write(RouteID);
                            bw.Write(EnemyID);
                            bw.Write(AERA_UnkByte4);
                        }

                        public AERAValue()
                        {
                            AreaMode = 0x00;
                            AreaType = 0x00;
                            AERA_EMACIndex = 0x00;
                            Priority = 0x00;
                            AERA_Position = new Vector3D(0, 0, 0);
                            AERA_Rotation = new Vector3D(0, 0, 0);
                            AERA_Scale = new Vector3D(0, 0, 0);
                            AERA_Setting1 = 0;
                            AERA_Setting2 = 0;
                            RouteID = 0x00;
                            EnemyID = 0x00;
                            AERA_UnkByte4 = 0;
                        }
                    }

                    public void ReadAERA(BinaryReader br)
                    {
                        AERAHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int AERACount = 0; AERACount < NumOfEntries; AERACount++)
                        {
                            AERAValue aERAValue = new AERAValue();
                            aERAValue.ReadAERAValue(br);
                            AERAValue_List.Add(aERAValue);
                        }
                    }

                    public void WriteAERA(BinaryWriter bw)
                    {
                        bw.Write(AERAHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int Count = 0; Count < NumOfEntries; Count++)
                        {
                            AERAValue_List[Count].WriteAERAValue(bw);
                        }
                    }

                    public AERA_Section()
                    {
                        AERAHeader = "AERA".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        AERAValue_List = new List<AERAValue>();
                    }
                }

                public EMAC_Section EMAC { get; set; }
                public class EMAC_Section
                {
                    public char[] EMACHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<EMACValue> EMACValue_List { get; set; }
                    public class EMACValue
                    {
                        public byte CameraType { get; set; }
                        public byte NextCameraIndex { get; set; }
                        public byte EMAC_NextVideoIndex { get; set; }
                        public byte EMAC_ITOP_CameraIndex { get; set; }
                        public ushort RouteSpeed { get; set; }
                        public ushort FOVSpeed { get; set; }
                        public ushort ViewpointSpeed { get; set; }
                        public byte EMAC_StartFlag { get; set; }
                        public byte EMAC_VideoFlag { get; set; }
                        public Vector3D EMAC_Position { get; set; }
                        public Vector3D EMAC_Rotation { get; set; }
                        public float FOVAngle_Start { get; set; }
                        public float FOVAngle_End { get; set; }
                        public Vector3D Viewpoint_Start { get; set; }
                        public Vector3D Viewpoint_Destination { get; set; }
                        public float Camera_Active_Time { get; set; }

                        public void ReadEMACValue(BinaryReader br)
                        {
                            CameraType = br.ReadByte();
                            NextCameraIndex = br.ReadByte();
                            EMAC_NextVideoIndex = br.ReadByte();
                            EMAC_ITOP_CameraIndex = br.ReadByte();
                            RouteSpeed = br.ReadUInt16();
                            FOVSpeed = br.ReadUInt16();
                            ViewpointSpeed = br.ReadUInt16();
                            EMAC_StartFlag = br.ReadByte();
                            EMAC_VideoFlag = br.ReadByte();
                            EMAC_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            EMAC_Rotation = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            FOVAngle_Start = br.ReadSingle();
                            FOVAngle_End = br.ReadSingle();
                            Viewpoint_Start = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            Viewpoint_Destination = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            Camera_Active_Time = br.ReadSingle();
                        }

                        public void WriteEMACValue(BinaryWriter bw)
                        {
                            bw.Write(CameraType);
                            bw.Write(NextCameraIndex);
                            bw.Write(EMAC_NextVideoIndex);
                            bw.Write(EMAC_ITOP_CameraIndex);
                            bw.Write(RouteSpeed);
                            bw.Write(FOVSpeed);
                            bw.Write(ViewpointSpeed);
                            bw.Write(EMAC_StartFlag);
                            bw.Write(EMAC_VideoFlag);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC_Position)[2]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC_Rotation)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC_Rotation)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC_Rotation)[2]);
                            bw.Write(FOVAngle_Start);
                            bw.Write(FOVAngle_End);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(Viewpoint_Start)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(Viewpoint_Start)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(Viewpoint_Start)[2]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(Viewpoint_Destination)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(Viewpoint_Destination)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(Viewpoint_Destination)[2]);
                            bw.Write(Camera_Active_Time);
                        }

                        public EMACValue()
                        {
                            CameraType = 0x00;
                            NextCameraIndex = 0x00;
                            EMAC_NextVideoIndex = 0x00;
                            EMAC_ITOP_CameraIndex = 0x00;
                            RouteSpeed = 0;
                            FOVSpeed = 0;
                            ViewpointSpeed = 0;
                            EMAC_StartFlag = 0x00;
                            EMAC_VideoFlag = 0x00;
                            EMAC_Position = new Vector3D(0, 0, 0);
                            EMAC_Rotation = new Vector3D(0, 0, 0);
                            FOVAngle_Start = 0f;
                            FOVAngle_End = 0f;
                            Viewpoint_Start = new Vector3D(0, 0, 0);
                            Viewpoint_Destination = new Vector3D(0, 0, 0);
                            Camera_Active_Time = 0f;
                        }
                    }

                    public void ReadEMAC(BinaryReader br)
                    {
                        EMACHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int EMACCount = 0; EMACCount < NumOfEntries; EMACCount++)
                        {
                            EMACValue eMACValue = new EMACValue();
                            eMACValue.ReadEMACValue(br);
                            EMACValue_List.Add(eMACValue);
                        }
                    }

                    public void WriteEMAC(BinaryWriter bw)
                    {
                        bw.Write(EMACHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int Count = 0; Count < NumOfEntries; Count++)
                        {
                            EMACValue_List[Count].WriteEMACValue(bw);
                        }
                    }

                    public EMAC_Section()
                    {
                        EMACHeader = "EMAC".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        EMACValue_List = new List<EMACValue>();
                    }
                }

                public TPGJ_Section TPGJ { get; set; }
                public class TPGJ_Section
                {
                    public char[] TPGJHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<TPGJValue> TPGJValue_List { get; set; }
                    public class TPGJValue
                    {
                        public Vector3D TPGJ_Position { get; set; }
                        public Vector3D TPGJ_Rotation { get; set; }
                        public ushort TPGJ_RespawnID { get; set; }
                        public ushort TPGJ_UnkBytes1 { get; set; }

                        public void ReadTPGJValue(BinaryReader br)
                        {
                            TPGJ_Position = KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            TPGJ_Rotation = KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            TPGJ_RespawnID = br.ReadUInt16();
                            TPGJ_UnkBytes1 = br.ReadUInt16();
                        }

                        public void WriteTPGJValue(BinaryWriter bw)
                        {
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ_Position)[2]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ_Rotation)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ_Rotation)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ_Rotation)[2]);
                            bw.Write(TPGJ_RespawnID);
                            bw.Write(TPGJ_UnkBytes1);
                        }

                        public TPGJValue()
                        {
                            TPGJ_Position = new Vector3D(0, 0, 0);
                            TPGJ_Rotation = new Vector3D(0, 0, 0);
                            TPGJ_RespawnID = 0;
                            TPGJ_UnkBytes1 = 0;
                        }
                    }

                    public void ReadTPGJ(BinaryReader br)
                    {
                        TPGJHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int TPGJCount = 0; TPGJCount < NumOfEntries; TPGJCount++)
                        {
                            TPGJValue tPGJValue = new TPGJValue();
                            tPGJValue.ReadTPGJValue(br);
                            TPGJValue_List.Add(tPGJValue);
                        }
                    }

                    public void WriteTPGJ(BinaryWriter bw)
                    {
                        bw.Write(TPGJHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int Count = 0; Count < NumOfEntries; Count++)
                        {
                            TPGJValue_List[Count].WriteTPGJValue(bw);
                        }
                    }

                    public TPGJ_Section()
                    {
                        TPGJHeader = "TPGJ".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        TPGJValue_List = new List<TPGJValue>();
                    }
                }

                //Unused Section
                public TPNC_Section TPNC { get; set; }
                public class TPNC_Section
                {
                    public char[] TPNCHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    //public List<TPNCValue> TPNCValue_List { get; set; }
                    //public class TPNCValue
                    //{
                    //    //Unused
                    //}

                    public void ReadTPNC(BinaryReader br)
                    {
                        TPNCHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();
                    }

                    public void WriteTPNC(BinaryWriter bw)
                    {
                        bw.Write(TPNCHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);
                    }

                    public TPNC_Section()
                    {
                        TPNCHeader = "TPNC".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                    }
                }

                //Unused Section
                public TPSM_Section TPSM { get; set; }
                public class TPSM_Section
                {
                    public char[] TPSMHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    //public List<TPSMValue> TPSMValue_List { get; set; }
                    //public class TPSMValue
                    //{
                    //    //Unused
                    //}

                    public void ReadTPSM(BinaryReader br)
                    {
                        TPSMHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();
                    }

                    public void WriteTPSM(BinaryWriter bw)
                    {
                        bw.Write(TPSMHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);
                    }

                    public TPSM_Section()
                    {
                        TPSMHeader = "TPSM".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                    }
                }

                public IGTS_Section IGTS { get; set; }
                public class IGTS_Section
                {
                    public char[] IGTSHeader { get; set; }

                    public uint Unknown1 { get; set; }
                    public byte LapCount { get; set; }
                    public byte PolePosition { get; set; }
                    public byte Unknown2 { get; set; }
                    public byte Unknown3 { get; set; }

                    public RGBA RGBAColor { get; set; }
                    public class RGBA
                    {
                        public byte R { get; set; }
                        public byte G { get; set; }
                        public byte B { get; set; }
                        public byte A { get; set; }

                        public void ReadRGBA(BinaryReader br)
                        {
                            R = br.ReadByte();
                            G = br.ReadByte();
                            B = br.ReadByte();
                            A = br.ReadByte();
                        }

                        public void WriteRGBA(BinaryWriter bw)
                        {
                            bw.Write(R);
                            bw.Write(G);
                            bw.Write(B);
                            bw.Write(A);
                        }

                        public RGBA(byte ColorR = 0xFF, byte ColorG = 0xFF, byte ColorB = 0xFF, byte ColorA = 0xFF)
                        {
                            R = ColorR;
                            G = ColorG;
                            B = ColorB;
                            A = ColorA;
                        }

                        public RGBA()
                        {
                            R = 255;
                            G = 255;
                            B = 255;
                            A = 255;
                        }
                    }

                    public uint FlareAlpha { get; set; }

                    public void ReadIGTS(BinaryReader br)
                    {
                        IGTSHeader = br.ReadChars(4);
                        Unknown1 = br.ReadUInt32();
                        LapCount = br.ReadByte();
                        PolePosition = br.ReadByte();
                        Unknown2 = br.ReadByte();
                        Unknown3 = br.ReadByte();
                        RGBAColor.ReadRGBA(br);
                        FlareAlpha = br.ReadUInt32();
                    }

                    public void WriteIGTS(BinaryWriter bw)
                    {
                        bw.Write(IGTSHeader);
                        bw.Write(Unknown1);
                        bw.Write(LapCount);
                        bw.Write(PolePosition);
                        bw.Write(Unknown2);
                        bw.Write(Unknown3);
                        RGBAColor.WriteRGBA(bw);
                        bw.Write(FlareAlpha);
                    }

                    public IGTS_Section()
                    {
                        IGTSHeader = "IGTS".ToCharArray();
                        Unknown1 = 0;
                        LapCount = 0x00;
                        PolePosition = 0x00;
                        Unknown2 = 0x00;
                        Unknown3 = 0x00;
                        RGBAColor = new RGBA();
                        FlareAlpha = 0;
                    }
                }

                //Unused Section
                public SROC_Section SROC { get; set; }
                public class SROC_Section
                {
                    public char[] SROCHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    //public List<SROCValue> SROCValue_List { get; set; }
                    //public class SROCValue
                    //{
                    //    //Unused
                    //}

                    public void ReadSROC(BinaryReader br)
                    {
                        SROCHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();
                    }

                    public void WriteSROC(BinaryWriter bw)
                    {
                        bw.Write(SROCHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);
                    }

                    public SROC_Section()
                    {
                        SROCHeader = "SROC".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                    }
                }

                public TPLG_Section TPLG { get; set; }
                public class TPLG_Section
                {
                    public char[] TPLGHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<TPLGValue> TPLGValue_List { get; set; }
                    public class TPLGValue
                    {
                        public Vector3D TPLG_Position { get; set; }
                        public float TPLG_PointScaleValue { get; set; }
                        public uint TPLG_UnkBytes1 { get; set; }
                        public uint TPLG_UnkBytes2 { get; set; }

                        public void ReadTPLGValue(BinaryReader br)
                        {
                            TPLG_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                            TPLG_PointScaleValue = br.ReadSingle();
                            TPLG_UnkBytes1 = br.ReadUInt32();
                            TPLG_UnkBytes2 = br.ReadUInt32();
                        }

                        public void WriteTPLGValue(BinaryWriter bw)
                        {
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG_Position)[0]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG_Position)[1]);
                            bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG_Position)[2]);
                            bw.Write(TPLG_PointScaleValue);
                            bw.Write(TPLG_UnkBytes1);
                            bw.Write(TPLG_UnkBytes2);
                        }

                        public TPLGValue()
                        {
                            TPLG_Position = new Vector3D(0, 0, 0);
                            TPLG_PointScaleValue = 0f;
                            TPLG_UnkBytes1 = 0;
                            TPLG_UnkBytes2 = 0;
                        }
                    }

                    public void ReadTPLG(BinaryReader br)
                    {
                        TPLGHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int TPLGCount = 0; TPLGCount < NumOfEntries; TPLGCount++)
                        {
                            TPLGValue tPLGValue = new TPLGValue();
                            tPLGValue.ReadTPLGValue(br);
                            TPLGValue_List.Add(tPLGValue);
                        }
                    }

                    public void WriteTPLG(BinaryWriter bw)
                    {
                        bw.Write(TPLGHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int Count = 0; Count < NumOfEntries; Count++)
                        {
                            TPLGValue_List[Count].WriteTPLGValue(bw);
                        }
                    }

                    public TPLG_Section()
                    {
                        TPLGHeader = "TPLG".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        TPLGValue_List = new List<TPLGValue>();
                    }
                }

                public HPLG_Section HPLG { get; set; }
                public class HPLG_Section
                {
                    public char[] HPLGHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<HPLGValue> HPLGValue_List { get; set; }
                    public class HPLGValue
                    {
                        public byte HPLG_StartPoint { get; set; }
                        public byte HPLG_Length { get; set; }

                        public HPLG_PreviewGroups HPLG_PreviewGroup { get; set; }
                        public class HPLG_PreviewGroups
                        {
                            public byte Prev0 { get; set; }
                            public byte Prev1 { get; set; }
                            public byte Prev2 { get; set; }
                            public byte Prev3 { get; set; }
                            public byte Prev4 { get; set; }
                            public byte Prev5 { get; set; }

                            public void ReadHPLGPrevGroups(BinaryReader br)
                            {
                                Prev0 = br.ReadByte();
                                Prev1 = br.ReadByte();
                                Prev2 = br.ReadByte();
                                Prev3 = br.ReadByte();
                                Prev4 = br.ReadByte();
                                Prev5 = br.ReadByte();
                            }

                            public void WriteHPLGPrevGroups(BinaryWriter bw)
                            {
                                bw.Write(Prev0);
                                bw.Write(Prev1);
                                bw.Write(Prev2);
                                bw.Write(Prev3);
                                bw.Write(Prev4);
                                bw.Write(Prev5);
                            }

                            public HPLG_PreviewGroups()
                            {
                                Prev0 = 0xFF;
                                Prev1 = 0xFF;
                                Prev2 = 0xFF;
                                Prev3 = 0xFF;
                                Prev4 = 0xFF;
                                Prev5 = 0xFF;
                            }
                        }

                        public HPLG_NextGroups HPLG_NextGroup { get; set; }
                        public class HPLG_NextGroups
                        {
                            public byte Next0 { get; set; }
                            public byte Next1 { get; set; }
                            public byte Next2 { get; set; }
                            public byte Next3 { get; set; }
                            public byte Next4 { get; set; }
                            public byte Next5 { get; set; }

                            public void ReadHPLGNextGroups(BinaryReader br)
                            {
                                Next0 = br.ReadByte();
                                Next1 = br.ReadByte();
                                Next2 = br.ReadByte();
                                Next3 = br.ReadByte();
                                Next4 = br.ReadByte();
                                Next5 = br.ReadByte();
                            }

                            public void WriteHPLGNextGroups(BinaryWriter bw)
                            {
                                bw.Write(Next0);
                                bw.Write(Next1);
                                bw.Write(Next2);
                                bw.Write(Next3);
                                bw.Write(Next4);
                                bw.Write(Next5);
                            }

                            public HPLG_NextGroups()
                            {
                                Next0 = 0xFF;
                                Next1 = 0xFF;
                                Next2 = 0xFF;
                                Next3 = 0xFF;
                                Next4 = 0xFF;
                                Next5 = 0xFF;
                            }
                        }

                        public uint RouteSetting { get; set; }
                        public uint HPLG_UnkBytes2 { get; set; }

                        public void ReadHPLGValue(BinaryReader br)
                        {
                            HPLG_StartPoint = br.ReadByte();
                            HPLG_Length = br.ReadByte();
                            HPLG_PreviewGroup.ReadHPLGPrevGroups(br);
                            HPLG_NextGroup.ReadHPLGNextGroups(br);
                            RouteSetting = br.ReadUInt32();
                            HPLG_UnkBytes2 = br.ReadUInt32();
                        }

                        public void WriteHPLGValue(BinaryWriter bw)
                        {
                            bw.Write(HPLG_StartPoint);
                            bw.Write(HPLG_Length);
                            HPLG_PreviewGroup.WriteHPLGPrevGroups(bw);
                            HPLG_NextGroup.WriteHPLGNextGroups(bw);
                            bw.Write(RouteSetting);
                            bw.Write(HPLG_UnkBytes2);
                        }

                        public HPLGValue()
                        {
                            HPLG_StartPoint = 0x00;
                            HPLG_Length = 0x00;
                            HPLG_PreviewGroup = new HPLG_PreviewGroups();
                            HPLG_NextGroup = new HPLG_NextGroups();
                            RouteSetting = 0;
                            HPLG_UnkBytes2 = 0;
                        }
                    }

                    public void ReadHPLG(BinaryReader br)
                    {
                        HPLGHeader = br.ReadChars(4);
                        NumOfEntries = br.ReadUInt16();
                        AdditionalValue = br.ReadUInt16();

                        for (int HPLGCount = 0; HPLGCount < NumOfEntries; HPLGCount++)
                        {
                            HPLGValue hPLGValue = new HPLGValue();
                            hPLGValue.ReadHPLGValue(br);
                            HPLGValue_List.Add(hPLGValue);
                        }
                    }

                    public void WriteHPLG(BinaryWriter bw)
                    {
                        bw.Write(HPLGHeader);
                        bw.Write(NumOfEntries);
                        bw.Write(AdditionalValue);

                        for (int Count = 0; Count < NumOfEntries; Count++)
                        {
                            HPLGValue_List[Count].WriteHPLGValue(bw);
                        }
                    }

                    public HPLG_Section()
                    {
                        HPLGHeader = "HPLG".ToCharArray();
                        NumOfEntries = 0;
                        AdditionalValue = 0;
                        HPLGValue_List = new List<HPLGValue>();
                    }
                }

                public void ReadKMPSection(BinaryReader br, uint Version)
                {
                    TPTK_Offset = br.ReadUInt32();
                    TPNE_Offset = br.ReadUInt32();
                    HPNE_Offset = br.ReadUInt32();
                    TPTI_Offset = br.ReadUInt32();
                    HPTI_Offset = br.ReadUInt32();
                    TPKC_Offset = br.ReadUInt32();
                    HPKC_Offset = br.ReadUInt32();
                    JBOG_Offset = br.ReadUInt32();
                    ITOP_Offset = br.ReadUInt32();
                    AERA_Offset = br.ReadUInt32();
                    EMAC_Offset = br.ReadUInt32();
                    TPGJ_Offset = br.ReadUInt32();
                    TPNC_Offset = br.ReadUInt32();
                    TPSM_Offset = br.ReadUInt32();
                    IGTS_Offset = br.ReadUInt32();
                    SROC_Offset = br.ReadUInt32();
                    TPLG_Offset = br.ReadUInt32();
                    HPLG_Offset = br.ReadUInt32();

                    long KMPSectionPos = br.BaseStream.Position;

                    br.BaseStream.Seek(TPTK_Offset, SeekOrigin.Current);
                    TPTK.ReadTPTK(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(TPNE_Offset, SeekOrigin.Current);
                    TPNE.ReadTPNE(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(HPNE_Offset, SeekOrigin.Current);
                    HPNE.ReadHPNE(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(TPTI_Offset, SeekOrigin.Current);
                    TPTI.ReadTPTI(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(HPTI_Offset, SeekOrigin.Current);
                    HPTI.ReadHPTI(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(TPKC_Offset, SeekOrigin.Current);
                    TPKC.ReadTPKC(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(HPKC_Offset, SeekOrigin.Current);
                    HPKC.ReadHPKC(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(JBOG_Offset, SeekOrigin.Current);
                    JBOG.ReadJBOG(br, Version);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(ITOP_Offset, SeekOrigin.Current);
                    ITOP.ReadITOP(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(AERA_Offset, SeekOrigin.Current);
                    AERA.ReadAERA(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(EMAC_Offset, SeekOrigin.Current);
                    EMAC.ReadEMAC(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(TPGJ_Offset, SeekOrigin.Current);
                    TPGJ.ReadTPGJ(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(TPNC_Offset, SeekOrigin.Current);
                    TPNC.ReadTPNC(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(TPSM_Offset, SeekOrigin.Current);
                    TPSM.ReadTPSM(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(IGTS_Offset, SeekOrigin.Current);
                    IGTS.ReadIGTS(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(SROC_Offset, SeekOrigin.Current);
                    SROC.ReadSROC(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(TPLG_Offset, SeekOrigin.Current);
                    TPLG.ReadTPLG(br);
                    br.BaseStream.Position = KMPSectionPos;

                    br.BaseStream.Seek(HPLG_Offset, SeekOrigin.Current);
                    HPLG.ReadHPLG(br);
                    br.BaseStream.Position = KMPSectionPos;
                }

                public void WriteKMPSection(BinaryWriter bw, uint Version)
                {
                    long SectionOffsetBasePos = bw.BaseStream.Position;

                    #region WriteOffset (Default)
                    bw.Write(TPTK_Offset);
                    bw.Write(TPNE_Offset);
                    bw.Write(HPNE_Offset);
                    bw.Write(TPTI_Offset);
                    bw.Write(HPTI_Offset);
                    bw.Write(TPKC_Offset);
                    bw.Write(HPKC_Offset);
                    bw.Write(JBOG_Offset);
                    bw.Write(ITOP_Offset);
                    bw.Write(AERA_Offset);
                    bw.Write(EMAC_Offset);
                    bw.Write(TPGJ_Offset);
                    bw.Write(TPNC_Offset);
                    bw.Write(TPSM_Offset);
                    bw.Write(IGTS_Offset);
                    bw.Write(SROC_Offset);
                    bw.Write(TPLG_Offset);
                    bw.Write(HPLG_Offset);
                    #endregion

                    #region Write
                    TPTK_Offset = (uint)bw.BaseStream.Position;
                    TPTK.WriteTPTK(bw);

                    TPNE_Offset = (uint)bw.BaseStream.Position;
                    TPNE.WriteTPNE(bw);

                    HPNE_Offset = (uint)bw.BaseStream.Position;
                    HPNE.WriteHPNE(bw);

                    TPTI_Offset = (uint)bw.BaseStream.Position;
                    TPTI.WriteTPTI(bw);

                    HPTI_Offset = (uint)bw.BaseStream.Position;
                    HPTI.WriteHPTI(bw);

                    TPKC_Offset = (uint)bw.BaseStream.Position;
                    TPKC.WriteTPKC(bw);

                    HPKC_Offset = (uint)bw.BaseStream.Position;
                    HPKC.WriteHPKC(bw);

                    JBOG_Offset = (uint)bw.BaseStream.Position;
                    JBOG.WriteJBOG(bw, Version);

                    ITOP_Offset = (uint)bw.BaseStream.Position;
                    ITOP.WriteITOP(bw);

                    AERA_Offset = (uint)bw.BaseStream.Position;
                    AERA.WriteAERA(bw);

                    EMAC_Offset = (uint)bw.BaseStream.Position;
                    EMAC.WriteEMAC(bw);

                    TPGJ_Offset = (uint)bw.BaseStream.Position;
                    TPGJ.WriteTPGJ(bw);

                    TPNC_Offset = (uint)bw.BaseStream.Position;
                    TPNC.WriteTPNC(bw);

                    TPSM_Offset = (uint)bw.BaseStream.Position;
                    TPSM.WriteTPSM(bw);

                    IGTS_Offset = (uint)bw.BaseStream.Position;
                    IGTS.WriteIGTS(bw);

                    SROC_Offset = (uint)bw.BaseStream.Position;
                    SROC.WriteSROC(bw);

                    TPLG_Offset = (uint)bw.BaseStream.Position;
                    TPLG.WriteTPLG(bw);

                    HPLG_Offset = (uint)bw.BaseStream.Position;
                    HPLG.WriteHPLG(bw);
                    #endregion

                    //FileSize
                    long FileEndLocation = bw.BaseStream.Position;

                    bw.BaseStream.Position = SectionOffsetBasePos;

                    #region WriteOffset
                    bw.Write(TPTK_Offset - 88);
                    bw.Write(TPNE_Offset - 88);
                    bw.Write(HPNE_Offset - 88);
                    bw.Write(TPTI_Offset - 88);
                    bw.Write(HPTI_Offset - 88);
                    bw.Write(TPKC_Offset - 88);
                    bw.Write(HPKC_Offset - 88);
                    bw.Write(JBOG_Offset - 88);
                    bw.Write(ITOP_Offset - 88);
                    bw.Write(AERA_Offset - 88);
                    bw.Write(EMAC_Offset - 88);
                    bw.Write(TPGJ_Offset - 88);
                    bw.Write(TPNC_Offset - 88);
                    bw.Write(TPSM_Offset - 88);
                    bw.Write(IGTS_Offset - 88);
                    bw.Write(SROC_Offset - 88);
                    bw.Write(TPLG_Offset - 88);
                    bw.Write(HPLG_Offset - 88);
                    #endregion

                    //Reset Position
                    bw.BaseStream.Position = FileEndLocation;
                }

                public KMPSection()
                {
                    TPTK_Offset = 0;
                    TPNE_Offset = 0;
                    HPNE_Offset = 0;
                    TPTI_Offset = 0;
                    HPTI_Offset = 0;
                    TPKC_Offset = 0;
                    HPKC_Offset = 0;
                    JBOG_Offset = 0;
                    ITOP_Offset = 0;
                    AERA_Offset = 0;
                    EMAC_Offset = 0;
                    TPGJ_Offset = 0;
                    TPNC_Offset = 0;
                    TPSM_Offset = 0;
                    IGTS_Offset = 0;
                    SROC_Offset = 0;
                    TPLG_Offset = 0;
                    HPLG_Offset = 0;

                    TPTK = new TPTK_Section();
                    TPNE = new TPNE_Section();
                    HPNE = new HPNE_Section();
                    TPTI = new TPTI_Section();
                    HPTI = new HPTI_Section();
                    TPKC = new TPKC_Section();
                    HPKC = new HPKC_Section();
                    JBOG = new JBOG_Section();
                    ITOP = new ITOP_Section();
                    AERA = new AERA_Section();
                    EMAC = new EMAC_Section();
                    TPGJ = new TPGJ_Section();
                    TPNC = new TPNC_Section();
                    TPSM = new TPSM_Section();
                    IGTS = new IGTS_Section();
                    SROC = new SROC_Section();
                    TPLG = new TPLG_Section();
                    HPLG = new HPLG_Section();
                }
            }

            public void ReadKMP(BinaryReader br)
            {
                DMDCHeader = br.ReadChars(4);
                FileSize = br.ReadUInt32();
                SectionCount = br.ReadUInt16();
                DMDCHeaderSize = br.ReadUInt16();
                VersionNumber = br.ReadUInt32();
                KMP_Section.ReadKMPSection(br, VersionNumber); //3000 : Divide (Unused (?)), 3100 : Normal
            }

            public void WriteKMP(BinaryWriter bw)
            {
                bw.Write(DMDCHeader);
                bw.Write((uint)0); //FileSize (Default)

                bw.Write(SectionCount);
                bw.Write(DMDCHeaderSize);
                bw.Write(VersionNumber);
                KMP_Section.WriteKMPSection(bw, VersionNumber); //3000 : Divide (Unused (?)), 3100 : Normal

                FileSize = (uint)bw.BaseStream.Position;
                bw.BaseStream.Seek(4, SeekOrigin.Begin);
                bw.Write(FileSize);
            }

            public KMPFormat(uint Version = 3100)
            {
                DMDCHeader = "DMDC".ToCharArray();
                FileSize = 0;
                SectionCount = 18;
                DMDCHeaderSize = 88;
                VersionNumber = Version;
                KMP_Section = new KMPSection();
            }
        }

        public class KMPHelper
        {
            public class ByteVector3D
            {
                public byte[] Byte_X { get; set; }
                public byte[] Byte_Y { get; set; }
                public byte[] Byte_Z { get; set; }
            }

            public class ByteArrayToVector3DConverter : KMPHelper
            {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="BVector3D"></param>
                /// <returns></returns>
                public static Vector3D ByteArrayToVector3D(ByteVector3D BVector3D)
                {
                    double Value_X = BitConverter.ToSingle(BVector3D.Byte_X, 0);
                    double Value_Y = BitConverter.ToSingle(BVector3D.Byte_Y, 0);
                    double Value_Z = BitConverter.ToSingle(BVector3D.Byte_Z, 0);

                    return new Vector3D(Value_X, Value_Y, Value_Z);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="BVector3D"></param>
                /// <returns></returns>
                public static Vector3D ByteArrayToVector3D(byte[][] BVector3D)
                {
                    double Value_X = BitConverter.ToSingle(BVector3D[0], 0);
                    double Value_Y = BitConverter.ToSingle(BVector3D[1], 0);
                    double Value_Z = BitConverter.ToSingle(BVector3D[2], 0);

                    return new Vector3D(Value_X, Value_Y, Value_Z);
                }
            }

            public class Vector3DToByteArrayConverter : KMPHelper
            {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="Vector3D"></param>
                /// <returns></returns>
                public static ByteVector3D Vector3DToBVector3D(Vector3D Vector3D)
                {
                    byte[] Byte_X = BitConverter.GetBytes(Convert.ToSingle(Vector3D.X));
                    byte[] Byte_Y = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Y));
                    byte[] Byte_Z = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Z));

                    ByteVector3D BVector3D = new ByteVector3D
                    {
                        Byte_X = Byte_X,
                        Byte_Y = Byte_Y,
                        Byte_Z = Byte_Z
                    };

                    return BVector3D;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="Vector3D"></param>
                /// <returns></returns>
                public static byte[][] Vector3DToByteArray(Vector3D Vector3D)
                {
                    byte[] Byte_X = BitConverter.GetBytes(Convert.ToSingle(Vector3D.X));
                    byte[] Byte_Y = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Y));
                    byte[] Byte_Z = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Z));

                    return new byte[][] { Byte_X, Byte_Y, Byte_Z };
                }
            }

            public class Vector3DTo2DConverter : KMPHelper
            {
                public class ByteVector2D
                {
                    public byte[] Byte_X { get; set; }
                    public byte[] Byte_Y { get; set; }
                }

                /// <summary>
                /// ByteVector2DからVector2に変換
                /// </summary>
                /// <param name="InputBVector2"></param>
                /// <returns>Vector2</returns>
                public static Vector2 BVector2ToVector2D(ByteVector2D InputBVector2)
                {
                    return new Vector2(BitConverter.ToSingle(InputBVector2.Byte_X, 0), BitConverter.ToSingle(InputBVector2.Byte_Y, 0));
                }

                /// <summary>
                /// ByteArrayからVector2に変換
                /// </summary>
                /// <param name="InputByteArray"></param>
                /// <returns>Vector2</returns>
                public static Vector2 ByteArrayToVector2D(byte[][] InputByteArray)
                {
                    return new Vector2(BitConverter.ToSingle(InputByteArray[0], 0), BitConverter.ToSingle(InputByteArray[1], 0));
                }

                /// <summary>
                /// Vector2からByteVector2Dに変換
                /// </summary>
                /// <param name="InputVector2"></param>
                /// <returns>ByteVector2D</returns>
                public static ByteVector2D Vector2ToBVector2D(Vector2 InputVector2)
                {
                    ByteVector2D BVector2D = new ByteVector2D
                    {
                        Byte_X = BitConverter.GetBytes(Convert.ToSingle(InputVector2.X)),
                        Byte_Y = BitConverter.GetBytes(Convert.ToSingle(InputVector2.Y))
                    };

                    return BVector2D;
                }

                /// <summary>
                /// Vector2からByteArrayに変換
                /// </summary>
                /// <param name="InputVector2"></param>
                /// <returns>ByteVector2D</returns>
                public static byte[][] Vector2ToByteArray(Vector2 InputVector2)
                {
                    return new byte[][] { BitConverter.GetBytes(Convert.ToSingle(InputVector2.X)), BitConverter.GetBytes(Convert.ToSingle(InputVector2.Y)) };
                }

                public enum Axis_Up
                {
                    X,
                    Y,
                    Z
                }

                /// <summary>
                /// Vector3DからVector2に変換
                /// </summary>
                /// <param name="InputVector3D"></param>
                /// <param name="AxisToExc"></param>
                /// <returns></returns>
                public static Vector2 Vector3DTo2D(Vector3D InputVector3D, Axis_Up AxisToExc = Axis_Up.Y)
                {
                    Vector2 Position2D = new Vector2();
                    if (AxisToExc == Axis_Up.X)
                    {
                        Position2D = new Vector2(Convert.ToSingle(InputVector3D.Y), Convert.ToSingle(InputVector3D.Z));
                    }
                    if (AxisToExc == Axis_Up.Y)
                    {
                        Position2D = new Vector2(Convert.ToSingle(InputVector3D.X), Convert.ToSingle(InputVector3D.Z));
                    }
                    if (AxisToExc == Axis_Up.Z)
                    {
                        Position2D = new Vector2(Convert.ToSingle(InputVector3D.X), Convert.ToSingle(InputVector3D.Y));
                    }

                    return Position2D;
                }

                /// <summary>
                /// Vector2からVector3Dに変換
                /// </summary>
                /// <param name="InputVector2D"></param>
                /// <param name="UpDirection"></param>
                /// <param name="Height"></param>
                /// <returns></returns>
                public static Vector3D Vector2DTo3D(Vector2 InputVector2D, Axis_Up UpDirection = Axis_Up.Y, double Height = 0)
                {
                    Vector3D Position3D = new Vector3D();
                    if (UpDirection == Axis_Up.X)
                    {
                        Position3D = new Vector3D(Height, Convert.ToSingle(InputVector2D.X), Convert.ToSingle(InputVector2D.Y));
                    }
                    if (UpDirection == Axis_Up.Y)
                    {
                        Position3D = new Vector3D(Convert.ToSingle(InputVector2D.X), Height, Convert.ToSingle(InputVector2D.Y));
                    }
                    if (UpDirection == Axis_Up.Z)
                    {
                        Position3D = new Vector3D(Convert.ToSingle(InputVector2D.X), Convert.ToSingle(InputVector2D.Y), Height);
                    }

                    return Position3D;
                }

                public class CheckpointLR_3D
                {
                    public Vector3D Left { get; set; }
                    public Vector3D Right { get; set; }
                }

                public class CheckpointLR_2D
                {
                    public Vector2 Left { get; set; }
                    public Vector2 Right { get; set; }
                }
            }

            public class ObjFlowReader
            {
                public class Binary
				{
                    public class FBOC
                    {
                        public char[] fboc_Chunk;
                        public short fboc_HeaderSize;
                        public short OBJCount;
                        public List<ObjFlowData> ObjFlow_Data { get; set; }
                        public class ObjFlowData
                        {
                            public byte[] ObjID { get; set; } //オブジェクトID(0x2)
                            public byte[] ColType { get; set; }  //衝突判定(0x2)
                            public byte[] PathType { get; set; }  //パスタイプ(0x2)
                            public short LOD { get; set; }  //LOD(0x2)
                            public int LODHPoly { get; set; }  //LOD1(ハイポリモデルでの数値), 0x4
                            public int LODLPoly { get; set; }  //LOD2(ローポリモデルの数値), 0x4
                            public int LODDef { get; set; }  //LOD(デフォルトの数値), 0x4
                            public byte[] ModelSetting { get; set; }  //モデルの設定(0x2)
                            public short ObjX { get; set; }  //X(スケールさせない場合は[0E 00]となってY、Zに値は入らない)(0x2)
                            public short ObjY { get; set; }  //Y(0x2)
                            public short ObjZ { get; set; }  //Z(0x2)
                            public byte[] Unknown1 { get; set; }  //何も無い?(0x4)
                            public char[] ObjFlowName1 { get; set; }  //Object Name 1
                            public char[] ObjFlowName2 { get; set; }  //Object Name 2
                        }
                    }

                    public static FBOC Read(string Path)
					{
                        System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);

                        FBOC FBOCData = new FBOC
                        {
                            fboc_Chunk = br.ReadChars(4),
                            fboc_HeaderSize = br.ReadInt16(),
                            OBJCount = br.ReadInt16(),
                            ObjFlow_Data = null
                        };

                        if (new string(FBOCData.fboc_Chunk) != "FBOC") throw new Exception("Invalid file.");

                        List<FBOC.ObjFlowData> datas = new List<FBOC.ObjFlowData>();

                        for (int Count = 0; Count < FBOCData.OBJCount; Count++)
                        {
                            FBOC.ObjFlowData ObjFlowData = new FBOC.ObjFlowData
                            {
                                ObjID = br.ReadBytes(2),
                                ColType = br.ReadBytes(2),
                                PathType = br.ReadBytes(2),
                                LOD = br.ReadInt16(),
                                LODHPoly = br.ReadInt32(),
                                LODLPoly = br.ReadInt32(),
                                LODDef = br.ReadInt32(),
                                ModelSetting = br.ReadBytes(2),
                                ObjX = br.ReadInt16(),
                                ObjY = br.ReadInt16(),
                                ObjZ = br.ReadInt16(),
                                Unknown1 = br.ReadBytes(4),
                                ObjFlowName1 = br.ReadChars(64),
                                ObjFlowName2 = br.ReadChars(64)
                            };

                            datas.Add(ObjFlowData);
                        }

                        FBOCData.ObjFlow_Data = datas;

                        br.Close();
                        fs.Close();

                        return FBOCData;
                    }

                    public static void Write(List<FBOC.ObjFlowData> objFlowDatas, string Path)
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.Create, FileAccess.Write);
                        BinaryWriter bw = new BinaryWriter(fs);

                        FBOC FBOCData = new FBOC
                        {
                            fboc_Chunk = new char[] { 'F', 'B', 'O', 'C' },
                            fboc_HeaderSize = 8,
                            OBJCount = Convert.ToInt16(objFlowDatas.Count),
                            ObjFlow_Data = null
                        };

                        bw.Write(FBOCData.fboc_Chunk);
                        bw.Write(FBOCData.fboc_HeaderSize);
                        bw.Write(FBOCData.OBJCount);

                        for (int Count = 0; Count < objFlowDatas.Count; Count++)
                        {
                            #region OBJName1
                            char[] OBJ_NAME1 = objFlowDatas[Count].ObjFlowName1;
                            int OBJ_NAME1_ZEROPadding = 64 - OBJ_NAME1.Length;
                            char[] ZEROPaddingChars1 = new char[OBJ_NAME1_ZEROPadding];
                            for (int p1 = 0; p1 < OBJ_NAME1_ZEROPadding; p1++)
                            {
                                ZEROPaddingChars1[p1] = '\0';
                            }

                            char[] OBJNameChar1 = OBJ_NAME1.Concat(ZEROPaddingChars1).ToArray();
                            #endregion

                            #region OBJName2
                            char[] OBJ_NAME2 = objFlowDatas[Count].ObjFlowName2;
                            int OBJ_NAME2_ZEROPadding = 64 - OBJ_NAME2.Length;
                            char[] ZEROPaddingChars2 = new char[OBJ_NAME2_ZEROPadding];
                            for (int p2 = 0; p2 < OBJ_NAME2_ZEROPadding; p2++)
                            {
                                ZEROPaddingChars2[p2] = '\0';
                            }

                            char[] OBJNameChar2 = OBJ_NAME2.Concat(ZEROPaddingChars2).ToArray();
                            #endregion

                            bw.Write(objFlowDatas[Count].ObjID);
                            bw.Write(objFlowDatas[Count].ColType);
                            bw.Write(objFlowDatas[Count].PathType);
                            bw.Write(objFlowDatas[Count].LOD);
                            bw.Write(objFlowDatas[Count].LODHPoly);
                            bw.Write(objFlowDatas[Count].LODLPoly);
                            bw.Write(objFlowDatas[Count].LODDef);
                            bw.Write(objFlowDatas[Count].ModelSetting);
                            bw.Write(objFlowDatas[Count].ObjX);
                            bw.Write(objFlowDatas[Count].ObjY);
                            bw.Write(objFlowDatas[Count].ObjZ);
                            bw.Write(objFlowDatas[Count].Unknown1);
                            bw.Write(objFlowDatas[Count].ObjFlowName1);
                            bw.Write(objFlowDatas[Count].ObjFlowName2);
                        }

                        bw.Close();
                        fs.Close();
                    }
                }

                public class Xml
				{
                    public class ObjFlowDB
                    {
                        public string ObjectID { get; set; }
                        public string ObjectName { get; set; }
                        public string Path { get; set; }
                        public bool UseKCL { get; set; }
                        public string ObjectType { get; set; }

                        public Common Commons { get; set; }
                        public class Common
                        {
                            public string ColType { get; set; }
                            public string PathType { get; set; }
                            public string ModelSetting { get; set; }
                            public string Unknown1 { get; set; }
                        }

                        public LOD_Setting LODSetting { get; set; }
                        public class LOD_Setting
                        {
                            public int LOD { get; set; }
                            public int LODHPoly { get; set; }
                            public int LODLPoly { get; set; }
                            public int LODDef { get; set; }
                        }

                        public Scale Scales { get; set; }
                        public class Scale
                        {
                            public int X { get; set; }
                            public int Y { get; set; }
                            public int Z { get; set; }
                        }

                        public Name Names { get; set; }
                        public class Name
                        {
                            public string Main { get; set; }
                            public string Sub { get; set; }
                        }

                        public Default_Values DefaultValues { get; set; }
                        public class Default_Values
                        {
                            public List<Value> Values { get; set; }
                            public class Value
                            {
                                public int DefaultObjectValue { get; set; }
                                public string Description { get; set; }
                            }
                        }
                    }

                    public static Dictionary<string[], string> ObjFlowMdlPathDictionary(List<ObjFlowDB> ObjFlowDataXml, string Path)
                    {
                        //指定したディレクトリの中にあるファイルパスを全て取得
                        string[] PathAry = System.IO.Directory.GetFiles(Path, "*.obj", System.IO.SearchOption.AllDirectories);

                        Dictionary<string[], string> ObjFlowDicts = new Dictionary<string[], string>();

                        foreach (var ObjFlowValue in ObjFlowDataXml.Select((item, index) => new { item, index }))
                        {
                            //Search the path of the corresponding model from PathAry(string[])
                            if (PathAry.Contains(ObjFlowValue.item.Path))
                            {
                                //Get ObjectID
                                string ObjectID = ObjFlowDataXml.Find(x => x.Path == ObjFlowValue.item.Path).ObjectID ?? "";
                                ObjFlowDicts.Add(new string[] { ObjFlowValue.item.ObjectName, ObjectID }, ObjFlowValue.item.Path);
                            }
                        }

                        return ObjFlowDicts;
                    }

                    //Create Xml
                    public static void CreateXml(List<ObjFlowDB> ObjFlowVal_List, string KMPObjectFolderPath, string DefaultModelPath, string XmlPath)
                    {
                        string[] PathAry = System.IO.Directory.GetFiles(KMPObjectFolderPath, "*.obj", System.IO.SearchOption.AllDirectories);

                        TestXml.KMPObjFlowDataXml kMPObjFlowDataXml = new TestXml.KMPObjFlowDataXml
                        {
                            ObjFlows = null
                        };

                        List<TestXml.KMPObjFlowDataXml.ObjFlow> ObjFlowList = new List<TestXml.KMPObjFlowDataXml.ObjFlow>();

                        foreach (var ObjFlowValue in ObjFlowVal_List.Select((item, index) => new { item, index }))
                        {
                            string MDLPath = "";

                            //Search the path of the corresponding model from PathAry(string[])
                            if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.Names.Main + ".obj"))
                            {
                                MDLPath = KMPObjectFolderPath + "\\" + ObjFlowValue.item.Names.Main + ".obj";
                            }
                            if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.Names.Main + ".obj") == false)
                            {
                                MDLPath = DefaultModelPath;
                            }

                            TestXml.KMPObjFlowDataXml.ObjFlow objFlow = new TestXml.KMPObjFlowDataXml.ObjFlow
                            {
                                ObjectID = ObjFlowValue.item.ObjectID,
                                ObjectName = ObjFlowValue.item.ObjectName,
                                Path = MDLPath,
                                UseKCL = false,
                                ObjectType = "Unknown",
                                Commons = new TestXml.KMPObjFlowDataXml.ObjFlow.Common
                                {
                                    ColType = ObjFlowValue.item.Commons.ColType,
                                    PathType = ObjFlowValue.item.Commons.PathType,
                                    ModelSetting = ObjFlowValue.item.Commons.ModelSetting,
                                    Unknown1 = ObjFlowValue.item.Commons.Unknown1
                                },
                                LODSetting = new TestXml.KMPObjFlowDataXml.ObjFlow.LOD_Setting
                                {
                                    LOD = ObjFlowValue.item.LODSetting.LOD,
                                    LODHPoly = ObjFlowValue.item.LODSetting.LODHPoly,
                                    LODLPoly = ObjFlowValue.item.LODSetting.LODLPoly,
                                    LODDef = ObjFlowValue.item.LODSetting.LODDef
                                },
                                Scales = new TestXml.KMPObjFlowDataXml.ObjFlow.Scale
                                {
                                    X = ObjFlowValue.item.Scales.X,
                                    Y = ObjFlowValue.item.Scales.Y,
                                    Z = ObjFlowValue.item.Scales.Z
                                },
                                Names = new TestXml.KMPObjFlowDataXml.ObjFlow.Name
                                {
                                    Main = ObjFlowValue.item.Names.Main,
                                    Sub = ObjFlowValue.item.Names.Sub
                                },
                                DefaultValues = new TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values
                                {
                                    Values = null
                                }
                            };

                            #region Values
                            List<TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value> ValuesList = new List<TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value>();

                            for (int i = 0; i < 8; i++)
                            {
                                TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value value = new TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value
                                {
                                    DefaultObjectValue = 0,
                                    Description = "Test " + i
                                };

                                ValuesList.Add(value);
                            }

                            objFlow.DefaultValues.Values = ValuesList;
                            #endregion

                            ObjFlowList.Add(objFlow);
                        }

                        kMPObjFlowDataXml.ObjFlows = ObjFlowList;

                        //Delete Namespaces
                        var xns = new XmlSerializerNamespaces();
                        xns.Add(string.Empty, string.Empty);

                        System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.KMPObjFlowDataXml));
                        System.IO.StreamWriter sw = new StreamWriter(XmlPath, false, new System.Text.UTF8Encoding(false));
                        serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                        sw.Close();
                    }

                    //Read ObjFlowData.xml
                    public static List<ObjFlowDB> ReadObjFlowXml(string Path)
                    {
                        System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
                        System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPObjFlowDataXml));
                        TestXml.KMPObjFlowDataXml ObjFlowXml = (TestXml.KMPObjFlowDataXml)s1.Deserialize(fs1);

                        List<ObjFlowDB> ObjFlowXml_List = new List<ObjFlowDB>();

                        foreach (var ObjFlowData in ObjFlowXml.ObjFlows)
                        {
                            ObjFlowDB objFlow = new ObjFlowDB
                            {
                                ObjectID = ObjFlowData.ObjectID,
                                ObjectName = ObjFlowData.ObjectName,
                                Path = ObjFlowData.Path,
                                UseKCL = ObjFlowData.UseKCL,
                                ObjectType = ObjFlowData.ObjectType,
                                Commons = new ObjFlowDB.Common
                                {
                                    ColType = ObjFlowData.Commons.ColType,
                                    PathType = ObjFlowData.Commons.PathType,
                                    ModelSetting = ObjFlowData.Commons.ModelSetting,
                                    Unknown1 = ObjFlowData.Commons.Unknown1
                                },
                                LODSetting = new ObjFlowDB.LOD_Setting
                                {
                                    LOD = ObjFlowData.LODSetting.LOD,
                                    LODHPoly = ObjFlowData.LODSetting.LODHPoly,
                                    LODLPoly = ObjFlowData.LODSetting.LODLPoly,
                                    LODDef = ObjFlowData.LODSetting.LODDef
                                },
                                Scales = new ObjFlowDB.Scale
                                {
                                    X = ObjFlowData.Scales.X,
                                    Y = ObjFlowData.Scales.Y,
                                    Z = ObjFlowData.Scales.Z
                                },
                                Names = new ObjFlowDB.Name
                                {
                                    Main = ObjFlowData.Names.Main,
                                    Sub = ObjFlowData.Names.Sub
                                },
                                DefaultValues = new ObjFlowDB.Default_Values
                                {
                                    Values = null
                                }
                            };

                            List<ObjFlowDB.Default_Values.Value> valueList = new List<ObjFlowDB.Default_Values.Value>();

                            foreach (var ObjFlowDataValue in ObjFlowData.DefaultValues.Values)
                            {
                                ObjFlowDB.Default_Values.Value value = new ObjFlowDB.Default_Values.Value
                                {
                                    DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
                                    Description = ObjFlowDataValue.Description
                                };

                                valueList.Add(value);
                            }

                            objFlow.DefaultValues.Values = valueList;

                            ObjFlowXml_List.Add(objFlow);
                        }

                        fs1.Close();
                        fs1.Dispose();

                        return ObjFlowXml_List;
                    }

                    //Write ObjFlowData.xml
                    public static void WriteObjFlowXml(List<ObjFlowDB> ObjFlowDBList, string Path)
                    {
                        TestXml.KMPObjFlowDataXml kMPObjFlowDataXml = new TestXml.KMPObjFlowDataXml
                        {
                            ObjFlows = null
                        };

                        List<TestXml.KMPObjFlowDataXml.ObjFlow> ObjFlowList = new List<TestXml.KMPObjFlowDataXml.ObjFlow>();

                        foreach (var ObjFlowValue in ObjFlowDBList.Select((item, index) => new { item, index }))
                        {
                            TestXml.KMPObjFlowDataXml.ObjFlow objFlow = new TestXml.KMPObjFlowDataXml.ObjFlow
                            {
                                ObjectID = ObjFlowValue.item.ObjectID,
                                ObjectName = ObjFlowValue.item.ObjectName,
                                Path = ObjFlowValue.item.Path,
                                UseKCL = ObjFlowValue.item.UseKCL,
                                ObjectType = ObjFlowValue.item.ObjectType,
                                Commons = new TestXml.KMPObjFlowDataXml.ObjFlow.Common
                                {
                                    ColType = ObjFlowValue.item.Commons.ColType,
                                    PathType = ObjFlowValue.item.Commons.PathType,
                                    ModelSetting = ObjFlowValue.item.Commons.ModelSetting,
                                    Unknown1 = ObjFlowValue.item.Commons.Unknown1
                                },
                                LODSetting = new TestXml.KMPObjFlowDataXml.ObjFlow.LOD_Setting
                                {
                                    LOD = ObjFlowValue.item.LODSetting.LOD,
                                    LODHPoly = ObjFlowValue.item.LODSetting.LODHPoly,
                                    LODLPoly = ObjFlowValue.item.LODSetting.LODLPoly,
                                    LODDef = ObjFlowValue.item.LODSetting.LODDef
                                },
                                Scales = new TestXml.KMPObjFlowDataXml.ObjFlow.Scale
                                {
                                    X = Convert.ToInt32(ObjFlowValue.item.Scales.X),
                                    Y = Convert.ToInt32(ObjFlowValue.item.Scales.Y),
                                    Z = Convert.ToInt32(ObjFlowValue.item.Scales.Z)
                                },
                                Names = new TestXml.KMPObjFlowDataXml.ObjFlow.Name
                                {
                                    Main = ObjFlowValue.item.Names.Main,
                                    Sub = ObjFlowValue.item.Names.Sub
                                },
                                DefaultValues = new TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values
                                {
                                    Values = null
                                }
                            };

                            #region Values
                            List<TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value> ValuesList = new List<TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value>();

                            foreach (var i in ObjFlowValue.item.DefaultValues.Values)
                            {
                                TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value value = new TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value
                                {
                                    DefaultObjectValue = i.DefaultObjectValue,
                                    Description = i.Description
                                };

                                ValuesList.Add(value);
                            }

                            objFlow.DefaultValues.Values = ValuesList;
                            #endregion

                            ObjFlowList.Add(objFlow);
                        }

                        kMPObjFlowDataXml.ObjFlows = ObjFlowList;

                        //Delete Namespaces
                        var xns = new XmlSerializerNamespaces();
                        xns.Add(string.Empty, string.Empty);

                        System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.KMPObjFlowDataXml));
                        System.IO.StreamWriter sw = new StreamWriter(Path, false, new System.Text.UTF8Encoding(false));
                        serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                        sw.Close();
                    }
                }

				public class ConvertTo
				{
					public static ObjFlowReader.Binary.FBOC ToFBOC(List<Xml.ObjFlowDB> ObjFlowDataXml_List)
					{
                        Binary.FBOC FBOCFormat = new Binary.FBOC
                        {
                            fboc_Chunk = new char[] { 'F', 'B', 'O', 'C' },
                            fboc_HeaderSize = 8,
                            OBJCount = 0,
                            ObjFlow_Data = null
                        };

                        List<Binary.FBOC.ObjFlowData> objFlowDatas = new List<Binary.FBOC.ObjFlowData>();

						for (int Count = 0; Count < ObjFlowDataXml_List.Count; Count++)
						{
							#region OBJName1
							char[] OBJ_NAME1 = ObjFlowDataXml_List[Count].Names.Main.ToCharArray();
							int OBJ_NAME1_ZEROPadding = 64 - OBJ_NAME1.Length;
							char[] ZEROPaddingChars1 = new char[OBJ_NAME1_ZEROPadding];
							for (int p1 = 0; p1 < OBJ_NAME1_ZEROPadding; p1++)
							{
								ZEROPaddingChars1[p1] = '\0';
							}

							char[] OBJNameChar1 = OBJ_NAME1.Concat(ZEROPaddingChars1).ToArray();
							#endregion

							#region OBJName2
							char[] OBJ_NAME2 = ObjFlowDataXml_List[Count].Names.Sub.ToCharArray();
							int OBJ_NAME2_ZEROPadding = 64 - OBJ_NAME2.Length;
							char[] ZEROPaddingChars2 = new char[OBJ_NAME2_ZEROPadding];
							for (int p2 = 0; p2 < OBJ_NAME2_ZEROPadding; p2++)
							{
								ZEROPaddingChars2[p2] = '\0';
							}

							char[] OBJNameChar2 = OBJ_NAME2.Concat(ZEROPaddingChars2).ToArray();
							#endregion

							Binary.FBOC.ObjFlowData ObjFlowData = new Binary.FBOC.ObjFlowData
							{
								ObjID = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].ObjectID).Reverse().ToArray(),
								ColType = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].Commons.ColType).Reverse().ToArray(),
								PathType = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].Commons.PathType).Reverse().ToArray(),
								LOD = (short)ObjFlowDataXml_List[Count].LODSetting.LOD,
								LODHPoly = (short)ObjFlowDataXml_List[Count].LODSetting.LODHPoly,
								LODLPoly = (short)ObjFlowDataXml_List[Count].LODSetting.LODLPoly,
								LODDef = (short)ObjFlowDataXml_List[Count].LODSetting.LODDef,
								ModelSetting = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].Commons.ModelSetting).Reverse().ToArray(),
								ObjX = (short)ObjFlowDataXml_List[Count].Scales.X,
								ObjY = (short)ObjFlowDataXml_List[Count].Scales.Y,
								ObjZ = (short)ObjFlowDataXml_List[Count].Scales.Z,
								Unknown1 = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].Commons.Unknown1).Reverse().ToArray(),
								ObjFlowName1 = OBJNameChar1,
								ObjFlowName2 = OBJNameChar2
							};

                            objFlowDatas.Add(ObjFlowData);
						}

                        FBOCFormat.ObjFlow_Data = objFlowDatas;
                        FBOCFormat.OBJCount = Convert.ToInt16(objFlowDatas.Count);

                        return FBOCFormat;
                    }

                    public static List<Xml.ObjFlowDB> ToObjFlowDB(ObjFlowReader.Binary.FBOC FBOCData)
					{
						List<Xml.ObjFlowDB> ObjFlowDataXmlList = new List<Xml.ObjFlowDB>();

						for (int Count = 0; Count < FBOCData.OBJCount; Count++)
						{
                            Xml.ObjFlowDB ObjFlowDatabase = new Xml.ObjFlowDB
                            {
                                ObjectID = BitConverter.ToString(FBOCData.ObjFlow_Data[Count].ObjID.Reverse().ToArray()).Replace("-", string.Empty),
                                ObjectName = new string(FBOCData.ObjFlow_Data[Count].ObjFlowName1).Replace("\0", ""),
                                Path = "",
                                UseKCL = false,
                                ObjectType = "NaN",
                                Commons = new Xml.ObjFlowDB.Common
                                {
                                    ColType = BitConverter.ToString(FBOCData.ObjFlow_Data[Count].ColType.Reverse().ToArray()).Replace("-", string.Empty),
                                    PathType = BitConverter.ToString(FBOCData.ObjFlow_Data[Count].PathType.Reverse().ToArray()).Replace("-", string.Empty),
                                    ModelSetting = BitConverter.ToString(FBOCData.ObjFlow_Data[Count].ModelSetting.Reverse().ToArray()).Replace("-", string.Empty),
                                    Unknown1 = BitConverter.ToString(FBOCData.ObjFlow_Data[Count].Unknown1.Reverse().ToArray(), 0).Replace("-", string.Empty)
                                },
                                LODSetting = new Xml.ObjFlowDB.LOD_Setting
                                {
                                    LOD = FBOCData.ObjFlow_Data[Count].LOD,
                                    LODHPoly = FBOCData.ObjFlow_Data[Count].LODHPoly,
                                    LODLPoly = FBOCData.ObjFlow_Data[Count].LODLPoly,
                                    LODDef = FBOCData.ObjFlow_Data[Count].LODDef
                                },
                                Scales = new Xml.ObjFlowDB.Scale
                                {
                                    X = FBOCData.ObjFlow_Data[Count].ObjX,
                                    Y = FBOCData.ObjFlow_Data[Count].ObjY,
                                    Z = FBOCData.ObjFlow_Data[Count].ObjZ
                                },
                                Names = new Xml.ObjFlowDB.Name
                                {
                                    Main = new string(FBOCData.ObjFlow_Data[Count].ObjFlowName1).Replace("\0", ""),
                                    Sub = new string(FBOCData.ObjFlow_Data[Count].ObjFlowName2).Replace("\0", "")
                                },
                                DefaultValues = new Xml.ObjFlowDB.Default_Values
                                {
                                    Values = new List<Xml.ObjFlowDB.Default_Values.Value>()
                                }
                            };

                            List<Xml.ObjFlowDB.Default_Values.Value> valueList = new List<Xml.ObjFlowDB.Default_Values.Value>();

                            foreach (var ObjFlowDataValue in ObjFlowDatabase.DefaultValues.Values)
                            {
                                Xml.ObjFlowDB.Default_Values.Value value = new Xml.ObjFlowDB.Default_Values.Value
                                {
                                    DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
                                    Description = ObjFlowDataValue.Description
                                };

                                valueList.Add(value);
                            }

                            ObjFlowDatabase.DefaultValues.Values = valueList;

                            ObjFlowDataXmlList.Add(ObjFlowDatabase);
                        }

                        return ObjFlowDataXmlList;
					}
				}
			}

            public class Byte2StringConverter
            {
				public static byte[] ToByteArray(string InputString)
				{
                    List<byte> Str2byte = new List<byte>();
                    for (int i = 0; i < InputString.Length / 2; i++) Str2byte.Add(Convert.ToByte(InputString.Substring(i * 2, 2), 16));
					return Str2byte.ToArray();
				}

				public static byte[] OBJIDStrToByteArray(string InputString_OBJID)
                {
                    string w1 = InputString_OBJID.Substring(0, 2);
                    string w2 = InputString_OBJID.Substring(2);

                    string[] SplitStr = new string[] { w2, w1 };
                    byte[] Str2byte = new byte[SplitStr.Length];
                    for (int n = 0; n < SplitStr.Length; n++)
                    {
                        byte b = byte.Parse(SplitStr[n], System.Globalization.NumberStyles.AllowHexSpecifier);
                        Str2byte[n] = b;
                    }

                    return Str2byte;
                }
            }

            public class FlagConverter : KMPHelper
            {
                public class EnemyRoute
                {
                    #region RouteSetting(I'm using the code in "KMPExpander-master\KMPExpander\Class\SimpleKMPs\EnemyRoutes.cs" of "KMP Expander")
                    public byte Flags { get; set; }
                    public bool WideTurn
                    {
                        get
                        {
                            return (Flags & 0x1) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 0)) | ((value ? 1 : 0) << 0));
                        }
                    }

                    public bool NormalTurn
                    {
                        get
                        {
                            return (Flags & 0x4) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 2)) | ((value ? 1 : 0) << 2));
                        }
                    }

                    public bool SharpTurn
                    {
                        get
                        {
                            return (Flags & 0x10) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 4)) | ((value ? 1 : 0) << 4));
                        }
                    }

                    public bool TricksForbidden
                    {
                        get
                        {
                            return (Flags & 0x8) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 3)) | ((value ? 1 : 0) << 3));
                        }
                    }

                    public bool StickToRoute
                    {
                        get
                        {
                            return (Flags & 0x40) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 6)) | ((value ? 1 : 0) << 6));
                        }
                    }

                    public bool BouncyMushSection
                    {
                        get
                        {
                            return (Flags & 0x20) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 5)) | ((value ? 1 : 0) << 5));
                        }
                    }

                    public bool ForceDefaultSpeed
                    {
                        get
                        {
                            return (Flags & 0x80) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 7)) | ((value ? 1 : 0) << 7));
                        }
                    }

                    public bool UnknownFlag
                    {
                        get
                        {
                            return (Flags & 0x2) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 1)) | ((value ? 1 : 0) << 1));
                        }
                    }
                    #endregion

                    public enum FlagType
                    {
                        WideTurn,
                        NormalTurn,
                        SharpTurn,
                        TricksForbidden,
                        StickToRoute,
                        BouncyMushSection,
                        ForceDefaultSpeed,
                        NoPathSwitch
                    }

                    public bool ConvertFlags(byte InputFlags, FlagType flagType)
                    {
                        Flags = InputFlags;

                        bool FlagValue = new bool();
                        if (flagType == FlagType.WideTurn)
                        {
                            FlagValue = WideTurn;
                        }
                        if (flagType == FlagType.NormalTurn)
                        {
                            FlagValue = NormalTurn;
                        }
                        if (flagType == FlagType.SharpTurn)
                        {
                            FlagValue = SharpTurn;
                        }
                        if (flagType == FlagType.TricksForbidden)
                        {
                            FlagValue = TricksForbidden;
                        }
                        if (flagType == FlagType.StickToRoute)
                        {
                            FlagValue = StickToRoute;
                        }
                        if (flagType == FlagType.BouncyMushSection)
                        {
                            FlagValue = BouncyMushSection;
                        }
                        if (flagType == FlagType.ForceDefaultSpeed)
                        {
                            FlagValue = ForceDefaultSpeed;
                        }
                        if (flagType == FlagType.NoPathSwitch)
                        {
                            FlagValue = UnknownFlag;
                        }

                        return FlagValue;
                    }
                }

                public class GlideRoute
                {
                    #region RouteSetting(I'm using the code in "KMPExpander-master\KMPExpander\Class\SimpleKMPs\GliderRoutes.cs" of "KMP Expander")
                    public uint RouteSettings { get; set; }
                    public bool ForceToRoute
                    {
                        get
                        {
                            return (RouteSettings & 0xFF) != 0;
                        }
                        set
                        {
                            RouteSettings = (RouteSettings & ~0xFFu) | (value ? 1u : 0u);
                        }
                    }

                    public bool CannonSection
                    {
                        get
                        {
                            return (RouteSettings & 0xFF00) != 0;
                        }
                        set
                        {
                            RouteSettings = (RouteSettings & ~0xFF00u) | (value ? 1u : 0u) << 8;
                        }
                    }

                    public bool PreventRaising
                    {
                        get
                        {
                            return (RouteSettings & 0xFF0000) != 0;
                        }
                        set
                        {
                            RouteSettings = (RouteSettings & ~0xFF0000u) | (value ? 1u : 0u) << 16;
                        }
                    }
                    #endregion

                    public enum FlagType
                    {
                        ForceToRoute,
                        CannonSection,
                        PreventRaising
                    }

                    public bool ConvertFlags(uint InputFlags, FlagType flagType)
                    {
                        RouteSettings = InputFlags;

                        bool FlagValue = new bool();
                        if (flagType == FlagType.ForceToRoute)
                        {
                            FlagValue = ForceToRoute;
                        }
                        if (flagType == FlagType.CannonSection)
                        {
                            FlagValue = CannonSection;
                        }
                        if (flagType == FlagType.PreventRaising)
                        {
                            FlagValue = PreventRaising;
                        }

                        return FlagValue;
                    }
                }
            }

            public class KMPValueTypeConverter : KMPHelper
            {
                public class EnemyRoute
                {
                    public enum MushSetting
                    {
                        CanUseMushroom = 0,
                        NeedsMushroom = 1,
                        CannotUseMushroom = 2,
                        Unknown
                    }

                    public static MushSetting MushSettingType(ushort Value)
                    {
                        MushSetting mushSetting;
                        if (Value > 2)
                        {
                            mushSetting = MushSetting.Unknown;
                        }
                        else
                        {
                            mushSetting = (MushSetting)Value;
                        }

                        return mushSetting;
                    }

                    public enum DriftSetting
                    {
                        AllowDrift_AllowMiniturbo,
                        DisallowDrift_AllowMiniturbo,
                        DisallowDrift_DisallowMiniturbo,
                        Unknown
                    }

                    public static DriftSetting DriftSettingType(ushort Value)
                    {
                        DriftSetting driftSetting;
                        if (Value > 2)
                        {
                            driftSetting = DriftSetting.Unknown;
                        }
                        else
                        {
                            driftSetting = (DriftSetting)Value;
                        }

                        return driftSetting;
                    }

                    public enum PathFindOption
                    {
                        Taken_under_unknown_flag2 = -4,
                        Taken_under_unknown_flag1 = -3,
                        Bullet_cannot_find = -2,
                        CPU_Racer_cannot_find = -1,
                        No_restrictions = 0,
                        Unknown
                    }

                    public static PathFindOption PathFindOptionType(short Value)
                    {
                        PathFindOption pathFindOption;
                        if (Value > 0 || Value < -4)
                        {
                            pathFindOption = PathFindOption.Unknown;
                        }
                        else
                        {
                            pathFindOption = (PathFindOption)Value;
                        }

                        return pathFindOption;
                    }

                    public enum MaxSearchYOffsetOption
                    {
                        Limited_offset_MinusOne = -1,
                        No_limited_offset = 0,
                        Limited_offset
                    }

                    public static MaxSearchYOffsetOption MaxSearchYOffsetOptionType(short Value)
                    {
                        MaxSearchYOffsetOption maxSearchYOffsetOption;
                        if (Value < 0)
                        {
                            maxSearchYOffsetOption = MaxSearchYOffsetOption.Limited_offset_MinusOne;
                        }
                        else if(Value > 0)
                        {
                            maxSearchYOffsetOption = MaxSearchYOffsetOption.Limited_offset;
                        }
                        else
                        {
                            maxSearchYOffsetOption = MaxSearchYOffsetOption.No_limited_offset;
                        }

                        return maxSearchYOffsetOption;
                    }
                }

                public class ItemRoute
                {
                    public enum GravityMode
                    {
                        Affected_By_Gravity = 0,
                        Unaffected_By_Gravity = 1,
                        Cannon_Section = 2,
                        Unknown
                    }

                    public static GravityMode GravityModeType(ushort Value)
                    {
                        GravityMode gravityMode;
                        if (Value > 2)
                        {
                            gravityMode = GravityMode.Unknown;
                        }
                        else
                        {
                            gravityMode = (GravityMode)Value;
                        }

                        return gravityMode;
                    }

                    public enum PlayerScanRadius
                    {
                        Small = 0,
                        Big = 1,
                        Unknown
                    }

                    public static PlayerScanRadius PlayerScanRadiusType(ushort Value)
                    {
                        PlayerScanRadius playerScanRadius;
                        if (Value > 1)
                        {
                            playerScanRadius = PlayerScanRadius.Unknown;
                        }
                        else
                        {
                            playerScanRadius = (PlayerScanRadius)Value;
                        }

                        return playerScanRadius;
                    }
                }

                public class Area
                {
                    public enum AreaMode
                    {
                        Box = 0,
                        Cylinder = 1,
                        Unknown
                    }

                    public static AreaMode AreaModes(byte Value)
                    {
                        AreaMode areaMode;
                        if (Value > 1)
                        {
                            areaMode = AreaMode.Unknown;
                        }
                        else
                        {
                            areaMode = (AreaMode)Value;
                        }

                        return areaMode;

                    }
                }
            }
        }
    }
}
