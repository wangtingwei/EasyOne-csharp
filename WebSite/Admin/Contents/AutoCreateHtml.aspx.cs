namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Web.UI.WebControls;
    using System.Xml;
    using EasyOne.Model.Contents;

    public partial class AutoCreateHtml : AdminPage
    {

        protected void CreateCategoryListById_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                try
                {
                    string filename = base.Server.MapPath("~/Config/AutoCreate.config");
                    XmlDocument document = new XmlDocument();
                    document.Load(filename);
                    XmlNode node = document.SelectSingleNode("Jobs");
                    if (node != null)
                    {
                        XmlElement element = (XmlElement) node;
                        element.SetAttribute("minutes", this.TxtMinutes.Text.Trim());
                        XmlNode node2 = node.ChildNodes[0];
                        element = (XmlElement) node2;
                        element.SetAttribute("number", this.TxtNumber.Text.Trim());
                        element.SetAttribute("enabled", this.ChkIsEnable.Checked.ToString());
                        element.SetAttribute("enableShutDown", this.ChkIsShutDown.Checked.ToString());
                        element.SetAttribute("nodeIdArray", this.GetCreateHtmlNodesList(false));
                        element.SetAttribute("createNodePage", this.ChkCateogry.Checked.ToString());
                        element.SetAttribute("createIndexPage", this.RadLIndexPage.SelectedValue);
                        document.Save(filename);
                        AdminPage.WriteSuccessMsg("定时生成设置保存成功", "AutoCreateHtml.aspx");
                    }
                }
                catch (FileNotFoundException)
                {
                    AdminPage.WriteErrMsg("<li>文件未找到</li>", "AutoCreateHtml.aspx");
                }
                catch (UnauthorizedAccessException)
                {
                    AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "AutoCreateHtml.aspx");
                }
                catch (ConfigurationErrorsException)
                {
                    AdminPage.WriteErrMsg("<li>检查您的服务器是否给配置文件或文件夹写入权限。</li>", "AutoCreateHtml.aspx");
                }
            }
        }

        protected string GetCreateHtmlNodesList(bool isSelectedAll)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.LstNodes.Items.Count; i++)
            {
                if (isSelectedAll)
                {
                    StringHelper.AppendString(sb, this.LstNodes.Items[i].Value);
                }
                else if (this.LstNodes.Items[i].Selected)
                {
                    StringHelper.AppendString(sb, this.LstNodes.Items[i].Value);
                }
            }
            return sb.ToString();
        }

        public void ListDataBind()
        {
            IList<NodeInfo> nodeNameForContainerItems = Nodes.GetNodeNameForContainerItems();
            this.LstNodes.Items.Clear();
            this.LstNodes.DataSource = nodeNameForContainerItems;
            this.LstNodes.DataBind();
        }

        private void Modify()
        {
            string filename = base.Server.MapPath("~/Config/AutoCreate.config");
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            XmlNode node = document.SelectSingleNode("Jobs");
            XmlAttribute attribute = node.Attributes["minutes"];
            if (attribute != null)
            {
                this.TxtMinutes.Text = attribute.Value;
            }
            XmlNode node2 = node.ChildNodes[0];
            attribute = node2.Attributes["enabled"];
            if (attribute != null)
            {
                this.ChkIsEnable.Checked = DataConverter.CBoolean(attribute.Value);
            }
            attribute = node2.Attributes["enableShutDown"];
            if (attribute != null)
            {
                this.ChkIsShutDown.Checked = DataConverter.CBoolean(attribute.Value);
            }
            attribute = node2.Attributes["number"];
            if (attribute != null)
            {
                this.TxtNumber.Text = attribute.Value;
            }
            attribute = node2.Attributes["createNodePage"];
            if (attribute != null)
            {
                this.ChkCateogry.Checked = DataConverter.CBoolean(attribute.Value);
            }
            attribute = node2.Attributes["createIndexPage"];
            if (attribute != null)
            {
                this.RadLIndexPage.SelectedIndex = this.RadLIndexPage.Items.IndexOf(this.RadLIndexPage.Items.FindByValue(attribute.Value));
            }
            attribute = node2.Attributes["nodeIdArray"];
            if ((attribute != null) && !string.IsNullOrEmpty(attribute.Value))
            {
                string[] strArray = attribute.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    ListItem item = this.LstNodes.Items.FindByValue(strArray[i]);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                RolePermissions.BusinessAccessCheck(OperateCode.CreateHtmlManage);
                this.ListDataBind();
                this.Modify();
            }
        }
    }
}

