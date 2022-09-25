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

namespace MK7_KMP_Editor_For_PG_
{
    public partial class Form1 : Form
    {
        //UserControl1.xamlの初期化
        //ここは作成時の名前にも影響されるので必ず確認すること
        public UserControl1 render = new UserControl1();

        #region MV3DList
        KMPs.KMPViewportObject KMPViewportObject = new KMPs.KMPViewportObject();

        //Course Object
        public Dictionary<string, ArrayList> MV3D_Dictionary = new Dictionary<string, ArrayList>();
        #endregion

        public KMPPropertyGridSettings.TPTK_Section TPTK_Section { get; set; }
        public KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section { get; set; }
        public KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section { get; set; }
        public KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section { get; set; }
        public KMPPropertyGridSettings.JBOG_Section JBOG_Section { get; set; }
        public KMPPropertyGridSettings.ITOP_Section ITOP_Section { get; set; }
        public KMPPropertyGridSettings.AERA_Section AERA_Section { get; set; }
        public KMPPropertyGridSettings.EMAC_Section EMAC_Section { get; set; }
        public KMPPropertyGridSettings.TPGJ_Section TPGJ_Section { get; set; }
        public KMPPropertyGridSettings.IGTS_Section IGTS_Section { get; set; }
        public KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section { get; set; }

        public Form1()
        {
            InitializeComponent();

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

            CH_KMPGroupPoint.Enabled = false;

            string CD = System.IO.Directory.GetCurrentDirectory();
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
            OpenFileDialog openFileDialog2 = new OpenFileDialog()
            {
                Title = "Open Model",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "obj file|*.obj"
            };

            if (openFileDialog2.ShowDialog() != DialogResult.OK) return;

            MV3D_Dictionary = HTK_3DES.OBJData.OBJReader_AryListDictionary(openFileDialog2.FileName);

            foreach(var i in MV3D_Dictionary)
            {
                render.MainViewPort.Children.Add((ModelVisual3D)MV3D_Dictionary[i.Key][1]);
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
            foreach(var f in MV3D_Dictionary)
            {
                render.MainViewPort.Children.Remove((ModelVisual3D)f.Value[1]);
            }

            MV3D_Dictionary.Clear();

            closeObjToolStripMenuItem.Enabled = false;
            visibilityToolStripMenuItem.Enabled = false;
        }
        #endregion

        HitTestResult HTR = null;
        ModelVisual3D FindMV3D = null;
        //HTK_3DES.TSRSystem.Transform_Value transform_Value = null;
        HTK_3DES.TSRSystem.Transform transform_Value = null;

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
                    if (typeof(LinesVisual3D) == HTR.VisualHit.GetType()) return;
                    if (typeof(TubeVisual3D) == HTR.VisualHit.GetType()) return;
                    if (typeof(RectangleVisual3D) == HTR.VisualHit.GetType()) return;

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
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = TPTK_Section.TPTKValueList[MDLNum].Position_Value.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = TPTK_Section.TPTKValueList[MDLNum].Rotate_Value.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "EnemyRoute")
                    {
                        //Dictionaryに存在するKeyを検索
                        if (KMPViewportObject.EnemyRoute_Rail_List[GroupNum].MV3D_List.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("EnemyRoutes");
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum].Control, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "ItemRoute")
                    {
                        if (KMPViewportObject.ItemRoute_Rail_List[GroupNum].MV3D_List.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("ItemRoutes");
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum].TPTI_Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum].TPTI_PointSize, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "Checkpoint_Left")
                    {
                        if (KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_Left.MV3D_List.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("CheckPoint");
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            var P2DLeft = HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Left;

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = new Vector3D(P2DLeft.X, Convert.ToDouble(textBox1.Text), P2DLeft.Y),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "Checkpoint_Right")
                    {
                        if (KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_Right.MV3D_List.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("CheckPoint");
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            var P2DRight = HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Right;

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = new Vector3D(P2DRight.X, Convert.ToDouble(textBox1.Text), P2DRight.Y),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "OBJ")
                    {
                        if (KMPViewportObject.OBJ_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("Obj");
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = JBOG_Section.JBOGValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(JBOG_Section.JBOGValueList[MDLNum].Scales.GetVector3D(), 2),
                                Rotate3D = JBOG_Section.JBOGValueList[MDLNum].Rotations.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "Routes")
                    {
                        if (KMPViewportObject.Routes_List[GroupNum].MV3D_List.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("Route");
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = ITOP_Section.ITOP_RouteList[GroupNum].ITOP_PointList[MDLNum].Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "Area")
                    {
                        if (KMPViewportObject.Area_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("Area");
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = AERA_Section.AERAValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(AERA_Section.AERAValueList[MDLNum].Scales.GetVector3D(), 2000),
                                Rotate3D = AERA_Section.AERAValueList[MDLNum].Rotations.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "Camera")
                    {
                        if (KMPViewportObject.Camera_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("Camera");
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = EMAC_Section.EMACValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = EMAC_Section.EMACValueList[MDLNum].Rotations.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "RespawnPoint")
                    {
                        if (KMPViewportObject.RespawnPoint_MV3DList.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("JugemPoint");
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = TPGJ_Section.TPGJValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = TPGJ_Section.TPGJValueList[MDLNum].Rotations.GetVector3D()
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (OBJ_Name == "GlideRoutes")
                    {
                        if (KMPViewportObject.GlideRoute_Rail_List[GroupNum].MV3D_List.Contains(FindMV3D))
                        {
                            #region SelectedIndex
                            KMPSectionComboBox.SelectedIndex = KMPSectionComboBox.Items.IndexOf("GlideRoutes");
                            KMP_Group_ListBox.SelectedIndex = GroupNum;
                            KMP_Path_ListBox.SelectedIndex = MDLNum;
                            tabControl1.SelectedIndex = 1;
                            #endregion

                            transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum].Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum].TPLG_PointScaleValue, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            //出力
                            Section_Name_LBL.Text = OBJ_Name;
                            Section_ID_LBL.Text = MDLNum.ToString();
                            Section_Group_ID_LBL.Text = GroupNum.ToString();
                        }
                    }
                    if (MV3D_Dictionary.ContainsKey(OBJ_Name))
                    {
                        //出力
                        Section_Name_LBL.Text = MDLStr_GetName[0];
                        Section_ID_LBL.Text = MDLStr_GetName[1] + ": None";
                        Section_Group_ID_LBL.Text = MDLStr_GetName[2] + ": None";
                    }
                }
                if (HTR == null)
                {
                    Section_Name_LBL.Text = "Not Selected.";
                    Section_ID_LBL.Text = "<!>Null<!>";
                    Section_Group_ID_LBL.Text = "<!>Null<!>";
                }
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) == true)
            {
                Point3D Pos = new Point3D();
                if (ViewportTypeChange.Checked == true)
                {
                    Pos = render.ViewportPosition(UserControl1.PositionMode.ElementPosition);
                }
                if (ViewportTypeChange.Checked == false)
                {
                    Pos = render.ViewportPosition(UserControl1.PositionMode.MouseCursor);
                }

                if (KMPSectionComboBox.Text == "KartPoint")
                {
                    KMPPropertyGridSettings.TPTK_Section.TPTKValue tPTKValue = new KMPPropertyGridSettings.TPTK_Section.TPTKValue(Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    TPTK_Section.TPTKValueList.Add(tPTKValue);

                    KMP_Path_ListBox.Items.Add(tPTKValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(StartPosition)
                        ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + tPTKValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform StartPosition_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = tPTKValue.Position_Value.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = tPTKValue.Rotate_Value.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_StartPositionOBJ, StartPosition_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        KMPViewportObject.StartPosition_MV3DList.Add(dv3D_StartPositionOBJ);

                        render.MainViewPort.Children.Add(dv3D_StartPositionOBJ);

                        HTK_3DES.TSRSystem.GC_Dispose(dv3D_StartPositionOBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue tPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.Add(tPNEValue);

                        KMP_Path_ListBox.Items.Add(tPNEValue);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Add Model(EnemyRoutes)
                            ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + tPNEValue.ID + " " + tPNEValue.Group_ID);

                            HTK_3DES.TSRSystem.Transform EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = tPNEValue.Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(tPNEValue.Control, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                            tSRSystem3D.TestTransform3D();

                            //Add Rail => MV3DList
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List.Add(dv3D_EnemyPathOBJ);

                            render.MainViewPort.Children.Add(dv3D_EnemyPathOBJ);
                            #endregion

                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Orange);

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue tPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.Add(tPTIValue);

                        KMP_Path_ListBox.Items.Add(tPTIValue);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Add Model(ItemRoutes)
                            ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + tPTIValue.ID + " " + tPTIValue.Group_ID);

                            HTK_3DES.TSRSystem.Transform ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = tPTIValue.TPTI_Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(tPTIValue.TPTI_PointSize, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                            tSRSystem3D.TestTransform3D();

                            //Add Rail => MV3DList
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List.Add(dv3D_ItemPathOBJ);

                            render.MainViewPort.Children.Add(dv3D_ItemPathOBJ);
                            #endregion

                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Green);

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        Vector2 LeftPos = new Vector2(Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Z));
                        Vector2 RightPos = new Vector2(Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Z));

                        KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue tPKCValue = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue(LeftPos, RightPos, KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.Add(tPKCValue);

                        KMP_Path_ListBox.Items.Add(tPKCValue);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Create
                            var P2D_Left = tPKCValue.Position_2D_Left;
                            Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                            Point3D P3DLeft = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DLeftToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                            P3DLeft.Y = Convert.ToDouble(textBox1.Text);

                            #region Transform(Left)
                            ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + tPKCValue.ID + " " + tPKCValue.Group_ID);

                            HTK_3DES.TSRSystem.Transform P2DLeft_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = P3DLeft.ToVector3D(),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_P2DLeft = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CheckpointLeftOBJ, P2DLeft_transform_Value);
                            tSRSystem3D_P2DLeft.TestTransform3D();

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                            render.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                            HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointLeftOBJ);
                            #endregion

                            var P2D_Right = tPKCValue.Position_2D_Right;
                            Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                            Point3D P3DRight = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DRightToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                            P3DRight.Y = Convert.ToDouble(textBox1.Text);

                            #region Transform(Right)
                            ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + tPKCValue.ID + " " + tPKCValue.Group_ID);

                            HTK_3DES.TSRSystem.Transform P2DRight_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = P3DRight.ToVector3D(),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_P2DRight = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CheckpointRightOBJ, P2DRight_transform_Value);
                            tSRSystem3D_P2DRight.TestTransform3D();

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.MV3D_List.Add(dv3D_CheckpointRightOBJ);

                            render.MainViewPort.Children.Add(dv3D_CheckpointRightOBJ);

