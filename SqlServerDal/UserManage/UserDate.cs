namespace EasyOne.SqlServerDal.UserManage
{
    using EasyOne.Common;
    using EasyOne.IDal.UserManage;
    using EasyOne.Model.UserManage;
    using System;

    public class UserDate : IUserDate
    {
        private IUsers user = new Users();

        public bool AddDateForAll(int date)
        {
            foreach (UserInfo info in this.user.GetAllUsers(0, 0, 0, "", 0))
            {
                this.CheckDateAndUpdateUserEndTime(date, info);
            }
            return true;
        }

        public bool AddDateForGroups(string toGroups, int date)
        {
            if (string.IsNullOrEmpty(toGroups))
            {
                return false;
            }
            foreach (UserInfo info in this.user.GetUsersByGroupId(toGroups))
            {
                this.CheckDateAndUpdateUserEndTime(date, info);
            }
            return true;
        }

        public bool AddDateForUsers(string toUsers, int date)
        {
            if (string.IsNullOrEmpty(toUsers))
            {
                return false;
            }
            char[] separator = new char[] { ',' };
            string[] strArray = toUsers.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                int userId = DataConverter.CLng(strArray[i]);
                UserInfo userById = this.user.GetUserById(userId);
                this.CheckDateAndUpdateUserEndTime(date, userById);
            }
            return true;
        }

        private void CheckDateAndUpdateUserEndTime(int date, UserInfo userInfo)
        {
            if (date == 0x270f)
            {
                userInfo.EndTime = new DateTime?(DateTime.MaxValue);
                this.user.Update(userInfo);
            }
            else if (date == -9999)
            {
                userInfo.EndTime = null;
                this.user.Update(userInfo);
            }
            else
            {
                if (!userInfo.EndTime.HasValue || (userInfo.EndTime.Value.CompareTo(DateTime.Now) <= 0))
                {
                    if (date <= 0)
                    {
                        return;
                    }
                    userInfo.EndTime = new DateTime?(DateTime.Now.AddDays((double) date));
                }
                else
                {
                    TimeSpan span = (TimeSpan) (DateTime.MaxValue - userInfo.EndTime.Value);
                    if ((span.TotalDays - date) > 0.0)
                    {
                        userInfo.EndTime = new DateTime?(userInfo.EndTime.Value.AddDays((double) date));
                    }
                }
                this.user.Update(userInfo);
            }
        }
    }
}

