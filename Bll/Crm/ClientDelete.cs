namespace EasyOne.Crm
{
    using EasyOne.Common;
    using EasyOne.IDal.Crm;
    using System;
    using System.Collections.Generic;
    using EasyOne.DalFactory;

    public class ClientDelete
    {
        private static readonly IClientDelete dal = DataAccess.CreateClientDelete();
        private EasyOne.Crm.CheckClientType m_CheckClientType;
        private string m_ClientIdList;
        private int m_Count;
        private string m_DeleteInfo;
        private string m_Exclusion;
        private string m_GroupIdList;

        private bool CheckClient(int clientId)
        {
            if (StringHelper.FoundCharInArr(this.Exclusion, "ExcludingBalance") && (Client.GetClientById(clientId).Balance > 0M))
            {
                return false;
            }
            if (StringHelper.FoundCharInArr(this.Exclusion, "ExcludingOrder") && (dal.GetOrderCount(clientId) > 0))
            {
                return false;
            }
            if (StringHelper.FoundCharInArr(this.Exclusion, "ExcludingBankroll") && (dal.GetBankrollItemCount(clientId) > 0))
            {
                return false;
            }
            if (StringHelper.FoundCharInArr(this.Exclusion, "ExcludingService") && (dal.GetServiceItemCount(clientId) > 0))
            {
                return false;
            }
            if (StringHelper.FoundCharInArr(this.Exclusion, "ExcludingComplain") && (dal.GetComplainItemCount(clientId) > 0))
            {
                return false;
            }
            return true;
        }

        public void Delete()
        {
            this.GetClientIdList();
            if (DataValidator.IsValidId(this.ClientIdList))
            {
                foreach (string str in this.ClientIdList.Split(new char[] { ',' }))
                {
                    int clientId = DataConverter.CLng(str);
                    if ((clientId != 0) && this.CheckClient(clientId))
                    {
                        this.DeleteCorrelativeInfo(clientId);
                    }
                }
            }
        }

        private bool DeleteCorrelativeInfo(int clientId)
        {
            if (Client.Delete(clientId.ToString()))
            {
                this.m_Count++;
            }
            else
            {
                return false;
            }
            if (StringHelper.FoundCharInArr(this.DeleteInfo, "DelOrder"))
            {
                dal.DelOrder(clientId);
            }
            if (StringHelper.FoundCharInArr(this.DeleteInfo, "DelBankroll"))
            {
                dal.DelBankrollItem(clientId);
            }
            else
            {
                dal.UpdateBankrollItem(clientId);
            }
            if (StringHelper.FoundCharInArr(this.DeleteInfo, "DelService"))
            {
                dal.DelServiceItem(clientId);
            }
            if (StringHelper.FoundCharInArr(this.DeleteInfo, "DelComplain"))
            {
                dal.DelComplainItem(clientId);
            }
            if (StringHelper.FoundCharInArr(this.DeleteInfo, "DelCompany"))
            {
                dal.DelCompany(clientId);
            }
            else
            {
                dal.UpdateCompany(clientId);
            }
            if (StringHelper.FoundCharInArr(this.DeleteInfo, "DelContacter"))
            {
                dal.DelContacter(clientId);
            }
            else
            {
                dal.UpdateContacter(clientId);
            }
            if (StringHelper.FoundCharInArr(this.DeleteInfo, "DelUser"))
            {
                foreach (KeyValuePair<int, string> pair in dal.GetUserIdByClientId(clientId))
                {
                    string str = pair.Value;
                    if (!string.IsNullOrEmpty(str))
                    {
                        IList<int> orderId = dal.GetOrderId(str);
                        if ((orderId.Count > 0) && dal.DelOrder(str))
                        {
                            foreach (int num in orderId)
                            {
                                dal.DelOrderItem(num);
                            }
                        }
                        dal.DelPaymentLog(str);
                        dal.DelBankrollItem(str);
                        dal.DelPointLog(str);
                        dal.DelValidLog(str);
                        if (dal.DelUser(clientId))
                        {
                            dal.DelContacter(str);
                        }
                    }
                }
            }
            else
            {
                dal.UpdateUser(clientId);
            }
            return true;
        }

        private void GetClientIdList()
        {
            switch (this.CheckClientType)
            {
                case EasyOne.Crm.CheckClientType.AppointId:
                    break;

                case EasyOne.Crm.CheckClientType.AppointGroup:
                    this.ClientIdList = Client.GetClientIdByGroup(this.GroupIdList);
                    break;

                case EasyOne.Crm.CheckClientType.All:
                    this.ClientIdList = Client.GetAllClientId();
                    return;

                default:
                    return;
            }
        }

        public EasyOne.Crm.CheckClientType CheckClientType
        {
            get
            {
                return this.m_CheckClientType;
            }
            set
            {
                this.m_CheckClientType = value;
            }
        }

        public string ClientIdList
        {
            get
            {
                return this.m_ClientIdList;
            }
            set
            {
                this.m_ClientIdList = value;
            }
        }

        public int Count
        {
            get
            {
                return this.m_Count;
            }
        }

        public string DeleteInfo
        {
            get
            {
                return this.m_DeleteInfo;
            }
            set
            {
                this.m_DeleteInfo = value;
            }
        }

        public string Exclusion
        {
            get
            {
                return this.m_Exclusion;
            }
            set
            {
                this.m_Exclusion = value;
            }
        }

        public string GroupIdList
        {
            get
            {
                return this.m_GroupIdList;
            }
            set
            {
                this.m_GroupIdList = value;
            }
        }
    }
}

