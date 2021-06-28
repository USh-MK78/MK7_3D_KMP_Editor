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

namespace MK7_KMP_Editor_For_PG_
{
    public partial class AddKMPObjectForm : Form
    {
        //UserControl1.xamlの初期化
        //ここは作成時の名前にも影響されるので必ず確認すること
        public UserControl2 render = new UserControl2();

        public KMPs.KMPHelper.ObjFlowReader.ObjFlowXmlToObject ObjFlowDictionary { get; set; }
        ModelVisual3D dv3D_OBJ = null;

        KMPs.KMPHelper.ObjFlowReader ObjFlowReader = new KMPs.KMPHelper.ObjFlowReader();

        public AddKMPObjectForm()
        {
            InitializeComponent();
            elementHost1.Child = render;

            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.ReadOnly = true;
        }

        public SelectedKMPObjectInfo SelectedKMPObject_Info { get; set; } = new SelectedKMPObjectInfo();
        public class SelectedKMPObjectInfo
        {
            public string Name { get; set; }
            public string ObjID { get; set; }
        }

        private void AddKMPObjectForm_Load(object sender, EventArgs e)
        {
            ObjFlowDictionary = ObjFlowReader.ReadObjFlowXml("ObjFlowData.xml");

            foreach (var f in ObjFlowDictionary.ObjFlows)
            {
                comboBox1.Items.Add(f.ObjectName + "," + f.ObjectID);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //reset
            render.MainViewPort.Children.Remove(dv3D_OBJ);

            #region Add Model(OBJ)
            HTK_3DES.TSRSystem.Transform_Value OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform_Value
            {
                Translate_Value = new HTK_3DES.TSRSystem.Transform_Value.Translate
                {
                    X = 0,
                    Y = 0,
                    Z = 0
                },
                Scale_Value = new HTK_3DES.TSRSystem.Transform_Value.Scale
                {
                    X = 1,
                    Y = 1,
                    Z = 1
                },
                Rotate_Value = new HTK_3DES.TSRSystem.Transform_Value.Rotate
                {
                    X = 0,
                    Y = 0,
                    Z = 0
                }
            };

            string Path = ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Path;
            dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(Path);

            HTK_3DES.TransformMV3D.Transform_MV3D(OBJ_transform_Value, dv3D_OBJ);

            render.MainViewPort.Children.Add(dv3D_OBJ);
            #endregion

            SelectedKMPObject_Info.Name = ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectName;
            SelectedKMPObject_Info.ObjID = ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectID;

            textBox1.Text = "Object ID : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectID + "\r\n" +
                            "Object Name : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectName + "\r\n" +
                            "Object Path : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Path + "\r\n" +
                            "Use KCL : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).UseKCL + "\r\n" +
                            "ColType : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Commons.ColType + "\r\n" +
                            "PathType : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Commons.PathType + "\r\n" +
                            "ModelSetting : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Commons.ModelSetting + "\r\n" +
                            "Unknown : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Commons.Unknown1 + "\r\n" +
                            "LOD : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).LODSetting.LOD + "\r\n" +
                            "LOD Low Poly : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).LODSetting.LODLPoly + "\r\n" +
                            "LOD High Poly : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).LODSetting.LODHPoly + "\r\n" +
                            "LOD Default : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).LODSetting.LODDef + "\r\n" +
                            "Main Name : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Names.Main + "\r\n" +
                            "Sub Name : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Names.Sub + "\r\n" +
                            "Default Value 0 : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[0].Description + "\r\n" +
                            "Default Value 1 : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[1].Description + "\r\n" +
                            "Default Value 2 : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[2].Description + "\r\n" +
                            "Default Value 3 : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[3].Description + "\r\n" +
                            "Default Value 4 : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[4].Description + "\r\n" +
                            "Default Value 5 : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[5].Description + "\r\n" +
                            "Default Value 6 : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[6].Description + "\r\n" +
                            "Default Value 7 : " + ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[7].Description + "\r\n";
        }
    }
}
