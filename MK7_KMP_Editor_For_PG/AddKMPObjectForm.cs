﻿using KMPLibrary.KMPHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static MK7_3D_KMP_Editor.HTK_3DES;

namespace MK7_3D_KMP_Editor
{
    public partial class AddKMPObjectForm : Form
    {
        //UserControl1.xamlの初期化
        //ここは作成時の名前にも影響されるので必ず確認すること
        public UserControl2 render = new UserControl2();

        public List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDictionary { get; set; }
        ModelVisual3D MV3D_OBJ = null;

        public string DefaultObjectID { get; set; }

        public SelectedKMPObjectInfo SelectedKMPObject_Info { get; set; }
        public class SelectedKMPObjectInfo
        {
            public string ObjectName { get; set; }
            public string ObjectID { get; set; }

            public SelectedKMPObjectInfo(string ObjectName, string ObjectID)
            {
                this.ObjectName = ObjectName;
                this.ObjectID = ObjectID;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedKMPObjectInfo"></param>
        /// <param name="DefaultObjectID">DefaultObjectID (Setting)</param>
        public AddKMPObjectForm(SelectedKMPObjectInfo selectedKMPObjectInfo, string DefaultObjectID)
        {
            InitializeComponent();
            elementHost1.Child = render;

            ObjFlowXMLInfo_TXT.ScrollBars = ScrollBars.Vertical;
            ObjFlowXMLInfo_TXT.ReadOnly = true;

            ObjFlowItemComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            ObjFlowItemComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            this.DefaultObjectID = DefaultObjectID;
            SelectedKMPObject_Info = selectedKMPObjectInfo;
        }

        private void AddKMPObjectForm_Load(object sender, EventArgs e)
        {
            ObjFlowDictionary = KMPLibrary.XMLConvert.Statics.ObjFlow.ReadObjFlowXml("ObjFlowData.xml").ObjFlows;

            AutoCompleteStringCollection ACS_ObjFlowCollection = new AutoCompleteStringCollection();

            foreach (var item in ObjFlowDictionary)
            {
                ObjFlowItemComboBox.Items.Add(item.ObjectName + "," + item.ObjectID);
                ACS_ObjFlowCollection.Add(item.ObjectName + "," + item.ObjectID);
            }

            ObjFlowItemComboBox.AutoCompleteCustomSource = ACS_ObjFlowCollection;

            if (SelectedKMPObject_Info == null)
            {
                var Item = ObjFlowDictionary.Find(x => x.ObjectID == DefaultObjectID);
                ObjFlowItemComboBox.SelectedIndex = ObjFlowItemComboBox.FindStringExact(Item.ObjectName + "," + Item.ObjectID);
            }
            else if (SelectedKMPObject_Info != null)
            {
                ObjFlowItemComboBox.SelectedIndex = ObjFlowItemComboBox.FindStringExact(SelectedKMPObject_Info.ObjectName + "," + SelectedKMPObject_Info.ObjectID);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //reset
            render.MainViewPort.Children.Remove(MV3D_OBJ);

            #region Add Model(OBJ)
            string Path = ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).Path;
            MV3D_OBJ = HTK_3DES.OBJReader(Path);

            Transform OBJ_transform_Value = new Transform { Translate3D = new Vector3D(0, 0, 0), Scale3D = new Vector3D(1, 1, 1), Rotate3D = new Vector3D(0, 0, 0) };

            TSRSystem3D tSRSystem3D = new TSRSystem3D(MV3D_OBJ, OBJ_transform_Value);
            tSRSystem3D.Transform3D(TSRSystem3D.RotationCenterSetting.DefaultCenterSetting(), TSRSystem3D.RotationType.Radian);

            render.MainViewPort.Children.Add(MV3D_OBJ);
            #endregion

            var Data = ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]);
            SelectedKMPObject_Info = new SelectedKMPObjectInfo(Data.ObjectName, Data.ObjectID);

            ObjFlowXMLInfo_TXT.Text = "Object ID : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).ObjectID + "\r\n" +
                                      "Object Name : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).ObjectName + "\r\n" +
                                      "Object Path : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).Path + "\r\n" +
                                      "Use KCL : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).UseKCL + "\r\n" +
                                      "ColType : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).CommonData.ColType + "\r\n" +
                                      "PathType : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).CommonData.PathType + "\r\n" +
                                      "ModelSetting : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).CommonData.ModelSetting + "\r\n" +
                                      "Unknown : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).CommonData.Unknown1 + "\r\n" +
                                      "LOD : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).LODSetting.LOD + "\r\n" +
                                      "LOD Low Poly : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).LODSetting.LODLowPoly + "\r\n" +
                                      "LOD High Poly : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).LODSetting.LODHighPoly + "\r\n" +
                                      "LOD Default : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).LODSetting.LODDefault + "\r\n" +
                                      "Main Name : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).NameData.Main + "\r\n" +
                                      "Sub Name : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).NameData.Sub + "\r\n" +
                                      "Default Value 0 : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).DefaultValueData.Values[0].Description + "\r\n" +
                                      "Default Value 1 : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).DefaultValueData.Values[1].Description + "\r\n" +
                                      "Default Value 2 : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).DefaultValueData.Values[2].Description + "\r\n" +
                                      "Default Value 3 : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).DefaultValueData.Values[3].Description + "\r\n" +
                                      "Default Value 4 : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).DefaultValueData.Values[4].Description + "\r\n" +
                                      "Default Value 5 : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).DefaultValueData.Values[5].Description + "\r\n" +
                                      "Default Value 6 : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).DefaultValueData.Values[6].Description + "\r\n" +
                                      "Default Value 7 : " + ObjFlowDictionary.Find(x => x.ObjectID == ObjFlowItemComboBox.Text.Split(',')[1]).DefaultValueData.Values[7].Description + "\r\n";
        }
    }
}
