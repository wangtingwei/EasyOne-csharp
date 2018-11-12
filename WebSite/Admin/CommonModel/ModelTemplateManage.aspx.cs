namespace EasyOne.WebSite.Admin.CommonModel
{
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ModelTemplateManage : AdminPage
    {

        protected void EBtnDelete_Click(object sender, EventArgs e)
        {
            if (EasyOne.CommonModel.ModelTemplate.Delete(this.EgvModelTemplate.SelectList.ToString()))
            {
                AdminPage.WriteSuccessMsg("<li>删除指定的模型模板成功！</li>", "ModelTemplateManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString());
            }
            else
            {
                AdminPage.WriteErrMsg("<li>删除失败！</li>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.OdsModelTemplate.SelectParameters["type"].DefaultValue = BasePage.RequestInt32("ModelType").ToString();
            if (!this.Page.IsPostBack)
            {
                string str;
                int num = BasePage.RequestInt32("TemplateID");
                if (((str = BasePage.RequestString("Action")) != null) && (str == "Delete"))
                {
                    if (EasyOne.CommonModel.ModelTemplate.Delete(num.ToString()))
                    {
                        this.EgvModelTemplate.DataBind();
                        AdminPage.WriteSuccessMsg("<li>删除指定的模型模板成功！</li>", "ModelTemplateManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString());
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>删除指定的模型模板失败！</li>");
                    }
                }
            }
        }
    }
}

