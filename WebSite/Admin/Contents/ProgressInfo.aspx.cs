﻿namespace EasyOne.WebSite.Admin.Contents
{
    using System;

    public class ProgressInfo
    {
        private int m_Completed;
        private int m_Count;
        private DateTime m_EndTime;
        private string m_ErrorMessage;
        private DateTime m_ErrorTime;
        private string m_ExecutionTime;
        private string m_Id;
        private string m_Message;
        private string m_Progress;
        private string m_RemainingTime;
        private DateTime m_StartTime;
        private int m_Status;
        private string m_UnitName;

        public int Completed
        {
            get
            {
                return this.m_Completed;
            }
            set
            {
                this.m_Completed = value;
            }
        }

        public int Count
        {
            get
            {
                return this.m_Count;
            }
            set
            {
                this.m_Count = value;
            }
        }

        public DateTime EndTime
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

        public string ErrorMessage
        {
            get
            {
                return this.m_ErrorMessage;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.m_ErrorMessage = "";
                }
                else
                {
                    this.m_ErrorMessage = value;
                }
            }
        }

        public DateTime ErrorTime
        {
            get
            {
                return this.m_ErrorTime;
            }
            set
            {
                this.m_ErrorTime = value;
            }
        }

        public string ExecutionTime
        {
            get
            {
                return this.m_ExecutionTime;
            }
            set
            {
                this.m_ExecutionTime = value;
            }
        }

        public string Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
            }
        }

        public string Message
        {
            get
            {
                return this.m_Message;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.m_Message = "";
                }
                else
                {
                    this.m_Message = value;
                }
            }
        }

        public string Progress
        {
            get
            {
                return this.m_Progress;
            }
            set
            {
                this.m_Progress = value;
            }
        }

        public string RemainingTime
        {
            get
            {
                return this.m_RemainingTime;
            }
            set
            {
                this.m_RemainingTime = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.m_StartTime;
            }
            set
            {
                this.m_StartTime = value;
            }
        }

        public int Status
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

        public string UnitName
        {
            get
            {
                return this.m_UnitName;
            }
            set
            {
                this.m_UnitName = value;
            }
        }
    }
}

