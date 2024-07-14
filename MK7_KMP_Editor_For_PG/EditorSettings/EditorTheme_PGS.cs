using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MK7_3D_KMP_Editor.PropertyGridObject.CustomPropertyGridClassConverter;
using static System.Net.Mime.MediaTypeNames;

namespace MK7_3D_KMP_Editor.EditorSettings
{
    public class EditorTheme_PGS
    {
        [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
        public MainForm MainFormTheme { get; set; } = new MainForm();
        public class MainForm
        {
            public Color BaseColor { get; set; }
            public Color TextColor { get; set; }

            public MainForm(EditorSettings.EditorThemeXML.MainForm mainForm)
            {
                BaseColor = mainForm.BaseColor.ToColor();
                TextColor = mainForm.TextColor.ToColor();
            }

            public MainForm(Color Base, Color Text)
            {
                BaseColor = Base;
                TextColor = Text;
            }

            public MainForm() { }
        }

        [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
        public MainSplitContainer MainSplitContainerTheme { get; set; } = new MainSplitContainer();
        public class MainSplitContainer
        {
            public Color Panel1BaseColor { get; set; }
            public Color Panel1TextColor { get; set; }

            public Color Panel2BaseColor { get; set; }
            public Color Panel2TextColor { get; set; }

            public MainSplitContainer(EditorSettings.EditorThemeXML.MainSplitContainer mainSplitContainer)
            {
                Panel1BaseColor = mainSplitContainer.Panel1_BaseColor.ToColor();
                Panel1TextColor = mainSplitContainer.Panel1_TextColor.ToColor();

                Panel2BaseColor = mainSplitContainer.Panel2_BaseColor.ToColor();
                Panel2TextColor = mainSplitContainer.Panel2_TextColor.ToColor();
            }

            public MainSplitContainer(Color p1Base, Color p1Text, Color p2Base, Color p2Text)
            {
                Panel1BaseColor = p1Base;
                Panel1TextColor = p1Text;
                Panel2BaseColor = p2Base;
                Panel2TextColor = p2Text;
            }

            public MainSplitContainer() { }
        }

        public List<MainTab> MainTabThemeList = new List<MainTab>();
        public List<MainTab> MainTabTheme_List { get => MainTabThemeList; set => MainTabThemeList = value; }
        public class MainTab
        {
            public Color BaseColor { get; set; }
            public Color TextColor { get; set; }

            public MainTab(EditorSettings.EditorThemeXML.MainTab mainTab)
            {
                BaseColor = mainTab.BaseColor.ToColor();
                TextColor = mainTab.TextColor.ToColor();
            }

            public MainTab(Color Base, Color Text)
            {
                BaseColor = Base;
                TextColor = Text;
            }

            public MainTab() { }
        }

        public EditorTheme_PGS(EditorSettings.EditorThemeXML editorThemeXML)
        {
            MainFormTheme = new MainForm(editorThemeXML.MainFormTheme);

            MainSplitContainerTheme = new MainSplitContainer(editorThemeXML.MainSplitContainerTheme);
            
            foreach (var item in editorThemeXML.MainTabThemeList)
            {
                MainTabThemeList.Add(new MainTab(item));
            }
        }

        public EditorTheme_PGS(MainForm mainForm, MainSplitContainer mainSplitContainer, List<MainTab> mainTabs)
        {
            MainFormTheme = mainForm;
            MainSplitContainerTheme = mainSplitContainer;
            MainTabThemeList = mainTabs;
        }

        public EditorTheme_PGS() { }

    }
}
