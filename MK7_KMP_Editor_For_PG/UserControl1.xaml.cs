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

        public enum CheckpointSearchOption
        {
            Left,
            Right,
            Null
        }

        public void FindObject(object input, int ValueIndex, int GroupIndex = -1, CheckpointSearchOption checkpointSearchOption = CheckpointSearchOption.Null, double ChkptYOffsetValue = 0)
        {
            if (input is List<KMPPropertyGridSettings.TPTK_Section.TPTKValue>)
            {
                var PG_KartPositions = input as List<KMPPropertyGridSettings.TPTK_Section.TPTKValue>;
                MainViewPort.Camera.LookAt(PG_KartPositions[ValueIndex].Position_Value.GetVector3D().ToPoint3D(), 500, 1000);
            }
            if (input is List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>)
            {
                var PG_EnemyPoints = input as List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>;
                MainViewPort.Camera.LookAt(PG_EnemyPoints[GroupIndex].TPNEValueList[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000);
            }
            if (input is List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>)
            {
                var PG_ItemPoints = input as List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>;
                MainViewPort.Camera.LookAt(PG_ItemPoints[GroupIndex].TPTIValueList[ValueIndex].TPTI_Positions.GetVector3D().ToPoint3D(), 500, 1000);
            }
            if (input is List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue>)
            {
                var PG_Checkpoints = input as List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue>;
                if (checkpointSearchOption == CheckpointSearchOption.Left)
                {
                    var p_Left = PG_Checkpoints[GroupIndex].TPKCValueList[ValueIndex].Position_2D_Left;
                    Vector3D Chkpt_Left = new Vector3D(p_Left.X, ChkptYOffsetValue, p_Left.Y);
                    MainViewPort.Camera.LookAt(Chkpt_Left.ToPoint3D(), 500, 1000);
                }
                if (checkpointSearchOption == CheckpointSearchOption.Right)
                {
                    var p_Right = PG_Checkpoints[GroupIndex].TPKCValueList[ValueIndex].Position_2D_Right;
                    Vector3D Chkpt_Right = new Vector3D(p_Right.X, ChkptYOffsetValue, p_Right.Y);
                    MainViewPort.Camera.LookAt(Chkpt_Right.ToPoint3D(), 500, 1000);
                }
            }
            if (input is List<KMPPropertyGridSettings.JBOG_section.JBOGValue>)
            {
                var PG_Objects = input as List<KMPPropertyGridSettings.JBOG_section.JBOGValue>;
                MainViewPort.Camera.LookAt(PG_Objects[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000);
            }
            if (input is List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route>)
            {
                var PG_Routes = input as List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route>;
                MainViewPort.Camera.LookAt(PG_Routes[GroupIndex].ITOP_PointList[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000);
            }
            if (input is List<KMPPropertyGridSettings.AERA_Section.AERAValue>)
            {
                var PG_Areas = input as List<KMPPropertyGridSettings.AERA_Section.AERAValue>;
                MainViewPort.Camera.LookAt(PG_Areas[GroupIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000);
            }
            if (input is List<KMPPropertyGridSettings.EMAC_Section.EMACValue>)
            {
                var PG_Cameras = input as List<KMPPropertyGridSettings.EMAC_Section.EMACValue>;
                MainViewPort.Camera.LookAt(PG_Cameras[GroupIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000);
            }
            if (input is List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue>)
            {
                var PG_JugemPoints = input as List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue>;
                MainViewPort.Camera.LookAt(PG_JugemPoints[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000);
            }
            if (input is List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>)
            {
                var PG_GlideRoutes = input as List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>;
                MainViewPort.Camera.LookAt(PG_GlideRoutes[GroupIndex].TPLGValueList[ValueIndex].Positions.GetVector3D().ToPoint3D(), 500, 1000);
            }
        }
    }
}
