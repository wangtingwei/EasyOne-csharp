namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Controls;
    using EasyOne.Model.Collection;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;

    public partial class ConfigStep1 : AdminPage
    {
        protected void DropNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("ConfigStep1.aspx?ItemId=" + BasePage.RequestInt32("ItemId").ToString() + "&Modify=" + this.DropModelId.SelectedValue + "&NodeID=" + BasePage.RequestInt32("NodeID").ToString() + ")");
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string str = this.TxtUrl.Text.Trim();
                if (string.IsNullOrEmpty(str))
                {
                    AdminPage.WriteErrMsg("采集URL地址不能为空！");
                }
                else if (str.Length < 8)
                {
                    AdminPage.WriteErrMsg("采集URL不是有效的URL地址！");
                }
                else if (str.Substring(0, 7) != "http://")
                {
                    AdminPage.WriteErrMsg("采集URL不是有效的URL地址！");
                }
                else
                {
                    bool flag;
                    CollectionItemInfo collectionItemInfo = new CollectionItemInfo();
                    collectionItemInfo.ItemName = this.TxtItemName.Text;
                    collectionItemInfo.NodeId = DataConverter.CLng(this.NodeType1.FieldValue);
                    collectionItemInfo.InfoNodeId = this.NodeType1.InfoNodeId;
                    collectionItemInfo.Url = str;
                    collectionItemInfo.UrlName = this.TxtWebSite.Text;
                    collectionItemInfo.CodeType = this.RadlCodeType.SelectedValue;
                    collectionItemInfo.MaxNum = DataConverter.CLng(this.TxtMaxNum.Text);
                    collectionItemInfo.OrderType = DataConverter.CLng(this.RadlOrder.SelectedValue);
                    collectionItemInfo.Intro = this.TxtIntro.Text;
                    collectionItemInfo.SpecialId = this.SpecialId.FieldValue;
                    collectionItemInfo.AutoCreateHtml = DataConverter.CBoolean(this.RadlAutoCreateHtml.SelectedValue);
                    bool flag2 = false;
                    if (this.HdnAction.Value == "Modify")
                    {
                        collectionItemInfo.ItemId = BasePage.RequestInt32("ItemID");
                        collectionItemInfo.ModelId = DataConverter.CLng(this.HiddenModel.Value);
                        collectionItemInfo.Detection = DataConverter.CBoolean(this.HdnDetection.Value);
                        if (collectionItemInfo.ItemName == this.HdnItemName.Value)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = CollectionItem.Exists(collectionItemInfo.ItemName);
                        }
                    }
                    else
                    {
                        collectionItemInfo.Detection = false;
                        collectionItemInfo.ModelId = DataConverter.CLng(this.DropModelId.SelectedValue);
                        flag = CollectionItem.Exists(collectionItemInfo.ItemName);
                    }
                    if (flag)
                    {
                        AdminPage.WriteErrMsg("<li>数据库中已经存在此采集项目！</li>");
                    }
                    if (this.HdnAction.Value == "Modify")
                    {
                        flag2 = CollectionItem.Update(collectionItemInfo);
                    }
                    else
                    {
                        flag2 = CollectionItem.Add(collectionItemInfo);
                    }
                    if (flag2)
                    {
                        BasePage.ResponseRedirect("ConfigStep2.aspx?Action=" + this.HdnAction.Value + "&ItemId=" + collectionItemInfo.ItemId.ToString());
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("保存采集配置第一步失败！");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action", "Add");
            if (!base.IsPostBack)
            {
                IList<ModelInfo> cacheModelList = ModelManager.GetCacheModelList();
                this.DropModelId.DataSource = cacheModelList;
                this.DropModelId.DataBind();
                this.DropModelId.SelectedValue = BasePage.RequestString("ModelId");
                this.DropModelId.Attributes.Add("Onchange", "javascript:window.location='ConfigStep1.aspx?ItemId=" + BasePage.RequestInt32("ItemId").ToString() + "&ModelId='+this.options[this.selectedIndex].value +'&NodeID=" + BasePage.RequestInt32("NodeID").ToString() + "';");
                this.NodeType1.NodeInfoType = 2;
                this.SpecialId.SpecialInfoType = 2;
                if (str == "Modify")
                {
                    int id = BasePage.RequestInt32("ItemId");
                    if (id <= 0)
                    {
                        AdminPage.WriteErrMsg("<li>ItemID不存在！");
                    }
                    CollectionItemInfo infoById = CollectionItem.GetInfoById(id);
                    if (infoById.IsNull)
                    {
                        AdminPage.WriteErrMsg("<li>采集项目规则不存在！");
                    }
                    if (!infoById.IsNull)
                    {
                        this.TxtItemName.Text = infoById.ItemName;
                        this.TxtWebSite.Text = infoById.UrlName;
                        this.TxtUrl.Text = infoById.Url;
                        this.RadlCodeType.SelectedValue = infoById.CodeType;
                        this.TxtMaxNum.Text = infoById.MaxNum.ToString();
                        this.RadlCodeType.SelectedValue = infoById.OrderType.ToString();
                        this.TxtIntro.Text = infoById.Intro;
                        this.DropModelId.SelectedValue = infoById.ModelId.ToString();
                        this.NodeType1.FieldValue = infoById.NodeId.ToString();
                        this.SpecialId.FieldValue = infoById.SpecialId.ToString();
                        this.RadlAutoCreateHtml.SelectedValue = infoById.AutoCreateHtml.ToString();
                        this.RadlOrder.SelectedValue = infoById.OrderType.ToString();
                        this.HdnAction.Value = str;
                        this.HdnItemName.Value = infoById.ItemName;
                        this.HiddenModel.Value = infoById.ModelId.ToString();
                        this.DropModelId.Enabled = false;
                    }
                    this.SmpNavigator.CurrentNode = "<span style='color:red;'>采集项目设置</span> >> <a title=\"列表页采集设置\" href=\"ConfigStep2.aspx?Action=Modify&amp;ItemID=" + id.ToString() + "\">列表页采集设置</a> >> <a title=\"内容页采集设置\" href=\"ConfigStep3.aspx?Action=Modify&amp;ItemID=" + id.ToString() + "\">内容页采集设置</a>";
                }
            }
        }
    }
}

