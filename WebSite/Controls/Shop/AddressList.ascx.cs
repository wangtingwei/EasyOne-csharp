namespace EasyOne.WebSite.Controls
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class AddressList : BaseUserControl
    {
        protected void EgvAddress_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AddressInfo dataItem = (AddressInfo) e.Row.DataItem;
                string str = dataItem.Country + "$$$" + dataItem.Province + "$$$" + dataItem.City + "$$$" + dataItem.Area + "$$$" + dataItem.ZipCode + "$$$" + dataItem.Address + "$$$" + dataItem.ConsigneeName + "$$$" + dataItem.HomePhone + "$$$" + dataItem.Mobile;
                string format = "<a href='#' onclick=\"window.opener.__doPostBack('AddressList_PostBack','{0}');window.close();\">{1}</a>";
                e.Row.Cells[0].Text = string.Format(format, str, dataItem.ConsigneeName);
                e.Row.Cells[1].Text = string.Format(format, str, dataItem.Country);
                e.Row.Cells[2].Text = string.Format(format, str, dataItem.Province);
                e.Row.Cells[3].Text = string.Format(format, str, dataItem.City);
                e.Row.Cells[4].Text = string.Format(format, str, dataItem.Area);
                e.Row.Cells[5].Text = string.Format(format, str, dataItem.ZipCode);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = BaseUserControl.RequestString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                userName = PEContext.Current.User.UserName;
            }
            if (!this.Page.IsPostBack)
            {
                this.EgvAddress.DataSource = Address.GetAddressListByUserName(userName);
                this.EgvAddress.DataBind();
            }
        }
    }
}

