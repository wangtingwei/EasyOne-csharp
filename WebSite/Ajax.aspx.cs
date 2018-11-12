namespace EasyOne.WebSite
{
    using EasyOne.AccessManage;
    using EasyOne.Accessories;
    using EasyOne.Api;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Model.UserManage;
    using EasyOne.Shop;
    using EasyOne.Templates;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using System.Xml;

    public partial class Ajax : BasePage
    {
        public XmlTextWriter XmlResponseWriter;

        private void AddComment(XmlDocument xmlDoc)
        {
            CommentInfo commentInfo = new CommentInfo();
            commentInfo.CommentTitle = GetNodeInnerText(xmlDoc, "//commenttitle");
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                commentInfo.UserName = "游客";
            }
            else
            {
                commentInfo.UserName = PEContext.Current.User.UserName;
            }
            commentInfo.Content = GetNodeInnerText(xmlDoc, "//content");
            commentInfo.Email = GetNodeInnerText(xmlDoc, "//email");
            commentInfo.Face = GetNodeInnerText(xmlDoc, "//face");
            commentInfo.GeneralId = DataConverter.CLng(GetNodeInnerText(xmlDoc, "//gid"));
            commentInfo.NodeId = DataConverter.CLng(GetNodeInnerText(xmlDoc, "//nid"));
            commentInfo.TopicId = DataConverter.CLng(GetNodeInnerText(xmlDoc, "//tid"));
            commentInfo.IsPrivate = DataConverter.CBoolean(GetNodeInnerText(xmlDoc, "//private", "true"));
            commentInfo.Position = DataConverter.CLng(GetNodeInnerText(xmlDoc, "//position"));
            commentInfo.Score = DataConverter.CLng(GetNodeInnerText(xmlDoc, "//score", "3"));
            commentInfo.IP = this.GetClientIP();
            commentInfo.UpdateDateTime = DateTime.Now.ToLocalTime();
            commentInfo.ReplyUserName = GetNodeInnerText(xmlDoc, "//username", "游客");
            int num = string.Compare(commentInfo.UserName, "游客", StringComparison.OrdinalIgnoreCase);
            string str = "";
            NodeInfo cacheNodeById = Nodes.GetCacheNodeById(ContentManage.GetCommonModelInfoById(commentInfo.GeneralId).NodeId);
            commentInfo.Status = cacheNodeById.Settings.CommentNeedCheck;
            UserPurviewInfo userPurview = null;
            if (num != 0)
            {
                userPurview = PEContext.Current.User.UserInfo.UserPurview;
                if (userPurview.CommentNeedCheck)
                {
                    commentInfo.Status = true;
                }
                else
                {
                    commentInfo.Status = !cacheNodeById.Settings.CommentNeedCheck;
                }
            }
            else if (!cacheNodeById.Settings.EnableTouristsComment)
            {
                str = "noTourists";
            }
            else
            {
                commentInfo.Status = !cacheNodeById.Settings.CommentNeedCheck;
            }
            bool enableComment = false;
            bool commentNeedCheck = false;
            if (userPurview != null)
            {
                enableComment = userPurview.EnableComment;
                commentNeedCheck = userPurview.CommentNeedCheck;
            }
            if (string.IsNullOrEmpty(str))
            {
                if (cacheNodeById.Settings.EnableComment || enableComment)
                {
                    if (Comment.Add(commentInfo))
                    {
                        if (commentInfo.Status || commentNeedCheck)
                        {
                            str = "ok";
                        }
                        else
                        {
                            str = "check";
                        }
                    }
                    else
                    {
                        str = "err";
                    }
                }
                else
                {
                    str = "nopurview";
                }
            }
            this.XmlResponseWriter.WriteElementString("status", str);
        }

        private void AddPKZone(XmlDocument xmlDoc)
        {
            int num = DataConverter.CLng(GetNodeInnerText(xmlDoc, "//commentid"));
            if (num > 0)
            {
                CommentPKZoneInfo commentPKZoneInfo = new CommentPKZoneInfo();
                commentPKZoneInfo.CommentId = num;
                commentPKZoneInfo.Content = GetNodeInnerText(xmlDoc, "//content");
                commentPKZoneInfo.Position = DataConverter.CLng(GetNodeInnerText(xmlDoc, "//position"));
                commentPKZoneInfo.IP = this.GetClientIP();
                commentPKZoneInfo.UpdateTime = DateTime.Now;
                if (string.IsNullOrEmpty(PEContext.Current.User.UserName))
                {
                    commentPKZoneInfo.UserName = "匿名发表";
                }
                else
                {
                    commentPKZoneInfo.UserName = PEContext.Current.User.UserName;
                }
                CommentPKZone.Add(commentPKZoneInfo);
            }
            if (num > 0)
            {
                this.XmlResponseWriter.WriteElementString("status", "ok");
            }
            else
            {
                this.XmlResponseWriter.WriteElementString("status", "err");
            }
        }

        private void AdminEditCheck(int generalId)
        {
            string str = "CanNotEdit";
            if (PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                int nodeId = ContentManage.GetCommonModelInfoById(generalId).NodeId;
                bool flag = false;
                if (PEContext.Current.Admin.IsSuperAdmin)
                {
                    flag = true;
                }
                else
                {
                    bool flag2 = false;
                    bool flag3 = false;
                    bool flag4 = false;
                    flag2 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentCheck, -1) || RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentCheck, nodeId);
                    flag3 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, -1) || RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentManage, nodeId);
                    flag4 = RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, -1) || RolePermissions.AccessCheckNodePermission(OperateCode.NodeContentInput, nodeId);
                    if ((flag2 || flag4) || flag3)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    str = "OK";
                }
            }
            this.XmlResponseWriter.WriteElementString("status", str);
        }

        private bool CheckLabel(string name)
        {
            string path = HttpContext.Current.Server.MapPath("~/") + "Config/AjaxLabel.config";
            if (!File.Exists(path))
            {
                return false;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(path);
            }
            catch (XmlException)
            {
                return false;
            }
            return (document.SelectSingleNode("root/label[@name='" + name + "']") != null);
        }

        private string ContentLabelProc(string getlabel, string lrootpath, string lpagename, int lpage)
        {
            if (string.IsNullOrEmpty(getlabel))
            {
                return string.Empty;
            }
            getlabel = getlabel.Replace("{", "<").Replace("}", ">");
            XmlDocument document = new XmlDocument();
            LabelInfo labelInfo = new LabelInfo();
            labelInfo.RootPath = lrootpath;
            labelInfo.PageName = lpagename;
            labelInfo.Page = lpage;
            try
            {
                document.LoadXml(getlabel);
                foreach (XmlAttribute attribute in document.FirstChild.Attributes)
                {
                    labelInfo.OriginalData[attribute.Name] = attribute.Value;
                }
            }
            catch (XmlException exception)
            {
                return ("[err:内容标签" + getlabel.Replace("<", string.Empty).Replace("/>", string.Empty) + "错，原因：" + exception.Message + "]");
            }
            string key = "CK_Label_TransformCacheData_" + labelInfo.OriginalData["id"] + "_" + labelInfo.OriginalData["cacheid"];
            int seconds = DataConverter.CLng(labelInfo.OriginalData["cachetime"]);
            if ((seconds > 0) && (SiteCache.Get(key) != null))
            {
                labelInfo = (LabelInfo) SiteCache.Get(key);
            }
            else
            {
                labelInfo = LabelTransform.GetLabel(labelInfo);
                if (seconds > 0)
                {
                    SiteCache.Insert(key, labelInfo, seconds);
                }
            }
            return labelInfo.LabelContent.ToString();
        }

        private void EnableCheckCodeOfLogOn()
        {
            if (SiteConfig.UserConfig.EnableCheckCodeOfLogOn)
            {
                this.XmlResponseWriter.WriteElementString("status", "yes");
            }
            else
            {
                this.XmlResponseWriter.WriteElementString("status", "no");
            }
        }

        private string GetClientIP()
        {
            string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = PEContext.Current.UserHostAddress;
            }
            return userHostAddress;
        }

        private static string GetNodeInnerText(XmlDocument xmldoc, string nodeName)
        {
            string str = "";
            XmlNode node = xmldoc.DocumentElement.SelectSingleNode(nodeName);
            if ((node != null) && !string.IsNullOrEmpty(node.InnerText))
            {
                str = node.InnerText.Trim();
            }
            return str;
        }

        private static string GetNodeInnerText(XmlDocument xmldoc, string nodeName, string defaultValue)
        {
            string nodeInnerText = GetNodeInnerText(xmldoc, nodeName);
            if (string.IsNullOrEmpty(nodeInnerText))
            {
                nodeInnerText = defaultValue;
            }
            return nodeInnerText;
        }

        private string GetSigninStatus(int generalId)
        {
            if (!PEContext.Current.User.Identity.IsAuthenticated)
            {
                return "NoLogin";
            }
            SignInLogInfo signInLog = SignInLog.GetSignInLog(generalId, PEContext.Current.User.UserName);
            if (signInLog.IsNull)
            {
                return "NoSignin";
            }
            if (signInLog.IsSignIn)
            {
                return "Signined";
            }
            return "NotSignin";
        }

        public void LoginCheck()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                this.XmlResponseWriter.WriteElementString("status", "ok");
                UserPrincipal user = PEContext.Current.User;
                this.XmlResponseWriter.WriteElementString("username", user.UserName);
                this.XmlResponseWriter.WriteElementString("usergroup", user.UserInfo.GroupName);
                this.XmlResponseWriter.WriteElementString("email", user.UserInfo.Email);
                this.XmlResponseWriter.WriteElementString("userid", user.UserId.ToString());
                this.XmlResponseWriter.WriteElementString("logintimes", user.UserInfo.LogOnTimes.ToString());
                this.XmlResponseWriter.WriteElementString("passeitems", user.UserInfo.PassedItems.ToString());
                this.XmlResponseWriter.WriteElementString("sign", user.UserInfo.Sign);
                this.XmlResponseWriter.WriteElementString("exp", user.UserInfo.UserExp.ToString());
                this.XmlResponseWriter.WriteElementString("face", user.UserInfo.UserFace);
                this.XmlResponseWriter.WriteElementString("point", user.UserInfo.UserPoint.ToString());
                this.XmlResponseWriter.WriteElementString("pointunit", SiteConfig.UserConfig.PointUnit);
                this.XmlResponseWriter.WriteElementString("msg", Message.UnreadMessageCount(user.UserName).ToString());
                this.XmlResponseWriter.WriteElementString("balance", user.UserInfo.Balance.ToString());
                this.XmlResponseWriter.WriteElementString("signincontent", SignInLog.GetNotSignInContentCountByUserName(user.UserName).ToString());
            }
            else
            {
                this.XmlResponseWriter.WriteElementString("status", "err");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(base.Request.InputStream);
            }
            catch (XmlException exception)
            {
                string path = "~/Temp/ajaxnote.txt";
                path = HttpContext.Current.Server.MapPath(path);
                if (File.Exists(path))
                {
                    base.Response.Write(File.ReadAllText(path));
                }
                else
                {
                    base.Response.Write(exception.Message);
                }
                return;
            }
            base.Response.Clear();
            base.Response.Buffer = true;
            base.Response.Charset = "utf-8";
            base.Response.AddHeader("contenttype", "text/xml");
            base.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            base.Response.ContentType = "text/xml";
            this.XmlResponseWriter = new XmlTextWriter(HttpContext.Current.Response.OutputStream, Encoding.UTF8);
            this.XmlResponseWriter.Formatting = Formatting.Indented;
            this.XmlResponseWriter.Indentation = 4;
            this.XmlResponseWriter.WriteStartDocument();
            this.XmlResponseWriter.WriteStartElement("root", "");
            string str2 = HttpContext.Current.Items["ErrorStatus"] as string;
            if (!string.IsNullOrEmpty(str2))
            {
                this.XmlResponseWriter.WriteElementString("status", "err");
                this.XmlResponseWriter.WriteEndElement();
                this.XmlResponseWriter.WriteEndDocument();
                this.XmlResponseWriter.Close();
                base.Response.End();
            }
            if (xmldoc.HasChildNodes)
            {
                string nodeInnerText = GetNodeInnerText(xmldoc, "//type");
                switch (nodeInnerText)
                {
                    case "userlogin":
                        this.UserLogin(xmldoc);
                        goto Label_0472;

                    case "userlogout":
                        this.UserLogout();
                        goto Label_0472;

                    case "logincheck":
                        this.LoginCheck();
                        goto Label_0472;

                    case "usercheck":
                        this.UserCheck(GetNodeInnerText(xmldoc, "//username"));
                        goto Label_0472;

                    case "updatelabel":
                        this.PutLabelBody(xmldoc);
                        goto Label_0472;

                    case "updatepage":
                        this.PutPageBody(xmldoc);
                        goto Label_0472;

                    case "addcomment":
                        this.AddComment(xmldoc);
                        goto Label_0472;

                    case "addpkzone":
                        this.AddPKZone(xmldoc);
                        goto Label_0472;

                    case "uploadvalue":
                    {
                        StringBuilder builder = new StringBuilder("getvalue:<br/>");
                        foreach (XmlNode node in xmldoc.DocumentElement.FirstChild.ChildNodes)
                        {
                            builder.Append(node.Name + "=");
                            builder.Append(node.InnerText + "<br/>");
                        }
                        this.XmlResponseWriter.WriteElementString("status", "ok");
                        this.XmlResponseWriter.WriteElementString("body", builder.ToString());
                        goto Label_0472;
                    }
                    case "SigninContent":
                    {
                        int generalId = DataConverter.CLng(GetNodeInnerText(xmldoc, "//itemid"));
                        this.SigninContent(generalId);
                        goto Label_0472;
                    }
                    case "GetContentSigninStatus":
                    {
                        int num2 = DataConverter.CLng(GetNodeInnerText(xmldoc, "//itemid"));
                        this.ResponseContentSigninStatus(num2);
                        goto Label_0472;
                    }
                    case "admineditcheck":
                    {
                        int num3 = DataConverter.CLng(GetNodeInnerText(xmldoc, "//itemid"));
                        this.AdminEditCheck(num3);
                        goto Label_0472;
                    }
                    case "showPop":
                        this.ShowPop();
                        goto Label_0472;

                    case "EnableValidCode":
                        this.EnableCheckCodeOfLogOn();
                        goto Label_0472;

                    default:
                        if (!string.IsNullOrEmpty(nodeInnerText))
                        {
                            this.PutSystemLabel(nodeInnerText, xmldoc.DocumentElement.SelectNodes("//attrib"));
                            goto Label_0472;
                        }
                        this.PutErrMessage("标签名不能为空");
                        return;
                }
                return;
            }
        Label_0472:
            this.XmlResponseWriter.WriteEndElement();
            this.XmlResponseWriter.WriteEndDocument();
            this.XmlResponseWriter.Close();
            base.Response.End();
        }

        private void PutErrMessage(string message)
        {
            this.XmlResponseWriter.WriteElementString("status", "err");
            this.XmlResponseWriter.WriteElementString("body", message);
        }

        private void PutLabelBody(XmlDocument xmldoc)
        {
            string nodeInnerText = GetNodeInnerText(xmldoc, "//labelname");
            string lrootpath = GetNodeInnerText(xmldoc, "//rootpath");
            string str3 = GetNodeInnerText(xmldoc, "//pagename");
            string input = GetNodeInnerText(xmldoc, "//currentpage", "1");
            XmlNodeList list = xmldoc.DocumentElement.SelectNodes("//attrib");
            if (string.IsNullOrEmpty(nodeInnerText))
            {
                this.PutErrMessage("标签名不能为空！");
            }
            else if (!this.CheckLabel(nodeInnerText))
            {
                this.PutErrMessage(nodeInnerText + "标签禁止AJAX访问！");
            }
            else
            {
                LabelInfo labelInfo = new LabelInfo();
                new XmlDocument();
                labelInfo.RootPath = lrootpath;
                labelInfo.PageName = str3;
                labelInfo.Page = DataConverter.CLng(input);
                labelInfo.TotalPub = 0;
                labelInfo.OriginalData["id"] = nodeInnerText;
                foreach (XmlNode node in list)
                {
                    if (node.FirstChild != null)
                    {
                        labelInfo.OriginalData[node.FirstChild.Name.Trim()] = DataSecurity.FilterSqlKeyword(DataSecurity.PELabelEncode(node.FirstChild.InnerText.Trim()));
                    }
                }
                string key = "CK_Label_TransformCacheData_" + nodeInnerText + "_" + labelInfo.OriginalData["cacheid"];
                int seconds = DataConverter.CLng(labelInfo.OriginalData["cachetime"]);
                if ((seconds > 0) && (SiteCache.Get(key) != null))
                {
                    labelInfo = (LabelInfo) SiteCache.Get(key);
                }
                else
                {
                    labelInfo = LabelTransform.GetLabel(labelInfo);
                    string pattern = @"{PE\.Label([\s\S](?!{PE))*?\/}";
                    bool flag = false;
                    do
                    {
                        flag = false;
                        foreach (Match match in Regex.Matches(labelInfo.LabelContent.ToString(), pattern, RegexOptions.IgnoreCase))
                        {
                            labelInfo.LabelContent.Replace(match.Value, this.ContentLabelProc(match.Value, lrootpath, labelInfo.PageName, labelInfo.Page));
                            flag = true;
                        }
                    }
                    while (flag);
                    if (seconds > 0)
                    {
                        SiteCache.Insert(key, labelInfo, seconds);
                    }
                }
                this.XmlResponseWriter.WriteElementString("status", "ok");
                this.XmlResponseWriter.WriteElementString("body", DataSecurity.PELabelDecode(labelInfo.LabelContent.ToString()));
                this.XmlResponseWriter.WriteElementString("pagename", str3);
                this.XmlResponseWriter.WriteElementString("total", labelInfo.TotalPub.ToString());
                this.XmlResponseWriter.WriteElementString("currentpage", labelInfo.Page.ToString());
                this.XmlResponseWriter.WriteElementString("pagesize", labelInfo.PageSize.ToString());
            }
        }

        private void PutPageBody(XmlDocument xmlDoc)
        {
            string nodeInnerText = GetNodeInnerText(xmlDoc, "//labelname");
            string str2 = GetNodeInnerText(xmlDoc, "//sourcename");
            string input = GetNodeInnerText(xmlDoc, "//currentpage", "1");
            string str4 = GetNodeInnerText(xmlDoc, "//total", "0");
            string str5 = GetNodeInnerText(xmlDoc, "//pagesize", "1");
            string unitname = GetNodeInnerText(xmlDoc, "//unitname");
            if (string.IsNullOrEmpty(nodeInnerText))
            {
                this.PutErrMessage("分页标签名不能为空！");
            }
            else
            {
                PageInfo iPage = new PageInfo();
                iPage.SpanName = str2;
                iPage.PageName = string.Empty;
                int num = DataConverter.CLng(str4) / DataConverter.CLng(str5);
                if ((DataConverter.CLng(str4) % DataConverter.CLng(str5)) > 0)
                {
                    num++;
                }
                iPage.PageNum = num;
                iPage.PageSize = DataConverter.CLng(str5);
                iPage.CurrentPage = DataConverter.CLng(input);
                iPage.TotalPub = DataConverter.CLng(str4);
                iPage.IsDynamicPage = true;
                iPage.PageName = "ajaxpage";
                this.XmlResponseWriter.WriteElementString("status", "ok");
                this.XmlResponseWriter.WriteElementString("body", LabelTransform.GetListPage(nodeInnerText, iPage, unitname));
            }
        }

        private void PutSystemLabel(string labelname, XmlNodeList bnode)
        {
            string labelName = labelname;
            List<string> list = new List<string>();
            list.Add("webmaster");
            list.Add("webmasteremail");
            list.Add("managedir");
            list.Add("readfile");
            if (list.Contains(labelname))
            {
                this.PutErrMessage(labelname + "标签禁止AJAX访问！");
            }
            else
            {
                foreach (XmlNode node in bnode)
                {
                    if (node.FirstChild != null)
                    {
                        string str2 = labelName;
                        labelName = str2 + " " + node.FirstChild.Name.Trim() + "=\"" + node.FirstChild.InnerText.Trim() + "\"";
                    }
                }
                this.XmlResponseWriter.WriteElementString("status", "ok");
                this.XmlResponseWriter.WriteElementString("body", LabelTransform.GetSiteConfigLabel(labelName).ToString());
            }
        }

        private void ResponseContentSigninStatus(int generalId)
        {
            string signinStatus = this.GetSigninStatus(generalId);
            if (signinStatus == "NotSignin")
            {
                if (SiteConfig.SiteOption.IsAutoSignIn)
                {
                    this.XmlResponseWriter.WriteElementString("status", "AutoSignin");
                    this.XmlResponseWriter.WriteElementString("time", SiteConfig.SiteOption.AutoSignInTime.ToString());
                }
                else
                {
                    this.XmlResponseWriter.WriteElementString("status", "NormalSignin");
                }
            }
            else
            {
                this.XmlResponseWriter.WriteElementString("status", signinStatus);
            }
        }

        private void ShowPop()
        {
            if (PEContext.Current.Admin.Identity.IsAuthenticated)
            {
                if (string.Compare(SiteConfig.SiteInfo.ProductEdition, "eshop", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Order.CountByOrderStatus(OrderStatus.WaitForConfirm);
                    Product.GetStockAlarmCount(2);
                    Order.CountByNoConsignment();
                }
                if (RolePermissions.AccessCheck(OperateCode.OrderConfirm))
                {
                    this.XmlResponseWriter.WriteElementString("ordercount", Order.CountByOrderStatus(OrderStatus.WaitForConfirm).ToString());
                }
                this.XmlResponseWriter.WriteElementString("messagecount", Message.UnreadMessageCount(PEContext.Current.Admin.UserName).ToString());
                if (RolePermissions.AccessCheck(OperateCode.ContentManage))
                {
                    this.XmlResponseWriter.WriteElementString("articlestatuscount", ContentManage.GetCountByStatus(0).ToString());
                }
                if (RolePermissions.AccessCheck(OperateCode.CommentManage))
                {
                    this.XmlResponseWriter.WriteElementString("commentcount", Comment.GetCountByStatus(2).ToString());
                }
                this.XmlResponseWriter.WriteElementString("articlesignincount", ContentManage.GetCountBySignIn(PEContext.Current.Admin.UserName, false).ToString());
                if (RolePermissions.AccessCheck(OperateCode.StockManage))
                {
                    this.XmlResponseWriter.WriteElementString("productstockalarmcount", Product.GetStockAlarmCount(2).ToString());
                }
                if (RolePermissions.AccessCheck(OperateCode.OrderSendOrReturnGoods))
                {
                    this.XmlResponseWriter.WriteElementString("ordercountbynoconsignment", Order.CountByNoConsignment().ToString());
                }
            }
        }

        private void SigninContent(int generalId)
        {
            string str = "NotSignined";
            if (PEContext.Current.User.Identity.IsAuthenticated && SignInLog.SignIn(generalId, PEContext.Current.User.UserName, true, PEContext.Current.UserHostAddress))
            {
                str = "Signined";
            }
            this.XmlResponseWriter.WriteElementString("status", str);
        }

        private void UserCheck(string username)
        {
            if (Users.Exists(username))
            {
                this.XmlResponseWriter.WriteElementString("status", "err");
            }
            else
            {
                this.XmlResponseWriter.WriteElementString("status", "ok");
            }
        }

        private void UserLogin(XmlDocument xmlDoc)
        {
            bool flag;
            DateTime now;
            DateTime time2;
            string str7;
            UserPrincipal principal;
            string nodeInnerText = GetNodeInnerText(xmlDoc, "//username");
            string str2 = GetNodeInnerText(xmlDoc, "//password");
            string str3 = GetNodeInnerText(xmlDoc, "//checkcode");
            string str4 = GetNodeInnerText(xmlDoc, "//expiration");
            if (string.IsNullOrEmpty(nodeInnerText))
            {
                this.PutErrMessage("用户名不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(str2))
            {
                this.PutErrMessage("密码不能为空！");
                return;
            }
            if (SiteConfig.UserConfig.EnableCheckCodeOfLogOn && string.IsNullOrEmpty(str3))
            {
                this.PutErrMessage("验证码不能为空！");
                return;
            }
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = nodeInnerText;
            userInfo.UserPassword = str2;
            if (SiteConfig.UserConfig.EnableCheckCodeOfLogOn)
            {
                string strB = (this.Session["ValidateCodeSession"] == null) ? "" : this.Session["ValidateCodeSession"].ToString();
                if (string.Compare(str3, strB, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    this.PutErrMessage("您输入的验证码错误！");
                    return;
                }
            }
            UserStatus status = Users.ValidateUser(userInfo);
            if ((Int32)status >= 100)
            {
                this.PutErrMessage("用户登录名称或用户密码不对！");
                return;
            }
            switch (status)
            {
                case UserStatus.None:
                    flag = false;
                    now = DateTime.Now;
                    time2 = DateTime.Now;
                    switch (str4)
                    {
                        case "None":
                            flag = false;
                            time2 = now.AddMinutes(20.0);
                            break;

                        case "Day":
                            flag = true;
                            time2 = now.AddDays(1.0);
                            break;

                        case "Month":
                            flag = true;
                            time2 = now.AddMonths(1);
                            break;

                        case "Year":
                            flag = true;
                            time2 = now.AddYears(1);
                            break;
                    }
                    flag = false;
                    time2 = now.AddMinutes(20.0);
                    break;

                case UserStatus.Locked:
                    this.PutErrMessage("用户帐户被锁定！");
                    return;

                case UserStatus.WaitValidateByEmail:
                    this.PutErrMessage("用户帐户等待邮件验证！");
                    return;

                case UserStatus.WaitValidateByAdmin:
                    this.PutErrMessage("用户帐户等待管理员验证！");
                    return;

                case UserStatus.WaitValidateByMobile:
                    this.PutErrMessage("用户帐户等待手机验证！");
                    return;

                default:
                    this.PutErrMessage("用户登录名称或用户密码不对！");
                    return;
            }
            string savecookie = "";
            if (!ApiData.IsAPiEnable())
            {
                goto Label_028F;
            }
            string str13 = str4;
            if (str13 != null)
            {
                if (!(str13 == "None"))
                {
                    if (str13 == "Day")
                    {
                        savecookie = "1";
                        goto Label_0263;
                    }
                    if (str13 == "Month")
                    {
                        savecookie = "30";
                        goto Label_0263;
                    }
                    if (str13 == "Year")
                    {
                        savecookie = "365";
                        goto Label_0263;
                    }
                }
                else
                {
                    savecookie = "-1";
                    goto Label_0263;
                }
            }
            savecookie = "-1";
        Label_0263:
            str7 = ApiFunction.LogOn(nodeInnerText, str2, savecookie);
            if (str7 != "true")
            {
                this.PutErrMessage("登陆失败!" + str7);
                return;
            }
        Label_028F:
            principal = new UserPrincipal();
            principal.UserName = userInfo.UserName;
            principal.LastPassword = userInfo.LastPassword;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userInfo.UserName, now, time2, flag, principal.SerializeToString());
            string str8 = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str8);
            if (flag)
            {
                cookie.Expires = time2;
            }
            base.Response.Cookies.Add(cookie);
            HttpCookie cookie2 = new HttpCookie(FormsAuthentication.FormsCookieName + "IsUserLogOut", "false");
            cookie2.HttpOnly = true;
            cookie2.Path = FormsAuthentication.FormsCookiePath;
            cookie2.Secure = FormsAuthentication.RequireSSL;
            base.Response.Cookies.Add(cookie2);
            this.Session["UserName"] = userInfo.UserName;
            this.XmlResponseWriter.WriteElementString("status", "ok");
            this.XmlResponseWriter.WriteElementString("username", userInfo.UserName);
            this.XmlResponseWriter.WriteElementString("usergroup", userInfo.GroupName);
            if (ApiData.IsAPiEnable())
            {
                ApiData data = new ApiData();
                string apiKey = data.ApiKey;
                apiKey = StringHelper.MD5GB2312(userInfo.UserName + apiKey).Substring(8, 0x10);
                string str10 = "";
                foreach (string str11 in data.Urls)
                {
                    str10 = str10 + "<iframe width=\"0\" height=\"0\" src=\"" + str11 + "?syskey=" + apiKey + "&username=" + HttpUtility.UrlEncode(userInfo.UserName, Encoding.GetEncoding("GB2312")) + "&password=" + userInfo.UserPassword + "&savecookie=" + savecookie + "\"></iframe>";
                }
                this.XmlResponseWriter.WriteElementString("API_Enable", "1");
                this.XmlResponseWriter.WriteElementString("LoginString", str10);
            }
            else
            {
                this.XmlResponseWriter.WriteElementString("API_Enable", "0");
            }
        }

        private void UserLogout()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName + "IsUserLogOut", "true");
            cookie.HttpOnly = true;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Secure = FormsAuthentication.RequireSSL;
            base.Response.Cookies.Add(cookie);
            this.XmlResponseWriter.WriteElementString("status", "ok");
        }
    }
}

