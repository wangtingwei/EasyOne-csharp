namespace EasyOne.Model.Survey
{
    using EasyOne.Model;
    using System;

    public class SurveyVoteInfo : EasyOne.Model.Nullable
    {
        private int m_ID;
        private int m_OptionId;
        private int m_QuestionId;
        private int m_SurveyId;
        private int m_VoteAmount;

        public SurveyVoteInfo()
        {
        }

        public SurveyVoteInfo(bool value)
        {
            base.IsNull = value;
        }

        public int Id
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }

        public int OptionId
        {
            get
            {
                return this.m_OptionId;
            }
            set
            {
                this.m_OptionId = value;
            }
        }

        public int QuestionId
        {
            get
            {
                return this.m_QuestionId;
            }
            set
            {
                this.m_QuestionId = value;
            }
        }

        public int SurveyId
        {
            get
            {
                return this.m_SurveyId;
            }
            set
            {
                this.m_SurveyId = value;
            }
        }

        public int VoteAmount
        {
            get
            {
                return this.m_VoteAmount;
            }
            set
            {
                this.m_VoteAmount = value;
            }
        }
    }
}

