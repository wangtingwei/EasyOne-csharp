namespace EasyOne.Controls
{
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.Design.WebControls;
    //using System.Web.UI.Design.

    public class PagerDesigner : PanelContainerDesigner
    {
        private AspNetPager wb;

        public override string GetDesignTimeHtml()
        {
            this.wb = (AspNetPager) base.Component;
            this.wb.RecordCount = 0xe1;
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            this.wb.RenderControl(writer2);
            return writer.ToString();
        }

        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string instruction = "创建控件时出错：" + e.Message;
            return base.CreatePlaceHolderDesignTimeHtml(instruction);
        }
    }
}

