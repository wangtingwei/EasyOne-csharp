using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InserDataDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void OnAddClick(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            bool isOk = da.InserDt(5000);
            if(isOk == true)
            {
                this.Response.Write("插入成功！");
            }
        }
    }
}
