using KMPLibrary.KMPHelper;
using KMPLibrary.XMLConvert.ObjFlowData;
using MK7_3D_KMP_Editor.PropertyGridObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MK7_3D_KMP_Editor
{
    public partial class KMPErrorCheck : Form
    {
        public string SectionName { get; set; }

        public enum CheckValueType
        {
            Info,
            Warning,
            Error
        }

        //public static List<CheckValue> CheckValues { get; set; } = new List<CheckValue>();
        public class CheckValue
        {
            public int GroupNum { get; set; }
            public int ValueNum { get; set; }
            public string Description { get; set; }
            public CheckValueType CheckValueType { get; set; }
        }

        public KMP_Main_PGS KMPPropertyGridSettings { get; set; }

        public KMPErrorCheck(string Section_Name, object KMP_Props)
        {
            InitializeComponent();

            TopMost = true;

            SectionName = Section_Name;

            if (KMP_Props is KMP_Main_PGS)
            {
                KMPPropertyGridSettings = KMP_Props as KMP_Main_PGS;
            }
        }

        public static void AddCheckValue(ListBox listBox, List<CheckValue> KMPCheck)
        {
            listBox.Items.Clear();
            listBox.Update();
            List<string> s = KMPCheck.Select(x => "[Grp_ID] " + x.GroupNum + " : " + "[Val_ID] " + x.ValueNum + " : " + "[Desc] " + x.Description).ToList();
            listBox.Items.AddRange(s.ToArray());
        }

        private void KMPErrorCheck_Load(object sender, EventArgs e)
        {
            if (SectionName == "All")
            {
                AddCheckValue(KartPointListBox, KMPCheck.TPTK_Check(KMPPropertyGridSettings.TPTK_Section));
                AddCheckValue(EnemyRouteListBox, KMPCheck.HPNE_TPNE_Check(KMPPropertyGridSettings.HPNE_TPNE_Section));
                AddCheckValue(ItemRouteListBox, KMPCheck.HPTI_TPTI_Check(KMPPropertyGridSettings.HPTI_TPTI_Section));
                AddCheckValue(CheckpointListBox, KMPCheck.HPKC_TPKC_Check(KMPPropertyGridSettings.HPKC_TPKC_Section, KMPPropertyGridSettings.JBOG_Section, KMPPropertyGridSettings.TPGJ_Section));
                AddCheckValue(ObjectListBox, KMPCheck.JBOG_Check(KMPPropertyGridSettings.JBOG_Section, KMPPropertyGridSettings.ITOP_Section));
                AddCheckValue(RouteListBox, KMPCheck.ITOP_Check(KMPPropertyGridSettings.ITOP_Section));
                AddCheckValue(AreaListBox, KMPCheck.AERA_Check(KMPPropertyGridSettings.AERA_Section, KMPPropertyGridSettings.ITOP_Section, KMPPropertyGridSettings.EMAC_Section));
                AddCheckValue(CameraListBox, KMPCheck.EMAC_Check(KMPPropertyGridSettings.EMAC_Section, KMPPropertyGridSettings.ITOP_Section));
                AddCheckValue(JugemPointListBox, KMPCheck.TPGJ_Check(KMPPropertyGridSettings.TPGJ_Section));
                AddCheckValue(GlideRouteListBox, KMPCheck.HPLG_TPLG_Check(KMPPropertyGridSettings.HPLG_TPLG_Section));
            }
            if (SectionName == "Kart Point")
            {
                AddCheckValue(KartPointListBox, KMPCheck.TPTK_Check(KMPPropertyGridSettings.TPTK_Section));
                KMPErrorCheckTabControl.SelectedIndex = 0;
            }
            if (SectionName == "Enemy Route")
            {
                AddCheckValue(EnemyRouteListBox, KMPCheck.HPNE_TPNE_Check(KMPPropertyGridSettings.HPNE_TPNE_Section));
                KMPErrorCheckTabControl.SelectedIndex = 1;
            }
            if (SectionName == "Item Route")
            {
                AddCheckValue(ItemRouteListBox, KMPCheck.HPTI_TPTI_Check(KMPPropertyGridSettings.HPTI_TPTI_Section));
                KMPErrorCheckTabControl.SelectedIndex = 2;
            }
            if (SectionName == "Checkpoint")
            {
                AddCheckValue(CheckpointListBox, KMPCheck.HPKC_TPKC_Check(KMPPropertyGridSettings.HPKC_TPKC_Section, KMPPropertyGridSettings.JBOG_Section, KMPPropertyGridSettings.TPGJ_Section));
                KMPErrorCheckTabControl.SelectedIndex = 3;
            }
            if (SectionName == "Object")
            {
                AddCheckValue(ObjectListBox, KMPCheck.JBOG_Check(KMPPropertyGridSettings.JBOG_Section, KMPPropertyGridSettings.ITOP_Section));
                KMPErrorCheckTabControl.SelectedIndex = 4;
            }
            if (SectionName == "Route")
            {
                AddCheckValue(RouteListBox, KMPCheck.ITOP_Check(KMPPropertyGridSettings.ITOP_Section));
                KMPErrorCheckTabControl.SelectedIndex = 5;
            }
            if (SectionName == "Area")
            {
                AddCheckValue(AreaListBox, KMPCheck.AERA_Check(KMPPropertyGridSettings.AERA_Section, KMPPropertyGridSettings.ITOP_Section, KMPPropertyGridSettings.EMAC_Section));
                KMPErrorCheckTabControl.SelectedIndex = 6;
            }
            if (SectionName == "Camera")
            {
                AddCheckValue(CameraListBox, KMPCheck.EMAC_Check(KMPPropertyGridSettings.EMAC_Section, KMPPropertyGridSettings.ITOP_Section));
                KMPErrorCheckTabControl.SelectedIndex = 7;
            }
            if (SectionName == "Jugem Point")
            {
                AddCheckValue(JugemPointListBox, KMPCheck.TPGJ_Check(KMPPropertyGridSettings.TPGJ_Section));
                KMPErrorCheckTabControl.SelectedIndex = 8;
            }
            if (SectionName == "Glide Route")
            {
                AddCheckValue(GlideRouteListBox, KMPCheck.HPLG_TPLG_Check(KMPPropertyGridSettings.HPLG_TPLG_Section));
                KMPErrorCheckTabControl.SelectedIndex = 9;
            }
        }

        public class KMPCheck
        {
            public static List<CheckValue> TPTK_Check(KartPoint_PGS TPTK_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                //if (Values.TPKC_Checkpoint_Type < 255)
                //{
                //    var t = HPKC_TPKC_Section.HPKCValueList[i].TPKCValueList.Where(n => n.TPKC_Checkpoint_Type < 255).ToList();

                //    if (t.Where(h => h.TPKC_Checkpoint_Type == Values.TPKC_Checkpoint_Type).Count() > 1)
                //    {
                //        CheckValue checkValue = new CheckValue
                //        {
                //            GroupNum = i,
                //            ValueNum = j,
                //            Description = "Duplicate Number : " + Values.TPKC_Checkpoint_Type,
                //            CheckValueType = CheckValueType.Error
                //        };

                //        ch.Add(checkValue);
                //    }
                //}

                var t = TPTK_Section.TPTKValueList.GroupBy(f => f.Player_Index).Where(s => s.Count() > 1).Select(x => x).ToList();

                foreach (var d in t)
                {
                    foreach (var m in d)
                    {
                        CheckValue checkValue = new CheckValue
                        {
                            GroupNum = -1,
                            ValueNum = m.ID,
                            Description = "Duplicate Number : " + m.Player_Index,
                            CheckValueType = CheckValueType.Error
                        };

                        CheckValueList.Add(checkValue);
                    }
                }

                return CheckValueList;
            }

            public static List<CheckValue> HPNE_TPNE_Check(EnemyRoute_PGS HPNE_TPNE_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                if (HPNE_TPNE_Section.HPNEValueList.Count != 0)
                {
                    for (int i = 0; i < HPNE_TPNE_Section.HPNEValueList.Count; i++)
                    {
                        var NextGrp = HPNE_TPNE_Section.HPNEValueList[i].HPNENextGroups;
                        ushort[] NextGrpAry = new ushort[] { NextGrp.Next0, NextGrp.Next1, NextGrp.Next2, NextGrp.Next3, NextGrp.Next4, NextGrp.Next5, NextGrp.Next6, NextGrp.Next7, NextGrp.Next8, NextGrp.Next9, NextGrp.Next10, NextGrp.Next11, NextGrp.Next12, NextGrp.Next13, NextGrp.Next14, NextGrp.Next15 };
                        for (int Num = 0; Num < NextGrpAry.ToList().Count; Num++)
                        {
                            if (NextGrpAry[Num] == 65535)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "Warning (Next" + Num +") : 65535 cannot be specified (-1)",
                                    CheckValueType = CheckValueType.Warning
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (NextGrpAry[Num] < 65535 && HPNE_TPNE_Section.HPNEValueList.Select(x => x.GroupID).ToList().Contains(NextGrpAry[Num]) == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "The GroupID \"" + NextGrpAry[Num] + "\" specified in Next" + Num + " was not found.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }
                        }

                        var PrevGrp = HPNE_TPNE_Section.HPNEValueList[i].HPNEPreviewGroups;
                        ushort[] PrevGrpAry = new ushort[] { PrevGrp.Prev0, PrevGrp.Prev1, PrevGrp.Prev2, PrevGrp.Prev3, PrevGrp.Prev4, PrevGrp.Prev5, PrevGrp.Prev6, PrevGrp.Prev7, PrevGrp.Prev8, PrevGrp.Prev9, PrevGrp.Prev10, PrevGrp.Prev11, PrevGrp.Prev12, PrevGrp.Prev13, PrevGrp.Prev14, PrevGrp.Prev15 };
                        for (int Num = 0; Num < PrevGrpAry.ToList().Count; Num++)
                        {
                            if (PrevGrpAry[Num] == 65535)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "Warning (Prev" + Num + ") : 65535 cannot be specified (-1)",
                                    CheckValueType = CheckValueType.Warning
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (PrevGrpAry[Num] < 65535 && HPNE_TPNE_Section.HPNEValueList.Select(x => x.GroupID).ToList().Contains(PrevGrpAry[Num]) == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "The GroupID \"" + PrevGrpAry[Num] + "\" specified in Prev" + Num + " was not found.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }
                        }
                    }
                }

                return CheckValueList;
            }

            public static List<CheckValue> HPTI_TPTI_Check(ItemRoute_PGS HPTI_TPTI_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                if (HPTI_TPTI_Section.HPTIValueList.Count != 0)
                {
                    for (int i = 0; i < HPTI_TPTI_Section.HPTIValueList.Count; i++)
                    {
                        var NextGrp = HPTI_TPTI_Section.HPTIValueList[i].HPTI_NextGroup;
                        ushort[] NextGrpAry = new ushort[] { NextGrp.Next0, NextGrp.Next1, NextGrp.Next2, NextGrp.Next3, NextGrp.Next4, NextGrp.Next5 };
                        for (int Num = 0; Num < NextGrpAry.ToList().Count; Num++)
                        {
                            if (NextGrpAry[Num] == 65535)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "Warning (Next" + Num + ") : 65535 cannot be specified (-1)",
                                    CheckValueType = CheckValueType.Warning
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (NextGrpAry[Num] < 65535 && HPTI_TPTI_Section.HPTIValueList.Select(x => x.GroupID).ToList().Contains(NextGrpAry[Num]) == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "The GroupID \"" + NextGrpAry[Num] + "\" specified in Next" + Num + " was not found.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }
                        }

                        var PrevGrp = HPTI_TPTI_Section.HPTIValueList[i].HPTI_PreviewGroup;
                        ushort[] PrevGrpAry = new ushort[] { PrevGrp.Prev0, PrevGrp.Prev1, PrevGrp.Prev2, PrevGrp.Prev3, PrevGrp.Prev4, PrevGrp.Prev5 };
                        for (int Num = 0; Num < PrevGrpAry.ToList().Count; Num++)
                        {
                            if (PrevGrpAry[Num] == 65535)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "Warning (Prev" + Num + ") : 65535 cannot be specified (-1)",
                                    CheckValueType = CheckValueType.Warning
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (PrevGrpAry[Num] < 65535 && HPTI_TPTI_Section.HPTIValueList.Select(x => x.GroupID).ToList().Contains(PrevGrpAry[Num]) == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "The GroupID \"" + PrevGrpAry[Num] + "\" specified in Prev" + Num + " was not found.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }
                        }
                    }
                }

                return CheckValueList;
            }

            public static List<CheckValue> HPKC_TPKC_Check(Checkpoint_PGS HPKC_TPKC_Section, KMPObject_PGS JBOG_Section, RespawnPoint_PGS TPGJ_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                if (HPKC_TPKC_Section.HPKCValueList.Count != 0)
                {
                    for (int i = 0; i < HPKC_TPKC_Section.HPKCValueList.Count; i++)
                    {
                        var NextGrp = HPKC_TPKC_Section.HPKCValueList[i].HPKC_NextGroup;
                        ushort[] NextGrpAry = new ushort[] { NextGrp.Next0, NextGrp.Next1, NextGrp.Next2, NextGrp.Next3, NextGrp.Next4, NextGrp.Next5 };
                        for (int Num = 0; Num < NextGrpAry.ToList().Count; Num++)
                        {
                            if (NextGrpAry[Num] == 255)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "Warning (Next" + Num + ") : 255 cannot be specified (-1)",
                                    CheckValueType = CheckValueType.Warning
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (NextGrpAry[Num] < 255 && HPKC_TPKC_Section.HPKCValueList.Select(x => x.GroupID).ToList().Contains(NextGrpAry[Num]) == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "The GroupID \"" + NextGrpAry[Num] + "\" specified in Next" + Num + " was not found.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }
                        }

                        var PrevGrp = HPKC_TPKC_Section.HPKCValueList[i].HPKC_PreviewGroup;
                        ushort[] PrevGrpAry = new ushort[] { PrevGrp.Prev0, PrevGrp.Prev1, PrevGrp.Prev2, PrevGrp.Prev3, PrevGrp.Prev4, PrevGrp.Prev5 };
                        for (int Num = 0; Num < PrevGrpAry.ToList().Count; Num++)
                        {
                            if (PrevGrpAry[Num] == 255)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "Warning (Prev" + Num + ") : 255 cannot be specified (-1)",
                                    CheckValueType = CheckValueType.Warning
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (PrevGrpAry[Num] < 255 && HPKC_TPKC_Section.HPKCValueList.Select(x => x.GroupID).ToList().Contains(PrevGrpAry[Num]) == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "The GroupID \"" + PrevGrpAry[Num] + "\" specified in Prev" + Num + " was not found.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }
                        }

                        for (int TPKCValueCount = 0; TPKCValueCount < HPKC_TPKC_Section.HPKCValueList[i].TPKCValueList.Count; TPKCValueCount++)
                        {
                            //RespawnID, Key, ClipID, Section Check
                            var Values = HPKC_TPKC_Section.HPKCValueList[i].TPKCValueList[TPKCValueCount];
                            if (Values.TPKC_ClipID < 255)
                            {
                                var yh = JBOG_Section.JBOGValueList.Select(x => x.ObjectID == "001E").ToList().Count;
                                if (yh != 0)
                                {
                                    var f = JBOG_Section.JBOGValueList.Where(x => x.ObjectID == "001E").ToList().Find(y => y.JBOG_Specific_Setting.Value0 == Values.TPKC_ClipID);

                                    if (f == null)
                                    {
                                        CheckValue checkValue = new CheckValue
                                        {
                                            GroupNum = i,
                                            ValueNum = TPKCValueCount,
                                            Description = "ClipID \"" + (int)Values.TPKC_ClipID + "\" is not set in \"Specific value 0\" of the Culling Handler [001E] object.",
                                            CheckValueType = CheckValueType.Error
                                        };

                                        CheckValueList.Add(checkValue);
                                    }
                                }
                                if (yh == 0)
                                {
                                    CheckValue checkValue = new CheckValue
                                    {
                                        GroupNum = i,
                                        ValueNum = TPKCValueCount,
                                        Description = "To use ClipID, you need a Culling Handler[001E] object. | Culling Handler[001E] : Specific value 0 = " + (int)Values.TPKC_ClipID,
                                        CheckValueType = CheckValueType.Error
                                    };

                                    CheckValueList.Add(checkValue);
                                }
                            }

                            if (Values.TPKC_Checkpoint_KeyID < 255)
                            {
                                var t = HPKC_TPKC_Section.HPKCValueList[i].TPKCValueList.Where(n => n.TPKC_Checkpoint_KeyID < 255).ToList();

                                if (t.Where(h => h.TPKC_Checkpoint_KeyID == Values.TPKC_Checkpoint_KeyID).Count() > 1)
                                {
                                    CheckValue checkValue = new CheckValue
                                    {
                                        GroupNum = i,
                                        ValueNum = TPKCValueCount,
                                        Description = "Duplicate Number : " + Values.TPKC_Checkpoint_KeyID,
                                        CheckValueType = CheckValueType.Error
                                    };

                                    CheckValueList.Add(checkValue);
                                }
                            }

                            if (Values.TPKC_RespawnID < 255)
                            {
                                var qb = TPGJ_Section.TPGJValueList.Select(x => x.ID).ToList().Contains(Values.TPKC_RespawnID) ? true : false;
                                if (qb == false)
                                {
                                    CheckValue checkValue = new CheckValue
                                    {
                                        GroupNum = i,
                                        ValueNum = TPKCValueCount,
                                        Description = "Respawn ID \"" + Values.TPKC_RespawnID + "\" does not exist.",
                                        CheckValueType = CheckValueType.Error
                                    };

                                    CheckValueList.Add(checkValue);
                                }
                            }

                            //if (Values.TPKC_NextCheckPoint < 255)
                            //{
                            //    var cd = Values.TPKC_NextCheckPoint == HPKC_TPKC_Section.HPKCValueList[i].TPKCValueList[j + 1].ID ? true : false;
                            //}
                        }
                    }
                }

                return CheckValueList;
            }

            public static List<CheckValue> JBOG_Check(KMPObject_PGS JBOG_Section, Route_PGS ITOP_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                var ObjFlowXml = KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml");
                List<ObjFlowData_XML.ObjFlow> ObjFlowList = ObjFlowXml.ObjFlows;

                for (int Count = 0; Count < JBOG_Section.JBOGValueList.Count; Count++)
                {
                    var Specific_Setting = JBOG_Section.JBOGValueList[Count].JBOG_Specific_Setting;
                    ushort[] JBOG_Specific_ValueAry = Specific_Setting.GetSpecificSettingArray();

                    for (int ValueCount = 0; ValueCount < JBOG_Specific_ValueAry.ToList().Count; ValueCount++)
                    {
                        bool b = ObjFlowList.Find(x => x.ObjectID == JBOG_Section.JBOGValueList[Count].ObjectID).DefaultValueData.Values[ValueCount].DefaultObjectValue != -1 ? true : false;

                        if (b == true)
                        {
                            //Value
                            bool b2 = JBOG_Specific_ValueAry[ValueCount] < 65535 ? true : false;

                            if (b2 == true) continue;
                            if (b2 == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = -1,
                                    ValueNum = JBOG_Section.JBOGValueList[Count].ID,
                                    Description = "Invalid value (ID : " + JBOG_Section.JBOGValueList[Count].ID + ") : The Specific value " + JBOG_Specific_ValueAry[ValueCount] + "cannot be 65535(-1). Be sure to enter a valid value.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }

                        }
                        if (b == false)
                        {
                            bool b2 = JBOG_Specific_ValueAry[ValueCount] < 65535 ? true : false;

                            if (b2 == true)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = -1,
                                    ValueNum = JBOG_Section.JBOGValueList[Count].ID,
                                    Description = "Invalid value (ID : " + JBOG_Section.JBOGValueList[Count].ID + ") : The Specific value " + JBOG_Specific_ValueAry[ValueCount] + " is unused, so -1 is always used.",
                                    CheckValueType = CheckValueType.Info
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (b2 == false) continue;
                        }
                    }

                    var RouteID = JBOG_Section.JBOGValueList[Count].JBOG_ITOP_RouteIDIndex;
                    if (RouteID < 65535)
                    {
                        var qb = ITOP_Section.ITOP_RouteList.Select(x => x.GroupID).ToList().Contains(RouteID) ? true : false;
                        if (qb == false)
                        {
                            CheckValue checkValue = new CheckValue
                            {
                                GroupNum = -1,
                                ValueNum = JBOG_Section.JBOGValueList[Count].ID,
                                Description = "Route ID \"" + RouteID + "\" does not exist.",
                                CheckValueType = CheckValueType.Error
                            };

                            CheckValueList.Add(checkValue);
                        }
                    }
                    if (RouteID == 65535)
                    {
                        CheckValue checkValue = new CheckValue
                        {
                            GroupNum = -1,
                            ValueNum = JBOG_Section.JBOGValueList[Count].ID,
                            Description = "Route ID : 65535 (-1)",
                            CheckValueType = CheckValueType.Info
                        };

                        CheckValueList.Add(checkValue);
                    }
                }

                return CheckValueList;
            }

            public static List<CheckValue> ITOP_Check(Route_PGS ITOP_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                for (int i = 0; i < ITOP_Section.ITOP_RouteList.Count; i++)
                {
                    CheckValue checkValue = new CheckValue
                    {
                        GroupNum = i,
                        ValueNum = -1,
                        Description = "Loop : " + ITOP_Section.ITOP_RouteList[i].ITOP_Loop + " | " + "Smooth : " + ITOP_Section.ITOP_RouteList[i].ITOP_Smooth,
                        CheckValueType = CheckValueType.Info
                    };

                    CheckValueList.Add(checkValue);
                }

                return CheckValueList;
            }

            public static List<CheckValue> AERA_Check(Area_PGS AERA_Section, Route_PGS ITOP_Section, Camera_PGS EMAC_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                for (int i = 0; i < AERA_Section.AERAValueList.Count; i++)
                {
                    var RouteID = AERA_Section.AERAValueList[i].RouteID;
                    if (RouteID < 255)
                    {
                        var qb = ITOP_Section.ITOP_RouteList.Select(x => x.GroupID).ToList().Contains(RouteID) ? true : false;
                        if (qb == false)
                        {
                            CheckValue checkValue = new CheckValue
                            {
                                GroupNum = -1,
                                ValueNum = AERA_Section.AERAValueList[i].ID,
                                Description = "Route ID \"" + RouteID + "\" does not exist.",
                                CheckValueType = CheckValueType.Error
                            };

                            CheckValueList.Add(checkValue);
                        }
                    }
                    if (RouteID == 255)
                    {
                        CheckValue checkValue = new CheckValue
                        {
                            GroupNum = -1,
                            ValueNum = AERA_Section.AERAValueList[i].ID,
                            Description = "Route ID : 255 (-1)",
                            CheckValueType = CheckValueType.Info
                        };

                        CheckValueList.Add(checkValue);
                    }

                    var CameraID = AERA_Section.AERAValueList[i].AERA_EMACIndex;
                    if (CameraID < 255)
                    {
                        var qb = EMAC_Section.EMACValueList.Select(x => x.ID).ToList().Contains(CameraID) ? true : false;
                        if (qb == true)
                        {
                            //var ty = EMAC_Section.EMACValueList.Where(x => x.CameraType == 0).ToList();

                        }
                        if (qb == false)
                        {
                            CheckValue checkValue = new CheckValue
                            {
                                GroupNum = -1,
                                ValueNum = AERA_Section.AERAValueList[i].ID,
                                Description = "Camera ID \"" + CameraID + "\" does not exist.",
                                CheckValueType = CheckValueType.Error
                            };

                            CheckValueList.Add(checkValue);
                        }
                    }
                    if (CameraID == 255)
                    {
                        CheckValue checkValue = new CheckValue
                        {
                            GroupNum = -1,
                            ValueNum = AERA_Section.AERAValueList[i].ID,
                            Description = "Camera ID : 255 (-1)",
                            CheckValueType = CheckValueType.Info
                        };

                        CheckValueList.Add(checkValue);
                    }
                }

                return CheckValueList;
            }

            public static List<CheckValue> EMAC_Check(Camera_PGS EMAC_Section, Route_PGS ITOP_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                for (int i = 0; i < EMAC_Section.EMACValueList.Count; i++)
                {
                    var NCameraIdx = EMAC_Section.EMACValueList[i].NextCameraIndex;
                    if (NCameraIdx < 255)
                    {
                        var qb = EMAC_Section.EMACValueList.Select(x => x.ID).ToList().Contains(NCameraIdx) ? true : false;
                        if (qb == false)
                        {
                            CheckValue checkValue = new CheckValue
                            {
                                GroupNum = -1,
                                ValueNum = EMAC_Section.EMACValueList[i].ID,
                                Description = "Camera ID \"" + NCameraIdx + "\" does not exist.",
                                CheckValueType = CheckValueType.Error
                            };

                            CheckValueList.Add(checkValue);
                        }
                    }
                    if (NCameraIdx == 255)
                    {
                        CheckValue checkValue = new CheckValue
                        {
                            GroupNum = -1,
                            ValueNum = EMAC_Section.EMACValueList[i].ID,
                            Description = "Camera ID : 255 (-1)",
                            CheckValueType = CheckValueType.Info
                        };

                        CheckValueList.Add(checkValue);
                    }

                    var NVideoIdx = EMAC_Section.EMACValueList[i].EMAC_NextVideoIndex;
                    if (NVideoIdx < 255)
                    {
                        var qb = EMAC_Section.EMACValueList.Select(x => x.ID).ToList().Contains(NVideoIdx) ? true : false;
                        if (qb == false)
                        {
                            CheckValue checkValue = new CheckValue
                            {
                                GroupNum = -1,
                                ValueNum = EMAC_Section.EMACValueList[i].ID,
                                Description = "Video ID \"" + NVideoIdx + "\" does not exist.",
                                CheckValueType = CheckValueType.Error
                            };

                            CheckValueList.Add(checkValue);
                        }
                    }
                    if (NVideoIdx == 255)
                    {
                        CheckValue checkValue = new CheckValue
                        {
                            GroupNum = -1,
                            ValueNum = EMAC_Section.EMACValueList[i].ID,
                            Description = "Video ID : 255 (-1)",
                            CheckValueType = CheckValueType.Info
                        };

                        CheckValueList.Add(checkValue);
                    }

                    var RouteID = EMAC_Section.EMACValueList[i].EMAC_ITOP_CameraIndex;
                    if (RouteID < 255)
                    {
                        var qb = ITOP_Section.ITOP_RouteList.Select(x => x.GroupID).ToList().Contains(RouteID) ? true : false;
                        if (qb == false)
                        {
                            CheckValue checkValue = new CheckValue
                            {
                                GroupNum = -1,
                                ValueNum = EMAC_Section.EMACValueList[i].ID,
                                Description = "Route ID \"" + RouteID + "\" does not exist.",
                                CheckValueType = CheckValueType.Error
                            };

                            CheckValueList.Add(checkValue);
                        }
                    }
                    if (RouteID == 255)
                    {
                        CheckValue checkValue = new CheckValue
                        {
                            GroupNum = -1,
                            ValueNum = EMAC_Section.EMACValueList[i].ID,
                            Description = "Route ID : 255 (-1)",
                            CheckValueType = CheckValueType.Info
                        };

                        CheckValueList.Add(checkValue);
                    }
                }

                return CheckValueList;
            }

            public static List<CheckValue> TPGJ_Check(RespawnPoint_PGS TPGJ_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();
                for (int i = 0; i < TPGJ_Section.TPGJValueList.Count; i++)
                {
                    if (TPGJ_Section.TPGJValueList[i].TPGJ_UnknownData1 < 65535)
                    {
                        CheckValue checkValue = new CheckValue
                        {
                            GroupNum = -1,
                            ValueNum = TPGJ_Section.TPGJValueList[i].ID,
                            Description = "UnkByte1 Value : " + TPGJ_Section.TPGJValueList[i].TPGJ_UnknownData1,
                            CheckValueType = CheckValueType.Info
                        };

                        CheckValueList.Add(checkValue);
                    }
                }

                return CheckValueList;
            }

            public static List<CheckValue> HPLG_TPLG_Check(GlideRoute_PGS HPLG_TPLG_Section)
            {
                List<CheckValue> CheckValueList = new List<CheckValue>();

                if (HPLG_TPLG_Section.HPLGValueList.Count != 0)
                {
                    for (int i = 0; i < HPLG_TPLG_Section.HPLGValueList.Count; i++)
                    {
                        var NextGrp = HPLG_TPLG_Section.HPLGValueList[i].HPLG_NextGroup;
                        ushort[] NextGrpAry = new ushort[] { NextGrp.Next0, NextGrp.Next1, NextGrp.Next2, NextGrp.Next3, NextGrp.Next4, NextGrp.Next5 };
                        for (int Num = 0; Num < NextGrpAry.ToList().Count; Num++)
                        {
                            if (NextGrpAry[Num] == 255)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "Warning (Next" + Num + ") : 255 cannot be specified (-1)",
                                    CheckValueType = CheckValueType.Warning
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (NextGrpAry[Num] < 255 && HPLG_TPLG_Section.HPLGValueList.Select(x => x.GroupID).ToList().Contains(NextGrpAry[Num]) == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "The GroupID \"" + NextGrpAry[Num] + "\" specified in Next" + Num + " was not found.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }
                        }

                        var PrevGrp = HPLG_TPLG_Section.HPLGValueList[i].HPLG_PreviewGroup;
                        ushort[] PrevGrpAry = new ushort[] { PrevGrp.Prev0, PrevGrp.Prev1, PrevGrp.Prev2, PrevGrp.Prev3, PrevGrp.Prev4, PrevGrp.Prev5 };
                        for (int Num = 0; Num < PrevGrpAry.ToList().Count; Num++)
                        {
                            if (PrevGrpAry[Num] == 255)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "Warning (Prev" + Num + ") : 255 cannot be specified (-1)",
                                    CheckValueType = CheckValueType.Warning
                                };

                                CheckValueList.Add(checkValue);
                            }
                            if (PrevGrpAry[Num] < 255 && HPLG_TPLG_Section.HPLGValueList.Select(x => x.GroupID).ToList().Contains(PrevGrpAry[Num]) == false)
                            {
                                CheckValue checkValue = new CheckValue
                                {
                                    GroupNum = i,
                                    Description = "The GroupID \"" + PrevGrpAry[Num] + "\" specified in Prev" + Num + " was not found.",
                                    CheckValueType = CheckValueType.Error
                                };

                                CheckValueList.Add(checkValue);
                            }
                        }

                        for (int j = 0; j < HPLG_TPLG_Section.HPLGValueList[i].TPLGValueList.Count; j++)
                        {
                            //var Values = HPLG_TPLG_Section.HPLGValueList[i].TPLGValueList[j];
                            //if (Values.)
                        }
                    }
                }

                return CheckValueList;
            }
        }

        public static void ChangeErrorChkType(ListBox listBox, List<CheckValue> KMPCheck, CheckBox Chk_Info, CheckBox Chk_Warning, CheckBox Chk_Error)
        {
            listBox.Items.Clear();
            listBox.Update();
            List<CheckValue> CheckValues = new List<CheckValue>();

            if (Chk_Info.Checked == true) CheckValues.AddRange(KMPCheck.Where(x => x.CheckValueType == CheckValueType.Info));
            if (Chk_Warning.Checked == true) CheckValues.AddRange(KMPCheck.Where(x => x.CheckValueType == CheckValueType.Warning));
            if (Chk_Error.Checked == true) CheckValues.AddRange(KMPCheck.Where(x => x.CheckValueType == CheckValueType.Error));

            List<string> s = CheckValues.Select(x => "[Grp_ID] " + x.GroupNum + " : " + "[Val_ID] " + x.ValueNum + " : " + "[Desc] " + x.Description).ToList();

            listBox.Items.AddRange(s.ToArray());
        }

        private void ErrorChkType_CheckedChanged(object sender, EventArgs e)
        {
            if (SectionName == "All")
            {
                ChangeErrorChkType(KartPointListBox, KMPCheck.TPTK_Check(KMPPropertyGridSettings.TPTK_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(EnemyRouteListBox, KMPCheck.HPNE_TPNE_Check(KMPPropertyGridSettings.HPNE_TPNE_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(ItemRouteListBox, KMPCheck.HPTI_TPTI_Check(KMPPropertyGridSettings.HPTI_TPTI_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(CheckpointListBox, KMPCheck.HPKC_TPKC_Check(KMPPropertyGridSettings.HPKC_TPKC_Section, KMPPropertyGridSettings.JBOG_Section, KMPPropertyGridSettings.TPGJ_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(ObjectListBox, KMPCheck.JBOG_Check(KMPPropertyGridSettings.JBOG_Section, KMPPropertyGridSettings.ITOP_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(RouteListBox, KMPCheck.ITOP_Check(KMPPropertyGridSettings.ITOP_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(AreaListBox, KMPCheck.AERA_Check(KMPPropertyGridSettings.AERA_Section, KMPPropertyGridSettings.ITOP_Section, KMPPropertyGridSettings.EMAC_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(CameraListBox, KMPCheck.EMAC_Check(KMPPropertyGridSettings.EMAC_Section, KMPPropertyGridSettings.ITOP_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(JugemPointListBox, KMPCheck.TPGJ_Check(KMPPropertyGridSettings.TPGJ_Section), Chk_Info, Chk_Warning, Chk_Error);
                ChangeErrorChkType(GlideRouteListBox, KMPCheck.HPLG_TPLG_Check(KMPPropertyGridSettings.HPLG_TPLG_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Kart Point")
            {
                ChangeErrorChkType(KartPointListBox, KMPCheck.TPTK_Check(KMPPropertyGridSettings.TPTK_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Enemy Route")
            {
                ChangeErrorChkType(EnemyRouteListBox, KMPCheck.HPNE_TPNE_Check(KMPPropertyGridSettings.HPNE_TPNE_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Item Route")
            {
                ChangeErrorChkType(ItemRouteListBox, KMPCheck.HPTI_TPTI_Check(KMPPropertyGridSettings.HPTI_TPTI_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Checkpoint")
            {
                ChangeErrorChkType(CheckpointListBox, KMPCheck.HPKC_TPKC_Check(KMPPropertyGridSettings.HPKC_TPKC_Section, KMPPropertyGridSettings.JBOG_Section, KMPPropertyGridSettings.TPGJ_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Object")
            {
                ChangeErrorChkType(ObjectListBox, KMPCheck.JBOG_Check(KMPPropertyGridSettings.JBOG_Section, KMPPropertyGridSettings.ITOP_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Route")
            {
                ChangeErrorChkType(RouteListBox, KMPCheck.ITOP_Check(KMPPropertyGridSettings.ITOP_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Area")
            {
                ChangeErrorChkType(AreaListBox, KMPCheck.AERA_Check(KMPPropertyGridSettings.AERA_Section, KMPPropertyGridSettings.ITOP_Section, KMPPropertyGridSettings.EMAC_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Camera")
            {
                ChangeErrorChkType(CameraListBox, KMPCheck.EMAC_Check(KMPPropertyGridSettings.EMAC_Section, KMPPropertyGridSettings.ITOP_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Jugem Point")
            {
                ChangeErrorChkType(JugemPointListBox, KMPCheck.TPGJ_Check(KMPPropertyGridSettings.TPGJ_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
            if (SectionName == "Glide Route")
            {
                ChangeErrorChkType(GlideRouteListBox, KMPCheck.HPLG_TPLG_Check(KMPPropertyGridSettings.HPLG_TPLG_Section), Chk_Info, Chk_Warning, Chk_Error);
            }
        }
    }
}
