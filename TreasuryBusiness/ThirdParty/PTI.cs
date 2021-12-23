using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using System.Xml;
using TreasuryModel.Master;

namespace TreasuryBusiness.ThirdParty
{
    public class PTI
    {        
        public static String ExcelToXML(Model_MS_PTITRANS md)
        {
            try
            {
                    XmlDocument XD = new XmlDocument();
                    XmlNode Root = XD.AppendChild(XD.CreateElement("ns0", "CSDXML", "http://SET.IT.CSD.BizTalk.UploadDVPDF.Schema.UploadDVPDF"));
                    XmlAttribute versionAttr = Root.Attributes.Append(XD.CreateAttribute("Version"));
                    versionAttr.InnerText = "1.0";
                    XmlNode UploadNode = Root.AppendChild(XD.CreateElement("Upload"));

                    XmlNode AppHeaderNode = UploadNode.AppendChild(XD.CreateElement("AppHdrBlk"));
                    XmlNode PartiIDNode = AppHeaderNode.AppendChild(XD.CreateElement("PartiID"));
                    PartiIDNode.InnerText = md.PartialID;

                   
                    XmlNode TxtNode = UploadNode.AppendChild(XD.CreateElement("TxtBlk"));
                    XmlNode MsgTypeNode = TxtNode.AppendChild(XD.CreateElement("MsgType"));
                    XmlNode PriorityNode = TxtNode.AppendChild(XD.CreateElement("Priority"));
                    XmlNode SenderRefNode = TxtNode.AppendChild(XD.CreateElement("SenderRef"));
                    XmlNode RelateRefNode = TxtNode.AppendChild(XD.CreateElement("RelatedRef"));
                    XmlNode PlaceCodeNode = TxtNode.AppendChild(XD.CreateElement("PlaceCode"));
                    XmlNode SettlementDateNode = TxtNode.AppendChild(XD.CreateElement("SettlementDate"));
                    XmlNode TradeDateNode = TxtNode.AppendChild(XD.CreateElement("TradeDate"));
                    XmlNode PercentageTypeNode = TxtNode.AppendChild(XD.CreateElement("PercentageType"));
                    XmlNode DealPriceNode = TxtNode.AppendChild(XD.CreateElement("DealPrice"));
                    XmlNode ISINNode = TxtNode.AppendChild(XD.CreateElement("ISIN"));
                    XmlNode FaceAmountNode = TxtNode.AppendChild(XD.CreateElement("FaceAmount"));
                    XmlNode AcctNoNode = TxtNode.AppendChild(XD.CreateElement("AcctNo"));
                    XmlNode CashAcctNode = TxtNode.AppendChild(XD.CreateElement("CashAcct"));
                    XmlNode BusinessTypeNode = TxtNode.AppendChild(XD.CreateElement("BusinessType"));
                    XmlNode SettlementTypeNode = TxtNode.AppendChild(XD.CreateElement("SettlementType"));
                    XmlNode SettlementPlaceNode = TxtNode.AppendChild(XD.CreateElement("SettlementPlace"));
                    XmlNode AgentBICNode = TxtNode.AppendChild(XD.CreateElement("AgentBIC"));
                    XmlNode AgentAcctnoNode = TxtNode.AppendChild(XD.CreateElement("AgentAcctNo"));
                    XmlNode SettlementAmtNode = TxtNode.AppendChild(XD.CreateElement("SettlementAmt"));


                    MsgTypeNode.InnerText = md.MsgType;
                    PriorityNode.InnerText = md.Priority;
                    SenderRefNode.InnerText = md.SenderRef;
                    RelateRefNode.InnerText = md.RelateRef;
                    PlaceCodeNode.InnerText = md.PlaceCode;
                    SettlementDateNode.InnerText = md.SettDate;
                    TradeDateNode.InnerText = md.TradeDate;
                    PercentageTypeNode.InnerText = md.PercentType;
                    DealPriceNode.InnerText = md.DealPrice;
                    ISINNode.InnerText = md.ISIN;
                    FaceAmountNode.InnerText = md.FaceAmt;
                    AcctNoNode.InnerText = md.AcctNo;
                    CashAcctNode.InnerText = md.CashAcct;
                    BusinessTypeNode.InnerText = md.BusinessType;
                    SettlementTypeNode.InnerText = md.SettType;
                    SettlementPlaceNode.InnerText = md.SettPlace;
                    AgentBICNode.InnerText = md.AgentBIC;
                    AgentAcctnoNode.InnerText = md.AgentAcctNo;
                    SettlementAmtNode.InnerText = md.SettAmt;
                    return XD.InnerXml;
            }
            catch (Exception ex)
            { 
                return null;
            }
        }

