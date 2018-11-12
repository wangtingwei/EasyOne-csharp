namespace EasyOne.Contents
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Contents;
    using EasyOne.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class Votes
    {
        private static readonly IVotes dal = DataAccess.CreateVote();

        private Votes()
        {
        }

        public static void Add(VoteInfo voteInfo)
        {
            voteInfo.VoteItem = voteInfo.VoteItem.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            dal.Add(voteInfo);
        }

        public static void Delete(string generalId)
        {
            if (!DataValidator.IsValidId(generalId))
            {
                throw new ArgumentException("错误的参数！");
            }
            dal.Delete(generalId);
        }

        public static string GetFormByGeneralId(int generalId)
        {
            VoteInfo voteInfoByGeneralId = GetVoteInfoByGeneralId(generalId);
            if (voteInfoByGeneralId.IsNull || !voteInfoByGeneralId.IsAlive)
            {
                return string.Empty;
            }
            if ((voteInfoByGeneralId.StartTime > DateTime.Now) || (voteInfoByGeneralId.EndTime < DateTime.Now))
            {
                return string.Empty;
            }
            string str = "type=\"radio\"";
            if (voteInfoByGeneralId.ItemType == 1)
            {
                str = "type=\"checkbox\"";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<form method=\"post\" name=\"VoteForm\" id=\"VoteForm\" action=\"" + SiteConfig.SiteInfo.VirtualPath + "Common/vote.aspx\"> ");
            builder.Append("<table><tr><td><h4>您对<font color=red>");
            builder.Append(voteInfoByGeneralId.VoteTitle);
            builder.Append("</font>的看法是</h4></td></tr>");
            IList<VoteItemInfo> list = new Serialize<VoteItemInfo>().DeserializeFieldList(voteInfoByGeneralId.VoteItem);
            builder.Append("<tr><td>");
            int num = 0;
            foreach (VoteItemInfo info2 in list)
            {
                num++;
                if (!string.IsNullOrEmpty(info2.Title))
                {
                    builder.Append("<input " + str + " name=\"VoteOption\" id=\"VoteOption" + num.ToString() + "\" value=\"" + info2.Title + "\" style=\"border:0\"/> ");
                    builder.Append(info2.Title);
                    builder.Append("<br/>");
                }
            }
            builder.Append("</td></tr>");
            builder.Append("<input type=\"hidden\" name=\"generalId\" id=\"generalId\" value=\"" + generalId.ToString() + "\" />");
            builder.Append("<tr><td align=\"center\">");
            builder.Append("<input type=\"button\" value=\"投票\" name=\"btnVote\" onclick=\"SubmitVote()\" id=\"btnVote\"/>&nbsp;&nbsp;<input type=\"button\" value=\"查看\" name=\"ShowVote\" onclick=\"ShowVoteResult();\" id=\"ShowVote\"/>");
            builder.Append("</td></tr>");
            builder.Append("</table></form>");
            builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
            builder.Append("function SubmitVote(){");
            builder.Append("document.VoteForm.submit();");
            builder.Append("}\r\n");
            builder.Append("function ShowVoteResult(){");
            builder.Append(string.Concat(new object[] { "window.location=\"", SiteConfig.SiteInfo.VirtualPath, "Common/Vote.aspx?GeneralId=", generalId, "\";" }));
            builder.Append("}");
            builder.Append("</script>");
            return builder.ToString();
        }

        public static VoteInfo GetVoteInfoByGeneralId(int generalId)
        {
            VoteInfo voteInfoByGeneralId = dal.GetVoteInfoByGeneralId(generalId);
            if (!voteInfoByGeneralId.IsNull)
            {
                voteInfoByGeneralId.VoteItem = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + voteInfoByGeneralId.VoteItem;
            }
            return voteInfoByGeneralId;
        }

        public static void Update(VoteInfo voteInfo)
        {
            voteInfo.VoteItem = voteInfo.VoteItem.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            dal.Update(voteInfo);
        }
    }
}

