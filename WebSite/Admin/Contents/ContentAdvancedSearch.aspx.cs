namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using EasyOne.Model.Contents;

    public partial class ContentAdvancedSearch : AdminPage
    {
        private string AddFieldValue(string fieldName, IList<FieldInfo> fieldList)
        {
            string str = "";
            foreach (FieldInfo info in fieldList)
            {
                if (fieldName.CompareTo(info.FieldName) != 0)
                {
                    continue;
                }
                switch (info.FieldType)
                {
                    case FieldType.NumberType:
                    {
                        str = DataConverter.CSingle(base.Request.Form[fieldName]).ToString();
                        continue;
                    }
                    case FieldType.MoneyType:
                    {
                        str = DataConverter.CDecimal(base.Request.Form[fieldName]).ToString();
                        continue;
                    }
                    case FieldType.DateTimeType:
                    {
                        str = DataConverter.CDate(base.Request.Form[fieldName]).ToString();
                        continue;
                    }
                    case FieldType.BoolType:
                    {
                        str = DataConverter.CBoolean(base.Request.Form[fieldName]).ToString();
                        continue;
                    }
                }
                str = DataSecurity.FilterBadChar(base.Request.Form[fieldName]);
            }
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("<li>" + fieldName + "字段没有填写查询值！</li>");
            }
            return str;
        }

        protected void BtnAdvancedSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int modelId = DataConverter.CLng(this.DropModel.SelectedValue);
            string str = this.HdnSearchValue.Value;
            StringBuilder builder2 = new StringBuilder();
            for (int i = 0; i < this.ListNode.Items.Count; i++)
            {
                if (this.ListNode.Items[i].Selected)
                {
                    StringHelper.AppendString(sb, this.ListNode.Items[i].Value);
                }
            }
            if (string.IsNullOrEmpty(sb.ToString()))
            {
                AdminPage.WriteErrMsg("<li>没有选择要查询的栏目</li>");
            }
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("没有选择要查询的字段");
            }
            IList<FieldInfo> fieldList = Field.GetFieldList(modelId);
            if (str.IndexOf(',') != -1)
            {
                foreach (string str2 in str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    StringHelper.AppendString(builder2, this.AddFieldValue(str2, fieldList));
                }
            }
            else
            {
                builder2.Append(this.AddFieldValue(str, fieldList));
            }
            this.Session["SearchFieldName"] = str;
            this.Session["SearchFieldValue"] = builder2.ToString();
            BasePage.ResponseRedirect("ContentAdvancedSearchManage.aspx?NodeId=" + sb.ToString() + "&ModelId=" + modelId.ToString());
        }

        private void InitControl()
        {
            IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
            this.ListNode.DataSource = nodeNameForContainerItems;
            this.ListNode.DataTextField = "NodeName";
            this.ListNode.DataValueField = "NodeId";
            this.ListNode.DataBind();
            this.DropModel.DataSource = ModelManager.GetModelList(ModelType.Content, ModelShowType.Enable);
            this.DropModel.DataTextField = "ModelName";
            this.DropModel.DataValueField = "ModelId";
            this.DropModel.DataBind();
            this.DropModel.Attributes.Add("onchange", "GetServices(this.options[this.options.selectedIndex].value)");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitControl();
            }
        }
    }
}

