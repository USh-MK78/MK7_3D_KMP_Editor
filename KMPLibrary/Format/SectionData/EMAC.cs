using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// EMAC (Camera Section)
    /// </summary>
    public class EMAC
    {
        public char[] EMACHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        public List<EMACValue> EMACValue_List { get; set; }
        public class EMACValue
        {
            public byte CameraType { get; set; }
            public byte NextCameraIndex { get; set; }
            public byte EMAC_NextVideoIndex { get; set; }
            public byte EMAC_ITOP_CameraIndex { get; set; }
            public ushort RouteSpeed { get; set; }
            public ushort FOVSpeed { get; set; }
            public ushort ViewpointSpeed { get; set; }
            public byte EMAC_StartFlag { get; set; }
            public byte EMAC_VideoFlag { get; set; }
            public Vector3D EMAC_Position { get; set; }
            public Vector3D EMAC_Rotation { get; set; }
            public float FOVAngle_Start { get; set; }
            public float FOVAngle_End { get; set; }
            public Vector3D Viewpoint_Start { get; set; }
            public Vector3D Viewpoint_Destination { get; set; }
            public float Camera_Active_Time { get; set; }

            public void ReadEMACValue(BinaryReader br)
            {
                CameraType = br.ReadByte();
                NextCameraIndex = br.ReadByte();
                EMAC_NextVideoIndex = br.ReadByte();
                EMAC_ITOP_CameraIndex = br.ReadByte();
                RouteSpeed = br.ReadUInt16();
                FOVSpeed = br.ReadUInt16();
                ViewpointSpeed = br.ReadUInt16();
                EMAC_StartFlag = br.ReadByte();
                EMAC_VideoFlag = br.ReadByte();
                EMAC_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                EMAC_Rotation = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                FOVAngle_Start = br.ReadSingle();
                FOVAngle_End = br.ReadSingle();
                Viewpoint_Start = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                Viewpoint_Destination = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                Camera_Active_Time = br.ReadSingle();
            }

            public void WriteEMACValue(BinaryWriter bw)
            {
                bw.Write(CameraType);
                bw.Write(NextCameraIndex);
                bw.Write(EMAC_NextVideoIndex);
                bw.Write(EMAC_ITOP_CameraIndex);
                bw.Write(RouteSpeed);
                bw.Write(FOVSpeed);
                bw.Write(ViewpointSpeed);
                bw.Write(EMAC_StartFlag);
                bw.Write(EMAC_VideoFlag);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(EMAC_Position)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(EMAC_Position)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(EMAC_Position)[2]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(EMAC_Rotation)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(EMAC_Rotation)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(EMAC_Rotation)[2]);
                bw.Write(FOVAngle_Start);
                bw.Write(FOVAngle_End);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(Viewpoint_Start)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(Viewpoint_Start)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(Viewpoint_Start)[2]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(Viewpoint_Destination)[0]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(Viewpoint_Destination)[1]);
                bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(Viewpoint_Destination)[2]);
                bw.Write(Camera_Active_Time);
            }

            public EMACValue()
            {
                CameraType = 0x00;
                NextCameraIndex = 0x00;
                EMAC_NextVideoIndex = 0x00;
                EMAC_ITOP_CameraIndex = 0x00;
                RouteSpeed = 0;
                FOVSpeed = 0;
                ViewpointSpeed = 0;
                EMAC_StartFlag = 0x00;
                EMAC_VideoFlag = 0x00;
                EMAC_Position = new Vector3D(0, 0, 0);
                EMAC_Rotation = new Vector3D(0, 0, 0);
                FOVAngle_Start = 0f;
                FOVAngle_End = 0f;
                Viewpoint_Start = new Vector3D(0, 0, 0);
                Viewpoint_Destination = new Vector3D(0, 0, 0);
                Camera_Active_Time = 0f;
            }
        }

        public void ReadEMAC(BinaryReader br)
        {
            EMACHeader = br.ReadChars(4);
            if (new string(EMACHeader) != "EMAC") throw new Exception("Error : EMAC");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();

            for (int EMACCount = 0; EMACCount < NumOfEntries; EMACCount++)
            {
                EMACValue eMACValue = new EMACValue();
                eMACValue.ReadEMACValue(br);
                EMACValue_List.Add(eMACValue);
            }
        }

        public void WriteEMAC(BinaryWriter bw)
        {
            bw.Write(EMACHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);

            for (int Count = 0; Count < NumOfEntries; Count++)
            {
                EMACValue_List[Count].WriteEMACValue(bw);
            }
        }

        /// <summary>
        /// Initialize EMAC (Write)
        /// </summary>
        /// <param name="EMACValueList"></param>
        /// <param name="AdditionalValue">Default : 0xFFFF</param>
        public EMAC(List<EMACValue> EMACValueList, ushort AdditionalValue = 65535)
        {
            EMACHeader = "EMAC".ToCharArray();
            NumOfEntries = Convert.ToUInt16(EMACValueList.Count);
            this.AdditionalValue = AdditionalValue;
            EMACValue_List = EMACValueList;
        }

        /// <summary>
        /// Initialize EMAC (Read)
        /// </summary>
        public EMAC()
        {
            EMACHeader = new char[4];
            NumOfEntries = 0;
            AdditionalValue = 0;
            EMACValue_List = new List<EMACValue>();
        }

        //public EMAC()
        //{
        //    EMACHeader = "EMAC".ToCharArray();
        //    NumOfEntries = 0;
        //    AdditionalValue = 0;
        //    EMACValue_List = new List<EMACValue>();
        //}
    }
}
