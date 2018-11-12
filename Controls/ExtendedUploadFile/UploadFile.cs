namespace EasyOne.Controls.ExtendedUploadFile
{
    using System;
    using System.IO;

    public class UploadFile
    {
        private string m_Contenttype;
        private long m_Filelength;
        private string m_Filename;
        private string m_Filepath;

        public UploadFile(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "name can not be null!");
            }
            string str = string.Empty;
            if (IsContentHeader(name))
            {
                str = name;
            }
            else if (IsContentHeader(Utils.Context().Request[name]))
            {
                str = Utils.Context().Request[name];
            }
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(new char[] { ';' });
                this.m_Contenttype = strArray[0];
                this.m_Contenttype = this.m_Contenttype.Substring(14, this.m_Contenttype.Length - 14);
                this.m_Filename = strArray[1];
                this.m_Filename = this.m_Filename.Substring(10, this.m_Filename.Length - 11);
                this.m_Filepath = strArray[2];
                this.m_Filepath = this.m_Filepath.Substring(10, this.m_Filepath.Length - 11);
                string str2 = Utils.UploadFolder();
                this.m_Filepath = Path.Combine(str2, this.m_Filepath);
                try
                {
                    this.m_Filelength = new FileInfo(this.m_Filepath).Length;
                }
                catch
                {
                    string str3 = Utils.Context().Request["EasyOne_Web_Upload_UploadGUID"];
                    if (str3 != null)
                    {
                        Utils.Context().Application.Remove("_UploadGUID_" + str3);
                    }
                    throw new FileNotFoundException("文件没有找到！");
                }
            }
        }

        private static bool IsContentHeader(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return false;
            }
            string[] strArray = line.Split(new char[] { ';' });
            return (((strArray.Length == 3) && strArray[0].StartsWith("Content-Type:", StringComparison.OrdinalIgnoreCase)) && (strArray[1].StartsWith("filename=\"", StringComparison.OrdinalIgnoreCase) && strArray[2].StartsWith("filepath=\"", StringComparison.OrdinalIgnoreCase)));
        }

        public void SaveAs(string fileName)
        {
            string uploadId = Utils.Context().Request["EasyOne_Web_Upload_UploadGUID"];
            try
            {
                UploadStatus status;
                FileInfo info = new FileInfo(this.m_Filepath);
                if (uploadId != null)
                {
                    status = new UploadStatus(uploadId) {
                        State = UploadState.Moving
                    };
                    Utils.Context().Application["_UploadGUID_" + uploadId] = status;
                }
                string directoryName = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                else if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                info.MoveTo(fileName);
                if (uploadId != null)
                {
                    status = new UploadStatus(uploadId) {
                        State = UploadState.Completed
                    };
                    Utils.Context().Application["_UploadGUID_" + uploadId] = status;
                }
            }
            catch
            {
                if (uploadId != null)
                {
                    Utils.Context().Application.Remove("_UploadGUID_" + uploadId);
                }
                throw new FileNotFoundException(fileName);
            }
        }

        public long ContentLength
        {
            get
            {
                return this.m_Filelength;
            }
        }

        public string ContentType
        {
            get
            {
                return this.m_Contenttype;
            }
        }

        public string FileName
        {
            get
            {
                return this.m_Filename;
            }
        }

        public string FilePath
        {
            get
            {
                return this.m_Filepath;
            }
        }
    }
}

