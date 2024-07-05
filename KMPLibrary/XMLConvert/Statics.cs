using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static KMPLibrary.XMLConvert.IO.XML_Exporter;

namespace KMPLibrary.XMLConvert
{
    public class Statics
    {
        public class KMP
        {
            #region KMP => XML
            /// <summary>
            /// Export all KMP data (XML) 
            /// </summary>
            /// <param name="KMPData"></param>
            /// <param name="Path">FilePath</param>
            public static void ExportAll(Format.KMP KMPData, string Path)
            {
                KMPData.KMP_XML KMP_XML = new KMPData.KMP_XML(KMPData);
                XmlSerializerNamespaces xns = EmptyXmlSerializerNamespaces();
                XMLExport<KMPData.KMP_XML>(Path + "_All.xml", KMP_XML, xns);

                #region DELETE
                ////Delete Namespaces
                //var xns = new XmlSerializerNamespaces();
                //xns.Add(string.Empty, string.Empty);

                //System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(KMPData.KMP_XML));
                //System.IO.StreamWriter sw = new StreamWriter(Path + "_All.xml", false, new System.Text.UTF8Encoding(false));
                //serializer.Serialize(sw, KMP_XML, xns);
                //sw.Close();
                #endregion
            }

            /// <summary>
            /// Export KMP Section (XML)
            /// </summary>
            /// <param name="KMP">KMPData</param>
            /// <param name="Path">FilePath</param>
            /// <param name="SectionType">SectionType</param>
            public static void ExportSection(Format.KMP KMP, string Path, KMPData.KMPXmlSetting.Section SectionType)
            {
                KMPData.KMP_XML KMP_Xml = KMPData.KMP_XML.CreateNullDefault();

                if (SectionType == KMPData.KMPXmlSetting.Section.KartPoint) KMP_Xml.startPositions = new KMPData.SectionData.StartPosition(KMP.KMP_Section.TPTK);
                else if (SectionType == KMPData.KMPXmlSetting.Section.EnemyRoutes) KMP_Xml.EnemyRoutes = new KMPData.SectionData.EnemyRoute(KMP.KMP_Section.HPNE, KMP.KMP_Section.TPNE);
                else if (SectionType == KMPData.KMPXmlSetting.Section.ItemRoutes) KMP_Xml.ItemRoutes = new KMPData.SectionData.ItemRoute(KMP.KMP_Section.HPTI, KMP.KMP_Section.TPTI);
                else if (SectionType == KMPData.KMPXmlSetting.Section.CheckPoint) KMP_Xml.Checkpoints = new KMPData.SectionData.Checkpoint(KMP.KMP_Section.HPKC, KMP.KMP_Section.TPKC);
                else if (SectionType == KMPData.KMPXmlSetting.Section.Object) KMP_Xml.Objects = new KMPData.SectionData.Object(KMP.KMP_Section.JBOG);
                else if (SectionType == KMPData.KMPXmlSetting.Section.Route) KMP_Xml.Routes = new KMPData.SectionData.Route(KMP.KMP_Section.ITOP);
                else if (SectionType == KMPData.KMPXmlSetting.Section.Area) KMP_Xml.Areas = new KMPData.SectionData.Area(KMP.KMP_Section.AERA);
                else if (SectionType == KMPData.KMPXmlSetting.Section.Camera) KMP_Xml.Cameras = new KMPData.SectionData.Camera(KMP.KMP_Section.EMAC);
                else if (SectionType == KMPData.KMPXmlSetting.Section.JugemPoint) KMP_Xml.JugemPoints = new KMPData.SectionData.JugemPoint(KMP.KMP_Section.TPGJ);
                else if (SectionType == KMPData.KMPXmlSetting.Section.GlideRoutes) KMP_Xml.GlideRoutes = new KMPData.SectionData.GlideRoute(KMP.KMP_Section.HPLG, KMP.KMP_Section.TPLG);

                XmlSerializerNamespaces xns = EmptyXmlSerializerNamespaces();
                XMLExport<KMPData.KMP_XML>(Path + "_" + SectionType.ToString() + ".xml", KMP_Xml, xns);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="KMPData"></param>
            /// <param name="Path"></param>
            /// <param name="RouteType"></param>
            public static void ExportXXXXRoute(Format.KMP KMPData, string Path, XXXXRouteData.XXXXRouteXmlSetting.RouteType RouteType)
            {
                XXXXRouteData.XXXXRoute_XML XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML();
                if (RouteType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.EnemyRoute)
                {
                    XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPNE, KMPData.KMP_Section.TPNE));
                }
                else if (RouteType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.ItemRoute)
                {
                    XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPTI, KMPData.KMP_Section.TPTI));
                }
                else if (RouteType == XXXXRouteData.XXXXRouteXmlSetting.RouteType.GlideRoute)
                {
                    XXXXRoute_XML = new XXXXRouteData.XXXXRoute_XML(new XXXXRouteData.XXXXRoute_XML.XXXXRoute(KMPData.KMP_Section.HPLG, KMPData.KMP_Section.TPLG));
                }

