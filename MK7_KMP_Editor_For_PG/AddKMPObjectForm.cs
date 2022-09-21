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

        public List<KMPs.KMPHelper.ObjFlowReader.Xml.ObjFlowDB> ObjFlowDictionary { get; set; }
        ModelVisual3D dv3D_OBJ = null;

        public int ids = -1;

        public AddKMPObjectForm()
        {
            InitializeComponent();
            elementHost1.Child = render;

            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.ReadOnly = true;

            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public SelectedKMPObjectInfo SelectedKMPObject_Info { get; set; } = new SelectedKMPObjectInfo();
        public class SelectedKMPObjectInfo
        {
            public string Name { get; set; }
            public string ObjID { get; set; }
        }

        private void AddKMPObjectForm_Load(object sender, EventArgs e)
        {
            ObjFlowDictionary = KMPs.KMPHelper.ObjFlowReader.Xml.ReadObjFlowXml("ObjFlowData.xml");

            AutoCompleteStringCollection ACS_ObjFlowCollection = new AutoCompleteStringCollection();

            foreach (var f in ObjFlowDictionary)
            {
                comboBox1.Items.Add(f.ObjectName + "," + f.ObjectID);
                ACS_ObjFlowCollection.Add(f.ObjectName + "," + f.ObjectID);
            }

            comboBox1.AutoCompleteCustomSource = ACS_ObjFlowCollection;

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //reset
            render.MainViewPort.Children.Remove(dv3D_OBJ);

            #region Add Model(OBJ)
            string Path = ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Path;
            dv3D_OBJ = HTK_3DES.TSRSystem.OBJReader(Path);

            HTK_3DES.TSRSystem.Transform OBJ_transform_Value = new HTK_3DES.TSRSystem.Transform
            {
                Translate3D = new Vector3D(0, 0, 0),
                Scale3D = new Vector3D(1, 1, 1),
                Rotate3D = new Vector3D(0, 0, 0)
            };

            HTK_3DES.TSRSystem.TSRSystem3D tSRSystem3D = new HTK_3DES.TSRSystem.TSRSystem3D(dv3D_OBJ, OBJ_transform_Value);
            tSRSystem3D.TestTransform3D();

            render.MainViewPort.Children.Add(dv3D_OBJ);
            #endregion

            SelectedKMPObject_Info.Name = ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectName;
            SelectedKMPObject_Info.ObjID = ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectID;

            textBox1.Text = "Object ID : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectID + "\r\n" +
                            "Object Name : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectName + "\r\n" +
                            "Object Path : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Path + "\r\n" +
                            "Use KCL : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).UseKCL + "\r\n" +
                            "ColType : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Commons.ColType + "\r\n" +
                            "PathType : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Commons.PathType + "\r\n" +
                            "ModelSetting : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Commons.ModelSetting + "\r\n" +
                            "Unknown : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Commons.Unknown1 + "\r\n" +
                            "LOD : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).LODSetting.LOD + "\r\n" +
                            "LOD Low Poly : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).LODSetting.LODLPoly + "\r\n" +
                            "LOD High Poly : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).LODSetting.LODHPoly + "\r\n" +
                            "LOD Default : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).LODSetting.LODDef + "\r\n" +
                            "Main Name : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Names.Main + "\r\n" +
                            "Sub Name : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).Names.Sub + "\r\n" +
                            "Default Value 0 : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[0].Description + "\r\n" +
                            "Default Value 1 : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[1].Description + "\r\n" +
                            "Default Value 2 : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[2].Description + "\r\n" +
                            "Default Value 3 : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[3].Description + "\r\n" +
                            "Default Value 4 : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[4].Description + "\r\n" +
                            "Default Value 5 : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[5].Description + "\r\n" +
                            "Default Value 6 : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[6].Description + "\r\n" +
                            "Default Value 7 : " + ObjFlowDictionary.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).DefaultValues.Values[7].Description + "\r\n";
        }
    }
}
