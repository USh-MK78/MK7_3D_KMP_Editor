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

        HTK_3DES.TSRSystem HTK_3DEdit = new HTK_3DES.TSRSystem();
        KMPs.KMPHelper.ObjFlowReader ObjFlowReader = new KMPs.KMPHelper.ObjFlowReader();
        HTK_3DES.TransformMV3D_NewCreate TransformMV3D_NotNewCreate = new HTK_3DES.TransformMV3D_NewCreate();


        public AddKMPObjectForm()
        {
            InitializeComponent();
            elementHost1.Child = render;
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
            dv3D_OBJ = HTK_3DEdit.OBJReader(Path);

            TransformMV3D_NotNewCreate.Transform_MV3D(OBJ_transform_Value, dv3D_OBJ);

            render.MainViewPort.Children.Add(dv3D_OBJ);
            #endregion

            SelectedKMPObject_Info.Name = ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectName;
            SelectedKMPObject_Info.ObjID = ObjFlowDictionary.ObjFlows.Find(x => x.ObjectID == comboBox1.Text.Split(',')[1]).ObjectID;
        }
    }
}
