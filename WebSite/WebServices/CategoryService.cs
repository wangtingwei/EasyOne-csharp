namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Model.CommonModel;
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web;
    using System.Web.Script.Services;
    using System.Web.Services;
    using EasyOne.CommonModel;

    [ScriptService, WebService(Namespace="http://tempuri.org/"), WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1), ToolboxItem(false)]
    public class CategoryService : WebService
    {
        [WebMethod]
        public string CategoryDir(int nodeId)
        {
            if (nodeId > 0)
            {
                NodeInfo cacheNodeById = Nodes.GetCacheNodeById(nodeId);
                return (cacheNodeById.ParentDir + VirtualPathUtility.AppendTrailingSlash(cacheNodeById.NodeDir));
            }
            return string.Empty;
        }

        [WebMethod]
        public List<FieldInfo> GetFieldList(int modelId)
        {
            if (modelId > 0)
            {
                return (List<FieldInfo>) ModelManager.GetFieldListByModelId(modelId);
            }
            return new List<FieldInfo>();
        }

        [WebMethod]
        public string GetInitial(string nodeName)
        {
            return StringHelper.GetInitial(nodeName);
        }

        [WebMethod]
        public string GetPinyinTitles(string title)
        {
            return ChineseSpell.MakeSpellCode(title, SpellOptions.EnableUnicodeLetter | SpellOptions.TranslateUnknowWordToInterrogation);
        }
    }
}

