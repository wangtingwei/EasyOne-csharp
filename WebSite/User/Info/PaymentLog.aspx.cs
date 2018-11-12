namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class PaymentLog : DynamicPage
    {

        protected void EgvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label = e.Row.FindControl("LblPlatform") as Label;
                Label label2 = e.Row.FindControl("LblStatus") as Label;
                PaymentLogInfo dataItem = e.Row.DataItem as PaymentLogInfo;
                if (dataItem != null)
                {
                    if (label != null)
                    {
                        label.Text = PayPlatform.GetPayPlatformById(dataItem.PlatformId).PayPlatformName;
                    }
                    if (label2 != null)
                    {
                        label2.Text = EasyOne.Accessories.PaymentLog.GetStatusDepict(dataItem.PlatformId, dataItem.Status);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.HdnUserName.Value = PEContext.Current.User.UserName;
            }
        }
    }
}

