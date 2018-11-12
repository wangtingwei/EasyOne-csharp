namespace EasyOne.Controls.ExtendedUploadFile
{
    using System;

    public class UploadStatus
    {
        private DateTime beginTime;
        private int fileCount;
        private long fileLength;
        private string fileName;
        private bool isActive;
        private int percent;
        private long readLength;
        private UploadState state;

        internal UploadStatus()
        {
            this.state = UploadState.Initializing;
            this.beginTime = DateTime.MinValue;
            this.isActive = true;
            this.beginTime = DateTime.Now;
        }

        public UploadStatus(string uploadId)
        {
            this.state = UploadState.Initializing;
            this.beginTime = DateTime.MinValue;
            this.isActive = true;
            UploadStatus status = (UploadStatus) Utils.Context().Application["_UploadGUID_" + uploadId];
            if (status != null)
            {
                this.fileLength = status.fileLength;
                this.readLength = status.ReceivedLength;
                this.percent = status.Percent;
                this.state = status.State;
                this.beginTime = status.BeginTime;
                this.fileName = status.FileName;
                this.fileCount = status.FileCount;
            }
            this.isActive = Utils.Context().Application["_UploadGUID_" + uploadId] != null;
        }

        internal DateTime BeginTime
        {
            get
            {
                return this.beginTime;
            }
        }

        public int FileCount
        {
            get
            {
                return this.fileCount;
            }
            set
            {
                this.fileCount = value;
            }
        }

        public long FileLength
        {
            get
            {
                return this.fileLength;
            }
            set
            {
                this.fileLength = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
        }

        public TimeSpan LeftTime
        {
            get
            {
                TimeSpan maxValue = TimeSpan.MaxValue;
                if ((this.fileLength - this.readLength) > 0L)
                {
                    maxValue = new TimeSpan(0, 0, (int) Math.Round((double) (((double) (this.fileLength - this.readLength)) / this.Speed), 0));
                }
                return maxValue;
            }
        }

        public int Percent
        {
            get
            {
                if (this.fileLength > 0L)
                {
                    decimal d = (this.readLength * 100L) / this.fileLength;
                    this.percent = (int) Math.Floor(d);
                }
                return this.percent;
            }
        }

        public long ReceivedLength
        {
            get
            {
                return this.readLength;
            }
            set
            {
                this.readLength = value;
                if (this.readLength < this.fileLength)
                {
                    this.state = UploadState.Uploading;
                }
            }
        }

        public double Speed
        {
            get
            {
                double totalSeconds = DateTime.Now.Subtract(this.beginTime).TotalSeconds;
                double num2 = 0.0;
                if ((totalSeconds == 0.0) && (this.readLength == this.fileLength))
                {
                    totalSeconds = 1.0;
                }
                if (totalSeconds > 0.0)
                {
                    num2 = Math.Round((double) (((double) this.readLength) / totalSeconds), 2);
                }
                return num2;
            }
        }

        public UploadState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
    }
}

