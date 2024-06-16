using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using KMPLibrary.Format.SectionData;
using KMPLibrary.KMPHelper;
using static MK7_3D_KMP_Editor.CustomPropertyGridClassConverter;

namespace MK7_3D_KMP_Editor.PropertyGridObject
{
    /// <summary>
    /// Object (PropertyGrid)
    /// </summary>
    public class KMPObject_PGS
    {
        public List<JBOGValue> JBOGValue_List = new List<JBOGValue>();
        public List<JBOGValue> JBOGValueList { get => JBOGValue_List; set => JBOGValue_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class JBOGValue
        {
            [ReadOnly(true)]
            public int ID { get; set; }

            [ReadOnly(true)]
            public string ObjectName { get; set; }
            public string ObjectID { get; set; }
            public string JBOG_UnkByte1 { get; set; }

            [TypeConverter(typeof(ExpandableObjectConverter))]
            public Position Positions { get; set; } = new Position();
            public class Position
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

                public Position()
                {
                    _X = 0;
                    _Y = 0;
                    _Z = 0;
                }

                public Position(float X, float Y, float Z)
                {
                    _X = X;
                    _Y = Y;
                    _Z = Z;
                }

                public Position(Vector3D vector3D)
                {
                    _X = (float)vector3D.X;
                    _Y = (float)vector3D.Y;
                    _Z = (float)vector3D.Z;
                }

                public Vector3D GetVector3D()
                {
                    double X = Convert.ToDouble(_X);
                    double Y = Convert.ToDouble(_Y);
                    double Z = Convert.ToDouble(_Z);

                    return new Vector3D(X, Y, Z);
                }

                public override string ToString()
                {
                    return "Position";
                }
            }

            [TypeConverter(typeof(ExpandableObjectConverter))]
            public Rotation Rotations { get; set; } = new Rotation();
            public class Rotation
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

                public Rotation()
                {
                    _X = 0;
                    _Y = 0;
                    _Z = 0;
                }

                public Rotation(float X, float Y, float Z)
                {
                    _X = X;
                    _Y = Y;
                    _Z = Z;
                }

                public Rotation(Vector3D vector3D)
                {
                    _X = HTK_3DES.RadianToAngle(vector3D.X);
                    _Y = HTK_3DES.RadianToAngle(vector3D.Y);
                    _Z = HTK_3DES.RadianToAngle(vector3D.Z);
                }

                public Vector3D GetVector3D()
                {
                    double X = Convert.ToDouble(_X);
                    double Y = Convert.ToDouble(_Y);
                    double Z = Convert.ToDouble(_Z);

                    return new Vector3D(X, Y, Z);
                }

                public override string ToString()
                {
                    return "Rotation";
                }
            }

            [TypeConverter(typeof(ExpandableObjectConverter))]
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
                    _X = 1;
                    _Y = 1;
                    _Z = 1;
                }

                public Scale(float X, float Y, float Z)
                {
                    _X = X;
                    _Y = Y;
                    _Z = Z;
                }

                public Scale(Vector3D vector3D)
                {
                    _X = (float)vector3D.X;
                    _Y = (float)vector3D.Y;
                    _Z = (float)vector3D.Z;
                }

                public Vector3D GetVector3D()
                {
                    double X = Convert.ToDouble(_X);
                    double Y = Convert.ToDouble(_Y);
                    double Z = Convert.ToDouble(_Z);

                    return new Vector3D(X, Y, Z);
                }

