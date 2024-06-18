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

            /// <summary>
            /// Create Xml
            /// </summary>
            /// <param name="ObjFlowVal_List"></param>
            /// <param name="KMPObjectFolderPath"></param>
            /// <param name="DefaultModelPath"></param>
            /// <param name="XmlPath"></param>
            public static void CreateXml(List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowVal_List, string KMPObjectFolderPath, string DefaultModelPath, string XmlPath)
            {
                string[] PathAry = System.IO.Directory.GetFiles(KMPObjectFolderPath, "*.obj", System.IO.SearchOption.AllDirectories);

                foreach (var ObjFlowValue in ObjFlowVal_List.Select((item, index) => new { item, index }))
                {
                    string MDLPath = "";

                    //Search the path of the corresponding model from PathAry(string[])
                    if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.NameData.Main + ".obj"))
                    {
                        MDLPath = KMPObjectFolderPath + "\\" + ObjFlowValue.item.NameData.Main + ".obj";
                    }
                    else if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.NameData.Main + ".obj") == false)
                    {
                        MDLPath = DefaultModelPath;
                    }

                    List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value> ValuesList = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

                    for (int i = 0; i < 8; i++)
                    {
                        XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value
                        {
                            DefaultObjectValue = 0,
                            Description = "Test " + i
                        };

                        ValuesList.Add(value);
                    }

                    ObjFlowValue.item.Path = MDLPath;
                    ObjFlowValue.item.ObjectType = "Unknown";
                    ObjFlowValue.item.DefaultValueData.Values = ValuesList;
                }

                XMLConvert.ObjFlowData.ObjFlowData_XML kMPObjFlowDataXml = new XMLConvert.ObjFlowData.ObjFlowData_XML { ObjFlows = ObjFlowVal_List };

                //Delete Namespaces
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);

                System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(XMLConvert.ObjFlowData.ObjFlowData_XML));
                System.IO.StreamWriter sw = new StreamWriter(XmlPath, false, new System.Text.UTF8Encoding(false));
                serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                sw.Close();
            }

            #region CreateXML [DELETE]
            ////Create Xml
            //public static void CreateXml(List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowVal_List, string KMPObjectFolderPath, string DefaultModelPath, string XmlPath)
            //{
            //    string[] PathAry = System.IO.Directory.GetFiles(KMPObjectFolderPath, "*.obj", System.IO.SearchOption.AllDirectories);

            //    XMLConvert.ObjFlowData.ObjFlowData_XML kMPObjFlowDataXml = new XMLConvert.ObjFlowData.ObjFlowData_XML
            //    {
            //        ObjFlows = null
            //    };

            //    List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowList = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow>();

            //    foreach (var ObjFlowValue in ObjFlowVal_List.Select((item, index) => new { item, index }))
            //    {
            //        string MDLPath = "";

            //        //Search the path of the corresponding model from PathAry(string[])
            //        if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.NameData.Main + ".obj"))
            //        {
            //            MDLPath = KMPObjectFolderPath + "\\" + ObjFlowValue.item.NameData.Main + ".obj";
            //        }
            //        if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.NameData.Main + ".obj") == false)
            //        {
            //            MDLPath = DefaultModelPath;
            //        }

            //        XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow objFlow = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow
            //        {
            //            ObjectID = ObjFlowValue.item.ObjectID,
            //            ObjectName = ObjFlowValue.item.ObjectName,
            //            Path = MDLPath,
            //            UseKCL = false,
            //            ObjectType = "Unknown",
            //            CommonData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Common
            //            {
            //                ColType = ObjFlowValue.item.CommonData.ColType,
            //                PathType = ObjFlowValue.item.CommonData.PathType,
            //                ModelSetting = ObjFlowValue.item.CommonData.ModelSetting,
            //                Unknown1 = ObjFlowValue.item.CommonData.Unknown1
            //            },
            //            LODSetting = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
            //            {
            //                LOD = ObjFlowValue.item.LODSetting.LOD,
            //                LODHighPoly = ObjFlowValue.item.LODSetting.LODHighPoly,
            //                LODLowPoly = ObjFlowValue.item.LODSetting.LODLowPoly,
            //                LODDefault = ObjFlowValue.item.LODSetting.LODDefault
            //            },
            //            ScaleData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
            //            {
            //                X = ObjFlowValue.item.ScaleData.X,
            //                Y = ObjFlowValue.item.ScaleData.Y,
            //                Z = ObjFlowValue.item.ScaleData.Z
            //            },
            //            NameData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Name
            //            {
            //                Main = ObjFlowValue.item.NameData.Main,
            //                Sub = ObjFlowValue.item.NameData.Sub
            //            },
            //            DefaultValueData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue
            //            {
            //                Values = null
            //            }
            //        };

            //        #region Values
            //        List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value> ValuesList = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

            //        for (int i = 0; i < 8; i++)
            //        {
            //            XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value
            //            {
            //                DefaultObjectValue = 0,
            //                Description = "Test " + i
            //            };

            //            ValuesList.Add(value);
            //        }

            //        objFlow.DefaultValueData.Values = ValuesList;
            //        #endregion

            //        ObjFlowList.Add(objFlow);
            //    }

            //    kMPObjFlowDataXml.ObjFlows = ObjFlowList;

            //    //Delete Namespaces
            //    var xns = new XmlSerializerNamespaces();
            //    xns.Add(string.Empty, string.Empty);

            //    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(XMLConvert.ObjFlowData.ObjFlowData_XML));
            //    System.IO.StreamWriter sw = new StreamWriter(XmlPath, false, new System.Text.UTF8Encoding(false));
            //    serializer.Serialize(sw, kMPObjFlowDataXml, xns);
            //    sw.Close();
            //}
            #endregion

            /// <summary>
            /// Read ObjFlowData.xml
            /// </summary>
            /// <param name="Path"></param>
            /// <returns></returns>
            public static XMLConvert.ObjFlowData.ObjFlowData_XML ReadObjFlowXml(string Path)
            {
                System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
                System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(XMLConvert.ObjFlowData.ObjFlowData_XML));
                XMLConvert.ObjFlowData.ObjFlowData_XML ObjFlowXml = (XMLConvert.ObjFlowData.ObjFlowData_XML)s1.Deserialize(fs1);

                fs1.Close();
                fs1.Dispose();

                return ObjFlowXml;
            }

            #region ReadObjFlowXml [DELETE]
            ////Read ObjFlowData.xml
            //public static List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ReadObjFlowXml(string Path)
            //{
            //    System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
            //    System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(XMLConvert.ObjFlowData.ObjFlowData_XML));
            //    XMLConvert.ObjFlowData.ObjFlowData_XML ObjFlowXml = (XMLConvert.ObjFlowData.ObjFlowData_XML)s1.Deserialize(fs1);

            //    fs1.Close();
            //    fs1.Dispose();

            //    return ObjFlowXml.ObjFlows;


            //    //List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowXml_List = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow>();

            //    //foreach (var ObjFlowData in ObjFlowXml.ObjFlows)
            //    //{
            //    //    XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow objFlow = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow
            //    //    {
            //    //        ObjectID = ObjFlowData.ObjectID,
            //    //        ObjectName = ObjFlowData.ObjectName,
            //    //        Path = ObjFlowData.Path,
            //    //        UseKCL = ObjFlowData.UseKCL,
            //    //        ObjectType = ObjFlowData.ObjectType,
            //    //        CommonData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Common
            //    //        {
            //    //            ColType = ObjFlowData.CommonData.ColType,
            //    //            PathType = ObjFlowData.CommonData.PathType,
            //    //            ModelSetting = ObjFlowData.CommonData.ModelSetting,
            //    //            Unknown1 = ObjFlowData.CommonData.Unknown1
            //    //        },
            //    //        LODSetting = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
            //    //        {
            //    //            LOD = ObjFlowData.LODSetting.LOD,
            //    //            LODHighPoly = ObjFlowData.LODSetting.LODHighPoly,
            //    //            LODLowPoly = ObjFlowData.LODSetting.LODLowPoly,
            //    //            LODDefault = ObjFlowData.LODSetting.LODDefault
            //    //        },
            //    //        ScaleData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
            //    //        {
            //    //            X = ObjFlowData.ScaleData.X,
            //    //            Y = ObjFlowData.ScaleData.Y,
            //    //            Z = ObjFlowData.ScaleData.Z
            //    //        },
            //    //        NameData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Name
            //    //        {
            //    //            Main = ObjFlowData.NameData.Main,
            //    //            Sub = ObjFlowData.NameData.Sub
            //    //        },
            //    //        DefaultValueData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue
            //    //        {
            //    //            Values = null
            //    //        }
            //    //    };

            //    //    List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value> valueList = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

            //    //    foreach (var ObjFlowDataValue in ObjFlowData.DefaultValueData.Values)
            //    //    {
            //    //        XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value
            //    //        {
            //    //            DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
            //    //            Description = ObjFlowDataValue.Description
            //    //        };

            //    //        valueList.Add(value);
            //    //    }

            //    //    objFlow.DefaultValueData.Values = valueList;

            //    //    ObjFlowXml_List.Add(objFlow);
            //    //}

            //    //fs1.Close();
            //    //fs1.Dispose();

            //    //return ObjFlowXml_List;
            //}
            #endregion

            /// <summary>
            /// Write ObjFlowData.xml
            /// </summary>
            /// <param name="ObjFlowDBList"></param>
            /// <param name="Path"></param>
            public static void WriteObjFlowXml(List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDBList, string Path)
            {
                XMLConvert.ObjFlowData.ObjFlowData_XML kMPObjFlowDataXml = new XMLConvert.ObjFlowData.ObjFlowData_XML
                {
                    ObjFlows = null
                };

                List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowList = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow>();

                foreach (var ObjFlowValue in ObjFlowDBList.Select((item, index) => new { item, index }))
                {
                    XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow objFlow = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow
                    {
                        ObjectID = ObjFlowValue.item.ObjectID,
                        ObjectName = ObjFlowValue.item.ObjectName,
                        Path = ObjFlowValue.item.Path,
                        UseKCL = ObjFlowValue.item.UseKCL,
                        ObjectType = ObjFlowValue.item.ObjectType,
                        CommonData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Common
                        {
                            ColType = ObjFlowValue.item.CommonData.ColType,
                            PathType = ObjFlowValue.item.CommonData.PathType,
                            ModelSetting = ObjFlowValue.item.CommonData.ModelSetting,
                            Unknown1 = ObjFlowValue.item.CommonData.Unknown1
                        },
                        LODSetting = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
                        {
                            LOD = ObjFlowValue.item.LODSetting.LOD,
                            LODHighPoly = ObjFlowValue.item.LODSetting.LODHighPoly,
                            LODLowPoly = ObjFlowValue.item.LODSetting.LODLowPoly,
                            LODDefault = ObjFlowValue.item.LODSetting.LODDefault
                        },
                        ScaleData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
                        {
                            X = Convert.ToInt32(ObjFlowValue.item.ScaleData.X),
                            Y = Convert.ToInt32(ObjFlowValue.item.ScaleData.Y),
                            Z = Convert.ToInt32(ObjFlowValue.item.ScaleData.Z)
                        },
                        NameData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Name
                        {
                            Main = ObjFlowValue.item.NameData.Main,
                            Sub = ObjFlowValue.item.NameData.Sub
                        },
                        DefaultValueData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue
                        {
                            Values = null
                        }
                    };

                    #region Values
                    List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value> ValuesList = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

                    foreach (var i in ObjFlowValue.item.DefaultValueData.Values)
                    {
                        XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value
                        {
                            DefaultObjectValue = i.DefaultObjectValue,
                            Description = i.Description
                        };

                        ValuesList.Add(value);
                    }

                    objFlow.DefaultValueData.Values = ValuesList;
                    #endregion

                    ObjFlowList.Add(objFlow);
                }

                kMPObjFlowDataXml.ObjFlows = ObjFlowList;

                //Delete Namespaces
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);

                System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(XMLConvert.ObjFlowData.ObjFlowData_XML));
                System.IO.StreamWriter sw = new StreamWriter(Path, false, new System.Text.UTF8Encoding(false));
                serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                sw.Close();
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

            #region ToObjFlowDB_XML[DELETE]
            //public static List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ToObjFlowDB_XML(FBOCLibrary.FBOC FBOCData)
            //{
            //    List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDataXmlList = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow>();

            //    for (int Count = 0; Count < FBOCData.ObjFlowDataList.Count; Count++)
            //    {
            //        XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow ObjFlowDatabase = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow
            //        {
            //            ObjectID = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].ObjectID.Reverse().ToArray()).Replace("-", string.Empty),
            //            ObjectName = new string(FBOCData.ObjFlowDataList[Count].ObjFlowName1).Replace("\0", ""),
            //            Path = "",
            //            UseKCL = false,
            //            ObjectType = "NaN",
            //            CommonData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Common
            //            {
            //                ColType = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].CollisionType.Reverse().ToArray()).Replace("-", string.Empty),
            //                PathType = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].PathType.Reverse().ToArray()).Replace("-", string.Empty),
            //                ModelSetting = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].ModelSetting.Reverse().ToArray()).Replace("-", string.Empty),
            //                Unknown1 = BitConverter.ToString(FBOCData.ObjFlowDataList[Count].Unknown1.Reverse().ToArray(), 0).Replace("-", string.Empty)
            //            },
            //            LODSetting = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
            //            {
            //                LOD = FBOCData.ObjFlowDataList[Count].LOD_Setting.LOD,
            //                LODHighPoly = FBOCData.ObjFlowDataList[Count].LOD_Setting.LODHighPoly,
            //                LODLowPoly = FBOCData.ObjFlowDataList[Count].LOD_Setting.LODLowPoly,
            //                LODDefault = FBOCData.ObjFlowDataList[Count].LOD_Setting.LODDefault
            //            },
            //            ScaleData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
            //            {
            //                X = FBOCData.ObjFlowDataList[Count].ObjFlowScale.X,
            //                Y = FBOCData.ObjFlowDataList[Count].ObjFlowScale.Y,
            //                Z = FBOCData.ObjFlowDataList[Count].ObjFlowScale.Z
            //            },
            //            NameData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.Name
            //            {
            //                Main = new string(FBOCData.ObjFlowDataList[Count].ObjFlowName1).Replace("\0", ""),
            //                Sub = new string(FBOCData.ObjFlowDataList[Count].ObjFlowName2).Replace("\0", "")
            //            },
            //            DefaultValueData = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue
            //            {
            //                Values = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value>()
            //            }
            //        };

            //        List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value> valueList = new List<XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

            //        foreach (var ObjFlowDataValue in ObjFlowDatabase.DefaultValueData.Values)
            //        {
            //            XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new XMLConvert.ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value
            //            {
            //                DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
            //                Description = ObjFlowDataValue.Description
            //            };

            //            valueList.Add(value);
            //        }

            //        ObjFlowDatabase.DefaultValueData.Values = valueList;

            //        ObjFlowDataXmlList.Add(ObjFlowDatabase);
            //    }

            //    return ObjFlowDataXmlList;
            //}
            #endregion
        }
    }
}
