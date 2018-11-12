namespace EasyOne.IDal.CommonModel
{
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public interface IModel
    {
        bool Add(ModelInfo modelInfo);
        void AddFieldToTable(FieldInfo fieldInfo, string tableName);
        bool AddModelForNodes(int nodeId, int modelId, string templateId);
        bool AddNodesModelTemplateRelationShip(NodesModelTemplateRelationShipInfo info);
        bool AddTemplateForNodes(int nodeId, int templateId, bool isDefault);
        bool Delete(int modelId);
        bool DeleteNodeModel(int nodeId);
        bool DeleteNodesModelTemplateRelationShip(int nodeId);
        bool DeleteNodeTemplateId(int nodeId);
        void DeleteTableField(string fieldName, string tableName);
        bool Disable(int id, bool disabled);
        bool EnableCharge(int id, bool charge);
        bool EnableSignIn(int id, bool signIn);
        bool ExistsNodesModelTemplateRelationShip(NodesModelTemplateRelationShipInfo info);
        bool ExistsNodesModelTemplateRelationShip(int nodeId);
        DataTable GetContentModelListByNodeId(int nodeId, bool enable);
        ArrayList GetLookupField(string tableName, string fieldName, int modelId);
        int GetMaxId();
        ModelInfo GetModelInfoById(int id);
        IList<ModelInfo> GetModelList(ModelType modelTyp, ModelShowType showType);
        DataTable GetModelListByNodeId(int nodeId, bool enable);
        int GetNodeModeId(int nodeId);
        IList<NodesModelTemplateRelationShipInfo> GetNodesModelTemplateList(int nodeId);
        NodesModelTemplateRelationShipInfo GetNodesModelTemplateRelationShip(int nodeId, int modelId);
        DataTable GetShopModelListByNodeId(int nodeId, bool enable);
        IList<int> GetTemplateIdList(int nodeId);
        string GetXmlFieldByModelId(int id);
        bool ModelIdExists(int nodeId, int modelId);
        bool ModelNameExists(string modelName);
        bool TableNameExists(string tableName);
        bool TemplateIdExists(int nodeId, int templateId);
        bool Update(ModelInfo modelInfo);
        void UpdateField(int modelId, string fieldList);
        void UpdateTableField(FieldInfo fieldInfo, string tableName);
    }
}

