namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.StaticHtml;
    using System;
    using System.ComponentModel;
    using System.Web;
    using System.Web.Script.Services;
    using System.Web.Services;
    using System.Xml;

    [WebService(Namespace="http://tempuri.org/"), ToolboxItem(false), ScriptService, WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1)]
    public class GetCreateProgressService : WebService
    {
        [WebMethod]
        public ProgressInfo AcquireProgress(string workId)
        {
            ProgressInfo progressInfo = new ProgressInfo();
            this.GetProgress(workId, progressInfo);
            return progressInfo;
        }

        private void GetHtmlAbstract(string workId, ProgressInfo progressInfo)
        {
            HtmlAbstract @abstract = base.Application[workId] as HtmlAbstract;
            if (@abstract != null)
            {
                if (@abstract.CreateCompleted == @abstract.CreateCount)
                {
                    progressInfo.Completed = @abstract.CreateCompleted;
                    progressInfo.Message = "总共需要生成 <font color='blue'><b>" + @abstract.CreateCount.ToString() + "</b></font>条信息<br/>" + @abstract.CreateMessage;
                    progressInfo.Progress = "100%";
                    TimeSpan span = (TimeSpan) (DateTime.Now - @abstract.CreateStartTime);
                    progressInfo.ExecutionTime = span.TotalSeconds.ToString() + "秒";
                    progressInfo.RemainingTime = "0秒";
                    progressInfo.Id = @abstract.CreateId;
                    progressInfo.Count = @abstract.CreateCount;
                    progressInfo.Completed = @abstract.CreateCompleted;
                    base.Application.Remove(workId);
                    XmlDocument document = new XmlDocument();
                    string filename = HttpContext.Current.Server.MapPath("~/Config/CreateHtmlWork.config");
                    document.Load(filename);
                    foreach (XmlNode node in document.SelectNodes("CreateWork/WorkId"))
                    {
                        if (string.Compare(node.Attributes[0].Value, workId, true) == 0)
                        {
                            document.SelectSingleNode("CreateWork").RemoveChild(node);
                        }
                    }
                    document.Save(filename);
                }
                else
                {
                    progressInfo.Completed = @abstract.CreateCompleted;
                    progressInfo.Message = "总共需要生成 <font color='blue'><b>" + @abstract.CreateCount.ToString() + "</b></font>条信息<br/>" + @abstract.CreateMessage;
                    progressInfo.Id = @abstract.CreateId;
                    progressInfo.Count = @abstract.CreateCount;
                    if ((@abstract.CreateCompleted >= 0) && (@abstract.CreateCompleted < @abstract.CreateCount))
                    {
                        progressInfo.Progress = (((double) @abstract.CreateCompleted) / ((double) @abstract.CreateCount)).ToString("p");
                        TimeSpan span2 = (TimeSpan) (DateTime.Now - @abstract.CreateStartTime);
                        progressInfo.ExecutionTime = span2.Hours.ToString() + "小时:" + span2.Minutes.ToString() + "分:" + span2.Seconds.ToString() + "秒";
                        progressInfo.RemainingTime = (((span2.TotalSeconds / ((double) (@abstract.CreateCompleted + 1))) * (@abstract.CreateCount - @abstract.CreateCompleted))).ToString() + "秒";
                    }
                    else if (@abstract.CreateCompleted == @abstract.CreateCount)
                    {
                        progressInfo.Progress = "100%";
                        TimeSpan span3 = (TimeSpan) (DateTime.Now - @abstract.CreateStartTime);
                        progressInfo.ExecutionTime = span3.Hours.ToString() + "小时:" + span3.Minutes.ToString() + "分:" + span3.Seconds.ToString() + "秒";
                        progressInfo.RemainingTime = "0秒";
                    }
                }
            }
        }

        private void GetProgress(string workId, ProgressInfo progressInfo)
        {
            if (string.IsNullOrEmpty(workId) || (base.Application[workId] == null))
            {
                string[] allKeys = base.Application.AllKeys;
                if (allKeys.Length > 0)
                {
                    this.GetHtmlAbstract(allKeys[0], progressInfo);
                }
                else
                {
                    progressInfo = null;
                }
            }
            else
            {
                this.GetHtmlAbstract(workId, progressInfo);
            }
        }
    }
}

