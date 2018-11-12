namespace EasyOne.Shop
{
    using System;

    public class CommonSend : AbstractOperationOfOrder
    {
        private string m_Body;
        private string m_Msg;
        private string m_Title;

        public CommonSend(string title, string body, string successMsg)
        {
            this.m_Title = title;
            this.m_Body = body;
            this.m_Msg = successMsg;
        }

        public override string GetBody()
        {
            return this.m_Body;
        }

        public override string GetOperationMsg()
        {
            return this.m_Msg;
        }

        public override string GetTitle()
        {
            return this.m_Title;
        }
    }
}

