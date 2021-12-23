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
using System.Web.Services;
using System.Collections.Generic;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TreasuryShadowSystem.Collateral
{
    using TreasuryModel.Data.Collateral;
    using TreasuryShadowSystem.Collateral.Filters;

    public partial class CtrBalance : AjaxBasePage<BALANCE_MOVEMENT>, IController<BalanceFilter, BALANCE_MOVEMENT>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            try
            {
                string json = string.Empty;
                int Id = 0;
                BalanceFilter filter = new BalanceFilter();
                BALANCE_MOVEMENT model = new BALANCE_MOVEMENT();
                string Operator = Request.QueryString["Operator"];
                if (Request.Form.Count > 0) json = Request.Form[0];
                if (Request.QueryString["Id"] != null) Id = Convert.ToInt32(Request.QueryString["Id"]);

                switch (Operator.ToUpper())
                {
                    case "GETALL":
                        filter = JsonConvert.DeserializeObject<BalanceFilter>(json);
                        GetAll(filter);
                        break;
                    case "ADD":
                        model = JsonConvert.DeserializeObject<BALANCE_MOVEMENT>(json);
                        Add(model);
                        break;
                    case "UPDATE":
                        model = JsonConvert.DeserializeObject<BALANCE_MOVEMENT>(json);
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



        #region IController<BalanceFilter,BALANCE_MOVEMENT> Members

        public void GetAll(BalanceFilter filter)
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

        public void Add(BALANCE_MOVEMENT data)
        {
            throw new NotImplementedException();
        }

        public void Update(BALANCE_MOVEMENT data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
