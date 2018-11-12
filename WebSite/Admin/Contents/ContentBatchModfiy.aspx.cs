namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ContentBatchModfiy : AdminPage
    {

        protected void BtnBatch_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form[this.ListModelField.UniqueID];
            int modelId = DataConverter.CLng(this.DropModel.SelectedValue);
            string nodeIds = base.Request.Form[this.ListNode.UniqueID];
            string oldValue = this.TxtTargetContent.Text.Trim();
            string text = this.TxtNewContent.Text;
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("请选择要替换的字段！", "ContentBatchModfiy.aspx");
            }
            int startIndex = 0;
            int pageSize = 30;
            int num4 = ContentManage.GetTotalOfCommonModelInfo(0, ContentSortType.DayHitsAsc, 0);
            if ((num4 % pageSize) == 0)
            {
                int num1 = num4 / pageSize;
            }
            else
            {
                int num7 = num4 / pageSize;
            }
            for (int i = 0; i < num4; i++)
            {
                startIndex = pageSize * i;
                DataSet ds = ContentManage.GetContentList(modelId, nodeIds, startIndex, pageSize);
                string[] strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (string str5 in strArray)
                    {
                        string str6 = row[str5].ToString().Replace(oldValue, text);
                        row[str5] = str6;
                    }
                }
                ContentManage.UpdateDataSet(ds, modelId);
            }
            AdminPage.WriteSuccessMsg("替换成功！", "ContentManage.aspx");
        }

        private void InitControl()
        {
            this.ListNode.DataSource = EasyOne.Contents.Nodes.GetNodesList(NodeType.Container);
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

