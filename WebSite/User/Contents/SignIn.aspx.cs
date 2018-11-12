namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Contents;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class SignIn : DynamicPage
    {
        protected Dictionary<int, string> m_NodeNameDictionary = new Dictionary<int, string>();

        protected void EBtnSignIn_Click(object sender, EventArgs e)
        {
            bool flag = true;
            StringBuilder selectList = this.EgvContentSignIn.SelectList;
            if (selectList.Length < 1)
            {
                DynamicPage.WriteUserErrMsg("请选择要签收的项目！");
            }
            else
            {
                for (int i = 0; i < selectList.Length; i++)
                {
                    if (!SignInLog.SignIn(selectList[i], PEContext.Current.User.UserName, true, PEContext.Current.UserHostAddress))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
            {
                DynamicPage.WriteUserSuccessMsg("<li>签收成功！</li>", "ContentSignin.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
            }
            else
            {
                DynamicPage.WriteUserErrMsg("<li>签收失败！</li>");
            }
        }

        protected void EgvContentSignIn_RowCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "SignIn")
            {
                if (SignInLog.SignIn(DataConverter.CLng(e.CommandArgument.ToString()), PEContext.Current.User.UserName, true, PEContext.Current.UserHostAddress))
                {
                    DynamicPage.WriteUserSuccessMsg("<li>签收成功！</li>", "Signin.aspx?NodeID=" + BasePage.RequestInt32("NodeID"));
                }
                else
                {
                    DynamicPage.WriteUserErrMsg("<li>签收失败！</li>");
                }
            }
        }

        protected void EgvContentSignIn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataItem = (DataRowView) e.Row.DataItem;
                int key = BasePage.RequestInt32("NodeID");
                string nodeName = "";
                LinkImage image = e.Row.FindControl("LinkImageModel") as LinkImage;
                string itemIcon = ModelManager.GetCacheModelById(DataConverter.CLng(dataItem["ModelId"].ToString())).ItemIcon;
                if (string.IsNullOrEmpty(itemIcon))
                {
                    itemIcon = "Default.gif";
                }
                image.Icon = itemIcon;
                if (dataItem["LinkType"].ToString() != "0")
                {
                    image.IsShowLink = true;
                }
                if (DataConverter.CLng(dataItem["NodeID"].ToString()) != key)
                {
                    key = DataConverter.CLng(dataItem["NodeID"].ToString());
                    if (this.m_NodeNameDictionary.ContainsKey(key))
                    {
                        nodeName = this.m_NodeNameDictionary[key];
                    }
                    else
                    {
                        NodeInfo cacheNodeById = EasyOne.Contents.Nodes.GetCacheNodeById(key);
                        if (!cacheNodeById.IsNull)
                        {
                            nodeName = cacheNodeById.NodeName;
                            this.m_NodeNameDictionary.Add(key, nodeName);
                        }
                    }
                    ExtendedHyperLink link = e.Row.FindControl("LnkNodeLink") as ExtendedHyperLink;
                    link.BeginTag = "<strong>[";
                    link.Text = nodeName;
                    link.EndTag = "]</strong>";
                    link.NavigateUrl = "Signin.aspx?NodeID=" + dataItem["NodeID"].ToString();
                }
                HyperLink link2 = e.Row.FindControl("HypTitle") as HyperLink;
                link2.NavigateUrl = string.Concat(new object[] { base.FullBasePath, "Item/", dataItem["GeneralId"], ".aspx" });
                Label label = e.Row.FindControl("LblSignInStatus") as Label;
                LinkButton button = e.Row.FindControl("ELbtnContentSignIn") as LinkButton;
                if (DataConverter.CBoolean(dataItem["IsSignin"].ToString()))
                {
                    label.Text = "<span style=\"color:Green\">已签收</span>";
                    button.Enabled = false;
                }
                else
                {
                    label.Text = "<span style=\"color:Red\">未签收</span>";
                    button.Enabled = true;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.OdsContents.SelectParameters["UserName"].DefaultValue = PEContext.Current.User.UserName;
        }
    }
}

