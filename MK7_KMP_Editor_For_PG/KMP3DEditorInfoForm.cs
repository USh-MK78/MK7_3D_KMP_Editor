using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MK7_KMP_Editor_For_PG_
{
    public partial class KMP3DEditorInfoForm : Form
    {
        public KMP3DEditorInfoForm()
        {
            InitializeComponent();
        }

        private void KMP3DEditorInfoForm_Load(object sender, EventArgs e)
        {
            MK73DKMPEditorImgBox.SizeMode = PictureBoxSizeMode.Zoom;
            MK73DKMPEditorImgBox.Image = Properties.Resources.MK73DKMPEditorLogo;
        }

        private void MH_GithubLink_LinkLBL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MH_GithubLink_LinkLBL.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/MichaelHinrichs/MK7_3D_KMP_Editor/commit/2ac239a870d673651056f7d5b57faed77817073e");
        }

        private void B_SquoTCRFLink_LBL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MH_GithubLink_LinkLBL.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tcrf.net/User:B_squo#Differences_between_Gctr_MarioCircuit_Divide.27s_.280x0BB8.29_and_the_final_.280x0C1C.29_KMP_versions");
        }
    }
}
