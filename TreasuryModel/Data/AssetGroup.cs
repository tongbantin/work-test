using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryModel.Data
{
    public class AssetGroup
    {
        public int ASSETGROUPID { get; set; }
        public string NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string SUBREPORTFIX { get; set; }
        public string PARENT { get; set; }
        public string RANGEYEAR { get; set; }
        public string SCRIPLESS { get; set; }
        public int TLEVEL { get; set; }
        public int SUBGROUP { get; set; }
        public string CREATEBY { get; set; }
        public DateTime CREATEDATE { get; set; }
        public string UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string ENABLED { get; set; }
    }
}
