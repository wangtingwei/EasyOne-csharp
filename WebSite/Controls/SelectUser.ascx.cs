namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using EasyOne.Model.UserManage;

    public partial class SelectUser : BaseUserControl
    {

        private string GetUserGroupId(ListControl chklUserGroupList)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < chklUserGroupList.Items.Count; i++)
            {
                if (chklUserGroupList.Items[i].Selected)
                {
                    StringHelper.AppendString(sb, this.ChklUserGroupList.Items[i].Value);
                }
            }
            return sb.ToString();
        }

        protected void GetUserGroupList()
        {
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            this.ChklUserGroupList.Items.Clear();
            this.ChklUserGroupList.DataSource = userGroupList;
            this.ChklUserGroupList.DataTextField = "GroupName";
            this.ChklUserGroupList.DataValueField = "GroupId";
            this.ChklUserGroupList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.GetUserGroupList();
            }
        }

        public string GroupId
        {
            get
            {
                return this.GetUserGroupId(this.ChklUserGroupList);
            }
        }

        public string UserId
        {
            get
            {
                return this.TxtUserID.Text;
            }
            set
            {
                this.TxtUserID.Text = value;
            }
        }

        public int UserType
        {
            get
            {
                foreach (Control control in this.Controls)
                {
                    if ((!string.IsNullOrEmpty(control.ID) && control.ID.StartsWith("RadUserType", StringComparison.Ordinal)) && ((control is RadioButton) && ((RadioButton) control).Checked))
                    {
                        return DataConverter.CLng(control.ID.Replace("RadUserType", ""));
                    }
                }
                return 0;
            }
            set
            {
                ((RadioButton) this.FindControl("RadUserType" + value)).Checked = true;
            }
        }
    }
}

