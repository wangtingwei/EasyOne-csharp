namespace EasyOne.Model
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Nullable
    {
        private bool m_IsNull;

        [XmlIgnore]
        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
            protected set
            {
                this.m_IsNull = value;
            }
        }
    }
}

