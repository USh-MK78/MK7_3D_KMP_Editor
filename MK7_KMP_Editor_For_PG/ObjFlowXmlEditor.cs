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
using KMPLibrary.KMPHelper;
using KMPLibrary.XMLConvert.ObjFlowData;

namespace MK7_3D_KMP_Editor
{
    public partial class ObjFlowXmlEditor : Form
    {
        public List<ObjFlowData_XML.ObjFlow> objFlowDataXml_List;

        public PropertyGridObject.ObjFlow.ObjFlow_PGS ObjFlowXmlPropertyGridSettings;

        public ObjFlowXmlEditor()
        {
            InitializeComponent();
        }

        private void ObjFlowXmlEditor_Load(object sender, EventArgs e)
        {
            objFlowDataXml_List = ObjFlowConverter.Xml.ReadObjFlowXml("ObjFlowData.Xml");

            ObjFlowXmlPropertyGridSettings = new PropertyGridObject.ObjFlow.ObjFlow_PGS
            {
                ObjFlowsList = null
            };

            List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow> objFlows = new List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow>();  

            foreach (var f in objFlowDataXml_List)
            {
                PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow objFlow = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow
                {
                    ObjectID = f.ObjectID,
                    ObjectName = f.ObjectName,
                    Path = f.Path,
                    UseKCL = f.UseKCL,
                    ObjectType = f.ObjectType,
                    Commons = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Common
                    {
                        ColType = f.Commons.ColType,
                        PathType = f.Commons.PathType,
                        ModelSetting = f.Commons.ModelSetting,
                        Unknown1 = f.Commons.Unknown1
                    },
                    LODSetting = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.LOD_Setting
                    {
                        LOD = f.LODSetting.LOD,
                        LODHPoly = f.LODSetting.LODHPoly,
                        LODLPoly = f.LODSetting.LODLPoly,
                        LODDef = f.LODSetting.LODDef
                    },
                    Scales = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Scale
                    {
                        X = f.Scales.X,
                        Y = f.Scales.Y,
                        Z = f.Scales.Z
                    },
                    Names = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Name
                    {
                        Main = f.Names.Main,
                        Sub = f.Names.Sub
                    },
                    DefaultValues = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values
                    {
                        ValuesList = null
                    }
                };

                List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value> values_List = new List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value>();

                foreach (var ds in f.DefaultValues.Values)
                {
                    PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value value = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
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
                PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow objFlow = ObjFlowXmlPropertyGridSettings.ObjFlowsList[listBox1.SelectedIndex];
                ObjFlowXmlPropertyGridSettings.ObjFlowsList.Remove(objFlow);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void AddObjFlowXmlData_Click(object sender, EventArgs e)
        {
            PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow objFlow = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow
            {
                ObjectID = "0000",
                ObjectName = "TestObject",
                Path = "",
                UseKCL = false,
                ObjectType = "Undefined Type",
                Commons = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Common
                {
                    ColType = "",
                    PathType = "",
                    ModelSetting = "",
                    Unknown1 = ""
                },
                LODSetting = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.LOD_Setting
                {
                    LOD = 0,
                    LODHPoly = 0,
                    LODLPoly = 0,
                    LODDef = 0
                },
                Scales = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Scale
                {
                    X = 0,
                    Y = 0,
                    Z = 0
                },
                Names = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Name
                {
                    Main = "Main",
                    Sub = "Sub"
                },
                DefaultValues = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values
                {
                    ValuesList = new List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value>()
                    {
                        new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Text"
                        },
                        new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values.Value
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

            List<ObjFlowData_XML.ObjFlow> ObjFlowDBList = new List<ObjFlowData_XML.ObjFlow>();

            foreach (var ObjFlowData in ObjFlowXmlPropertyGridSettings.ObjFlowsList)
			{
                ObjFlowData_XML.ObjFlow ObjFlowDB = new ObjFlowData_XML.ObjFlow
                {
                    ObjectID = ObjFlowData.ObjectID,
                    ObjectName = ObjFlowData.ObjectName,
                    Path = ObjFlowData.Path,
                    UseKCL = ObjFlowData.UseKCL,
                    ObjectType = ObjFlowData.ObjectType,
                    Commons = new ObjFlowData_XML.ObjFlow.Common
                    {
                        ColType = ObjFlowData.Commons.ColType,
                        PathType = ObjFlowData.Commons.PathType,
                        ModelSetting = ObjFlowData.Commons.ModelSetting,
                        Unknown1 = ObjFlowData.Commons.Unknown1
                    },
                    LODSetting = new ObjFlowData_XML.ObjFlow.LOD_Setting
                    {
                        LOD = ObjFlowData.LODSetting.LOD,
                        LODHPoly = ObjFlowData.LODSetting.LODHPoly,
                        LODLPoly = ObjFlowData.LODSetting.LODLPoly,
                        LODDef = ObjFlowData.LODSetting.LODDef
                    },
                    Scales = new ObjFlowData_XML.ObjFlow.Scale
                    {
                        X = (int)ObjFlowData.Scales.X,
                        Y = (int)ObjFlowData.Scales.Y,
                        Z = (int)ObjFlowData.Scales.Z
                    },
                    Names = new ObjFlowData_XML.ObjFlow.Name
                    {
                        Main = ObjFlowData.Names.Main,
                        Sub = ObjFlowData.Names.Sub
                    },
                    DefaultValues = new ObjFlowData_XML.ObjFlow.Default_Values
                    {
                        Values = null
                    }
                };

                List<ObjFlowData_XML.ObjFlow.Default_Values.Value> valueList = new List<ObjFlowData_XML.ObjFlow.Default_Values.Value>();

                foreach (var ObjFlowDataValue in ObjFlowData.DefaultValues.ValuesList)
                {
                    ObjFlowData_XML.ObjFlow.Default_Values.Value value = new ObjFlowData_XML.ObjFlow.Default_Values.Value
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
            ObjFlowConverter.Xml.WriteObjFlowXml(ObjFlowDBList, "ObjFlowData.Xml");
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(ObjFlowXmlPropertyGridSettings.ObjFlowsList.ToArray());
        }
    }
}
