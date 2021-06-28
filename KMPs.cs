using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.IO;
using System.Data;
using System.Collections;
using System.Xml.Serialization;

namespace MK7_KMP_Editor_For_PG_
{
    public class KMPs
    {
        public class KMPViewportObject
        {
            public List<ModelVisual3D> Area_MV3DList { get; set; }
            public List<ModelVisual3D> Camera_MV3DList { get; set; }
            public List<HTK_3DES.PathTools.Rail> EnemyRoute_Rail_List { get; set; }
            public List<HTK_3DES.PathTools.Rail> ItemRoute_Rail_List { get; set; }
            public List<HTK_3DES.PathTools.Rail> Routes_List { get; set; }
            public List<HTK_3DES.PathTools.Rail> GlideRoute_Rail_List { get; set; }
            public List<HTK_3DES.KMP_3DCheckpointSystem.Checkpoint> Checkpoint_Rail { get; set; }
            public List<ModelVisual3D> OBJ_MV3DList { get; set; }
            public List<ModelVisual3D> RespawnPoint_MV3DList { get; set; }
            public List<ModelVisual3D> StartPosition_MV3DList { get; set; }
        }

        public class KMPFormat
        {
            public char[] DMDCHeader { get; set; } //0x4
            public uint FileSize { get; set; } //0x4
            public ushort SectionCount { get; set; } //0x2
            public ushort DMDCHeaderSize { get; set; } //0x2
            public uint VersionNumber { get; set; } //0x4
            public DMDCSectionOffset DMDC_SectionOffset { get; set; }
            public class DMDCSectionOffset
            {
                public uint TPTK_Offset { get; set; }
                public uint TPNE_Offset { get; set; }
                public uint HPNE_Offset { get; set; }
                public uint TPTI_Offset { get; set; }
                public uint HPTI_Offset { get; set; }
                public uint TPKC_Offset { get; set; }
                public uint HPKC_Offset { get; set; }
                public uint JBOG_Offset { get; set; }
                public uint ITOP_Offset { get; set; }
                public uint AERA_Offset { get; set; }
                public uint EMAC_Offset { get; set; }
                public uint TPGJ_Offset { get; set; }
                public uint TPNC_Offset { get; set; }
                public uint TPSM_Offset { get; set; }
                public uint IGTS_Offset { get; set; }
                public uint SROC_Offset { get; set; }
                public uint TPLG_Offset { get; set; }
                public uint HPLG_Offset { get; set; }
            }

            public KMPSection KMP_Section { get; set; }
            public class KMPSection
            {
                public TPTK_Section TPTK { get; set; }
                public class TPTK_Section
                {
                    public char[] TPTKHeader { get; set; } //0x4
                    public ushort NumOfEntries { get; set; } //0x2
                    public ushort AdditionalValue { get; set; } //0x2
                    public List<TPTKValue> TPTKValue_List { get; set; }
                    public class TPTKValue
                    {
                        public Vector3D TPTK_Position { get; set; } //0x4
                        public Vector3D TPTK_Rotation { get; set; } //0x4
                        public ushort Player_Index { get; set; } //0x2
                        public ushort TPTK_UnkBytes { get; set; } //0x2
                    }
                }

                public TPNE_Section TPNE { get; set; }
                public class TPNE_Section
                {
                    public char[] TPNEHeader { get; set; } //0x4
                    public ushort NumOfEntries { get; set; } //0x2
                    public ushort AdditionalValue { get; set; } //0x2
                    public List<TPNEValue> TPNEValue_List { get; set; }
                    public class TPNEValue
                    {
                        public Vector3D TPNE_Position { get; set; }
                        public float Control { get; set; }
                        public ushort MushSetting { get; set; }
                        public byte DriftSetting { get; set; }
                        public byte Flags { get; set; }
                        public short PathFindOption { get; set; }
                        public short MaxSearchYOffset { get; set; }
                    }
                }

                public HPNE_Section HPNE { get; set; }
                public class HPNE_Section
                {
                    public char[] HPNEHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<HPNEValue> HPNEValue_List { get; set; }
                    public class HPNEValue
                    {
                        public ushort HPNE_StartPoint { get; set; }
                        public ushort HPNE_Length { get; set; }

                        public HPNE_PreviewGroups HPNE_PreviewGroup { get; set; }
                        public class HPNE_PreviewGroups
                        {
                            public ushort Prev0 { get; set; }
                            public ushort Prev1 { get; set; }
                            public ushort Prev2 { get; set; }
                            public ushort Prev3 { get; set; }
                            public ushort Prev4 { get; set; }
                            public ushort Prev5 { get; set; }
                            public ushort Prev6 { get; set; }
                            public ushort Prev7 { get; set; }
                            public ushort Prev8 { get; set; }
                            public ushort Prev9 { get; set; }
                            public ushort Prev10 { get; set; }
                            public ushort Prev11 { get; set; }
                            public ushort Prev12 { get; set; }
                            public ushort Prev13 { get; set; }
                            public ushort Prev14 { get; set; }
                            public ushort Prev15 { get; set; }
                        }

                        public HPNE_NextGroups HPNE_NextGroup { get; set; }
                        public class HPNE_NextGroups
                        {
                            public ushort Next0 { get; set; }
                            public ushort Next1 { get; set; }
                            public ushort Next2 { get; set; }
                            public ushort Next3 { get; set; }
                            public ushort Next4 { get; set; }
                            public ushort Next5 { get; set; }
                            public ushort Next6 { get; set; }
                            public ushort Next7 { get; set; }
                            public ushort Next8 { get; set; }
                            public ushort Next9 { get; set; }
                            public ushort Next10 { get; set; }
                            public ushort Next11 { get; set; }
                            public ushort Next12 { get; set; }
                            public ushort Next13 { get; set; }
                            public ushort Next14 { get; set; }
                            public ushort Next15 { get; set; }
                        }

                        public uint HPNE_UnkBytes1 { get; set; }
                    }
                }

                public TPTI_Section TPTI { get; set; }
                public class TPTI_Section
                {
                    public char[] TPTIHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<TPTIValue> TPTIValue_List { get; set; }
                    public class TPTIValue
                    {
                        public Vector3D TPTI_Position { get; set; }
                        public float TPTI_PointSize { get; set; }
                        public ushort GravityMode { get; set; }
                        public ushort PlayerScanRadius { get; set; }
                    }
                }

                public HPTI_Section HPTI { get; set; }
                public class HPTI_Section
                {
                    public char[] HPTIHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<HPTIValue> HPTIValue_List { get; set; }
                    public class HPTIValue
                    {
                        public ushort HPTI_StartPoint { get; set; }
                        public ushort HPTI_Length { get; set; }

                        public HPTI_PreviewGroups HPTI_PreviewGroup { get; set; }
                        public class HPTI_PreviewGroups
                        {
                            public ushort Prev0 { get; set; }
                            public ushort Prev1 { get; set; }
                            public ushort Prev2 { get; set; }
                            public ushort Prev3 { get; set; }
                            public ushort Prev4 { get; set; }
                            public ushort Prev5 { get; set; }
                        }

                        public HPTI_NextGroups HPTI_NextGroup { get; set; }
                        public class HPTI_NextGroups
                        {
                            public ushort Next0 { get; set; }
                            public ushort Next1 { get; set; }
                            public ushort Next2 { get; set; }
                            public ushort Next3 { get; set; }
                            public ushort Next4 { get; set; }
                            public ushort Next5 { get; set; }
                        }
                    }
                }

                public TPKC_Section TPKC { get; set; }
                public class TPKC_Section
                {
                    public char[] TPKCHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<TPKCValue> TPKCValue_List { get; set; }
                    public class TPKCValue
                    {
                        public Vector2 TPKC_2DPosition_Left { get; set; }
                        public Vector2 TPKC_2DPosition_Right { get; set; }
                        public byte TPKC_RespawnID { get; set; }
                        public byte TPKC_Checkpoint_Type { get; set; }
                        public byte TPKC_PreviousCheckPoint { get; set; }
                        public byte TPKC_NextCheckPoint { get; set; }
                        public byte TPKC_UnkBytes1 { get; set; }
                        public byte TPKC_UnkBytes2 { get; set; }
                        public byte TPKC_UnkBytes3 { get; set; }
                        public byte TPKC_UnkBytes4 { get; set; }
                    }
                }

                public HPKC_Section HPKC { get; set; }
                public class HPKC_Section
                {
                    public char[] HPKCHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<HPKCValue> HPKCValue_List { get; set; }
                    public class HPKCValue
                    {
                        public byte HPKC_StartPoint { get; set; }
                        public byte HPKC_Length { get; set; }

