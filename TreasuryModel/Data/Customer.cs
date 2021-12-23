using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using KKB.Treasury.TreasuryCommon.Common;
using TreasuryModel.Master;
using log4net;

namespace TreasuryModel.Data
{
    public class Customer
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Customer));

        //OPICINF
        public int CustomerID { get; set; }
        public string CustomerType { get; set; }
        public string Code { get; set; }
        public string NameTh { get; set; }
        public string NameEn { get; set; }
        public string SegregateAcct { get; set; }
        public int CustodianID { get; set; }
        public string AssetCompany { get; set; }
        public string TaxID { get; set; }
        public string GIIN { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Tel { get; set; }
        public string BuildingName { get; set; }
        public string FloorNo { get; set; }
        public string RoomNo { get; set; }
        public string VillageName { get; set; }
        public string AddressNo { get; set; }
        public string MooNo { get; set; }
        public string Soi { get; set; }
        public string StreetName { get; set; }
        public string Tumbon { get; set; }
        public string Amphur { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string IncVat { get; set; }
        public string TargetAcct { get; set; }
        public string TargetBank { get; set; }
        public string Issuer { get; set; }
        public string Seller { get; set; }
        public string IssuerType { get; set; }
        public string IssuerRemark { get; set; }
        public string AnnualRegist { get; set; }
        public string AnnualRegistText { get; set; }
        public string CreateBY { get; set; }
        public string CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }

        public static DataTable getCustomerByID(string ConnectionString, String id)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select * From OPICINF.KKB_TB_CUSTOMER WHERE CUSTOMERID='" + id + "'";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }

        public static DataTable getCustomerByCondition(string ConnectionString, String Name)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select KKB_TB_CUSTOMER.* , (Select Name From OPICINF.KKB_TB_CUSTODIAN WHERE CustodianID=KKB_TB_CUSTOMER.CustodianID) CustodianName From OPICINF.KKB_TB_CUSTOMER WHERE (UPPER(TRIM(NAMEEN)) like UPPER(:NameEN)) OR (TRIM(NAMETH) like :NameTH) ";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setNCharInputParameter("NameEN", "" + Name.ToUpper().Trim() + "%");
                o.setNCharInputParameter("NameTH", "%" + Name.ToUpper().Trim() + "%");
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }

        public static DataTable getCustomerForServices(string ConnectionString, String Name)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select CustomerID, NameEn, AssetCompany, IncVat, TargetBank, TargetAcct From OPICINF.KKB_TB_CUSTOMER WHERE ((UPPER(TRIM(NAMEEN)) like UPPER(:NameEN)) OR (TRIM(NAMETH) like :NameTH) OR (UPPER(TRIM(CODE)) like :Code)) AND ROWNUM<=50";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setNCharInputParameter("NameEN", "" + Name.ToUpper().Trim() + "%");
                o.setNCharInputParameter("NameTH", "%" + Name.ToUpper().Trim() + "%");
                o.setVarCharInputParameter("Code", "" + Name.ToUpper().Trim() + "%");
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }

        //Add search customer with equal
        public static DataTable getCustomerForName(string ConnectionString, String Name)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select CustomerID, NameEn, AssetCompany, IncVat, TargetBank, TargetAcct From OPICINF.KKB_TB_CUSTOMER WHERE ((UPPER(TRIM(NAMEEN)) = UPPER(:NameEN)) OR (TRIM(NAMETH) = :NameTH) OR (UPPER(TRIM(CODE)) = :Code))";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setNCharInputParameter("NameEN", Name.ToUpper().Trim());
                o.setNCharInputParameter("NameTH", Name.ToUpper().Trim());
                o.setVarCharInputParameter("Code", Name.ToUpper().Trim());
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }


        public static DataTable getIssuerForServices(string ConnectionString, String Name, String CustType)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                string sql = "Select CustomerID, NameEn, TargetBank, TargetAcct From OPICINF.KKB_TB_CUSTOMER WHERE ((UPPER(TRIM(NAMEEN)) like UPPER(:NameEN)) OR (TRIM(NAMETH) like :NameTH) OR (UPPER(TRIM(CODE)) like :Code)) AND ISSUER='1' AND ROWNUM<=100";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setNCharInputParameter("NameEN", "" + Name.ToUpper().Trim() + "%");
                o.setNCharInputParameter("NameTH", "%" + Name.ToUpper().Trim() + "%");
                o.setVarCharInputParameter("Code", "" + Name.ToUpper().Trim() + "%");
                s = o.executeReader();
                return s.Tables[0];
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }

        public static DataTable getSellerForServices(string connectionString, String Name, String CustType)
        {
            DataSet s = null;
            Oracle o = Oracle.getInstance(connectionString);
            try
            {
                string sql = "Select CustomerID, NameEn, TargetBank, TargetAcct From OPICINF.KKB_TB_CUSTOMER WHERE ((UPPER(TRIM(NAMEEN)) like UPPER(:NameEN)) OR (TRIM(NAMETH) like :NameTH) OR (UPPER(TRIM(CODE)) like :Code)) AND SELLER='1' AND ROWNUM<=100";
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setNCharInputParameter("NameEN", "" + Name.ToUpper().Trim() + "%");
                o.setNCharInputParameter("NameTH", "%" + Name.ToUpper().Trim() + "%");
                o.setVarCharInputParameter("Code", "" + Name.ToUpper().Trim() + "%");
                s = o.executeReader();
                return s.Tables[0];
            }
            catch (Exception ex)
            {
                Log.Error("-----Model_MS_Customer Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_MS_Customer Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Create(string ConnectionString)
        {
            String sql = "Insert Into OPICINF.KKB_TB_CUSTOMER(CustomerType, Code, NameTh, NameEn, SegregateAcct, CustodianID, AssetCompany, TaxID, GIIN, Address, Contact, Tel, BuildingName, FloorNo, RoomNo, VillageName, AddressNo, MooNo, Soi, StreetName, Tumbon, Amphur, Province, PostalCode, IncVat, TargetBank, TargetAcct, Issuer, IssuerType, IssuerRemark, AnnualRegist, AnnualRegistText, SELLER) ";
            sql += " Values( :CustomerType, :Code, :NameTh, :NameEn, :SegregateAcct, :CustodianID, :AssetCompany, :TaxID, :GIIN, :Address, :Contact, :Tel, :BuildingName, :FloorNo, :RoomNo, :VillageName, :AddressNo, :MooNo, :Soi, :StreetName, :Tumbon, :Amphur, :Province, :PostalCode, :IncVat, :TargetBank, :TargetAcct, :Issuer, :IssuerType, :IssuerRemark, :AnnualRegist, :AnnualRegistText, :SELLER)";
            
            Log.Info("-----KKB_TB_CUSTOMER Start insert-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setCharInputParameter("CustomerType", this.CustomerType);
                o.setNCharInputParameter("Code", this.Code);
                o.setNCharInputParameter("NameTh", this.NameTh);
                o.setNCharInputParameter("NameEn", this.NameEn);
                o.setNCharInputParameter("SegregateAcct", this.SegregateAcct);
                o.setIntegerInputParameter("CustodianID", this.CustodianID);
                o.setNCharInputParameter("AssetCompany", this.AssetCompany);
                o.setNCharInputParameter("TaxID", this.TaxID);
                o.setNCharInputParameter("GIIN", this.GIIN);
                o.setNCharInputParameter("Address", this.Address);
                o.setNCharInputParameter("Contact", this.Contact);
                o.setNCharInputParameter("Tel", this.Tel);
                o.setNCharInputParameter("BuildingName", this.BuildingName);
                o.setNCharInputParameter("FloorNo", this.FloorNo);
                o.setNCharInputParameter("RoomNo", this.RoomNo);
                o.setNCharInputParameter("VillageName", this.VillageName);
                o.setNCharInputParameter("AddressNo", this.AddressNo);
                o.setNCharInputParameter("MooNo", this.MooNo);
                o.setNCharInputParameter("Soi", this.Soi);
                o.setNCharInputParameter("StreetName", this.StreetName);
                o.setNCharInputParameter("Tumbon", this.Tumbon);
                o.setNCharInputParameter("Amphur", this.Amphur);
                o.setNCharInputParameter("Province", this.Province);
                o.setNCharInputParameter("PostalCode", this.PostalCode);
                o.setCharInputParameter("IncVat", this.IncVat);
                o.setVarCharInputParameter("TargetBank", this.TargetBank);
                o.setVarCharInputParameter("TargetAcct", this.TargetAcct); 
                o.setCharInputParameter("Issuer", this.Issuer);
                o.setVarCharInputParameter("IssuerType", this.IssuerType);
                o.setNCharInputParameter("IssuerRemark", this.IssuerRemark);
                o.setVarCharInputParameter("AnnualRegist", this.AnnualRegist);
                o.setNCharInputParameter("AnnualRegistText", this.AnnualRegistText);
                o.setNCharInputParameter("SELLER", this.Seller);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Insert KKTB_TB_CUSTOMER Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKTB_TB_CUSTOMER Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Update(string ConnectionString)
        {
            String sql = "Update OPICINF.KKB_TB_CUSTOMER Set CustomerType=:CustomerType, Code=:Code, NameTh=:NameTh, NameEn=:NameEn, SegregateAcct=:SegregateAcct, CustodianID=:CustodianID, AssetCompany=:AssetCompany, TaxID=:TaxID, GIIN=:GIIN, Address=:Address, Contact=:Contact, Tel=:Tel, BuildingName=:BuildingName, FloorNo=:FloorNo, RoomNo=:RoomNo, VillageName=:VillageName, AddressNo=:AddressNo, MooNo=:MooNo, Soi=:Soi, StreetName=:StreetName, Tumbon=:Tumbon, Amphur=:Amphur, Province=:Province, PostalCode=:PostalCode, IncVat=:IncVat, TargetBank=:TargetBank, TargetAcct=:TargetAcct, Issuer=:Issuer, IssuerType=:IssuerType, IssuerRemark=:IssuerRemark, AnnualRegist=:AnnualRegist, AnnualRegistText=:AnnualRegistText, SELLER = :SELLER Where CUSTOMERID=:CustomerID";

            Log.Info("-----KKTB_TB_CUSTOMER Start Update-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                o.setCharInputParameter("CustomerType", this.CustomerType);
                o.setNCharInputParameter("Code", this.Code);
                o.setNCharInputParameter("NameTh", this.NameTh);
                o.setNCharInputParameter("NameEn", this.NameEn);
                o.setNCharInputParameter("SegregateAcct", this.SegregateAcct);
                o.setIntegerInputParameter("CustodianID", this.CustodianID);
                o.setNCharInputParameter("AssetCompany", this.AssetCompany);
                o.setNCharInputParameter("TaxID", this.TaxID);
                o.setNCharInputParameter("GIIN", this.GIIN);
                o.setNCharInputParameter("Address", this.Address);
                o.setNCharInputParameter("Contact", this.Contact);
                o.setNCharInputParameter("Tel", this.Tel);
                o.setNCharInputParameter("BuildingName", this.BuildingName);
                o.setNCharInputParameter("FloorNo", this.FloorNo);
                o.setNCharInputParameter("RoomNo", this.RoomNo);
                o.setNCharInputParameter("VillageName", this.VillageName);
                o.setNCharInputParameter("AddressNo", this.AddressNo);
                o.setNCharInputParameter("MooNo", this.MooNo);
                o.setNCharInputParameter("Soi", this.Soi);
                o.setNCharInputParameter("StreetName", this.StreetName);
                o.setNCharInputParameter("Tumbon", this.Tumbon);
                o.setNCharInputParameter("Amphur", this.Amphur);
                o.setNCharInputParameter("Province", this.Province);
                o.setNCharInputParameter("PostalCode", this.PostalCode);
                o.setCharInputParameter("IncVat", this.IncVat);
                o.setVarCharInputParameter("TargetBank", this.TargetBank);
                o.setVarCharInputParameter("TargetAcct", this.TargetAcct);
                o.setCharInputParameter("Issuer", this.Issuer);
                o.setVarCharInputParameter("IssuerType", this.IssuerType);
                o.setNCharInputParameter("IssuerRemark", this.IssuerRemark);
                o.setVarCharInputParameter("AnnualRegist", this.AnnualRegist);
                o.setNCharInputParameter("AnnualRegistText", this.AnnualRegistText);
                o.setNCharInputParameter("SELLER", this.Seller);
                o.setIntegerInputParameter("CustomerID", this.CustomerID);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Log.Error("-----Update KKTB_TB_CUSTOMER Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKTB_TB_CUSTOMER Finish-----");
                o.closeConnection();
            }
        }

        public Boolean Delete(string ConnectionString)
        {
            String sql = "Delete From OPICINF.KKB_TB_CUSTOMER Where CustomerID='" + this.CustomerID + "'";
            Log.Info("-----KKTB_TB_CUSTOMER Start Delete-----");
            Log.Info("SQL Statement : " + sql);

            int row = 0;
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand(sql, CommandType.Text);
                row = o.executeNonQuery();
                if (row > 0)
                    return true;
                else
                    return false;
                //Log.Info(s.GetXml());
            }
            catch (Exception ex)
            {
                Log.Error("-----Delete KKTB_TB_CUSTOMER Error-----");
                Log.Error(ex.Message);
                return false;
            }
            finally
            {
                Log.Info("-----KKTB_TB_CUSTOMER Finish-----");
                o.closeConnection();
            }
        }
    }
}
