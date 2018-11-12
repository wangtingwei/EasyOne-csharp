namespace EasyOne.Contents
{
    using System;

    public class SpecialTree
    {
        private int id;
        private bool isSpecialCategory;
        private string name;
        private int treeLineType;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public bool IsSpecialCategory
        {
            get
            {
                return this.isSpecialCategory;
            }
            set
            {
                this.isSpecialCategory = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public int TreeLineType
        {
            get
            {
                return this.treeLineType;
            }
            set
            {
                this.treeLineType = value;
            }
        }
    }
}

