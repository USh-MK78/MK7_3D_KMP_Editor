using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// SROC (Unused Section)
    /// </summary>
    public class SROC
    {
        public char[] SROCHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        //public List<SROCValue> SROCValue_List { get; set; }
        //public class SROCValue
        //{
        //    //Unused
        //}

        public void ReadSROC(BinaryReader br)
        {
            SROCHeader = br.ReadChars(4);
            if (new string(SROCHeader) != "SROC") throw new Exception("Error : SROC");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();
        }

        public void WriteSROC(BinaryWriter bw)
        {
            bw.Write(SROCHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);
        }

        public static SROC ToSROC_Section()
        {
            return new SROC();
        }

        public SROC()
        {
            SROCHeader = "SROC".ToCharArray();
            NumOfEntries = 0;
            AdditionalValue = 0;
        }
    }
}
