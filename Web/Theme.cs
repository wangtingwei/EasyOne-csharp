namespace EasyOne.Web
{
    using System;

    public class Theme
    {
        private string m_Name;

        public Theme(string name)
        {
            this.m_Name = name;
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }
    }
}

