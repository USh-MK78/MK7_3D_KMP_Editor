using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.IO;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Forms.Design;
using System.Collections;
using System.Numerics;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using System.Xml.Serialization;
using System.Reflection;
using Color = System.Windows.Media.Color;

using KMPLibrary.Format;
using KMPLibrary.KMPHelper;
using KMPLibrary.XMLConvert.XXXXRouteData;
using MK7_3D_KMP_Editor.Render;
using MK7_3D_KMP_Editor.PropertyGridObject;
using KMPLibrary.XMLConvert.IO;

namespace MK7_3D_KMP_Editor
{
    public partial class Form1 : Form
    {
        //UserControl1.xamlの初期化
        //ここは作成時の名前にも影響されるので必ず確認すること
        public UserControl1 render = new UserControl1();

        #region MV3DList
        KMPRendering.KMPViewportObject KMPViewportObject = new KMPRendering.KMPViewportObject();
        public Dictionary<string, ArrayList> CourseModel_Dictionary = new Dictionary<string, ArrayList>();
        #endregion

        public string FormTitle { get; set; } = "MK7 3D KMP Editor - U5h_MK78";
        public string FilePath { get; set; }
        public EditorSettings.EditorSettingXML EditorSetting { get; set; }

        public KMP KMPData { get; set; }
        public KMP_Main_PGS KMP_Main_PGS { get; set; }
        public ViewPortObjVisibleSetting.KMPSectionVisibility KMPSection_Visibility { get; set; } = ViewPortObjVisibleSetting.KMPSectionVisibility.DefaultSetting();

        #region Property
        /// <summary>
        /// Delete item by specifying index
        /// </summary>
        /// <typeparam name="T">T = BYAMLPropertyGridSettings.XXXX</typeparam>
        /// <param name="InputList">List<T></param>
        /// <param name="MV3DList">List<ModelVisual3D></param>
        /// <param name="Input_ListBox">Specifying a ListBox</param>
        /// <param name="Index">Index</param>
        public void DeleteItem<T>(List<T> InputList, List<ModelVisual3D> MV3DList, int Index)
        {
            InputList.RemoveAt(Index);
            render.RemoveItem(MV3DList[Index]);
            MV3DList.RemoveAt(Index);
        }

        /// <summary>
        /// Get property
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="PropertyName">PropertyName</param>
        /// <returns>object</returns>
        public object GetProperty(object obj, string PropertyName)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(PropertyName);
            object GetValue = propertyInfo.GetValue(obj);
            return GetValue;
        }

