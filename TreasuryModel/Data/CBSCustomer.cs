using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KKB.Treasury.TreasuryCommon.Common;
using System.Data;
using log4net;
using System.Globalization; 
using KKB.Treasury.TreasuryModel.Configuration; 

namespace TreasuryModel.Data
{
    public class CBSCustomer
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(CBSCustomer));

        public int CBSID { get; set; }
        public string CUSTOMERNUMBER { get; set; }
        public string CNO { get; set; }
        public string TITLE { get; set; }
        public string FORMATTEDNAMEFORSORTING { get; set; }
        public string ALTERNATEFORMATTEDNAME { get; set; }
        public string PRIMARYNAME01 { get; set; }
        public string PRIMARYNAME02 { get; set; }
        public string PRIMARYNAME03 { get; set; }
        public string PRIMARYNAME04 { get; set; }
        public string ALTERNATENAME01 { get; set; }
        public string ALTERNATENAME02 { get; set; }
        public string ALTERNATENAME03 { get; set; }
        public string ALTERNATENAME04 { get; set; }
        public string ADDRESSLINE1 { get; set; }
        public string ADDRESSLINE2 { get; set; }
        public string ADDRESSLINE3 { get; set; }
        public string ADDRESSLINE4 { get; set; }
        public string ADDRESSLINE5 { get; set; }
        public string ADDRESSLINE6 { get; set; }
        public string ADDRESSLINE7 { get; set; }
        public DateTime BIRTHDATE { get; set; }
        public string BOIFLAG { get; set; }
        public string COUNTRYCODE { get; set; }
        public string COUNTRYOFCITIZENSHIPINCORPO { get; set; }
        public string CUSTOMERCATEGORY { get; set; }
        public string CUSTOMERSUBTYPE { get; set; }
        public string IDISSUECOUNTRYCODE { get; set; }
        public string IDNUMBER { get; set; }
        public string IDTYPECODE { get; set; }
        public string STATECODE { get; set; }
        public string POSTALCODE { get; set; }
        public string TAXNUMBER { get; set; }
        public string ENABLED { get; set; }

        public string BIRTHDATE_STR { get; set; }
        public string TITLEEN { get; set; }
        public string CUSTOMERCATEGORYDESC { get; set; }
        public string CUSTOMERSUBTYPEDESC { get; set; }
        public string IDTYPECODEDESC { get; set; }

        public static List<CBSCustomer> GetList(string ConnectionString, string CIFID, string NAME, bool Opics, bool DataSet)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select CBSID, CUSTOMERNUMBER, CNO, TITLE, FORMATTEDNAMEFORSORTING, IDTYPECODE, IDNUMBER, ENABLED From OPICINF.KKB_TB_CBS_CUSTOMER  Where Enabled='1' ";
                if (!CIFID.Equals(""))
                    sql += "AND TRIM(CUSTOMERNUMBER)=:CIFID ";
                if (!NAME.Equals(""))
                    sql += "AND ((TRIM(FORMATTEDNAMEFORSORTING) LIKE :NAME) OR (TRIM(ALTERNATEFORMATTEDNAME) LIKE :NAME)) ";
                if (Opics)
                    sql += "AND TRIM(CNO) NOT IN (SELECT TRIM(CNO) FROM OPICS.CUST) ";
                if (DataSet)
                    sql += "AND TRIM(CNO) NOT IN (SELECT TRIM(CNO) FROM TDS.DS_IPI) ";

                Log.Info("-----CBCustomerList-----");
                Log.Info("SQL Statement : " + sql);

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                if (!CIFID.Equals(""))
                    o.setNCharInputParameter("CIFID", CIFID);
                if (!NAME.Equals(""))
                    o.setNCharInputParameter("Name", "%" + NAME.ToUpperInvariant().Trim() + "%");
                DataTable dt = o.executeReaderToDT("CBSCustomer");

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");


                List<CBSCustomer> param = new List<CBSCustomer>();
                int rownum = dt.Rows.Count > 100 ? 100 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    CBSCustomer p = new CBSCustomer();
                    p.CBSID = int.Parse(dt.Rows[i]["CBSID"].ToString());
                    p.CUSTOMERNUMBER = dt.Rows[i]["CUSTOMERNUMBER"].ToString();
                    p.CNO = dt.Rows[i]["CNO"].ToString();
                    p.TITLE = dt.Rows[i]["TITLE"].ToString();
                    p.FORMATTEDNAMEFORSORTING = dt.Rows[i]["FORMATTEDNAMEFORSORTING"].ToString();
                    p.IDTYPECODE = dt.Rows[i]["IDTYPECODE"].ToString();
                    p.IDNUMBER = dt.Rows[i]["IDNUMBER"].ToString();
                    p.ENABLED = dt.Rows[i]["ENABLED"].ToString();


                    DataTable IDTypeCodeData = CBSMapping.GetMappingIDTypeCode(ConnectionString, CBSDESTINATION.TDS);
                    DataTable TitleEnData = CBSMapping.GetMappingTitleEn(ConnectionString, CBSDESTINATION.TDS);

                    DataRow drIDTypeCode = IDTypeCodeData.Select("CBSCODE='" + dt.Rows[i]["IDTYPECODE"].ToString().Trim() + "'").FirstOrDefault();
                    string IDTypeCodeText = drIDTypeCode != null ? drIDTypeCode["CBSDESCRIPTION"].ToString() : "";
                    p.IDTYPECODEDESC = IDTypeCodeText;

                    DataRow drTitleEn = TitleEnData.Select("CBSCODE='" + dt.Rows[i]["TITLE"].ToString().Trim() + "'").FirstOrDefault();
                    string TitleEnText = drTitleEn != null ? drTitleEn["CBSDESCRIPTION"].ToString() : "";
                    p.TITLEEN = TitleEnText;

                    param.Add(p);
                }

                //Return result to jTable
                return param;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                o.closeConnection();
            }
        }

        public static object GetCustomer(string ConnectionString, int CBSID)
        {
            String sql = "Select CUSTOMER.*, TO_CHAR(BIRTHDATE,'MM/DD/YYYY HH:MI:SS') as BIRTHDATE_MDY From OPICINF.KKB_TB_CBS_CUSTOMER CUSTOMER Where CUSTOMER.CBSID='" + CBSID + "' ";
            Log.Info("-----KKB_TB_CBS_CUSTOMER Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("CBSCustomer");

                List<CBSCustomer> param = new List<CBSCustomer>();
                CBSCustomer p = new CBSCustomer();
                p.CBSID = int.Parse(dt.Rows[0]["CBSID"].ToString().Trim());
                p.CUSTOMERNUMBER = dt.Rows[0]["CUSTOMERNUMBER"].ToString().Trim();
                p.CNO = dt.Rows[0]["CNO"].ToString().Trim();
                p.TITLE = dt.Rows[0]["TITLE"].ToString().Trim();
                p.FORMATTEDNAMEFORSORTING = dt.Rows[0]["FORMATTEDNAMEFORSORTING"].ToString().Trim();
                p.ALTERNATEFORMATTEDNAME = dt.Rows[0]["ALTERNATEFORMATTEDNAME"].ToString().Trim();
                p.PRIMARYNAME01 = dt.Rows[0]["PRIMARYNAME01"].ToString().Trim();
                p.PRIMARYNAME02 = dt.Rows[0]["PRIMARYNAME02"].ToString().Trim();
                p.PRIMARYNAME03 = dt.Rows[0]["PRIMARYNAME03"].ToString().Trim();
                p.PRIMARYNAME04 = dt.Rows[0]["PRIMARYNAME04"].ToString().Trim();
                p.ALTERNATENAME01 = dt.Rows[0]["ALTERNATENAME01"].ToString().Trim();
                p.ALTERNATENAME02 = dt.Rows[0]["ALTERNATENAME02"].ToString().Trim();
                p.ALTERNATENAME03 = dt.Rows[0]["ALTERNATENAME03"].ToString().Trim();
                p.ALTERNATENAME04 = dt.Rows[0]["ALTERNATENAME04"].ToString().Trim();
                p.ADDRESSLINE1 = dt.Rows[0]["ADDRESSLINE1"].ToString().Trim();
                p.ADDRESSLINE2 = dt.Rows[0]["ADDRESSLINE2"].ToString().Trim();
                p.ADDRESSLINE3 = dt.Rows[0]["ADDRESSLINE3"].ToString().Trim();
                p.BIRTHDATE = DateTime.Parse(dt.Rows[0]["BIRTHDATE_MDY"].ToString().Trim());
                p.BOIFLAG = dt.Rows[0]["BOIFLAG"].ToString().Trim();
                p.COUNTRYCODE = dt.Rows[0]["COUNTRYCODE"].ToString().Trim();
                p.COUNTRYOFCITIZENSHIPINCORPO = dt.Rows[0]["COUNTRYOFCITIZENSHIPINCORPO"].ToString().Trim();
                p.CUSTOMERCATEGORY = dt.Rows[0]["CUSTOMERCATEGORY"].ToString().Trim();
                p.CUSTOMERSUBTYPE = dt.Rows[0]["CUSTOMERSUBTYPE"].ToString().Trim();
                p.IDISSUECOUNTRYCODE = dt.Rows[0]["IDISSUECOUNTRYCODE"].ToString().Trim();
                p.IDNUMBER = dt.Rows[0]["IDNUMBER"].ToString().Trim();
                p.IDTYPECODE = dt.Rows[0]["IDTYPECODE"].ToString().Trim();
                p.STATECODE = dt.Rows[0]["STATECODE"].ToString().Trim();
                p.POSTALCODE = dt.Rows[0]["POSTALCODE"].ToString().Trim();
                p.TAXNUMBER = dt.Rows[0]["TAXNUMBER"].ToString().Trim();
                p.ENABLED = dt.Rows[0]["ENABLED"].ToString().Trim();

                p.BIRTHDATE_STR = p.BIRTHDATE.ToString("dd/MM/yyyy");


                DataTable CustomerCategoryData = CBSMapping.GetMappingCustomerCategory(ConnectionString, CBSDESTINATION.TDS);
                DataTable CustomerSubTypeData = CBSMapping.GetMappingCustomerSubType(ConnectionString, CBSDESTINATION.TDS);
                DataTable IDTypeCodeData = CBSMapping.GetMappingIDTypeCode(ConnectionString, CBSDESTINATION.TDS);
                DataTable TitleEnData = CBSMapping.GetMappingTitleEn(ConnectionString, CBSDESTINATION.TDS);
                DataTable TitleThData = CBSMapping.GetMappingTitleTh(ConnectionString, CBSDESTINATION.TDS);

                DataRow drCustomerCategory = CustomerCategoryData.Select("CBSCODE='" + dt.Rows[0]["CUSTOMERCATEGORY"].ToString().Trim() + "'").FirstOrDefault();
                string CustomerCategoryText = drCustomerCategory != null ? drCustomerCategory["CBSDESCRIPTION"].ToString() : "";
                p.CUSTOMERCATEGORYDESC = CustomerCategoryText;

                DataRow drCustomerSubType = CustomerSubTypeData.Select("CBSCODE='" + dt.Rows[0]["CUSTOMERSUBTYPE"].ToString().Trim() + "'").FirstOrDefault();
                string CustomerSubTypeText = drCustomerSubType != null ? drCustomerSubType["CBSDESCRIPTION"].ToString() : "";
                p.CUSTOMERSUBTYPEDESC = CustomerSubTypeText;

                DataRow drIDTypeCode = IDTypeCodeData.Select("CBSCODE='" + dt.Rows[0]["IDTYPECODE"].ToString().Trim() + "'").FirstOrDefault();
                string IDTypeCodeText = drIDTypeCode != null ? drIDTypeCode["CBSDESCRIPTION"].ToString() : "";
                p.IDTYPECODEDESC = IDTypeCodeText;

                DataRow drTitleEn = TitleEnData.Select("CBSCODE='" + dt.Rows[0]["TITLE"].ToString().Trim() + "'").FirstOrDefault();
                string TitleEnText = drTitleEn != null ? drTitleEn["CBSDESCRIPTION"].ToString() : "";
                p.TITLEEN = TitleEnText;

                
                param.Add(p);

                //Return result to jTable
                return param;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Could not get data" };
            }
            finally
            {
                Log.Info("-----KKB_TB_CBS_CONTACT Finish-----");
                o.closeConnection();
            }
        }

        public static object GetCustomer(string ConnectionString, int[] CBSID)
        {
            string cbsstr = "";
            foreach (int c in CBSID)
            {
                cbsstr += c.ToString() + " ";
            }
            cbsstr = cbsstr.Trim();
            cbsstr = cbsstr.Replace(' ',',');

            String sql = "Select CUSTOMER.*, TO_CHAR(BIRTHDATE,'MM/DD/YYYY HH:MI:SS') as BIRTHDATE_MDY From OPICINF.KKB_TB_CBS_CUSTOMER CUSTOMER Where CUSTOMER.CBSID in (" + cbsstr + ") ";
            Log.Info("-----KKB_TB_CBS_CUSTOMER Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("CBSCustomer");

                List<CBSCustomer> param = new List<CBSCustomer>();
                CBSCustomer p = new CBSCustomer();
                p.CBSID = int.Parse(dt.Rows[0]["CBSID"].ToString().Trim());
                p.CUSTOMERNUMBER = dt.Rows[0]["CUSTOMERNUMBER"].ToString().Trim();
                p.CNO = dt.Rows[0]["CNO"].ToString().Trim();
                p.TITLE = dt.Rows[0]["TITLE"].ToString().Trim();
                p.FORMATTEDNAMEFORSORTING = dt.Rows[0]["FORMATTEDNAMEFORSORTING"].ToString().Trim();
                p.ALTERNATEFORMATTEDNAME = dt.Rows[0]["ALTERNATEFORMATTEDNAME"].ToString().Trim();
                p.PRIMARYNAME01 = dt.Rows[0]["PRIMARYNAME01"].ToString().Trim();
                p.PRIMARYNAME02 = dt.Rows[0]["PRIMARYNAME02"].ToString().Trim();
                p.PRIMARYNAME03 = dt.Rows[0]["PRIMARYNAME03"].ToString().Trim();
                p.PRIMARYNAME04 = dt.Rows[0]["PRIMARYNAME04"].ToString().Trim();
                p.ALTERNATENAME01 = dt.Rows[0]["ALTERNATENAME01"].ToString().Trim();
                p.ALTERNATENAME02 = dt.Rows[0]["ALTERNATENAME02"].ToString().Trim();
                p.ALTERNATENAME03 = dt.Rows[0]["ALTERNATENAME03"].ToString().Trim();
                p.ALTERNATENAME04 = dt.Rows[0]["ALTERNATENAME04"].ToString().Trim();
                p.ADDRESSLINE1 = dt.Rows[0]["ADDRESSLINE1"].ToString().Trim();
                p.ADDRESSLINE2 = dt.Rows[0]["ADDRESSLINE2"].ToString().Trim();
                p.ADDRESSLINE3 = dt.Rows[0]["ADDRESSLINE3"].ToString().Trim();
                p.BIRTHDATE = DateTime.Parse(dt.Rows[0]["BIRTHDATE_MDY"].ToString().Trim());
                p.BOIFLAG = dt.Rows[0]["BOIFLAG"].ToString().Trim();
                p.COUNTRYCODE = dt.Rows[0]["COUNTRYCODE"].ToString().Trim();
                p.COUNTRYOFCITIZENSHIPINCORPO = dt.Rows[0]["COUNTRYOFCITIZENSHIPINCORPO"].ToString().Trim();
                p.CUSTOMERCATEGORY = dt.Rows[0]["CUSTOMERCATEGORY"].ToString().Trim();
                p.CUSTOMERSUBTYPE = dt.Rows[0]["CUSTOMERSUBTYPE"].ToString().Trim();
                p.IDISSUECOUNTRYCODE = dt.Rows[0]["IDISSUECOUNTRYCODE"].ToString().Trim();
                p.IDNUMBER = dt.Rows[0]["IDNUMBER"].ToString().Trim();
                p.IDTYPECODE = dt.Rows[0]["IDTYPECODE"].ToString().Trim();
                p.STATECODE = dt.Rows[0]["STATECODE"].ToString().Trim();
                p.POSTALCODE = dt.Rows[0]["POSTALCODE"].ToString().Trim();
                p.TAXNUMBER = dt.Rows[0]["TAXNUMBER"].ToString().Trim();
                p.ENABLED = dt.Rows[0]["ENABLED"].ToString().Trim();

                p.BIRTHDATE_STR = p.BIRTHDATE.ToString("dd/MM/yyyy");

                param.Add(p);

                //Return result to jTable
                return param;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Could not get data." };
            }
            finally
            {
                Log.Info("-----KKB_TB_CBS_CONTACT Finish-----");
                o.closeConnection();
            }
        }
        
        protected static CBSCustomerService.InquiryCustomerDetailResponseHeaderCustomerInfoCustomer GetCustomerByCIFNO(string ServiceHosting, string CIFID)
        {
            try
            {
                Log.Debug("===Start Set Customer Detail Header====");
                CBSCustomerService.InquiryCustomerDetailHeader header = new CBSCustomerService.InquiryCustomerDetailHeader();
                header.ReferenceNo = "TRO" + DateTime.Now.ToString("yyyyMMddHHmmss");
                header.ServiceName = "InquiryCustomerDetail";
                header.SystemCode = "TRO";
                header.TransactionDateTime = DateTime.Now;
                header.ChannelID = "TRO";

                Log.Debug("===Start Set Customer Detail Detail====");
                CBSCustomerService.InquiryCustomerDetailInquiryCustomerDetailRequest request = new CBSCustomerService.InquiryCustomerDetailInquiryCustomerDetailRequest();
                request.CustomerNumber = CIFID;
                //request.IDIssueCountryCode = "TH";
                //request.IDNumber = "7654321098761";
                //request.IDTypeCode = "001";
                
                Log.Debug("===Start Set Customer Detail====");
                CBSCustomerService.InquiryCustomerDetail customer = new CBSCustomerService.InquiryCustomerDetail();
                customer.Header = header;
                customer.InquiryCustomerDetailRequest = request;
                
                Log.Debug("===Start Customer Detail Request====");
                CBSCustomerService.CBSCustomerService c = new CBSCustomerService.CBSCustomerService();
                c.Url = ServiceHosting + "/ws/CBSCustomerService";
                Log.Debug(c.Url); 
                CBSCustomerService.InquiryCustomerDetailResponse response = c.InquiryCustomerDetail(customer);
                CBSCustomerService.InquiryCustomerDetailResponseHeaderCustomerInfoCustomer cust = response.Header.CustomerInfo.Customer;
                Log.Debug("==Request Completed====");
                return cust;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static object SyncCustomer(string ConnectionString, string ServiceHosting, string CIFID)
        {
            try
            {

                CBSCustomerService.InquiryCustomerDetailResponseHeaderCustomerInfoCustomer c = CBSCustomer.GetCustomerByCIFNO(ServiceHosting, CIFID);

                List<CBSCustomer> param = new List<CBSCustomer>();
                CBSCustomer cust = new CBSCustomer();
                if (c != null)
                {
                    cust.ADDRESSLINE1 = c.AddressInfo.Address.AddressLine1;
                    cust.ADDRESSLINE2 = c.AddressInfo.Address.AddressLine2;
                    cust.ADDRESSLINE3 = c.AddressInfo.Address.AddressLine3;
                    cust.ADDRESSLINE4 = c.AddressInfo.Address.AddressLine4;
                    cust.ADDRESSLINE5 = c.AddressInfo.Address.AddressLine5;
                    cust.ADDRESSLINE6 = c.AddressInfo.Address.AddressLine6;
                    cust.ADDRESSLINE7 = c.AddressInfo.Address.AddressLine7;
                    cust.ALTERNATEFORMATTEDNAME = c.CustomerNameInfo.CustomerName.AlternateCustomerName;
                    //cust.ALTERNATENAME01 = c.AlternateName01; //Not Use
                    //cust.ALTERNATENAME02 = c.AlternateName02; //Not Use
                    //cust.ALTERNATENAME03 = c.AlternateName03; //Not Use
                    //cust.ALTERNATENAME04 = c.AlternateName04; //Not Use
                    cust.BIRTHDATE = DateTime.Parse(c.BirthDate.ToString()); //
                    DateTime BirthDate = TimeZoneInfo.ConvertTimeFromUtc(cust.BIRTHDATE, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
                    cust.BIRTHDATE_STR = BirthDate.ToString("dd/MM/yyyy");
                    cust.BOIFLAG = c.CentralBankCodeInfo.CentralBankCode.BOIFlag;
                    cust.COUNTRYCODE = c.AddressInfo.Address.CountryCode;
                    cust.COUNTRYOFCITIZENSHIPINCORPO = c.CountryOfCitizenship;
                    cust.CUSTOMERCATEGORY = c.CustomerCategory;
                    cust.CUSTOMERNUMBER = c.CustomerNumber;
                    cust.CUSTOMERSUBTYPE = c.CustomerSubType.ToString(); //
                    cust.FORMATTEDNAMEFORSORTING = c.CustomerNameInfo.CustomerName.CustomerName;
                    cust.IDISSUECOUNTRYCODE = c.IDInfo.ID.IDIssueCountryCode;
                    cust.IDNUMBER = c.IDInfo.ID.IDNumber;
                    cust.IDTYPECODE = c.IDInfo.ID.IDTypeCode;
                    cust.POSTALCODE = c.AddressInfo.Address.PostalCode;
                    //cust.PRIMARYNAME01 = c.PrimaryName01; //CMNE
                    //cust.PRIMARYNAME02 = c.PrimaryName02; //SN
                    //cust.PRIMARYNAME03 = c.PrimaryName03; //BIC
                    cust.PRIMARYNAME04 = c.CentralBankCodeInfo.CentralBankCode.CentralBankCode01; //ISIC

                    cust.STATECODE = c.AddressInfo.Address.StateCode;
                    cust.TITLE = c.CustomerNameInfo.CustomerName.TitleCode;
                    if (c.IDInfo.ID.AlternateIDType.Equals("003"))
                        cust.TAXNUMBER = c.IDInfo.ID.AlternateIDNumber; //
                    else if (c.IDInfo.ID.IDTypeCode.Equals("003"))
                        cust.TAXNUMBER = c.IDInfo.ID.IDNumber;

                    cust.CNO = "";

                    DataTable CustomerCategoryData = CBSMapping.GetMappingCustomerCategory(ConnectionString, CBSDESTINATION.TDS);
                    DataTable CustomerSubTypeData = CBSMapping.GetMappingCustomerSubType(ConnectionString, CBSDESTINATION.TDS);
                    DataTable IDTypeCodeData = CBSMapping.GetMappingIDTypeCode(ConnectionString, CBSDESTINATION.TDS);
                    DataTable TitleEnData = CBSMapping.GetMappingTitleEn(ConnectionString, CBSDESTINATION.TDS);
                    DataTable TitleThData = CBSMapping.GetMappingTitleTh(ConnectionString, CBSDESTINATION.TDS);

                    try
                    {
                        DataRow drCustomerCategory = CustomerCategoryData.Select("CBSCODE='" + c.CustomerCategory.ToString().Trim() + "'").FirstOrDefault();
                        string CustomerCategoryText = drCustomerCategory != null ? drCustomerCategory["CBSDESCRIPTION"].ToString() : "";
                        cust.CUSTOMERCATEGORYDESC = CustomerCategoryText;
                    }
                    catch (Exception ex) { Log.Error("CustomerCategory Error : " + ex.Message); }
                    try
                    {
                        DataRow drCustomerSubType = CustomerSubTypeData.Select("CBSCODE='" + c.CustomerSubType.ToString().Trim() + "'").FirstOrDefault();
                        string CustomerSubTypeText = drCustomerSubType != null ? drCustomerSubType["CBSDESCRIPTION"].ToString() : "";
                        cust.CUSTOMERSUBTYPEDESC = CustomerSubTypeText;
                    }
                    catch (Exception ex) { Log.Error("CustomerSubType : " + ex.Message); }
                    try
                    {
                        DataRow drIDTypeCode = IDTypeCodeData.Select("CBSCODE='" + c.IDInfo.ID.IDTypeCode.ToString().Trim() + "'").FirstOrDefault();
                        string IDTypeCodeText = drIDTypeCode != null ? drIDTypeCode["CBSDESCRIPTION"].ToString() : "";
                        cust.IDTYPECODEDESC = IDTypeCodeText;
                    }
                    catch (Exception ex) { Log.Error("IDTypeCode Error : " + ex.Message); }
                    try
                    {
                        DataRow drTitleEn = TitleEnData.Select("CBSCODE='" + c.CustomerNameInfo.CustomerName.TitleCode.ToString().Trim() + "'").FirstOrDefault();
                        string TitleEnText = drTitleEn != null ? drTitleEn["CBSDESCRIPTION"].ToString() : "";
                        cust.TITLEEN = TitleEnText;
                    }
                    catch (Exception ex) { Log.Error("TitleEn Error : " + ex.Message); }
                    param.Add(cust);
                    return param;
                }
                else
                    return new { Result = "ERROR", Message = "Data not exists." };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Data not exists." };
            }
        }

        public static object Save(string ConnectionString, CBSCustomer record)
        {
            Log.Info("-----KKB_TB_CBS_CUSTOMER Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                ////string sql = "SELECT COUNT(*) FROM OPICINF.KKB_TB_CBS_CUSTOMER WHERE CUSTOMERNUMBER='" + record.CUSTOMERNUMBER + "' AND IDNUMBER='" + record.IDNUMBER + "' AND IDTYPECODE='" + record.IDTYPECODE + "' AND IDISSUECOUNTRYCODE='" + record.IDISSUECOUNTRYCODE + "' ";
                //string sql = "SELECT COUNT(*) FROM OPICINF.KKB_TB_CBS_CUSTOMER WHERE CUSTOMERNUMBER='" + record.CUSTOMERNUMBER + "' ";
                //o.createCommand(sql, CommandType.Text);
                //int rowcount = int.Parse(o.executeScalar().ToString());

                o.createCommand("DELETE FROM OPICINF.KKB_TB_CBS_CUSTOMER WHERE TRIM(CUSTOMERNUMBER)='" + record.CUSTOMERNUMBER + "'", CommandType.Text);

                //if (rowcount <= 0)
                //{
                string sql = "Insert Into OPICINF.KKB_TB_CBS_CUSTOMER(CNO, TITLE, FORMATTEDNAMEFORSORTING, ALTERNATEFORMATTEDNAME, PRIMARYNAME01, PRIMARYNAME02, PRIMARYNAME03, PRIMARYNAME04, ALTERNATENAME01, ALTERNATENAME02, ALTERNATENAME03, ALTERNATENAME04, ADDRESSLINE1, ADDRESSLINE2, ADDRESSLINE3, BIRTHDATE, BOIFLAG, COUNTRYCODE, COUNTRYOFCITIZENSHIPINCORPO, CUSTOMERCATEGORY, CUSTOMERSUBTYPE, STATECODE, POSTALCODE, TAXNUMBER, CREATEDATE, ENABLED, IDISSUECOUNTRYCODE, IDNUMBER, IDTYPECODE, CUSTOMERNUMBER) ";
                sql += "Values(:CNO, :TITLE, :FORMATTEDNAMEFORSORTING, :ALTERNATEFORMATTEDNAME, :PRIMARYNAME01, :PRIMARYNAME02, :PRIMARYNAME03, :PRIMARYNAME04, :ALTERNATENAME01, :ALTERNATENAME02, :ALTERNATENAME03, :ALTERNATENAME04, :ADDRESSLINE1, :ADDRESSLINE2, :ADDRESSLINE3, TO_DATE(:BIRTHDATE,'DD/MM/YYYY'), :BOIFLAG, :COUNTRYCODE, :COUNTRYOFCITIZENSHIPINCORPO, :CUSTOMERCATEGORY, :CUSTOMERSUBTYPE, :STATECODE, :POSTALCODE, :TAXNUMBER, SYSDATE, :ENABLED, :IDISSUECOUNTRYCODE, :IDNUMBER, :IDTYPECODE, :CUSTOMERNUMBER) ";
                //}
                //else
                //{
                //    //sql = "Update OPICINF.KKB_TB_CBS_CUSTOMER SET CNO=:CNO, TITLE=:TITLE, FORMATTEDNAMEFORSORTING=:FORMATTEDNAMEFORSORTING, ALTERNATEFORMATTEDNAME=:ALTERNATEFORMATTEDNAME, PRIMARYNAME01=:PRIMARYNAME01, PRIMARYNAME02=:PRIMARYNAME02, PRIMARYNAME03=:PRIMARYNAME03, PRIMARYNAME04=:PRIMARYNAME04, ALTERNATENAME01=:ALTERNATENAME01, ALTERNATENAME02=:ALTERNATENAME02, ALTERNATENAME03=:ALTERNATENAME03, ALTERNATENAME04=:ALTERNATENAME04, ADDRESSLINE1=:ADDRESSLINE1, ADDRESSLINE2=:ADDRESSLINE2, ADDRESSLINE3=:ADDRESSLINE3, BIRTHDATE=TO_DATE(:BIRTHDATE,'DD/MM/YYYY'), BOIFLAG=:BOIFLAG, COUNTRYCODE=:COUNTRYCODE, COUNTRYOFCITIZENSHIPINCORPO=:COUNTRYOFCITIZENSHIPINCORPO, CUSTOMERCATEGORY=:CUSTOMERCATEGORY, CUSTOMERSUBTYPE=:CUSTOMERSUBTYPE, STATECODE=:STATECODE, POSTALCODE=:POSTALCODE, TAXNUMBER=:TAXNUMBER, LASTUPDATE=SYSDATE, ENABLED=:ENABLED WHERE TRIM(CUSTOMERNUMBER)=TRIM(:CUSTOMERNUMBER) AND TRIM(IDISSUECOUNTRYCODE)=TRIM(:IDISSUECOUNTRYCODE) AND TRIM(IDNUMBER)=TRIM(:IDNUMBER) AND TRIM(IDTYPECODE)=TRIM(:IDTYPECODE) ";sql = "Update OPICINF.KKB_TB_CBS_CUSTOMER SET CNO=:CNO, TITLE=:TITLE, FORMATTEDNAMEFORSORTING=:FORMATTEDNAMEFORSORTING, ALTERNATEFORMATTEDNAME=:ALTERNATEFORMATTEDNAME, PRIMARYNAME01=:PRIMARYNAME01, PRIMARYNAME02=:PRIMARYNAME02, PRIMARYNAME03=:PRIMARYNAME03, PRIMARYNAME04=:PRIMARYNAME04, ALTERNATENAME01=:ALTERNATENAME01, ALTERNATENAME02=:ALTERNATENAME02, ALTERNATENAME03=:ALTERNATENAME03, ALTERNATENAME04=:ALTERNATENAME04, ADDRESSLINE1=:ADDRESSLINE1, ADDRESSLINE2=:ADDRESSLINE2, ADDRESSLINE3=:ADDRESSLINE3, BIRTHDATE=TO_DATE(:BIRTHDATE,'DD/MM/YYYY'), BOIFLAG=:BOIFLAG, COUNTRYCODE=:COUNTRYCODE, COUNTRYOFCITIZENSHIPINCORPO=:COUNTRYOFCITIZENSHIPINCORPO, CUSTOMERCATEGORY=:CUSTOMERCATEGORY, CUSTOMERSUBTYPE=:CUSTOMERSUBTYPE, STATECODE=:STATECODE, POSTALCODE=:POSTALCODE, TAXNUMBER=:TAXNUMBER, LASTUPDATE=SYSDATE, ENABLED=:ENABLED WHERE TRIM(CUSTOMERNUMBER)=TRIM(:CUSTOMERNUMBER) AND TRIM(IDISSUECOUNTRYCODE)=TRIM(:IDISSUECOUNTRYCODE) AND TRIM(IDNUMBER)=TRIM(:IDNUMBER) AND TRIM(IDTYPECODE)=TRIM(:IDTYPECODE) ";
                //    sql = "Update OPICINF.KKB_TB_CBS_CUSTOMER SET CNO=:CNO, TITLE=:TITLE, FORMATTEDNAMEFORSORTING=:FORMATTEDNAMEFORSORTING, ALTERNATEFORMATTEDNAME=:ALTERNATEFORMATTEDNAME, PRIMARYNAME01=:PRIMARYNAME01, PRIMARYNAME02=:PRIMARYNAME02, PRIMARYNAME03=:PRIMARYNAME03, PRIMARYNAME04=:PRIMARYNAME04, ALTERNATENAME01=:ALTERNATENAME01, ALTERNATENAME02=:ALTERNATENAME02, ALTERNATENAME03=:ALTERNATENAME03, ALTERNATENAME04=:ALTERNATENAME04, ADDRESSLINE1=:ADDRESSLINE1, ADDRESSLINE2=:ADDRESSLINE2, ADDRESSLINE3=:ADDRESSLINE3, BIRTHDATE=TO_DATE(:BIRTHDATE,'DD/MM/YYYY'), BOIFLAG=:BOIFLAG, COUNTRYCODE=:COUNTRYCODE, COUNTRYOFCITIZENSHIPINCORPO=:COUNTRYOFCITIZENSHIPINCORPO, CUSTOMERCATEGORY=:CUSTOMERCATEGORY, CUSTOMERSUBTYPE=:CUSTOMERSUBTYPE, STATECODE=:STATECODE, POSTALCODE=:POSTALCODE, TAXNUMBER=:TAXNUMBER, LASTUPDATE=SYSDATE, ENABLED=:ENABLED WHERE TRIM(CUSTOMERNUMBER)=TRIM(:CUSTOMERNUMBER) AND TRIM(IDISSUECOUNTRYCODE)=TRIM(:IDISSUECOUNTRYCODE) AND TRIM(IDNUMBER)=TRIM(:IDNUMBER) AND TRIM(IDTYPECODE)=TRIM(:IDTYPECODE) "; sql = "Update OPICINF.KKB_TB_CBS_CUSTOMER SET CNO=:CNO, TITLE=:TITLE, FORMATTEDNAMEFORSORTING=:FORMATTEDNAMEFORSORTING, ALTERNATEFORMATTEDNAME=:ALTERNATEFORMATTEDNAME, PRIMARYNAME01=:PRIMARYNAME01, PRIMARYNAME02=:PRIMARYNAME02, PRIMARYNAME03=:PRIMARYNAME03, PRIMARYNAME04=:PRIMARYNAME04, ALTERNATENAME01=:ALTERNATENAME01, ALTERNATENAME02=:ALTERNATENAME02, ALTERNATENAME03=:ALTERNATENAME03, ALTERNATENAME04=:ALTERNATENAME04, ADDRESSLINE1=:ADDRESSLINE1, ADDRESSLINE2=:ADDRESSLINE2, ADDRESSLINE3=:ADDRESSLINE3, BIRTHDATE=TO_DATE(:BIRTHDATE,'DD/MM/YYYY'), BOIFLAG=:BOIFLAG, COUNTRYCODE=:COUNTRYCODE, COUNTRYOFCITIZENSHIPINCORPO=:COUNTRYOFCITIZENSHIPINCORPO, CUSTOMERCATEGORY=:CUSTOMERCATEGORY, CUSTOMERSUBTYPE=:CUSTOMERSUBTYPE, STATECODE=:STATECODE, POSTALCODE=:POSTALCODE, TAXNUMBER=:TAXNUMBER, LASTUPDATE=SYSDATE, ENABLED=:ENABLED , TRIM(IDISSUECOUNTRYCODE)=TRIM(:IDISSUECOUNTRYCODE) , TRIM(IDNUMBER)=TRIM(:IDNUMBER) , TRIM(IDTYPECODE)=TRIM(:IDTYPECODE) WHERE TRIM(CUSTOMERNUMBER)=TRIM(:CUSTOMERNUMBER) ";
                //}
                o.createCommand(sql, CommandType.Text);

                //if (rowcount <= 0)
                o.setInputParameter("CNO", record.CNO);
                o.setNCharInputParameter("TITLE", record.TITLE);
                o.setNCharInputParameter("FORMATTEDNAMEFORSORTING", record.FORMATTEDNAMEFORSORTING);
                o.setNCharInputParameter("ALTERNATEFORMATTEDNAME", record.ALTERNATEFORMATTEDNAME);
                o.setNCharInputParameter("PRIMARYNAME01", record.PRIMARYNAME01);
                o.setNCharInputParameter("PRIMARYNAME02", record.PRIMARYNAME02);
                o.setNCharInputParameter("PRIMARYNAME03", record.PRIMARYNAME03);
                o.setNCharInputParameter("PRIMARYNAME04", record.PRIMARYNAME04);
                o.setNCharInputParameter("ALTERNATENAME01", record.ALTERNATENAME01);
                o.setNCharInputParameter("ALTERNATENAME02", record.ALTERNATENAME02);
                o.setNCharInputParameter("ALTERNATENAME03", record.ALTERNATENAME03);
                o.setNCharInputParameter("ALTERNATENAME04", record.ALTERNATENAME04);
                o.setNCharInputParameter("ADDRESSLINE1", record.ADDRESSLINE1);
                o.setNCharInputParameter("ADDRESSLINE2", record.ADDRESSLINE2);
                o.setNCharInputParameter("ADDRESSLINE3", record.ADDRESSLINE3);
                o.setInputParameter("BIRTHDATE", record.BIRTHDATE.ToString("dd/MM/yyyy"));
                o.setInputParameter("BOIFLAG", record.BOIFLAG);
                o.setInputParameter("COUNTRYCODE", record.COUNTRYCODE);
                o.setInputParameter("COUNTRYOFCITIZENSHIPINCORPO", record.COUNTRYOFCITIZENSHIPINCORPO);
                o.setInputParameter("CUSTOMERCATEGORY", record.CUSTOMERCATEGORY);
                o.setInputParameter("CUSTOMERSUBTYPE", record.CUSTOMERSUBTYPE);
                o.setInputParameter("STATECODE", record.STATECODE);
                o.setInputParameter("POSTALCODE", record.POSTALCODE);
                o.setInputParameter("TAXNUMBER", record.TAXNUMBER);
                o.setInputParameter("ENABLED", record.ENABLED);
                o.setInputParameter("IDISSUECOUNTRYCODE", record.IDISSUECOUNTRYCODE);
                o.setInputParameter("IDNUMBER", record.IDNUMBER);
                o.setInputParameter("IDTYPECODE", record.IDTYPECODE);
                o.setInputParameter("CUSTOMERNUMBER", record.CUSTOMERNUMBER);
                row = o.executeNonQuery();

                return new { Result = "OK", Record = record };
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKTB_TB_CBS_CUSTOMER Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = "Could not insert data." };
            }
            finally
            {
                Log.Info("-----KKTB_TB_CBS_CUSTOMER Finish-----");
                o.closeConnection();
            }
        }

        public static object Update(string ConnectionString, CBSCustomer record)
        {
            Log.Info("-----KKB_TB_CBS_CUSTOMER Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                String sql = "Update OPICINF.KKB_TB_CBS_CUSTOMER Set CNO=:CNO, PRIMARYNAME01=:PRIMARYNAME01, PRIMARYNAME02=:PRIMARYNAME02, PRIMARYNAME03=:PRIMARYNAME03, PRIMARYNAME04=:PRIMARYNAME04, ENABLED=:ENABLED, LASTUPDATE=SYSDATE  Where CBSID=:CBSID ";
                o.createCommand(sql, CommandType.Text);
                o.setInputParameter("CNO", record.CNO);
                o.setNCharInputParameter("PRIMARYNAME01", record.PRIMARYNAME01);
                o.setNCharInputParameter("PRIMARYNAME02", record.PRIMARYNAME02);
                o.setNCharInputParameter("PRIMARYNAME03", record.PRIMARYNAME03);
                o.setNCharInputParameter("PRIMARYNAME04", record.PRIMARYNAME04);
                o.setInputParameter("ENABLED", record.ENABLED);
                o.setInputParameter("CBSID", record.CBSID);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Could not update data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_CBS_CUSTOMER Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKTB_TB_CBS_CUSTOMER Finish-----");
                o.closeConnection();
            }
        }

        //public static object Delete(string ConnectionString, int CBSID)
        //{

        //    Log.Info("-----KKB_TB_CBS_CUSTOMER Start delete-----");
        //    int row = 0;
        //    Oracle o = Oracle.getInstance(ConnectionString);
        //    try
        //    {
        //        o.openConnection();
        //        string sql = "DELETE FROM OPICINF.KKB_TB_CBS_CUSTOMER WHERE CBSID='" + CBSID.ToString() + "'";
        //        o.createCommand(sql, CommandType.Text);
        //        row = o.executeNonQuery();
        //        if (row > 0)
        //            return new { Result = "OK" };
        //        else
        //            return new { Result = "ERROR", Message = "Could not Delete data." };
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("-----Delete KKB_TB_CBS_CUSTOMER Error-----");
        //        Log.Error(ex.Message);
        //        return new { Result = "ERROR", Message = ex.Message };
        //    }
        //    finally
        //    {
        //        Log.Info("-----KKB_TB_BE_CBS_CUSTOMER Finish-----");
        //        o.closeConnection();
        //    }
        //}

        public static void GenerateCNO(string Connectionstring, string key)
        {
            try
            {
                string LastCNO = CBSCustomer.GetLastCno(Connectionstring);
                int StartCNO = 800000;
                if ((LastCNO != "") && (LastCNO != "0"))
                    StartCNO = Convert.ToInt32(LastCNO);

                string[] no = key.Split(',');
                Oracle o = Oracle.getInstance(Connectionstring);
                String sql = "Update OPICINF.KKB_TB_CBS_CUSTOMER Set CNO=:CNO, LASTUPDATE=SYSDATE  Where CBSID=:CBSID ";
                o.openConnection();
                for (int i = 0; i < no.Count(); i++)
                {
                    string localsql = "Select CNO From OPICINF.KKB_TB_CBS_CUSTOMER WHERE CBSID=" + no[i];
                    o.createCommand(localsql, CommandType.Text);
                    string cno = o.executeScalar().ToString();
                    if (cno.Trim().Equals(""))
                    {
                        StartCNO += 1;
                        string CNO = StartCNO.ToString();

                        o.createCommand(sql, CommandType.Text);
                        o.setInputParameter("CNO", CNO);
                        o.setInputParameter("CBSID", no[i]);
                        int row = o.executeNonQuery();
                    }
                }

            } catch(Exception ex)
            {
            
            }
        }

        public static DataTable GetDataForOpics(string ConnectionString, string key)
        {
            string DBLink = "";
            if (ConnectionString.Contains("TDS"))
                DBLink = "@TOTRODB";
            string sql = "SELECT CBSID,CUSTOMERNUMBER ,CNO,TITLE ,TITLEEN  ,FORMATTEDNAMEFORSORTING,ALTERNATEFORMATTEDNAME,PRIMARYNAME01,PRIMARYNAME02,PRIMARYNAME04, ";
            sql += "ALTERNATENAME01,ALTERNATENAME02,ALTERNATENAME03,ALTERNATENAME04 ,ADDRESSLINE1,ADDRESSLINE2,ADDRESSLINE3,BIRTHDATE,BOIFLAG, ";
            sql += "COUNTRYCODE,COUNTRYOFCITIZENSHIPINCORPO,CUSTOMERCATEGORY,CUSTOMERSUBTYPE,IDISSUECOUNTRYCODE,IDNUMBER,IDTYPECODE ,STATECODE,POSTALCODE ,";
            sql += "TAXNUMBER,CREATEDATE,LASTUPDATE,PRIMARYNAME03,ENABLED, TO_CHAR(BIRTHDATE,'YYYYMMDD') as REGISTERDATE FROM OPICINF.KKB_TB_CBS_CUSTOMER" + DBLink + "  WHERE CBSID IN (" + key + ")";
            Log.Info("-----KKB_TB_CBS_CUSTOMER Start-----");
            Log.Info("SQL Statement : " + sql);

            try
            {
                return Oracle.getDataAdapter(sql, ConnectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }

            /*
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataSet ds = o.executeReader();
                if (ds.Tables.Count > 0)
                {
                    Log.Debug("Customer Exists Data");
                    return ds.Tables[0];
                }
                else
                {
                    Log.Debug("Get CBS Customer Error");
                    return null;
                }
                //return o.executeReaderToDT("CBSCustomer");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                o.closeConnection();
            }
            */
        }

        //public static bool TransferToDataSet(string ConnectionString, string TDSConnectionString, string key, out string message)
        //{
        //    //Setting 
            

        //    Oracle o = Oracle.getInstance(TDSConnectionString);
        //    string destsql = "INSERT INTO TDS.DS_IPI(ORGCODE, DATASETDATE, INVPTYTYPEBOT, INVPTYTYPEID, TAXNO, TAXFLAG, OTHERID, UNIQUEIDTYPEBOT, UNIQUEIDTYPEID, NATBOT, NATID, CNTYRESBOT, CNTYRESID, REGISTERDATE, TITLEENID, TITLE, TITLETHID, TITLETH, FIRSTNAME, FIRSTNAMETH, MIDDLENAME, MIDDLENAMETH, LASTNAME, LASTNAMETH, SWIFTCODE, PRIMBUSINESSTYPE, BOIFLAG, JURISTICID, RELATEDINVPTYID, RELATEDUNIQUEBOT, RELATEDUNIQUEID, IPIPRELTYPEBOT, IPIPRELTYPEID, CNO, CMNE, FICODE, CTYPE, ISINDIVIAL_FLAG, INVPTYTYPEBOT1, INVPTYTYPEID1, LOG_INSERTBY, LOG_INSERTDATE) ";
        //    destsql += "VALUES(:ORGCODE, TO_DATE(:DATASETDATE,'DD/MM/YYYY'), :INVPTYTYPEBOT, :INVPTYTYPEID, :TAXNO, :TAXFLAG, :OTHERID, :UNIQUEIDTYPEBOT, :UNIQUEIDTYPEID, :NATBOT, :NATID, :CNTYRESBOT, :CNTYRESID, TO_DATE(:REGISTERDATE,'DD/MM/YYYY'), :TITLEENID, :TITLE, :TITLETHID, :TITLETH, :FIRSTNAME, :FIRSTNAMETH, :MIDDLENAME, :MIDDLENAMETH, :LASTNAME, :LASTNAMETH, :SWIFTCODE, :PRIMBUSINESSTYPE, :BOIFLAG, :JURISTICID, :RELATEDINVPTYID, :RELATEDUNIQUEBOT, :RELATEDUNIQUEID, :IPIPRELTYPEBOT, :IPIPRELTYPEID, :CNO, :CMNE, :FICODE, :CTYPE, :ISINDIVIAL_FLAG, :INVPTYTYPEBOT1, :INVPTYTYPEID1, :LOG_INSERTBY, SYSDATE) ";

        //    DataTable dt = null, CustomerCategoryData = null, CustomerSubTypeData=null, IDTypeCodeData=null, TitleEnData=null, TitleThData=null, CountryData=null;

        //    try
        //    {
        //        CustomerCategoryData = CBSMapping.GetMappingCustomerCategory(ConnectionString, CBSDESTINATION.TDS);
        //        Log.Debug("CustomerCategoryData : " + CustomerCategoryData);
        //    }
        //    catch (Exception ex) {
        //        Log.Error("CustomerCategoryData Error : " + ex.Message);
        //    }
        //    try
        //    {
        //        CustomerSubTypeData = CBSMapping.GetMappingCustomerSubType(ConnectionString, CBSDESTINATION.TDS);
        //        Log.Debug("CustomerSubTypeData : " + CustomerSubTypeData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("CustomerCategoryData Error : " + ex.Message);
        //    }
        //    try{
        //        IDTypeCodeData = CBSMapping.GetMappingIDTypeCode(ConnectionString, CBSDESTINATION.TDS);
        //        Log.Debug("IDTypeCodeData : " + IDTypeCodeData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("CustomerCategoryData Error : " + ex.Message);
        //    }
        //    try{
        //        TitleEnData = CBSMapping.GetMappingTitleEn(ConnectionString, CBSDESTINATION.TDS);
        //        Log.Debug("TitleEnData : " + TitleEnData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("CustomerCategoryData Error : " + ex.Message);
        //    }
        //    try{
        //        TitleThData = CBSMapping.GetMappingTitleTh(ConnectionString, CBSDESTINATION.TDS);
        //        Log.Debug("TitleThData : " + TitleThData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("CustomerCategoryData Error : " + ex.Message);
        //    }
        //    try {
        //        CountryData = CBSMapping.GetMappingCountry(TDSConnectionString);
        //        Log.Debug("CountryData : " + CountryData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("CustomerCategoryData Error : " + ex.Message);
        //    }

        //    try
        //    {
        //        dt = CBSCustomer.GetDataForOpics(ConnectionString, key);
        //    }
        //    catch (Exception ex)
        //    {
        //        dt = null;
        //        Log.Error("Customer Opics Profile : " + ex.Message);
        //    }


        //    try
        //    {
        //        o.openConnection(); 
                
        //        Log.Info("-----KKB_TB_CBS_CUSTOMER Start-----");
        //        Log.Info("SQL Statement : " +destsql);

        //        if (dt != null)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                Log.Debug("Start Process Insert To Dataset");
        //                string existssql = "SELECT COUNT(*) FROM TDS.DS_IPI WHERE TRIM(CNO)='" + dr["CNO"].ToString().Trim() + "' ";
        //                o.createCommand(existssql, CommandType.Text);
        //                string val = o.executeScalar().ToString();
        //                if (!val.Equals("0"))
        //                {
        //                    Log.Error("CNO: " + dr["CNO"].ToString().Trim() + " is Exists.");
        //                    message = "CNO: " + dr["CNO"].ToString().Trim() + " is Exists.";
        //                    return false;
        //                }

        //                string CustomerCategoryText = "";
        //                string CustomerCategoryID = "";
        //                try
        //                {
        //                    DataRow drCustomerCategory = CustomerCategoryData.Select("CBSCODE='" + dr["CUSTOMERCATEGORY"].ToString().Trim() + "'").FirstOrDefault();
        //                    CustomerCategoryText = drCustomerCategory != null ? drCustomerCategory["TROCODE"].ToString() : "";
        //                    CustomerCategoryID = drCustomerCategory != null ? drCustomerCategory["TROID"].ToString() : "";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("CustomerCategory : " + ex.Message);
        //                }

        //                string CustomerSubTypeText = "";
        //                string CustomerSubTypeID = "";
        //                try
        //                {
        //                    DataRow drCustomerSubType = CustomerSubTypeData.Select("CBSCODE='" + dr["CUSTOMERSUBTYPE"].ToString().Trim() + "'").FirstOrDefault();
        //                    CustomerSubTypeText = drCustomerSubType != null ? drCustomerSubType["TROCODE"].ToString() : "";
        //                    CustomerSubTypeID = drCustomerSubType != null ? drCustomerSubType["TROID"].ToString() : "";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("CustomerSubtype : " + ex.Message);
        //                }

        //                string IDTypeCodeText = "";
        //                string IDTypeCodeID = "";
        //                try
        //                {
        //                    DataRow drIDTypeCode = IDTypeCodeData.Select("CBSCODE='" + dr["IDTYPECODE"].ToString().Trim() + "'").FirstOrDefault();
        //                    IDTypeCodeText = drIDTypeCode != null ? drIDTypeCode["TROCODE"].ToString() : "";
        //                    IDTypeCodeID = drIDTypeCode != null ? drIDTypeCode["TROID"].ToString() : "";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("IDTypeCode : " + ex.Message);
        //                }

        //                string TitleEnText = "";
        //                string TitleEnID = "";
        //                try
        //                {
        //                    DataRow drTitleEn = TitleEnData.Select("CBSCODE='" + dr["TITLE"].ToString().Trim() + "'").FirstOrDefault();
        //                    TitleEnText = drTitleEn != null ? drTitleEn["TROCODE"].ToString() : "";
        //                    TitleEnID = drTitleEn != null ? drTitleEn["TROID"].ToString() : "";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("TitleEn : " + ex.Message);
        //                }

        //                string TitleThText = "";
        //                string TitleThID = "";
        //                try
        //                {
        //                    DataRow drTitleTh = TitleThData.Select("CBSCODE='" + dr["TITLE"].ToString().Trim() + "'").FirstOrDefault();
        //                    TitleThText = drTitleTh != null ? drTitleTh["TROCODE"].ToString() : "";
        //                    TitleThID = drTitleTh != null ? drTitleTh["TROID"].ToString() : "";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("TitleTh : " + ex.Message);
        //                }

        //                string NatText = "";
        //                string NatID = "";
        //                try
        //                {
        //                    DataRow drNat = CountryData.Select("FIELDNAME='" + dr["IDIssueCountryCode"].ToString().Trim() + "'").FirstOrDefault();
        //                    NatText = drNat != null ? drNat["FIELDNAME"].ToString() : "";
        //                    NatID = drNat != null ? drNat["MSBOTID"].ToString() : "";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("Nat : " + ex.Message);
        //                }

        //                string CNTYRESText = "";
        //                string CNTYRESID = "";
        //                try
        //                {
        //                    DataRow drCNTYRES = CountryData.Select("FIELDNAME='" + dr["CountryCode"].ToString().Trim() + "'").FirstOrDefault();
        //                    CNTYRESText = drCNTYRES != null ? drCNTYRES["FIELDNAME"].ToString() : "";
        //                    CNTYRESID = drCNTYRES != null ? drCNTYRES["MSBOTID"].ToString() : "";
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log.Error("CNTyRestText : " + ex.Message);
        //                }

        //                Log.Debug("Start Set Parameter : ");
        //                o.createCommand(destsql, CommandType.Text);
        //                o.setInputParameter("ORGCODE", "069");
        //                o.setInputParameter("DATASETDATE", DateTime.Today.ToString("dd/MM/yyyy"));
        //                o.setNCharInputParameter("INVPTYTYPEBOT", CustomerSubTypeText == null ? "" : CustomerSubTypeText);
        //                o.setInputParameter("INVPTYTYPEID", CustomerSubTypeID == null ? "" : CustomerSubTypeID);
        //                o.setInputParameter("TAXNO", dr["TAXNUMBER"] == null ? "" : dr["TAXNUMBER"].ToString().Trim());
        //                o.setInputParameter("TAXFLAG", dr["TAXNUMBER"] == null ? "N" : "Y");
        //                if (IDTypeCodeText.Equals("324008"))
        //                    o.setInputParameter("OTHERID", dr["IDNUMBER"] == null ? "" : dr["IDNUMBER"]); //IDNUMBER
        //                else
        //                    o.setInputParameter("OTHERID", "");
        //                o.setNCharInputParameter("UNIQUEIDTYPEBOT", IDTypeCodeText == null ? "" : IDTypeCodeText);
        //                o.setInputParameter("UNIQUEIDTYPEID", IDTypeCodeID == null ? "" : IDTypeCodeID);
        //                o.setNCharInputParameter("NATBOT", NatText == null ? "" : NatText);
        //                o.setInputParameter("NATID", NatID == null ? "" : NatID);
        //                o.setNCharInputParameter("CNTYRESBOT", CNTYRESText == null ? "" : CNTYRESText);
        //                o.setInputParameter("CNTYRESID", CNTYRESID == null ? "" : CNTYRESID);
        //                o.setInputParameter("REGISTERDATE", dr["REGISTERDATE"] == null ? "" : dr["REGISTERDATE"]);
        //                o.setInputParameter("TITLEENID", TitleEnID == null ? "" : TitleEnID);
        //                o.setNCharInputParameter("TITLE", TitleEnText == null ? "" : TitleEnText);
        //                o.setInputParameter("TITLETHID", TitleThID == null ? "" : TitleThID);
        //                o.setNCharInputParameter("TITLETH", TitleThText == null ? "" : TitleThText);
        //                if (CustomerCategoryText == "I")
        //                {
        //                    string FirstNameEN = dr["ALTERNATEFORMATTEDNAME"].ToString().Trim().Substring(0, dr["ALTERNATEFORMATTEDNAME"].ToString().Trim().IndexOf(" "));
        //                    string LastNameEN = dr["ALTERNATEFORMATTEDNAME"].ToString().Trim().Substring(dr["ALTERNATEFORMATTEDNAME"].ToString().Trim().IndexOf(" ") + 1);
        //                    string FirstNameTH = dr["FORMATTEDNAMEFORSORTING"].ToString().Trim().Substring(0, dr["FORMATTEDNAMEFORSORTING"].ToString().Trim().IndexOf(" "));
        //                    string LastNameTH = dr["FORMATTEDNAMEFORSORTING"].ToString().Trim().Substring(dr["FORMATTEDNAMEFORSORTING"].ToString().Trim().IndexOf(" ") + 1);

        //                    o.setInputParameter("FIRSTNAME", FirstNameEN == null ? "" : FirstNameEN);
        //                    o.setInputParameter("FIRSTNAMETH", FirstNameTH == null ? "" : FirstNameTH);
        //                    o.setInputParameter("MIDDLENAME", "");
        //                    o.setInputParameter("MIDDLENAMETH", "");
        //                    o.setInputParameter("LASTNAME", LastNameEN == null ? "" : LastNameEN);
        //                    o.setInputParameter("LASTNAMETH", LastNameTH == null ? "" : LastNameTH);
        //                }
        //                else
        //                {
        //                    o.setInputParameter("FIRSTNAME", dr["ALTERNATEFORMATTEDNAME"] == null ? "" : dr["ALTERNATEFORMATTEDNAME"].ToString().Trim());
        //                    o.setInputParameter("FIRSTNAMETH", dr["FORMATTEDNAMEFORSORTING"] == null ? "" : dr["FORMATTEDNAMEFORSORTING"].ToString().Trim());
        //                    o.setInputParameter("MIDDLENAME", "");
        //                    o.setInputParameter("MIDDLENAMETH", "");
        //                    o.setInputParameter("LASTNAME", "");
        //                    o.setInputParameter("LASTNAMETH", "");
        //                }
        //                if (IDTypeCodeText.Equals("324008"))
        //                    o.setInputParameter("SWIFTCODE", dr["IDNUMBER"] == null ? "" : dr["IDNUMBER"].ToString().Trim()); //IDNUMBER
        //                else
        //                    o.setInputParameter("SWIFTCODE", "");
        //                o.setInputParameter("PRIMBUSINESSTYPE", "");
        //                o.setInputParameter("BOIFLAG", dr["BOIFlag"] == null ? "" : dr["BOIFlag"].ToString().Trim());
        //                if (IDTypeCodeText.Equals("324004"))
        //                    o.setInputParameter("JURISTICID", dr["IDNUMBER"] == null ? "" : dr["IDNUMBER"].ToString().Trim()); //IDNUMBER
        //                else
        //                    o.setInputParameter("JURISTICID", "");
        //                o.setInputParameter("RELATEDINVPTYID", "");
        //                o.setInputParameter("RELATEDUNIQUEBOT", "");
        //                o.setInputParameter("RELATEDUNIQUEID", "");
        //                o.setInputParameter("IPIPRELTYPEBOT", "");
        //                o.setInputParameter("IPIPRELTYPEID", "");
        //                o.setInputParameter("CNO", dr["CNO"] == null ? "" : dr["CNO"].ToString().Trim());
        //                o.setInputParameter("CMNE", dr["PRIMARYNAME01"] == null ? "" : dr["PRIMARYNAME01"].ToString().Trim());
        //                if (IDTypeCodeText.Equals("324007"))
        //                    o.setInputParameter("FICODE", dr["IDNUMBER"] == null ? "" : dr["IDNUMBER"].ToString().Trim()); //IDNUMBER
        //                else
        //                    o.setInputParameter("FICODE", "");
        //                o.setInputParameter("CTYPE", CustomerCategoryText == null ? "" : CustomerCategoryText);
        //                o.setInputParameter("ISINDIVIAL_FLAG", CustomerCategoryText == "I" ? "I" : "");
        //                o.setInputParameter("INVPTYTYPEBOT1", "");
        //                o.setInputParameter("INVPTYTYPEID1", "");
        //                o.setInputParameter("LOG_INSERTBY", "");
        //                Log.Debug("Set Parameter Completed");
        //                int row = o.executeNonQuery();

        //                if (row <= 0)
        //                {
        //                    message = "ไม่สามารถเพิ่มข้อมูลลง DS_IPI ได้ กรุณาตรวจสอบข้อมูล";
        //                    return false;
        //                }
        //            }
        //            message = "บันทึกข้อมูลลง TDS เรียบร้อยแล้ว";
        //            return true;
        //        }
        //        else {
        //            message = "เกิดข้อผิดพลาด ไม่สามารถติดต่อฐานข้อมูล DataSet ได้";
        //            Log.Error("Error DataTable");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        message = "ไม่สามารถเพิ่มข้อมูลลง DS_IPI ได้ " + ex.Message + ":" + ex.InnerException.Message;
        //        return false;
        //    }
        //    finally
        //    {
        //        o.closeConnection();
        //    }
        //}

        public static bool TransferToDataSet(string TDSConnectionString, string key, out string message)
        {
            Oracle o = Oracle.getInstance(TDSConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("TDS.KKB_TDS_SP_FROMOPICS_CBS", CommandType.StoredProcedure);
                o.setInputParameter("p_cbsid", key);
                Log.Debug("Connection : " + TDSConnectionString);
                Log.Debug("DataSet Parameter : " + key);
                int rows = o.executeNonQuery("inserted_rows");
                Log.Debug("DataSet Result : " + rows);
                if (rows < 0)
                {
                    message = "ไม่สามารถเพิ่มข้อมูลลง DS_IPI ได้ เนื่องจากบางรายการมีใน TDS แล้ว";
                    return false;
                }
                else if (rows > 0)
                {
                    message = "บันทึกข้อมูลลง TDS เรียบร้อยแล้ว";
                    return true;
                }
                else
                {
                    message = "ไม่สามารถเพิ่มข้อมูลลง DS_IPI ได้ กรุณาตรวจสอบข้อมูล(" + rows.ToString() + ")";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                message = "ไม่สามารถเพิ่มข้อมูลลง DS_IPI ได้ " + ex.Message + ":" + ex.InnerException.Message;
                return false;
            }
            finally {
                o.closeConnection();
            }
        }

        public static string GetLastCno(string ConnectionString)
        {
            String sql = "Select MAX(CNO) From OPICINF.KKB_TB_CBS_CUSTOMER Where TO_CHAR(CNO) LIKE '8%' ";
            Log.Info("-----KKB_TB_CBS_CUSTOMER Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                return o.executeScalar().ToString();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return "0";
            }
            finally {
                o.closeConnection();
            }
        }
    }
}
