namespace EasyOne.Model.Shop
{
    using EasyOne.Model;
    using System;

    [Serializable]
    public class PackageInfo : EasyOne.Model.Nullable
    {
        private double m_GoodsWeightMax;
        private double m_GoodsWeightMin;
        private int m_PackageId;
        private string m_PackageName;
        private double m_PackageWeight;

        public PackageInfo()
        {
        }

        public PackageInfo(bool value)
        {
            base.IsNull = value;
        }

        public double GoodsWeightMax
        {
            get
            {
                return this.m_GoodsWeightMax;
            }
            set
            {
                this.m_GoodsWeightMax = value;
            }
        }

        public double GoodsWeightMin
        {
            get
            {
                return this.m_GoodsWeightMin;
            }
            set
            {
                this.m_GoodsWeightMin = value;
            }
        }

        public int PackageId
        {
            get
            {
                return this.m_PackageId;
            }
            set
            {
                this.m_PackageId = value;
            }
        }

        public string PackageName
        {
            get
            {
                return this.m_PackageName;
            }
            set
            {
                this.m_PackageName = value;
            }
        }

        public double PackageWeight
        {
            get
            {
                return this.m_PackageWeight;
            }
            set
            {
                this.m_PackageWeight = value;
            }
        }
    }
}

