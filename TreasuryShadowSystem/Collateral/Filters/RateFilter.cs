using System;

namespace TreasuryShadowSystem.Collateral.Filters
{
    public class RateFilter
    {
        public string RATE_GRP_NAME { get; set; }
        public string RATE_DATE_BEGIN { get; set; }
        public string RATE_DATE_END { get; set; }
        public decimal? RATE { get; set; }
        public string CURRENCY { get; set; }

    }
}
