namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.ExtendedControls;
    using EasyOne.Model.UserManage;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class UserIndividuation : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (BaseUserControl.RequestInt32("GroupId") == -2)
                {
                    this.ChkManageSelfPublicInfo.Visible = false;
                    this.ChkSetToNotCheck.Visible = false;
                    this.ChkSetEditor.Visible = false;
                    this.LblMaxPublicInfoOneDay.Visible = false;
                    this.LblGetExp.Visible = false;
                    this.LblManageSelfPublicInfo.Visible = false;
                    this.LblSetToNotCheck.Visible = false;
                    this.LblSetEditor.Visible = false;
                    this.LblMaxPublicInfoOneDay.Visible = false;
                    this.LblMaxPublicInfoOneDay2.Visible = false;
                    this.LblGetExp.Visible = false;
                    this.LblGetExp2.Visible = false;
                    this.TxtMaxPublicInfoOneDay.Visible = false;
                    this.TxtGetExp.Visible = false;
                    this.PnlComment.Visible = false;
                    this.PnlMessage.Visible = false;
                    this.PnlFavorite.Visible = false;
                    this.PnlShop.Visible = false;
                    this.PnlCharge.Visible = false;
                    this.PnlMinusPoint.Visible = false;
                    this.PnlEnableExchange.Visible = false;
                }
                else
                {
                    this.PnlShop.Visible = BaseUserControl.IseShop;
                }
                if (!SiteConfig.SiteOption.EnablePointMoneyExp)
                {
                    this.LblGetExp.Visible = false;
                    this.TxtGetExp.Visible = false;
                    this.LblGetExp2.Visible = false;
                    this.PnlCharge.Style.Add("display", "none");
                    this.PnlMinusPoint.Style.Add("display", "none");
                    this.PnlEnableExchange.Style.Add("display", "none");
                }
            }
            if (this.PublicInfoNoNeedCheck)
            {
                this.SpanSetToNotCheck.Style.Add("display", "none");
            }
            this.ChkPublicInfoNoNeedCheck.Attributes.Add("onclick", "SwicthPublicInfoNoNeedCheck(this);");
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("function SwicthPublicInfoNoNeedCheck(obj)");
            builder.Append("{");
            builder.Append("if(obj.checked){");
            builder.Append("document.getElementById(\"" + this.SpanSetToNotCheck.ClientID + "\").style.display = \"none\";");
            builder.Append("}else{");
            builder.Append("document.getElementById(\"" + this.SpanSetToNotCheck.ClientID + "\").style.display = \"\";");
            builder.Append("}}</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "SwicthPublicInfoNoNeedCheck", builder.ToString());
        }

        public bool ChargeByPoint
        {
            get
            {
                return this.RadChargeByPoint.Checked;
            }
            set
            {
                this.RadChargeByPoint.Checked = value;
            }
        }

        public bool ChargeByPointAndValidDate
        {
            get
            {
                return this.RadChargeByPointAndValidDate.Checked;
            }
            set
            {
                this.RadChargeByPointAndValidDate.Checked = value;
            }
        }

        public bool ChargeByPointOrValidDate
        {
            get
            {
                return this.RadChargeByPointOrValidDate.Checked;
            }
            set
            {
                this.RadChargeByPointOrValidDate.Checked = value;
            }
        }

        public bool ChargeByValidDate
        {
            get
            {
                return this.RadChargeByValidDate.Checked;
            }
            set
            {
                this.RadChargeByValidDate.Checked = value;
            }
        }

        public bool CommentNeedCheck
        {
            get
            {
                return this.ChkCommentNeedCheck.Checked;
            }
            set
            {
                this.ChkCommentNeedCheck.Checked = value;
            }
        }

        public double Discount
        {
            get
            {
                return DataConverter.CDouble(this.TxtDiscount.Text);
            }
            set
            {
                this.TxtDiscount.Text = value.ToString();
            }
        }

        public bool EnableBuyPoint
        {
            get
            {
                return this.ChkEnableBuyPoint.Checked;
            }
            set
            {
                this.ChkEnableBuyPoint.Checked = value;
            }
        }

        public bool EnableComment
        {
            get
            {
                return this.ChkEnableComment.Checked;
            }
            set
            {
                this.ChkEnableComment.Checked = value;
            }
        }

        public bool EnableExchangePoint
        {
            get
            {
                return this.ChkEnableExchangePoint.Checked;
            }
            set
            {
                this.ChkEnableExchangePoint.Checked = value;
            }
        }

        public bool EnableExchangeValidDate
        {
            get
            {
                return this.ChkEnableExchangeValidDate.Checked;
            }
            set
            {
                this.ChkEnableExchangeValidDate.Checked = value;
            }
        }

        public bool EnableGivePointToOthers
        {
            get
            {
                return this.ChkEnableGivePointToOthers.Checked;
            }
            set
            {
                this.ChkEnableGivePointToOthers.Checked = value;
            }
        }

        public bool Enablepm
        {
            get
            {
                return this.ChkEnablePm.Checked;
            }
            set
            {
                this.ChkEnablePm.Checked = value;
            }
        }

        public bool EnableUpload
        {
            get
            {
                return this.ChkEnableUpload.Checked;
            }
            set
            {
                this.ChkEnableUpload.Checked = value;
            }
        }

        public int GetExp
        {
            get
            {
                return DataConverter.CLng(this.TxtGetExp.Text);
            }
            set
            {
                this.TxtGetExp.Text = value.ToString();
            }
        }

        public bool IsXssFilter
        {
            get
            {
                return this.ChkIsXssFilter.Checked;
            }
            set
            {
                this.ChkIsXssFilter.Checked = value;
            }
        }

        public bool ManageSelfPublicInfo
        {
            get
            {
                return this.ChkManageSelfPublicInfo.Checked;
            }
            set
            {
                this.ChkManageSelfPublicInfo.Checked = value;
            }
        }

        public int MaxPublicInfoOneDay
        {
            get
            {
                return DataConverter.CLng(this.TxtMaxPublicInfoOneDay.Text);
            }
            set
            {
                this.TxtMaxPublicInfoOneDay.Text = value.ToString();
            }
        }

        public int MaxSaveInfos
        {
            get
            {
                return DataConverter.CLng(this.TxtMaxSaveInfos.Text);
            }
            set
            {
                this.TxtMaxSaveInfos.Text = value.ToString();
            }
        }

        public int MaxSendToUsers
        {
            get
            {
                return DataConverter.CLng(this.TxtMaxSendToUsers.Text);
            }
            set
            {
                this.TxtMaxSendToUsers.Text = value.ToString();
            }
        }

        public bool MinusPoint
        {
            get
            {
                return this.RadMinusPoint.Checked;
            }
            set
            {
                this.RadMinusPoint.Checked = value;
            }
        }

        public bool NotMinusPointNotWriteToLog
        {
            get
            {
                return this.RadNotMinusPointNotWriteToLog.Checked;
            }
            set
            {
                this.RadNotMinusPointNotWriteToLog.Checked = value;
            }
        }

        public double Overdraft
        {
            get
            {
                return DataConverter.CDouble(this.TxtOverdraft.Text);
            }
            set
            {
                this.TxtOverdraft.Text = value.ToString();
            }
        }

        public bool PublicInfoNoNeedCheck
        {
            get
            {
                return this.ChkPublicInfoNoNeedCheck.Checked;
            }
            set
            {
                this.ChkPublicInfoNoNeedCheck.Checked = value;
            }
        }

        public UserPurviewInfo PurviewInfo
        {
            get
            {
                UserPurviewInfo info = new UserPurviewInfo();
                info.PublicInfoNoNeedCheck = this.PublicInfoNoNeedCheck;
                info.ManageSelfPublicInfo = this.ManageSelfPublicInfo;
                info.SetToNotCheck = this.SetToNotCheck;
                info.SetEditor = this.SetEditor;
                info.MaxPublicInfoOneDay = this.MaxPublicInfoOneDay;
                info.GetExp = this.GetExp;
                info.IsXssFilter = this.IsXssFilter;
                info.EnableComment = this.EnableComment;
                info.CommentNeedCheck = this.CommentNeedCheck;
                info.MaxSendToUsers = this.MaxSendToUsers;
                info.MaxSaveInfos = this.MaxSaveInfos;
                info.EnableUpload = this.EnableUpload;
                info.UploadSize = this.UploadSize;
                info.SetEnableSale = this.SetEnableSale;
                info.Discount = this.Discount;
                info.Overdraft = this.Overdraft;
                info.Enablepm = this.Enablepm;
                info.ChargeByPoint = this.ChargeByPoint;
                info.ChargeByValidDate = this.ChargeByValidDate;
                info.ChargeByPointOrValidDate = this.ChargeByPointOrValidDate;
                info.ChargeByPointAndValidDate = this.ChargeByPointAndValidDate;
                info.NotMinusPointNotWriteToLog = this.NotMinusPointNotWriteToLog;
                info.WriteToLog = this.WriteToLog;
                info.MinusPoint = this.MinusPoint;
                info.TotalViewInfoNumber = this.TotalViewInfoNumber;
                info.ViewInfoNumberOneDay = this.ViewInfoNumberOneDay;
                info.EnableExchangePoint = this.EnableExchangePoint;
                info.EnableExchangeValidDate = this.EnableExchangeValidDate;
                info.EnableGivePointToOthers = this.EnableGivePointToOthers;
                info.EnableBuyPoint = this.EnableBuyPoint;
                return info;
            }
            set
            {
                this.PublicInfoNoNeedCheck = value.PublicInfoNoNeedCheck;
                this.ManageSelfPublicInfo = value.ManageSelfPublicInfo;
                this.SetToNotCheck = value.SetToNotCheck;
                this.SetEditor = value.SetEditor;
                this.MaxPublicInfoOneDay = value.MaxPublicInfoOneDay;
                this.GetExp = value.GetExp;
                this.IsXssFilter = value.IsXssFilter;
                this.EnableComment = value.EnableComment;
                this.CommentNeedCheck = value.CommentNeedCheck;
                this.MaxSendToUsers = value.MaxSendToUsers;
                this.MaxSaveInfos = value.MaxSaveInfos;
                this.EnableUpload = value.EnableUpload;
                this.UploadSize = value.UploadSize;
                this.Discount = value.Discount;
                this.SetEnableSale = value.SetEnableSale;
                this.Overdraft = value.Overdraft;
                this.Enablepm = value.Enablepm;
                this.ChargeByPoint = value.ChargeByPoint;
                this.ChargeByValidDate = value.ChargeByValidDate;
                this.ChargeByPointOrValidDate = value.ChargeByPointOrValidDate;
                this.ChargeByPointAndValidDate = value.ChargeByPointAndValidDate;
                this.NotMinusPointNotWriteToLog = value.NotMinusPointNotWriteToLog;
                this.WriteToLog = value.WriteToLog;
                this.MinusPoint = value.MinusPoint;
                this.TotalViewInfoNumber = value.TotalViewInfoNumber;
                this.ViewInfoNumberOneDay = value.ViewInfoNumberOneDay;
                this.EnableExchangePoint = value.EnableExchangePoint;
                this.EnableExchangeValidDate = value.EnableExchangeValidDate;
                this.EnableGivePointToOthers = value.EnableGivePointToOthers;
                this.EnableBuyPoint = value.EnableBuyPoint;
            }
        }

        public bool SetEditor
        {
            get
            {
                return this.ChkSetEditor.Checked;
            }
            set
            {
                this.ChkSetEditor.Checked = value;
            }
        }

        public bool SetEnableSale
        {
            get
            {
                return this.ChkSetEnableSale.Checked;
            }
            set
            {
                this.ChkSetEnableSale.Checked = value;
            }
        }

        public bool SetToNotCheck
        {
            get
            {
                return this.ChkSetToNotCheck.Checked;
            }
            set
            {
                this.ChkSetToNotCheck.Checked = value;
            }
        }

        public int TotalViewInfoNumber
        {
            get
            {
                return DataConverter.CLng(this.TxtTotalViewInfoNumber.Text);
            }
            set
            {
                this.TxtTotalViewInfoNumber.Text = value.ToString();
            }
        }

        public int UploadSize
        {
            get
            {
                return DataConverter.CLng(this.TxtFileUploadSize.Text);
            }
            set
            {
                this.TxtFileUploadSize.Text = value.ToString();
            }
        }

        public int ViewInfoNumberOneDay
        {
            get
            {
                return DataConverter.CLng(this.TxtViewInfoNumberOneDay.Text);
            }
            set
            {
                this.TxtViewInfoNumberOneDay.Text = value.ToString();
            }
        }

        public bool WriteToLog
        {
            get
            {
                return this.RadWriteToLog.Checked;
            }
            set
            {
                this.RadWriteToLog.Checked = value;
            }
        }
    }
}