                        public HPKC_PreviewGroups HPKC_PreviewGroup { get; set; }
                        public class HPKC_PreviewGroups
                        {
                            public byte Prev0 { get; set; }
                            public byte Prev1 { get; set; }
                            public byte Prev2 { get; set; }
                            public byte Prev3 { get; set; }
                            public byte Prev4 { get; set; }
                            public byte Prev5 { get; set; }
                        }

                        public HPKC_NextGroups HPKC_NextGroup { get; set; }
                        public class HPKC_NextGroups
                        {
                            public byte Next0 { get; set; }
                            public byte Next1 { get; set; }
                            public byte Next2 { get; set; }
                            public byte Next3 { get; set; }
                            public byte Next4 { get; set; }
                            public byte Next5 { get; set; }
                        }

                        public ushort HPKC_UnkBytes1 { get; set; }
                    }
                }

                public JBOG_Section JBOG { get; set; }
                public class JBOG_Section
                {
                    public char[] JBOGHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<JBOGValue> JBOGValue_List { get; set; }
                    public class JBOGValue
                    {
                        public byte[] ObjectID { get; set; }
                        public byte[] JBOG_UnkByte1 { get; set; }
                        public Vector3D JBOG_Position { get; set; }
                        public Vector3D JBOG_Rotation { get; set; }
                        public Vector3D JBOG_Scale { get; set; }
                        public ushort JBOG_ITOP_RouteIDIndex { get; set; }
                        public JBOG_SpecificSetting JOBJ_Specific_Setting { get; set; }
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
                        }
                        public ushort JBOG_PresenceSetting { get; set; }
                        public byte[] JBOG_UnkByte2 { get; set; }
                        public ushort JBOG_UnkByte3 { get; set; }
                    }
                }

                public ITOP_Section ITOP { get; set; }
                public class ITOP_Section
                {
                    public char[] ITOPHeader { get; set; }
                    public ushort ITOP_NumberOfRoute { get; set; }
                    public ushort ITOP_NumberOfPoint { get; set; }
                    public List<ITOP_Route> ITOP_Route_List { get; set; }
                    public class ITOP_Route
                    {
                        public ushort ITOP_Route_NumOfPoint { get; set; }
                        public byte ITOP_RouteSetting1 { get; set; }
                        public byte ITOP_RouteSetting2 { get; set; }
                        public List<ITOP_Point> ITOP_Point_List { get; set; }
                        public class ITOP_Point
                        {
                            public Vector3D ITOP_Point_Position { get; set; }
                            public ushort ITOP_Point_RouteSpeed { get; set; }
                            public ushort ITOP_PointSetting2 { get; set; }
                        }
                    }
                }

                public AERA_Section AERA { get; set; }
                public class AERA_Section
                {
                    public char[] AERAHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<AERAValue> AERAValue_List { get; set; }
                    public class AERAValue
                    {
                        public byte AreaMode { get; set; }
                        public byte AreaType { get; set; }
                        public byte AERA_EMACIndex { get; set; }
                        public byte Priority { get; set; }
                        public Vector3D AERA_Position { get; set; }
                        public Vector3D AERA_Rotation { get; set; }
                        public Vector3D AERA_Scale { get; set; }
                        public ushort AERA_UnkByte1 { get; set; }
                        public ushort AERA_UnkByte2 { get; set; }
                        public byte RouteID { get; set; }
                        public byte EnemyID { get; set; }
                        public ushort AERA_UnkByte4 { get; set; }
                    }
                }

                public EMAC_Section EMAC { get; set; }
                public class EMAC_Section
                {
                    public char[] EMACHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<EMACValue> EMACValue_List { get; set; }
                    public class EMACValue
                    {
                        public byte CameraType { get; set; }
                        public byte NextCameraIndex { get; set; }
                        public byte EMAC_UnkBytes1 { get; set; }
                        public byte EMAC_ITOP_CameraIndex { get; set; }
                        public ushort RouteSpeed { get; set; }
                        public ushort FOVSpeed { get; set; }
                        public ushort ViewpointSpeed { get; set; }
                        public byte EMAC_UnkBytes2 { get; set; }
                        public byte EMAC_UnkBytes3 { get; set; }
                        public Vector3D EMAC_Position { get; set; }
                        public Vector3D EMAC_Rotation { get; set; }
                        public float FOVAngle_Start { get; set; }
                        public float FOVAngle_End { get; set; }
                        public Vector3D Viewpoint_Start { get; set; }
                        public Vector3D Viewpoint_Destination { get; set; }
                        public float Camera_Active_Time { get; set; }
                    }
                }

                public TPGJ_Section TPGJ { get; set; }
                public class TPGJ_Section
                {
                    public char[] TPGJHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<TPGJValue> TPGJValue_List { get; set; }
                    public class TPGJValue
                    {
                        public Vector3D TPGJ_Position { get; set; }
                        public Vector3D TPGJ_Rotation { get; set; }
                        public ushort TPGJ_RespawnID { get; set; }
                        public ushort TPGJ_UnkBytes1 { get; set; }
                    }
                }

                public TPNC_Section TPNC { get; set; }
                public class TPNC_Section
                {
                    public char[] TPNCHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    //public List<TPNCValue> TPNCValue_List { get; set; }
                    //public class TPNCValue
                    //{
                    //    //Unused
                    //}
                }

                public TPSM_Section TPSM { get; set; }
                public class TPSM_Section
                {
                    public char[] TPSMHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    //public List<TPSMValue> TPSMValue_List { get; set; }
                    //public class TPSMValue
                    //{
                    //    //Unused
                    //}
                }

                public IGTS_Section IGTS { get; set; }
                public class IGTS_Section
                {
                    public char[] IGTSHeader { get; set; }

                    public uint Unknown1 { get; set; }
                    public byte LapCount { get; set; }
                    public byte PolePosition { get; set; }
                    public byte Unknown2 { get; set; }
                    public byte Unknown3 { get; set; }

                    public RGBA RGBAColor { get; set; }
                    public class RGBA
                    {
                        public byte R { get; set; }
                        public byte G { get; set; }
                        public byte B { get; set; }
                        public byte A { get; set; }
                    }

                    public uint FlareAlpha { get; set; }
                }

                public SROC_Section SROC { get; set; }
                public class SROC_Section
                {
                    public char[] SROCHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    //public List<SROCValue> SROCValue_List { get; set; }
                    //public class SROCValue
                    //{
                    //    //Unused
                    //}
                }

                public TPLG_Section TPLG { get; set; }
                public class TPLG_Section
                {
                    public char[] TPLGHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<TPLGValue> TPLGValue_List { get; set; }
                    public class TPLGValue
                    {
                        public Vector3D TPLG_Position { get; set; }
                        public float TPLG_PointScaleValue { get; set; }
                        public uint TPLG_UnkBytes1 { get; set; }
                        public uint TPLG_UnkBytes2 { get; set; }
                    }
                }

                public HPLG_Section HPLG { get; set; }
                public class HPLG_Section
                {
                    public char[] HPLGHeader { get; set; }
                    public ushort NumOfEntries { get; set; }
                    public ushort AdditionalValue { get; set; }
                    public List<HPLGValue> HPLGValue_List { get; set; }
                    public class HPLGValue
                    {
                        public byte HPLG_StartPoint { get; set; }
                        public byte HPLG_Length { get; set; }

                        public HPLG_PreviewGroups HPLG_PreviewGroup { get; set; }
                        public class HPLG_PreviewGroups
                        {
                            public byte Prev0 { get; set; }
                            public byte Prev1 { get; set; }
                            public byte Prev2 { get; set; }
                            public byte Prev3 { get; set; }
                            public byte Prev4 { get; set; }
                            public byte Prev5 { get; set; }
                        }

                        public HPLG_NextGroups HPLG_NextGroup { get; set; }
                        public class HPLG_NextGroups
                        {
                            public byte Next0 { get; set; }
                            public byte Next1 { get; set; }
                            public byte Next2 { get; set; }
                            public byte Next3 { get; set; }
                            public byte Next4 { get; set; }
                            public byte Next5 { get; set; }
                        }

                        public uint RouteSetting { get; set; }
                        public uint HPLG_UnkBytes2 { get; set; }
                    }
                }
            }
        }

        public class KMPHelper
        {
            public class ByteVector3D
            {
                public byte[] Byte_X { get; set; }
                public byte[] Byte_Y { get; set; }
                public byte[] Byte_Z { get; set; }
            }

