namespace EasyOne.WebSite.Admin.CommonModel
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ModelPreView : AdminPage
    {

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("FieldManage.aspx?ModelType=" + BasePage.RequestString("ModelType") + "&ModelId=" + BasePage.RequestString("ModelId") + "&ModelName=" + base.Server.UrlEncode(BasePage.RequestString("ModelName")));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str = BasePage.RequestString("Action");
                if (str != null)
                {
                    if (!(str == "Model"))
                    {
                        if (str == "Template")
                        {
                            this.RepModel.DataSource = EasyOne.CommonModel.TemplateField.GetFieldList(BasePage.RequestInt32("TemplateID"), false);
                        }
                    }
                    else
                    {
                        this.RepModel.DataSource = EasyOne.CommonModel.Field.GetFieldList(BasePage.RequestInt32("ModelID"), false);
                    }
                }
                this.RepModel.DataBind();
            }
        }

        protected void RepModel_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                FieldControl control = (FieldControl) e.Item.FindControl("Field");
                FieldInfo dataItem = (FieldInfo) e.Item.DataItem;
                if (control.ControlType == FieldType.LookType)
                {
                    int modelId = DataConverter.CLng(dataItem.Settings[0]);
                    if (!EasyOne.CommonModel.Field.FieldExists(modelId, dataItem.Settings[1]))
                    {
                        EasyOne.CommonModel.Field.SetDisabled(dataItem.FieldName, modelId, true);
                        e.Item.Visible = false;
                    }
                }
            }
        }
    }
}

