using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBOCLibrary;

namespace KMPLibrary.XMLConvert.ObjFlowData
{
    [System.Xml.Serialization.XmlRoot("KMPObjFlowData")]
    public class ObjFlowData_XML
    {
        [System.Xml.Serialization.XmlElement("ObjFlow")]
        public List<ObjFlow> ObjFlows { get; set; } = new List<ObjFlow>();
        public class ObjFlow
        {
            [System.Xml.Serialization.XmlAttribute("ObjectID")]
            public string ObjectID { get; set; }

            [System.Xml.Serialization.XmlAttribute("ObjectName")]
            public string ObjectName { get; set; }

            [System.Xml.Serialization.XmlAttribute("Path")]
            public string Path { get; set; }

            [System.Xml.Serialization.XmlAttribute("KCLFile")]
            public bool UseKCL { get; set; }

            [System.Xml.Serialization.XmlAttribute("ObjectType")]
            public string ObjectType { get; set; }

            [System.Xml.Serialization.XmlElement("Common")]
            public Common CommonData { get; set; } = new Common();
            public class Common
            {
                [System.Xml.Serialization.XmlAttribute("ColType")]
                public string ColType { get; set; }

                [System.Xml.Serialization.XmlAttribute("PathType")]
                public string PathType { get; set; }

                [System.Xml.Serialization.XmlAttribute("ModelSetting")]
                public string ModelSetting { get; set; }

                [System.Xml.Serialization.XmlAttribute("Unknown1")]
                public string Unknown1 { get; set; }

                public Common() { }

                public Common(string ColType, string PathType, string ModelSetting, string Unknown1)
                {
                    this.ColType = ColType;
                    this.PathType = PathType;
                    this.ModelSetting = ModelSetting;
                    this.Unknown1 = Unknown1;
                }

                public Common(FBOC.ObjFlowData CommonData)
                {
                    ColType = BitConverter.ToString(CommonData.CollisionType.Reverse().ToArray()).Replace("-", string.Empty);
                    PathType = BitConverter.ToString(CommonData.PathType.Reverse().ToArray()).Replace("-", string.Empty);
                    ModelSetting = BitConverter.ToString(CommonData.ModelSetting.Reverse().ToArray()).Replace("-", string.Empty);
                    Unknown1 = BitConverter.ToString(CommonData.Unknown1.Reverse().ToArray(), 0).Replace("-", string.Empty);
                }
            }

            [System.Xml.Serialization.XmlElement("LODSetting")]
            public LOD_Setting LODSetting { get; set; } = new LOD_Setting();
            public class LOD_Setting
            {
                [System.Xml.Serialization.XmlAttribute("LOD")]
                public int LOD { get; set; }

                [System.Xml.Serialization.XmlAttribute("LODHighPoly")]
                public int LODHighPoly { get; set; }

                [System.Xml.Serialization.XmlAttribute("LODLowPoly")]
                public int LODLowPoly { get; set; }

                [System.Xml.Serialization.XmlAttribute("LODDefault")]
                public int LODDefault { get; set; }

                public LOD_Setting() { }

                public LOD_Setting(int LOD, int High, int Low, int Default)
                {
                    this.LOD = LOD;
                    this.LODHighPoly = High;
                    this.LODLowPoly = Low;
                    this.LODDefault = Default;
                }

                public LOD_Setting(FBOC.ObjFlowData.LODSetting LODSetting)
                {
                    LOD = LODSetting.LOD;
                    LODHighPoly = LODSetting.LODHighPoly;
                    LODLowPoly = LODSetting.LODLowPoly;
                    LODDefault = LODSetting.LODDefault;
                }
            }

            [System.Xml.Serialization.XmlElement("Scale")]
            public Scale ScaleData { get; set; } = new Scale();
            public class Scale
            {
                [System.Xml.Serialization.XmlAttribute("X")]
                public int X { get; set; }

                [System.Xml.Serialization.XmlAttribute("Y")]
                public int Y { get; set; }

                [System.Xml.Serialization.XmlAttribute("Z")]
                public int Z { get; set; }

                public Scale() { }

                public Scale(int X, int Y, int Z)
                {
                    this.X = X;
                    this.Y = Y;
                    this.Z = Z;
                }

