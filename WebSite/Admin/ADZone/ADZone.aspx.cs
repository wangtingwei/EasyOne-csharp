namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using EasyOne.Web.UI;
    using System;
    using System.Text.RegularExpressions;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ADZones : AdminPage
    {
        

        protected void EBtnAdZone_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                ADZoneInfo adZoneInfo = new ADZoneInfo();
                adZoneInfo.ZoneName = this.TxtZoneName.Text.Trim();
                adZoneInfo.ZoneJSName = this.TxtZoneJSName.Text.Trim();
                adZoneInfo.ZoneIntro = this.TxtZoneIntro.Text.Trim();
                adZoneInfo.ZoneType = (ADZoneType) DataConverter.CLng(this.RadlZoneType.SelectedValue);
                adZoneInfo.DefaultSetting = this.GetDefaultSetting(this.RadlDefaultSetting.SelectedValue);
                adZoneInfo.Setting = this.GetZoneSetting(adZoneInfo.DefaultSetting);
                adZoneInfo.ZoneWidth = DataConverter.CLng(this.TxtZoneWidth.Text.Trim());
                adZoneInfo.ZoneHeight = DataConverter.CLng(this.TxtZoneHeight.Text.Trim());
                adZoneInfo.ShowType = DataConverter.CLng(this.RadlShowType.SelectedValue);
                adZoneInfo.Active = this.ChkActive.Checked;
                adZoneInfo.UpdateTime = DateTime.Now;
                if (this.HdnAction.Value.Trim() == "Modify")
                {
                    adZoneInfo.ZoneId = DataConverter.CLng(this.HdnZoneId.Value.Trim());
                    if (ADZone.Update(adZoneInfo))
                    {
                        BasePage.ResponseRedirect("ADZoneManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("更新出错！", "ADZone.aspx?ZoneId=" + adZoneInfo.ZoneId);
                    }
                }
                else if (ADZone.Add(adZoneInfo) == DataActionState.Successed)
                {
                    BasePage.ResponseRedirect("ADZoneManage.aspx");
                }
                else
                {
                    AdminPage.WriteErrMsg("添加出错！", "ADZone.aspx");
                }
            }
        }

        private bool GetDefaultSetting(string defaultset)
        {
            return (defaultset == "0");
        }

        private string GetJSFileName()
        {
            return ADZone.GetNewJSName();
        }

        private string GetZoneSetting(bool isDefaultSetting)
        {
            if (isDefaultSetting)
            {
                switch (this.RadlZoneType.SelectedValue)
                {
                    case "2":
                        return "1,100,100,0,1";

                    case "3":
                        return "15,200,0.015,false,#FFFFFF,1";

                    case "4":
                        return "100,100,false,#FFFFFF,1";

                    case "5":
                        return "1,100,100,false,#FFFFFF,1";

                    case "7":
                        return "15,200,0.015,false,#FFFFFF";
                }
                return "";
            }
            switch (this.RadlZoneType.SelectedValue)
            {
                case "2":
                    return (this.DropPopType.SelectedValue + "," + this.TxtPopLeft.Text.Trim() + "," + this.TxtPopTop.Text.Trim() + "," + this.TxtPopCookieHour.Text.Trim() + "," + this.DropPopPosition.SelectedValue);

                case "3":
                    return (this.TxtMoveLeft.Text.Trim() + "," + this.TxtMoveTop.Text.Trim() + "," + this.TxtMoveDelay.Text.Trim() + "," + this.RadlMoveShowCloseAD.SelectedValue + "," + this.TxtMoveCloseFontColor.Text.Trim() + "," + this.DropMovePosition.SelectedValue);

                case "4":
                    return (this.TxtFixedLeft.Text.Trim() + "," + this.TxtFixedTop.Text.Trim() + "," + this.RadlFixedShowCloseAD.SelectedValue + "," + this.TxtFixedCloseFontColor.Text.Trim() + "," + this.DropFixedPosition.SelectedValue);

                case "5":
                    return (this.DropFloatType.SelectedValue + "," + this.TxtFloatLeft.Text.Trim() + "," + this.TxtFloatTop.Text.Trim() + "," + this.RadlFloatShowCloseAD.SelectedValue + "," + this.TxtFloatCloseFontColor.Text.Trim() + "," + this.DropFloatPosition.SelectedValue);

                case "7":
                    return (this.TxtCoupletLeft.Text.Trim() + "," + this.TxtCoupletTop.Text.Trim() + "," + this.TxtCoupletDelay.Text.Trim() + "," + this.RadlCoupletShowCloseAD.SelectedValue + "," + this.TxtCoupletCloseFontColor.Text.Trim());
            }
            return "";
        }

        private void InitDropADZoneSize(string flag)
        {
            this.DropAdZoneSize.Attributes.Add("OnChange", "Zone_SelectSize(this)");
            if (flag != null)
            {
                for (int i = 0; i < this.DropAdZoneSize.Items.Count; i++)
                {
                    if (this.DropAdZoneSize.Items[i].Value == flag)
                    {
                        this.DropAdZoneSize.Items[i].Selected = true;
                    }
                }
            }
            else
            {
                this.DropAdZoneSize.Items[0].Selected = true;
            }
        }

        private void InitRblADZoneDefaultSetting(int selected)
        {
            ListItem item = new ListItem();
            item.Text = "默认设置";
            item.Value = "0";
            this.RadlDefaultSetting.Items.Add(item);
            item = new ListItem();
            item.Text = "自定义设置";
            item.Value = "1";
            this.RadlDefaultSetting.Items.Add(item);
            this.RadlDefaultSetting.Items[0].Attributes.Add("OnClick", "ShowZoneTypePanel()");
            this.RadlDefaultSetting.Items[1].Attributes.Add("OnClick", "ShowZoneTypePanel()");
            BasePage.SetSelectedIndexByValue(this.RadlDefaultSetting, selected.ToString());
        }

        private void InitRblADZoneType(int selected)
        {
            for (int i = 1; i <= 7; i++)
            {
                ListItem item = new ListItem();
                item.Value = i.ToString();
                item.Text = BasePage.EnumToHtml<ADZoneType>((ADZoneType) i);
                if (selected == i)
                {
                    item.Selected = true;
                }
                item.Attributes.Add("OnClick", "ShowZoneTypePanel()");
                this.RadlZoneType.Items.Add(item);
            }
        }

        private void InitSetting(string setting, ADZoneType showType)
        {
            string[] strArray = this.SplitZetting(setting + ",,,,,");
            switch (showType)
            {
                case ADZoneType.Banner:
                case ADZoneType.Code:
                    break;

                case ADZoneType.Pop:
                    this.DropPopType.SelectedIndex = this.DropPopType.Items.IndexOf(this.DropPopType.Items.FindByValue(strArray[0]));
                    this.TxtPopLeft.Text = strArray[1];
                    this.TxtPopTop.Text = strArray[2];
                    this.TxtPopCookieHour.Text = strArray[3];
                    this.DropPopPosition.SelectedIndex = this.DropPopPosition.Items.IndexOf(this.DropPopPosition.Items.FindByValue(strArray[4]));
                    return;

                case ADZoneType.Move:
                    this.TxtMoveLeft.Text = strArray[0];
                    this.TxtMoveTop.Text = strArray[1];
                    this.TxtMoveDelay.Text = strArray[2];
                    BasePage.SetSelectedIndexByValue(this.RadlMoveShowCloseAD, strArray[3]);
                    this.TxtMoveCloseFontColor.Text = strArray[4];
                    this.DropMovePosition.SelectedIndex = this.DropMovePosition.Items.IndexOf(this.DropMovePosition.Items.FindByValue(strArray[5]));
                    return;

                case ADZoneType.Fixed:
                    this.TxtFixedLeft.Text = strArray[0];
                    this.TxtFixedTop.Text = strArray[1];
                    BasePage.SetSelectedIndexByValue(this.RadlFixedShowCloseAD, strArray[2]);
                    this.TxtFixedCloseFontColor.Text = strArray[3];
                    this.DropFixedPosition.SelectedIndex = this.DropFixedPosition.Items.IndexOf(this.DropFixedPosition.Items.FindByValue(strArray[4]));
                    return;

                case ADZoneType.Float:
                    this.DropFloatType.SelectedIndex = this.DropFloatType.Items.IndexOf(this.DropFloatType.Items.FindByValue(strArray[0]));
                    this.TxtFloatLeft.Text = strArray[1];
                    this.TxtFloatTop.Text = strArray[2];
                    BasePage.SetSelectedIndexByValue(this.RadlFloatShowCloseAD, strArray[3]);
                    this.TxtFloatCloseFontColor.Text = strArray[4];
                    this.DropFloatPosition.SelectedIndex = this.DropFloatPosition.Items.IndexOf(this.DropFloatPosition.Items.FindByValue(strArray[5]));
                    return;

                case ADZoneType.Couplet:
                    this.TxtCoupletLeft.Text = strArray[0];
                    this.TxtCoupletTop.Text = strArray[1];
                    this.TxtCoupletDelay.Text = strArray[2];
                    BasePage.SetSelectedIndexByValue(this.RadlCoupletShowCloseAD, strArray[3]);
                    this.TxtCoupletCloseFontColor.Text = strArray[4];
                    break;

                default:
                    return;
            }
        }

        private void InitShowPanel(ADZoneType selected)
        {
            switch (selected)
            {
                case ADZoneType.Banner:
                    this.ZoneTypeSetting1.Attributes.Add("style", "display: ");
                    return;

                case ADZoneType.Pop:
                    this.ZoneTypeSetting2.Attributes.Add("style", "display: ");
                    return;

                case ADZoneType.Move:
                    this.ZoneTypeSetting3.Attributes.Add("style", "display: ");
                    return;

                case ADZoneType.Fixed:
                    this.ZoneTypeSetting4.Attributes.Add("style", "display: ");
                    return;

                case ADZoneType.Float:
                    this.ZoneTypeSetting5.Attributes.Add("style", "display: ");
                    return;

                case ADZoneType.Code:
                    this.ZoneTypeSetting6.Attributes.Add("style", "display: ");
                    return;

                case ADZoneType.Couplet:
                    this.ZoneTypeSetting7.Attributes.Add("style", "display: ");
                    return;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.TxtZoneJSName.Text = this.GetJSFileName();
                this.DropFixedPosition.Attributes.Add("onchange", "ChangePositonShow(this)");
                this.DropFloatPosition.Attributes.Add("onchange", "ChangePositonShow(this)");
                this.DropMovePosition.Attributes.Add("onchange", "ChangePositonShow(this)");
                this.DropPopPosition.Attributes.Add("onchange", "ChangePositonShow(this)");
                if (BasePage.RequestString("Action") == "Modify")
                {
                    int id = BasePage.RequestInt32("ZoneId");
                    ADZoneInfo adZoneById = ADZone.GetAdZoneById(id);
                    this.TxtZoneName.Text = adZoneById.ZoneName;
                    this.TxtZoneJSName.Text = adZoneById.ZoneJSName;
                    this.TxtZoneIntro.Text = adZoneById.ZoneIntro;
                    this.InitRblADZoneType((int) adZoneById.ZoneType);
                    if (adZoneById.DefaultSetting)
                    {
                        this.InitRblADZoneDefaultSetting(0);
                    }
                    else
                    {
                        this.InitRblADZoneDefaultSetting(1);
                    }
                    BasePage.SetSelectedIndexByValue(this.RadlShowType, adZoneById.ShowType.ToString());
                    string flag = adZoneById.ZoneWidth.ToString() + "x" + adZoneById.ZoneHeight.ToString();
                    this.InitDropADZoneSize(flag);
                    this.TxtZoneWidth.Text = adZoneById.ZoneWidth.ToString();
                    this.TxtZoneHeight.Text = adZoneById.ZoneHeight.ToString();
                    this.InitSetting(adZoneById.Setting, adZoneById.ZoneType);
                    if (this.RadlDefaultSetting.SelectedValue == "1")
                    {
                        this.InitShowPanel(adZoneById.ZoneType);
                    }
                    this.ChkActive.Checked = adZoneById.Active;
                    this.EBtnAdZone.Text = "修 改";
                    this.HdnAction.Value = "Modify";
                    this.HdnZoneId.Value = id.ToString();
                    if ((adZoneById.ZoneType > ADZoneType.Banner) && (adZoneById.ZoneType == ADZoneType.Code))
                    {
                        this.DropAdZoneSize.Enabled = false;
                        this.TxtZoneHeight.Enabled = false;
                        this.TxtZoneWidth.Enabled = false;
                    }
                }
                else
                {
                    this.InitRblADZoneType(1);
                    this.InitRblADZoneDefaultSetting(0);
                    this.InitDropADZoneSize(null);
                    this.EBtnAdZone.Text = "添 加";
                    this.HdnAction.Value = "Add";
                }
            }
        }

        private string[] SplitZetting(string setting)
        {
            Regex regex = new Regex(",");
            return regex.Split(setting);
        }
    }
}

