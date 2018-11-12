namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Model.CommonModel;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class LookType : BaseFieldControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string tableName;
                int id = DataConverter.CLng(base.Settings[0]);
                ModelInfo modelInfoById = ModelManager.GetModelInfoById(id);
                if (Field.GetFieldInfoByFieldName(id, base.Settings[1]).FieldLevel == 0)
                {
                    tableName = "PE_CommonModel";
                }
                else
                {
                    tableName = modelInfoById.TableName;
                }
                if (Field.FieldExists(id, base.Settings[1]))
                {
                    ArrayList list = ModelManager.GetLookupField(tableName, base.Settings[1], id);
                    this.DropSelectItem.DataSource = list;
                    this.DropSelectItem.DataBind();
                }
                BaseUserControl.SetSelectedIndexByValue(this.DropSelectItem, this.FieldValue);
            }
            else
            {
                this.FieldValue = this.DropSelectItem.SelectedValue;
            }
        }
    }
}

