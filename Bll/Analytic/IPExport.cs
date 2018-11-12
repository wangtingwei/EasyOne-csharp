namespace EasyOne.Analytics
{
    using System;
    using System.IO;
    using System.Text;

    public class IPExport : IDisposable
    {
        private string country;
        private int countryFlag;
        private long endIpOff;
        private string local;
        private FileStream objfs;
        private long startIp;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.objfs.Close();
            }
        }

        private string GetCountry()
        {
            switch (this.countryFlag)
            {
                case 1:
                case 2:
                    this.country = this.GetFlagStr(this.endIpOff + 4L);
                    this.local = (1 == this.countryFlag) ? " " : this.GetFlagStr(this.endIpOff + 8L);
                    break;

                default:
                    this.country = this.GetFlagStr(this.endIpOff + 4L);
                    this.local = this.GetFlagStr(this.objfs.Position);
                    break;
            }
            return " ";
        }

        private string GetFlagStr(long offSet)
        {
            int num = 0;
            byte[] buffer = new byte[3];
            while (true)
            {
                this.objfs.Position = offSet;
                num = this.objfs.ReadByte();
                if ((num != 1) && (num != 2))
                {
                    break;
                }
                this.objfs.Read(buffer, 0, 3);
                if (num == 2)
                {
                    this.countryFlag = 2;
                    this.endIpOff = offSet - 4L;
                }
                offSet = (Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L);
            }
            if (offSet < 12L)
            {
                return " ";
            }
            this.objfs.Position = offSet;
            return this.GetStr();
        }

        private string GetStr()
        {
            byte num = 0;
            byte num2 = 0;
            StringBuilder builder = new StringBuilder();
            byte[] bytes = new byte[2];
            while (true)
            {
                num = (byte) this.objfs.ReadByte();
                if (num == 0)
                {
                    return builder.ToString();
                }
                if (num > 0x7f)
                {
                    num2 = (byte) this.objfs.ReadByte();
                    bytes[0] = num;
                    bytes[1] = num2;
                    Encoding encoding = Encoding.GetEncoding("GB2312");
                    builder.Append(encoding.GetString(bytes));
                }
                else
                {
                    builder.Append((char) num);
                }
            }
        }

        private static string IntToIP(long ip_Int)
        {
            long num = (long) ((ip_Int & 0xff000000L) >> 0x18);
            if (num < 0L)
            {
                num += 0x100L;
            }
            long num2 = (ip_Int & 0xff0000L) >> 0x10;
            if (num2 < 0L)
            {
                num2 += 0x100L;
            }
            long num3 = (ip_Int & 0xff00L) >> 8;
            if (num3 < 0L)
            {
                num3 += 0x100L;
            }
            long num4 = ip_Int & 0xffL;
            if (num4 < 0L)
            {
                num4 += 0x100L;
            }
            return (num.ToString() + "." + num2.ToString() + "." + num3.ToString() + "." + num4.ToString());
        }

        public void SaveToText(string toFilePath, string fromFilePath)
        {
            this.objfs = new FileStream(fromFilePath, FileMode.Open, FileAccess.Read);
            this.objfs.Position = 0L;
            byte[] buffer = new byte[8];
            this.objfs.Read(buffer, 0, 8);
            int num = ((buffer[0] + (buffer[1] * 0x100)) + ((buffer[2] * 0x100) * 0x100)) + (((buffer[3] * 0x100) * 0x100) * 0x100);
            int num2 = ((buffer[4] + (buffer[5] * 0x100)) + ((buffer[6] * 0x100) * 0x100)) + (((buffer[7] * 0x100) * 0x100) * 0x100);
            long num3 = Convert.ToInt64((double) (((double) (num2 - num)) / 7.0));
            if (num3 <= 1L)
            {
                this.country = "FileDataError";
                this.objfs.Close();
            }
            StreamWriter writer = File.AppendText(toFilePath);
            for (int i = 0; i <= num3; i++)
            {
                long num5 = num + (i * 7);
                this.objfs.Position = num5;
                byte[] buffer2 = new byte[7];
                this.objfs.Read(buffer2, 0, 7);
                this.endIpOff = (Convert.ToInt64(buffer2[4].ToString()) + (Convert.ToInt64(buffer2[5].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer2[6].ToString()) * 0x100L) * 0x100L);
                this.startIp = ((Convert.ToInt64(buffer2[0].ToString()) + (Convert.ToInt64(buffer2[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer2[2].ToString()) * 0x100L) * 0x100L)) + (((Convert.ToInt64(buffer2[3].ToString()) * 0x100L) * 0x100L) * 0x100L);
                this.objfs.Position = this.endIpOff;
                byte[] buffer3 = new byte[5];
                this.objfs.Read(buffer3, 0, 5);
                this.countryFlag = buffer3[4];
                string str = IntToIP(this.startIp);
                this.GetCountry();
                writer.WriteLine(str + " " + this.country + this.local);
            }
            writer.Close();
        }
    }
}

