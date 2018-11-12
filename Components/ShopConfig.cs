namespace EasyOne.Components
{
    using System;

    [Serializable]
    public class ShopConfig
    {
        private string m_AgainBuyUrl;
        private string m_City;
        private string m_ConsignmentFormat;
        private string m_ContinueBuy;
        private string m_Country;
        private string m_EmailOfDeliver;
        private string m_EmailOfInvoice;
        private string m_EmailOfOrderConfirm;
        private string m_EmailOfReceiptMoney;
        private string m_EmailOfRefund;
        private string m_EmailOfSendCard;
        private bool m_EnableCoupon;
        private bool m_EnableGuestBuy;
        private bool m_EnablePartPay;
        private int m_GwcThumbsHeight;
        private int m_GwcThumbsWidth;
        private bool m_IsGwcShowProducdtThumb;
        private bool m_IsNull;
        private bool m_IsOrderProductListShowThumb;
        private bool m_IsPaymentShowProducdtThumb;
        private bool m_IsPayPassword;
        private bool m_IsPreviewShowProducdtThumb;
        private bool m_IsProductListThumb;
        private bool m_IsSetFunctionary;
        private bool m_IsShowGwcMarkPrice;
        private bool m_IsShowGwcProductType;
        private bool m_IsShowGwcSaleType;
        private bool m_IsShowPaymentMarkPrice;
        private bool m_IsShowPaymentProductType;
        private bool m_IsShowPaymentSaleType;
        private bool m_IsShowPreviewMarkPrice;
        private bool m_IsShowPreviewProductType;
        private bool m_IsShowPreviewSaleType;
        private bool m_IsThumb;
        private bool m_IsWatermark;
        private decimal m_MoneyPresentPoint;
        private string m_OrderFormat;
        private int m_OrderProductListThumbsHeight;
        private int m_OrderProductListThumbsWidth;
        private int m_OrderProductNumber;
        private int m_PartPayAge;
        private int m_PaymentThumbsHeight;
        private int m_PaymentThumbsWidth;
        private string m_PostCode;
        private string m_PrefixOrderFormNum;
        private string m_PrefixPaymentNum;
        private int m_PreviewThumbsHeight;
        private int m_PreviewThumbsWidth;
        private int m_ProductListThumbsHeight;
        private int m_ProductListThumbsWidth;
        private string m_Province;
        private float m_TaxRate;
        private int m_TaxRateType;

        public ShopConfig()
        {
        }

        public ShopConfig(bool value)
        {
            this.m_IsNull = value;
        }

        public string AgainBuyUrl
        {
            get
            {
                return this.m_AgainBuyUrl;
            }
            set
            {
                this.m_AgainBuyUrl = value;
            }
        }

        public string City
        {
            get
            {
                return this.m_City;
            }
            set
            {
                this.m_City = value;
            }
        }

        public string ConsignmentFormat
        {
            get
            {
                return this.m_ConsignmentFormat;
            }
            set
            {
                this.m_ConsignmentFormat = value;
            }
        }

        public string ContinueBuy
        {
            get
            {
                return this.m_ContinueBuy;
            }
            set
            {
                this.m_ContinueBuy = value;
            }
        }

        public string Country
        {
            get
            {
                return this.m_Country;
            }
            set
            {
                this.m_Country = value;
            }
        }

        public string EmailOfDeliver
        {
            get
            {
                return this.m_EmailOfDeliver;
            }
            set
            {
                this.m_EmailOfDeliver = value;
            }
        }

        public string EmailOfInvoice
        {
            get
            {
                return this.m_EmailOfInvoice;
            }
            set
            {
                this.m_EmailOfInvoice = value;
            }
        }

        public string EmailOfOrderConfirm
        {
            get
            {
                return this.m_EmailOfOrderConfirm;
            }
            set
            {
                this.m_EmailOfOrderConfirm = value;
            }
        }

        public string EmailOfReceiptMoney
        {
            get
            {
                return this.m_EmailOfReceiptMoney;
            }
            set
            {
                this.m_EmailOfReceiptMoney = value;
            }
        }

        public string EmailOfRefund
        {
            get
            {
                return this.m_EmailOfRefund;
            }
            set
            {
                this.m_EmailOfRefund = value;
            }
        }

        public string EmailOfSendCard
        {
            get
            {
                return this.m_EmailOfSendCard;
            }
            set
            {
                this.m_EmailOfSendCard = value;
            }
        }

        public bool EnableCoupon
        {
            get
            {
                return this.m_EnableCoupon;
            }
            set
            {
                this.m_EnableCoupon = value;
            }
        }

        public bool EnableGuestBuy
        {
            get
            {
                return this.m_EnableGuestBuy;
            }
            set
            {
                this.m_EnableGuestBuy = value;
            }
        }

        public bool EnablePartPay
        {
            get
            {
                return this.m_EnablePartPay;
            }
            set
            {
                this.m_EnablePartPay = value;
            }
        }

        public int GwcThumbsHeight
        {
            get
            {
                return this.m_GwcThumbsHeight;
            }
            set
            {
                this.m_GwcThumbsHeight = value;
            }
        }

        public int GwcThumbsWidth
        {
            get
            {
                return this.m_GwcThumbsWidth;
            }
            set
            {
                this.m_GwcThumbsWidth = value;
            }
        }

        public bool IsGwcShowProducdtThumb
        {
            get
            {
                return this.m_IsGwcShowProducdtThumb;
            }
            set
            {
                this.m_IsGwcShowProducdtThumb = value;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
        }

        public bool IsOrderProductListShowThumb
        {
            get
            {
                return this.m_IsOrderProductListShowThumb;
            }
            set
            {
                this.m_IsOrderProductListShowThumb = value;
            }
        }

        public bool IsPaymentShowProducdtThumb
        {
            get
            {
                return this.m_IsPaymentShowProducdtThumb;
            }
            set
            {
                this.m_IsPaymentShowProducdtThumb = value;
            }
        }

        public bool IsPayPassword
        {
            get
            {
                return this.m_IsPayPassword;
            }
            set
            {
                this.m_IsPayPassword = value;
            }
        }

        public bool IsPreviewShowProducdtThumb
        {
            get
            {
                return this.m_IsPreviewShowProducdtThumb;
            }
            set
            {
                this.m_IsPreviewShowProducdtThumb = value;
            }
        }

        public bool IsProductListThumb
        {
            get
            {
                return this.m_IsProductListThumb;
            }
            set
            {
                this.m_IsProductListThumb = value;
            }
        }

        public bool IsSetFunctionary
        {
            get
            {
                return this.m_IsSetFunctionary;
            }
            set
            {
                this.m_IsSetFunctionary = value;
            }
        }

        public bool IsShowGwcMarkPrice
        {
            get
            {
                return this.m_IsShowGwcMarkPrice;
            }
            set
            {
                this.m_IsShowGwcMarkPrice = value;
            }
        }

        public bool IsShowGwcProductType
        {
            get
            {
                return this.m_IsShowGwcProductType;
            }
            set
            {
                this.m_IsShowGwcProductType = value;
            }
        }

        public bool IsShowGwcSaleType
        {
            get
            {
                return this.m_IsShowGwcSaleType;
            }
            set
            {
                this.m_IsShowGwcSaleType = value;
            }
        }

        public bool IsShowPaymentMarkPrice
        {
            get
            {
                return this.m_IsShowPaymentMarkPrice;
            }
            set
            {
                this.m_IsShowPaymentMarkPrice = value;
            }
        }

        public bool IsShowPaymentProductType
        {
            get
            {
                return this.m_IsShowPaymentProductType;
            }
            set
            {
                this.m_IsShowPaymentProductType = value;
            }
        }

        public bool IsShowPaymentSaleType
        {
            get
            {
                return this.m_IsShowPaymentSaleType;
            }
            set
            {
                this.m_IsShowPaymentSaleType = value;
            }
        }

        public bool IsShowPreviewMarkPrice
        {
            get
            {
                return this.m_IsShowPreviewMarkPrice;
            }
            set
            {
                this.m_IsShowPreviewMarkPrice = value;
            }
        }

        public bool IsShowPreviewProductType
        {
            get
            {
                return this.m_IsShowPreviewProductType;
            }
            set
            {
                this.m_IsShowPreviewProductType = value;
            }
        }

        public bool IsShowPreviewSaleType
        {
            get
            {
                return this.m_IsShowPreviewSaleType;
            }
            set
            {
                this.m_IsShowPreviewSaleType = value;
            }
        }

        public bool IsThumb
        {
            get
            {
                return this.m_IsThumb;
            }
            set
            {
                this.m_IsThumb = value;
            }
        }

        public bool IsWatermark
        {
            get
            {
                return this.m_IsWatermark;
            }
            set
            {
                this.m_IsWatermark = value;
            }
        }

        public decimal MoneyPresentPoint
        {
            get
            {
                if (this.m_MoneyPresentPoint <= 0M)
                {
                    return 1M;
                }
                return this.m_MoneyPresentPoint;
            }
            set
            {
                this.m_MoneyPresentPoint = value;
            }
        }

        public string OrderFormat
        {
            get
            {
                return this.m_OrderFormat;
            }
            set
            {
                this.m_OrderFormat = value;
            }
        }

        public int OrderProductListThumbsHeight
        {
            get
            {
                return this.m_OrderProductListThumbsHeight;
            }
            set
            {
                this.m_OrderProductListThumbsHeight = value;
            }
        }

        public int OrderProductListThumbsWidth
        {
            get
            {
                return this.m_OrderProductListThumbsWidth;
            }
            set
            {
                this.m_OrderProductListThumbsWidth = value;
            }
        }

        public int OrderProductNumber
        {
            get
            {
                return this.m_OrderProductNumber;
            }
            set
            {
                this.m_OrderProductNumber = value;
            }
        }

        public int PartPayAge
        {
            get
            {
                return this.m_PartPayAge;
            }
            set
            {
                this.m_PartPayAge = value;
            }
        }

        public int PaymentThumbsHeight
        {
            get
            {
                return this.m_PaymentThumbsHeight;
            }
            set
            {
                this.m_PaymentThumbsHeight = value;
            }
        }

        public int PaymentThumbsWidth
        {
            get
            {
                return this.m_PaymentThumbsWidth;
            }
            set
            {
                this.m_PaymentThumbsWidth = value;
            }
        }

        public string PostCode
        {
            get
            {
                return this.m_PostCode;
            }
            set
            {
                this.m_PostCode = value;
            }
        }

        public string PrefixOrderFormNum
        {
            get
            {
                return this.m_PrefixOrderFormNum;
            }
            set
            {
                this.m_PrefixOrderFormNum = value;
            }
        }

        public string PrefixPaymentNum
        {
            get
            {
                return this.m_PrefixPaymentNum;
            }
            set
            {
                this.m_PrefixPaymentNum = value;
            }
        }

        public int PreviewThumbsHeight
        {
            get
            {
                return this.m_PreviewThumbsHeight;
            }
            set
            {
                this.m_PreviewThumbsHeight = value;
            }
        }

        public int PreviewThumbsWidth
        {
            get
            {
                return this.m_PreviewThumbsWidth;
            }
            set
            {
                this.m_PreviewThumbsWidth = value;
            }
        }

        public int ProductListThumbsHeight
        {
            get
            {
                return this.m_ProductListThumbsHeight;
            }
            set
            {
                this.m_ProductListThumbsHeight = value;
            }
        }

        public int ProductListThumbsWidth
        {
            get
            {
                return this.m_ProductListThumbsWidth;
            }
            set
            {
                this.m_ProductListThumbsWidth = value;
            }
        }

        public string Province
        {
            get
            {
                return this.m_Province;
            }
            set
            {
                this.m_Province = value;
            }
        }

        public float TaxRate
        {
            get
            {
                return this.m_TaxRate;
            }
            set
            {
                this.m_TaxRate = value;
            }
        }

        public int TaxRateType
        {
            get
            {
                return this.m_TaxRateType;
            }
            set
            {
                this.m_TaxRateType = value;
            }
        }
    }
}

