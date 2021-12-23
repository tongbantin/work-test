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

    public partial class CtrCashFlow : AjaxBasePage<CASH_FLOW>, IController<CashFlowFilter, CASH_FLOW>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            try
            {
                string json = string.Empty;
                int Id = 0;
                CashFlowFilter filter = new CashFlowFilter();
                CASH_FLOW model = new CASH_FLOW();
                string Operator = Request.QueryString["Operator"];
                if (Request.Form.Count > 0) json = Request.Form[0];
                if (Request.QueryString["Id"] != null) Id = Convert.ToInt32(Request.QueryString["Id"]);

                switch (Operator.ToUpper())
                {
                    case "GETALL":
                        filter = JsonConvert.DeserializeObject<CashFlowFilter>(json);
                        GetAll(filter);
                        break;
                    case "ADD":
                        model = JsonConvert.DeserializeObject<CASH_FLOW>(json);
                        Add(model);
                        break;
                    case "UPDATE":
                        model = JsonConvert.DeserializeObject<CASH_FLOW>(json);
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
        
        public void GetAll(CashFlowFilter data)
        {
            IList<CASH_FLOW> result = new List<CASH_FLOW>();
            for (int i = 0; i < 5; i++)
                result.Add(CASH_FLOW.CreateMockup(i));

            if (data.CTR_NAME.Length > 0)
                result = result.Where(w => w.CTR_NAME == data.CTR_NAME).ToList();

            Oks(result);
        }

        public void GetById(int Id)
        {
            CASH_FLOW cashFlow = CASH_FLOW.CreateMockup(Id);
            Ok(cashFlow);
        }

        public void Add(CASH_FLOW data)
        {
            var msg = new Message
            {
                StatusCode = data.CASH_FLOW_ID,
                Text = "has been Added"
            };
            Ok(msg);
        }

        public void Remove(int Id)
        {
            var msg = new Message
            {
                StatusCode = Id,
                Text = "has been removed"
            };
            Ok(msg);
        }

        public void Update(CASH_FLOW data)
        {
            var msg = new Message
            {
                StatusCode = data.CASH_FLOW_ID,
                Text = "has been update"
            };
            Ok(msg);
        }
        
    }

}
