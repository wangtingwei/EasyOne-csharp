namespace EasyOne.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Globalization;
    using System.Web;

    public sealed class ChargeManage
    {
        private CommonModelInfo m_CommonModelInfo;
        private ContentChargeInfo m_contentChargeInfo;
        private string m_ErrMsg = "";
        private string m_errMsg_NeedPoint = GetGlobalString("NeedPoint");
        private string m_errMsg_NoCheck = GetGlobalString("NoCheck");
        private string m_errMsg_NoLogin = GetGlobalString("NoLogin");
        private string m_errMsg_NoMail = GetGlobalString("NoMail");
        private string m_errMsg_OutTime = GetGlobalString("OutTime");
        private string m_errMsg_Overflow_Today = GetGlobalString("Overflow_Today");
        private string m_errMsg_Overflow_Total = GetGlobalString("Overflow_Total");
        private string m_errMsg_PurviewCheckedErr = GetGlobalString("PurviewCheckedErr");
        private string m_errMsg_PurviewCheckedErr2 = GetGlobalString("PurviewCheckedErr2");
        private string m_errMsg_UsePoint = GetGlobalString("UsePoint");
        private ModelInfo m_ModelInfo;
        private NodeInfo m_NodeInfo;
        private string m_Pay;
        private UserInfo m_UserInfo;
        private string m_UserTrueIP;

        public ChargeManage()
        {
            int generalId = DataConverter.CLng(HttpContext.Current.Request["id"]);
            this.m_UserInfo = PEContext.Current.User.UserInfo;
            this.m_CommonModelInfo = ContentManage.GetCommonModelInfoById(generalId);
            this.m_ModelInfo = ModelManager.GetModelInfoById(this.m_CommonModelInfo.ModelId);
            this.m_NodeInfo = Nodes.GetCacheNodeById(this.m_CommonModelInfo.NodeId);
            this.m_contentChargeInfo = ContentCharge.GetContentChargeInfoById(this.m_CommonModelInfo.GeneralId);
            this.m_UserTrueIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(this.m_UserTrueIP))
            {
                this.m_UserTrueIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            this.m_UserTrueIP = DataSecurity.FilterBadChar(this.m_UserTrueIP);
            this.m_Pay = HttpContext.Current.Request["Pay"];
            this.m_errMsg_NoLogin = this.m_errMsg_NoLogin.Replace("{$InstallDir}", SiteConfig.SiteInfo.VirtualPath);
            this.m_errMsg_NoLogin = this.m_errMsg_NoLogin.Replace("{$ItemName}", this.m_ModelInfo.ItemName);
            this.m_errMsg_NoLogin = this.m_errMsg_NoLogin.Replace("{$ReturnUrl}", DataSecurity.HtmlEncode(HttpContext.Current.Request.RawUrl).Replace("&", "%26"));
            this.m_errMsg_OutTime = this.m_errMsg_OutTime.Replace("{$ItemName}", this.m_ModelInfo.ItemName);
            this.m_errMsg_NeedPoint = this.m_errMsg_NeedPoint.Replace("{$ItemName}", this.m_ModelInfo.ItemName);
            this.m_errMsg_NeedPoint = this.m_errMsg_NeedPoint.Replace("{$InfoPoint}", this.m_contentChargeInfo.InfoPoint.ToString());
            this.m_errMsg_NeedPoint = this.m_errMsg_NeedPoint.Replace("{$NeedPoint}", this.m_contentChargeInfo.InfoPoint.ToString());
            this.m_errMsg_NeedPoint = this.m_errMsg_NeedPoint.Replace("{$NowPoint}", this.m_UserInfo.UserPoint.ToString());
            this.m_errMsg_NeedPoint = this.m_errMsg_NeedPoint.Replace("{$PointName}", SiteConfig.UserConfig.PointName);
            this.m_errMsg_NeedPoint = this.m_errMsg_NeedPoint.Replace("{$PointUnit}", SiteConfig.UserConfig.PointUnit);
            this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$ItemName}", this.m_ModelInfo.ItemName);
            this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$InfoPoint}", this.m_contentChargeInfo.InfoPoint.ToString());
            this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$NowPoint}", this.m_UserInfo.UserPoint.ToString());
            this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$FinalPoint}", (this.m_UserInfo.UserPoint - this.m_contentChargeInfo.InfoPoint).ToString());
            if (HttpContext.Current.Request.RawUrl.Contains("?"))
            {
                this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$url}", HttpContext.Current.Request.RawUrl + "&Pay=yes");
            }
            else
            {
                this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$url}", HttpContext.Current.Request.RawUrl + "?Pay=yes");
            }
            this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$InstallDir}", SiteConfig.SiteInfo.VirtualPath);
            this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$PointName}", SiteConfig.UserConfig.PointName);
            this.m_errMsg_UsePoint = this.m_errMsg_UsePoint.Replace("{$PointUnit}", SiteConfig.UserConfig.PointUnit);
        }

        private UserPointLogInfo AddPointLogInfo()
        {
            UserPointLogInfo info = new UserPointLogInfo();
            info.UserName = this.m_UserInfo.UserName;
            info.InfoId = this.m_CommonModelInfo.GeneralId;
            info.IncomePayOut = 2;
            info.ModuleType = this.m_ModelInfo.ModelId;
            info.LogTime = DateTime.Now;
            info.IP = this.m_UserTrueIP;
            return info;
        }

        private bool CheckIsAuthenticated()
        {
            bool flag = true;
            if (!PEContext.Current.User.Identity.IsAuthenticated)
            {
                this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_NoLogin.Replace("{$ReturnUrl}", DataSecurity.HtmlEncode(HttpContext.Current.Request.RawUrl).Replace("&", "%26"));
                flag = false;
            }
            return flag;
        }

        public bool CheckPermission()
        {
            int permissionType;
            bool flag = true;
            ContentPermissionInfo contentPermissionInfoById = PermissionContent.GetContentPermissionInfoById(DataConverter.CLng(HttpContext.Current.Request["id"]));
            if (contentPermissionInfoById == null)
            {
                permissionType = 0;
            }
            else
            {
                permissionType = contentPermissionInfoById.PermissionType;
            }
            switch (permissionType)
            {
                case 0:
                    switch (this.m_NodeInfo.PurviewType)
                    {
                        case 1:
                        case 2:
                            if (!this.CheckIsAuthenticated())
                            {
                                flag = false;
                            }
                            if (string.IsNullOrEmpty(this.m_ErrMsg))
                            {
                                if (this.m_NodeInfo.ParentId > 0)
                                {
                                    if (!UserPermissions.AccessCheck(OperateCode.NodeContentPreview, this.m_NodeInfo.ParentPath + "," + this.m_NodeInfo.NodeId))
                                    {
                                        flag = false;
                                    }
                                }
                                else if (!UserPermissions.AccessCheck(OperateCode.NodeContentPreview, this.m_NodeInfo.NodeId))
                                {
                                    flag = false;
                                }
                                if (!flag)
                                {
                                    this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_PurviewCheckedErr;
                                }
                            }
                            return flag;
                    }
                    return flag;

                case 1:
                    if (!this.CheckIsAuthenticated())
                    {
                        flag = false;
                    }
                    return flag;

                case 2:
                    if (!this.CheckIsAuthenticated())
                    {
                        flag = false;
                    }
                    if (!StringHelper.FoundCharInArr(contentPermissionInfoById.ArrGroupId, PEContext.Current.User.GroupId.ToString()))
                    {
                        this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_PurviewCheckedErr2;
                        flag = false;
                    }
                    return flag;
            }
            return flag;
        }

        private void DividePoint(UserPointLogInfo userPointLogInfo)
        {
            int infoPoint = DataConverter.CLng((this.m_contentChargeInfo.InfoPoint * this.m_contentChargeInfo.DividePercent) / 100);
            if (Users.AddPoint(infoPoint, this.m_CommonModelInfo.Inputer))
            {
                userPointLogInfo.UserName = this.m_CommonModelInfo.Inputer;
                userPointLogInfo.InfoId = 0;
                userPointLogInfo.Point = infoPoint;
                userPointLogInfo.IncomePayOut = 1;
                userPointLogInfo.Remark = "从“" + this.m_CommonModelInfo.Title + "”的收费中分成";
                UserPointLog.Add(userPointLogInfo);
            }
        }

        public bool ExecuteContentCharge()
        {
            object obj2 = HttpContext.Current.Items["IsPay"];
            if ((obj2 != null) && ((bool) obj2))
            {
                return true;
            }
            if ((this.m_contentChargeInfo.InfoPoint <= 0) || (this.m_contentChargeInfo.InfoPoint > 0x270f))
            {
                return true;
            }
            if ((this.m_UserInfo.Status & UserStatus.WaitValidateByEmail) == UserStatus.WaitValidateByEmail)
            {
                this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_NoMail;
                return false;
            }
            if ((this.m_UserInfo.Status & UserStatus.WaitValidateByAdmin) == UserStatus.WaitValidateByAdmin)
            {
                this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_NoCheck;
                return false;
            }
            if (!this.CheckIsAuthenticated())
            {
                return false;
            }
            UserPurviewInfo purviewInfo = PEContext.Current.User.PurviewInfo;
            if (purviewInfo.ChargeByPoint)
            {
                this.Pointfirst();
            }
            else
            {
                int num = 0;
                if (this.m_UserInfo.EndTime.HasValue)
                {
                    TimeSpan span = (TimeSpan) (Convert.ToDateTime(this.m_UserInfo.EndTime) - DateTime.Now.Date);
                    num = Convert.ToInt32(Convert.ToDecimal(span.TotalDays));
                }
                if (num <= 0)
                {
                    this.OverdueDisposal(purviewInfo);
                }
                else
                {
                    this.InValidate(purviewInfo);
                }
            }
            return string.IsNullOrEmpty(this.m_ErrMsg);
        }

        public static string GetGlobalString(string resourceKey)
        {
            string str = (string) HttpContext.GetGlobalResourceObject("ChargeResources", resourceKey, CultureInfo.CurrentCulture);
            if (str == null)
            {
                str = string.Empty;
            }
            return str;
        }

        private void InValidate(UserPurviewInfo userPurviewInfo)
        {
            if (!userPurviewInfo.NotMinusPointNotWriteToLog)
            {
                int logId = UserPointLog.GetValidPointLogId(this.m_UserInfo.UserName, this.m_ModelInfo.ModelId, this.m_CommonModelInfo.GeneralId, this.m_contentChargeInfo.ChargeType, this.m_contentChargeInfo.PitchTime, this.m_contentChargeInfo.ReadTimes);
                if (logId == 0)
                {
                    if ((userPurviewInfo.TotalViewInfoNumber > 0) && (UserPointLog.ViewTotalInfos(this.m_UserInfo.UserName) >= userPurviewInfo.TotalViewInfoNumber))
                    {
                        this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_Overflow_Total;
                    }
                    else if ((userPurviewInfo.ViewInfoNumberOneDay > 0) && (UserPointLog.ViewInfosOneDay(this.m_UserInfo.UserName) >= userPurviewInfo.ViewInfoNumberOneDay))
                    {
                        this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_Overflow_Today;
                    }
                    else if (string.IsNullOrEmpty(this.m_ErrMsg))
                    {
                        if (userPurviewInfo.WriteToLog)
                        {
                            UserPointLogInfo userPointLogInfo = this.AddPointLogInfo();
                            userPointLogInfo.Point = 0;
                            userPointLogInfo.Remark = string.Concat(new object[] { "有效期内查看收费", this.m_ModelInfo.ItemName, "应扣点数：", this.m_ModelInfo.ItemName, this.m_CommonModelInfo.Title, "，应扣点数：", this.m_contentChargeInfo.InfoPoint });
                            UserPointLog.Add(userPointLogInfo);
                        }
                        else if (this.m_UserInfo.UserPoint <= this.m_contentChargeInfo.InfoPoint)
                        {
                            if (userPurviewInfo.ChargeByPointOrValidDate)
                            {
                                this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_NeedPoint;
                            }
                            else
                            {
                                UserPointLogInfo info3 = this.AddPointLogInfo();
                                info3.Point = 0;
                                info3.Remark = string.Concat(new object[] { "有效期内查看收费", this.m_ModelInfo.ItemName, "应扣点数：", this.m_ModelInfo.ItemName, this.m_CommonModelInfo.Title, "，应扣点数：", this.m_contentChargeInfo.InfoPoint });
                                UserPointLog.Add(info3);
                            }
                        }
                        else if (this.m_Pay != "yes")
                        {
                            this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_UsePoint;
                        }
                        else if (Users.MinusPoint(this.m_contentChargeInfo.InfoPoint, this.m_UserInfo.UserName))
                        {
                            UserPointLogInfo info2 = this.AddPointLogInfo();
                            info2.Point = this.m_contentChargeInfo.InfoPoint;
                            info2.Remark = "有效期内查看收费" + this.m_ModelInfo.ItemName + this.m_CommonModelInfo.Title;
                            UserPointLog.Add(info2);
                            if ((this.m_contentChargeInfo.DividePercent > 0) && (this.m_contentChargeInfo.DividePercent < 100))
                            {
                                this.DividePoint(info2);
                            }
                            HttpContext.Current.Items.Add("IsPay", true);
                        }
                    }
                }
                else
                {
                    UserPointLog.UpdateTimes(this.m_UserTrueIP, logId);
                    HttpContext.Current.Items.Add("IsPay", true);
                }
            }
        }

        private void OverdueDisposal(UserPurviewInfo userPurviewInfo)
        {
            if (userPurviewInfo.ChargeByValidDate || userPurviewInfo.ChargeByPointOrValidDate)
            {
                this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_OutTime;
            }
            else
            {
                int logId = UserPointLog.GetValidPointLogId(this.m_UserInfo.UserName, this.m_ModelInfo.ModelId, this.m_CommonModelInfo.GeneralId, this.m_contentChargeInfo.ChargeType, this.m_contentChargeInfo.PitchTime, this.m_contentChargeInfo.ReadTimes);
                if (logId == 0)
                {
                    if (this.m_UserInfo.UserPoint < this.m_contentChargeInfo.InfoPoint)
                    {
                        this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_NeedPoint;
                    }
                    else if (this.m_Pay != "yes")
                    {
                        this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_UsePoint;
                    }
                    else if (Users.MinusPoint(this.m_contentChargeInfo.InfoPoint, this.m_UserInfo.UserName))
                    {
                        UserPointLogInfo userPointLogInfo = this.AddPointLogInfo();
                        userPointLogInfo.Point = this.m_contentChargeInfo.InfoPoint;
                        userPointLogInfo.Remark = "用于查看收费" + this.m_ModelInfo.ItemName + this.m_CommonModelInfo.Title;
                        UserPointLog.Add(userPointLogInfo);
                        if ((this.m_contentChargeInfo.DividePercent > 0) && (this.m_contentChargeInfo.DividePercent < 100))
                        {
                            this.DividePoint(userPointLogInfo);
                        }
                        HttpContext.Current.Items.Add("IsPay", true);
                    }
                }
                else
                {
                    UserPointLog.UpdateTimes(this.m_UserTrueIP, logId);
                    HttpContext.Current.Items.Add("IsPay", true);
                }
            }
        }

        private void Pointfirst()
        {
            int logId = UserPointLog.GetValidPointLogId(this.m_UserInfo.UserName, this.m_ModelInfo.ModelId, this.m_CommonModelInfo.GeneralId, this.m_contentChargeInfo.ChargeType, this.m_contentChargeInfo.PitchTime, this.m_contentChargeInfo.ReadTimes);
            if (logId == 0)
            {
                if (this.m_UserInfo.UserPoint < this.m_contentChargeInfo.InfoPoint)
                {
                    this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_NeedPoint;
                }
                else if (this.m_Pay != "yes")
                {
                    this.m_ErrMsg = this.m_ErrMsg + this.m_errMsg_UsePoint;
                }
                else if (Users.MinusPoint(this.m_contentChargeInfo.InfoPoint, this.m_UserInfo.UserName))
                {
                    UserPointLogInfo userPointLogInfo = this.AddPointLogInfo();
                    userPointLogInfo.Point = this.m_contentChargeInfo.InfoPoint;
                    userPointLogInfo.Remark = "用于查看收费" + this.m_ModelInfo.ItemName + this.m_CommonModelInfo.Title;
                    UserPointLog.Add(userPointLogInfo);
                    if ((this.m_contentChargeInfo.DividePercent > 0) && (this.m_contentChargeInfo.DividePercent < 100))
                    {
                        this.DividePoint(userPointLogInfo);
                    }
                    HttpContext.Current.Items.Add("IsPay", true);
                }
            }
            else
            {
                UserPointLog.UpdateTimes(this.m_UserTrueIP, logId);
                HttpContext.Current.Items.Add("IsPay", true);
            }
        }

        public string ErrMsg
        {
            get
            {
                return this.m_ErrMsg;
            }
            set
            {
                this.m_ErrMsg = value;
            }
        }
    }
}

