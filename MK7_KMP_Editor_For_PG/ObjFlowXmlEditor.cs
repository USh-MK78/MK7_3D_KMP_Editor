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
        //public List<ObjFlowData_XML.ObjFlow> objFlowDataXml_List;
        public ObjFlowData_XML objFlowDataXml_List;

        public PropertyGridObject.ObjFlow.ObjFlow_PGS ObjFlowXmlPropertyGridSettings;

        public ObjFlowXmlEditor()
        {
            InitializeComponent();
        }

        private void ObjFlowXmlEditor_Load(object sender, EventArgs e)
        {
            objFlowDataXml_List = ObjFlowConverter.Xml.ReadObjFlowXml("ObjFlowData.Xml");
            
            List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow> objFlows = new List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow>();
            foreach (var f in objFlowDataXml_List.ObjFlows)
            {
                PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow objFlow = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow(f);
                objFlows.Add(objFlow);
                listBox1.Items.Add(objFlow);
            }

            ObjFlowXmlPropertyGridSettings = new PropertyGridObject.ObjFlow.ObjFlow_PGS(objFlowDataXml_List);

            //ObjFlowXmlPropertyGridSettings = new PropertyGridObject.ObjFlow.ObjFlow_PGS
            //{
            //    ObjFlowsList = objFlows
            //};


            //ObjFlowXmlPropertyGridSettings = new PropertyGridObject.ObjFlow.ObjFlow_PGS
            //{
            //    ObjFlowsList = null
            //};

            //List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow> objFlows = new List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow>();  

            //foreach (var f in objFlowDataXml_List)
            //{
            //    PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow objFlow = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow
            //    {
            //        ObjectID = f.ObjectID,
            //        ObjectName = f.ObjectName,
            //        Path = f.Path,
            //        UseKCL = f.UseKCL,
            //        ObjectType = f.ObjectType,
            //        CommonData = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Common
            //        {
            //            ColType = f.CommonData.ColType,
            //            PathType = f.CommonData.PathType,
            //            ModelSetting = f.CommonData.ModelSetting,
            //            Unknown1 = f.CommonData.Unknown1
            //        },
            //        LODSetting = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.LOD_Setting
            //        {
            //            LOD = f.LODSetting.LOD,
            //            LODHighPoly = f.LODSetting.LODHighPoly,
            //            LODLowPoly = f.LODSetting.LODLowPoly,
            //            LODDefault = f.LODSetting.LODDefault
            //        },
            //        ScaleData = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Scale
            //        {
            //            X = f.ScaleData.X,
            //            Y = f.ScaleData.Y,
            //            Z = f.ScaleData.Z
            //        },
            //        NameData = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Name
            //        {
            //            Main = f.NameData.Main,
            //            Sub = f.NameData.Sub
            //        },
            //        DefaultValueData = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Default_Values
            //        {
            //            ValuesList = null
            //        }
            //    };

            //    List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value> values_List = new List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value>();

            //    foreach (var ds in f.DefaultValueData.Values)
            //    {
            //        PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value value = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value
            //        {
            //            DefaultObjectValue = ds.DefaultObjectValue,
            //            Description = ds.Description
            //        };

            //        values_List.Add(value);
            //    }

            //    objFlow.DefaultValueData.ValuesList = values_List;

            //    objFlows.Add(objFlow);

            //    listBox1.Items.Add(objFlow);
            //}

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
            //PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow objFlow = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow
            //{
            //    ObjectID = "0000",
            //    ObjectName = "TestObject",
            //    Path = "",
            //    UseKCL = false,
            //    ObjectType = "Undefined Type",
            //    CommonData = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Common("", "", "", ""),
            //    LODSetting = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.LOD_Setting(0, 0, 0, 0),
            //    ScaleData = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Scale(0, 0, 0),
            //    NameData = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.Name("Main", "Sub"),
            //    DefaultValueData = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue
            //    {
            //        ValuesList = new List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value>()
            //        {
            //            new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value(0, "Text"),
            //            new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value(0, "Text"),
            //            new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value(0, "Text"),
            //            new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value(0, "Text"),
            //            new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value(0, "Text"),
            //            new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value(0, "Text"),
            //            new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value(0, "Text"),
            //            new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow.DefaultValue.Value(0, "Text")
            //        }
            //    }
            //};

            PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow objFlow = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow();

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
                    CommonData = new ObjFlowData_XML.ObjFlow.Common
                    {
                        ColType = ObjFlowData.CommonData.ColType,
                        PathType = ObjFlowData.CommonData.PathType,
                        ModelSetting = ObjFlowData.CommonData.ModelSetting,
                        Unknown1 = ObjFlowData.CommonData.Unknown1
                    },
                    LODSetting = new ObjFlowData_XML.ObjFlow.LOD_Setting
                    {
                        LOD = ObjFlowData.LODSetting.LOD,
                        LODHighPoly = ObjFlowData.LODSetting.LODHighPoly,
                        LODLowPoly = ObjFlowData.LODSetting.LODLowPoly,
                        LODDefault = ObjFlowData.LODSetting.LODDefault
                    },
                    ScaleData = new ObjFlowData_XML.ObjFlow.Scale
                    {
                        X = (int)ObjFlowData.ScaleData.X,
                        Y = (int)ObjFlowData.ScaleData.Y,
                        Z = (int)ObjFlowData.ScaleData.Z
                    },
                    NameData = new ObjFlowData_XML.ObjFlow.Name
                    {
                        Main = ObjFlowData.NameData.Main,
                        Sub = ObjFlowData.NameData.Sub
                    },
                    DefaultValueData = new ObjFlowData_XML.ObjFlow.DefaultValue
                    {
                        Values = null
                    }
                };

                List<ObjFlowData_XML.ObjFlow.DefaultValue.Value> valueList = new List<ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

                foreach (var ObjFlowDataValue in ObjFlowData.DefaultValueData.ValuesList)
                {
                    ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new ObjFlowData_XML.ObjFlow.DefaultValue.Value
                    {
                        DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
                        Description = ObjFlowDataValue.Description
                    };

                    valueList.Add(value);
                }

                ObjFlowDB.DefaultValueData.Values = valueList;

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
