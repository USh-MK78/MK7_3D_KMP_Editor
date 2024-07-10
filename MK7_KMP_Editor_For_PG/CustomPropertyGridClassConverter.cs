//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MK7_3D_KMP_Editor
//{
//    public class CustomPropertyGridClassConverter
//    {
//        public class CustomSortTypeConverter : TypeConverter
//        {
//            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
//            {
//                PropertyDescriptorCollection PDC = TypeDescriptor.GetProperties(value, attributes);

//                Type type = value.GetType();

//                List<string> list = type.GetProperties().Select(x => x.Name).ToList();

//                return PDC.Sort(list.ToArray());
//            }

//            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
//            {
//                return true;
//            }
//        }

//        public class CustomExpandableObjectSortTypeConverter : ExpandableObjectConverter
//        {
//            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
//            {
//                PropertyDescriptorCollection PDC = TypeDescriptor.GetProperties(value, attributes);

//                Type type = value.GetType();

//                List<string> list = type.GetProperties().Select(x => x.Name).ToList();

//                return PDC.Sort(list.ToArray());
//            }

//            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
//            {
//                return true;
//            }
//        }
//    }
//}
