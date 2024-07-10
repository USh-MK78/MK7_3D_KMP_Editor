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
    /// KartPoint (PropertyGrid)
    /// </summary>
    public class KartPoint_PGS
    {
        public List<TPTKValue> TPTKValue_List = new List<TPTKValue>();
        public List<TPTKValue> TPTKValueList { get => TPTKValue_List; set => TPTKValue_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class TPTKValue
        {
            [ReadOnly(true)]
            public int ID { get; set; }

            public bool IsViewportVisible { get; set; } = true;

            [TypeConverter(typeof(ExpandableObjectConverter))]
            public Position Position_Value { get; set; } = new Position();
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
            public Rotation Rotate_Value { get; set; } = new Rotation();
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
                    return "Rotate";
                }
            }

            public ushort Player_Index { get; set; }
            public ushort TPTK_UnknownData { get; set; }

            public TPTKValue(Vector3D Pos, int InputID)
            {
                ID = InputID;
                Position_Value = new Position(Pos);
                Rotate_Value = new Rotation();
                Player_Index = 0;
                TPTK_UnknownData = 0;
            }

            public TPTKValue(TPTK.TPTKValue TPTKValue, int InputID)
            {
                ID = InputID;
                Position_Value = new Position(TPTKValue.TPTK_Position);
                Rotate_Value = new Rotation(TPTKValue.TPTK_Rotation);
                Player_Index = TPTKValue.Player_Index;
                TPTK_UnknownData = TPTKValue.TPTK_UnknownData;
            }

            public TPTKValue(KMPLibrary.XMLConvert.KMPData.SectionData.StartPosition.StartPosition_Value StartPosition_Value, int InputID)
            {
                ID = InputID;
                Position_Value = new Position(StartPosition_Value.Position.ToVector3D());
                Rotate_Value = new Rotation(StartPosition_Value.Rotation.ToVector3D());
                Player_Index = StartPosition_Value.Player_Index;
                TPTK_UnknownData = StartPosition_Value.TPTK_UnknownData;
            }

            public override string ToString()
            {
                return "Kart Point " + ID;
            }
        }

        public KartPoint_PGS(TPTK TPTK_Section)
        {
            for (int i = 0; i < TPTK_Section.NumOfEntries; i++) TPTKValueList.Add(new TPTKValue(TPTK_Section.TPTKValue_List[i], i));
        }

        public KartPoint_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.StartPosition StartPosition)
        {
            for (int i = 0; i < StartPosition.StartPositionValues.Count; i++) TPTKValueList.Add(new TPTKValue(StartPosition.StartPositionValues[i], i));
        }

        public KartPoint_PGS()
        {
            TPTKValueList = new List<TPTKValue>();
        }

        public TPTK ToTPTK()
        {
            List<TPTK.TPTKValue> TPTK_Value_List = new List<TPTK.TPTKValue>();

            for (int Count = 0; Count < TPTKValueList.Count; Count++)
            {
                double RX = HTK_3DES.AngleToRadian(TPTKValueList[Count].Rotate_Value.X);
                double RY = HTK_3DES.AngleToRadian(TPTKValueList[Count].Rotate_Value.Y);
                double RZ = HTK_3DES.AngleToRadian(TPTKValueList[Count].Rotate_Value.Z);

                TPTK.TPTKValue TPTK_Values = new TPTK.TPTKValue(TPTKValueList[Count].Position_Value.GetVector3D(), new Vector3D(RX, RY, RZ), TPTKValueList[Count].Player_Index, TPTKValueList[Count].TPTK_UnknownData);
                TPTK_Value_List.Add(TPTK_Values);
            }

            return new TPTK(TPTK_Value_List);
        }
    }
}
