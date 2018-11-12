namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;
    using EasyOne.Enumerations;

    public partial class UserManage : AdminPage
    {
        protected string m_PointName = SiteConfig.UserConfig.PointName;

        private void CheckSelectListAndRedirectUrl(string pageName)
        {
            string str = this.EgvUser.SelectList.ToString();
            if (string.IsNullOrEmpty(str))
            {
                BasePage.ResponseRedirect(pageName);
            }
            else
            {
                string str2 = "?";
                if (pageName.IndexOf('?') > 0)
                {
                    str2 = "&";
                }
                BasePage.ResponseRedirect(pageName + str2 + "UserID=" + str);
            }
        }

        private void CheckSelectListIsEmpty(string strItemId, string errorMsg)
        {
            if (string.IsNullOrEmpty(strItemId))
            {
                AdminPage.WriteErrMsg(errorMsg);
            }
        }

        protected void EBtnBatchDelete_Click(object sender, EventArgs e)
        {
            string strItemId = this.EgvUser.SelectList.ToString();
            this.CheckSelectListIsEmpty(strItemId, "<li>对不起，您还没选择要删除的会员！</li>");
            if (ApiData.IsAPiEnable())
            {
                string str2 = "";
                if (!DataValidator.IsValidId(strItemId))
                {
                    AdminPage.WriteErrMsg("<li>删除失败！</li>");
                }
                foreach (string str3 in strItemId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    UserInfo userById = Users.GetUserById(DataConverter.CLng(str3));
                    str2 = str2 + userById.UserName + ",";
                }
                string str4 = ApiFunction.DeleteUsers(str2.Remove(str2.Length - 1));
                if (str4 != "true")
                {
                    AdminPage.WriteErrMsg("<li>" + str4 + "</li><br><li>删除失败！</li>");
                }
            }
            if (Users.Delete(strItemId))
            {
                BasePage.ResponseRedirect("UserManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void EBtnBatchLock_Click(object sender, EventArgs e)
        {
            string strItemId = this.EgvUser.SelectList.ToString();
            this.CheckSelectListIsEmpty(strItemId, "<li>对不起，您还没选择要锁定的会员！</li>");
            Users.BatchLock(strItemId);
            AdminPage.WriteSuccessMsg("<li>批量锁定成功！</li>", "UserManage.aspx");
        }

        protected void EBtnBatchMove_Click(object sender, EventArgs e)
        {
            this.CheckSelectListAndRedirectUrl("BatchMove.aspx");
        }

        protected void EBtnBatchunLock_Click(object sender, EventArgs e)
        {
            string strItemId = this.EgvUser.SelectList.ToString();
            this.CheckSelectListIsEmpty(strItemId, "<li>对不起，您还没选择要解锁的会员！</li>");
            Users.BatchUnlock(strItemId);
            AdminPage.WriteSuccessMsg("<li>批量解锁成功！</li>", "UserManage.aspx");
        }

        protected void EBtnSendEmail_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.ManageDir + "/Accessories/MailListSend.aspx");
        }

        protected void EBtnSendTelMessage_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(SiteConfig.SiteInfo.VirtualPath + SiteConfig.SiteOption.ManageDir + "/SMS/SmsMessageToUser.aspx");
        }

        protected void EBtnUserAuditing_Click(object sender, EventArgs e)
        {
            string strItemId = this.EgvUser.SelectList.ToString();
            this.CheckSelectListIsEmpty(strItemId, "<li>对不起，您还没选择要审核的会员！</li>");
            Users.BatchAuditing(strItemId);
            AdminPage.WriteSuccessMsg("<li>批量审核成功！</li>", "UserManage.aspx");
        }

        protected void EBtnUserMoneyAdd_Click(object sender, EventArgs e)
        {
            this.CheckSelectListAndRedirectUrl("BatchAddMoney.aspx?Action=AddMoney");
        }

        protected void EBtnUserMoneyMinus_Click(object sender, EventArgs e)
        {
            this.CheckSelectListAndRedirectUrl("BatchAddMoney.aspx?Action=SubtractMoeny");
        }

        protected void EBtnUserNormal_Click(object sender, EventArgs e)
        {
            string strItemId = this.EgvUser.SelectList.ToString();
            this.CheckSelectListIsEmpty(strItemId, "<li>对不起，您还没选择要置为正常的会员！</li>");
            Users.BatchNormal(strItemId);
            AdminPage.WriteSuccessMsg("<li>批量置为正常成功！</li>", "UserManage.aspx");
        }

        protected void EBtnUserPointAdd_Click(object sender, EventArgs e)
        {
            this.CheckSelectListAndRedirectUrl("BatchAddPoint.aspx?Action=AddPoint");
        }

        protected void EBtnUserPointMinus_Click(object sender, EventArgs e)
        {
            this.CheckSelectListAndRedirectUrl("BatchAddPoint.aspx?Action=SubtractPoint");
        }

        protected void EBtnUserValidDateAdd_Click(object sender, EventArgs e)
        {
            this.CheckSelectListAndRedirectUrl("BatchAddValidDate.aspx?Action=AddValidDate");
        }

        protected void EBtnUserValidDateMinus_Click(object sender, EventArgs e)
        {
            this.CheckSelectListAndRedirectUrl("BatchAddValidDate.aspx?Action=SubtractValidDate");
        }

        protected void EgvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            UserInfo dataItem = (UserInfo) e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label = (Label) e.Row.FindControl("LblUserType");
                Label label2 = (Label) e.Row.FindControl("LblValidNum");
                Label label3 = (Label) e.Row.FindControl("LblStatus");
                label.Text = BasePage.EnumToHtml<UserType>(dataItem.UserType);
                label3.Text = BasePage.EnumToHtml<UserStatus>(dataItem.Status);
                label2.Text = Users.GetValidNum(dataItem.EndTime);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.EBtnUserPointAdd.Text = "奖励" + this.m_PointName;
            this.EBtnUserPointMinus.Text = "扣除" + this.m_PointName;
            this.EgvUser.Columns[4].HeaderText = "可用" + this.m_PointName;
            if (!base.IsPostBack)
            {
                if (base.Request.UrlReferrer.ToString().Contains("UserGroupManage"))
                {
                    this.SmpNavigator.AdditionalNode = "会员组管理 >> " + this.ShowUserNode();
                }
                else
                {
                    this.SmpNavigator.AdditionalNode = this.ShowUserNode();
                }
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.EgvUser.Columns[3].Visible = false;
                    this.EgvUser.Columns[4].Visible = false;
                    this.EgvUser.Columns[5].Visible = false;
                    this.EgvUser.Columns[6].Visible = false;
                    this.EBtnUserMoneyAdd.Visible = false;
                    this.EBtnUserMoneyMinus.Visible = false;
                    this.EBtnUserPointAdd.Visible = false;
                    this.EBtnUserPointMinus.Visible = false;
                    this.EBtnUserValidDateAdd.Visible = false;
                    this.EBtnUserValidDateMinus.Visible = false;
                }
            }
        }

        protected string ShowUserNode()
        {
            string str = BasePage.RequestString("KeyWord");
            string str2 = "所属 <span style='color:#f00'>" + BasePage.RequestString("GroupName") + "</span> 会员组会员";
            string str3 = "根据ID“ <span style='color:#f00'>" + str + "</span> ”搜索的会员";
            string str4 = "Email中含有“ <span style='color:#f00'>" + str + "</span> ”的会员";
            string[] strArray = new string[] { 
                "所有会员", "文章最多TOP100", "文章最少的100个会员", "最近24小时内登录的会员", "最近24小时内注册的会员", "所有被锁住的会员", this.m_PointName + "数大于0的会员", "积分大于0的会员", "资金余额大于0的会员", "资金余额小于等于0的会员", str2, str3, "会员名中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", str4, "未通过邮件验证的会员", "未通过管理员认证的会员", 
                "消费金额TOP100的会员", "消费" + this.m_PointName + "TOP100的会员", "消费积分TOP100的会员", "有效期剩余5天的会员", "个人主页中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "真实姓名中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "身份证号码中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "单位名称中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "联系地址中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "手机号码中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "办公电话中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "家庭电话中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "小灵通中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "传真号码中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "QQ号中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "ICQ号中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", 
                "MSN帐号中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "雅虎通帐号中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "UC号中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "Aim帐号中含有“ <span style='color:#f00'>" + str + "</span> ”的会员", "设置了单独权限的会员"
             };
            return strArray[BasePage.RequestInt32("ListType")];
        }
    }
}

