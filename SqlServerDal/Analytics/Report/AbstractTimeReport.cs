namespace EasyOne.SqlServerDal.Analytics.Report
{
    using EasyOne.IDal.Analytics;
    using EasyOne.SqlServerDal;
    using System;

    public abstract class AbstractTimeReport : ITimeReport
    {
        protected AbstractTimeReport()
        {
        }

        public abstract int[] GetAllList();
        protected static int[] GetArray(int arrayLength, string sql, Parameters param)
        {
            int[] numArray = new int[arrayLength];
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(sql, param))
            {
                if (!reader.Read())
                {
                    return numArray;
                }
                for (int i = 0; i < arrayLength; i++)
                {
                    numArray[i] = reader.GetInt32(i);
                }
            }
            return numArray;
        }

        public abstract int[] GetList(string value);
    }
}

