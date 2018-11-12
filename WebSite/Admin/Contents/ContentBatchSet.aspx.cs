namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ContentBatch : AdminPage
    {

        protected void EBtnBacthSet_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                int batchType = 0;
                StringBuilder sb = new StringBuilder();
                if (this.BatchItemType.Checked)
                {
                    sb.Append(this.TxtBatchItmeID.Text.Trim());
                    if (sb.Length < 1)
                    {
                        AdminPage.WriteErrMsg("请先选择要批量设置的项目ID！");
                    }
                }
                else
                {
                    foreach (ListItem item in this.LstNodes.Items)
                    {
                        if (item.Selected)
                        {
                            StringHelper.AppendString(sb, item.Value);
                        }
                    }
                    if (sb.Length <= 0)
                    {
                        AdminPage.WriteErrMsg("请先选择要批量设置的节点！");
                    }
                    batchType = 1;
                }
                if (!this.GetCheckItem().ContainsValue(true))
                {
                    AdminPage.WriteErrMsg("您没有构选右侧的多项框，请选择要指定的替换！");
                }
                if (ContentManage.BatchUpdate(this.GetCommonModelInfo(), sb.ToString(), this.GetCheckItem(), batchType))
                {
                    if (SiteConfig.SiteOption.EnablePointMoneyExp)
                    {
                        this.UpdatePurview(batchType, sb);
                        this.UpdateCharge(batchType, sb);
                    }
                    AdminPage.WriteSuccessMsg("批量设置成功！", "ContentManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("批量设置失败！");
                }
            }
        }

        private Dictionary<string, bool> GetCheckItem()
        {
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            dictionary.Add("EliteLevel", this.ChkEliteLevel.Checked);
            dictionary.Add("Priority", this.ChkPriority.Checked);
            dictionary.Add("Hits", this.ChkHits.Checked);
            dictionary.Add("DayHits", this.ChkDayHits.Checked);
            dictionary.Add("WeekHits", this.ChkWeekHits.Checked);
            dictionary.Add("MonthHits", this.ChkMonthHits.Checked);
            dictionary.Add("UpdateTime", this.ChkUpdateTime.Checked);
            dictionary.Add("TemplateFile", this.ChkTemplateFile.Checked);
            dictionary.Add("InfoPurview", this.ChkInfoPurview.Checked);
            dictionary.Add("InfoPoint", this.ChkInfoPoint.Checked);
            dictionary.Add("ChargeType", this.ChkChargeType.Checked);
            dictionary.Add("DividePercent", this.ChkDividePercent.Checked);
            return dictionary;
        }

        private CommonModelInfo GetCommonModelInfo()
        {
            CommonModelInfo info = new CommonModelInfo();
            info.EliteLevel = DataConverter.CLng(this.TxtEliteLevel.Text.Trim());
            info.Priority = DataConverter.CLng(this.TxtPriority.Text.Trim());
            info.Hits = DataConverter.CLng(this.TxtHits.Text.Trim());
            info.DayHits = DataConverter.CLng(this.TxtDayHits.Text.Trim());
            info.WeekHits = DataConverter.CLng(this.TxtWeekHits.Text.Trim());
            info.MonthHits = DataConverter.CLng(this.TxtMonthHits.Text.Trim());
            info.UpdateTime = this.DpkUpdateTime.Date;
            info.TemplateFile = this.FileCTemplate.Text;
            return info;
        }

        private void InitUserGroupCheckBoxList()
        {
            this.EChklUserGroupList.Items.Clear();
            this.EChklUserGroupList.DataSource = UserGroups.GetUserGroupList(0, 0);
            this.EChklUserGroupList.DataTextField = "GroupName";
            this.EChklUserGroupList.DataValueField = "GroupId";
            this.EChklUserGroupList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str = BasePage.RequestString("GeneralID");
                if (!string.IsNullOrEmpty(str))
                {
                    this.TxtBatchItmeID.Text = str;
                    this.BatchItemType.Checked = true;
                }
                else
                {
                    this.BatchNodeType.Checked = true;
                }
                IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
                if (nodeNameForContainerItems.Count < 1)
                {
                    ListItem item = new ListItem("无节点，请先添加节点", "0");
                    item.Enabled = false;
                }
                else
                {
                    this.LstNodes.DataSource = nodeNameForContainerItems;
                    this.LstNodes.DataBind();
                }
                StringBuilder builder = new StringBuilder();
                builder.Append("<script type=\"text/javascript\">");
                builder.Append("function SelectAll(){");
                builder.Append("for(var i=0;i<document.getElementById('");
                builder.Append(this.LstNodes.ClientID);
                builder.Append("').length;i++){");
                builder.Append("document.getElementById('");
                builder.Append(this.LstNodes.ClientID);
                builder.Append("').options[i].selected=true;}}");
                builder.Append("function UnSelectAll(){");
                builder.Append("for(var i=0;i<document.getElementById('");
                builder.Append(this.LstNodes.ClientID);
                builder.Append("').length;i++){");
                builder.Append("document.getElementById('");
                builder.Append(this.LstNodes.ClientID);
                builder.Append("').options[i].selected=false;}}");
                builder.Append("</script>");
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "Select", builder.ToString());
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.TabTitle1.Style.Add("display", "none");
                }
                this.InitUserGroupCheckBoxList();
            }
        }

        private void UpdateCharge(int batchType, StringBuilder itemId)
        {
            ContentChargeInfo contentChargeInfo = new ContentChargeInfo();
            contentChargeInfo.InfoPoint = DataConverter.CLng(this.TxtInfoPoint.Text);
            contentChargeInfo.ChargeType = this.ShowChargeType.ChargeType;
            contentChargeInfo.PitchTime = this.ShowChargeType.PitchTime;
            contentChargeInfo.ReadTimes = this.ShowChargeType.ReadTimes;
            contentChargeInfo.DividePercent = DataConverter.CLng(this.TxtDividePercent.Text);
            if (!EasyOne.Contents.ContentCharge.BatchUpdate(contentChargeInfo, itemId.ToString(), this.GetCheckItem(), batchType))
            {
                AdminPage.WriteErrMsg("批量设置内容收费失败！");
            }
        }

        private void UpdatePurview(int batchType, StringBuilder itemId)
        {
            if (this.GetCheckItem()["InfoPurview"])
            {
                ContentPermissionInfo contentPermissionInfo = new ContentPermissionInfo();
                contentPermissionInfo.PermissionType = DataConverter.CLng(this.RadlInfoPurview.SelectedValue);
                StringBuilder sb = new StringBuilder("");
                for (int i = 0; i < this.EChklUserGroupList.Items.Count; i++)
                {
                    if (this.EChklUserGroupList.Items[i].Selected)
                    {
                        StringHelper.AppendString(sb, this.EChklUserGroupList.Items[i].Value);
                    }
                }
                contentPermissionInfo.ArrGroupId = sb.ToString();
                if (!PermissionContent.BatchUpdate(contentPermissionInfo, itemId.ToString(), this.GetCheckItem(), batchType))
                {
                    AdminPage.WriteErrMsg("批量设置收费权限失败！");
                }
            }
        }
    }
}

