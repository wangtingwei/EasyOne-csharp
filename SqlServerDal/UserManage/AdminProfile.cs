namespace EasyOne.SqlServerDal.UserManage
{
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class AdminProfile : IAdminProfile
    {
        private Serialize<AdminProfileInfo> adminProfileSer = new Serialize<AdminProfileInfo>();

        public void Add(AdminProfileInfo adminProileInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@AdminName", DbType.String, adminProileInfo.AdminName);
            cmdParams.AddInParameter("@WebPartSetting", DbType.String, adminProileInfo.WebPartSetting);
            string str = this.adminProfileSer.SerializeField(adminProileInfo);
            cmdParams.AddInParameter("@PersonalSetting", DbType.String, str);
            DBHelper.ExecuteSql("INSERT INTO PE_AdminProfile (AdminName, WebPartSetting, PersonalSetting) VALUES (@AdminName, @WebPartSetting, @PersonalSetting)", cmdParams);
        }

        public bool ExistsAdminName(string adminName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@AdminName", DbType.String, adminName);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_AdminProfile WHERE AdminName = @AdminName", cmdParams);
        }

        public AdminProfileInfo GetAdminProfile(string adminName)
        {
            AdminProfileInfo info = null;
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@AdminName", DbType.String, adminName);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_AdminProfile WHERE AdminName = @AdminName", cmdParams))
            {
                if (reader.Read())
                {
                    info = this.adminProfileSer.DeserializeField(reader.GetString("PersonalSetting"));
                    info.AdminName = reader.GetString("AdminName");
                    info.WebPartSetting = reader.GetString("WebPartSetting");
                    return info;
                }
                return new AdminProfileInfo(true);
            }
        }

        public void Update(AdminProfileInfo adminProileInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@AdminName", DbType.String, adminProileInfo.AdminName);
            cmdParams.AddInParameter("@WebPartSetting", DbType.String, adminProileInfo.WebPartSetting);
            string str = this.adminProfileSer.SerializeField(adminProileInfo);
            cmdParams.AddInParameter("@PersonalSetting", DbType.String, str);
            DBHelper.ExecuteSql("UPDATE PE_AdminProfile SET PersonalSetting = @PersonalSetting, WebPartSetting = @WebPartSetting WHERE AdminName = @AdminName", cmdParams);
        }
    }
}