                public Scale(FBOC.ObjFlowData.ObjFlowScaleSetting ObjFlowScaleSetting)
                {
                    X = ObjFlowScaleSetting.X;
                    Y = ObjFlowScaleSetting.Y;
                    Z = ObjFlowScaleSetting.Z;
                }
            }

            [System.Xml.Serialization.XmlElement("Name")]
            public Name NameData { get; set; } = new Name();
            public class Name
            {
                [System.Xml.Serialization.XmlAttribute("Main")]
                public string Main { get; set; }

                [System.Xml.Serialization.XmlAttribute("Sub")]
                public string Sub { get; set; }

                public Name() { }

                public Name(string Main, string Sub)
                {
                    this.Main = Main;
                    this.Sub = Sub;
                }

                public Name(FBOC.ObjFlowData NameData)
                {
                    Main = new string(NameData.ObjFlowName1).Replace("\0", "");
                    Sub = new string(NameData.ObjFlowName2).Replace("\0", "");
                }
            }

            [System.Xml.Serialization.XmlElement("DefaultValue")]
            public DefaultValue DefaultValueData { get; set; } = new DefaultValue();
            public class DefaultValue
            {
                [System.Xml.Serialization.XmlElement("Value")]
                public List<Value> Values { get; set; } = new List<Value>();
                public class Value
                {
                    [System.Xml.Serialization.XmlAttribute("Default")]
                    public int DefaultObjectValue { get; set; } = 0;

                    [System.Xml.Serialization.XmlText]
                    public string Description { get; set; } = "None";

                    public Value() { }
                    
                    public Value(int DefaultParamValue, string DescriptionText)
                    {
                        DefaultObjectValue = DefaultParamValue;
                        Description = DescriptionText;
                    }
                }

                public DefaultValue() { }

                public static List<Value> CreateDefaultValue()
                {
                    List<Value> values = new List<Value>();
                    for (int i = 0; i < 8; i++)
                    {
                        Value value = new Value(0, "Test" + i);
                        values.Add(value);
                    }

                    return values;
                }

                public DefaultValue(List<Value> values)
                {
                    Values = values;
                }

                //public DefaultValue(FBOC.ObjFlowData ObjFlowData)
                //{
                //    List<Value> values = new List<Value>();
                //    for (int i = 0; i < 8; i++)
                //    {
                //        Value value = new Value(0, "Test" + i);
                //        values.Add(value);
                //    }

                //    Values = values;
                //}
            }

            public ObjFlow() { }

            public ObjFlow(string ObjectID, string ObjectName, string Path, bool UseKCL, string ObjectType, Common Common, LOD_Setting LOD_Setting, Scale Scale, Name Name, DefaultValue defaultValue)
            {
                this.ObjectID = ObjectID;
                this.ObjectName = ObjectName;
                this.Path = Path;
                this.UseKCL = UseKCL;
                this.ObjectType = ObjectType;
                this.CommonData = Common;
                this.LODSetting = LOD_Setting;
                this.ScaleData = Scale;
                this.NameData = Name;
                this.DefaultValueData = defaultValue;
            }

            public ObjFlow(FBOC.ObjFlowData ObjFlowData)
            {
                ObjectID = BitConverter.ToString(ObjFlowData.ObjectID.Reverse().ToArray()).Replace("-", string.Empty);
                ObjectName = new string(ObjFlowData.ObjFlowName1).Replace("\0", "");
                Path = "";
                UseKCL = false;
                ObjectType = "NaN";
                CommonData = new Common(ObjFlowData);
                LODSetting = new LOD_Setting(ObjFlowData.LOD_Setting);
                ScaleData = new Scale(ObjFlowData.ObjFlowScale);
                NameData = new Name(ObjFlowData);
                DefaultValueData = new DefaultValue(DefaultValue.CreateDefaultValue());
            }
        }

        public ObjFlowData_XML() { }

        public ObjFlowData_XML(List<ObjFlow> objFlows)
        {
            ObjFlows = objFlows;
        }

        public ObjFlowData_XML(FBOC FBOC)
        {
            List<ObjFlow> ObjFlow_List = new List<ObjFlow>();
            for (int i = 0; i < FBOC.ObjFlowDataList.Count; i++)
            {
                ObjFlow_List.Add(new ObjFlow(FBOC.ObjFlowDataList[i]));
            }

            ObjFlows = ObjFlow_List;
        }
    }
}
