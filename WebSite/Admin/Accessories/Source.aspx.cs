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
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class Sources : AdminPage
    {
        public string m_ImgPath;

        public void BindDropList(ListControl dropname)
        {
            IList<SourceInfo> sourceTypeList = Source.GetSourceTypeList();
            dropname.Items.Clear();
            dropname.DataSource = sourceTypeList;
            dropname.DataBind();
            this.DropAuthorType.Attributes.Add("OnChange", "OnChangeCategory(this.options[this.selectedIndex].text)");
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.TxtName.Text))
            {
                base.Response.Write("来源名不能为空");
            }
            SourceInfo ainfo = new SourceInfo();
            ainfo.Id = BasePage.RequestInt32("Id");
            ainfo.Type = this.TxtType.Text;
            ainfo.Name = this.TxtName.Text;
            ainfo.Passed = this.ChkPass.Checked;
            ainfo.OnTop = this.ChkOnTop.Checked;
            ainfo.Elite = this.ChkElite.Checked;
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
            ainfo.ContacterName = this.TxtContacterName.Text;
            bool flag = false;
            string str = this.ViewState["action"].ToString();
            if (str != null)
            {
                if (!(str == "Add"))
                {
                    if (str == "Modify")
                    {
                        flag = Source.Update(ainfo);
                        goto Label_01BF;
                    }
                }
                else
                {
                    flag = Source.Add(ainfo);
                    goto Label_01BF;
                }
            }
            flag = Source.Add(ainfo);
        Label_01BF:
            if (flag)
            {
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload()</script>");
                if (this.ViewState["action"].ToString() == "Add")
                {
                    AdminPage.WriteSuccessMsg("添加来源成功！", "SourceManage.aspx");
                    return;
                }
                AdminPage.WriteSuccessMsg("修改来源成功！", "SourceManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("操作失败！", "SourceManage.aspx");
            }
        }

        private void FillData()
        {
            SourceInfo sourceInfoById = Source.GetSourceInfoById(BasePage.RequestInt32("Id"));
            this.TxtType.Text = sourceInfoById.Type;
            BasePage.SetSelectedIndexByValue(this.DropAuthorType, sourceInfoById.Type);
            this.TxtName.Text = sourceInfoById.Name;
            this.ChkPass.Checked = sourceInfoById.Passed;
            this.ChkOnTop.Checked = sourceInfoById.OnTop;
            this.ChkElite.Checked = sourceInfoById.Elite;
            this.ExtenFileUpload.FilePath = sourceInfoById.Photo;
            this.TxtIntro.Text = sourceInfoById.Intro;
            this.TxtAddress.Text = sourceInfoById.Address;
            this.TxtTel.Text = sourceInfoById.Tel;
            this.TxtFax.Text = sourceInfoById.Fax;
            this.TxtMail.Text = sourceInfoById.Mail;
            this.TxtEmail.Text = sourceInfoById.Email;
            this.TxtZipCode.Text = sourceInfoById.ZipCode.ToString();
            this.TxtHomePage.Text = sourceInfoById.HomePage;
            this.TxtIm.Text = sourceInfoById.Imeeting;
            this.TxtContacterName.Text = sourceInfoById.ContacterName;
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
                this.BindDropList(this.DropAuthorType);
                switch (BasePage.RequestString("Action", "Add"))
                {
                    case "Add":
                        this.ViewState["action"] = "Add";
                        return;

                    case "Modify":
                        this.ViewState["action"] = "Modify";
                        this.LblPTitle.Text = "修改来源信息";
                        this.FillData();
                        return;
                }
                this.ViewState["action"] = "Add";
            }
        }
    }
}

