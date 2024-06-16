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
            public static Dictionary<string[], string> ObjFlowMdlPathDictionary(List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDataXml, string Path)
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

            //Create Xml
            public static void CreateXml(List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowVal_List, string KMPObjectFolderPath, string DefaultModelPath, string XmlPath)
            {
                string[] PathAry = System.IO.Directory.GetFiles(KMPObjectFolderPath, "*.obj", System.IO.SearchOption.AllDirectories);

                KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML kMPObjFlowDataXml = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML
                {
                    ObjFlows = null
                };

                List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowList = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow>();

                foreach (var ObjFlowValue in ObjFlowVal_List.Select((item, index) => new { item, index }))
                {
                    string MDLPath = "";

                    //Search the path of the corresponding model from PathAry(string[])
                    if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.Names.Main + ".obj"))
                    {
                        MDLPath = KMPObjectFolderPath + "\\" + ObjFlowValue.item.Names.Main + ".obj";
                    }
                    if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.Names.Main + ".obj") == false)
                    {
                        MDLPath = DefaultModelPath;
                    }

                    KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow objFlow = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow
                    {
                        ObjectID = ObjFlowValue.item.ObjectID,
                        ObjectName = ObjFlowValue.item.ObjectName,
                        Path = MDLPath,
                        UseKCL = false,
                        ObjectType = "Unknown",
                        Commons = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Common
                        {
                            ColType = ObjFlowValue.item.Commons.ColType,
                            PathType = ObjFlowValue.item.Commons.PathType,
                            ModelSetting = ObjFlowValue.item.Commons.ModelSetting,
                            Unknown1 = ObjFlowValue.item.Commons.Unknown1
                        },
                        LODSetting = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
                        {
                            LOD = ObjFlowValue.item.LODSetting.LOD,
                            LODHPoly = ObjFlowValue.item.LODSetting.LODHPoly,
                            LODLPoly = ObjFlowValue.item.LODSetting.LODLPoly,
                            LODDef = ObjFlowValue.item.LODSetting.LODDef
                        },
                        Scales = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
                        {
                            X = ObjFlowValue.item.Scales.X,
                            Y = ObjFlowValue.item.Scales.Y,
                            Z = ObjFlowValue.item.Scales.Z
                        },
                        Names = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Name
                        {
                            Main = ObjFlowValue.item.Names.Main,
                            Sub = ObjFlowValue.item.Names.Sub
                        },
                        DefaultValues = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values
                        {
                            Values = null
                        }
                    };

                    #region Values
                    List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value> ValuesList = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value>();

                    for (int i = 0; i < 8; i++)
                    {
                        KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value value = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Test " + i
                        };

                        ValuesList.Add(value);
                    }

                    objFlow.DefaultValues.Values = ValuesList;
                    #endregion

                    ObjFlowList.Add(objFlow);
                }

                kMPObjFlowDataXml.ObjFlows = ObjFlowList;

                //Delete Namespaces
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);

                System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML));
                System.IO.StreamWriter sw = new StreamWriter(XmlPath, false, new System.Text.UTF8Encoding(false));
                serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                sw.Close();
            }

            //Read ObjFlowData.xml
            public static List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ReadObjFlowXml(string Path)
            {
                System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
                System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML));
                KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML ObjFlowXml = (KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML)s1.Deserialize(fs1);

                List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowXml_List = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow>();

                foreach (var ObjFlowData in ObjFlowXml.ObjFlows)
                {
                    KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow objFlow = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow
                    {
                        ObjectID = ObjFlowData.ObjectID,
                        ObjectName = ObjFlowData.ObjectName,
                        Path = ObjFlowData.Path,
                        UseKCL = ObjFlowData.UseKCL,
                        ObjectType = ObjFlowData.ObjectType,
                        Commons = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Common
                        {
                            ColType = ObjFlowData.Commons.ColType,
                            PathType = ObjFlowData.Commons.PathType,
                            ModelSetting = ObjFlowData.Commons.ModelSetting,
                            Unknown1 = ObjFlowData.Commons.Unknown1
                        },
                        LODSetting = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
                        {
                            LOD = ObjFlowData.LODSetting.LOD,
                            LODHPoly = ObjFlowData.LODSetting.LODHPoly,
                            LODLPoly = ObjFlowData.LODSetting.LODLPoly,
                            LODDef = ObjFlowData.LODSetting.LODDef
                        },
                        Scales = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
                        {
                            X = ObjFlowData.Scales.X,
                            Y = ObjFlowData.Scales.Y,
                            Z = ObjFlowData.Scales.Z
                        },
                        Names = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Name
                        {
                            Main = ObjFlowData.Names.Main,
                            Sub = ObjFlowData.Names.Sub
                        },
                        DefaultValues = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values
                        {
                            Values = null
                        }
                    };

                    List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value> valueList = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value>();

                    foreach (var ObjFlowDataValue in ObjFlowData.DefaultValues.Values)
                    {
                        KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value value = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
                            Description = ObjFlowDataValue.Description
                        };

                        valueList.Add(value);
                    }

                    objFlow.DefaultValues.Values = valueList;

                    ObjFlowXml_List.Add(objFlow);
                }

                fs1.Close();
                fs1.Dispose();

                return ObjFlowXml_List;
            }

            //Write ObjFlowData.xml
            public static void WriteObjFlowXml(List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDBList, string Path)
            {
                KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML kMPObjFlowDataXml = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML
                {
                    ObjFlows = null
                };

                List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowList = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow>();

                foreach (var ObjFlowValue in ObjFlowDBList.Select((item, index) => new { item, index }))
                {
                    KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow objFlow = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow
                    {
                        ObjectID = ObjFlowValue.item.ObjectID,
                        ObjectName = ObjFlowValue.item.ObjectName,
                        Path = ObjFlowValue.item.Path,
                        UseKCL = ObjFlowValue.item.UseKCL,
                        ObjectType = ObjFlowValue.item.ObjectType,
                        Commons = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Common
                        {
                            ColType = ObjFlowValue.item.Commons.ColType,
                            PathType = ObjFlowValue.item.Commons.PathType,
                            ModelSetting = ObjFlowValue.item.Commons.ModelSetting,
                            Unknown1 = ObjFlowValue.item.Commons.Unknown1
                        },
                        LODSetting = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
                        {
                            LOD = ObjFlowValue.item.LODSetting.LOD,
                            LODHPoly = ObjFlowValue.item.LODSetting.LODHPoly,
                            LODLPoly = ObjFlowValue.item.LODSetting.LODLPoly,
                            LODDef = ObjFlowValue.item.LODSetting.LODDef
                        },
                        Scales = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
                        {
                            X = Convert.ToInt32(ObjFlowValue.item.Scales.X),
                            Y = Convert.ToInt32(ObjFlowValue.item.Scales.Y),
                            Z = Convert.ToInt32(ObjFlowValue.item.Scales.Z)
                        },
                        Names = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Name
                        {
                            Main = ObjFlowValue.item.Names.Main,
                            Sub = ObjFlowValue.item.Names.Sub
                        },
                        DefaultValues = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values
                        {
                            Values = null
                        }
                    };

                    #region Values
                    List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value> ValuesList = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value>();

                    foreach (var i in ObjFlowValue.item.DefaultValues.Values)
                    {
                        KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value value = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = i.DefaultObjectValue,
                            Description = i.Description
                        };

                        ValuesList.Add(value);
                    }

                    objFlow.DefaultValues.Values = ValuesList;
                    #endregion

                    ObjFlowList.Add(objFlow);
                }

                kMPObjFlowDataXml.ObjFlows = ObjFlowList;

                //Delete Namespaces
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);

                System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML));
                System.IO.StreamWriter sw = new StreamWriter(Path, false, new System.Text.UTF8Encoding(false));
                serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                sw.Close();
            }
        }

        public class ConvertTo
        {
            public static FBOC ToFBOC(List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDataXml_List)
            {
                List<FBOC.ObjFlowData> ObjFlowDataList = new List<FBOC.ObjFlowData>();
                for (int Count = 0; Count < ObjFlowDataXml_List.Count; Count++)
                {
                    FBOC.ObjFlowData ObjFlowData = new FBOC.ObjFlowData
                    {
                        ObjectID = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].ObjectID).Reverse().ToArray(),
                        CollisionType = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].Commons.ColType).Reverse().ToArray(),
                        PathType = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].Commons.PathType).Reverse().ToArray(),
                        LOD_Setting = new FBOC.ObjFlowData.LODSetting
                        {
                            LOD = (short)ObjFlowDataXml_List[Count].LODSetting.LOD,
                            LODHighPoly = (short)ObjFlowDataXml_List[Count].LODSetting.LODHPoly,
                            LODLowPoly = (short)ObjFlowDataXml_List[Count].LODSetting.LODLPoly,
                            LODDefault = (short)ObjFlowDataXml_List[Count].LODSetting.LODDef,
                        },
                        ModelSetting = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].Commons.ModelSetting).Reverse().ToArray(),
                        ObjFlowScale = new FBOC.ObjFlowData.ObjFlowScaleSetting
                        {
                            X = (short)ObjFlowDataXml_List[Count].Scales.X,
                            Y = (short)ObjFlowDataXml_List[Count].Scales.Y,
                            Z = (short)ObjFlowDataXml_List[Count].Scales.Z,
                        },
                        Unknown1 = Byte2StringConverter.ToByteArray(ObjFlowDataXml_List[Count].Commons.Unknown1).Reverse().ToArray(),
                        ObjFlowName1 = Misc.ZEROPaddingedCharArray(ObjFlowDataXml_List[Count].Names.Main.ToCharArray()),
                        ObjFlowName2 = Misc.ZEROPaddingedCharArray(ObjFlowDataXml_List[Count].Names.Sub.ToCharArray())
                    };

                    ObjFlowDataList.Add(ObjFlowData);
                }

                return new FBOC(ObjFlowDataList);
            }

            public static List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ToObjFlowDB(FBOCLibrary.FBOC FBOCData)
            {
                List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDataXmlList = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow>();

                for (int Count = 0; Count < FBOCData.ObjFlowDataList.Count; Count++)
                {
                    KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow ObjFlowDatabase = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow
                    {
                        ObjectID = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].ObjectID.Reverse().ToArray()).Replace("-", string.Empty),
                        ObjectName = new string(FBOCData.ObjFlowDataList[Count].ObjFlowName1).Replace("\0", ""),
                        Path = "",
                        UseKCL = false,
                        ObjectType = "NaN",
                        Commons = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Common
                        {
                            ColType = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].CollisionType.Reverse().ToArray()).Replace("-", string.Empty),
                            PathType = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].PathType.Reverse().ToArray()).Replace("-", string.Empty),
                            ModelSetting = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].ModelSetting.Reverse().ToArray()).Replace("-", string.Empty),
                            Unknown1 = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].Unknown1.Reverse().ToArray(), 0).Replace("-", string.Empty)
                        },
                        LODSetting = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
                        {
                            LOD = FBOCData.ObjFlowDataList[Count].LOD_Setting.LOD,
                            LODHPoly = FBOCData.ObjFlowDataList[Count].LOD_Setting.LODHighPoly,
                            LODLPoly = FBOCData.ObjFlowDataList[Count].LOD_Setting.LODLowPoly,
                            LODDef = FBOCData.ObjFlowDataList[Count].LOD_Setting.LODDefault
                        },
                        Scales = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
                        {
                            X = FBOCData.ObjFlowDataList[Count].ObjFlowScale.X,
                            Y = FBOCData.ObjFlowDataList[Count].ObjFlowScale.Y,
                            Z = FBOCData.ObjFlowDataList[Count].ObjFlowScale.Z
                        },
                        Names = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Name
                        {
                            Main = new string(FBOCData.ObjFlowDataList[Count].ObjFlowName1).Replace("\0", ""),
                            Sub = new string(FBOCData.ObjFlowDataList[Count].ObjFlowName2).Replace("\0", "")
                        },
                        DefaultValues = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values
                        {
                            Values = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value>()
                        }
                    };

                    List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value> valueList = new List<KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value>();

                    foreach (var ObjFlowDataValue in ObjFlowDatabase.DefaultValues.Values)
                    {
                        KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value value = new KMPLibrary.XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Default_Values.Value
                        {
                            DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
                            Description = ObjFlowDataValue.Description
                        };

                        valueList.Add(value);
                    }

                    ObjFlowDatabase.DefaultValues.Values = valueList;

                    ObjFlowDataXmlList.Add(ObjFlowDatabase);
                }

                return ObjFlowDataXmlList;
            }
        }
    }
}
