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
    using System.Text;
    using System.Web.UI.WebControls;
    using EasyOne.Model.AD;

    public partial class ADZoneExport : AdminPage
    {

        protected void BtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LstZoneID.Items.Count; i++)
            {
                this.LstZoneID.Items[i].Selected = true;
            }
        }

        protected void BtnunSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LstZoneID.Items.Count; i++)
            {
                this.LstZoneID.Items[i].Selected = false;
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TxtExportMdb.Text))
            {
                AdminPage.WriteErrMsg("<li>导出的数据库链接不能为空！</li>");
            }
            else
            {
                StringBuilder sb = new StringBuilder("");
                for (int i = 0; i < this.LstZoneID.Items.Count; i++)
                {
                    if (this.LstZoneID.Items[i].Selected)
                    {
                        StringHelper.AppendString(sb, this.LstZoneID.Items[i].Value);
                    }
                }
                if (sb.Length < 1)
                {
                    AdminPage.WriteErrMsg("请选择要导出的项目！");
                }
                else if (ADZone.ExportData(sb.ToString(), base.Server.MapPath(this.TxtExportMdb.Text), this.ChkFormatConn.Checked))
                {
                    AdminPage.WriteSuccessMsg("<li>已经成功将所选中的版位设置导出到指定的数据库中！</li>", "ADZoneManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>导出失败！</li>", "ADZoneManage.aspx");
                }
            }
        }

        public void ListDataBind(ListControl dropName)
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ListDataBind(this.LstZoneID);
            }
        }
    }
}

