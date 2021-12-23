using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;
using log4net;

namespace TreasuryShadowSystem.Model
{
    public class MappingList
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(MappingList));
        private KiatnakinBank kkb = new KiatnakinBank();
        public MappingList(string XMLPath)
        {
            try
            {
                XmlSerializer reader = new XmlSerializer(kkb.GetType());

                Console.WriteLine("Read XML Config File : " + XMLPath);
                Log.Debug("Read XML Config File : " + XMLPath);

                StreamReader file = new StreamReader(@"" + XMLPath + "/config.xml");
                kkb = (KiatnakinBank)reader.Deserialize(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!XML Load File : " + ex.Message);
                Log.Error("XML Load File : " + ex.Message);
                kkb = null;
            }
        }

        public KiatnakinBankForeignExchange ForeignExchange()
        {
            try
            {
                return (KiatnakinBankForeignExchange)kkb.Items[0];                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /* TreasuryMappingList Method
         * ------------------------------------------
         * return Node:Treasury Config
         */
        public List<KiatnakinBankForeignExchangeCurrency> ForeignExchangeMappingList()
        {
            try
            {
                //Start Mapping           
                List<KiatnakinBankForeignExchangeCurrency> LModel = new List<KiatnakinBankForeignExchangeCurrency>();
                KiatnakinBankForeignExchange treasury = (KiatnakinBankForeignExchange)kkb.Items[0];
                LModel = treasury.Currency.ToList<KiatnakinBankForeignExchangeCurrency>();
                return LModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!XML Config File Treasury: " + ex.Message);
                Log.Error("XML Config File : " + ex.Message);
                return null;
            }
        }
        /* SheetDayMappingList method
         * ------------------------------------------------
         * Return Node:Summary Config
         */
        public List<KiatnakinBankSheetDayFrequencySheet> SheetDayMappingList(string Frequency)
        {
            try
            {
                //Start Mapping           
                List<KiatnakinBankSheetDayFrequencySheet> LModel = new List<KiatnakinBankSheetDayFrequencySheet>();
                KiatnakinBankSheetDay summary = (KiatnakinBankSheetDay)kkb.Items[1];
                for (int i = 0; i < summary.Frequency.Length; i++)
                {
                    if (summary.Frequency[i].Value.Equals(Frequency))
                    {
                        LModel = summary.Frequency[i].Sheet.ToList<KiatnakinBankSheetDayFrequencySheet>();
                        break;
                    } 
                }
                
                return LModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!XML Config File Summary: " + ex.Message);
                Log.Error("XML Config File : " + ex.Message);
                return null;
            }
        }
        /* Sheet Property method
         * ------------------------------------------------
         * Return Node:Summary Config
         
        public string SheetFrequency()
        {
            try
            {
                //Start Mapping           
                KiatnakinBankSheetDay summary = (KiatnakinBankSheetDay)kkb.Items[1];
                return summary.Frequency;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!XML Config File Summary: " + ex.Message);
                Log.Error("XML Config File : " + ex.Message);
                return null;
            }
        }*/
    }
}
