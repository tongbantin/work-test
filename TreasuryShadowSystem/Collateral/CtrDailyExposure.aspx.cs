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
    

    public partial class CtrDailyExposure : AjaxBasePage<CTR_EXPOSURE>, IController<DailyExposureFilter, CTR_EXPOSURE>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            try
            {
                string json = string.Empty;
                int Id = 0;
                DailyExposureFilter filter = new DailyExposureFilter();
                CTR_EXPOSURE model = new CTR_EXPOSURE();
                string Operator = Request.QueryString["Operator"];
                if (Request.Form.Count > 0) json = Request.Form[0];
                if (Request.QueryString["Id"] != null) Id = Convert.ToInt32(Request.QueryString["Id"]);

                switch (Operator.ToUpper())
                {
                    case "GETALL":
                        filter = JsonConvert.DeserializeObject<DailyExposureFilter>(json);
                        GetAll(filter);
                        break;
                    case "ADD":
                        model = JsonConvert.DeserializeObject<CTR_EXPOSURE>(json);
                        Add(model);
                        break;
                    case "UPDATE":
                        model = JsonConvert.DeserializeObject<CTR_EXPOSURE>(json);
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

        #region IController<DailyExposureFilter,CTR_EXPOSURE> Members

        public void GetAll(DailyExposureFilter filter)
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

        public void Add(CTR_EXPOSURE data)
        {
            throw new NotImplementedException();
        }

        public void Update(CTR_EXPOSURE data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
