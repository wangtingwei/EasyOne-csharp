namespace EasyOne.WebSite.Admin.Template
{
    using EasyOne.Controls;
    using EasyOne.Model.TemplateProc;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Text;
    using System.Web.UI.WebControls;
    using System.Xml;

    public partial class LabelBatch : AdminPage
    {

        protected void BtnFinal_Click(object sender, EventArgs e)
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(this.ReplaceSource.Text))
            {
                if (string.Compare(this.RbtSearType.SelectedValue, "type", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (!string.IsNullOrEmpty(this.RbtLabelType.SelectedValue))
                    {
                        foreach (LabelManageInfo info in LabelManage.GetLabelList(this.RbtLabelType.SelectedValue))
                        {
                            if (!this.ReplaceProc(info.Name))
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                }
                else if (string.Compare(this.RbtSearType.SelectedValue, "keyword", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (!string.IsNullOrEmpty(this.KeyWord.Text))
                    {
                        foreach (LabelManageInfo info2 in LabelManage.GetLabelList(1, 1, this.KeyWord.Text, ""))
                        {
                            if (!this.ReplaceProc(info2.Name))
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (LabelManageInfo info3 in LabelManage.GetLabelList(string.Empty))
                    {
                        if (!this.ReplaceProc(info3.Name))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    BasePage.ResponseRedirect("LabelManage.aspx");
                }
                else
                {
                    base.Response.Write("处理错误");
                }
            }
        }

        private void InitListLabelType()
        {
            this.RbtLabelType.DataSource = LabelManage.GetLabelTypeList();
            this.RbtLabelType.DataTextField = "Name";
            this.RbtLabelType.DataValueField = "Name";
            this.RbtLabelType.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RbtSearType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.Compare(this.RbtSearType.SelectedValue, "type", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.RbtLabelType.Visible = true;
                this.KeyWord.Visible = false;
                this.InitListLabelType();
            }
            else if (string.Compare(this.RbtSearType.SelectedValue, "keyword", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.RbtLabelType.Visible = false;
                this.KeyWord.Visible = true;
            }
            else
            {
                this.RbtLabelType.Visible = false;
                this.KeyWord.Visible = false;
            }
        }

        protected bool ReplaceProc(string labelName)
        {
            LabelManageInfo ainfo = new LabelManageInfo();
            ainfo = LabelManage.GetLabelByName(labelName);
            if (string.Compare(this.RadioButtonList2.SelectedValue, "labelname", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string str = ainfo.Name.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
                if (this.ChkAdd.Checked)
                {
                    ainfo.Name = str;
                    return LabelManage.Add(ainfo);
                }
                return LabelManage.Update(ainfo, str);
            }
            if (string.Compare(this.RadioButtonList2.SelectedValue, "labeltype", StringComparison.OrdinalIgnoreCase) == 0)
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(ainfo.Define.ToString());
                if (document.SelectSingleNode("root/LabelType") != null)
                {
                    document.SelectSingleNode("root/LabelType").InnerText = document.SelectSingleNode("root/LabelType").InnerText.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
                }
                ainfo.Define = new StringBuilder(document.InnerXml);
                if (this.ChkAdd.Checked)
                {
                    return LabelManage.Add(ainfo);
                }
                return LabelManage.Update(ainfo);
            }
            if (string.Compare(this.RadioButtonList2.SelectedValue, "labelintro", StringComparison.OrdinalIgnoreCase) == 0)
            {
                XmlDocument document2 = new XmlDocument();
                document2.LoadXml(ainfo.Define.ToString());
                if (document2.SelectSingleNode("root/LabelIntro") != null)
                {
                    document2.SelectSingleNode("root/LabelIntro").InnerText = document2.SelectSingleNode("root/LabelIntro").InnerText.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
                }
                ainfo.Define = new StringBuilder(document2.InnerXml);
                if (this.ChkAdd.Checked)
                {
                    return LabelManage.Add(ainfo);
                }
                return LabelManage.Update(ainfo);
            }
            if (string.Compare(this.RadioButtonList2.SelectedValue, "sql", StringComparison.OrdinalIgnoreCase) == 0)
            {
                XmlDocument document3 = new XmlDocument();
                document3.LoadXml(ainfo.Define.ToString());
                if (document3.SelectSingleNode("root/LabelSqlString") != null)
                {
                    document3.SelectSingleNode("root/LabelSqlString").InnerText = document3.SelectSingleNode("root/LabelSqlString").InnerText.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
                }
                ainfo.Define = new StringBuilder(document3.InnerXml);
                if (this.ChkAdd.Checked)
                {
                    return LabelManage.Add(ainfo);
                }
                return LabelManage.Update(ainfo);
            }
            if (string.Compare(this.RadioButtonList2.SelectedValue, "template", StringComparison.OrdinalIgnoreCase) == 0)
            {
                XmlDocument document4 = new XmlDocument();
                document4.LoadXml(ainfo.Define.ToString());
                if (document4.SelectSingleNode("root/LabelTemplate") != null)
                {
                    document4.SelectSingleNode("root/LabelTemplate").InnerText = document4.SelectSingleNode("root/LabelTemplate").InnerText.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
                }
                ainfo.Define = new StringBuilder(document4.InnerXml);
                if (this.ChkAdd.Checked)
                {
                    return LabelManage.Add(ainfo);
                }
                return LabelManage.Update(ainfo);
            }
            string newLableName = ainfo.Name.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
            XmlDocument document5 = new XmlDocument();
            document5.LoadXml(ainfo.Define.ToString());
            if (document5.SelectSingleNode("root/LabelType") != null)
            {
                document5.SelectSingleNode("root/LabelType").InnerText = document5.SelectSingleNode("root/LabelType").InnerText.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
            }
            if (document5.SelectSingleNode("root/LabelIntro") != null)
            {
                document5.SelectSingleNode("root/LabelIntro").InnerText = document5.SelectSingleNode("root/LabelIntro").InnerText.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
            }
            if (document5.SelectSingleNode("root/LabelSqlString") != null)
            {
                document5.SelectSingleNode("root/LabelSqlString").InnerText = document5.SelectSingleNode("root/LabelSqlString").InnerText.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
            }
            if (document5.SelectSingleNode("root/LabelTemplate") != null)
            {
                document5.SelectSingleNode("root/LabelTemplate").InnerText = document5.SelectSingleNode("root/LabelTemplate").InnerText.Replace(this.ReplaceSource.Text, this.ReplaceTarget.Text);
            }
            ainfo.Define = new StringBuilder(document5.InnerXml);
            if (this.ChkAdd.Checked)
            {
                ainfo.Name = newLableName;
                return LabelManage.Add(ainfo);
            }
            return LabelManage.Update(ainfo, newLableName);
        }
    }
}

