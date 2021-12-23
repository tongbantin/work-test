using System;
namespace TreasuryModel.Data.Collateral
{
    public abstract class BaseModel
    {
        public string CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
    }
}
