namespace EasyOne.Api
{
    using EasyOne.Common;
    using EasyOne.Components;
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web;

    public static class ApiFunction
    {
        public static bool CheckEmail(string userName, string email)
        {
            ApiData data = new ApiData();
            data.SpeItems[1, 1] = "checkemail";
            data.SpeItems[5, 1] = userName;
            data.SpeItems[7, 1] = email;
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(userName + data.ApiKey).Substring(8, 0x10);
            data.PrepareXml(true);
            data.SendPost();
            if (data.FoundErr)
            {
                return false;
            }
            return true;
        }

        public static bool CheckName(string userName, string email)
        {
            ApiData data = new ApiData();
            data.SpeItems[1, 1] = "checkname";
            data.SpeItems[5, 1] = userName;
            data.SpeItems[7, 1] = email;
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(userName + data.ApiKey).Substring(8, 0x10);
            data.PrepareXml(true);
            data.SendPost();
            if (data.FoundErr)
            {
                return false;
            }
            return true;
        }

        public static string DeleteUsers(string userName)
        {
            userName = userName.Replace("'", "");
            if (string.IsNullOrEmpty(userName))
            {
                return "false";
            }
            ApiData data = new ApiData();
            data.SpeItems[1, 1] = "delete";
            data.SpeItems[5, 1] = userName;
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(userName + data.ApiKey).Substring(8, 0x10);
            data.PrepareXml(true);
            data.SendPost();
            if (data.FoundErr)
            {
                return data.ErrMsg;
            }
            return "true";
        }

        public static bool LockUser(string userName, int userstatus)
        {
            ApiData data = new ApiData();
            data.SpeItems[1, 1] = "lock";
            data.SpeItems[5, 1] = userName;
            data.SpeItems[3, 1] = userstatus.ToString(CultureInfo.CurrentCulture);
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(userName + data.ApiKey).Substring(8, 0x10);
            data.PrepareXml(true);
            data.SendPost();
            if (data.FoundErr)
            {
                return false;
            }
            return true;
        }

        public static void LogOff(string userName)
        {
            ApiData data = new ApiData();
            data.SpeItems[5, 1] = userName;
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(userName + data.ApiKey).Substring(8, 0x10);
            foreach (string str in data.Urls)
            {
                HttpContext.Current.Response.Write("<script type=\"text/javascript\" language=\"JavaScript\" src=\"" + str + "?syskey=" + data.SpeItems[2, 1] + "&username=" + HttpUtility.UrlEncode(userName, Encoding.GetEncoding("GB2312")) + "\"></script>");
            }
        }

        public static string LogOn(string userName, string password, string savecookie)
        {
            userName = userName.Replace("'", "");
            if (string.IsNullOrEmpty(userName))
            {
                return "用户名不能为空";
            }
            ApiData data = new ApiData();
            data.SpeItems[1, 1] = "login";
            data.SpeItems[5, 1] = userName;
            data.SpeItems[6, 1] = password;
            data.SpeItems[10, 1] = savecookie;
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(userName + data.ApiKey).Substring(8, 0x10);
            data.PrepareXml(true);
            data.SendPost();
            if (data.FoundErr)
            {
                return data.ErrMsg;
            }
            return "true";
        }

        public static string RegLogOn(string userName, string password, string loginday)
        {
            ApiData data = new ApiData();
            string str = "";
            data.SpeItems[5, 1] = userName;
            data.SpeItems[6, 1] = StringHelper.MD5GB2312(password);
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(userName + data.ApiKey).Substring(8, 0x10);
            foreach (string str2 in data.Urls)
            {
                str = str + "<script type=\"text/javascript\" language=\"JavaScript\" src=\"" + str2 + "?syskey=" + data.SpeItems[2, 1] + "&username=" + HttpUtility.UrlEncode(data.SpeItems[5, 1], Encoding.GetEncoding("GB2312")) + "&password=" + data.SpeItems[6, 1] + "&savecookie=" + loginday + "\"></script>";
            }
            return str;
        }

        public static string RegUser(string name, string password, string question, string answer, string email, string trueName, string sex, string birthday, string qq, string msn, string mobile, string officePhone, string province, string city, string address, string zipCode, string homePage)
        {
            ApiData data = new ApiData();
            data.SpeItems[1, 1] = "reguser";
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(name + data.ApiKey).Substring(8, 0x10);
            data.SpeItems[5, 1] = name;
            data.SpeItems[6, 1] = password;
            data.SpeItems[8, 1] = question;
            data.SpeItems[9, 1] = answer;
            data.SpeItems[7, 1] = email;
            data.SpeItems[0x1c, 1] = "0";
            data.SpeItems[0x16, 1] = DateTime.Now.ToString();
            data.SpeItems[0x15, 1] = PEContext.Current.UserHostAddress;
            data.SpeItems[11, 1] = trueName;
            data.SpeItems[0x1f, 1] = ApiData.ExchangeGender(sex).ToString(CultureInfo.CurrentCulture);
            data.SpeItems[13, 1] = birthday;
            data.SpeItems[14, 1] = qq;
            data.SpeItems[15, 1] = msn;
            data.SpeItems[0x10, 1] = mobile;
            data.SpeItems[0x11, 1] = officePhone;
            data.SpeItems[0x1d, 1] = province;
            data.SpeItems[30, 1] = city;
            data.SpeItems[0x12, 1] = address;
            data.SpeItems[0x13, 1] = zipCode;
            data.SpeItems[20, 1] = homePage;
            data.PrepareXml(true);
            data.SendPost();
            if (data.FoundErr)
            {
                return data.ErrMsg;
            }
            return "true";
        }

        public static string UpdateUser(string userName, string password, string email, string question, string answer, string truename, string sex, string birthday, string qq, string msn, string mobile, string telephone, string address, string zipcode, string homepage)
        {
            ApiData data = new ApiData();
            data.SpeItems[1, 1] = "update";
            data.SpeItems[2, 1] = StringHelper.MD5GB2312(userName + data.ApiKey).Substring(8, 0x10);
            data.SpeItems[5, 1] = userName;
            data.SpeItems[6, 1] = password;
            data.SpeItems[8, 1] = question;
            data.SpeItems[9, 1] = answer;
            data.SpeItems[7, 1] = email;
            data.SpeItems[0x1c, 1] = "0";
            data.SpeItems[0x16, 1] = DateTime.Now.ToString();
            data.SpeItems[0x15, 1] = PEContext.Current.UserHostAddress;
            data.SpeItems[11, 1] = truename;
            data.SpeItems[12, 1] = ApiData.GenderToDV(sex).ToString(CultureInfo.CurrentCulture);
            data.SpeItems[13, 1] = birthday;
            data.SpeItems[14, 1] = qq;
            data.SpeItems[15, 1] = msn;
            data.SpeItems[0x10, 1] = mobile;
            data.SpeItems[0x11, 1] = telephone;
            data.SpeItems[0x12, 1] = address;
            data.SpeItems[0x13, 1] = zipcode;
            data.SpeItems[20, 1] = homepage;
            data.PrepareXml(true);
            data.SendPost();
            if (data.FoundErr)
            {
                return data.ErrMsg;
            }
            return "true";
        }
    }
}

