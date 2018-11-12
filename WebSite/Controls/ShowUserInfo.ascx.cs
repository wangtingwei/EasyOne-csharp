namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ShowUserInfo : BaseUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                EasyOne.Model.UserManage.UserInfo userInfo = this.UserInfo;
                if (userInfo == null)
                {
                    userInfo = Users.GetUserById(DataConverter.CLng(base.Request.QueryString["UserID"]));
                    this.ViewState["UserInfo"] = userInfo;
                }
                if (userInfo.IsNull)
                {
                    BaseUserControl.WriteErrMsg("<li>找不到指定的会员！</li>");
                }
                else
                {
                    if (string.IsNullOrEmpty(userInfo.GroupName))
                    {
                        userInfo.GroupName = UserGroups.GetUserGroupById(userInfo.GroupId).GroupName;
                    }
                    this.LblUserName.Text = userInfo.UserName;
                    this.LblUserGroup.Text = userInfo.GroupName;
                    if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                    {
                        this.Balance.Style.Add("display", "none");
                        this.Point.Style.Add("display", "none");
                        this.UserExp.Style.Add("display", "none");
                        this.EndTime.Style.Add("display", "none");
                    }
                    else
                    {
                        this.LblBalance.Text = userInfo.Balance.ToString("0.00");
                        this.LblPoint.Text = userInfo.UserPoint.ToString();
                        if (!userInfo.EndTime.HasValue)
                        {
                            this.LblEndTime.Text = "";
                        }
                        else
                        {
                            this.LblEndTime.Text = userInfo.EndTime.Value.ToString("yyyy-MM-dd");
                        }
                        this.LblUserExp.Text = userInfo.UserExp.ToString();
                        this.LblValidDays.Text = Users.GetValidNum(userInfo.EndTime);
                    }
                }
            }
        }

        public EasyOne.Model.UserManage.UserInfo UserInfo
        {
            get
            {
                if (this.ViewState["UserInfo"] != null)
                {
                    return (EasyOne.Model.UserManage.UserInfo) this.ViewState["UserInfo"];
                }
                return null;
            }
            set
            {
                this.ViewState["UserInfo"] = value;
            }
        }
    }
}

