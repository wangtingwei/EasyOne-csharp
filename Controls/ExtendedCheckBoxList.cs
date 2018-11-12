namespace EasyOne.Controls
{
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExtendedCheckBoxList ID=\"EChkl\" runat=\"server\"></{0}:ExtendedCheckBoxList>")]
    public class ExtendedCheckBoxList : CheckBoxList
    {
        public string SelectList()
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(this.Split))
            {
                this.Split = ",";
            }
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Selected)
                {
                    builder.Append(this.Items[i].Value);
                    builder.Append(this.Split);
                }
            }
            if (builder.Length > 1)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        public void SetSelectValue(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (string.IsNullOrEmpty(this.Split))
                {
                    this.Split = ",";
                }
                string[] strArray = id.Split(new string[] { this.Split }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    ListItem item = this.Items.FindByValue(strArray[i]);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public string Split
        {
            get
            {
                object obj2 = this.ViewState["Split"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Split"] = value;
            }
        }
    }
}

