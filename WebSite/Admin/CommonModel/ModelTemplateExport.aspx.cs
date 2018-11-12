namespace EasyOne.WebSite.Admin.CommonModel
{
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.CommonModel;

    public partial class ModelTemplateExport : AdminPage
    {

        protected void BtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LstTemplateID.Items.Count; i++)
            {
                this.LstTemplateID.Items[i].Selected = true;
            }
        }

        protected void BtnunSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LstTemplateID.Items.Count; i++)
            {
                this.LstTemplateID.Items[i].Selected = false;
            }
        }

        public void ListDataBind(ListControl dropName)
        {
            IList<ModelTemplatesInfo> list = EasyOne.CommonModel.ModelTemplate.GetModelTemplateInfoList(0, 0, (ModelType) BasePage.RequestInt32("ModelType"));
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ListDataBind(this.LstTemplateID);
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TxtExportMdb.Text))
            {
                AdminPage.WriteErrMsg("<li>导出的数据库链接不能为空！</li>");
            }
            else
            {
                StringBuilder builder = new StringBuilder("");
                for (int i = 0; i < this.LstTemplateID.Items.Count; i++)
                {
                    if (this.LstTemplateID.Items[i].Selected)
                    {
                        builder.Append(this.LstTemplateID.Items[i].Value);
                        builder.Append(",");
                    }
                }
                if (builder.Length < 1)
                {
                    AdminPage.WriteErrMsg("请选择要导出的项目！");
                }
                else
                {
                    builder.Remove(builder.Length - 1, 1);
                    if (EasyOne.CommonModel.ModelTemplate.ExportData(builder.ToString(), base.Server.MapPath(this.TxtExportMdb.Text), this.ChkFormatConn.Checked))
                    {
                        AdminPage.WriteSuccessMsg("<li>已经成功将所选中的版位设置导出到指定的数据库中！</li>", "ModelTemplateManage.aspx?ModelType=" + BasePage.RequestInt32("ModelType").ToString());
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>导出项目失败，请检查导出所在的文件夹是否是只读，是否给写入权限或查看导出数据库是否损坏！</li>");
                    }
                }
            }
        }
    }
}

