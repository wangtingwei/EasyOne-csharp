namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;
    using EasyOne.ModelControls;
    public partial class AuthorGuide : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RptAuthorTypeList.DataSource = Choiceset.GetDictionaryFieldValueByName("PE_Author", "Type");
            this.RptAuthorTypeList.DataBind();
        }
    }
}

