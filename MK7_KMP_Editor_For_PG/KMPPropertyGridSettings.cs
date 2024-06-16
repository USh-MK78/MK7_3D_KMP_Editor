//using System;
//using System.Reflection;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing.Design;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Forms.Design;
//using System.Windows.Media.Media3D;
//using System.Numerics;
//using KMPLibrary.Format.SectionData;

//namespace MK7_KMP_Editor_For_PG_
//{
//    #region DELETE
//    //public class KMPPropertyGridSettings
//    //{
//    //    //public TPTK_Section TPTKSection { get; set; }
//    //    //public HPNE_TPNE_Section HPNE_TPNESection { get; set; }
//    //    //public HPTI_TPTI_Section HPTI_TPTISection { get; set; }
//    //    //public HPKC_TPKC_Section HPKC_TPKCSection { get; set; }
//    //    //public JBOG_Section JBOGSection { get; set; }
//    //    //public ITOP_Section ITOPSection { get; set; }
//    //    //public AERA_Section AERASection { get; set; }
//    //    //public EMAC_Section EMACSection { get; set; }
//    //    //public TPGJ_Section TPGJSection { get; set; }

//    //    //TPNC = null
//    //    //TPSM = null

//    //    //public IGTS_Section IGTSSection { get; set; }

//    //    //SROC = null

//    //    //public HPLG_TPLG_Section HPLG_TPLGSection { get; set; }

//    //}

//    //public class PropertyGridClassToBinaryConverter
//    //{
//    //    //public static TPTK_Section ToTPTK_Section(KMPPropertyGridSettings.TPTK_Section TPTK_Section)
//    //    //{

//    //    //}

//    //    //public class HPNE_TPNESection
//    //    //{
//    //    //    public static HPNE_TPNEData ToHPNE_TPNE_Section(KMPPropertyGridSettings.HPNE_TPNE_Section HPNE_TPNE_Section)
//    //    //    {

//    //    //    }
//    //    //}

//    //    //public class HPTI_TPTISection
//    //    //{
//    //    //    public static HPTI_TPTIData ToHPTI_TPTI_Section(KMPPropertyGridSettings.HPTI_TPTI_Section HPTI_TPTI_Section)
//    //    //    {

//    //    //    }
//    //    //}

//    //    //public class HPKC_TPKCSection
//    //    //{

//    //    //    public static HPKC_TPKCData ToHPKC_TPKC_Section(KMPPropertyGridSettings.HPKC_TPKC_Section HPKC_TPKC_Section)
//    //    //    {

//    //    //    }
//    //    //}

//    //    //public static KMPs.KMPFormat.KMPSection.JBOG_Section ToJBOG_Section(KMPPropertyGridSettings.JBOG_Section JBOG_Section, uint KMP_Version)
//    //    //{

//    //    //}

//    //    //public static KMPs.KMPFormat.KMPSection.ITOP_Section ToITOP_Section(KMPPropertyGridSettings.ITOP_Section ITOP_Section)
//    //    //{

//    //    //}

//    //    //public static KMPs.KMPFormat.KMPSection.AERA_Section ToAERA_section(KMPPropertyGridSettings.AERA_Section AERA_Section)
//    //    //{

//    //    //}

//    //    //public static KMPs.KMPFormat.KMPSection.EMAC_Section ToEMAC_Section(KMPPropertyGridSettings.EMAC_Section EMAC_Section)
//    //    //{

//    //    //}

//    //    //public static KMPs.KMPFormat.KMPSection.TPGJ_Section ToTPGJ_Section(KMPPropertyGridSettings.TPGJ_Section TPGJ_Section)
//    //    //{

//    //    //}





//    //    //public static KMPs.KMPFormat.KMPSection.IGTS_Section ToIGTS_Section(KMPPropertyGridSettings.IGTS_Section IGTS_Section)
//    //    //{

//    //    //}


//    //    //public class HPLG_TPLGSection
//    //    //{


//    //    //    public static HPLG_TPLGData ToHPLG_TPLG_Section(KMPPropertyGridSettings.HPLG_TPLG_Section HPLG_TPLG_Section)
//    //    //    {

//    //    //    }
//    //    //}
//    //}
//    #endregion

