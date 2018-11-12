namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.Collection;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ConfigStep2 : AdminPage
    {
        protected string m_CodeType = "GB2312";
        protected int m_itemId;
        private int m_ModelId;
        private int m_NodeId;
        protected string m_Url = "";

        protected void BtnCancel1_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect(string.Concat(new object[] { "ConfigStep1.aspx?Action=Modify&ItemId=", this.m_itemId, "&ModelId=", this.m_ModelId.ToString(), "&NodeId=", this.m_NodeId.ToString() }));
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                bool flag = false;
                CollectionListRuleInfo collectionListRuleInfo = new CollectionListRuleInfo();
                collectionListRuleInfo.ItemId = this.m_itemId;
                collectionListRuleInfo.ListBeginCode = this.TxtListBegin.Text;
                collectionListRuleInfo.ListEndCode = this.TxtListEnd.Text;
                collectionListRuleInfo.LinkBeginCode = this.TxtLinkBegin.Text;
                collectionListRuleInfo.LinkEndCode = this.TxtLinkEnd.Text;
                if (this.RadlPaingType.Checked)
                {
                    collectionListRuleInfo.UsePaging = false;
                }
                else
                {
                    collectionListRuleInfo.UsePaging = true;
                }
                if (CollectionListRules.Exists(this.m_itemId))
                {
                    flag = CollectionListRules.Update(collectionListRuleInfo);
                }
                else
                {
                    flag = CollectionListRules.Add(collectionListRuleInfo);
                }
                if (!flag)
                {
                    AdminPage.WriteErrMsg("列表设置失败！");
                }
                if (this.SavePaing())
                {
                    BasePage.ResponseRedirect(string.Concat(new object[] { "ConfigStep3.aspx?Action=", this.HdnAction.Value, "&ItemId=", this.m_itemId }));
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = BasePage.RequestInt32("ItemId");
            if (id <= 0)
            {
                AdminPage.WriteErrMsg("<li>ItemID不存在！</li>");
            }
            CollectionItemInfo infoById = CollectionItem.GetInfoById(id);
            if (infoById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>当前采集项目不存在！</li>");
            }
            this.m_CodeType = infoById.CodeType;
            this.m_Url = infoById.Url;
            this.m_itemId = infoById.ItemId;
            this.m_ModelId = infoById.ModelId;
            this.m_NodeId = infoById.NodeId;
            this.LblItemName.Text = infoById.ItemName;
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(this.m_Url);
            this.TxtShowCode.Text = common.GetHttpPage(url, this.m_CodeType);
            this.HdnTestContent.Value = this.TxtShowCode.Text;
            this.LblLink.Text = " <a href='" + this.m_Url + "' target='_blank'>查看原始网页</a>";
            string str = BasePage.RequestString("Action", "Add");
            if (!base.IsPostBack)
            {
                this.RadlPaingType.Attributes.Add("onclick", "javascript:ListPaing(0);");
                this.RadlPaingType1.Attributes.Add("onclick", "javascript:ListPaing(1);");
                this.RadlPaingType2.Attributes.Add("onclick", "javascript:ListPaing(2);");
                this.RadlPaingType3.Attributes.Add("onclick", "javascript:ListPaing(3);");
                this.RadlPaingType4.Attributes.Add("onclick", "javascript:ListPaing(4);");
                if (str == "Modify")
                {
                    CollectionListRuleInfo info2 = CollectionListRules.GetInfoById(infoById.ItemId);
                    if (!info2.IsNull)
                    {
                        this.TxtListBegin.Text = info2.ListBeginCode;
                        this.TxtListEnd.Text = info2.ListEndCode;
                        this.TxtLinkBegin.Text = info2.LinkBeginCode;
                        this.TxtLinkEnd.Text = info2.LinkEndCode;
                        if (info2.UsePaging)
                        {
                            CollectionPagingRuleInfo info3 = CollectionPagingRules.GetInfoById(infoById.ItemId, 0);
                            this.TxtPaingBegin.Text = info3.PagingBeginCode;
                            this.TxtPaingEnd.Text = info3.PagingEndCode;
                            switch (info3.PagingType)
                            {
                                case 0:
                                    this.RadlPaingType.Checked = true;
                                    break;

                                case 1:
                                    this.RadlPaingType1.Checked = true;
                                    break;

                                case 2:
                                    this.RadlPaingType2.Checked = true;
                                    break;

                                case 3:
                                    this.RadlPaingType3.Checked = true;
                                    break;

                                case 4:
                                    this.RadlPaingType4.Checked = true;
                                    break;

                                default:
                                    this.RadlPaingType.Checked = true;
                                    break;
                            }
                            this.TxtPaingAddress.Text = info3.DesignatedUrl;
                            this.TxtScopeBegin.Text = info3.ScopeBegin.ToString();
                            this.TxtScopeEnd.Text = info3.ScopeEnd.ToString();
                            this.TxtListPaing.Text = info3.PagingUrlList;
                            this.TxtPaingBegin2.Text = info3.PagingBeginCode;
                            this.TxtPaingEnd2.Text = info3.PagingEndCode;
                            this.TxtLinkBegin2.Text = info3.LinkBeginCode;
                            this.TxtLinkEnd2.Text = info3.LinkEndCode;
                            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "Init", "<script type='text/javascript'>ListPaing(" + DataConverter.CLng(info3.PagingType).ToString() + ");</script>");
                        }
                        this.BtnCancel1.Visible = true;
                    }
                }
            }
            this.SmpNavigator.CurrentNode = string.Concat(new object[] { "<a title=\"采集项目设置\" href=\"ConfigStep1.aspx?Action=Modify&ItemId=", this.m_itemId, "&ModelId=", this.m_ModelId.ToString(), "&NodeId=", this.m_NodeId.ToString(), "\">采集项目设置</a> >> <span style='color:red;'>列表页采集设置</span> >> <a title=\"内容页采集设置\" href=\"ConfigStep3.aspx?Action=Modify&amp;ItemID=", id.ToString(), "\">内容页采集设置</a>" });
            this.HdnAction.Value = str;
        }

        private bool SavePaing()
        {
            bool flag = true;
            int num = 0;
            if (this.RadlPaingType1.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtPaingBegin.Text))
                {
                    AdminPage.WriteErrMsg("“下一页”URL开始代码不能为空！");
                }
                if (string.IsNullOrEmpty(this.TxtPaingEnd.Text))
                {
                    AdminPage.WriteErrMsg("“下一页”URL结束代码不能为空！");
                }
                num = 1;
            }
            if (this.RadlPaingType2.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtPaingAddress.Text))
                {
                    AdminPage.WriteErrMsg("URL地址不能为空！");
                }
                if (DataValidator.IsUrl(this.TxtPaingAddress.Text))
                {
                    AdminPage.WriteErrMsg("URL地址不是有效的URL！");
                }
                if (DataConverter.CLng(this.TxtScopeBegin.Text) <= 0)
                {
                    AdminPage.WriteErrMsg("ID开始范围为不是数字或小于1！");
                }
                if (DataConverter.CLng(this.TxtScopeEnd.Text) <= 0)
                {
                    AdminPage.WriteErrMsg("ID结束范围为不是数字或小于1！");
                }
                num = 2;
            }
            if (this.RadlPaingType3.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtListPaing.Text))
                {
                    AdminPage.WriteErrMsg("URL列表不能为空！");
                }
                num = 3;
            }
            if (this.RadlPaingType4.Checked)
            {
                if (string.IsNullOrEmpty(this.TxtPaingBegin2.Text))
                {
                    AdminPage.WriteErrMsg("分页代码开始不能为空！");
                }
                if (string.IsNullOrEmpty(this.TxtPaingEnd2.Text))
                {
                    AdminPage.WriteErrMsg("分页代码结束不能为空！");
                }
                if (string.IsNullOrEmpty(this.TxtLinkBegin2.Text))
                {
                    AdminPage.WriteErrMsg("分页URL开始代码不能为空！");
                }
                if (string.IsNullOrEmpty(this.TxtLinkEnd2.Text))
                {
                    AdminPage.WriteErrMsg("分页URL结束代码不能为空！");
                }
                num = 4;
            }
            if (!this.RadlPaingType.Checked)
            {
                CollectionPagingRuleInfo collectionPagingRuleInfo = new CollectionPagingRuleInfo();
                collectionPagingRuleInfo.ItemId = this.m_itemId;
                collectionPagingRuleInfo.RuleType = 0;
                collectionPagingRuleInfo.PagingType = num;
                collectionPagingRuleInfo.PagingBeginCode = this.TxtPaingBegin.Text;
                collectionPagingRuleInfo.PagingEndCode = this.TxtPaingEnd.Text;
                collectionPagingRuleInfo.DesignatedUrl = this.TxtPaingAddress.Text;
                collectionPagingRuleInfo.ScopeBegin = DataConverter.CLng(this.TxtScopeBegin.Text);
                collectionPagingRuleInfo.ScopeEnd = DataConverter.CLng(this.TxtScopeEnd.Text);
                collectionPagingRuleInfo.PagingUrlList = this.TxtListPaing.Text;
                if (num == 4)
                {
                    collectionPagingRuleInfo.PagingBeginCode = this.TxtPaingBegin2.Text;
                    collectionPagingRuleInfo.PagingEndCode = this.TxtPaingEnd2.Text;
                    collectionPagingRuleInfo.LinkBeginCode = this.TxtLinkBegin2.Text;
                    collectionPagingRuleInfo.LinkEndCode = this.TxtLinkEnd2.Text;
                }
                if (CollectionPagingRules.Exists(this.m_itemId, 0))
                {
                    flag = CollectionPagingRules.Update(collectionPagingRuleInfo);
                }
                else
                {
                    flag = CollectionPagingRules.Add(collectionPagingRuleInfo);
                }
                if (!flag)
                {
                    AdminPage.WriteErrMsg("分页设置失败！");
                }
            }
            return flag;
        }
    }
}

