namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class ADAdd : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string adZoneIdList = this.GetAdZoneIdList();
            if (this.Page.IsValid)
            {
                DataActionState state;
                AdvertisementInfo advertisementInfo = new AdvertisementInfo();
                advertisementInfo.UserId = 0;
                advertisementInfo.ADName = this.TxtADName.Text.Trim();
                advertisementInfo.ADType = DataConverter.CLng(this.RadlADType.SelectedValue);
                advertisementInfo.Priority = DataConverter.CLng(this.TxtPriority.Text.Trim());
                advertisementInfo.Passed = this.ChkPassed.Checked;
                advertisementInfo.CountView = this.ChkCountView.Checked;
                advertisementInfo.Views = DataConverter.CLng(this.TxtViews.Text.Trim());
                advertisementInfo.CountClick = this.ChkCountClick.Checked;
                advertisementInfo.Clicks = DataConverter.CLng(this.TxtClicks.Text.Trim());
                advertisementInfo.ZoneId = adZoneIdList;
                advertisementInfo.ADId = DataConverter.CLng(this.HdnAdId.Value.Trim());
                advertisementInfo.OverdueDate = this.DpkOverdueDate.Date;
                switch (advertisementInfo.ADType)
                {
                    case 1:
                    {
                        advertisementInfo.ImgUrl = this.FileUploadControl1.FilePath;
                        if (string.IsNullOrEmpty(advertisementInfo.ImgUrl))
                        {
                            AdminPage.WriteErrMsg("图片广告请上传图片！");
                        }
                        advertisementInfo.ImgHeight = DataConverter.CLng(this.TxtImgHeight.Text.Trim());
                        advertisementInfo.ImgWidth = DataConverter.CLng(this.TxtImgWidth.Text.Trim());
                        string str2 = this.TxtLinkUrl.Text.Trim();
                        advertisementInfo.LinkUrl = str2;
                        advertisementInfo.LinkTarget = DataConverter.CLng(this.RadlLinkTarget.SelectedValue);
                        advertisementInfo.LinkAlt = this.TxtLinkAlt.Text.Trim();
                        advertisementInfo.ADIntro = this.TxtADIntro.Text.Trim();
                        break;
                    }
                    case 2:
                        advertisementInfo.ImgUrl = this.ExtenFileUpload.FilePath;
                        if (string.IsNullOrEmpty(advertisementInfo.ImgUrl))
                        {
                            AdminPage.WriteErrMsg("Flash广告请上传Flash！");
                        }
                        advertisementInfo.ImgHeight = DataConverter.CLng(this.TxtFlashHeight.Text.Trim());
                        advertisementInfo.ImgWidth = DataConverter.CLng(this.TxtFlashWidth.Text.Trim());
                        advertisementInfo.FlashWmode = DataConverter.CLng(this.RadlFlashMode.SelectedValue);
                        break;

                    case 3:
                        advertisementInfo.ADIntro = this.TxtADText.Text.Trim();
                        break;

                    case 4:
                        advertisementInfo.ADIntro = this.TxtADCode.Text.Trim();
                        break;

                    case 5:
                        advertisementInfo.ADIntro = this.TxtWebFileUrl.Text.Trim();
                        break;
                }
                if (this.HdnAction.Value.Trim() == "Modify")
                {
                    state = Advertisement.Update(advertisementInfo);
                }
                else
                {
                    state = Advertisement.Add(advertisementInfo);
                }
                switch (state)
                {
                    case DataActionState.Successed:
                        if (!string.IsNullOrEmpty(adZoneIdList))
                        {
                            ADZone.CreateJS(adZoneIdList);
                        }
                        AdminPage.WriteSuccessMsg("保存操作广告成功！", "AdManage.aspx");
                        return;

                    case DataActionState.Unknown:
                        AdminPage.WriteErrMsg("操作失败！", "Advertisement.aspx?AdId=" + advertisementInfo.ADId + "&Action=Modify");
                        return;
                }
                BasePage.ResponseRedirect("AdManage.aspx");
            }
        }

        private string GetAdZoneIdList()
        {
            StringBuilder sb = new StringBuilder();
            int count = this.LstZoneName.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.LstZoneName.Items[i].Selected)
                {
                    StringHelper.AppendString(sb, this.LstZoneName.Items[i].Value);
                }
            }
            return sb.ToString();
        }

        private void InitJSScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=text/javascript>\n");
            builder.Append("function Change_ADType()\n");
            builder.Append("{\n");
            builder.Append("  for (var j=1;j<=document.getElementsByName('" + this.RadlADType.UniqueID + "').length;j++){\n");
            builder.Append(" var ot = eval(document.getElementById('" + this.ADContent.ClientID + "_'+ j +''));\n");
            builder.Append("if(document.getElementById('" + this.RadlADType.ClientID + "_'+(j-1)+'').checked){\n");
            builder.Append("ot.style.display = '';\n");
            builder.Append(" if(j==1){\n");
            builder.Append("document.getElementById('" + this.ChkCountClick.ClientID + "').disabled = false;\n");
            builder.Append("document.getElementById('" + this.TxtClicks.ClientID + "').disabled = false;\n");
            builder.Append(" }else{\n");
            builder.Append("document.getElementById('" + this.ChkCountClick.ClientID + "').disabled = true;\n");
            builder.Append("document.getElementById('" + this.TxtClicks.ClientID + "').disabled = true;\n");
            builder.Append(" }\n");
            builder.Append(" }else{\n");
            builder.Append(" ot.style.display = 'none';\n");
            builder.Append(" }\n");
            builder.Append(" }\n");
            builder.Append(" }\n");
            builder.Append("function ADTypeChecked(i)\n");
            builder.Append("{\n ");
            builder.Append("document.getElementById('" + this.RadlADType.ClientID + "_'+(i-1)+'').checked=true;");
            builder.Append("Change_ADType();\n");
            builder.Append("}\n");
            builder.Append("function CheckForm()\n");
            builder.Append("{\n");
            builder.Append("if(document.getElementById('" + this.TxtADName.ClientID + "').value==''){");
            builder.Append(" alert('广告名称不能为空！');");
            builder.Append("document.getElementById('" + this.TxtADName.ClientID + "').focus();");
            builder.Append("return false;");
            builder.Append("}\n");
            builder.Append("if(!CheckUploadFile()){return false;}");
            builder.Append("if(document.getElementById('" + this.RadlADType.ClientID + "_2').checked && document.getElementById('" + this.TxtADText.ClientID + "').value==''){");
            builder.Append("alert('广告文本不能为空！');");
            builder.Append("document.getElementById('" + this.TxtADText.ClientID + "').focus();");
            builder.Append(" return false;");
            builder.Append(" }\n");
            builder.Append("if(document.getElementById('" + this.RadlADType.ClientID + "_3').checked && document.getElementById('" + this.TxtADCode.ClientID + "').value==''){");
            builder.Append("alert('广告代码不能为空！');");
            builder.Append("document.getElementById('" + this.TxtADCode.ClientID + "').focus();");
            builder.Append("return false;");
            builder.Append("}\n");
            builder.Append("if(document.getElementById('" + this.TxtPriority.ClientID + "').value==''){");
            builder.Append(" alert('广告权重不能为空！');");
            builder.Append(" document.getElementById('" + this.TxtPriority.ClientID + "').focus();");
            builder.Append("return false;");
            builder.Append("}\n");
            builder.Append("}\n");
            builder.Append("</script>\n");
            if (!base.ClientScript.IsClientScriptBlockRegistered("ClientScript"))
            {
                base.ClientScript.RegisterClientScriptBlock(base.GetType(), "ClientScript", builder.ToString());
            }
            this.EBtnSubmit.Attributes.Add("OnClick", "javascript:return CheckForm();");
        }

        private void InitLstZoneName()
        {
            this.LstZoneName.DataSource = ADZone.GetADZoneList(0, 0);
            this.LstZoneName.DataTextField = "ZoneName";
            this.LstZoneName.DataValueField = "ZoneId";
            this.LstZoneName.DataBind();
        }

        private void InitRadlAdType(int seleced)
        {
            ArrayList aDType = Advertisement.GetADType();
            for (int i = 1; i <= aDType.Count; i++)
            {
                ListItem item = new ListItem();
                item.Value = i.ToString();
                item.Text = aDType[i - 1].ToString();
                item.Attributes.Add("OnClick", "ADTypeChecked(" + i + ")");
                if (seleced == i)
                {
                    item.Selected = true;
                    if (seleced == 1)
                    {
                        this.ADContent_1.Attributes.Add("style", "display: ");
                    }
                }
                this.RadlADType.Items.Add(item);
            }
        }

        private void InitShowPanel(AdvertisementInfo advertisementInfo)
        {
            switch (advertisementInfo.ADType)
            {
                case 1:
                    this.ADContent_1.Attributes.Add("style", "display: ");
                    this.FileUploadControl1.FilePath = advertisementInfo.ImgUrl;
                    this.TxtImgWidth.Text = advertisementInfo.ImgWidth.ToString();
                    this.TxtImgHeight.Text = advertisementInfo.ImgHeight.ToString();
                    this.TxtLinkUrl.Text = advertisementInfo.LinkUrl;
                    this.TxtLinkAlt.Text = advertisementInfo.LinkAlt;
                    BasePage.SetSelectedIndexByValue(this.RadlLinkTarget, advertisementInfo.LinkTarget.ToString());
                    this.TxtADIntro.Text = advertisementInfo.ADIntro;
                    return;

                case 2:
                    this.ADContent_2.Attributes.Add("style", "display: ");
                    this.ExtenFileUpload.FilePath = advertisementInfo.ImgUrl;
                    this.TxtFlashWidth.Text = advertisementInfo.ImgWidth.ToString();
                    this.TxtFlashHeight.Text = advertisementInfo.ImgHeight.ToString();
                    BasePage.SetSelectedIndexByValue(this.RadlFlashMode, advertisementInfo.FlashWmode.ToString());
                    return;

                case 3:
                    this.ADContent_3.Attributes.Add("style", "display: ");
                    this.TxtADText.Text = advertisementInfo.ADIntro;
                    return;

                case 4:
                    this.ADContent_4.Attributes.Add("style", "display: ");
                    this.TxtADCode.Text = advertisementInfo.ADIntro;
                    return;

                case 5:
                    this.ADContent_5.Attributes.Add("style", "display: ");
                    this.TxtWebFileUrl.Text = advertisementInfo.ADIntro;
                    return;
            }
            this.ADContent_1.Attributes.Add("style", "display: ");
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
                if (BasePage.RequestString("Action") == "Modify")
                {
                    int id = BasePage.RequestInt32("AdId");
                    AdvertisementInfo advertisementInfo = new AdvertisementInfo();
                    advertisementInfo = Advertisement.GetAdvertisementById(id);
                    this.TxtADName.Text = advertisementInfo.ADName;
                    this.TxtPriority.Text = advertisementInfo.Priority.ToString();
                    this.DpkOverdueDate.Text = advertisementInfo.OverdueDate.ToString("yyyy-MM-dd");
                    this.InitRadlAdType(advertisementInfo.ADType);
                    this.InitShowPanel(advertisementInfo);
                    this.HdnAction.Value = "Modify";
                    this.HdnAdId.Value = advertisementInfo.ADId.ToString();
                    this.InitLstZoneName();
                    this.SetLstZoneNameSelected(advertisementInfo.ZoneId);
                    this.ChkCountClick.Checked = advertisementInfo.CountClick;
                    this.ChkCountView.Checked = advertisementInfo.CountView;
                    this.TxtClicks.Text = advertisementInfo.Clicks.ToString();
                    this.TxtViews.Text = advertisementInfo.Views.ToString();
                    this.InitJSScript();
                    if (advertisementInfo.ADType > 1)
                    {
                        this.ChkCountClick.Enabled = false;
                        this.TxtClicks.Enabled = false;
                    }
                    this.ChkPassed.Checked = advertisementInfo.Passed;
                }
                else
                {
                    this.InitLstZoneName();
                    this.DpkOverdueDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(BasePage.RequestString("ZoneId")))
                    {
                        string ids = BasePage.RequestString("ZoneId") + ",";
                        this.SetLstZoneNameSelected(ids);
                    }
                    this.InitJSScript();
                    this.InitRadlAdType(1);
                }
            }
        }

        private void SetLstZoneNameSelected(string Ids)
        {
            string[] strArray = Ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                for (int j = 0; j < this.LstZoneName.Items.Count; j++)
                {
                    if (strArray[i] == this.LstZoneName.Items[j].Value)
                    {
                        this.LstZoneName.Items[j].Selected = true;
                    }
                }
            }
        }
    }
}

