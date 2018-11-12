namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Enumerations;
    using EasyOne.Model.Analytics;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class CounterGuide : AdminPage
    {
        private static string ConvertAction(string action)
        {
            switch (action)
            {
                case "FIP":
                    return StatName.UserIP.ToString();

                case "FAddress":
                    return StatName.UserAddress.ToString();

                case "FTimezone":
                    return StatName.UserTimezone.ToString();

                case "FKeyword":
                    return StatName.UserKeyword.ToString();

                case "FWeburl":
                    return StatName.UserWeburl.ToString();

                case "FRefer":
                    return StatName.UserRefer.ToString();

                case "FSystem":
                    return StatName.UserSystem.ToString();

                case "FBrowser":
                    return StatName.UserBrowser.ToString();

                case "FMozilla":
                    return StatName.UserMozilla.ToString();

                case "FScreen":
                    return StatName.UserScreen.ToString();

                case "FColor":
                    return StatName.UserColor.ToString();
            }
            return string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Literal child = new Literal();
                StringBuilder builder = new StringBuilder("");
                StatInfoListInfo statInfoListInfo = OtherReport.GetStatInfoListInfo();
                if ((statInfoListInfo != null) && !string.IsNullOrEmpty(statInfoListInfo.RegFieldsFill))
                {
                    string[] strArray = statInfoListInfo.RegFieldsFill.Split(new char[] { ',' });
                    if (strArray != null)
                    {
                        foreach (string str in strArray)
                        {
                            switch (str)
                            {
                                case "FVisit":
                                case "IsCountOnline":
                                    builder.Append("<li><a href='StatOtherReport.aspx?Action=" + str.Trim() + "' target='main_right'>" + Counter.GetItemName(str.Trim()) + "</a></li>");
                                    break;

                                default:
                                    builder.Append("<li><a href='StatUserDataReport.aspx?Action=" + ConvertAction(str.Trim()) + "' target='main_right'>" + Counter.GetItemName(str.Trim()) + "</a></li>");
                                    break;
                            }
                        }
                    }
                }
                child.Text = builder.ToString();
                this.PlhCounter.Controls.Add(child);
            }
        }

        protected string GetYearSelect
        {
            get
            {
                StringBuilder builder = new StringBuilder("<select id='qyear' name='qyear' onchange='change_it()' style='width: 122px'>");
                int num = DateTime.Today.Year - 5;
                for (int i = 0; i < 10; i++)
                {
                    builder.Append("<option value='").Append((int) (num + i)).Append("'>").Append((int) (num + i)).Append("</option>");
                }
                builder.Append("</select> 年<br />");
                return builder.ToString();
            }
        }
    }
}

