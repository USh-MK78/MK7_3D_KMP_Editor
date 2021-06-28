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
        /// 3Dオブジェクトの表示、非表示の切り替えに使用する
        /// </summary>
        /// <param name="Visible">true=Hide</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3D">ModelVisual3D List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, List<ModelVisual3D> MV3D)
        {
            //非表示にする
            if (Visible == true)
            {
                try
                {
                    //Visual3Dに変換
                    var MV3DList_Del = MV3D.ToArray<Visual3D>();

                    //foreachで全て削除
                    foreach (var d in MV3DList_Del)
                    {
                        UserCtrl.MainViewPort.Children.Remove(d);
                    }
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }

            //表示する
            if (Visible == false)
            {
                try
                {
                    //ModelVisual3Dに変換
                    var MV3DList_Add = MV3D.ToArray<ModelVisual3D>();

                    //foreachで全て追加
                    foreach (var d in MV3DList_Add)
                    {
                        UserCtrl.MainViewPort.Children.Add(d);
                    }
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示の切り替えに使用する
        /// </summary>
        /// <param name="Visible">true=Hide</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3DList_List">ModelVisual3D List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, List<List<ModelVisual3D>> MV3DList_List)
        {
            //非表示にする
            if (Visible == true)
            {
                foreach(var es in MV3DList_List)
                {
                    try
                    {
                        //Visual3Dに変換
                        var MV3DList_Del = es.ToArray<Visual3D>();

                        //foreachで全て削除
                        foreach (var d in MV3DList_Del)
                        {
                            UserCtrl.MainViewPort.Children.Remove(d);
                        }
                    }
                    catch (System.ArgumentException)
                    {
                        //Nothing
                    }
                }
            }

            //表示する
            if (Visible == false)
            {
                foreach (var es in MV3DList_List)
                {
                    try
                    {
                        //Visual3Dに変換
                        var MV3DList_Del = es.ToArray<Visual3D>();

                        //foreachで全て削除
                        foreach (var d in MV3DList_Del)
                        {
                            UserCtrl.MainViewPort.Children.Add(d);
                        }
                    }
                    catch (System.ArgumentException)
                    {
                        //Nothing
                    }
                }
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3D">ModelVisual3D List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, ModelVisual3D MV3D)
        {
            //非表示にする
            if (Visible == true)
            {
                try
                {
                    var M3D_Del = (Visual3D)MV3D;
                    UserCtrl.MainViewPort.Children.Remove(M3D_Del);
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }
            //表示する
            if (Visible == false)
            {
                try
                {
                    var M3D_Del = (Visual3D)MV3D;
                    UserCtrl.MainViewPort.Children.Add(M3D_Del);
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3D">ModelVisual3D List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, HTK_3DES.PathTools.Rail rail)
        {
            //非表示にする
            if (Visible == true)
            {
                try
                {
                    #region ModelVisual3D
                    //Visual3Dに変換
                    var MV3DList_Del = rail.MV3D_List.ToArray<Visual3D>();

                    //foreachで全て削除
                    foreach (var d in MV3DList_Del)
                    {
                        UserCtrl.MainViewPort.Children.Remove(d);
                    }
                    #endregion

                    #region TubeVisual3D
                    var TV3DList_Del = rail.TV3D_List.ToArray<Visual3D>();

                    foreach (var d in TV3DList_Del)
                    {
                        UserCtrl.MainViewPort.Children.Remove(d);
                    }
                    #endregion
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }

            //表示する
            if (Visible == false)
            {
                try
                {
                    #region ModelVisual3D
                    //ModelVisual3Dに変換
                    var MV3DList_Add = rail.MV3D_List.ToArray<ModelVisual3D>();

                    //foreachで全て追加
                    foreach (var d in MV3DList_Add)
                    {
                        UserCtrl.MainViewPort.Children.Add(d);
                    }
                    #endregion

                    #region TubeVisual3D
                    var TV3DList_Add = rail.TV3D_List.ToArray<TubeVisual3D>();

                    foreach (var d in TV3DList_Add)
                    {
                        UserCtrl.MainViewPort.Children.Add(d);
                    }
                    #endregion
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3D">ModelVisual3D List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, List<HTK_3DES.PathTools.Rail> rail_List)
        {
            //非表示にする
            if (Visible == true)
            {
                try
                {
                    foreach (var es in rail_List)
                    {
                        #region ModelVisual3D
                        //Visual3Dに変換
                        var MV3DList_Del = es.MV3D_List.ToArray<Visual3D>();

                        //foreachで全て削除
                        foreach (var d in MV3DList_Del)
                        {
                            UserCtrl.MainViewPort.Children.Remove(d);
                        }
                        #endregion

                        #region TubeVisual3D
                        var TV3DList_Del = es.TV3D_List.ToArray<Visual3D>();

                        foreach (var d in TV3DList_Del)
                        {
                            UserCtrl.MainViewPort.Children.Remove(d);
                        }
                        #endregion
                    }
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }

            //表示する
            if (Visible == false)
            {
                try
                {
                    foreach (var es in rail_List)
                    {
                        #region ModelVisual3D
                        //ModelVisual3Dに変換
                        var MV3DList_Add = es.MV3D_List.ToArray<ModelVisual3D>();

                        //foreachで全て追加
                        foreach (var d in MV3DList_Add)
                        {
                            UserCtrl.MainViewPort.Children.Add(d);
                        }
                        #endregion

                        #region TubeVisual3D
                        var TV3DList_Add = es.TV3D_List.ToArray<TubeVisual3D>();

                        foreach (var d in TV3DList_Add)
                        {
                            UserCtrl.MainViewPort.Children.Add(d);
                        }
                        #endregion
                    }
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">bool</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3D">ModelVisual3D List</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint> Checkpoint_List)
        {
            //非表示にする
            if (Visible == true)
            {
                try
                {
                    foreach (var es in Checkpoint_List)
                    {
                        #region Checkpoint_DLine
                        //Visual3Dに変換
                        var Checkpoint_DLine_Del = es.Checkpoint_Line.ToArray<Visual3D>();

                        //foreachで全て削除
                        foreach (var d in Checkpoint_DLine_Del)
                        {
                            UserCtrl.MainViewPort.Children.Remove(d);
                        }
                        #endregion

                        #region Checkpoint_LeftLine
                        var Checkpoint_LeftLine = es.Checkpoint_Left.LV3D_List.ToArray<Visual3D>();

                        foreach (var d in Checkpoint_LeftLine)
                        {
                            UserCtrl.MainViewPort.Children.Remove(d);
                        }
                        #endregion

                        #region Checkpoint_LeftObj
                        var Checkpoint_LeftObj = es.Checkpoint_Left.MV3D_List.ToArray<Visual3D>();

                        foreach (var d in Checkpoint_LeftObj)
                        {
                            UserCtrl.MainViewPort.Children.Remove(d);
                        }
                        #endregion

                        #region Checkpoint_RightLine
                        var Checkpoint_RightLine = es.Checkpoint_Right.LV3D_List.ToArray<Visual3D>();

                        foreach (var d in Checkpoint_RightLine)
                        {
                            UserCtrl.MainViewPort.Children.Remove(d);
                        }
                        #endregion

                        #region Checkpoint_RightObj
                        var Checkpoint_RightObj = es.Checkpoint_Right.MV3D_List.ToArray<Visual3D>();

                        foreach (var d in Checkpoint_RightObj)
                        {
                            UserCtrl.MainViewPort.Children.Remove(d);
                        }
                        #endregion
                    }
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }

            //表示する
            if (Visible == false)
            {
                try
                {
                    foreach (var es in Checkpoint_List)
                    {
                        #region Checkpoint_DLine
                        //ModelVisual3Dに変換
                        var Checkpoint_DLine_Add = es.Checkpoint_Line.ToArray<LinesVisual3D>();

                        //foreachで全て追加
                        foreach (var d in Checkpoint_DLine_Add)
                        {
                            UserCtrl.MainViewPort.Children.Add(d);
                        }
                        #endregion

                        #region Checkpoint_LeftLine
                        var Checkpoint_LeftLine_Add = es.Checkpoint_Left.LV3D_List.ToArray<LinesVisual3D>();

                        foreach (var d in Checkpoint_LeftLine_Add)
                        {
                            UserCtrl.MainViewPort.Children.Add(d);
                        }
                        #endregion

                        #region Checkpoint_LeftObj
                        var Checkpoint_LeftObj_Add = es.Checkpoint_Left.MV3D_List.ToArray<ModelVisual3D>();

                        foreach (var d in Checkpoint_LeftObj_Add)
                        {
                            UserCtrl.MainViewPort.Children.Add(d);
                        }
                        #endregion

                        #region Checkpoint_RightLine
                        var Checkpoint_RightLine_Add = es.Checkpoint_Right.LV3D_List.ToArray<LinesVisual3D>();

                        foreach (var d in Checkpoint_RightLine_Add)
                        {
                            UserCtrl.MainViewPort.Children.Add(d);
                        }
                        #endregion

                        #region Checkpoint_RightObj
                        var Checkpoint_RightObj_Add = es.Checkpoint_Right.MV3D_List.ToArray<ModelVisual3D>();

                        foreach (var d in Checkpoint_RightObj_Add)
                        {
                            UserCtrl.MainViewPort.Children.Add(d);
                        }
                        #endregion
                    }
                }
                catch (System.ArgumentException)
                {
                    //Nothing
                }
            }
        }
    }
}
