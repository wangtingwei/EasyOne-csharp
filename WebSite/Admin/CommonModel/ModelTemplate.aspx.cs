namespace EasyOne.WebSite.Admin.CommonModel
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ModelTemplate : AdminPage
    {

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                DataActionState unknown = DataActionState.Unknown;
                ModelTemplatesInfo modelTemplatesInfo = new ModelTemplatesInfo();
                string successMessage = "";
                string returnurl = "ModelTemplateManage.aspx";
                modelTemplatesInfo.TemplateName = this.TxtTemplateName.Text;
                modelTemplatesInfo.TemplateDescription = this.TxtTemplateDescription.Text;
                modelTemplatesInfo.IsEshop = (ModelType) BasePage.RequestInt32("ModelType");
                string str3 = this.ViewState["action"].ToString();
                if (str3 != null)
                {
                    if (!(str3 == "Add"))
                    {
                        if (str3 == "Modify")
                        {
                            modelTemplatesInfo.TemplateId = DataConverter.CLng(this.HdnModelId.Value);
                            unknown = EasyOne.CommonModel.ModelTemplate.Update(modelTemplatesInfo);
                            successMessage = "<li>修改模型模板操作成功！</li>";
                        }
                        else if (str3 == "AddModelToFieldTemplate")
                        {
                            ModelInfo modelInfoById = new ModelInfo();
                            modelInfoById = ModelManager.GetModelInfoById(DataConverter.CLng(this.HdnModelId.Value));
                            modelTemplatesInfo.Field = modelInfoById.Field;
                            unknown = EasyOne.CommonModel.ModelTemplate.Add(modelTemplatesInfo);
                            successMessage = "<li>保存到模型模板操作成功！</li>";
                            if (modelTemplatesInfo.IsEshop == ModelType.Content)
                            {
                                returnurl = "../Contents/ModelManage.aspx";
                            }
                            else
                            {
                                returnurl = "../Shop/ProductModelManage.aspx";
                            }
                        }
                    }
                    else
                    {
                        modelTemplatesInfo.Field = ModelManager.AddDefaultField();
                        unknown = EasyOne.CommonModel.ModelTemplate.Add(modelTemplatesInfo);
                        successMessage = "<li>添加模型模板操作成功！</li>";
                    }
                }
                switch (unknown)
                {
                    case DataActionState.Successed:
                        if (modelTemplatesInfo.IsEshop != ModelType.Shop)
                        {
                            returnurl = returnurl + "?ModelType=1";
                            break;
                        }
                        returnurl = returnurl + "?ModelType=2";
                        break;

                    case DataActionState.Exist:
                        AdminPage.WriteErrMsg("<li>该模型下已经存在此字段，请指定其它的字段！</li>");
                        return;

                    case DataActionState.Unknown:
                        AdminPage.WriteErrMsg("<li>模型模板操作失败！</li>");
                        return;

                    default:
                        return;
                }
                AdminPage.WriteSuccessMsg(successMessage, returnurl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("Action", "Add");
                this.ViewState["action"] = str;
                string str2 = str;
                if (str2 != null)
                {
                    if (!(str2 == "Add"))
                    {
                        if (!(str2 == "Modify"))
                        {
                            if (str2 == "AddModelToFieldTemplate")
                            {
                                this.LblTitle.Text = "将" + BasePage.RequestString("ModelName") + "模型保存为模型模板";
                                this.HdnModelId.Value = BasePage.RequestInt32("ModelID").ToString();
                            }
                            return;
                        }
                    }
                    else
                    {
                        this.LblTitle.Text = "添加模型模板";
                        return;
                    }
                    this.LblTitle.Text = "修改模型模板";
                    ModelTemplatesInfo infoById = EasyOne.CommonModel.ModelTemplate.GetInfoById(BasePage.RequestInt32("TemplateID"));
                    if (!infoById.IsNull)
                    {
                        this.TxtTemplateName.Text = infoById.TemplateName;
                        this.TxtTemplateDescription.Text = infoById.TemplateDescription;
                        this.HdnModelId.Value = BasePage.RequestInt32("TemplateID").ToString();
                    }
                }
            }
        }
    }
}

