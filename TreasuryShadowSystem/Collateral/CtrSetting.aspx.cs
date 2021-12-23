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
    using TreasuryModel.Repository;

    public partial class CtrSetting : AjaxBasePage<CTR_SETTING>, IController<SettingFilter, CTR_SETTING> 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            try
            {
                string json = string.Empty;
                int Id = 0;
                SettingFilter filter = new SettingFilter();
                CTR_SETTING model = new CTR_SETTING();
                string Operator = Request.QueryString["Operator"];
                if (Request.Form.Count > 0) json = Request.Form[0];
                if (Request.QueryString["Id"] != null) Id = Convert.ToInt32(Request.QueryString["Id"]);

                switch (Operator.ToUpper())
                {
                    case "GETALL":
                        filter = JsonConvert.DeserializeObject<SettingFilter>(json);
                        GetAll(filter);
                        break;
                    case "ADD":
                        model = JsonConvert.DeserializeObject<CTR_SETTING>(json);
                        Add(model);
                        break;
                    case "UPDATE":
                        model = JsonConvert.DeserializeObject<CTR_SETTING>(json);
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


        #region IController<SettingFilter,CTR_SETTING> Members

        public void GetAll(SettingFilter filter)
        {
            List<CTR_SETTING> results = CounterPartyConfigRepository.GetAll();
            Ok(results);
        }

        public void GetById(int Id)
        {
            CTR_SETTING result = CounterPartyConfigRepository.GetById(Id);
            Ok(result);
        }

        public void Remove(int Id)
        {
            int effected = CounterPartyConfigRepository.Delete(Id);
            Ok(effected);
        }

        public void Add(CTR_SETTING data)
        {
            int effected = -1;
            int effectedTrans = -1;
            try
            {
                effected = CounterPartyConfigRepository.Create(data);
                if (effected > 1)
                {
                    var conf = CounterPartyConfigRepository.GetAll().FirstOrDefault(x => (x.CTR_NAME == data.CTR_NAME) && (x.CREATED_DATE == DateTime.Today.ToString("yyyy-MM-dd")));
                    IList<TRANS_TYPE> transTypes = data.CONDITION.Select(c => { c.SETTING_ID = conf.SETTING_ID; return c; }).ToList();
                    foreach (TRANS_TYPE tran in transTypes)
                        effectedTrans += TransactionRepository.SaveTransTypeConfig(tran);
                    
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Ok(effected);
        }

        public void Update(CTR_SETTING data)
        {
            int effected = CounterPartyConfigRepository.Update(data);
            Ok(effected);
        }

        #endregion

    }

}
