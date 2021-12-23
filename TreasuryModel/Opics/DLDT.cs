using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryModel.Opics
{
    public class DLDT
    {
        public String DEALNO { get; set; }
        public String PRODUCT { get; set; }
        public String PRODTYPE { get; set; }
        public String TRAD { get; set; }
        public String VDATE { get; set; }
        public String MDATE { get; set; }
        public String CNO { get; set; }
        public String CCY { get; set; }
        public String CCYAMT { get; set; }
        public String RATECODE { get; set; }
        public String BASIS { get; set; }
        public String INTRATE { get; set; }
        public String SPREAD_8 { get; set; }
        public String NETAMT { get; set; }
        public String RATEREVDATE { get; set; }
        public String BROK { get; set; }
        public String BROKCCY { get; set; }
        public String BROKAMT { get; set; }
        public String BROKRATE_8 { get; set; }
        public String PORT { get; set; }
        public String COST { get; set; }
        public String DEALDATE { get; set; }
        public String TENOR { get; set; }
        public String LINKDEALNO { get; set; }
        public String DEALTEXT { get; set; }
        public String LSTMNTDTE { get; set; }
        public String SIIND { get; set; }
        public String VERIND { get; set; }
        public String SUPPAYIND { get; set; }
        public String SUPRECIND { get; set; }
        public String SUPCONFIND { get; set; } //31

        public const int TOTAL_COLUMN = 123;
    }
}
