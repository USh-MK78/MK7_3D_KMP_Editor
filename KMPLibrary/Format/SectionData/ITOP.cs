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
    /// ITOP (Route)
    /// </summary>
    public class ITOP
    {
        public char[] ITOPHeader { get; set; }
        public ushort ITOP_NumberOfRoute { get; set; }
        public ushort ITOP_NumberOfPoint { get; set; }
        public List<ITOP_Route> ITOP_Route_List { get; set; }
        public class ITOP_Route
        {
            public ushort ITOP_Route_NumOfPoint { get; set; }
            public byte ITOP_LoopSetting { get; set; }
            public byte ITOP_SmoothSetting { get; set; }
            public List<ITOP_Point> ITOP_Point_List { get; set; }
            public class ITOP_Point
            {
                public Vector3D ITOP_Point_Position { get; set; }
                public ushort ITOP_Point_RouteSpeed { get; set; }
                public ushort ITOP_PointSetting2 { get; set; }

                public void ReadITOP_Point(BinaryReader br)
                {
                    ITOP_Point_Position = KMPHelper.Converter3D.ByteArrayToVector3D(new byte[][] { br.ReadBytes(4), br.ReadBytes(4), br.ReadBytes(4) });
                    ITOP_Point_RouteSpeed = br.ReadUInt16();
                    ITOP_PointSetting2 = br.ReadUInt16();
                }

                public void WriteITOP_Point(BinaryWriter bw)
                {
                    bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(ITOP_Point_Position)[0]);
                    bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(ITOP_Point_Position)[1]);
                    bw.Write(KMPHelper.Converter3D.Vector3DToByteArray(ITOP_Point_Position)[2]);
                    bw.Write(ITOP_Point_RouteSpeed);
                    bw.Write(ITOP_PointSetting2);
                }

                public ITOP_Point(Vector3D Position, ushort RouteSpeed, ushort PointSetting2)
                {
                    this.ITOP_Point_Position = Position;
                    this.ITOP_Point_RouteSpeed = RouteSpeed;
                    this.ITOP_PointSetting2 = PointSetting2;
                }

                public ITOP_Point()
                {
                    ITOP_Point_Position = new Vector3D(0, 0, 0);
                    ITOP_Point_RouteSpeed = 0;
                    ITOP_PointSetting2 = 0;
                }
            }

            public void ReadITOPRoute(BinaryReader br)
            {
                ITOP_Route_NumOfPoint = br.ReadUInt16();
                ITOP_LoopSetting = br.ReadByte();
                ITOP_SmoothSetting = br.ReadByte();

                for (int ITOP_PointCount = 0; ITOP_PointCount < ITOP_Route_NumOfPoint; ITOP_PointCount++)
                {
                    ITOP_Point iTOP_Point = new ITOP_Point();
                    iTOP_Point.ReadITOP_Point(br);
                    ITOP_Point_List.Add(iTOP_Point);
                }
            }

            public void WriteITOPRoute(BinaryWriter bw)
            {
                bw.Write(ITOP_Route_NumOfPoint);
                bw.Write(ITOP_LoopSetting);
                bw.Write(ITOP_SmoothSetting);

                for (int ITOP_PointsCount = 0; ITOP_PointsCount < ITOP_Route_NumOfPoint; ITOP_PointsCount++)
                {
                    ITOP_Point_List[ITOP_PointsCount].WriteITOP_Point(bw);
                }
            }

            public ITOP_Route(byte LoopSetting, byte SmoothSetting, List<ITOP_Point> ITOP_Point_List)
            {
                ITOP_Route_NumOfPoint = Convert.ToUInt16(ITOP_Point_List.Count);
                ITOP_LoopSetting = LoopSetting;
                ITOP_SmoothSetting = SmoothSetting;
                this.ITOP_Point_List = ITOP_Point_List;
            }

            public ITOP_Route()
            {
                ITOP_Route_NumOfPoint = 0;
                ITOP_LoopSetting = 0x00;
                ITOP_SmoothSetting = 0x00;
                ITOP_Point_List = new List<ITOP_Point>();
            }
        }

        public void ReadITOP(BinaryReader br)
        {
            ITOPHeader = br.ReadChars(4);
            if (new string(ITOPHeader) != "ITOP") throw new Exception("Error : ITOP");

            ITOP_NumberOfRoute = br.ReadUInt16();
            ITOP_NumberOfPoint = br.ReadUInt16();

            for (int ITOPRouteCount = 0; ITOPRouteCount < ITOP_NumberOfRoute; ITOPRouteCount++)
            {
                ITOP_Route iTOP_Route = new ITOP_Route();
                iTOP_Route.ReadITOPRoute(br);
                ITOP_Route_List.Add(iTOP_Route);
            }
        }

        public void WriteITOP(BinaryWriter bw)
        {
            bw.Write(ITOPHeader);
            bw.Write(ITOP_NumberOfRoute);
            bw.Write(ITOP_NumberOfPoint);

            for (int ITOP_RoutesCount = 0; ITOP_RoutesCount < ITOP_NumberOfRoute; ITOP_RoutesCount++)
            {
                ITOP_Route_List[ITOP_RoutesCount].WriteITOPRoute(bw);
            }
        }

        public ITOP(List<ITOP_Route> ITOP_Route_List)
        {
            ITOPHeader = "ITOP".ToCharArray();
            ITOP_NumberOfRoute = Convert.ToUInt16(ITOP_Route_List.Count);
            ITOP_NumberOfPoint = Convert.ToUInt16(ITOP_Route_List.Select(x => x.ITOP_Point_List.Count).Sum());
            this.ITOP_Route_List = ITOP_Route_List;
        }

        public ITOP()
        {
            ITOPHeader = new char[4];
            ITOP_NumberOfRoute = 0;
            ITOP_NumberOfPoint = 0;
            ITOP_Route_List = new List<ITOP_Route>();
        }
    }
}
