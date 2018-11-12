namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.ExtendedControls;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Template_AddPage : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("n");
            string str2 = BasePage.RequestString("b");
            if (!string.IsNullOrEmpty(str))
            {
                this.LabelName.Text = str;
                string str3 = "<div>　数据源：";
                string[] strArray = str2.Split(new string[] { "|||" }, StringSplitOptions.None);
                str3 = str3 + "<select name=\"datasource\">";
                foreach (string str4 in strArray)
                {
                    if (!string.IsNullOrEmpty(str4.Trim()))
                    {
                        string str5 = str3;
                        str3 = str5 + "<option value='" + str4.Trim() + "'>" + str4.Trim() + "</option>";
                    }
                }
                str3 = (str3 + "</select></div>") + "<div>容器类型：<input type=\"text\" name=\"span\" size=\"10\" id=\"spantype\" value=\"span\" /></div>" + "<div>容器风格：<input type=\"text\" name=\"cssname\" size=\"10\" id=\"cssname\" /></div>";
                this.labelbody.Text = str3;
            }
        }
    }
}

