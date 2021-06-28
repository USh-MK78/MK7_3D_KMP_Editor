using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MK7_KMP_Editor_For_PG_
{
    public partial class ObjFlowXmlEditor : Form
    {
        public KMPs.KMPHelper.ObjFlowReader.ObjFlowXmlToObject objFlowXmlToObject;

        public ObjFlowXmlPropertyGridSetting ObjFlowXmlPropertyGridSettings;

        public ObjFlowXmlEditor()
        {
            InitializeComponent();
        }

        private void ObjFlowXmlEditor_Load(object sender, EventArgs e)
        {
            objFlowXmlToObject = KMPs.KMPHelper.ObjFlowReader.ReadObjFlowXml("ObjFlowData.Xml");

            ObjFlowXmlPropertyGridSettings = new ObjFlowXmlPropertyGridSetting
            {
                ObjFlowsList = null
            };

            List<ObjFlowXmlPropertyGridSetting.ObjFlow> objFlows = new List<ObjFlowXmlPropertyGridSetting.ObjFlow>();  

            foreach (var f in objFlowXmlToObject.ObjFlows)
            {
                ObjFlowXmlPropertyGridSetting.ObjFlow objFlow = new ObjFlowXmlPropertyGridSetting.ObjFlow
                {
                    ObjectID = f.ObjectID,
                    ObjectName = f.ObjectName,
                    Path = f.Path,
                    UseKCL = f.UseKCL,
                    ObjectType = f.ObjectType,
                    Commons = new ObjFlowXmlPropertyGridSetting.ObjFlow.Common
                    {
                        ObjID = f.Commons.ObjID,
                        ColType = f.Commons.ColType,
                        PathType = f.Commons.PathType,
                        ModelSetting = f.Commons.ModelSetting,
                        Unknown1 = f.Commons.Unknown1
                    },
                    LODSetting = new ObjFlowXmlPropertyGridSetting.ObjFlow.LOD_Setting
                    {
                        LOD = f.LODSetting.LOD,
                        LODHPoly = f.LODSetting.LODHPoly,
                        LODLPoly = f.LODSetting.LODLPoly,
                        LODDef = f.LODSetting.LODDef
                    },
                    Scales = new ObjFlowXmlPropertyGridSetting.ObjFlow.Scale
                    {
                        X = f.Scales.X.ToString(),
                        Y = f.Scales.Y.ToString(),
                        Z = f.Scales.Z.ToString()
                    },
                    Names = new ObjFlowXmlPropertyGridSetting.ObjFlow.Name
                    {
                        Main = f.Names.Main,
                        Sub = f.Names.Sub
                    },
                    DefaultValues = new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values
                    {
                        ValuesList = null
                    }
                };

                List<ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value> values_List = new List<ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value>();

                foreach (var ds in f.DefaultValues.Values)
                {
                    ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value value = new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                    {
                        DefaultObjectValue = ds.DefaultObjectValue,
                        Description = ds.Description
                    };

                    values_List.Add(value);
                }

                objFlow.DefaultValues.ValuesList = values_List;

                objFlows.Add(objFlow);

                listBox1.Items.Add(objFlow);
            }

            ObjFlowXmlPropertyGridSettings.ObjFlowsList = objFlows;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                propertyGrid1.SelectedObject = ObjFlowXmlPropertyGridSettings.ObjFlowsList[listBox1.SelectedIndex];
            }
        }

        private void DeleteObjFlowXmlData_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count != -1)
            {
                ObjFlowXmlPropertyGridSetting.ObjFlow objFlow = ObjFlowXmlPropertyGridSettings.ObjFlowsList[listBox1.SelectedIndex];
                ObjFlowXmlPropertyGridSettings.ObjFlowsList.Remove(objFlow);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void AddObjFlowXmlData_Click(object sender, EventArgs e)
        {
            ObjFlowXmlPropertyGridSetting.ObjFlow objFlow = new ObjFlowXmlPropertyGridSetting.ObjFlow
            {
                ObjectID = "0000",
                ObjectName = "TestObject",
                Path = "",
                UseKCL = false,
                ObjectType = "Undefined Type",
                Commons = new ObjFlowXmlPropertyGridSetting.ObjFlow.Common
                {
                    ObjID = "0000",
                    ColType = "",
                    PathType = "",
                    ModelSetting = "",
                    Unknown1 = ""
                },
                LODSetting = new ObjFlowXmlPropertyGridSetting.ObjFlow.LOD_Setting
                {
                    LOD = 0,
                    LODHPoly = 0,
                    LODLPoly = 0,
                    LODDef = 0
                },
                Scales = new ObjFlowXmlPropertyGridSetting.ObjFlow.Scale
                {
                    X = "0",
                    Y = "0",
                    Z = "0"
                },
                Names = new ObjFlowXmlPropertyGridSetting.ObjFlow.Name
                {
                    Main = "Main",
                    Sub = "Sub"
                },
                DefaultValues = new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values
                {
                    ValuesList = new List<ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value>()
                    {
                        new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new ObjFlowXmlPropertyGridSetting.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        }
                    }
                }
            };

            listBox1.Items.Add(objFlow);
            ObjFlowXmlPropertyGridSettings.ObjFlowsList.Add(objFlow);
        }

        private void ObjFlowXmlEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists("ObjFlowData.xml.backup") == false)
            {
                File.Move("ObjFlowData.xml", "ObjFlowData.xml.backup");
            }

            //Save ObjFlowData.xml
            KMPs.KMPHelper.ObjFlowReader.WriteObjFlowXml(ObjFlowXmlPropertyGridSettings, "ObjFlowData.Xml");
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(ObjFlowXmlPropertyGridSettings.ObjFlowsList.ToArray());
        }
    }
}
