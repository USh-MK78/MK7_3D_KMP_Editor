using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KMPLibrary.Format.SectionData;
using static MK7_3D_KMP_Editor.CustomPropertyGridClassConverter;

namespace MK7_3D_KMP_Editor.PropertyGridObject
{
    public class HPKC_TPKCData
    {
        public HPKC HPKC_Section;
        public TPKC TPKC_Section;

        public HPKC_TPKCData(HPKC HPKC, TPKC TPKC)
        {
            HPKC_Section = HPKC;
            TPKC_Section = TPKC;
        }
    }

    /// <summary>
    /// Checkpoint (PropertyGrid)
    /// </summary>
    public class Checkpoint_PGS
    {
        public List<HPKCValue> HPKCValue_List = new List<HPKCValue>();
        public List<HPKCValue> HPKCValueList { get => HPKCValue_List; set => HPKCValue_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class HPKCValue
        {
            [ReadOnly(true)]
            public int GroupID { get; set; }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public HPKC_PreviewGroups HPKC_PreviewGroup { get; set; } = new HPKC_PreviewGroups();
            public class HPKC_PreviewGroups
            {
                public byte Prev0 { get; set; }
                public byte Prev1 { get; set; }
                public byte Prev2 { get; set; }
                public byte Prev3 { get; set; }
                public byte Prev4 { get; set; }
                public byte Prev5 { get; set; }

                public byte[] GetPrevGroupArray()
                {
                    return new byte[] { Prev0, Prev1, Prev2, Prev3, Prev4, Prev5 };
                }

                public HPKC_PreviewGroups(byte[] PrevGroupArray)
                {
                    Prev0 = PrevGroupArray[0];
                    Prev1 = PrevGroupArray[1];
                    Prev2 = PrevGroupArray[2];
                    Prev3 = PrevGroupArray[3];
                    Prev4 = PrevGroupArray[4];
                    Prev5 = PrevGroupArray[5];
                }

                public HPKC_PreviewGroups()
                {
                    Prev0 = 255;
                    Prev1 = 255;
                    Prev2 = 255;
                    Prev3 = 255;
                    Prev4 = 255;
                    Prev5 = 255;
                }

                public HPKC_PreviewGroups(HPKC.HPKCValue.HPKC_PreviewGroups HPKC_PreviewGroup)
                {
                    Prev0 = HPKC_PreviewGroup.Prev0;
                    Prev1 = HPKC_PreviewGroup.Prev1;
                    Prev2 = HPKC_PreviewGroup.Prev2;
                    Prev3 = HPKC_PreviewGroup.Prev3;
                    Prev4 = HPKC_PreviewGroup.Prev4;
                    Prev5 = HPKC_PreviewGroup.Prev5;
                }

                public HPKC_PreviewGroups(KMPLibrary.XMLConvert.KMPData.SectionData.Checkpoint.Checkpoint_Group.CP_PreviousGroup previous)
                {
                    Prev0 = previous.Prev0;
                    Prev1 = previous.Prev1;
                    Prev2 = previous.Prev2;
                    Prev3 = previous.Prev3;
                    Prev4 = previous.Prev4;
                    Prev5 = previous.Prev5;
                }

                public override string ToString()
                {
                    return "Preview";
                }
            }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public HPKC_NextGroups HPKC_NextGroup { get; set; } = new HPKC_NextGroups();
            public class HPKC_NextGroups
            {
                public byte Next0 { get; set; }
                public byte Next1 { get; set; }
                public byte Next2 { get; set; }
                public byte Next3 { get; set; }
                public byte Next4 { get; set; }
                public byte Next5 { get; set; }

                public byte[] GetNextGroupArray()
                {
                    return new byte[] { Next0, Next1, Next2, Next3, Next4, Next5 };
                }

                public HPKC_NextGroups(byte[] NextGroupArray)
                {
                    Next0 = NextGroupArray[0];
                    Next1 = NextGroupArray[1];
                    Next2 = NextGroupArray[2];
                    Next3 = NextGroupArray[3];
                    Next4 = NextGroupArray[4];
                    Next5 = NextGroupArray[5];
                }

                public HPKC_NextGroups()
                {
                    Next0 = 255;
                    Next1 = 255;
                    Next2 = 255;
                    Next3 = 255;
                    Next4 = 255;
                    Next5 = 255;
                }

                public HPKC_NextGroups(HPKC.HPKCValue.HPKC_NextGroups HPKC_NextGroup)
                {
                    Next0 = HPKC_NextGroup.Next0;
                    Next1 = HPKC_NextGroup.Next1;
                    Next2 = HPKC_NextGroup.Next2;
                    Next3 = HPKC_NextGroup.Next3;
                    Next4 = HPKC_NextGroup.Next4;
                    Next5 = HPKC_NextGroup.Next5;
                }

                public HPKC_NextGroups(KMPLibrary.XMLConvert.KMPData.SectionData.Checkpoint.Checkpoint_Group.CP_NextGroup next)
                {
                    Next0 = next.Next0;
                    Next1 = next.Next1;
                    Next2 = next.Next2;
                    Next3 = next.Next3;
                    Next4 = next.Next4;
                    Next5 = next.Next5;
                }

                public override string ToString()
                {
                    return "Next";
                }
            }

            public ushort HPKC_UnknownData1 { get; set; }

            public List<TPKCValue> TPKCValue_List = new List<TPKCValue>();
            [Browsable(false)]
            public List<TPKCValue> TPKCValueList { get => TPKCValue_List; set => TPKCValue_List = value; }
            [TypeConverter(typeof(CustomSortTypeConverter))]
            public class TPKCValue
            {
                [ReadOnly(true)]
                public int Group_ID { get; set; }

                [ReadOnly(true)]
                public int ID { get; set; }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Position2D_Left Position_2D_Left { get; set; } = new Position2D_Left();
                public class Position2D_Left
                {
                    public float X { get; set; }
                    public float Y { get; set; }

                    public Position2D_Left()
                    {
                        X = 0;
                        Y = 0;
                    }

                    public Position2D_Left(float Left_X, float Left_Y)
                    {
                        X = Left_X;
                        Y = Left_Y;
                    }

                    public Position2D_Left(Vector2 vector2)
                    {
                        X = (float)vector2.X;
                        Y = (float)vector2.Y;
                    }

                    public Vector2 GetVector2()
                    {
                        return new Vector2(this.X, this.Y);
                    }

                    public override string ToString()
                    {
                        return "Position2D Left";
                    }
                }

                [TypeConverter(typeof(ExpandableObjectConverter))]
                public Position2D_Right Position_2D_Right { get; set; } = new Position2D_Right();
                public class Position2D_Right
                {
                    public float X { get; set; }
                    public float Y { get; set; }

                    public Position2D_Right()
                    {
                        X = 0;
                        Y = 0;
                    }

                    public Position2D_Right(float Right_X, float Right_Y)
                    {
                        X = Right_X;
                        Y = Right_Y;
                    }

                    public Position2D_Right(Vector2 vector2)
                    {
                        X = (float)vector2.X;
                        Y = (float)vector2.Y;
                    }

                    public Vector2 GetVector2()
                    {
                        return new Vector2(this.X, this.Y);
                    }

                    public override string ToString()
                    {
                        return "Position2D Right";
                    }
                }

                public byte TPKC_RespawnID { get; set; }
                public byte TPKC_Checkpoint_Type { get; set; }
                public byte TPKC_PreviousCheckPoint { get; set; }
                public byte TPKC_NextCheckPoint { get; set; }
                public byte TPKC_ClipID { get; set; }
                public byte TPKC_Section { get; set; }
                public byte TPKC_UnkBytes3 { get; set; }
                public byte TPKC_UnkBytes4 { get; set; }

                public TPKCValue(Vector2 LeftPos, Vector2 RightPos, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    Position_2D_Left = new Position2D_Left(LeftPos);
                    Position_2D_Right = new Position2D_Right(RightPos);
                    TPKC_RespawnID = 0xFF;
                    TPKC_Checkpoint_Type = 0;
                    TPKC_NextCheckPoint = 0xFF;
                    TPKC_PreviousCheckPoint = 0xFF;
                    TPKC_ClipID = 255;
                    TPKC_Section = 0;
                    TPKC_UnkBytes3 = 0;
                    TPKC_UnkBytes4 = 0;
                }

                public TPKCValue(TPKC.TPKCValue TPKCValue, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    Position_2D_Left = new Position2D_Left(TPKCValue.TPKC_2DPosition_Left);
                    Position_2D_Right = new Position2D_Right(TPKCValue.TPKC_2DPosition_Right);
                    TPKC_RespawnID = TPKCValue.TPKC_RespawnID;
                    TPKC_Checkpoint_Type = TPKCValue.TPKC_Checkpoint_Type;
                    TPKC_NextCheckPoint = TPKCValue.TPKC_NextCheckPoint;
                    TPKC_PreviousCheckPoint = TPKCValue.TPKC_PreviousCheckPoint;
                    TPKC_ClipID = TPKCValue.TPKC_ClipID;
                    TPKC_Section = TPKCValue.TPKC_Section;
                    TPKC_UnkBytes3 = TPKCValue.TPKC_UnkBytes3;
                    TPKC_UnkBytes4 = TPKCValue.TPKC_UnkBytes4;
                }

                public TPKCValue(KMPLibrary.XMLConvert.KMPData.SectionData.Checkpoint.Checkpoint_Group.Checkpoint_Point checkpoint_Point, int GroupID, int InputID)
                {
                    Group_ID = GroupID;
                    ID = InputID;
                    Position_2D_Left = new Position2D_Left(checkpoint_Point.Position_2D_Left.ToVector2());
                    Position_2D_Right = new Position2D_Right(checkpoint_Point.Position_2D_Right.ToVector2());
                    TPKC_RespawnID = checkpoint_Point.RespawnID;
                    TPKC_Checkpoint_Type = checkpoint_Point.Checkpoint_Type;
                    TPKC_NextCheckPoint = checkpoint_Point.NextCheckPoint;
                    TPKC_PreviousCheckPoint = checkpoint_Point.PreviousCheckPoint;
                    TPKC_ClipID = checkpoint_Point.ClipID;
                    TPKC_Section = checkpoint_Point.Section;
                    TPKC_UnkBytes3 = checkpoint_Point.UnkBytes3;
                    TPKC_UnkBytes4 = checkpoint_Point.UnkBytes4;
                }

                public override string ToString()
                {
                    return "CheckPoint Point " + ID;
                }
            }

            public HPKCValue(int InputID)
            {
                GroupID = InputID;
                HPKC_PreviewGroup = new HPKC_PreviewGroups();
                HPKC_NextGroup = new HPKC_NextGroups();
                HPKC_UnknownData1 = 0;
                TPKCValueList = new List<TPKCValue>();
            }

            public HPKCValue(HPKC.HPKCValue HPKCValue, TPKC TPKC, int InputID)
            {
                GroupID = InputID;
                HPKC_PreviewGroup = new HPKC_PreviewGroups(HPKCValue.HPKC_PreviewGroup);
                HPKC_NextGroup = new HPKC_NextGroups(HPKCValue.HPKC_NextGroup);
                HPKC_UnknownData1 = HPKCValue.HPKC_UnknownShortData1;

                for (int i = 0; i < HPKCValue.HPKC_Length; i++)
                {
                    TPKCValueList.Add(new TPKCValue(TPKC.TPKCValue_List[i + HPKCValue.HPKC_StartPoint], InputID, i));
                }
            }

            public HPKCValue(KMPLibrary.XMLConvert.KMPData.SectionData.Checkpoint.Checkpoint_Group checkpoint_Group, int InputID)
            {
                GroupID = InputID;
                HPKC_PreviewGroup = new HPKC_PreviewGroups(checkpoint_Group.PreviousGroups);
                HPKC_NextGroup = new HPKC_NextGroups(checkpoint_Group.NextGroups);
                HPKC_UnknownData1 = checkpoint_Group.UnknownData1;

                for (int i = 0; i < checkpoint_Group.Points.Count; i++)
                {
                    TPKCValueList.Add(new TPKCValue(checkpoint_Group.Points[i], InputID, i));
                }
            }

            public override string ToString()
            {
                return "CheckPoint " + GroupID;
            }
        }

        public Checkpoint_PGS(HPKC HPKC, TPKC TPKC)
        {
            for (int i = 0; i < HPKC.NumOfEntries; i++)
            {
                HPKCValueList.Add(new HPKCValue(HPKC.HPKCValue_List[i], TPKC, i));
            }
        }

        public Checkpoint_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.Checkpoint checkpoint)
        {
            for (int i = 0; i < checkpoint.Groups.Count; i++)
            {
                HPKCValueList.Add(new HPKCValue(checkpoint.Groups[i], i));
            }
        }

