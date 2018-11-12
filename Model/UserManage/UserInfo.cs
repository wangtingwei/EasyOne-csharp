namespace EasyOne.Model.UserManage
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    [Serializable]
    public class UserInfo : EasyOne.Model.Nullable
    {
        private string m_Answer;
        private decimal m_Balance;
        private string m_CheckNum;
        private int m_ClientId;
        private int m_CompanyId;
        private int m_ConsumeExp;
        private int m_ConsumeMoney;
        private int m_ConsumePoint;
        private int m_DelItems;
        private string m_Email;
        private bool m_EnableResetPassword;
        private DateTime? m_EndTime;
        private int m_FaceHeight;
        private int m_FaceWidth;
        private int m_FailedPasswordAnswerAttempCount;
        private int m_FailedPasswordAttemptCount;
        private DateTime? m_FirstFailedPasswordAnswerAttempTime;
        private DateTime? m_FirstFailedPasswordAttempTime;
        private int m_GroupId;
        private string m_GroupName;
        private bool m_IsInheritGroupRole;
        private DateTime m_JoinTime;
        private DateTime? m_LastLockoutTime;
        private string m_LastLogOnIP;
        private DateTime? m_LastLogOnTime;
        private string m_LastPassword;
        private DateTime? m_LastPasswordChangedTime;
        private DateTime? m_LastPresentTime;
        private int m_LogOnTimes;
        private int m_PassedItems;
        private string m_PayPassword;
        private int m_PostItems;
        private int m_PrivacySetting;
        private string m_Question;
        private DateTime m_RegTime;
        private int m_RejectItems;
        private UserSexType m_Sex;
        private string m_Sign;
        private UserStatus m_Status;
        private int m_UserExp;
        private string m_UserFace;
        private string m_UserFriendGroup;
        private int m_UserId;
        private string m_UserName;
        private string m_UserPassword;
        private int m_UserPoint;
        private string m_UserSetting;
        private string m_UserTrueName;
        private EasyOne.Enumerations.UserType m_UserType;
        private static Serialize<UserPurviewInfo> ser = new Serialize<UserPurviewInfo>();

        public UserInfo()
        {
        }

        public UserInfo(bool value)
        {
            base.IsNull = value;
        }

        public string Answer
        {
            get
            {
                return this.m_Answer;
            }
            set
            {
                this.m_Answer = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return this.m_Balance;
            }
            set
            {
                this.m_Balance = value;
            }
        }

        public string CheckNum
        {
            get
            {
                return this.m_CheckNum;
            }
            set
            {
                this.m_CheckNum = value;
            }
        }

        public int ClientId
        {
            get
            {
                return this.m_ClientId;
            }
            set
            {
                this.m_ClientId = value;
            }
        }

        public int CompanyId
        {
            get
            {
                return this.m_CompanyId;
            }
            set
            {
                this.m_CompanyId = value;
            }
        }

        public int ConsumeExp
        {
            get
            {
                return this.m_ConsumeExp;
            }
            set
            {
                this.m_ConsumeExp = value;
            }
        }

        public int ConsumeMoney
        {
            get
            {
                return this.m_ConsumeMoney;
            }
            set
            {
                this.m_ConsumeMoney = value;
            }
        }

        public int ConsumePoint
        {
            get
            {
                return this.m_ConsumePoint;
            }
            set
            {
                this.m_ConsumePoint = value;
            }
        }

        public int DelItems
        {
            get
            {
                return this.m_DelItems;
            }
            set
            {
                this.m_DelItems = value;
            }
        }

        public string Email
        {
            get
            {
                return this.m_Email;
            }
            set
            {
                this.m_Email = value;
            }
        }

        public bool EnableResetPassword
        {
            get
            {
                return this.m_EnableResetPassword;
            }
            set
            {
                this.m_EnableResetPassword = value;
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return this.m_EndTime;
            }
            set
            {
                this.m_EndTime = value;
            }
        }

        public int FaceHeight
        {
            get
            {
                return this.m_FaceHeight;
            }
            set
            {
                this.m_FaceHeight = value;
            }
        }

        public int FaceWidth
        {
            get
            {
                return this.m_FaceWidth;
            }
            set
            {
                this.m_FaceWidth = value;
            }
        }

        public int FailedPasswordAnswerAttempCount
        {
            get
            {
                return this.m_FailedPasswordAnswerAttempCount;
            }
            set
            {
                this.m_FailedPasswordAnswerAttempCount = value;
            }
        }

        public int FailedPasswordAttemptCount
        {
            get
            {
                return this.m_FailedPasswordAttemptCount;
            }
            set
            {
                this.m_FailedPasswordAttemptCount = value;
            }
        }

        public DateTime? FirstFailedPasswordAnswerAttempTime
        {
            get
            {
                return this.m_FirstFailedPasswordAnswerAttempTime;
            }
            set
            {
                this.m_FirstFailedPasswordAnswerAttempTime = value;
            }
        }

        public DateTime? FirstFailedPasswordAttempTime
        {
            get
            {
                return this.m_FirstFailedPasswordAttempTime;
            }
            set
            {
                this.m_FirstFailedPasswordAttempTime = value;
            }
        }

        public int GroupId
        {
            get
            {
                return this.m_GroupId;
            }
            set
            {
                this.m_GroupId = value;
            }
        }

        public string GroupName
        {
            get
            {
                return this.m_GroupName;
            }
            set
            {
                this.m_GroupName = value;
            }
        }

        public bool IsInheritGroupRole
        {
            get
            {
                return this.m_IsInheritGroupRole;
            }
            set
            {
                this.m_IsInheritGroupRole = value;
            }
        }

        public DateTime JoinTime
        {
            get
            {
                return this.m_JoinTime;
            }
            set
            {
                this.m_JoinTime = value;
            }
        }

        public DateTime? LastLockoutTime
        {
            get
            {
                return this.m_LastLockoutTime;
            }
            set
            {
                this.m_LastLockoutTime = value;
            }
        }

        public string LastLogOnIP
        {
            get
            {
                return this.m_LastLogOnIP;
            }
            set
            {
                this.m_LastLogOnIP = value;
            }
        }

        public DateTime? LastLogOnTime
        {
            get
            {
                return this.m_LastLogOnTime;
            }
            set
            {
                this.m_LastLogOnTime = value;
            }
        }

        public string LastPassword
        {
            get
            {
                return this.m_LastPassword;
            }
            set
            {
                this.m_LastPassword = value;
            }
        }

        public DateTime? LastPasswordChangedTime
        {
            get
            {
                return this.m_LastPasswordChangedTime;
            }
            set
            {
                this.m_LastPasswordChangedTime = value;
            }
        }

        public DateTime? LastPresentTime
        {
            get
            {
                return this.m_LastPresentTime;
            }
            set
            {
                this.m_LastPresentTime = value;
            }
        }

        public int LogOnTimes
        {
            get
            {
                return this.m_LogOnTimes;
            }
            set
            {
                this.m_LogOnTimes = value;
            }
        }

        public int PassedItems
        {
            get
            {
                return this.m_PassedItems;
            }
            set
            {
                this.m_PassedItems = value;
            }
        }

        public string PayPassword
        {
            get
            {
                return this.m_PayPassword;
            }
            set
            {
                this.m_PayPassword = value;
            }
        }

        public int PostItems
        {
            get
            {
                return this.m_PostItems;
            }
            set
            {
                this.m_PostItems = value;
            }
        }

        public int PrivacySetting
        {
            get
            {
                return this.m_PrivacySetting;
            }
            set
            {
                this.m_PrivacySetting = value;
            }
        }

        public string Question
        {
            get
            {
                return this.m_Question;
            }
            set
            {
                this.m_Question = value;
            }
        }

        public DateTime RegTime
        {
            get
            {
                return this.m_RegTime;
            }
            set
            {
                this.m_RegTime = value;
            }
        }

        public int RejectItems
        {
            get
            {
                return this.m_RejectItems;
            }
            set
            {
                this.m_RejectItems = value;
            }
        }

        public UserSexType Sex
        {
            get
            {
                return this.m_Sex;
            }
            set
            {
                this.m_Sex = value;
            }
        }

        public string Sign
        {
            get
            {
                return this.m_Sign;
            }
            set
            {
                this.m_Sign = value;
            }
        }

        public UserStatus Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }

        public int UserExp
        {
            get
            {
                return this.m_UserExp;
            }
            set
            {
                this.m_UserExp = value;
            }
        }

        public string UserFace
        {
            get
            {
                return this.m_UserFace;
            }
            set
            {
                this.m_UserFace = value;
            }
        }

        public string UserFriendGroup
        {
            get
            {
                return this.m_UserFriendGroup;
            }
            set
            {
                this.m_UserFriendGroup = value;
            }
        }

        public int UserId
        {
            get
            {
                return this.m_UserId;
            }
            set
            {
                this.m_UserId = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return this.m_UserPassword;
            }
            set
            {
                this.m_UserPassword = value;
            }
        }

        public int UserPoint
        {
            get
            {
                return this.m_UserPoint;
            }
            set
            {
                this.m_UserPoint = value;
            }
        }

        public UserPurviewInfo UserPurview
        {
            get
            {
                if (!string.IsNullOrEmpty(this.m_UserSetting))
                {
                    return ser.DeserializeField(this.m_UserSetting);
                }
                return new UserPurviewInfo(true);
            }
        }

        public string UserSetting
        {
            get
            {
                return this.m_UserSetting;
            }
            set
            {
                this.m_UserSetting = value;
            }
        }

        public string UserTrueName
        {
            get
            {
                return this.m_UserTrueName;
            }
            set
            {
                this.m_UserTrueName = value;
            }
        }

        public EasyOne.Enumerations.UserType UserType
        {
            get
            {
                return this.m_UserType;
            }
            set
            {
                this.m_UserType = value;
            }
        }
    }
}

