using KMPLibrary.XMLConvert.ObjFlowData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MK7_3D_KMP_Editor.PropertyGridObject.CustomPropertyGridClassConverter;

namespace MK7_3D_KMP_Editor.PropertyGridObject.ObjFlow
{
    public class ObjFlow_PGS
    {
        public List<ObjFlow> ObjFlows_List = new List<ObjFlow>();
        public List<ObjFlow> ObjFlowsList { get => ObjFlows_List; set => ObjFlows_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class ObjFlow
        {
            public string ObjectID { get; set; }
            public string ObjectName { get; set; }
            public string Path { get; set; }
            public bool UseKCL { get; set; }
            public string ObjectType { get; set; }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Common CommonData { get; set; } = new Common();
            public class Common
            {
                public string ColType { get; set; }
                public string PathType { get; set; }
                public string ModelSetting { get; set; }
                public string Unknown1 { get; set; }

                public Common()
                {
                    ColType = "Null";
                    PathType = "Null";
                    ModelSetting = "Null";
                    Unknown1 = "Null";
                }

                public Common(ObjFlowData_XML.ObjFlow.Common Common)
                {
                    ColType = Common.ColType;
                    PathType = Common.PathType;
                    ModelSetting = Common.ModelSetting;
                    Unknown1 = Common.Unknown1;
                }

                public Common(string ColType, string PathType, string ModelSetting, string Unknown1)
                {
                    this.ColType = ColType;
                    this.PathType = PathType;
                    this.ModelSetting = ModelSetting;
                    this.Unknown1 = Unknown1;
                }

                public override string ToString()
                {
                    return "Common";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public LOD_Setting LODSetting { get; set; } = new LOD_Setting();
            public class LOD_Setting
            {
                public int LOD { get; set; }
                public int LODHighPoly { get; set; }
                public int LODLowPoly { get; set; }
                public int LODDefault { get; set; }

                public LOD_Setting()
                {
                    LOD = 0;
                    LODHighPoly = 0;
                    LODLowPoly = 0;
                    LODDefault = 0;
                }

                public LOD_Setting(ObjFlowData_XML.ObjFlow.LOD_Setting LODSetting)
                {
                    LOD = LODSetting.LOD;
                    LODHighPoly = LODSetting.LODHighPoly;
                    LODLowPoly = LODSetting.LODLowPoly;
                    LODDefault = LODSetting.LODDefault;
                }

                public LOD_Setting(int LOD, int HighPoly, int LowPoly, int Default)
                {
                    this.LOD = LOD;
                    LODHighPoly = HighPoly;
                    LODLowPoly = LowPoly;
                    LODDefault = Default;
                }

                public override string ToString()
                {
                    return "LODSetting";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Scale ScaleData { get; set; } = new Scale();
            public class Scale
            {
                private float _X;
                public float X
                {
                    get { return _X; }
                    set { _X = value; }
                }

                private float _Y;
                public float Y
                {
                    get { return _Y; }
                    set { _Y = value; }
                }

                private float _Z;
                public float Z
                {
                    get { return _Z; }
                    set { _Z = value; }
                }

                public Scale()
                {
                    _X = 0;
                    _Y = 0;
                    _Z = 0;
                }

                public Scale(ObjFlowData_XML.ObjFlow.Scale Scale)
                {
                    _X = Scale.X;
                    _Y = Scale.Y;
                    _Z = Scale.Z;
                }

                public Scale(float X, float Y, float Z)
                {
                    _X = X;
                    _Y = Y;
                    _Z = Z;
                }

                public override string ToString()
                {
                    return "Scale";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Name NameData { get; set; } = new Name("", "");
            public class Name
            {
                public string Main { get; set; }
                public string Sub { get; set; }

                public Name(ObjFlowData_XML.ObjFlow.Name Name)
                {
                    Main = Name.Main;
                    Sub = Name.Sub;
                }

                public Name(string MainName, string SubName)
                {
                    Main = MainName;
                    Sub = SubName;
                }

                public override string ToString()
                {
                    return "Name";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public DefaultValue DefaultValueData { get; set; } = new DefaultValue();
            public class DefaultValue
            {
                public List<Value> Values_List = new List<Value>();
                public List<Value> ValuesList { get => Values_List; set => Values_List = value; }
                [TypeConverter(typeof(CustomSortTypeConverter))]
                public class Value
                {
                    public int DefaultObjectValue { get; set; }
                    public string Description { get; set; }

                    public Value(int DefaultParamValue, string DescriptionText)
                    {
                        this.DefaultObjectValue = DefaultParamValue;
                        Description = DescriptionText;
                    }

                    public Value(ObjFlowData_XML.ObjFlow.DefaultValue.Value value)
                    {
                        DefaultObjectValue = value.DefaultObjectValue;
                        Description = value.Description;
                    }

                    public override string ToString()
                    {
                        return "Value";
                    }
                }

                public DefaultValue()
                {
                    List<Value> values = new List<Value>();
                    values.Add(new Value(0, "Text"));
                    values.Add(new Value(0, "Text"));
                    values.Add(new Value(0, "Text"));
                    values.Add(new Value(0, "Text"));
                    values.Add(new Value(0, "Text"));
                    values.Add(new Value(0, "Text"));
                    values.Add(new Value(0, "Text"));
                    values.Add(new Value(0, "Text"));

                    Values_List = values;
                }

                public DefaultValue(List<Value> ValueList)
                {
                    Values_List = ValueList;
                }

                public DefaultValue(ObjFlowData_XML.ObjFlow.DefaultValue defaultValue)
                {
                    foreach (var item in defaultValue.Values)
                    {
                        Values_List.Add(new Value(item));
                    }
                }

                public override string ToString()
                {
                    return "DefaultValue";
                }
            }

            public ObjFlow()
            {
                ObjectID = "0000";
                ObjectName = "TestObject";
                Path = "";
                UseKCL = false;
                ObjectType = "Undefined Type";
                CommonData = new Common("", "", "", "");
                LODSetting = new LOD_Setting(0, 0, 0, 0);
                ScaleData = new Scale(0, 0, 0);
                NameData = new Name("Main", "Sub");

                DefaultValueData = new DefaultValue();
            }

            public ObjFlow(string ObjectID, string ObjectName, string Path, bool UseKCL, string ObjectType, Common CommonData, LOD_Setting LODSetting, Scale ScaleData, Name NameData)
            {
                this.ObjectID = ObjectID;
                this.ObjectName = ObjectName;
                this.Path = Path;
                this.UseKCL = UseKCL;
                this.ObjectType = ObjectType;
                this.CommonData = CommonData;
                this.LODSetting = LODSetting;
                this.ScaleData = ScaleData;
                this.NameData = NameData;
            }

            public ObjFlow(ObjFlowData_XML.ObjFlow ObjFlow)
            {
                ObjectID = ObjFlow.ObjectID;
                ObjectName = ObjFlow.ObjectName;
                Path = ObjFlow.Path;
                UseKCL = ObjFlow.UseKCL;
                ObjectType = ObjFlow.ObjectType;
                CommonData = new Common(ObjFlow.CommonData);
                LODSetting = new LOD_Setting(ObjFlow.LODSetting);
                ScaleData = new Scale(ObjFlow.ScaleData);
                NameData = new Name(ObjFlow.NameData);

                DefaultValueData = new DefaultValue(ObjFlow.DefaultValueData);
            }

            public override string ToString()
            {
                return ObjectName + " [" + ObjectID + "]";
            }
        }

        public ObjFlow_PGS(ObjFlowData_XML ObjFlowData_XML)
        {
            for (int i = 0; i < ObjFlowData_XML.ObjFlows.Count; i++)
            {
                ObjFlows_List.Add(new ObjFlow(ObjFlowData_XML.ObjFlows[i]));
            }
        }
    }
}
