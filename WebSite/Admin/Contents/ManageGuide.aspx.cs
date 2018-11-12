namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.CommonModel;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using EasyOne.Model;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using EasyOne.Model.CommonModel;

    public partial class ManageGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            IList<ModelInfo> list = ModelManager.ContentModelList(ModelShowType.Enable);
            this.RptModelList.DataSource = list;
            this.RptModelList.DataBind();
        }
    }
}

