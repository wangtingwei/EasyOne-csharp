namespace EasyOne.Common
{
    using System;

    public abstract class DataConverter
    {
        protected DataConverter()
        {
        }

        public static bool CBoolean(string input)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(input))
            {
                return flag;
            }
            input = input.Trim();
            if (((string.Compare(input, "true", StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(input, "yes", StringComparison.OrdinalIgnoreCase) != 0)) && (string.Compare(input, "1", StringComparison.OrdinalIgnoreCase) != 0))
            {
                return flag;
            }
            return true;
        }

        public static DateTime CDate(object input)
        {
            if (!Convert.IsDBNull(input) && !object.Equals(input, null))
            {
                return CDate(input.ToString());
            }
            return DateTime.Now;
        }

        public static DateTime CDate(string input)
        {
            DateTime now;
            if (!DateTime.TryParse(input, out now))
            {
                now = DateTime.Now;
            }
            return now;
        }

        public static DateTime? CDate(string input, DateTime? outTime)
        {
            DateTime time;
            if (!DateTime.TryParse(input, out time))
            {
                return outTime;
            }
            return new DateTime?(time);
        }

        public static decimal CDecimal(object input)
        {
            return CDecimal(input, 0M);
        }

        public static decimal CDecimal(string input)
        {
            return CDecimal(input, 0M);
        }

        public static decimal CDecimal(object input, decimal defaultValue)
        {
            if (!Convert.IsDBNull(input) && !object.Equals(input, null))
            {
                return CDecimal(input.ToString(), defaultValue);
            }
            return 0M;
        }

        public static decimal CDecimal(string input, decimal defaultValue)
        {
            decimal num;
            if (!decimal.TryParse(input, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public static double CDouble(object input)
        {
            return CDouble(input, 0.0);
        }

        public static double CDouble(string input)
        {
            return CDouble(input, 0.0);
        }

        public static double CDouble(object input, double defaultValue)
        {
            if (!Convert.IsDBNull(input) && !object.Equals(input, null))
            {
                return CDouble(input.ToString(), defaultValue);
            }
            return 0.0;
        }

        public static double CDouble(string input, double defaultValue)
        {
            double num;
            if (!double.TryParse(input, out num))
            {
                return defaultValue;
            }
            return num;
        }

        public static int CLng(object input)
        {
            return CLng(input, 0);
        }

        public static int CLng(string input)
        {
            return CLng(input, 0);
        }

        public static int CLng(object input, int defaultValue)
        {
            if (!Convert.IsDBNull(input) && !object.Equals(input, null))
            {
                return CLng(input.ToString(), defaultValue);
            }
            return defaultValue;
        }

        public static int CLng(string input, int defaultValue)
        {
            int num;
            if (!int.TryParse(input, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        public static float CSingle(object input)
        {
            return CSingle(input, 0f);
        }

        public static float CSingle(string input)
        {
            return CSingle(input, 0f);
        }

        public static float CSingle(object input, float defaultValue)
        {
            if (!Convert.IsDBNull(input) && !object.Equals(input, null))
            {
                return CSingle(input.ToString(), defaultValue);
            }
            return 0f;
        }

        public static float CSingle(string input, float defaultValue)
        {
            float num;
            if (!float.TryParse(input, out num))
            {
                num = defaultValue;
            }
            return num;
        }
    }
}

