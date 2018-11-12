namespace EasyOne.Shop
{
    using EasyOne.Accessories;
    using EasyOne.Crm;
    using EasyOne.Model.Crm;
    using EasyOne.Model.Shop;
    using System;

    public class SmsOfOrder : AbstractMessageOfOrder
    {
        private OrderInfo m_OrderInfo;
        private string m_Reserve;
        private string m_SendNum;
        private SendType m_SendType;

        public SmsOfOrder(string reserve, SendType sendType, OrderInfo orderInfo)
        {
            this.m_OrderInfo = new OrderInfo(true);
            this.m_Reserve = reserve;
            this.m_SendType = sendType;
            this.m_OrderInfo = orderInfo;
        }

        public SmsOfOrder(string reserve, SendType sendType, string sendNum)
        {
            this.m_OrderInfo = new OrderInfo(true);
            this.m_Reserve = reserve;
            this.m_SendType = sendType;
            this.m_SendNum = sendNum;
        }

        public override void Send()
        {
            string str = string.Empty;
            switch (this.m_SendType)
            {
                case SendType.SendToUser:
                    str = "会员";
                    if (string.IsNullOrEmpty(this.m_SendNum))
                    {
                        ContacterInfo contacterByUserName = Contacter.GetContacterByUserName(this.m_OrderInfo.UserName);
                        this.m_SendNum = contacterByUserName.Mobile;
                        if (string.IsNullOrEmpty(this.m_SendNum))
                        {
                            this.m_SendNum = contacterByUserName.Phs;
                        }
                    }
                    break;

                case SendType.SendToContacter:
                    str = "收货人";
                    if (string.IsNullOrEmpty(this.m_SendNum))
                    {
                        this.m_SendNum = this.m_OrderInfo.Mobile;
                    }
                    break;

                case SendType.SendToAdmin:
                    str = "管理员";
                    break;
            }
            if (string.IsNullOrEmpty(this.m_SendNum))
            {
                base.ErrorMsg.Append("<br>未指定接收号码，向" + str + "发送手机短信失败！");
            }
            else if (string.IsNullOrEmpty(base.MessageBody))
            {
                base.ErrorMsg.Append("<br>短信内容为空，向" + str + "发送手机短信失败！");
            }
            else
            {
                SmsMessage.SendMessage(this.m_SendNum, base.MessageBody, "0", DateTime.Now.ToString(), this.m_Reserve);
                base.SuccessMsg.Append("<br>已经向" + str + "发送了一条手机短信，通知他");
                base.SuccessMsg.Append(base.OperationMsg);
                base.SuccessMsg.Append("！");
            }
        }
    }
}

