using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MK7_3D_KMP_Editor.EditorSettings.EditorTheme_PGS;

namespace MK7_3D_KMP_Editor.EditorSettings
{
    [System.Xml.Serialization.XmlRoot("Theme")]
    public class EditorThemeXML
    {
        [System.Xml.Serialization.XmlElement("MainFormColor")]
        public MainForm MainFormTheme { get; set; } = new MainForm();
        public class MainForm
        {
            [System.Xml.Serialization.XmlElement("MainFormBaseColor")]
            public ColorXML BaseColor { get; set; } = new ColorXML();

            [System.Xml.Serialization.XmlElement("MainFormTextColor")]
            public ColorXML TextColor { get; set; } = new ColorXML();

            public MainForm(EditorSettings.EditorTheme_PGS.MainForm mainForm)
            {
                BaseColor = new ColorXML(mainForm.BaseColor);
                TextColor = new ColorXML(mainForm.TextColor);
            }

            public MainForm(Color Base, Color Text)
            {
                BaseColor = new ColorXML(Base);
                TextColor = new ColorXML(Text);
            }

            public MainForm() { }
        }

        [System.Xml.Serialization.XmlElement("MainSplitContainerColor")]
        public MainSplitContainer MainSplitContainerTheme { get; set; } = new MainSplitContainer();
        public class MainSplitContainer
        {
            [System.Xml.Serialization.XmlElement("Panel1BaseColor")]
            public ColorXML Panel1_BaseColor { get; set; } = new ColorXML();

            [System.Xml.Serialization.XmlElement("Panel1TextColor")]
            public ColorXML Panel1_TextColor { get; set; } = new ColorXML();

            [System.Xml.Serialization.XmlElement("Panel2BaseColor")]
            public ColorXML Panel2_BaseColor { get; set; } = new ColorXML();

            [System.Xml.Serialization.XmlElement("Panel2TextColor")]
            public ColorXML Panel2_TextColor { get; set; } = new ColorXML();

            public MainSplitContainer(EditorSettings.EditorTheme_PGS.MainSplitContainer mainSplitContainer)
            {
                Panel1_BaseColor = new ColorXML(mainSplitContainer.Panel1BaseColor);
                Panel1_TextColor = new ColorXML(mainSplitContainer.Panel1TextColor);
                Panel2_BaseColor = new ColorXML(mainSplitContainer.Panel2BaseColor);
                Panel2_TextColor = new ColorXML(mainSplitContainer.Panel2TextColor);
            }

            public MainSplitContainer(Color p1Base, Color p1Text, Color p2Base, Color p2Text)
            {
                Panel1_BaseColor = new ColorXML(p1Base);
                Panel1_TextColor = new ColorXML(p1Text);

                Panel2_BaseColor = new ColorXML(p2Base);
                Panel2_TextColor = new ColorXML(p2Text);
            }

            public MainSplitContainer() { }
        }

        [System.Xml.Serialization.XmlElement("MainTabColor")]
        public List<MainTab> MainTabThemeList { get; set; } = new List<MainTab>();
        public class MainTab
        {
            [System.Xml.Serialization.XmlElement("MainTabBaseColor")]
            public ColorXML BaseColor { get; set; } = new ColorXML();

            [System.Xml.Serialization.XmlElement("MainTabTextColor")]
            public ColorXML TextColor { get; set; } = new ColorXML();

            public MainTab(EditorSettings.EditorTheme_PGS.MainTab mainTab)
            {
                BaseColor = new ColorXML(mainTab.BaseColor);
                TextColor = new ColorXML(mainTab.TextColor);
            }

            public MainTab(Color Base, Color Text)
            {
                BaseColor = new ColorXML(Base);
                TextColor = new ColorXML(Text);
            }

            public MainTab() { }
        }

        public EditorThemeXML(EditorSettings.EditorTheme_PGS editorTheme_PGS)
        {
            MainFormTheme = new MainForm(editorTheme_PGS.MainFormTheme);

            MainSplitContainerTheme = new MainSplitContainer(editorTheme_PGS.MainSplitContainerTheme);

            foreach (var item in editorTheme_PGS.MainTabThemeList)
            {
                MainTabThemeList.Add(new MainTab(item));
            }
        }

        public EditorThemeXML(MainForm mainForm, MainSplitContainer mainSplitContainer, List<MainTab> mainTabs)
        {
            MainFormTheme = mainForm;
            MainSplitContainerTheme = mainSplitContainer;
            MainTabThemeList = mainTabs;
        }

        public EditorThemeXML() { }
    }

    public class ColorXML
    {
        [System.Xml.Serialization.XmlAttribute("R")]
        public byte R { get; set; }

        [System.Xml.Serialization.XmlAttribute("G")]
        public byte G { get; set; }

        [System.Xml.Serialization.XmlAttribute("B")]
        public byte B { get; set; }

        [System.Xml.Serialization.XmlAttribute("A")]
        public byte A { get; set; }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }

        public ColorXML(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        public ColorXML() { }
    }
}
