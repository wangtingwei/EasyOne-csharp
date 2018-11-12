namespace EasyOne.WebSite.Admin.CommonModel
{
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.CommonModel;

    public partial class ModelTemplateImport : AdminPage
    {

        protected void BtnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.LstImportTemplateId.SelectedValue))
            {
                AdminPage.WriteErrMsg("<li>请选择要导入的模型模板列表！</li>");
            }
            if (string.IsNullOrEmpty(this.TxtModelTemplateMdb.Text))
            {
                AdminPage.WriteErrMsg("<li>导入的数据库链接不能为空！</li>");
            }
            else
            {
                StringBuilder builder = new StringBuilder("");
                for (int i = 0; i < this.LstImportTemplateId.Items.Count; i++)
                {
                    if (this.LstImportTemplateId.Items[i].Selected)
                    {
                        builder.Append(this.LstImportTemplateId.Items[i].Value);
                        builder.Append(",");
                    }
                }
                if (builder.Length < 1)
                {
                    AdminPage.WriteErrMsg("<li>请选择要导入的项目！</li>");
                }
                else
                {
                    builder.Remove(builder.Length - 1, 1);
                    if (EasyOne.CommonModel.ModelTemplate.ImportData(builder.ToString(), base.Server.MapPath(this.TxtModelTemplateMdb.Text)))
                    {
                        BasePage.ResponseRedirect("ModelTemplateImport.aspx?Step=1&ImportPath=" + base.Server.HtmlEncode(this.TxtModelTemplateMdb.Text) + "&ModelType=" + BasePage.RequestInt32("ModelType").ToString());
                    }
                }
            }
        }

        private void DataBasePath(string importPath)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + base.Server.MapPath(importPath));
            try
            {
                connection.Open();
            }
            catch (OleDbException exception)
            {
                AdminPage.WriteErrMsg("<li>数据库操作失败，请以后再试，错误原因：" + exception.Message + "</li>", "ModelTemplateImport.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString());
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        protected void ImportModelTemplateDataBind(ListControl dropName, string importPath)
        {
            IList<ModelTemplatesInfo> importList = EasyOne.CommonModel.ModelTemplate.GetImportList(importPath, (ModelType) BasePage.RequestInt32("ModelType"));
            if (importList.Count > 0)
            {
                dropName.Items.Clear();
                dropName.DataSource = importList;
                dropName.DataBind();
            }
            else
            {
                dropName.Items.Clear();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int num = BasePage.RequestInt32("Step");
                if (num == 1)
                {
                    this.WzdSkin.ActiveStepIndex = num;
                    string importPath = base.Request.QueryString["ImportPath"];
                    this.DataBasePath(importPath);
                    this.ImportModelTemplateDataBind(this.LstImportTemplateId, base.Server.MapPath(importPath));
                    this.SystemModelTemplateDataBind(this.LstSystemTemplateId);
                }
            }
        }

        public void SystemModelTemplateDataBind(ListControl dropName)
        {
            IList<ModelTemplatesInfo> list = EasyOne.CommonModel.ModelTemplate.GetModelTemplateInfoList(0, 100, (ModelType) BasePage.RequestInt32("ModelType"));
            if (list.Count > 0)
            {
                dropName.Items.Clear();
                dropName.DataSource = list;
                dropName.DataBind();
            }
            else
            {
                dropName.Items.Clear();
            }
        }

        protected void WzdModelTemplate_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            BasePage.ResponseRedirect("ModelTemplateManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString());
        }

        protected void WzdModelTemplate_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            string text = this.TxtModelTemplateMdb.Text;
            if (string.IsNullOrEmpty(text))
            {
                AdminPage.WriteErrMsg("<li>请填写导入数据库名！</li>", "ModelTemplateImport.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString());
            }
            this.DataBasePath(text);
            this.ImportModelTemplateDataBind(this.LstImportTemplateId, base.Server.MapPath(text));
            this.SystemModelTemplateDataBind(this.LstSystemTemplateId);
        }
    }
}