                XmlSerializerNamespaces xns = EmptyXmlSerializerNamespaces();
                XMLExport<XXXXRouteData.XXXXRoute_XML>(Path + "_PositionAndScaleOnly" + ".xml", XXXXRoute_XML, xns);
            }
            #endregion

        }

        public class ObjFlow
        {
            /// <summary>
            /// Create Xml
            /// </summary>
            /// <param name="ObjFlowVal_List"></param>
            /// <param name="KMPObjectFolderPath"></param>
            /// <param name="DefaultModelPath"></param>
            /// <param name="XmlPath"></param>
            public static void CreateXml(List<ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowVal_List, string KMPObjectFolderPath, string DefaultModelPath, string XmlPath)
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

                    List<ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value> ValuesList = new List<ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

                    for (int i = 0; i < 8; i++)
                    {
                        ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value
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

                ObjFlowData.ObjFlowData_XML kMPObjFlowDataXml = new ObjFlowData.ObjFlowData_XML { ObjFlows = ObjFlowVal_List };

                //Delete Namespaces
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);

                System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(ObjFlowData.ObjFlowData_XML));
                System.IO.StreamWriter sw = new StreamWriter(XmlPath, false, new System.Text.UTF8Encoding(false));
                serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                sw.Close();
            }

            /// <summary>
            /// Read ObjFlowData.xml
            /// </summary>
            /// <param name="Path"></param>
            /// <returns></returns>
            public static ObjFlowData.ObjFlowData_XML ReadObjFlowXml(string Path)
            {
                System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
                System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(ObjFlowData.ObjFlowData_XML));
                ObjFlowData.ObjFlowData_XML ObjFlowXml = (ObjFlowData.ObjFlowData_XML)s1.Deserialize(fs1);

                fs1.Close();
                fs1.Dispose();

                return ObjFlowXml;
            }

            /// <summary>
            /// Write ObjFlowData.xml
            /// </summary>
            /// <param name="ObjFlowDBList"></param>
            /// <param name="Path"></param>
            public static void WriteObjFlowXml(List<ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowDBList, string Path)
            {
                ObjFlowData.ObjFlowData_XML kMPObjFlowDataXml = new ObjFlowData.ObjFlowData_XML
                {
                    ObjFlows = null
                };

                List<ObjFlowData.ObjFlowData_XML.ObjFlow> ObjFlowList = new List<ObjFlowData.ObjFlowData_XML.ObjFlow>();

                foreach (var ObjFlowValue in ObjFlowDBList.Select((item, index) => new { item, index }))
                {
                    ObjFlowData.ObjFlowData_XML.ObjFlow objFlow = new ObjFlowData.ObjFlowData_XML.ObjFlow
                    {
                        ObjectID = ObjFlowValue.item.ObjectID,
                        ObjectName = ObjFlowValue.item.ObjectName,
                        Path = ObjFlowValue.item.Path,
                        UseKCL = ObjFlowValue.item.UseKCL,
                        ObjectType = ObjFlowValue.item.ObjectType,
                        CommonData = new ObjFlowData.ObjFlowData_XML.ObjFlow.Common
                        {
                            ColType = ObjFlowValue.item.CommonData.ColType,
                            PathType = ObjFlowValue.item.CommonData.PathType,
                            ModelSetting = ObjFlowValue.item.CommonData.ModelSetting,
                            Unknown1 = ObjFlowValue.item.CommonData.Unknown1
                        },
                        LODSetting = new ObjFlowData.ObjFlowData_XML.ObjFlow.LOD_Setting
                        {
                            LOD = ObjFlowValue.item.LODSetting.LOD,
                            LODHighPoly = ObjFlowValue.item.LODSetting.LODHighPoly,
                            LODLowPoly = ObjFlowValue.item.LODSetting.LODLowPoly,
                            LODDefault = ObjFlowValue.item.LODSetting.LODDefault
                        },
                        ScaleData = new ObjFlowData.ObjFlowData_XML.ObjFlow.Scale
                        {
                            X = Convert.ToInt32(ObjFlowValue.item.ScaleData.X),
                            Y = Convert.ToInt32(ObjFlowValue.item.ScaleData.Y),
                            Z = Convert.ToInt32(ObjFlowValue.item.ScaleData.Z)
                        },
                        NameData = new ObjFlowData.ObjFlowData_XML.ObjFlow.Name
                        {
                            Main = ObjFlowValue.item.NameData.Main,
                            Sub = ObjFlowValue.item.NameData.Sub
                        },
                        DefaultValueData = new ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue
                        {
                            Values = null
                        }
                    };

                    #region Values
                    List<ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value> ValuesList = new List<ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value>();

                    foreach (var i in ObjFlowValue.item.DefaultValueData.Values)
                    {
                        ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value value = new ObjFlowData.ObjFlowData_XML.ObjFlow.DefaultValue.Value
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

                System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(ObjFlowData.ObjFlowData_XML));
                System.IO.StreamWriter sw = new StreamWriter(Path, false, new System.Text.UTF8Encoding(false));
                serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                sw.Close();
            }
        }
    }
}
