using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using KMPLibrary.Format.SectionData;
using static MK7_3D_KMP_Editor.CustomPropertyGridClassConverter;

namespace MK7_3D_KMP_Editor.PropertyGridObject
{
    /// <summary>
    /// Respawn Point (PropertyGrid)
    /// </summary>
    public class RespawnPoint_PGS
    {
        public List<TPGJValue> TPGJValue_List = new List<TPGJValue>();
        public List<TPGJValue> TPGJValueList { get => TPGJValue_List; set => TPGJValue_List = value; }
        [TypeConverter(typeof(CustomSortTypeConverter))]
        public class TPGJValue
        {
            [ReadOnly(true)]
            public int ID { get; set; }

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
                    _X = HTK_3DES.RadianToAngle(vector3D.X);
                    _Y = HTK_3DES.RadianToAngle(vector3D.Y);
                    _Z = HTK_3DES.RadianToAngle(vector3D.Z);
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

            public ushort TPGJ_RespawnID { get; set; }
            public ushort TPGJ_UnknownData1 { get; set; }

            public TPGJValue(Vector3D Pos, int InputID)
            {
                ID = InputID;
                TPGJ_RespawnID = 65535;
                Positions = new Position(Pos);
                Rotations = new Rotation();
                TPGJ_UnknownData1 = 0;
            }

            public TPGJValue(TPGJ.TPGJValue tPGJValue, int InputID)
            {
                ID = InputID;
                TPGJ_RespawnID = tPGJValue.TPGJ_RespawnID;
                Positions = new Position(tPGJValue.TPGJ_Position);
                Rotations = new Rotation(tPGJValue.TPGJ_Rotation);
                TPGJ_UnknownData1 = tPGJValue.TPGJ_UnknownData1;
            }

            public TPGJValue(KMPLibrary.XMLConvert.KMPData.SectionData.JugemPoint.JugemPoint_Value jugemPoint_Value, int InputID)
            {
                ID = InputID;
                TPGJ_RespawnID = jugemPoint_Value.RespawnID;
                Positions = new Position(jugemPoint_Value.Position.ToVector3D());
                Rotations = new Rotation(jugemPoint_Value.Rotation.ToVector3D());
                TPGJ_UnknownData1 = jugemPoint_Value.UnkBytes1;
            }

            public override string ToString()
            {
                return "Jugem Point " + ID;
            }
        }

        public RespawnPoint_PGS(TPGJ tPGJ_Section)
        {
            for (int i = 0; i < tPGJ_Section.NumOfEntries; i++) TPGJValueList.Add(new TPGJValue(tPGJ_Section.TPGJValue_List[i], i));
        }

        public RespawnPoint_PGS(KMPLibrary.XMLConvert.KMPData.SectionData.JugemPoint jugemPoint)
        {
            for (int i = 0; i < jugemPoint.Values.Count; i++) TPGJValueList.Add(new TPGJValue(jugemPoint.Values[i], i));
        }

        public RespawnPoint_PGS()
        {
            TPGJValueList = new List<TPGJValue>();
        }

        public TPGJ ToTPGJ()
        {
            //TPGJ TPGJ = new TPGJ
            //{
            //    TPGJHeader = "TPGJ".ToCharArray(),
            //    NumOfEntries = Convert.ToUInt16(TPGJValueList.Count),
            //    AdditionalValue = 0,
            //    TPGJValue_List = null
            //};

            List<TPGJ.TPGJValue> TPGJ_Value_List = new List<TPGJ.TPGJValue>();

            for (int TPGJCount = 0; TPGJCount < TPGJValueList.Count; TPGJCount++)
            {
                double RX = HTK_3DES.AngleToRadian(TPGJValueList[TPGJCount].Rotations.X);
                double RY = HTK_3DES.AngleToRadian(TPGJValueList[TPGJCount].Rotations.Y);
                double RZ = HTK_3DES.AngleToRadian(TPGJValueList[TPGJCount].Rotations.Z);

                TPGJ.TPGJValue TPGJ_Values = new TPGJ.TPGJValue
                {
                    TPGJ_Position = TPGJValueList[TPGJCount].Positions.GetVector3D(),
                    TPGJ_Rotation = new Vector3D(RX, RY, RZ),
                    TPGJ_RespawnID = TPGJValueList[TPGJCount].TPGJ_RespawnID,
                    TPGJ_UnknownData1 = TPGJValueList[TPGJCount].TPGJ_UnknownData1,
                };

                TPGJ_Value_List.Add(TPGJ_Values);
            }

            return new TPGJ(TPGJ_Value_List);

            //TPGJ.TPGJValue_List = TPGJ_Value_List;

            //return TPGJ;
        }
    }
}
