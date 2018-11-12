namespace EasyOne.Controls.ExtendedUploadFile
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Web;

    public class RequestFileContent : IDisposable
    {
        private ArrayList contentBody = new ArrayList();
        private FileStatus fileStatus = FileStatus.Close;
        private System.IO.FileStream fs = null;
        private string originalFileName = string.Empty;
        private ArrayList readBody = new ArrayList();
        private ReadStatus readStatus = ReadStatus.NoRead;

        public RequestFileContent(byte[] preloadBytes, byte[] boundaryBytes, System.IO.FileStream fileStream, FileStatus fileStatus, ReadStatus readStatus, string uploadFolder, bool writeToDisk, HttpContext context, string currFileName)
        {
            this.originalFileName = currFileName;
            this.fs = fileStream;
            this.fileStatus = fileStatus;
            this.readStatus = readStatus;
            int index = 0;
            while (index < preloadBytes.Length)
            {
                ArrayList c = new ArrayList();
                if (preloadBytes[index] == 13)
                {
                    int num2 = 0;
                    while ((index < preloadBytes.Length) && (num2 < boundaryBytes.Length))
                    {
                        if (preloadBytes[index] != boundaryBytes[num2])
                        {
                            break;
                        }
                        c.Add(preloadBytes[index]);
                        num2++;
                        index++;
                    }
                    if (num2 == boundaryBytes.Length)
                    {
                        this.SetFileStatus(context);
                        if ((index + 2) < preloadBytes.Length)
                        {
                            c.Add(preloadBytes[index]);
                            index++;
                            c.Add(preloadBytes[index]);
                            index++;
                            ArrayList list2 = new ArrayList();
                            while (index < preloadBytes.Length)
                            {
                                c.Add(preloadBytes[index]);
                                if (preloadBytes[index] == 13)
                                {
                                    byte[] array = new byte[list2.Count];
                                    list2.CopyTo(array);
                                    string str = Utils.Context().Request.ContentEncoding.GetString(array);
                                    if (str.IndexOf("\"; filename=\"", StringComparison.OrdinalIgnoreCase) > 0)
                                    {
                                        index++;
                                        ArrayList list3 = new ArrayList();
                                        while (index < preloadBytes.Length)
                                        {
                                            c.Add(preloadBytes[index]);
                                            if (preloadBytes[index] == 13)
                                            {
                                                if ((index + 3) < preloadBytes.Length)
                                                {
                                                    char[] separator = new char[] { ';' };
                                                    string[] strArray = str.Split(separator);
                                                    string str2 = strArray[2].Trim();
                                                    str2 = str2.Substring(10, str2.Length - 11);
                                                    if (writeToDisk && !string.IsNullOrEmpty(str2))
                                                    {
                                                        this.originalFileName = Path.GetFileName(str2);
                                                        index += 3;
                                                        byte[] buffer2 = new byte[list3.Count];
                                                        list3.CopyTo(buffer2);
                                                        string str3 = Utils.Context().Request.ContentEncoding.GetString(buffer2);
                                                        string str4 = Guid.NewGuid().ToString() + Path.GetExtension(str2);
                                                        string tempPath = uploadFolder;
                                                        if (string.IsNullOrEmpty(tempPath))
                                                        {
                                                            tempPath = Path.GetTempPath();
                                                        }
                                                        tempPath = Path.Combine(tempPath, str4);
                                                        StringBuilder builder = new StringBuilder();
                                                        builder.Append("\r\n");
                                                        builder.Append(strArray[0]);
                                                        builder.Append(";");
                                                        builder.Append(strArray[1]);
                                                        builder.Append("\r\n\r\n");
                                                        builder.Append(str3.Trim());
                                                        builder.Append(";filename=\"");
                                                        builder.Append(str2);
                                                        builder.Append("\";filepath=\"");
                                                        builder.Append(str4);
                                                        builder.Append("\"");
                                                        this.fs = new System.IO.FileStream(tempPath, FileMode.Create);
                                                        context.Items["EasyOne_Web_Upload_FileStream"] = this.fs;
                                                        this.fileStatus = FileStatus.Open;
                                                        context.Items["EasyOne_Web_Upload_FileStatus"] = this.fileStatus;
                                                        Hashtable hashtable = (Hashtable) context.Items["EasyOne_Web_Upload_FileList"];
                                                        hashtable.Add(Path.GetFileNameWithoutExtension(str4), tempPath);
                                                        context.Items["EasyOne_Web_Upload_FileList"] = hashtable;
                                                        this.readBody.AddRange(boundaryBytes);
                                                        this.readBody.AddRange(Encoding.UTF8.GetBytes(builder.ToString().ToCharArray()));
                                                        builder.Remove(0, builder.Length);
                                                    }
                                                    else
                                                    {
                                                        index++;
                                                    }
                                                }
                                                else
                                                {
                                                    this.contentBody.AddRange(c);
                                                }
                                                break;
                                            }
                                            list3.Add(preloadBytes[index]);
                                            index++;
                                        }
                                    }
                                    else
                                    {
                                        this.readStatus = ReadStatus.NoRead;
                                        this.readBody.AddRange(c);
                                    }
                                    break;
                                }
                                list2.Add(preloadBytes[index]);
                                index++;
                            }
                            if (index < preloadBytes.Length)
                            {
                                index++;
                            }
                            else
                            {
                                this.contentBody.AddRange(c);
                                index++;
                            }
                            continue;
                        }
                        this.contentBody.AddRange(c);
                    }
                    else if (index < preloadBytes.Length)
                    {
                        if (this.fileStatus == FileStatus.Open)
                        {
                            byte[] buffer = new byte[c.Count];
                            for (int i = 0; i < c.Count; i++)
                            {
                                buffer[i] = (byte) c[i];
                            }
                            this.fs.Write(buffer, 0, buffer.Length);
                        }
                        else if (this.readStatus == ReadStatus.NoRead)
                        {
                            this.readBody.AddRange(c);
                        }
                        index--;
                    }
                    else
                    {
                        this.contentBody.AddRange(c);
                    }
                }
                else if (this.fileStatus == FileStatus.Open)
                {
                    this.fs.WriteByte(preloadBytes[index]);
                }
                else if (this.readStatus == ReadStatus.NoRead)
                {
                    this.readBody.Add(preloadBytes[index]);
                }
                index++;
            }
            index = 0;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                this.readBody = null;
                this.contentBody = null;
                this.fs.Dispose();
            }
        }

        private void SetFileStatus(HttpContext context)
        {
            if (this.fileStatus == FileStatus.Open)
            {
                this.fs.Flush();
                this.fs.Close();
                this.fileStatus = FileStatus.Close;
                this.originalFileName = string.Empty;
            }
            else if (this.readStatus == ReadStatus.NoRead)
            {
                this.readStatus = ReadStatus.Read;
                context.Items["EasyOne_Web_Upload_FileStatus"] = this.fileStatus;
            }
        }

        public ArrayList ContentBody
        {
            get
            {
                return this.contentBody;
            }
        }

        public System.IO.FileStream FileStream
        {
            get
            {
                return this.fs;
            }
        }

        public FileStatus FStatus
        {
            get
            {
                return this.fileStatus;
            }
        }

        public string OriginalFileName
        {
            get
            {
                return this.originalFileName;
            }
        }

        public ArrayList ReadBody
        {
            get
            {
                return this.readBody;
            }
        }

        public ReadStatus RStatus
        {
            get
            {
                return this.readStatus;
            }
        }
    }
}

