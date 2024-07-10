using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using KMPLibrary.Format.SectionData;
using static MK7_3D_KMP_Editor.PropertyGridObject.CustomPropertyGridClassConverter;

namespace MK7_3D_KMP_Editor.PropertyGridObject
{
    /// <summary>
    /// Area (PropertyGrid)
    /// </summary>
    public class Area_PGS
    {
        public List<AERAValue> AERAValue_List = new List<AERAValue>();
        public List<AERAValue> AERAValueList { get => AERAValue_List; set => AERAValue_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class AERAValue
        {
            [ReadOnly(true)]
            public int ID { get; set; }

            public bool IsViewportVisible { get; set; } = true;

            public byte AreaType { get; set; }

            [TypeConverter(typeof(CustomExpandableObjectSortTypeConverter))]
            public AreaModeSetting AreaModeSettings { get; set; } = new AreaModeSetting();
            public class AreaModeSetting
            {
                [ReadOnly(true)]
                public AERA.AERAValue.AreaMode AreaTypeEnum
                {
                    get { return (AERA.AERAValue.AreaMode)Enum.ToObject(typeof(AERA.AERAValue.AreaMode), AreaModeValue); }
                }

                public byte AreaModeValue { get; set; }

                public override string ToString()
                {
                    return "Area Mode";
                }
            }

            public byte AERA_EMACIndex { get; set; }
            public byte Priority { get; set; }

            [TypeConverter(typeof(ExpandableObjectConverter))]
            public Position Positions { get; set; } = new Position();
            public class Position
            {
                private float _X;
                public float X
                {
                    get { return _X; }
                    set { _X = value; }
                }

                private float _Y;
                public float Y
                {
                    get { return _Y; }
                    set { _Y = value; }
                }

                private float _Z;
                public float Z
                {
                    get { return _Z; }
                    set { _Z = value; }
                }

                public Position()
                {
                    _X = 0;
                    _Y = 0;
                    _Z = 0;
                }

                public Position(float X, float Y, float Z)
                {
                    _X = X;
                    _Y = Y;
                    _Z = Z;
                }

                public Position(Vector3D vector3D)
                {
                    _X = (float)vector3D.X;
                    _Y = (float)vector3D.Y;
                    _Z = (float)vector3D.Z;
                }

                public Vector3D GetVector3D()
                {
                    double X = Convert.ToDouble(_X);
                    double Y = Convert.ToDouble(_Y);
                    double Z = Convert.ToDouble(_Z);

                    return new Vector3D(X, Y, Z);
                }

                public override string ToString()
                {
                    return "Position";
                }
            }

            [TypeConverter(typeof(ExpandableObjectConverter))]
            public Rotation Rotations { get; set; } = new Rotation();
            public class Rotation
            {
                private float _X;
                public double X
                {
                    get { return (double)HTK_3DES.RadianToAngle(_X); }
                    set { _X = (float)HTK_3DES.AngleToRadian(value); }
                }

                private float _Y;
                public double Y
                {
                    get { return (double)HTK_3DES.RadianToAngle(_Y); }
                    set { _Y = (float)HTK_3DES.AngleToRadian(value); }
                }

                private float _Z;
                public double Z
                {
                    get { return (double)HTK_3DES.RadianToAngle(_Z); }
                    set { _Z = (float)HTK_3DES.AngleToRadian(value); }
                }

                public Rotation()
                {
                    _X = 0;
                    _Y = 0;
                    _Z = 0;
                }

                public Rotation(float X, float Y, float Z)
                {
                    _X = X;
                    _Y = Y;
                    _Z = Z;
                }

                public Rotation(Vector3D vector3D)
                {
                    _X = (float)vector3D.X;
                    _Y = (float)vector3D.Y;
                    _Z = (float)vector3D.Z;
                }

                public Vector3D GetVector3D()
                {
                    double X = Convert.ToDouble(_X);
                    double Y = Convert.ToDouble(_Y);
                    double Z = Convert.ToDouble(_Z);

                    return new Vector3D(X, Y, Z);
                }

                public override string ToString()
                {
                    return "Rotation";
                }
            }

            [TypeConverter(typeof(ExpandableObjectConverter))]
            public Scale Scales { get; set; } = new Scale();
            public class Scale
            {
                private float _X;
                public float X
                {
                    get { return _X; }
                    set { _X = value; }
                }

                private float _Y;
                public float Y
                {
                    get { return _Y; }
                    set { _Y = value; }
                }

                private float _Z;
                public float Z
                {
                    get { return _Z; }
                    set { _Z = value; }
                }

                public Scale()
                {
                    _X = 1;
                    _Y = 1;
                    _Z = 1;
                }

                public Scale(float X, float Y, float Z)
                {
                    _X = X;
                    _Y = Y;
                    _Z = Z;
                }

                public Scale(Vector3D vector3D)
                {
                    _X = (float)vector3D.X;
                    _Y = (float)vector3D.Y;
                    _Z = (float)vector3D.Z;
                }

                public Vector3D GetVector3D()
                {
                    double X = Convert.ToDouble(_X);
                    double Y = Convert.ToDouble(_Y);
                    double Z = Convert.ToDouble(_Z);

                    return new Vector3D(X, Y, Z);
                }

                public override string ToString()
                {
                    return "Scale";
                }
            }

            public ushort AERA_Setting1 { get; set; }
            public ushort AERA_Setting2 { get; set; }
            public byte RouteID { get; set; }
            public byte EnemyID { get; set; }
            public ushort AERA_UnknownData1 { get; set; }

            public AERAValue(Vector3D Pos, int InputID)
            {
                ID = InputID;
                AreaType = 0;
                AreaModeSettings.AreaModeValue = 0;
                AERA_EMACIndex = 0;
                Priority = 0;
                Positions = new Position(Pos);
                Rotations = new Rotation();
                Scales = new Scale();
                AERA_Setting1 = 0;
                AERA_Setting2 = 0;
                RouteID = 0;
                EnemyID = 0;
                AERA_UnknownData1 = 0;
            }

            public AERAValue(AERA.AERAValue AERAValue, int InputID)
            {
                ID = InputID;
                AreaType = AERAValue.AreaType;
                AreaModeSettings.AreaModeValue = AERAValue.AreaModeValue;
                AERA_EMACIndex = AERAValue.AERA_EMACIndex;
                Priority = AERAValue.Priority;
                Positions = new Position(AERAValue.AERA_Position);
                Rotations = new Rotation(AERAValue.AERA_Rotation);
                Scales = new Scale(AERAValue.AERA_Scale);
                AERA_Setting1 = AERAValue.AERA_Setting1;
                AERA_Setting2 = AERAValue.AERA_Setting2;
                RouteID = AERAValue.RouteID;
                EnemyID = AERAValue.EnemyID;
                AERA_UnknownData1 = AERAValue.AERA_UnknownData1;
            }

            public AERAValue(KMPLibrary.XMLConvert.KMPData.SectionData.Area.Area_Value Area_Value, int InputID)
            {
                ID = InputID;
                AreaType = Area_Value.AreaType;
                AreaModeSettings.AreaModeValue = Area_Value.AreaMode;
                AERA_EMACIndex = Area_Value.CameraIndex;
                Priority = Area_Value.Priority;
                Positions = new Position(Area_Value.Position.ToVector3D());
                Rotations = new Rotation(Area_Value.Rotation.ToVector3D());
                Scales = new Scale(Area_Value.Scale.ToVector3D());
                AERA_Setting1 = Area_Value.Setting1;
                AERA_Setting2 = Area_Value.Setting2;
                RouteID = Area_Value.RouteID;
                EnemyID = Area_Value.EnemyID;
                AERA_UnknownData1 = Area_Value.UnknownData1;
            }

            public override string ToString()
            {
                return "Area " + ID;
            }
        }

        public Area_PGS(AERA AERA_Section)
        {
            for (int i = 0; i < AERA_Section.NumOfEntries; i++) AERAValueList.Add(new AERAValue(AERA_Section.AERAValue_List[i], i));
        }

        public Area_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.Area Area)
        {
            for (int i = 0; i < Area.Area_Values.Count; i++) AERAValueList.Add(new AERAValue(Area.Area_Values[i], i));
        }