        public static String ExcelToXMLList(List<Model_MS_PTITRANS> l)
        {
            try
            {
                XmlDocument XD = new XmlDocument();
                XmlNode Root = XD.AppendChild(XD.CreateElement("ns0", "CSDXML", "http://SET.IT.CSD.BizTalk.UploadDVPDF.Schema.UploadDVPDF"));
                XmlAttribute versionAttr = Root.Attributes.Append(XD.CreateAttribute("Version"));
                versionAttr.InnerText = "1.0";
                
                //Loop allotment data
                foreach(Model_MS_PTITRANS node in l){
                    XmlNode UploadNode = Root.AppendChild(XD.CreateElement("Upload"));
                    XmlNode AppHeaderNode = UploadNode.AppendChild(XD.CreateElement("AppHdrBlk"));
                    XmlNode PartiIDNode = AppHeaderNode.AppendChild(XD.CreateElement("PartiID"));
                    PartiIDNode.InnerText = node.PartialID;


                    XmlNode TxtNode = UploadNode.AppendChild(XD.CreateElement("TxtBlk"));
                    XmlNode MsgTypeNode = TxtNode.AppendChild(XD.CreateElement("MsgType"));
                    XmlNode PriorityNode = TxtNode.AppendChild(XD.CreateElement("Priority"));
                    XmlNode SenderRefNode = TxtNode.AppendChild(XD.CreateElement("SenderRef"));
                    XmlNode RelateRefNode = TxtNode.AppendChild(XD.CreateElement("RelatedRef"));
                    XmlNode PlaceCodeNode = TxtNode.AppendChild(XD.CreateElement("PlaceCode"));
                    XmlNode SettlementDateNode = TxtNode.AppendChild(XD.CreateElement("SettlementDate"));
                    XmlNode TradeDateNode = TxtNode.AppendChild(XD.CreateElement("TradeDate"));
                    XmlNode PercentageTypeNode = TxtNode.AppendChild(XD.CreateElement("PercentageType"));
                    XmlNode DealPriceNode = TxtNode.AppendChild(XD.CreateElement("DealPrice"));
                    XmlNode ISINNode = TxtNode.AppendChild(XD.CreateElement("ISIN"));
                    XmlNode FaceAmountNode = TxtNode.AppendChild(XD.CreateElement("FaceAmount"));
                    XmlNode AcctNoNode = TxtNode.AppendChild(XD.CreateElement("AcctNo"));
                    XmlNode CashAcctNode = TxtNode.AppendChild(XD.CreateElement("CashAcct"));
                    XmlNode BusinessTypeNode = TxtNode.AppendChild(XD.CreateElement("BusinessType"));
                    XmlNode SettlementTypeNode = TxtNode.AppendChild(XD.CreateElement("SettlementType"));
                    XmlNode SettlementPlaceNode = TxtNode.AppendChild(XD.CreateElement("SettlementPlace"));
                    XmlNode AgentBICNode = TxtNode.AppendChild(XD.CreateElement("AgentBIC"));
                    XmlNode AgentAcctnoNode = TxtNode.AppendChild(XD.CreateElement("AgentAcctNo"));
                    XmlNode SettlementAmtNode = TxtNode.AppendChild(XD.CreateElement("SettlementAmt"));

                    MsgTypeNode.InnerText = node.MsgType;
                    PriorityNode.InnerText = node.Priority;
                    SenderRefNode.InnerText = node.SenderRef;
                    RelateRefNode.InnerText = node.RelateRef;
                    PlaceCodeNode.InnerText = node.PlaceCode;
                    SettlementDateNode.InnerText = node.SettDate;
                    TradeDateNode.InnerText = node.TradeDate;
                    PercentageTypeNode.InnerText = node.PercentType;
                    DealPriceNode.InnerText = node.DealPrice;
                    ISINNode.InnerText = node.ISIN;
                    FaceAmountNode.InnerText = node.FaceAmt;
                    AcctNoNode.InnerText = node.AcctNo;
                    CashAcctNode.InnerText = node.CashAcct;
                    BusinessTypeNode.InnerText = node.BusinessType;
                    SettlementTypeNode.InnerText = node.SettType;
                    SettlementPlaceNode.InnerText = node.SettPlace;
                    AgentBICNode.InnerText = node.AgentBIC;
                    AgentAcctnoNode.InnerText = node.AgentAcctNo;
                    SettlementAmtNode.InnerText = node.SettAmt;
                
                }
               
                return XD.InnerXml;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
