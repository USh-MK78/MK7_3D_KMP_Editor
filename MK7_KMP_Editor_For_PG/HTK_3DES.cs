using HelixToolkit.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static MK7_3D_KMP_Editor.HTK_3DES.PathTools.Rail;

namespace MK7_3D_KMP_Editor
{
    public class HTK_3DES
    {
        #region static
        /// <summary>
        /// objファイルを読み込み、ModelVisual3Dを返すメソッド
        /// </summary>
        /// <param name="Path">Model Path</param>
        /// <returns>ModelVisual3D</returns>
        public static ModelVisual3D OBJReader(string Path)
        {
            ModelVisual3D ModelVisual3D = new ModelVisual3D();
            ObjReader objRead = new ObjReader();
            ModelVisual3D.Content = objRead.Read(Path);

            return ModelVisual3D;
        }

        /// <summary>
        /// ガベージコレクション
        /// </summary>
        public static void GC_Dispose(object f)
        {
            int GCNum = GC.GetGeneration(f);

            GC.Collect(GCNum);
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// ModelVisual3Dに文字列を関連付けて新しいModelVisual3Dを生成する
        /// </summary>
        /// <param name="MV3D">Input ModelVisual3D</param>
        /// <param name="InputString">Input String</param>
        /// <returns></returns>
        public static ModelVisual3D SetStringAndNewMV3D(ModelVisual3D MV3D, string InputString)
        {
            MV3D.SetName(InputString);
            return MV3D;
        }

        /// <summary>
        /// ModelVisual3Dに文字列を関連付ける
        /// </summary>
        /// <param name="MV3D">Input ModelVisual3D</param>
        /// <param name="InputString">Input String</param>
        public static void SetString_MV3D(ModelVisual3D MV3D, string InputString)
        {
            MV3D.SetName(InputString);
        }

        public static Vector3D ScaleFactor(Vector3D InputVector3D, double ScaleFactor)
        {
            return new Vector3D(InputVector3D.X * ScaleFactor, InputVector3D.Y * ScaleFactor, InputVector3D.Z * ScaleFactor);
        }

        public static Vector3D ScaleFactor(float PointSize, double ScaleFactor)
        {
            return new Vector3D(PointSize * ScaleFactor, PointSize * ScaleFactor, PointSize * ScaleFactor);
        }

        /// <summary>
        /// Radianを角度に変換
        /// </summary>
        /// <param name="Radian"></param>
        /// <returns></returns>
        public static float RadianToAngle(double Radian)
        {
            return (float)(Radian * (180 / Math.PI));
        }

        /// <summary>
        /// 角度をRadianに変換
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        public static double AngleToRadian(double Angle)
        {
            return (float)(Angle * (Math.PI / 180));
        }

        public static Vector3D RadianToAngleVector3D(Vector3D vector3D)
        {
            return new Vector3D((float)(vector3D.X * (180 / Math.PI)), (float)(vector3D.Y * (180 / Math.PI)), (float)(vector3D.Z * (180 / Math.PI)));
        }

        public static Vector3D AngleToRadianVector3D(Vector3D vector3D)
        {
            return new Vector3D((float)(vector3D.X * (Math.PI / 180)), (float)(vector3D.Y * (Math.PI / 180)), (float)(vector3D.Z * (Math.PI / 180)));
        }

        public static Point3D CalculateModelCenterPoint(ModelVisual3D MV3D)
        {
            Rect3D r = MV3D.Content.Bounds;
            double cX = r.X + r.SizeX / 2;
            double cY = r.Y + r.SizeY / 2;
            double cZ = r.Z + r.SizeZ / 2;
            Point3D P3 = new Point3D(cX, cY, cZ);

            return P3;
        }

        public static Point3D CalculateModelCenterPoint(Model3D MV3D)
        {
            Rect3D r = MV3D.Bounds;
            double cX = r.X + r.SizeX / 2;
            double cY = r.Y + r.SizeY / 2;
            double cZ = r.Z + r.SizeZ / 2;
            Point3D P3 = new Point3D(cX, cY, cZ);

            return P3;
        }

        public static Vector3D Scalefactor(Vector3D v, double Factor)
        {
            return new Vector3D(v.X / Factor, v.Y / Factor, v.Z / Factor);
        }
        #endregion

        #region Transform
        public class Transform
        {
            public Vector3D Rotate3D { get; set; }
            public Vector3D Scale3D { get; set; }
            public Vector3D Translate3D { get; set; }

            public Transform(Vector3D Rotate, Vector3D Scale, Vector3D Translate)
            {
                Rotate3D = Rotate;
                Scale3D = Scale;
                Translate3D = Translate;
            }

            public Transform() { }
        }

        public class TSRSystem3D
        {
            public Transform Transform { get; set; } = new Transform();
            public ModelVisual3D InputMV3D { get; set; }
            public Model3D InputM3D;
            public bool IsContent;

            //public ModelVisual3D InputMV3D;
            //public Model3D M3D
            //{
            //    get
            //    {
            //        return InputMV3D.Content ?? null;
            //    }
            //    set
            //    {
            //        InputMV3D = new ModelVisual3D { Content = M3D };
            //    }
            //}

            public TSRSystem3D()
            {
                return;
            }

            /// <summary>
            /// TSRSystem3Dの初期化
            /// </summary>
            /// <param name="MV3D"></param>
            /// <param name="transform"></param>
            public TSRSystem3D(ModelVisual3D MV3D, Transform transform)
            {
                InputMV3D = MV3D;
                InputM3D = null;
                Transform = transform;
                IsContent = MV3D.Content != null ? true : false;
            }

            /// <summary>
            /// TSRSystem3Dの初期化
            /// </summary>
            /// <param name="MV3D"></param>
            /// <param name="transform"></param>
            public TSRSystem3D(Model3D M3D, Transform transform)
            {
                InputMV3D = null;
                InputM3D = M3D;
                Transform = transform;
                IsContent = true;
            }

            #region Rotation
            public RotateTransform3D Rotate_X { get; set; } = new RotateTransform3D();
            public RotateTransform3D Rotate_Y { get; set; } = new RotateTransform3D();
            public RotateTransform3D Rotate_Z { get; set; } = new RotateTransform3D();

            //public RotationCenterSetting RotationCenterSettings { get; }
            public class RotationCenterSetting
            {
                public Vector3D RotationX { get; set; } = new Vector3D(1, 0, 0);
                public Vector3D RotationY { get; set; } = new Vector3D(0, 1, 0);
                public Vector3D RotationZ { get; set; } = new Vector3D(0, 0, 1);

                public RotationCenterSetting(Vector3D Rotation_X, Vector3D Rotation_Y, Vector3D Rotation_Z)
                {
                    RotationX = Rotation_X;
                    RotationY = Rotation_Y;
                    RotationZ = Rotation_Z;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public static RotationCenterSetting DefaultCenterSetting()
                {
                    return new RotationCenterSetting(new Vector3D(1, 0, 0), new Vector3D(0, 1, 0), new Vector3D(0, 0, 1));
                }

                public RotationCenterSetting()
                {
                    RotationX = new Vector3D(1, 0, 0);
                    RotationY = new Vector3D(0, 1, 0);
                    RotationZ = new Vector3D(0, 0, 1);
                }
            }

            public enum RotationType
            {
                Angle,
                Radian
            }

            public void TSR_Rotate(RotationCenterSetting RotationCenterSettings, RotationType RotationSettings = RotationType.Angle)
            {
                double RotateX = new double();
                double RotateY = new double();
                double RotateZ = new double();

                if (RotationSettings == RotationType.Angle)
                {
                    RotateX = Transform.Rotate3D.X;
                    RotateY = Transform.Rotate3D.Y;
                    RotateZ = Transform.Rotate3D.Z;
                }
                if (RotationSettings == RotationType.Radian)
                {
                    RotateX = RadianToAngle(Transform.Rotate3D.X);
                    RotateY = RadianToAngle(Transform.Rotate3D.Y);
                    RotateZ = RadianToAngle(Transform.Rotate3D.Z);
                }

                Rotate_X.Rotation = new QuaternionRotation3D(new Quaternion(RotationCenterSettings.RotationX, RotateX));
                Rotate_Y.Rotation = new QuaternionRotation3D(new Quaternion(RotationCenterSettings.RotationY, RotateY));
                Rotate_Z.Rotation = new QuaternionRotation3D(new Quaternion(RotationCenterSettings.RotationZ, RotateZ));
            }
            #endregion

            #region Scale
            public ScaleTransform3D ScaleTransform3D;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ScaleFactor"></param>
            /// <param name="Center"></param>
            public void TSR_Scale3D(double ScaleFactorValue = 2, Point3D? Center = null, bool CenterFlag = false)
            {
                if (CenterFlag == true) ScaleTransform3D = new ScaleTransform3D(Scalefactor(Transform.Scale3D, ScaleFactorValue), Center ?? new Point3D(0, 0, 0));
                if (CenterFlag == false) ScaleTransform3D = new ScaleTransform3D(Scalefactor(Transform.Scale3D, ScaleFactorValue));
            }
            #endregion

            #region Translate
            public TranslateTransform3D TranslateTransform3D;
            public void TSR_Translate3D()
            {
                TranslateTransform3D = new TranslateTransform3D(Transform.Translate3D);
            }
            #endregion

            #region Transform
            public ContentType GetContentType
            {
                get
                {
                    ContentType contentType = new ContentType();
                    if ((InputMV3D != null && InputM3D == null) == true) contentType = ContentType.ModelVisual3D;
                    else if ((InputMV3D == null && InputM3D != null) == true) contentType = ContentType.Model3D;
                    return contentType;
                }
            }

            public enum ContentType
            {
                ModelVisual3D,
                Model3D
            }

            public void StartTransform()
            {
                Transform3DCollection T3D_Collection = new Transform3DCollection();
                T3D_Collection.Add(ScaleTransform3D);
                T3D_Collection.Add(Rotate_X);
                T3D_Collection.Add(Rotate_Y);
                T3D_Collection.Add(Rotate_Z);
                T3D_Collection.Add(TranslateTransform3D);

                Transform3DGroup T3DGroup = new Transform3DGroup { Children = T3D_Collection };

                if (GetContentType == ContentType.ModelVisual3D)
                {
                    if (IsContent == true) InputMV3D.Content.Transform = T3DGroup;
                    else if (IsContent == false) InputMV3D.Transform = T3DGroup;
                }
                else if (GetContentType == ContentType.Model3D)
                {
                    InputM3D.Transform = T3DGroup;
                }
            }

            public void Transform3D(RotationCenterSetting rotationCenterSetting, RotationType rotationType = RotationType.Angle)
            {
                TSRSystem3D tSRSystem3D = null;
                if (GetContentType == ContentType.ModelVisual3D) tSRSystem3D = new TSRSystem3D(InputMV3D, Transform);
                else if (GetContentType == ContentType.Model3D) tSRSystem3D = new TSRSystem3D(InputM3D, Transform);
                tSRSystem3D.TSR_Rotate(rotationCenterSetting, rotationType);
                tSRSystem3D.TSR_Scale3D();
                tSRSystem3D.TSR_Translate3D();
                tSRSystem3D.StartTransform();
            }

            public void Transform3D(RotationCenterSetting rotationCenterSetting, RotationType rotationType, double ScaleFactor = 2, Point3D? Center = null, bool CenterFlag = false)
            {
                TSRSystem3D tSRSystem3D = null;
                if (GetContentType == ContentType.ModelVisual3D) tSRSystem3D = new TSRSystem3D(InputMV3D, Transform);
                else if (GetContentType == ContentType.Model3D) tSRSystem3D = new TSRSystem3D(InputM3D, Transform);
                tSRSystem3D.TSR_Rotate(rotationCenterSetting, rotationType);
                tSRSystem3D.TSR_Scale3D(ScaleFactor, Center, CenterFlag);
                tSRSystem3D.TSR_Translate3D();
                tSRSystem3D.StartTransform();
            }
            #endregion
        }
        #endregion

        public class UnionModel3D
        {
            ///// <summary>
            ///// Point3DのListからModelVisual3Dを生成
            ///// </summary>
            ///// <param name="P3DList">Point3D_List</param>
            ///// <param name="LV3D_List">LineVisual3D_List</param>
            ///// <param name="colors">Set Color</param>
            ///// <returns>List<ModelVisual3D>List<ModelVisual3D></returns>
            //public List<ModelVisual3D> CustomModelCreate(List<Point3D> P3DList, List<LinesVisual3D> LV3D_List, System.Windows.Media.Color colors)
            //{
            //    //List<Point3D>を使用して線を描く
            //    for (int i = 0; i < P3DList.Count; i++)
            //    {
            //        LV3D_List.Add(new LinesVisual3D { Points = new Point3DCollection(P3DList), Color = colors });
            //    }

            //    List<ModelVisual3D> ConvertLV3DToMV3D_List = new List<ModelVisual3D>();

            //    for (int LV3DCount = 0; LV3DCount < LV3D_List.Count; LV3DCount++)
            //    {
            //        //LinesVisual3DをModel3Dに変換
            //        Model3D LV3DToM3D = LV3D_List[LV3DCount].Content;
            //        ModelVisual3D M3DToMV3D = new ModelVisual3D { Content = LV3DToM3D };

            //        //Add
            //        ConvertLV3DToMV3D_List.Add(M3DToMV3D);
            //    }

            //    return ConvertLV3DToMV3D_List;
            //}

            public Model3DGroup UnionModelVisual3DGroup;
            public List<ModelVisual3D> InputModelVisual3DList { get; set; }

            /// <summary>
            /// List<ModelVisual3D>を1つのModelVisual3Dに結合する
            /// </summary>
            /// <param name="MV3D_List"></param>
            /// <returns>ModelVisual3D</returns>
            public ModelVisual3D UnionModelVisual3D()
            {
                for (int Count = 0; Count < InputModelVisual3DList.Count; Count++) UnionModelVisual3DGroup.Children.Add(InputModelVisual3DList[Count].Content);
                ModelVisual3D JoinedMV3D = new ModelVisual3D { Content = UnionModelVisual3DGroup };
                return JoinedMV3D;
            }

            public void UpdateModel3DGroup(Model3DGroup model3DGroup)
            {
                UnionModelVisual3DGroup = model3DGroup;
            }

            public Model3DGroup GetModel3DGroup()
            {
                return UnionModelVisual3DGroup;
            }

            public UnionModel3D(List<ModelVisual3D> InputMV3DList)
            {
                UnionModelVisual3DGroup = new Model3DGroup();
                InputModelVisual3DList = InputMV3DList;
            }
        }

        public class HitTestHelper
        {
            public class Search
            {
                public enum HitTestType
                {
                    Adorner,
                    Geometry,
                    Point,
                    Ray,
                    RayMeshGeometry3D
                }

                public static HitTestResult HitTestViewport(Visual Target, Point Point2D, HitTestType hitTestType)
                {
                    HitTestResult HTR = null;
                    HitTestResult HTRs = VisualTreeHelper.HitTest(Target, Point2D);
                    if (hitTestType == HitTestType.Adorner) HTR = HTRs as AdornerHitTestResult;
                    if (hitTestType == HitTestType.Geometry) HTR = HTRs as GeometryHitTestResult;
                    if (hitTestType == HitTestType.Point) HTR = HTRs as PointHitTestResult;
                    if (hitTestType == HitTestType.Ray) HTR = HTRs as RayHitTestResult;
                    if (hitTestType == HitTestType.RayMeshGeometry3D) HTR = HTRs as RayMeshGeometry3DHitTestResult;

                    return HTR;
                    //return HTRs as RayMeshGeometry3DHitTestResult;
                }
            }

            //public static T GetObjectName<T>(ModelVisual3D FindMV3D, HitTestResult hitTestResult, )
            //{
            //    object MDLStr_GetName = new object();
            //    if (typeof(ModelVisual3D) == hitTestResult.VisualHit.GetType())
            //    {
            //        //ダウンキャスト
            //        FindMV3D = (ModelVisual3D)hitTestResult.VisualHit;
            //        MDLStr_GetName = HTR.VisualHit.GetName().Split(' ');
            //    }
            //    if (typeof(LinesVisual3D) == HTR.VisualHit.GetType()) return;
            //    if (typeof(TubeVisual3D) == HTR.VisualHit.GetType()) return;


            //    return (T)MDLStr_GetName;
            //}

        }

        public class PathTools
        {
            public class Rail
            {
                public enum RailType
                {
                    Line,
                    Tube
                }

                public enum PointType
                {
                    Model,
                    Point3D
                }

                public RailType PathRailType { get; set; }
                public PointType PathPointType { get; set; }

                public List<ModelVisual3D> BasePointModelList { get; set; } = null;
                public List<Point3D> Point3DList { get; set; } = null;
                public List<LinesVisual3D> LV3D_List { get; set; }
                public List<TubeVisual3D> TV3D_List { get; set; }

                /// <summary>
                /// Initialize Rail
                /// </summary>
                /// <param name="BaseModelList">List<ModelVisual3D></param>
                /// <param name="railType">RailType</param>
                public Rail(List<ModelVisual3D> BaseModelList, RailType railType = RailType.Line)
                {
                    PathPointType = PointType.Model;
                    this.BasePointModelList = BaseModelList;

                    PathRailType = railType;
                    if (railType == RailType.Line)
                    {
                        LV3D_List = new List<LinesVisual3D>();
                        TV3D_List = null;
                    }
                    else if (railType == RailType.Tube)
                    {
                        LV3D_List = null;
                        TV3D_List = new List<TubeVisual3D>();
                    }
                }

                /// <summary>
                /// Initialize Rail
                /// </summary>
                /// <param name="Point3DList">List<Point3D></param>
                /// <param name="railType"></param>
                public Rail(List<Point3D> Point3DList, RailType railType = RailType.Line)
                {
                    PathPointType = PointType.Point3D;
                    this.Point3DList = Point3DList;

                    PathRailType = railType;
                    if (railType == RailType.Line)
                    {
                        LV3D_List = new List<LinesVisual3D>();
                        TV3D_List = null;
                    }
                    else if (railType == RailType.Tube)
                    {
                        LV3D_List = null;
                        TV3D_List = new List<TubeVisual3D>();
                    }
                }

                /// <summary>
                /// Initialize Rail
                /// </summary>
                /// <param name="pointType"></param>
                /// <param name="railType"></param>
                public Rail(PointType pointType = PointType.Point3D, RailType railType = RailType.Line)
                {
                    if (pointType == PointType.Point3D)
                    {
                        PathPointType = PointType.Point3D;
                        Point3DList = new List<Point3D>();
                    }
                    else if (pointType == PointType.Model)
                    {
                        PathPointType = PointType.Model;
                        BasePointModelList = new List<ModelVisual3D>();
                    }

                    PathRailType = railType;
                    if (railType == RailType.Line)
                    {
                        LV3D_List = new List<LinesVisual3D>();
                        TV3D_List = null;
                    }
                    else if (railType == RailType.Tube)
                    {
                        LV3D_List = null;
                        TV3D_List = new List<TubeVisual3D>();
                    }
                }

                public List<Point3D> MV3DListToPoint3DList()
                {
                    List<Point3D> point3Ds = new List<Point3D>();
                    if (PathPointType == PointType.Model)
                    {
                        for (int i = 0; i < BasePointModelList.Count; i++)
                        {
                            Model3D n = BasePointModelList[i].Content;
                            Point3D p3d = new Point3D(n.Transform.Value.OffsetX, n.Transform.Value.OffsetY, n.Transform.Value.OffsetZ);
                            point3Ds.Add(p3d);
                        }
                    }
                    else if (PathPointType == PointType.Point3D)
                    {
                        point3Ds = Point3DList;
                    }

                    return point3Ds;
                }

                /// <summary>
                /// Draw Rail (Line)
                /// </summary>
                /// <param name="UserCtrl"></param>
                /// <param name="Thickness"></param>
                /// <param name="color"></param>
                /// <returns></returns>
                public List<LinesVisual3D> DrawPath_Line(UserControl1 UserCtrl, double Thickness, Color color)
                {
                    List<Point3D> Point3D_List = MV3DListToPoint3DList();

                    if (Point3D_List.Count > 1)
                    {
                        for (int i = 1; i < Point3D_List.Count; i++)
                        {
                            List<Point3D> OneLine = new List<Point3D>();
                            OneLine.Add(Point3D_List[i - 1]);
                            OneLine.Add(Point3D_List[i]);

                            LinesVisual3D linesVisual3D = new LinesVisual3D { Points = new Point3DCollection(OneLine), Thickness = Thickness, Color = color };
                            UserCtrl.MainViewPort.Children.Add(linesVisual3D);
                            LV3D_List.Add(linesVisual3D);
                        }
                    }


                    return LV3D_List;
                }

                /// <summary>
                /// Draw Rail (Tube)
                /// </summary>
                /// <param name="UserCtrl"></param>
                /// <param name="TubeDiametor"></param>
                /// <param name="color"></param>
                /// <returns></returns>
                public List<TubeVisual3D> DrawPath_Tube(UserControl1 UserCtrl, double TubeDiametor, Color color)
                {
                    List<Point3D> Point3D_List = MV3DListToPoint3DList();

                    if (Point3D_List.Count > 1)
                    {
                        for (int i = 1; i < Point3D_List.Count; i++)
                        {
                            //TubeVisual3Dの直径を指定
                            double Diametor_Value = TubeDiametor;

                            TubeVisual3D tubeVisual3D = new TubeVisual3D();
                            tubeVisual3D.Fill = new SolidColorBrush(color);
                            tubeVisual3D.Path = new Point3DCollection();
                            tubeVisual3D.Path.Add(Point3D_List[i - 1]);
                            tubeVisual3D.Path.Add(Point3D_List[i]);
                            tubeVisual3D.Diameter = Diametor_Value;
                            tubeVisual3D.IsPathClosed = false;

                            TV3D_List.Add(tubeVisual3D);

                            //Add Tube
                            UserCtrl.MainViewPort.Children.Add(tubeVisual3D);
                        }
                    }

                    return TV3D_List;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="MDLNum"></param>
                /// <param name="Pos"></param>
                /// <param name="railType"></param>
                public void MoveRails(int MDLNum, Vector3D Pos, RailType railType)
                {
                    if (railType == RailType.Line)
                    {
                        if (MDLNum == 0)
                        {
                            LV3D_List[MDLNum].Points[0] = (Point3D)Pos;
                        }
                        if (MDLNum > 0 && MDLNum < LV3D_List.Count)
                        {
                            LV3D_List[MDLNum - 1].Points[1] = (Point3D)Pos;
                            LV3D_List[MDLNum].Points[0] = (Point3D)Pos;
                        }
                        if (MDLNum == LV3D_List.Count)
                        {
                            LV3D_List[MDLNum - 1].Points[1] = (Point3D)Pos;
                        }
                    }
                    else if (railType == RailType.Tube)
                    {
                        if (MDLNum == 0)
                        {
                            TV3D_List[MDLNum].Path[0] = (Point3D)Pos;
                        }
                        if (MDLNum > 0 && MDLNum < TV3D_List.Count)
                        {
                            TV3D_List[MDLNum - 1].Path[1] = (Point3D)Pos;
                            TV3D_List[MDLNum].Path[0] = (Point3D)Pos;
                        }
                        if (MDLNum == TV3D_List.Count)
                        {
                            TV3D_List[MDLNum - 1].Path[1] = (Point3D)Pos;
                        }
                    }
                }

                /// <summary>
                /// Delete Rail
                /// </summary>
                /// <param name="UserCtrl"></param>
                public void DeleteRail(UserControl1 UserCtrl)
                {
                    if (TV3D_List != null)
                    {
                        for (int TVCount = 0; TVCount < TV3D_List.Count; TVCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(TV3D_List[TVCount]);
                            UserCtrl.UpdateLayout();
                        }

                        TV3D_List.Clear();
                    }

                    if (LV3D_List != null)
                    {
                        for (int LVCount = 0; LVCount < LV3D_List.Count; LVCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(LV3D_List[LVCount]);
                            UserCtrl.UpdateLayout();
                        }

                        LV3D_List.Clear();
                    }

                    if (BasePointModelList != null)
                    {
                        for (int MV3DCount = 0; MV3DCount < BasePointModelList.Count; MV3DCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(BasePointModelList[MV3DCount]);
                            UserCtrl.UpdateLayout();
                        }

                        BasePointModelList.Clear();
                    }

                    if (Point3DList != null)
                    {
                        Point3DList.Clear();
                    }
                }

                /// <summary>
                /// Insert Point (ModelVisual3D)
                /// </summary>
                /// <param name="UserCtrl"></param>
                /// <param name="MV3D"></param>
                /// <param name="Index"></param>
                /// <param name="TubeDiametor"></param>
                /// <param name="color"></param>
                public void InsertRailPoint(UserControl1 UserCtrl, ModelVisual3D MV3D, int Index, double TubeDiametor, Color color)
                {
                    if (PathPointType == PointType.Model)
                    {
                        BasePointModelList.Insert(Index, MV3D);

                        UserCtrl.MainViewPort.Children.Insert(Index, BasePointModelList[Index]);

                        //ReInput
                        for (int i = 0; i < BasePointModelList.Count; i++)
                        {
                            string[] MDLInfo = BasePointModelList[i].GetName().Split(' ');
                            string New_MDLInfo = MDLInfo[0] + " " + i.ToString() + " " + MDLInfo[2];
                            BasePointModelList[i].SetName(New_MDLInfo);
                        }
                    }

                    //Reset
                    ResetRail(UserCtrl);

                    //ReDraw
                    if (PathRailType == RailType.Line)
                    {
                        DrawPath_Line(UserCtrl, TubeDiametor, color);
                    }
                    else if (PathRailType == RailType.Tube)
                    {
                        DrawPath_Tube(UserCtrl, TubeDiametor, color);
                    }
                }

                /// <summary>
                /// Insert Point (Point3D)
                /// </summary>
                /// <param name="UserCtrl"></param>
                /// <param name="Pos"></param>
                /// <param name="Index"></param>
                /// <param name="TubeDiametor"></param>
                /// <param name="color"></param>
                public void InsertRailPoint(UserControl1 UserCtrl, Point3D Pos, int Index, double TubeDiametor, Color color)
                {
                    if (PathPointType == PointType.Point3D)
                    {
                        Point3DList.Insert(Index, Pos);
                    }

                    //Reset
                    ResetRail(UserCtrl);

                    //ReDraw
                    if (PathRailType == RailType.Line)
                    {
                        DrawPath_Line(UserCtrl, TubeDiametor, color);
                    }
                    else if (PathRailType == RailType.Tube)
                    {
                        DrawPath_Tube(UserCtrl, TubeDiametor, color);
                    }
                }

                public void DeleteRailPoint(UserControl1 UserCtrl, int SelectedIdx, double TubeDiametor, Color color)
                {
                    if (PathPointType == PointType.Model)
                    {
                        UserCtrl.MainViewPort.Children.Remove(BasePointModelList[SelectedIdx]);

                        //Delete List
                        BasePointModelList.RemoveAt(SelectedIdx);

                        //ReInput
                        for (int i = 0; i < BasePointModelList.Count; i++)
                        {
                            string[] MDLInfo = BasePointModelList[i].GetName().Split(' ');
                            string New_MDLInfo = MDLInfo[0] + " " + i.ToString() + " " + MDLInfo[2];
                            BasePointModelList[i].SetName(New_MDLInfo);
                        }
                    }
                    else if (PathPointType == PointType.Point3D)
                    {
                        Point3DList.RemoveAt(SelectedIdx);
                    }

                    //Reset
                    ResetRail(UserCtrl);

                    //ReDraw
                    if (PathRailType == RailType.Line)
                    {
                        DrawPath_Line(UserCtrl, TubeDiametor, color);
                    }
                    else if (PathRailType == RailType.Tube)
                    {
                        DrawPath_Tube(UserCtrl, TubeDiametor, color);
                    }
                }

                /// <summary>
                /// Reset Rail
                /// </summary>
                /// <param name="UserCtrl"></param>
                public void ResetRail(UserControl1 UserCtrl)
                {
                    if (PathRailType == RailType.Line)
                    {
                        for (int i = 0; i < LV3D_List.Count; i++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(LV3D_List[i]);
                        }

                        LV3D_List.Clear();
                    }
                    else if (PathRailType == RailType.Tube)
                    {
                        for (int i = 0; i < TV3D_List.Count; i++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(TV3D_List[i]);
                        }

                        TV3D_List.Clear();
                    }

                    UserCtrl.UpdateLayout();
                }

                /// <summary>
                /// Reset Rail
                /// </summary>
                /// <param name="UserCtrl"></param>
                /// <param name="rail"></param>
                /// <param name="railType"></param>
                public void ResetRail(UserControl1 UserCtrl, RailType railType)
                {
                    PathRailType = railType;
                    if (railType == RailType.Line)
                    {
                        for (int i = 0; i < LV3D_List.Count; i++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(LV3D_List[i]);
                        }

                        LV3D_List.Clear();
                    }
                    else if (railType == RailType.Tube)
                    {
                        for (int i = 0; i < TV3D_List.Count; i++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(TV3D_List[i]);
                        }

                        TV3D_List.Clear();
                    }

                    UserCtrl.UpdateLayout();
                }
            }
        }

        public class Point3DSystem
        {
            /// <summary>
            /// List<DrawLine_Value>を使用してLinesVisual3Dを生成、ModelVisual3Dに変換する
            /// </summary>
            /// <param name="DrawPoint_Value_List">Point3D_List</param>
            /// <param name="colors">Set Color</param>
            /// <returns>List<ModelVisual3D>List<ModelVisual3D></returns>
            public ModelVisual3D DrawPointsVisual3D(Point3D DrawPoint3D, List<PointsVisual3D> PV3D_List, System.Windows.Media.Color colors, double PointSize)
            {
                List<Point3D> p3d = new List<Point3D>();
                p3d.Add(DrawPoint3D);
                PV3D_List.Add(new PointsVisual3D { Points = new Point3DCollection(p3d), Color = colors, Size = PointSize });
                return PV3D_List[0];
            }
        }

        public class KMP_3DCheckpointSystem : PathTools
        {
            //public class KMP_3DCheckpointSetting
            //{
            //    public enum SplitWallLineType
            //    {
            //        Line = 0,
            //        Tube = 1
            //    }

            //    public double Diametor { get; set; } = 5.0;
            //    public SplitWallLineType SplitWallLine_Type { get; set; } = SplitWallLineType.Line;
            //    public int CheckpointWallHeight { get; set; } = 500;

            //    public Color LeftPathColor { get; set; } = Colors.Green;
            //    public Color RightPathColor { get; set; } = Colors.Red;

            //    public LeftPathSetting LeftSetting { get; set; }
            //    public class LeftPathSetting
            //    {
            //        public Color Color { get; set; } = Colors.Green;
            //        public ModelVisual3D BaseModel { get; set; }

            //        public LeftPathSetting(Color LeftColor)
            //        {
            //            Color = LeftColor;
            //        }
            //    }

            //    public RightPathSetting RightSetting { get; set; }
            //    public class RightPathSetting
            //    {
            //        public Color Color { get; set; } = Colors.Red;
            //        public ModelVisual3D BaseModel { get; set; }

            //        public RightPathSetting(Color RightColor)
            //        {
            //            Color = RightColor;
            //        }
            //    }

            //    public KMP_3DCheckpointSetting DefaultSetting()
            //    {
            //        return new KMP_3DCheckpointSetting();
            //    }

            //    public KMP_3DCheckpointSetting(double Diametor, SplitWallLineType SplitWallLineType, int Height, Color Left, Color Right)
            //    {
            //        this.Diametor = Diametor;
            //        this.SplitWallLine_Type = SplitWallLineType;
            //        CheckpointWallHeight = Height;

            //        LeftPathColor = Left;
            //        RightPathColor = Right;
            //    }

            //    public KMP_3DCheckpointSetting()
            //    {
            //        Diametor = 5.0;
            //        SplitWallLine_Type = SplitWallLineType.Line;
            //        CheckpointWallHeight = 500;

            //        LeftPathColor = Colors.Green;
            //        RightPathColor = Colors.Red;
            //    }
            //}

            public class Checkpoint
            {
                //public KMP_3DCheckpointSetting CheckpointSetting { get; set; } = new KMP_3DCheckpointSetting();
                public Rail Checkpoint_Left { get; set; }
                public Rail Checkpoint_Right { get; set; }
                public List<ModelVisual3D> SideWall_Left { get; set; }
                public List<ModelVisual3D> SideWall_Right { get; set; }
                public List<LinesVisual3D> Checkpoint_Line { get; set; }
                public List<TubeVisual3D> Checkpoint_Tube { get; set; }
                public List<ModelVisual3D> Checkpoint_SplitWallMDL { get; set; }

                public Checkpoint()
                {
                    Checkpoint_Left = new Rail(PointType.Model, RailType.Line);
                    Checkpoint_Right = new Rail(PointType.Model, RailType.Line);
                    SideWall_Left = new List<ModelVisual3D>();
                    SideWall_Right = new List<ModelVisual3D>();
                    Checkpoint_Line = new List<LinesVisual3D>();
                    Checkpoint_Tube = new List<TubeVisual3D>();
                    Checkpoint_SplitWallMDL = new List<ModelVisual3D>();
                }

                public void DrawPath_SideWall(UserControl1 UserCtrl, Color LeftWallColor, Color RightWallColor, string LeftWallText = "SideWall -1 -1", string RightWallText = "SideWall -1 -1")
                {
                    if (Checkpoint_Left.MV3DListToPoint3DList().Count > 1)
                    {
                        for (int i = 1; i < Checkpoint_Left.MV3DListToPoint3DList().Count; i++)
                        {
                            #region Memo
                            //OneLine.Add(point3Ds[i - 1]); //1
                            //OneLine.Add(point3Ds[i]); //3
                            //OneLine.Add(new Point3D(point3Ds[i - 1].X, 0, point3Ds[i - 1].Z)); //0
                            //OneLine.Add(new Point3D(point3Ds[i].X, 0, point3Ds[i].Z)); //2
                            #endregion

                            List<Point3D> OneSiideWallLeftMDL = new List<Point3D>();
                            OneSiideWallLeftMDL.Add(new Point3D(Checkpoint_Left.MV3DListToPoint3DList()[i - 1].X, 0, Checkpoint_Left.MV3DListToPoint3DList()[i - 1].Z)); //0
                            OneSiideWallLeftMDL.Add(Checkpoint_Left.MV3DListToPoint3DList()[i - 1]); //1
                            OneSiideWallLeftMDL.Add(new Point3D(Checkpoint_Left.MV3DListToPoint3DList()[i].X, 0, Checkpoint_Left.MV3DListToPoint3DList()[i].Z)); //2
                            OneSiideWallLeftMDL.Add(Checkpoint_Left.MV3DListToPoint3DList()[i]); //3

                            ModelVisual3D SideWallLeft_MV3D = CustomModelCreateHelper.CustomRectanglePlane3D(CustomModelCreateHelper.ToPoint3DCollection(OneSiideWallLeftMDL), LeftWallColor, LeftWallColor);
                            HTK_3DES.SetString_MV3D(SideWallLeft_MV3D, LeftWallText);
                            UserCtrl.MainViewPort.Children.Add(SideWallLeft_MV3D);
                            SideWall_Left.Add(SideWallLeft_MV3D);
                        }
                    }
                    if (Checkpoint_Right.MV3DListToPoint3DList().Count > 1)
                    {
                        for (int i = 1; i < Checkpoint_Right.MV3DListToPoint3DList().Count; i++)
                        {
                            #region Memo
                            //OneLine.Add(point3Ds[i - 1]); //1
                            //OneLine.Add(point3Ds[i]); //3
                            //OneLine.Add(new Point3D(point3Ds[i - 1].X, 0, point3Ds[i - 1].Z)); //0
                            //OneLine.Add(new Point3D(point3Ds[i].X, 0, point3Ds[i].Z)); //2
                            #endregion

                            List<Point3D> OneSiideWallRightMDL = new List<Point3D>();
                            OneSiideWallRightMDL.Add(new Point3D(Checkpoint_Right.MV3DListToPoint3DList()[i - 1].X, 0, Checkpoint_Right.MV3DListToPoint3DList()[i - 1].Z)); //0
                            OneSiideWallRightMDL.Add(Checkpoint_Right.MV3DListToPoint3DList()[i - 1]); //1
                            OneSiideWallRightMDL.Add(new Point3D(Checkpoint_Right.MV3DListToPoint3DList()[i].X, 0, Checkpoint_Right.MV3DListToPoint3DList()[i].Z)); //2
                            OneSiideWallRightMDL.Add(Checkpoint_Right.MV3DListToPoint3DList()[i]); //3

                            ModelVisual3D SideWallRight_MV3D = CustomModelCreateHelper.CustomRectanglePlane3D(CustomModelCreateHelper.ToPoint3DCollection(OneSiideWallRightMDL), RightWallColor, RightWallColor);
                            HTK_3DES.SetString_MV3D(SideWallRight_MV3D, RightWallText);
                            UserCtrl.MainViewPort.Children.Add(SideWallRight_MV3D);
                            SideWall_Right.Add(SideWallRight_MV3D);
                        }
                    }
                }

                public enum SideWallType
                {
                    Left,
                    Right
                }

                public void MoveSideWalls(int MDLNum, Vector3D Pos, SideWallType sideWallType)
                {
                    List<ModelVisual3D> ModelVisual3D_List = null;
                    if (sideWallType == SideWallType.Left) ModelVisual3D_List = SideWall_Left;
                    else if (sideWallType == SideWallType.Right) ModelVisual3D_List = SideWall_Right;

                    if (MDLNum == 0)
                    {
                        HTK_3DES.OBJData.GetMeshGeometry3D(ModelVisual3D_List[MDLNum].Content).Positions[0] = new Point3D(((Point3D)Pos).X, 0, ((Point3D)Pos).Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(ModelVisual3D_List[MDLNum].Content).Positions[1] = (Point3D)Pos;
                    }
                    if (MDLNum > 0 && MDLNum < ModelVisual3D_List.Count)
                    {
                        HTK_3DES.OBJData.GetMeshGeometry3D(ModelVisual3D_List[MDLNum - 1].Content).Positions[2] = new Point3D(((Point3D)Pos).X, 0, ((Point3D)Pos).Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(ModelVisual3D_List[MDLNum - 1].Content).Positions[3] = (Point3D)Pos;

                        HTK_3DES.OBJData.GetMeshGeometry3D(ModelVisual3D_List[MDLNum].Content).Positions[0] = new Point3D(((Point3D)Pos).X, 0, ((Point3D)Pos).Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(ModelVisual3D_List[MDLNum].Content).Positions[1] = (Point3D)Pos;
                    }
                    if (MDLNum == ModelVisual3D_List.Count)
                    {
                        HTK_3DES.OBJData.GetMeshGeometry3D(ModelVisual3D_List[MDLNum - 1].Content).Positions[2] = new Point3D(((Point3D)Pos).X, 0, ((Point3D)Pos).Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(ModelVisual3D_List[MDLNum - 1].Content).Positions[3] = (Point3D)Pos;
                    }
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="UserCtrl"></param>
                /// <param name="rail"></param>
                /// <param name="railType"></param>
                public void ResetSideWall(UserControl1 UserCtrl)
                {
                    for (int i = 0; i < SideWall_Left.Count; i++) UserCtrl.MainViewPort.Children.Remove(SideWall_Left[i]);
                    SideWall_Left.Clear();

                    for (int i = 0; i < SideWall_Right.Count; i++) UserCtrl.MainViewPort.Children.Remove(SideWall_Right[i]);
                    SideWall_Right.Clear();
                }

                public void DeleteRailChk(UserControl1 UserCtrl)
                {
                    if (Checkpoint_Left.BasePointModelList != null)
                    {
                        for (int ChkLeftCount = 0; ChkLeftCount < Checkpoint_Left.BasePointModelList.Count; ChkLeftCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(Checkpoint_Left.BasePointModelList[ChkLeftCount]);
                            UserCtrl.UpdateLayout();
                        }

                        Checkpoint_Left.BasePointModelList.Clear();
                    }
                    if (Checkpoint_Right.BasePointModelList != null)
                    {
                        for (int ChkRightCount = 0; ChkRightCount < Checkpoint_Right.BasePointModelList.Count; ChkRightCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(Checkpoint_Right.BasePointModelList[ChkRightCount]);
                            UserCtrl.UpdateLayout();
                        }

                        Checkpoint_Right.BasePointModelList.Clear();
                    }
                    if (Checkpoint_Line != null)
                    {
                        for (int ChkLineCount = 0; ChkLineCount < Checkpoint_Line.Count; ChkLineCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(Checkpoint_Line[ChkLineCount]);
                            UserCtrl.UpdateLayout();
                        }

                        Checkpoint_Line.Clear();
                    }
                    if (Checkpoint_Tube != null)
                    {
                        for (int ChkTubeCount = 0; ChkTubeCount < Checkpoint_Tube.Count; ChkTubeCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(Checkpoint_Tube[ChkTubeCount]);
                            UserCtrl.UpdateLayout();
                        }

                        Checkpoint_Tube.Clear();
                    }
                    if (Checkpoint_Left.LV3D_List != null)
                    {
                        for (int ChkRLineLeftCount = 0; ChkRLineLeftCount < Checkpoint_Left.LV3D_List.Count; ChkRLineLeftCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(Checkpoint_Left.LV3D_List[ChkRLineLeftCount]);
                            UserCtrl.UpdateLayout();
                        }

                        Checkpoint_Left.LV3D_List.Clear();
                    }
                    if (Checkpoint_Right.LV3D_List != null)
                    {
                        for (int ChkRLineRightCount = 0; ChkRLineRightCount < Checkpoint_Right.LV3D_List.Count; ChkRLineRightCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(Checkpoint_Right.LV3D_List[ChkRLineRightCount]);
                            UserCtrl.UpdateLayout();
                        }

                        Checkpoint_Right.LV3D_List.Clear();
                    }
                    if (Checkpoint_SplitWallMDL != null)
                    {
                        for (int ChkSplitWallCount = 0; ChkSplitWallCount < Checkpoint_SplitWallMDL.Count; ChkSplitWallCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(Checkpoint_SplitWallMDL[ChkSplitWallCount]);
                            UserCtrl.UpdateLayout();
                        }

                        Checkpoint_SplitWallMDL.Clear();
                    }
                    if (SideWall_Left != null)
                    {
                        for (int ChkSideWallLeftCount = 0; ChkSideWallLeftCount < SideWall_Left.Count; ChkSideWallLeftCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(SideWall_Left[ChkSideWallLeftCount]);
                            UserCtrl.UpdateLayout();
                        }

                        SideWall_Left.Clear();
                    }
                    if (SideWall_Right != null)
                    {
                        for (int ChkSideWallRightCount = 0; ChkSideWallRightCount < SideWall_Right.Count; ChkSideWallRightCount++)
                        {
                            UserCtrl.MainViewPort.Children.Remove(SideWall_Right[ChkSideWallRightCount]);
                            UserCtrl.UpdateLayout();
                        }

                        SideWall_Right.Clear();
                    }
                }
            }
        }

        public class OBJData
        {
            public static Matrix3D ReScale(Matrix3D Matrix_3D, double ScaleFactor)
            {
                Matrix3D M = Matrix_3D;
                M.M11 = M.M11 / ScaleFactor;
                M.M22 = M.M22 / ScaleFactor;
                M.M33 = M.M33 / ScaleFactor;
                return M;
            }

            /// <summary>
            /// ModelVisual3D or ArrayList
            /// </summary>
            /// <param name="Path"></param>
            /// <returns></returns>
            public static Dictionary<string, ModelVisual3D> OBJReader_Dictionary(string Path)
            {
                Dictionary<string, ModelVisual3D> MV3D_Dictionary = new Dictionary<string, ModelVisual3D>();

                Model3DGroup M3D_Group = null;
                ObjReader OBJ_Reader = new ObjReader();
                M3D_Group = OBJ_Reader.Read(Path);

                for (int MDLChildCount = 0; MDLChildCount < M3D_Group.Children.Count; MDLChildCount++)
                {
                    Model3D NewM3D = M3D_Group.Children[MDLChildCount];
                    ModelVisual3D MV3D = new ModelVisual3D { Content = NewM3D };

                    MV3D.Transform = new MatrixTransform3D(ReScale(MV3D.Content.Transform.Value, 100));

                    GeometryModel3D GM3D = (GeometryModel3D)M3D_Group.Children[MDLChildCount];
                    string MatName = GM3D.Material.GetName();

                    //Give a name to ModelVisual3D
                    MV3D.SetName(MatName);

                    if (MV3D_Dictionary.Keys.Contains(MatName) && MV3D_Dictionary.Values.Contains(MV3D))
                    {
                        //マテリアルの名前が同じだった場合
                        MV3D_Dictionary.Add(MatName + MDLChildCount, MV3D);
                    }
                    else
                    {
                        MV3D_Dictionary.Add(MatName, MV3D);
                    }
                }

                return MV3D_Dictionary;
            }

            public static Dictionary<string, ArrayList> OBJReader_AryListDictionary(string Path)
            {
                Dictionary<string, ArrayList> MV3D_Dictionary = new Dictionary<string, ArrayList>();

                Model3DGroup M3D_Group = null;
                ObjReader OBJ_Reader = new ObjReader();
                M3D_Group = OBJ_Reader.Read(Path);

                for (int MDLChildCount = 0; MDLChildCount < M3D_Group.Children.Count; MDLChildCount++)
                {
                    Model3D NewM3D = M3D_Group.Children[MDLChildCount];
                    ModelVisual3D MV3D = new ModelVisual3D { Content = NewM3D };

                    //MV3D.Transform = new MatrixTransform3D(ReScale(MV3D.Content.Transform.Value, 100));

                    GeometryModel3D GM3D = (GeometryModel3D)M3D_Group.Children[MDLChildCount];
                    string MatName = GM3D.Material.GetName();

                    //ModelVisual3Dに名前をつける
                    MV3D.SetName(MatName + " -1 -1");

                    ArrayList arrayList = new ArrayList();
                    arrayList.Add(true);
                    arrayList.Add(MV3D);


                    if (MV3D_Dictionary.Keys.Contains(MatName) && MV3D_Dictionary.Values.Contains(arrayList))
                    {
                        //マテリアルの名前が同じだった場合
                        MV3D_Dictionary.Add(MatName + MDLChildCount, arrayList);
                    }
                    else
                    {
                        MV3D_Dictionary.Add(MatName, arrayList);
                    }
                }

                return MV3D_Dictionary;
            }

            public static GeometryModel3D GetGeometryModel3D(Model3D MV3D)
            {
                return (GeometryModel3D)MV3D;
            }

            public static Geometry3D GetGeometry3D(Model3D MV3D)
            {
                return ((GeometryModel3D)MV3D).Geometry;
            }

            public static MeshGeometry3D GetMeshGeometry3D(Model3D MV3D)
            {
                return (MeshGeometry3D)((GeometryModel3D)MV3D).Geometry;
            }
        }

        public class CustomModelCreateHelper
        {
            public static List<Point3D> DefaultBoxData()
            {
                List<Point3D> point3Ds = new List<Point3D>();

                #region d1
                point3Ds.Add(new Point3D(-0.5, -0.5, -0.5));
                point3Ds.Add(new Point3D(-0.5, 0.5, -0.5));

                point3Ds.Add(new Point3D(-0.5, 0.5, 0.5));
                point3Ds.Add(new Point3D(-0.5, -0.5, 0.5));

                point3Ds.Add(new Point3D(0.5, -0.5, 0.5));
                point3Ds.Add(new Point3D(0.5, 0.5, 0.5));

                point3Ds.Add(new Point3D(0.5, 0.5, -0.5));
                point3Ds.Add(new Point3D(0.5, -0.5, -0.5));
                #endregion

                #region d2
                point3Ds.Add(new Point3D(0.5, -0.5, -0.5));
                point3Ds.Add(new Point3D(-0.5, -0.5, -0.5));

                point3Ds.Add(new Point3D(-0.5, -0.5, 0.5));
                point3Ds.Add(new Point3D(0.5, -0.5, 0.5));

                point3Ds.Add(new Point3D(0.5, 0.5, 0.5));
                point3Ds.Add(new Point3D(-0.5, 0.5, 0.5));

                point3Ds.Add(new Point3D(-0.5, 0.5, -0.5));
                point3Ds.Add(new Point3D(0.5, 0.5, -0.5));
                #endregion

                #region d3
                point3Ds.Add(new Point3D(-0.5, -0.5, -0.5));
                point3Ds.Add(new Point3D(-0.5, -0.5, 0.5));

                point3Ds.Add(new Point3D(-0.5, 0.5, 0.5));
                point3Ds.Add(new Point3D(-0.5, 0.5, -0.5));

                point3Ds.Add(new Point3D(0.5, 0.5, 0.5));
                point3Ds.Add(new Point3D(0.5, 0.5, -0.5));

                point3Ds.Add(new Point3D(0.5, -0.5, 0.5));
                point3Ds.Add(new Point3D(0.5, -0.5, -0.5));
                #endregion

                return point3Ds;
            }

            public static Point3DCollection ToPoint3DCollection(List<Point3D> point3DList)
            {
                Point3DCollection P3DCollection = new Point3DCollection();
                for (int i = 0; i < point3DList.Count; i++) P3DCollection.Add(point3DList[i]);
                return P3DCollection;
            }

            public static List<Point3D> ToPoint3DList(Point3DCollection P3DCollection)
            {
                List<Point3D> point3DList = new List<Point3D>();
                for (int i = 0; i < P3DCollection.Count; i++) point3DList.Add(P3DCollection[i]);
                return point3DList;
            }

            public static MeshBuilder AddPoint3DList(List<Point3D> point3Ds)
            {
                var msb = new MeshBuilder();
                for (int i = 0; i < point3Ds.Count; i++) msb.Positions.Add(point3Ds[i]);
                return msb;
            }

            public static MeshBuilder AddPoint3DCollection(Point3DCollection P3DCollection)
            {
                var msb = new MeshBuilder();
                for (int i = 0; i < P3DCollection.Count; i++) msb.Positions.Add(P3DCollection[i]);
                return msb;
            }

            public static ModelVisual3D MeshGeometryToModelVisual3D(Point3DCollection point3Ds, Color color, Color Backcolor, string MDLName = null)
            {
                GeometryModel3D mesh = new GeometryModel3D
                {
                    Geometry = new MeshGeometry3D
                    {
                        Positions = point3Ds
                    },
                    Material = MaterialHelper.CreateMaterial(color),
                    BackMaterial = MaterialHelper.CreateMaterial(Backcolor)
                };

                ModelVisual3D modelVisual3D = new ModelVisual3D { Content = mesh };
                modelVisual3D.SetName(MDLName);

                return modelVisual3D;
            }

            public static ModelVisual3D MeshGeometryToModelVisual3D(Point3DCollection point3Ds, Int32Collection TriangleIndicesSetting, Color color, Color Backcolor, string MDLName = null)
            {
                GeometryModel3D mesh = new GeometryModel3D
                {
                    Geometry = new MeshGeometry3D
                    {
                        Positions = point3Ds,
                        TriangleIndices = TriangleIndicesSetting
                    },
                    Material = MaterialHelper.CreateMaterial(color),
                    BackMaterial = MaterialHelper.CreateMaterial(Backcolor)
                };

                ModelVisual3D modelVisual3D = new ModelVisual3D { Content = mesh };
                modelVisual3D.SetName(MDLName);

                return modelVisual3D;
            }

            public static ModelVisual3D CreateWireBoxLine()
            {
                LinesVisual3D linesVisual3D = new LinesVisual3D();
                linesVisual3D.Thickness = 5;
                linesVisual3D.Points = ToPoint3DCollection(DefaultBoxData());
                ModelVisual3D modelVisual3D = linesVisual3D;
                return modelVisual3D;
            }

            public static ModelVisual3D CreateWireBoxTube()
            {
                TubeVisual3D tubeVisual3D = new TubeVisual3D();
                tubeVisual3D.Diameter = 0.04;
                tubeVisual3D.Path = ToPoint3DCollection(DefaultBoxData());
                tubeVisual3D.IsPathClosed = false;
                ModelVisual3D modelVisual3D = tubeVisual3D;
                return modelVisual3D;
            }

            public static List<Point3D> Vector3DToPoint3DList(Vector3D vector3D)
            {
                double v1 = vector3D.X / 2;
                double v2 = -(vector3D.X / 2);

                double v3 = vector3D.Y / 2;
                double v4 = -(vector3D.Y / 2);

                double v5 = vector3D.Z / 2;
                double v6 = -(vector3D.Z / 2);

                List<Point3D> point3Ds = new List<Point3D>();

                #region d1
                point3Ds.Add(new Point3D(v2, v3, v6));
                point3Ds.Add(new Point3D(v2, v4, v6));

                point3Ds.Add(new Point3D(v2, v4, v6));
                point3Ds.Add(new Point3D(v1, v4, v6));

                point3Ds.Add(new Point3D(v1, v4, v6));
                point3Ds.Add(new Point3D(v1, v3, v6));

                point3Ds.Add(new Point3D(v1, v3, v6));
                point3Ds.Add(new Point3D(v2, v3, v6));
                #endregion

                #region d2
                point3Ds.Add(new Point3D(v2, v3, v5));
                point3Ds.Add(new Point3D(v2, v4, v5));

                point3Ds.Add(new Point3D(v2, v4, v5));
                point3Ds.Add(new Point3D(v1, v4, v5));

                point3Ds.Add(new Point3D(v1, v4, v5));
                point3Ds.Add(new Point3D(v1, v3, v5));

                point3Ds.Add(new Point3D(v1, v3, v5));
                point3Ds.Add(new Point3D(v2, v3, v5));
                #endregion

                #region d3
                point3Ds.Add(new Point3D(v2, v4, v6));
                point3Ds.Add(new Point3D(v2, v4, v5));

                point3Ds.Add(new Point3D(v1, v4, v5));
                point3Ds.Add(new Point3D(v1, v4, v6));

                point3Ds.Add(new Point3D(v1, v3, v6));
                point3Ds.Add(new Point3D(v1, v3, v5));

                point3Ds.Add(new Point3D(v2, v3, v6));
                point3Ds.Add(new Point3D(v2, v3, v5));
                #endregion

                return point3Ds;
            }

            public static ModelVisual3D CreateWireFrameMDLLine(List<Point3D> point3Ds)
            {
                LinesVisual3D linesVisual3D = new LinesVisual3D();
                linesVisual3D.Thickness = 4;
                linesVisual3D.Points = ToPoint3DCollection(point3Ds);
                ModelVisual3D modelVisual3D = linesVisual3D;
                return modelVisual3D;
            }

            public static ModelVisual3D CreateWireFrameMDLTube(List<Point3D> point3Ds)
            {
                TubeVisual3D tubeVisual3D = new TubeVisual3D();
                tubeVisual3D.Diameter = 0.05;
                tubeVisual3D.Path = ToPoint3DCollection(point3Ds);
                ModelVisual3D modelVisual3D = tubeVisual3D;
                return modelVisual3D;
            }

            public static ModelVisual3D CustomCylinderVisual3D(Color color, Color BackColor)
            {
                Point3DCollection point3Ds = new Point3DCollection();
                point3Ds.Add(new Point3D(0, -0.5, 0));
                point3Ds.Add(new Point3D(0, 0.5, 0));

                TubeVisual3D tubeVisual3D = new TubeVisual3D
                {
                    Diameter = 1,
                    Path = point3Ds,
                    AddCaps = true,
                    Material = MaterialHelper.CreateMaterial(color),
                    BackMaterial = MaterialHelper.CreateMaterial(BackColor)
                };

                Model3DGroup model3DGroup = new Model3DGroup();
                model3DGroup.Children.Add(tubeVisual3D.Content);

                ModelVisual3D modelVisual3D = new ModelVisual3D { Content = model3DGroup };

                return modelVisual3D;
            }

            public static ModelVisual3D CustomBoxVisual3D(Vector3D vector3D, Point3D center, Color Color, Color BackColor)
            {
                BoxVisual3D boxVisual3D = new BoxVisual3D
                {
                    Length = vector3D.X,
                    Width = vector3D.Y,
                    Height = vector3D.Z,
                    TopFace = true,
                    BottomFace = true,
                    Visible = true,
                    Center = center,
                    Material = MaterialHelper.CreateMaterial(Color),
                    BackMaterial = MaterialHelper.CreateMaterial(BackColor),
                };

                Model3DGroup model3DGroup = new Model3DGroup();
                model3DGroup.Children.Add(boxVisual3D.Content);

                ModelVisual3D modelVisual3D = new ModelVisual3D { Content = model3DGroup };

                //ModelVisual3D modelVisual3D = boxVisual3D;

                return modelVisual3D;
            }

            public static ModelVisual3D CustomSphereVisual3D(int ThetaDivValue, int PhiDivValue, double RadiusValue, Color Color, Color BackColor)
            {
                //int ThetaDivValue = 30, int PhiDivValue = 10, double RadiusValue = 0.5
                SphereVisual3D sphereVisual3D = new SphereVisual3D
                {
                    ThetaDiv = ThetaDivValue,
                    PhiDiv = PhiDivValue,
                    Radius = RadiusValue,
                    Material = MaterialHelper.CreateMaterial(Color),
                    BackMaterial = MaterialHelper.CreateMaterial(BackColor)
                };

                Model3DGroup model3DGroup = new Model3DGroup();
                model3DGroup.Children.Add(sphereVisual3D.Content);

                ModelVisual3D modelVisual3D = new ModelVisual3D { Content = model3DGroup };

                //ModelVisual3D modelVisual3D = sphereVisual3D;
                return modelVisual3D;
            }

            public static ModelVisual3D CustomRectanglePlane3D(Point3DCollection P3DCollection, Color color, Color Backcolor, string MDLName = "")
            {
                RectangleVisual3D rectangleVisual3D = new RectangleVisual3D
                {
                    Length = 1,
                    Width = 1,
                    DivLength = 1,
                    DivWidth = 1,
                    Origin = new Point3D(0, 0, 0)
                };

                ModelVisual3D modelVisual3D = rectangleVisual3D;
                HTK_3DES.OBJData.GetMeshGeometry3D(modelVisual3D.Content).Positions = P3DCollection;
                ((GeometryModel3D)modelVisual3D.Content).Material = MaterialHelper.CreateMaterial(color);
                ((GeometryModel3D)modelVisual3D.Content).BackMaterial = MaterialHelper.CreateMaterial(Backcolor);
                modelVisual3D.SetName(MDLName);

                return modelVisual3D;
            }

            public static ModelVisual3D CustomArrowVisual3D(double Diameter, int ThetaDiv, double HeadLength, Vector3D Direction, Point3D Origin, Color Color, Color BackColor)
            {
                ArrowVisual3D arrowVisual3D = new ArrowVisual3D
                {
                    Diameter = Diameter,
                    ThetaDiv = ThetaDiv,
                    HeadLength = HeadLength,
                    Direction = Direction,
                    Origin = Origin,
                    Material = MaterialHelper.CreateMaterial(Color),
                    BackMaterial = MaterialHelper.CreateMaterial(BackColor)
                };

                ModelVisual3D modelVisual3D = arrowVisual3D;

                return modelVisual3D;
            }

            public static ModelVisual3D CustomPointVector3D(Color BoxColor, Color BoxBackColor, Color ArrowColor, Color ArrowBackColor, Color SphereColor, Color SphereBackColor)
            {
                //BoxVisual3D boxVisual3D = (BoxVisual3D)CustomBoxVisual3D(new Vector3D(0.3, 0.3, 2.5), new Point3D(0, 0, 1.65), BoxColor, BoxBackColor);
                ArrowVisual3D arrowVisual3D = (ArrowVisual3D)CustomArrowVisual3D(0.3, 5, 1, new Vector3D(0, 1, 0), new Point3D(0, -0.5, 0), ArrowColor, ArrowBackColor);

                Transform transform = new Transform
                {
                    Rotate3D = new Vector3D(0, 0, 0),
                    Scale3D = new Vector3D(1, 1, 1),
                    Translate3D = new Vector3D(0, -0.1, 0)
                };

                TSRSystem3D tSRSystem3D = new TSRSystem3D(arrowVisual3D, transform);
                tSRSystem3D.Transform3D(TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), TSRSystem3D.RotationType.Radian);

                //HTK_3DES.TransformSetting transformSetting = new TSRSystem.TransformSetting { InputMV3D = arrowVisual3D };

                //HTK_3DES.New_TransformSystem3D(transform, transformSetting);

                //HTK_3DES.TransformMV3D.Transform_MV3D(transform, arrowVisual3D, HTK_3DES.RotationSetting.Angle);

                Model3DGroup model3DGroup = new Model3DGroup();
                model3DGroup.Children.Add(CustomBoxVisual3D(new Vector3D(0.3, 0.3, 5), new Point3D(0, 0, 2.65), BoxColor, BoxBackColor).Content);
                model3DGroup.Children.Add(arrowVisual3D.Content);
                model3DGroup.Children.Add(CustomSphereVisual3D(30, 10, 1, SphereColor, SphereBackColor).Content);

                ModelVisual3D modelVisual3D = new ModelVisual3D { Content = model3DGroup };

                return modelVisual3D;
            }

            public static CuttingPlaneGroup CreateCuttingPlaneGroup(List<Visual3D> visual3Ds, List<Plane3D> plane3Ds, CuttingOperation cuttingOperation, bool IsEnabled)
            {
                CuttingPlaneGroup cuttingPlaneGroup = new CuttingPlaneGroup
                {
                    CuttingPlanes = plane3Ds,
                    Operation = cuttingOperation,
                    IsEnabled = IsEnabled
                };

                for (int f = 0; f < visual3Ds.Count; f++) cuttingPlaneGroup.Children.Add(visual3Ds[f]);

                return cuttingPlaneGroup;
            }

            public static CuttingPlaneGroup CreateCuttingPlaneGroup(Visual3D visual3D, List<Plane3D> plane3Ds, CuttingOperation cuttingOperation, bool IsEnabled)
            {
                CuttingPlaneGroup cuttingPlaneGroup = new CuttingPlaneGroup
                {
                    CuttingPlanes = plane3Ds,
                    Operation = cuttingOperation,
                    IsEnabled = IsEnabled
                };

                cuttingPlaneGroup.Children.Add(visual3D);

                return cuttingPlaneGroup;
            }

            public enum Option
            {
                Setting1,
                Setting2,
                Setting3
            }

            public static ModelVisual3D CustomSphereHurf3D(int ThetaDivValue, int PhiDivValue, double RadiusValue, Color Color, Color BackColor, Option option)
            {
                //int ThetaDivValue = 30, int PhiDivValue = 10, double RadiusValue = 0.5
                SphereVisual3D sphereVisual3D = new SphereVisual3D
                {
                    ThetaDiv = ThetaDivValue,
                    PhiDiv = PhiDivValue,
                    Radius = RadiusValue,
                    Material = MaterialHelper.CreateMaterial(Color),
                    BackMaterial = MaterialHelper.CreateMaterial(BackColor)
                };

                List<Plane3D> plane3Ds = new List<Plane3D>();

                if (option == Option.Setting1) plane3Ds.Add(new Plane3D { Normal = new Vector3D(0, 1, 0), Position = new Point3D(0, 0, 0) });
                if (option == Option.Setting2) plane3Ds.Add(new Plane3D { Normal = new Vector3D(1, 0, 0), Position = new Point3D(0, 0, 0) });
                if (option == Option.Setting3) plane3Ds.Add(new Plane3D { Normal = new Vector3D(0, 0, 1), Position = new Point3D(0, 0, 0) });

                ModelVisual3D modelVisual3D = CreateCuttingPlaneGroup(sphereVisual3D, plane3Ds, CuttingOperation.Intersect, true);
                return modelVisual3D;
            }

            public static ModelVisual3D CustomPointModel3D()
            {
                List<Plane3D> plane3Ds = new List<Plane3D>();
                plane3Ds.Add(new Plane3D { Normal = new Vector3D(0, 1, 0), Position = new Point3D(0, 0, 0) });

                List<Plane3D> plane3Ds2 = new List<Plane3D>();
                plane3Ds2.Add(new Plane3D { Normal = new Vector3D(0, -1, 0), Position = new Point3D(0, 0, 0) });

                ModelVisual3D sp1 = CustomSphereVisual3D(30, 10, 0.5, Color.FromArgb(0x80, 0x00, 0xF0, 0x00), Color.FromArgb(0x80, 0x00, 0xF0, 0x00));
                ModelVisual3D sp2 = CustomSphereVisual3D(30, 10, 0.5, Color.FromArgb(0x80, 0xF0, 0x00, 0x00), Color.FromArgb(0x80, 0xF0, 0x00, 0x00));
                ModelVisual3D Box1 = CustomBoxVisual3D(new Vector3D(0.3, 0.3, 2.5), new Point3D(0, 0, 1), Color.FromArgb(0x80, 0xF0, 0x00, 0xF0), Color.FromArgb(0x80, 0xF0, 0x00, 0xF0));
                ModelVisual3D Box2 = CustomBoxVisual3D(new Vector3D(0.3, 0.3, 2.5), new Point3D(0, 0, 1), Color.FromArgb(0x80, 0x00, 0xF0, 0xF0), Color.FromArgb(0x80, 0x00, 0xF0, 0xF0));

                ModelVisual3D f1 = CreateCuttingPlaneGroup(sp1, plane3Ds, CuttingOperation.Intersect, true);
                ModelVisual3D f2 = CreateCuttingPlaneGroup(sp2, plane3Ds2, CuttingOperation.Intersect, true);
                ModelVisual3D f3 = CreateCuttingPlaneGroup(Box1, plane3Ds, CuttingOperation.Intersect, true);
                ModelVisual3D f4 = CreateCuttingPlaneGroup(Box2, plane3Ds2, CuttingOperation.Intersect, true);

                ModelVisual3D MV3D = new ModelVisual3D();
                MV3D.Children.Add(f1);
                MV3D.Children.Add(f2);
                MV3D.Children.Add(f3);
                MV3D.Children.Add(f4);

                return MV3D;
            }
        }
    }
}
