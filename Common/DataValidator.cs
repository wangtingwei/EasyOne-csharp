namespace EasyOne.Common
{
    using System;
    using System.Text.RegularExpressions;

    public abstract class DataValidator
    {
        protected DataValidator()
        {
        }

        public static bool IsAreaCode(string input)
        {
            return ((IsNumber(input) && (input.Length >= 3)) && (input.Length <= 5));
        }

        public static bool IsDecimal(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[0-9]+[.]?[0-9]+$");
        }

        public static bool IsDecimalSign(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[+-]?[0-9]+[.]?[0-9]+$");
        }

        public static bool IsEmail(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        public static bool IsIP(string input)
        {
            return (!string.IsNullOrEmpty(input) && Regex.IsMatch(input.Trim(), @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"));
        }

        public static bool IsNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[0-9]+$");
        }

        public static bool IsNumberSign(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[+-]?[0-9]+$");
        }

        public static bool IsPostCode(string input)
        {
            return (IsNumber(input) && (input.Length == 6));
        }

        public static bool IsUrl(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
        }

        public static bool IsValidId(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            input = input.Replace("|", "").Replace(",", "").Replace("-", "").Replace(" ", "").Trim();
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return IsNumber(input);
        }

        public static bool IsValidUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            if (userName.Length > 20)
            {
                return false;
            }
            if (userName.Trim().Length == 0)
            {
                return false;
            }
            if (userName.Trim(new char[] { '.' }).Length == 0)
            {
                return false;
            }
            string str = "\\/\"[]:|<>+=;,?*@";
            for (int i = 0; i < userName.Length; i++)
            {
                if (str.IndexOf(userName[i]) >= 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