            public class ByteArrayToVector3DConverter : KMPHelper
            {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="BVector3D"></param>
                /// <returns></returns>
                public Vector3D ByteArrayToVector3D(ByteVector3D BVector3D)
                {
                    double Value_X = BitConverter.ToSingle(BVector3D.Byte_X, 0);
                    double Value_Y = BitConverter.ToSingle(BVector3D.Byte_Y, 0);
                    double Value_Z = BitConverter.ToSingle(BVector3D.Byte_Z, 0);

                    return new Vector3D(Value_X, Value_Y, Value_Z);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="BVector3D"></param>
                /// <returns></returns>
                public Vector3D ByteArrayToVector3D(byte[][] BVector3D)
                {
                    double Value_X = BitConverter.ToSingle(BVector3D[0], 0);
                    double Value_Y = BitConverter.ToSingle(BVector3D[1], 0);
                    double Value_Z = BitConverter.ToSingle(BVector3D[2], 0);

                    return new Vector3D(Value_X, Value_Y, Value_Z);
                }
            }

            public class Vector3DToByteArrayConverter : KMPHelper
            {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="Vector3D"></param>
                /// <returns></returns>
                public ByteVector3D Vector3DToBVector3D(Vector3D Vector3D)
                {
                    byte[] Byte_X = BitConverter.GetBytes(Convert.ToSingle(Vector3D.X));
                    byte[] Byte_Y = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Y));
                    byte[] Byte_Z = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Z));

                    ByteVector3D BVector3D = new ByteVector3D
                    {
                        Byte_X = Byte_X,
                        Byte_Y = Byte_Y,
                        Byte_Z = Byte_Z
                    };

