namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Authors : AdminPage
    {
        public string m_ImgPath;

        private void BindAuthorType()
        {
            ChoicesetValueInfoCollection dictionaryFieldValueByName = Choiceset.GetDictionaryFieldValueByName("PE_Author", "Type");
            this.RadlAuthorType.DataSource = dictionaryFieldValueByName;
            this.RadlAuthorType.DataTextField = "DataTextField";
            this.RadlAuthorType.DataValueField = "DataTextField";
            this.RadlAuthorType.DataBind();
            foreach (ChoicesetValueInfo info in dictionaryFieldValueByName)
            {
                if (info.IsDefault)
                {
                    this.RadlAuthorType.SelectedValue = info.DataTextField;
                    break;
                }
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }
            AuthorInfo ainfo = new AuthorInfo();
            ainfo.Id = (this.ViewState["AuthorId"] == null) ? 0 : DataConverter.CLng(this.ViewState["AuthorId"].ToString());
            if (!string.IsNullOrEmpty(this.TxtUserName.Text))
            {
                ainfo.UserId = Author.GetUserId(this.TxtUserName.Text);
            }
            ainfo.Type = this.RadlAuthorType.SelectedValue;
            ainfo.Name = this.TxtName.Text;
            ainfo.Passed = Convert.ToBoolean(this.ChkPass.Checked);
            ainfo.OnTop = Convert.ToBoolean(this.ChkOnTop.Checked);
            ainfo.Elite = Convert.ToBoolean(this.ChkElite.Checked);
            ainfo.Hits = 0;
            ainfo.LastUseTime = DateTime.Now;
            ainfo.Photo = this.ExtenFileUpload.FilePath;
            ainfo.Intro = this.TxtIntro.Text;
            ainfo.Address = this.TxtAddress.Text;
            ainfo.Tel = this.TxtTel.Text;
            ainfo.Fax = this.TxtFax.Text;
            ainfo.Mail = this.TxtMail.Text;
            ainfo.Email = this.TxtEmail.Text;
            ainfo.ZipCode = DataConverter.CLng(this.TxtZipCode.Text);
            ainfo.HomePage = this.TxtHomePage.Text;
            ainfo.Imeeting = this.TxtIm.Text;
            ainfo.Sex = DataConverter.CLng(this.RadlSex.Text);
            ainfo.BirthDay = DataConverter.CDate(this.TxtBirthDay.Text);
            ainfo.Company = this.TxtCompany.Text;
            ainfo.Department = this.TxtDepartment.Text;
            bool flag = false;
            string str = this.ViewState["action"].ToString();
            if (str != null)
            {
                if (!(str == "add"))
                {
                    if (str == "modify")
                    {
                        flag = Author.Update(ainfo);
                        goto Label_0236;
                    }
                }
                else
                {
                    flag = Author.Add(ainfo);
                    goto Label_0236;
                }
            }
            flag = Author.Add(ainfo);
        Label_0236:
            if (flag)
            {
                if (this.ViewState["action"].ToString() == "add")
                {
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("添加作者成功！", "AuthorManage.aspx");
                    return;
                }
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("修改作者成功！", "AuthorManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("操作失败！", "AuthorManage.aspx");
            }
        }

        private void FillData()
        {
            this.ViewState["AuthorId"] = BasePage.RequestString("Id");
            AuthorInfo authorInfoById = Author.GetAuthorInfoById(DataConverter.CLng(BasePage.RequestString("Id")));
            if (authorInfoById.IsNull)
            {
                AdminPage.WriteErrMsg("错误的作者ID！", "AuthorManage.aspx");
            }
            this.TxtUserName.Text = Author.GetUserName(authorInfoById.UserId);
            BasePage.SetSelectedIndexByValue(this.RadlAuthorType, authorInfoById.Type);
            this.TxtName.Text = authorInfoById.Name;
            this.ChkPass.Checked = authorInfoById.Passed;
            this.ChkOnTop.Checked = authorInfoById.OnTop;
            this.ChkElite.Checked = authorInfoById.Elite;
            this.ExtenFileUpload.FilePath = authorInfoById.Photo;
            this.TxtIntro.Text = authorInfoById.Intro;
            this.TxtAddress.Text = authorInfoById.Address;
            this.TxtTel.Text = authorInfoById.Tel;
            this.TxtFax.Text = authorInfoById.Fax;
            this.TxtMail.Text = authorInfoById.Mail;
            this.TxtEmail.Text = authorInfoById.Email;
            this.TxtZipCode.Text = Convert.ToString(authorInfoById.ZipCode, (IFormatProvider) null);
            this.TxtHomePage.Text = authorInfoById.HomePage;
            this.TxtIm.Text = authorInfoById.Imeeting;
            for (int i = 0; i < this.RadlSex.Items.Count; i++)
            {
                if (this.RadlSex.Items[i].Value == Convert.ToString(authorInfoById.Sex, (IFormatProvider) null))
                {
                    this.RadlSex.Items[i].Selected = true;
                }
            }
            this.TxtBirthDay.Text = authorInfoById.BirthDay.ToString("yyyy-MM-dd");
            this.TxtCompany.Text = authorInfoById.Company;
            this.TxtDepartment.Text = authorInfoById.Department;
            if (!string.IsNullOrEmpty(this.ExtenFileUpload.FilePath))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "showphoto", "<script type='text/javascript'>document.getElementById(\"showphoto\").src ='" + this.m_ImgPath + "/" + this.ExtenFileUpload.FilePath + "';</script>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ImgPath = base.BasePath + SiteConfig.SiteOption.UploadDir;
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestStringToLower("Action", "add");
                this.BindAuthorType();
                switch (str)
                {
                    case "add":
                        this.ViewState["action"] = "add";
                        this.TxtUserName.Text = Author.GetUserName(BasePage.RequestInt32("UserID"));
                        return;

                    case "modify":
                        this.ViewState["action"] = "modify";
                        this.FillData();
                        return;
                }
                this.ViewState["action"] = "add";
            }
        }
    }
}

