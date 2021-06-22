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

namespace MK7_KMP_Editor_For_PG_
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

        #region MouseMoveRotateSystem(WIP)
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
        //        if (signcheck == 1)
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
            if (PositionMode == PositionMode.MouseCursor)
            {
                //CursorPosition
                PosMode = (Point3D)MainViewPort.CursorPosition;
            }
            if (PositionMode == PositionMode.ElementPosition)
            {
                PosMode = (Point3D)MainViewPort.CursorOnElementPosition;
            }

            return PosMode;
        }
    }
}
