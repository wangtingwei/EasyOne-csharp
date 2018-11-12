namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.AD;

    public partial class ADZoneImport : AdminPage
    {

        protected void BtnImport_Click(object sender, EventArgs e)
        {
            this.SaveImportData();
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

        public void ImportDataBind(ListControl dropName, string importDatabase)
        {
            IList<ADZoneInfo> importList = ADZone.GetImportList(importDatabase);
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int num = BasePage.RequestInt32("Step");
                if (num == 1)
                {
                    this.WzdAd.ActiveStepIndex = num;
                    string importPath = base.Request.QueryString["ImportPath"];
                    this.DataBasePath(importPath);
                    this.ImportDataBind(this.LstImportZoneId, base.Server.MapPath(importPath));
                    this.SystemDataBind(this.LstSystemZoneId);
                }
            }
        }

        private void SaveImportData()
        {
            string selectedValue = this.LstImportZoneId.SelectedValue;
            string text = this.TxtImportMdb.Text;
            if (string.IsNullOrEmpty(selectedValue))
            {
                AdminPage.WriteErrMsg("<li>请选择要导入的广告版位列表！</li>");
            }
            if (string.IsNullOrEmpty(text))
            {
                AdminPage.WriteErrMsg("<li>导入的数据库链接不能为空！</li>");
            }
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < this.LstImportZoneId.Items.Count; i++)
            {
                if (this.LstImportZoneId.Items[i].Selected)
                {
                    StringHelper.AppendString(sb, this.LstImportZoneId.Items[i].Value);
                }
            }
            if (sb.Length < 1)
            {
                AdminPage.WriteErrMsg("请选择要导入的项目！");
            }
            else if (ADZone.ImportData(sb.ToString(), base.Server.MapPath(text)))
            {
                BasePage.ResponseRedirect("ADZoneImport.aspx?Step=1&ImportPath=" + base.Server.HtmlEncode(this.TxtImportMdb.Text) + "&ModelType=" + BasePage.RequestInt32("ModelType").ToString());
            }
        }

        public void SystemDataBind(ListControl dropName)
        {
            IList<ADZoneInfo> aDZoneList = ADZone.GetADZoneList(0, 0);
            if (aDZoneList.Count > 0)
            {
                dropName.Items.Clear();
                dropName.DataSource = aDZoneList;
                dropName.DataBind();
            }
            else
            {
                dropName.Items.Clear();
            }
        }

        protected void WzdAd_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            BasePage.ResponseRedirect("ADZoneManage.aspx");
        }

        protected void WzdAd_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            string text = this.TxtImportMdb.Text;
            if (string.IsNullOrEmpty(text))
            {
                AdminPage.WriteErrMsg("<li>请填写导入数据库名！</li>", "ADZoneImport.aspx");
            }
            this.DataBasePath(text);
            this.ImportDataBind(this.LstImportZoneId, base.Server.MapPath(text));
            this.SystemDataBind(this.LstSystemZoneId);
        }
    }
}

