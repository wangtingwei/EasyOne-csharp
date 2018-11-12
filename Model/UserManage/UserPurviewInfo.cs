namespace EasyOne.Model.UserManage
{
    using System;

    [Serializable]
    public class UserPurviewInfo
    {
        private bool m_ChargeByPoint;
        private bool m_ChargeByPointAndValidDate;
        private bool m_ChargeByPointOrValidDate;
        private bool m_ChargeByValidDate;
        private bool m_CommentNeedCheck;
        private double m_Discount;
        private bool m_EnableBuyPoint;
        private bool m_EnableComment;
        private bool m_EnableExchangePoint;
        private bool m_EnableExchangeValidDate;
        private bool m_EnableGivePointToOthers;
        private bool m_Enablepm;
        private bool m_EnableUpload;
        private int m_GetExp;
        private bool m_IsNull;
        private bool m_IsXssFilter;
        private bool m_ManageSelfPublicInfo;
        private int m_MaxPublicInfoOneDay;
        private int m_MaxSaveInfos;
        private int m_MaxSendToUsers;
        private bool m_MinusPoint;
        private bool m_NotMinusPointNotWriteToLog;
        private double m_Overdraft;
        private bool m_PublicInfoNoNeedCheck;
        private bool m_SetEditor;
        private bool m_SetEnableSale;
        private bool m_SetToNotCheck;
        private int m_TotalViewInfoNumber;
        private int m_UploadSize;
        private int m_ViewInfoNumberOneDay;
        private bool m_WriteToLog;

        public UserPurviewInfo()
        {
        }

        public UserPurviewInfo(bool isNull)
        {
            this.m_IsNull = isNull;
        }

        public bool ChargeByPoint
        {
            get
            {
                return this.m_ChargeByPoint;
            }
            set
            {
                this.m_ChargeByPoint = value;
            }
        }

        public bool ChargeByPointAndValidDate
        {
            get
            {
                return this.m_ChargeByPointAndValidDate;
            }
            set
            {
                this.m_ChargeByPointAndValidDate = value;
            }
        }

        public bool ChargeByPointOrValidDate
        {
            get
            {
                return this.m_ChargeByPointOrValidDate;
            }
            set
            {
                this.m_ChargeByPointOrValidDate = value;
            }
        }

        public bool ChargeByValidDate
        {
            get
            {
                return this.m_ChargeByValidDate;
            }
            set
            {
                this.m_ChargeByValidDate = value;
            }
        }

        public bool CommentNeedCheck
        {
            get
            {
                return this.m_CommentNeedCheck;
            }
            set
            {
                this.m_CommentNeedCheck = value;
            }
        }

        public double Discount
        {
            get
            {
                return this.m_Discount;
            }
            set
            {
                this.m_Discount = value;
            }
        }

        public bool EnableBuyPoint
        {
            get
            {
                return this.m_EnableBuyPoint;
            }
            set
            {
                this.m_EnableBuyPoint = value;
            }
        }

        public bool EnableComment
        {
            get
            {
                return this.m_EnableComment;
            }
            set
            {
                this.m_EnableComment = value;
            }
        }

        public bool EnableExchangePoint
        {
            get
            {
                return this.m_EnableExchangePoint;
            }
            set
            {
                this.m_EnableExchangePoint = value;
            }
        }

        public bool EnableExchangeValidDate
        {
            get
            {
                return this.m_EnableExchangeValidDate;
            }
            set
            {
                this.m_EnableExchangeValidDate = value;
            }
        }

        public bool EnableGivePointToOthers
        {
            get
            {
                return this.m_EnableGivePointToOthers;
            }
            set
            {
                this.m_EnableGivePointToOthers = value;
            }
        }

        public bool Enablepm
        {
            get
            {
                return this.m_Enablepm;
            }
            set
            {
                this.m_Enablepm = value;
            }
        }

        public bool EnableUpload
        {
            get
            {
                return this.m_EnableUpload;
            }
            set
            {
                this.m_EnableUpload = value;
            }
        }

        public int GetExp
        {
            get
            {
                return this.m_GetExp;
            }
            set
            {
                this.m_GetExp = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public bool IsXssFilter
        {
            get
            {
                return this.m_IsXssFilter;
            }
            set
            {
                this.m_IsXssFilter = value;
            }
        }

        public bool ManageSelfPublicInfo
        {
            get
            {
                return this.m_ManageSelfPublicInfo;
            }
            set
            {
                this.m_ManageSelfPublicInfo = value;
            }
        }

        public int MaxPublicInfoOneDay
        {
            get
            {
                return this.m_MaxPublicInfoOneDay;
            }
            set
            {
                this.m_MaxPublicInfoOneDay = value;
            }
        }

        public int MaxSaveInfos
        {
            get
            {
                return this.m_MaxSaveInfos;
            }
            set
            {
                this.m_MaxSaveInfos = value;
            }
        }

        public int MaxSendToUsers
        {
            get
            {
                return this.m_MaxSendToUsers;
            }
            set
            {
                this.m_MaxSendToUsers = value;
            }
        }

        public bool MinusPoint
        {
            get
            {
                return this.m_MinusPoint;
            }
            set
            {
                this.m_MinusPoint = value;
            }
        }

        public bool NotMinusPointNotWriteToLog
        {
            get
            {
                return this.m_NotMinusPointNotWriteToLog;
            }
            set
            {
                this.m_NotMinusPointNotWriteToLog = value;
            }
        }

        public double Overdraft
        {
            get
            {
                return this.m_Overdraft;
            }
            set
            {
                this.m_Overdraft = value;
            }
        }

        public bool PublicInfoNoNeedCheck
        {
            get
            {
                return this.m_PublicInfoNoNeedCheck;
            }
            set
            {
                this.m_PublicInfoNoNeedCheck = value;
            }
        }

        public bool SetEditor
        {
            get
            {
                return this.m_SetEditor;
            }
            set
            {
                this.m_SetEditor = value;
            }
        }

        public bool SetEnableSale
        {
            get
            {
                return this.m_SetEnableSale;
            }
            set
            {
                this.m_SetEnableSale = value;
            }
        }

        public bool SetToNotCheck
        {
            get
            {
                return this.m_SetToNotCheck;
            }
            set
            {
                this.m_SetToNotCheck = value;
            }
        }

        public int TotalViewInfoNumber
        {
            get
            {
                return this.m_TotalViewInfoNumber;
            }
            set
            {
                this.m_TotalViewInfoNumber = value;
            }
        }

        public int UploadSize
        {
            get
            {
                return this.m_UploadSize;
            }
            set
            {
                this.m_UploadSize = value;
            }
        }

        public int ViewInfoNumberOneDay
        {
            get
            {
                return this.m_ViewInfoNumberOneDay;
            }
            set
            {
                this.m_ViewInfoNumberOneDay = value;
            }
        }

        public bool WriteToLog
        {
            get
            {
                return this.m_WriteToLog;
            }
            set
            {
                this.m_WriteToLog = value;
            }
        }
    }
}

