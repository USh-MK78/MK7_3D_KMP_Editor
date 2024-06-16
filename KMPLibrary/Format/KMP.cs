
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KMPLibrary.Format.SectionData;

namespace KMPLibrary.Format
{
    /// <summary>
    /// KMP Data (DMDC)
    /// </summary>
    public class KMP
    {
        public char[] DMDCHeader { get; set; } //0x4
        public uint FileSize { get; set; } //0x4
        public ushort SectionCount { get; set; } //0x2
        public ushort DMDCHeaderSize { get; set; } //0x2
        public uint VersionNumber { get; set; } //0x4
        public KMPSection KMP_Section { get; set; }
        public class KMPSection
        {
            public uint TPTK_Offset { get; set; }
            public uint TPNE_Offset { get; set; }
            public uint HPNE_Offset { get; set; }
            public uint TPTI_Offset { get; set; }
            public uint HPTI_Offset { get; set; }
            public uint TPKC_Offset { get; set; }
            public uint HPKC_Offset { get; set; }
            public uint JBOG_Offset { get; set; }
            public uint ITOP_Offset { get; set; }
            public uint AERA_Offset { get; set; }
            public uint EMAC_Offset { get; set; }
            public uint TPGJ_Offset { get; set; }
            public uint TPNC_Offset { get; set; }
            public uint TPSM_Offset { get; set; }
            public uint IGTS_Offset { get; set; }
            public uint SROC_Offset { get; set; }
            public uint TPLG_Offset { get; set; }
            public uint HPLG_Offset { get; set; }

            public TPTK TPTK { get; set; }
            public TPNE TPNE { get; set; }
            public HPNE HPNE { get; set; }
            public TPTI TPTI { get; set; }
            public HPTI HPTI { get; set; }
            public TPKC TPKC { get; set; }
            public HPKC HPKC { get; set; }
            public JBOG JBOG { get; set; }
            public ITOP ITOP { get; set; }
            public AERA AERA { get; set; }
            public EMAC EMAC { get; set; }
            public TPGJ TPGJ { get; set; }
            public TPNC TPNC { get; set; } //Unused Section
            public TPSM TPSM { get; set; } //Unused Section
            public IGTS IGTS { get; set; }
            public SROC SROC { get; set; } //Unused Section
            public TPLG TPLG { get; set; }
            public HPLG HPLG { get; set; }

