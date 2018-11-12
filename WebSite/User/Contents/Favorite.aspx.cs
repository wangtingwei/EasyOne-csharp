namespace EasyOne.WebSite.User.Info
{
    using EasyOne.Accessories;
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using EasyOne.WebSite.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Favorites : DynamicPage
    {
        private int m_UserId;

        protected void BtnBatchDelete_Click(object sender, EventArgs e)
        {
            if (Favorite.Delete(this.m_UserId, this.EgvFavorite.SelectList.ToString()))
            {
                this.EgvFavorite.DataBind();
            }
            else
            {
                DynamicPage.WriteUserErrMsg("取消失败");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfo usersByUserName = Users.GetUsersByUserName(PEContext.Current.User.UserName);
            this.m_UserId = usersByUserName.UserId;
            this.OdsFavorite.SelectParameters["userId"].DefaultValue = this.m_UserId.ToString();
            if (!base.IsPostBack)
            {
                FavoriteInfo favoriteInfo = new FavoriteInfo();
                favoriteInfo.FavoriteTime = DateTime.Now;
                favoriteInfo.InfoId = BasePage.RequestInt32("Id");
                favoriteInfo.UserId = this.m_UserId;
                string str = BasePage.RequestStringToLower("Action");
                if (str != null)
                {
                    if (!(str == "add"))
                    {
                        if (!(str == "delete"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (favoriteInfo.InfoId <= 0)
                        {
                            DynamicPage.WriteErrMsg("你收藏的内容不存在！", "Index.aspx");
                        }
                        UserPurviewInfo userPurview = usersByUserName.UserPurview;
                        int maxSaveInfos = 0;
                        if (!userPurview.IsNull)
                        {
                            maxSaveInfos = userPurview.MaxSaveInfos;
                        }
                        if (maxSaveInfos <= 0)
                        {
                            DynamicPage.WriteErrMsg("你没有收藏权限，请与网站管理员联系！", "Index.aspx");
                        }
                        if (maxSaveInfos > Favorite.GetUserFavoiteCount(this.m_UserId))
                        {
                            Favorite.Add(favoriteInfo);
                            DynamicPage.WriteSuccessMsg("收藏成功", "~/Item/" + favoriteInfo.InfoId.ToString() + ".aspx");
                            return;
                        }
                        DynamicPage.WriteErrMsg("你收藏的内容已达到最大收藏数");
                        return;
                    }
                    if (favoriteInfo.InfoId <= 0)
                    {
                        DynamicPage.WriteUserErrMsg("你收藏的内容不存在！", "Favorite.aspx");
                    }
                    if (!Favorite.Delete(this.m_UserId, BasePage.RequestInt32("Id")))
                    {
                        DynamicPage.WriteUserErrMsg("取消失败");
                    }
                }
            }
        }
    }
}

