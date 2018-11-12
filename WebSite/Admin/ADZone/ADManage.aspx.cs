namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using EasyOne.Web.UI;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public partial class ADManage : AdminPage
    {

        private void DoDelegate(BatchPassedOrCancelPassedOrDelete batch)
        {
            string str = this.GdvAD.SelectList.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                if (batch(str))
                {
                    BasePage.ResponseRedirect("ADManage.aspx");
                }
            }
            else
            {
                AdminPage.WriteErrMsg("没有选中任何广告！");
            }
        }

        protected void EBtnCancelPased_Click(object sender, EventArgs e)
        {
            BatchPassedOrCancelPassedOrDelete batch = new BatchPassedOrCancelPassedOrDelete(Advertisement.CancelPassed);
            this.DoDelegate(batch);
        }

        protected void EBtnDelete_Click(object sender, EventArgs e)
        {
            BatchPassedOrCancelPassedOrDelete batch = new BatchPassedOrCancelPassedOrDelete(Advertisement.Delete);
            this.DoDelegate(batch);
        }

        protected void EBtnPassed_Click(object sender, EventArgs e)
        {
            BatchPassedOrCancelPassedOrDelete batch = new BatchPassedOrCancelPassedOrDelete(Advertisement.Passed);
            this.DoDelegate(batch);
        }

        protected void GdvAD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AdvertisementInfo dataItem = (AdvertisementInfo) e.Row.DataItem;
                Label label = (Label) e.Row.FindControl("LabViews");
                Label label2 = (Label) e.Row.FindControl("LabClicks");
                Label label3 = (Label) e.Row.FindControl("LabDays");
                HyperLink link = (HyperLink) e.Row.FindControl("HypPreview");
                link.NavigateUrl = "PreviewAD.aspx?Type=Ad&AdId=" + dataItem.ADId;
                Label label4 = (Label) e.Row.FindControl("LabRate");
                if (dataItem.Clicks == 0)
                {
                    label4.Text = "0%";
                }
                else if (dataItem.Views == 0)
                {
                    label4.Text = "100%";
                }
                else
                {
                    label4.Text = (((((float) dataItem.Clicks) / ((float) dataItem.Views)) * 100f)).ToString() + "%";
                }
                if (dataItem.ADType == 4)
                {
                    link.Attributes.Add("onmouseover", "ShowADPreview('&nbsp;代码广告请点击预览&nbsp;')");
                }
                else
                {
                    link.Attributes.Add("onmouseover", "ShowADPreview('" + Advertisement.GetAdContent(dataItem) + "')");
                }
                link.Attributes.Add("onmouseout", "hideTooltip('dHTMLADPreview')");
                if (dataItem.CountClick)
                {
                    label2.Text = dataItem.Clicks.ToString();
                }
                else
                {
                    label2.Text = "<font color='#999999'>不统计</font>";
                }
                if (dataItem.CountView)
                {
                    label.Text = dataItem.Views.ToString();
                }
                else
                {
                    label.Text = "<font color='#999999'>不统计</font>";
                }
                if (dataItem.Days >= 0)
                {
                    label3.Text = dataItem.Days.ToString() + "天";
                }
                else
                {
                    label3.Text = "<font color=\"red\">已经过期</font>";
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action");
            string redirecturl = "ADManage.aspx";
            if (!string.IsNullOrEmpty(str))
            {
                string id = BasePage.RequestString("AdId");
                string str4 = str;
                if (str4 != null)
                {
                    if (!(str4 == "Delete"))
                    {
                        if (!(str4 == "Copy"))
                        {
                            if (!(str4 == "Passed"))
                            {
                                if (str4 == "CancelPassed")
                                {
                                    Advertisement.CancelPassed(id);
                                    BasePage.ResponseRedirect(redirecturl);
                                }
                                return;
                            }
                            Advertisement.Passed(id);
                            BasePage.ResponseRedirect(redirecturl);
                            return;
                        }
                    }
                    else
                    {
                        Advertisement.Delete(id);
                        BasePage.ResponseRedirect(redirecturl);
                        return;
                    }
                    if (Advertisement.CopyAdvertisement(id))
                    {
                        AdminPage.WriteSuccessMsg("复制广告成功！", redirecturl);
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("复制广告失败！", redirecturl);
                    }
                }
            }
        }

        private delegate bool BatchPassedOrCancelPassedOrDelete(string id);
    }
}

