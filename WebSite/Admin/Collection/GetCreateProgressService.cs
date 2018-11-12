namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.Collection;
    using EasyOne.StaticHtml;
    using System;
    using System.ComponentModel;
    using System.Web.Script.Services;
    using System.Web.Services;

    [ToolboxItem(false), ScriptService, WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1), WebService(Namespace="http://tempuri.org/")]
    public class GetCreateProgressService : WebService
    {
        [WebMethod]
        public ProgressInfo AcquireProgress()
        {
            ProgressInfo info = new ProgressInfo();
            string nodeValue = XmlManage.Instance("Config/CreateCollectionWork.config", XmlType.File).GetNodeValue("CollectionWork/WorkId");
            if (base.Application[nodeValue] == null)
            {
                return null;
            }
            CollectionProgress progress = (CollectionProgress) base.Application[nodeValue];
            info.IsInput = progress.IsInput;
            info.Message = progress.CollectionMessage;
            info.CollectionEnd = false;
            info.ErrorMessage = progress.ErrorInfo;
            info.IsCreateHtml = progress.IsCreateHtml;
            info.ItemId = progress.ItemId;
            if (!string.IsNullOrEmpty(info.ErrorMessage))
            {
                info.CollectionEnd = true;
                base.Application.Remove(nodeValue);
                return info;
            }
            if (progress.IsInput)
            {
                if (progress.CollectionCompleted == progress.CollectionCount)
                {
                    info.Completed = progress.CollectionCompleted;
                    info.ExecutionTime = ((TimeSpan) (DateTime.Now - progress.CollectionStartTime)).ToString();
                    info.Id = progress.CreateId;
                    info.Count = progress.CollectionCount;
                    info.Completed = progress.CollectionCompleted;
                    info.Progress = "100%";
                    if (progress.ItemCompleted == progress.ItemCount)
                    {
                        info.CollectionEnd = true;
                        base.Application.Remove(nodeValue);
                    }
                    if (info.IsCreateHtml)
                    {
                        string nodeIds = "";
                        string itemId = info.ItemId;
                        if (!string.IsNullOrEmpty(itemId))
                        {
                            if (itemId.Contains(","))
                            {
                                foreach (string str4 in itemId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    nodeIds = GetNodeIs(nodeIds, str4);
                                }
                            }
                            else
                            {
                                nodeIds = GetNodeIs(nodeIds, itemId);
                            }
                        }
                        info.Message = "请稍后，准备生成采集文章...";
                        HtmlContent content = new HtmlContent();
                        content.CreateMethod = CreateContentType.CreateByNotCreate;
                        content.NodeIdArray = nodeIds;
                        info.CreateWorkId = Guid.NewGuid().ToString();
                        content.CommonCreateHtml();
                    }
                    return info;
                }
                info.Completed = progress.CollectionCompleted;
                info.Id = progress.CreateId;
                info.Count = progress.CollectionCount;
                info.ExecutionTime = "0:00:00";
                info.Progress = "0";
                if (progress.CollectionCompleted != 0)
                {
                    info.Progress = (((double) progress.CollectionCompleted) / ((double) progress.CollectionCount)).ToString("p");
                    TimeSpan span2 = (TimeSpan) (DateTime.Now - progress.CollectionStartTime);
                    info.ExecutionTime = span2.Hours.ToString() + ":" + span2.Minutes.ToString() + ":" + span2.Seconds.ToString();
                }
            }
            return info;
        }

        private static string GetNodeIs(string nodeIds, string itemIds)
        {
            CollectionItemInfo infoById = CollectionItem.GetInfoById(DataConverter.CLng(itemIds));
            if (!infoById.IsNull)
            {
                nodeIds = nodeIds + infoById.NodeId.ToString() + "," + infoById.InfoNodeId;
            }
            return nodeIds;
        }
    }
}

