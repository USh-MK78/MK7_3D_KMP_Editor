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
                    HTK_3DES.TSRSystem.Transform_Value StartPosition_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = TPTK.TPTKValue_List[i].TPTK_Position.X,
                            Y = TPTK.TPTKValue_List[i].TPTK_Position.Y,
                            Z = TPTK.TPTKValue_List[i].TPTK_Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 20,
                            Y = 20,
                            Z = 20
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = TPTK.TPTKValue_List[i].TPTK_Rotation.X,
                            Y = TPTK.TPTKValue_List[i].TPTK_Rotation.Y,
                            Z = TPTK.TPTKValue_List[i].TPTK_Rotation.Z
                        }
                    };

                    ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + i + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_StartPositionOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(StartPosition_transform_Value, transformSetting);

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
                        HTK_3DES.TSRSystem.Transform_Value EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].TPNE_Position.X,
                                Y = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].TPNE_Position.Y,
                                Z = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].TPNE_Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Control * 100,
                                Y = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Control * 100,
                                Z = TPNE.TPNEValue_List[Count + HPNE.HPNEValue_List[i].HPNE_StartPoint].Control * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + Count + " " + i);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_EnemyPathOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(EnemyPoint_transform_Value, transformSetting);

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
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.EnemyRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.EnemyRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Orange);
                }
                #endregion
            }

            public static void Render_ItemRoute(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.HPTI_Section HPTI, KMPs.KMPFormat.KMPSection.TPTI_Section TPTI)
            {
                for (int HPTICount = 0; HPTICount < HPTI.NumOfEntries; HPTICount++)
                {
                    //Rail
                    HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                    for (int Count = 0; Count < HPTI.HPTIValue_List[HPTICount].HPTI_Length; Count++)
                    {
                        HTK_3DES.TSRSystem.Transform_Value ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_Position.X,
                                Y = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_Position.Y,
                                Z = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_PointSize * 100,
                                Y = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_PointSize * 100,
                                Z = TPTI.TPTIValue_List[Count + HPTI.HPTIValue_List[HPTICount].HPTI_StartPoint].TPTI_PointSize * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + Count + " " + HPTICount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_ItemPathOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(ItemPoint_transform_Value, transformSetting);

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
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.ItemRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.ItemRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Green);
                }
                #endregion
            }

            public static void Render_Checkpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.HPKC_Section HPKC, KMPs.KMPFormat.KMPSection.TPKC_Section TPKC, double CheckpointYOffset)
            {
                for (int HPKCCount = 0; HPKCCount < HPKC.NumOfEntries; HPKCCount++)
                {
                    //Checkpoint_Rails
                    HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = new HTK_3DES.KMP_3DCheckpointSystem.Checkpoint
                    {
                        Checkpoint_Left = new HTK_3DES.PathTools.Rail(),
                        Checkpoint_Right = new HTK_3DES.PathTools.Rail(),
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

                    for (int Count = 0; Count < HPKC.HPKCValue_List[HPKCCount].HPKC_Length; Count++)
                    {
                        #region Create
                        var P2D_Left = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Left;
                        Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                        Point3D P3DLeft = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DLeftToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                        P3DLeft.Y = CheckpointYOffset;

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

                        ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + Count + " " + HPKCCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting_P2DLeft = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_CheckpointLeftOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(P2DLeft_transform_Value, transformSetting_P2DLeft);

                        checkpoint.Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                        HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointLeftOBJ);
                        #endregion

                        var P2D_Right = TPKC.TPKCValue_List[Count + HPKC.HPKCValue_List[HPKCCount].HPKC_StartPoint].TPKC_2DPosition_Right;
                        Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                        Point3D P3DRight = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DRightToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                        P3DRight.Y = CheckpointYOffset;

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

                        ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + Count + " " + HPKCCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting_P2DRight = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_CheckpointRightOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(P2DRight_transform_Value, transformSetting_P2DRight);

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
                    List<Point3D> point3Ds_Left = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.MV3D_List);
                    kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.LV3D_List = HTK_3DES.PathTools.DrawPath_Line(UserCtrl, point3Ds_Left, 5, Colors.Green);

                    kMPViewportObject.Checkpoint_Rail[i].SideWall_Left.SideWallList = HTK_3DES.PathTools.DrawPath_SideWall(UserCtrl, point3Ds_Left, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00));

                    List<Point3D> point3Ds_Right = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.MV3D_List);
                    kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.LV3D_List = HTK_3DES.PathTools.DrawPath_Line(UserCtrl, point3Ds_Right, 5, Colors.Red);

                    kMPViewportObject.Checkpoint_Rail[i].SideWall_Right.SideWallList = HTK_3DES.PathTools.DrawPath_SideWall(UserCtrl, point3Ds_Right, System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                }
                #endregion
            }

            public static void Render_Object(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.JBOG_Section JBOG, KMPs.KMPHelper.ObjFlowReader.ObjFlowXmlToObject objFlowXmlToObject)
            {
                for (int Count = 0; Count < JBOG.NumOfEntries; Count++)
                {
                    HTK_3DES.TSRSystem.Transform_Value OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = JBOG.JBOGValue_List[Count].JBOG_Position.X,
                            Y = JBOG.JBOGValue_List[Count].JBOG_Position.Y,
                            Z = JBOG.JBOGValue_List[Count].JBOG_Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = JBOG.JBOGValue_List[Count].JBOG_Scale.X * 2,
                            Y = JBOG.JBOGValue_List[Count].JBOG_Scale.Y * 2,
                            Z = JBOG.JBOGValue_List[Count].JBOG_Scale.Z * 2
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = JBOG.JBOGValue_List[Count].JBOG_Rotation.X,
                            Y = JBOG.JBOGValue_List[Count].JBOG_Rotation.Y,
                            Z = JBOG.JBOGValue_List[Count].JBOG_Rotation.Z
                        }
                    };

                    string Path = objFlowXmlToObject.ObjFlows.Find(x => x.ObjectID == BitConverter.ToString(JBOG.JBOGValue_List[Count].ObjectID.Reverse().ToArray()).Replace("-", string.Empty)).Path;
                    ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(Path);

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + Count + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_OBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(OBJ_transform_Value, transformSetting);

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
                        HTK_3DES.TSRSystem.Transform_Value Route_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = Convert.ToSingle(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position.X),
                                Y = Convert.ToSingle(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position.Y),
                                Z = Convert.ToSingle(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position.Z)
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = 20,
                                Y = 20,
                                Z = 20
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RouteOBJ, "Routes " + ITOP_PointsCount + " " + ITOP_RoutesCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_RouteOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(Route_transform_Value, transformSetting);

                        //AddMDL
                        Route_Rail.MV3D_List.Add(dv3D_RouteOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_RouteOBJ);
                    }

                    kMPViewportObject.Routes_List.Add(Route_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.Routes_List.Count; i++)
                {
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.Routes_List[i].MV3D_List);
                    kMPViewportObject.Routes_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Blue);
                }
                #endregion
            }

            public static void Render_Area(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.AERA_Section AERA)
            {
                for (int AERACount = 0; AERACount < AERA.NumOfEntries; AERACount++)
                {
                    HTK_3DES.TSRSystem.Transform_Value Area_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = AERA.AERAValue_List[AERACount].AERA_Position.X,
                            Y = AERA.AERAValue_List[AERACount].AERA_Position.Y,
                            Z = AERA.AERAValue_List[AERACount].AERA_Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = AERA.AERAValue_List[AERACount].AERA_Scale.X * 1000,
                            Y = AERA.AERAValue_List[AERACount].AERA_Scale.Y * 1000,
                            Z = AERA.AERAValue_List[AERACount].AERA_Scale.Z * 1000
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = AERA.AERAValue_List[AERACount].AERA_Rotation.X,
                            Y = AERA.AERAValue_List[AERACount].AERA_Rotation.Y,
                            Z = AERA.AERAValue_List[AERACount].AERA_Rotation.Z
                        }
                    };

                    ModelVisual3D dv3D_AreaOBJ = null;
                    if (AERA.AERAValue_List[AERACount].AreaMode == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (AERA.AERAValue_List[AERACount].AreaMode == 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomCylinderVisual3D(Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (AERA.AERAValue_List[AERACount].AreaMode > 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + AERACount + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_AreaOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(Area_transform_Value, transformSetting);

                    //Area_MV3D_List.Add(dv3D_AreaOBJ);
                    kMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_AreaOBJ);
                }
            }

            public static void Render_Camera(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.EMAC_Section EMAC)
            {
                for (int EMACCount = 0; EMACCount < EMAC.NumOfEntries; EMACCount++)
                {
                    HTK_3DES.TSRSystem.Transform_Value Camera_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = EMAC.EMACValue_List[EMACCount].EMAC_Position.X,
                            Y = EMAC.EMACValue_List[EMACCount].EMAC_Position.Y,
                            Z = EMAC.EMACValue_List[EMACCount].EMAC_Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 20,
                            Y = 20,
                            Z = 20
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = EMAC.EMACValue_List[EMACCount].EMAC_Rotation.X,
                            Y = EMAC.EMACValue_List[EMACCount].EMAC_Rotation.Y,
                            Z = EMAC.EMACValue_List[EMACCount].EMAC_Rotation.Z
                        }
                    };

                    ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CameraOBJ, "Camera " + EMACCount + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_CameraOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(Camera_transform_Value, transformSetting);

                    //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                    kMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CameraOBJ);
                }
            }

            public static void Render_Returnpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, KMPs.KMPFormat.KMPSection.TPGJ_Section TPGJ)
            {
                for (int TPGJCount = 0; TPGJCount < TPGJ.NumOfEntries; TPGJCount++)
                {
                    HTK_3DES.TSRSystem.Transform_Value RespawnPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Position.X,
                            Y = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Position.Y,
                            Z = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 20,
                            Y = 20,
                            Z = 20
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Rotation.X,
                            Y = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Rotation.Y,
                            Z = TPGJ.TPGJValue_List[TPGJCount].TPGJ_Rotation.Z
                        }
                    };

                    ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + TPGJCount + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_RespawnPointOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(RespawnPoint_transform_Value, transformSetting);

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
                        HTK_3DES.TSRSystem.Transform_Value GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_Position.X,
                                Y = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_Position.Y,
                                Z = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_PointScaleValue * 100,
                                Y = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_PointScaleValue * 100,
                                Z = TPLG.TPLGValue_List[Count + HPLG.HPLGValue_List[i].HPLG_StartPoint].TPLG_PointScaleValue * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + Count + " " + i);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_GliderPathOBJ, RotationSetting = HTK_3DES.TSRSystem.RotationSetting.Radian };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(GliderPoint_transform_Value, transformSetting);

                        //Add model
                        GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    }

                    kMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.GlideRoute_Rail_List.Count; i++)
                {
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.GlideRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.GlideRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.LightSkyBlue);
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
                    HTK_3DES.TSRSystem.Transform_Value StartPosition_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = startPosition.startPosition_Value[i].Position.X,
                            Y = startPosition.startPosition_Value[i].Position.Y,
                            Z = startPosition.startPosition_Value[i].Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 20,
                            Y = 20,
                            Z = 20
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = startPosition.startPosition_Value[i].Rotation.X,
                            Y = startPosition.startPosition_Value[i].Rotation.Y,
                            Z = startPosition.startPosition_Value[i].Rotation.Z
                        }
                    };

                    ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + i + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_StartPositionOBJ };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(StartPosition_transform_Value, transformSetting);

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
                        HTK_3DES.TSRSystem.Transform_Value EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = enemyRoute.Groups[i].Points[Count].Position.X,
                                Y = enemyRoute.Groups[i].Points[Count].Position.Y,
                                Z = enemyRoute.Groups[i].Points[Count].Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = enemyRoute.Groups[i].Points[Count].Control * 100,
                                Y = enemyRoute.Groups[i].Points[Count].Control * 100,
                                Z = enemyRoute.Groups[i].Points[Count].Control * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + Count + " " + i);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_EnemyPathOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(EnemyPoint_transform_Value, transformSetting);

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
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.EnemyRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.EnemyRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Orange);
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
                        HTK_3DES.TSRSystem.Transform_Value ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = itemRoute.Groups[GroupCount].Points[PointCount].Position.X,
                                Y = itemRoute.Groups[GroupCount].Points[PointCount].Position.Y,
                                Z = itemRoute.Groups[GroupCount].Points[PointCount].Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = itemRoute.Groups[GroupCount].Points[PointCount].PointSize * 100,
                                Y = itemRoute.Groups[GroupCount].Points[PointCount].PointSize * 100,
                                Z = itemRoute.Groups[GroupCount].Points[PointCount].PointSize * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_ItemPathOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(ItemPoint_transform_Value, transformSetting);

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
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.ItemRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.ItemRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Green);
                }
                #endregion
            }

            public static void Render_Checkpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Checkpoint Checkpoint, double CheckpointYOffset)
            {
                for (int GroupCount = 0; GroupCount < Checkpoint.Groups.Count; GroupCount++)
                {
                    //Checkpoint_Rails
                    HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = new HTK_3DES.KMP_3DCheckpointSystem.Checkpoint
                    {
                        Checkpoint_Left = new HTK_3DES.PathTools.Rail(),
                        Checkpoint_Right = new HTK_3DES.PathTools.Rail(),
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

                    for (int PointCount = 0; PointCount < Checkpoint.Groups[GroupCount].Points.Count; PointCount++)
                    {
                        #region Create
                        var P2D_Left = Checkpoint.Groups[GroupCount].Points[PointCount].Position_2D_Left;
                        Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                        Point3D P3DLeft = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DLeftToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                        P3DLeft.Y = CheckpointYOffset;

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

                        ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting_P2DLeft = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_CheckpointLeftOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(P2DLeft_transform_Value, transformSetting_P2DLeft);

                        checkpoint.Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                        HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointLeftOBJ);
                        #endregion

                        var P2D_Right = Checkpoint.Groups[GroupCount].Points[PointCount].Position_2D_Right;
                        Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                        Point3D P3DRight = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DRightToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                        P3DRight.Y = CheckpointYOffset;

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

                        ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting_P2DRight = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_CheckpointRightOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(P2DRight_transform_Value, transformSetting_P2DRight);

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
                    List<Point3D> point3Ds_Left = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.MV3D_List);
                    kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Left.LV3D_List = HTK_3DES.PathTools.DrawPath_Line(UserCtrl, point3Ds_Left, 5, Colors.Green);

                    kMPViewportObject.Checkpoint_Rail[i].SideWall_Left.SideWallList = HTK_3DES.PathTools.DrawPath_SideWall(UserCtrl, point3Ds_Left, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00));

                    List<Point3D> point3Ds_Right = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.MV3D_List);
                    kMPViewportObject.Checkpoint_Rail[i].Checkpoint_Right.LV3D_List = HTK_3DES.PathTools.DrawPath_Line(UserCtrl, point3Ds_Right, 5, Colors.Red);

                    kMPViewportObject.Checkpoint_Rail[i].SideWall_Right.SideWallList = HTK_3DES.PathTools.DrawPath_SideWall(UserCtrl, point3Ds_Right, System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                }
                #endregion
            }

            public static void Render_Object(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Object Object, KMPs.KMPHelper.ObjFlowReader.ObjFlowXmlToObject objFlowXmlToObject)
            {
                for (int Count = 0; Count < Object.Object_Values.Count; Count++)
                {
                    HTK_3DES.TSRSystem.Transform_Value OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = Object.Object_Values[Count].Position.X,
                            Y = Object.Object_Values[Count].Position.Y,
                            Z = Object.Object_Values[Count].Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = Object.Object_Values[Count].Scale.X * 2,
                            Y = Object.Object_Values[Count].Scale.Y * 2,
                            Z = Object.Object_Values[Count].Scale.Z * 2
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = Object.Object_Values[Count].Rotation.X,
                            Y = Object.Object_Values[Count].Rotation.Y,
                            Z = Object.Object_Values[Count].Rotation.Z
                        }
                    };

                    string ObjectPath = objFlowXmlToObject.ObjFlows.Find(x => x.ObjectID == Object.Object_Values[Count].ObjectID).Path;
                    ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(ObjectPath);

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + Count + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_OBJ };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(OBJ_transform_Value, transformSetting);

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
                        HTK_3DES.TSRSystem.Transform_Value Route_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = route.Groups[GroupCount].Points[PointsCount].Position.X,
                                Y = route.Groups[GroupCount].Points[PointsCount].Position.Y,
                                Z = route.Groups[GroupCount].Points[PointsCount].Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = 20,
                                Y = 20,
                                Z = 20
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RouteOBJ, "Routes " + PointsCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_RouteOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(Route_transform_Value, transformSetting);

                        //AddMDL
                        Route_Rail.MV3D_List.Add(dv3D_RouteOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_RouteOBJ);
                    }

                    kMPViewportObject.Routes_List.Add(Route_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.Routes_List.Count; i++)
                {
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.Routes_List[i].MV3D_List);
                    kMPViewportObject.Routes_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Blue);
                }
                #endregion
            }

            public static void Render_Area(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Area area)
            {
                for (int Count = 0; Count < area.Area_Values.Count; Count++)
                {
                    HTK_3DES.TSRSystem.Transform_Value Area_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = area.Area_Values[Count].Position.X,
                            Y = area.Area_Values[Count].Position.Y,
                            Z = area.Area_Values[Count].Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = area.Area_Values[Count].Scale.X * 1000,
                            Y = area.Area_Values[Count].Scale.Y * 1000,
                            Z = area.Area_Values[Count].Scale.Z * 1000
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = area.Area_Values[Count].Rotation.X,
                            Y = area.Area_Values[Count].Rotation.Y,
                            Z = area.Area_Values[Count].Rotation.Z
                        }
                    };

                    ModelVisual3D dv3D_AreaOBJ = null;
                    if (area.Area_Values[Count].AreaMode == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (area.Area_Values[Count].AreaMode == 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomCylinderVisual3D(Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    if (area.Area_Values[Count].AreaMode > 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + Count + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_AreaOBJ };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(Area_transform_Value, transformSetting);

                    //Area_MV3D_List.Add(dv3D_AreaOBJ);
                    kMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_AreaOBJ);
                }
            }

            public static void Render_Camera(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.Camera camera)
            {
                for (int Count = 0; Count < camera.Values.Count; Count++)
                {
                    HTK_3DES.TSRSystem.Transform_Value Camera_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = camera.Values[Count].Position.X,
                            Y = camera.Values[Count].Position.Y,
                            Z = camera.Values[Count].Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 20,
                            Y = 20,
                            Z = 20
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = camera.Values[Count].Rotation.X,
                            Y = camera.Values[Count].Rotation.Y,
                            Z = camera.Values[Count].Rotation.Z
                        }
                    };

                    ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CameraOBJ, "Camera " + Count + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_CameraOBJ };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(Camera_transform_Value, transformSetting);

                    //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                    kMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                    UserCtrl.MainViewPort.Children.Add(dv3D_CameraOBJ);
                }
            }

            public static void Render_Returnpoint(UserControl1 UserCtrl, KMPViewportObject kMPViewportObject, TestXml.KMPXml.JugemPoint jugemPoint)
            {
                for (int Count = 0; Count < jugemPoint.Values.Count; Count++)
                {
                    HTK_3DES.TSRSystem.Transform_Value RespawnPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                    {
                        Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                        {
                            X = jugemPoint.Values[Count].Position.X,
                            Y = jugemPoint.Values[Count].Position.Y,
                            Z = jugemPoint.Values[Count].Position.Z
                        },
                        Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                        {
                            X = 20,
                            Y = 20,
                            Z = 20
                        },
                        Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                        {
                            X = jugemPoint.Values[Count].Rotation.X,
                            Y = jugemPoint.Values[Count].Rotation.Y,
                            Z = jugemPoint.Values[Count].Rotation.Z
                        }
                    };

                    ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + Count + " " + -1);

                    HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_RespawnPointOBJ };
                    HTK_3DES.TSRSystem.New_TransformSystem3D(RespawnPoint_transform_Value, transformSetting);

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
                        HTK_3DES.TSRSystem.Transform_Value GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = glideRoute.Groups[GroupCount].Points[PointCount].Position.X,
                                Y = glideRoute.Groups[GroupCount].Points[PointCount].Position.Y,
                                Z = glideRoute.Groups[GroupCount].Points[PointCount].Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = glideRoute.Groups[GroupCount].Points[PointCount].PointScale * 100,
                                Y = glideRoute.Groups[GroupCount].Points[PointCount].PointScale * 100,
                                Z = glideRoute.Groups[GroupCount].Points[PointCount].PointScale * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_GliderPathOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(GliderPoint_transform_Value, transformSetting);

                        //Add model
                        GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    }

                    kMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.GlideRoute_Rail_List.Count; i++)
                {
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.GlideRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.GlideRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.LightSkyBlue);
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
                        HTK_3DES.TSRSystem.Transform_Value EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = xXXXRoute.Groups[i].Points[Count].Position.X,
                                Y = xXXXRoute.Groups[i].Points[Count].Position.Y,
                                Z = xXXXRoute.Groups[i].Points[Count].Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = xXXXRoute.Groups[i].Points[Count].ScaleValue * 100,
                                Y = xXXXRoute.Groups[i].Points[Count].ScaleValue * 100,
                                Z = xXXXRoute.Groups[i].Points[Count].ScaleValue * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + Count + " " + i);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_EnemyPathOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(EnemyPoint_transform_Value, transformSetting);

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
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.EnemyRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.EnemyRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Orange);
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
                        HTK_3DES.TSRSystem.Transform_Value ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.X,
                                Y = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.Y,
                                Z = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue * 100,
                                Y = xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue * 100,
                                Z = xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_ItemPathOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(ItemPoint_transform_Value, transformSetting);

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
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.ItemRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.ItemRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.Green);
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
                        HTK_3DES.TSRSystem.Transform_Value GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
                        {
                            Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                            {
                                X = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.X,
                                Y = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.Y,
                                Z = xXXXRoute.Groups[GroupCount].Points[PointCount].Position.Z
                            },
                            Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                            {
                                X = xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue * 100,
                                Y = xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue * 100,
                                Z = xXXXRoute.Groups[GroupCount].Points[PointCount].ScaleValue * 100
                            },
                            Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                            {
                                X = 0,
                                Y = 0,
                                Z = 0
                            }
                        };

                        ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + PointCount + " " + GroupCount);

                        HTK_3DES.TSRSystem.TransformSetting transformSetting = new HTK_3DES.TSRSystem.TransformSetting { InputMV3D = dv3D_GliderPathOBJ };
                        HTK_3DES.TSRSystem.New_TransformSystem3D(GliderPoint_transform_Value, transformSetting);

                        //Add model
                        GlideRoute_Rail.MV3D_List.Add(dv3D_GliderPathOBJ);

                        UserCtrl.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                    }

                    kMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);
                }

                #region Add Rail
                for (int i = 0; i < kMPViewportObject.GlideRoute_Rail_List.Count; i++)
                {
                    List<Point3D> point3Ds = HTK_3DES.PathTools.MV3DListToPoint3DList(kMPViewportObject.GlideRoute_Rail_List[i].MV3D_List);
                    kMPViewportObject.GlideRoute_Rail_List[i].TV3D_List = HTK_3DES.PathTools.DrawPath_Tube(UserCtrl, point3Ds, 10.0, Colors.LightSkyBlue);
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
            public DMDCSectionOffset DMDC_SectionOffset { get; set; }
            public class DMDCSectionOffset
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

                public DMDCSectionOffset()
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
                }
            }

            public KMPSection KMP_Section { get; set; }
            public class KMPSection
            {
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
                        }

                        public uint HPNE_UnkBytes1 { get; set; }
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
                        }
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
                        public byte TPKC_UnkBytes1 { get; set; }
                        public byte TPKC_UnkBytes2 { get; set; }
                        public byte TPKC_UnkBytes3 { get; set; }
                        public byte TPKC_UnkBytes4 { get; set; }
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
                        }

                        public ushort HPKC_UnkBytes1 { get; set; }
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
                        public JBOG_SpecificSetting JOBJ_Specific_Setting { get; set; }
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
                        }
                        public ushort JBOG_PresenceSetting { get; set; }
                        public byte[] JBOG_UnkByte2 { get; set; }
                        public ushort JBOG_UnkByte3 { get; set; }
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
                        public byte ITOP_RouteSetting1 { get; set; }
                        public byte ITOP_RouteSetting2 { get; set; }
                        public List<ITOP_Point> ITOP_Point_List { get; set; }
                        public class ITOP_Point
                        {
                            public Vector3D ITOP_Point_Position { get; set; }
                            public ushort ITOP_Point_RouteSpeed { get; set; }
                            public ushort ITOP_PointSetting2 { get; set; }
                        }
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
                        public ushort AERA_UnkByte1 { get; set; }
                        public ushort AERA_UnkByte2 { get; set; }
                        public byte RouteID { get; set; }
                        public byte EnemyID { get; set; }
                        public ushort AERA_UnkByte4 { get; set; }
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
                        public byte EMAC_UnkBytes1 { get; set; }
                        public byte EMAC_ITOP_CameraIndex { get; set; }
                        public ushort RouteSpeed { get; set; }
                        public ushort FOVSpeed { get; set; }
                        public ushort ViewpointSpeed { get; set; }
                        public byte EMAC_UnkBytes2 { get; set; }
                        public byte EMAC_UnkBytes3 { get; set; }
                        public Vector3D EMAC_Position { get; set; }
                        public Vector3D EMAC_Rotation { get; set; }
                        public float FOVAngle_Start { get; set; }
                        public float FOVAngle_End { get; set; }
                        public Vector3D Viewpoint_Start { get; set; }
                        public Vector3D Viewpoint_Destination { get; set; }
                        public float Camera_Active_Time { get; set; }
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
                    }
                }

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
                }

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
                    }

                    public uint FlareAlpha { get; set; }
                }

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
                        }

                        public uint RouteSetting { get; set; }
                        public uint HPLG_UnkBytes2 { get; set; }
                    }
                }

                public KMPSection()
                {
                    TPTK = null;
                    TPNE = null;
                    HPNE = null;
                    TPTI = null;
                    HPTI = null;
                    TPKC = null;
                    HPKC = null;
                    JBOG = null;
                    ITOP = null;
                    AERA = null;
                    EMAC = null;
                    TPGJ = null;
                    TPNC = null;
                    TPSM = null;
                    IGTS = null;
                    SROC = null;
                    TPLG = null;
                    HPLG = null;
                }
            }

            public KMPFormat()
            {
                DMDCHeader = new char[] { ' ', ' ', ' ', ' ' };
                FileSize = 0;
                SectionCount = 0;
                DMDCHeaderSize = 0;
                VersionNumber = 0;
                DMDC_SectionOffset = new DMDCSectionOffset();
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
                public class ObjFlowValue
                {
                    //Object ID
                    public string ObjID { get; set; }

                    //衝突タイプ
                    public string ColType { get; set; }

                    //パスタイプ
                    public string PathType { get; set; }

                    //LOD
                    public int LOD { get; set; }
                    public int LODHPoly { get; set; }
                    public int LODLPoly { get; set; }
                    public int LODDef { get; set; }

                    //モデル設定
                    public string ModelSetting { get; set; }

                    //X
                    public int ObjX { get; set; }

                    //Y
                    public int ObjY { get; set; }

                    //Z
                    public int ObjZ { get; set; }

                    public string Unk1 { get; set; }

                    //Object Name 1
                    public string ObjFlowName1Text { get; set; }
                    //Object Name 2
                    public string ObjFlowName2Text { get; set; }
                }

                public class ObjFlowXmlToObject
                {
                    public List<ObjFlow> ObjFlows { get; set; }
                    public class ObjFlow
                    {
                        public string ObjectID { get; set; }
                        public string ObjectName { get; set; }
                        public string Path { get; set; }
                        public bool UseKCL { get; set; }
                        public string ObjectType { get; set; }

                        public Common Commons { get; set; }
                        public class Common
                        {
                            public string ObjID { get; set; }
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
                }

                public static List<ObjFlowValue> Read(string Path)
                {
                    List<ObjFlowValue> objFlowValues_List = new List<ObjFlowValue>();

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

                        ObjFlowValue objFlowValue = new ObjFlowValue
                        {
                            ObjID = BitConverter.ToString(ObjFlowData.ObjID.Reverse().ToArray()).Replace("-", string.Empty),
                            ColType = BitConverter.ToString(ObjFlowData.ColType),
                            PathType = BitConverter.ToString(ObjFlowData.PathType),
                            LOD = ObjFlowData.LOD,
                            LODHPoly = ObjFlowData.LODHPoly,
                            LODLPoly = ObjFlowData.LODLPoly,
                            LODDef = ObjFlowData.LODDef,
                            ModelSetting = BitConverter.ToString(ObjFlowData.ModelSetting),
                            ObjX = ObjFlowData.ObjX,
                            ObjY = ObjFlowData.ObjY,
                            ObjZ = ObjFlowData.ObjZ,
                            Unk1 = BitConverter.ToString(ObjFlowData.Unknown1, 0),
                            ObjFlowName1Text = new string(ObjFlowData.ObjFlowName1).Replace("\0", ""),
                            ObjFlowName2Text = new string(ObjFlowData.ObjFlowName2).Replace("\0", "")
                        };

                        objFlowValues_List.Add(objFlowValue);
                    }

                    br.Close();
                    fs.Close();

                    return objFlowValues_List;
                }

                public static Dictionary<string[], string> ObjFlowMdlPathDictionary(ObjFlowXmlToObject ObjFlowToObj, string Path, string DefaultModelPath)
                {
                    //指定したディレクトリの中にあるファイルパスを全て取得
                    string[] PathAry = System.IO.Directory.GetFiles(Path, "*.obj", System.IO.SearchOption.AllDirectories);

                    Dictionary<string[], string> ObjFlowDicts = new Dictionary<string[], string>();

                    foreach (var ObjFlowValue in ObjFlowToObj.ObjFlows.Select((item, index) => new { item, index }))
                    {
                        //Search the path of the corresponding model from PathAry(string[])
                        if (PathAry.Contains(ObjFlowValue.item.Path))
                        {
                            //Get ObjectID
                            string ObjectID = ObjFlowToObj.ObjFlows.Find(x => x.Path == ObjFlowValue.item.Path).ObjectID ?? "";
                            ObjFlowDicts.Add(new string[] { ObjFlowValue.item.ObjectName, ObjectID }, ObjFlowValue.item.Path);
                        }
                    }

                    return ObjFlowDicts;
                }

                public static void CreateXml(List<ObjFlowValue> ObjFlowVal_List, string KMPObjectFolderPath, string DefaultModelPath, string XmlPath)
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
                        if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.ObjFlowName1Text + ".obj"))
                        {
                            MDLPath = KMPObjectFolderPath + "\\" + ObjFlowValue.item.ObjFlowName1Text + ".obj";
                        }
                        if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.ObjFlowName1Text + ".obj") == false)
                        {
                            MDLPath = DefaultModelPath;
                        }

                        TestXml.KMPObjFlowDataXml.ObjFlow objFlow = new TestXml.KMPObjFlowDataXml.ObjFlow
                        {
                            ObjectID = ObjFlowValue.item.ObjID,
                            ObjectName = ObjFlowValue.item.ObjFlowName1Text,
                            Path = MDLPath,
                            UseKCL = false,
                            ObjectType = "Unknown",
                            Commons = new TestXml.KMPObjFlowDataXml.ObjFlow.Common
                            {
                                ObjID = ObjFlowValue.item.ObjID,
                                ColType = ObjFlowValue.item.ColType,
                                PathType = ObjFlowValue.item.PathType,
                                ModelSetting = ObjFlowValue.item.ModelSetting,
                                Unknown1 = ObjFlowValue.item.Unk1
                            },
                            LODSetting = new TestXml.KMPObjFlowDataXml.ObjFlow.LOD_Setting
                            {
                                LOD = ObjFlowValue.item.LOD,
                                LODHPoly = ObjFlowValue.item.LODHPoly,
                                LODLPoly = ObjFlowValue.item.LODLPoly,
                                LODDef = ObjFlowValue.item.LODDef
                            },
                            Scales = new TestXml.KMPObjFlowDataXml.ObjFlow.Scale
                            {
                                X = ObjFlowValue.item.ObjX,
                                Y = ObjFlowValue.item.ObjY,
                                Z = ObjFlowValue.item.ObjZ
                            },
                            Names = new TestXml.KMPObjFlowDataXml.ObjFlow.Name
                            {
                                Main = ObjFlowValue.item.ObjFlowName1Text,
                                Sub = ObjFlowValue.item.ObjFlowName2Text
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

                public static ObjFlowXmlToObject ReadObjFlowXml(string Path)
                {
                    System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
                    System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPObjFlowDataXml));
                    TestXml.KMPObjFlowDataXml ObjFlowXml = (TestXml.KMPObjFlowDataXml)s1.Deserialize(fs1);

                    ObjFlowXmlToObject objFlowXmlToObject = new ObjFlowXmlToObject
                    {
                        ObjFlows = null
                    };

                    List<ObjFlowXmlToObject.ObjFlow> ObjFlow_List = new List<ObjFlowXmlToObject.ObjFlow>();

                    foreach(var ObjFlowData in ObjFlowXml.ObjFlows)
                    {
                        ObjFlowXmlToObject.ObjFlow objFlow = new ObjFlowXmlToObject.ObjFlow
                        {
                            ObjectID = ObjFlowData.ObjectID,
                            ObjectName = ObjFlowData.ObjectName,
                            Path = ObjFlowData.Path,
                            UseKCL = ObjFlowData.UseKCL,
                            ObjectType = ObjFlowData.ObjectType,
                            Commons = new ObjFlowXmlToObject.ObjFlow.Common
                            {
                                ObjID = ObjFlowData.Commons.ObjID,
                                ColType = ObjFlowData.Commons.ColType,
                                PathType = ObjFlowData.Commons.PathType,
                                ModelSetting = ObjFlowData.Commons.ModelSetting,
                                Unknown1 = ObjFlowData.Commons.Unknown1
                            },
                            LODSetting = new ObjFlowXmlToObject.ObjFlow.LOD_Setting
                            {
                                LOD = ObjFlowData.LODSetting.LOD,
                                LODHPoly = ObjFlowData.LODSetting.LODHPoly,
                                LODLPoly = ObjFlowData.LODSetting.LODLPoly,
                                LODDef = ObjFlowData.LODSetting.LODDef
                            },
                            Scales = new ObjFlowXmlToObject.ObjFlow.Scale
                            {
                                X = ObjFlowData.Scales.X,
                                Y = ObjFlowData.Scales.Y,
                                Z = ObjFlowData.Scales.Z
                            },
                            Names = new ObjFlowXmlToObject.ObjFlow.Name
                            {
                                Main = ObjFlowData.Names.Main,
                                Sub = ObjFlowData.Names.Sub
                            },
                            DefaultValues = new ObjFlowXmlToObject.ObjFlow.Default_Values
                            {
                                Values = null
                            }
                        };

                        List<ObjFlowXmlToObject.ObjFlow.Default_Values.Value> valueList = new List<ObjFlowXmlToObject.ObjFlow.Default_Values.Value>();

                        foreach(var ObjFlowDataValue in ObjFlowData.DefaultValues.Values)
                        {
                            ObjFlowXmlToObject.ObjFlow.Default_Values.Value value = new ObjFlowXmlToObject.ObjFlow.Default_Values.Value
                            {
                                DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
                                Description = ObjFlowDataValue.Description
                            };

                            valueList.Add(value);
                        }

                        objFlow.DefaultValues.Values = valueList;

                        ObjFlow_List.Add(objFlow);
                    }

                    objFlowXmlToObject.ObjFlows = ObjFlow_List;

                    fs1.Close();
                    fs1.Dispose();

                    return objFlowXmlToObject;
                }

                public static void WriteObjFlowXml(ObjFlowXmlPropertyGridSetting objFlowXmlToObject, string Path)
                {
                    TestXml.KMPObjFlowDataXml kMPObjFlowDataXml = new TestXml.KMPObjFlowDataXml
                    {
                        ObjFlows = null
                    };

                    List<TestXml.KMPObjFlowDataXml.ObjFlow> ObjFlowList = new List<TestXml.KMPObjFlowDataXml.ObjFlow>();

                    foreach (var ObjFlowValue in objFlowXmlToObject.ObjFlowsList.Select((item, index) => new { item, index }))
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
                                ObjID = ObjFlowValue.item.Commons.ObjID,
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

                        foreach (var i in ObjFlowValue.item.DefaultValues.ValuesList)
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

            public class Byte2StringConverter
            {
                //public byte[] ToByteArray(string InputString)
                //{
                //    string[] SplitStr = InputString.Split('-');
                //    byte[] Str2byte = new byte[SplitStr.Length];
                //    for (int n = 0; n < SplitStr.Length; n++)
                //    {
                //        byte b = byte.Parse(SplitStr[n], System.Globalization.NumberStyles.AllowHexSpecifier);
                //        Str2byte[n] = b;
                //    }

                //    return Str2byte;
                //}

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

        public class KMPReader : KMPs
        {
            public static KMPs.KMPFormat.KMPSection.TPTK_Section Read_TPTK(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.TPTK_Section TPTK = new KMPs.KMPFormat.KMPSection.TPTK_Section
                {
                    TPTKHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    TPTKValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue> TPTKValue_List = new List<KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue>();

                for (int TPTKCount = 0; TPTKCount < TPTK.NumOfEntries; TPTKCount++)
                {
                    KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue TPTK_Values = new KMPs.KMPFormat.KMPSection.TPTK_Section.TPTKValue
                    {
                        TPTK_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        TPTK_Rotation = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        Player_Index = br.ReadUInt16(),
                        TPTK_UnkBytes = br.ReadUInt16()
                    };

                    TPTKValue_List.Add(TPTK_Values);
                }

                TPTK.TPTKValue_List = TPTKValue_List;

                return TPTK;
            }

            public static KMPs.KMPFormat.KMPSection.TPNE_Section Read_TPNE(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.TPNE_Section TPNE = new KMPs.KMPFormat.KMPSection.TPNE_Section
                {
                    TPNEHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    TPNEValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue> TPNEValue_List = new List<KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue>();

                for (int TPNECount = 0; TPNECount < TPNE.NumOfEntries; TPNECount++)
                {
                    KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue TPNE_Values = new KMPs.KMPFormat.KMPSection.TPNE_Section.TPNEValue
                    {
                        TPNE_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        Control = br.ReadSingle(),
                        MushSetting = br.ReadUInt16(),
                        DriftSetting = br.ReadByte(),
                        Flags = br.ReadByte(),
                        PathFindOption = br.ReadInt16(),
                        MaxSearchYOffset = br.ReadInt16()
                    };

                    TPNEValue_List.Add(TPNE_Values);
                }

                TPNE.TPNEValue_List = TPNEValue_List;
                return TPNE;
            }

            public static KMPs.KMPFormat.KMPSection.HPNE_Section Read_HPNE(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.HPNE_Section HPNE = new KMPs.KMPFormat.KMPSection.HPNE_Section
                {
                    HPNEHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    HPNEValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue> HPNEValue_List = new List<KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue>();

                for (int HPNECount = 0; HPNECount < HPNE.NumOfEntries; HPNECount++)
                {
                    KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue HPNE_Values = new KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue
                    {
                        HPNE_StartPoint = br.ReadUInt16(),
                        HPNE_Length = br.ReadUInt16(),
                        HPNE_PreviewGroup = new KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue.HPNE_PreviewGroups
                        {
                            Prev0 = br.ReadUInt16(),
                            Prev1 = br.ReadUInt16(),
                            Prev2 = br.ReadUInt16(),
                            Prev3 = br.ReadUInt16(),
                            Prev4 = br.ReadUInt16(),
                            Prev5 = br.ReadUInt16(),
                            Prev6 = br.ReadUInt16(),
                            Prev7 = br.ReadUInt16(),
                            Prev8 = br.ReadUInt16(),
                            Prev9 = br.ReadUInt16(),
                            Prev10 = br.ReadUInt16(),
                            Prev11 = br.ReadUInt16(),
                            Prev12 = br.ReadUInt16(),
                            Prev13 = br.ReadUInt16(),
                            Prev14 = br.ReadUInt16(),
                            Prev15 = br.ReadUInt16()
                        },
                        HPNE_NextGroup = new KMPs.KMPFormat.KMPSection.HPNE_Section.HPNEValue.HPNE_NextGroups
                        {
                            Next0 = br.ReadUInt16(),
                            Next1 = br.ReadUInt16(),
                            Next2 = br.ReadUInt16(),
                            Next3 = br.ReadUInt16(),
                            Next4 = br.ReadUInt16(),
                            Next5 = br.ReadUInt16(),
                            Next6 = br.ReadUInt16(),
                            Next7 = br.ReadUInt16(),
                            Next8 = br.ReadUInt16(),
                            Next9 = br.ReadUInt16(),
                            Next10 = br.ReadUInt16(),
                            Next11 = br.ReadUInt16(),
                            Next12 = br.ReadUInt16(),
                            Next13 = br.ReadUInt16(),
                            Next14 = br.ReadUInt16(),
                            Next15 = br.ReadUInt16()
                        },
                        HPNE_UnkBytes1 = br.ReadUInt32()
                    };

                    HPNEValue_List.Add(HPNE_Values);
                }

                HPNE.HPNEValue_List = HPNEValue_List;
                return HPNE;
            }

            public static KMPs.KMPFormat.KMPSection.TPTI_Section Read_TPTI(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.TPTI_Section TPTI = new KMPs.KMPFormat.KMPSection.TPTI_Section
                {
                    TPTIHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    TPTIValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue> TPTIValue_List = new List<KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue>();

                for (int TPTICount = 0; TPTICount < TPTI.NumOfEntries; TPTICount++)
                {
                    byte[] BPX = br.ReadBytes(4);
                    byte[] BPY = br.ReadBytes(4);
                    byte[] BPZ = br.ReadBytes(4);

                    KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue TPTI_Values = new KMPs.KMPFormat.KMPSection.TPTI_Section.TPTIValue
                    {
                        TPTI_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { BPX, BPY, BPZ }),
                        TPTI_PointSize = br.ReadSingle(),
                        GravityMode = br.ReadUInt16(),
                        PlayerScanRadius = br.ReadUInt16()
                    };

                    TPTIValue_List.Add(TPTI_Values);
                }

                TPTI.TPTIValue_List = TPTIValue_List;
                return TPTI;
            }

            public static KMPs.KMPFormat.KMPSection.HPTI_Section Read_HPTI(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.HPTI_Section HPTI = new KMPs.KMPFormat.KMPSection.HPTI_Section
                {
                    HPTIHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    HPTIValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue> HPTIValue_List = new List<KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue>();

                for (int HPTICount = 0; HPTICount < HPTI.NumOfEntries; HPTICount++)
                {
                    KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue HPTI_Values = new KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue
                    {
                        HPTI_StartPoint = br.ReadUInt16(),
                        HPTI_Length = br.ReadUInt16(),
                        HPTI_PreviewGroup = new KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue.HPTI_PreviewGroups
                        {
                            Prev0 = br.ReadUInt16(),
                            Prev1 = br.ReadUInt16(),
                            Prev2 = br.ReadUInt16(),
                            Prev3 = br.ReadUInt16(),
                            Prev4 = br.ReadUInt16(),
                            Prev5 = br.ReadUInt16()
                        },
                        HPTI_NextGroup = new KMPs.KMPFormat.KMPSection.HPTI_Section.HPTIValue.HPTI_NextGroups
                        {
                            Next0 = br.ReadUInt16(),
                            Next1 = br.ReadUInt16(),
                            Next2 = br.ReadUInt16(),
                            Next3 = br.ReadUInt16(),
                            Next4 = br.ReadUInt16(),
                            Next5 = br.ReadUInt16()
                        }
                    };

                    HPTIValue_List.Add(HPTI_Values);
                }

                HPTI.HPTIValue_List = HPTIValue_List;
                return HPTI;
            }

            public static KMPs.KMPFormat.KMPSection.TPKC_Section Read_TPKC(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.TPKC_Section TPKC = new KMPs.KMPFormat.KMPSection.TPKC_Section
                {
                    TPKCHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    TPKCValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue> TPKCValue_List = new List<KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue>();

                for (int TPKCCount = 0; TPKCCount < TPKC.NumOfEntries; TPKCCount++)
                {
                    KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue TPKC_Values = new KMPs.KMPFormat.KMPSection.TPKC_Section.TPKCValue
                    {
                        TPKC_2DPosition_Left = KMPs.KMPHelper.Vector3DTo2DConverter.ByteArrayToVector2D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4) }),
                        TPKC_2DPosition_Right = KMPs.KMPHelper.Vector3DTo2DConverter.ByteArrayToVector2D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4) }),
                        TPKC_RespawnID = br.ReadByte(),
                        TPKC_Checkpoint_Type = br.ReadByte(),
                        TPKC_PreviousCheckPoint = br.ReadByte(),
                        TPKC_NextCheckPoint = br.ReadByte(),
                        TPKC_UnkBytes1 = br.ReadByte(),
                        TPKC_UnkBytes2 = br.ReadByte(),
                        TPKC_UnkBytes3 = br.ReadByte(),
                        TPKC_UnkBytes4 = br.ReadByte()
                    };

                    TPKCValue_List.Add(TPKC_Values);
                }

                TPKC.TPKCValue_List = TPKCValue_List;
                return TPKC;
            }

            public static KMPs.KMPFormat.KMPSection.HPKC_Section Read_HPKC(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.HPKC_Section HPKC = new KMPs.KMPFormat.KMPSection.HPKC_Section
                {
                    HPKCHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    HPKCValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue> HPKCValue_List = new List<KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue>();

                for (int HPKCCount = 0; HPKCCount < HPKC.NumOfEntries; HPKCCount++)
                {
                    KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue HPKC_Values = new KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue
                    {
                        HPKC_StartPoint = br.ReadByte(),
                        HPKC_Length = br.ReadByte(),
                        HPKC_PreviewGroup = new KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue.HPKC_PreviewGroups
                        {
                            Prev0 = br.ReadByte(),
                            Prev1 = br.ReadByte(),
                            Prev2 = br.ReadByte(),
                            Prev3 = br.ReadByte(),
                            Prev4 = br.ReadByte(),
                            Prev5 = br.ReadByte()
                        },
                        HPKC_NextGroup = new KMPs.KMPFormat.KMPSection.HPKC_Section.HPKCValue.HPKC_NextGroups
                        {
                            Next0 = br.ReadByte(),
                            Next1 = br.ReadByte(),
                            Next2 = br.ReadByte(),
                            Next3 = br.ReadByte(),
                            Next4 = br.ReadByte(),
                            Next5 = br.ReadByte()
                        },
                        HPKC_UnkBytes1 = br.ReadUInt16()
                    };

                    HPKCValue_List.Add(HPKC_Values);
                }

                HPKC.HPKCValue_List = HPKCValue_List;
                return HPKC;
            }

            public static KMPs.KMPFormat.KMPSection.JBOG_Section Read_JBOG(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.JBOG_Section JBOG = new KMPs.KMPFormat.KMPSection.JBOG_Section
                {
                    JBOGHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    JBOGValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue> JBOGValue_List = new List<KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue>();

                for (int JBOGCount = 0; JBOGCount < JBOG.NumOfEntries; JBOGCount++)
                {
                    KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue JBOG_Values = new KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue
                    {
                        ObjectID = br.ReadBytes(2),
                        JBOG_UnkByte1 = br.ReadBytes(2),
                        JBOG_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        JBOG_Rotation = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        JBOG_Scale = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        JBOG_ITOP_RouteIDIndex = br.ReadUInt16(),
                        JOBJ_Specific_Setting = new KMPs.KMPFormat.KMPSection.JBOG_Section.JBOGValue.JBOG_SpecificSetting
                        {
                            Value0 = br.ReadUInt16(),
                            Value1 = br.ReadUInt16(),
                            Value2 = br.ReadUInt16(),
                            Value3 = br.ReadUInt16(),
                            Value4 = br.ReadUInt16(),
                            Value5 = br.ReadUInt16(),
                            Value6 = br.ReadUInt16(),
                            Value7 = br.ReadUInt16(),
                        },
                        JBOG_PresenceSetting = br.ReadUInt16(),
                        JBOG_UnkByte2 = br.ReadBytes(2),
                        JBOG_UnkByte3 = br.ReadUInt16()
                    };

                    JBOGValue_List.Add(JBOG_Values);
                }

                JBOG.JBOGValue_List = JBOGValue_List;
                return JBOG;
            }

            public static KMPs.KMPFormat.KMPSection.ITOP_Section Read_ITOP(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.ITOP_Section ITOP = new KMPs.KMPFormat.KMPSection.ITOP_Section
                {
                    ITOPHeader = br.ReadChars(4),
                    ITOP_NumberOfRoute = br.ReadUInt16(),
                    ITOP_NumberOfPoint = br.ReadUInt16(),
                    ITOP_Route_List = null
                };

                List<KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route> ITOP_Route_List = new List<KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route>();

                for (int ITOPRouteCount = 0; ITOPRouteCount < ITOP.ITOP_NumberOfRoute; ITOPRouteCount++)
                {
                    KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route ITOP_Routes = new KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route
                    {
                        ITOP_Route_NumOfPoint = br.ReadUInt16(),
                        ITOP_RouteSetting1 = br.ReadByte(),
                        ITOP_RouteSetting2 = br.ReadByte(),
                        ITOP_Point_List = null
                    };

                    List<KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point> ITOP_Point_List = new List<KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point>();

                    for (int ITOP_PointCount = 0; ITOP_PointCount < ITOP_Routes.ITOP_Route_NumOfPoint; ITOP_PointCount++)
                    {
                        KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point ITOP_Points = new KMPs.KMPFormat.KMPSection.ITOP_Section.ITOP_Route.ITOP_Point
                        {
                            ITOP_Point_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                            ITOP_Point_RouteSpeed = br.ReadUInt16(),
                            ITOP_PointSetting2 = br.ReadUInt16()
                        };

                        ITOP_Point_List.Add(ITOP_Points);
                    }

                    ITOP_Routes.ITOP_Point_List = ITOP_Point_List;

                    ITOP_Route_List.Add(ITOP_Routes);
                }

                ITOP.ITOP_Route_List = ITOP_Route_List;
                return ITOP;
            }

            public static KMPs.KMPFormat.KMPSection.AERA_Section Read_AERA(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.AERA_Section AERA = new KMPs.KMPFormat.KMPSection.AERA_Section
                {
                    AERAHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    AERAValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue> AERAValue_List = new List<KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue>();

                for (int AERACount = 0; AERACount < AERA.NumOfEntries; AERACount++)
                {
                    KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue AERA_Values = new KMPs.KMPFormat.KMPSection.AERA_Section.AERAValue
                    {
                        AreaMode = br.ReadByte(),
                        AreaType = br.ReadByte(),
                        AERA_EMACIndex = br.ReadByte(),
                        Priority = br.ReadByte(),
                        AERA_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        AERA_Rotation = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        AERA_Scale = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        AERA_UnkByte1 = br.ReadUInt16(),
                        AERA_UnkByte2 = br.ReadUInt16(),
                        RouteID = br.ReadByte(),
                        EnemyID = br.ReadByte(),
                        AERA_UnkByte4 = br.ReadUInt16()
                    };

                    AERAValue_List.Add(AERA_Values);
                }

                AERA.AERAValue_List = AERAValue_List;
                return AERA;
            }

            public static KMPs.KMPFormat.KMPSection.EMAC_Section Read_EMAC(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.EMAC_Section EMAC = new KMPs.KMPFormat.KMPSection.EMAC_Section
                {
                    EMACHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    EMACValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue> EMACValue_List = new List<KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue>();

                for (int EMACCount = 0; EMACCount < EMAC.NumOfEntries; EMACCount++)
                {
                    KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue EMAC_Values = new KMPs.KMPFormat.KMPSection.EMAC_Section.EMACValue
                    {
                        CameraType = br.ReadByte(),
                        NextCameraIndex = br.ReadByte(),
                        EMAC_UnkBytes1 = br.ReadByte(),
                        EMAC_ITOP_CameraIndex = br.ReadByte(),
                        RouteSpeed = br.ReadUInt16(),
                        FOVSpeed = br.ReadUInt16(),
                        ViewpointSpeed = br.ReadUInt16(),
                        EMAC_UnkBytes2 = br.ReadByte(),
                        EMAC_UnkBytes3 = br.ReadByte(),
                        EMAC_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        EMAC_Rotation = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        FOVAngle_Start = br.ReadSingle(),
                        FOVAngle_End = br.ReadSingle(),
                        Viewpoint_Start = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        Viewpoint_Destination = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        Camera_Active_Time = br.ReadSingle()
                    };

                    EMACValue_List.Add(EMAC_Values);
                }

                EMAC.EMACValue_List = EMACValue_List;
                return EMAC;
            }

            public static KMPs.KMPFormat.KMPSection.TPGJ_Section Read_TPGJ(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.TPGJ_Section TPGJ = new KMPs.KMPFormat.KMPSection.TPGJ_Section
                {
                    TPGJHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    TPGJValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue> TPGJValue_List = new List<KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue>();

                for (int TPGJCount = 0; TPGJCount < TPGJ.NumOfEntries; TPGJCount++)
                {
                    KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue TPGJ_Values = new KMPs.KMPFormat.KMPSection.TPGJ_Section.TPGJValue
                    {
                        TPGJ_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        TPGJ_Rotation = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        TPGJ_RespawnID = br.ReadUInt16(),
                        TPGJ_UnkBytes1 = br.ReadUInt16()
                    };

                    TPGJValue_List.Add(TPGJ_Values);
                }

                TPGJ.TPGJValue_List = TPGJValue_List;
                return TPGJ;
            }

            public static KMPs.KMPFormat.KMPSection.TPNC_Section Read_TPNC(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.TPNC_Section TPNC = new KMPs.KMPFormat.KMPSection.TPNC_Section
                {
                    TPNCHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                };

                return TPNC;
            }

            public static KMPs.KMPFormat.KMPSection.TPSM_Section Read_TPSM(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.TPSM_Section TPSM = new KMPs.KMPFormat.KMPSection.TPSM_Section
                {
                    TPSMHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                };

                return TPSM;
            }

            public static KMPs.KMPFormat.KMPSection.IGTS_Section Read_IGTS(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.IGTS_Section IGTS = new KMPs.KMPFormat.KMPSection.IGTS_Section
                {
                    IGTSHeader = br.ReadChars(4),
                    Unknown1 = br.ReadUInt32(),
                    LapCount = br.ReadByte(),
                    PolePosition = br.ReadByte(),
                    Unknown2 = br.ReadByte(),
                    Unknown3 = br.ReadByte(),
                    RGBAColor = new KMPs.KMPFormat.KMPSection.IGTS_Section.RGBA
                    {
                        R = br.ReadByte(),
                        G = br.ReadByte(),
                        B = br.ReadByte(),
                        A = br.ReadByte()
                    },
                    FlareAlpha = br.ReadUInt32()
                };

                return IGTS;
            }

            public static KMPs.KMPFormat.KMPSection.SROC_Section Read_SROC(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.SROC_Section SROC = new KMPs.KMPFormat.KMPSection.SROC_Section
                {
                    SROCHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                };

                return SROC;
            }

            public static KMPs.KMPFormat.KMPSection.TPLG_Section Read_TPLG(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.TPLG_Section TPLG = new KMPs.KMPFormat.KMPSection.TPLG_Section
                {
                    TPLGHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    TPLGValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue> TPLGValue_List = new List<KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue>();

                for (int TPLGCount = 0; TPLGCount < TPLG.NumOfEntries; TPLGCount++)
                {
                    KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue TPLG_Values = new KMPs.KMPFormat.KMPSection.TPLG_Section.TPLGValue
                    {
                        TPLG_Position = KMPs.KMPHelper.ByteArrayToVector3DConverter.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) }),
                        TPLG_PointScaleValue = br.ReadSingle(),
                        TPLG_UnkBytes1 = br.ReadUInt32(),
                        TPLG_UnkBytes2 = br.ReadUInt32()
                    };

                    TPLGValue_List.Add(TPLG_Values);
                }

                TPLG.TPLGValue_List = TPLGValue_List;
                return TPLG;
            }

            public static KMPs.KMPFormat.KMPSection.HPLG_Section Read_HPLG(BinaryReader br)
            {
                KMPs.KMPFormat.KMPSection.HPLG_Section HPLG = new KMPs.KMPFormat.KMPSection.HPLG_Section
                {
                    HPLGHeader = br.ReadChars(4),
                    NumOfEntries = br.ReadUInt16(),
                    AdditionalValue = br.ReadUInt16(),
                    HPLGValue_List = null
                };

                List<KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue> HPLGValue_List = new List<KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue>();

                for (int HPLGCount = 0; HPLGCount < HPLG.NumOfEntries; HPLGCount++)
                {
                    KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue HPLG_Values = new KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue
                    {
                        HPLG_StartPoint = br.ReadByte(),
                        HPLG_Length = br.ReadByte(),
                        HPLG_PreviewGroup = new KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue.HPLG_PreviewGroups
                        {
                            Prev0 = br.ReadByte(),
                            Prev1 = br.ReadByte(),
                            Prev2 = br.ReadByte(),
                            Prev3 = br.ReadByte(),
                            Prev4 = br.ReadByte(),
                            Prev5 = br.ReadByte()
                        },
                        HPLG_NextGroup = new KMPs.KMPFormat.KMPSection.HPLG_Section.HPLGValue.HPLG_NextGroups
                        {
                            Next0 = br.ReadByte(),
                            Next1 = br.ReadByte(),
                            Next2 = br.ReadByte(),
                            Next3 = br.ReadByte(),
                            Next4 = br.ReadByte(),
                            Next5 = br.ReadByte()
                        },
                        RouteSetting = br.ReadUInt32(),
                        HPLG_UnkBytes2 = br.ReadUInt32()
                    };

                    HPLGValue_List.Add(HPLG_Values);
                }

                HPLG.HPLGValue_List = HPLGValue_List;
                return HPLG;
            }
        }

        public class KMPWriter : KMPs
        {
            public static void WriteHeader(BinaryWriter bw, KMPFormat KMPHeader)
            {
                bw.Write(KMPHeader.DMDCHeader);
                bw.Write(KMPHeader.FileSize);
                bw.Write(KMPHeader.SectionCount);
                bw.Write(KMPHeader.DMDCHeaderSize);
                bw.Write(KMPHeader.VersionNumber);
                bw.Write(KMPHeader.DMDC_SectionOffset.TPTK_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.TPNE_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.HPNE_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.TPTI_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.HPTI_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.TPKC_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.HPKC_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.JBOG_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.ITOP_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.AERA_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.EMAC_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.TPGJ_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.TPNC_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.TPSM_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.IGTS_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.SROC_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.TPLG_Offset);
                bw.Write(KMPHeader.DMDC_SectionOffset.HPLG_Offset);
            }

            public static uint Write_TPTK(BinaryWriter bw, KMPFormat.KMPSection.TPTK_Section TPTK)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPTK.TPTKHeader);
                bw.Write(TPTK.NumOfEntries);
                bw.Write(TPTK.AdditionalValue);

                for(int Count = 0; Count < TPTK.NumOfEntries; Count++)
                {
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Position)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Position)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Position)[2]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Rotation)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Rotation)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Rotation)[2]);
                    bw.Write(TPTK.TPTKValue_List[Count].Player_Index);
                    bw.Write(TPTK.TPTKValue_List[Count].TPTK_UnkBytes);
                }

                return WritePosition;
            }

            public class TPNE_HPNE_WritePosition
            {
                public uint TPNE { get; set; }
                public uint HPNE { get; set; }
            }

            public static TPNE_HPNE_WritePosition Write_TPNE_HPNE(BinaryWriter bw, KMPFormat.KMPSection.TPNE_Section TPNE, KMPFormat.KMPSection.HPNE_Section HPNE)
            {
                TPNE_HPNE_WritePosition tPNE_HPNE_WritePosition = new TPNE_HPNE_WritePosition
                {
                    TPNE = 0,
                    HPNE = 0
                };

                #region TPNE
                tPNE_HPNE_WritePosition.TPNE = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPNE.TPNEHeader);
                bw.Write(TPNE.NumOfEntries);
                bw.Write(TPNE.AdditionalValue);

                for (int Count = 0; Count < TPNE.TPNEValue_List.Count; Count++)
                {
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE.TPNEValue_List[Count].TPNE_Position)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE.TPNEValue_List[Count].TPNE_Position)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE.TPNEValue_List[Count].TPNE_Position)[2]);
                    bw.Write(TPNE.TPNEValue_List[Count].Control);
                    bw.Write(TPNE.TPNEValue_List[Count].MushSetting);
                    bw.Write(TPNE.TPNEValue_List[Count].DriftSetting);
                    bw.Write(TPNE.TPNEValue_List[Count].Flags);
                    bw.Write(TPNE.TPNEValue_List[Count].PathFindOption);
                    bw.Write(TPNE.TPNEValue_List[Count].MaxSearchYOffset);
                }
                #endregion

                #region HPNE
                tPNE_HPNE_WritePosition.HPNE = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(HPNE.HPNEHeader);
                bw.Write(HPNE.NumOfEntries);
                bw.Write(HPNE.AdditionalValue);

                for (int Count = 0; Count < HPNE.NumOfEntries; Count++)
                {
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_StartPoint);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_Length);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev0);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev1);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev2);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev3);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev4);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev5);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev6);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev7);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev8);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev9);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev10);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev11);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev12);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev13);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev14);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev15);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next0);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next1);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next2);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next3);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next4);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next5);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next6);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next7);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next8);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next9);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next10);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next11);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next12);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next13);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next14);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next15);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_UnkBytes1);
                }
                #endregion

                return tPNE_HPNE_WritePosition;
            }

            public class TPTI_HPTI_WritePosition
            {
                public uint TPTI { get; set; }
                public uint HPTI { get; set; }
            }

            public static TPTI_HPTI_WritePosition Write_TPTI_HPTI(BinaryWriter bw, KMPFormat.KMPSection.TPTI_Section TPTI, KMPFormat.KMPSection.HPTI_Section HPTI)
            {
                TPTI_HPTI_WritePosition tPTI_HPTI_WritePosition = new TPTI_HPTI_WritePosition
                {
                    TPTI = 0,
                    HPTI = 0
                };

                #region TPTI
                tPTI_HPTI_WritePosition.TPTI = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPTI.TPTIHeader);
                bw.Write(TPTI.NumOfEntries);
                bw.Write(TPTI.AdditionalValue);

                for (int Count = 0; Count < TPTI.TPTIValue_List.Count; Count++)
                {
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI.TPTIValue_List[Count].TPTI_Position)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI.TPTIValue_List[Count].TPTI_Position)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI.TPTIValue_List[Count].TPTI_Position)[2]);
                    bw.Write(TPTI.TPTIValue_List[Count].TPTI_PointSize);
                    bw.Write(TPTI.TPTIValue_List[Count].GravityMode);
                    bw.Write(TPTI.TPTIValue_List[Count].PlayerScanRadius);
                }
                #endregion

                #region HPTI
                tPTI_HPTI_WritePosition.HPTI = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(HPTI.HPTIHeader);
                bw.Write(HPTI.NumOfEntries);
                bw.Write(HPTI.AdditionalValue);

                for (int Count = 0; Count < HPTI.NumOfEntries; Count++)
                {
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_StartPoint);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_Length);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev0);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev1);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev2);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev3);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev4);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev5);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next0);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next1);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next2);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next3);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next4);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next5);
                }
                #endregion

                return tPTI_HPTI_WritePosition;
            }

            public class TPKC_HPKC_WritePosition
            {
                public uint TPKC { get; set; }
                public uint HPKC { get; set; }
            }

            public static TPKC_HPKC_WritePosition Write_TPKC_HPKC(BinaryWriter bw, KMPFormat.KMPSection.TPKC_Section TPKC, KMPFormat.KMPSection.HPKC_Section HPKC)
            {
                TPKC_HPKC_WritePosition tPKC_HPKC_WritePosition = new TPKC_HPKC_WritePosition
                {
                    TPKC = 0,
                    HPKC = 0
                };

                #region TPKC
                tPKC_HPKC_WritePosition.TPKC = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPKC.TPKCHeader);
                bw.Write(TPKC.NumOfEntries);
                bw.Write(TPKC.AdditionalValue);

                for (int Count = 0; Count < TPKC.TPKCValue_List.Count; Count++)
                {
                    bw.Write(KMPHelper.Vector3DTo2DConverter.Vector2ToByteArray(TPKC.TPKCValue_List[Count].TPKC_2DPosition_Left)[0]);
                    bw.Write(KMPHelper.Vector3DTo2DConverter.Vector2ToByteArray(TPKC.TPKCValue_List[Count].TPKC_2DPosition_Left)[1]);
                    bw.Write(KMPHelper.Vector3DTo2DConverter.Vector2ToByteArray(TPKC.TPKCValue_List[Count].TPKC_2DPosition_Right)[0]);
                    bw.Write(KMPHelper.Vector3DTo2DConverter.Vector2ToByteArray(TPKC.TPKCValue_List[Count].TPKC_2DPosition_Right)[1]);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_RespawnID);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_Checkpoint_Type);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_PreviousCheckPoint);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_NextCheckPoint);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_UnkBytes1);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_UnkBytes2);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_UnkBytes3);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_UnkBytes4);

                }
                #endregion

                #region HPKC
                tPKC_HPKC_WritePosition.HPKC = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(HPKC.HPKCHeader);
                bw.Write(HPKC.NumOfEntries);
                bw.Write(HPKC.AdditionalValue);

                for (int Count = 0; Count < HPKC.NumOfEntries; Count++)
                {
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_StartPoint);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_Length);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev0);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev1);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev2);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev3);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev4);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev5);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next0);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next1);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next2);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next3);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next4);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next5);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_UnkBytes1);
                }
                #endregion

                return tPKC_HPKC_WritePosition;
            }

            public static uint Write_JBOG(BinaryWriter bw, KMPFormat.KMPSection.JBOG_Section JBOG)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(JBOG.JBOGHeader);
                bw.Write(JBOG.NumOfEntries);
                bw.Write(JBOG.AdditionalValue);

                for (int Count = 0; Count < JBOG.NumOfEntries; Count++)
                {
                    bw.Write(JBOG.JBOGValue_List[Count].ObjectID);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_UnkByte1);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Position)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Position)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Position)[2]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Rotation)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Rotation)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Rotation)[2]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Scale)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Scale)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Scale)[2]);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_ITOP_RouteIDIndex);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value0);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value1);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value2);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value3);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value4);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value5);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value6);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value7);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_PresenceSetting);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_UnkByte2);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_UnkByte3);
                }

                return WritePosition;
            }

            public static uint Write_ITOP(BinaryWriter bw, KMPFormat.KMPSection.ITOP_Section ITOP)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(ITOP.ITOPHeader);
                bw.Write(ITOP.ITOP_NumberOfRoute);
                bw.Write(ITOP.ITOP_NumberOfPoint);

                for (int ITOP_RoutesCount = 0; ITOP_RoutesCount < ITOP.ITOP_NumberOfRoute; ITOP_RoutesCount++)
                {
                    bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Route_NumOfPoint);
                    bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_RouteSetting1);
                    bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_RouteSetting2);

                    for (int ITOP_PointsCount = 0; ITOP_PointsCount < ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Route_NumOfPoint; ITOP_PointsCount++)
                    {
                        bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position)[0]);
                        bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position)[1]);
                        bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position)[2]);
                        bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_RouteSpeed);
                        bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_PointSetting2);
                    }
                }

                return WritePosition;
            }

            public static uint Write_AERA(BinaryWriter bw, KMPFormat.KMPSection.AERA_Section AERA)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(AERA.AERAHeader);
                bw.Write(AERA.NumOfEntries);
                bw.Write(AERA.AdditionalValue);

                for (int Count = 0; Count < AERA.NumOfEntries; Count++)
                {
                    bw.Write(AERA.AERAValue_List[Count].AreaMode);
                    bw.Write(AERA.AERAValue_List[Count].AreaType);
                    bw.Write(AERA.AERAValue_List[Count].AERA_EMACIndex);
                    bw.Write(AERA.AERAValue_List[Count].Priority);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Position)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Position)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Position)[2]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Rotation)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Rotation)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Rotation)[2]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Scale)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Scale)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Scale)[2]);
                    bw.Write(AERA.AERAValue_List[Count].AERA_UnkByte1);
                    bw.Write(AERA.AERAValue_List[Count].AERA_UnkByte2);
                    bw.Write(AERA.AERAValue_List[Count].RouteID);
                    bw.Write(AERA.AERAValue_List[Count].EnemyID);
                    bw.Write(AERA.AERAValue_List[Count].AERA_UnkByte4);
                }

                return WritePosition;
            }

            public static uint Write_EMAC(BinaryWriter bw, KMPFormat.KMPSection.EMAC_Section EMAC)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(EMAC.EMACHeader);
                bw.Write(EMAC.NumOfEntries);
                bw.Write(EMAC.AdditionalValue);

                for (int Count = 0; Count < EMAC.NumOfEntries; Count++)
                {
                    bw.Write(EMAC.EMACValue_List[Count].CameraType);
                    bw.Write(EMAC.EMACValue_List[Count].NextCameraIndex);
                    bw.Write(EMAC.EMACValue_List[Count].EMAC_UnkBytes1);
                    bw.Write(EMAC.EMACValue_List[Count].EMAC_ITOP_CameraIndex);
                    bw.Write(EMAC.EMACValue_List[Count].RouteSpeed);
                    bw.Write(EMAC.EMACValue_List[Count].FOVSpeed);
                    bw.Write(EMAC.EMACValue_List[Count].ViewpointSpeed);
                    bw.Write(EMAC.EMACValue_List[Count].EMAC_UnkBytes2);
                    bw.Write(EMAC.EMACValue_List[Count].EMAC_UnkBytes3);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Position)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Position)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Position)[2]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Rotation)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Rotation)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Rotation)[2]);
                    bw.Write(EMAC.EMACValue_List[Count].FOVAngle_Start);
                    bw.Write(EMAC.EMACValue_List[Count].FOVAngle_End);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Start)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Start)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Start)[2]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Destination)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Destination)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Destination)[2]);
                    bw.Write(EMAC.EMACValue_List[Count].Camera_Active_Time);
                }

                return WritePosition;
            }

            public static uint Write_TPGJ(BinaryWriter bw, KMPFormat.KMPSection.TPGJ_Section TPGJ)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPGJ.TPGJHeader);
                bw.Write(TPGJ.NumOfEntries);
                bw.Write(TPGJ.AdditionalValue);

                for (int Count = 0; Count < TPGJ.NumOfEntries; Count++)
                {
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Position)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Position)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Position)[2]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Rotation)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Rotation)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Rotation)[2]);
                    bw.Write(TPGJ.TPGJValue_List[Count].TPGJ_RespawnID);
                    bw.Write(TPGJ.TPGJValue_List[Count].TPGJ_UnkBytes1);
                }

                return WritePosition;
            }

            //Unused Section
            public static uint Write_TPNC(BinaryWriter bw, KMPFormat.KMPSection.TPNC_Section TPNC)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPNC.TPNCHeader);
                bw.Write(TPNC.NumOfEntries);
                bw.Write(TPNC.AdditionalValue);

                return WritePosition;
            }

            //Unused Section
            public static uint Write_TPSM(BinaryWriter bw, KMPFormat.KMPSection.TPSM_Section TPSM)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPSM.TPSMHeader);
                bw.Write(TPSM.NumOfEntries);
                bw.Write(TPSM.AdditionalValue);

                return WritePosition;
            }

            public static uint Write_IGTS(BinaryWriter bw, KMPFormat.KMPSection.IGTS_Section IGTS)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(IGTS.IGTSHeader);
                bw.Write(IGTS.Unknown1);
                bw.Write(IGTS.LapCount);
                bw.Write(IGTS.PolePosition);
                bw.Write(IGTS.Unknown2);
                bw.Write(IGTS.Unknown3);
                bw.Write(IGTS.RGBAColor.R);
                bw.Write(IGTS.RGBAColor.G);
                bw.Write(IGTS.RGBAColor.B);
                bw.Write(IGTS.RGBAColor.A);
                bw.Write(IGTS.FlareAlpha);
                return WritePosition;
            }

            //Unused Section
            public static uint Write_SROC(BinaryWriter bw, KMPFormat.KMPSection.SROC_Section SROC)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(SROC.SROCHeader);
                bw.Write(SROC.NumOfEntries);
                bw.Write(SROC.AdditionalValue);

                return WritePosition;
            }

            public class TPLG_HPLG_WritePosition
            {
                public uint TPLG { get; set; }
                public uint HPLG { get; set; }
            }

            public static TPLG_HPLG_WritePosition Write_TPLG_HPLG(BinaryWriter bw, KMPFormat.KMPSection.TPLG_Section TPLG, KMPFormat.KMPSection.HPLG_Section HPLG)
            {
                TPLG_HPLG_WritePosition tPLG_HPLG_WritePosition = new TPLG_HPLG_WritePosition
                {
                    TPLG = 0,
                    HPLG = 0
                };

                #region TPLG

                tPLG_HPLG_WritePosition.TPLG = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPLG.TPLGHeader);
                bw.Write(TPLG.NumOfEntries);
                bw.Write(TPLG.AdditionalValue);

                for (int Count = 0; Count < TPLG.NumOfEntries; Count++)
                {
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG.TPLGValue_List[Count].TPLG_Position)[0]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG.TPLGValue_List[Count].TPLG_Position)[1]);
                    bw.Write(KMPHelper.Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG.TPLGValue_List[Count].TPLG_Position)[2]);
                    bw.Write(TPLG.TPLGValue_List[Count].TPLG_PointScaleValue);
                    bw.Write(TPLG.TPLGValue_List[Count].TPLG_UnkBytes1);
                    bw.Write(TPLG.TPLGValue_List[Count].TPLG_UnkBytes2);
                }
                #endregion

                #region HPKC
                tPLG_HPLG_WritePosition.HPLG = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(HPLG.HPLGHeader);
                bw.Write(HPLG.NumOfEntries);
                bw.Write(HPLG.AdditionalValue);

                for (int Count = 0; Count < HPLG.NumOfEntries; Count++)
                {
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_StartPoint);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_Length);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev0);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev1);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev2);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev3);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev4);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev5);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next0);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next1);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next2);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next3);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next4);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next5);
                    bw.Write(HPLG.HPLGValue_List[Count].RouteSetting);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_UnkBytes2);
                }
                #endregion

                return tPLG_HPLG_WritePosition;
            }
        }
    }
}
