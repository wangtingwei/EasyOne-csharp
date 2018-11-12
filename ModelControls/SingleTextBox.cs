namespace EasyOne.ModelControls
{
    using EasyOne.Common;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class SingleTextBox : Control, INamingContainer
    {
        private bool enableNull;
        private Page page;
        private string[] setting;

        public SingleTextBox(Page page, string[] setting, bool enableNull)
        {
            this.page = page;
            this.setting = setting;
            this.enableNull = enableNull;
        }

        public TextBox CreateSingleTextBox()
        {
            string str;
            TextBox box = new TextBox();
            box.ID = "EasyOne2007";
            box.ApplyStyleSheetSkin(this.page);
            box.MaxLength = DataConverter.CLng(this.setting[0]);
            box.Columns = DataConverter.CLng(this.setting[1]);
            if (DataConverter.CBoolean(this.setting[2]))
            {
                box.TextMode = TextBoxMode.Password;
            }
            else
            {
                box.TextMode = TextBoxMode.SingleLine;
            }
            if (((str = this.setting[3]) != null) && (str != "0"))
            {
                if (!(str == "1"))
                {
                    if (str == "2")
                    {
                        box.Attributes.Add("style", "ime-mode:disabled;");
                    }
                }
                else
                {
                    box.Attributes.Add("style", "auto");
                }
            }
            if (this.enableNull)
            {
                RequiredFieldValidator child = new RequiredFieldValidator();
                child.Display = ValidatorDisplay.Dynamic;
                child.ControlToValidate = box.ID;
                child.ErrorMessage = "&nbsp;必填项不能为空";
                this.Controls.Add(child);
            }
            return box;
        }
    }
}

