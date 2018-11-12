namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Components;
    using EasyOne.Controls;
    using EasyOne.ModelControls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class FrontPageTemplateConfig : AdminPage
    {
        
        private Collection<FrontTemplate> m_InfoList = new Collection<FrontTemplate>();

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            SiteConfigInfo info = this.ViewState["siteConfigInfo"] as SiteConfigInfo;
            this.DoControl(this.Page);
            info.CopyToTemplateInfoList(this.m_InfoList);
            SiteConfig config = new SiteConfig();
            try
            {
                config.Update(info);
                AdminPage.WriteSuccessMsg("前台动态页面模板配置保存成功！", "FrontPageTemplateConfig.aspx");
            }
            catch (FileNotFoundException)
            {
                AdminPage.WriteErrMsg("<li>文件未找到</li>", "FrontPageTemplateConfig.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "FrontPageTemplateConfig.aspx");
            }
            catch (ConfigurationErrorsException)
            {
                AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "FrontPageTemplateConfig.aspx");
            }
        }

        private void DoControl(Control c)
        {
            TemplateSelectControl control = c as TemplateSelectControl;
            if (control != null)
            {
                this.m_InfoList.Add(this.SetInfoValue(control.ID.ToString().Replace("Template", ""), control.Text));
            }
            foreach (Control control2 in c.Controls)
            {
                this.DoControl(control2);
            }
        }

        protected string IsShow()
        {
            string str = "";
            if (!BasePage.IseShop)
            {
                str = "display:none";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                SiteConfigInfo info = SiteConfig.ConfigInfo();
                this.ViewState["siteConfigInfo"] = info;
                string str = HttpContext.Current.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir;
                foreach (FrontTemplate template in info.FrontTemplateList)
                {
                    TemplateSelectControl control = (TemplateSelectControl) ((ContentPlaceHolder) base.Master.FindControl("CphContent")).FindControl("Template" + template.Key);
                    if (control != null)
                    {
                        control.Text = template.Value;
                        if (!string.IsNullOrEmpty(template.Value) && !File.Exists(str + template.Value))
                        {
                            Label label = (Label) ((ContentPlaceHolder) base.Master.FindControl("CphContent")).FindControl("Label" + template.Key);
                            if (label != null)
                            {
                                label.Text = "当前指定的模板页不存在！";
                            }
                        }
                    }
                }
                if (!SiteConfig.ConfigInfo().SiteOption.EnablePointMoneyExp)
                {
                    this.TabTitle4.Style.Add("display", "none");
                }
            }
        }

        private FrontTemplate SetInfoValue(string key, string value)
        {
            FrontTemplate template = new FrontTemplate();
            template.Key = key;
            template.Value = value;
            return template;
        }
    }
}

