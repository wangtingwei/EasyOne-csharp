namespace EasyOne.WebSite.Controls
{
    using EasyOne.AccessManage;
    using EasyOne.Enumerations;
    using EasyOne.StaticHtml;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using System.Web;

    public partial class CreateHtmlProgress : BaseUserControl
    {
        protected string m_UrlReferrer = "";
        private string path = HttpContext.Current.Server.MapPath("~/Config/CreateHtmlWork.config");
        private XmlDocument xmlDoc = new XmlDocument();
        private XmlNodeList nodeList;
        protected void BtnStopCreate_Click(object sender, EventArgs e)
        {
            foreach (XmlNode node in this.nodeList)
            {
                string name = node.Attributes[0].Value;
                if (base.Application[name] != null)
                {
                    HtmlAbstract @abstract = base.Application[name] as HtmlAbstract;
                    @abstract.CreateThread.Abort();
                    base.Application.Remove(name);
                }
                if (string.Compare(node.Attributes[0].Value, name, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.xmlDoc.SelectSingleNode("CreateWork").RemoveChild(node);
                }
            }
            this.BtnStopCreate.Visible = false;
            this.xmlDoc.Save(this.path);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_UrlReferrer = base.Request.UrlReferrer.ToString();
            this.nodeList = this.xmlDoc.SelectNodes("CreateWork/WorkId");
            this.xmlDoc.Load(this.path);
            RolePermissions.BusinessAccessCheck(OperateCode.CreateHtmlManage);
            this.HdnWorkId.Value = BaseUserControl.RequestString("WorkId");
            if (this.nodeList.Count > 0)
            {
                this.BtnStopCreate.Visible = true;
            }
            else
            {
                this.BtnStopCreate.Visible = false;
            }
        }
    }
}