            public void ReadKMPSection(BinaryReader br, uint Version)
            {
                TPTK_Offset = br.ReadUInt32();
                TPNE_Offset = br.ReadUInt32();
                HPNE_Offset = br.ReadUInt32();
                TPTI_Offset = br.ReadUInt32();
                HPTI_Offset = br.ReadUInt32();
                TPKC_Offset = br.ReadUInt32();
                HPKC_Offset = br.ReadUInt32();
                JBOG_Offset = br.ReadUInt32();
                ITOP_Offset = br.ReadUInt32();
                AERA_Offset = br.ReadUInt32();
                EMAC_Offset = br.ReadUInt32();
                TPGJ_Offset = br.ReadUInt32();
                TPNC_Offset = br.ReadUInt32();
                TPSM_Offset = br.ReadUInt32();
                IGTS_Offset = br.ReadUInt32();
                SROC_Offset = br.ReadUInt32();
                TPLG_Offset = br.ReadUInt32();
                HPLG_Offset = br.ReadUInt32();

                long KMPSectionPos = br.BaseStream.Position;

                br.BaseStream.Seek(TPTK_Offset, SeekOrigin.Current);
                TPTK.ReadTPTK(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(TPNE_Offset, SeekOrigin.Current);
                TPNE.ReadTPNE(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(HPNE_Offset, SeekOrigin.Current);
                HPNE.ReadHPNE(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(TPTI_Offset, SeekOrigin.Current);
                TPTI.ReadTPTI(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(HPTI_Offset, SeekOrigin.Current);
                HPTI.ReadHPTI(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(TPKC_Offset, SeekOrigin.Current);
                TPKC.ReadTPKC(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(HPKC_Offset, SeekOrigin.Current);
                HPKC.ReadHPKC(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(JBOG_Offset, SeekOrigin.Current);
                JBOG.ReadJBOG(br, Version);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(ITOP_Offset, SeekOrigin.Current);
                ITOP.ReadITOP(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(AERA_Offset, SeekOrigin.Current);
                AERA.ReadAERA(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(EMAC_Offset, SeekOrigin.Current);
                EMAC.ReadEMAC(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(TPGJ_Offset, SeekOrigin.Current);
                TPGJ.ReadTPGJ(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(TPNC_Offset, SeekOrigin.Current);
                TPNC.ReadTPNC(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(TPSM_Offset, SeekOrigin.Current);
                TPSM.ReadTPSM(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(IGTS_Offset, SeekOrigin.Current);
                IGTS.ReadIGTS(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(SROC_Offset, SeekOrigin.Current);
                SROC.ReadSROC(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(TPLG_Offset, SeekOrigin.Current);
                TPLG.ReadTPLG(br);
                br.BaseStream.Position = KMPSectionPos;

                br.BaseStream.Seek(HPLG_Offset, SeekOrigin.Current);
                HPLG.ReadHPLG(br);
                br.BaseStream.Position = KMPSectionPos;
            }

            public void WriteKMPSection(BinaryWriter bw, uint Version)
            {
                long SectionOffsetBasePos = bw.BaseStream.Position;

                #region WriteOffset (Default)
                bw.Write(TPTK_Offset);
                bw.Write(TPNE_Offset);
                bw.Write(HPNE_Offset);
                bw.Write(TPTI_Offset);
                bw.Write(HPTI_Offset);
                bw.Write(TPKC_Offset);
                bw.Write(HPKC_Offset);
                bw.Write(JBOG_Offset);
                bw.Write(ITOP_Offset);
                bw.Write(AERA_Offset);
                bw.Write(EMAC_Offset);
                bw.Write(TPGJ_Offset);
                bw.Write(TPNC_Offset);
                bw.Write(TPSM_Offset);
                bw.Write(IGTS_Offset);
                bw.Write(SROC_Offset);
                bw.Write(TPLG_Offset);
                bw.Write(HPLG_Offset);
                #endregion

                #region Write
                TPTK_Offset = (uint)bw.BaseStream.Position;
                TPTK.WriteTPTK(bw);

                TPNE_Offset = (uint)bw.BaseStream.Position;
                TPNE.WriteTPNE(bw);

                HPNE_Offset = (uint)bw.BaseStream.Position;
                HPNE.WriteHPNE(bw);

                TPTI_Offset = (uint)bw.BaseStream.Position;
                TPTI.WriteTPTI(bw);

                HPTI_Offset = (uint)bw.BaseStream.Position;
                HPTI.WriteHPTI(bw);

                TPKC_Offset = (uint)bw.BaseStream.Position;
                TPKC.WriteTPKC(bw);

                HPKC_Offset = (uint)bw.BaseStream.Position;
                HPKC.WriteHPKC(bw);

                JBOG_Offset = (uint)bw.BaseStream.Position;
                JBOG.WriteJBOG(bw, Version);

                ITOP_Offset = (uint)bw.BaseStream.Position;
                ITOP.WriteITOP(bw);

                AERA_Offset = (uint)bw.BaseStream.Position;
                AERA.WriteAERA(bw);

                EMAC_Offset = (uint)bw.BaseStream.Position;
                EMAC.WriteEMAC(bw);

                TPGJ_Offset = (uint)bw.BaseStream.Position;
                TPGJ.WriteTPGJ(bw);

                TPNC_Offset = (uint)bw.BaseStream.Position;
                TPNC.WriteTPNC(bw);

                TPSM_Offset = (uint)bw.BaseStream.Position;
                TPSM.WriteTPSM(bw);

                IGTS_Offset = (uint)bw.BaseStream.Position;
                IGTS.WriteIGTS(bw);

                SROC_Offset = (uint)bw.BaseStream.Position;
                SROC.WriteSROC(bw);

                TPLG_Offset = (uint)bw.BaseStream.Position;
                TPLG.WriteTPLG(bw);

                HPLG_Offset = (uint)bw.BaseStream.Position;
                HPLG.WriteHPLG(bw);
                #endregion

                //FileSize
                long FileEndLocation = bw.BaseStream.Position;

                bw.BaseStream.Position = SectionOffsetBasePos;

                #region WriteOffset
                bw.Write(TPTK_Offset - 88);
                bw.Write(TPNE_Offset - 88);
                bw.Write(HPNE_Offset - 88);
                bw.Write(TPTI_Offset - 88);
                bw.Write(HPTI_Offset - 88);
                bw.Write(TPKC_Offset - 88);
                bw.Write(HPKC_Offset - 88);
                bw.Write(JBOG_Offset - 88);
                bw.Write(ITOP_Offset - 88);
                bw.Write(AERA_Offset - 88);
                bw.Write(EMAC_Offset - 88);
                bw.Write(TPGJ_Offset - 88);
                bw.Write(TPNC_Offset - 88);
                bw.Write(TPSM_Offset - 88);
                bw.Write(IGTS_Offset - 88);
                bw.Write(SROC_Offset - 88);
                bw.Write(TPLG_Offset - 88);
                bw.Write(HPLG_Offset - 88);
                #endregion

                //Reset Position
                bw.BaseStream.Position = FileEndLocation;
            }

            public KMPSection()
            {
                TPTK_Offset = 0;
                TPNE_Offset = 0;
                HPNE_Offset = 0;
                TPTI_Offset = 0;
                HPTI_Offset = 0;
                TPKC_Offset = 0;
                HPKC_Offset = 0;
                JBOG_Offset = 0;
                ITOP_Offset = 0;
                AERA_Offset = 0;
                EMAC_Offset = 0;
                TPGJ_Offset = 0;
                TPNC_Offset = 0;
                TPSM_Offset = 0;
                IGTS_Offset = 0;
                SROC_Offset = 0;
                TPLG_Offset = 0;
                HPLG_Offset = 0;

                TPTK = new TPTK();
                TPNE = new TPNE();
                HPNE = new HPNE();
                TPTI = new TPTI();
                HPTI = new HPTI();
                TPKC = new TPKC();
                HPKC = new HPKC();
                JBOG = new JBOG();
                ITOP = new ITOP();
                AERA = new AERA();
                EMAC = new EMAC();
                TPGJ = new TPGJ();
                TPNC = new TPNC();
                TPSM = new TPSM();
                IGTS = new IGTS();
                SROC = new SROC();
                TPLG = new TPLG();
                HPLG = new HPLG();
            }
        }

        public void ReadKMP(BinaryReader br)
        {
            DMDCHeader = br.ReadChars(4);
            FileSize = br.ReadUInt32();
            SectionCount = br.ReadUInt16();
            DMDCHeaderSize = br.ReadUInt16();
            VersionNumber = br.ReadUInt32();
            KMP_Section.ReadKMPSection(br, VersionNumber);
        }

        public void WriteKMP(BinaryWriter bw)
        {
            bw.Write(DMDCHeader);
            bw.Write((uint)0); //FileSize (Default)

            bw.Write(SectionCount);
            bw.Write(DMDCHeaderSize);
            bw.Write(VersionNumber);
            KMP_Section.WriteKMPSection(bw, VersionNumber);

            //Write FileSize
            FileSize = (uint)bw.BaseStream.Position;
            bw.BaseStream.Seek(4, SeekOrigin.Begin);
            bw.Write(FileSize);
        }

        /// <summary>
        /// Initialize KMP
        /// </summary>
        /// <param name="Section">KMP Section</param>
        /// <param name="Version">3000 : Divide (Unused (?)) | 3100 : Normal</param>
        public KMP(KMPSection Section, uint Version = 3100)
        {
            DMDCHeader = "DMDC".ToCharArray();
            FileSize = 0;
            SectionCount = 18;
            DMDCHeaderSize = 88;
            VersionNumber = Version;
            KMP_Section = Section;
        }

        /// <summary>
        /// Initialize KMP (Read)
        /// </summary>
        public KMP()
        {
            DMDCHeader = new char[4];
            FileSize = 0;
            SectionCount = 0;
            DMDCHeaderSize = 0;
            VersionNumber = 0;
            KMP_Section = new KMPSection();
        }
    }
}
