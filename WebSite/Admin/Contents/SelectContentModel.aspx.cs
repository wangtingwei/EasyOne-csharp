namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.WebControls;
    using EasyOne.Model.CommonModel;

    public partial class SelectContentModel : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int nodeId = BasePage.RequestInt32("NodeID");
            if (BasePage.RequestString("isEshop") == "yes")
            {
                IList<ModelInfo> shopModelList = ModelManager.GetShopModelList(ModelShowType.Enable);
                this.RptModelList.DataSource = shopModelList;
                this.RptModelList.DataBind();
            }
            else if (nodeId > 0)
            {
                DataTable modelListByNodeId = ModelManager.GetModelListByNodeId(nodeId, true);
                this.RptModelList.DataSource = modelListByNodeId;
                this.RptModelList.DataBind();
            }
            else
            {
                IList<ModelInfo> modelList = ModelManager.GetModelList(ModelType.None, ModelShowType.Enable);
                this.RptModelList.DataSource = modelList;
                this.RptModelList.DataBind();
            }
        }
    }
}

