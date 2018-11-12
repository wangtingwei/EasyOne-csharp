namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class InsideLink : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string text = this.TxtSourceWord.Text;
            string str2 = "";
            if (string.IsNullOrEmpty(this.TxtSourceWord.Text))
            {
                str2 = "<li>站内链接目标不能为空！</li>";
            }
            if (!DataValidator.IsUrl(this.TxtTargetWord.Text))
            {
                str2 = str2 + "<li>链接地址不是有效的url地址！</li>";
            }
            if (string.IsNullOrEmpty(this.TxtPriority.Text))
            {
                str2 = str2 + "<li>站内链接优先级别不能为空！</li>";
            }
            if (string.IsNullOrEmpty(this.TxtReplaceTimes.Text))
            {
                str2 = str2 + "<li>站内链接替换次数不能为空！</li>";
            }
            if (!string.IsNullOrEmpty(str2))
            {
                AdminPage.WriteErrMsg(str2);
            }
            WordReplaceInfo wordReplaceInfo = new WordReplaceInfo();
            if (BasePage.RequestString("Action", "Add") == "Add")
            {
                if (!WordReplace.Exists(text, 1))
                {
                    wordReplaceInfo.SourceWord = this.TxtSourceWord.Text;
                    wordReplaceInfo.TargetWord = this.TxtTargetWord.Text;
                    wordReplaceInfo.Title = this.TxtTitle.Text;
                    wordReplaceInfo.ReplaceTimes = DataConverter.CLng(this.TxtReplaceTimes.Text);
                    wordReplaceInfo.Priority = DataConverter.CLng(this.TxtPriority.Text);
                    wordReplaceInfo.OpenType = Convert.ToBoolean(this.RadlOpenType.SelectedValue);
                    wordReplaceInfo.IsEnabled = Convert.ToBoolean(this.RadioIsEnabled.SelectedValue);
                    wordReplaceInfo.ReplaceType = 1;
                    wordReplaceInfo.ScopesType = 0;
                    if (WordReplace.Add(wordReplaceInfo))
                    {
                        AdminPage.WriteSuccessMsg("<li>添加站内链接成功！</li>", "InsideLinkManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>添加站内链接操作失败！</li>");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在此站内链接！</li>");
                }
            }
            else
            {
                bool flag;
                if (text == this.HdnSource.Value)
                {
                    flag = false;
                }
                else
                {
                    flag = WordReplace.Exists(text, 1);
                }
                if (!flag)
                {
                    wordReplaceInfo.ItemId = BasePage.RequestInt32("ItemID");
                    wordReplaceInfo.SourceWord = this.TxtSourceWord.Text;
                    wordReplaceInfo.TargetWord = this.TxtTargetWord.Text;
                    wordReplaceInfo.Title = this.TxtTitle.Text;
                    wordReplaceInfo.ReplaceTimes = DataConverter.CLng(this.TxtReplaceTimes.Text);
                    wordReplaceInfo.Priority = DataConverter.CLng(this.TxtPriority.Text);
                    wordReplaceInfo.OpenType = Convert.ToBoolean(this.RadlOpenType.SelectedValue);
                    wordReplaceInfo.IsEnabled = Convert.ToBoolean(this.RadioIsEnabled.SelectedValue);
                    wordReplaceInfo.ReplaceType = 1;
                    wordReplaceInfo.ScopesType = 0;
                    if (WordReplace.Update(wordReplaceInfo))
                    {
                        AdminPage.WriteSuccessMsg("<li>修改站内链接成功！</li>", "InsideLinkManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>修改站内链接操作失败！</li>");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在此站内链接！</li>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && (BasePage.RequestString("Action", "Add") == "Modify"))
            {
                int id = BasePage.RequestInt32("ItemID");
                if (id == 0)
                {
                    AdminPage.WriteErrMsg("<li>ID不能为空！</li>");
                }
                WordReplaceInfo infoById = WordReplace.GetInfoById(id);
                if (!infoById.IsNull)
                {
                    this.TxtSourceWord.Text = infoById.SourceWord;
                    this.TxtTargetWord.Text = infoById.TargetWord;
                    this.TxtTitle.Text = infoById.Title;
                    this.TxtReplaceTimes.Text = infoById.ReplaceTimes.ToString();
                    this.TxtPriority.Text = infoById.Priority.ToString();
                    this.RadlOpenType.SelectedValue = infoById.OpenType.ToString();
                    this.RadioIsEnabled.SelectedValue = infoById.IsEnabled.ToString();
                    this.HdnSource.Value = infoById.SourceWord.ToString();
                }
            }
        }
    }
}

