namespace EasyOne.Analytics
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public sealed class IPScanner
    {
        private static string _dataPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/QQWry.Dat");
        private static string _ip = "0.0.0.0";
        private static string country;
        private static int countryFlag;
        private static long endIpOff;
        private static string errMsg;
        private static long firstStartIp;
        private static long lastStartIp;
        private static string local;
        private static FileStream objfs;
        private static long startIp;

        private IPScanner()
        {
        }

        private static string GetCountry()
        {
            switch (countryFlag)
            {
                case 1:
                case 2:
                    country = GetFlagStr(endIpOff + 4L);
                    local = (1 == countryFlag) ? " " : GetFlagStr(endIpOff + 8L);
                    break;

                default:
                    country = GetFlagStr(endIpOff + 4L);
                    local = GetFlagStr(objfs.Position);
                    break;
            }
            return " ";
        }

        private static long GetEndIp()
        {
            objfs.Position = endIpOff;
            byte[] buffer = new byte[5];
            objfs.Read(buffer, 0, 5);
            long num = ((Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L)) + (((Convert.ToInt64(buffer[3].ToString()) * 0x100L) * 0x100L) * 0x100L);
            countryFlag = buffer[4];
            return num;
        }

        private static string GetFlagStr(long offSet)
        {
            int num = 0;
            byte[] buffer = new byte[3];
            while (true)
            {
                objfs.Position = offSet;
                num = objfs.ReadByte();
                if ((num != 1) && (num != 2))
                {
                    break;
                }
                objfs.Read(buffer, 0, 3);
                if (num == 2)
                {
                    countryFlag = 2;
                    endIpOff = offSet - 4L;
                }
                offSet = (Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L);
            }
            if (offSet < 12L)
            {
                return " ";
            }
            objfs.Position = offSet;
            return GetStr();
        }

        private static long GetStartIp(long recNO)
        {
            long num = firstStartIp + (recNO * 7L);
            objfs.Position = num;
            byte[] buffer = new byte[7];
            objfs.Read(buffer, 0, 7);
            endIpOff = (Convert.ToInt64(buffer[4].ToString()) + (Convert.ToInt64(buffer[5].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[6].ToString()) * 0x100L) * 0x100L);
            startIp = ((Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L)) + (((Convert.ToInt64(buffer[3].ToString()) * 0x100L) * 0x100L) * 0x100L);
            return startIp;
        }

        private static string GetStr()
        {
            byte num = 0;
            byte num2 = 0;
            StringBuilder builder = new StringBuilder();
            byte[] bytes = new byte[2];
            while (true)
            {
                num = (byte) objfs.ReadByte();
                if (num == 0)
                {
                    break;
                }
                if (num > 0x7f)
                {
                    num2 = (byte) objfs.ReadByte();
                    bytes[0] = num;
                    bytes[1] = num2;
                    Encoding encoding = Encoding.GetEncoding("GB2312");
                    if (num2 == 0)
                    {
                        break;
                    }
                    builder.Append(encoding.GetString(bytes));
                }
                else
                {
                    builder.Append((char) num);
                }
            }
            return builder.ToString();
        }

        public static string IPLocation()
        {
            QQwry();
            return (country + local);
        }

        public static string IPLocation(string ip)
        {
            _ip = ip;
            QQwry();
            return (country + local).Replace("CZ88.NET", "");
        }

        public static string IPLocation(string dataPath, string ip)
        {
            _dataPath = dataPath;
            _ip = ip;
            QQwry();
            return (country + local);
        }

        private static long IpToInt(string ip)
        {
            char[] separator = new char[] { '.' };
            if (ip.Split(separator).Length == 3)
            {
                ip = ip + ".0";
            }
            string[] strArray = ip.Split(separator);
            long num2 = ((long.Parse(strArray[0]) * 0x100L) * 0x100L) * 0x100L;
            long num3 = (long.Parse(strArray[1]) * 0x100L) * 0x100L;
            long num4 = long.Parse(strArray[2]) * 0x100L;
            long num5 = long.Parse(strArray[3]);
            return (((num2 + num3) + num4) + num5);
        }

        private static int QQwry()
        {
            string pattern = @"(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))";
            Regex regex = new Regex(pattern);
            if (!regex.Match(_ip).Success)
            {
                errMsg = "IP格式错误";
                return 4;
            }
            long num = IpToInt(_ip);
            int num2 = 0;
            if ((num >= IpToInt("127.0.0.0")) && (num <= IpToInt("127.255.255.255")))
            {
                country = "本机内部环回地址";
                local = "";
                num2 = 1;
            }
            else if ((((num >= IpToInt("0.0.0.0")) && (num <= IpToInt("2.255.255.255"))) || ((num >= IpToInt("64.0.0.0")) && (num <= IpToInt("126.255.255.255")))) || ((num >= IpToInt("58.0.0.0")) && (num <= IpToInt("60.255.255.255"))))
            {
                country = "网络保留地址";
                local = "";
                num2 = 1;
            }
            objfs = new FileStream(_dataPath, FileMode.Open, FileAccess.Read);
            objfs.Position = 0L;
            byte[] buffer = new byte[8];
            objfs.Read(buffer, 0, 8);
            firstStartIp = ((buffer[0] + (buffer[1] * 0x100)) + ((buffer[2] * 0x100) * 0x100)) + (((buffer[3] * 0x100) * 0x100) * 0x100);
            lastStartIp = ((buffer[4] + (buffer[5] * 0x100)) + ((buffer[6] * 0x100) * 0x100)) + (((buffer[7] * 0x100) * 0x100) * 0x100);
            long num3 = Convert.ToInt64((double) (((double) (lastStartIp - firstStartIp)) / 7.0));
            if (num3 <= 1L)
            {
                country = "FileDataError";
                objfs.Close();
                return 2;
            }
            long num4 = num3;
            long recNO = 0L;
            long num6 = 0L;
            while (recNO < (num4 - 1L))
            {
                num6 = (num4 + recNO) / 2L;
                GetStartIp(num6);
                if (num == startIp)
                {
                    recNO = num6;
                    break;
                }
                if (num > startIp)
                {
                    recNO = num6;
                }
                else
                {
                    num4 = num6;
                }
            }
            GetStartIp(recNO);
            long endIp = GetEndIp();
            if ((startIp <= num) && (endIp >= num))
            {
                GetCountry();
            }
            else
            {
                num2 = 3;
                country = "未知";
                local = "";
            }
            objfs.Close();
            return num2;
        }

        public static string Country
        {
            get
            {
                return country;
            }
        }

        public static string DataPath
        {
            get
            {
                return _dataPath;
            }
            set
            {
                _dataPath = value;
            }
        }

        public static string ErrMsg
        {
            get
            {
                return errMsg;
            }
        }

        public static string IP
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }

        public static FileStream IPFileStream
        {
            get
            {
                return objfs;
            }
            set
            {
                objfs = value;
            }
        }

        public static string Local
        {
            get
            {
                return local;
            }
        }
    }
}

