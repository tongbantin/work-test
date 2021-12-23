
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;
using System.Collections;

namespace TreasuryModel.Data
{
    public class FXNostroAvg
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(FXNostroAvg));

        public int NostAvgID { get; set; }
        public string VDATE { get; set; }
        public decimal BUYAMT { get; set; }
        public decimal SELLAMT { get; set; }
        public string STATEDATE { get; set; }
        public decimal STATERATE { get; set; }
        public string OPICSDATE { get; set; }
        public decimal OPICSRATE { get; set; }
        public decimal STATEMOVE { get; set; }
        public decimal OPICSMOVE { get; set; }
        public decimal ADJDIFSPOTRATE { get; set; }
        public decimal ADJPLTHB { get; set; }
        public decimal BALANCECCY { get; set; }
        public decimal BALANCETHB { get; set; }
        public decimal EXCHRATEAVG { get; set; }

        public string CCY { get; set; }

        public ArrayList save(string ConnectionString, List<FXNostroAvg> nost)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            ArrayList error = new ArrayList();
            try
            {
                o.openConnection();
                o.beginTransaction();
                
                string sql = "Delete From OPICINF.KKB_TB_NOSTROAVG Where VDATE Between TO_DATE('" + Convert.ToDateTime(nost[0].VDATE).ToString("dd/MM/yyyy") + "','DD/MM/YYYY') AND TO_DATE('" + Convert.ToDateTime(nost[nost.Count - 1].VDATE).ToString("dd/MM/yyyy") + "','DD/MM/YYYY') AND ACCTCODEID IN (SELECT ACCTCODEID FROM OPICINF.KKB_TB_ACCTCODE WHERE CCYGRP='" + nost[0].CCY + "')";
                o.createCommand(sql, CommandType.Text);
                o.executeNonQuery();    

                int i = 0; 
                foreach (FXNostroAvg f in nost)
                {
                    try
                    {

                        sql = "Insert Into OPICINF.KKB_TB_NOSTROAVG(VDATE, BUYAMT, SELLAMT, STATEDATE, STATERATE, OPICSDATE, OPICSRATE, STATEMOVE, OPICSMOVE, ADJDIFSPOTRATE, ADJPLTHB, BALANCECCY, BALANCETHB, EXCHRATEAVG, ACCTCODEID, CREATEBY, CREATEDATE) ";
                        sql += " Select TO_DATE(:VDATE,'DD/MM/YYYY'), :BUYAMT, :SELLAMT, TO_DATE(:STATEDATE,'DD/MM/YYYY'), :STATERATE, TO_DATE(:OPICSDATE,'DD/MM/YYYY'), :OPICSRATE, :STATEMOVE, :OPICSMOVE, :ADJDIFSPOTRATE, :ADJPLTHB, :BALANCECCY, :BALANCETHB, :EXCHRATEAVG, ACCTCODEID, 'P', SYSDATE FROM OPICINF.KKB_TB_ACCTCODE WHERE CCYGRP=:CCY";
                        Log.Info("Execute : " + sql);
                        o.createCommand(sql, CommandType.Text);
                        o.setCharInputParameter("VDATE", Convert.ToDateTime(f.VDATE).ToString("dd/MM/yyyy"));
                        o.setCharInputParameter("BUYAMT", f.BUYAMT);
                        o.setCharInputParameter("SELLAMT", f.SELLAMT);
                        o.setCharInputParameter("STATEDATE", Convert.ToDateTime(f.STATEDATE).ToString("dd/MM/yyyy"));
                        o.setCharInputParameter("STATERATE", f.STATERATE);
                        o.setCharInputParameter("OPICSDATE", Convert.ToDateTime(f.OPICSDATE).ToString("dd/MM/yyyy"));
                        o.setCharInputParameter("OPICSRATE", f.OPICSRATE);
                        o.setCharInputParameter("STATEMOVE", f.STATEMOVE);
                        o.setCharInputParameter("OPICSMOVE", f.OPICSMOVE);
                        o.setCharInputParameter("ADJDIFSPOTRATE", f.ADJDIFSPOTRATE);
                        o.setCharInputParameter("ADJPLTHB", f.ADJPLTHB);
                        o.setCharInputParameter("BALANCECCY", f.BALANCECCY);
                        o.setCharInputParameter("BALANCETHB", f.BALANCETHB);
                        o.setCharInputParameter("EXCHRATEAVG", f.EXCHRATEAVG);
                        o.setCharInputParameter("CCY", f.CCY); 
                        o.executeNonQuery();                         
                    }
                    catch (Exception ex)
                    {
                        error.Add(f.NostAvgID);
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
                Log.Error("----Start Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                o.closeConnection();
            }
            return error;
        }
    }
}
