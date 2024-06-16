using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MK7_3D_KMP_Editor.CustomPropertyGridClassConverter;

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
            public Common Commons { get; set; } = new Common();
            public class Common
            {
                public string ColType { get; set; }
                public string PathType { get; set; }
                public string ModelSetting { get; set; }
                public string Unknown1 { get; set; }

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
                public int LODHPoly { get; set; }
                public int LODLPoly { get; set; }
                public int LODDef { get; set; }

                public override string ToString()
                {
                    return "LODSetting";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Scale Scales { get; set; } = new Scale();
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

                public override string ToString()
                {
                    return "Scale";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Name Names { get; set; } = new Name();
            public class Name
            {
                public string Main { get; set; }
                public string Sub { get; set; }

                public override string ToString()
                {
                    return "Name";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public Default_Values DefaultValues { get; set; } = new Default_Values();
            public class Default_Values
            {
                public List<Value> Values_List = new List<Value>();
                public List<Value> ValuesList { get => Values_List; set => Values_List = value; }
                [TypeConverter(typeof(CustomSortTypeConverter))]
                public class Value
                {
                    public int DefaultObjectValue { get; set; }
                    public string Description { get; set; }

                    public override string ToString()
                    {
                        return "Value";
                    }
                }

                public override string ToString()
                {
                    return "DefaultValue";
                }
            }

            public override string ToString()
            {
                return ObjectName + " [" + ObjectID + "]";
            }
        }
    }
}
