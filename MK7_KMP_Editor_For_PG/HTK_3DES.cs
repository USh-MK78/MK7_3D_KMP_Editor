﻿using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MK7_KMP_Editor_For_PG_
{
    public class HTK_3DES
    {
        public class TSRSystem
        {
            public Transform_Value GetTransform_Value { get; set; }

            /// <summary>
            /// Translate,Scale,Rotateの値を格納するクラス
            /// </summary>
            public class Transform_Value
            {
                public Translate Translate_Value { get; set; }
                public class Translate
                {
                    public double X { get; set; }
                    public double Y { get; set; }
                    public double Z { get; set; }
                }

                public Scale Scale_Value { get; set; }
                public class Scale
                {
                    public double X { get; set; }
                    public double Y { get; set; }
                    public double Z { get; set; }
                }

                public Rotate Rotate_Value { get; set; }
                public class Rotate
                {
                    public double X { get; set; }
                    public double Y { get; set; }
                    public double Z { get; set; }
                }
            }

            /// <summary>
            /// objファイルを読み込み、ModelVisual3Dを返すメソッド
            /// </summary>
            /// <param name="Path">Model Path</param>
            /// <returns>ModelVisual3D</returns>
            public static ModelVisual3D OBJReader(string Path)
            {
                ModelVisual3D dv3D = new ModelVisual3D();
                ObjReader objRead = new ObjReader();
                dv3D.Content = objRead.Read(Path);

                #region delcode(?)
                //ObjReader objRead = new ObjReader();

                //SortingVisual3D sortingVisual3D = new SortingVisual3D
                //{
                //    Method = SortingMethod.BoundingSphereSurface,
                //    SortingFrequency = 2,
                //    Content = objRead.Read(Path)
                //};

                //ModelVisual3D dv3D = sortingVisual3D;
                #endregion

                return dv3D;
            }

            /// <summary>
            /// ガベージコレクション
            /// </summary>
            public static void GC_Dispose(object f)
            {
                int GCNum = GC.GetGeneration(f);

                GC.Collect(GCNum);
                GC.WaitForPendingFinalizers();
                //GC.Collect();
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

            public class Transform
            {
                public Vector3D Rotate3D { get; set; }
                public Vector3D Scale3D { get; set; }
                public Vector3D Translate3D { get; set; }
            }

            #region delcode
            ///// <summary>
            ///// Radianを角度に変換
            ///// </summary>
            ///// <param name="Radian"></param>
            ///// <returns></returns>
            //public static double RadianToAngle(double Radian)
            //{
            //    return Radian * (180 / Math.PI);
            //}

            ///// <summary>
            ///// 角度をRadianに変換
            ///// </summary>
            ///// <param name="Angle"></param>
            ///// <returns></returns>
            //public static double AngleToRadian(double Angle)
            //{
            //    return Angle * (Math.PI / 180);
            //}
            #endregion

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

            public enum RotationSetting
            {
                Angle,
                Radian
            }

            public static void NewTransformSystem3D(Transform transform, ModelVisual3D MV3D, RotationSetting rotationSetting)
            {
                double RotateX = new double();
                double RotateY = new double();
                double RotateZ = new double();

                if (rotationSetting == RotationSetting.Angle)
                {
                    RotateX = transform.Rotate3D.X;
                    RotateY = transform.Rotate3D.Y;
                    RotateZ = transform.Rotate3D.Z;
                }
                if (rotationSetting == RotationSetting.Radian)
                {
                    RotateX = RadianToAngle(transform.Rotate3D.X);
                    RotateY = RadianToAngle(transform.Rotate3D.Y);
                    RotateZ = RadianToAngle(transform.Rotate3D.Z);
                }

                Model3D Model = MV3D.Content;
                var Rotate3D_X = new RotateTransform3D();
                Rotate3D_X.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(1, 0, 0), RotateX));

                var Rotate3D_Y = new RotateTransform3D();
                Rotate3D_Y.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 1, 0), RotateY));

                var Rotate3D_Z = new RotateTransform3D();
                Rotate3D_Z.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 0, 1), RotateZ));

                var Scale3D = new ScaleTransform3D(transform.Scale3D, CalculateModelCenterPoint(Model));
                var Translate3D = new TranslateTransform3D(transform.Translate3D);

                Transform3DCollection T3D_Collection = new Transform3DCollection();
                T3D_Collection.Add(Scale3D);
                T3D_Collection.Add(Rotate3D_X);
                T3D_Collection.Add(Rotate3D_Y);
                T3D_Collection.Add(Rotate3D_Z);
                T3D_Collection.Add(Translate3D);

                Transform3DGroup T3DGroup = new Transform3DGroup { Children = T3D_Collection };
                Model.Transform = T3DGroup;
            }

            public static void NewTransformSystem3D(Transform transform, Model3D MV3D, RotationSetting rotationSetting)
            {
                double RotateX = new double();
                double RotateY = new double();
                double RotateZ = new double();

                if (rotationSetting == RotationSetting.Angle)
                {
                    RotateX = transform.Rotate3D.X;
                    RotateY = transform.Rotate3D.Y;
                    RotateZ = transform.Rotate3D.Z;
                }
                if (rotationSetting == RotationSetting.Radian)
                {
                    RotateX = RadianToAngle(transform.Rotate3D.X);
                    RotateY = RadianToAngle(transform.Rotate3D.Y);
                    RotateZ = RadianToAngle(transform.Rotate3D.Z);
                }

                var Rotate3D_X = new RotateTransform3D();
                Rotate3D_X.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(1, 0, 0), RotateX));

                var Rotate3D_Y = new RotateTransform3D();
                Rotate3D_Y.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 1, 0), RotateY));

                var Rotate3D_Z = new RotateTransform3D();
                Rotate3D_Z.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 0, 1), RotateZ));

                var Scale3D = new ScaleTransform3D(transform.Scale3D);
                var Translate3D = new TranslateTransform3D(transform.Translate3D);

                Transform3DCollection T3D_Collection = new Transform3DCollection();
                T3D_Collection.Add(Scale3D);
                T3D_Collection.Add(Rotate3D_X);
                T3D_Collection.Add(Rotate3D_Y);
                T3D_Collection.Add(Rotate3D_Z);
                T3D_Collection.Add(Translate3D);

                Transform3DGroup T3DGroup = new Transform3DGroup { Children = T3D_Collection };
                MV3D.Transform = T3DGroup;
            }

            public static void NewTransformSystem3D(Transform_Value transform, ModelVisual3D MV3D, RotationSetting rotationSetting)
            {
                double RotateX = new double();
                double RotateY = new double();
                double RotateZ = new double();

                if (rotationSetting == RotationSetting.Angle)
                {
                    RotateX = transform.Rotate_Value.X;
                    RotateY = transform.Rotate_Value.Y;
                    RotateZ = transform.Rotate_Value.Z;
                }
                if (rotationSetting == RotationSetting.Radian)
                {
                    RotateX = RadianToAngle(transform.Rotate_Value.X);
                    RotateY = RadianToAngle(transform.Rotate_Value.Y);
                    RotateZ = RadianToAngle(transform.Rotate_Value.Z);
                }

                Model3D Model = MV3D.Content;
                var Rotate3D_X = new RotateTransform3D();
                Rotate3D_X.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(1, 0, 0), RotateX));

                var Rotate3D_Y = new RotateTransform3D();
                Rotate3D_Y.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 1, 0), RotateY));

                var Rotate3D_Z = new RotateTransform3D();
                Rotate3D_Z.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 0, 1), RotateZ));

                var Scale3D = new ScaleTransform3D(new Vector3D(transform.Scale_Value.X, transform.Scale_Value.Y, transform.Scale_Value.Z), CalculateModelCenterPoint(Model));
                var Translate3D = new TranslateTransform3D(transform.Translate_Value.X, transform.Translate_Value.Y, transform.Translate_Value.Z);

                Transform3DCollection T3D_Collection = new Transform3DCollection();
                T3D_Collection.Add(Scale3D);
                T3D_Collection.Add(Rotate3D_X);
                T3D_Collection.Add(Rotate3D_Y);
                T3D_Collection.Add(Rotate3D_Z);
                T3D_Collection.Add(Translate3D);

                Transform3DGroup T3DGroup = new Transform3DGroup { Children = T3D_Collection };
                Model.Transform = T3DGroup;
            }

            public static void NewTransformSystem3D(Transform_Value transform, Model3D MV3D, RotationSetting rotationSetting)
            {
                double RotateX = new double();
                double RotateY = new double();
                double RotateZ = new double();

                if (rotationSetting == RotationSetting.Angle)
                {
                    RotateX = transform.Rotate_Value.X;
                    RotateY = transform.Rotate_Value.Y;
                    RotateZ = transform.Rotate_Value.Z;
                }
                if (rotationSetting == RotationSetting.Radian)
                {
                    RotateX = RadianToAngle(transform.Rotate_Value.X);
                    RotateY = RadianToAngle(transform.Rotate_Value.Y);
                    RotateZ = RadianToAngle(transform.Rotate_Value.Z);
                }

                var Rotate3D_X = new RotateTransform3D();
                Rotate3D_X.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(1, 0, 0), RotateX));

                var Rotate3D_Y = new RotateTransform3D();
                Rotate3D_Y.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 1, 0), RotateY));

                var Rotate3D_Z = new RotateTransform3D();
                Rotate3D_Z.Rotation = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 0, 1), RotateZ));

                var Scale3D = new ScaleTransform3D(transform.Scale_Value.X, transform.Scale_Value.Y, transform.Scale_Value.Z);
                var Translate3D = new TranslateTransform3D(transform.Translate_Value.X, transform.Translate_Value.Y, transform.Translate_Value.Z);

                Transform3DCollection T3D_Collection = new Transform3DCollection();
                T3D_Collection.Add(Scale3D);
                T3D_Collection.Add(Rotate3D_X);
                T3D_Collection.Add(Rotate3D_Y);
                T3D_Collection.Add(Rotate3D_Z);
                T3D_Collection.Add(Translate3D);

                Transform3DGroup T3DGroup = new Transform3DGroup { Children = T3D_Collection };
                MV3D.Transform = T3DGroup;
            }
        }

        public class TransformMV3D : TSRSystem
        {
            public static ModelVisual3D Transform_MV3D(Transform_Value transform_Value, ModelVisual3D dv3D, RotationSetting rotationSetting = RotationSetting.Radian, bool IsContent = true, double ScaleFactor = 2)
            {
                Transform t = new Transform
                {
                    Rotate3D = new Vector3D(transform_Value.Rotate_Value.X, transform_Value.Rotate_Value.Y, transform_Value.Rotate_Value.Z),
                    Scale3D = new Vector3D(transform_Value.Scale_Value.X / ScaleFactor, transform_Value.Scale_Value.Y / ScaleFactor, transform_Value.Scale_Value.Z / ScaleFactor),
                    Translate3D = new Vector3D(transform_Value.Translate_Value.X, transform_Value.Translate_Value.Y, transform_Value.Translate_Value.Z)
                };

                if (IsContent == true) NewTransformSystem3D(t, dv3D.Content, rotationSetting);
                if (IsContent == false) NewTransformSystem3D(t, dv3D, rotationSetting);
                return dv3D;
            }

            public static ModelVisual3D Transform_MV3D(Transform transform_Value, ModelVisual3D dv3D, RotationSetting rotationSetting = RotationSetting.Radian, bool IsContent = true, double ScaleFactor = 2)
            {
                transform_Value.Scale3D = new Vector3D(transform_Value.Scale3D.X / ScaleFactor, transform_Value.Scale3D.Y / ScaleFactor, transform_Value.Scale3D.Z / ScaleFactor);
                if (IsContent == true) NewTransformSystem3D(transform_Value, dv3D.Content, rotationSetting);
                if (IsContent == false) NewTransformSystem3D(transform_Value, dv3D, rotationSetting);
                return dv3D;
            }

            #region Unused(?)
            ///// <summary>
            ///// Transform_Valueを使用してモデルの変換のみを提供するメソッド
            ///// </summary>
            ///// <param name="transform_Value"></param>
            ///// <param name="dv3D">Input ModelVisual3D</param>
            //public static Model3D Transform_MV3D(Transform_Value transform_Value, Model3D dv3D, bool IsScale = true, RotationSetting rotationSetting = RotationSetting.Radian)
            //{
            //    if(IsScale == true)
            //    {
            //        transform_Value.Scale_Value.X = transform_Value.Scale_Value.X / 10;
            //        transform_Value.Scale_Value.Y = transform_Value.Scale_Value.Y / 10;
            //        transform_Value.Scale_Value.Z = transform_Value.Scale_Value.Z / 10;
            //    }
            //    NewTransformSystem3D(transform_Value, dv3D, rotationSetting);
            //    return dv3D;
            //}

            ///// <summary>
            ///// Transform_Valueを使用してモデルの変換のみを提供するメソッド
            ///// </summary>
            ///// <param name="transform_Value"></param>
            ///// <param name="dv3D">Input ModelVisual3D</param>
            //public static Model3D Transform_MV3D(Transform transform_Value, Model3D dv3D, RotationSetting rotationSetting = RotationSetting.Radian)
            //{
            //    transform_Value.Scale3D = new Vector3D(transform_Value.Scale3D.X / 10, transform_Value.Scale3D.Y / 10, transform_Value.Scale3D.Z / 10);
            //    NewTransformSystem3D(transform_Value, dv3D, rotationSetting);
            //    return dv3D;
            //}
            #endregion
        }

        public class Line3DSystem : TSRSystem
        {
            /// <summary>
            /// Point3Dの値を格納するクラス
            /// </summary>
            public class DrawLine_Value
            {
                public Start_Point3D StartPoint3D { get; set; }
                public class Start_Point3D
                {
                    public double X { get; set; }
                    public double Y { get; set; }
                    public double Z { get; set; }
                }

                public End_Point3D EndPoint3D { get; set; }
                public class End_Point3D
                {
                    public double X { get; set; }
                    public double Y { get; set; }
                    public double Z { get; set; }
                }
            }

            /// <summary>
            /// List<DrawLine_Value>を使用してLinesVisual3Dを生成、ModelVisual3Dに変換する
            /// </summary>
            /// <param name="DrawLine_Value_List">Point3D_List</param>
            /// <param name="colors">Set Color</param>
            /// <returns>List<ModelVisual3D>List<ModelVisual3D></returns>
            public List<ModelVisual3D> DrawLinesVisual3D(List<DrawLine_Value> DrawLine_Value_List, List<LinesVisual3D> LV3D_List, System.Windows.Media.Color colors)
            {
                List<ModelVisual3D> ConvertLV3DToMV3D_List = new List<ModelVisual3D>();
                
                //List<Point3D>を使用して線を描く
                for (int i = 0; i < DrawLine_Value_List.Count; i++)
                {
                    List<Point3D> p3d = new List<Point3D>();
                    p3d.Add(new Point3D(DrawLine_Value_List[i].StartPoint3D.X, DrawLine_Value_List[i].StartPoint3D.Y, DrawLine_Value_List[i].StartPoint3D.Z));
                    p3d.Add(new Point3D(DrawLine_Value_List[i].EndPoint3D.X, DrawLine_Value_List[i].EndPoint3D.Y, DrawLine_Value_List[i].EndPoint3D.Z));

                    LV3D_List.Add(new LinesVisual3D { Points = new Point3DCollection(p3d), Color = colors });
                    ConvertLV3DToMV3D_List.Add(LV3D_List[i]);
                }

                return ConvertLV3DToMV3D_List;
            }

            /// <summary>
            /// List<DrawLine_Value>を使用してLinesVisual3Dを生成、ModelVisual3Dに変換する
            /// </summary>
            /// <param name="DrawLine_Value_List">Point3D_List</param>
            /// <param name="colors">Set Color</param>
            /// <returns>List<ModelVisual3D>List<ModelVisual3D></returns>
            public List<ModelVisual3D> DrawLinesVisual3D(List<DrawLine_Value> DrawLine_Value_List, System.Windows.Media.Color colors)
            {
                List<ModelVisual3D> ConvertLV3DToMV3D_List = new List<ModelVisual3D>();
                List<LinesVisual3D> LV3D_List = new List<LinesVisual3D>();


                //List<Point3D>を使用して線を描く
                for (int i = 0; i < DrawLine_Value_List.Count; i++)
                {
                    List<Point3D> p3d = new List<Point3D>();
                    p3d.Add(new Point3D(DrawLine_Value_List[i].StartPoint3D.X, DrawLine_Value_List[i].StartPoint3D.Y, DrawLine_Value_List[i].StartPoint3D.Z));
                    p3d.Add(new Point3D(DrawLine_Value_List[i].EndPoint3D.X, DrawLine_Value_List[i].EndPoint3D.Y, DrawLine_Value_List[i].EndPoint3D.Z));

                    LV3D_List.Add(new LinesVisual3D { Points = new Point3DCollection(p3d), Color = colors });
                    ConvertLV3DToMV3D_List.Add(LV3D_List[i]);
                }

                return ConvertLV3DToMV3D_List;
            }

            /// <summary>
            /// List<Point3D>を使用してLinesVisual3Dを生成、ModelVisual3Dに変換する
            /// </summary>
            /// <param name="P3DList">Point3D_List</param>
            /// <param name="LV3D_List">LineVisual3D_List</param>
            /// <param name="colors">Set Color</param>
            /// <returns>List<ModelVisual3D>List<ModelVisual3D></returns>
            public List<ModelVisual3D> DrawLinesVisual3D(List<Point3D> P3DList, List<LinesVisual3D> LV3D_List, System.Windows.Media.Color colors)
            {
                List<ModelVisual3D> ConvertLV3DToMV3D_List = new List<ModelVisual3D>();

                //List<Point3D>を使用して線を描く
                for (int i = 0; i < P3DList.Count; i++)
                {
                    LV3D_List.Add(new LinesVisual3D { Points = new Point3DCollection(P3DList), Color = colors });
                    ConvertLV3DToMV3D_List.Add(LV3D_List[i]);
                }

                return ConvertLV3DToMV3D_List;
            }

            /// <summary>
            /// List<Point3D>を使用してLinesVisual3Dを生成、ModelVisual3Dに変換する
            /// </summary>
            /// <param name="P3DList">Point3D_List</param>
            /// <param name="LV3D_List">LineVisual3D_List</param>
            /// <param name="colors">Set Color</param>
            /// <returns>List<ModelVisual3D>List<ModelVisual3D></returns>
            public List<ModelVisual3D> DrawLinesVisual3D(List<Point3D> P3DList, System.Windows.Media.Color colors)
            {
                List<ModelVisual3D> ConvertLV3DToMV3D_List = new List<ModelVisual3D>();
                List<LinesVisual3D> LV3D_List = new List<LinesVisual3D>();

                //List<Point3D>を使用して線を描く
                for (int i = 0; i < P3DList.Count; i++)
                {
                    LV3D_List.Add(new LinesVisual3D { Points = new Point3DCollection(P3DList), Color = colors });
                    ConvertLV3DToMV3D_List.Add(LV3D_List[i]);
                }

                return ConvertLV3DToMV3D_List;
            }
        }

        public class CustomModelMV3D : TSRSystem
        {
            /// <summary>
            /// Point3DのListからModelVisual3Dを生成
            /// </summary>
            /// <param name="P3DList">Point3D_List</param>
            /// <param name="LV3D_List">LineVisual3D_List</param>
            /// <param name="colors">Set Color</param>
            /// <returns>List<ModelVisual3D>List<ModelVisual3D></returns>
            public List<ModelVisual3D> CustomModelCreate(List<Point3D> P3DList, List<LinesVisual3D> LV3D_List, System.Windows.Media.Color colors)
            {
                //List<Point3D>を使用して線を描く
                for (int i = 0; i < P3DList.Count; i++)
                {
                    LV3D_List.Add(new LinesVisual3D { Points = new Point3DCollection(P3DList), Color = colors });
                }

                List<ModelVisual3D> ConvertLV3DToMV3D_List = new List<ModelVisual3D>();

                for (int LV3DCount = 0; LV3DCount < LV3D_List.Count; LV3DCount++)
                {
                    //LinesVisual3DをModel3Dに変換
                    Model3D LV3DToM3D = LV3D_List[LV3DCount].Content;
                    ModelVisual3D M3DToMV3D = new ModelVisual3D { Content = LV3DToM3D };

                    //Add
                    ConvertLV3DToMV3D_List.Add(M3DToMV3D);
                }

                return ConvertLV3DToMV3D_List;
            }

            /// <summary>
            /// List<ModelVisual3D>を1つのModelVisual3Dに結合する
            /// </summary>
            /// <param name="MV3D_List"></param>
            /// <returns>ModelVisual3D</returns>
            public ModelVisual3D UnionModelVisual3D(List<ModelVisual3D> MV3D_List)
            {
                Model3DGroup UnionModelVisual3DGroup = new Model3DGroup();

                for (int ModelVisual3DCount = 0; ModelVisual3DCount < MV3D_List.Count; ModelVisual3DCount++)
                {
                    UnionModelVisual3DGroup.Children.Add(MV3D_List[ModelVisual3DCount].Content);
                }

                ModelVisual3D JoinedMV3D = new ModelVisual3D { Content = UnionModelVisual3DGroup };

                return JoinedMV3D;
            }
        }

        public class PathTools : TSRSystem
        {
            public class Rail
            {
                public List<ModelVisual3D> MV3D_List { get; set; }

                public List<LinesVisual3D> LV3D_List { get; set; }

                public List<TubeVisual3D> TV3D_List { get; set; }
            }

            public static List<Point3D> MV3DListToPoint3DList(List<ModelVisual3D> MV3DList)
            {
                List<Point3D> point3Ds = new List<Point3D>();

                for (int i = 0; i < MV3DList.Count; i++)
                {
                    Model3D n = MV3DList[i].Content;

                    Point3D p3d = new Point3D(n.Transform.Value.OffsetX, n.Transform.Value.OffsetY, n.Transform.Value.OffsetZ);

                    point3Ds.Add(p3d);
                }

                return point3Ds;
            }

            public static List<LinesVisual3D> DrawPath_Line(UserControl1 UserCtrl, List<Point3D> point3Ds, double Thickness, Color color)
            {
                List<LinesVisual3D> linesVisual3DList_Out = new List<LinesVisual3D>();
                if (point3Ds.Count > 1)
                {
                    for (int i = 1; i < point3Ds.Count; i++)
                    {
                        List<Point3D> OneLine = new List<Point3D>();
                        OneLine.Add(point3Ds[i - 1]);
                        OneLine.Add(point3Ds[i]);

                        LinesVisual3D linesVisual3D = new LinesVisual3D
                        {
                            Points = new Point3DCollection(OneLine),
                            Thickness = Thickness,
                            Color = color
                        };

                        UserCtrl.MainViewPort.Children.Add(linesVisual3D);

                        linesVisual3DList_Out.Add(linesVisual3D);
                    }
                }

                return linesVisual3DList_Out;
            }

            public static List<TubeVisual3D> DrawPath_Tube(UserControl1 UserCtrl, List<Point3D> point3Ds, double TubeDiametor, Color color)
            {
                List<TubeVisual3D> tubeVisual3DList_Out = new List<TubeVisual3D>();
                if (point3Ds.Count > 1)
                {
                    for(int i = 1; i < point3Ds.Count; i++)
                    {
                        //TubeVisual3Dの直径を指定
                        double Diametor_Value = TubeDiametor;

                        TubeVisual3D tubeVisual3D = new TubeVisual3D();
                        tubeVisual3D.Fill = new SolidColorBrush(color);
                        tubeVisual3D.Path = new Point3DCollection();
                        tubeVisual3D.Path.Add(point3Ds[i - 1]);
                        tubeVisual3D.Path.Add(point3Ds[i]);
                        tubeVisual3D.Diameter = Diametor_Value;
                        tubeVisual3D.IsPathClosed = false;

                        tubeVisual3DList_Out.Add(tubeVisual3D);

                        //Add Tube
                        UserCtrl.MainViewPort.Children.Add(tubeVisual3D);
                    }
                }

                return tubeVisual3DList_Out;
            }

            public static void MoveRails(int MDLNum, Vector3D Pos, List<TubeVisual3D> TubeVisual3D_List)
            {
                if (MDLNum == 0)
                {
                    TubeVisual3D_List[MDLNum].Path[0] = (Point3D)Pos;
                }
                if (MDLNum > 0 && MDLNum < TubeVisual3D_List.Count)
                {
                    TubeVisual3D_List[MDLNum - 1].Path[1] = (Point3D)Pos;
                    TubeVisual3D_List[MDLNum].Path[0] = (Point3D)Pos;
                }
                if (MDLNum == TubeVisual3D_List.Count)
                {
                    TubeVisual3D_List[MDLNum - 1].Path[1] = (Point3D)Pos;
                }
            }

            public static void MoveRails(int MDLNum, Vector3D Pos, List<LinesVisual3D> LinesVisual3D_List)
            {
                if (MDLNum == 0)
                {
                    LinesVisual3D_List[MDLNum].Points[0] = (Point3D)Pos;
                }
                if (MDLNum > 0 && MDLNum < LinesVisual3D_List.Count)
                {
                    LinesVisual3D_List[MDLNum - 1].Points[1] = (Point3D)Pos;
                    LinesVisual3D_List[MDLNum].Points[0] = (Point3D)Pos;
                }
                if (MDLNum == LinesVisual3D_List.Count)
                {
                    LinesVisual3D_List[MDLNum - 1].Points[1] = (Point3D)Pos;
                }
            }

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

            public static void DeleteRailPoint(UserControl1 UserCtrl, Rail rail, int SelectedIdx, double TubeDiametor, Color color, RailType railType)
            {
                Point3D? SelectedIndex_Next = null;
                Point3D? SelectedIndex_Current = null;
                Point3D? SelectedIndex_Prev = null;

                List<Point3D> point3Ds = new List<Point3D>();

                #region SelectedIndex_Next
                try
                {
                    SelectedIndex_Next = new Point3D(rail.MV3D_List[SelectedIdx + 1].Content.Transform.Value.OffsetX, rail.MV3D_List[SelectedIdx + 1].Content.Transform.Value.OffsetY, rail.MV3D_List[SelectedIdx + 1].Content.Transform.Value.OffsetZ);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    SelectedIndex_Next = null;
                }
                #endregion

                #region SelectedIndex_Current
                try
                {
                    SelectedIndex_Current = new Point3D(rail.MV3D_List[SelectedIdx].Content.Transform.Value.OffsetX, rail.MV3D_List[SelectedIdx].Content.Transform.Value.OffsetY, rail.MV3D_List[SelectedIdx].Content.Transform.Value.OffsetZ);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    SelectedIndex_Current = null;
                }
                #endregion

                #region SelectedIndex_Prev
                try
                {
                    SelectedIndex_Prev = new Point3D(rail.MV3D_List[SelectedIdx - 1].Content.Transform.Value.OffsetX, rail.MV3D_List[SelectedIdx - 1].Content.Transform.Value.OffsetY, rail.MV3D_List[SelectedIdx - 1].Content.Transform.Value.OffsetZ);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    SelectedIndex_Prev = null;
                }
                #endregion

                if (railType == RailType.Tube)
                {
                    if (SelectedIndex_Current != null)
                    {
                        if ((SelectedIndex_Next == null && SelectedIndex_Prev == null) == true)
                        {
                            UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[SelectedIdx]);
                            rail.MV3D_List.Remove(rail.MV3D_List[SelectedIdx]);

                            //MessageBox.Show("Point3D Only");
                        }
                        else if ((SelectedIndex_Next != null && SelectedIndex_Prev != null) == true)
                        {
                            point3Ds.Add(SelectedIndex_Prev.Value);
                            point3Ds.Add(SelectedIndex_Next.Value);

                            //Pointを削除
                            UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[SelectedIdx]);
                            rail.MV3D_List.Remove(rail.MV3D_List[SelectedIdx]);

                            //Pointの両端に存在するTubeVisual3Dを削除
                            UserCtrl.MainViewPort.Children.Remove(rail.TV3D_List[SelectedIdx]);
                            rail.TV3D_List.Remove(rail.TV3D_List[SelectedIdx]);

                            UserCtrl.MainViewPort.Children.Remove(rail.TV3D_List[SelectedIdx - 1]);
                            rail.TV3D_List.Remove(rail.TV3D_List[SelectedIdx - 1]);

                            for (int i = 0; i < rail.MV3D_List.Count; i++)
                            {
                                string[] MDLInfo = rail.MV3D_List[i].GetName().Split(' ');
                                string New_MDLInfo = MDLInfo[0] + " " + i.ToString() + " " + MDLInfo[2];
                                rail.MV3D_List[i].SetName(New_MDLInfo);
                            }

                            //TubeVisual3Dの直径を指定
                            double Diametor_Value = TubeDiametor;

                            TubeVisual3D tubeVisual3D = new TubeVisual3D();
                            tubeVisual3D.Fill = new SolidColorBrush(color);
                            tubeVisual3D.Path = new Point3DCollection();
                            tubeVisual3D.Path.Add(point3Ds[0]);
                            tubeVisual3D.Path.Add(point3Ds[1]);
                            tubeVisual3D.Diameter = Diametor_Value;
                            tubeVisual3D.IsPathClosed = false;

                            rail.TV3D_List.Insert(SelectedIdx - 1, tubeVisual3D);

                            //Add Tube
                            UserCtrl.MainViewPort.Children.Add(tubeVisual3D);

                            //MessageBox.Show("PrevPoint and NextPoint");
                        }
                        else if ((SelectedIndex_Next != null || SelectedIndex_Prev != null) == true)
                        {
                            if (SelectedIndex_Prev == null)
                            {
                                UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[SelectedIdx]);
                                rail.MV3D_List.Remove(rail.MV3D_List[SelectedIdx]);

                                UserCtrl.MainViewPort.Children.Remove(rail.TV3D_List[SelectedIdx]);
                                rail.TV3D_List.Remove(rail.TV3D_List[SelectedIdx]);

                                for (int i = 0; i < rail.MV3D_List.Count; i++)
                                {
                                    string[] MDLInfo = rail.MV3D_List[i].GetName().Split(' ');
                                    string New_MDLInfo = MDLInfo[0] + " " + i.ToString() + " " + MDLInfo[2];
                                    rail.MV3D_List[i].SetName(New_MDLInfo);
                                }

                                //MessageBox.Show("PrevPoint not found : FirstPoint");
                            }
                            if (SelectedIndex_Next == null)
                            {
                                UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[SelectedIdx]);
                                rail.MV3D_List.Remove(rail.MV3D_List[SelectedIdx]);

                                UserCtrl.MainViewPort.Children.Remove(rail.TV3D_List[SelectedIdx - 1]);
                                rail.TV3D_List.Remove(rail.TV3D_List[SelectedIdx - 1]);

                                for (int i = 0; i < rail.MV3D_List.Count; i++)
                                {
                                    string[] MDLInfo = rail.MV3D_List[i].GetName().Split(' ');
                                    string New_MDLInfo = MDLInfo[0] + " " + i.ToString() + " " + MDLInfo[2];
                                    rail.MV3D_List[i].SetName(New_MDLInfo);
                                }

                                //MessageBox.Show("NextPoint not found : EndPoint");
                            }
                        }
                    }
                }
                if (railType == RailType.Line)
                {
                    if (SelectedIndex_Current != null)
                    {
                        if ((SelectedIndex_Next == null && SelectedIndex_Prev == null) == true)
                        {
                            UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[SelectedIdx]);
                            rail.MV3D_List.Remove(rail.MV3D_List[SelectedIdx]);

                            //MessageBox.Show("Point3D Only");
                        }
                        else if ((SelectedIndex_Next != null && SelectedIndex_Prev != null) == true)
                        {
                            point3Ds.Add(SelectedIndex_Prev.Value);
                            point3Ds.Add(SelectedIndex_Next.Value);

                            //Pointを削除
                            UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[SelectedIdx]);
                            rail.MV3D_List.Remove(rail.MV3D_List[SelectedIdx]);

                            //Pointの両端に存在するLinesVisual3Dを削除
                            UserCtrl.MainViewPort.Children.Remove(rail.LV3D_List[SelectedIdx]);
                            rail.LV3D_List.Remove(rail.LV3D_List[SelectedIdx]);

                            UserCtrl.MainViewPort.Children.Remove(rail.LV3D_List[SelectedIdx - 1]);
                            rail.LV3D_List.Remove(rail.LV3D_List[SelectedIdx - 1]);

                            for (int i = 0; i < rail.MV3D_List.Count; i++)
                            {
                                string[] MDLInfo = rail.MV3D_List[i].GetName().Split(' ');
                                string New_MDLInfo = MDLInfo[0] + " " + i.ToString() + " " + MDLInfo[2];
                                rail.MV3D_List[i].SetName(New_MDLInfo);
                            }

                            List<Point3D> OneLine = new List<Point3D>();
                            OneLine.Add(point3Ds[0]);
                            OneLine.Add(point3Ds[1]);

                            LinesVisual3D linesVisual3D = new LinesVisual3D
                            {
                                Points = new Point3DCollection(OneLine),
                                Thickness = TubeDiametor,
                                Color = color
                            };

                            rail.LV3D_List.Insert(SelectedIdx - 1, linesVisual3D);

                            UserCtrl.MainViewPort.Children.Add(linesVisual3D);

                            //MessageBox.Show("PrevPoint and NextPoint");
                        }
                        else if ((SelectedIndex_Next != null || SelectedIndex_Prev != null) == true)
                        {
                            if (SelectedIndex_Prev == null)
                            {
                                UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[SelectedIdx]);
                                rail.MV3D_List.Remove(rail.MV3D_List[SelectedIdx]);

                                UserCtrl.MainViewPort.Children.Remove(rail.LV3D_List[SelectedIdx]);
                                rail.LV3D_List.Remove(rail.LV3D_List[SelectedIdx]);

                                for (int i = 0; i < rail.MV3D_List.Count; i++)
                                {
                                    string[] MDLInfo = rail.MV3D_List[i].GetName().Split(' ');
                                    string New_MDLInfo = MDLInfo[0] + " " + i.ToString() + " " + MDLInfo[2];
                                    rail.MV3D_List[i].SetName(New_MDLInfo);
                                }

                                //MessageBox.Show("PrevPoint not found : FirstPoint");
                            }
                            if (SelectedIndex_Next == null)
                            {
                                UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[SelectedIdx]);
                                rail.MV3D_List.Remove(rail.MV3D_List[SelectedIdx]);

                                UserCtrl.MainViewPort.Children.Remove(rail.LV3D_List[SelectedIdx - 1]);
                                rail.LV3D_List.Remove(rail.LV3D_List[SelectedIdx - 1]);

                                for (int i = 0; i < rail.MV3D_List.Count; i++)
                                {
                                    string[] MDLInfo = rail.MV3D_List[i].GetName().Split(' ');
                                    string New_MDLInfo = MDLInfo[0] + " " + i.ToString() + " " + MDLInfo[2];
                                    rail.MV3D_List[i].SetName(New_MDLInfo);
                                }

                                //MessageBox.Show("NextPoint not found : EndPoint");
                            }
                        }
                    }
                }
            }

            public static void DeleteRail(UserControl1 UserCtrl, Rail rail)
            {
                if (rail.TV3D_List != null)
                {
                    for (int TVCount = 0; TVCount < rail.TV3D_List.Count; TVCount++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(rail.TV3D_List[TVCount]);
                        UserCtrl.UpdateLayout();
                    }

                    rail.TV3D_List.Clear();
                }

                if (rail.LV3D_List != null)
                {
                    for (int LVCount = 0; LVCount < rail.LV3D_List.Count; LVCount++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(rail.LV3D_List[LVCount]);
                        UserCtrl.UpdateLayout();
                    }

                    rail.LV3D_List.Clear();
                }

                if (rail.MV3D_List != null)
                {
                    for (int MV3DCount = 0; MV3DCount < rail.MV3D_List.Count; MV3DCount++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(rail.MV3D_List[MV3DCount]);
                        UserCtrl.UpdateLayout();
                    }

                    rail.MV3D_List.Clear();
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="UserCtrl"></param>
            /// <param name="rail"></param>
            /// <param name="railType"></param>
            public static void ResetRail(UserControl1 UserCtrl, Rail rail, RailType railType)
            {
                if (railType == RailType.Line)
                {
                    for (int i = 0; i < rail.LV3D_List.Count; i++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(rail.LV3D_List[i]);
                    }

                    rail.LV3D_List.Clear();
                }
                if (railType == RailType.Tube)
                {
                    for (int i = 0; i < rail.TV3D_List.Count; i++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(rail.TV3D_List[i]);
                    }

                    rail.TV3D_List.Clear();
                }
            }
        }

        public class Point3DSystem : TSRSystem
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

        public class KMP_3DCheckpointSystem : HTK_3DES.PathTools
        {
            public class Checkpoint
            {
                public Rail Checkpoint_Left { get; set; }
                public Rail Checkpoint_Right { get; set; }
                public List<LinesVisual3D> Checkpoint_Line { get; set; }
                public List<TubeVisual3D> Checkpoint_Tube { get; set; }
            }

            public static void DeleteRailChk(UserControl1 UserCtrl, Checkpoint railChk)
            {
                if (railChk.Checkpoint_Left.MV3D_List != null)
                {
                    for (int ChkLeftCount = 0; ChkLeftCount < railChk.Checkpoint_Left.MV3D_List.Count; ChkLeftCount++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(railChk.Checkpoint_Left.MV3D_List[ChkLeftCount]);
                        UserCtrl.UpdateLayout();
                    }

                    railChk.Checkpoint_Left.MV3D_List.Clear();
                }

                if (railChk.Checkpoint_Right.MV3D_List != null)
                {
                    for (int ChkRightCount = 0; ChkRightCount < railChk.Checkpoint_Right.MV3D_List.Count; ChkRightCount++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(railChk.Checkpoint_Right.MV3D_List[ChkRightCount]);
                        UserCtrl.UpdateLayout();
                    }

                    railChk.Checkpoint_Right.MV3D_List.Clear();
                }

                if (railChk.Checkpoint_Line != null)
                {
                    for (int ChkLineCount = 0; ChkLineCount < railChk.Checkpoint_Line.Count; ChkLineCount++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(railChk.Checkpoint_Line[ChkLineCount]);
                        UserCtrl.UpdateLayout();
                    }

                    railChk.Checkpoint_Line.Clear();
                }

                if (railChk.Checkpoint_Left.LV3D_List != null)
                {
                    for (int ChkRLineLeftCount = 0; ChkRLineLeftCount < railChk.Checkpoint_Left.LV3D_List.Count; ChkRLineLeftCount++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(railChk.Checkpoint_Left.LV3D_List[ChkRLineLeftCount]);
                        UserCtrl.UpdateLayout();
                    }

                    railChk.Checkpoint_Left.LV3D_List.Clear();
                }

                if (railChk.Checkpoint_Right.LV3D_List != null)
                {
                    for (int ChkRLineRightCount = 0; ChkRLineRightCount < railChk.Checkpoint_Right.LV3D_List.Count; ChkRLineRightCount++)
                    {
                        UserCtrl.MainViewPort.Children.Remove(railChk.Checkpoint_Right.LV3D_List[ChkRLineRightCount]);
                        UserCtrl.UpdateLayout();
                    }

                    railChk.Checkpoint_Right.LV3D_List.Clear();
                }
            }
        }
    }
}
