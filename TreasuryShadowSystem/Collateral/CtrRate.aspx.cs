using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TreasuryShadowSystem.Collateral
{
    using TreasuryModel.Data.Collateral;
    using TreasuryShadowSystem.Collateral.Filters;

    public partial class CtrRate : AjaxBasePage<RATE_INT>, IController<RateFilter, RATE_INT>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            try
            {
                string json = string.Empty;
                int Id = 0;
                RateFilter filter = new RateFilter();
                RATE_INT model = new RATE_INT();
                string Operator = Request.QueryString["Operator"];
                if (Request.Form.Count > 0) json = Request.Form[0];
                if (Request.QueryString["Id"] != null) Id = Convert.ToInt32(Request.QueryString["Id"]);

                switch (Operator.ToUpper())
                {
                    case "GETALL":
                        filter = JsonConvert.DeserializeObject<RateFilter>(json);
                        GetAll(filter);
                        break;
                    case "ADD":
                        model = JsonConvert.DeserializeObject<RATE_INT>(json);
                        Add(model);
                        break;
                    case "UPDATE":
                        model = JsonConvert.DeserializeObject<RATE_INT>(json);
                        Update(model);
                        break;
                    case "GETBYID":
                        GetById(Id);
                        break;
                    case "REMOVE":
                        Remove(Id);
                        break;

                }
            }
            catch (Exception ex)
            {
                NotFound(ex);
            }

        }

        #region IController<RateFilter,RATE_INT> Members

        public void GetAll(RateFilter filter)
        {
            throw new NotImplementedException();
        }

        public void GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public void Add(RATE_INT data)
        {
            throw new NotImplementedException();
        }

        public void Update(RATE_INT data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