        public Area_PGS()
        {
            AERAValueList = new List<AERAValue>();
        }

        public AERA ToAERA()
        {
            List<AERA.AERAValue> AERA_Value_List = new List<AERA.AERAValue>();

            for (int Count = 0; Count < AERAValueList.Count; Count++)
            {
                double RX = HTK_3DES.AngleToRadian(AERAValueList[Count].Rotations.X);
                double RY = HTK_3DES.AngleToRadian(AERAValueList[Count].Rotations.Y);
                double RZ = HTK_3DES.AngleToRadian(AERAValueList[Count].Rotations.Z);

                AERA.AERAValue AERA_Values = new AERA.AERAValue
                {
                    AreaModeValue = AERAValueList[Count].AreaModeSettings.AreaModeValue,
                    AreaType = AERAValueList[Count].AreaType,
                    AERA_EMACIndex = AERAValueList[Count].AERA_EMACIndex,
                    Priority = AERAValueList[Count].Priority,
                    AERA_Position = AERAValueList[Count].Positions.GetVector3D(),
                    AERA_Rotation = new Vector3D(RX, RY, RZ),
                    AERA_Scale = AERAValueList[Count].Scales.GetVector3D(),
                    AERA_Setting1 = AERAValueList[Count].AERA_Setting1,
                    AERA_Setting2 = AERAValueList[Count].AERA_Setting2,
                    RouteID = AERAValueList[Count].RouteID,
                    EnemyID = AERAValueList[Count].EnemyID,
                    AERA_UnknownData1 = AERAValueList[Count].AERA_UnknownData1
                };

                AERA_Value_List.Add(AERA_Values);
            }

            return new AERA(AERA_Value_List);
        }
    }
}
