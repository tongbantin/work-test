using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryModel.Api
{
    public class LoanIRDataDetail
    {
        public string period { get; set; }
        public string bank_type_name_th { get; set; }
        public string bank_type_name_eng { get; set; }
        public string bank_name_th { get; set; }
        public string bank_name_eng { get; set; }
        public string mor { get; set; }
        public string mlr { get; set; }
        public string mrr { get; set; }
        public string ceiling_rate { get; set; }
        public string default_rate { get; set; }
        public string creditcard_min { get; set; }
        public string creditcard_max { get; set; }
    }

    public class LoanIRData
    {
        public List<LoanIRDataDetail> data_detail { get; set; }
    }

    public class LoanIRResult
    {
        public string api { get; set; }
        public string timestamp { get; set; }
        public LoanIRData data { get; set; }
    }

    public class LoanIRRootObject
    {
        public LoanIRResult result { get; set; }
    }
}
