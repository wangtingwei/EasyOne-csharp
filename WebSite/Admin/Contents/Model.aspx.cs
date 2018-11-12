namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Model : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string text = this.TxtModelName.Text;
            int modelTemplateId = DataConverter.CLng(this.DropModelTemplate.SelectedValue);
            ModelInfo modelInfo = new ModelInfo();
            modelInfo.ModelName = this.TxtModelName.Text;
            modelInfo.Description = this.TxtDescription.Text;
            modelInfo.ItemName = this.TxtItemName.Text;
            modelInfo.ItemUnit = this.TxtItemUnit.Text;
            modelInfo.ItemIcon = this.TxtItemIcon.Text;
            modelInfo.TableName = "PE_U_" + this.TxtTableName.Text;
            modelInfo.IsEshop = false;
            modelInfo.IsCountHits = DataConverter.CBoolean(this.RadioIsCountHits.SelectedValue);
            modelInfo.Disabled = DataConverter.CBoolean(this.RadioDisabled.SelectedValue);
            modelInfo.DefaultTemplateFile = this.FileCTemplate.Text;
            modelInfo.PrintTemplate = this.TscPrintTemplate.Text;
            modelInfo.SearchTemplate = this.TscSearchTemplate.Text;
            modelInfo.AdvanceSearchFormTemplate = this.TscAdvanceSearchFormTemplate.Text;
            modelInfo.AdvanceSearchTemplate = this.TscAdvanceSearchTemplate.Text;
            modelInfo.CommentManageTemplate = this.TscCommentManageTemplate.Text;
            modelInfo.AddInfoFilePath = this.TxtAddInfoFilePath.Text.Trim();
            modelInfo.ManageInfoFilePath = this.TxtManageInfoFilePath.Text.Trim();
            modelInfo.PreviewInfoFilePath = this.TxtPreviewInfoFilePath.Text.Trim();
            modelInfo.BatchInfoFilePath = this.TxtBatchInfoFilePath.Text.Trim();
            modelInfo.EnableCharge = DataConverter.CBoolean(this.RadioEnableCharge.SelectedValue);
            modelInfo.EnableSignIn = DataConverter.CBoolean(this.RadioEnableSignin.SelectedValue);
            modelInfo.EnbaleVote = DataConverter.CBoolean(this.RadVote.SelectedValue);
            modelInfo.ChargeTips = this.TxtModelChargeTips.Text.Trim();
            modelInfo.MaxPerUser = DataConverter.CLng(this.TxtMaxPerUser.Text);
            if (this.ViewState["action"].ToString() == "Add")
            {
                if (ModelManager.ModelNameExists(modelInfo.ModelName))
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在此模型名称！</li>");
                }
                else if (ModelManager.TableNameExists(modelInfo.TableName))
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在要创建的表名请返回换一个表名！</li>");
                }
                else if (ModelManager.Add(modelInfo, modelTemplateId))
                {
                    base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                    AdminPage.WriteSuccessMsg("<li>添加内容模型成功，并将模型的默认字段初始化！</li>", "ModelManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>添加内容模型操作失败！</li>");
                }
            }
            else
            {
                bool flag;
                modelInfo.ModelId = DataConverter.CLng(this.HdnModelId.Value);
                modelInfo.TableName = this.HdnTableName.Value;
                if (text == this.HdnModelName.Value)
                {
                    flag = false;
                }
                else
                {
                    flag = ModelManager.ModelNameExists(text);
                }
                if (!flag)
                {
                    if (ModelManager.Update(modelInfo))
                    {
                        base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                        AdminPage.WriteSuccessMsg("<li>修改内容模型成功！</li>", "ModelManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>修改内容模型操作失败！</li>");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在此内容模型名称！</li>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (DataRow row in FileSystemObject.GetDirectoryInfos(HttpContext.Current.Server.MapPath("~/Images/ModelIcon/"), FsoMethod.File).Rows)
            {
                this.DrpItemIcon.Items.Add(new ListItem(row["name"].ToString(), row["name"].ToString()));
            }
            this.DrpItemIcon.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);ChangeTxtItemIcon(this.value);");
            this.TxtItemIcon.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);");
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("Action", "Add");
                this.ViewState["action"] = str;
                if (str == "Modify")
                {
                    ModelInfo modelInfoById = ModelManager.GetModelInfoById(BasePage.RequestInt32("ModelID"));
                    if (modelInfoById.IsNull)
                    {
                        AdminPage.WriteErrMsg("<li>模型不存在！</li>");
                    }
                    this.TxtModelName.Text = modelInfoById.ModelName.ToString();
                    this.TxtDescription.Text = modelInfoById.Description;
                    this.TxtItemName.Text = modelInfoById.ItemName;
                    this.TxtItemUnit.Text = modelInfoById.ItemUnit;
                    string selectValue = string.IsNullOrEmpty(modelInfoById.ItemIcon) ? "Default.gif" : modelInfoById.ItemIcon;
                    this.ImgItemIcon.ImageUrl = "~/Images/ModelIcon/" + selectValue;
                    BasePage.SetSelectedIndexByValue(this.DrpItemIcon, selectValue);
                    this.TxtItemIcon.Text = modelInfoById.ItemIcon;
                    this.TxtTableName.Text = modelInfoById.TableName;
                    this.FileCTemplate.Text = modelInfoById.DefaultTemplateFile;
                    this.TscPrintTemplate.Text = modelInfoById.PrintTemplate;
                    this.TscSearchTemplate.Text = modelInfoById.SearchTemplate;
                    this.TscAdvanceSearchFormTemplate.Text = modelInfoById.AdvanceSearchFormTemplate;
                    this.TscAdvanceSearchTemplate.Text = modelInfoById.AdvanceSearchTemplate;
                    this.TscCommentManageTemplate.Text = modelInfoById.CommentManageTemplate;
                    this.TxtTableName.Enabled = false;
                    this.RadioIsCountHits.SelectedValue = modelInfoById.IsCountHits.ToString();
                    this.RadioDisabled.SelectedValue = modelInfoById.Disabled.ToString();
                    this.HdnModelId.Value = modelInfoById.ModelId.ToString();
                    this.HdnModelName.Value = modelInfoById.ModelName;
                    this.HdnTableName.Value = modelInfoById.TableName;
                    this.LblTablePrefix.Visible = false;
                    this.TxtAddInfoFilePath.Text = modelInfoById.AddInfoFilePath;
                    this.TxtManageInfoFilePath.Text = modelInfoById.ManageInfoFilePath;
                    this.TxtPreviewInfoFilePath.Text = modelInfoById.PreviewInfoFilePath;
                    this.TxtBatchInfoFilePath.Text = modelInfoById.BatchInfoFilePath;
                    this.TxtModelChargeTips.Text = modelInfoById.ChargeTips;
                    this.RadioEnableCharge.SelectedValue = modelInfoById.EnableCharge.ToString();
                    this.RadioEnableSignin.SelectedValue = modelInfoById.EnableSignIn.ToString();
                    this.RadVote.SelectedValue = modelInfoById.EnbaleVote.ToString();
                    this.trModelTemmpalteId.Visible = false;
                    this.TxtMaxPerUser.Text = modelInfoById.MaxPerUser.ToString();
                }
                else
                {
                    this.ShowFieldModelList();
                }
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.EnableCharge.Style.Add("display", "none");
                    this.EnableChargeTips.Style.Add("display", "none");
                }
            }
        }

        protected void ShowFieldModelList()
        {
            IList<ModelTemplatesInfo> list = ModelTemplate.GetModelTemplateInfoList(0, 0, ModelType.Content);
            this.DropModelTemplate.Items.Clear();
            ListItem item = new ListItem("空白内容模型模板", "0");
            this.DropModelTemplate.Items.Add(item);
            this.DropModelTemplate.AppendDataBoundItems = true;
            this.DropModelTemplate.DataSource = list;
            this.DropModelTemplate.DataTextField = "TemplateName";
            this.DropModelTemplate.DataValueField = "TemplateId";
            this.DropModelTemplate.DataBind();
        }
    }
}

