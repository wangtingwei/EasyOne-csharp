namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Web.UI;
    using EasyOne.WorkFlows;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.WorkFlow;

    public partial class WorkFlowsGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            IList<WorkFlowsInfo> workFlowsList = WorkFlow.GetWorkFlowsList();
            this.RptWorkFlowsList.DataSource = workFlowsList;
            this.RptWorkFlowsList.DataBind();
        }
    }
}

