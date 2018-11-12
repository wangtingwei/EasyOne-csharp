namespace EasyOne.SqlServerDal.Accessories
{
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using EasyOne.SqlServerDal;
    using System;

    public class DataBaseHandle : IDataBaseHandle
    {
        private static DataBaseVersionInfo DataBaseVersionFromDataReader(NullableDataReader rdr)
        {
            DataBaseVersionInfo info = new DataBaseVersionInfo();
            info.VersionId = rdr.GetInt32("VersionID");
            info.Major = rdr.GetInt32("Major");
            info.Minor = rdr.GetInt32("Minor");
            info.Build = rdr.GetInt32("Build");
            info.Revision = rdr.GetInt32("Revision");
            info.CreatedDate = rdr.GetDateTime("CreatedDate");
            return info;
        }

        public DataBaseVersionInfo LastVersion()
        {
            DataBaseVersionInfo info = new DataBaseVersionInfo(true);
            try
            {
                using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT TOP 1 * FROM PE_Version ORDER BY VersionID DESC"))
                {
                    if (reader.Read())
                    {
                        info = DataBaseVersionFromDataReader(reader);
                    }
                    return info;
                }
            }
            catch
            {
                info.Major = 0x63;
                info.Minor = 0x63;
                info.Build = 0x63;
                info.Revision = 0x63;
            }
            return info;
        }
    }
}

