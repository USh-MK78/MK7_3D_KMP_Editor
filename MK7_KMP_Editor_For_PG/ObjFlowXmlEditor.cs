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
        public ObjFlowData_XML ObjFlowDataXml_List;

        public PropertyGridObject.ObjFlow.ObjFlow_PGS ObjFlowXmlPropertyGridSettings;

        public ObjFlowXmlEditor()
        {
            InitializeComponent();
        }

        private void ObjFlowXmlEditor_Load(object sender, EventArgs e)
        {
            ObjFlowDataXml_List = KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.Xml");
            
            List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow> objFlows = new List<PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow>();
            foreach (var f in ObjFlowDataXml_List.ObjFlows)
            {
                PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow objFlow = new PropertyGridObject.ObjFlow.ObjFlow_PGS.ObjFlow(f);
                objFlows.Add(objFlow);
                listBox1.Items.Add(objFlow);
            }

            ObjFlowXmlPropertyGridSettings = new PropertyGridObject.ObjFlow.ObjFlow_PGS(ObjFlowDataXml_List);

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

            //TODO : PGS => XML
            foreach (var ObjFlowData in ObjFlowXmlPropertyGridSettings.ObjFlowsList)
            {
                var CommonData = new ObjFlowData_XML.ObjFlow.Common(ObjFlowData.CommonData.ColType, ObjFlowData.CommonData.PathType, ObjFlowData.CommonData.ModelSetting, ObjFlowData.CommonData.Unknown1);
                var LODSettingData = new ObjFlowData_XML.ObjFlow.LOD_Setting(ObjFlowData.LODSetting.LOD, ObjFlowData.LODSetting.LODHighPoly, ObjFlowData.LODSetting.LODLowPoly, ObjFlowData.LODSetting.LODDefault);
                var ScaleData = new ObjFlowData_XML.ObjFlow.Scale((int)ObjFlowData.ScaleData.X, (int)ObjFlowData.ScaleData.Y, (int)ObjFlowData.ScaleData.Z);
                var NameData = new ObjFlowData_XML.ObjFlow.Name(ObjFlowData.NameData.Main, ObjFlowData.NameData.Sub);

                List<ObjFlowData_XML.ObjFlow.DefaultValue.Value> ValueList = new List<ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

                foreach (var ObjFlowDataValue in ObjFlowData.DefaultValueData.ValuesList)
                {
                    ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new ObjFlowData_XML.ObjFlow.DefaultValue.Value(ObjFlowDataValue.DefaultObjectValue, ObjFlowDataValue.Description);
                    ValueList.Add(value);
                }

                ObjFlowData_XML.ObjFlow ObjFlowDB = new ObjFlowData_XML.ObjFlow
                {
                    ObjectID = ObjFlowData.ObjectID,
                    ObjectName = ObjFlowData.ObjectName,
                    Path = ObjFlowData.Path,
                    UseKCL = ObjFlowData.UseKCL,
                    ObjectType = ObjFlowData.ObjectType,
                    CommonData = CommonData,
                    LODSetting = LODSettingData,
                    ScaleData = ScaleData,
                    NameData = NameData,
                    DefaultValueData = new ObjFlowData_XML.ObjFlow.DefaultValue(ValueList)
                };

                ObjFlowDBList.Add(ObjFlowDB);

            }

            //Save ObjFlowData.xml
            KMPLibrary.XMLConvert.Statics.ObjFlow.WriteObjFlowXml(ObjFlowDBList, "ObjFlowData.Xml");
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(ObjFlowXmlPropertyGridSettings.ObjFlowsList.ToArray());
        }
    }
}
