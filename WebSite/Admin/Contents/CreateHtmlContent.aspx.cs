namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.StaticHtml;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CreateHtmlContent : AdminPage
    {

        protected void EBtnAll_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                HtmlContent content = new HtmlContent();
                content.CreateMethod = CreateContentType.CreateAll;
                content.NodeIdArray = this.GetCreateHtmlNodesList(this.LstNodes);
                content.CommonCreateHtml();
                BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
            }
        }

        protected void EBtnAppointId_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                HtmlContent content = new HtmlContent();
                content.CreateMethod = CreateContentType.CreateByGeneralId;
                content.NodeIdArray = this.GetCreateHtmlNodesList(this.LstNodes);
                content.ContentGeneralIdArray = this.TxtAppointId.Text;
                content.CommonCreateHtml();
                BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
            }
        }

        protected void EBtnBoundId_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                HtmlContent content = new HtmlContent();
                content.NodeIdArray = this.GetCreateHtmlNodesList(this.LstNodes);
                content.CreateMethod = CreateContentType.CreateBetweenId;
                content.ContentMinId = DataConverter.CLng(this.TxtBeginId.Text);
                content.ContentMaxId = DataConverter.CLng(this.TxtEndId.Text);
                content.CommonCreateHtml();
                BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
            }
        }

        protected void EBtnDate_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                HtmlContent content = new HtmlContent();
                content.CreateMethod = CreateContentType.CreateByUpdateTime;
                content.NodeIdArray = this.GetCreateHtmlNodesList(this.LstNodes);
                content.ContentBeginTime = DataConverter.CDate(this.DpkBeginDate.Text);
                content.ContentEndTime = DataConverter.CDate(this.DpkEndDate.Text);
                content.CommonCreateHtml();
                BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
            }
        }

        protected void EBtnNotCreate_Click(object sender, EventArgs e)
        {
            HtmlContent content = new HtmlContent();
            content.CreateMethod = CreateContentType.CreateByNotCreate;
            content.NodeIdArray = this.GetCreateHtmlNodesList(this.LstNodes);
            content.CommonCreateHtml();
            BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
        }

        protected void EBtnTopNew_Click(object sender, EventArgs e)
        {
            if (base.IsValid)
            {
                HtmlContent content = new HtmlContent();
                content.CreateMethod = CreateContentType.CreateLatest;
                content.NodeIdArray = this.GetCreateHtmlNodesList(this.LstNodes);
                content.LatestNumber = DataConverter.CLng(this.TxtTopNew.Text);
                content.CommonCreateHtml();
                BasePage.ResponseRedirect("CreateHtmlProgress.aspx?workId=" + content.CreateId);
            }
        }

        protected string GetCreateHtmlNodesList(ListControl listName)
        {
            string str = string.Empty;
            bool flag = DataConverter.CBoolean(this.RdlIsCreateChild.SelectedValue);
            for (int i = 0; i < listName.Items.Count; i++)
            {
                if (listName.Items[i].Selected)
                {
                    if (flag)
                    {
                        if (!str.Contains(listName.Items[i].Value))
                        {
                            NodeInfo cacheNodeById = Nodes.GetCacheNodeById(DataConverter.CLng(listName.Items[i].Value));
                            if (string.IsNullOrEmpty(str))
                            {
                                str = cacheNodeById.ArrChildId + "," + str + ",";
                            }
                            else
                            {
                                str = str.Replace(cacheNodeById.ArrChildId, "") + "," + cacheNodeById.ArrChildId;
                            }
                        }
                    }
                    else
                    {
                        str = listName.Items[i].Value + "," + str;
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < strArray.Length; j++)
                {
                    NodeInfo info2 = Nodes.GetCacheNodeById(DataConverter.CLng(strArray[j]));
                    if (info2.IsCreateContentPage && (info2.PurviewType == 0))
                    {
                        StringHelper.AppendString(sb, strArray[j], ",");
                    }
                }
            }
            return sb.ToString();
        }

        protected void ListDataBind(ListControl listName)
        {
            int num = 0;
            IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
            listName.Items.Clear();
            foreach (NodeInfo info in nodeNameForContainerItems)
            {
                ListItem item = new ListItem();
                item.Text = info.NodeName;
                item.Value = info.NodeId.ToString();
                if ((info.PurviewType > 0) || !info.IsCreateContentPage)
                {
                    item.Enabled = false;
                    num++;
                }
                else
                {
                    num = 0;
                }
                listName.Items.Add(item);
            }
            if (nodeNameForContainerItems.Count == num)
            {
                this.EBtnAll.Enabled = false;
                this.EBtnAppointId.Enabled = false;
                this.EBtnBoundId.Enabled = false;
                this.EBtnDate.Enabled = false;
                this.EBtnNotCreate.Enabled = false;
                this.EBtnTopNew.Enabled = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                RolePermissions.BusinessAccessCheck(OperateCode.CreateHtmlManage);
                this.ListDataBind(this.LstNodes);
            }
        }
    }
}

