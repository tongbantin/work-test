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
    public class CBSAddress
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(CBSAddress));

        public int ADDRESSID { get; set; }
        public string CUSTOMERNUMBER { get; set; }
        public long SEQ { get; set; }
        public string ADDRESSFORMAT { get; set; }
        public string ADDRESSTYPE { get; set; }
        public string ADDRESSLINE1 { get; set; }
        public string ADDRESSLINE2 { get; set; }
        public string ADDRESSLINE3 { get; set; }
        public string ADDRESSLINE4 { get; set; }
        public string ADDRESSLINE5 { get; set; }
        public string ADDRESSLINE6 { get; set; }
        public string ADDRESSLINE7 { get; set; }
        public string INSUFFICIENTADDRESS { get; set; }
        public string REMARKS { get; set; }
        public string POSTALCODE { get; set; }
        public string STATECODE { get; set; }
        public string COUNTRYCODE { get; set; }
        public string DEFAULTADDRESS { get; set; }
        public string ACCOUNTLINKED { get; set; }
        public string CONTACTLINKINDICATOR { get; set; }
        public string CONSOLIDATESTATEMENT { get; set; }
        public string HOUSENUMBER { get; set; }
        public string MOOLABEL { get; set; }
        public string MOO { get; set; }
        public string FLOORNUMBERLABEL { get; set; }
        public string FLOORNUMBER { get; set; }
        public string ROOMNUMBERLABEL { get; set; }
        public string ROOMNUMBER { get; set; }
        public string BUILDING { get; set; }
        public string SOILABEL { get; set; }
        public string SOI { get; set; }
        public string ROADLABEL { get; set; }
        public string ROAD { get; set; }
        public string SUBDISTRICT { get; set; }
        public string SUBDISTRICTTH { get; set; }
        public string DISTRICT { get; set; }
        public string DISTRICTTH { get; set; }
        public string PROVINCE { get; set; }
        public string PROVINCETH { get; set; }
        public string POSTCODE { get; set; }
        public string COUNTRY { get; set; }

        public string ADDRESSLINE { get; set; }
        public string FORMATTEDNAMEFORSORTING { get; set; }

        public static List<CBSAddress> GetList(string ConnectionString, string CIFID)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select ADDRESS.ADDRESSID, ADDRESS.CUSTOMERNUMBER, CUSTOMER.FORMATTEDNAMEFORSORTING, ADDRESS.SEQ, ADDRESS.ADDRESSLINE1, ADDRESS.ADDRESSLINE2, ADDRESS.ADDRESSLINE3, ADDRESS.ADDRESSLINE4, ADDRESS.ADDRESSLINE5, ADDRESS.ADDRESSLINE6, ADDRESS.ADDRESSLINE7, ADDRESS.SUBDISTRICT, SUB_DISTRICT.SUB_DISTRICT_TH, ADDRESS.DISTRICT, DISTRICT.DISTRICT_TH, ADDRESS.PROVINCE, PROVINCE.PROVINCE_TH ";
                sql += ",TRIM(ADDRESS.HOUSENUMBER) ";
                sql += "|| CASE WHEN ADDRESS.MOO IS NULL THEN n'' ELSE n' ' || TRIM(ADDRESS.MOOLABEL || ADDRESS.MOO) END ";
                sql += "|| CASE WHEN ADDRESS.FLOORNUMBER IS NULL THEN n'' ELSE n' ' || TRIM(ADDRESS.FLOORNUMBERLABEL || ADDRESS.FLOORNUMBER) END ";
                sql += "|| CASE WHEN ADDRESS.ROOMNUMBER IS NULL THEN n'' ELSE n' ' || TRIM(ADDRESS.ROOMNUMBER) END ";
                sql += "|| CASE WHEN ADDRESS.BUILDING IS NULL THEN n'' ELSE n' ' || TRIM(ADDRESS.BUILDING) END ";
                sql += "|| CASE WHEN ADDRESS.SOI IS NULL THEN n'' ELSE n' ' || TRIM(ADDRESS.SOILABEL || ADDRESS.SOI) END ";
                sql += "|| CASE WHEN ADDRESS.ROAD IS NULL THEN n'' ELSE n' ' || TRIM(ADDRESS.ROADLABEL) || TRIM(ADDRESS.ROAD) END as ADDRESSLINE ";
                sql += "From OPICINF.KKB_TB_CBS_ADDRESS ADDRESS LEFT JOIN OPICINF.KKB_TB_CBS_CUSTOMER CUSTOMER ON ADDRESS.CUSTOMERNUMBER=CUSTOMER.CUSTOMERNUMBER  ";
                sql += "LEFT OUTER JOIN OPICINF.KKB_TB_CBS_PROVINCE PROVINCE ON ADDRESS.PROVINCE=PROVINCE.PROVINCE_CODE ";
                sql += "LEFT OUTER JOIN OPICINF.KKB_TB_CBS_DISTRICT DISTRICT ON ADDRESS.DISTRICT=DISTRICT.DISTRICT_CODE ";
                sql += "LEFT OUTER JOIN OPICINF.KKB_TB_CBS_SUB_DISTRICT SUB_DISTRICT ON ADDRESS.SUBDISTRICT=SUB_DISTRICT.SUB_DISTRICT_CODE ";
                sql += "Where 1=1 ";
                if (!CIFID.Equals(""))
                    sql += "AND ADDRESS.CUSTOMERNUMBER='" + CIFID + "'";

                Log.Info("-----CBSAddress-----");
                Log.Info("SQL Statement : " + sql);

                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("CBSAddress");

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");


                List<CBSAddress> param = new List<CBSAddress>();
                int rownum = dt.Rows.Count > 100 ? 100 : dt.Rows.Count;
                for (int i = 0; i < rownum; i++)
                {
                    CBSAddress p = new CBSAddress();
                    p.ADDRESSID = int.Parse(dt.Rows[i]["ADDRESSID"].ToString().Trim());
                    p.CUSTOMERNUMBER = dt.Rows[i]["CUSTOMERNUMBER"].ToString().Trim();
                    p.FORMATTEDNAMEFORSORTING = dt.Rows[i]["FORMATTEDNAMEFORSORTING"].ToString().Trim();
                    p.SEQ = int.Parse(dt.Rows[i]["SEQ"].ToString().Trim());
                    p.ADDRESSLINE1 = dt.Rows[i]["ADDRESSLINE1"].ToString().Trim();
                    p.ADDRESSLINE2 = dt.Rows[i]["ADDRESSLINE2"].ToString().Trim();
                    p.ADDRESSLINE3 = dt.Rows[i]["ADDRESSLINE3"].ToString().Trim();
                    p.ADDRESSLINE4 = dt.Rows[i]["ADDRESSLINE4"].ToString().Trim();
                    p.ADDRESSLINE5 = dt.Rows[i]["ADDRESSLINE5"].ToString().Trim();
                    p.ADDRESSLINE6 = dt.Rows[i]["ADDRESSLINE6"].ToString().Trim();
                    p.ADDRESSLINE7 = dt.Rows[i]["ADDRESSLINE7"].ToString().Trim();
                    p.ADDRESSLINE = dt.Rows[i]["ADDRESSLINE"].ToString().Trim();
                    p.SUBDISTRICT = dt.Rows[i]["SUBDISTRICT"].ToString().Trim();
                    p.DISTRICT = dt.Rows[i]["DISTRICT"].ToString().Trim();
                    p.PROVINCE = dt.Rows[i]["PROVINCE"].ToString().Trim();
                    p.SUBDISTRICTTH = dt.Rows[i]["SUB_DISTRICT_TH"].ToString().Trim();
                    p.DISTRICTTH = dt.Rows[i]["DISTRICT_TH"].ToString().Trim();
                    p.PROVINCETH = dt.Rows[i]["PROVINCE_TH"].ToString().Trim();
                    
                    param.Add(p);
                }

                //Return result to jTable
                return param;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object GetAddress(string ConnectionString, int AddressID)
        {
            String sql = "Select Address.*, Customer.FORMATTEDNAMEFORSORTING, SUB_DISTRICT.SUB_DISTRICT_TH, DISTRICT.DISTRICT_TH, PROVINCE.PROVINCE_TH ";
            sql += "From OPICINF.KKB_TB_CBS_ADDRESS ADDRESS LEFT JOIN OPICINF.KKB_TB_CBS_CUSTOMER CUSTOMER ON ADDRESS.CUSTOMERNUMBER=CUSTOMER.CUSTOMERNUMBER ";
            sql += "LEFT OUTER JOIN OPICINF.KKB_TB_CBS_PROVINCE PROVINCE ON ADDRESS.PROVINCE=PROVINCE.PROVINCE_CODE ";
            sql += "LEFT OUTER JOIN OPICINF.KKB_TB_CBS_DISTRICT DISTRICT ON ADDRESS.DISTRICT=DISTRICT.DISTRICT_CODE ";
            sql += "LEFT OUTER JOIN OPICINF.KKB_TB_CBS_SUB_DISTRICT SUB_DISTRICT ON ADDRESS.SUBDISTRICT=SUB_DISTRICT.SUB_DISTRICT_CODE ";
            sql += "Where ADDRESS.ADDRESSID='" + AddressID + "' ";
            Log.Info("-----KKB_TB_CBS_ADDRESS Start-----");
            Log.Info("SQL Statement : " + sql);

            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                DataTable dt = o.executeReaderToDT("CBSAddress");

                List<CBSAddress> param = new List<CBSAddress>();
                CBSAddress p = new CBSAddress();
                p.ADDRESSID = int.Parse(dt.Rows[0]["ADDRESSID"].ToString().Trim());
                p.CUSTOMERNUMBER = dt.Rows[0]["CUSTOMERNUMBER"].ToString().Trim();
                p.SEQ = int.Parse(dt.Rows[0]["SEQ"].ToString().Trim());
                p.ADDRESSFORMAT = dt.Rows[0]["ADDRESSFORMAT"].ToString().Trim();
                p.ADDRESSTYPE = dt.Rows[0]["ADDRESSTYPE"].ToString().Trim();
                p.ADDRESSLINE1 = dt.Rows[0]["ADDRESSLINE1"].ToString().Trim();
                p.ADDRESSLINE2 = dt.Rows[0]["ADDRESSLINE2"].ToString().Trim();
                p.ADDRESSLINE3 = dt.Rows[0]["ADDRESSLINE3"].ToString().Trim();
                p.ADDRESSLINE4 = dt.Rows[0]["ADDRESSLINE4"].ToString().Trim();
                p.ADDRESSLINE5 = dt.Rows[0]["ADDRESSLINE5"].ToString().Trim();
                p.ADDRESSLINE6 = dt.Rows[0]["ADDRESSLINE6"].ToString().Trim();
                p.ADDRESSLINE7 = dt.Rows[0]["ADDRESSLINE7"].ToString().Trim();
                p.INSUFFICIENTADDRESS = dt.Rows[0]["INSUFFICIENTADDRESS"].ToString().Trim();
                p.REMARKS = dt.Rows[0]["REMARKS"].ToString().Trim();
                p.FORMATTEDNAMEFORSORTING = dt.Rows[0]["FORMATTEDNAMEFORSORTING"].ToString().Trim();
                p.POSTALCODE = dt.Rows[0]["POSTALCODE"].ToString().Trim();
                p.STATECODE = dt.Rows[0]["STATECODE"].ToString().Trim();
                p.COUNTRYCODE = dt.Rows[0]["COUNTRYCODE"].ToString().Trim();
                p.DEFAULTADDRESS = dt.Rows[0]["DEFAULTADDRESS"].ToString().Trim();
                p.ACCOUNTLINKED = dt.Rows[0]["ACCOUNTLINKED"].ToString().Trim();
                p.CONTACTLINKINDICATOR = dt.Rows[0]["CONTACTLINKINDICATOR"].ToString().Trim();
                p.CONSOLIDATESTATEMENT = dt.Rows[0]["CONSOLIDATESTATEMENT"].ToString().Trim();
                p.HOUSENUMBER = dt.Rows[0]["HOUSENUMBER"].ToString().Trim();
                p.MOOLABEL = dt.Rows[0]["MOOLABEL"].ToString().Trim();
                p.MOO = dt.Rows[0]["MOO"].ToString().Trim();
                p.FLOORNUMBERLABEL = dt.Rows[0]["FLOORNUMBERLABEL"].ToString().Trim();
                p.FLOORNUMBER = dt.Rows[0]["FLOORNUMBER"].ToString().Trim();
                p.ROOMNUMBERLABEL = dt.Rows[0]["ROOMNUMBERLABEL"].ToString().Trim();
                p.ROOMNUMBER = dt.Rows[0]["ROOMNUMBER"].ToString().Trim();
                p.BUILDING = dt.Rows[0]["BUILDING"].ToString().Trim();
                p.SOILABEL = dt.Rows[0]["SOILABEL"].ToString().Trim();
                p.SOI = dt.Rows[0]["SOI"].ToString().Trim();
                p.ROADLABEL = dt.Rows[0]["ROADLABEL"].ToString().Trim();
                p.ROAD = dt.Rows[0]["ROAD"].ToString().Trim();
                p.SUBDISTRICT = dt.Rows[0]["SUBDISTRICT"].ToString().Trim();
                p.DISTRICT = dt.Rows[0]["DISTRICT"].ToString().Trim();
                p.PROVINCE = dt.Rows[0]["PROVINCE"].ToString().Trim();
                p.POSTCODE = dt.Rows[0]["POSTCODE"].ToString().Trim();
                p.SUBDISTRICTTH = dt.Rows[0]["SUB_DISTRICT_TH"].ToString().Trim();
                p.DISTRICTTH = dt.Rows[0]["DISTRICT_TH"].ToString().Trim();
                p.PROVINCETH = dt.Rows[0]["PROVINCE_TH"].ToString().Trim();
                p.COUNTRY = dt.Rows[0]["COUNTRY"].ToString().Trim();
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
        public static object DeleteAddress(string ConnectionString, int AddressID)
        {

            Log.Info("-----KKB_TB_CBS_ADDRESS Start delete-----");
            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                string sql = "DELETE FROM OPICINF.KKB_TB_CBS_ADDRESS WHERE ADDRESSID='" + AddressID.ToString() + "'";
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Could not Delete data." };
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKB_TB_CBS_ADDRESS Error-----");
                Log.Error(ex.Message);
                return new { Result = "ERROR", Message = ex.Message };
            }
            finally
            {
                Log.Info("-----KKB_TB_CBS_ADDRESS Finish-----");
                o.closeConnection();
            }
        }
        protected static CBSCustomerService.InquiryCustomerAddressResponseHeaderAddress[] GetAddressByCIFNo(string ServiceHosting, string CIFID)
        {
            try
            {
                Log.Debug("===Start Set Customer Address Header====");
                CBSCustomerService.InquiryCustomerAddressHeader header = new CBSCustomerService.InquiryCustomerAddressHeader();
                header.ReferenceNo = "TRO" + DateTime.Now.ToString("yyyyMMddHHmmSS");
                header.ServiceName = "InquiryCustomerAddress";
                header.SystemCode = "TRO";
                header.TransactionDateTime = DateTime.Now;
                header.ChannelID = "TRO";

                Log.Debug("===Start Set Customer Address Detail====");
                CBSCustomerService.InquiryCustomerAddressInquiryCustomerAddressRequest request = new CBSCustomerService.InquiryCustomerAddressInquiryCustomerAddressRequest();
                request.CustomerNumber = CIFID;
                request.InquiryMode = "F";

                Log.Debug("====Start Set Customer Address Paging===");
                CBSCustomerService.InquiryCustomerAddressPaging paging = new CBSCustomerService.InquiryCustomerAddressPaging();
                paging.PageNumber = 1;
                paging.PageSize = 50;
                paging.SortDirection = "ASC";


                Log.Debug("===Start Set Customer Address====");
                CBSCustomerService.InquiryCustomerAddress address = new CBSCustomerService.InquiryCustomerAddress();
                address.Header = header;
                address.InquiryCustomerAddressRequest = request;
                address.Paging = paging;


                Log.Debug("===Start Customer Address Request====");
                CBSCustomerService.CBSCustomerService c = new CBSCustomerService.CBSCustomerService();
                c.Url = ServiceHosting + "/ws/CBSCustomerService";
                CBSCustomerService.InquiryCustomerAddressResponse response = c.InquiryCustomerAddress(address);
                CBSCustomerService.InquiryCustomerAddressResponseHeaderAddress[] obj = response.Header.AddressList;
                Log.Debug("URL : " + c.Url);
                Log.Debug("==Request Completed====");
                return obj;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public static CBSAddress[] TransformCBSAddress(string ServiceHosting, string CIFID)
        {
            try
            {
                CBSCustomerService.InquiryCustomerAddressResponseHeaderAddress[] obj = CBSAddress.GetAddressByCIFNo(ServiceHosting, CIFID);
                CBSAddress[] address = new CBSAddress[obj.Count()];
                int i = 0;
                foreach (var c in obj)
                {
                    CBSAddress addr = new CBSAddress();
                    addr.CUSTOMERNUMBER = CIFID;
                    addr.SEQ = c.Seq;
                    //addr.ACCOUNTLINKED = c.AccountLinked;
                    addr.ADDRESSFORMAT = c.AddressFormat;
                    addr.ADDRESSLINE1 = c.AddressLine1;
                    addr.ADDRESSLINE2 = c.AddressLine2;
                    addr.ADDRESSLINE3 = c.AddressLine3;
                    addr.ADDRESSLINE4 = c.AddressLine4;
                    addr.ADDRESSLINE5 = c.AddressLine5;
                    addr.ADDRESSLINE6 = c.AddressLine6;
                    addr.ADDRESSLINE7 = c.AddressLine7;
                    addr.ADDRESSTYPE = c.AddressType;
                    //addr.CONSOLIDATESTATEMENT = c.ConsolidateStatement;
                    //addr.CONTACTLINKINDICATOR = c.ContactLinkIndicator;
                    addr.COUNTRYCODE = c.CountryCode;
                    //addr.DEFAULTADDRESS = c.DefaultAddress;
                    //addr.INSUFFICIENTADDRESS = c.InsufficientAddress;
                    addr.POSTALCODE = c.PostalCode;
                    addr.REMARKS = c.Remarks;
                    addr.STATECODE = c.StateCode;

                    CBSCustomerService.InquiryCustomerAddressResponseHeaderAddressLocalAddressInfo local = c.LocalAddressInfo;
                    if (local!=null)
                    {
                        addr.BUILDING = local.LocalAddress.Building;
                        addr.COUNTRY = local.LocalAddress.CountryCode;
                        addr.DISTRICT = local.LocalAddress.DistrictCode;
                        addr.FLOORNUMBER = local.LocalAddress.FloorNumber;
                        addr.FLOORNUMBERLABEL = local.LocalAddress.FloorNumberLabel;
                        addr.HOUSENUMBER = local.LocalAddress.HouseNumber;
                        addr.MOO = local.LocalAddress.Moo;
                        addr.MOOLABEL = local.LocalAddress.MooLabel;
                        addr.POSTCODE = local.LocalAddress.PostalCode;
                        addr.PROVINCE = local.LocalAddress.ProvinceCode;
                        addr.ROAD = local.LocalAddress.Road;
                        addr.ROADLABEL = local.LocalAddress.RoadLabel;
                        addr.ROOMNUMBER = local.LocalAddress.RoomNumber;
                        addr.ROOMNUMBERLABEL = local.LocalAddress.RoomNumberLabel;
                        addr.SOI = local.LocalAddress.Soi;
                        addr.SOILABEL = local.LocalAddress.SoiLabel;
                        addr.SUBDISTRICT = local.LocalAddress.SubDistrictCode;
                    }
                    address[i] = addr;
                    i++;
                }
                return address;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool Save(string ConnectionString, CBSAddress[] address)
        {
            Log.Info("-----KKB_TB_CBS_ADDRESS Start update-----");

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();

                foreach (CBSAddress record in address)
                {
                    string sql = "SELECT COUNT(*) FROM OPICINF.KKB_TB_CBS_ADDRESS WHERE CUSTOMERNUMBER='" + record.CUSTOMERNUMBER + "' AND SEQ='" + record.SEQ + "' ";
                    o.createCommand(sql, CommandType.Text);
                    int rowcount = int.Parse(o.executeScalar().ToString());

                    if (rowcount <= 0)
                    {
                        sql = "Insert into OPICINF.KKB_TB_CBS_ADDRESS(ADDRESSFORMAT, ADDRESSTYPE, ADDRESSLINE1, ADDRESSLINE2, ADDRESSLINE3, ADDRESSLINE4, ADDRESSLINE5, ADDRESSLINE6, ADDRESSLINE7, INSUFFICIENTADDRESS, REMARKS, POSTALCODE, STATECODE, COUNTRYCODE, DEFAULTADDRESS, ACCOUNTLINKED, CONTACTLINKINDICATOR, CONSOLIDATESTATEMENT, HOUSENUMBER, MOOLABEL, MOO, FLOORNUMBERLABEL, FLOORNUMBER, ROOMNUMBERLABEL, ROOMNUMBER, BUILDING, SOILABEL, SOI, ROADLABEL, ROAD, SUBDISTRICT, DISTRICT,PROVINCE, POSTCODE, COUNTRY, CREATEDATE, CUSTOMERNUMBER, SEQ) ";
                        sql += "Values(:ADDRESSFORMAT, :ADDRESSTYPE, :ADDRESSLINE1, :ADDRESSLINE2, :ADDRESSLINE3, :ADDRESSLINE4, :ADDRESSLINE5, :ADDRESSLINE6, :ADDRESSLINE7, :INSUFFICIENTADDRESS, :REMARKS, :POSTALCODE, :STATECODE, :COUNTRYCODE, :DEFAULTADDRESS, :ACCOUNTLINKED, :CONTACTLINKINDICATOR, :CONSOLIDATESTATEMENT, :HOUSENUMBER, :MOOLABEL, :MOO, :FLOORNUMBERLABEL, :FLOORNUMBER, :ROOMNUMBERLABEL, :ROOMNUMBER, :BUILDING, :SOILABEL, :SOI, :ROADLABEL, :ROAD, :SUBDISTRICT, :DISTRICT, :PROVINCE, :POSTCODE, :COUNTRY, SYSDATE, :CUSTOMERNUMBER, :SEQ)";
                    }
                    else
                    {
                        sql = "Update OPICINF.KKB_TB_CBS_ADDRESS SET ADDRESSFORMAT=:ADDRESSFORMAT, ADDRESSTYPE=:ADDRESSTYPE, ADDRESSLINE1=:ADDRESSLINE1, ADDRESSLINE2=:ADDRESSLINE2, ADDRESSLINE3=:ADDRESSLINE3, ADDRESSLINE4=:ADDRESSLINE4, ADDRESSLINE5=:ADDRESSLINE5, ADDRESSLINE6=:ADDRESSLINE6, ADDRESSLINE7=:ADDRESSLINE7, INSUFFICIENTADDRESS=:INSUFFICIENTADDRESS, REMARKS=:REMARKS, POSTALCODE=:POSTALCODE, STATECODE=:STATECODE, COUNTRYCODE=:COUNTRYCODE, DEFAULTADDRESS=:DEFAULTADDRESS, ACCOUNTLINKED=:ACCOUNTLINKED, CONTACTLINKINDICATOR=:CONTACTLINKINDICATOR, CONSOLIDATESTATEMENT=:CONSOLIDATESTATEMENT, HOUSENUMBER=:HOUSENUMBER, MOOLABEL=:MOOLABEL, MOO=:MOO, FLOORNUMBERLABEL=:FLOORNUMBERLABEL, FLOORNUMBER=:FLOORNUMBER, ROOMNUMBERLABEL=:ROOMNUMBERLABEL, ROOMNUMBER=:ROOMNUMBER, BUILDING=:BUILDING, SOILABEL=:SOILABEL, SOI=:SOI, ROADLABEL=:ROADLABEL, ROAD=:ROAD, SUBDISTRICT=:SUBDISTRICT, DISTRICT=:DISTRICT, PROVINCE=:PROVINCE, POSTCODE=:POSTCODE, COUNTRY=:COUNTRY, LASTUPDATE=SYSDATE WHERE CUSTOMERNUMBER=:CUSTOMERNUMBER AND SEQ=:SEQ";
                    }
                    o.createCommand(sql, CommandType.Text);
                    o.setInputParameter("ADDRESSFORMAT", record.ADDRESSFORMAT);
                    o.setInputParameter("ADDRESSTYPE", record.ADDRESSTYPE);
                    o.setNCharInputParameter("ADDRESSLINE1", record.ADDRESSLINE1);
                    o.setNCharInputParameter("ADDRESSLINE2", record.ADDRESSLINE2);
                    o.setNCharInputParameter("ADDRESSLINE3", record.ADDRESSLINE3);
                    o.setNCharInputParameter("ADDRESSLINE4", record.ADDRESSLINE4);
                    o.setNCharInputParameter("ADDRESSLINE5", record.ADDRESSLINE5);
                    o.setNCharInputParameter("ADDRESSLINE6", record.ADDRESSLINE6);
                    o.setNCharInputParameter("ADDRESSLINE7", record.ADDRESSLINE7);
                    o.setInputParameter("INSUFFICIENTADDRESS", record.INSUFFICIENTADDRESS);
                    o.setNCharInputParameter("REMARKS", record.REMARKS);
                    o.setInputParameter("POSTALCODE", record.POSTALCODE);
                    o.setInputParameter("STATECODE", record.STATECODE);
                    o.setInputParameter("COUNTRYCODE", record.COUNTRYCODE);
                    o.setInputParameter("DEFAULTADDRESS", record.DEFAULTADDRESS);
                    o.setInputParameter("ACCOUNTLINKED", record.ACCOUNTLINKED);
                    o.setInputParameter("CONTACTLINKINDICATOR", record.CONTACTLINKINDICATOR);
                    o.setInputParameter("CONSOLIDATESTATEMENT", record.CONSOLIDATESTATEMENT);
                    o.setInputParameter("HOUSENUMBER", record.HOUSENUMBER);
                    o.setNCharInputParameter("MOOLABEL", record.MOOLABEL);
                    o.setInputParameter("MOO", record.MOO);
                    o.setNCharInputParameter("FLOORNUMBERLABEL", record.FLOORNUMBERLABEL);
                    o.setNCharInputParameter("FLOORNUMBER", record.FLOORNUMBER);
                    o.setNCharInputParameter("ROOMNUMBERLABEL", record.ROOMNUMBERLABEL);
                    o.setNCharInputParameter("ROOMNUMBER", record.ROOMNUMBER);
                    o.setNCharInputParameter("BUILDING", record.BUILDING);
                    o.setNCharInputParameter("SOILABEL", record.SOILABEL);
                    o.setNCharInputParameter("SOI", record.SOI);
                    o.setNCharInputParameter("ROADLABEL", record.ROADLABEL);
                    o.setNCharInputParameter("ROAD", record.ROAD);
                    o.setInputParameter("SUBDISTRICT", record.SUBDISTRICT);
                    o.setInputParameter("DISTRICT", record.DISTRICT);
                    o.setInputParameter("PROVINCE", record.PROVINCE);
                    o.setInputParameter("POSTCODE", record.POSTCODE);
                    o.setInputParameter("COUNTRY", record.COUNTRY); 
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
                Log.Error("-----Insert KKB_TB_CBS_ADDRESS Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKB_TB_CBS_ADDRESS Finish-----");
                o.closeConnection();
            }
        }
    }
}
