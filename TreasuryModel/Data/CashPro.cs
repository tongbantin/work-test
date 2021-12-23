using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using KKB.Treasury.TreasuryCommon.Common;
using System.Data;

namespace TreasuryModel.Data
{
    public class CashPro
    {
        public DateTime ASOF { get; set; }
        public string CCY { get; set; }
        public string BANK_ID_TYPE { get; set; }
        public string BANK_ID { get; set; }
        public string ACCOUNT { get; set; }
        public string DATA_TYPE { get; set; }
        public string BAI_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal AMOUNT { get; set; }
        public DateTime VALUE_DATE { get; set; }
        public string CUST_REFERENCE { get; set; }
        public string IMMEDIATE_AVAILABILITY { get; set; }
        public string DAY1_FLOAT { get; set;}
        public string DAY2_FLOAT { get; set;}
        public string BANK_REFERENCE { get;set;}
        public string OF_ITEMS { get; set; }
        public string TEXT_DESCRIPTION { get; set;}

        public ArrayList save(string ConnectionString, List<CashPro> cpro)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            ArrayList error = new ArrayList();
            try
            {
                o.openConnection();
                o.beginTransaction();
                // Disabled Old Field

                int i = 0;
                foreach (CashPro d in cpro)
                {
                    try
                    {
                        string sql = "Insert Into OPICINF.KKB_TB_CASHPRO(ASOF, CCY, BANK_ID_TYPE, BANK_ID, ACCOUNT, DATA_TYPE, BAI_CODE, DESCRIPTION, AMOUNT, VALUE_DATE, CUST_REFERENCE, IMMEDIATE_AVAILABILITY, DAY1_FLOAT, DAY2_FLOAT, BANK_REFERENCE, OF_ITEMS, TEXT_DESCRIPTION) ";
                        sql += " Values( :ASOF, :CCY, :BANK_ID_TYPE, :BANK_ID, :ACCOUNT, :DATA_TYPE, :BAI_CODE, :DESCRIPTION, :AMOUNT, :VALUE_DATE, :CUST_REFERENCE, :IMMEDIATE_AVAILABILITY, :DAY1_FLOAT, :DAY2_FLOAT, :BANK_REFERENCE, :OF_ITEMS, :TEXT_DESCRIPTION )";
                        o.createCommand(sql, CommandType.Text);

                        o.setDateInputParameter("ASOF", d.ASOF);
                        o.setCharInputParameter("CCY", d.CCY.Trim());
                        o.setCharInputParameter("BANK_ID_TYPE", d.BANK_ID_TYPE.Trim());
                        o.setCharInputParameter("BANK_ID", d.BANK_ID.Trim());
                        o.setCharInputParameter("ACCOUNT", d.ACCOUNT.Trim());
                        o.setCharInputParameter("DATA_TYPE", d.DATA_TYPE.Trim());
                        o.setCharInputParameter("BAI_CODE", d.BAI_CODE.Trim());
                        o.setCharInputParameter("DESCRIPTION", d.DESCRIPTION.Trim());
                        o.setDecimalInputParameter("AMOUNT", d.AMOUNT);
                        o.setDateInputParameter("VALUE_DATE", d.VALUE_DATE);
                        o.setCharInputParameter("CUST_REFERENCE", d.CUST_REFERENCE.Trim());
                        o.setCharInputParameter("IMMEDIATE_AVAILABILITY", d.IMMEDIATE_AVAILABILITY.Trim());
                        o.setCharInputParameter("DAY1_FLOAT", d.DAY1_FLOAT.Trim());
                        o.setCharInputParameter("DAY2_FLOAT", d.DAY2_FLOAT.Trim());
                        o.setCharInputParameter("BANK_REFERENCE", d.BANK_REFERENCE.Trim());
                        o.setCharInputParameter("OF_ITEMS", d.OF_ITEMS.Trim());
                        o.setCharInputParameter("TEXT_DESCRIPTION", d.TEXT_DESCRIPTION.Trim()); 
                        o.executeNonQuery();

                    }
                    catch (Exception ex)
                    {
                        //error.Add(d.Records);
                        i++;
                    }
                }

                if (error.Count <= 0)
                    o.commitTransaction();
                else
                    o.rollbackTransaction();
            }
            catch (Exception ex)
            {
                //Log.Error("----Start Error-----");
                //Log.Error(ex.Message);
            }
            finally
            {
                o.closeConnection();
            }
            return error;
        }

    }
}
