namespace EasyOne.Controls
{
    using System;
    using System.Web.UI.WebControls;

    internal sealed class InputCheckBoxField : CheckBoxField
    {
        public const string CheckBoxID = "CheckBoxButton";

        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);
            if (cell.Controls.Count == 0)
            {
                CheckBox child = new CheckBox {
                    ID = "CheckBoxButton"
                };
                cell.Controls.Add(child);
            }
        }
    }
}

