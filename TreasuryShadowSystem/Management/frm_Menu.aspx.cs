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

namespace TreasuryShadowSystem.Management
{
    public partial class frm_Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static object MenuList(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return TreasuryModel.Data.Menu.MenuList(Config.ConnectionString, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreateMenu(TreasuryModel.Data.Menu record)
        {
            return TreasuryModel.Data.Menu.CreateMenu(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateMenu(TreasuryModel.Data.Menu record)
        {
            return TreasuryModel.Data.Menu.UpdateMenu(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteMenu(int ID)
        {
            return TreasuryModel.Data.Menu.DeleteMenu(Config.ConnectionString, ID);
        }
        
        [WebMethod(EnableSession = true)]
        public static object GetParentOption()
        {
            List<TreasuryModel.Data.Menu> _menu = TreasuryModel.Data.Menu.ParentList(Config.ConnectionString);
            var menu = _menu.OrderBy(p => p.ID)
                                .Select(c => new { DisplayText = c.Data_Name_Eng, Value = c.ID });

            return new { Result = "OK", Options = menu };
        }

        [WebMethod(EnableSession = true)]
        public static object GetGroupOption()
        {
            List<TreasuryModel.Data.Menu> _menu = TreasuryModel.Data.Menu.ParentGroupList(Config.ConnectionString);
            var menu = _menu.OrderBy(p => p.ID)
                                .Select(c => new { DisplayText = c.Data_Name_Eng, Value = c.ID });

            return new { Result = "OK", Options = menu };
        }
    }
}
