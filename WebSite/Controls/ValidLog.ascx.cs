namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;
    using EasyOne.Components;

    public partial class ValidLog : BaseUserControl
    {
        private string m_AdminName = PEContext.Current.Admin.AdminName;

        protected void EgvUserValid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserValidLogInfo dataItem = (UserValidLogInfo) e.Row.DataItem;
                if (dataItem != null)
                {
                    HyperLink link = (HyperLink) e.Row.FindControl("HypUserName");
                    if (string.IsNullOrEmpty(this.m_AdminName))
                    {
                        link.NavigateUrl = "~/User/Default.aspx?UserName=" + dataItem.UserName;
                    }
                    else
                    {
                        link.NavigateUrl = "~/Admin/User/UserShow.aspx?UserName=" + dataItem.UserName;
                    }
                    link.Text = dataItem.UserName;
                }
            }
        }

        public string GetIncomePayout(int consumeType, int validNum)
        {
            string str2 = validNum.ToString() + "天";
            switch (consumeType)
            {
                case 1:
                    return ("<span style='color:blue'> +" + str2 + "</span>");

                case 2:
                    return ("<span style='color:red'> " + str2 + "</span>");
            }
            return "<span style='color:#000000'>其他</span>";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HdnField.Value))
            {
                this.HdnField.Value = BaseUserControl.RequestInt32("Field").ToString();
            }
            if (string.IsNullOrEmpty(this.HdnKeyword.Value))
            {
                this.HdnKeyword.Value = BaseUserControl.RequestString("KeyWord");
            }
            if (string.IsNullOrEmpty(this.HdnSearchType.Value))
            {
                this.HdnSearchType.Value = BaseUserControl.RequestInt32("SearchType").ToString();
            }
            if (string.IsNullOrEmpty(this.m_AdminName))
            {
                this.EgvUserValid.Columns[1].Visible = false;
                this.EgvUserValid.Columns[4].Visible = false;
            }
        }

        public int Field
        {
            get
            {
                return DataConverter.CLng(this.HdnField.Value);
            }
            set
            {
                this.HdnField.Value = value.ToString();
            }
        }

        public string Keyword
        {
            get
            {
                return this.HdnKeyword.Value;
            }
            set
            {
                this.HdnKeyword.Value = value;
            }
        }

        public int SearchType
        {
            get
            {
                return DataConverter.CLng(this.HdnSearchType.Value);
            }
            set
            {
                this.HdnSearchType.Value = value.ToString();
            }
        }
    }
}

