namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class WordFilter : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string text = this.TxtSourceWord.Text;
            WordReplaceInfo wordReplaceInfo = new WordReplaceInfo();
            if (this.ViewState["action"].ToString() == "Add")
            {
                if (!WordReplace.Exists(text, 0))
                {
                    wordReplaceInfo.ItemId = WordReplace.GetMaxId() + 1;
                    wordReplaceInfo.SourceWord = this.TxtSourceWord.Text;
                    wordReplaceInfo.TargetWord = this.TxtTargetWord.Text;
                    wordReplaceInfo.Priority = DataConverter.CLng(this.TxtPriority.Text);
                    wordReplaceInfo.IsEnabled = Convert.ToBoolean(this.RadioIsEnabled.SelectedValue);
                    wordReplaceInfo.ReplaceType = 0;
                    if (WordReplace.Add(wordReplaceInfo))
                    {
                        AdminPage.WriteSuccessMsg("<li>添加字符过滤成功！</li>", "WordFilterManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>添加字符过滤操作失败！</li>");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在此字符过滤！</li>");
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
                    flag = WordReplace.Exists(text, 0);
                }
                if (!flag)
                {
                    wordReplaceInfo.ItemId = BasePage.RequestInt32("ItemID");
                    wordReplaceInfo.SourceWord = this.TxtSourceWord.Text;
                    wordReplaceInfo.TargetWord = this.TxtTargetWord.Text;
                    wordReplaceInfo.Priority = DataConverter.CLng(this.TxtPriority.Text);
                    wordReplaceInfo.IsEnabled = Convert.ToBoolean(this.RadioIsEnabled.SelectedValue);
                    wordReplaceInfo.ReplaceType = 0;
                    if (WordReplace.Update(wordReplaceInfo))
                    {
                        AdminPage.WriteSuccessMsg("<li>修改字符过滤成功！</li>", "WordFilterManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>修改字符过滤操作失败！</li>");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在此字符过滤！</li>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("Action", "Add");
                this.ViewState["action"] = str;
                if (str == "Modify")
                {
                    WordReplaceInfo infoById = new WordReplaceInfo();
                    infoById = WordReplace.GetInfoById(BasePage.RequestInt32("ItemID"));
                    this.TxtSourceWord.Text = infoById.SourceWord;
                    this.TxtTargetWord.Text = infoById.TargetWord;
                    this.TxtPriority.Text = infoById.Priority.ToString();
                    this.RadioIsEnabled.SelectedValue = infoById.IsEnabled.ToString();
                    this.HdnSource.Value = infoById.SourceWord.ToString();
                }
            }
        }
    }
}

