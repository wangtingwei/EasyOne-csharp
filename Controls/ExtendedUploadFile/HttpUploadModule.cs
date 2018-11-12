namespace EasyOne.Controls.ExtendedUploadFile
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;

    public class HttpUploadModule : IHttpModule
    {
        private DateTime beginTime = DateTime.Now;

        private HttpUploadModule()
        {
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            if (IsUploadRequest(application.Request))
            {
                try
                {
                    if (HttpContext.Current.Request.Cookies["EasyOne_Web_Upload_UploadGUID"] != null)
                    {
                        HttpWorkerRequest workerRequest = GetWorkerRequest();
                        Encoding contentEncoding = application.Context.Request.ContentEncoding;
                        if ((workerRequest != null) && workerRequest.HasEntityBody())
                        {
                            TimeSpan span = DateTime.Now.Subtract(this.beginTime);
                            byte[] boundaryData = GetboundaryData(application.Request.ContentType, contentEncoding);
                            int num = Convert.ToInt32(workerRequest.GetKnownRequestHeader(11));
                            bool writeToDisk = true;
                            if ((num < 0) || (num > Utils.UpLoadFileLength()))
                            {
                                writeToDisk = false;
                            }
                            if ((0.0 > span.TotalHours) || (span.TotalHours > 3.0))
                            {
                                writeToDisk = false;
                            }
                            UploadStatus status = new UploadStatus();
                            application.Context.Items.Add("EasyOne_Web_Upload_FileList", new Hashtable());
                            int num2 = 0;
                            string str = HttpContext.Current.Request.Cookies["EasyOne_Web_Upload_UploadGUID"].Value;
                            if (!string.IsNullOrEmpty(str))
                            {
                                application.Context.Items.Add("EasyOne_Web_Upload_UploadGUID", str);
                            }
                            string uploadFolder = Utils.UploadFolder();
                            ArrayList list = new ArrayList();
                            byte[] preloadedEntityBody = workerRequest.GetPreloadedEntityBody();
                            num2 += preloadedEntityBody.Length;
                            RequestFileContent content = new RequestFileContent(preloadedEntityBody, boundaryData, null, FileStatus.Close, ReadStatus.NoRead, uploadFolder, writeToDisk, application.Context, string.Empty);
                            list.AddRange(content.ReadBody);
                            if (!string.IsNullOrEmpty(str))
                            {
                                status.FileLength = num;
                                status.ReceivedLength = num2;
                                status.FileName = content.OriginalFileName;
                                status.FileCount = ((Hashtable) application.Context.Items["EasyOne_Web_Upload_FileList"]).Count;
                                application.Application["_UploadGUID_" + str] = status;
                            }
                            if (!workerRequest.IsEntireEntityBodyIsPreloaded())
                            {
                                byte[] buffer3;
                                ArrayList contentBody;
                                int num3 = 0x32000;
                                byte[] buffer = new byte[num3];
                                while ((num - num2) >= num3)
                                {
                                    if (!application.Context.Response.IsClientConnected)
                                    {
                                        ReleaseRes(application);
                                    }
                                    num3 = workerRequest.ReadEntityBody(buffer, buffer.Length);
                                    num2 += num3;
                                    contentBody = content.ContentBody;
                                    if (contentBody.Count > 0)
                                    {
                                        buffer3 = new byte[contentBody.Count + buffer.Length];
                                        contentBody.CopyTo(buffer3, 0);
                                        buffer.CopyTo(buffer3, contentBody.Count);
                                        content = new RequestFileContent(buffer3, boundaryData, content.FileStream, content.FStatus, content.RStatus, uploadFolder, writeToDisk, application.Context, content.OriginalFileName);
                                    }
                                    else
                                    {
                                        content = new RequestFileContent(buffer, boundaryData, content.FileStream, content.FStatus, content.RStatus, uploadFolder, writeToDisk, application.Context, content.OriginalFileName);
                                    }
                                    list.AddRange(content.ReadBody);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        status.ReceivedLength = num2;
                                        status.FileName = content.OriginalFileName;
                                        status.FileCount = ((Hashtable) application.Context.Items["EasyOne_Web_Upload_FileList"]).Count;
                                        application.Application["_UploadGUID_" + str] = status;
                                    }
                                }
                                buffer = new byte[num - num2];
                                if (!application.Context.Response.IsClientConnected && (content.FStatus == FileStatus.Open))
                                {
                                    ReleaseRes(application);
                                }
                                num3 = workerRequest.ReadEntityBody(buffer, buffer.Length);
                                contentBody = content.ContentBody;
                                if (contentBody.Count > 0)
                                {
                                    buffer3 = new byte[contentBody.Count + buffer.Length];
                                    contentBody.CopyTo(buffer3, 0);
                                    buffer.CopyTo(buffer3, contentBody.Count);
                                    content = new RequestFileContent(buffer3, boundaryData, content.FileStream, content.FStatus, content.RStatus, uploadFolder, writeToDisk, application.Context, content.OriginalFileName);
                                }
                                else
                                {
                                    content = new RequestFileContent(buffer, boundaryData, content.FileStream, content.FStatus, content.RStatus, uploadFolder, writeToDisk, application.Context, content.OriginalFileName);
                                }
                                list.AddRange(content.ReadBody);
                                if (!string.IsNullOrEmpty(str))
                                {
                                    status.ReceivedLength = num2 + buffer.Length;
                                    status.FileName = content.OriginalFileName;
                                    status.FileCount = ((Hashtable) application.Context.Items["EasyOne_Web_Upload_FileList"]).Count;
                                    if (writeToDisk)
                                    {
                                        status.State = UploadState.Uploaded;
                                    }
                                    else
                                    {
                                        application.Application.Remove("_UploadGUID_" + str);
                                    }
                                }
                            }
                            byte[] array = new byte[list.Count];
                            list.CopyTo(array);
                            InjectTextParts(workerRequest, array);
                            ClearApplication(application);
                        }
                    }
                }
                catch
                {
                    ReleaseRes(application);
                    throw new HttpException("未知错误！");
                }
            }
        }

        private void Application_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            application.Context.Items.Clear();
        }

        private void Application_Error(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            ReleaseRes(application);
        }

        private static void ClearApplication(HttpApplication application)
        {
            if ((application.Context.Items["EasyOne_Web_Upload_FileStatus"] != null) && (((byte) ((FileStatus) application.Context.Items["EasyOne_Web_Upload_FileStatus"])) == 0))
            {
                ((FileStream) application.Context.Items["EasyOne_Web_Upload_FileStream"]).Close();
            }
            if (application.Context.Items["EasyOne_Web_Upload_UploadGUID"] != null)
            {
                string str = (string) application.Context.Items["EasyOne_Web_Upload_UploadGUID"];
                application.Application.Remove("_UploadGUID_" + str);
            }
        }

        public void Dispose()
        {
        }

        private static byte[] GetboundaryData(string contentType, Encoding contentEncoding)
        {
            int index = contentType.IndexOf("boundary=", StringComparison.OrdinalIgnoreCase);
            if (index > 0)
            {
                return contentEncoding.GetBytes("\r\n--" + contentType.Substring(index + 9));
            }
            return null;
        }

        private static HttpWorkerRequest GetWorkerRequest()
        {
            return (HttpWorkerRequest) ((IServiceProvider) HttpContext.Current).GetService(typeof(HttpWorkerRequest));
        }

        public void Init(HttpApplication context)
        {
            if (IsUploadRequest(context.Context.Request))
            {
                context.BeginRequest += new EventHandler(this.Application_BeginRequest);
                context.EndRequest += new EventHandler(this.Application_EndRequest);
                context.Error += new EventHandler(this.Application_Error);
            }
        }

        private static byte[] InjectTextParts(HttpWorkerRequest request, byte[] textParts)
        {
            Type baseType;
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            if (Utils.Context().Request.ServerVariables["SERVER_SOFTWARE"].Equals("Microsoft-IIS/6.0"))
            {
                baseType = request.GetType().BaseType.BaseType;
            }
            else
            {
                baseType = request.GetType().BaseType;
            }
            int length = textParts.Length;
            baseType.GetField("_contentAvailLength", bindingAttr).SetValue(request, length);
            baseType.GetField("_contentTotalLength", bindingAttr).SetValue(request, length);
            baseType.GetField("_preloadedContent", bindingAttr).SetValue(request, textParts);
            baseType.GetField("_preloadedContentRead", bindingAttr).SetValue(request, true);
            return textParts;
        }

        private static bool IsUploadRequest(HttpRequest request)
        {
            return request.ContentType.ToLower().StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase);
        }

        private static void ReleaseRes(HttpApplication application)
        {
            ClearApplication(application);
            if (application.Context.Items["EasyOne_Web_Upload_FileList"] != null)
            {
                Hashtable hashtable = (Hashtable) application.Context.Items["EasyOne_Web_Upload_FileList"];
                foreach (object obj2 in hashtable.Values)
                {
                    if (File.Exists(obj2.ToString()))
                    {
                        File.Delete(obj2.ToString());
                    }
                }
            }
            application.Context.Items.Clear();
        }
    }
}

