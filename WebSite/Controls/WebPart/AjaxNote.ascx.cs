namespace EasyOne.WebSite.Controls.WebPart
{
    using EasyOne.Components;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;

    public partial class AjaxNote : BaseWebPart, IWebPartPermissibility
    {
        protected string m_OperateCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base.Title))
            {
                base.Title = "备忘录（自动保存）";
            }
            if (!this.Page.IsPostBack)
            {
                if (base.Session["AdminNoteText"] != null)
                {
                    this.TxtEditor.Text = base.Session["AdminNoteText"].ToString();
                }
                else
                {
                    AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(PEContext.Current.Admin.AdminName);
                    if (!adminProfile.IsNull)
                    {
                        this.TxtEditor.Text = adminProfile.NoteText;
                    }
                }
            }
        }

        protected void TxtEditor_TextChanged(object sender, EventArgs e)
        {
            base.Session["AdminNoteText"] = this.TxtEditor.Text;
            string adminName = PEContext.Current.Admin.AdminName;
            AdminProfileInfo adminProfile = AdminProfile.GetAdminProfile(adminName);
            adminProfile.NoteText = this.TxtEditor.Text;
            if (!adminProfile.IsNull)
            {
                AdminProfile.Update(adminProfile);
            }
            else
            {
                adminProfile.AdminName = adminName;
                AdminProfile.Add(adminProfile);
            }
        }

        [Personalizable(PersonalizationScope.User)]
        public string OperateCode
        {
            get
            {
                return this.m_OperateCode;
            }
            set
            {
                this.m_OperateCode = value;
            }
        }
    }
}

