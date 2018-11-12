namespace EasyOne.Controls
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [Serializable, ToolboxData("<{0}:GridViewTemplate runat=server></{0}:GridViewTemplate>"), DefaultProperty("Text")]
    public class GridViewTemplate : ITemplate
    {
        private string m_ControlId;
        private string m_ControlType;
        private string m_DataField;
        private Type m_DataItemType;

        public GridViewTemplate(string controlId) : this(controlId, "TextBox", null, "")
        {
        }

        public GridViewTemplate(string controlId, string controlType) : this(controlId, controlType, null, "")
        {
        }

        public GridViewTemplate(string controlId, string controlType, Type dataItemType, string dataField)
        {
            this.m_ControlId = controlId;
            this.m_ControlType = controlType;
            this.m_DataItemType = dataItemType;
            this.m_DataField = dataField;
        }

        private void control_DataBinding(object sender, EventArgs e)
        {
            string controlType = this.m_ControlType;
            if (controlType != null)
            {
                GridViewRow namingContainer;
                if (!(controlType == "Label"))
                {
                    if (!(controlType == "TextBox"))
                    {
                        if (controlType == "CheckBox")
                        {
                            CheckBox box2 = (CheckBox) sender;
                            namingContainer = (GridViewRow) box2.NamingContainer;
                        }
                        return;
                    }
                }
                else
                {
                    Label label = (Label) sender;
                    namingContainer = (GridViewRow) label.NamingContainer;
                    if (!string.IsNullOrEmpty(this.m_DataField))
                    {
                        if (this.m_DataItemType != null)
                        {
                            label.Text = this.m_DataItemType.InvokeMember(this.m_DataField, BindingFlags.GetProperty, null, namingContainer.DataItem, null).ToString();
                            return;
                        }
                        label.Text = this.m_DataField;
                    }
                    return;
                }
                TextBox box = (TextBox) sender;
                namingContainer = (GridViewRow) box.NamingContainer;
                if (!string.IsNullOrEmpty(this.m_DataField))
                {
                    if (this.m_DataItemType != null)
                    {
                        box.Text = this.m_DataItemType.InvokeMember(this.m_DataField, BindingFlags.GetProperty, null, namingContainer.DataItem, null).ToString();
                    }
                    else
                    {
                        box.Text = this.m_DataField;
                    }
                }
            }
        }

        public void InstantiateIn(Control container)
        {
            string controlType = this.m_ControlType;
            if (controlType != null)
            {
                if (!(controlType == "Label"))
                {
                    if (!(controlType == "TextBox"))
                    {
                        if (controlType == "CheckBox")
                        {
                            CheckBox box2 = new CheckBox {
                                ID = this.m_ControlId,
                                Checked = true
                            };
                            container.Controls.Add(box2);
                        }
                        return;
                    }
                }
                else
                {
                    Label label = new Label {
                        ID = this.m_ControlId
                    };
                    label.DataBinding += new EventHandler(this.control_DataBinding);
                    container.Controls.Add(label);
                    return;
                }
                TextBox child = new TextBox {
                    ID = this.m_ControlId,
                    Width = Unit.Pixel(50),
                    Height = Unit.Pixel(12)
                };
                child.DataBinding += new EventHandler(this.control_DataBinding);
                container.Controls.Add(child);
            }
        }
    }
}