                            HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointRightOBJ);
                            #endregion

                            List<Point3D> point3Ds = new List<Point3D>();
                            point3Ds.Add(P3DLeft);
                            point3Ds.Add(P3DRight);

                            LinesVisual3D linesVisual3D = new LinesVisual3D
                            {
                                Points = new Point3DCollection(point3Ds),
                                Thickness = 1,
                                Color = Colors.Black
                            };

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line.Add(linesVisual3D);
                            render.MainViewPort.Children.Add(linesVisual3D);

                            #region SplitWall
                            Point3DCollection point3Ds1 = new Point3DCollection();
                            point3Ds1.Add(new Point3D(point3Ds[1].X, 0, point3Ds[1].Z));
                            point3Ds1.Add(point3Ds[1]);
                            point3Ds1.Add(new Point3D(point3Ds[0].X, 0, point3Ds[0].Z));
                            point3Ds1.Add(point3Ds[0]);

                            ModelVisual3D SplitWall = HTK_3DES.CustomModelCreateHelper.CustomRectanglePlane3D(point3Ds1, System.Windows.Media.Color.FromArgb(0xA0, 0xA0, 0x00, 0xA0), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                            HTK_3DES.TSRSystem.SetString_MV3D(SplitWall, "SplitWall -1 -1");
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL.Add(SplitWall);
                            render.MainViewPort.Children.Add(SplitWall);
                            #endregion
                            #endregion

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Line);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.DrawPath_Line(render, 5, Colors.Green);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Line);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.DrawPath_Line(render, 5, Colors.Red);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].ResetSideWall(render);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].DrawPath_SideWall(render, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                if (KMPSectionComboBox.Text == "Obj")
                {
                    AddKMPObjectForm addKMPObjectForm = new AddKMPObjectForm();
                    addKMPObjectForm.ShowDialog();

                    var data = addKMPObjectForm.SelectedKMPObject_Info;

                    KMPPropertyGridSettings.JBOG_Section.JBOGValue jBOGValue = new KMPPropertyGridSettings.JBOG_Section.JBOGValue(data.Name, data.ObjID, Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    JBOG_Section.JBOGValueList.Add(jBOGValue);

                    KMP_Path_ListBox.Items.Add(jBOGValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(OBJ)
                        List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDataXml_List = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml");
                        string Path = ObjFlowDataXml_List.Find(x => x.ObjectID == data.ObjID).Path;
                        ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(Path);

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + jBOGValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = jBOGValue.Positions.GetVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(jBOGValue.Scales.GetVector3D(), 2),
                            Rotate3D = jBOGValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        KMPViewportObject.OBJ_MV3DList.Add(dv3D_OBJ);

                        render.MainViewPort.Children.Add(dv3D_OBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "Route")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point iTOP_Point = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.Add(iTOP_Point);

                        KMP_Path_ListBox.Items.Add(iTOP_Point);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Add Model(Routes)
                            ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RouteOBJ, "Routes " + iTOP_Point.ID + " " + iTOP_Point.GroupID);

                            HTK_3DES.TSRSystem.Transform JugemPath_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = iTOP_Point.Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_RouteOBJ, JugemPath_transform_Value);
                            tSRSystem3D.TestTransform3D();

                            //AddMDL
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].MV3D_List.Add(dv3D_RouteOBJ);

                            render.MainViewPort.Children.Add(dv3D_RouteOBJ);
                            #endregion

                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Blue);

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                if (KMPSectionComboBox.Text == "Area")
                {
                    KMPPropertyGridSettings.AERA_Section.AERAValue aERAValue = new KMPPropertyGridSettings.AERA_Section.AERAValue(Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    AERA_Section.AERAValueList.Add(aERAValue);

                    KMP_Path_ListBox.Items.Add(aERAValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(Area)
                        ModelVisual3D dv3D_AreaOBJ = null;
                        if (aERAValue.AreaModeSettings.AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + aERAValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform Area_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = aERAValue.Positions.GetVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(aERAValue.Scales.GetVector3D(), 2000),
                            Rotate3D = aERAValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //Area_MV3D_List.Add(dv3D_AreaOBJ);
                        KMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                        render.MainViewPort.Children.Add(dv3D_AreaOBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "Camera")
                {
                    KMPPropertyGridSettings.EMAC_Section.EMACValue eMACValue = new KMPPropertyGridSettings.EMAC_Section.EMACValue(Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    EMAC_Section.EMACValueList.Add(eMACValue);

                    KMP_Path_ListBox.Items.Add(eMACValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(Camera)
                        ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CameraOBJ, "Camera " + eMACValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform Camera_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = eMACValue.Positions.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = eMACValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CameraOBJ, Camera_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                        KMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                        render.MainViewPort.Children.Add(dv3D_CameraOBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "JugemPoint")
                {
                    KMPPropertyGridSettings.TPGJ_Section.TPGJValue tPGJValue = new KMPPropertyGridSettings.TPGJ_Section.TPGJValue(Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    TPGJ_Section.TPGJValueList.Add(tPGJValue);

                    KMP_Path_ListBox.Items.Add(tPGJValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(RespawnPoint)
                        ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + tPGJValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform RespawnPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = tPGJValue.Positions.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = tPGJValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_RespawnPointOBJ, RespawnPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //RespawnPoint_MV3D_List.Add(dv3D_RespawnPointOBJ);
                        KMPViewportObject.RespawnPoint_MV3DList.Add(dv3D_RespawnPointOBJ);

                        render.MainViewPort.Children.Add(dv3D_RespawnPointOBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue tPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.Add(tPLGValue);

                        KMP_Path_ListBox.Items.Add(tPLGValue);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Add Model(GlideRoutes)
                            ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + tPLGValue.ID + " " + tPLGValue.GroupID);

                            HTK_3DES.TSRSystem.Transform GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = tPLGValue.Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(tPLGValue.TPLG_PointScaleValue, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                            tSRSystem3D.TestTransform3D();

                            //Add model
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List.Add(dv3D_GliderPathOBJ);

                            render.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                            #endregion

                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.LightSkyBlue);

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
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
                    if (typeof(LinesVisual3D) == HTR.VisualHit.GetType()) return;
                    if (typeof(TubeVisual3D) == HTR.VisualHit.GetType()) return;
                    if (typeof(RectangleVisual3D) == HTR.VisualHit.GetType()) return;

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
                        TPTK_Section.TPTKValueList[MDLNum].Position_Value.X = (float)transform_Value.Translate3D.X;
                        TPTK_Section.TPTKValueList[MDLNum].Position_Value.Y = (float)transform_Value.Translate3D.Y;
                        TPTK_Section.TPTKValueList[MDLNum].Position_Value.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = TPTK_Section.TPTKValueList[MDLNum];
                    }
                    if (OBJ_Name == "EnemyRoute")
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
                        HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum].Positions.X = (float)transform_Value.Translate3D.X;
                        HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum].Positions.Y = (float)transform_Value.Translate3D.Y;
                        HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum].Positions.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.EnemyRoute_Rail_List[GroupNum];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = HPNE_TPNE_Section.HPNEValueList[GroupNum].TPNEValueList[MDLNum];
                    }
                    if (OBJ_Name == "ItemRoute")
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
                        HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum].TPTI_Positions.X = (float)transform_Value.Translate3D.X;
                        HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum].TPTI_Positions.Y = (float)transform_Value.Translate3D.Y;
                        HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum].TPTI_Positions.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.ItemRoute_Rail_List[GroupNum];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = HPTI_TPTI_Section.HPTIValueList[GroupNum].TPTIValueList[MDLNum];
                    }
                    if (OBJ_Name == "Checkpoint_Left")
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
                        HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Left.X = (float)transform_Value.Translate3D.X;
                        HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Left.Y = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //パスの形を変更(機能の追加)
                        HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = KMPViewportObject.Checkpoint_Rail[GroupNum];

                        //Green
                        if (checkpoint.Checkpoint_Left.LV3D_List.Count != 0) checkpoint.Checkpoint_Left.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Line);
                        KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_Line[MDLNum].Points[0] = transform_Value.Translate3D.ToPoint3D();

                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_SplitWallMDL[MDLNum].Content).Positions[2] = new Point3D(transform_Value.Translate3D.X, 0, transform_Value.Translate3D.Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_SplitWallMDL[MDLNum].Content).Positions[3] = transform_Value.Translate3D.ToPoint3D();

                        if (checkpoint.SideWall_Left.Count != 0) checkpoint.MoveSideWalls(MDLNum, transform_Value.Translate3D, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint.SideWallType.Left);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum];
                    }
                    if (OBJ_Name == "Checkpoint_Right")
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
                        HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Right.X = (float)transform_Value.Translate3D.X;
                        HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum].Position_2D_Right.Y = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //パスの形を変更(機能の追加)
                        HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = KMPViewportObject.Checkpoint_Rail[GroupNum];

                        //Red
                        if (checkpoint.Checkpoint_Right.LV3D_List.Count != 0) checkpoint.Checkpoint_Right.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Line);
                        KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_Line[MDLNum].Points[1] = transform_Value.Translate3D.ToPoint3D();

                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_SplitWallMDL[MDLNum].Content).Positions[0] = new Point3D(transform_Value.Translate3D.X, 0, transform_Value.Translate3D.Z);
                        HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[GroupNum].Checkpoint_SplitWallMDL[MDLNum].Content).Positions[1] = transform_Value.Translate3D.ToPoint3D();

                        if (checkpoint.SideWall_Right.Count != 0) checkpoint.MoveSideWalls(MDLNum, transform_Value.Translate3D, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint.SideWallType.Right);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = HPKC_TPKC_Section.HPKCValueList[GroupNum].TPKCValueList[MDLNum];
                    }
                    if (OBJ_Name == "OBJ")
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
                        JBOG_Section.JBOGValueList[MDLNum].Positions.X = (float)transform_Value.Translate3D.X;
                        JBOG_Section.JBOGValueList[MDLNum].Positions.Y = (float)transform_Value.Translate3D.Y;
                        JBOG_Section.JBOGValueList[MDLNum].Positions.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = JBOG_Section.JBOGValueList[MDLNum];
                    }
                    if (OBJ_Name == "Routes")
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
                        ITOP_Section.ITOP_RouteList[GroupNum].ITOP_PointList[MDLNum].Positions.X = (float)transform_Value.Translate3D.X;
                        ITOP_Section.ITOP_RouteList[GroupNum].ITOP_PointList[MDLNum].Positions.Y = (float)transform_Value.Translate3D.Y;
                        ITOP_Section.ITOP_RouteList[GroupNum].ITOP_PointList[MDLNum].Positions.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.Routes_List[GroupNum];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = ITOP_Section.ITOP_RouteList[GroupNum].ITOP_PointList[MDLNum];
                    }
                    if (OBJ_Name == "Area")
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
                        AERA_Section.AERAValueList[MDLNum].Positions.X = (float)transform_Value.Translate3D.X;
                        AERA_Section.AERAValueList[MDLNum].Positions.Y = (float)transform_Value.Translate3D.Y;
                        AERA_Section.AERAValueList[MDLNum].Positions.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = AERA_Section.AERAValueList[MDLNum];
                    }
                    if (OBJ_Name == "Camera")
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
                        EMAC_Section.EMACValueList[MDLNum].Positions.X = (float)transform_Value.Translate3D.X;
                        EMAC_Section.EMACValueList[MDLNum].Positions.Y = (float)transform_Value.Translate3D.Y;
                        EMAC_Section.EMACValueList[MDLNum].Positions.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = EMAC_Section.EMACValueList[MDLNum];
                    }
                    if (OBJ_Name == "RespawnPoint")
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
                        TPGJ_Section.TPGJValueList[MDLNum].Positions.X = (float)transform_Value.Translate3D.X;
                        TPGJ_Section.TPGJValueList[MDLNum].Positions.Y = (float)transform_Value.Translate3D.Y;
                        TPGJ_Section.TPGJValueList[MDLNum].Positions.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = TPGJ_Section.TPGJValueList[MDLNum];
                    }
                    if (OBJ_Name == "GlideRoutes")
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
                        HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum].Positions.X = (float)transform_Value.Translate3D.X;
                        HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum].Positions.Y = (float)transform_Value.Translate3D.Y;
                        HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum].Positions.Z = (float)transform_Value.Translate3D.Z;

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(FindMV3D, transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //パスの形を変更
                        HTK_3DES.PathTools.Rail rail = KMPViewportObject.GlideRoute_Rail_List[GroupNum];
                        if (rail.TV3D_List.Count != 0) rail.MoveRails(MDLNum, transform_Value.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                        //PropertyGridにPropertyを表示させる
                        propertyGrid_KMP_Path.SelectedObject = HPLG_TPLG_Section.HPLGValueList[GroupNum].TPLGValueList[MDLNum];
                    }
                    if (OBJ_Name == "GridLine") return;
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
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "kmp file|*.kmp"
            };

            if (Open_KMP.ShowDialog() != DialogResult.OK) return;

            System.IO.FileStream fs1 = new FileStream(Open_KMP.FileName, FileMode.Open, FileAccess.Read);
            BinaryReader br1 = new BinaryReader(fs1);

            KMPs.KMPFormat KMP_Section = new KMPs.KMPFormat();
            KMP_Section.ReadKMP(br1);

            KMPVersion_TXT.Text = KMP_Section.VersionNumber.ToString();

            #region Add PropertyGrid
            TPTK_Section = new KMPPropertyGridSettings.TPTK_Section(KMP_Section.KMP_Section.TPTK);
            KMPs.KMPViewportRendering.Render_StartPosition(render, KMPViewportObject, KMP_Section.KMP_Section.TPTK);

            HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section(KMP_Section.KMP_Section.HPNE, KMP_Section.KMP_Section.TPNE);
            KMPs.KMPViewportRendering.Render_EnemyRoute(render, KMPViewportObject, KMP_Section.KMP_Section.HPNE, KMP_Section.KMP_Section.TPNE);

            HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section(KMP_Section.KMP_Section.HPTI, KMP_Section.KMP_Section.TPTI);
            KMPs.KMPViewportRendering.Render_ItemRoute(render, KMPViewportObject, KMP_Section.KMP_Section.HPTI, KMP_Section.KMP_Section.TPTI);

            HPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section(KMP_Section.KMP_Section.HPKC, KMP_Section.KMP_Section.TPKC);
            double d = Convert.ToDouble(textBox1.Text);
            KMPs.KMPViewportRendering.Render_Checkpoint(render, KMPViewportObject, KMP_Section.KMP_Section.HPKC, KMP_Section.KMP_Section.TPKC, d);

            JBOG_Section = new KMPPropertyGridSettings.JBOG_Section(KMP_Section.KMP_Section.JBOG, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));
            KMPs.KMPViewportRendering.Render_Object(render, KMPViewportObject, KMP_Section.KMP_Section.JBOG, KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml"));

            ITOP_Section = new KMPPropertyGridSettings.ITOP_Section(KMP_Section.KMP_Section.ITOP);
            KMPs.KMPViewportRendering.Render_Route(render, KMPViewportObject, KMP_Section.KMP_Section.ITOP);

            AERA_Section = new KMPPropertyGridSettings.AERA_Section(KMP_Section.KMP_Section.AERA);
            KMPs.KMPViewportRendering.Render_Area(render, KMPViewportObject, KMP_Section.KMP_Section.AERA);

            EMAC_Section = new KMPPropertyGridSettings.EMAC_Section(KMP_Section.KMP_Section.EMAC);
            KMPs.KMPViewportRendering.Render_Camera(render, KMPViewportObject, KMP_Section.KMP_Section.EMAC);

            TPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section(KMP_Section.KMP_Section.TPGJ);
            KMPs.KMPViewportRendering.Render_Returnpoint(render, KMPViewportObject, KMP_Section.KMP_Section.TPGJ);

            //TPNC : Unused Section
            //TPSM : Unused Section

            IGTS_Section = new KMPPropertyGridSettings.IGTS_Section(KMP_Section.KMP_Section.IGTS);

            //SROC : Unused Section

            HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section(KMP_Section.KMP_Section.HPLG, KMP_Section.KMP_Section.TPLG);
            KMPs.KMPViewportRendering.Render_GlideRoute(render, KMPViewportObject, KMP_Section.KMP_Section.HPLG, KMP_Section.KMP_Section.TPLG);
            #endregion

            br1.Close();
            fs1.Close();

            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Area.Checked, render, KMPViewportObject.StartPosition_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_EnemyRoutes.Checked, render, KMPViewportObject.EnemyRoute_Rail_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_ItemRoutes.Checked, render, KMPViewportObject.ItemRoute_Rail_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Checkpoint.Checked, render, KMPViewportObject.Checkpoint_Rail);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_OBJ.Checked, render, KMPViewportObject.OBJ_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Routes.Checked, render, KMPViewportObject.Routes_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Area.Checked, render, KMPViewportObject.Area_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Camera.Checked, render, KMPViewportObject.Camera_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Returnpoints.Checked, render, KMPViewportObject.RespawnPoint_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_GlideRoutes.Checked, render, KMPViewportObject.GlideRoute_Rail_List);

            if (KMPSectionComboBox.Items.Count == 0)
            {
                string[] AllSectionAry = new string[] { "KartPoint", "EnemyRoutes", "ItemRoutes", "CheckPoint", "Obj", "Route", "Area", "Camera", "JugemPoint", "GlideRoutes" };
                KMPSectionComboBox.Items.AddRange(AllSectionAry.ToArray());
            }

            KMPSectionComboBox.SelectedIndex = 0;

            if (IGTS_Section != null)
            {
                //Display only IGTS section directly to PropertyGrid
                propertyGrid_KMP_StageInfo.SelectedObject = IGTS_Section;
            }

            writeBinaryToolStripMenuItem.Enabled = true;
            closeKMPToolStripMenuItem.Enabled = true;
            exportToolStripMenuItem.Enabled = true;
            inputXmlAsXXXXToolStripMenuItem.Enabled = true;
        }

        private void writeBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save_KMP = new SaveFileDialog()
            {
                Title = "Save KMP",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "kmp file|*.kmp"
            };

            if (Save_KMP.ShowDialog() != DialogResult.OK) return;

            System.IO.FileStream fs1 = new FileStream(Save_KMP.FileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw1 = new BinaryWriter(fs1);

            KMPs.KMPFormat kMPFormat = new KMPs.KMPFormat(Convert.ToUInt32(KMPVersion_TXT.Text));
            //KMPs.KMPFormat kMPFormat = new KMPs.KMPFormat();
            kMPFormat.KMP_Section.TPTK = PropertyGridClassToBinaryConverter.ToTPTK_Section(TPTK_Section);
            kMPFormat.KMP_Section.TPNE = PropertyGridClassToBinaryConverter.HPNE_TPNESection.ToHPNE_TPNE_Section(HPNE_TPNE_Section).TPNE_Section;
            kMPFormat.KMP_Section.HPNE = PropertyGridClassToBinaryConverter.HPNE_TPNESection.ToHPNE_TPNE_Section(HPNE_TPNE_Section).HPNE_Section;
            kMPFormat.KMP_Section.TPTI = PropertyGridClassToBinaryConverter.HPTI_TPTISection.ToHPTI_TPTI_Section(HPTI_TPTI_Section).TPTI_Section;
            kMPFormat.KMP_Section.HPTI = PropertyGridClassToBinaryConverter.HPTI_TPTISection.ToHPTI_TPTI_Section(HPTI_TPTI_Section).HPTI_Section;
            kMPFormat.KMP_Section.TPKC = PropertyGridClassToBinaryConverter.HPKC_TPKCSection.ToHPKC_TPKC_Section(HPKC_TPKC_Section).TPKC_Section;
            kMPFormat.KMP_Section.HPKC = PropertyGridClassToBinaryConverter.HPKC_TPKCSection.ToHPKC_TPKC_Section(HPKC_TPKC_Section).HPKC_Section;
            kMPFormat.KMP_Section.JBOG = PropertyGridClassToBinaryConverter.ToJBOG_Section(JBOG_Section, kMPFormat.VersionNumber);
            kMPFormat.KMP_Section.ITOP = PropertyGridClassToBinaryConverter.ToITOP_Section(ITOP_Section);
            kMPFormat.KMP_Section.AERA = PropertyGridClassToBinaryConverter.ToAERA_section(AERA_Section);
            kMPFormat.KMP_Section.EMAC = PropertyGridClassToBinaryConverter.ToEMAC_Section(EMAC_Section);
            kMPFormat.KMP_Section.TPGJ = PropertyGridClassToBinaryConverter.ToTPGJ_Section(TPGJ_Section);
            kMPFormat.KMP_Section.TPNC = new KMPs.KMPFormat.KMPSection.TPNC_Section();
            kMPFormat.KMP_Section.TPSM = new KMPs.KMPFormat.KMPSection.TPSM_Section();
            kMPFormat.KMP_Section.IGTS = PropertyGridClassToBinaryConverter.ToIGTS_Section(IGTS_Section);
            kMPFormat.KMP_Section.SROC = new KMPs.KMPFormat.KMPSection.SROC_Section();
            kMPFormat.KMP_Section.TPLG = PropertyGridClassToBinaryConverter.HPLG_TPLGSection.ToHPLG_TPLG_Section(HPLG_TPLG_Section).TPLG_Section;
            kMPFormat.KMP_Section.HPLG = PropertyGridClassToBinaryConverter.HPLG_TPLGSection.ToHPLG_TPLG_Section(HPLG_TPLG_Section).HPLG_Section;

            kMPFormat.WriteKMP(bw1);

            bw1.Close();
            fs1.Close();
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
            if(Viewport_Type_3D.Checked == false)
            {
                render.MainViewPort.Orthographic = false;
                render.MainViewPort.CameraController.IsTouchZoomEnabled = true;
                render.MainViewPort.CameraController.IsRotationEnabled = true;
            }
        }

        private void AddKMPSection_Click(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 0)
            {
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue hPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue(KMP_Group_ListBox.Items.Count);

                    HPNE_TPNE_Section.HPNEValueList.Add(hPNEValue);

                    KMP_Group_ListBox.Items.Add(hPNEValue);

                    if (KMP_Group_ListBox.Items.Count != 0)
                    {
                        //Rail
                        HTK_3DES.PathTools.Rail KMP_EnemyRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                        //Add
                        KMPViewportObject.EnemyRoute_Rail_List.Add(KMP_EnemyRoute_Rail);

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue hPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue(KMP_Group_ListBox.Items.Count);

                    HPTI_TPTI_Section.HPTIValueList.Add(hPTIValue);

                    KMP_Group_ListBox.Items.Add(hPTIValue);

                    if (KMP_Group_ListBox.Items.Count != 0)
                    {
                        //Rail
                        HTK_3DES.PathTools.Rail KMP_ItemRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                        //Add
                        KMPViewportObject.ItemRoute_Rail_List.Add(KMP_ItemRoute_Rail);

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue hPKCValue = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue(KMP_Group_ListBox.Items.Count);

                    HPKC_TPKC_Section.HPKCValueList.Add(hPKCValue);

                    KMP_Group_ListBox.Items.Add(hPKCValue);

                    if (KMP_Group_ListBox.Items.Count != 0)
                    {
                        //Checkpoint_Rails
                        HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = new HTK_3DES.KMP_3DCheckpointSystem.Checkpoint();

                        //Add
                        KMPViewportObject.Checkpoint_Rail.Add(checkpoint);

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "Route")
                {
                    KMPPropertyGridSettings.ITOP_Section.ITOP_Route iTOP_Route = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route(KMP_Group_ListBox.Items.Count);

                    ITOP_Section.ITOP_RouteList.Add(iTOP_Route);

                    KMP_Group_ListBox.Items.Add(iTOP_Route);

                    if (KMP_Group_ListBox.Items.Count != 0)
                    {
                        //Rail
                        HTK_3DES.PathTools.Rail Route_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                        //Add
                        KMPViewportObject.Routes_List.Add(Route_Rail);

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue hPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue(KMP_Group_ListBox.Items.Count);

                    HPLG_TPLG_Section.HPLGValueList.Add(hPLGValue);

                    KMP_Group_ListBox.Items.Add(hPLGValue);

                    if (KMP_Group_ListBox.Items.Count != 0)
                    {
                        //Rail
                        HTK_3DES.PathTools.Rail GlideRoute_Rail = new HTK_3DES.PathTools.Rail(new List<ModelVisual3D>(), null, new List<TubeVisual3D>());

                        //Add
                        KMPViewportObject.GlideRoute_Rail_List.Add(GlideRoute_Rail);

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
            }
            if(tabControl1.SelectedIndex == 1)
            {
                //カメラの位置を取得
                Point3D Pos = render.ViewportPosition(UserControl1.PositionMode.CameraPos);

                if (KMPSectionComboBox.Text == "KartPoint")
                {
                    KMPPropertyGridSettings.TPTK_Section.TPTKValue tPTKValue = new KMPPropertyGridSettings.TPTK_Section.TPTKValue(Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    TPTK_Section.TPTKValueList.Add(tPTKValue);

                    KMP_Path_ListBox.Items.Add(tPTKValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(StartPosition)
                        ModelVisual3D dv3D_StartPositionOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0x80, 0xED, 0xFF, 0x03), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0xFF, 0x00, 0x00, 0xFF), Color.FromArgb(0x80, 0x03, 0xFF, 0x60), Color.FromArgb(0x80, 0x03, 0xFF, 0x60));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_StartPositionOBJ, "StartPosition " + tPTKValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform StartPosition_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = tPTKValue.Position_Value.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = tPTKValue.Rotate_Value.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_StartPositionOBJ, StartPosition_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        KMPViewportObject.StartPosition_MV3DList.Add(dv3D_StartPositionOBJ);

                        render.MainViewPort.Children.Add(dv3D_StartPositionOBJ);

                        HTK_3DES.TSRSystem.GC_Dispose(dv3D_StartPositionOBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue tPNEValue = new KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.Add(tPNEValue);

                        KMP_Path_ListBox.Items.Add(tPNEValue);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Add Model(EnemyRoutes)
                            ModelVisual3D dv3D_EnemyPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0xFF, 0x9B, 0x34), Color.FromArgb(0x80, 0xFF, 0x9B, 0x34));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_EnemyPathOBJ, "EnemyRoute " + tPNEValue.ID + " " + tPNEValue.Group_ID);

                            HTK_3DES.TSRSystem.Transform EnemyPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = tPNEValue.Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(tPNEValue.Control, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_EnemyPathOBJ, EnemyPoint_transform_Value);
                            tSRSystem3D.TestTransform3D();

                            //Add Rail => MV3DList
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List.Add(dv3D_EnemyPathOBJ);

                            render.MainViewPort.Children.Add(dv3D_EnemyPathOBJ);
                            #endregion

                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Orange);

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue tPTIValue = new KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.Add(tPTIValue);

                        KMP_Path_ListBox.Items.Add(tPTIValue);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Add Model(ItemRoutes)
                            ModelVisual3D dv3D_ItemPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x00, 0xD1, 0x41), Color.FromArgb(0x80, 0x00, 0xD1, 0x41));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_ItemPathOBJ, "ItemRoute " + tPTIValue.ID + " " + tPTIValue.Group_ID);

                            HTK_3DES.TSRSystem.Transform ItemPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = tPTIValue.TPTI_Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(tPTIValue.TPTI_PointSize, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_ItemPathOBJ, ItemPoint_transform_Value);
                            tSRSystem3D.TestTransform3D();

                            //Add Rail => MV3DList
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List.Add(dv3D_ItemPathOBJ);

                            render.MainViewPort.Children.Add(dv3D_ItemPathOBJ);
                            #endregion

                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Green);

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        Vector2 LeftPos = new Vector2(Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Z));
                        Vector2 RightPos = new Vector2(Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Z));

                        KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue tPKCValue = new KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue(LeftPos, RightPos, KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.Add(tPKCValue);

                        KMP_Path_ListBox.Items.Add(tPKCValue);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Create
                            var P2D_Left = tPKCValue.Position_2D_Left;
                            Vector2 P2DLeftToVector2 = new Vector2(Convert.ToSingle(P2D_Left.X), Convert.ToSingle(P2D_Left.Y));
                            Point3D P3DLeft = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DLeftToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                            P3DLeft.Y = Convert.ToDouble(textBox1.Text);

                            #region Transform(Left)
                            ModelVisual3D dv3D_CheckpointLeftOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46), Color.FromArgb(0xFF, 0x00, 0x7F, 0x46));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointLeftOBJ, "Checkpoint_Left " + tPKCValue.ID + " " + tPKCValue.Group_ID);

                            HTK_3DES.TSRSystem.Transform P2DLeft_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = P3DLeft.ToVector3D(),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_P2DLeft = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CheckpointLeftOBJ, P2DLeft_transform_Value);
                            tSRSystem3D_P2DLeft.TestTransform3D();

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.MV3D_List.Add(dv3D_CheckpointLeftOBJ);

                            render.MainViewPort.Children.Add(dv3D_CheckpointLeftOBJ);

                            HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointLeftOBJ);
                            #endregion

                            var P2D_Right = tPKCValue.Position_2D_Right;
                            Vector2 P2DRightToVector2 = new Vector2(Convert.ToSingle(P2D_Right.X), Convert.ToSingle(P2D_Right.Y));
                            Point3D P3DRight = KMPs.KMPHelper.Vector3DTo2DConverter.Vector2DTo3D(P2DRightToVector2, KMPs.KMPHelper.Vector3DTo2DConverter.Axis_Up.Y).ToPoint3D();
                            P3DRight.Y = Convert.ToDouble(textBox1.Text);

                            #region Transform(Right)
                            ModelVisual3D dv3D_CheckpointRightOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00, 0x00));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CheckpointRightOBJ, "Checkpoint_Right " + tPKCValue.ID + " " + tPKCValue.Group_ID);

                            HTK_3DES.TSRSystem.Transform P2DRight_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = P3DRight.ToVector3D(),
                                Scale3D = new Vector3D(50, 50, 50),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_P2DRight = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CheckpointRightOBJ, P2DRight_transform_Value);
                            tSRSystem3D_P2DRight.TestTransform3D();

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.MV3D_List.Add(dv3D_CheckpointRightOBJ);

                            render.MainViewPort.Children.Add(dv3D_CheckpointRightOBJ);

                            HTK_3DES.TSRSystem.GC_Dispose(dv3D_CheckpointRightOBJ);
                            #endregion

                            List<Point3D> point3Ds = new List<Point3D>();
                            point3Ds.Add(P3DLeft);
                            point3Ds.Add(P3DRight);

                            LinesVisual3D linesVisual3D = new LinesVisual3D
                            {
                                Points = new Point3DCollection(point3Ds),
                                Thickness = 1,
                                Color = Colors.Black
                            };

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line.Add(linesVisual3D);
                            render.MainViewPort.Children.Add(linesVisual3D);

                            #region SplitWall
                            Point3DCollection point3Ds1 = new Point3DCollection();
                            point3Ds1.Add(new Point3D(point3Ds[1].X, 0, point3Ds[1].Z));
                            point3Ds1.Add(point3Ds[1]);
                            point3Ds1.Add(new Point3D(point3Ds[0].X, 0, point3Ds[0].Z));
                            point3Ds1.Add(point3Ds[0]);

                            ModelVisual3D SplitWall = HTK_3DES.CustomModelCreateHelper.CustomRectanglePlane3D(point3Ds1, System.Windows.Media.Color.FromArgb(0xA0, 0xA0, 0x00, 0xA0), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));
                            HTK_3DES.TSRSystem.SetString_MV3D(SplitWall, "SplitWall -1 -1");
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL.Add(SplitWall);
                            render.MainViewPort.Children.Add(SplitWall);
                            #endregion
                            #endregion

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Line);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.DrawPath_Line(render, 5, Colors.Green);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Line);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.DrawPath_Line(render, 5, Colors.Red);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].ResetSideWall(render);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].DrawPath_SideWall(render, System.Windows.Media.Color.FromArgb(0x45, 0x00, 0xA0, 0x00), System.Windows.Media.Color.FromArgb(0x45, 0xA0, 0x00, 0x00));

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                if (KMPSectionComboBox.Text == "Obj")
                {
                    AddKMPObjectForm addKMPObjectForm = new AddKMPObjectForm();
                    addKMPObjectForm.ShowDialog();

                    var data = addKMPObjectForm.SelectedKMPObject_Info;

                    KMPPropertyGridSettings.JBOG_Section.JBOGValue jBOGValue = new KMPPropertyGridSettings.JBOG_Section.JBOGValue(data.Name, data.ObjID, Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    JBOG_Section.JBOGValueList.Add(jBOGValue);

                    KMP_Path_ListBox.Items.Add(jBOGValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(OBJ)
                        List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDataXml_List = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml");
                        string Path = ObjFlowDataXml_List.Find(x => x.ObjectID == data.ObjID).Path;
                        ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(Path);

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + jBOGValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = jBOGValue.Positions.GetVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(jBOGValue.Scales.GetVector3D(), 2),
                            Rotate3D = jBOGValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        KMPViewportObject.OBJ_MV3DList.Add(dv3D_OBJ);

                        render.MainViewPort.Children.Add(dv3D_OBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "Route")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point iTOP_Point = new KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.Add(iTOP_Point);

                        KMP_Path_ListBox.Items.Add(iTOP_Point);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Add Model(Routes)
                            ModelVisual3D dv3D_RouteOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x3F, 0x45, 0xE2), Color.FromArgb(0x80, 0x3F, 0x45, 0xE2));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RouteOBJ, "Routes " + iTOP_Point.ID + " " + iTOP_Point.GroupID);

                            HTK_3DES.TSRSystem.Transform Route_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = iTOP_Point.Positions.GetVector3D(),
                                Scale3D = new Vector3D(20, 20, 20),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_RouteOBJ, Route_transform_Value);
                            tSRSystem3D.TestTransform3D();

                            //AddMDL
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].MV3D_List.Add(dv3D_RouteOBJ);

                            render.MainViewPort.Children.Add(dv3D_RouteOBJ);
                            #endregion

                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.Blue);

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
                if (KMPSectionComboBox.Text == "Area")
                {
                    KMPPropertyGridSettings.AERA_Section.AERAValue aERAValue = new KMPPropertyGridSettings.AERA_Section.AERAValue(Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    AERA_Section.AERAValueList.Add(aERAValue);

                    KMP_Path_ListBox.Items.Add(aERAValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(Area)
                        ModelVisual3D dv3D_AreaOBJ = null;
                        if (aERAValue.AreaModeSettings.AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + aERAValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform Area_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = aERAValue.Positions.GetVector3D(),
                            Scale3D = HTK_3DES.TSRSystem.ScaleFactor(aERAValue.Scales.GetVector3D(), 2000),
                            Rotate3D = aERAValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //Area_MV3D_List.Add(dv3D_AreaOBJ);
                        KMPViewportObject.Area_MV3DList.Add(dv3D_AreaOBJ);

                        render.MainViewPort.Children.Add(dv3D_AreaOBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "Camera")
                {
                    KMPPropertyGridSettings.EMAC_Section.EMACValue eMACValue = new KMPPropertyGridSettings.EMAC_Section.EMACValue(Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    EMAC_Section.EMACValueList.Add(eMACValue);

                    KMP_Path_ListBox.Items.Add(eMACValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(Camera)
                        ModelVisual3D dv3D_CameraOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0x80, 0xFA, 0xFF, 0x00), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0xFF, 0x00, 0x53, 0xF2), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF), Color.FromArgb(0x80, 0x00, 0xE7, 0xFF));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_CameraOBJ, "Camera " + eMACValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform Camera_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = eMACValue.Positions.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = eMACValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_CameraOBJ, Camera_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //Camera_MV3D_List.Add(dv3D_CameraOBJ);
                        KMPViewportObject.Camera_MV3DList.Add(dv3D_CameraOBJ);

                        render.MainViewPort.Children.Add(dv3D_CameraOBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "JugemPoint")
                {
                    KMPPropertyGridSettings.TPGJ_Section.TPGJValue tPGJValue = new KMPPropertyGridSettings.TPGJ_Section.TPGJValue(Pos.ToVector3D(), KMP_Path_ListBox.Items.Count);

                    TPGJ_Section.TPGJValueList.Add(tPGJValue);

                    KMP_Path_ListBox.Items.Add(tPGJValue);

                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        #region Add Model(RespawnPoint)
                        ModelVisual3D dv3D_RespawnPointOBJ = HTK_3DES.CustomModelCreateHelper.CustomPointVector3D(Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0x80, 0x5A, 0x1F, 0x97), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0xFF, 0xFF, 0x06, 0x2B), Color.FromArgb(0x80, 0x00, 0xFF, 0x73), Color.FromArgb(0x80, 0x00, 0xFF, 0x73));

                        //モデルの名前と番号を文字列に格納(情報化)
                        HTK_3DES.TSRSystem.SetString_MV3D(dv3D_RespawnPointOBJ, "RespawnPoint " + tPGJValue.ID + " " + -1);

                        HTK_3DES.TSRSystem.Transform RespawnPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                        {
                            Translate3D = tPGJValue.Positions.GetVector3D(),
                            Scale3D = new Vector3D(20, 20, 20),
                            Rotate3D = tPGJValue.Rotations.GetVector3D()
                        };

                        HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_RespawnPointOBJ, RespawnPoint_transform_Value);
                        tSRSystem3D.TestTransform3D();

                        //RespawnPoint_MV3D_List.Add(dv3D_RespawnPointOBJ);
                        KMPViewportObject.RespawnPoint_MV3DList.Add(dv3D_RespawnPointOBJ);

                        render.MainViewPort.Children.Add(dv3D_RespawnPointOBJ);
                        #endregion

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue tPLGValue = new KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue(Pos.ToVector3D(), KMP_Group_ListBox.SelectedIndex, KMP_Path_ListBox.Items.Count);

                        HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.Add(tPLGValue);

                        KMP_Path_ListBox.Items.Add(tPLGValue);

                        if (KMP_Path_ListBox.Items.Count != 0)
                        {
                            #region Add Model(GlideRoutes)
                            ModelVisual3D dv3D_GliderPathOBJ = HTK_3DES.CustomModelCreateHelper.CustomSphereVisual3D(30, 10, 1, Color.FromArgb(0x80, 0x13, 0xDC, 0xFF), Color.FromArgb(0x80, 0x13, 0xDC, 0xFF));

                            //モデルの名前と番号を文字列に格納(情報化)
                            HTK_3DES.TSRSystem.SetString_MV3D(dv3D_GliderPathOBJ, "GlideRoutes " + tPLGValue.ID + " " + tPLGValue.GroupID);

                            HTK_3DES.TSRSystem.Transform GliderPoint_transform_Value = new HTK_3DES.TSRSystem.Transform
                            {
                                Translate3D = tPLGValue.Positions.GetVector3D(),
                                Scale3D = HTK_3DES.TSRSystem.ScaleFactor(tPLGValue.TPLG_PointScaleValue, 100),
                                Rotate3D = new Vector3D(0, 0, 0)
                            };

                            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_GliderPathOBJ, GliderPoint_transform_Value);
                            tSRSystem3D.TestTransform3D();

                            //Add model
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List.Add(dv3D_GliderPathOBJ);

                            render.MainViewPort.Children.Add(dv3D_GliderPathOBJ);
                            #endregion

                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].ResetRail(render, HTK_3DES.PathTools.Rail.RailType.Tube);
                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DrawPath_Tube(render, 10.0, Colors.LightSkyBlue);

                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : The group may not be selected or it may be empty.");
                    }
                }
            }
        }

        #region [CustomBYAMLCollectionEditor] Delete
        /// <summary>
        /// Delete item by specifying index
        /// </summary>
        /// <typeparam name="T">T = BYAMLPropertyGridSettings.XXXX</typeparam>
        /// <param name="InputList">List<T></param>
        /// <param name="MV3DList">List<ModelVisual3D></param>
        /// <param name="Input_ListBox">Specifying a ListBox</param>
        /// <param name="Index">Index</param>
        public void DeleteItem<T>(List<T> InputList, List<ModelVisual3D> MV3DList, System.Windows.Forms.ListBox Input_ListBox, int Index)
        {
            InputList.RemoveAt(Index);
            render.MainViewPort.Children.Remove(MV3DList[Index]);
            MV3DList.RemoveAt(Index);
            Input_ListBox.Items.RemoveAt(Index);
        }

        /// <summary>
        /// Delete item by specifying index
        /// </summary>
        /// <typeparam name="T">T = BYAMLPropertyGridSettings.XXXX</typeparam>
        /// <param name="InputList">List<T></param>
        /// <param name="MV3DList">List<List<ModelVisual3D>></param>
        /// <param name="Input_ListBox">Specifying a ListBox</param>
        /// <param name="Index">Index</param>
        public void DeleteItem<T>(List<T> InputList, List<List<ModelVisual3D>> MV3DList, System.Windows.Forms.ListBox Input_ListBox, int Index)
        {
            InputList.RemoveAt(Index);

            for (int Del = 0; Del < MV3DList[Index].Count; Del++)
            {
                render.MainViewPort.Children.Remove(MV3DList[Index][Del]);
                render.UpdateLayout();
            }

            MV3DList.RemoveAt(Index);
            Input_ListBox.Items.RemoveAt(Index);
        }

        /// <summary>
        /// Delete item by specifying index
        /// </summary>
        /// <typeparam name="T">T = BYAMLPropertyGridSettings.XXXX</typeparam>
        /// <param name="InputList">List<T></param>
        /// <param name="MV3DList_1">List<List<ModelVisual3D>> 1</param>
        /// <param name="MV3DList_2">List<List<ModelVisual3D>> 2</param>
        /// <param name="Input_ListBox">Specifying a ListBox</param>
        /// <param name="Index">Index</param>
        public void DeleteItem<T>(List<T> InputList, List<List<ModelVisual3D>> MV3DList_1, List<List<ModelVisual3D>> MV3DList_2, System.Windows.Forms.ListBox Input_ListBox, int Index)
        {
            InputList.RemoveAt(Index);

            for (int Del = 0; Del < MV3DList_1[Index].Count; Del++)
            {
                render.MainViewPort.Children.Remove(MV3DList_1[Index][Del]);
                render.UpdateLayout();
            }

            MV3DList_1.RemoveAt(Index);

            for (int Del = 0; Del < MV3DList_2[Index].Count; Del++)
            {
                render.MainViewPort.Children.Remove(MV3DList_2[Index][Del]);
                render.UpdateLayout();
            }

            MV3DList_2.RemoveAt(Index);
            Input_ListBox.Items.RemoveAt(Index);
        }

        /// <summary>
        /// Get property
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="PropertyName">PropertyName</param>
        /// <returns>object</returns>
        public object GetBYAMLXmlProperty(object obj, string PropertyName)
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

                for (int ReCountPtNum = 0; ReCountPtNum < rail.MV3D_List.Count; ReCountPtNum++)
                {
                    rail.MV3D_List[ReCountPtNum].SetName(InputName + " " + ReCountPtNum + " " + ReCountGrpNum);
                }
            }
        }

        /// <summary>
        /// Modify the ID of the specified List<HTK_3DES.PathTools.Rail>.
        /// </summary>
        /// <param name="InputRail">List<HTK_3DES.PathTools.Rail></param>
        /// <param name="InputName">ModelName</param>
        public void ReInputModelID(List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint> InputRailChk, string InputName_Left, string InputName_Right)
        {
            //再度IDを入れる
            for (int ReCountGrpNum = 0; ReCountGrpNum < InputRailChk.Count; ReCountGrpNum++)
            {
                HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint = InputRailChk[ReCountGrpNum];

                for (int ChkLeftCount = 0; ChkLeftCount < checkpoint.Checkpoint_Left.MV3D_List.Count; ChkLeftCount++)
                {
                    checkpoint.Checkpoint_Left.MV3D_List[ChkLeftCount].SetName(InputName_Left + " " + ChkLeftCount + " " + ReCountGrpNum);
                }

                for (int ChkRightCount = 0; ChkRightCount < checkpoint.Checkpoint_Right.MV3D_List.Count; ChkRightCount++)
                {
                    checkpoint.Checkpoint_Right.MV3D_List[ChkRightCount].SetName(InputName_Right + " " + ChkRightCount + " " + ReCountGrpNum);
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

        private void DeleteKMPSection_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    int N = KMP_Group_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        KMPViewportObject.EnemyRoute_Rail_List[N].DeleteRail(render);

                        KMPViewportObject.EnemyRoute_Rail_List.RemoveAt(N);

                        ReInputModelID(KMPViewportObject.EnemyRoute_Rail_List, "EnemyRoute");

                        HPNE_TPNE_Section.HPNEValueList.RemoveAt(N);
                        KMP_Group_ListBox.Items.RemoveAt(N);

                        ReInputID(HPNE_TPNE_Section.HPNEValueList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < HPNE_TPNE_Section.HPNEValueList.Count; ReInputCount++)
                        {
                            for(int i = 0; i < HPNE_TPNE_Section.HPNEValueList[ReInputCount].TPNEValueList.Count; i++)
                            {
                                SetProperty(HPNE_TPNE_Section.HPNEValueList[ReInputCount].TPNEValueList[i], ReInputCount, "Group_ID");
                            }
                        }

                        propertyGrid_KMP_Group.SelectedObject = null;
                        UpdateListBox(HPNE_TPNE_Section.HPNEValueList, KMP_Group_ListBox);

                        KMP_Path_ListBox.Items.Clear();
                        propertyGrid_KMP_Path.SelectedObject = null;

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    int N = KMP_Group_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        KMPViewportObject.ItemRoute_Rail_List[N].DeleteRail(render);

                        KMPViewportObject.ItemRoute_Rail_List.RemoveAt(N);

                        ReInputModelID(KMPViewportObject.ItemRoute_Rail_List, "ItemRoute");

                        HPTI_TPTI_Section.HPTIValueList.RemoveAt(N);
                        KMP_Group_ListBox.Items.RemoveAt(N);

                        ReInputID(HPTI_TPTI_Section.HPTIValueList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < HPTI_TPTI_Section.HPTIValueList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < HPTI_TPTI_Section.HPTIValueList[ReInputCount].TPTIValueList.Count; i++)
                            {
                                SetProperty(HPTI_TPTI_Section.HPTIValueList[ReInputCount].TPTIValueList[i], ReInputCount, "Group_ID");
                            }
                        }

                        propertyGrid_KMP_Group.SelectedObject = null;
                        UpdateListBox(HPTI_TPTI_Section.HPTIValueList, KMP_Group_ListBox);

                        KMP_Path_ListBox.Items.Clear();
                        propertyGrid_KMP_Path.SelectedObject = null;

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    int N = KMP_Group_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        KMPViewportObject.Checkpoint_Rail[N].DeleteRailChk(render);

                        KMPViewportObject.Checkpoint_Rail.RemoveAt(N);

                        ReInputModelID(KMPViewportObject.Checkpoint_Rail, "Checkpoint_Left", "Checkpoint_Right");

                        HPKC_TPKC_Section.HPKCValueList.RemoveAt(N);
                        KMP_Group_ListBox.Items.RemoveAt(N);

                        ReInputID(HPKC_TPKC_Section.HPKCValueList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < HPKC_TPKC_Section.HPKCValueList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < HPKC_TPKC_Section.HPKCValueList[ReInputCount].TPKCValueList.Count; i++)
                            {
                                SetProperty(HPKC_TPKC_Section.HPKCValueList[ReInputCount].TPKCValueList[i], ReInputCount, "Group_ID");
                            }
                        }

                        propertyGrid_KMP_Group.SelectedObject = null;
                        UpdateListBox(HPKC_TPKC_Section.HPKCValueList, KMP_Group_ListBox);

                        KMP_Path_ListBox.Items.Clear();
                        propertyGrid_KMP_Path.SelectedObject = null;

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "Route")
                {
                    int N = KMP_Group_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        KMPViewportObject.Routes_List[N].DeleteRail(render);

                        KMPViewportObject.Routes_List.RemoveAt(N);

                        ReInputModelID(KMPViewportObject.Routes_List, "Routes");

                        ITOP_Section.ITOP_RouteList.RemoveAt(N);
                        KMP_Group_ListBox.Items.RemoveAt(N);

                        ReInputID(ITOP_Section.ITOP_RouteList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < ITOP_Section.ITOP_RouteList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < ITOP_Section.ITOP_RouteList[ReInputCount].ITOP_PointList.Count; i++)
                            {
                                SetProperty(ITOP_Section.ITOP_RouteList[ReInputCount].ITOP_PointList[i], ReInputCount, "GroupID");
                            }
                        }

                        propertyGrid_KMP_Group.SelectedObject = null;
                        UpdateListBox(ITOP_Section.ITOP_RouteList, KMP_Group_ListBox);

                        KMP_Path_ListBox.Items.Clear();
                        propertyGrid_KMP_Path.SelectedObject = null;

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    int N = KMP_Group_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        KMPViewportObject.GlideRoute_Rail_List[N].DeleteRail(render);

                        KMPViewportObject.GlideRoute_Rail_List.RemoveAt(N);

                        ReInputModelID(KMPViewportObject.GlideRoute_Rail_List, "GlideRoutes");

                        HPLG_TPLG_Section.HPLGValueList.RemoveAt(N);
                        KMP_Group_ListBox.Items.RemoveAt(N);

                        ReInputID(HPLG_TPLG_Section.HPLGValueList, "GroupID");

                        for (int ReInputCount = 0; ReInputCount < HPLG_TPLG_Section.HPLGValueList.Count; ReInputCount++)
                        {
                            for (int i = 0; i < HPLG_TPLG_Section.HPLGValueList[ReInputCount].TPLGValueList.Count; i++)
                            {
                                SetProperty(HPLG_TPLG_Section.HPLGValueList[ReInputCount].TPLGValueList[i], ReInputCount, "GroupID");
                            }
                        }

                        propertyGrid_KMP_Group.SelectedObject = null;
                        UpdateListBox(HPLG_TPLG_Section.HPLGValueList, KMP_Group_ListBox);

                        KMP_Path_ListBox.Items.Clear();
                        propertyGrid_KMP_Path.SelectedObject = null;

                        KMP_Group_ListBox.SelectedIndex = KMP_Group_ListBox.Items.Count - 1;
                    }
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                if (KMPSectionComboBox.Text == "KartPoint")
                {
                    int N = KMP_Path_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        //Itemの削除
                        DeleteItem(TPTK_Section.TPTKValueList, KMPViewportObject.StartPosition_MV3DList, KMP_Path_ListBox, N);

                        //再度IDを入れる
                        ReInputID(TPTK_Section.TPTKValueList, "ID");
                        ReInputModelID(KMPViewportObject.StartPosition_MV3DList, "StartPosition");

                        propertyGrid_KMP_Path.SelectedObject = null;

                        //ListBoxの再描画
                        UpdateListBox(TPTK_Section.TPTKValueList, KMP_Path_ListBox);

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int N = KMP_Path_ListBox.SelectedIndex;
                        if (N != -1)
                        {
                            HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.RemoveAt(N);

                            KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DeleteRailPoint(render, N, 10.0, Colors.Orange, HTK_3DES.PathTools.Rail.RailType.Tube);

                            KMP_Path_ListBox.Items.RemoveAt(N);

                            ReInputID(HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList, "ID");
                            propertyGrid_KMP_Path.SelectedObject = null;
                            UpdateListBox(HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList, KMP_Path_ListBox);
                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Groupを選択してください");
                    }
                }
                if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int N = KMP_Path_ListBox.SelectedIndex;
                        if (N != -1)
                        {
                            HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.RemoveAt(N);

                            KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DeleteRailPoint(render, N, 10.0, Colors.Green, HTK_3DES.PathTools.Rail.RailType.Tube);

                            KMP_Path_ListBox.Items.RemoveAt(N);

                            ReInputID(HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList, "ID");
                            propertyGrid_KMP_Path.SelectedObject = null;
                            UpdateListBox(HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList, KMP_Path_ListBox);
                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : Null");
                    }
                }
                if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int N = KMP_Path_ListBox.SelectedIndex;
                        if (N != -1)
                        {
                            HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.RemoveAt(N);

                            render.MainViewPort.Children.Remove(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line[N]);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line.RemoveAt(N);

                            render.MainViewPort.Children.Remove(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[N]);
                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL.RemoveAt(N);

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.DeleteRailPoint(render, N, 5.0, Colors.Green, HTK_3DES.PathTools.Rail.RailType.Line);

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

                            KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.DeleteRailPoint(render, N, 5.0, Colors.Red, HTK_3DES.PathTools.Rail.RailType.Line);

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

                            KMP_Path_ListBox.Items.RemoveAt(N);

                            ReInputID(HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList, "ID");
                            propertyGrid_KMP_Path.SelectedObject = null;
                            UpdateListBox(HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList, KMP_Path_ListBox);
                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Groupを選択してください");
                    }
                }
                if (KMPSectionComboBox.Text == "Obj")
                {
                    int N = KMP_Path_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        //Itemの削除
                        DeleteItem(JBOG_Section.JBOGValueList, KMPViewportObject.OBJ_MV3DList, KMP_Path_ListBox, N);

                        //再度IDを入れる
                        ReInputID(JBOG_Section.JBOGValueList, "ID");
                        ReInputModelID(KMPViewportObject.OBJ_MV3DList, "OBJ");

                        propertyGrid_KMP_Path.SelectedObject = null;

                        //ListBoxの再描画
                        UpdateListBox(JBOG_Section.JBOGValueList, KMP_Path_ListBox);

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "Route")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int N = KMP_Path_ListBox.SelectedIndex;
                        if (N != -1)
                        {
                            ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.RemoveAt(N);

                            KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].DeleteRailPoint(render, N, 10.0, Colors.Blue, HTK_3DES.PathTools.Rail.RailType.Tube);

                            KMP_Path_ListBox.Items.RemoveAt(N);

                            ReInputID(ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList, "ID");
                            propertyGrid_KMP_Path.SelectedObject = null;
                            UpdateListBox(ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList, KMP_Path_ListBox);
                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Group : Null");
                    }
                }
                if (KMPSectionComboBox.Text == "Area")
                {
                    int N = KMP_Path_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        //Itemの削除
                        DeleteItem(AERA_Section.AERAValueList, KMPViewportObject.Area_MV3DList, KMP_Path_ListBox, N);

                        //再度IDを入れる
                        ReInputID(AERA_Section.AERAValueList, "ID");
                        ReInputModelID(KMPViewportObject.Area_MV3DList, "Area");

                        propertyGrid_KMP_Path.SelectedObject = null;

                        //ListBoxの再描画
                        UpdateListBox(AERA_Section.AERAValueList, KMP_Path_ListBox);

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "Camera")
                {
                    int N = KMP_Path_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        //Itemの削除
                        DeleteItem(EMAC_Section.EMACValueList, KMPViewportObject.Camera_MV3DList, KMP_Path_ListBox, N);

                        //再度IDを入れる
                        ReInputID(EMAC_Section.EMACValueList, "ID");
                        ReInputModelID(KMPViewportObject.Camera_MV3DList, "Camera");

                        propertyGrid_KMP_Path.SelectedObject = null;

                        //ListBoxの再描画
                        UpdateListBox(EMAC_Section.EMACValueList, KMP_Path_ListBox);

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "JugemPoint")
                {
                    int N = KMP_Path_ListBox.SelectedIndex;
                    if (N != -1)
                    {
                        //Itemの削除
                        DeleteItem(TPGJ_Section.TPGJValueList, KMPViewportObject.RespawnPoint_MV3DList, KMP_Path_ListBox, N);

                        //再度IDを入れる
                        ReInputID(TPGJ_Section.TPGJValueList, "ID");
                        ReInputModelID(KMPViewportObject.RespawnPoint_MV3DList, "RespawnPoint");

                        propertyGrid_KMP_Path.SelectedObject = null;

                        //ListBoxの再描画
                        UpdateListBox(TPGJ_Section.TPGJValueList, KMP_Path_ListBox);

                        KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
                    }
                }
                if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    if (KMP_Group_ListBox.SelectedIndex != -1)
                    {
                        int N = KMP_Path_ListBox.SelectedIndex;
                        if (N != -1)
                        {
                            HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.RemoveAt(N);

                            KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].DeleteRailPoint(render, N, 10.0, Colors.LightSkyBlue, HTK_3DES.PathTools.Rail.RailType.Tube);

                            KMP_Path_ListBox.Items.RemoveAt(N);

                            ReInputID(HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList, "ID");
                            propertyGrid_KMP_Path.SelectedObject = null;
                            UpdateListBox(HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList, KMP_Path_ListBox);
                            KMP_Path_ListBox.SelectedIndex = KMP_Path_ListBox.Items.Count - 1;
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
            KMP_Path_ListBox.Items.Clear();

            if (KMPSectionComboBox.Text == "KartPoint")
            {
                KMP_Path_ListBox.Items.AddRange(TPTK_Section.TPTKValueList.ToArray());
                if (KMP_Path_ListBox.Items.Count != 0) KMP_Path_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;

                CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                KMP_Group_ListBox.Items.AddRange(HPNE_TPNE_Section.HPNEValueList.ToArray());
                if (KMP_Group_ListBox.Items.Count != 0) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = true;

                xXXXRouteImporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = true;

                CH_KMPGroupPoint.Enabled = true;
            }
            if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                KMP_Group_ListBox.Items.AddRange(HPTI_TPTI_Section.HPTIValueList.ToArray());
                if (KMP_Group_ListBox.Items.Count != 0) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = true;

                xXXXRouteImporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = true;

                CH_KMPGroupPoint.Enabled = true;
            }
            if (KMPSectionComboBox.Text == "CheckPoint")
            {
                KMP_Group_ListBox.Items.AddRange(HPKC_TPKC_Section.HPKCValueList.ToArray());
                if (KMP_Group_ListBox.Items.Count != 0) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;

                CH_KMPGroupPoint.Enabled = true;
            }
            if (KMPSectionComboBox.Text == "Obj")
            {
                KMP_Path_ListBox.Items.AddRange(JBOG_Section.JBOGValueList.ToArray());
                if (KMP_Path_ListBox.Items.Count != 0) KMP_Path_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;

                CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "Route")
            {
                KMP_Group_ListBox.Items.AddRange(ITOP_Section.ITOP_RouteList.ToArray());
                if (KMP_Group_ListBox.Items.Count != 0) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;

                CH_KMPGroupPoint.Enabled = true;
            }
            if (KMPSectionComboBox.Text == "Area")
            {
                KMP_Path_ListBox.Items.AddRange(AERA_Section.AERAValueList.ToArray());
                if (KMP_Path_ListBox.Items.Count != 0) KMP_Path_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;

                CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "Camera")
            {
                KMP_Path_ListBox.Items.AddRange(EMAC_Section.EMACValueList.ToArray());
                if (KMP_Path_ListBox.Items.Count != 0) KMP_Path_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;

                CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "JugemPoint")
            {
                KMP_Path_ListBox.Items.AddRange(TPGJ_Section.TPGJValueList.ToArray());
                if (KMP_Path_ListBox.Items.Count != 0) KMP_Path_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = "XXXX Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = false;

                xXXXRouteImporterToolStripMenuItem.Text = "XXXX Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = false;

                CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                KMP_Group_ListBox.Items.AddRange(HPLG_TPLG_Section.HPLGValueList.ToArray());
                if (KMP_Group_ListBox.Items.Count != 0) KMP_Group_ListBox.SelectedIndex = 0;

                xXXXRouteExporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Exporter";
                xXXXRouteExporterToolStripMenuItem.Enabled = true;

                xXXXRouteImporterToolStripMenuItem.Text = KMPSectionComboBox.Text + " Route Importer";
                xXXXRouteImporterToolStripMenuItem.Enabled = true;

                CH_KMPGroupPoint.Enabled = true;
            }

            outputXXXXAsXmlToolStripMenuItem.Text = "Output " + KMPSectionComboBox.Text + " as Xml";
            inputXmlAsXXXXToolStripMenuItem.Text = "Input Xml as " + KMPSectionComboBox.Text; 
        }

        private void KMP_Group_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KMP_Group_ListBox.SelectedIndex != -1)
            {
                if (KMPSectionComboBox.SelectedIndex == -1) return;
                if (KMPSectionComboBox.Text == "EnemyRoutes")
                {
                    KMP_Path_ListBox.Items.Clear();
                    KMP_Path_ListBox.Items.AddRange(HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList.ToArray());
                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        propertyGrid_KMP_Group.SelectedObject = HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex];
                        CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex]);
                        KMP_Path_ListBox.SelectedIndex = 0;
                    }
                }
                if (KMPSectionComboBox.Text == "ItemRoutes")
                {
                    KMP_Path_ListBox.Items.Clear();
                    KMP_Path_ListBox.Items.AddRange(HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList.ToArray());
                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        propertyGrid_KMP_Group.SelectedObject = HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex];
                        CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex]);
                        KMP_Path_ListBox.SelectedIndex = 0;
                    }
                }
                if (KMPSectionComboBox.Text == "CheckPoint")
                {
                    KMP_Path_ListBox.Items.Clear();
                    KMP_Path_ListBox.Items.AddRange(HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList.ToArray());
                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        propertyGrid_KMP_Group.SelectedObject = HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex];
                        CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex]);
                        KMP_Path_ListBox.SelectedIndex = 0;
                    }
                }
                if (KMPSectionComboBox.Text == "Route")
                {
                    KMP_Path_ListBox.Items.Clear();
                    KMP_Path_ListBox.Items.AddRange(ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList.ToArray());
                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        propertyGrid_KMP_Group.SelectedObject = ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex];
                        CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex]);
                        KMP_Path_ListBox.SelectedIndex = 0;
                    }
                }
                if (KMPSectionComboBox.Text == "GlideRoutes")
                {
                    KMP_Path_ListBox.Items.Clear();
                    KMP_Path_ListBox.Items.AddRange(HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList.ToArray());
                    if (KMP_Path_ListBox.Items.Count != 0)
                    {
                        propertyGrid_KMP_Group.SelectedObject = HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex];
                        CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex]);
                        KMP_Path_ListBox.SelectedIndex = 0;
                    }
                }
            }
        }

        private void KMP_Path_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KMP_Path_ListBox.SelectedIndex == -1) return;
            if (KMPSectionComboBox.Text == "KartPoint")
            {
                propertyGrid_KMP_Path.SelectedObject = TPTK_Section.TPTKValueList[KMP_Path_ListBox.SelectedIndex];
                CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.StartPosition_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            }
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                propertyGrid_KMP_Path.SelectedObject = HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList[KMP_Path_ListBox.SelectedIndex];
            }
            if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                propertyGrid_KMP_Path.SelectedObject = HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList[KMP_Path_ListBox.SelectedIndex];
            }
            if (KMPSectionComboBox.Text == "CheckPoint")
            {
                propertyGrid_KMP_Path.SelectedObject = HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList[KMP_Path_ListBox.SelectedIndex];
            }
            if (KMPSectionComboBox.Text == "Obj")
            {
                propertyGrid_KMP_Path.SelectedObject = JBOG_Section.JBOGValueList[KMP_Path_ListBox.SelectedIndex];
                CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.OBJ_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            }
            if (KMPSectionComboBox.Text == "Route")
            {
                propertyGrid_KMP_Path.SelectedObject = ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList[KMP_Path_ListBox.SelectedIndex];
            }
            if (KMPSectionComboBox.Text == "Area")
            {
                propertyGrid_KMP_Path.SelectedObject = AERA_Section.AERAValueList[KMP_Path_ListBox.SelectedIndex];
                CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.Area_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            }
            if (KMPSectionComboBox.Text == "Camera")
            {
                propertyGrid_KMP_Path.SelectedObject = EMAC_Section.EMACValueList[KMP_Path_ListBox.SelectedIndex];
                CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.Camera_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            }
            if (KMPSectionComboBox.Text == "JugemPoint")
            {
                propertyGrid_KMP_Path.SelectedObject = TPGJ_Section.TPGJValueList[KMP_Path_ListBox.SelectedIndex];
                CH_KMPGroupPoint.Checked = ViewPortObjVisibleSetting.VisibleCheck(render, KMPViewportObject.RespawnPoint_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            }
            if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                propertyGrid_KMP_Path.SelectedObject = HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList[KMP_Path_ListBox.SelectedIndex];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double p = Convert.ToDouble((textBox1.Text != "") ? textBox1.Text : "0");

            for (int Count = 0; Count < KMPViewportObject.Checkpoint_Rail.Count; Count++)
            {
                for (int CP_MDL_L = 0; CP_MDL_L < KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.MV3D_List.Count; CP_MDL_L++)
                {
                    double SX = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.MV3D_List[CP_MDL_L].Content.Transform.Value.M11;
                    double SY = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.MV3D_List[CP_MDL_L].Content.Transform.Value.M22;
                    double SZ = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.MV3D_List[CP_MDL_L].Content.Transform.Value.M33;

                    double TX = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.MV3D_List[CP_MDL_L].Content.Transform.Value.OffsetX;
                    double TY = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.MV3D_List[CP_MDL_L].Content.Transform.Value.OffsetY;
                    double TZ = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.MV3D_List[CP_MDL_L].Content.Transform.Value.OffsetZ;

                    HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = new Vector3D(TX, p, TZ),
                        Scale3D = new Vector3D(SX, SY, SZ),
                        Rotate3D = new Vector3D(0, 0, 0)
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Left.MV3D_List[CP_MDL_L], t);
                    tSRSystem3D.TestTransform3D(new HTK_3DES.TSRSystem.TSRSystem3D.RotationCenterSetting(), HTK_3DES.TSRSystem.TSRSystem3D.RotationType.Angle, 1, null, false);
                }

                for (int CP_MDL_R = 0; CP_MDL_R < KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.MV3D_List.Count; CP_MDL_R++)
                {
                    double SX = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.MV3D_List[CP_MDL_R].Content.Transform.Value.M11;
                    double SY = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.MV3D_List[CP_MDL_R].Content.Transform.Value.M22;
                    double SZ = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.MV3D_List[CP_MDL_R].Content.Transform.Value.M33;

                    double TX = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.MV3D_List[CP_MDL_R].Content.Transform.Value.OffsetX;
                    double TY = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.MV3D_List[CP_MDL_R].Content.Transform.Value.OffsetY;
                    double TZ = KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.MV3D_List[CP_MDL_R].Content.Transform.Value.OffsetZ;

                    HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = new Vector3D(TX, p, TZ),
                        Scale3D = new Vector3D(SX, SY, SZ),
                        Rotate3D = new Vector3D(0, 0, 0),
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.Checkpoint_Rail[Count].Checkpoint_Right.MV3D_List[CP_MDL_R], t);
                    tSRSystem3D.TestTransform3D(new HTK_3DES.TSRSystem.TSRSystem3D.RotationCenterSetting(), HTK_3DES.TSRSystem.TSRSystem3D.RotationType.Angle, 1, null, false);
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

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "") textBox1.Text = "0";
        }

        private void objFlowXmlEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("ObjFlow.bin") == true)
            {
                if (File.Exists("ObjFlowData.xml") == true)
                {
                    ObjFlowXmlEditor objFlowXmlEditor = new ObjFlowXmlEditor();
                    objFlowXmlEditor.Show();
                }
                if (File.Exists("ObjFlowData.xml") == false)
                {
                    string MText = "ObjFlowXmlEditorを使用するにはObjFlowData.xmlが必要です。\r\nObjFlow.binを使用してObjFlowData.xmlを作成しますか?";
                    string Caption = "確認";
                    MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;

                    DialogResult dialogResult = (DialogResult)System.Windows.MessageBox.Show(MText, Caption, messageBoxButton);
                    if(dialogResult == DialogResult.Yes)
                    {
                        var FBOCFormat = KMPs.KMPHelper.ObjFlowReader.Binary.Read("ObjFlow.bin");
                        List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> objFlowValues = KMPs.KMPHelper.ObjFlowReader.ConvertTo.ToObjFlowDB(FBOCFormat);
                        KMPs.KMPHelper.ObjFlowReader.Xml.CreateXml(objFlowValues, "KMPObjectFlow", "KMP_OBJ\\OBJ\\OBJ.obj", "ObjFlowData.xml");

                        ObjFlowXmlEditor objFlowXmlEditor = new ObjFlowXmlEditor();
                        objFlowXmlEditor.Show();
                    }
                    if (dialogResult == DialogResult.No)
                    {
                        System.Windows.MessageBox.Show("ObjFlowData.xmlの作成を中止しました");
                        return;
                    }
                }
            }
            if (File.Exists("ObjFlow.bin") == false) System.Windows.MessageBox.Show("[ObjFlowXmlEditor]\r\nObjFlow.bin : null\r\nObjFlow.binがこのプログラムと同じディレクトリ内に存在しません。\r\nObjFlow.binをこのプログラムと同じディレクトリに配置してください。\r\nObjFlow.binは[RaceCommon.szs]に格納されています。", "Error");
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
                var ObjFlowDB = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml(Open_ObjFlowDataXml.FileName);
                KMPs.KMPHelper.ObjFlowReader.Binary.FBOC FBOC = KMPs.KMPHelper.ObjFlowReader.ConvertTo.ToFBOC(ObjFlowDB);

                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Title = "Save ObjFlow.bin",
                    InitialDirectory = @"C:\Users\User\Desktop",
                    Filter = "bin file|*.bin"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK) KMPs.KMPHelper.ObjFlowReader.Binary.Write(FBOC.ObjFlow_Data, saveFileDialog.FileName + "_NewObjFlow.bin");
                else return;
            }
            else System.Windows.MessageBox.Show("Abort 1");
        }

        private void propertyGrid_KMP_Path_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (KMPSectionComboBox.Text == "KartPoint")
            {
                KMPPropertyGridSettings.TPTK_Section.TPTKValue GetTPTKValue = TPTK_Section.TPTKValueList[KMP_Path_ListBox.SelectedIndex];

                HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                {
                    Translate3D = new Vector3D(GetTPTKValue.Position_Value.X, GetTPTKValue.Position_Value.Y, GetTPTKValue.Position_Value.Z),
                    Scale3D = new Vector3D(20, 20, 20),
                    Rotate3D = new Vector3D(GetTPTKValue.Rotate_Value.X, GetTPTKValue.Rotate_Value.Y, GetTPTKValue.Rotate_Value.Z)
                };

                HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.StartPosition_MV3DList[KMP_Path_ListBox.SelectedIndex], t);
                tSRSystem3D.TestTransform3D();
            }
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue.TPNEValue GetTPNEValue = HPNE_TPNE_Section.HPNEValueList[KMP_Group_ListBox.SelectedIndex].TPNEValueList[KMP_Path_ListBox.SelectedIndex];

                    HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = new Vector3D(GetTPNEValue.Positions.X, GetTPNEValue.Positions.Y, GetTPNEValue.Positions.Z),
                        Scale3D = new Vector3D(GetTPNEValue.Control * 100, GetTPNEValue.Control * 100, GetTPNEValue.Control * 100),
                        Rotate3D = new Vector3D(0, 0, 0)
                    };

                    //パスの形を変更
                    HTK_3DES.PathTools.Rail rail = KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex];
                    if (rail.TV3D_List.Count != 0) rail.MoveRails(KMP_Path_ListBox.SelectedIndex, t.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List[KMP_Path_ListBox.SelectedIndex], t);
                    tSRSystem3D.TestTransform3D();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
            if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue.TPTIValue GetTPTIValue = HPTI_TPTI_Section.HPTIValueList[KMP_Group_ListBox.SelectedIndex].TPTIValueList[KMP_Path_ListBox.SelectedIndex];

                    HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = new Vector3D(GetTPTIValue.TPTI_Positions.X, GetTPTIValue.TPTI_Positions.Y, GetTPTIValue.TPTI_Positions.Z),
                        Scale3D = new Vector3D(GetTPTIValue.TPTI_PointSize * 100, GetTPTIValue.TPTI_PointSize * 100, GetTPTIValue.TPTI_PointSize * 100),
                        Rotate3D = new Vector3D(0, 0, 0)
                    };

                    //パスの形を変更
                    HTK_3DES.PathTools.Rail rail = KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex];
                    if (rail.TV3D_List.Count != 0) rail.MoveRails(KMP_Path_ListBox.SelectedIndex, t.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List[KMP_Path_ListBox.SelectedIndex], t);
                    tSRSystem3D.TestTransform3D();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
            if (KMPSectionComboBox.Text == "CheckPoint")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    #region Point Left
                    KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue GetTPKCValue_Left = HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList[KMP_Path_ListBox.SelectedIndex];

                    HTK_3DES.TSRSystem.Transform t_Left = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = new Vector3D(GetTPKCValue_Left.Position_2D_Left.X, float.Parse(textBox1.Text), GetTPKCValue_Left.Position_2D_Left.Y),
                        Scale3D = new Vector3D(50, 50, 50),
                        Rotate3D = new Vector3D(0, 0, 0)
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_tLeft = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Left.MV3D_List[KMP_Path_ListBox.SelectedIndex], t_Left);
                    tSRSystem3D_tLeft.TestTransform3D();

                    //パスの形を変更(Green)
                    HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint_Left = KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex];
                    if (checkpoint_Left.Checkpoint_Left.LV3D_List.Count != 0) checkpoint_Left.Checkpoint_Left.MoveRails(KMP_Path_ListBox.SelectedIndex, t_Left.Translate3D, HTK_3DES.PathTools.Rail.RailType.Line);
                    KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line[KMP_Path_ListBox.SelectedIndex].Points[0] = t_Left.Translate3D.ToPoint3D();

                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[KMP_Path_ListBox.SelectedIndex].Content).Positions[2] = new Point3D(t_Left.Translate3D.X, 0, t_Left.Translate3D.Z);
                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[KMP_Path_ListBox.SelectedIndex].Content).Positions[3] = t_Left.Translate3D.ToPoint3D();

                    if (checkpoint_Left.SideWall_Left.Count != 0) checkpoint_Left.MoveSideWalls(KMP_Path_ListBox.SelectedIndex, t_Left.Translate3D, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint.SideWallType.Left);
                    #endregion

                    #region Point_Right
                    KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue.TPKCValue GetTPKCValue_Right = HPKC_TPKC_Section.HPKCValueList[KMP_Group_ListBox.SelectedIndex].TPKCValueList[KMP_Path_ListBox.SelectedIndex];

                    HTK_3DES.TSRSystem.Transform t_Right = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = new Vector3D(GetTPKCValue_Right.Position_2D_Right.X, float.Parse(textBox1.Text), GetTPKCValue_Right.Position_2D_Right.Y),
                        Scale3D = new Vector3D(50, 50, 50),
                        Rotate3D = new Vector3D(0, 0, 0)
                    };

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D_tRight = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Right.MV3D_List[KMP_Path_ListBox.SelectedIndex], t_Right);
                    tSRSystem3D_tRight.TestTransform3D();

                    //パスの形を変更(Red)
                    HTK_3DES.KMP_3DCheckpointSystem.Checkpoint checkpoint_Right = KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex];
                    if (checkpoint_Right.Checkpoint_Right.LV3D_List.Count != 0) checkpoint_Right.Checkpoint_Right.MoveRails(KMP_Path_ListBox.SelectedIndex, t_Right.Translate3D, HTK_3DES.PathTools.Rail.RailType.Line);
                    KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_Line[KMP_Path_ListBox.SelectedIndex].Points[1] = t_Right.Translate3D.ToPoint3D();

                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[KMP_Path_ListBox.SelectedIndex].Content).Positions[0] = new Point3D(t_Right.Translate3D.X, 0, t_Right.Translate3D.Z);
                    HTK_3DES.OBJData.GetMeshGeometry3D(KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex].Checkpoint_SplitWallMDL[KMP_Path_ListBox.SelectedIndex].Content).Positions[1] = t_Right.Translate3D.ToPoint3D();

                    if (checkpoint_Right.SideWall_Right.Count != 0) checkpoint_Right.MoveSideWalls(KMP_Path_ListBox.SelectedIndex, t_Right.Translate3D, HTK_3DES.KMP_3DCheckpointSystem.Checkpoint.SideWallType.Right);
                    #endregion
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
            if (KMPSectionComboBox.Text == "Obj")
            {
                render.MainViewPort.Children.Remove(KMPViewportObject.OBJ_MV3DList[KMP_Path_ListBox.SelectedIndex]);
                KMPViewportObject.OBJ_MV3DList.Remove(KMPViewportObject.OBJ_MV3DList[KMP_Path_ListBox.SelectedIndex]);

                KMPPropertyGridSettings.JBOG_Section.JBOGValue GetJBOGValue = JBOG_Section.JBOGValueList[KMP_Path_ListBox.SelectedIndex];

                List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDB_FindName = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml");
                string ObjectName = ObjFlowDB_FindName.Find(x => x.ObjectID == GetJBOGValue.ObjectID).ObjectName;
                JBOG_Section.JBOGValueList[KMP_Path_ListBox.SelectedIndex].ObjectName = ObjectName;

                #region Add Model(OBJ)
                HTK_3DES.TSRSystem.Transform OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform
                {
                    Translate3D = GetJBOGValue.Positions.GetVector3D(),
                    Scale3D = HTK_3DES.TSRSystem.ScaleFactor(GetJBOGValue.Scales.GetVector3D(), 2),
                    Rotate3D = GetJBOGValue.Rotations.GetVector3D()
                };

                List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDB = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml");
                string Path = ObjFlowDB.Find(x => x.ObjectID == GetJBOGValue.ObjectID).Path;
                ModelVisual3D dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(Path);

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_OBJ, "OBJ " + KMP_Path_ListBox.SelectedIndex + " " + -1);

                HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
                tSRSystem3D.TestTransform3D();

                KMPViewportObject.OBJ_MV3DList.Insert(KMP_Path_ListBox.SelectedIndex, dv3D_OBJ);

                render.MainViewPort.Children.Insert(KMP_Path_ListBox.SelectedIndex, dv3D_OBJ);
                #endregion

                KMP_Path_ListBox.Items.Clear();
                KMP_Path_ListBox.Items.AddRange(JBOG_Section.JBOGValueList.ToArray());
                KMP_Path_ListBox.SelectedIndex = int.Parse(dv3D_OBJ.GetName().Split(' ')[1]);
            }
            if (KMPSectionComboBox.Text == "Route")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    KMPPropertyGridSettings.ITOP_Section.ITOP_Route.ITOP_Point GetITOPValue = ITOP_Section.ITOP_RouteList[KMP_Group_ListBox.SelectedIndex].ITOP_PointList[KMP_Path_ListBox.SelectedIndex];

                    HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = new Vector3D(GetITOPValue.Positions.X, GetITOPValue.Positions.Y, GetITOPValue.Positions.Z),
                        Scale3D = new Vector3D(20, 20, 20),
                        Rotate3D = new Vector3D(0, 0, 0)
                    };

                    //パスの形を変更
                    HTK_3DES.PathTools.Rail rail = KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex];
                    if (rail.TV3D_List.Count != 0) rail.MoveRails(KMP_Path_ListBox.SelectedIndex, t.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex].MV3D_List[KMP_Path_ListBox.SelectedIndex], t);
                    tSRSystem3D.TestTransform3D();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
            if (KMPSectionComboBox.Text == "Area")
            {
                render.MainViewPort.Children.Remove(KMPViewportObject.Area_MV3DList[KMP_Path_ListBox.SelectedIndex]);
                KMPViewportObject.Area_MV3DList.Remove(KMPViewportObject.Area_MV3DList[KMP_Path_ListBox.SelectedIndex]);

                KMPPropertyGridSettings.AERA_Section.AERAValue GetAERAValue = AERA_Section.AERAValueList[KMP_Path_ListBox.SelectedIndex];

                #region Add Model(OBJ)
                HTK_3DES.TSRSystem.Transform Area_transform_Value = new HTK_3DES.TSRSystem.Transform
                {
                    Translate3D = GetAERAValue.Positions.GetVector3D(),
                    Scale3D = HTK_3DES.TSRSystem.ScaleFactor(GetAERAValue.Scales.GetVector3D(), 2000),
                    Rotate3D = GetAERAValue.Rotations.GetVector3D()
                };

                ModelVisual3D dv3D_AreaOBJ = null;
                if (GetAERAValue.AreaModeSettings.AreaModeValue == 0) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                if (GetAERAValue.AreaModeSettings.AreaModeValue == 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomCylinderVisual3D(Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));
                if (GetAERAValue.AreaModeSettings.AreaModeValue > 1) dv3D_AreaOBJ = HTK_3DES.CustomModelCreateHelper.CustomBoxVisual3D(new Vector3D(1, 1, 1), new Point3D(0, 0, 0), Color.FromArgb(0x80, 0xFF, 0x00, 0x00), Color.FromArgb(0x80, 0xFF, 0x00, 0x00));

                //モデルの名前と番号を文字列に格納(情報化)
                HTK_3DES.TSRSystem.SetString_MV3D(dv3D_AreaOBJ, "Area " + KMP_Path_ListBox.SelectedIndex + " " + -1);

                HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_AreaOBJ, Area_transform_Value);
                tSRSystem3D.TestTransform3D();

                KMPViewportObject.Area_MV3DList.Insert(KMP_Path_ListBox.SelectedIndex, dv3D_AreaOBJ);

                render.MainViewPort.Children.Insert(KMP_Path_ListBox.SelectedIndex, dv3D_AreaOBJ);
                #endregion
            }
            if (KMPSectionComboBox.Text == "Camera")
            {
                KMPPropertyGridSettings.EMAC_Section.EMACValue GetAERAValue = EMAC_Section.EMACValueList[KMP_Path_ListBox.SelectedIndex];

                HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                {
                    Translate3D = new Vector3D(GetAERAValue.Positions.X, GetAERAValue.Positions.Y, GetAERAValue.Positions.Z),
                    Scale3D = new Vector3D(20, 20, 20),
                    Rotate3D = new Vector3D(GetAERAValue.Rotations.X, GetAERAValue.Rotations.Y, GetAERAValue.Rotations.Z)
                };

                HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.Camera_MV3DList[KMP_Path_ListBox.SelectedIndex], t);
                tSRSystem3D.TestTransform3D();
            }
            if (KMPSectionComboBox.Text == "JugemPoint")
            {
                KMPPropertyGridSettings.TPGJ_Section.TPGJValue GetTPGJValue = TPGJ_Section.TPGJValueList[KMP_Path_ListBox.SelectedIndex];

                HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                {
                    Translate3D = new Vector3D(GetTPGJValue.Positions.X, GetTPGJValue.Positions.Y, GetTPGJValue.Positions.Z),
                    Scale3D = new Vector3D(20, 20, 20),
                    Rotate3D = new Vector3D(GetTPGJValue.Rotations.X, GetTPGJValue.Rotations.Y, GetTPGJValue.Rotations.Z)
                };

                HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.RespawnPoint_MV3DList[KMP_Path_ListBox.SelectedIndex], t);
                tSRSystem3D.TestTransform3D();
            }
            if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                if (KMP_Group_ListBox.SelectedIndex != -1)
                {
                    KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue.TPLGValue GetTPLGValue = HPLG_TPLG_Section.HPLGValueList[KMP_Group_ListBox.SelectedIndex].TPLGValueList[KMP_Path_ListBox.SelectedIndex];

                    HTK_3DES.TSRSystem.Transform t = new HTK_3DES.TSRSystem.Transform
                    {
                        Translate3D = new Vector3D(GetTPLGValue.Positions.X, GetTPLGValue.Positions.Y, GetTPLGValue.Positions.Z),
                        Scale3D = HTK_3DES.TSRSystem.ScaleFactor(GetTPLGValue.TPLG_PointScaleValue, 100),
                        Rotate3D = new Vector3D(0, 0, 0)
                    };

                    //パスの形を変更
                    HTK_3DES.PathTools.Rail rail = KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex];
                    if (rail.TV3D_List.Count != 0) rail.MoveRails(KMP_Path_ListBox.SelectedIndex, t.Translate3D, HTK_3DES.PathTools.Rail.RailType.Tube);

                    HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex].MV3D_List[KMP_Path_ListBox.SelectedIndex], t);
                    tSRSystem3D.TestTransform3D();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Group : Null");
                }
            }
        }

        private void closeKMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TPTK_Section = null;
            HPNE_TPNE_Section = null;
            HPTI_TPTI_Section = null;
            HPKC_TPKC_Section = null;
            JBOG_Section = null;
            ITOP_Section = null;
            AERA_Section = null;
            EMAC_Section = null;
            TPGJ_Section = null;
            IGTS_Section = null;
            HPLG_TPLG_Section = null;

            for (int del = 0; del < KMPViewportObject.StartPosition_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.StartPosition_MV3DList[del]);
            KMPViewportObject.StartPosition_MV3DList.Clear();

            for (int del = 0; del < KMPViewportObject.EnemyRoute_Rail_List.Count; del++) KMPViewportObject.EnemyRoute_Rail_List[del].DeleteRail(render);
            KMPViewportObject.EnemyRoute_Rail_List.Clear();

            for (int del = 0; del < KMPViewportObject.ItemRoute_Rail_List.Count; del++) KMPViewportObject.ItemRoute_Rail_List[del].DeleteRail(render);
            KMPViewportObject.ItemRoute_Rail_List.Clear();

            for (int del = 0; del < KMPViewportObject.Checkpoint_Rail.Count; del++) KMPViewportObject.Checkpoint_Rail[del].DeleteRailChk(render);
            KMPViewportObject.ItemRoute_Rail_List.Clear();

            for (int del = 0; del < KMPViewportObject.OBJ_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.OBJ_MV3DList[del]);
            KMPViewportObject.OBJ_MV3DList.Clear();

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
            KMP_Path_ListBox.Items.Clear();
            propertyGrid_KMP_Group.SelectedObject = null;
            propertyGrid_KMP_Path.SelectedObject = null;
            propertyGrid_KMP_StageInfo.SelectedObject = null;
            KMPVersion_TXT.Text = "";
            KMPSectionComboBox.Items.Clear();

            writeBinaryToolStripMenuItem.Enabled = false;
            closeKMPToolStripMenuItem.Enabled = false;
            exportToolStripMenuItem.Enabled = false;

            inputXmlAsXXXXToolStripMenuItem.Enabled = false;
            xXXXRouteImporterToolStripMenuItem.Enabled = false;
        }

        private void KMPVisibility_CheckedChanged(object sender, EventArgs e)
        {
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Kartpoint.Checked, render, KMPViewportObject.StartPosition_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_EnemyRoutes.Checked, render, KMPViewportObject.EnemyRoute_Rail_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_ItemRoutes.Checked, render, KMPViewportObject.ItemRoute_Rail_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Checkpoint.Checked, render, KMPViewportObject.Checkpoint_Rail);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_OBJ.Checked, render, KMPViewportObject.OBJ_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Routes.Checked, render, KMPViewportObject.Routes_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Area.Checked, render, KMPViewportObject.Area_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Camera.Checked, render, KMPViewportObject.Camera_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Returnpoints.Checked, render, KMPViewportObject.RespawnPoint_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_GlideRoutes.Checked, render, KMPViewportObject.GlideRoute_Rail_List);
        }

        private void createKMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TPTK_Section = new KMPPropertyGridSettings.TPTK_Section();
            HPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section();
            HPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section();
            HPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section();
            JBOG_Section = new KMPPropertyGridSettings.JBOG_Section();
            ITOP_Section = new KMPPropertyGridSettings.ITOP_Section();
            AERA_Section = new KMPPropertyGridSettings.AERA_Section();
            EMAC_Section = new KMPPropertyGridSettings.EMAC_Section();
            TPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section();
            IGTS_Section = new KMPPropertyGridSettings.IGTS_Section();
            HPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section();

            KMPVersion_TXT.Text = "3100";

            propertyGrid_KMP_StageInfo.SelectedObject = IGTS_Section;

            if (KMPSectionComboBox.Items.Count == 0)
            {
                string[] AllSectionAry = new string[] { "KartPoint", "EnemyRoutes", "ItemRoutes", "CheckPoint", "Obj", "Route", "Area", "Camera", "JugemPoint", "GlideRoutes" };
                KMPSectionComboBox.Items.AddRange(AllSectionAry.ToArray());
            }

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
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "xml file|*.xml"
                //FileName = ""
            };

            if (Save_KMPXML.ShowDialog() != DialogResult.OK) return;

            KMPPropertyGridSettings kMPPropertyGridSettings = new KMPPropertyGridSettings
            {
                TPTKSection = TPTK_Section,
                HPNE_TPNESection = HPNE_TPNE_Section,
                HPTI_TPTISection = HPTI_TPTI_Section,
                HPKC_TPKCSection = HPKC_TPKC_Section,
                JBOGSection = JBOG_Section,
                ITOPSection = ITOP_Section,
                AERASection = AERA_Section,
                EMACSection = EMAC_Section,
                TPGJSection = TPGJ_Section,
                IGTSSection = IGTS_Section,
                HPLG_TPLGSection = HPLG_TPLG_Section
            };

            XMLExporter.ExportAll(kMPPropertyGridSettings, Save_KMPXML.FileName);
        }

        private void outputXXXXAsXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save_KMPXML = new SaveFileDialog()
            {
                Title = "Save XML(KMP)",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "xml file|*.xml"
                //FileName = ""
            };

            if (Save_KMPXML.ShowDialog() != DialogResult.OK) return;

            KMPPropertyGridSettings kMPPropertyGridSettings = new KMPPropertyGridSettings
            {
                TPTKSection = TPTK_Section,
                HPNE_TPNESection = HPNE_TPNE_Section,
                HPTI_TPTISection = HPTI_TPTI_Section,
                HPKC_TPKCSection = HPKC_TPKC_Section,
                JBOGSection = JBOG_Section,
                ITOPSection = ITOP_Section,
                AERASection = AERA_Section,
                EMACSection = EMAC_Section,
                TPGJSection = TPGJ_Section,
                IGTSSection = IGTS_Section,
                HPLG_TPLGSection = HPLG_TPLG_Section
            };

            if (KMPSectionComboBox.Text == "KartPoint") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.KartPoint);
            if (KMPSectionComboBox.Text == "EnemyRoutes") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.EnemyRoutes);
            if (KMPSectionComboBox.Text == "ItemRoutes") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.ItemRoutes);
            if (KMPSectionComboBox.Text == "CheckPoint") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.CheckPoint);
            if (KMPSectionComboBox.Text == "Obj") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.Obj);
            if (KMPSectionComboBox.Text == "Route") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.Route);
            if (KMPSectionComboBox.Text == "Area") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.Area);
            if (KMPSectionComboBox.Text == "Camera") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.Camera);
            if (KMPSectionComboBox.Text == "JugemPoint") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.JugemPoint);
            if (KMPSectionComboBox.Text == "GlideRoutes") XMLExporter.ExportSection(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.KMPXmlSetting.Section.GlideRoutes);
        }

        private void XXXXRouteExporterTSM_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save_KMPXML = new SaveFileDialog()
            {
                Title = "Save XML(KMP)",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "xml file|*.xml"
                //FileName = ""
            };

            if (Save_KMPXML.ShowDialog() != DialogResult.OK) return;

            KMPPropertyGridSettings kMPPropertyGridSettings = new KMPPropertyGridSettings
            {
                TPTKSection = TPTK_Section,
                HPNE_TPNESection = HPNE_TPNE_Section,
                HPTI_TPTISection = HPTI_TPTI_Section,
                HPKC_TPKCSection = HPKC_TPKC_Section,
                JBOGSection = JBOG_Section,
                ITOPSection = ITOP_Section,
                AERASection = AERA_Section,
                EMACSection = EMAC_Section,
                TPGJSection = TPGJ_Section,
                IGTSSection = IGTS_Section,
                HPLG_TPLGSection = HPLG_TPLG_Section
            };

            if (KMPSectionComboBox.Text == "EnemyRoutes") XMLExporter.ExportXXXXRoute(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.XXXXRouteXmlSetting.RouteType.EnemyRoute);
            if (KMPSectionComboBox.Text == "ItemRoutes") XMLExporter.ExportXXXXRoute(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.XXXXRouteXmlSetting.RouteType.ItemRoute);
            if (KMPSectionComboBox.Text == "GlideRoutes") XMLExporter.ExportXXXXRoute(kMPPropertyGridSettings, Save_KMPXML.FileName, TestXml.XXXXRouteXmlSetting.RouteType.GlideRoute);
        }

        private void ImportAllSectionTSM_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Title = "Open Xml",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "xml file|*.xml"
            };

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            KMPViewportObject = new KMPs.KMPViewportObject();

            KMPPropertyGridSettings kMPPropertyGridSettings = XMLImporter.ImportAll(KMPViewportObject, openFileDialog1.FileName, render, textBox1.Text);

            TPTK_Section = kMPPropertyGridSettings.TPTKSection;
            HPNE_TPNE_Section = kMPPropertyGridSettings.HPNE_TPNESection;
            HPTI_TPTI_Section = kMPPropertyGridSettings.HPTI_TPTISection;
            HPKC_TPKC_Section = kMPPropertyGridSettings.HPKC_TPKCSection;
            JBOG_Section = kMPPropertyGridSettings.JBOGSection;
            ITOP_Section = kMPPropertyGridSettings.ITOPSection;
            AERA_Section = kMPPropertyGridSettings.AERASection;
            EMAC_Section = kMPPropertyGridSettings.EMACSection;
            TPGJ_Section = kMPPropertyGridSettings.TPGJSection;
            IGTS_Section = kMPPropertyGridSettings.IGTSSection;
            HPLG_TPLG_Section = kMPPropertyGridSettings.HPLG_TPLGSection;

            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Area.Checked, render, KMPViewportObject.StartPosition_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_EnemyRoutes.Checked, render, KMPViewportObject.EnemyRoute_Rail_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_ItemRoutes.Checked, render, KMPViewportObject.ItemRoute_Rail_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Checkpoint.Checked, render, KMPViewportObject.Checkpoint_Rail);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_OBJ.Checked, render, KMPViewportObject.OBJ_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Routes.Checked, render, KMPViewportObject.Routes_List);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Area.Checked, render, KMPViewportObject.Area_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Camera.Checked, render, KMPViewportObject.Camera_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_Returnpoints.Checked, render, KMPViewportObject.RespawnPoint_MV3DList);
            ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_GlideRoutes.Checked, render, KMPViewportObject.GlideRoute_Rail_List);

            string[] AllSectionAry = new string[] { "KartPoint", "EnemyRoutes", "ItemRoutes", "CheckPoint", "Obj", "Route", "Area", "Camera", "JugemPoint", "GlideRoutes" };
            KMPSectionComboBox.Items.AddRange(AllSectionAry.ToArray());
            KMPSectionComboBox.SelectedIndex = 0;

            if (IGTS_Section != null)
            {
                //Display only IGTS section directly to PropertyGrid
                propertyGrid_KMP_StageInfo.SelectedObject = IGTS_Section;
            }

            writeBinaryToolStripMenuItem.Enabled = true;
            closeKMPToolStripMenuItem.Enabled = true;
            exportToolStripMenuItem.Enabled = true;
            xXXXRouteImporterToolStripMenuItem.Enabled = true;
            inputXmlAsXXXXToolStripMenuItem.Enabled = true;
        }

        private void InputXmlAsXXXXTSM_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Title = "Open Xml",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "xml file|*.xml"
            };

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            if (KMPSectionComboBox.Text == "KartPoint")
            {
                for (int del = 0; del < KMPViewportObject.StartPosition_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.StartPosition_MV3DList[del]);
                KMPViewportObject.StartPosition_MV3DList.Clear();

                KMPViewportObject.StartPosition_MV3DList = new List<ModelVisual3D>();
                TPTK_Section = XMLImporter.ImportKartPosition(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                for (int del = 0; del < KMPViewportObject.EnemyRoute_Rail_List.Count; del++) KMPViewportObject.EnemyRoute_Rail_List[del].DeleteRail(render);
                KMPViewportObject.EnemyRoute_Rail_List.Clear();

                KMPViewportObject.EnemyRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                HPNE_TPNE_Section = XMLImporter.ImportEnemyRoute(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                for (int del = 0; del < KMPViewportObject.ItemRoute_Rail_List.Count; del++) KMPViewportObject.ItemRoute_Rail_List[del].DeleteRail(render);
                KMPViewportObject.ItemRoute_Rail_List.Clear();

                KMPViewportObject.ItemRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                HPTI_TPTI_Section = XMLImporter.ImportItemRoute(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "CheckPoint")
            {
                for (int del = 0; del < KMPViewportObject.Checkpoint_Rail.Count; del++) KMPViewportObject.Checkpoint_Rail[del].DeleteRailChk(render);
                KMPViewportObject.Checkpoint_Rail.Clear();

                KMPViewportObject.Checkpoint_Rail = new List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint>();
                HPKC_TPKC_Section = XMLImporter.ImportCheckpoint(KMPViewportObject, openFileDialog1.FileName, render, textBox1.Text);
            }
            if (KMPSectionComboBox.Text == "Obj")
            {
                for (int del = 0; del < KMPViewportObject.OBJ_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.OBJ_MV3DList[del]);
                KMPViewportObject.OBJ_MV3DList.Clear();

                KMPViewportObject.OBJ_MV3DList = new List<ModelVisual3D>();
                JBOG_Section = XMLImporter.ImportObject(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "Route")
            {
                for (int del = 0; del < KMPViewportObject.Routes_List.Count; del++) KMPViewportObject.Routes_List[del].DeleteRail(render);
                KMPViewportObject.Routes_List.Clear();

                KMPViewportObject.Routes_List = new List<HTK_3DES.PathTools.Rail>();
                ITOP_Section = XMLImporter.ImportRoute(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "Area")
            {
                for (int del = 0; del < KMPViewportObject.Area_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.Area_MV3DList[del]);
                KMPViewportObject.Area_MV3DList.Clear();

                KMPViewportObject.Area_MV3DList = new List<ModelVisual3D>();
                AERA_Section = XMLImporter.ImportArea(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "Camera")
            {
                for (int del = 0; del < KMPViewportObject.Camera_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.Camera_MV3DList[del]);
                KMPViewportObject.Camera_MV3DList.Clear();

                KMPViewportObject.Camera_MV3DList = new List<ModelVisual3D>();
                EMAC_Section = XMLImporter.ImportCamera(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "JugemPoint")
            {
                for (int del = 0; del < KMPViewportObject.RespawnPoint_MV3DList.Count; del++) render.MainViewPort.Children.Remove(KMPViewportObject.RespawnPoint_MV3DList[del]);
                KMPViewportObject.RespawnPoint_MV3DList.Clear();

                KMPViewportObject.RespawnPoint_MV3DList = new List<ModelVisual3D>();
                TPGJ_Section = XMLImporter.ImportJugemPoint(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                for (int del = 0; del < KMPViewportObject.GlideRoute_Rail_List.Count; del++) KMPViewportObject.GlideRoute_Rail_List[del].DeleteRail(render);
                KMPViewportObject.GlideRoute_Rail_List.Clear();

                KMPViewportObject.GlideRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                HPLG_TPLG_Section = XMLImporter.ImportGlideRoute(KMPViewportObject, openFileDialog1.FileName, render);
            }

            KMPSectionComboBox.SelectedIndex = 0;
        }

        private void xXXXRouteImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Title = "Open Xml",
                InitialDirectory = @"C:\Users\User\Desktop",
                Filter = "xml file|*.xml"
            };

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                for (int del = 0; del < KMPViewportObject.EnemyRoute_Rail_List.Count; del++) KMPViewportObject.EnemyRoute_Rail_List[del].DeleteRail(render);
                KMPViewportObject.EnemyRoute_Rail_List.Clear();

                KMPViewportObject.EnemyRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                HPNE_TPNE_Section = XMLImporter.ImportEnemyRoutePositionAndScaleOnly(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                for (int del = 0; del < KMPViewportObject.ItemRoute_Rail_List.Count; del++) KMPViewportObject.ItemRoute_Rail_List[del].DeleteRail(render);
                KMPViewportObject.ItemRoute_Rail_List.Clear();

                KMPViewportObject.ItemRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                HPTI_TPTI_Section = XMLImporter.ImportItemRoutePositionAndScaleOnly(KMPViewportObject, openFileDialog1.FileName, render);
            }
            if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                for (int del = 0; del < KMPViewportObject.GlideRoute_Rail_List.Count; del++) KMPViewportObject.GlideRoute_Rail_List[del].DeleteRail(render);
                KMPViewportObject.GlideRoute_Rail_List.Clear();

                KMPViewportObject.GlideRoute_Rail_List = new List<HTK_3DES.PathTools.Rail>();
                HPLG_TPLG_Section = XMLImporter.ImportGlideRoutePositionAndScaleOnly(KMPViewportObject, openFileDialog1.FileName, render);
            }

            KMPSectionComboBox.SelectedIndex = 0;
        }

        private void KMP_Path_ListBox_DoubleClick(object sender, EventArgs e)
        {
            if (KMPSectionComboBox.Text == "KartPoint")
            {
                render.FindObject(TPTK_Section.TPTKValueList, KMP_Path_ListBox.SelectedIndex);
            }
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                if (tabControl1.SelectedIndex == 1) render.FindObject(HPNE_TPNE_Section.HPNEValueList, KMP_Path_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex);
            }
            if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                if (tabControl1.SelectedIndex == 1) render.FindObject(HPTI_TPTI_Section.HPTIValueList, KMP_Path_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex);
            }
            if (KMPSectionComboBox.Text == "CheckPoint")
            {
                if (KMPChkpt_RDTBtn_L.Checked == true)
                {
                    double OffsetValue = Convert.ToDouble(textBox1.Text);
                    render.FindObject(HPKC_TPKC_Section.HPKCValueList, KMP_Path_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex, UserControl1.CheckpointSearchOption.Left, OffsetValue);
                }
                if (KMPChkpt_RDTBtn_R.Checked == true)
                {
                    double OffsetValue = Convert.ToDouble(textBox1.Text);
                    render.FindObject(HPKC_TPKC_Section.HPKCValueList, KMP_Path_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex, UserControl1.CheckpointSearchOption.Right, OffsetValue);
                }
            }
            if (KMPSectionComboBox.Text == "Obj")
            {
                render.FindObject(JBOG_Section.JBOGValueList, KMP_Path_ListBox.SelectedIndex);
            }
            if (KMPSectionComboBox.Text == "Route")
            {
                if (tabControl1.SelectedIndex == 1) render.FindObject(ITOP_Section.ITOP_RouteList, KMP_Path_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex);
            }
            if (KMPSectionComboBox.Text == "Area")
            {
                render.FindObject(AERA_Section.AERAValueList, KMP_Path_ListBox.SelectedIndex);
            }
            if (KMPSectionComboBox.Text == "Camera")
            {
                render.FindObject(EMAC_Section.EMACValueList, KMP_Path_ListBox.SelectedIndex);
            }
            if (KMPSectionComboBox.Text == "JugemPoint")
            {
                render.FindObject(TPGJ_Section.TPGJValueList, KMP_Path_ListBox.SelectedIndex);
            }
            if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                if (tabControl1.SelectedIndex == 1) render.FindObject(HPLG_TPLG_Section.HPLGValueList, KMP_Path_ListBox.SelectedIndex, KMP_Group_ListBox.SelectedIndex);
            }
        }

        private void KMPVisibilityGroupPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (KMPSectionComboBox.Text == "KartPoint") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.StartPosition_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "EnemyRoutes") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.EnemyRoute_Rail_List[KMP_Group_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "ItemRoutes") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.ItemRoute_Rail_List[KMP_Group_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "CheckPoint") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.Checkpoint_Rail[KMP_Group_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "Obj") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.OBJ_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "Route") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.Routes_List[KMP_Group_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "Area") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.Area_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "Camera") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.Camera_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "JugemPoint") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.RespawnPoint_MV3DList[KMP_Path_ListBox.SelectedIndex]);
            if (KMPSectionComboBox.Text == "GlideRoutes") ViewPortObjVisibleSetting.ViewportObj_Visibility(CH_KMPGroupPoint.Checked, render, KMPViewportObject.GlideRoute_Rail_List[KMP_Group_ListBox.SelectedIndex]);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KMPSectionComboBox.Text == "KartPoint")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "CheckPoint")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "Obj")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "Route")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "Area")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "Camera")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "JugemPoint")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
            if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                if (tabControl1.SelectedIndex == 0) CH_KMPGroupPoint.Enabled = true;
                if (tabControl1.SelectedIndex == 1) CH_KMPGroupPoint.Enabled = false;
                if (tabControl1.SelectedIndex == 2) CH_KMPGroupPoint.Enabled = false;
            }
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KMPPropertyGridSettings kMPPropertyGridSettings = new KMPPropertyGridSettings
            {
                TPTKSection = TPTK_Section,
                HPNE_TPNESection = HPNE_TPNE_Section,
                HPTI_TPTISection = HPTI_TPTI_Section,
                HPKC_TPKCSection = HPKC_TPKC_Section,
                JBOGSection = JBOG_Section,
                ITOPSection = ITOP_Section,
                AERASection = AERA_Section,
                EMACSection = EMAC_Section,
                TPGJSection = TPGJ_Section,
                IGTSSection = IGTS_Section,
                HPLG_TPLGSection = HPLG_TPLG_Section
            };

            KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("All", kMPPropertyGridSettings);
            kMPErrorCheck.Show();
        }

        private void thisSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KMPPropertyGridSettings KMPPropertyGridSetting = new KMPPropertyGridSettings
            {
                TPTKSection = TPTK_Section,
                HPNE_TPNESection = HPNE_TPNE_Section,
                HPTI_TPTISection = HPTI_TPTI_Section,
                HPKC_TPKCSection = HPKC_TPKC_Section,
                JBOGSection = JBOG_Section,
                ITOPSection = ITOP_Section,
                AERASection = AERA_Section,
                EMACSection = EMAC_Section,
                TPGJSection = TPGJ_Section,
                IGTSSection = IGTS_Section,
                HPLG_TPLGSection = HPLG_TPLG_Section
            };

            if (KMPSectionComboBox.Text == "KartPoint")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Kart Point", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "EnemyRoutes")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Enemy Route", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "ItemRoutes")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Item Route", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "CheckPoint")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Checkpoint", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "Obj")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Object", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "Route")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Route", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "Area")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Area", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "Camera")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Camera", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "JugemPoint")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Jugem Point", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
            if (KMPSectionComboBox.Text == "GlideRoutes")
            {
                KMPErrorCheck kMPErrorCheck = new KMPErrorCheck("Glide Route", KMPPropertyGridSetting);
                kMPErrorCheck.Show();
            }
        }

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            KMP3DEditorInfoForm kMP3DEditorInfoForm = new KMP3DEditorInfoForm();
            kMP3DEditorInfoForm.ShowDialog();
        }
	}
}