using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryModel.Data
{
    public class SecAsset
    {
        public int SECASSETID { get; set; }
        public string SECID { get; set; }
        public decimal QTY { get; set; }
        public decimal DEPOSITQTY { get; set; }
        public DateTime MDATE { get; set; }
        public int ASSETGROUPID { get; set; }
        public string SCRIPLESS { get; set; }
        public string SECTYPE { get; set; }
        public string CREATEBY { get; set; }
        public DateTime CREATEDATE { get; set; }
        public string UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string ENABLED { get; set; }
    }
}
