using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TXT_XML
{
    static class XMLConvertor
    {
        public static void Convert(string inpath, string outpath, bool TSV_CSV, string messageId, string time, int numPayments, double controlSum, string InitgPtyNm)
        {

            try
            {
                InputDataValidator.Validate(messageId, time, numPayments, controlSum, InitgPtyNm);
            }
            catch (BadInputDataException e)
            {
                Console.Write(e.Message);
                return;
            }

            List<Payment> payments;

            try
            {
                payments = PaymentMapper.Map(TSV_CSV, File.ReadAllLines(inpath));
            }
            catch (Exception e)
            {
                if (e is PaymentBadFormatException || e is PaymentBadFieldFormatException || e is PaymentMissingFieldException || e is IOException)
                {
                    Console.Write(e.Message);
                    return;
                }
                else
                {
                    throw;
                }
            }

            XmlDocument xml = new XmlDocument();

            XmlElement Document = (XmlElement)xml.AppendChild(xml.CreateElement("Document"));
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xml.InsertBefore(xmlDeclaration, Document);
            Document.SetAttribute("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.001.001.03");
            #region Document
            {
                XmlElement CstmrCdtTrfInitn = (XmlElement)Document.AppendChild(xml.CreateElement("CstmrCdtTrfInitn"));
                #region CstmrCdtTrfInitn
                {
                    XmlElement GrpHdr = (XmlElement)CstmrCdtTrfInitn.AppendChild(xml.CreateElement("GrpHdr"));
                    #region GrpHdr
                    {
                        XmlElement MsgId = (XmlElement)GrpHdr.AppendChild(xml.CreateElement("MsgId"));
                        MsgId.InnerText = messageId;
                        XmlElement CreDtTm = (XmlElement)GrpHdr.AppendChild(xml.CreateElement("CreDtTm"));
                        CreDtTm.InnerText = time;
                        XmlElement NbOfTxs = (XmlElement)GrpHdr.AppendChild(xml.CreateElement("NbOfTxs"));
                        NbOfTxs.InnerText = numPayments.ToString();
                        XmlElement CtrlSum = (XmlElement)GrpHdr.AppendChild(xml.CreateElement("CtrlSum"));
                        CtrlSum.InnerText = Math.Round(controlSum,2).ToString();
                        XmlElement InitgPty = (XmlElement)GrpHdr.AppendChild(xml.CreateElement("InitgPty"));
                        #region InitgPty
                        {
                            XmlElement Nm = (XmlElement)InitgPty.AppendChild(xml.CreateElement("Nm"));
                            Nm.InnerText = InitgPtyNm;
                        }
                        #endregion InitgPty
                    }
                    #endregion GrpHdr

                    foreach (Payment p in payments)
                    {
                        XmlElement PmtInf = (XmlElement)CstmrCdtTrfInitn.AppendChild(xml.CreateElement("PmtInf"));
                        #region PmtInf
                        {
                            XmlElement PmtInfId = (XmlElement)PmtInf.AppendChild(xml.CreateElement("PmtInfId"));
                            PmtInfId.InnerText = "";
                            XmlElement PmtMtd = (XmlElement)PmtInf.AppendChild(xml.CreateElement("PmtMtd"));
                            PmtMtd.InnerText = p.PaymentMethod;
                            XmlElement BtchBookg = (XmlElement)PmtInf.AppendChild(xml.CreateElement("BtchBookg"));
                            BtchBookg.InnerText = "";
                            XmlElement PmtTpInf = (XmlElement)PmtInf.AppendChild(xml.CreateElement("PmtTpInf"));
                            #region PmtTpInf
                            {
                                XmlElement InstrPrty = (XmlElement)PmtTpInf.AppendChild(xml.CreateElement("InstrPrty"));
                                InstrPrty.InnerText = "";
                                XmlElement SvcLvl = (XmlElement)PmtTpInf.AppendChild(xml.CreateElement("SvcLvl"));
                                #region SvcLvl
                                {
                                    XmlElement Cd = (XmlElement)SvcLvl.AppendChild(xml.CreateElement("Cd"));
                                    Cd.InnerText = "";
                                }
                                #endregion SvcLvl
                                XmlElement LclInstrm = (XmlElement)PmtTpInf.AppendChild(xml.CreateElement("LclInstrm"));
                                #region LclInstrm
                                {
                                    XmlElement Cd = (XmlElement)LclInstrm.AppendChild(xml.CreateElement("Cd"));
                                    Cd.InnerText = "";
                                }
                                #endregion LclInstrm
                            }
                            #endregion PmtTpInf
                            XmlElement ReqdExctnDt = (XmlElement)PmtInf.AppendChild(xml.CreateElement("ReqdExctnDt"));
                            ReqdExctnDt.InnerText = "";
                            XmlElement Dbtr = (XmlElement)PmtInf.AppendChild(xml.CreateElement("Dbtr"));
                            #region Dbtr
                            {
                                XmlElement Nm = (XmlElement)Dbtr.AppendChild(xml.CreateElement("Nm"));
                                Nm.InnerText = "";
                                XmlElement Id = (XmlElement)Dbtr.AppendChild(xml.CreateElement("Id"));
                                #region Id
                                {
                                    XmlElement PrvtId = (XmlElement)Id.AppendChild(xml.CreateElement("PrvtId"));
                                    #region PrvtId
                                    {
                                        XmlElement Othr = (XmlElement)PrvtId.AppendChild(xml.CreateElement("Othr"));
                                        #region Othr
                                        {
                                            XmlElement Idx = (XmlElement)Othr.AppendChild(xml.CreateElement("Id"));
                                            Idx.InnerText = "";
                                        }
                                        #endregion Othr
                                    }
                                    #endregion PrvtId
                                }
                                #endregion Id
                                XmlElement CtryOfRes = (XmlElement)Dbtr.AppendChild(xml.CreateElement("CtryOfRes"));
                                CtryOfRes.InnerText = "";
                            }
                            #endregion Dbtr
                            XmlElement DbtrAcct = (XmlElement)PmtInf.AppendChild(xml.CreateElement("DbtrAcct"));
                            #region DbtrAcct
                            {
                                XmlElement Id = (XmlElement)DbtrAcct.AppendChild(xml.CreateElement("Id"));
                                #region Id
                                {
                                    XmlElement IBAN = (XmlElement)Id.AppendChild(xml.CreateElement("IBAN"));
                                    IBAN.InnerText = p.SourceAccount;
                                }
                                #endregion Id
                            }
                            #endregion DbtrAcct
                            XmlElement DbtrAgt = (XmlElement)PmtInf.AppendChild(xml.CreateElement("DbtrAgt"));
                            #region DbtrAgt
                            {
                                XmlElement FinInstnId = (XmlElement)DbtrAgt.AppendChild(xml.CreateElement("FinInstnId"));
                                #region FinInstnId
                                {
                                    XmlElement BIC = (XmlElement)FinInstnId.AppendChild(xml.CreateElement("BIC"));
                                    BIC.InnerText = "";
                                    XmlElement Nm = (XmlElement)FinInstnId.AppendChild(xml.CreateElement("Nm"));
                                    Nm.InnerText = "";
                                }
                                #endregion FinInstnId
                            }
                            #endregion DbtrAgt
                            XmlElement ChrgBr = (XmlElement)PmtInf.AppendChild(xml.CreateElement("ChrgBr"));
                            ChrgBr.InnerText = "";
                            XmlElement CdtTrfTxInf = (XmlElement)PmtInf.AppendChild(xml.CreateElement("CdtTrfTxInf"));
                            #region CdtTrfTxInf
                            {
                                XmlElement PmtId = (XmlElement)CdtTrfTxInf.AppendChild(xml.CreateElement("PmtId"));
                                #region PmtId
                                {
                                    XmlElement InstrId = (XmlElement)PmtId.AppendChild(xml.CreateElement("InstrId"));
                                    InstrId.InnerText = "";
                                    XmlElement EndToEndId = (XmlElement)PmtId.AppendChild(xml.CreateElement("EndToEndId"));
                                    EndToEndId.InnerText = "";
                                }
                                #endregion PmtId
                                XmlElement PmtTpInfx = (XmlElement)CdtTrfTxInf.AppendChild(xml.CreateElement("PmtTpInf"));
                                #region PmtTpInf
                                {
                                    XmlElement InstrPrty = (XmlElement)PmtTpInfx.AppendChild(xml.CreateElement("InstrPrty"));
                                    InstrPrty.InnerText = "";
                                    XmlElement SvcLvl = (XmlElement)PmtTpInfx.AppendChild(xml.CreateElement("SvcLvl"));
                                    #region SvcLvl
                                    {
                                        XmlElement Cd = (XmlElement)SvcLvl.AppendChild(xml.CreateElement("Cd"));
                                        Cd.InnerText = "";
                                    }
                                    #endregion SvcLvl
                                    XmlElement LclInstrm = (XmlElement)PmtTpInfx.AppendChild(xml.CreateElement("LclInstrm"));
                                    #region LclInstrm
                                    {
                                        XmlElement Cd = (XmlElement)LclInstrm.AppendChild(xml.CreateElement("Cd"));
                                        Cd.InnerText = "";
                                    }
                                    #endregion LclInstrm
                                }
                                #endregion PmtTpInf
                                XmlElement Amt = (XmlElement)CdtTrfTxInf.AppendChild(xml.CreateElement("Amt"));
                                #region Amt
                                {
                                    XmlElement InstdAmt = (XmlElement)Amt.AppendChild(xml.CreateElement("InstdAmt"));
                                    InstdAmt.InnerText = "";
                                }
                                #endregion Amt
                                XmlElement CdtrAgt = (XmlElement)CdtTrfTxInf.AppendChild(xml.CreateElement("CdtrAgt"));
                                #region CdtrAgt
                                {
                                    XmlElement FinInstnId = (XmlElement)CdtrAgt.AppendChild(xml.CreateElement("FinInstnId"));
                                    #region FinInstnId
                                    {
                                        XmlElement BIC = (XmlElement)FinInstnId.AppendChild(xml.CreateElement("BIC"));
                                        BIC.InnerText = "";
                                        XmlElement Nm = (XmlElement)FinInstnId.AppendChild(xml.CreateElement("Nm"));
                                        Nm.InnerText = "";
                                        XmlElement PstlAdr = (XmlElement)FinInstnId.AppendChild(xml.CreateElement("PstlAdr"));
                                        #region PstlAdr
                                        {
                                            XmlElement Ctry = (XmlElement)PstlAdr.AppendChild(xml.CreateElement("Ctry"));
                                            Ctry.InnerText = "";
                                        }
                                        #endregion PstlAdr
                                    }
                                    #endregion FinInstnId
                                }
                                #endregion CdtrAgt
                                XmlElement Cdtr = (XmlElement)CdtTrfTxInf.AppendChild(xml.CreateElement("Cdtr"));
                                #region Cdtr
                                {
                                    XmlElement Nm = (XmlElement)Cdtr.AppendChild(xml.CreateElement("Nm"));
                                    Nm.InnerText = "";
                                    XmlElement CtryOfRes = (XmlElement)Cdtr.AppendChild(xml.CreateElement("CtryOfRes"));
                                    CtryOfRes.InnerText = "";
                                }
                                #endregion Cdtr
                                XmlElement CdtrAcct = (XmlElement)CdtTrfTxInf.AppendChild(xml.CreateElement("CdtrAcct"));
                                #region CdtrAcct
                                {
                                    XmlElement Id = (XmlElement)CdtrAcct.AppendChild(xml.CreateElement("Id"));
                                    #region Id
                                    {
                                        XmlElement IBAN = (XmlElement)Id.AppendChild(xml.CreateElement("IBAN"));
                                        IBAN.InnerText = p.BeneficiaryAccount;
                                    }
                                    #endregion Id
                                }
                                #endregion CdtrAcct
                                XmlElement RgltryRptg = (XmlElement)CdtTrfTxInf.AppendChild(xml.CreateElement("RgltryRptg"));
                                #region RgltryRptg
                                {
                                    XmlElement Authrty = (XmlElement)RgltryRptg.AppendChild(xml.CreateElement("Authrty"));
                                    #region Authrty
                                    {
                                        XmlElement Nm = (XmlElement)Authrty.AppendChild(xml.CreateElement("Nm"));
                                        Nm.InnerText = "";
                                        XmlElement Ctry = (XmlElement)Authrty.AppendChild(xml.CreateElement("Ctry"));
                                        Ctry.InnerText = "";
                                    }
                                    #endregion Authrty
                                    XmlElement Dtls = (XmlElement)RgltryRptg.AppendChild(xml.CreateElement("Dtls"));
                                    #region Dtls
                                    {
                                        XmlElement Tp = (XmlElement)Dtls.AppendChild(xml.CreateElement("Tp"));
                                        Tp.InnerText = "";
                                        XmlElement Dt = (XmlElement)Dtls.AppendChild(xml.CreateElement("Dt"));
                                        Dt.InnerText = "";
                                        XmlElement Ctry = (XmlElement)Dtls.AppendChild(xml.CreateElement("Ctry"));
                                        Ctry.InnerText = "";
                                        XmlElement Cd = (XmlElement)Dtls.AppendChild(xml.CreateElement("Cd"));
                                        Cd.InnerText = "";
                                        XmlElement Amtx = (XmlElement)Dtls.AppendChild(xml.CreateElement("Amt"));
                                        Amtx.InnerText = "";
                                        XmlElement Inf = (XmlElement)Dtls.AppendChild(xml.CreateElement("Inf"));
                                        Inf.InnerText = "";
                                    }
                                    #endregion Dtls
                                }
                                #endregion RgltryRptg
                                XmlElement RmtInf = (XmlElement)CdtTrfTxInf.AppendChild(xml.CreateElement("RmtInf"));
                                #region RmtInf
                                {
                                    XmlElement Ustrd = (XmlElement)RmtInf.AppendChild(xml.CreateElement("Ustrd"));
                                    Ustrd.InnerText = "";
                                    XmlElement Ustrdx = (XmlElement)RmtInf.AppendChild(xml.CreateElement("Ustrd"));
                                    Ustrdx.InnerText = "";
                                    XmlElement Strd = (XmlElement)RmtInf.AppendChild(xml.CreateElement("Strd"));
                                    #region Strd
                                    {
                                        XmlElement RfrdDocInf = (XmlElement)Strd.AppendChild(xml.CreateElement("RfrdDocInf"));
                                        #region RfrdDocInf
                                        {
                                            XmlElement Tp = (XmlElement)RfrdDocInf.AppendChild(xml.CreateElement("Tp"));
                                            #region Tp
                                            {
                                                XmlElement CdOrPrtry = (XmlElement)Tp.AppendChild(xml.CreateElement("CdOrPrtry"));
                                                #region CdOrPrtry
                                                {
                                                    XmlElement Cd = (XmlElement)CdOrPrtry.AppendChild(xml.CreateElement("Cd"));
                                                    Cd.InnerText = "";
                                                }
                                                #endregion CdOrPrtry
                                            }
                                            #endregion Tp
                                            XmlElement Nb = (XmlElement)RfrdDocInf.AppendChild(xml.CreateElement("Nb"));
                                            Nb.InnerText = "";
                                            XmlElement RltdDt = (XmlElement)RfrdDocInf.AppendChild(xml.CreateElement("RltdDt"));
                                            RltdDt.InnerText = "";
                                        }
                                        #endregion RfrdDocInf
                                        XmlElement CdtrRefInf = (XmlElement)Strd.AppendChild(xml.CreateElement("CdtrRefInf"));
                                        #region CdtrRefInf
                                        {
                                            XmlElement Ref = (XmlElement)CdtrRefInf.AppendChild(xml.CreateElement("Ref"));
                                            Ref.InnerText = "";
                                        }
                                        #endregion CdtrRefInf
                                    }
                                    #endregion Strd
                                }
                                #endregion RmtInf
                            }
                            #endregion CdtTrfTxInf
                        }
                        #endregion PmtInf
                    }
                }
                #endregion CstmrCdtTrfInitn
            }
            #endregion Document

            xml.Save(outpath);

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
