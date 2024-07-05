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
using MK7_3D_KMP_Editor.PropertyGridObject;
using static MK7_3D_KMP_Editor.Render.KMPRendering;

namespace MK7_3D_KMP_Editor
{
    public class ViewPortObjVisibleSetting
    {
        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">true => UnHide, false => Hide</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="MV3D">ModelVisual3D</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, ModelVisual3D MV3D)
        {
            if (Visible == true)
            {
                if (UserCtrl.MainViewPort.Children.Contains(MV3D) == false) UserCtrl.MainViewPort.Children.Add(MV3D);
            }
            else if (Visible == false)
            {
                //非表示
                UserCtrl.MainViewPort.Children.Remove(MV3D);
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">true => UnHide, false => Hide</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="rail">Rail</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, HTK_3DES.PathTools.Rail rail)
        {
            if (Visible == true)
            {
                foreach (var MV3D_Add in rail.BasePointModelList.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false)) UserCtrl.MainViewPort.Children.Add(MV3D_Add);
                foreach (var TV3D_Add in rail.TV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false)) UserCtrl.MainViewPort.Children.Add(TV3D_Add);

                #region Backup
                //foreach (var d in rail.MV3D_List)
                //{
                //    if (UserCtrl.MainViewPort.Children.Contains(d) == false)
                //    {
                //        UserCtrl.MainViewPort.Children.Add(d);
                //    }
                //}
                #endregion
            }
            else if (Visible == false)
            {
                foreach (var MV3DList_Del in rail.BasePointModelList.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(MV3DList_Del);
                foreach (var TV3DList_Del in rail.TV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(TV3DList_Del);
            }
        }

        /// <summary>
        /// 3Dオブジェクトの表示、非表示
        /// </summary>
        /// <param name="Visible">true => UnHide, false => Hide</param>
        /// <param name="UserCtrl">UserControl(UserControl1.xaml)</param>
        /// <param name="Checkpoint">Checkpoint</param>
        public static void ViewportObj_Visibility(bool Visible, UserControl1 UserCtrl, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint Checkpoint)
        {
            if (Visible == true)
            {
                foreach (var Checkpoint_DLine_Add in Checkpoint.Checkpoint_Line.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_DLine_Add);
                }

                foreach (var Checkpoint_LeftLine_Add in Checkpoint.Checkpoint_Left.LV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_LeftLine_Add);
                }

                foreach (var Checkpoint_LeftObj_Add in Checkpoint.Checkpoint_Left.BasePointModelList.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_LeftObj_Add);
                }

                foreach (var Checkpoint_RightLine_Add in Checkpoint.Checkpoint_Right.LV3D_List.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
                {
                    UserCtrl.MainViewPort.Children.Add(Checkpoint_RightLine_Add);
                }

                foreach (var Checkpoint_RightObj_Add in Checkpoint.Checkpoint_Right.BasePointModelList.Where(x => UserCtrl.MainViewPort.Children.Contains(x) == false))
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
            else if (Visible == false)
            {
                foreach (var Checkpoint_DLine_Del in Checkpoint.Checkpoint_Line.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_DLine_Del);
                foreach (var Checkpoint_LeftLine in Checkpoint.Checkpoint_Left.LV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_LeftLine);
                foreach (var Checkpoint_LeftObj in Checkpoint.Checkpoint_Left.BasePointModelList.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_LeftObj);
                foreach (var Checkpoint_RightLine in Checkpoint.Checkpoint_Right.LV3D_List.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_RightLine);
                foreach (var Checkpoint_RightObj in Checkpoint.Checkpoint_Right.BasePointModelList.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_RightObj);
                foreach (var Checkpoint_SideWall_Left in Checkpoint.SideWall_Left.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SideWall_Left);
                foreach (var Checkpoint_SideWall_Right in Checkpoint.SideWall_Right.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SideWall_Right);
                foreach (var Checkpoint_SplitWall in Checkpoint.Checkpoint_SplitWallMDL.ToArray<Visual3D>()) UserCtrl.MainViewPort.Children.Remove(Checkpoint_SplitWall);
            }
        }

        public class KMPSectionVisibility
        {
            public bool Kartpoint { get; set; }
            public bool EnemyRoutes { get; set; }
            public bool ItemRoutes { get; set; }
            public bool Checkpoint { get; set; }
            public bool GameObject { get; set; }
            public bool Routes { get; set; }
            public bool Area { get; set; }
            public bool Camera { get; set; }
            public bool Returnpoints { get; set; }
            public bool GlideRoutes { get; set; }

            public static KMPSectionVisibility DefaultSetting(bool IsVisible = true)
            {
                return new KMPSectionVisibility(IsVisible, IsVisible, IsVisible, IsVisible, IsVisible, IsVisible, IsVisible, IsVisible, IsVisible, IsVisible);
            }

            public KMPSectionVisibility(bool Kartpoint, bool EnemyRoutes, bool ItemRoutes, bool Checkpoint, bool GameObject, bool Routes, bool Area, bool Camera, bool Returnpoints, bool GlideRoutes)
            {
                this.Kartpoint = Kartpoint;
                this.EnemyRoutes = EnemyRoutes;
                this.ItemRoutes = ItemRoutes;
                this.Checkpoint = Checkpoint;
                this.GameObject = GameObject;
                this.Routes = Routes;
                this.Area = Area;
                this.Camera = Camera;
                this.Returnpoints = Returnpoints;
                this.GlideRoutes = GlideRoutes;
            }

            public KMPSectionVisibility() { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="render"></param>
        /// <param name="KMPSectionVisibility"></param>
        /// <param name="KMP_Main_PGS"></param>
        /// <param name="KMPViewportObject"></param>
        public static void CheckKMPVisibility(UserControl1 render, KMPSectionVisibility KMPSectionVisibility, KMP_Main_PGS KMP_Main_PGS, KMPViewportObject KMPViewportObject)
        {
            #region Visibility
            foreach (var TPTKValue in KMP_Main_PGS.TPTK_Section.TPTKValueList)
            {
                TPTKValue.IsViewportVisible = KMPSectionVisibility.Kartpoint;
                ViewportObj_Visibility(KMPSectionVisibility.Kartpoint, render, KMPViewportObject.StartPosition_MV3DList[TPTKValue.ID]);
            }
            foreach (var HPNEValue in KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList)
            {
                HPNEValue.IsViewportVisible = KMPSectionVisibility.EnemyRoutes;
                ViewportObj_Visibility(KMPSectionVisibility.EnemyRoutes, render, KMPViewportObject.EnemyRoute_Rail_List[HPNEValue.GroupID]);
            }
            foreach (var HPTIValue in KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList)
            {
                HPTIValue.IsViewportVisible = KMPSectionVisibility.ItemRoutes;
                ViewportObj_Visibility(KMPSectionVisibility.ItemRoutes, render, KMPViewportObject.ItemRoute_Rail_List[HPTIValue.GroupID]);
            }
            foreach (var HPKCValue in KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList)
            {
                HPKCValue.IsViewportVisible = KMPSectionVisibility.Checkpoint;
                ViewportObj_Visibility(KMPSectionVisibility.Checkpoint, render, KMPViewportObject.Checkpoint_Rail[HPKCValue.GroupID]);
            }
            foreach (var JBOGValue in KMP_Main_PGS.JBOG_Section.JBOGValueList)
            {
                JBOGValue.IsViewportVisible = KMPSectionVisibility.GameObject;
                ViewportObj_Visibility(KMPSectionVisibility.GameObject, render, KMPViewportObject.GameObject_MV3DList[JBOGValue.ID]);
            }
            foreach (var ITOPRouteValue in KMP_Main_PGS.ITOP_Section.ITOP_RouteList)
            {
                ITOPRouteValue.IsViewportVisible = KMPSectionVisibility.Routes;
                ViewportObj_Visibility(KMPSectionVisibility.Routes, render, KMPViewportObject.Routes_List[ITOPRouteValue.GroupID]);
            }
            foreach (var AERAValue in KMP_Main_PGS.AERA_Section.AERAValueList)
            {
                AERAValue.IsViewportVisible = KMPSectionVisibility.Area;
                ViewportObj_Visibility(KMPSectionVisibility.Area, render, KMPViewportObject.Area_MV3DList[AERAValue.ID]);
            }
            foreach (var EMACValue in KMP_Main_PGS.EMAC_Section.EMACValueList)
            {
                EMACValue.IsViewportVisible = KMPSectionVisibility.Camera;
                ViewportObj_Visibility(KMPSectionVisibility.Camera, render, KMPViewportObject.Camera_MV3DList[EMACValue.ID]);
            }
            foreach (var TPGJValue in KMP_Main_PGS.TPGJ_Section.TPGJValueList)
            {
                TPGJValue.IsViewportVisible = KMPSectionVisibility.Returnpoints;
                ViewportObj_Visibility(KMPSectionVisibility.Returnpoints, render, KMPViewportObject.RespawnPoint_MV3DList[TPGJValue.ID]);
            }
            foreach (var HPLGValue in KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList)
            {
                HPLGValue.IsViewportVisible = KMPSectionVisibility.GlideRoutes;
                ViewportObj_Visibility(KMPSectionVisibility.GlideRoutes, render, KMPViewportObject.GlideRoute_Rail_List[HPLGValue.GroupID]);
            }
            #endregion
        }
    }
}