        public Checkpoint_PGS()
        {
            HPKCValueList = new List<HPKCValue>();
        }

        public HPKC_TPKCData ToHPKC_TPKCData()
        {
            HPKC_TPKCData hPKC_TPKCData = null;

            if (HPKCValueList.Count != 0)
            {
                List<TPKC.TPKCValue> TPKC_Values_List = new List<TPKC.TPKCValue>();
                List<HPKC.HPKCValue> HPKC_Values_List = new List<HPKC.HPKCValue>();

                int StartPoint = 0;
                for (int HPKCCount = 0; HPKCCount < HPKCValueList.Count; HPKCCount++)
                {
                    HPKC.HPKCValue HPKC_Values = new HPKC.HPKCValue
                    {
                        HPKC_StartPoint = Convert.ToByte(StartPoint),
                        HPKC_Length = Convert.ToByte(HPKCValueList[HPKCCount].TPKCValueList.Count),
                        HPKC_PreviewGroup = new HPKC.HPKCValue.HPKC_PreviewGroups(HPKCValueList[HPKCCount].HPKC_PreviewGroup.GetPrevGroupArray()),
                        HPKC_NextGroup = new HPKC.HPKCValue.HPKC_NextGroups(HPKCValueList[HPKCCount].HPKC_NextGroup.GetNextGroupArray()),
                        HPKC_UnknownShortData1 = HPKCValueList[HPKCCount].HPKC_UnknownData1
                        //HPKC_PreviewGroup = new HPKC.HPKCValue.HPKC_PreviewGroups
                        //{
                        //    Prev0 = HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev0,
                        //    Prev1 = HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev1,
                        //    Prev2 = HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev2,
                        //    Prev3 = HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev3,
                        //    Prev4 = HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev4,
                        //    Prev5 = HPKCValueList[HPKCCount].HPKC_PreviewGroup.Prev5,
                        //},
                        //HPKC_NextGroup = new HPKC.HPKCValue.HPKC_NextGroups
                        //{
                        //    Next0 = HPKCValueList[HPKCCount].HPKC_NextGroup.Next0,
                        //    Next1 = HPKCValueList[HPKCCount].HPKC_NextGroup.Next1,
                        //    Next2 = HPKCValueList[HPKCCount].HPKC_NextGroup.Next2,
                        //    Next3 = HPKCValueList[HPKCCount].HPKC_NextGroup.Next3,
                        //    Next4 = HPKCValueList[HPKCCount].HPKC_NextGroup.Next4,
                        //    Next5 = HPKCValueList[HPKCCount].HPKC_NextGroup.Next5,
                        //}
                    };

                    HPKC_Values_List.Add(HPKC_Values);

                    for (int TPKCCount = 0; TPKCCount < HPKCValueList[HPKCCount].TPKCValueList.Count; TPKCCount++)
                    {
                        TPKC.TPKCValue TPKC_Values = new TPKC.TPKCValue
                        {
                            TPKC_2DPosition_Left = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].Position_2D_Left.GetVector2(),
                            TPKC_2DPosition_Right = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].Position_2D_Right.GetVector2(),

                            TPKC_RespawnID = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_RespawnID,
                            TPKC_Checkpoint_Type = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_Checkpoint_Type,
                            TPKC_PreviousCheckPoint = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_PreviousCheckPoint,
                            TPKC_NextCheckPoint = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_NextCheckPoint,
                            TPKC_ClipID = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_ClipID,
                            TPKC_Section = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_Section,
                            TPKC_UnkBytes3 = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_UnkBytes3,
                            TPKC_UnkBytes4 = HPKCValueList[HPKCCount].TPKCValueList[TPKCCount].TPKC_UnkBytes4
                        };

                        TPKC_Values_List.Add(TPKC_Values);

                        StartPoint++;
                    }
                }

