using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Collections;
using HelixToolkit.Wpf;

namespace MK7_KMP_Editor_For_PG_
{
    class ViewPortObjVisibleSetting
    {
        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3D">ModelVisual3D</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, ModelVisual3D MV3D)
        {
            //非表示にする
            if (Visible == true)
            {
                UserCtrl.MainViewPort.Children.Remove((Visual3D)MV3D);
            }
            //表示する
            if (Visible == false)
            {
                if (UserCtrl.MainViewPort.Children.Contains(MV3D) == true) return;
                else UserCtrl.MainViewPort.Children.Add((Visual3D)MV3D);
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示の切り替えに使用する
        /// </summary>
        /// <param name="Visible">true=Hide</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3D_List">ModelVisual3D List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, List<ModelVisual3D> MV3D_List)
        {
            //非表示にする
            if (Visible == true)
            {
                //Visual3Dに変換, foreachで全て削除
                foreach (var MV3D_Del in MV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(MV3D_Del);
            }

            //表示する
            if (Visible == false)
            {
                foreach (var MV3D_Add in MV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false)) UserCtrl.MainViewPort.Children.Add(MV3D_Add);

                //foreach (var d in MV3D_List)
                //{
                //    if (UserCtrl.MainViewPort.Children.Contains(d) == false)
                //    {
                //        UserCtrl.MainViewPort.Children.Add(d);
                //    }
                //}
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="rail">Rail</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, HTK_3DES.PathTools.Rail rail)
        {
            //非表示にする
            if (Visible == true)
            {
                foreach (var MV3DList_Del in rail.MV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(MV3DList_Del);
                foreach (var TV3DList_Del in rail.TV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(TV3DList_Del);
            }

            //表示する
            if (Visible == false)
            {
                foreach (var MV3D_Add in rail.MV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false)) UserCtrl.MainViewPort.Children.Add(MV3D_Add);
                foreach (var TV3D_Add in rail.TV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false)) UserCtrl.MainViewPort.Children.Add(TV3D_Add);

                //foreach (var d in rail.MV3D_List)
                //{
                //    if (UserCtrl.MainViewPort.Children.Contains(d) == false)
                //    {
                //        UserCtrl.MainViewPort.Children.Add(d);
                //    }
                //}
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="rail_List">Rail List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, List<HTK_3DES.PathTools.Rail> rail_List)
        {
            //非表示にする
            if (Visible == true)
            {
                foreach (var MV3DList_Del in rail_List.SelectMany(x => x.MV3D_List.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(MV3DList_Del);
                foreach (var TV3DList_Del in rail_List.SelectMany(x => x.TV3D_List.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(TV3DList_Del);

                //foreach (var es in rail_List)
                //{
                //    foreach (var MV3DList_Del in es.MV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(MV3DList_Del);
                //    foreach (var TV3DList_Del in es.TV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(TV3DList_Del);
                //}
            }

            //表示する
            if (Visible == false)
            {
                foreach (var MV3D_Add in rail_List.SelectMany(x => x.MV3D_List.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false))) UserCtrl.MainViewPort.Children.Add(MV3D_Add);
                foreach (var TV3D_Add in rail_List.SelectMany(x => x.TV3D_List.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false))) UserCtrl.MainViewPort.Children.Add(TV3D_Add);

                //foreach (var dr in rail_List)
                //{
                //    foreach (var dq1 in dr.MV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                //    {
                //        UserCtrl.MainViewPort.Children.Add(dq1);
                //    }

                //    foreach (var dq2 in dr.TV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                //    {
                //        UserCtrl.MainViewPort.Children.Add(dq2);
                //    }
                //}
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="Checkpoint">Checkpoint</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint Checkpoint)
        {
            //非表示にする
            if (Visible == true)
            {
                foreach (var Checkpoint_DLine_Del in Checkpoint.Checkpoint_Line.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_DLine_Del);
                foreach (var Checkpoint_LeftLine in Checkpoint.Checkpoint_Left.LV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_LeftLine);
                foreach (var Checkpoint_LeftObj in Checkpoint.Checkpoint_Left.MV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_LeftObj);
                foreach (var Checkpoint_RightLine in Checkpoint.Checkpoint_Right.LV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_RightLine);
                foreach (var Checkpoint_RightObj in Checkpoint.Checkpoint_Right.MV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_RightObj);
                foreach (var Checkpoint_SideWall_Left in Checkpoint.SideWall_Left.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SideWall_Left);
                foreach (var Checkpoint_SideWall_Right in Checkpoint.SideWall_Right.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SideWall_Right);
                foreach (var Checkpoint_SplitWall in Checkpoint.Checkpoint_SplitWallMDL.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SplitWall);
            }

            //表示する
            if (Visible == false)
            {
                foreach (var Checkpoint_DLine_Add in Checkpoint.Checkpoint_Line.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_DLine_Add);
                }

                foreach (var Checkpoint_LeftLine_Add in Checkpoint.Checkpoint_Left.LV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_LeftLine_Add);
                }

                foreach (var Checkpoint_LeftObj_Add in Checkpoint.Checkpoint_Left.MV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_LeftObj_Add);
                }

                foreach (var Checkpoint_RightLine_Add in Checkpoint.Checkpoint_Right.LV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_RightLine_Add);
                }

                foreach (var Checkpoint_RightObj_Add in Checkpoint.Checkpoint_Right.MV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_RightObj_Add);
                }


                foreach (var Checkpoint_SideWall_Left_Add in Checkpoint.SideWall_Left.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_SideWall_Left_Add);
                }

                foreach (var Checkpoint_SideWall_Right_Add in Checkpoint.SideWall_Right.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_SideWall_Right_Add);
                }

                foreach (var Checkpoint_SplitWall_Add in Checkpoint.Checkpoint_SplitWallMDL.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_SplitWall_Add);
                }
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="Checkpoint_List">Checkpoint List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint> Checkpoint_List)
        {
            //非表示にする
            if (Visible == true)
            {
                foreach (var Checkpoint_DLine in Checkpoint_List.SelectMany(x => x.Checkpoint_Line.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(Checkpoint_DLine);
                foreach (var Checkpoint_LeftLine in Checkpoint_List.SelectMany(x => x.Checkpoint_Left.LV3D_List.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(Checkpoint_LeftLine);
                foreach (var Checkpoint_LeftObj in Checkpoint_List.SelectMany(x => x.Checkpoint_Left.MV3D_List.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(Checkpoint_LeftObj);
                foreach (var Checkpoint_RightLine in Checkpoint_List.SelectMany(x => x.Checkpoint_Right.LV3D_List.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(Checkpoint_RightLine);
                foreach (var Checkpoint_RightObj in Checkpoint_List.SelectMany(x => x.Checkpoint_Right.MV3D_List.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(Checkpoint_RightObj);
                foreach (var Checkpoint_SideWall_Left in Checkpoint_List.SelectMany(x => x.SideWall_Left.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SideWall_Left);
                foreach (var Checkpoint_SideWall_Right in Checkpoint_List.SelectMany(x => x.SideWall_Right.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SideWall_Right);
                foreach (var Checkpoint_SplitWall in Checkpoint_List.SelectMany(x => x.Checkpoint_SplitWallMDL.ToArray<Visual3D>())) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SplitWall);
            }

            //表示する
            if (Visible == false)
            {
                foreach (var Checkpoint_DLine in Checkpoint_List.SelectMany(x => x.Checkpoint_Line.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false)))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_DLine);
                }

                foreach (var Checkpoint_LeftLine in Checkpoint_List.SelectMany(x => x.Checkpoint_Left.LV3D_List.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false)))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_LeftLine);
                }

                foreach (var Checkpoint_LeftObj in Checkpoint_List.SelectMany(x => x.Checkpoint_Left.MV3D_List.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false)))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_LeftObj);
                }

                foreach (var Checkpoint_RightLine in Checkpoint_List.SelectMany(x => x.Checkpoint_Right.LV3D_List.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false)))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_RightLine);
                }

                foreach (var Checkpoint_RightObj in Checkpoint_List.SelectMany(x => x.Checkpoint_Right.MV3D_List.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false)))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_RightObj);
                }

                foreach (var Checkpoint_SideWall_Left in Checkpoint_List.SelectMany(x => x.SideWall_Left.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false)))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_SideWall_Left);
                }

                foreach (var Checkpoint_SideWall_Right in Checkpoint_List.SelectMany(x => x.SideWall_Right.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false)))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_SideWall_Right);
                }

                foreach (var Checkpoint_SplitWall in Checkpoint_List.SelectMany(x => x.Checkpoint_SplitWallMDL.Where(y => UserCtrl.MainViewPort.Children.Contains(y) == false)))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_SplitWall);
                }
            }
        }

        #region VisibleCheck
        public static bool VisibleCheck(UserControl1 UserCtrl, HTK_3DES.PathTools.Rail rail)
        {
            bool Ch = new bool();

            List<bool> XXXXRoute_TV3D_BoolList = rail.TV3D_List.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();
            List<bool> XXXXRoute_MV3D_BoolList = rail.MV3D_List.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();

            if (XXXXRoute_TV3D_BoolList.Distinct().Count() == 1 && XXXXRoute_MV3D_BoolList.Distinct().Count() == 1)
            {
                if (XXXXRoute_TV3D_BoolList.Distinct().ToList()[0] == true && XXXXRoute_MV3D_BoolList.Distinct().ToList()[0] == true) Ch = false;
                if (XXXXRoute_TV3D_BoolList.Distinct().ToList()[0] == false && XXXXRoute_MV3D_BoolList.Distinct().ToList()[0] == false) Ch = true;
            }

            return Ch;
        }

        public static bool VisibleCheck(UserControl1 UserCtrl, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint)
        {
            bool Ch = new bool();

            List<bool> BL_Line = checkpoint.Checkpoint_Line.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();
            List<bool> BL_L_LV3D = checkpoint.Checkpoint_Left.LV3D_List.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();
            List<bool> BL_L_MV3D = checkpoint.Checkpoint_Left.MV3D_List.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();
            List<bool> BL_R_LV3D = checkpoint.Checkpoint_Right.LV3D_List.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();
            List<bool> BL_R_MV3D = checkpoint.Checkpoint_Right.MV3D_List.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();

            List<bool> BL_L_SW = checkpoint.SideWall_Left.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();
            List<bool> BL_R_SW = checkpoint.SideWall_Right.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();
            List<bool> BL_SplitWall = checkpoint.Checkpoint_SplitWallMDL.Select(x => UserCtrl.MainViewPort.Children.Contains(x)).ToList();

            if (BL_Line.Distinct().Count() == 1 && BL_L_LV3D.Distinct().Count() == 1 && BL_L_MV3D.Distinct().Count() == 1 && BL_R_LV3D.Distinct().Count() == 1 && BL_R_MV3D.Distinct().Count() == 1 && BL_L_SW.Distinct().Count() == 1 && BL_R_SW.Distinct().Count() == 1 && BL_SplitWall.Distinct().Count() == 1)
            {
                if (BL_Line.Distinct().ToList()[0] == true && BL_L_LV3D.Distinct().ToList()[0] == true && BL_L_MV3D.Distinct().ToList()[0] == true && BL_R_LV3D.Distinct().ToList()[0] == true && BL_R_MV3D.Distinct().ToList()[0] == true && BL_L_SW.Distinct().ToList()[0] == true && BL_R_SW.Distinct().ToList()[0] == true && BL_SplitWall.Distinct().ToList()[0] == true) Ch = false;
                if (BL_Line.Distinct().ToList()[0] == false && BL_L_LV3D.Distinct().ToList()[0] == false && BL_L_MV3D.Distinct().ToList()[0] == false && BL_R_LV3D.Distinct().ToList()[0] == false && BL_R_MV3D.Distinct().ToList()[0] == false && BL_L_SW.Distinct().ToList()[0] == false && BL_R_SW.Distinct().ToList()[0] == false && BL_SplitWall.Distinct().ToList()[0] == false) Ch = true;
            }

            return Ch;
        }

        public static bool VisibleCheck(UserControl1 UserCtrl, ModelVisual3D MV3D)
        {
            bool Ch = new bool();
            if (UserCtrl.MainViewPort.Children.Contains(MV3D) == true) Ch = false;
            if (UserCtrl.MainViewPort.Children.Contains(MV3D) == false) Ch = true;

            return Ch;
        }
        #endregion
    }
}
