using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TreasuryModel.Master
{
    public class Model_MS_CALENDAR
    {
        public  static DataTable GetMonths()
        {
            try
            {
                string[] month = {"January","February","March","April","May","June","July","August","September","October","November","December"};
                DataTable dt = new DataTable();
                dt.Columns.Add("MonthID");
                dt.Columns.Add("MonthName");

                for(int i=0; i<month.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    int m = i + 1;
                    dr["MonthID"] = m<10? "0" + m.ToString() : m.ToString();
                    dr["MonthName"] = month[i];
                    dt.Rows.Add(dr);
                }
                return dt;
            } catch(Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetYears()
        {
            try
            {
                int year = 2010;
                DataTable dt = new DataTable();
                dt.Columns.Add("YearName");

                for (int i = 1; i <= 20; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["YearName"] = (year + i).ToString();
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
