﻿namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public partial class NodesList : AdminPage, ICallbackEventHandler
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
            this.XLoadNodeTree.XmlSrc = "ContentSelectTreeXml.aspx?ModelId=" + DataConverter.CLng(base.Request["ModelId"]);
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            this.result = Nodes.ShowNodeNavigation(DataConverter.CLng(eventArgument));
        }
    }
}