                    return BVector3D;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="Vector3D"></param>
                /// <returns></returns>
                public byte[][] Vector3DToByteArray(Vector3D Vector3D)
                {
                    byte[] Byte_X = BitConverter.GetBytes(Convert.ToSingle(Vector3D.X));
                    byte[] Byte_Y = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Y));
                    byte[] Byte_Z = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Z));

                    return new byte[][] { Byte_X, Byte_Y, Byte_Z };
                }
            }

            public class Vector3DTo2DConverter : KMPHelper
            {
                public class ByteVector2D
                {
                    public byte[] Byte_X { get; set; }
                    public byte[] Byte_Y { get; set; }
                }

                /// <summary>
                /// ByteVector2DからVector2に変換
                /// </summary>
                /// <param name="InputBVector2"></param>
                /// <returns>Vector2</returns>
                public Vector2 BVector2ToVector2D(ByteVector2D InputBVector2)
                {
                    return new Vector2(BitConverter.ToSingle(InputBVector2.Byte_X, 0), BitConverter.ToSingle(InputBVector2.Byte_Y, 0));
                }

                /// <summary>
                /// ByteArrayからVector2に変換
                /// </summary>
                /// <param name="InputByteArray"></param>
                /// <returns>Vector2</returns>
                public Vector2 ByteArrayToVector2D(byte[][] InputByteArray)
                {
                    return new Vector2(BitConverter.ToSingle(InputByteArray[0], 0), BitConverter.ToSingle(InputByteArray[1], 0));
                }

                /// <summary>
                /// Vector2からByteVector2Dに変換
                /// </summary>
                /// <param name="InputVector2"></param>
                /// <returns>ByteVector2D</returns>
                public ByteVector2D Vector2ToBVector2D(Vector2 InputVector2)
                {
                    ByteVector2D BVector2D = new ByteVector2D
                    {
                        Byte_X = BitConverter.GetBytes(Convert.ToSingle(InputVector2.X)),
                        Byte_Y = BitConverter.GetBytes(Convert.ToSingle(InputVector2.Y))
                    };

                    return BVector2D;
                }

                /// <summary>
                /// Vector2からByteArrayに変換
                /// </summary>
                /// <param name="InputVector2"></param>
                /// <returns>ByteVector2D</returns>
                public byte[][] Vector2ToByteArray(Vector2 InputVector2)
                {
                    return new byte[][] { BitConverter.GetBytes(Convert.ToSingle(InputVector2.X)), BitConverter.GetBytes(Convert.ToSingle(InputVector2.Y)) };
                }

                public enum Axis_Up
                {
                    X,
                    Y,
                    Z
                }

                /// <summary>
                /// Vector3DからVector2に変換
                /// </summary>
                /// <param name="InputVector3D"></param>
                /// <param name="AxisToExc"></param>
                /// <returns></returns>
                public Vector2 Vector3DTo2D(Vector3D InputVector3D, Axis_Up AxisToExc = Axis_Up.Y)
                {
                    Vector2 Position2D = new Vector2();
                    if (AxisToExc == Axis_Up.X)
                    {
                        Position2D = new Vector2(Convert.ToSingle(InputVector3D.Y), Convert.ToSingle(InputVector3D.Z));
                    }
                    if (AxisToExc == Axis_Up.Y)
                    {
                        Position2D = new Vector2(Convert.ToSingle(InputVector3D.X), Convert.ToSingle(InputVector3D.Z));
                    }
                    if (AxisToExc == Axis_Up.Z)
                    {
                        Position2D = new Vector2(Convert.ToSingle(InputVector3D.X), Convert.ToSingle(InputVector3D.Y));
                    }

                    return Position2D;
                }

                /// <summary>
                /// Vector2からVector3Dに変換
                /// </summary>
                /// <param name="InputVector2D"></param>
                /// <param name="UpDirection"></param>
                /// <param name="Height"></param>
                /// <returns></returns>
                public Vector3D Vector2DTo3D(Vector2 InputVector2D, Axis_Up UpDirection = Axis_Up.Y, double Height = 0)
                {
                    Vector3D Position3D = new Vector3D();
                    if (UpDirection == Axis_Up.X)
                    {
                        Position3D = new Vector3D(Height, Convert.ToSingle(InputVector2D.X), Convert.ToSingle(InputVector2D.Y));
                    }
                    if (UpDirection == Axis_Up.Y)
                    {
                        Position3D = new Vector3D(Convert.ToSingle(InputVector2D.X), Height, Convert.ToSingle(InputVector2D.Y));
                    }
                    if (UpDirection == Axis_Up.Z)
                    {
                        Position3D = new Vector3D(Convert.ToSingle(InputVector2D.X), Convert.ToSingle(InputVector2D.Y), Height);
                    }

                    return Position3D;
                }

                public class CheckpointLR_3D
                {
                    public Vector3D Left { get; set; }
                    public Vector3D Right { get; set; }
                }

                public class CheckpointLR_2D
                {
                    public Vector2 Left { get; set; }
                    public Vector2 Right { get; set; }
                }
            }

            public class ObjFlowReader
            {
                public class ObjFlowValue
                {
                    //Object ID
                    public string ObjID { get; set; }

                    //衝突タイプ
                    public string ColType { get; set; }

                    //パスタイプ
                    public string PathType { get; set; }

                    //LOD
                    public int LOD { get; set; }
                    public int LODHPoly { get; set; }
                    public int LODLPoly { get; set; }
                    public int LODDef { get; set; }

                    //モデル設定
                    public string ModelSetting { get; set; }

                    //X
                    public int ObjX { get; set; }

                    //Y
                    public int ObjY { get; set; }

                    //Z
                    public int ObjZ { get; set; }

                    public string Unk1 { get; set; }

                    //Object Name 1
                    public string ObjFlowName1Text { get; set; }
                    //Object Name 2
                    public string ObjFlowName2Text { get; set; }
                }

                public class ObjFlowXmlToObject
                {
                    public List<ObjFlow> ObjFlows { get; set; }
                    public class ObjFlow
                    {
                        public string ObjectID { get; set; }
                        public string ObjectName { get; set; }
                        public string Path { get; set; }
                        public bool UseKCL { get; set; }
                        public string ObjectType { get; set; }

                        public Common Commons { get; set; }
                        public class Common
                        {
                            public string ObjID { get; set; }
                            public string ColType { get; set; }
                            public string PathType { get; set; }
                            public string ModelSetting { get; set; }
                            public string Unknown1 { get; set; }
                        }

                        public LOD_Setting LODSetting { get; set; }
                        public class LOD_Setting
                        {
                            public int LOD { get; set; }
                            public int LODHPoly { get; set; }
                            public int LODLPoly { get; set; }
                            public int LODDef { get; set; }
                        }

                        public Scale Scales { get; set; }
                        public class Scale
                        {
                            public int X { get; set; }
                            public int Y { get; set; }
                            public int Z { get; set; }
                        }

                        public Name Names { get; set; }
                        public class Name
                        {
                            public string Main { get; set; }
                            public string Sub { get; set; }
                        }

                        public Default_Values DefaultValues { get; set; }
                        public class Default_Values
                        {
                            public List<Value> Values { get; set; }
                            public class Value
                            {
                                public int DefaultObjectValue { get; set; }
                                public string Description { get; set; }
                            }
                        }
                    }
                }

                public List<ObjFlowValue> Read(string Path)
                {
                    List<ObjFlowValue> objFlowValues_List = new List<ObjFlowValue>();

                    System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);

                    FBOC FBOCData = new FBOC
                    {
                        fboc_Chunk = br.ReadChars(4),
                        fboc_HeaderSize = br.ReadInt16(),
                        OBJCount = br.ReadInt16(),
                        ObjFlow_Data = null
                    };

                    if (new string(FBOCData.fboc_Chunk) != "FBOC") throw new Exception("Invalid file.");

                    for (int Count = 0; Count < FBOCData.OBJCount; Count++)
                    {
                        FBOC.ObjFlowData ObjFlowData = new FBOC.ObjFlowData
                        {
                            ObjID = br.ReadBytes(2),
                            ColType = br.ReadBytes(2),
                            PathType = br.ReadBytes(2),
                            LOD = br.ReadInt16(),
                            LODHPoly = br.ReadInt32(),
                            LODLPoly = br.ReadInt32(),
                            LODDef = br.ReadInt32(),
                            ModelSetting = br.ReadBytes(2),
                            ObjX = br.ReadInt16(),
                            ObjY = br.ReadInt16(),
                            ObjZ = br.ReadInt16(),
                            Unknown1 = br.ReadBytes(4),
                            ObjFlowName1 = br.ReadChars(64),
                            ObjFlowName2 = br.ReadChars(64)
                        };

                        ObjFlowValue objFlowValue = new ObjFlowValue
                        {
                            ObjID = BitConverter.ToString(ObjFlowData.ObjID.Reverse().ToArray()).Replace("-", string.Empty),
                            ColType = BitConverter.ToString(ObjFlowData.ColType),
                            PathType = BitConverter.ToString(ObjFlowData.PathType),
                            LOD = ObjFlowData.LOD,
                            LODHPoly = ObjFlowData.LODHPoly,
                            LODLPoly = ObjFlowData.LODLPoly,
                            LODDef = ObjFlowData.LODDef,
                            ModelSetting = BitConverter.ToString(ObjFlowData.ModelSetting),
                            ObjX = ObjFlowData.ObjX,
                            ObjY = ObjFlowData.ObjY,
                            ObjZ = ObjFlowData.ObjZ,
                            Unk1 = BitConverter.ToString(ObjFlowData.Unknown1, 0),
                            ObjFlowName1Text = new string(ObjFlowData.ObjFlowName1).Replace("\0", ""),
                            ObjFlowName2Text = new string(ObjFlowData.ObjFlowName2).Replace("\0", "")
                        };

                        objFlowValues_List.Add(objFlowValue);
                    }

                    br.Close();
                    fs.Close();

                    return objFlowValues_List;
                }

                public Dictionary<string[], string> ObjFlowMdlPathDictionary(ObjFlowXmlToObject ObjFlowToObj, string Path, string DefaultModelPath)
                {
                    //指定したディレクトリの中にあるファイルパスを全て取得
                    string[] PathAry = System.IO.Directory.GetFiles(Path, "*.obj", System.IO.SearchOption.AllDirectories);

                    Dictionary<string[], string> ObjFlowDicts = new Dictionary<string[], string>();

                    foreach (var ObjFlowValue in ObjFlowToObj.ObjFlows.Select((item, index) => new { item, index }))
                    {
                        //Search the path of the corresponding model from PathAry(string[])
                        if (PathAry.Contains(ObjFlowValue.item.Path))
                        {
                            //Get ObjectID
                            string ObjectID = ObjFlowToObj.ObjFlows.Find(x => x.Path == ObjFlowValue.item.Path).ObjectID ?? "";
                            ObjFlowDicts.Add(new string[] { ObjFlowValue.item.ObjectName, ObjectID }, ObjFlowValue.item.Path);
                        }
                    }

                    return ObjFlowDicts;
                }

                public void CreateXml(List<ObjFlowValue> ObjFlowVal_List, string KMPObjectFolderPath, string DefaultModelPath, string XmlPath)
                {
                    string[] PathAry = System.IO.Directory.GetFiles(KMPObjectFolderPath, "*.obj", System.IO.SearchOption.AllDirectories);

                    TestXml.KMPObjFlowDataXml kMPObjFlowDataXml = new TestXml.KMPObjFlowDataXml
                    {
                        ObjFlows = null
                    };

                    List<TestXml.KMPObjFlowDataXml.ObjFlow> ObjFlowList = new List<TestXml.KMPObjFlowDataXml.ObjFlow>();

                    foreach (var ObjFlowValue in ObjFlowVal_List.Select((item, index) => new { item, index }))
                    {
                        string MDLPath = "";

                        //Search the path of the corresponding model from PathAry(string[])
                        if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.ObjFlowName1Text + ".obj"))
                        {
                            MDLPath = KMPObjectFolderPath + "\\" + ObjFlowValue.item.ObjFlowName1Text + ".obj";
                        }
                        if (PathAry.Contains(KMPObjectFolderPath + "\\" + ObjFlowValue.item.ObjFlowName1Text + ".obj") == false)
                        {
                            MDLPath = DefaultModelPath;
                        }

                        TestXml.KMPObjFlowDataXml.ObjFlow objFlow = new TestXml.KMPObjFlowDataXml.ObjFlow
                        {
                            ObjectID = ObjFlowValue.item.ObjID,
                            ObjectName = ObjFlowValue.item.ObjFlowName1Text,
                            Path = MDLPath,
                            UseKCL = false,
                            ObjectType = "Unknown",
                            Commons = new TestXml.KMPObjFlowDataXml.ObjFlow.Common
                            {
                                ObjID = ObjFlowValue.item.ObjID,
                                ColType = ObjFlowValue.item.ColType,
                                PathType = ObjFlowValue.item.PathType,
                                ModelSetting = ObjFlowValue.item.ModelSetting,
                                Unknown1 = ObjFlowValue.item.Unk1
                            },
                            LODSetting = new TestXml.KMPObjFlowDataXml.ObjFlow.LOD_Setting
                            {
                                LOD = ObjFlowValue.item.LOD,
                                LODHPoly = ObjFlowValue.item.LODHPoly,
                                LODLPoly = ObjFlowValue.item.LODLPoly,
                                LODDef = ObjFlowValue.item.LODDef
                            },
                            Scales = new TestXml.KMPObjFlowDataXml.ObjFlow.Scale
                            {
                                X = ObjFlowValue.item.ObjX,
                                Y = ObjFlowValue.item.ObjY,
                                Z = ObjFlowValue.item.ObjZ
                            },
                            Names = new TestXml.KMPObjFlowDataXml.ObjFlow.Name
                            {
                                Main = ObjFlowValue.item.ObjFlowName1Text,
                                Sub = ObjFlowValue.item.ObjFlowName2Text
                            },
                            DefaultValues = new TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values
                            {
                                Values = null
                            }
                        };

                        #region Values
                        List<TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value> ValuesList = new List<TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value>();

                        for (int i = 0; i < 8; i++)
                        {
                            TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value value = new TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value
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

                    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.KMPObjFlowDataXml));
                    System.IO.StreamWriter sw = new StreamWriter(XmlPath, false, new System.Text.UTF8Encoding(false));
                    serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                    sw.Close();
                }

                public ObjFlowXmlToObject ReadObjFlowXml(string Path)
                {
                    System.IO.FileStream fs1 = new FileStream(Path, FileMode.Open, FileAccess.Read);
                    System.Xml.Serialization.XmlSerializer s1 = new System.Xml.Serialization.XmlSerializer(typeof(TestXml.KMPObjFlowDataXml));
                    TestXml.KMPObjFlowDataXml ObjFlowXml = (TestXml.KMPObjFlowDataXml)s1.Deserialize(fs1);

                    ObjFlowXmlToObject objFlowXmlToObject = new ObjFlowXmlToObject
                    {
                        ObjFlows = null
                    };

                    List<ObjFlowXmlToObject.ObjFlow> ObjFlow_List = new List<ObjFlowXmlToObject.ObjFlow>();

                    foreach(var ObjFlowData in ObjFlowXml.ObjFlows)
                    {
                        ObjFlowXmlToObject.ObjFlow objFlow = new ObjFlowXmlToObject.ObjFlow
                        {
                            ObjectID = ObjFlowData.ObjectID,
                            ObjectName = ObjFlowData.ObjectName,
                            Path = ObjFlowData.Path,
                            UseKCL = ObjFlowData.UseKCL,
                            ObjectType = ObjFlowData.ObjectType,
                            Commons = new ObjFlowXmlToObject.ObjFlow.Common
                            {
                                ObjID = ObjFlowData.Commons.ObjID,
                                ColType = ObjFlowData.Commons.ColType,
                                PathType = ObjFlowData.Commons.PathType,
                                ModelSetting = ObjFlowData.Commons.ModelSetting,
                                Unknown1 = ObjFlowData.Commons.Unknown1
                            },
                            LODSetting = new ObjFlowXmlToObject.ObjFlow.LOD_Setting
                            {
                                LOD = ObjFlowData.LODSetting.LOD,
                                LODHPoly = ObjFlowData.LODSetting.LODHPoly,
                                LODLPoly = ObjFlowData.LODSetting.LODLPoly,
                                LODDef = ObjFlowData.LODSetting.LODDef
                            },
                            Scales = new ObjFlowXmlToObject.ObjFlow.Scale
                            {
                                X = ObjFlowData.Scales.X,
                                Y = ObjFlowData.Scales.Y,
                                Z = ObjFlowData.Scales.Z
                            },
                            Names = new ObjFlowXmlToObject.ObjFlow.Name
                            {
                                Main = ObjFlowData.Names.Main,
                                Sub = ObjFlowData.Names.Sub
                            },
                            DefaultValues = new ObjFlowXmlToObject.ObjFlow.Default_Values
                            {
                                Values = null
                            }
                        };

                        List<ObjFlowXmlToObject.ObjFlow.Default_Values.Value> valueList = new List<ObjFlowXmlToObject.ObjFlow.Default_Values.Value>();

                        foreach(var ObjFlowDataValue in ObjFlowData.DefaultValues.Values)
                        {
                            ObjFlowXmlToObject.ObjFlow.Default_Values.Value value = new ObjFlowXmlToObject.ObjFlow.Default_Values.Value
                            {
                                DefaultObjectValue = ObjFlowDataValue.DefaultObjectValue,
                                Description = ObjFlowDataValue.Description
                            };

                            valueList.Add(value);
                        }

                        objFlow.DefaultValues.Values = valueList;

                        ObjFlow_List.Add(objFlow);
                    }

                    objFlowXmlToObject.ObjFlows = ObjFlow_List;

                    fs1.Close();
                    fs1.Dispose();

                    return objFlowXmlToObject;
                }

                public void WriteObjFlowXml(ObjFlowXmlPropertyGridSetting objFlowXmlToObject, string Path)
                {
                    TestXml.KMPObjFlowDataXml kMPObjFlowDataXml = new TestXml.KMPObjFlowDataXml
                    {
                        ObjFlows = null
                    };

                    List<TestXml.KMPObjFlowDataXml.ObjFlow> ObjFlowList = new List<TestXml.KMPObjFlowDataXml.ObjFlow>();

                    foreach (var ObjFlowValue in objFlowXmlToObject.ObjFlowsList.Select((item, index) => new { item, index }))
                    {
                        TestXml.KMPObjFlowDataXml.ObjFlow objFlow = new TestXml.KMPObjFlowDataXml.ObjFlow
                        {
                            ObjectID = ObjFlowValue.item.ObjectID,
                            ObjectName = ObjFlowValue.item.ObjectName,
                            Path = ObjFlowValue.item.Path,
                            UseKCL = ObjFlowValue.item.UseKCL,
                            ObjectType = ObjFlowValue.item.ObjectType,
                            Commons = new TestXml.KMPObjFlowDataXml.ObjFlow.Common
                            {
                                ObjID = ObjFlowValue.item.Commons.ObjID,
                                ColType = ObjFlowValue.item.Commons.ColType,
                                PathType = ObjFlowValue.item.Commons.PathType,
                                ModelSetting = ObjFlowValue.item.Commons.ModelSetting,
                                Unknown1 = ObjFlowValue.item.Commons.Unknown1
                            },
                            LODSetting = new TestXml.KMPObjFlowDataXml.ObjFlow.LOD_Setting
                            {
                                LOD = ObjFlowValue.item.LODSetting.LOD,
                                LODHPoly = ObjFlowValue.item.LODSetting.LODHPoly,
                                LODLPoly = ObjFlowValue.item.LODSetting.LODLPoly,
                                LODDef = ObjFlowValue.item.LODSetting.LODDef
                            },
                            Scales = new TestXml.KMPObjFlowDataXml.ObjFlow.Scale
                            {
                                X = Convert.ToInt32(ObjFlowValue.item.Scales.X),
                                Y = Convert.ToInt32(ObjFlowValue.item.Scales.Y),
                                Z = Convert.ToInt32(ObjFlowValue.item.Scales.Z)
                            },
                            Names = new TestXml.KMPObjFlowDataXml.ObjFlow.Name
                            {
                                Main = ObjFlowValue.item.Names.Main,
                                Sub = ObjFlowValue.item.Names.Sub
                            },
                            DefaultValues = new TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values
                            {
                                Values = null
                            }
                        };

                        #region Values
                        List<TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value> ValuesList = new List<TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value>();

                        foreach (var i in ObjFlowValue.item.DefaultValues.ValuesList)
                        {
                            TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value value = new TestXml.KMPObjFlowDataXml.ObjFlow.Default_Values.Value
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

                    System.Xml.Serialization.XmlSerializer serializer = new XmlSerializer(typeof(TestXml.KMPObjFlowDataXml));
                    System.IO.StreamWriter sw = new StreamWriter(Path, false, new System.Text.UTF8Encoding(false));
                    serializer.Serialize(sw, kMPObjFlowDataXml, xns);
                    sw.Close();
                }
            }

            public class Byte2StringConverter
            {
                //public byte[] ToByteArray(string InputString)
                //{
                //    string[] SplitStr = InputString.Split('-');
                //    byte[] Str2byte = new byte[SplitStr.Length];
                //    for (int n = 0; n < SplitStr.Length; n++)
                //    {
                //        byte b = byte.Parse(SplitStr[n], System.Globalization.NumberStyles.AllowHexSpecifier);
                //        Str2byte[n] = b;
                //    }

                //    return Str2byte;
                //}

                public byte[] OBJIDStrToByteArray(string InputString_OBJID)
                {
                    string w1 = InputString_OBJID.Substring(0, 2);
                    string w2 = InputString_OBJID.Substring(2);

                    string[] SplitStr = new string[] { w2, w1 };
                    byte[] Str2byte = new byte[SplitStr.Length];
                    for (int n = 0; n < SplitStr.Length; n++)
                    {
                        byte b = byte.Parse(SplitStr[n], System.Globalization.NumberStyles.AllowHexSpecifier);
                        Str2byte[n] = b;
                    }

                    return Str2byte;
                }
            }

            public class FlagConverter : KMPHelper
            {
                public class EnemyRoute
                {
                    #region RouteSetting(I'm using the code in "KMPExpander-master\KMPExpander\Class\SimpleKMPs\EnemyRoutes.cs" of "KMP Expander")
                    public byte Flags { get; set; }
                    public bool WideTurn
                    {
                        get
                        {
                            return (Flags & 0x1) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 0)) | ((value ? 1 : 0) << 0));
                        }
                    }

                    public bool NormalTurn
                    {
                        get
                        {
                            return (Flags & 0x4) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 2)) | ((value ? 1 : 0) << 2));
                        }
                    }

                    public bool SharpTurn
                    {
                        get
                        {
                            return (Flags & 0x10) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 4)) | ((value ? 1 : 0) << 4));
                        }
                    }

                    public bool TricksForbidden
                    {
                        get
                        {
                            return (Flags & 0x8) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 3)) | ((value ? 1 : 0) << 3));
                        }
                    }

                    public bool StickToRoute
                    {
                        get
                        {
                            return (Flags & 0x40) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 6)) | ((value ? 1 : 0) << 6));
                        }
                    }

                    public bool BouncyMushSection
                    {
                        get
                        {
                            return (Flags & 0x20) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 5)) | ((value ? 1 : 0) << 5));
                        }
                    }

                    public bool ForceDefaultSpeed
                    {
                        get
                        {
                            return (Flags & 0x80) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 7)) | ((value ? 1 : 0) << 7));
                        }
                    }

                    public bool UnknownFlag
                    {
                        get
                        {
                            return (Flags & 0x2) != 0;
                        }
                        set
                        {
                            Flags = (byte)((Flags & ~(1 << 1)) | ((value ? 1 : 0) << 1));
                        }
                    }
                    #endregion

                    public enum FlagType
                    {
                        WideTurn,
                        NormalTurn,
                        SharpTurn,
                        TricksForbidden,
                        StickToRoute,
                        BouncyMushSection,
                        ForceDefaultSpeed,
                        UnknownFlag
                    }

                    public bool ConvertFlags(byte InputFlags, FlagType flagType)
                    {
                        Flags = InputFlags;

                        bool FlagValue = new bool();
                        if (flagType == FlagType.WideTurn)
                        {
                            FlagValue = WideTurn;
                        }
                        if (flagType == FlagType.NormalTurn)
                        {
                            FlagValue = NormalTurn;
                        }
                        if (flagType == FlagType.SharpTurn)
                        {
                            FlagValue = SharpTurn;
                        }
                        if (flagType == FlagType.TricksForbidden)
                        {
                            FlagValue = TricksForbidden;
                        }
                        if (flagType == FlagType.StickToRoute)
                        {
                            FlagValue = StickToRoute;
                        }
                        if (flagType == FlagType.BouncyMushSection)
                        {
                            FlagValue = BouncyMushSection;
                        }
                        if (flagType == FlagType.ForceDefaultSpeed)
                        {
                            FlagValue = ForceDefaultSpeed;
                        }
                        if (flagType == FlagType.UnknownFlag)
                        {
                            FlagValue = UnknownFlag;
                        }

                        return FlagValue;
                    }
                }

                public class GlideRoute
                {
                    #region RouteSetting(I'm using the code in "KMPExpander-master\KMPExpander\Class\SimpleKMPs\GliderRoutes.cs" of "KMP Expander")
                    public uint RouteSettings { get; set; }
                    public bool ForceToRoute
                    {
                        get
                        {
                            return (RouteSettings & 0xFF) != 0;
                        }
                        set
                        {
                            RouteSettings = (RouteSettings & ~0xFFu) | (value ? 1u : 0u);
                        }
                    }

                    public bool CannonSection
                    {
                        get
                        {
                            return (RouteSettings & 0xFF00) != 0;
                        }
                        set
                        {
                            RouteSettings = (RouteSettings & ~0xFF00u) | (value ? 1u : 0u) << 8;
                        }
                    }

                    public bool PreventRaising
                    {
                        get
                        {
                            return (RouteSettings & 0xFF0000) != 0;
                        }
                        set
                        {
                            RouteSettings = (RouteSettings & ~0xFF0000u) | (value ? 1u : 0u) << 16;
                        }
                    }
                    #endregion

                    public enum FlagType
                    {
                        ForceToRoute,
                        CannonSection,
                        PreventRaising
                    }

                    public bool ConvertFlags(uint InputFlags, FlagType flagType)
                    {
                        RouteSettings = InputFlags;

                        bool FlagValue = new bool();
                        if (flagType == FlagType.ForceToRoute)
                        {
                            FlagValue = ForceToRoute;
                        }
                        if (flagType == FlagType.CannonSection)
                        {
                            FlagValue = CannonSection;
                        }
                        if (flagType == FlagType.PreventRaising)
                        {
                            FlagValue = PreventRaising;
                        }

                        return FlagValue;
                    }
                }
            }

            public class KMPValueTypeConverter : KMPHelper
            {
                public class EnemyRoute
                {
                    public enum MushSetting
                    {
                        CanUseMushroom = 0,
                        NeedsMushroom = 1,
                        CannotUseMushroom = 2,
                        Unknown
                    }

                    public static MushSetting MushSettingType(ushort Value)
                    {
                        MushSetting mushSetting;
                        if (Value > 2)
                        {
                            mushSetting = MushSetting.Unknown;
                        }
                        else
                        {
                            mushSetting = (MushSetting)Value;
                        }

                        return mushSetting;
                    }

                    public enum DriftSetting
                    {
                        AllowDrift_AllowMiniturbo,
                        DisallowDrift_AllowMiniturbo,
                        DisallowDrift_DisallowMiniturbo,
                        Unknown
                    }

                    public static DriftSetting DriftSettingType(ushort Value)
                    {
                        DriftSetting driftSetting;
                        if (Value > 2)
                        {
                            driftSetting = DriftSetting.Unknown;
                        }
                        else
                        {
                            driftSetting = (DriftSetting)Value;
                        }

                        return driftSetting;
                    }

                    public enum PathFindOption
                    {
                        Taken_under_unknown_flag2 = -4,
                        Taken_under_unknown_flag1 = -3,
                        Bullet_cannot_find = -2,
                        CPU_Racer_cannot_find = -1,
                        No_restrictions = 0,
                        Unknown
                    }

                    public static PathFindOption PathFindOptionType(short Value)
                    {
                        PathFindOption pathFindOption;
                        if (Value > 0 || Value < -4)
                        {
                            pathFindOption = PathFindOption.Unknown;
                        }
                        else
                        {
                            pathFindOption = (PathFindOption)Value;
                        }

                        return pathFindOption;
                    }

                    public enum MaxSearchYOffsetOption
                    {
                        Limited_offset_MinusOne = -1,
                        No_limited_offset = 0,
                        Limited_offset
                    }

                    public static MaxSearchYOffsetOption MaxSearchYOffsetOptionType(short Value)
                    {
                        MaxSearchYOffsetOption maxSearchYOffsetOption;
                        if (Value < 0)
                        {
                            maxSearchYOffsetOption = MaxSearchYOffsetOption.Limited_offset_MinusOne;
                        }
                        else if(Value > 0)
                        {
                            maxSearchYOffsetOption = MaxSearchYOffsetOption.Limited_offset;
                        }
                        else
                        {
                            maxSearchYOffsetOption = MaxSearchYOffsetOption.No_limited_offset;
                        }

                        return maxSearchYOffsetOption;
                    }
                }

                public class ItemRoute
                {
                    public enum GravityMode
                    {
                        Affected_By_Gravity = 0,
                        Unaffected_By_Gravity = 1,
                        Cannon_Section = 2,
                        Unknown
                    }

                    public static GravityMode GravityModeType(ushort Value)
                    {
                        GravityMode gravityMode;
                        if (Value > 2)
                        {
                            gravityMode = GravityMode.Unknown;
                        }
                        else
                        {
                            gravityMode = (GravityMode)Value;
                        }

                        return gravityMode;
                    }

                    public enum PlayerScanRadius
                    {
                        Small = 0,
                        Big = 1,
                        Unknown
                    }

                    public static PlayerScanRadius PlayerScanRadiusType(ushort Value)
                    {
                        PlayerScanRadius playerScanRadius;
                        if (Value > 1)
                        {
                            playerScanRadius = PlayerScanRadius.Unknown;
                        }
                        else
                        {
                            playerScanRadius = (PlayerScanRadius)Value;
                        }

                        return playerScanRadius;
                    }
                }

                public class Area
                {
                    public enum AreaMode
                    {
                        Box = 0,
                        Cylinder = 1,
                        Unknown
                    }

                    public static AreaMode AreaModes(byte Value)
                    {
                        AreaMode areaMode;
                        if (Value > 1)
                        {
                            areaMode = AreaMode.Unknown;
                        }
                        else
                        {
                            areaMode = (AreaMode)Value;
                        }

                        return areaMode;

                    }
                }
            }
        }

        public class KMPWriter : KMPs
        {
            public uint Write_TPTK(BinaryWriter bw, KMPFormat.KMPSection.TPTK_Section TPTK)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                bw.Write(TPTK.TPTKHeader);
                bw.Write(TPTK.NumOfEntries);
                bw.Write(TPTK.AdditionalValue);

                for(int Count = 0; Count < TPTK.NumOfEntries; Count++)
                {
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Position)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Position)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Position)[2]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Rotation)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Rotation)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTK.TPTKValue_List[Count].TPTK_Rotation)[2]);
                    bw.Write(TPTK.TPTKValue_List[Count].Player_Index);
                    bw.Write(TPTK.TPTKValue_List[Count].TPTK_UnkBytes);
                }

                return WritePosition;
            }

            public class TPNE_HPNE_WritePosition
            {
                public uint TPNE { get; set; }
                public uint HPNE { get; set; }
            }

            public TPNE_HPNE_WritePosition Write_TPNE_HPNE(BinaryWriter bw, KMPFormat.KMPSection.TPNE_Section TPNE, KMPFormat.KMPSection.HPNE_Section HPNE)
            {
                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                TPNE_HPNE_WritePosition tPNE_HPNE_WritePosition = new TPNE_HPNE_WritePosition
                {
                    TPNE = 0,
                    HPNE = 0
                };

                #region TPNE
                tPNE_HPNE_WritePosition.TPNE = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPNE.TPNEHeader);
                bw.Write(TPNE.NumOfEntries);
                bw.Write(TPNE.AdditionalValue);

                for (int Count = 0; Count < TPNE.TPNEValue_List.Count; Count++)
                {
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE.TPNEValue_List[Count].TPNE_Position)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE.TPNEValue_List[Count].TPNE_Position)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPNE.TPNEValue_List[Count].TPNE_Position)[2]);
                    bw.Write(TPNE.TPNEValue_List[Count].Control);
                    bw.Write(TPNE.TPNEValue_List[Count].MushSetting);
                    bw.Write(TPNE.TPNEValue_List[Count].DriftSetting);
                    bw.Write(TPNE.TPNEValue_List[Count].Flags);
                    bw.Write(TPNE.TPNEValue_List[Count].PathFindOption);
                    bw.Write(TPNE.TPNEValue_List[Count].MaxSearchYOffset);
                }
                #endregion

                #region HPNE
                tPNE_HPNE_WritePosition.HPNE = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(HPNE.HPNEHeader);
                bw.Write(HPNE.NumOfEntries);
                bw.Write(HPNE.AdditionalValue);

                for (int Count = 0; Count < HPNE.NumOfEntries; Count++)
                {
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_StartPoint);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_Length);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev0);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev1);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev2);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev3);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev4);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev5);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev6);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev7);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev8);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev9);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev10);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev11);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev12);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev13);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev14);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_PreviewGroup.Prev15);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next0);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next1);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next2);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next3);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next4);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next5);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next6);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next7);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next8);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next9);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next10);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next11);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next12);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next13);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next14);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_NextGroup.Next15);
                    bw.Write(HPNE.HPNEValue_List[Count].HPNE_UnkBytes1);
                }
                #endregion

                return tPNE_HPNE_WritePosition;
            }

            public class TPTI_HPTI_WritePosition
            {
                public uint TPTI { get; set; }
                public uint HPTI { get; set; }
            }

            public TPTI_HPTI_WritePosition Write_TPTI_HPTI(BinaryWriter bw, KMPFormat.KMPSection.TPTI_Section TPTI, KMPFormat.KMPSection.HPTI_Section HPTI)
            {
                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                TPTI_HPTI_WritePosition tPTI_HPTI_WritePosition = new TPTI_HPTI_WritePosition
                {
                    TPTI = 0,
                    HPTI = 0
                };

                #region TPTI
                tPTI_HPTI_WritePosition.TPTI = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPTI.TPTIHeader);
                bw.Write(TPTI.NumOfEntries);
                bw.Write(TPTI.AdditionalValue);

                for (int Count = 0; Count < TPTI.TPTIValue_List.Count; Count++)
                {
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI.TPTIValue_List[Count].TPTI_Position)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI.TPTIValue_List[Count].TPTI_Position)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPTI.TPTIValue_List[Count].TPTI_Position)[2]);
                    bw.Write(TPTI.TPTIValue_List[Count].TPTI_PointSize);
                    bw.Write(TPTI.TPTIValue_List[Count].GravityMode);
                    bw.Write(TPTI.TPTIValue_List[Count].PlayerScanRadius);
                }
                #endregion

                #region HPTI
                tPTI_HPTI_WritePosition.HPTI = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(HPTI.HPTIHeader);
                bw.Write(HPTI.NumOfEntries);
                bw.Write(HPTI.AdditionalValue);

                for (int Count = 0; Count < HPTI.NumOfEntries; Count++)
                {
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_StartPoint);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_Length);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev0);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev1);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev2);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev3);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev4);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_PreviewGroup.Prev5);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next0);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next1);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next2);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next3);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next4);
                    bw.Write(HPTI.HPTIValue_List[Count].HPTI_NextGroup.Next5);
                }
                #endregion

                return tPTI_HPTI_WritePosition;
            }

            public class TPKC_HPKC_WritePosition
            {
                public uint TPKC { get; set; }
                public uint HPKC { get; set; }
            }

            public TPKC_HPKC_WritePosition Write_TPKC_HPKC(BinaryWriter bw, KMPFormat.KMPSection.TPKC_Section TPKC, KMPFormat.KMPSection.HPKC_Section HPKC)
            {
                KMPHelper.Vector3DTo2DConverter Vector2DToByteArrayConverter = new KMPHelper.Vector3DTo2DConverter();

                TPKC_HPKC_WritePosition tPKC_HPKC_WritePosition = new TPKC_HPKC_WritePosition
                {
                    TPKC = 0,
                    HPKC = 0
                };

                #region TPKC
                tPKC_HPKC_WritePosition.TPKC = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPKC.TPKCHeader);
                bw.Write(TPKC.NumOfEntries);
                bw.Write(TPKC.AdditionalValue);

                for (int Count = 0; Count < TPKC.TPKCValue_List.Count; Count++)
                {
                    bw.Write(Vector2DToByteArrayConverter.Vector2ToByteArray(TPKC.TPKCValue_List[Count].TPKC_2DPosition_Left)[0]);
                    bw.Write(Vector2DToByteArrayConverter.Vector2ToByteArray(TPKC.TPKCValue_List[Count].TPKC_2DPosition_Left)[1]);
                    bw.Write(Vector2DToByteArrayConverter.Vector2ToByteArray(TPKC.TPKCValue_List[Count].TPKC_2DPosition_Right)[0]);
                    bw.Write(Vector2DToByteArrayConverter.Vector2ToByteArray(TPKC.TPKCValue_List[Count].TPKC_2DPosition_Right)[1]);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_RespawnID);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_Checkpoint_Type);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_PreviousCheckPoint);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_NextCheckPoint);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_UnkBytes1);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_UnkBytes2);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_UnkBytes3);
                    bw.Write(TPKC.TPKCValue_List[Count].TPKC_UnkBytes4);

                }
                #endregion

                #region HPKC
                tPKC_HPKC_WritePosition.HPKC = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(HPKC.HPKCHeader);
                bw.Write(HPKC.NumOfEntries);
                bw.Write(HPKC.AdditionalValue);

                for (int Count = 0; Count < HPKC.NumOfEntries; Count++)
                {
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_StartPoint);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_Length);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev0);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev1);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev2);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev3);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev4);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_PreviewGroup.Prev5);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next0);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next1);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next2);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next3);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next4);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_NextGroup.Next5);
                    bw.Write(HPKC.HPKCValue_List[Count].HPKC_UnkBytes1);
                }
                #endregion

                return tPKC_HPKC_WritePosition;
            }

            public uint Write_JBOG(BinaryWriter bw, KMPFormat.KMPSection.JBOG_Section JBOG)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                bw.Write(JBOG.JBOGHeader);
                bw.Write(JBOG.NumOfEntries);
                bw.Write(JBOG.AdditionalValue);

                for (int Count = 0; Count < JBOG.NumOfEntries; Count++)
                {
                    bw.Write(JBOG.JBOGValue_List[Count].ObjectID);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_UnkByte1);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Position)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Position)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Position)[2]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Rotation)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Rotation)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Rotation)[2]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Scale)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Scale)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(JBOG.JBOGValue_List[Count].JBOG_Scale)[2]);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_ITOP_RouteIDIndex);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value0);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value1);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value2);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value3);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value4);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value5);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value6);
                    bw.Write(JBOG.JBOGValue_List[Count].JOBJ_Specific_Setting.Value7);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_PresenceSetting);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_UnkByte2);
                    bw.Write(JBOG.JBOGValue_List[Count].JBOG_UnkByte3);
                }

                return WritePosition;
            }

            public uint Write_ITOP(BinaryWriter bw, KMPFormat.KMPSection.ITOP_Section ITOP)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                bw.Write(ITOP.ITOPHeader);
                bw.Write(ITOP.ITOP_NumberOfRoute);
                bw.Write(ITOP.ITOP_NumberOfPoint);

                for (int ITOP_RoutesCount = 0; ITOP_RoutesCount < ITOP.ITOP_NumberOfRoute; ITOP_RoutesCount++)
                {
                    bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Route_NumOfPoint);
                    bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_RouteSetting1);
                    bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_RouteSetting2);

                    for (int ITOP_PointsCount = 0; ITOP_PointsCount < ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Route_NumOfPoint; ITOP_PointsCount++)
                    {
                        bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position)[0]);
                        bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position)[1]);
                        bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_Position)[2]);
                        bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_Point_RouteSpeed);
                        bw.Write(ITOP.ITOP_Route_List[ITOP_RoutesCount].ITOP_Point_List[ITOP_PointsCount].ITOP_PointSetting2);
                    }
                }

                return WritePosition;
            }

            public uint Write_AERA(BinaryWriter bw, KMPFormat.KMPSection.AERA_Section AERA)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                bw.Write(AERA.AERAHeader);
                bw.Write(AERA.NumOfEntries);
                bw.Write(AERA.AdditionalValue);

                for (int Count = 0; Count < AERA.NumOfEntries; Count++)
                {
                    bw.Write(AERA.AERAValue_List[Count].AreaMode);
                    bw.Write(AERA.AERAValue_List[Count].AreaType);
                    bw.Write(AERA.AERAValue_List[Count].AERA_EMACIndex);
                    bw.Write(AERA.AERAValue_List[Count].Priority);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Position)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Position)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Position)[2]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Rotation)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Rotation)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Rotation)[2]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Scale)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Scale)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(AERA.AERAValue_List[Count].AERA_Scale)[2]);
                    bw.Write(AERA.AERAValue_List[Count].AERA_UnkByte1);
                    bw.Write(AERA.AERAValue_List[Count].AERA_UnkByte2);
                    bw.Write(AERA.AERAValue_List[Count].RouteID);
                    bw.Write(AERA.AERAValue_List[Count].EnemyID);
                    bw.Write(AERA.AERAValue_List[Count].AERA_UnkByte4);
                }

                return WritePosition;
            }

            public uint Write_EMAC(BinaryWriter bw, KMPFormat.KMPSection.EMAC_Section EMAC)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                bw.Write(EMAC.EMACHeader);
                bw.Write(EMAC.NumOfEntries);
                bw.Write(EMAC.AdditionalValue);

                for (int Count = 0; Count < EMAC.NumOfEntries; Count++)
                {
                    bw.Write(EMAC.EMACValue_List[Count].CameraType);
                    bw.Write(EMAC.EMACValue_List[Count].NextCameraIndex);
                    bw.Write(EMAC.EMACValue_List[Count].EMAC_UnkBytes1);
                    bw.Write(EMAC.EMACValue_List[Count].EMAC_ITOP_CameraIndex);
                    bw.Write(EMAC.EMACValue_List[Count].RouteSpeed);
                    bw.Write(EMAC.EMACValue_List[Count].FOVSpeed);
                    bw.Write(EMAC.EMACValue_List[Count].ViewpointSpeed);
                    bw.Write(EMAC.EMACValue_List[Count].EMAC_UnkBytes2);
                    bw.Write(EMAC.EMACValue_List[Count].EMAC_UnkBytes3);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Position)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Position)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Position)[2]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Rotation)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Rotation)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].EMAC_Rotation)[2]);
                    bw.Write(EMAC.EMACValue_List[Count].FOVAngle_Start);
                    bw.Write(EMAC.EMACValue_List[Count].FOVAngle_End);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Start)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Start)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Start)[2]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Destination)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Destination)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(EMAC.EMACValue_List[Count].Viewpoint_Destination)[2]);
                    bw.Write(EMAC.EMACValue_List[Count].Camera_Active_Time);
                }

                return WritePosition;
            }

            public uint Write_TPGJ(BinaryWriter bw, KMPFormat.KMPSection.TPGJ_Section TPGJ)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                bw.Write(TPGJ.TPGJHeader);
                bw.Write(TPGJ.NumOfEntries);
                bw.Write(TPGJ.AdditionalValue);

                for (int Count = 0; Count < TPGJ.NumOfEntries; Count++)
                {
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Position)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Position)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Position)[2]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Rotation)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Rotation)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPGJ.TPGJValue_List[Count].TPGJ_Rotation)[2]);
                    bw.Write(TPGJ.TPGJValue_List[Count].TPGJ_RespawnID);
                    bw.Write(TPGJ.TPGJValue_List[Count].TPGJ_UnkBytes1);
                }

                return WritePosition;
            }

            //Unused Section
            public uint Write_TPNC(BinaryWriter bw, KMPFormat.KMPSection.TPNC_Section TPNC)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPNC.TPNCHeader);
                bw.Write(TPNC.NumOfEntries);
                bw.Write(TPNC.AdditionalValue);

                return WritePosition;
            }

            //Unused Section
            public uint Write_TPSM(BinaryWriter bw, KMPFormat.KMPSection.TPSM_Section TPSM)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPSM.TPSMHeader);
                bw.Write(TPSM.NumOfEntries);
                bw.Write(TPSM.AdditionalValue);

                return WritePosition;
            }

            public uint Write_IGTS(BinaryWriter bw, KMPFormat.KMPSection.IGTS_Section IGTS)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(IGTS.IGTSHeader);
                bw.Write(IGTS.Unknown1);
                bw.Write(IGTS.LapCount);
                bw.Write(IGTS.PolePosition);
                bw.Write(IGTS.Unknown2);
                bw.Write(IGTS.Unknown3);
                bw.Write(IGTS.RGBAColor.R);
                bw.Write(IGTS.RGBAColor.G);
                bw.Write(IGTS.RGBAColor.B);
                bw.Write(IGTS.RGBAColor.A);
                bw.Write(IGTS.FlareAlpha);
                return WritePosition;
            }

            //Unused Section
            public uint Write_SROC(BinaryWriter bw, KMPFormat.KMPSection.SROC_Section SROC)
            {
                uint WritePosition = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(SROC.SROCHeader);
                bw.Write(SROC.NumOfEntries);
                bw.Write(SROC.AdditionalValue);

                return WritePosition;
            }

            public class TPLG_HPLG_WritePosition
            {
                public uint TPLG { get; set; }
                public uint HPLG { get; set; }
            }

            public TPLG_HPLG_WritePosition Write_TPLG_HPLG(BinaryWriter bw, KMPFormat.KMPSection.TPLG_Section TPLG, KMPFormat.KMPSection.HPLG_Section HPLG)
            {
                TPLG_HPLG_WritePosition tPLG_HPLG_WritePosition = new TPLG_HPLG_WritePosition
                {
                    TPLG = 0,
                    HPLG = 0
                };

                #region TPLG
                KMPHelper.Vector3DToByteArrayConverter Vector3DToByteArrayConverter = new KMPHelper.Vector3DToByteArrayConverter();

                tPLG_HPLG_WritePosition.TPLG = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(TPLG.TPLGHeader);
                bw.Write(TPLG.NumOfEntries);
                bw.Write(TPLG.AdditionalValue);

                for (int Count = 0; Count < TPLG.NumOfEntries; Count++)
                {
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG.TPLGValue_List[Count].TPLG_Position)[0]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG.TPLGValue_List[Count].TPLG_Position)[1]);
                    bw.Write(Vector3DToByteArrayConverter.Vector3DToByteArray(TPLG.TPLGValue_List[Count].TPLG_Position)[2]);
                    bw.Write(TPLG.TPLGValue_List[Count].TPLG_PointScaleValue);
                    bw.Write(TPLG.TPLGValue_List[Count].TPLG_UnkBytes1);
                    bw.Write(TPLG.TPLGValue_List[Count].TPLG_UnkBytes2);
                }
                #endregion

                #region HPKC
                tPLG_HPLG_WritePosition.HPLG = Convert.ToUInt32(bw.BaseStream.Position);

                bw.Write(HPLG.HPLGHeader);
                bw.Write(HPLG.NumOfEntries);
                bw.Write(HPLG.AdditionalValue);

                for (int Count = 0; Count < HPLG.NumOfEntries; Count++)
                {
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_StartPoint);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_Length);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev0);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev1);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev2);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev3);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev4);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_PreviewGroup.Prev5);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next0);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next1);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next2);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next3);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next4);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_NextGroup.Next5);
                    bw.Write(HPLG.HPLGValue_List[Count].RouteSetting);
                    bw.Write(HPLG.HPLGValue_List[Count].HPLG_UnkBytes2);
                }
                #endregion

                return tPLG_HPLG_WritePosition;
            }
        }
    }
}
