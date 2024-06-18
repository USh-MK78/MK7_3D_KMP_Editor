using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KMPLibrary.KMPHelper
{
    public class Converter3D
    {
        public class ByteVector3D
        {
            public byte[] Byte_X { get; set; }
            public byte[] Byte_Y { get; set; }
            public byte[] Byte_Z { get; set; }

            public ByteVector3D(byte[] X, byte[] Y, byte[] Z)
            {
                Byte_X = X;
                Byte_Y = Y;
                Byte_Z = Z;
            }

            public ByteVector3D(float X, float Y, float Z)
            {
                Byte_X = BitConverter.GetBytes(Convert.ToSingle(X));
                Byte_Y = BitConverter.GetBytes(Convert.ToSingle(Y));
                Byte_Z = BitConverter.GetBytes(Convert.ToSingle(Z));
            }

            public ByteVector3D()
            {
                Byte_X = new byte[4];
                Byte_Y = new byte[4];
                Byte_Z = new byte[4];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BVector3D"></param>
        /// <returns></returns>
        public static Vector3D ByteArrayToVector3D(ByteVector3D BVector3D)
        {
            double Value_X = BitConverter.ToSingle(BVector3D.Byte_X, 0);
            double Value_Y = BitConverter.ToSingle(BVector3D.Byte_Y, 0);
            double Value_Z = BitConverter.ToSingle(BVector3D.Byte_Z, 0);

            return new Vector3D(Value_X, Value_Y, Value_Z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vector3D"></param>
        /// <returns></returns>
        public static ByteVector3D Vector3DToBVector3D(Vector3D Vector3D)
        {
            byte[] Byte_X = BitConverter.GetBytes(Convert.ToSingle(Vector3D.X));
            byte[] Byte_Y = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Y));
            byte[] Byte_Z = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Z));

            ByteVector3D BVector3D = new ByteVector3D(Byte_X, Byte_Y, Byte_Z);

            return BVector3D;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BVector3D"></param>
        /// <returns></returns>
        public static Vector3D ByteArrayToVector3D(byte[][] BVector3D)
        {
            double Value_X = BitConverter.ToSingle(BVector3D[0], 0);
            double Value_Y = BitConverter.ToSingle(BVector3D[1], 0);
            double Value_Z = BitConverter.ToSingle(BVector3D[2], 0);

            return new Vector3D(Value_X, Value_Y, Value_Z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vector3D"></param>
        /// <returns></returns>
        public static byte[][] Vector3DToByteArray(Vector3D Vector3D)
        {
            byte[] Byte_X = BitConverter.GetBytes(Convert.ToSingle(Vector3D.X));
            byte[] Byte_Y = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Y));
            byte[] Byte_Z = BitConverter.GetBytes(Convert.ToSingle(Vector3D.Z));

            return new byte[][] { Byte_X, Byte_Y, Byte_Z };
        }
    }

    public class Converter2D
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
        public static Vector2 BVector2ToVector2D(ByteVector2D InputBVector2)
        {
            return new Vector2(BitConverter.ToSingle(InputBVector2.Byte_X, 0), BitConverter.ToSingle(InputBVector2.Byte_Y, 0));
        }

        /// <summary>
        /// ByteArrayからVector2に変換
        /// </summary>
        /// <param name="InputByteArray"></param>
        /// <returns>Vector2</returns>
        public static Vector2 ByteArrayToVector2D(byte[][] InputByteArray)
        {
            return new Vector2(BitConverter.ToSingle(InputByteArray[0], 0), BitConverter.ToSingle(InputByteArray[1], 0));
        }

        /// <summary>
        /// Vector2からByteVector2Dに変換
        /// </summary>
        /// <param name="InputVector2"></param>
        /// <returns>ByteVector2D</returns>
        public static ByteVector2D Vector2ToBVector2D(Vector2 InputVector2)
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
        public static byte[][] Vector2ToByteArray(Vector2 InputVector2)
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
        public static Vector2 Vector3DTo2D(Vector3D InputVector3D, Axis_Up AxisToExc = Axis_Up.Y)
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
        public static Vector3D Vector2DTo3D(Vector2 InputVector2D, Axis_Up UpDirection = Axis_Up.Y, double Height = 0)
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

    public class Byte2StringConverter
    {
        public static byte[] ToByteArray(string InputString)
        {
            List<byte> Str2byte = new List<byte>();
            for (int i = 0; i < InputString.Length / 2; i++) Str2byte.Add(Convert.ToByte(InputString.Substring(i * 2, 2), 16));
            return Str2byte.ToArray();
        }

        public static byte[] OBJIDStrToByteArray(string InputString_OBJID)
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
}
