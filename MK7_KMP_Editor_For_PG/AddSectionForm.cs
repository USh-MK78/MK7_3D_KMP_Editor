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
    public partial class AddSectionForm : Form
    {
        public AddSectionForm()
        {
            InitializeComponent();
        }

        Form1 Form1;

        private void AddSectionForm_Load(object sender, EventArgs e)
        {
            Form1 = (Form1)Application.OpenForms["Form1"];

            string[] AllSectionAry = new string[] { "KartPoint", "EnemyRoutes", "ItemRoutes", "CheckPoint", "Obj", "Route", "Area", "Camera", "JugemPoint", "GlideRoutes" };

            var AllSection_List = AllSectionAry.ToList();

            var GetSectionList = Form1.KMPSectionComboBox.Items.Cast<Object>().Select(item => item.ToString()).ToList();

            for (int i = 0; i < GetSectionList.Count; i++)
            {
                AllSection_List.Remove(GetSectionList[i]);
            }

            comboBox1.Items.AddRange(AllSection_List.ToArray());

            if (AllSection_List.Count == 0)
            {
                comboBox1.SelectedIndex = -1;
            }
            if (AllSection_List.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "KartPoint")
            {
                KMPPropertyGridSettings.TPTK_Section tPTK_Section = new KMPPropertyGridSettings.TPTK_Section
                {
                    TPTKValueList = new List<KMPPropertyGridSettings.TPTK_Section.TPTKValue>()
                };

                Form1.TPTK_Section = tPTK_Section;
            }
            if (comboBox1.Text == "EnemyRoutes")
            {
                KMPPropertyGridSettings.HPNE_TPNE_Section hPNE_TPNE_Section = new KMPPropertyGridSettings.HPNE_TPNE_Section
                {
                    HPNEValueList = new List<KMPPropertyGridSettings.HPNE_TPNE_Section.HPNEValue>()
                };

                Form1.HPNE_TPNE_Section = hPNE_TPNE_Section;
            }
            if (comboBox1.Text == "ItemRoutes")
            {
                KMPPropertyGridSettings.HPTI_TPTI_Section hPTI_TPTI_Section = new KMPPropertyGridSettings.HPTI_TPTI_Section
                {
                    HPTIValueList = new List<KMPPropertyGridSettings.HPTI_TPTI_Section.HPTIValue>()
                };

                Form1.HPTI_TPTI_Section = hPTI_TPTI_Section;
            }
            if (comboBox1.Text == "CheckPoint")
            {
                KMPPropertyGridSettings.HPKC_TPKC_Section hPKC_TPKC_Section = new KMPPropertyGridSettings.HPKC_TPKC_Section
                {
                    HPKCValueList = new List<KMPPropertyGridSettings.HPKC_TPKC_Section.HPKCValue>()
                };

                Form1.HPKC_TPKC_Section = hPKC_TPKC_Section;
            }
            if (comboBox1.Text == "Obj")
            {
                KMPPropertyGridSettings.JBOG_section jBOG_Section = new KMPPropertyGridSettings.JBOG_section
                {
                    JBOGValueList = new List<KMPPropertyGridSettings.JBOG_section.JBOGValue>()
                };

                Form1.JBOG_Section = jBOG_Section;
            }
            if (comboBox1.Text == "Route")
            {
                KMPPropertyGridSettings.ITOP_Section iTOP_Section = new KMPPropertyGridSettings.ITOP_Section
                {
                    ITOP_RouteList = new List<KMPPropertyGridSettings.ITOP_Section.ITOP_Route>()
                };

                Form1.ITOP_Section = iTOP_Section;
            }
            if (comboBox1.Text == "Area")
            {
                KMPPropertyGridSettings.AERA_Section aERA_Section = new KMPPropertyGridSettings.AERA_Section
                {
                    AERAValueList = new List<KMPPropertyGridSettings.AERA_Section.AERAValue>()
                };

                Form1.AERA_Section = aERA_Section;
            }
            if (comboBox1.Text == "Camera")
            {
                KMPPropertyGridSettings.EMAC_Section eMAC_Section = new KMPPropertyGridSettings.EMAC_Section
                {
                    EMACValueList = new List<KMPPropertyGridSettings.EMAC_Section.EMACValue>()
                };

                Form1.EMAC_Section = eMAC_Section;
            }
            if (comboBox1.Text == "JugemPoint")
            {
                KMPPropertyGridSettings.TPGJ_Section tPGJ_Section = new KMPPropertyGridSettings.TPGJ_Section
                {
                    TPGJValueList = new List<KMPPropertyGridSettings.TPGJ_Section.TPGJValue>()
                };

                Form1.TPGJ_Section = tPGJ_Section;
            }
            if (comboBox1.Text == "GlideRoutes")
            {
                KMPPropertyGridSettings.HPLG_TPLG_Section hPLG_TPLG_Section = new KMPPropertyGridSettings.HPLG_TPLG_Section
                {
                    HPLGValueList = new List<KMPPropertyGridSettings.HPLG_TPLG_Section.HPLGValue>()
                };

                Form1.HPLG_TPLG_Section = hPLG_TPLG_Section;
            }

            Form1.KMPSectionComboBox.Items.Add(comboBox1.Text);

            comboBox1.Items.Remove(comboBox1.Text);
        }
    }
}
