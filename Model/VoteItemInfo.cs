namespace EasyOne.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("VoteItemInfo")]
    public class VoteItemInfo
    {
        private string m_Title;
        private int m_VoteNumber;

        [XmlAttribute("Title")]
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

        [XmlAttribute("VoteNumber")]
        public int VoteNumber
        {
            get
            {
                return this.m_VoteNumber;
            }
            set
            {
                this.m_VoteNumber = value;
            }
        }
    }
}

