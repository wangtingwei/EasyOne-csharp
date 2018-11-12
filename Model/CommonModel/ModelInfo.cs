namespace EasyOne.Model.CommonModel
{
    using EasyOne.Enumerations;
    using EasyOne.Model;
    using System;

    public class ModelInfo : EasyOne.Model.Nullable
    {
        private string m_AddInfoFilePath;
        private string m_AdvanceSearchFormTemplate;
        private string m_AdvanceSearchTemplate;
        private string m_BatchInfoFilePath;
        private ProductCharacter m_Character;
        private string m_ChargeTips;
        private string m_CommentManageTemplate;
        private string m_DefaultTemplateFile;
        private string m_Description;
        private bool m_Disabled;
        private bool m_EnableCharge;
        private bool m_EnableSignIn;
        private bool m_EnableVote;
        private string m_Field;
        private bool m_IsCountHits;
        private bool m_IsEshop;
        private string m_ItemIcon;
        private string m_ItemName;
        private string m_ItemUnit;
        private string m_ManageInfoFilePath;
        private int m_MaxPerUser;
        private int m_ModelId;
        private string m_ModelName;
        private string m_PreviewInfoFilePath;
        private string m_PrintTemplate;
        private string m_SearchTemplate;
        private string m_TableName;

        public ModelInfo()
        {
        }

        public ModelInfo(bool value)
        {
            base.IsNull = value;
        }

        public string AddInfoFilePath
        {
            get
            {
                return this.m_AddInfoFilePath;
            }
            set
            {
                this.m_AddInfoFilePath = value;
            }
        }

        public string AdvanceSearchFormTemplate
        {
            get
            {
                return this.m_AdvanceSearchFormTemplate;
            }
            set
            {
                this.m_AdvanceSearchFormTemplate = value;
            }
        }

        public string AdvanceSearchTemplate
        {
            get
            {
                return this.m_AdvanceSearchTemplate;
            }
            set
            {
                this.m_AdvanceSearchTemplate = value;
            }
        }

        public string BatchInfoFilePath
        {
            get
            {
                return this.m_BatchInfoFilePath;
            }
            set
            {
                this.m_BatchInfoFilePath = value;
            }
        }

        public ProductCharacter Character
        {
            get
            {
                return this.m_Character;
            }
            set
            {
                this.m_Character = value;
            }
        }

        public string ChargeTips
        {
            get
            {
                return this.m_ChargeTips;
            }
            set
            {
                this.m_ChargeTips = value;
            }
        }

        public string CommentManageTemplate
        {
            get
            {
                return this.m_CommentManageTemplate;
            }
            set
            {
                this.m_CommentManageTemplate = value;
            }
        }

        public string DefaultTemplateFile
        {
            get
            {
                return this.m_DefaultTemplateFile;
            }
            set
            {
                this.m_DefaultTemplateFile = value;
            }
        }

        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                this.m_Description = value;
            }
        }

        public bool Disabled
        {
            get
            {
                return this.m_Disabled;
            }
            set
            {
                this.m_Disabled = value;
            }
        }

        public bool EnableCharge
        {
            get
            {
                return this.m_EnableCharge;
            }
            set
            {
                this.m_EnableCharge = value;
            }
        }

        public bool EnableSignIn
        {
            get
            {
                return this.m_EnableSignIn;
            }
            set
            {
                this.m_EnableSignIn = value;
            }
        }

        public bool EnbaleVote
        {
            get
            {
                return this.m_EnableVote;
            }
            set
            {
                this.m_EnableVote = value;
            }
        }

        public string Field
        {
            get
            {
                return this.m_Field;
            }
            set
            {
                this.m_Field = value;
            }
        }

        public bool IsCountHits
        {
            get
            {
                return this.m_IsCountHits;
            }
            set
            {
                this.m_IsCountHits = value;
            }
        }

        public bool IsEshop
        {
            get
            {
                return this.m_IsEshop;
            }
            set
            {
                this.m_IsEshop = value;
            }
        }

        public string ItemIcon
        {
            get
            {
                return this.m_ItemIcon;
            }
            set
            {
                this.m_ItemIcon = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this.m_ItemName;
            }
            set
            {
                this.m_ItemName = value;
            }
        }

        public string ItemUnit
        {
            get
            {
                return this.m_ItemUnit;
            }
            set
            {
                this.m_ItemUnit = value;
            }
        }

        public string ManageInfoFilePath
        {
            get
            {
                return this.m_ManageInfoFilePath;
            }
            set
            {
                this.m_ManageInfoFilePath = value;
            }
        }

        public int MaxPerUser
        {
            get
            {
                return this.m_MaxPerUser;
            }
            set
            {
                this.m_MaxPerUser = value;
            }
        }

        public int ModelId
        {
            get
            {
                return this.m_ModelId;
            }
            set
            {
                this.m_ModelId = value;
            }
        }

        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
            set
            {
                this.m_ModelName = value;
            }
        }

        public string PreviewInfoFilePath
        {
            get
            {
                return this.m_PreviewInfoFilePath;
            }
            set
            {
                this.m_PreviewInfoFilePath = value;
            }
        }

        public string PrintTemplate
        {
            get
            {
                return this.m_PrintTemplate;
            }
            set
            {
                this.m_PrintTemplate = value;
            }
        }

        public string SearchTemplate
        {
            get
            {
                return this.m_SearchTemplate;
            }
            set
            {
                this.m_SearchTemplate = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }
    }
}

