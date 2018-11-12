namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class IPAdd : AdminPage
    {

        private void AddIP()
        {
            StatIPInfo info = new StatIPInfo();
            info.StartIP = StringHelper.EncodeIP(this.TxtStartIP.Text);
            info.EndIP = StringHelper.EncodeIP(this.TxtEndIP.Text);
            CompareIP(info);
            info.Address = DataSecurity.FilterBadChar(this.TxtIPAddress.Text);
            if (IPStorage.Add(info))
            {
                AdminPage.WriteSuccessMsg("网站统计IP添加成功！", "IPAdd.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>网站统计IP添加失败，添加的IP地址段与系统已存在的记录重叠或来源详细地址重复！</li>");
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("IPManage.aspx");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string str;
            if (((str = BasePage.RequestString("Action")) != null) && (str == "Edit"))
            {
                this.EditIP();
            }
            else
            {
                this.AddIP();
            }
        }

        private static void CompareIP(StatIPInfo info)
        {
            if (info.StartIP > info.EndIP)
            {
                double startIP = info.StartIP;
                info.StartIP = info.EndIP;
                info.EndIP = startIP;
            }
        }

        private void EditIP()
        {
            StatIPInfo info = new StatIPInfo();
            StatIPInfo oldInfo = new StatIPInfo();
            info.StartIP = StringHelper.EncodeIP(this.TxtStartIP.Text);
            info.EndIP = StringHelper.EncodeIP(this.TxtEndIP.Text);
            info.Address = DataSecurity.FilterBadChar(this.TxtIPAddress.Text);
            oldInfo.StartIP = StringHelper.EncodeIP(this.HdnOldStartIP.Value);
            oldInfo.EndIP = StringHelper.EncodeIP(this.HdnOldEndIP.Value);
            CompareIP(info);
            if (IPStorage.Update(info, oldInfo))
            {
                AdminPage.WriteSuccessMsg("网站统计IP修改成功！", "IPManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>网站统计IP修改失败，修改的IP地址段与系统已存在的记录重叠或来源详细地址重复！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str;
                if (((str = BasePage.RequestString("Action")) != null) && (str == "Edit"))
                {
                    if (base.PreviousPage != null)
                    {
                        this.HdnOldStartIP.Value = this.Context.Items["startIP"].ToString();
                        this.HdnOldEndIP.Value = this.Context.Items["endIP"].ToString();
                        this.TxtStartIP.Text = this.HdnOldStartIP.Value;
                        this.TxtEndIP.Text = this.HdnOldEndIP.Value;
                        this.TxtIPAddress.Text = this.Context.Items["address"].ToString();
                        this.BtnSave.Text = " 修改 ";
                        this.LblTitle.Text = "修改";
                    }
                }
                else
                {
                    this.BtnSave.Text = " 添加 ";
                    this.LblTitle.Text = "添加";
                }
            }
        }
    }
}

