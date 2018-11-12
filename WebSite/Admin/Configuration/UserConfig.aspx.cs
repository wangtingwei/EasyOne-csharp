namespace EasyOne.WebSite.Admin.User
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class UserConfig : AdminPage
    {

        private Dictionary<string, ListItem> m_RegFields;


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            SiteConfigInfo info = SiteConfig.ConfigReadFromFile();
            info.UserConfig.EnableUserReg = DataConverter.CBoolean(this.RadlEnableUserReg.SelectedValue);
            info.UserConfig.GroupId = DataConverter.CLng(this.RadlUserGroup.SelectedValue);
            info.UserConfig.EmailCheckReg = DataConverter.CBoolean(this.RadlEmailCheckReg.SelectedValue);
            info.UserConfig.AdminCheckReg = DataConverter.CBoolean(this.RadlAdminCheckReg.SelectedValue);
            info.UserConfig.EnableMultiRegPerEmail = DataConverter.CBoolean(this.RadlEnableMultiRegPerEmail.SelectedValue);
            info.UserConfig.EnableCheckCodeOfReg = DataConverter.CBoolean(this.RadlEnableCheckCodeOfReg.SelectedValue);
            info.UserConfig.EnableQAofReg = DataConverter.CBoolean(this.RadlEnableQAofReg.SelectedValue);
            info.UserConfig.RegQuestion1 = this.TxtRegQuestion1.Text.Trim();
            info.UserConfig.RegAnswer1 = this.TxtRegAnswer1.Text.Trim();
            info.UserConfig.RegQuestion2 = this.TxtRegQuestion2.Text.Trim();
            info.UserConfig.RegAnswer2 = this.TxtRegAnswer2.Text.Trim();
            info.UserConfig.RegQuestion3 = this.TxtRegQuestion3.Text.Trim();
            info.UserConfig.RegAnswer3 = this.TxtRegAnswer3.Text.Trim();
            info.UserConfig.UserNameLimit = DataConverter.CLng(this.TxtUserNameLimit.Text);
            info.UserConfig.UserNameMax = DataConverter.CLng(this.TxtUserNameMax.Text);
            info.UserConfig.UserNameRegDisabled = this.TxtUserName_RegDisabled.Text.Trim();
            info.UserConfig.RegFieldsMustFill = this.HdnRegFields_MustFill.Value;
            info.UserConfig.RegFieldsSelectFill = this.HdnRegFields_SelectFill.Value;
            info.UserConfig.PresentExp = DataConverter.CDouble(this.TxtPresentExp.Text);
            info.UserConfig.PresentMoney = DataConverter.CDouble(this.TxtPresentMoney.Text);
            info.UserConfig.PresentPoint = DataConverter.CLng(this.TxtPresentPoint.Text);
            info.UserConfig.PresentValidNum = DataConverter.CLng(this.TxtPresentValidNum.Text);
            info.UserConfig.PresentValidUnit = DataConverter.CLng(this.DropPresentValidUnit.SelectedValue);
            info.UserConfig.EnableCheckCodeOfLogOn = DataConverter.CBoolean(this.RadlEnableCheckCodeOfLogin.SelectedValue);
            info.UserConfig.EnableMultiLogOn = DataConverter.CBoolean(this.RadlEnableMultiLogin.SelectedValue);
            info.UserConfig.PresentExpPerLogOn = DataConverter.CDouble(this.TxtPresentExpPerLogin.Text);
            info.UserConfig.UserGetPasswordType = DataConverter.CLng(this.RadlGetPasswordType.SelectedValue);
            info.UserConfig.MoneyExchangePointByMoney = DataConverter.CDouble(this.TxtMoneyExchangePoint.Text);
            info.UserConfig.MoneyExchangeValidDayByMoney = DataConverter.CDouble(this.TxtMoneyExchangeValidDay.Text);
            info.UserConfig.UserExpExchangePointByExp = DataConverter.CDouble(this.TxtUserExpExchangePoint.Text);
            info.UserConfig.UserExpExchangeValidDayByExp = DataConverter.CDouble(this.TxtUserExpExchangeValidDay.Text);
            info.UserConfig.MoneyExchangePointByPoint = DataConverter.CDouble(this.TxtCMoneyExchangePoint.Text);
            info.UserConfig.MoneyExchangeValidDayByValidDay = DataConverter.CDouble(this.TxtCMoneyExchangeValidDay.Text);
            info.UserConfig.UserExpExchangePointByPoint = DataConverter.CDouble(this.TxtCUserExpExchangePoint.Text);
            info.UserConfig.UserExpExchangeValidDayByValidDay = DataConverter.CDouble(this.TxtCUserExpExchangeValidDay.Text);
            info.UserConfig.PointName = this.TxtPointName.Text.Trim();
            info.UserConfig.PointUnit = this.TxtPointUnit.Text.Trim();
            info.UserConfig.EmailOfRegCheck = this.TxtEmailOfRegCheck.Text.Trim();
            info.UserConfig.EnableRegCompany = DataConverter.CBoolean(this.RadlEnableRegCompany.SelectedValue);
            SiteConfig config = new SiteConfig();
            try
            {
                config.Update(info);
                AdminPage.WriteSuccessMsg("<li>网站信息配置保存成功！</li>", "UserConfig.aspx");
            }
            catch (FileNotFoundException)
            {
                AdminPage.WriteErrMsg("<li>文件未找到！</li>", "UserConfig.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "UserConfig.aspx");
            }
        }

        private void ModifyConfig()
        {
            this.RadlUserGroup.DataSource = UserGroups.GetGroupTable(GroupType.Register);
            this.RadlUserGroup.DataBind();
            SiteConfigInfo info = SiteConfig.ConfigInfo();
            this.RadlEnableUserReg.SelectedValue = this.SelectValue(info.UserConfig.EnableUserReg);
            BasePage.SetSelectedIndexByValue(this.RadlUserGroup, info.UserConfig.GroupId.ToString());
            this.RadlEmailCheckReg.SelectedValue = this.SelectValue(info.UserConfig.EmailCheckReg);
            this.RadlAdminCheckReg.SelectedValue = this.SelectValue(info.UserConfig.AdminCheckReg);
            this.RadlEnableMultiRegPerEmail.SelectedValue = this.SelectValue(info.UserConfig.EnableMultiRegPerEmail);
            this.RadlEnableCheckCodeOfReg.SelectedValue = this.SelectValue(info.UserConfig.EnableCheckCodeOfReg);
            this.RadlEnableQAofReg.SelectedValue = this.SelectValue(info.UserConfig.EnableQAofReg);
            BasePage.SetSelectedIndexByValue(this.RadlGetPasswordType, info.UserConfig.UserGetPasswordType.ToString());
            this.TxtRegQuestion1.Text = info.UserConfig.RegQuestion1;
            this.TxtRegAnswer1.Text = info.UserConfig.RegAnswer1;
            this.TxtRegQuestion2.Text = info.UserConfig.RegQuestion2;
            this.TxtRegAnswer2.Text = info.UserConfig.RegAnswer2;
            this.TxtRegQuestion3.Text = info.UserConfig.RegQuestion3;
            this.TxtRegAnswer3.Text = info.UserConfig.RegAnswer3;
            this.TxtUserNameLimit.Text = info.UserConfig.UserNameLimit.ToString();
            this.TxtUserNameMax.Text = info.UserConfig.UserNameMax.ToString();
            this.TxtUserName_RegDisabled.Text = info.UserConfig.UserNameRegDisabled;
            string regFieldsMustFill = info.UserConfig.RegFieldsMustFill;
            string regFieldsSelectFill = info.UserConfig.RegFieldsSelectFill;
            this.HdnRegFields_MustFill.Value = regFieldsMustFill;
            this.HdnRegFields_SelectFill.Value = regFieldsSelectFill;
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            if (!string.IsNullOrEmpty(regFieldsMustFill))
            {
                foreach (string str3 in regFieldsMustFill.Split(new char[] { ',' }))
                {
                    list.Add(str3);
                    if (this.m_RegFields.ContainsKey(str3))
                    {
                        this.LitRegFields_MustFill.Items.Add(this.m_RegFields[str3]);
                    }
                }
            }
            if (!string.IsNullOrEmpty(regFieldsSelectFill))
            {
                foreach (string str4 in regFieldsSelectFill.Split(new char[] { ',' }))
                {
                    list2.Add(str4);
                    if (this.m_RegFields.ContainsKey(str4))
                    {
                        this.LitRegFields_SelectFill.Items.Add(this.m_RegFields[str4]);
                    }
                }
            }
            foreach (string str5 in this.m_RegFields.Keys)
            {
                if (!list.Contains(str5) && !list2.Contains(str5))
                {
                    this.LitRegFields.Items.Add(this.m_RegFields[str5]);
                }
            }
            this.TxtPresentExp.Text = info.UserConfig.PresentExp.ToString();
            this.TxtPresentMoney.Text = info.UserConfig.PresentMoney.ToString();
            this.TxtPresentPoint.Text = info.UserConfig.PresentPoint.ToString();
            this.TxtPresentValidNum.Text = info.UserConfig.PresentValidNum.ToString();
            this.DropPresentValidUnit.SelectedValue = info.UserConfig.PresentValidUnit.ToString();
            this.RadlEnableCheckCodeOfLogin.Items[this.SelectIndex(info.UserConfig.EnableCheckCodeOfLogOn)].Selected = true;
            this.RadlEnableMultiLogin.Items[this.SelectIndex(info.UserConfig.EnableMultiLogOn)].Selected = true;
            this.TxtPresentExpPerLogin.Text = info.UserConfig.PresentExpPerLogOn.ToString();
            this.TxtMoneyExchangePoint.Text = info.UserConfig.MoneyExchangePointByMoney.ToString();
            this.TxtMoneyExchangeValidDay.Text = info.UserConfig.MoneyExchangeValidDayByMoney.ToString();
            this.TxtUserExpExchangePoint.Text = info.UserConfig.UserExpExchangePointByExp.ToString();
            this.TxtUserExpExchangeValidDay.Text = info.UserConfig.UserExpExchangeValidDayByExp.ToString();
            this.TxtCMoneyExchangePoint.Text = info.UserConfig.MoneyExchangePointByPoint.ToString();
            this.TxtCMoneyExchangeValidDay.Text = info.UserConfig.MoneyExchangeValidDayByValidDay.ToString();
            this.TxtCUserExpExchangePoint.Text = info.UserConfig.UserExpExchangePointByPoint.ToString();
            this.TxtCUserExpExchangeValidDay.Text = info.UserConfig.UserExpExchangeValidDayByValidDay.ToString();
            this.TxtPointName.Text = info.UserConfig.PointName;
            this.TxtPointUnit.Text = info.UserConfig.PointUnit;
            this.TxtEmailOfRegCheck.Text = info.UserConfig.EmailOfRegCheck;
            this.RadlEnableRegCompany.Items[this.SelectIndex(info.UserConfig.EnableRegCompany)].Selected = true;
            if (!info.SiteOption.EnablePointMoneyExp)
            {
                this.PresentExp.Style.Add("display", "none");
                this.PresentMoney.Style.Add("display", "none");
                this.PresentPoint.Style.Add("display", "none");
                this.PresentValidNum.Style.Add("display", "none");
                this.PresentExpPerLogin.Style.Add("display", "none");
                this.MoneyExchangePoint.Style.Add("display", "none");
                this.MoneyExchangeValidDay.Style.Add("display", "none");
                this.UserExpExchangePoint.Style.Add("display", "none");
                this.UserExpExchangeValidDay.Style.Add("display", "none");
                this.PointName.Style.Add("display", "none");
                this.PointUnit.Style.Add("display", "none");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_RegFields = new Dictionary<string, ListItem>();
            this.m_RegFields.Add("Homepage", new ListItem("主页", "Homepage"));
            this.m_RegFields.Add("QQ", new ListItem("QQ号码", "QQ"));
            this.m_RegFields.Add("ICQ", new ListItem("ICQ号码", "ICQ"));
            this.m_RegFields.Add("MSN", new ListItem("MSN帐号", "MSN"));
            this.m_RegFields.Add("UC", new ListItem("UC号码", "UC"));
            this.m_RegFields.Add("OfficePhone", new ListItem("办公电话", "OfficePhone"));
            this.m_RegFields.Add("HomePhone", new ListItem("家庭电话", "HomePhone"));
            this.m_RegFields.Add("Mobile", new ListItem("手机号码", "Mobile"));
            this.m_RegFields.Add("Fax", new ListItem("传真号码", "Fax"));
            this.m_RegFields.Add("PHS", new ListItem("小灵通", "PHS"));
            this.m_RegFields.Add("Region", new ListItem("国家/地区", "Region"));
            this.m_RegFields.Add("Address", new ListItem("联系地址", "Address"));
            this.m_RegFields.Add("ZipCode", new ListItem("邮政编码", "ZipCode"));
            this.m_RegFields.Add("Yahoo", new ListItem("雅虎通帐号", "Yahoo"));
            this.m_RegFields.Add("Aim", new ListItem("Aim帐号", "Aim"));
            this.m_RegFields.Add("TrueName", new ListItem("真实姓名", "TrueName"));
            this.m_RegFields.Add("Birthday", new ListItem("出生日期", "Birthday"));
            this.m_RegFields.Add("IDCard", new ListItem("身份证号码", "IDCard"));
            this.m_RegFields.Add("Department", new ListItem("部门", "Department"));
            this.m_RegFields.Add("Company", new ListItem("公司/单位", "Company"));
            this.m_RegFields.Add("PosTitle", new ListItem("职务", "PosTitle"));
            this.m_RegFields.Add("Marriage", new ListItem("婚姻状况", "Marriage"));
            this.m_RegFields.Add("Sex", new ListItem("性别", "Sex"));
            this.m_RegFields.Add("Income", new ListItem("收入情况", "Income"));
            this.m_RegFields.Add("UserFace", new ListItem("用户头像", "UserFace"));
            this.m_RegFields.Add("FaceWidth", new ListItem("头像宽度", "FaceWidth"));
            this.m_RegFields.Add("FaceHeight", new ListItem("头像高度", "FaceHeight"));
            this.m_RegFields.Add("Sign", new ListItem("签名档", "Sign"));
            this.m_RegFields.Add("Privacy", new ListItem("隐私设定", "Privacy"));
            if (!this.Page.IsPostBack)
            {
                this.ModifyConfig();
            }
        }

        private int SelectIndex(bool selected)
        {
            if (selected)
            {
                return 0;
            }
            return 1;
        }

        private string SelectValue(bool selected)
        {
            if (selected)
            {
                return "true";
            }
            return "false";
        }
    }
}

