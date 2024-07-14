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
using System.Xml.Serialization;

namespace MK7_3D_KMP_Editor.EditorSettings
{
    public partial class EditorSettingForm : Form
    {
        public EditorSettings.EditorSettingXML EditorSettingXML { get; set; }
        public EditorSettings.EditorThemeXML EditorThemeXML { get; set; }

        public EditorTheme_PGS EditorTheme_PGS { get; set; }

        public EditorSettingForm(EditorSettings.EditorSettingXML editorSettingXML, EditorSettings.EditorThemeXML editorThemeXML)
        {
            InitializeComponent();
            EditorSettingXML = editorSettingXML;
            EditorThemeXML = editorThemeXML;
        }

        private void EditorSettingForm_Load(object sender, EventArgs e)
        {
            DefaultDirectoryTXT.Text = EditorSettingXML.FilePathSetting.DefaultDirectory;
            DefaultNameKMP_TXT.Text = EditorSettingXML.FilePathSetting.DefaultKMPFileName;
            DefaultNameXML_TXT.Text = EditorSettingXML.FilePathSetting.DefaultXMLFileName;

            DefaultObjectID_TXT.Text = EditorSettingXML.GeneralSetting.DefaultValueObjectID;

            EditorTheme_PGS = new EditorTheme_PGS(EditorThemeXML);
            EditorTheme_PropertyGrid.SelectedObject = EditorTheme_PGS;
        }

        private void EditorSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EditorSettingXML.FilePathSetting.DefaultDirectory = DefaultDirectoryTXT.Text;
            EditorSettingXML.FilePathSetting.DefaultKMPFileName = DefaultNameKMP_TXT.Text;
            EditorSettingXML.FilePathSetting.DefaultXMLFileName = DefaultNameXML_TXT.Text;

            EditorSettingXML.GeneralSetting.DefaultValueObjectID = DefaultObjectID_TXT.Text;

            EditorThemeXML = new EditorThemeXML(EditorTheme_PGS);
        }

        public void EditDefaultDirectoryTXT()
        {
            if (DefaultDirectoryTXT.Text == "") DefaultDirectoryTXT.Text = Environment.CurrentDirectory;
            else if (DefaultDirectoryTXT.Text == "Desktop") DefaultDirectoryTXT.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            else
            {
                if (!System.IO.Directory.Exists(DefaultDirectoryTXT.Text))
                {
                    DefaultDirectoryTXT.Text = EditorSettingXML.FilePathSetting.DefaultDirectory;
                }
            }

            if (System.IO.Directory.Exists(DefaultDirectoryTXT.Text))
            {
                EditorSettingXML.FilePathSetting.DefaultDirectory = DefaultDirectoryTXT.Text;
                DefaultDirectoryTXT.Text = EditorSettingXML.FilePathSetting.DefaultDirectory;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void DefaultDirectoryTXT_Leave(object sender, EventArgs e)
        {
            EditDefaultDirectoryTXT();
        }

        private void DefaultDirectoryTXT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditDefaultDirectoryTXT();
            }
        }
    }
}
