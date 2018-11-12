namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public partial class SpecialList : AdminPage, ICallbackEventHandler
    {
        protected string result;

        public string GetCallbackResult()
        {
            return this.result;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
            StringBuilder builder = new StringBuilder();
            builder.Append("function CallTheServer(arg,context){ " + str + "} ;");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "abcdefg", builder.ToString(), true);
            this.XLoadNodeTree.XmlSrc = "SpecialSelectTreeXml.aspx";
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            SpecialInfo specialInfoById = Special.GetSpecialInfoById(DataConverter.CLng(eventArgument));
            SpecialCategoryInfo specialCategoryInfoById = Special.GetSpecialCategoryInfoById(specialInfoById.SpecialCategoryId);
            this.result = specialCategoryInfoById.SpecialCategoryName + ">>" + specialInfoById.SpecialName;
        }
    }
}

