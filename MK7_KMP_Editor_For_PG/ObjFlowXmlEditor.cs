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
        public List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> objFlowDataXml_List;

        public ObjFlowXmlPropertyGridSetting ObjFlowXmlPropertyGridSettings;

        public ObjFlowXmlEditor()
        {
            InitializeComponent();
        }

        private void ObjFlowXmlEditor_Load(object sender, EventArgs e)
        {
            objFlowDataXml_List = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.Xml");

            ObjFlowXmlPropertyGridSettings = new ObjFlowXmlPropertyGridSetting
            {
                ObjFlowsList = null
            };

            List<ObjFlowXmlPropertyGridSetting.ObjFlow> objFlows = new List<ObjFlowXmlPropertyGridSetting.ObjFlow>();  

            foreach (var f in objFlowDataXml_List)
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
                        X = f.Scales.X,
                        Y = f.Scales.Y,
                        Z = f.Scales.Z
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
                    X = 0,
                    Y = 0,
                    Z = 0
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

            List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDBList = new List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB>();

            foreach (var ObjFlowData in ObjFlowXmlPropertyGridSettings.ObjFlowsList)
			{
                KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB ObjFlowDB = new KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB
                {
                    ObjectID = ObjFlowData.ObjectID,
                    ObjectName = ObjFlowData.ObjectName,
                    Path = ObjFlowData.Path,
                    UseKCL = ObjFlowData.UseKCL,
                    ObjectType = ObjFlowData.ObjectType,
                    Commons = new KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.Common
                    {
                        ColType = ObjFlowData.Commons.ColType,
                        PathType = ObjFlowData.Commons.PathType,
                        ModelSetting = ObjFlowData.Commons.ModelSetting,
                        Unknown1 = ObjFlowData.Commons.Unknown1
                    },
                    LODSetting = new KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.LOD_Setting
                    {
                        LOD = ObjFlowData.LODSetting.LOD,
                        LODHPoly = ObjFlowData.LODSetting.LODHPoly,
                        LODLPoly = ObjFlowData.LODSetting.LODLPoly,
                        LODDef = ObjFlowData.LODSetting.LODDef
                    },
                    Scales = new KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.Scale
                    {
                        X = (int)ObjFlowData.Scales.X,
                        Y = (int)ObjFlowData.Scales.Y,
                        Z = (int)ObjFlowData.Scales.Z
                    },
                    Names = new KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.Name
                    {
                        Main = ObjFlowData.Names.Main,
                        Sub = ObjFlowData.Names.Sub
                    },
                    DefaultValues = new KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.Default_Values
                    {
                        Values = null
                    }
                };

                List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.Default_Values.Value> valueList = new List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.Default_Values.Value>();

                foreach (var ObjFlowDataValue in ObjFlowData.DefaultValues.ValuesList)
                {
                    KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.Default_Values.Value value = new KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB.Default_Values.Value
                    {
                        DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
                        Description = ObjFlowDataValue.Description
                    };

                    valueList.Add(value);
                }

                ObjFlowDB.DefaultValues.Values = valueList;

                ObjFlowDBList.Add(ObjFlowDB);

            }

            //Save ObjFlowData.xml
            KMPs.KMPHelper.ObjFlowReader.Xml.WriteObjFlowXml(ObjFlowDBList, "ObjFlowData.Xml");
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(ObjFlowXmlPropertyGridSettings.ObjFlowsList.ToArray());
        }
    }
}
