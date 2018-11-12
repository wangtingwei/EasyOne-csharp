namespace EasyOne.WebSite.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class ServiceCenterInfomation : UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                XmlDocument document = new XmlDocument();
                document.Load(base.Server.MapPath("~/Config/Question.Config"));
                this.LblMessage.Text = document.SelectSingleNode("Config/PhoneNum").InnerText;
            }
        }
    }
}

