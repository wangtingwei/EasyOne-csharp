using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace InserDataDemo
{
    public partial class Display : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            DataTable dt1 = da.GetStuInfo();
            this.Repeater1.DataSource = dt1;
            this.Repeater1.DataBind();
            this.Repeater1.ItemCreated+=new RepeaterItemEventHandler(Repeater1_ItemCreated);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                string str = dt1.Rows[i]["b_name"] as string;
                DataTable dt2 = da.GetStuInfoByBan(str);
                Repeater rep = this.Repeater1.Controls[0] as Repeater;
                if (rep != null)
                {
                    rep.DataSource = dt2;
                    rep.DataBind();
                }

            }
        }
        protected void Repeater1_ItemCreated(object sender, EventArgs e)
        {
            
        }
    }
}
