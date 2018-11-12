namespace EasyOne.Shop
{
    using System;
    using System.Text;

    public abstract class AbstractMessageOfOrder
    {
        internal StringBuilder ErrorMsg = new StringBuilder();
        private AbstractOperationOfOrder m_Implementor;
        internal StringBuilder SuccessMsg = new StringBuilder();

        protected AbstractMessageOfOrder()
        {
        }

        public abstract void Send();

        public string ErrMsgList
        {
            get
            {
                return this.ErrorMsg.ToString();
            }
        }

        public AbstractOperationOfOrder Implementor
        {
            get
            {
                return this.m_Implementor;
            }
            set
            {
                this.m_Implementor = value;
            }
        }

        protected string MessageBody
        {
            get
            {
                return this.m_Implementor.GetBody();
            }
        }

        protected string MessageTitle
        {
            get
            {
                return this.m_Implementor.GetTitle();
            }
        }

        protected string OperationMsg
        {
            get
            {
                return this.m_Implementor.GetOperationMsg();
            }
        }

        public string SuccessMsgList
        {
            get
            {
                return this.SuccessMsg.ToString();
            }
        }
    }
}

