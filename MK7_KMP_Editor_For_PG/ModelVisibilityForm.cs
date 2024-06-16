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

namespace MK7_3D_KMP_Editor
{
    public partial class ModelVisibilityForm : Form
    {
        Form1 Form1;
        public ModelVisibilityForm()
        {
            InitializeComponent();
            TopMost = true;
            Form1 = (Form1)Application.OpenForms["Form1"];
        }

        List<string> KeyList;
        private void ModelVisibilityForm_Load(object sender, EventArgs e)
        {
            treeView1.HideSelection = false;

            KeyList = new List<string>(Form1.MV3D_Dictionary.Keys);

            List<TreeNode> TreeNodeList = new List<TreeNode>();
            for (int i = 0; i < KeyList.Count; i++)
            {
                TreeNode ModelParts = new TreeNode(KeyList[i]);
                TreeNodeList.Add(ModelParts);
            }

            TreeNode ModelGroupNode = new TreeNode("Model_Root", TreeNodeList.ToArray());
            treeView1.Nodes.Add(ModelGroupNode);
            treeView1.TopNode.Expand();
        }

        string NodeName = string.Empty;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NodeName = treeView1.SelectedNode.Text;

            if (Form1.MV3D_Dictionary.ContainsKey(NodeName))
            {
                bool d = (bool)Form1.MV3D_Dictionary[NodeName][0];
                checkBox1.Checked = d;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Form1.MV3D_Dictionary.ContainsKey(NodeName))
            {
                var ModelVisual3D = (ModelVisual3D)Form1.MV3D_Dictionary[NodeName][1];
                ViewPortObjVisibleSetting.ViewportObj_Visibility(checkBox1.Checked, Form1.render, ModelVisual3D);
                Form1.MV3D_Dictionary[NodeName][0] = checkBox1.Checked;
            }
        }
    }
}