                TPKC TPKC = new TPKC(TPKC_Values_List);
                HPKC HPKC = new HPKC(HPKC_Values_List);

                //TPKC TPKC = new TPKC
                //{
                //    TPKCHeader = new char[] { 'T', 'P', 'K', 'C' },
                //    NumOfEntries = Convert.ToUInt16(TPKC_Values_List.Count),
                //    AdditionalValue = 0,
                //    TPKCValue_List = TPKC_Values_List
                //};

                //HPKC HPKC = new HPKC
                //{
                //    HPKCHeader = new char[] { 'H', 'P', 'K', 'C' },
                //    NumOfEntries = Convert.ToUInt16(HPKC_Values_List.Count),
                //    AdditionalValue = 0,
                //    HPKCValue_List = HPKC_Values_List
                //};

                hPKC_TPKCData = new HPKC_TPKCData(HPKC, TPKC);
            }
            if (HPKCValueList.Count == 0)
            {
                //TPKC TPKC = new TPKC
                //{
                //    TPKCHeader = new char[] { 'T', 'P', 'K', 'C' },
                //    NumOfEntries = 0,
                //    AdditionalValue = 0,
                //    TPKCValue_List = new List<TPKC.TPKCValue>()
                //};

                //HPKC HPKC = new HPKC
                //{
                //    HPKCHeader = new char[] { 'H', 'P', 'K', 'C' },
                //    NumOfEntries = 0,
                //    AdditionalValue = 0,
                //    HPKCValue_List = new List<HPKC.HPKCValue>()
                //};

                TPKC TPKC = new TPKC(new List<TPKC.TPKCValue>());
                HPKC HPKC = new HPKC(new List<HPKC.HPKCValue>());

                hPKC_TPKCData = new HPKC_TPKCData(HPKC, TPKC);
            }

            return hPKC_TPKCData;
        }
    }
}
