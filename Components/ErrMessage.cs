namespace EasyOne.Components
{
    using EasyOne.Enumerations;
    using System;
    using System.Xml;

    public class ErrMessage
    {
        private string m_Body;
        private int m_MessageId = -1;
        private string m_Title;

        public ErrMessage(XmlNode node)
        {
            try
            {
                this.m_MessageId = int.Parse(node.Attributes["id"].Value);
                this.m_Title = node.SelectSingleNode("title").InnerText;
                this.m_Body = node.SelectSingleNode("body").InnerText;
            }
            catch
            {
                new CustomException(PEExceptionType.ResourceError, @"资源文件Languages\zh-CHS\Messages.xml错误！").Log();
            }
        }

        public string Body
        {
            get
            {
                return this.m_Body;
            }
            set
            {
                this.m_Body = value;
            }
        }

        public int MessageId
        {
            get
            {
                return this.m_MessageId;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

