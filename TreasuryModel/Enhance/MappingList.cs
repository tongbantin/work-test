using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml.Linq;
using System.IO;

using log4net;

/* MappingList Class
 * -----------------------------------------------
 * Load XML Config File into C# Class
 * by The Config File must node order :
 * <KiatnakinBank>
 *  <Treasury>
 *      <Trans Sheet="0"> <!--Vertical=1, Horizontal=2-->
 *         <Column>A1:J1</Column>
 *         <Data>A2:J549</Data>
 *       </Trans>
 *  </Treasury>
 * </KiatnakinBank>
 */


namespace KKB.Treasury.TreasuryModel.Model
{
    public class MappingList
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(MappingList));
        private KiatnakinBank kkb = new KiatnakinBank();
        public MappingList(string XmlFile)
        {
            try
            {
                XmlSerializer reader = new XmlSerializer(kkb.GetType());

                Console.WriteLine("Read XML Config File : " + XmlFile);
                Log.Debug("Read XML Config File : " + XmlFile);

                StreamReader file = new StreamReader(@"" + XmlFile);
                kkb = (KiatnakinBank)reader.Deserialize(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!XML Load File : " + ex.Message);
                Log.Error("XML Load File : " + ex.Message);
                kkb = null;
            }
        }

        /* TreasuryMappingList Method
         * ------------------------------------------
         * return Node:Treasury Config
         */
        public List<KiatnakinBankTreasurySource> SourceMappingList()
        {
            try
            {
                //Start Mapping           
                List<KiatnakinBankTreasurySource> s = new List<KiatnakinBankTreasurySource>();
                KiatnakinBankTreasury treasury = (KiatnakinBankTreasury)kkb.Items[0];
                s = treasury.Source.ToList<KiatnakinBankTreasurySource>();
                return s;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!XML Config File Treasury: " + ex.Message);
                Log.Error("XML Config File : " + ex.Message);
                return null;
            }
        }
    }
}
