namespace EasyOne.SqlServerDal.Survey
{
    using EasyOne.IDal.Survey;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class SurveyCreate : ISurveyCreate
    {
        public bool FileNameExists(string fileName)
        {
            return DBHelper.ExistsSql("SELECT * FROM PE_Survey WHERE FileName = @FileName", new Parameters("@FileName", DbType.String, fileName));
        }
    }
}