        /// <summary>
        /// Set property
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="InputValue">Value to be entered</param>
        /// <param name="PropertyName">PropertyName</param>
        public void SetProperty(object obj, object InputValue, string PropertyName)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(PropertyName);
            propertyInfo.SetValue(obj, InputValue);
        }

        /// <summary>
        /// Modify the ID of the specified List<T>.
        /// </summary>
        /// <typeparam name="T">T = BYAMLPropertyGridSettings.XXXX</typeparam>
        /// <param name="Input">List<T></param>
        /// <paramref name="PropertyName"/>PropertyName</param>
        public void ReInputID<T>(List<T> Input, string PropertyName)
        {
            //再度IDを入れる
            for (int ReCountNum = 0; ReCountNum < Input.Count; ReCountNum++)
            {
                SetProperty(Input[ReCountNum], ReCountNum, PropertyName);
            }
        }

        /// <summary>
        /// Modify the ID of the specified List<ModelVisual3D>.
        /// </summary>
        /// <param name="Input">List<ModelVisual3D></param>
        /// <param name="InputName">ModelName</param>
        public void ReInputModelID(List<ModelVisual3D> Input, int ReCountGrpNum, string InputName)
        {
            //再度IDを入れる
            for (int ReCountPtNum = 0; ReCountPtNum < Input.Count; ReCountPtNum++)
            {
                //モデルの名前と番号を文字列として格納(情報化)
                Input[ReCountPtNum].SetName(InputName + " " + ReCountPtNum + " " + ReCountGrpNum);
            }
        }

        /// <summary>
        /// Modify the ID of the specified List<HTK_3DES.PathTools.Rail>.
        /// </summary>
        /// <param name="InputRail">List<HTK_3DES.PathTools.Rail></param>
        /// <param name="InputName">ModelName</param>
        public void ReInputModelID(List<HTK_3DES.PathTools.Rail> InputRail, string InputName)
        {
            //再度IDを入れる
            for (int ReCountGrpNum = 0; ReCountGrpNum < InputRail.Count; ReCountGrpNum++)
            {
                HTK_3DES.PathTools.Rail rail = InputRail[ReCountGrpNum];

                for (int ReCountPtNum = 0; ReCountPtNum < rail.BasePointModelList.Count; ReCountPtNum++)
                {
                    rail.BasePointModelList[ReCountPtNum].SetName(InputName + " " + ReCountPtNum + " " + ReCountGrpNum);
                }
            }
        }

        /// <summary>
        /// Modify the ID of the specified List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint>
        /// </summary>
        /// <param name="InputRailChk">List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint></param>
        /// <param name="InputName_Left">Name</param>
        /// <param name="InputName_Right">Name</param>
        public void ReInputModelID(List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint> InputRailChk, string InputName_Left, string InputName_Right)
        {
            //再度IDを入れる
            for (int ReCountGrpNum = 0; ReCountGrpNum < InputRailChk.Count; ReCountGrpNum++)
            {
                HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = InputRailChk[ReCountGrpNum];

                for (int ChkLeftCount = 0; ChkLeftCount < checkpoint.Checkpoint_Left.BasePointModelList.Count; ChkLeftCount++)
                {
                    checkpoint.Checkpoint_Left.BasePointModelList[ChkLeftCount].SetName(InputName_Left + " " + ChkLeftCount + " " + ReCountGrpNum);
                }

                for (int ChkRightCount = 0; ChkRightCount < checkpoint.Checkpoint_Right.BasePointModelList.Count; ChkRightCount++)
                {
                    checkpoint.Checkpoint_Right.BasePointModelList[ChkRightCount].SetName(InputName_Right + " " + ChkRightCount + " " + ReCountGrpNum);
                }
            }
        }

        /// <summary>
        /// Modify the ID of the specified List<ModelVisual3D>.
        /// </summary>
        /// <param name="Input">List<ModelVisual3D></param>
        /// <param name="InputName">ModelName</param>
        public void ReInputModelID(List<ModelVisual3D> Input, string InputName)
        {
            //再度IDを入れる
            for (int ReCountNum = 0; ReCountNum < Input.Count; ReCountNum++)
            {
                Input[ReCountNum].SetName(InputName + " " + ReCountNum + " " + -1);
            }
        }

        /// <summary>
        /// Redraw the ListBox
        /// </summary>
        /// <typeparam name="T">T = BYAMLPropertyGridSettings.XXXX</typeparam>
        /// <param name="InputList">List<T></param>
        /// <param name="Input_ListBox">Specifying the ListBox to be redrawn</param>
        public void UpdateListBox<T>(List<T> InputList, System.Windows.Forms.ListBox Input_ListBox)
        {
            //ListBoxの再描画
            Input_ListBox.Items.Clear();
            Input_ListBox.Items.AddRange(InputList.Cast<object>().ToArray());
        }
        #endregion

        public void SectionVisibleCheck(bool b)
        {
            CH_Kartpoint.Checked = b;
            CH_EnemyRoutes.Checked = b;
            CH_ItemRoutes.Checked = b;
            CH_Checkpoint.Checked = b;
            CH_GameObject.Checked = b;
            CH_Routes.Checked = b;
            CH_Area.Checked = b;
            CH_Camera.Checked = b;
            CH_Returnpoints.Checked = b;
            CH_GlideRoutes.Checked = b;
        }

        public Form1()
        {
            InitializeComponent();

            //Multi Select
            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            KMP_Point_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

            render.MainViewPort.CalculateCursorPosition = true;

            render.MouseLeftButtonDown += Render_MouseLeftButtonDown;
            render.MouseMove += Render_MouseMove;

            //ElementHost1のKey
            KeyPreview = true;
            elementHost1.Child = render;
            elementHost1.Child.KeyDown += render_KeyDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SectionVisibleCheck(true);
            KMPSection_Visibility = ViewPortObjVisibleSetting.KMPSectionVisibility.DefaultSetting();

            //Setting
            EditorSetting = new EditorSettings.EditorSettingXML();
            this.Text = FormTitle;

            //Panelの固定および非表示
            KMP_Viewport_SplitContainer.FixedPanel = FixedPanel.Panel2;
            KMP_Viewport_SplitContainer.Panel2Collapsed = true;
            KMP_Viewport_SplitContainer.IsSplitterFixed = true;
            KMP_Viewport_SplitContainer.Panel2.Hide();
            
            writeBinaryToolStripMenuItem.Enabled = false;
            closeKMPToolStripMenuItem.Enabled = false;
            exportToolStripMenuItem.Enabled = false;
            closeObjToolStripMenuItem.Enabled = false;
            visibilityToolStripMenuItem.Enabled = false;
            xXXXRouteExporterToolStripMenuItem.Enabled = false;
            xXXXRouteImporterToolStripMenuItem.Enabled = false;
            inputXmlAsXXXXToolStripMenuItem.Enabled = false;

            string CD = System.IO.Directory.GetCurrentDirectory();
            if (Directory.Exists(CD + "\\Settings") == false)
            {
                Directory.CreateDirectory(CD + "\\Settings");
                XML_Exporter.XMLExport(CD + "\\Settings\\EditorSetting.xml", EditorSetting, XML_Exporter.EmptyXmlSerializerNamespaces());
            }
            else if ((Directory.Exists(CD + "\\Settings") == true && File.Exists(CD + "\\Settings\\EditorSetting.xml") == false) == true)
            {
                XML_Exporter.XMLExport(CD + "\\Settings\\EditorSetting.xml", EditorSetting, XML_Exporter.EmptyXmlSerializerNamespaces());
            }

            if (Directory.Exists(CD + "\\KMP_OBJ") == false)
            {
                var KMPOBJ = Properties.Resources.KMP_OBJ;
                FileStream fs1 = new FileStream("KMP_OBJ.zip", FileMode.Create, FileAccess.Write);
                fs1.Write(KMPOBJ, 0, KMPOBJ.Length);
                fs1.Close();
                fs1.Dispose();
                System.IO.Compression.ZipFile.ExtractToDirectory("KMP_OBJ.zip", CD);
            }

            if (Directory.Exists(CD + "\\KMPObjectFlow") == false)
            {
                var ObjFlowModel = Properties.Resources.KMPObjectFlow;
                FileStream fs1 = new FileStream("KMPObjectFlow.zip", FileMode.Create, FileAccess.Write);
                fs1.Write(ObjFlowModel, 0, ObjFlowModel.Length);
                fs1.Close();
                fs1.Dispose();
                System.IO.Compression.ZipFile.ExtractToDirectory("KMPObjectFlow.zip", CD);
            }
        }

        #region Import OBJ
        private void readObjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Open_ModelObject = new OpenFileDialog()
            {
                Title = "Open Model",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "obj file|*.obj"
            };

            if (Open_ModelObject.ShowDialog() != DialogResult.OK) return;

            CourseModel_Dictionary = HTK_3DES.OBJData.OBJReader_AryListDictionary(Open_ModelObject.FileName);

            foreach(var item in CourseModel_Dictionary)
            {
                render.MainViewPort.Children.Add((ModelVisual3D)CourseModel_Dictionary[item.Key][1]);
            }

            closeObjToolStripMenuItem.Enabled = true;
            visibilityToolStripMenuItem.Enabled = true;
        }

        private void visibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelVisibilityForm modelVisibilityForm = new ModelVisibilityForm();
            modelVisibilityForm.Show();
        }

        private void closeObjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Close Course Model
            foreach(var item in CourseModel_Dictionary)
            {
                render.MainViewPort.Children.Remove((ModelVisual3D)item.Value[1]);
            }

            CourseModel_Dictionary.Clear();

            closeObjToolStripMenuItem.Enabled = false;
            visibilityToolStripMenuItem.Enabled = false;
        }
        #endregion

        HitTestResult HTR = null;
        ModelVisual3D FindMV3D = null;
        HTK_3DES.Transform transform_Value = null;

        private void Render_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //MainViewPortのマウス座標を取得
            var Vp_MousePos2D = e.GetPosition(this.render);

            if (Keyboard.IsKeyDown(Key.LeftCtrl) == true)
            {
                //マウス座標からヒットテストを実行
                HitTestResult HTRs = VisualTreeHelper.HitTest(render.MainViewPort, Vp_MousePos2D);
                HTR = HTRs as RayMeshGeometry3DHitTestResult;
                if (HTR != null)
                {
                    string[] MDLStr_GetName = null;
                    if (typeof(ModelVisual3D) == HTR.VisualHit.GetType())
                    {
                        //ダウンキャスト
                        FindMV3D = (ModelVisual3D)HTR.VisualHit;
                        MDLStr_GetName = HTR.VisualHit.GetName().Split(' ');
                    }
                    else if (typeof(LinesVisual3D) == HTR.VisualHit.GetType()) return;
                    else if (typeof(TubeVisual3D) == HTR.VisualHit.GetType()) return;
                    else if (typeof(RectangleVisual3D) == HTR.VisualHit.GetType()) return;

                    ////ダウンキャスト
                    //FindMV3D = (ModelVisual3D)HTR.VisualHit;
                    //string[] MDLStr_GetName = HTR.VisualHit.GetName().Split(' ');

                    #region Get Object info
                    string OBJ_Name = MDLStr_GetName[0];
                    int MDLNum = int.Parse(MDLStr_GetName[1]);
                    int GroupNum = int.Parse(MDLStr_GetName[2]);
                    #endregion

                    if (OBJ_Name == "StartPosition")
                    {
                        //Dictionaryに存在するKeyを検索
                        if (KMPViewportObject.StartPosition_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("KartPoint");

                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.TPTK_Section.TPTKValueList[MDLNum].Position_Value.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = KMP_Main_PGS.TPTK_Section.TPTKValueList[MDLNum].Rotate_Value.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "EnemyRoute")
                    {
                        //Dictionaryに存在するKeyを検索
                        if (KMPViewportObject.EnemyRoute_Rail_List[GroupNum].BasePointModelList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("EnemyRoutes");

                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;

                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum].Control, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "ItemRoute")
                    {
                        if (KMPViewportObject.ItemRoute_Rail_List[GroupNum].BasePointModelList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("ItemRoutes");

                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum].TPTI_Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum].TPTI_PointSize, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "Checkpoint_Left")
                    {
                        if (KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_Left.BasePointModelList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("CheckPoint");

                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            var P2DLeft = KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Left;

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = new Vector3D(P2DLeft.X, Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text), P2DLeft.Y),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "Checkpoint_Right")
                    {
                        if (KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_Right.BasePointModelList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("CheckPoint");

                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;

                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            var P2DRight = KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Right;

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = new Vector3D(P2DRight.X, Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text), P2DRight.Y),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "GameObject")
                    {
                        if (KMPViewportObject.GameObject_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("Object");

                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.JBOG_Section.JBOGValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(KMP_Main_PGS.JBOG_Section.JBOGValueList[MDLNum].Scales.GetVector3D(), 2),
                                Rotate3D = KMP_Main_PGS.JBOG_Section.JBOGValueList[MDLNum].Rotations.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "Routes")
                    {
                        if (KMPViewportObject.Routes_List[GroupNum].BasePointModelList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("Route");

                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.ITOP_Section.ITOP_RouteList[GroupNum].ITOP_PointList[MDLNum].Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "Area")
                    {
                        if (KMPViewportObject.Area_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("Area");
                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.AERA_Section.AERAValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(KMP_Main_PGS.AERA_Section.AERAValueList[MDLNum].Scales.GetVector3D(), 2000),
                                Rotate3D = KMP_Main_PGS.AERA_Section.AERAValueList[MDLNum].Rotations.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "Camera")
                    {
                        if (KMPViewportObject.Camera_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("Camera");
                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.EMAC_Section.EMACValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = KMP_Main_PGS.EMAC_Section.EMACValueList[MDLNum].Rotations.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "RespawnPoint")
                    {
                        if (KMPViewportObject.RespawnPoint_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("JugemPoint");
                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.TPGJ_Section.TPGJValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = KMP_Main_PGS.TPGJ_Section.TPGJValueList[MDLNum].Rotations.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (OBJ_Name == "GlideRoutes")
                    {
                        if (KMPViewportObject.GlideRoute_Rail_List[GroupNum].BasePointModelList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("GlideRoutes");

                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.One;
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Group_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;

                            KMP_Point_ListBox.SelectedIndex = -1;
                            KMP_Point_ListBox.SelectedIndex = MDLNum;
                            KMPSection_Main_TabCtrl.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum].TPLG_PointScaleValue, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    else if (CourseModel_Dictionary.ContainsKey(OBJ_Name))
                    {
                        //出力
                        Section_Name_LBL.Text = MDLStr_GetName[0];
                        Section_ID_LBL.Text = MDLStr_GetName[1] + ": None";
                        Section_Group_ID_LBL.Text = MDLStr_GetName[2] + ": None";
                    }
                }
                else if (HTR == null)
                {
                    Section_Name_LBL.Text = "Not Selected.";
                    Section_ID_LBL.Text = "<!>Null<!>";
                    Section_Group_ID_LBL.Text = "<!>Null<!>";
                }
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) == true)
            {
                Point3D Pos = new Point3D();
                if (OnElementPos_RadioBtn.Checked == true) Pos = render.ViewportPosition(UserControl1.PositionMode.ElementPosition);
                else if (MouseCursor_RadioBtn.Checked == true) Pos = render.ViewportPosition(UserControl1.PositionMode.MouseCursor);
                else if (CameraPosition_RadioBtn.Checked == true) Pos = render.ViewportPosition(UserControl1.PositionMode.CameraPos);

                if (KMPSectionComboBox.Text == "KartPoint")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    KartPoint_PGS.TPTKValue TPTKValue = new KartPoint_PGS.TPTKValue(Pos.ToVector3D(), Index);
                    KMP_Main_PGS.TPTK_Section.TPTKValueList.Insert(Index, TPTKValue);

                    //Update ID
                    ReInputID(KMP_Main_PGS.TPTK_Section.TPTKValueList, "ID");

                    if (KMP_Main_PGS.TPTK_Section.TPTKValueList.Count != 0)
                    {
                        #region Add Model(StartPosition)
                        ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                        HTK_3DES.Transform StartPosition_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = TPTKValue.Position_Value.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = TPTKValue.Rotate_Value.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_StartPositionOBJ, StartPosition_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.StartPosition_MV3DList);
                        KMPViewportObject.StartPosition_MV3DList.Insert(Index, dv3D_StartPositionOBJ);
                        ReInputModelID(KMPViewportObject.StartPosition_MV3DList, -1, "StartPosition");
                        render.VpItemAddRange(KMPViewportObject.StartPosition_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.TPTK_Section.TPTKValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        EnemyRoute_PGS.HPNEValue.TPNEValue TPNEValue = new EnemyRoute_PGS.HPNEValue.TPNEValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.Insert(Index, TPNEValue);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[ReInputCount].TPNEValueList, "ID");
                        }

                        if (KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.Count != 0)
                        {
                            #region Add Model(EnemyRoutes)
                            ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                            HTK_3DES.Transform EnemyPoint_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = TPNEValue.Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(TPNEValue.Control, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                            tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add Rail => MV3DList
                            render.VpItemDeleteRange(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList.Insert(Index, dv3D_EnemyPathOBJ);
                            ReInputModelID(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList, TPNEValue.Group_ID, "EnemyRoute");
                            render.VpItemAddRange(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            #endregion

                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Orange);
                        }

                        UpdateListBox(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                else if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        ItemRoute_PGS.HPTIValue.TPTIValue TPTIValue = new ItemRoute_PGS.HPTIValue.TPTIValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.Insert(Index, TPTIValue);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[ReInputCount].TPTIValueList, "ID");
                        }

                        if (KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.Count != 0)
                        {
                            #region Add Model(ItemRoutes)
                            ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                            HTK_3DES.Transform ItemPoint_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = TPTIValue.TPTI_Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(TPTIValue.TPTI_PointSize, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                            tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add Rail => MV3DList
                            render.VpItemDeleteRange(KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList.Insert(Index, dv3D_ItemPathOBJ);
                            ReInputModelID(KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList, TPTIValue.Group_ID, "ItemRoute");
                            render.VpItemAddRange(KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            #endregion

                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Green);
                        }

                        UpdateListBox(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                else if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        Vector2 LeftPos = new Vector2(Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Z));
                        Vector2 RightPos = new Vector2(Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Z));

                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        Checkpoint_PGS.HPKCValue.TPKCValue TPKCValue = new Checkpoint_PGS.HPKCValue.TPKCValue(LeftPos, RightPos, KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.Insert(Index, TPKCValue);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[ReInputCount].TPKCValue_List, "ID");
                        }

                        if (KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.Count != 0)
                        {
                            #region Create
                            var P2D_Left = TPKCValue.Position_2D_Left;
                            Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                            Point3D P3DLeft = Converter2D.Vector2DTo3D(P2DLeftToVector2, Converter2D.Axis_Up.Y).ToPoint3D();
                            P3DLeft.Y = Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text);

                            #region Transform(Left)
                            ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                            HTK_3DES.Transform P2DLeft_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = P3DLeft.ToVector3D(),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D_P2DLeft = new HTK_3DES.TSRSystem3D(dv3D_CheckpointLeftOBJ, P2DLeft_transform_Value);
                            tSRSystem3D_P2DLeft.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add
                            render.VpItemDeleteRange(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList.Insert(Index, dv3D_CheckpointLeftOBJ);
                            ReInputModelID(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList, TPKCValue.Group_ID, "Checkpoint_Left");
                            render.VpItemAddRange(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList);
                            #endregion

                            var P2D_Right = TPKCValue.Position_2D_Right;
                            Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                            Point3D P3DRight = Converter2D.Vector2DTo3D(P2DRightToVector2, Converter2D.Axis_Up.Y).ToPoint3D();
                            P3DRight.Y = Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text);

                            #region Transform(Right)
                            ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                            HTK_3DES.Transform P2DRight_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = P3DRight.ToVector3D(),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D_P2DRight = new HTK_3DES.TSRSystem3D(dv3D_CheckpointRightOBJ, P2DRight_transform_Value);
                            tSRSystem3D_P2DRight.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add
                            render.VpItemDeleteRange(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList.Insert(Index, dv3D_CheckpointRightOBJ);
                            ReInputModelID(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList, TPKCValue.Group_ID, "Checkpoint_Right");
                            render.VpItemAddRange(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList);
                            #endregion

                            List<Point3D> point3Ds = new List<Point3D>();
                            point3Ds.Add(P3DLeft);
                            point3Ds.Add(P3DRight);

                            LinesVisual3D linesVisual3D = new LinesVisual3D { Points = new Point3DCollection(point3Ds), Thickness = 1, Color = Colors.Black };

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line.Insert(Index, linesVisual3D);
                            render.MainViewPort.Children.Insert(Index, linesVisual3D);

                            #region SplitWall
                            Point3DCollection point3Ds1 = new Point3DCollection();
                            point3Ds1.Add(new Point3D(point3Ds[1].X, 0, point3Ds[1].Z));
                            point3Ds1.Add(point3Ds[1]);
                            point3Ds1.Add(new Point3D(point3Ds[0].X, 0, point3Ds[0].Z));
                            point3Ds1.Add(point3Ds[0]);

                            ModelVisual3D SplitWall = HTK_3DES.CustomModelCreateHelper.CustomRectanglePlane3D(point3Ds1, System.Windows.Media.Color.FromArgb(0xA0, 0xA0, 0x00, 0xA0), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                            HTK_3DES.SetString_MV3D(SplitWall, "SplitWall -1 -1");
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL.Insert(Index, SplitWall);
                            render.MainViewPort.Children.Insert(Index, SplitWall);
                            #endregion
                            #endregion

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Line);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.DrawPath_Line(render, 5, Colors.Green);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Line);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.DrawPath_Line(render, 5, Colors.Red);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].ResetSideWall(render);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].DrawPath_SideWall(render, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                        }

                        UpdateListBox(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValue_List, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                else if (KMPSectionComboBox.Text == "Object")
                {
                    AddKMPObjectForm addKMPObjectForm = new AddKMPObjectForm();
                    addKMPObjectForm.ShowDialog();
                    var data = addKMPObjectForm.SelectedKMPObject_Info;

                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    KMPObject_PGS.JBOGValue JBOGValue = new KMPObject_PGS.JBOGValue(data.Name, data.ObjID, Pos.ToVector3D(), Index);
                    KMP_Main_PGS.JBOG_Section.JBOGValueList.Insert(Index, JBOGValue);

                    ReInputID(KMP_Main_PGS.JBOG_Section.JBOGValueList, "ID");

                    if (KMP_Main_PGS.JBOG_Section.JBOGValueList.Count != 0)
                    {
                        #region Add Model(OBJ)
                        List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDataXml_List = KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml").ObjFlows;
                        string Path = ObjFlowDataXml_List.Find(x => x.ObjectID == data.ObjID).Path;
                        ModelVisual3D dv3D_OBJ = HTK_3DES.OBJReader(Path);

                        HTK_3DES.Transform OBJ_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = JBOGValue.Positions.GetVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(JBOGValue.Scales.GetVector3D(), 2),
                            Rotate3D = JBOGValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.GameObject_MV3DList);
                        KMPViewportObject.GameObject_MV3DList.Insert(Index, dv3D_OBJ);
                        ReInputModelID(KMPViewportObject.GameObject_MV3DList, -1, "GameObject"); 
                        render.VpItemAddRange(KMPViewportObject.GameObject_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.JBOG_Section.JBOGValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "Route")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        Route_PGS.ITOP_Route.ITOP_Point ITOP_Point = new Route_PGS.ITOP_Route.ITOP_Point(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.Insert(Index, ITOP_Point);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.ITOP_Section.ITOP_RouteList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[ReInputCount].ITOP_PointList, "ID");
                        }

                        if (KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.Count != 0)
                        {
                            #region Add Model(Routes)
                            ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                            HTK_3DES.Transform JugemPath_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = ITOP_Point.Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_RouteOBJ, JugemPath_transform_Value);
                            tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add Rail => MV3DList
                            render.VpItemDeleteRange(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList.Insert(Index, dv3D_RouteOBJ);
                            ReInputModelID(KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList, ITOP_Point.GroupID, "Routes");
                            render.VpItemAddRange(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            #endregion

                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Blue);
                        }

                        UpdateListBox(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                else if (KMPSectionComboBox.Text == "Area")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    Area_PGS.AERAValue AERAValue = new Area_PGS.AERAValue(Pos.ToVector3D(), Index);
                    KMP_Main_PGS.AERA_Section.AERAValueList.Insert(Index, AERAValue);

                    ReInputID(KMP_Main_PGS.AERA_Section.AERAValueList, "ID");

                    if (KMP_Main_PGS.AERA_Section.AERAValueList.Count != 0)
                    {
                        #region Add Model(Area)
                        ModelVisual3D dv3D_AreaOBJ = null;
                        if (AERAValue.AreaModeSettings.AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                        HTK_3DES.Transform Area_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = AERAValue.Positions.GetVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(AERAValue.Scales.GetVector3D(), 2000),
                            Rotate3D = AERAValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.Area_MV3DList);
                        KMPViewportObject.Area_MV3DList.Insert(Index, dv3D_AreaOBJ);
                        ReInputModelID(KMPViewportObject.Area_MV3DList, -1, "Area");
                        render.VpItemAddRange(KMPViewportObject.Area_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.AERA_Section.AERAValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "Camera")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    Camera_PGS.EMACValue EMACValue = new Camera_PGS.EMACValue(Pos.ToVector3D(), Index);
                    KMP_Main_PGS.EMAC_Section.EMACValueList.Insert(Index, EMACValue);

                    ReInputID(KMP_Main_PGS.EMAC_Section.EMACValueList, "ID");

                    if (KMP_Main_PGS.EMAC_Section.EMACValueList.Count != 0)
                    {
                        #region Add Model(Camera)
                        ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                        HTK_3DES.Transform Camera_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = EMACValue.Positions.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = EMACValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_CameraOBJ, Camera_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.Camera_MV3DList);
                        KMPViewportObject.Camera_MV3DList.Insert(Index, dv3D_CameraOBJ);
                        ReInputModelID(KMPViewportObject.Camera_MV3DList, -1, "Camera");
                        render.VpItemAddRange(KMPViewportObject.Camera_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.EMAC_Section.EMACValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "JugemPoint")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    RespawnPoint_PGS.TPGJValue TPGJValue = new RespawnPoint_PGS.TPGJValue(Pos.ToVector3D(), Index);
                    KMP_Main_PGS.TPGJ_Section.TPGJValueList.Insert(Index, TPGJValue);

                    ReInputID(KMP_Main_PGS.TPGJ_Section.TPGJValueList, "ID");

                    if (KMP_Main_PGS.TPGJ_Section.TPGJValueList.Count != 0)
                    {
                        #region Add Model(RespawnPoint)
                        ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                        HTK_3DES.Transform RespawnPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = TPGJValue.Positions.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = TPGJValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_RespawnPointOBJ, RespawnPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.RespawnPoint_MV3DList);
                        KMPViewportObject.RespawnPoint_MV3DList.Insert(Index, dv3D_RespawnPointOBJ);
                        ReInputModelID(KMPViewportObject.RespawnPoint_MV3DList, -1, "RespawnPoint");
                        render.VpItemAddRange(KMPViewportObject.RespawnPoint_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.TPGJ_Section.TPGJValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        GlideRoute_PGS.HPLGValue.TPLGValue TPLGValue = new GlideRoute_PGS.HPLGValue.TPLGValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.Insert(Index, TPLGValue);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[ReInputCount].TPLGValueList, "ID");
                        }

                        if (KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.Count != 0)
                        {
                            #region Add Model(GlideRoutes)
                            ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                            HTK_3DES.Transform GliderPoint_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = TPLGValue.Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(TPLGValue.TPLG_PointScaleValue, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                            tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add Rail => MV3DList
                            render.VpItemDeleteRange(KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList.Insert(Index, dv3D_GliderPathOBJ);
                            ReInputModelID(KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList, TPLGValue.GroupID, "GlideRoutes");
                            render.VpItemAddRange(KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            #endregion

                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.LightSkyBlue);
                        }

                        UpdateListBox(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
            }
        }

        public void Render_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) == true && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (HTR != null)
                {
                    string[] MDLStr_GetName = null;
                    if (typeof(ModelVisual3D) == HTR.VisualHit.GetType())
                    {
                        MDLStr_GetName = HTR.VisualHit.GetName().Split(' ');
                    }
                    else if (typeof(LinesVisual3D) == HTR.VisualHit.GetType()) return;
                    else if (typeof(TubeVisual3D) == HTR.VisualHit.GetType()) return;
                    else if (typeof(RectangleVisual3D) == HTR.VisualHit.GetType()) return;

                    //string[] MDLStr_GetName = HTR.VisualHit.GetName().Split(' ');

                    #region Get Object info
                    string OBJ_Name = MDLStr_GetName[0];
                    int MDLNum = int.Parse(MDLStr_GetName[1]);
                    int GroupNum = int.Parse(MDLStr_GetName[2]);
                    #endregion

                    if (OBJ_Name == "StartPosition")
                    {
                        //位置を計算
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.TPTK_Section.TPTKValueList[MDLNum].Position_Value = new KartPoint_PGS.TPTKValue.Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.TPTK_Section.TPTKValueList[MDLNum];
                    }
                    else if (OBJ_Name == "EnemyRoute")
                    {
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum].Positions = new EnemyRoute_PGS.HPNEValue.TPNEValue.Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.EnemyRoute_Rail_List[GroupNum];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum];
                    }
                    else if (OBJ_Name == "ItemRoute")
                    {
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum].TPTI_Positions = new ItemRoute_PGS.HPTIValue.TPTIValue.TPTI_Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.ItemRoute_Rail_List[GroupNum];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum];
                    }
                    else if (OBJ_Name == "Checkpoint_Left")
                    {
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Left.X = (float)transform_Value.Translate3D.X;
                        KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Left.Y = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //パスの形を変更(機能の追加)
                        HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = KMPViewportObject.Checkpoint_Rail[GroupNum];

                        //Green
                        if (checkpoint.Checkpoint_Left.LV3D_List.Count != 0) checkpoint.Checkpoint_Left.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Line);
                        KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_Line[MDLNum].Points[0] = transform_Value.Translate3D.ToPoint3D();

                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_SplitWallMDL[MDLNum].Content).Positions[2] = new Point3D(transform_Value.Translate3D.X, 0, transform_Value.Translate3D.Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_SplitWallMDL[MDLNum].Content).Positions[3] = transform_Value.Translate3D.ToPoint3D();

                        if (checkpoint.SideWall_Left.Count != 0) checkpoint.MoveSideWalls(MDLNum, transform_Value.Translate3D, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint.SideWallType.Left);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum];
                    }
                    else if (OBJ_Name == "Checkpoint_Right")
                    {
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Right.X = (float)transform_Value.Translate3D.X;
                        KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Right.Y = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //パスの形を変更(機能の追加)
                        HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = KMPViewportObject.Checkpoint_Rail[GroupNum];

                        //Red
                        if (checkpoint.Checkpoint_Right.LV3D_List.Count != 0) checkpoint.Checkpoint_Right.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Line);
                        KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_Line[MDLNum].Points[1] = transform_Value.Translate3D.ToPoint3D();

                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_SplitWallMDL[MDLNum].Content).Positions[0] = new Point3D(transform_Value.Translate3D.X, 0, transform_Value.Translate3D.Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_SplitWallMDL[MDLNum].Content).Positions[1] = transform_Value.Translate3D.ToPoint3D();

                        if (checkpoint.SideWall_Right.Count != 0) checkpoint.MoveSideWalls(MDLNum, transform_Value.Translate3D, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint.SideWallType.Right);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum];
                    }
                    else if (OBJ_Name == "GameObject")
                    {
                        //位置を計算
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.JBOG_Section.JBOGValueList[MDLNum].Positions = new KMPObject_PGS.JBOGValue.Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.JBOG_Section.JBOGValueList[MDLNum];
                    }
                    else if (OBJ_Name == "Routes")
                    {
                        //位置を計算
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.ITOP_Section.ITOP_RouteList[GroupNum].ITOP_PointList[MDLNum].Positions = new Route_PGS.ITOP_Route.ITOP_Point.Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.Routes_List[GroupNum];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.ITOP_Section.ITOP_RouteList[GroupNum].ITOP_PointList[MDLNum];
                    }
                    else if (OBJ_Name == "Area")
                    {
                        //位置を計算
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.AERA_Section.AERAValueList[MDLNum].Positions = new Area_PGS.AERAValue.Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.AERA_Section.AERAValueList[MDLNum];
                    }
                    else if (OBJ_Name == "Camera")
                    {
                        //位置を計算
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.EMAC_Section.EMACValueList[MDLNum].Positions = new Camera_PGS.EMACValue.Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.EMAC_Section.EMACValueList[MDLNum];
                    }
                    else if (OBJ_Name == "RespawnPoint")
                    {
                        //位置を計算
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.TPGJ_Section.TPGJValueList[MDLNum].Positions = new RespawnPoint_PGS.TPGJValue.Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.TPGJ_Section.TPGJValueList[MDLNum];
                    }
                    else if (OBJ_Name == "GlideRoutes")
                    {
                        //位置を計算
                        Vector3D NewPos = render.Drag(transform_Value.Translate3D, e);

                        #region Moving Axis
                        //一度Transform_ValueのTranslate_Valueに計算した値を格納
                        if (Rad_AxisAll.Checked == true) transform_Value.Translate3D = NewPos;
                        if (Rad_AxisX.Checked == true) transform_Value.Translate3D = new Vector3D(NewPos.X, transform_Value.Translate3D.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisY.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, NewPos.Y, transform_Value.Translate3D.Z);
                        if (Rad_AxisZ.Checked == true) transform_Value.Translate3D = new Vector3D(transform_Value.Translate3D.X, transform_Value.Translate3D.Y, NewPos.Z);
                        #endregion

                        //Propertyに値を格納する
                        KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum].Positions = new GlideRoute_PGS.HPLGValue.TPLGValue.Position(transform_Value.Translate3D);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.GlideRoute_Rail_List[GroupNum];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum];
                    }
                    else if (OBJ_Name == "GridLine") return;
                }
            }
        }

        public void render_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Panel制御
            bool SBool = KMP_Viewport_SplitContainer.Panel2Collapsed;
            if (Keyboard.IsKeyDown(Key.T) == true)
            {
                if (SBool == true)
                {
                    KMP_Viewport_SplitContainer.Panel2Collapsed = false;
                    KMP_Viewport_SplitContainer.Panel2.Show();
                }
                if (SBool == false)
                {
                    KMP_Viewport_SplitContainer.Panel2Collapsed = true;
                    KMP_Viewport_SplitContainer.Panel2.Hide();
                }
            }
        }

        private void readBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Open_KMP = new OpenFileDialog()
            {
                Title = "Open KMP",
                FileName = EditorSetting.FilePathSetting.DefaultKMPFileName,
                InitialDirectory = EditorSetting.FilePathSetting.DefaultDirectory,
                Filter = "kmp file|*.kmp"
            };

            if (Open_KMP.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = new FileStream(Open_KMP.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                KMP KMPData = new KMP();
                KMPData.ReadKMP(br);

                br.Close();
                fs.Close();

                KMPVersion_TXT.Text = KMPData.VersionNumber.ToString();

                //Add PropertyGrid
                KMP_Main_PGS = new KMP_Main_PGS(KMPData);

                #region Render
                Render.KMPRendering.KMPViewportRendering.Render_StartPosition(render, KMPViewportObject, KMPData.KMP_Section.TPTK);
                Render.KMPRendering.KMPViewportRendering.Render_EnemyRoute(render, KMPViewportObject, KMPData.KMP_Section.HPNE, KMPData.KMP_Section.TPNE);
                Render.KMPRendering.KMPViewportRendering.Render_ItemRoute(render, KMPViewportObject, KMPData.KMP_Section.HPTI, KMPData.KMP_Section.TPTI);
                Render.KMPRendering.KMPViewportRendering.Render_Checkpoint(render, KMPViewportObject, KMPData.KMP_Section.HPKC, KMPData.KMP_Section.TPKC, Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text));
                Render.KMPRendering.KMPViewportRendering.Render_Object(render, KMPViewportObject, KMPData.KMP_Section.JBOG, KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml").ObjFlows);
                Render.KMPRendering.KMPViewportRendering.Render_Route(render, KMPViewportObject, KMPData.KMP_Section.ITOP);
                Render.KMPRendering.KMPViewportRendering.Render_Area(render, KMPViewportObject, KMPData.KMP_Section.AERA);
                Render.KMPRendering.KMPViewportRendering.Render_Camera(render, KMPViewportObject, KMPData.KMP_Section.EMAC);
                Render.KMPRendering.KMPViewportRendering.Render_Returnpoint(render, KMPViewportObject, KMPData.KMP_Section.TPGJ);
                Render.KMPRendering.KMPViewportRendering.Render_GlideRoute(render, KMPViewportObject, KMPData.KMP_Section.HPLG, KMPData.KMP_Section.TPLG);
                #endregion

                #region Visibility
                KMPSection_Visibility = new ViewPortObjVisibleSetting.KMPSectionVisibility
                {
                    Kartpoint = CH_Kartpoint.Checked,
                    EnemyRoutes = CH_EnemyRoutes.Checked,
                    ItemRoutes = CH_ItemRoutes.Checked,
                    Checkpoint = CH_Checkpoint.Checked,
                    GameObject = CH_GameObject.Checked,
                    Routes = CH_Routes.Checked,
                    Area = CH_Area.Checked,
                    Camera = CH_Camera.Checked,
                    Returnpoints = CH_Returnpoints.Checked,
                    GlideRoutes = CH_GlideRoutes.Checked
                };

                ViewPortObjVisibleSetting.CheckKMPVisibility(render, KMPSection_Visibility, KMP_Main_PGS, KMPViewportObject);
                #endregion

                if (KMPSectionComboBox.Items.Count == 0)
                {
                    string[] AllSectionAry = new string[] { "KartPoint", "EnemyRoutes", "ItemRoutes", "CheckPoint", "Object", "Route", "Area", "Camera", "JugemPoint", "GlideRoutes" };
                    KMPSectionComboBox.Items.AddRange(AllSectionAry.ToArray());
                }

                KMPSectionComboBox.SelectedIndex = 0;

                if (KMP_Main_PGS.IGTS_Section != null)
                {
                    //Display only IGTS section directly to PropertyGrid
                    propertyGrid_KMP_StageInfo.SelectedObject = KMP_Main_PGS.IGTS_Section;
                }

                writeBinaryToolStripMenuItem.Enabled = true;
                closeKMPToolStripMenuItem.Enabled = true;
                exportToolStripMenuItem.Enabled = true;
                inputXmlAsXXXXToolStripMenuItem.Enabled = true;

                //Path
                FilePath = Open_KMP.FileName;
                this.Text = FormTitle + " [File : " + FilePath + " ]";
            }
            else return;
        }

        private void writeBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save_KMP = new SaveFileDialog()
            {
                Title = "Save KMP",
                FileName = EditorSetting.FilePathSetting.DefaultKMPFileName,
                InitialDirectory = EditorSetting.FilePathSetting.DefaultDirectory,
                Filter = "kmp file|*.kmp"
            };

            if (Save_KMP.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = new FileStream(Save_KMP.FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);

                KMP.KMPSection KMPSection = new KMP.KMPSection
                {
                    TPTK = KMP_Main_PGS.TPTK_Section.ToTPTK(),
                    TPNE = KMP_Main_PGS.HPNE_TPNE_Section.ToHPNE_TPNEData().TPNE_Section,
                    HPNE = KMP_Main_PGS.HPNE_TPNE_Section.ToHPNE_TPNEData().HPNE_Section,
                    TPTI = KMP_Main_PGS.HPTI_TPTI_Section.ToHPTI_TPTIData().TPTI_Section,
                    HPTI = KMP_Main_PGS.HPTI_TPTI_Section.ToHPTI_TPTIData().HPTI_Section,
                    TPKC = KMP_Main_PGS.HPKC_TPKC_Section.ToHPKC_TPKCData().TPKC_Section,
                    HPKC = KMP_Main_PGS.HPKC_TPKC_Section.ToHPKC_TPKCData().HPKC_Section,
                    JBOG = KMP_Main_PGS.JBOG_Section.ToJBOG(Convert.ToUInt32(KMPVersion_TXT.Text)),
                    ITOP = KMP_Main_PGS.ITOP_Section.ToITOP(),
                    AERA = KMP_Main_PGS.AERA_Section.ToAERA(),
                    EMAC = KMP_Main_PGS.EMAC_Section.ToEMAC(),
                    TPGJ = KMP_Main_PGS.TPGJ_Section.ToTPGJ(),
                    TPNC = new KMPLibrary.Format.SectionData.TPNC(),
                    TPSM = new KMPLibrary.Format.SectionData.TPSM(),
                    IGTS = KMP_Main_PGS.IGTS_Section.ToIGTS(),
                    SROC = new KMPLibrary.Format.SectionData.SROC(),
                    TPLG = KMP_Main_PGS.HPLG_TPLG_Section.ToHPLG_TPLGData().TPLG_Section,
                    HPLG = KMP_Main_PGS.HPLG_TPLG_Section.ToHPLG_TPLGData().HPLG_Section,
                };

                KMP kMPFormat = new KMP(KMPSection, Convert.ToUInt32(KMPVersion_TXT.Text));
                kMPFormat.WriteKMP(bw);

                bw.Close();
                fs.Close();

                System.Windows.MessageBox.Show("KMP file has been saved : " + Save_KMP.FileName);
            }
            else return;
        }

        private void ViewportTypeChange_TSM_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem Viewport_Type_3D = (ToolStripMenuItem)sender;
            Viewport_Type_3D.Checked = !Viewport_Type_3D.Checked;

            if (Viewport_Type_3D.Checked == true)
            {
                //Projection mode
                render.MainViewPort.Orthographic = true;
                render.MainViewPort.CameraController.IsTouchZoomEnabled = false;

                //Disable the ability to rotate the camera by dragging the Viewport
                render.MainViewPort.CameraController.IsRotationEnabled = false;
            }
            else if(Viewport_Type_3D.Checked == false)
            {
                render.MainViewPort.Orthographic = false;
                render.MainViewPort.CameraController.IsTouchZoomEnabled = true;
                render.MainViewPort.CameraController.IsRotationEnabled = true;
            }
        }

        private void AddKMPSection_Click(object sender, EventArgs e)
        {
            if(KMPSection_Main_TabCtrl.SelectedIndex == 0)
            {
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    int Index = (KMP_Group_ListBox.Items.Count == 0) ? 0 : (KMP_Group_ListBox.Items.Count >= 1 ? KMP_Group_ListBox.SelectedIndex + 1 : 0);

                    EnemyRoute_PGS.HPNEValue hPNEValue = new EnemyRoute_PGS.HPNEValue(Index);
                    KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList.Insert(Index, hPNEValue);

                    ReInputID(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList, "GroupID");
                    for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList.Count; ReInputCount++)
                    {
                        ReInputID(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[ReInputCount].TPNEValueList, "Group_ID");
                    }

                    KMP_Group_ListBox.Items.Clear();
                    KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList.ToArray());
                    KMP_Group_ListBox.SelectedIndex = Index;

                    //Rail
                    HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), HTK_3DES.PathTools.Rail.RailType.Tube);
                    KMPViewportObject.EnemyRoute_Rail_List.Insert(Index, KMP_EnemyRoute_Rail);
                }
                else if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    int Index = (KMP_Group_ListBox.Items.Count == 0) ? 0 : (KMP_Group_ListBox.Items.Count >= 1 ? KMP_Group_ListBox.SelectedIndex + 1 : 0);

                    ItemRoute_PGS.HPTIValue hPTIValue = new ItemRoute_PGS.HPTIValue(Index);
                    KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList.Insert(Index, hPTIValue);

                    ReInputID(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList, "GroupID");
                    for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList.Count; ReInputCount++)
                    {
                        ReInputID(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[ReInputCount].TPTIValueList, "Group_ID");
                    }

                    KMP_Group_ListBox.Items.Clear();
                    KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList.ToArray());
                    KMP_Group_ListBox.SelectedIndex = Index;

                    //Rail
                    HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), HTK_3DES.PathTools.Rail.RailType.Tube);
                    KMPViewportObject.ItemRoute_Rail_List.Insert(Index, KMP_ItemRoute_Rail);
                }
                else if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    int Index = (KMP_Group_ListBox.Items.Count == 0) ? 0 : (KMP_Group_ListBox.Items.Count >= 1 ? KMP_Group_ListBox.SelectedIndex + 1 : 0);

                    Checkpoint_PGS.HPKCValue hPKCValue = new Checkpoint_PGS.HPKCValue(Index);
                    KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList.Insert(Index, hPKCValue);

                    ReInputID(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList, "GroupID");
                    for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList.Count; ReInputCount++)
                    {
                        for (int i = 0; i < KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[ReInputCount].TPKCValueList.Count; i++)
                        {
                            SetProperty(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[ReInputCount].TPKCValueList[i], ReInputCount, "Group_ID");
                        }
                    }

                    KMP_Group_ListBox.Items.Clear();
                    KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList.ToArray());
                    KMP_Group_ListBox.SelectedIndex = Index;

                    //Checkpoint_Rails
                    HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = new HTK_3DES.KMP_3DCheckpointSystem.Checkpoint();
                    KMPViewportObject.Checkpoint_Rail.Insert(Index, checkpoint);
                }
                else if (KMPSectionComboBox.Text == "Route")
                {
                    int Index = (KMP_Group_ListBox.Items.Count == 0) ? 0 : (KMP_Group_ListBox.Items.Count >= 1 ? KMP_Group_ListBox.SelectedIndex + 1 : 0);

                    Route_PGS.ITOP_Route iTOP_Route = new Route_PGS.ITOP_Route(Index);
                    KMP_Main_PGS.ITOP_Section.ITOP_RouteList.Insert(Index, iTOP_Route);

                    ReInputID(KMP_Main_PGS.ITOP_Section.ITOP_RouteList, "GroupID");

                    for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.ITOP_Section.ITOP_RouteList.Count; ReInputCount++)
                    {
                        for (int i = 0; i < KMP_Main_PGS.ITOP_Section.ITOP_RouteList[ReInputCount].ITOP_PointList.Count; i++)
                        {
                            SetProperty(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[ReInputCount].ITOP_PointList[i], ReInputCount, "GroupID");
                        }
                    }

                    KMP_Group_ListBox.Items.Clear();
                    KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.ITOP_Section.ITOP_RouteList.ToArray());
                    KMP_Group_ListBox.SelectedIndex = Index;

                    //Rail
                    HTK_3DES.PathTools.Rail Route_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), HTK_3DES.PathTools.Rail.RailType.Tube);
                    KMPViewportObject.Routes_List.Insert(Index, Route_Rail);
                }
                else if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    int Index = (KMP_Group_ListBox.Items.Count == 0) ? 0 : (KMP_Group_ListBox.Items.Count >= 1 ? KMP_Group_ListBox.SelectedIndex + 1 : 0);

                    GlideRoute_PGS.HPLGValue hPLGValue = new GlideRoute_PGS.HPLGValue(Index);
                    KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList.Insert(Index, hPLGValue);

                    ReInputID(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList, "GroupID");
                    for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList.Count; ReInputCount++)
                    {
                        for (int i = 0; i < KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[ReInputCount].TPLGValueList.Count; i++)
                        {
                            SetProperty(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[ReInputCount].TPLGValueList[i], ReInputCount, "GroupID");
                        }
                    }

                    KMP_Group_ListBox.Items.Clear();
                    KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList.ToArray());
                    KMP_Group_ListBox.SelectedIndex = Index;

                    //Rail
                    HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), HTK_3DES.PathTools.Rail.RailType.Tube);
                    KMPViewportObject.GlideRoute_Rail_List.Insert(Index, GlideRoute_Rail);
                }
            }
            else if(KMPSection_Main_TabCtrl.SelectedIndex == 1)
            {
                //カメラの位置を取得
                Point3D Pos = render.ViewportPosition(UserControl1.PositionMode.CameraPos);

                if (KMPSectionComboBox.Text == "KartPoint")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    KartPoint_PGS.TPTKValue TPTKValue = new KartPoint_PGS.TPTKValue(Pos.ToVector3D(), Index);
                    KMP_Main_PGS.TPTK_Section.TPTKValueList.Insert(Index, TPTKValue);

                    ReInputID(KMP_Main_PGS.TPTK_Section.TPTKValueList, "ID");

                    if (KMP_Main_PGS.TPTK_Section.TPTKValueList.Count != 0)
                    {
                        #region Add Model(StartPosition)
                        ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                        HTK_3DES.Transform StartPosition_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = TPTKValue.Position_Value.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = TPTKValue.Rotate_Value.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_StartPositionOBJ, StartPosition_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.StartPosition_MV3DList);
                        KMPViewportObject.StartPosition_MV3DList.Insert(Index, dv3D_StartPositionOBJ);
                        ReInputModelID(KMPViewportObject.StartPosition_MV3DList, -1, "StartPosition");
                        render.VpItemAddRange(KMPViewportObject.StartPosition_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.TPTK_Section.TPTKValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;

                }
                else if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        EnemyRoute_PGS.HPNEValue.TPNEValue TPNEValue = new EnemyRoute_PGS.HPNEValue.TPNEValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.Insert(Index, TPNEValue);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[ReInputCount].TPNEValueList, "ID");
                        }

                        if (KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.Count != 0)
                        {
                            #region Add Model(EnemyRoutes)
                            ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                            HTK_3DES.Transform EnemyPoint_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = TPNEValue.Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(TPNEValue.Control, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                            tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add Rail => MV3DList
                            render.VpItemDeleteRange(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList.Insert(Index, dv3D_EnemyPathOBJ);
                            ReInputModelID(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList, TPNEValue.Group_ID, "EnemyRoute");
                            render.VpItemAddRange(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            #endregion

                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Orange);
                        }

                        UpdateListBox(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                else if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        ItemRoute_PGS.HPTIValue.TPTIValue TPTIValue = new ItemRoute_PGS.HPTIValue.TPTIValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.Insert(Index, TPTIValue);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[ReInputCount].TPTIValueList, "ID");
                        }

                        if (KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.Count != 0)
                        {
                            #region Add Model(ItemRoutes)
                            ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                            HTK_3DES.Transform ItemPoint_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = TPTIValue.TPTI_Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(TPTIValue.TPTI_PointSize, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                            tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add Rail => MV3DList
                            render.VpItemDeleteRange(KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList.Insert(Index, dv3D_ItemPathOBJ);
                            ReInputModelID(KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList, TPTIValue.Group_ID, "ItemRoute");
                            render.VpItemAddRange(KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            #endregion

                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Green);
                        }

                        UpdateListBox(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                else if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        Vector2 LeftPos = new Vector2(Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Z));
                        Vector2 RightPos = new Vector2(Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Z));

                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        Checkpoint_PGS.HPKCValue.TPKCValue TPKCValue = new Checkpoint_PGS.HPKCValue.TPKCValue(LeftPos, RightPos, KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.Insert(Index, TPKCValue);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[ReInputCount].TPKCValue_List, "ID");
                        }

                        if (KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.Count != 0)
                        {
                            #region Create
                            var P2D_Left = TPKCValue.Position_2D_Left;
                            Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                            Point3D P3DLeft = Converter2D.Vector2DTo3D(P2DLeftToVector2, Converter2D.Axis_Up.Y).ToPoint3D();
                            P3DLeft.Y = Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text);

                            #region Transform(Left)
                            ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                            HTK_3DES.Transform P2DLeft_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = P3DLeft.ToVector3D(),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D_P2DLeft = new HTK_3DES.TSRSystem3D(dv3D_CheckpointLeftOBJ, P2DLeft_transform_Value);
                            tSRSystem3D_P2DLeft.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add
                            render.VpItemDeleteRange(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList.Insert(Index, dv3D_CheckpointLeftOBJ);
                            ReInputModelID(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList, TPKCValue.Group_ID, "Checkpoint_Left");
                            render.VpItemAddRange(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList);
                            #endregion

                            var P2D_Right = TPKCValue.Position_2D_Right;
                            Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                            Point3D P3DRight = Converter2D.Vector2DTo3D(P2DRightToVector2, Converter2D.Axis_Up.Y).ToPoint3D();
                            P3DRight.Y = Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text);

                            #region Transform(Right)
                            ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                            HTK_3DES.Transform P2DRight_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = P3DRight.ToVector3D(),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D_P2DRight = new HTK_3DES.TSRSystem3D(dv3D_CheckpointRightOBJ, P2DRight_transform_Value);
                            tSRSystem3D_P2DRight.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add
                            render.VpItemDeleteRange(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList.Insert(Index, dv3D_CheckpointRightOBJ);
                            ReInputModelID(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList, TPKCValue.Group_ID, "Checkpoint_Right");
                            render.VpItemAddRange(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList);
                            #endregion

                            List<Point3D> point3Ds = new List<Point3D>();
                            point3Ds.Add(P3DLeft);
                            point3Ds.Add(P3DRight);

                            LinesVisual3D CheckpointLine_LV3D = new LinesVisual3D { Points = new Point3DCollection(point3Ds), Thickness = 1, Color = Colors.Black };

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line.Insert(Index, CheckpointLine_LV3D);
                            render.MainViewPort.Children.Insert(Index, CheckpointLine_LV3D);

                            #region SplitWall
                            Point3DCollection point3Ds1 = new Point3DCollection();
                            point3Ds1.Add(new Point3D(point3Ds[1].X, 0, point3Ds[1].Z));
                            point3Ds1.Add(point3Ds[1]);
                            point3Ds1.Add(new Point3D(point3Ds[0].X, 0, point3Ds[0].Z));
                            point3Ds1.Add(point3Ds[0]);

                            ModelVisual3D SplitWall = HTK_3DES.CustomModelCreateHelper.CustomRectanglePlane3D(point3Ds1, System.Windows.Media.Color.FromArgb(0xA0, 0xA0, 0x00, 0xA0), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                            HTK_3DES.SetString_MV3D(SplitWall, "SplitWall -1 -1");
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL.Insert(Index, SplitWall);
                            render.MainViewPort.Children.Insert(Index, SplitWall);
                            #endregion
                            #endregion

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Line);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.DrawPath_Line(render, 5, Colors.Green);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Line);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.DrawPath_Line(render, 5, Colors.Red);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].ResetSideWall(render);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].DrawPath_SideWall(render, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                        }

                        UpdateListBox(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValue_List, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                else if (KMPSectionComboBox.Text == "Object")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    AddKMPObjectForm addKMPObjectForm = new AddKMPObjectForm();
                    addKMPObjectForm.ShowDialog();
                    var data = addKMPObjectForm.SelectedKMPObject_Info;

                    KMPObject_PGS.JBOGValue JBOGValue = new KMPObject_PGS.JBOGValue(data.Name, data.ObjID, Pos.ToVector3D(), Index);
                    KMP_Main_PGS.JBOG_Section.JBOGValueList.Insert(Index, JBOGValue);

                    ReInputID(KMP_Main_PGS.JBOG_Section.JBOGValueList, "ID");

                    if (KMP_Main_PGS.JBOG_Section.JBOGValueList.Count != 0)
                    {
                        #region Add Model(OBJ)
                        List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDataXml_List = KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml").ObjFlows;
                        string Path = ObjFlowDataXml_List.Find(x => x.ObjectID == data.ObjID).Path;
                        ModelVisual3D dv3D_OBJ = HTK_3DES.OBJReader(Path);

                        HTK_3DES.Transform OBJ_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = JBOGValue.Positions.GetVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(JBOGValue.Scales.GetVector3D(), 2),
                            Rotate3D = JBOGValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.GameObject_MV3DList);
                        KMPViewportObject.GameObject_MV3DList.Insert(Index, dv3D_OBJ);
                        ReInputModelID(KMPViewportObject.GameObject_MV3DList, -1, "GameObject");
                        render.VpItemAddRange(KMPViewportObject.GameObject_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.JBOG_Section.JBOGValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "Route")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        Route_PGS.ITOP_Route.ITOP_Point ITOP_Point = new Route_PGS.ITOP_Route.ITOP_Point(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.Insert(Index, ITOP_Point);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.ITOP_Section.ITOP_RouteList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[ReInputCount].ITOP_PointList, "ID");
                        }

                        if (KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.Count != 0)
                        {
                            #region Add Model(Routes)
                            ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                            HTK_3DES.Transform Route_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = ITOP_Point.Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_RouteOBJ, Route_transform_Value);
                            tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add Rail => MV3DList
                            render.VpItemDeleteRange(KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList.Insert(Index, dv3D_RouteOBJ);
                            ReInputModelID(KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList, ITOP_Point.GroupID, "Routes");
                            render.VpItemAddRange(KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            #endregion

                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Blue);
                        }

                        UpdateListBox(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                else if (KMPSectionComboBox.Text == "Area")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    Area_PGS.AERAValue AERAValue = new Area_PGS.AERAValue(Pos.ToVector3D(), Index);
                    KMP_Main_PGS.AERA_Section.AERAValueList.Insert(Index, AERAValue);

                    ReInputID(KMP_Main_PGS.AERA_Section.AERAValueList, "ID");

                    if (KMP_Main_PGS.AERA_Section.AERAValueList.Count != 0)
                    {
                        #region Add Model(Area)
                        ModelVisual3D dv3D_AreaOBJ = null;
                        if (AERAValue.AreaModeSettings.AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                        HTK_3DES.Transform Area_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = AERAValue.Positions.GetVector3D(),
                            Scale3D = HTK_3DES.ScaleFactor(AERAValue.Scales.GetVector3D(), 2000),
                            Rotate3D = AERAValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.Area_MV3DList);
                        KMPViewportObject.Area_MV3DList.Insert(Index, dv3D_AreaOBJ);
                        ReInputModelID(KMPViewportObject.Area_MV3DList, -1, "Area");
                        render.VpItemAddRange(KMPViewportObject.Area_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.AERA_Section.AERAValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "Camera")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    Camera_PGS.EMACValue EMACValue = new Camera_PGS.EMACValue(Pos.ToVector3D(), Index);
                    KMP_Main_PGS.EMAC_Section.EMACValueList.Insert(Index, EMACValue);

                    ReInputID(KMP_Main_PGS.EMAC_Section.EMACValueList, "ID");

                    if (KMP_Main_PGS.EMAC_Section.EMACValueList.Count != 0)
                    {
                        #region Add Model(Camera)
                        ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                        HTK_3DES.Transform Camera_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = EMACValue.Positions.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = EMACValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_CameraOBJ, Camera_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.Camera_MV3DList);
                        KMPViewportObject.Camera_MV3DList.Insert(Index, dv3D_CameraOBJ);
                        ReInputModelID(KMPViewportObject.Camera_MV3DList, -1, "Camera");
                        render.VpItemAddRange(KMPViewportObject.Camera_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.EMAC_Section.EMACValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "JugemPoint")
                {
                    int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                    RespawnPoint_PGS.TPGJValue TPGJValue = new RespawnPoint_PGS.TPGJValue(Pos.ToVector3D(), Index);
                    KMP_Main_PGS.TPGJ_Section.TPGJValueList.Insert(Index, TPGJValue);

                    ReInputID(KMP_Main_PGS.TPGJ_Section.TPGJValueList, "ID");

                    if (KMP_Main_PGS.TPGJ_Section.TPGJValueList.Count != 0)
                    {
                        #region Add Model(RespawnPoint)
                        ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                        HTK_3DES.Transform RespawnPoint_transform_Value = new HTK_3DES.Transform
                        {
                            Translate3D = TPGJValue.Positions.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = TPGJValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_RespawnPointOBJ, RespawnPoint_transform_Value);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                        //Add MV3DList
                        render.VpItemDeleteRange(KMPViewportObject.RespawnPoint_MV3DList);
                        KMPViewportObject.RespawnPoint_MV3DList.Insert(Index, dv3D_RespawnPointOBJ);
                        ReInputModelID(KMPViewportObject.RespawnPoint_MV3DList, -1, "RespawnPoint");
                        render.VpItemAddRange(KMPViewportObject.RespawnPoint_MV3DList);
                        #endregion
                    }

                    UpdateListBox(KMP_Main_PGS.TPGJ_Section.TPGJValueList, KMP_Point_ListBox);
                    KMP_Point_ListBox.SelectedIndex = Index;
                }
                else if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int Index = (KMP_Point_ListBox.Items.Count == 0) ? 0 : (KMP_Point_ListBox.Items.Count >= 1 ? KMP_Point_ListBox.SelectedIndex + 1 : 0);

                        GlideRoute_PGS.HPLGValue.TPLGValue TPLGValue = new GlideRoute_PGS.HPLGValue.TPLGValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, Index);
                        KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.Insert(Index, TPLGValue);

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList.Count; ReInputCount++)
                        {
                            ReInputID(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[ReInputCount].TPLGValueList, "ID");
                        }

                        if (KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.Count != 0)
                        {
                            #region Add Model(GlideRoutes)
                            ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                            HTK_3DES.Transform GliderPoint_transform_Value = new HTK_3DES.Transform
                            {
                                Translate3D = TPLGValue.Positions.GetVector3D(),
                                Scale3D = HTK_3DES.ScaleFactor(TPLGValue.TPLG_PointScaleValue, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                            tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle);

                            //Add Rail => MV3DList
                            render.VpItemDeleteRange(KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList.Insert(Index, dv3D_GliderPathOBJ);
                            ReInputModelID(KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList, TPLGValue.GroupID, "GlideRoutes");
                            render.VpItemAddRange(KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList);
                            #endregion

                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.LightSkyBlue);
                        }

                        UpdateListBox(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList, KMP_Point_ListBox);
                        KMP_Point_ListBox.SelectedIndex = Index;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
            }
        }

        private void DeleteKMPSection_Click(object sender, EventArgs e)
        {
            if (KMPSection_Main_TabCtrl.SelectedIndex == 0)
            {
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        KMPViewportObject.EnemyRoute_Rail_List[Index].DeleteRail(render);
                        KMPViewportObject.EnemyRoute_Rail_List.RemoveAt(Index);
                        ReInputModelID(KMPViewportObject.EnemyRoute_Rail_List, "EnemyRoute");

                        KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList.RemoveAt(Index);
                        ReInputID(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[ReInputCount].TPNEValueList.Count; i++)
                            {
                                SetProperty(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[ReInputCount].TPNEValueList[i], ReInputCount, "Group_ID");
                            }
                        }
                    }

                    propertyGrid_KMP_Group.SelectedObject = null;
                    KMP_Group_ListBox.Items.Clear();
                    UpdateListBox(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList, KMP_Group_ListBox);

                    propertyGrid_KMP_Path.SelectedObject = null;
                    KMP_Point_ListBox.Items.Clear();

                    //SelectIndex
                    if ((SelectedItemIndexArray[0] - 1) >= 0)
                    {
                        KMP_Group_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                    }
                    else if ((SelectedItemIndexArray[0] - 1) == -1)
                    {
                        if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;
                        else KMP_Group_ListBox.SelectedIndex = -1;
                    }
                }
                else if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        KMPViewportObject.ItemRoute_Rail_List[Index].DeleteRail(render);
                        KMPViewportObject.ItemRoute_Rail_List.RemoveAt(Index);
                        ReInputModelID(KMPViewportObject.ItemRoute_Rail_List, "ItemRoute");

                        KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList.RemoveAt(Index);
                        ReInputID(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[ReInputCount].TPTIValueList.Count; i++)
                            {
                                SetProperty(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[ReInputCount].TPTIValueList[i], ReInputCount, "Group_ID");
                            }
                        }
                    }

                    propertyGrid_KMP_Group.SelectedObject = null;
                    KMP_Group_ListBox.Items.Clear();
                    UpdateListBox(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList, KMP_Group_ListBox);

                    propertyGrid_KMP_Path.SelectedObject = null;
                    KMP_Point_ListBox.Items.Clear();

                    //SelectIndex
                    if ((SelectedItemIndexArray[0] - 1) >= 0)
                    {
                        KMP_Group_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                    }
                    else if ((SelectedItemIndexArray[0] - 1) == -1)
                    {
                        if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;
                        else KMP_Group_ListBox.SelectedIndex = -1;
                    }
                }
                else if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        KMPViewportObject.Checkpoint_Rail[Index].DeleteRailChk(render);
                        KMPViewportObject.Checkpoint_Rail.RemoveAt(Index);
                        ReInputModelID(KMPViewportObject.Checkpoint_Rail, "Checkpoint_Left", "Checkpoint_Right");

                        KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList.RemoveAt(Index);
                        ReInputID(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[ReInputCount].TPKCValueList.Count; i++)
                            {
                                SetProperty(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[ReInputCount].TPKCValueList[i], ReInputCount, "Group_ID");
                            }
                        }
                    }

                    propertyGrid_KMP_Group.SelectedObject = null;
                    KMP_Group_ListBox.Items.Clear();
                    UpdateListBox(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList, KMP_Group_ListBox);

                    propertyGrid_KMP_Path.SelectedObject = null;
                    KMP_Point_ListBox.Items.Clear();

                    //SelectIndex
                    if ((SelectedItemIndexArray[0] - 1) >= 0)
                    {
                        KMP_Group_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                    }
                    else if ((SelectedItemIndexArray[0] - 1) == -1)
                    {
                        if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;
                        else KMP_Group_ListBox.SelectedIndex = -1;
                    }
                }
                else if (KMPSectionComboBox.Text == "Route")
                {
                    int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        KMPViewportObject.Routes_List[Index].DeleteRail(render);
                        KMPViewportObject.Routes_List.RemoveAt(Index);
                        ReInputModelID(KMPViewportObject.Routes_List, "Routes");

                        KMP_Main_PGS.ITOP_Section.ITOP_RouteList.RemoveAt(Index);
                        ReInputID(KMP_Main_PGS.ITOP_Section.ITOP_RouteList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.ITOP_Section.ITOP_RouteList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < KMP_Main_PGS.ITOP_Section.ITOP_RouteList[ReInputCount].ITOP_PointList.Count; i++)
                            {
                                SetProperty(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[ReInputCount].ITOP_PointList[i], ReInputCount, "GroupID");
                            }
                        }
                    }

                    propertyGrid_KMP_Group.SelectedObject = null;
                    KMP_Group_ListBox.Items.Clear();
                    UpdateListBox(KMP_Main_PGS.ITOP_Section.ITOP_RouteList, KMP_Group_ListBox);

                    propertyGrid_KMP_Path.SelectedObject = null;
                    KMP_Point_ListBox.Items.Clear();

                    //SelectIndex
                    if ((SelectedItemIndexArray[0] - 1) >= 0)
                    {
                        KMP_Group_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                    }
                    else if ((SelectedItemIndexArray[0] - 1) == -1)
                    {
                        if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;
                        else KMP_Group_ListBox.SelectedIndex = -1;
                    }
                }
                else if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        KMPViewportObject.GlideRoute_Rail_List[Index].DeleteRail(render);
                        KMPViewportObject.GlideRoute_Rail_List.RemoveAt(Index);
                        ReInputModelID(KMPViewportObject.GlideRoute_Rail_List, "GlideRoutes");

                        KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList.RemoveAt(Index);
                        ReInputID(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[ReInputCount].TPLGValueList.Count; i++)
                            {
                                SetProperty(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[ReInputCount].TPLGValueList[i], ReInputCount, "GroupID");
                            }
                        }
                    }

                    propertyGrid_KMP_Group.SelectedObject = null;
                    KMP_Group_ListBox.Items.Clear();
                    UpdateListBox(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList, KMP_Group_ListBox);

                    propertyGrid_KMP_Path.SelectedObject = null;
                    KMP_Point_ListBox.Items.Clear();

                    //SelectIndex
                    if ((SelectedItemIndexArray[0] - 1) >= 0)
                    {
                        KMP_Group_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                    }
                    else if ((SelectedItemIndexArray[0] - 1) == -1)
                    {
                        if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;
                        else KMP_Group_ListBox.SelectedIndex = -1;
                    }
                }
            }
            else if (KMPSection_Main_TabCtrl.SelectedIndex == 1)
            {
                if (KMPSectionComboBox.Text == "KartPoint")
                {
                    if (KMP_Point_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            DeleteItem(KMP_Main_PGS.TPTK_Section.TPTKValueList, KMPViewportObject.StartPosition_MV3DList, Index);
                        }

                        ReInputModelID(KMPViewportObject.StartPosition_MV3DList, "StartPosition");
                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.TPTK_Section.TPTKValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.TPTK_Section.TPTKValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                }
                else if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.RemoveAt(Index);
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DeleteRailPoint(render, Index, 10.0, Colors.Orange);
                            KMP_Point_ListBox.Items.RemoveAt(Index);
                        }

                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("[Error] Group : Null");
                    }
                }
                else if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.RemoveAt(Index);
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DeleteRailPoint(render, Index, 10.0, Colors.Green);
                            KMP_Point_ListBox.Items.RemoveAt(Index);
                        }

                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("[Error] Group : Null");
                    }
                }
                else if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.RemoveAt(Index);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.DeleteRailPoint(render, Index, 5.0, Colors.Green);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.DeleteRailPoint(render, Index, 5.0, Colors.Red);

                            render.MainViewPort.Children.Remove(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line[Index]);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line.RemoveAt(Index);

                            render.MainViewPort.Children.Remove(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[Index]);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL.RemoveAt(Index);

                            #region DrawSideWall (Left)
                            for (int i = 0; i < KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Left.Count; i++)
                            {
                                render.MainViewPort.Children.Remove(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Left[i]);
                                render.UpdateLayout();
                            }

                            for (int i = 0; i < KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Left.Count; i++)
                            {
                                KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Left.RemoveAt(i);
                            }

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Left.Clear();
                            #endregion

                            #region DrawSideWall (Right)
                            for (int i = 0; i < KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Right.Count; i++)
                            {
                                render.MainViewPort.Children.Remove(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Right[i]);
                                render.UpdateLayout();
                            }

                            for (int i = 0; i < KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Right.Count; i++)
                            {
                                KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Right.RemoveAt(i);
                            }

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].SideWall_Right.Clear();
                            #endregion

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].DrawPath_SideWall(render, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));

                            KMP_Point_ListBox.Items.RemoveAt(Index);
                        }

                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Groupを選択してください");
                    }
                }
                else if (KMPSectionComboBox.Text == "Object")
                {
                    if (KMP_Point_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            DeleteItem(KMP_Main_PGS.JBOG_Section.JBOGValueList, KMPViewportObject.GameObject_MV3DList, Index);
                        }

                        ReInputModelID(KMPViewportObject.GameObject_MV3DList, "GameObject");
                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.JBOG_Section.JBOGValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.JBOG_Section.JBOGValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                }
                else if (KMPSectionComboBox.Text == "Route")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.RemoveAt(Index);
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].DeleteRailPoint(render, Index, 10.0, Colors.Blue);
                            KMP_Point_ListBox.Items.RemoveAt(Index);
                        }

                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList, "ID");
                        UpdateListBox(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : Null");
                    }
                }
                else if (KMPSectionComboBox.Text == "Area")
                {
                    if (KMP_Point_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            DeleteItem(KMP_Main_PGS.AERA_Section.AERAValueList, KMPViewportObject.Area_MV3DList, Index);
                        }

                        ReInputModelID(KMPViewportObject.Area_MV3DList, "Area");
                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.AERA_Section.AERAValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.AERA_Section.AERAValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                }
                else if (KMPSectionComboBox.Text == "Camera")
                {
                    if (KMP_Point_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            DeleteItem(KMP_Main_PGS.EMAC_Section.EMACValueList, KMPViewportObject.Camera_MV3DList, Index);
                        }

                        ReInputModelID(KMPViewportObject.Camera_MV3DList, "Camera");
                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.EMAC_Section.EMACValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.AERA_Section.AERAValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                }
                else if (KMPSectionComboBox.Text == "JugemPoint")
                {
                    if (KMP_Point_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            DeleteItem(KMP_Main_PGS.TPGJ_Section.TPGJValueList, KMPViewportObject.RespawnPoint_MV3DList, Index);
                        }

                        ReInputModelID(KMPViewportObject.RespawnPoint_MV3DList, "RespawnPoint");
                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.TPGJ_Section.TPGJValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.TPGJ_Section.TPGJValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                }
                else if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                        foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                        {
                            KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.RemoveAt(Index);
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DeleteRailPoint(render, Index, 10.0, Colors.LightSkyBlue);
                            KMP_Point_ListBox.Items.RemoveAt(Index);
                        }

                        propertyGrid_KMP_Path.SelectedObject = null;
                        ReInputID(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList, "ID");
                        UpdateListBox(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList, KMP_Point_ListBox);

                        //SelectIndex
                        if ((SelectedItemIndexArray[0] - 1) >= 0)
                        {
                            KMP_Point_ListBox.SelectedIndex = SelectedItemIndexArray[0] - 1;
                        }
                        else if ((SelectedItemIndexArray[0] - 1) == -1)
                        {
                            if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;
                            else KMP_Point_ListBox.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : Null");
                    }
                }
            }
        }

        private void KMPSectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid_KMP_Path.SelectedObject = null;
            propertyGrid_KMP_Group.SelectedObject = null;

            KMP_Group_ListBox.Items.Clear();
            KMP_Point_ListBox.Items.Clear();

            if (KMPSectionComboBox.Text == "KartPoint")
            {
                KMP_Point_ListBox.Items.AddRange(KMP_Main_PGS.TPTK_Section.TPTKValueList.ToArray());
                if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;
            }
            else if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList.ToArray());
                if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = true;

                xXXXRouteImporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = true;
            }
            else if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList.ToArray());
                if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = true;

                xXXXRouteImporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = true;
            }
            else if (KMPSectionComboBox.Text == "CheckPoint")
            {
                KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList.ToArray());
                if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;
            }
            else if (KMPSectionComboBox.Text == "Object")
            {
                KMP_Point_ListBox.Items.AddRange(KMP_Main_PGS.JBOG_Section.JBOGValueList.ToArray());
                if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;
            }
            else if (KMPSectionComboBox.Text == "Route")
            {
                KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.ITOP_Section.ITOP_RouteList.ToArray());
                if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;
            }
            else if (KMPSectionComboBox.Text == "Area")
            {
                KMP_Point_ListBox.Items.AddRange(KMP_Main_PGS.AERA_Section.AERAValueList.ToArray());
                if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;
            }
            else if (KMPSectionComboBox.Text == "Camera")
            {
                KMP_Point_ListBox.Items.AddRange(KMP_Main_PGS.EMAC_Section.EMACValueList.ToArray());
                if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;
            }
            else if (KMPSectionComboBox.Text == "JugemPoint")
            {
                KMP_Point_ListBox.Items.AddRange(KMP_Main_PGS.TPGJ_Section.TPGJValueList.ToArray());
                if (KMP_Point_ListBox.Items.Count >= 1) KMP_Point_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;
            }
            else if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                KMP_Group_ListBox.Items.AddRange(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList.ToArray());
                if (KMP_Group_ListBox.Items.Count >= 1) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = true;

                xXXXRouteImporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = true;
            }

            outputXXXXAsXmlToolStripMenuItem.Text = "Output " + KMPSectionComboBox.Text + " as Xml";
            inputXmlAsXXXXToolStripMenuItem.Text = "Input Xml as " + KMPSectionComboBox.Text; 
        }

        private void KMP_Group_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KMPSectionComboBox.SelectedIndex == -1) return;
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                propertyGrid_KMP_Path.SelectedObject = null;
                UpdateListBox(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList, KMP_Point_ListBox);
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Group.SelectedObjects = KMP_Group_ListBox.SelectedItems.Cast<EnemyRoute_PGS.HPNEValue>().ToArray();
                    KMP_Point_ListBox.SelectedIndex = 0;
                }
            }
            else if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                propertyGrid_KMP_Path.SelectedObject = null;
                UpdateListBox(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList, KMP_Point_ListBox);
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Group.SelectedObjects = KMP_Group_ListBox.SelectedItems.Cast<ItemRoute_PGS.HPTIValue>().ToArray();
                    KMP_Point_ListBox.SelectedIndex = 0;
                }
            }
            else if (KMPSectionComboBox.Text == "CheckPoint")
            {
                propertyGrid_KMP_Path.SelectedObject = null;
                UpdateListBox(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList, KMP_Point_ListBox);
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Group.SelectedObjects = KMP_Group_ListBox.SelectedItems.Cast<Checkpoint_PGS.HPKCValue>().ToArray();
                    KMP_Point_ListBox.SelectedIndex = 0;
                }
            }
            else if (KMPSectionComboBox.Text == "Route")
            {
                propertyGrid_KMP_Path.SelectedObject = null;
                UpdateListBox(KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList, KMP_Point_ListBox);
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Group.SelectedObjects = KMP_Group_ListBox.SelectedItems.Cast<Route_PGS.ITOP_Route>().ToArray();
                    KMP_Point_ListBox.SelectedIndex = 0;
                }
            }
            else if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                propertyGrid_KMP_Path.SelectedObject = null;
                UpdateListBox(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList, KMP_Point_ListBox);
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Group.SelectedObjects = KMP_Group_ListBox.SelectedItems.Cast<GlideRoute_PGS.HPLGValue>().ToArray();
                    KMP_Point_ListBox.SelectedIndex = 0;
                }
            }
        }

        private void KMP_Path_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid_KMP_Path.SelectedObject = null;
            if (KMP_Point_ListBox.SelectedIndex == -1) return;
            if (KMPSectionComboBox.Text == "KartPoint")
            {
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<KartPoint_PGS.TPTKValue>().ToArray();
                }
            }
            else if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<EnemyRoute_PGS.HPNEValue.TPNEValue>().ToArray();
            }
            else if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<ItemRoute_PGS.HPTIValue.TPTIValue>().ToArray();
            }
            else if (KMPSectionComboBox.Text == "CheckPoint")
            {
                propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<Checkpoint_PGS.HPKCValue.TPKCValue>().ToArray();
            }
            else if (KMPSectionComboBox.Text == "Object")
            {
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<KMPObject_PGS.JBOGValue>().ToArray();
                }
            }
            else if (KMPSectionComboBox.Text == "Route")
            {
                propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<Route_PGS.ITOP_Route.ITOP_Point>().ToArray();
            }
            else if (KMPSectionComboBox.Text == "Area")
            {
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<Area_PGS.AERAValue>().ToArray();
                }
            }
            else if (KMPSectionComboBox.Text == "Camera")
            {
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<Camera_PGS.EMACValue>().ToArray();
                }
            }
            else if (KMPSectionComboBox.Text == "JugemPoint")
            {
                if (KMP_Point_ListBox.Items.Count >= 1)
                {
                    propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<RespawnPoint_PGS.TPGJValue>().ToArray();
                }
            }
            else if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                propertyGrid_KMP_Path.SelectedObjects = KMP_Point_ListBox.SelectedItems.Cast<GlideRoute_PGS.HPLGValue.TPLGValue>().ToArray();
            }
        }

        private void KMP_CheckpointHeightOffset_TXT_TextChanged(object sender, EventArgs e)
        {
            double p = Convert.ToDouble((KMP_CheckpointHeightOffset_TXT.Text != "") ? KMP_CheckpointHeightOffset_TXT.Text : "0");

            for (int Count = 0; Count < KMPViewportObject.Checkpoint_Rail.Count; Count++)
            {
                for (int CP_MDL_L = 0; CP_MDL_L < KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.BasePointModelList.Count; CP_MDL_L++)
                {
                    double SX = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.BasePointModelList[CP_MDL_L].Content.Transform.Value.M11;
                    double SY = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.BasePointModelList[CP_MDL_L].Content.Transform.Value.M22;
                    double SZ = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.BasePointModelList[CP_MDL_L].Content.Transform.Value.M33;

                    double TX = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.BasePointModelList[CP_MDL_L].Content.Transform.Value.OffsetX;
                    double TY = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.BasePointModelList[CP_MDL_L].Content.Transform.Value.OffsetY;
                    double TZ = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.BasePointModelList[CP_MDL_L].Content.Transform.Value.OffsetZ;

                    HTK_3DES.Transform t = new HTK_3DES.Transform
                    {
                        Translate3D = new Vector3D(TX, p, TZ),
                        Scale3D = new Vector3D(SX, SY, SZ),
                        Rotate3D = new Vector3D(0, 0, 0)
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.BasePointModelList[CP_MDL_L], t);
                    tSRSystem3D.Transform3D(new HTK_3DES.TSRSystem3D.RotationCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle, 1, null, false);
                }

                for (int CP_MDL_R = 0; CP_MDL_R < KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.BasePointModelList.Count; CP_MDL_R++)
                {
                    double SX = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.BasePointModelList[CP_MDL_R].Content.Transform.Value.M11;
                    double SY = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.BasePointModelList[CP_MDL_R].Content.Transform.Value.M22;
                    double SZ = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.BasePointModelList[CP_MDL_R].Content.Transform.Value.M33;

                    double TX = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.BasePointModelList[CP_MDL_R].Content.Transform.Value.OffsetX;
                    double TY = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.BasePointModelList[CP_MDL_R].Content.Transform.Value.OffsetY;
                    double TZ = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.BasePointModelList[CP_MDL_R].Content.Transform.Value.OffsetZ;

                    HTK_3DES.Transform t = new HTK_3DES.Transform
                    {
                        Translate3D = new Vector3D(TX, p, TZ),
                        Scale3D = new Vector3D(SX, SY, SZ),
                        Rotate3D = new Vector3D(0, 0, 0),
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.BasePointModelList[CP_MDL_R], t);
                    tSRSystem3D.Transform3D(new HTK_3DES.TSRSystem3D.RotationCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Angle, 1, null, false);
                }

                for (int CP_Dline = 0; CP_Dline < KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Line.Count; CP_Dline++)
                {
                    var DividingLine1 = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Line[CP_Dline].Points[0];
                    KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Line[CP_Dline].Points[0] = new Point3D(DividingLine1.X, p, DividingLine1.Z);

                    var DividingLine2 = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Line[CP_Dline].Points[1];
                    KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Line[CP_Dline].Points[1] = new Point3D(DividingLine2.X, p, DividingLine2.Z);
                }

                for (int CP_PathLine_L = 0; CP_PathLine_L < KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.LV3D_List.Count; CP_PathLine_L++)
                {
                    var RailLineLeft1 = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.LV3D_List[CP_PathLine_L].Points[0];
                    KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.LV3D_List[CP_PathLine_L].Points[0] = new Point3D(RailLineLeft1.X, p, RailLineLeft1.Z);

                    var RailLineLeft2 = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.LV3D_List[CP_PathLine_L].Points[1];
                    KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.LV3D_List[CP_PathLine_L].Points[1] = new Point3D(RailLineLeft2.X, p, RailLineLeft2.Z);
                }

                for (int CP_PathLine_R = 0; CP_PathLine_R < KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.LV3D_List.Count; CP_PathLine_R++)
                {
                    //var d = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.LV3D_List.Select(x => x.Points[0]);
                    //KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.LV3D_List[CP_DLine_R].Points[0] = d.Select(x => x);

                    var RailLineRight1 = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.LV3D_List[CP_PathLine_R].Points[0];
                    KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.LV3D_List[CP_PathLine_R].Points[0] = new Point3D(RailLineRight1.X, p, RailLineRight1.Z);

                    var RailLineRight2 = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.LV3D_List[CP_PathLine_R].Points[1];
                    KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.LV3D_List[CP_PathLine_R].Points[1] = new Point3D(RailLineRight2.X, p, RailLineRight2.Z);
                }

                for (int CP_SideWall_L = 0; CP_SideWall_L < KMPViewportObject.Checkpoint_Rail[Count].SideWall_Left.Count; CP_SideWall_L++)
                {
                    var SideWallLeft1 = HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].SideWall_Left[CP_SideWall_L].Content).Positions[1];
                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].SideWall_Left[CP_SideWall_L].Content).Positions[1] = new Point3D(SideWallLeft1.X, p, SideWallLeft1.Z);

                    var SideWallLeft2 = HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].SideWall_Left[CP_SideWall_L].Content).Positions[3];
                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].SideWall_Left[CP_SideWall_L].Content).Positions[3] = new Point3D(SideWallLeft2.X, p, SideWallLeft2.Z);
                }

                for (int CP_SideWall_R = 0; CP_SideWall_R < KMPViewportObject.Checkpoint_Rail[Count].SideWall_Right.Count; CP_SideWall_R++)
                {
                    var SideWallRight1 = HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].SideWall_Right[CP_SideWall_R].Content).Positions[1];
                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].SideWall_Right[CP_SideWall_R].Content).Positions[1] = new Point3D(SideWallRight1.X, p, SideWallRight1.Z);

                    var SideWallRight2 = HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].SideWall_Right[CP_SideWall_R].Content).Positions[3];
                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].SideWall_Right[CP_SideWall_R].Content).Positions[3] = new Point3D(SideWallRight2.X, p, SideWallRight2.Z);
                }

                for (int CP_SplitWall = 0; CP_SplitWall < KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_SplitWallMDL.Count; CP_SplitWall++)
                {
                    var SplitWall1 = HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_SplitWallMDL[CP_SplitWall].Content).Positions[1];
                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_SplitWallMDL[CP_SplitWall].Content).Positions[1] = new Point3D(SplitWall1.X, p, SplitWall1.Z);

                    var SplitWall2 = HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_SplitWallMDL[CP_SplitWall].Content).Positions[3];
                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_SplitWallMDL[CP_SplitWall].Content).Positions[3] = new Point3D(SplitWall2.X, p, SplitWall2.Z);
                }
            }
        }

        private void KMP_CheckpointHeightOffset_TXT_Leave(object sender, EventArgs e)
        {
            if (KMP_CheckpointHeightOffset_TXT.Text == "") KMP_CheckpointHeightOffset_TXT.Text = "0";
        }

        private void objFlowXmlEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("ObjFlow.bin") == true)
            {
                if (File.Exists("ObjFlowData.xml") == false)
                {
                    string MText = "ObjFlowXmlEditorを使用するにはObjFlowData.xmlが必要です。\r\nObjFlow.binを使用してObjFlowData.xmlを作成しますか?";
                    string Caption = "確認";
                    MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;

                    DialogResult dialogResult = (DialogResult)System.Windows.MessageBox.Show(MText, Caption, messageBoxButton);
                    if(dialogResult == DialogResult.Yes)
                    {
                        System.IO.FileStream fs = new FileStream("ObjFlow.bin", FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        FBOCLibrary.FBOC FBOCFormat = new FBOCLibrary.FBOC();
                        FBOCFormat.ReadFBOC(br);
                        br.Close();
                        fs.Close();

                        //var FBOCFormat = KMPs.KMPHelper.ObjFlowReader.Binary.Read("ObjFlow.bin");
                        List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> objFlowValues = ObjFlowConverter.ConvertTo.ToObjFlowDB_XML(FBOCFormat);
                        KMPLibrary.XMLConvert.Statics.ObjFlow.CreateXml(objFlowValues, "KMPObjectFlow", "KMP_OBJ\\OBJ\\OBJ.obj", "ObjFlowData.xml");

                        ObjFlowXmlEditor objFlowXmlEditor = new ObjFlowXmlEditor();
                        objFlowXmlEditor.Show();
                    }
                    if (dialogResult == DialogResult.No)
                    {
                        System.Windows.MessageBox.Show("ObjFlowData.xmlの作成を中止しました");
                        return;
                    }
                }
                else if (File.Exists("ObjFlowData.xml") == true)
                {
                    ObjFlowXmlEditor objFlowXmlEditor = new ObjFlowXmlEditor();
                    objFlowXmlEditor.Show();
                }
            }
            else if (File.Exists("ObjFlow.bin") == false)
            {
                System.Windows.MessageBox.Show("[ObjFlowXmlEditor]\r\nObjFlow.bin : null\r\nObjFlow.binがこのプログラムと同じディレクトリ内に存在しません。\r\nObjFlow.binをこのプログラムと同じディレクトリに配置してください。\r\nObjFlow.binは[RaceCommon.szs]に格納されています。", "Error");
            }
        }

        private void objFlowXmlToObjFlowbinTSM_Click(object sender, EventArgs e)
        {
            OpenFileDialog Open_ObjFlowDataXml = new OpenFileDialog()
            {
                Title = "Open ObjFlowData.xml",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "xml file|*.xml"
            };

            if (Open_ObjFlowDataXml.ShowDialog() == DialogResult.OK)
			{
                var ObjFlowDB = KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml(Open_ObjFlowDataXml.FileName);
                FBOCLibrary.FBOC FBOC = ObjFlowConverter.ConvertTo.ToFBOC(ObjFlowDB.ObjFlows);

                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Title = "Save ObjFlow.bin",
                    InitialDirectory = @"C:\Users\User\Desktop",
                    Filter = "bin file|*.bin"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(fs);

                    FBOC.WriteFBOC(bw);

                    bw.Close();
                    fs.Close();
                }
                else return;
            }
            else System.Windows.MessageBox.Show("Abort 1");
        }

        private void propertyGrid_KMP_Group_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (KMPSectionComboBox.SelectedIndex == -1) return;
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    EnemyRoute_PGS.HPNEValue GetHPNEValue = KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[Index];
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetHPNEValue.IsViewportVisible, render, KMPViewportObject.EnemyRoute_Rail_List[Index]);
                }
            }
            else if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    ItemRoute_PGS.HPTIValue GetHPTIValue = KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[Index];
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetHPTIValue.IsViewportVisible, render, KMPViewportObject.ItemRoute_Rail_List[Index]);
                }
            }
            else if (KMPSectionComboBox.Text == "CheckPoint")
            {
                int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    Checkpoint_PGS.HPKCValue GetHPKCValue = KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[Index];
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetHPKCValue.IsViewportVisible, render, KMPViewportObject.Checkpoint_Rail[Index]);
                }
            }
            else if (KMPSectionComboBox.Text == "Route")
            {
                int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    Route_PGS.ITOP_Route GetITOP_Group_Value = KMP_Main_PGS.ITOP_Section.ITOP_RouteList[Index];
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetITOP_Group_Value.IsViewportVisible, render, KMPViewportObject.Routes_List[Index]);
                }
            }
            else if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                int[] SelectedItemIndexArray = KMP_Group_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    GlideRoute_PGS.HPLGValue GetHPLGValue = KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[Index];
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetHPLGValue.IsViewportVisible, render, KMPViewportObject.GlideRoute_Rail_List[Index]);
                }
            }
        }

        private void propertyGrid_KMP_Path_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //Property変更後
            if (KMPSectionComboBox.Text == "KartPoint")
            {
                int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    KartPoint_PGS.TPTKValue GetTPTKValue = KMP_Main_PGS.TPTK_Section.TPTKValueList[Index];

                    HTK_3DES.Transform t = new HTK_3DES.Transform
                    {
                        Translate3D = new Vector3D(GetTPTKValue.Position_Value.X, GetTPTKValue.Position_Value.Y, GetTPTKValue.Position_Value.Z),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = new Vector3D(GetTPTKValue.Rotate_Value.X, GetTPTKValue.Rotate_Value.Y, GetTPTKValue.Rotate_Value.Z)
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.StartPosition_MV3DList[Index], t);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //VisibleCheck
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetTPTKValue.IsViewportVisible, render, KMPViewportObject.StartPosition_MV3DList[Index]);
                }
            }
            else if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        EnemyRoute_PGS.HPNEValue.TPNEValue GetTPNEValue = KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList[Index];

                        HTK_3DES.Transform t = new HTK_3DES.Transform
                        {
                            Translate3D = new Vector3D(GetTPNEValue.Positions.X, GetTPNEValue.Positions.Y, GetTPNEValue.Positions.Z),
                            Scale3D = new Vector3D(GetTPNEValue.Control * 100, GetTPNEValue.Control * 100, GetTPNEValue.Control * 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(Index, t.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList[Index], t);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
            else if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        ItemRoute_PGS.HPTIValue.TPTIValue GetTPTIValue = KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList[Index];

                        HTK_3DES.Transform t = new HTK_3DES.Transform
                        {
                            Translate3D = new Vector3D(GetTPTIValue.TPTI_Positions.X, GetTPTIValue.TPTI_Positions.Y, GetTPTIValue.TPTI_Positions.Z),
                            Scale3D = new Vector3D(GetTPTIValue.TPTI_PointSize * 100, GetTPTIValue.TPTI_PointSize * 100, GetTPTIValue.TPTI_PointSize * 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(Index, t.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList[Index], t);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
            else if (KMPSectionComboBox.Text == "CheckPoint")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        #region Point Left
                        Checkpoint_PGS.HPKCValue.TPKCValue GetTPKCValue_Left = KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList[Index];

                        HTK_3DES.Transform t_Left = new HTK_3DES.Transform
                        {
                            Translate3D = new Vector3D(GetTPKCValue_Left.Position_2D_Left.X, float.Parse(KMP_CheckpointHeightOffset_TXT.Text), GetTPKCValue_Left.Position_2D_Left.Y),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D_tLeft = new HTK_3DES.TSRSystem3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.BasePointModelList[Index], t_Left);
                        tSRSystem3D_tLeft.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //パスの形を変更(Green)
                        HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint_Left = KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex];
                        if (checkpoint_Left.Checkpoint_Left.LV3D_List.Count != 0) checkpoint_Left.Checkpoint_Left.MoveRails(Index, t_Left.Translate3D, HTK_3DES.PathTools.Rail.RailType.Line);
                        KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line[Index].Points[0] = t_Left.Translate3D.ToPoint3D();

                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[Index].Content).Positions[2] = new Point3D(t_Left.Translate3D.X, 0, t_Left.Translate3D.Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[Index].Content).Positions[3] = t_Left.Translate3D.ToPoint3D();

                        if (checkpoint_Left.SideWall_Left.Count != 0) checkpoint_Left.MoveSideWalls(Index, t_Left.Translate3D, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint.SideWallType.Left);
                        #endregion

                        #region Point_Right
                        Checkpoint_PGS.HPKCValue.TPKCValue GetTPKCValue_Right = KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList[Index];

                        HTK_3DES.Transform t_Right = new HTK_3DES.Transform
                        {
                            Translate3D = new Vector3D(GetTPKCValue_Right.Position_2D_Right.X, float.Parse(KMP_CheckpointHeightOffset_TXT.Text), GetTPKCValue_Right.Position_2D_Right.Y),
                            Scale3D = new Vector3D(50, 50, 50),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        HTK_3DES.TSRSystem3D tSRSystem3D_tRight = new HTK_3DES.TSRSystem3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.BasePointModelList[Index], t_Right);
                        tSRSystem3D_tRight.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                        //パスの形を変更(Red)
                        HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint_Right = KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex];
                        if (checkpoint_Right.Checkpoint_Right.LV3D_List.Count != 0) checkpoint_Right.Checkpoint_Right.MoveRails(Index, t_Right.Translate3D, HTK_3DES.PathTools.Rail.RailType.Line);
                        KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line[Index].Points[1] = t_Right.Translate3D.ToPoint3D();

                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[Index].Content).Positions[0] = new Point3D(t_Right.Translate3D.X, 0, t_Right.Translate3D.Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[Index].Content).Positions[1] = t_Right.Translate3D.ToPoint3D();

                        if (checkpoint_Right.SideWall_Right.Count != 0) checkpoint_Right.MoveSideWalls(KMP_Point_ListBox.SelectedIndex, t_Right.Translate3D, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint.SideWallType.Right);
                        #endregion
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
            else if (KMPSectionComboBox.Text == "Object")
            {
                int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    render.MainViewPort.Children.Remove(KMPViewportObject.GameObject_MV3DList[Index]);
                    KMPViewportObject.GameObject_MV3DList.Remove(KMPViewportObject.GameObject_MV3DList[Index]);

                    KMPObject_PGS.JBOGValue GetJBOGValue = KMP_Main_PGS.JBOG_Section.JBOGValueList[Index];

                    List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDB_FindName = KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml").ObjFlows;
                    KMP_Main_PGS.JBOG_Section.JBOGValueList[Index].ObjectName = ObjFlowDB_FindName.Find(x => x.ObjectID == GetJBOGValue.ObjectID).ObjectName;

                    #region Add Model(OBJ)
                    HTK_3DES.Transform OBJ_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = GetJBOGValue.Positions.GetVector3D(),
                        Scale3D = HTK_3DES.ScaleFactor(GetJBOGValue.Scales.GetVector3D(), 2),
                        Rotate3D = GetJBOGValue.Rotations.GetVector3D()
                    };

                    List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDB = KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml").ObjFlows;
                    ModelVisual3D dv3D_OBJ = HTK_3DES.OBJReader(ObjFlowDB.Find(x => x.ObjectID == GetJBOGValue.ObjectID).Path);

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_OBJ, "GameObject " + Index + " " + -1);

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    KMPViewportObject.GameObject_MV3DList.Insert(Index, dv3D_OBJ);
                    render.MainViewPort.Children.Insert(Index, dv3D_OBJ);
                    #endregion

                    //VisibleCheck
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetJBOGValue.IsViewportVisible, render, KMPViewportObject.GameObject_MV3DList[Index]);
                }

                //Update ListBox
                KMP_Point_ListBox.Items.Clear();
                KMP_Point_ListBox.Items.AddRange(KMP_Main_PGS.JBOG_Section.JBOGValueList.ToArray());

                foreach (var Index in SelectedItemIndexArray) KMP_Point_ListBox.SelectedIndices.Add(Index);
            }
            else if (KMPSectionComboBox.Text == "Route")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        Route_PGS.ITOP_Route.ITOP_Point GetITOPValue = KMP_Main_PGS.ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList[Index];

                        HTK_3DES.Transform t = new HTK_3DES.Transform
                        {
                            Translate3D = new Vector3D(GetITOPValue.Positions.X, GetITOPValue.Positions.Y, GetITOPValue.Positions.Z),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(Index, t.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList[Index], t);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
            else if (KMPSectionComboBox.Text == "Area")
            {
                int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    render.MainViewPort.Children.Remove(KMPViewportObject.Area_MV3DList[Index]);
                    KMPViewportObject.Area_MV3DList.Remove(KMPViewportObject.Area_MV3DList[Index]);

                    Area_PGS.AERAValue GetAERAValue = KMP_Main_PGS.AERA_Section.AERAValueList[Index];

                    #region Add Model(OBJ)
                    HTK_3DES.Transform Area_transform_Value = new HTK_3DES.Transform
                    {
                        Translate3D = GetAERAValue.Positions.GetVector3D(),
                        Scale3D = HTK_3DES.ScaleFactor(GetAERAValue.Scales.GetVector3D(), 2000),
                        Rotate3D = GetAERAValue.Rotations.GetVector3D()
                    };

                    ModelVisual3D dv3D_AreaOBJ = null;
                    if (GetAERAValue.AreaModeSettings.AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    else if (GetAERAValue.AreaModeSettings.AreaModeValue == 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomCylinderVisual3D(Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                    else if (GetAERAValue.AreaModeSettings.AreaModeValue > 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                    //モデルの名前と番号を文字列に格納(情報化)
                    HTK_3DES.SetString_MV3D(dv3D_AreaOBJ, "Area " + Index + " " + -1);

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    KMPViewportObject.Area_MV3DList.Insert(Index, dv3D_AreaOBJ);
                    render.MainViewPort.Children.Insert(Index, dv3D_AreaOBJ);
                    #endregion

                    //VisibleCheck
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetAERAValue.IsViewportVisible, render, KMPViewportObject.Area_MV3DList[Index]);
                }
            }
            else if (KMPSectionComboBox.Text == "Camera")
            {
                int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    Camera_PGS.EMACValue GetEMACValue = KMP_Main_PGS.EMAC_Section.EMACValueList[Index];

                    HTK_3DES.Transform t = new HTK_3DES.Transform
                    {
                        Translate3D = new Vector3D(GetEMACValue.Positions.X, GetEMACValue.Positions.Y, GetEMACValue.Positions.Z),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = new Vector3D(GetEMACValue.Rotations.X, GetEMACValue.Rotations.Y, GetEMACValue.Rotations.Z)
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.Camera_MV3DList[Index], t);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //VisibleCheck
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetEMACValue.IsViewportVisible, render, KMPViewportObject.Camera_MV3DList[Index]);
                }
            }
            else if (KMPSectionComboBox.Text == "JugemPoint")
            {
                int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                {
                    RespawnPoint_PGS.TPGJValue GetTPGJValue = KMP_Main_PGS.TPGJ_Section.TPGJValueList[Index];

                    HTK_3DES.Transform t = new HTK_3DES.Transform
                    {
                        Translate3D = new Vector3D(GetTPGJValue.Positions.X, GetTPGJValue.Positions.Y, GetTPGJValue.Positions.Z),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = new Vector3D(GetTPGJValue.Rotations.X, GetTPGJValue.Rotations.Y, GetTPGJValue.Rotations.Z)
                    };

                    HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.RespawnPoint_MV3DList[Index], t);
                    tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);

                    //VisibleCheck
                    ViewPortObjVisibleSetting.ViewportObj_Visibility(GetTPGJValue.IsViewportVisible, render, KMPViewportObject.RespawnPoint_MV3DList[Index]);
                }
            }
            else if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    int[] SelectedItemIndexArray = KMP_Point_ListBox.SelectedIndices.Cast<int>().ToArray();
                    foreach (var Index in SelectedItemIndexArray.Reverse().ToArray())
                    {
                        GlideRoute_PGS.HPLGValue.TPLGValue GetTPLGValue = KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList[Index];

                        HTK_3DES.Transform t = new HTK_3DES.Transform
                        {
                            Translate3D = new Vector3D(GetTPLGValue.Positions.X, GetTPLGValue.Positions.Y, GetTPLGValue.Positions.Z),
                            Scale3D = HTK_3DES.ScaleFactor(GetTPLGValue.TPLG_PointScaleValue, 100),
                            Rotate3D = new Vector3D(0, 0, 0)
                        };

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(Index, t.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        HTK_3DES.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem3D(KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].BasePointModelList[Index], t);
                        tSRSystem3D.Transform3D(HTK_3DES.TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), HTK_3DES.TSRSystem3D.RotationType.Radian);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
        }

        private void closeKMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Path
            FilePath = "";
            this.Text = FormTitle;

            KMP_Main_PGS = null;

            for (int del = 0; del < KMPViewportObject.StartPosition_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.StartPosition_MV3DList[del]);
            KMPViewportObject.StartPosition_MV3DList.Clear();

            for (int del = 0; del < KMPViewportObject.EnemyRoute_Rail_List.Count; del++) KMPViewportObject.EnemyRoute_Rail_List[del].DeleteRail(render);
            KMPViewportObject.EnemyRoute_Rail_List.Clear();

            for (int del = 0; del < KMPViewportObject.ItemRoute_Rail_List.Count; del++) KMPViewportObject.ItemRoute_Rail_List[del].DeleteRail(render);
            KMPViewportObject.ItemRoute_Rail_List.Clear();

            for (int del = 0; del < KMPViewportObject.Checkpoint_Rail.Count; del++) KMPViewportObject.Checkpoint_Rail[del].DeleteRailChk(render);
            KMPViewportObject.Checkpoint_Rail.Clear();

            for (int del = 0; del < KMPViewportObject.GameObject_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.GameObject_MV3DList[del]);
            KMPViewportObject.GameObject_MV3DList.Clear();

            for (int del = 0; del < KMPViewportObject.Routes_List.Count; del++) KMPViewportObject.Routes_List[del].DeleteRail(render);
            KMPViewportObject.Routes_List.Clear();

            for (int del = 0; del < KMPViewportObject.Area_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.Area_MV3DList[del]);
            KMPViewportObject.Area_MV3DList.Clear();

            for (int del = 0; del < KMPViewportObject.Camera_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.Camera_MV3DList[del]);
            KMPViewportObject.Camera_MV3DList.Clear();

            for (int del = 0; del < KMPViewportObject.RespawnPoint_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.RespawnPoint_MV3DList[del]);
            KMPViewportObject.RespawnPoint_MV3DList.Clear();

            for (int del = 0; del < KMPViewportObject.GlideRoute_Rail_List.Count; del++) KMPViewportObject.GlideRoute_Rail_List[del].DeleteRail(render);
            KMPViewportObject.GlideRoute_Rail_List.Clear();

            KMP_Group_ListBox.Items.Clear();
            KMP_Point_ListBox.Items.Clear();
            propertyGrid_KMP_Group.SelectedObject = null;
            propertyGrid_KMP_Path.SelectedObject = null;
            propertyGrid_KMP_StageInfo.SelectedObject = null;
            KMPVersion_TXT.Text = "3100";
            KMPSectionComboBox.Items.Clear();

            writeBinaryToolStripMenuItem.Enabled = false;
            closeKMPToolStripMenuItem.Enabled = false;
            exportToolStripMenuItem.Enabled = false;

            inputXmlAsXXXXToolStripMenuItem.Enabled = false;
            xXXXRouteImporterToolStripMenuItem.Enabled = false;
        }

        private void KMPVisibility_CheckedChanged(object sender, EventArgs e)
        {
            if (KMP_Main_PGS != null)
            {
                KMPSection_Visibility = new ViewPortObjVisibleSetting.KMPSectionVisibility
                {
                    Kartpoint = CH_Kartpoint.Checked,
                    EnemyRoutes = CH_EnemyRoutes.Checked,
                    ItemRoutes = CH_ItemRoutes.Checked,
                    Checkpoint = CH_Checkpoint.Checked,
                    GameObject = CH_GameObject.Checked,
                    Routes = CH_Routes.Checked,
                    Area = CH_Area.Checked,
                    Camera = CH_Camera.Checked,
                    Returnpoints = CH_Returnpoints.Checked,
                    GlideRoutes = CH_GlideRoutes.Checked
                };

                ViewPortObjVisibleSetting.CheckKMPVisibility(render, KMPSection_Visibility, KMP_Main_PGS, KMPViewportObject);
            }
            else if (KMP_Main_PGS == null)
            {
                KMPSection_Visibility = new ViewPortObjVisibleSetting.KMPSectionVisibility
                {
                    Kartpoint = CH_Kartpoint.Checked,
                    EnemyRoutes = CH_EnemyRoutes.Checked,
                    ItemRoutes = CH_ItemRoutes.Checked,
                    Checkpoint = CH_Checkpoint.Checked,
                    GameObject = CH_GameObject.Checked,
                    Routes = CH_Routes.Checked,
                    Area = CH_Area.Checked,
                    Camera = CH_Camera.Checked,
                    Returnpoints = CH_Returnpoints.Checked,
                    GlideRoutes = CH_GlideRoutes.Checked
                };
            }
        }

        private void createKMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Path
            //FilePath = Open_KMP.FileName;
            this.Text = FormTitle + " [File : Untitled ]";

            //TODO : グリッドが消されないようにする
            //render.MainViewPort.Children.Clear();
            KMP_Main_PGS = new KMP_Main_PGS();
            KMPVersion_TXT.Text = "3100";

            propertyGrid_KMP_StageInfo.SelectedObject = KMP_Main_PGS.IGTS_Section;

            if (KMPSectionComboBox.Items.Count == 0)
            {
                string[] AllSectionAry = new string[] { "KartPoint", "EnemyRoutes", "ItemRoutes", "CheckPoint", "Object", "Route", "Area", "Camera", "JugemPoint", "GlideRoutes" };
                KMPSectionComboBox.Items.AddRange(AllSectionAry.ToArray());
            }

            SectionVisibleCheck(true);

            KMPSectionComboBox.SelectedIndex = 0;
            writeBinaryToolStripMenuItem.Enabled = true;
            closeKMPToolStripMenuItem.Enabled = true;
            exportToolStripMenuItem.Enabled = true;
            inputXmlAsXXXXToolStripMenuItem.Enabled = true;
        }

        private void allSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save_KMPXML = new SaveFileDialog()
            {
                Title = "Save XML(KMP)",
                FileName = EditorSetting.FilePathSetting.DefaultXMLFileName,
                InitialDirectory = EditorSetting.FilePathSetting.DefaultDirectory,
                Filter = "xml file|*.xml"
            };

            if (Save_KMPXML.ShowDialog() == DialogResult.OK)
            {
                KMPLibrary.XMLConvert.Statics.KMP.ExportAll(KMP_Main_PGS.ToKMP(Convert.ToUInt32(KMPVersion_TXT.Text)), Save_KMPXML.FileName);
            }
            else return;
        }

        private void outputXXXXAsXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save_KMPXML = new SaveFileDialog()
            {
                Title = "Save XML(KMP)",
                FileName = EditorSetting.FilePathSetting.DefaultXMLFileName,
                InitialDirectory = EditorSetting.FilePathSetting.DefaultDirectory,
                Filter = "xml file|*.xml"
            };

            if (Save_KMPXML.ShowDialog() == DialogResult.OK)
            {
                var KMPData = KMP_Main_PGS.ToKMP(Convert.ToUInt32(KMPVersion_TXT.Text));
                if (KMPSectionComboBox.Text == "KartPoint") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.KartPoint);
                else if (KMPSectionComboBox.Text == "EnemyRoutes") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.EnemyRoutes);
                else if (KMPSectionComboBox.Text == "ItemRoutes") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.ItemRoutes);
                else if (KMPSectionComboBox.Text == "CheckPoint") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.CheckPoint);
                else if (KMPSectionComboBox.Text == "Object") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Object);
                else if (KMPSectionComboBox.Text == "Route") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Route);
                else if (KMPSectionComboBox.Text == "Area") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Area);
                else if (KMPSectionComboBox.Text == "Camera") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.Camera);
                else if (KMPSectionComboBox.Text == "JugemPoint") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.JugemPoint);
                else if (KMPSectionComboBox.Text == "GlideRoutes") KMPLibrary.XMLConvert.Statics.KMP.ExportSection(KMPData, Save_KMPXML.FileName, KMPLibrary.XMLConvert.KMPData.KMPXmlSetting.Section.GlideRoutes);
            }
            else return;
        }

        private void XXXXRouteExporterTSM_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save_KMPXML = new SaveFileDialog()
            {
                Title = "Save XML(KMP)",
                FileName = EditorSetting.FilePathSetting.DefaultXMLFileName,
                InitialDirectory = EditorSetting.FilePathSetting.DefaultDirectory,
                Filter = "xml file|*.xml"
            };

            if (Save_KMPXML.ShowDialog() == DialogResult.OK)
            {
                var KMPData = KMP_Main_PGS.ToKMP(Convert.ToUInt32(KMPVersion_TXT.Text));

                if (KMPSectionComboBox.Text == "EnemyRoutes") KMPLibrary.XMLConvert.Statics.KMP.ExportXXXXRoute(KMPData, Save_KMPXML.FileName, XXXXRouteXmlSetting.RouteType.EnemyRoute);
                else if (KMPSectionComboBox.Text == "ItemRoutes") KMPLibrary.XMLConvert.Statics.KMP.ExportXXXXRoute(KMPData, Save_KMPXML.FileName, XXXXRouteXmlSetting.RouteType.ItemRoute);
                else if (KMPSectionComboBox.Text == "GlideRoutes") KMPLibrary.XMLConvert.Statics.KMP.ExportXXXXRoute(KMPData, Save_KMPXML.FileName, XXXXRouteXmlSetting.RouteType.GlideRoute);
            }
            else return;
        }

        private void ImportAllSectionTSM_Click(object sender, EventArgs e)
        {
            OpenFileDialog Open_KMPXML = new OpenFileDialog()
            {
                Title = "Open Xml",
                FileName = EditorSetting.FilePathSetting.DefaultXMLFileName,
                InitialDirectory = EditorSetting.FilePathSetting.DefaultDirectory,
                Filter = "xml file|*.xml"
            };

            if (Open_KMPXML.ShowDialog() == DialogResult.OK)
            {
                KMPViewportObject = new KMPRendering.KMPViewportObject();
                KMPSectionComboBox.Items.Clear();
                KMP_Main_PGS = null;

                var KMP_Xml_Model = KMPLibrary.XMLConvert.IO.XML_Importer.XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Open_KMPXML.FileName);

                KMP_Main_PGS = new KMP_Main_PGS(KMP_Xml_Model);

                #region Render
                Render.KMPRendering.KMPViewportRenderingXML.Render_StartPosition(render, KMPViewportObject, KMP_Xml_Model.startPositions);
                Render.KMPRendering.KMPViewportRenderingXML.Render_EnemyRoute(render, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
                Render.KMPRendering.KMPViewportRenderingXML.Render_ItemRoute(render, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
                Render.KMPRendering.KMPViewportRenderingXML.Render_Checkpoint(render, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text));
                Render.KMPRendering.KMPViewportRenderingXML.Render_Object(render, KMPViewportObject, KMP_Xml_Model.Objects, KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml").ObjFlows);
                Render.KMPRendering.KMPViewportRenderingXML.Render_Route(render, KMPViewportObject, KMP_Xml_Model.Routes);
                Render.KMPRendering.KMPViewportRenderingXML.Render_Area(render, KMPViewportObject, KMP_Xml_Model.Areas);
                Render.KMPRendering.KMPViewportRenderingXML.Render_Camera(render, KMPViewportObject, KMP_Xml_Model.Cameras);
                Render.KMPRendering.KMPViewportRenderingXML.Render_Returnpoint(render, KMPViewportObject, KMP_Xml_Model.JugemPoints);
                Render.KMPRendering.KMPViewportRenderingXML.Render_GlideRoute(render, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
                #endregion

                #region Visibility
                KMPSection_Visibility = new ViewPortObjVisibleSetting.KMPSectionVisibility
                {
                    Kartpoint = CH_Kartpoint.Checked,
                    EnemyRoutes = CH_EnemyRoutes.Checked,
                    ItemRoutes = CH_ItemRoutes.Checked,
                    Checkpoint = CH_Checkpoint.Checked,
                    GameObject = CH_GameObject.Checked,
                    Routes = CH_Routes.Checked,
                    Area = CH_Area.Checked,
                    Camera = CH_Camera.Checked,
                    Returnpoints = CH_Returnpoints.Checked,
                    GlideRoutes = CH_GlideRoutes.Checked
                };

                ViewPortObjVisibleSetting.CheckKMPVisibility(render, KMPSection_Visibility, KMP_Main_PGS, KMPViewportObject);
                #endregion

                string[] AllSectionAry = new string[] { "KartPoint", "EnemyRoutes", "ItemRoutes", "CheckPoint", "Object", "Route", "Area", "Camera", "JugemPoint", "GlideRoutes" };
                KMPSectionComboBox.Items.AddRange(AllSectionAry.ToArray());
                KMPSectionComboBox.SelectedIndex = 0;

                if (KMP_Main_PGS.IGTS_Section != null)
                {
                    //Display only IGTS section directly to PropertyGrid
                    propertyGrid_KMP_StageInfo.SelectedObject = KMP_Main_PGS.IGTS_Section;
                }

                writeBinaryToolStripMenuItem.Enabled = true;
                closeKMPToolStripMenuItem.Enabled = true;
                exportToolStripMenuItem.Enabled = true;
                xXXXRouteImporterToolStripMenuItem.Enabled = true;
                inputXmlAsXXXXToolStripMenuItem.Enabled = true;

                //Path
                //FilePath = "";
                this.Text = FormTitle + " [File : Untitled ]";
            }
            else return;
        }

        private void InputXmlAsXXXXTSM_Click(object sender, EventArgs e)
        {
            OpenFileDialog Open_KMPXMLSection = new OpenFileDialog()
            {
                Title = "Open KMP Section (XML)",
                FileName = EditorSetting.FilePathSetting.DefaultXMLFileName,
                InitialDirectory = EditorSetting.FilePathSetting.DefaultDirectory,
                Filter = "xml file|*.xml"
            };

            if (Open_KMPXMLSection.ShowDialog() == DialogResult.OK)
            {
                var KMP_Xml_Model = KMPLibrary.XMLConvert.IO.XML_Importer.XMLImport<KMPLibrary.XMLConvert.KMPData.KMP_XML>(Open_KMPXMLSection.FileName);

                if (KMPSectionComboBox.Text == "KartPoint")
                {
                    for (int del = 0; del < KMPViewportObject.StartPosition_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.StartPosition_MV3DList[del]);
                    KMPViewportObject.StartPosition_MV3DList.Clear();

                    KMPViewportObject.StartPosition_MV3DList = new List<ModelVisual3D>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_StartPosition(render, KMPViewportObject, KMP_Xml_Model.startPositions);
                    KMP_Main_PGS.TPTK_Section = new KartPoint_PGS(KMP_Xml_Model.startPositions);
                }
                else if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    for (int del = 0; del < KMPViewportObject.EnemyRoute_Rail_List.Count; del++) KMPViewportObject.EnemyRoute_Rail_List[del].DeleteRail(render);
                    KMPViewportObject.EnemyRoute_Rail_List.Clear();

                    KMPViewportObject.EnemyRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_EnemyRoute(render, KMPViewportObject, KMP_Xml_Model.EnemyRoutes);
                    KMP_Main_PGS.HPNE_TPNE_Section = new EnemyRoute_PGS(KMP_Xml_Model.EnemyRoutes);
                }
                else if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    for (int del = 0; del < KMPViewportObject.ItemRoute_Rail_List.Count; del++) KMPViewportObject.ItemRoute_Rail_List[del].DeleteRail(render);
                    KMPViewportObject.ItemRoute_Rail_List.Clear();

                    KMPViewportObject.ItemRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_ItemRoute(render, KMPViewportObject, KMP_Xml_Model.ItemRoutes);
                    KMP_Main_PGS.HPTI_TPTI_Section = new ItemRoute_PGS(KMP_Xml_Model.ItemRoutes);
                }
                else if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    for (int del = 0; del < KMPViewportObject.Checkpoint_Rail.Count; del++) KMPViewportObject.Checkpoint_Rail[del].DeleteRailChk(render);
                    KMPViewportObject.Checkpoint_Rail.Clear();

                    KMPViewportObject.Checkpoint_Rail = new List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_Checkpoint(render, KMPViewportObject, KMP_Xml_Model.Checkpoints, Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text));
                    KMP_Main_PGS.HPKC_TPKC_Section = new Checkpoint_PGS(KMP_Xml_Model.Checkpoints);
                }
                else if (KMPSectionComboBox.Text == "Object")
                {
                    for (int del = 0; del < KMPViewportObject.GameObject_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.GameObject_MV3DList[del]);
                    KMPViewportObject.GameObject_MV3DList.Clear();

                    KMPViewportObject.GameObject_MV3DList = new List<ModelVisual3D>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_Object(render, KMPViewportObject, KMP_Xml_Model.Objects, KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml").ObjFlows);
                    KMP_Main_PGS.JBOG_Section = new KMPObject_PGS(KMP_Xml_Model.Objects);
                }
                else if (KMPSectionComboBox.Text == "Route")
                {
                    for (int del = 0; del < KMPViewportObject.Routes_List.Count; del++) KMPViewportObject.Routes_List[del].DeleteRail(render);
                    KMPViewportObject.Routes_List.Clear();

                    KMPViewportObject.Routes_List = new List<HTK_3DES.PathTools.Rail>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_Route(render, KMPViewportObject, KMP_Xml_Model.Routes);
                    KMP_Main_PGS.ITOP_Section = new Route_PGS(KMP_Xml_Model.Routes);
                }
                else if (KMPSectionComboBox.Text == "Area")
                {
                    for (int del = 0; del < KMPViewportObject.Area_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.Area_MV3DList[del]);
                    KMPViewportObject.Area_MV3DList.Clear();

                    KMPViewportObject.Area_MV3DList = new List<ModelVisual3D>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_Area(render, KMPViewportObject, KMP_Xml_Model.Areas);
                    KMP_Main_PGS.AERA_Section = new Area_PGS(KMP_Xml_Model.Areas);
                }
                else if (KMPSectionComboBox.Text == "Camera")
                {
                    for (int del = 0; del < KMPViewportObject.Camera_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.Camera_MV3DList[del]);
                    KMPViewportObject.Camera_MV3DList.Clear();

                    KMPViewportObject.Camera_MV3DList = new List<ModelVisual3D>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_Camera(render, KMPViewportObject, KMP_Xml_Model.Cameras);
                    KMP_Main_PGS.EMAC_Section = new Camera_PGS(KMP_Xml_Model.Cameras);
                }
                else if (KMPSectionComboBox.Text == "JugemPoint")
                {
                    for (int del = 0; del < KMPViewportObject.RespawnPoint_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.RespawnPoint_MV3DList[del]);
                    KMPViewportObject.RespawnPoint_MV3DList.Clear();

                    KMPViewportObject.RespawnPoint_MV3DList = new List<ModelVisual3D>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_Returnpoint(render, KMPViewportObject, KMP_Xml_Model.JugemPoints);
                    KMP_Main_PGS.TPGJ_Section = new RespawnPoint_PGS(KMP_Xml_Model.JugemPoints);
                }
                else if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    for (int del = 0; del < KMPViewportObject.GlideRoute_Rail_List.Count; del++) KMPViewportObject.GlideRoute_Rail_List[del].DeleteRail(render);
                    KMPViewportObject.GlideRoute_Rail_List.Clear();

                    KMPViewportObject.GlideRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                    Render.KMPRendering.KMPViewportRenderingXML.Render_GlideRoute(render, KMPViewportObject, KMP_Xml_Model.GlideRoutes);
                    KMP_Main_PGS.HPLG_TPLG_Section = new GlideRoute_PGS(KMP_Xml_Model.GlideRoutes);
                }

                KMPSectionComboBox.SelectedIndex = 0;
            }
            else return;
        }

        private void xXXXRouteImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenKMPSection_RouteDataOnly = new OpenFileDialog()
            {
                Title = "Open KMP Path (XML)",
                FileName = EditorSetting.FilePathSetting.DefaultXMLFileName,
                InitialDirectory = EditorSetting.FilePathSetting.DefaultDirectory,
                Filter = "xml file|*.xml"
            };

            if (OpenKMPSection_RouteDataOnly.ShowDialog() == DialogResult.OK)
            {
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    for (int del = 0; del < KMPViewportObject.EnemyRoute_Rail_List.Count; del++) KMPViewportObject.EnemyRoute_Rail_List[del].DeleteRail(render);
                    KMPViewportObject.EnemyRoute_Rail_List.Clear();

                    KMPViewportObject.EnemyRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                    XXXXRoute_XML XXXXRouteXml_Model = KMPLibrary.XMLConvert.IO.XML_Importer.XMLImport<XXXXRoute_XML>(OpenKMPSection_RouteDataOnly.FileName);
                    Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_EnemyRoute(render, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
                    KMP_Main_PGS.HPNE_TPNE_Section = new EnemyRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
                }
                else if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    for (int del = 0; del < KMPViewportObject.ItemRoute_Rail_List.Count; del++) KMPViewportObject.ItemRoute_Rail_List[del].DeleteRail(render);
                    KMPViewportObject.ItemRoute_Rail_List.Clear();

                    KMPViewportObject.ItemRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                    XXXXRoute_XML XXXXRouteXml_Model = KMPLibrary.XMLConvert.IO.XML_Importer.XMLImport<XXXXRoute_XML>(OpenKMPSection_RouteDataOnly.FileName);
                    Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_ItemRoute(render, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
                    KMP_Main_PGS.HPTI_TPTI_Section = new ItemRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
                }
                else if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    for (int del = 0; del < KMPViewportObject.GlideRoute_Rail_List.Count; del++) KMPViewportObject.GlideRoute_Rail_List[del].DeleteRail(render);
                    KMPViewportObject.GlideRoute_Rail_List.Clear();

                    KMPViewportObject.GlideRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                    XXXXRoute_XML XXXXRouteXml_Model = KMPLibrary.XMLConvert.IO.XML_Importer.XMLImport<XXXXRoute_XML>(OpenKMPSection_RouteDataOnly.FileName);
                    Render.KMPRendering.KMPViewportRenderingXML_XXXXRoute.Render_GlideRoute(render, KMPViewportObject, XXXXRouteXml_Model.XXXXRoutes);
                    KMP_Main_PGS.HPLG_TPLG_Section = new GlideRoute_PGS(XXXXRouteXml_Model.XXXXRoutes);
                }

                KMPSectionComboBox.SelectedIndex = 0;
            }
            else return;
        }

        private void KMP_Path_ListBox_DoubleClick(object sender, EventArgs e)
        {
            if (KMPSectionComboBox.Text == "KartPoint")
            {
                render.FindObject(KMP_Main_PGS.TPTK_Section.TPTKValueList, KMP_Point_ListBox.SelectedIndex);
            }
            else if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                if (KMPSection_Main_TabCtrl.SelectedIndex == 1) render.FindObject(KMP_Main_PGS.HPNE_TPNE_Section.HPNEValueList, KMP_Point_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex);
            }
            else if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                if (KMPSection_Main_TabCtrl.SelectedIndex == 1) render.FindObject(KMP_Main_PGS.HPTI_TPTI_Section.HPTIValueList, KMP_Point_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex);
            }
            else if (KMPSectionComboBox.Text == "CheckPoint")
            {
                if (KMPChkpt_RDTBtn_L.Checked == true)
                {
                    double OffsetValue = Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text);
                    render.FindObject(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList, KMP_Point_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex, UserControl1.CheckpointSearchOption.Left, OffsetValue);
                }
                else if (KMPChkpt_RDTBtn_R.Checked == true)
                {
                    double OffsetValue = Convert.ToDouble(KMP_CheckpointHeightOffset_TXT.Text);
                    render.FindObject(KMP_Main_PGS.HPKC_TPKC_Section.HPKCValueList, KMP_Point_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex, UserControl1.CheckpointSearchOption.Right, OffsetValue);
                }
            }
            else if (KMPSectionComboBox.Text == "Object")
            {
                render.FindObject(KMP_Main_PGS.JBOG_Section.JBOGValueList, KMP_Point_ListBox.SelectedIndex);
            }
            else if (KMPSectionComboBox.Text == "Route")
            {
                if (KMPSection_Main_TabCtrl.SelectedIndex == 1) render.FindObject(KMP_Main_PGS.ITOP_Section.ITOP_RouteList, KMP_Point_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex);
            }
            else if (KMPSectionComboBox.Text == "Area")
            {
                render.FindObject(KMP_Main_PGS.AERA_Section.AERAValueList, KMP_Point_ListBox.SelectedIndex);
            }
            else if (KMPSectionComboBox.Text == "Camera")
            {
                render.FindObject(KMP_Main_PGS.EMAC_Section.EMACValueList, KMP_Point_ListBox.SelectedIndex);
            }
            else if (KMPSectionComboBox.Text == "JugemPoint")
            {
                render.FindObject(KMP_Main_PGS.TPGJ_Section.TPGJValueList, KMP_Point_ListBox.SelectedIndex);
            }
            else if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                if (KMPSection_Main_TabCtrl.SelectedIndex == 1) render.FindObject(KMP_Main_PGS.HPLG_TPLG_Section.HPLGValueList, KMP_Point_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex);
            }
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (KMP_Main_PGS != null)
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("All", KMP_Main_PGS);
                kMPErrorCheck.Show();
            }
        }

        private void thisSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (KMP_Main_PGS != null)
            {
                if (KMPSectionComboBox.Text == "KartPoint")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Kart Point", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Enemy Route", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Item Route", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Checkpoint", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "Object")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Object", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "Route")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Route", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "Area")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Area", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "Camera")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Camera", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "JugemPoint")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Jugem Point", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
                else if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Glide Route", KMP_Main_PGS);
                    kMPErrorCheck.Show();
                }
            }
        }

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            KMP3DEditorInfoForm kMP3DEditorInfoForm = new KMP3DEditorInfoForm();
            kMP3DEditorInfoForm.ShowDialog();
        }

        private void AddObjectPosSetting_CheckedChanged(object sender, EventArgs e)
        {
            if (OnElementPos_RadioBtn.Checked == true)
            {
                MouseCursor_RadioBtn.Checked = false;
                CameraPosition_RadioBtn.Checked = false;
            }
            else if (MouseCursor_RadioBtn.Checked == true)
            {
                OnElementPos_RadioBtn.Checked = false;
                CameraPosition_RadioBtn.Checked = false;
            }
            else if (CameraPosition_RadioBtn.Checked == true)
            {
                OnElementPos_RadioBtn.Checked = false;
                MouseCursor_RadioBtn.Checked = false;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorSettings.EditorSettingForm editorSettingForm = new EditorSettings.EditorSettingForm(EditorSetting);
            editorSettingForm.ShowDialog();

            EditorSetting = editorSettingForm.EditorSettingXML;

            string CD = System.IO.Directory.GetCurrentDirectory();
            if (Directory.Exists(CD + "\\Settings") == false)
            {
                Directory.CreateDirectory(CD + "\\Settings");
                XML_Exporter.XMLExport(CD + "\\Settings\\EditorSetting.xml", EditorSetting, XML_Exporter.EmptyXmlSerializerNamespaces());
            }
            else if ((Directory.Exists(CD + "\\Settings") == true && File.Exists(CD + "\\Settings\\EditorSetting.xml") == false) == true)
            {
                XML_Exporter.XMLExport(CD + "\\Settings\\EditorSetting.xml", EditorSetting, XML_Exporter.EmptyXmlSerializerNamespaces());
            }
        }
    }
}