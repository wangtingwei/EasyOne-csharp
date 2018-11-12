namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.WebControls;

    public partial class ChoicesetGuide : AdminPage
    {

        private DataTable GetTableName()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TableName");
            table.Columns.Add("Title");
            DataRow row = table.NewRow();
            row["TableName"] = "客户信息表";
            row["Title"] = "PE_Client";
            table.Rows.Add(row);
            row = table.NewRow();
            row["TableName"] = "公司信息表";
            row["Title"] = "PE_Company";
            table.Rows.Add(row);
            row = table.NewRow();
            row["TableName"] = "联系人信息表";
            row["Title"] = "PE_Contacter";
            table.Rows.Add(row);
            row = table.NewRow();
            row["TableName"] = "作者类型表";
            row["Title"] = "PE_Author";
            table.Rows.Add(row);
            if (SiteConfig.SiteInfo.ProductEdition.CompareTo("eShop") == 0)
            {
                row = table.NewRow();
                row["TableName"] = "服务记录表";
                row["Title"] = "PE_ServiceItem";
                table.Rows.Add(row);
                row = table.NewRow();
                row["TableName"] = "投诉记录表";
                row["Title"] = "PE_ComplainItem";
                table.Rows.Add(row);
                row = table.NewRow();
                row["TableName"] = "订单信息表";
                row["Title"] = "PE_Orders";
                table.Rows.Add(row);
            }
            return table;
        }

        private DataTable GetTitle(string tableName)
        {
            IList<ChoicesetInfo> choicesetList = Choiceset.GetChoicesetList();
            DataTable table = new DataTable();
            table.Columns.Add("TableName");
            table.Columns.Add("Title");
            table.Columns.Add("FieldName");
            foreach (ChoicesetInfo info in choicesetList)
            {
                if (info.TableName == tableName)
                {
                    DataRow row = table.NewRow();
                    row["TableName"] = info.TableName;
                    row["Title"] = info.Title;
                    row["FieldName"] = info.FieldName;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RptMenu.DataSource = this.GetTableName();
            this.RptMenu.DataBind();
        }

        protected void RptChoicesetTitle_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dataItem = (DataRowView) e.Item.DataItem;
                HyperLink link = (HyperLink) e.Item.FindControl("LnkTitle");
                link.Text = dataItem["Title"].ToString();
                link.NavigateUrl = "~/" + SiteConfig.SiteOption.ManageDir + "/Configuration/Choiceset.aspx?TableName=" + dataItem["TableName"].ToString() + "&FieldName=" + dataItem["FieldName"].ToString();
                link.Target = "main_right";
            }
        }

        protected void RptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dataItem = (DataRowView) e.Item.DataItem;
                Label label = (Label) e.Item.FindControl("LblTableName");
                label.Text = dataItem["TableName"].ToString();
                Repeater repeater = (Repeater) e.Item.FindControl("RptChoicesetTitle");
                repeater.DataSource = this.GetTitle(dataItem["Title"].ToString());
                repeater.DataBind();
            }
        }
    }
}