                public override string ToString()
                {
                    return "Scale";
                }
            }

            public ushort JBOG_ITOP_RouteIDIndex { get; set; }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public JBOG_SpecificSetting JOBJ_Specific_Setting { get; set; } = new JBOG_SpecificSetting();
            public class JBOG_SpecificSetting
            {
                public ushort Value0 { get; set; }
                public ushort Value1 { get; set; }
                public ushort Value2 { get; set; }
                public ushort Value3 { get; set; }
                public ushort Value4 { get; set; }
                public ushort Value5 { get; set; }
                public ushort Value6 { get; set; }
                public ushort Value7 { get; set; }

                public JBOG_SpecificSetting(ushort[] SpecificSettingArray)
                {
                    Value0 = SpecificSettingArray[0];
                    Value1 = SpecificSettingArray[1];
                    Value2 = SpecificSettingArray[2];
                    Value3 = SpecificSettingArray[3];
                    Value4 = SpecificSettingArray[4];
                    Value5 = SpecificSettingArray[5];
                    Value6 = SpecificSettingArray[6];
                    Value7 = SpecificSettingArray[7];
                }

                public ushort[] GetSpecificSettingArray()
                {
                    return new ushort[] { Value0, Value1, Value2, Value3, Value4, Value5, Value6, Value7 };
                }

                public JBOG_SpecificSetting()
                {
                    Value0 = 0;
                    Value1 = 0;
                    Value2 = 0;
                    Value3 = 0;
                    Value4 = 0;
                    Value5 = 0;
                    Value6 = 0;
                    Value7 = 0;
                }

                public JBOG_SpecificSetting(JBOG.JBOGValue.JBOG_SpecificSetting jBOG_SpecificSetting)
                {
                    Value0 = jBOG_SpecificSetting.Value0;
                    Value1 = jBOG_SpecificSetting.Value1;
                    Value2 = jBOG_SpecificSetting.Value2;
                    Value3 = jBOG_SpecificSetting.Value3;
                    Value4 = jBOG_SpecificSetting.Value4;
                    Value5 = jBOG_SpecificSetting.Value5;
                    Value6 = jBOG_SpecificSetting.Value6;
                    Value7 = jBOG_SpecificSetting.Value7;
                }

                public JBOG_SpecificSetting(KMPLibrary.XMLConvert.KMPData.SectionData.Object.Object_Value.SpecificSettings SpecificSettings)
                {
                    Value0 = SpecificSettings.Value0;
                    Value1 = SpecificSettings.Value1;
                    Value2 = SpecificSettings.Value2;
                    Value3 = SpecificSettings.Value3;
                    Value4 = SpecificSettings.Value4;
                    Value5 = SpecificSettings.Value5;
                    Value6 = SpecificSettings.Value6;
                    Value7 = SpecificSettings.Value7;
                }

                public override string ToString()
                {
                    return "Obj Params";
                }
            }
            public ushort JBOG_PresenceSetting { get; set; }
            public string JBOG_UnkByte2 { get; set; }
            public ushort JBOG_UnkByte3 { get; set; }

            public JBOGValue(string Name, string ObjID, Vector3D Pos, int InputID)
            {
                ID = InputID;
                ObjectName = Name;
                ObjectID = ObjID;
                JBOG_ITOP_RouteIDIndex = 65535;
                JBOG_PresenceSetting = 7;
                JBOG_UnkByte1 = "0000";
                JBOG_UnkByte2 = "FFFF";
                JBOG_UnkByte3 = 0;
                Positions = new Position(Pos);
                Scales = new Scale();
                Rotations = new Rotation();
                JOBJ_Specific_Setting = new JBOG_SpecificSetting();
            }

            public JBOGValue(JBOG.JBOGValue jBOGValue, List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDB, int InputID)
            {
                string Name = ObjFlowDB.Find(x => x.ObjectID == BitConverter.ToString(jBOGValue.ObjectID.Reverse().ToArray()).Replace("-", string.Empty)).ObjectName;

                ID = InputID;
                ObjectName = Name;
                ObjectID = BitConverter.ToString(jBOGValue.ObjectID.Reverse().ToArray()).Replace("-", string.Empty);
                JBOG_ITOP_RouteIDIndex = jBOGValue.JBOG_ITOP_RouteIDIndex;
                JBOG_PresenceSetting = jBOGValue.JBOG_PresenceSetting;
                JBOG_UnkByte1 = BitConverter.ToString(jBOGValue.JBOG_UnkByte1.Reverse().ToArray()).Replace("-", string.Empty);
                JBOG_UnkByte2 = BitConverter.ToString(jBOGValue.JBOG_UnkByte2.Reverse().ToArray()).Replace("-", string.Empty);
                JBOG_UnkByte3 = jBOGValue.JBOG_UnkByte3;
                Positions = new Position(jBOGValue.JBOG_Position);
                Scales = new Scale(jBOGValue.JBOG_Scale);
                Rotations = new Rotation(jBOGValue.JBOG_Rotation);
                JOBJ_Specific_Setting = new JBOG_SpecificSetting(jBOGValue.GOBJ_Specific_Setting);
            }

            public JBOGValue(KMPLibrary.XMLConvert.KMPData.SectionData.Object.Object_Value object_Value, int InputID)
            {
                List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDB_FindName = ObjFlowConverter.Xml.ReadObjFlowXml("ObjFlowData.xml");
                string Name = ObjFlowDB_FindName.Find(x => x.ObjectID == object_Value.ObjectID).ObjectName;

                ID = InputID;
                ObjectID = object_Value.ObjectID;
                JBOG_ITOP_RouteIDIndex = object_Value.RouteIDIndex;
                JBOG_PresenceSetting = object_Value.PresenceSetting;
                JBOG_UnkByte1 = object_Value.UnkByte1;
                JBOG_UnkByte2 = object_Value.UnkByte2;
                JBOG_UnkByte3 = object_Value.UnkByte3;
                Positions = new Position(object_Value.Position.ToVector3D());
                Scales = new Scale(object_Value.Scale.ToVector3D());
                Rotations = new Rotation(object_Value.Rotation.ToVector3D());
                JOBJ_Specific_Setting = new JBOG_SpecificSetting(object_Value.SpecificSetting);
            }

            public override string ToString()
            {
                return "Object " + ID + " [" + "OBJID : " + ObjectID + "]";
            }
        }

        public KMPObject_PGS(JBOG jBOG_Section, List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDB)
        {
            for (int i = 0; i < jBOG_Section.NumOfEntries; i++) JBOGValueList.Add(new JBOGValue(jBOG_Section.JBOGValue_List[i], ObjFlowDB, i));
        }

        public KMPObject_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.Object obj)
        {
            for (int i = 0; i < obj.Object_Values.Count; i++) JBOGValueList.Add(new JBOGValue(obj.Object_Values[i], i));
        }

        public KMPObject_PGS()
        {
            JBOGValueList = new List<JBOGValue>();
        }

        public JBOG ToJBOG(uint KMP_Version)
        {
            //JBOG JBOG = new JBOG
            //{
            //    JBOGHeader = "JBOG".ToCharArray(),
            //    NumOfEntries = Convert.ToUInt16(JBOGValueList.Count),
            //    AdditionalValue = 0,
            //    JBOGValue_List = null
            //};

            List<JBOG.JBOGValue> JBOG_Value_List = new List<JBOG.JBOGValue>();

            for (int Count = 0; Count < JBOGValueList.Count; Count++)
            {
                double RX = HTK_3DES.AngleToRadian(JBOGValueList[Count].Rotations.X);
                double RY = HTK_3DES.AngleToRadian(JBOGValueList[Count].Rotations.Y);
                double RZ = HTK_3DES.AngleToRadian(JBOGValueList[Count].Rotations.Z);

                JBOG.JBOGValue JBOG_Values = new JBOG.JBOGValue();

                JBOG_Values.ObjectID = Byte2StringConverter.OBJIDStrToByteArray(JBOGValueList[Count].ObjectID);
                JBOG_Values.JBOG_UnkByte1 = Byte2StringConverter.OBJIDStrToByteArray(JBOGValueList[Count].JBOG_UnkByte1);
                JBOG_Values.JBOG_Position = JBOGValueList[Count].Positions.GetVector3D();
                JBOG_Values.JBOG_Rotation = new Vector3D(RX, RY, RZ);
                JBOG_Values.JBOG_Scale = JBOGValueList[Count].Scales.GetVector3D();
                JBOG_Values.JBOG_ITOP_RouteIDIndex = JBOGValueList[Count].JBOG_ITOP_RouteIDIndex;
                JBOG_Values.GOBJ_Specific_Setting = new JBOG.JBOGValue.JBOG_SpecificSetting(JBOGValueList[Count].JOBJ_Specific_Setting.GetSpecificSettingArray());

                #region DELETE
                //JBOG_Values.GOBJ_Specific_Setting = new JBOG.JBOGValue.JBOG_SpecificSetting
                //{
                //    Value0 = JBOGValueList[Count].JOBJ_Specific_Setting.Value0,
                //    Value1 = JBOGValueList[Count].JOBJ_Specific_Setting.Value1,
                //    Value2 = JBOGValueList[Count].JOBJ_Specific_Setting.Value2,
                //    Value3 = JBOGValueList[Count].JOBJ_Specific_Setting.Value3,
                //    Value4 = JBOGValueList[Count].JOBJ_Specific_Setting.Value4,
                //    Value5 = JBOGValueList[Count].JOBJ_Specific_Setting.Value5,
                //    Value6 = JBOGValueList[Count].JOBJ_Specific_Setting.Value6,
                //    Value7 = JBOGValueList[Count].JOBJ_Specific_Setting.Value7
                //};
                #endregion

                JBOG_Values.JBOG_PresenceSetting = JBOGValueList[Count].JBOG_PresenceSetting;

                if (KMP_Version == 3100)
                {
                    JBOG_Values.JBOG_UnkByte2 = Byte2StringConverter.OBJIDStrToByteArray(JBOGValueList[Count].JBOG_UnkByte2);
                    JBOG_Values.JBOG_UnkByte3 = JBOGValueList[Count].JBOG_UnkByte3;
                }
                else if (KMP_Version == 3000)
                {
                    JBOG_Values.JBOG_UnkByte2 = new byte[] { 0x00, 0x00 };
                    JBOG_Values.JBOG_UnkByte3 = new ushort();
                }

                JBOG_Value_List.Add(JBOG_Values);
            }

            return new JBOG(JBOG_Value_List);

            //JBOG.JBOGValue_List = JBOG_Value_List;

            //return JBOG;
        }
    }
}
