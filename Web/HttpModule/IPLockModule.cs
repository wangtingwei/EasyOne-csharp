namespace EasyOne.Web.HttpModule
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Web;
    using System;
    using System.Web;
    /// <summary>
    /// IP访问限制类
    /// </summary>
    public class IPLockModule : IHttpModule
    {
        private void Application_BeginRequest(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication) source;
            HttpContext context = application.Context;
            if ((application != null) && (context != null))
            {
                double userIP = StringHelper.EncodeIP(PEContext.Current.UserHostAddress);
                int lockType = DataConverter.CLng(SiteConfig.IPLockConfig.LockIPType);
                string lockIPWhite = SiteConfig.IPLockConfig.LockIPWhite;
                string lockIPBlack = SiteConfig.IPLockConfig.LockIPBlack;
                int lockAdminType = DataConverter.CLng(SiteConfig.IPLockConfig.AdminLockIPType);
                string adminLockIPBlack = SiteConfig.IPLockConfig.AdminLockIPBlack;
                string adminLockIPWhite = SiteConfig.IPLockConfig.AdminLockIPWhite;
                if (CheckIPlock(lockType, lockIPWhite, lockIPBlack, userIP))
                {
                    EasyOne.Web.Utility.WriteErrMsg(EasyOne.Web.Utility.GetGlobalErrorString("IPRefusedVisit"), string.Empty);
                }
                else if (IsRefusedVisitAdminPage(context, lockAdminType, adminLockIPWhite, adminLockIPBlack, userIP))
                {
                    EasyOne.Web.Utility.WriteErrMsg(EasyOne.Web.Utility.GetGlobalErrorString("IPRefusedVisitAdmin"), string.Empty);
                }
            }
        }
        /// <summary>
        /// IP访问限制设定
        /// </summary>
        /// <param name="lockType">IP访问限定方式</param>
        /// <param name="lockWhiteList">白名单列表</param>
        /// <param name="lockBlackList">黑名单列表</param>
        /// <param name="userIP">用户机器IP地址</param>
        /// <returns></returns>
        private static bool CheckIPlock(int lockType, string lockWhiteList, string lockBlackList, double userIP)
        {
            bool flag = false;
            if ((lockType == 0) || (userIP == 0.0))
            {
                return flag;
            }
            if (lockType == 4)
            {
                flag = GetAnalysisResult(lockBlackList, 1, userIP);
                if (flag)
                {
                    flag = GetAnalysisResult(lockWhiteList, 0, userIP);
                }
                return flag;
            }
            if ((lockType == 1) || (lockType == 3))
            {
                flag = GetAnalysisResult(lockWhiteList, 0, userIP);
            }
            if (flag || ((lockType != 2) && (lockType != 3)))
            {
                return flag;
            }
            return GetAnalysisResult(lockBlackList, 1, userIP);
        }

        public void Dispose()
        {
        }
        /// <summary>
        /// 得到IP判定结果
        /// </summary>
        /// <param name="lockList"></param>
        /// <param name="ipType"></param>
        /// <param name="userIP"></param>
        /// <returns></returns>
        private static bool GetAnalysisResult(string lockList, int ipType, double userIP)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(lockList))
            {
                //返回不含空格的指定的字符串
                string[] strArray = lockList.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strArray[i]))
                    {
                        try
                        {
                            bool flag2;
                            string[] strArray2 = strArray[i].Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                            if (ipType == 0)
                            {
                                flag = true;
                                flag2 = (DataConverter.CDouble(strArray2[0]) <= userIP) && (userIP <= DataConverter.CDouble(strArray2[1]));
                            }
                            else
                            {
                                flag2 = (DataConverter.CDouble(strArray2[0]) <= userIP) && (userIP <= DataConverter.CDouble(strArray2[1]));
                            }
                            if (flag2)
                            {
                                return (ipType == 1);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            return false;
                        }
                    }
                }
            }
            return flag;
        }

        public void Init(HttpApplication context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            context.BeginRequest += new EventHandler(this.Application_BeginRequest);
        }
        /// <summary>
        /// 判断是否拒绝访问管理员页面
        /// </summary>
        /// <param name="context"></param>
        /// <param name="lockAdminType"></param>
        /// <param name="lockAdminWhiteList"></param>
        /// <param name="lockAdminBlackList"></param>
        /// <param name="userIP"></param>
        /// <returns></returns>
        private static bool IsRefusedVisitAdminPage(HttpContext context, int lockAdminType, string lockAdminWhiteList, string lockAdminBlackList, double userIP)
        {
            string absolutePath = context.Request.Url.AbsolutePath;
            string str2 = EasyOne.Web.Utility.GetBasePath(context.Request) + SiteConfig.SiteOption.ManageDir + "/";
            return (absolutePath.StartsWith(str2, StringComparison.CurrentCultureIgnoreCase) && CheckIPlock(lockAdminType, lockAdminWhiteList, lockAdminBlackList, userIP));
        }
    }
}

