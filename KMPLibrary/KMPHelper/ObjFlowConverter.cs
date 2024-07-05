using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FBOCLibrary;

namespace KMPLibrary.KMPHelper
{
    public class ObjFlowConverter
    {
        public class Xml
        {
            public static Dictionary<string[], string> ObjFlowMdlPathDictionary(List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDataXml, string Path)
            {
                //指定したディレクトリの中にあるファイルパスを全て取得
                string[] PathAry = System.IO.Directory.GetFiles(Path, "*.obj", System.IO.SearchOption.AllDirectories);

                Dictionary<string[], string> ObjFlowDicts = new Dictionary<string[], string>();

                foreach (var ObjFlowValue in ObjFlowDataXml.Select((item, index) => new { item, index }))
                {
                    //Search the path of the corresponding model from PathAry(string[])
                    if (PathAry.Contains(ObjFlowValue.item.Path))
                    {
                        //Get ObjectID
                        string ObjectID = ObjFlowDataXml.Find(x => x.Path == ObjFlowValue.item.Path).ObjectID ?? "";
                        ObjFlowDicts.Add(new string[] { ObjFlowValue.item.ObjectName, ObjectID }, ObjFlowValue.item.Path);
                    }
                }

                return ObjFlowDicts;
            }
        }

        public class ConvertTo
        {
            /// <summary>
            /// Convert to FBOC
            /// </summary>
            /// <param name="ObjFlowDataXml_List"></param>
            /// <returns></returns>
            public static FBOC ToFBOC(List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDataXml_List)
            {
                List<FBOC.ObjFlowData> ObjFlowDataList = new List<FBOC.ObjFlowData>();
                for (int Count = 0; Count < ObjFlowDataXml_List.Count; Count++)
                {
                    FBOC.ObjFlowData ObjFlowData = new FBOC.ObjFlowData
                    {
                        ObjectID = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].ObjectID).Reverse().ToArray(),
                        CollisionType = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].CommonData.ColType).Reverse().ToArray(),
                        PathType = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].CommonData.PathType).Reverse().ToArray(),
                        LOD_Setting = new FBOC.ObjFlowData.LODSetting
                        {
                            LOD = (short)ObjFlowDataXml_List[Count].LODSetting.LOD,
                            LODHighPoly = (short)ObjFlowDataXml_List[Count].LODSetting.LODHighPoly,
                            LODLowPoly = (short)ObjFlowDataXml_List[Count].LODSetting.LODLowPoly,
                            LODDefault = (short)ObjFlowDataXml_List[Count].LODSetting.LODDefault,
                        },
                        ModelSetting = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].CommonData.ModelSetting).Reverse().ToArray(),
                        ObjFlowScale = new FBOC.ObjFlowData.ObjFlowScaleSetting
                        {
                            X = (short)ObjFlowDataXml_List[Count].ScaleData.X,
                            Y = (short)ObjFlowDataXml_List[Count].ScaleData.Y,
                            Z = (short)ObjFlowDataXml_List[Count].ScaleData.Z,
                        },
                        Unknown1 = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].CommonData.Unknown1).Reverse().ToArray(),
                        ObjFlowName1 = Misc.ZEROPaddingedCharArray(ObjFlowDataXml_List[Count].NameData.Main.ToCharArray()),
                        ObjFlowName2 = Misc.ZEROPaddingedCharArray(ObjFlowDataXml_List[Count].NameData.Sub.ToCharArray())
                    };

                    ObjFlowDataList.Add(ObjFlowData);
                }

                return new FBOC(ObjFlowDataList);
            }

            public static List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ToObjFlowDB_XML(FBOC FBOCData)
            {
                XMLConvert.ObjFlowData.ObjFlowData_XML objFlowData_XML = new XMLConvert.ObjFlowData.ObjFlowData_XML(FBOCData);
                return objFlowData_XML.ObjFlows;
            }
        }
    }
}
