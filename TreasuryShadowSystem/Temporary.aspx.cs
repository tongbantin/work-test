using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using TreasuryModel.Model;
using TreasuryModel.Repository;

namespace TreasuryShadowSystem
{
    public partial class Temporary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductRepository p = new ProductRepository(Configuration.Config.SConnectionString);
            GridView1.DataSource = p.getProduct();
            GridView1.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList r = new ArrayList();
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            {
                StockIn s = new StockIn();
                s.ProductID = Convert.ToInt32(GridView1.Rows[i].Cells[0].Text);
                s.Code = GridView1.Rows[i].Cells[1].Text;
                s.InDate = DateTime.Now.ToString("dd/MM/yyyy");
                s.Weight = 4;
                s.Price = 1200;
                s.CPU = 300;
                s.DocNo = "INV0001234";
                r.Add(s);
            }
            StockRepository stock = new StockRepository(Configuration.Config.SConnectionString);
            ArrayList trash = stock.StockIn(r);
            GridView1.DataSource = trash;
            GridView1.DataBind();
        }
    }
}
