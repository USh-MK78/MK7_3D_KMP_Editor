using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// TPSM (Unused Section, Point (?))
    /// </summary>
    public class TPSM
    {
        public char[] TPSMHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        //public List<TPSMValue> TPSMValue_List { get; set; }
        //public class TPSMValue
        //{
        //    //Unused
        //}

        public void ReadTPSM(BinaryReader br)
        {
            TPSMHeader = br.ReadChars(4);
            if (new string(TPSMHeader) != "TPSM") throw new Exception("Error : TPSM");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();
        }

        public void WriteTPSM(BinaryWriter bw)
        {
            bw.Write(TPSMHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);
        }

        public static TPSM ToTPSM_Section()
        {
            return new TPSM();
        }

        public TPSM()
        {
            TPSMHeader = "TPSM".ToCharArray();
            NumOfEntries = 0;
            AdditionalValue = 0;
        }
    }
}
