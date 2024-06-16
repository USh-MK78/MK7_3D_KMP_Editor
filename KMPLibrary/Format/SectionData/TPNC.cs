using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPLibrary.Format.SectionData
{
    /// <summary>
    /// TPNC (Unused Section, Point(?))
    /// </summary>
    public class TPNC
    {
        public char[] TPNCHeader { get; set; }
        public ushort NumOfEntries { get; set; }
        public ushort AdditionalValue { get; set; }
        //public List<TPNCValue> TPNCValue_List { get; set; }
        //public class TPNCValue
        //{
        //    //Unused
        //}

        public void ReadTPNC(BinaryReader br)
        {
            TPNCHeader = br.ReadChars(4);
            if (new string(TPNCHeader) != "TPNC") throw new Exception("Error : TPNC");

            NumOfEntries = br.ReadUInt16();
            AdditionalValue = br.ReadUInt16();
        }

        public void WriteTPNC(BinaryWriter bw)
        {
            bw.Write(TPNCHeader);
            bw.Write(NumOfEntries);
            bw.Write(AdditionalValue);
        }

        public static TPNC ToTPNC_Section()
        {
            return new TPNC();
        }

        public TPNC()
        {
            TPNCHeader = "TPNC".ToCharArray();
            NumOfEntries = 0;
            AdditionalValue = 0;
        }
    }
}
