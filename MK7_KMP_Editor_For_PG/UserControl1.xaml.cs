using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using MK7_3D_KMP_Editor.PropertyGridObject;

namespace MK7_3D_KMP_Editor
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        #region ViewPortControl
        public void AddItem(Visual3D visual3D)
        {
            MainViewPort.Children.Add(visual3D);
            UpdateViewport();
        }

        public void RemoveItem(Visual3D visual3D)
        {
            MainViewPort.Children.Remove(visual3D);
            UpdateViewport();
        }

        public void RemoveItemAt(int index)
        {
            MainViewPort.Children.RemoveAt(index);
            UpdateViewport();
        }

        public void VpItemAddRange(List<ModelVisual3D> values)
        {
            foreach (var i in values) MainViewPort.Children.Add(i);
            UpdateViewport();
        }

        public void VpItemDeleteRange(List<ModelVisual3D> values)
        {
            foreach (var i in values) MainViewPort.Children.Remove(i);
            UpdateViewport();
        }

        public void UpdateViewport()
        {
            MainViewPort.UpdateLayout();
        }

        //public void VpItemAddRange<T>(List<T> values)
        //{
        //    if (values.Count != 0)
        //    {
        //        foreach (var i in values)
        //        {
        //            Visual3D v = (Visual3D)(object)i;
        //            render.MainViewPort.Children.Add(v);
        //        }
        //    }
        //    else
        //    {
        //        System.Windows.MessageBox.Show("Error");
        //    }
        //}

        //public void VpItemDeleteRange<T>(List<T> values)
        //{
        //    if (values.Count != 0)
        //    {
        //        foreach (var i in values)
        //        {
        //            Visual3D v = (Visual3D)(object)i;
        //            render.MainViewPort.Children.Remove(v);
        //        }
        //    }
        //    else
        //    {
        //        System.Windows.MessageBox.Show("Error");
        //    }
        //}
        #endregion

        //public Vector3D Drag(System.Windows.Input.MouseEventArgs e, Vector3D InputVector3D, bool EnableOnElementPosition = false)
        //{
        //    Vector3D? pos = null;
        //    if (EnableOnElementPosition == true)
        //    {
        //        if (MainViewPort.CursorOnElementPosition != null)
        //        {
        //            Point p = e.GetPosition(MainViewPort);
        //            pos = MainViewPort.CursorOnElementPosition.GetValueOrDefault().ToVector3D();
        //            //var f = MainViewPort.Viewport.Point3DtoPoint2D(MainViewPort.CursorOnElementPosition.Value);
        //            //pos = MainViewPort.Viewport.Point2DtoRay3D(f).Origin.ToVector3D();

        //            //pos = MainViewPort.Viewport.UnProject(p, MainViewPort.CursorOnElementPosition.Value, new Vector3D(0, 0, 0)).Value.ToVector3D();
        //            ////if (pos.HasValue)
        //            ////{
        //            ////    //Vector3D vec = pos.Value.ToVector3D();
        //            ////    pos.Value.X = Math.Round(pos.Value.X, 3, MidpointRounding.AwayFromZero);
        //            ////    pos.Value.Y = Math.Round(pos.Value.Y, 3, MidpointRounding.AwayFromZero);
        //            ////    pos.Z = Math.Round(pos.Value.Z, 3, MidpointRounding.AwayFromZero);
        //            ////    //return vec;
        //            ////}
        //        }
        //        else if (MainViewPort.CursorOnElementPosition == null)
        //        {
        //            pos = Drag(InputVector3D, e);
        //        }
        //    }
        //    else if (EnableOnElementPosition == false)
        //    {
        //        pos = Drag(InputVector3D, e);
        //    }

        //    return (Vector3D)pos;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputVector3D"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Vector3D Drag(Vector3D InputVector3D, System.Windows.Input.MouseEventArgs e)
        {
            Point p = e.GetPosition(MainViewPort);
            Point3D? pos = MainViewPort.Viewport.UnProject(p, new Point3D(InputVector3D.X, InputVector3D.Y, InputVector3D.Z), MainViewPort.Camera.LookDirection);
            if (pos.HasValue)
            {
                Vector3D vec = pos.Value.ToVector3D();
                vec.X = Math.Round(vec.X, 3, MidpointRounding.AwayFromZero);
                vec.Y = Math.Round(vec.Y, 3, MidpointRounding.AwayFromZero);
                vec.Z = Math.Round(vec.Z, 3, MidpointRounding.AwayFromZero);
                return vec;
            }
            return pos.Value.ToVector3D();
        }

        #region MouseMoveRotateSystem(DELETE)
        //public double Degress(double d)
        //{
        //    double Degress_Val = new double();

        //    if (-3.141 <= d && d <= 3.141)
        //    {
        //        Degress_Val = d;
        //    }
        //    else
        //    {
        //        double signcheck = Math.Sign(d);

        //        if (signcheck == -1)
        //        {
        //            //符号付き
        //            Degress_Val = -3.141;
        //            //Degress_Val = Math.Min(-3.141, d);

        //        }
        //        else if (signcheck == 1)
        //        {
        //            //符号なし
        //            Degress_Val = 3.141;
        //            //Degress_Val = Math.Max(3.141, d);
        //        }
        //    }
        //    return Degress_Val;
        //}

        //public Vector3D RotateDrag(Vector3D InputVector3D)
        //{
        //    Vector3D NewRotate3D = InputVector3D;
        //    NewRotate3D.X = Degress(Math.Round(NewRotate3D.X, 3, MidpointRounding.AwayFromZero));
        //    NewRotate3D.Y = Degress(Math.Round(NewRotate3D.Y, 3, MidpointRounding.AwayFromZero));
        //    NewRotate3D.Z = Degress(Math.Round(NewRotate3D.Z, 3, MidpointRounding.AwayFromZero));

        //    return NewRotate3D;
        //}
        #endregion

        public enum PositionMode
        {
            CameraPos,
            MouseCursor,
            ElementPosition
        }

        /// <summary>
        /// オブジェクトの配置に関するメソッド(UserControl1.xaml.cs)
        /// </summary>
        /// <param name="PositionMode">オブジェクトがどの位置をもとに配置されるか</param>
        /// <returns>Point3D</returns>
        public Point3D ViewportPosition(PositionMode PositionMode)
        {
            //Point3D
            Point3D PosMode = new Point3D();
            //PerspectiveCameraの位置をPoint3Dで取得
            if (PositionMode == PositionMode.CameraPos)
            {
                //CameraPosition
                PosMode = MainViewPort.Camera.Position;
            }
            else if (PositionMode == PositionMode.MouseCursor)
            {
                //CursorPosition
                PosMode = (Point3D)MainViewPort.CursorPosition;
            }
            else if (PositionMode == PositionMode.ElementPosition)
            {
                if (MainViewPort.CursorOnElementPosition != null) PosMode = (Point3D)MainViewPort.CursorOnElementPosition;
                else if (MainViewPort.CursorOnElementPosition == null) PosMode = (Point3D)MainViewPort.CursorPosition;
            }

            return PosMode;
        }

        public enum CheckpointSearchOption
        {
            Left,
            Right,
            Null
        }

        /// <summary>
        /// LookAtObj() : CameraViewType
        /// </summary>
        public enum CameraViewType
        {
            Perspective,
            Orthographic
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point3D">Target</param>
        /// <param name="distance">Perspective = Enable, Orthographic = Disable</param>
        /// <param name="animationTime">animationTime</param>
        /// <param name="CameraType">Camera Type</param>
        public void LookAtObj(Point3D point3D, double distance, double animationTime, CameraViewType CameraType)
        {
            if (CameraType == CameraViewType.Perspective) MainViewPort.Camera.LookAt(point3D, distance, animationTime);
            if (CameraType == CameraViewType.Orthographic) MainViewPort.Camera.LookAt(point3D, animationTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point3D">Target</param>
        /// <param name="distance">CameraType = Perspective -> Enable, CameraType = Orthographic -> Disable</param>
        /// <param name="animationTime">animationTime</param>
        /// <param name="CameraType">(UserControl1).MainViewPort.Camera.GetType()</param>
        public void LookAtObj(Point3D point3D, double distance, double animationTime, Type CameraType)
        {
            if (CameraType.Name == "PerspectiveCamera") MainViewPort.Camera.LookAt(point3D, distance, animationTime);
            if (CameraType.Name == "OrthographicCamera") MainViewPort.Camera.LookAt(point3D, animationTime);
        }

        public void FindObject(object input, int ValueIndex, int GroupIndex = -1, CheckpointSearchOption checkpointSearchOption = CheckpointSearchOption.Null, double CheckpointYOffsetValue = 0)
        {
            if (input is List<KartPoint_PGS.TPTKValue>)
            {
                var PG_KartPositions = input as List<KartPoint_PGS.TPTKValue>;
                LookAtObj(PG_KartPositions[ValueIndex].Position_Value.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
            else if (input is List<EnemyRoute_PGS.HPNEValue>)
            {
                var PG_EnemyPoints = input as List<EnemyRoute_PGS.HPNEValue>;
                LookAtObj(PG_EnemyPoints[GroupIndex].TPNEValueList[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
            else if (input is List<ItemRoute_PGS.HPTIValue>)
            {
                var PG_ItemPoints = input as List<ItemRoute_PGS.HPTIValue>;
                LookAtObj(PG_ItemPoints[GroupIndex].TPTIValueList[ValueIndex].TPTI_Positions.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
            else if (input is List<Checkpoint_PGS.HPKCValue>)
            {
                var PG_Checkpoints = input as List<Checkpoint_PGS.HPKCValue>;
                if (checkpointSearchOption == CheckpointSearchOption.Left)
                {
                    var p_Left = PG_Checkpoints[GroupIndex].TPKCValueList[ValueIndex].Position_2D_Left;
                    Vector3D Chkpt_Left = new Vector3D(p_Left.X, CheckpointYOffsetValue, p_Left.Y);
                    LookAtObj(Chkpt_Left.ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
                }
                else if (checkpointSearchOption == CheckpointSearchOption.Right)
                {
                    var p_Right = PG_Checkpoints[GroupIndex].TPKCValueList[ValueIndex].Position_2D_Right;
                    Vector3D Chkpt_Right = new Vector3D(p_Right.X, CheckpointYOffsetValue, p_Right.Y);
                    LookAtObj(Chkpt_Right.ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
                }
            }
            else if (input is List<KMPObject_PGS.JBOGValue>)
            {
                var PG_Objects = input as List<KMPObject_PGS.JBOGValue>;
                LookAtObj(PG_Objects[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
            else if (input is List<Route_PGS.ITOP_Route>)
            {
                var PG_Routes = input as List<Route_PGS.ITOP_Route>;
                LookAtObj(PG_Routes[GroupIndex].ITOP_PointList[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
            else if (input is List<Area_PGS.AERAValue>)
            {
                var PG_Areas = input as List<Area_PGS.AERAValue>;
                LookAtObj(PG_Areas[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
            else if (input is List<Camera_PGS.EMACValue>)
            {
                var PG_Cameras = input as List<Camera_PGS.EMACValue>;
                LookAtObj(PG_Cameras[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
            else if (input is List<RespawnPoint_PGS.TPGJValue>)
            {
                var PG_JugemPoints = input as List<RespawnPoint_PGS.TPGJValue>;
                LookAtObj(PG_JugemPoints[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
            else if (input is List<GlideRoute_PGS.HPLGValue>)
            {
                var PG_GlideRoutes = input as List<GlideRoute_PGS.HPLGValue>;
                LookAtObj(PG_GlideRoutes[GroupIndex].TPLGValueList[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000, MainViewPort.Camera.GetType());
            }
        }
    }
}
