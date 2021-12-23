using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
using System.Globalization;

namespace TreasuryModel.Data
{
    public class CBSContact
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(CBSContact));

        public int CONTACTID { get; set; }
        public string CUSTOMERNUMBER { get; set; }
        public string PRIORITY { get; set; }
        public int SEQ { get; set; }
        public string CONTACTCODE { get; set; }
        public string COUNTRYCODE { get; set; }
        public string INTERDIRECTCODE { get; set; }
        public string CONTACTDETAIL { get; set; }
        public string CONTACTNAME { get; set; }
        public string INSUFFICIENTCONTACT { get; set; }
        public string AREACODE { get; set; }
        public string CONTACTEXT { get; set; }
        public string JOBTITLE { get; set; }
        public string ADDRESSLINKFLAG { get; set; }
        public string ACCOUNTLINKFLAG { get; set; }
        public DateTime CREATEDATE { get; set; }
        public DateTime LASTUPDDATE { get; set; }

        public string FORMATTEDNAMEFORSORTING { get; set; }

        public static List<CBSContact> GetList(string ConnectionString, string CIFID, string NAME)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select CONTACT.CONTACTID, CONTACT.CUSTOMERNUMBER, CUSTOMER.FORMATTEDNAMEFORSORTING, CONTACT.SEQ, CONTACT.CONTACTCODE, CONTACT.CONTACTDETAIL, CONTACT.CONTACTEXT From OPICINF.KKB_TB_CBS_CONTACT CONTACT LEFT JOIN OPICINF.KKB_TB_CBS_CUSTOMER CUSTOMER ON CONTACT.CUSTOMERNUMBER=CUSTOMER.CUSTOMERNUMBER  Where 1=1 ";
                if (!CIFID.Equals(""))
                    sql += "AND CONTACT.CUSTOMERNUMBER=:CIFID";
                if (!NAME.Equals(""))
                    sql += "AND ((TRIM(FORMATTEDNAMEFORSORTING) LIKE :NAME) OR (TRIM(ALTERNATEFORMATTEDNAME) LIKE :NAME)) ";

                Log.Info("-----CBSContact-----");
                Log.Info("SQL Statement : " + sql);

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                if (!CIFID.Equals(""))
                    o.setNCharInputParameter("CIFID", CIFID);
                if (!NAME.Equals(""))
                    o.setNCharInputParameter("Name", "%" + NAME.ToUpperInvariant().Trim() + "%");
                DataTable dt = o.executeReaderToDT("CBSContact");

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");


                List<CBSContact> param = new List<CBSContact>();
                int rownum = dt.Rows.Count > 100 ? 100 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    CBSContact p = new CBSContact();
                    p.CONTACTID = int.Parse(dt.Rows[i]["CONTACTID"].ToString().Trim());
                    p.CUSTOMERNUMBER = dt.Rows[i]["CUSTOMERNUMBER"].ToString().Trim();
                    p.FORMATTEDNAMEFORSORTING = dt.Rows[i]["FORMATTEDNAMEFORSORTING"].ToString().Trim();
                    p.SEQ = int.Parse(dt.Rows[i]["SEQ"].ToString().Trim());
                    p.CONTACTCODE = dt.Rows[i]["CONTACTCODE"].ToString().Trim();
                    p.CONTACTDETAIL = dt.Rows[i]["CONTACTDETAIL"].ToString().Trim();
                    p.CONTACTEXT = dt.Rows[i]["CONTACTEXT"].ToString().Trim();

                    param.Add(p);
                }

                //Return result to jTable
                return param;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public static object GetContact(string ConnectionString, int ContactID)
        {
            String sql = "Select Contact.*, Customer.FORMATTEDNAMEFORSORTING From OPICINF.KKB_TB_CBS_CONTACT CONTACT LEFT JOIN OPICINF.KKB_TB_CBS_CUSTOMER CUSTOMER ON CONTACT.CUSTOMERNUMBER=CUSTOMER.CUSTOMERNUMBER  Where Contact.ContactID='" + ContactID + "' ";
            Log.Info("-----KKB_TB_CBS_CONTACT Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("CBSContact");

                List<CBSContact> param = new List<CBSContact>();
                CBSContact p = new CBSContact();
                p.CONTACTID = int.Parse(dt.Rows[0]["CONTACTID"].ToString().Trim());
                p.CUSTOMERNUMBER = dt.Rows[0]["CUSTOMERNUMBER"].ToString().Trim();
                p.PRIORITY = dt.Rows[0]["PRIORITY"].ToString().Trim();
                p.SEQ = int.Parse(dt.Rows[0]["SEQ"].ToString().Trim());
                p.CONTACTCODE = dt.Rows[0]["CONTACTCODE"].ToString().Trim();
                p.COUNTRYCODE = dt.Rows[0]["COUNTRYCODE"].ToString().Trim();
                p.INTERDIRECTCODE = dt.Rows[0]["INTERDIRECTCODE"].ToString().Trim();
                p.CONTACTDETAIL = dt.Rows[0]["CONTACTDETAIL"].ToString().Trim();
                p.CONTACTNAME = dt.Rows[0]["CONTACTNAME"].ToString().Trim();
                p.INSUFFICIENTCONTACT = dt.Rows[0]["INSUFFICIENTCONTACT"].ToString().Trim();
                p.AREACODE = dt.Rows[0]["AREACODE"].ToString().Trim();
                p.CONTACTEXT = dt.Rows[0]["CONTACTEXT"].ToString().Trim();
                p.JOBTITLE = dt.Rows[0]["JOBTITLE"].ToString().Trim();

                p.ADDRESSLINKFLAG = dt.Rows[0]["ADDRESSLINKFLAG"].ToString().Trim();
                p.ACCOUNTLINKFLAG = dt.Rows[0]["ACCOUNTLINKFLAG"].ToString().Trim();
                p.FORMATTEDNAMEFORSORTING = dt.Rows[0]["FORMATTEDNAMEFORSORTING"].ToString().Trim();

                param.Add(p);

                //Return result to jTable
                return param;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----KKB_TB_CBS_CONTACT Finish-----");
                o.closeConnection();
            }
        }

        public static object DeleteContact(string ConnectionString, int ContactID)
        {

            Log.Info("-----KKB_TB_CBS_CONTACT Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.KKB_TB_CBS_CONTACT WHERE CONTACTID='" + ContactID.ToString() + "'";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_CBS_CONTACT Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_CBS_CONTACT Finish-----");
                o.closeConnection();
            }
        }

        protected static CBSCustomerService.InquiryCustomerContactResponseHeaderContact[] GetContactByCIFNo(string ServiceHosting, string CIFID)
        {
            try
            {
                Log.Debug("===Start Set Customer Contact Header====");
                CBSCustomerService.InquiryCustomerContactHeader header = new CBSCustomerService.InquiryCustomerContactHeader();
                header.ReferenceNo = "TRO" + DateTime.Now.ToString("yyyyMMddHHmmSS");
                header.ServiceName = "InquiryCustomerContact";
                header.SystemCode = "TRO";
                header.TransactionDateTime = DateTime.Now;
                header.ChannelID = "TRO";

                Log.Debug("===Start Set Customer Contact Detail====");
                CBSCustomerService.InquiryCustomerContactInquiryCustomerContactRequest request = new CBSCustomerService.InquiryCustomerContactInquiryCustomerContactRequest();
                request.CustomerNumber = CIFID;
                request.InquiryMode = "A";

                Log.Debug("====Start Set Customer Contact Paging===");
                CBSCustomerService.InquiryCustomerContactPaging paging = new CBSCustomerService.InquiryCustomerContactPaging();
                paging.PageNumber = 1;
                paging.PageSize = 50;

                //paging.SortDirection = "ASC";
                //paging.TotalRecord = 0;

                Log.Debug("===Start Set Customer Contact Object====");
                CBSCustomerService.InquiryCustomerContact contact = new CBSCustomerService.InquiryCustomerContact();
                contact.Header = header;
                contact.InquiryCustomerContactRequest = request;
                contact.Paging = paging;
                
                Log.Debug("===Start Customer Contact Request====");
                CBSCustomerService.CBSCustomerService c = new CBSCustomerService.CBSCustomerService();
                c.Url = ServiceHosting + "/ws/CBSCustomerService";
                CBSCustomerService.InquiryCustomerContactResponse response = c.InquiryCustomerContact(contact);
                CBSCustomerService.InquiryCustomerContactResponseHeaderContact[] obj = response.Header.ContactList;

                Log.Debug("==Request Completed====");
                return obj;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static CBSContact[] TransformCBSContact(string ServiceHosting, string CIFID)
        {
            try
            {
                CBSCustomerService.InquiryCustomerContactResponseHeaderContact[] obj = CBSContact.GetContactByCIFNo(ServiceHosting, CIFID);
                CBSContact[] contact = new CBSContact[obj.Count()];
                int i = 0;
                foreach (var c in obj)
                {
                    CBSContact ct = new CBSContact();
                    ct.CUSTOMERNUMBER = CIFID;
                    ct.ACCOUNTLINKFLAG = c.AccountLinkFlag;
                    ct.ADDRESSLINKFLAG = c.AddressLinkFlag;
                    ct.AREACODE = c.AreaCode;
                    ct.CONTACTCODE = c.ContactCode;
                    ct.CONTACTDETAIL = c.ContactDetail;
                    ct.CONTACTEXT = c.ContactExtensionNumber;
                    ct.CONTACTNAME = c.ContactName;
                    ct.COUNTRYCODE = c.CountryCode;
                    ct.INSUFFICIENTCONTACT = c.InsufficientAddressContact;
                    ct.INTERDIRECTCODE = c.InternationalDirectDiallingCode;
                    ct.JOBTITLE = c.JobTitle;
                    ct.PRIORITY = c.JobTitle;
                    ct.SEQ = int.Parse(c.Seq.ToString());
                    contact[i] = ct;
                    i++;
                }
                return contact;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static bool Save(string ConnectionString, CBSContact[] contact)
        {
            Log.Info("-----KKB_TB_CBS_CONTACT Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                foreach (CBSContact record in contact)
                {
                    string sql = "SELECT COUNT(*) FROM OPICINF.KKB_TB_CBS_CONTACT WHERE CUSTOMERNUMBER='" +record.CUSTOMERNUMBER +"' AND SEQ='" + record.SEQ +"' ";
                    o.createCommand(sql, CommandType.Text);
                    int rowcount = int.Parse(o.executeScalar().ToString());

                    if (rowcount <= 0)
                    {
                        sql = "Insert into OPICINF.KKB_TB_CBS_CONTACT(PRIORITY, CONTACTCODE, COUNTRYCODE, INTERDIRECTCODE, CONTACTDETAIL, CONTACTNAME, INSUFFICIENTCONTACT, AREACODE, CONTACTEXT, JOBTITLE, ADDRESSLINKFLAG, ACCOUNTLINKFLAG, CREATEDATE, CUSTOMERNUMBER, SEQ) ";
                        sql += "Values(:PRIORITY, :CONTACTCODE, :COUNTRYCODE, :INTERDIRECTCODE, :CONTACTDETAIL, :CONTACTNAME, :INSUFFICIENTCONTACT, :AREACODE, :CONTACTEXT, :JOBTITLE, :ADDRESSLINKFLAG, :ACCOUNTLINKFLAG, SYSDATE, :CUSTOMERNUMBER, :SEQ)";
                    }
                    else
                    {
                        sql = "Update OPICINF.KKB_TB_CBS_CONTACT SET PRIORITY=:PRIORITY, CONTACTCODE=:CONTACTCODE, COUNTRYCODE=:COUNTRYCODE, INTERDIRECTCODE=:INTERDIRECTCODE, CONTACTDETAIL=:CONTACTDETAIL, CONTACTNAME=:CONTACTNAME, INSUFFICIENTCONTACT=:INSUFFICIENTCONTACT, AREACODE=:AREACODE, CONTACTEXT=:CONTACTEXT, JOBTITLE=:JOBTITLE, ADDRESSLINKFLAG=:ADDRESSLINKFLAG, ACCOUNTLINKFLAG=:ACCOUNTLINKFLAG, LASTUPDATE=SYSDATE WHERE CUSTOMERNUMBER=:CUSTOMERNUMBER AND SEQ=:SEQ";
                    }
                    o.createCommand(sql, CommandType.Text);
                    o.setInputParameter("PRIORITY", record.PRIORITY);
                    o.setInputParameter("CONTACTCODE", record.CONTACTCODE);
                    o.setInputParameter("COUNTRYCODE", record.COUNTRYCODE);
                    o.setInputParameter("INTERDIRECTCODE", record.INTERDIRECTCODE);
                    o.setInputParameter("CONTACTDETAIL", record.CONTACTDETAIL);
                    o.setInputParameter("CONTACTNAME", record.CONTACTNAME);
                    o.setInputParameter("INSUFFICIENTCONTACT", record.INSUFFICIENTCONTACT);
                    o.setInputParameter("AREACODE", record.AREACODE);
                    o.setInputParameter("CONTACTEXT", record.CONTACTEXT);
                    o.setInputParameter("JOBTITLE", record.JOBTITLE);
                    o.setInputParameter("ADDRESSLINKFLAG", record.ADDRESSLINKFLAG);
                    o.setInputParameter("ACCOUNTLINKFLAG", record.ACCOUNTLINKFLAG);
                    o.setCharInputParameter("CUSTOMERNUMBER", record.CUSTOMERNUMBER);
                    o.setInputParameter("SEQ", record.SEQ);
                    row = o.executeNonQuery();

                    if (row <= 0)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKTB_TB_CBS_CONTACT Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKTB_TB_CBS_CONTACT Finish-----");
                o.closeConnection();
            }
        }

    }
}
