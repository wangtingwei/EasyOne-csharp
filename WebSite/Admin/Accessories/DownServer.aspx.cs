namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class DownServers : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                DownServerInfo downServerInfo = new DownServerInfo();
                downServerInfo.ServerId = BasePage.RequestInt32("ServerID");
                downServerInfo.ServerName = this.TxtServerName.Text;
                downServerInfo.ServerLogo = this.TxtServerLogo.Text;
                if (DataValidator.IsUrl(this.TxtServerUrl.Text))
                {
                    downServerInfo.ServerUrl = new Uri(this.TxtServerUrl.Text);
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>对不起，请输入正确格式的下载服务器地址！</li>", "DownServer.aspx");
                }
                downServerInfo.ShowType = DataConverter.CLng(this.DropShowType.SelectedValue);
                bool flag = false;
                string str2 = BasePage.RequestString("Action", "Add");
                if (str2 != null)
                {
                    if (!(str2 == "Add"))
                    {
                        if (str2 == "Modify")
                        {
                            flag = DownServer.Update(downServerInfo);
                        }
                    }
                    else
                    {
                        flag = DownServer.Add(downServerInfo);
                    }
                }
                if (flag)
                {
                    AdminPage.WriteSuccessMsg("<li>已经成功保存下载服务器信息！</li>", "DownServerManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>下载服务器信息保存失败！</li>", "DownServerManage.aspx");
                }
            }
        }

        private void InitData(int serverId)
        {
            if (serverId > 0)
            {
                DownServerInfo downServerById = DownServer.GetDownServerById(serverId);
                this.TxtServerName.Text = downServerById.ServerName;
                this.TxtServerLogo.Text = downServerById.ServerLogo;
                this.TxtServerUrl.Text = downServerById.ServerUrl.ToString();
                this.DropShowType.SelectedValue = downServerById.ShowType.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int serverId = BasePage.RequestInt32("ServerID");
                if (BasePage.RequestString("Action", "Add") == "Modify")
                {
                    this.InitData(serverId);
                }
            }
        }
    }
}

