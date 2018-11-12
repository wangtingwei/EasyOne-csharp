namespace EasyOne.WebSite
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public partial class Vote : TemplatePage
    {
        private int generalId;

        private void GetItems(VoteInfo voteInfo)
        {
            string voteItem = voteInfo.VoteItem;
            int num = 0;
            Serialize<VoteItemInfo> serialize = new Serialize<VoteItemInfo>();
            IList<VoteItemInfo> list = serialize.DeserializeFieldList(voteItem);
            foreach (string str2 in base.Request.Form.Keys)
            {
                if (str2.Contains("VoteOption"))
                {
                    if (voteInfo.ItemType == 0)
                    {
                        foreach (VoteItemInfo info in list)
                        {
                            if (string.Compare(base.Request.Form[str2], info.Title, StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                info.VoteNumber++;
                                num = info.VoteNumber + num;
                            }
                        }
                    }
                    else
                    {
                        string[] strArray = base.Request.Form["VoteOption"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (VoteItemInfo info2 in list)
                        {
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                if (string.Compare(strArray[i], info2.Title, StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    info2.VoteNumber++;
                                    num = info2.VoteNumber + num;
                                }
                            }
                        }
                    }
                }
            }
            voteItem = serialize.SerializeFieldList(list);
            voteInfo.VoteTotalNumber = num;
            voteInfo.VoteItem = voteItem;
        }

        public override void OnInitTemplateInfo(EventArgs e)
        {
            string dynamicConfigTemplatePath = TemplatePage.GetDynamicConfigTemplatePath(Path.GetFileNameWithoutExtension(this.Page.Request.FilePath));
            if (!string.IsNullOrEmpty(dynamicConfigTemplatePath))
            {
                TemplateInfo info = new TemplateInfo();
                info.QueryList = base.Request.QueryString;
                info.PageName = TemplatePage.RebuildPageName(base.Request.Url.LocalPath, base.Request.QueryString);
                info.TemplateContent = Template.GetTemplateContent(dynamicConfigTemplatePath);
                info.RootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                info.CurrentPage = DataConverter.CLng(base.Request.QueryString["page"], 1);
                info.PageType = 1;
                base.TemplateInfo = info;
            }
            else
            {
                TemplatePage.WriteErrMsg("您未设置投票模板！", SiteConfig.SiteInfo.VirtualPath + "Default.aspx");
            }
        }

        public override void OnInitTemplatePage(EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.generalId = DataConverter.CLng(base.Request.Form["GeneralId"]);
                VoteInfo voteInfoByGeneralId = Votes.GetVoteInfoByGeneralId(this.generalId);
                if (!voteInfoByGeneralId.IsNull)
                {
                    this.GetItems(voteInfoByGeneralId);
                    Votes.Update(voteInfoByGeneralId);
                    BasePage.ResponseRedirect(SiteConfig.SiteInfo.VirtualPath + "Common/Vote.aspx?GeneralId=" + this.generalId);
                }
            }
        }
    }
}

