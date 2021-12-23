using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;
using System.Web.Script.Serialization;
using System.Data;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_BrokerageFee : System.Web.UI.Page
    {
        private List<string> broker = new List<string>();
        private string Product = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Product = Request.QueryString["Product"];
                if (!IsPostBack)
                {
                    string message = Config.GetParameterFile("100Brok");
                    broker = message.Split(',').Select(p => p.Trim()).ToList<string>();                    
                    BindProduct();
                    BindBroker();
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void BindProduct()
        {
            try
            {
                string message = "";
                if (Product.Equals("FI"))
                {
                    message = Config.GetParameterFile("099Product");                    
                }
                else if (Product.Equals("FX"))
                {
                    message = Config.GetParameterFile("100Product");                    
                }

                var param = message.Split(',').Select(p => new { DisplayText = p.Trim().Substring(0, p.Trim().IndexOf('=')), Value = p.Trim().Substring(p.Trim().IndexOf('=') + 1) });

                prodtype.Items.Clear();
                prodtype.DataSource = param;
                prodtype.DataTextField = "DisplayText";
                prodtype.DataValueField = "Value";
                prodtype.DataBind();
            }
            catch (Exception ex)
            { 
            
            }
        }

        private void BindBroker()
        {
            try
            {
                brok.DataSource = broker;
                brok.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod(EnableSession = true)]
        public static object BrokerageFeeList(string dealno, string dealdate, string brok, string prodtype, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return BrokerageFee.BrokerageFeeList(Config.ConnectionString, dealno, dealdate, brok, prodtype, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreateBrokerageFee(BrokerageFee record)
        {
            return BrokerageFee.CreateBrokerageFee(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateBrokerageFee(BrokerageFee record)
        {
            return BrokerageFee.UpdateBrokerageFee(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteBrokerageFee(int DEALFEEID)
        {
            return BrokerageFee.DeleteBrokerageFee(Config.ConnectionString, DEALFEEID);
        }

        [WebMethod(EnableSession = true)]
        public static object NoFeeBrokerageFee(string DealFee)
        {
            return BrokerageFee.NoFeeBrokerageFee(Config.ConnectionString, DealFee);
        }
        
        [WebMethod(EnableSession = true)]
        public static object BrokerList()
        {
            string message = Config.GetParameterFile("100Brok");
            var param = message.Split(',').Select(p => new { DisplayText = p.Trim(), Value = p.Trim() });
            return new { Result = "OK", Options = param };
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                    BrokerageFee.CalculateFee(Config.ConnectionString, Product, FromDate.Value, ToDate.Value);
                
                    MessageBox.Text = "ระบบทำรายการเรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
