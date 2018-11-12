namespace EasyOne.Controls.ExtendedUploadFile
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class UploadHelper
    {
        private string guid;
        private string script = string.Empty;

        public UploadHelper()
        {
            if (Utils.IsAccordantBrowser())
            {
                this.script = "\r\n\t\t\t\t\t<script language=javascript>\r\n\t\t\t\t\t<!--\r\n\t\t\t\t\turl='${url}$';\r\n\t\t\t\t\tvar submited = false;\r\n\t\t\t\t\tfunction openProgress()\r\n\t\t\t\t\t{\r\n\t\t\t\t\t\tif(!submited)\r\n\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\tvar ary = document.getElementsByTagName('INPUT');\r\n\t\t\t\t\t\t\tvar openBar = false;\r\n\t\t\t\t\t\t\tfor(var i=0;i<ary.length;i++)\r\n\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\tvar obj = ary[i];\r\n\t\t\t\t\t\t\t\tif(obj.type  == 'file')\r\n\t\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\t\tif(obj.value != '')\r\n\t\t\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\t\t\topenBar = true;\r\n\t\t\t\t\t\t\t\t\t\tbreak;\r\n\t\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\tif(openBar)\r\n\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\twindow.showModelessDialog(url, window, 'status:no;help:no;resizable:no;scroll:no;dialogWidth:398px;dialogHeight:200px');\r\n\t\t\t\t\t\t\t\tsubmited = true;\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\treturn true;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\telse\r\n\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\tevent.srcElement.disabled = true;\r\n\t\t\t\t\t\t\treturn false;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t}\r\n\t\t\t\t\t//-->\r\n\t\t\t\t\t</script>";
            }
            else
            {
                this.script = "\r\n\t\t\t\t\t<script language=javascript>\r\n\t\t\t\t\t<!--\r\n\t\t\t\t\turl='${url}$';\r\n\t\t\t\t\tvar submited = false;\r\n\t\t\t\t\tfunction openProgress()\r\n\t\t\t\t\t{\r\n\t\t\t\t\t\tif(!submited)\r\n\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\tvar ary = document.getElementsByTagName('INPUT');\r\n\t\t\t\t\t\t\tvar openBar = false;\r\n\t\t\t\t\t\t\tfor(var i=0;i<ary.length;i++)\r\n\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\tvar obj = ary[i];\r\n\t\t\t\t\t\t\t\tif(obj.type  == 'file')\r\n\t\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\t\tif(obj.value != '')\r\n\t\t\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\t\t\topenBar = true;\r\n\t\t\t\t\t\t\t\t\t\tbreak;\r\n\t\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\tif(openBar)\r\n\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\tvar swd = window.screen.availWidth;\r\n\t\t\t\t\t\t\t\tvar sht = window.screen.availHeight;\r\n\t\t\t\t\t\t\t\tvar wd = 398;\r\n\t\t\t\t\t\t\t\tvar ht =170;\r\n\t\t\t\t\t\t\t\tvar left = (swd-wd)/2;\r\n\t\t\t\t\t\t\t\tvar top = (sht-ht)/2;\r\n\t\t\t\t\t\t\t\twindow.open(url,'_blank','status=no,toolbar=no,menubar=no,location=no,height='+ht+',width='+wd+',left='+left+',top='+top, true);\r\n\t\t\t\t\t\t\t\tsubmited = true;\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t\treturn true;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\telse\r\n\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\tevent.srcElement.disabled = true;\r\n\t\t\t\t\t\t\treturn false;\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t}\r\n\t\t\t\t\t//-->\r\n\t\t\t\t\t</script>";
            }
            this.guid = Guid.NewGuid().ToString();
        }

        public static UploadFile GetUploadFile(string name)
        {
            UploadFile file = new UploadFile(name);
            if (string.IsNullOrEmpty(file.FilePath))
            {
                return null;
            }
            return file;
        }

        public static UploadFileCollection GetUploadFileList(string name)
        {
            UploadFileCollection files = new UploadFileCollection();
            string str = Utils.Context().Request[name];
            if (!string.IsNullOrEmpty(str))
            {
                foreach (string str2 in str.Split(new char[] { ',' }))
                {
                    files.Add(new UploadFile(str2));
                }
            }
            return files;
        }

        public void RegisterProgressBar(Button uploadButton, bool causesValidation)
        {
            if (causesValidation)
            {
                uploadButton.CausesValidation = false;
                uploadButton.Attributes["onclick"] = "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate();if(!Page_BlockSubmit){openProgress();}";
            }
            else
            {
                uploadButton.Attributes["onclick"] = "openProgress();";
            }
            string newValue = "~/UploadProgress/UploadProgressTemplate.aspx?UploadID=" + this.guid;
            Page handler = (Page) Utils.Context().Handler;
            HiddenField child = new HiddenField {
                ID = "EasyOne_Web_Upload_UploadGUID",
                Value = this.guid
            };
            handler.Form.Controls.Add(child);
            if (HttpContext.Current.Response.Cookies["EasyOne_Web_Upload_UploadGUID"] != null)
            {
                HttpContext.Current.Response.Cookies.Set(new HttpCookie("EasyOne_Web_Upload_UploadGUID", this.guid));
            }
            else
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("EasyOne_Web_Upload_UploadGUID", this.guid));
            }
            this.script = this.script.Replace("${url}$", newValue);
            if (!handler.ClientScript.IsClientScriptBlockRegistered("ProgressScript"))
            {
                handler.ClientScript.RegisterClientScriptBlock(base.GetType(), "ProgressScript", this.script);
            }
            UploadStatus status = new UploadStatus();
            handler.Application.Add("_UploadGUID_" + this.guid, status);
        }

        public void SetUploadFolder(string folderPath)
        {
            if (!Path.IsPathRooted(folderPath))
            {
                if (!string.IsNullOrEmpty(this.guid))
                {
                    Utils.Context().Application.Remove("_UploadGUID_" + this.guid);
                }
                throw new IOException("Invaild path.");
            }
            if (!Directory.Exists(folderPath))
            {
                if (!string.IsNullOrEmpty(this.guid))
                {
                    Utils.Context().Application.Remove("_UploadGUID_" + this.guid);
                }
                throw new FileNotFoundException("Special path does not exsit.");
            }
            HiddenField child = new HiddenField {
                ID = "EasyOne_Web_Upload_UploadFolder",
                Value = folderPath
            };
            ((Page) Utils.Context().Handler).Form.Controls.Add(child);
        }
    }
}

