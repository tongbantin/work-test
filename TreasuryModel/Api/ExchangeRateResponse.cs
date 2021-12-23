using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryModel.Api
{
    public class FXRateReportSourceOfData
    {
        public string source_of_data_eng { get; set; }
        public string source_of_data_th { get; set; }
    }

    public class FXRateReportRemark
    {
        public string report_remark_eng { get; set; }
        public string report_remark_th { get; set; }
    }

    public class FXRateDataHeader
    {
        public string report_name_eng { get; set; }
        public string report_name_th { get; set; }
        public string report_uoq_name_eng { get; set; }
        public string report_uoq_name_th { get; set; }
        public List<FXRateReportSourceOfData> report_source_of_data { get; set; }
        public List<FXRateReportRemark> report_remark { get; set; }
        public string last_updated { get; set; }
    }

    public class FXRateDataDetail
    {
        public string period { get; set; }
        public string currency_id { get; set; }
        public string currency_name_th { get; set; }
        public string currency_name_eng { get; set; }
        public string buying_sight { get; set; }
        public string buying_transfer { get; set; }
        public string selling { get; set; }
        public string mid_rate { get; set; }
    }

    public class FXRateData
    {
        public FXRateDataHeader data_header { get; set; }
        public List<FXRateDataDetail> data_detail { get; set; }
    }

    public class FXRateResult
    {
        public string timestamp { get; set; }
        public string api { get; set; }
        public FXRateData data { get; set; }
    }

    public class FXRateRootObject
    {
        public FXRateResult result { get; set; }
    }
}