//   // public class ObjFlowXmlPropertyGridSetting
//   // {
//   ////     public List<ObjFlow> ObjFlows_List = new List<ObjFlow>();
//   ////     public List<ObjFlow> ObjFlowsList { get => ObjFlows_List; set => ObjFlows_List = value; }
//   ////     [TypeConverter(typeof(CustomSortTypeConverter))]
//   ////     public class ObjFlow
//   ////     {
//			////public string ObjectID { get; set; }
//			////public string ObjectName { get; set; }
//			////public string Path { get; set; }
//   ////         public bool UseKCL { get; set; }
//   ////         public string ObjectType { get; set; }

//   ////         [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
//   ////         public Common Commons { get; set; } = new Common();
//   ////         public class Common
//   ////         {
//   ////             public string ColType { get; set; }
//   ////             public string PathType { get; set; }
//   ////             public string ModelSetting { get; set; }
//   ////             public string Unknown1 { get; set; }

//   ////             public override string ToString()
//   ////             {
//   ////                 return "Common";
//   ////             }
//   ////         }

//   ////         [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
//   ////         public LOD_Setting LODSetting { get; set; } = new LOD_Setting();
//   ////         public class LOD_Setting
//   ////         {
//   ////             public int LOD { get; set; }
//   ////             public int LODHPoly { get; set; }
//   ////             public int LODLPoly { get; set; }
//   ////             public int LODDef { get; set; }

//   ////             public override string ToString()
//   ////             {
//   ////                 return "LODSetting";
//   ////             }
//   ////         }

//   ////         [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
//   ////         public Scale Scales { get; set; } = new Scale();
//   ////         public class Scale
//   ////         {
//   ////             private float _X;
//   ////             public float X
//   ////             {
//   ////                 get { return _X; }
//   ////                 set { _X = value; }
//   ////             }

//   ////             private float _Y;
//   ////             public float Y
//   ////             {
//   ////                 get { return _Y; }
//   ////                 set { _Y = value; }
//   ////             }

//   ////             private float _Z;
//   ////             public float Z
//   ////             {
//   ////                 get { return _Z; }
//   ////                 set { _Z = value; }
//   ////             }

//   ////             public Scale()
//   ////             {
//   ////                 _X = 0;
//   ////                 _Y = 0;
//   ////                 _Z = 0;
//   ////             }

//   ////             public override string ToString()
//   ////             {
//   ////                 return "Scale";
//   ////             }
//   ////         }

//   ////         [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
//   ////         public Name Names { get; set; } = new Name();
//   ////         public class Name
//   ////         {
//   ////             public string Main { get; set; }
//   ////             public string Sub { get; set; }

//   ////             public override string ToString()
//   ////             {
//   ////                 return "Name";
//   ////             }
//   ////         }

//   ////         [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
//   ////         public Default_Values DefaultValues { get; set; } = new Default_Values();
//   ////         public class Default_Values
//   ////         {
//   ////             public List<Value> Values_List = new List<Value>();
//   ////             public List<Value> ValuesList { get => Values_List; set => Values_List = value; }
//   ////             [TypeConverter(typeof(CustomSortTypeConverter))]
//   ////             public class Value
//   ////             {
//   ////                 public int DefaultObjectValue { get; set; }
//   ////                 public string Description { get; set; }

//   ////                 public override string ToString()
//   ////                 {
//   ////                     return "Value";
//   ////                 }
//   ////             }

//   ////             public override string ToString()
//   ////             {
//   ////                 return "DefaultValue";
//   ////             }
//   ////         }

//   ////         public override string ToString()
//   ////         {
//   ////             return ObjectName + " [" + ObjectID + "]";
//   ////         }
//   ////     }
//   // }

//    //public class CustomSortTypeConverter : TypeConverter
//    //{
//    //    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
//    //    {
//    //        PropertyDescriptorCollection PDC = TypeDescriptor.GetProperties(value, attributes);

//    //        Type type = value.GetType();

//    //        List<string> list = type.GetProperties().Select(x => x.Name).ToList();

//    //        return PDC.Sort(list.ToArray());
//    //    }

//    //    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
//    //    {
//    //        return true;
//    //    }
//    //}

//    //public class CustomExpandableObjectSortTypeConverter : ExpandableObjectConverter
//    //{
//    //    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
//    //    {
//    //        PropertyDescriptorCollection PDC = TypeDescriptor.GetProperties(value, attributes);

//    //        Type type = value.GetType();

//    //        List<string> list = type.GetProperties().Select(x => x.Name).ToList();

//    //        return PDC.Sort(list.ToArray());
//    //    }

//    //    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
//    //    {
//    //        return true;
//    //    }
//    //}
//}
