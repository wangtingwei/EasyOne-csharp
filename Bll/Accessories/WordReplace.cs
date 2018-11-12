namespace EasyOne.Accessories
{
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.IDal.Accessories;
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using EasyOne.DalFactory;

    public sealed class WordReplace
    {
        private static readonly IWordReplace dal = DataAccess.CreateWordReplace();
        private static string s_LinkType = "";
        private static int s_ReplaceTimes = 0;
        private static string s_SourceWord = "";

        private WordReplace()
        {
        }

        public static bool Add(WordReplaceInfo wordReplaceInfo)
        {
            return dal.Add(wordReplaceInfo);
        }

        public static bool Delete(string id)
        {
            return (DataValidator.IsValidId(id) && dal.Delete(id));
        }

        public static bool Disabled(string id)
        {
            return (DataValidator.IsValidId(id) && dal.Disabled(id));
        }

        public static bool Enabled(string id)
        {
            return (DataValidator.IsValidId(id) && dal.Enabled(id));
        }

        public static bool Exists(string source, int type)
        {
            return dal.Exists(source, type);
        }

        public static int GetCountNumber(string keyword, int listType)
        {
            return dal.GetCountNumber();
        }

        public static WordReplaceInfo GetInfoById(int id)
        {
            return dal.GetInfoById(id);
        }

        public static IList<WordReplaceInfo> GetInsideList(int startRowIndexId, int maxNumberRows, string keyword, int listType)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Replace("'", "''").Trim();
            }
            return dal.GetInsideLink(startRowIndexId, maxNumberRows, keyword, listType);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static IList<WordReplaceInfo> GetWordFilterList()
        {
            return dal.GetWordFilterList();
        }

        public static IList<WordReplaceInfo> GetWordFilterList(int startRowIndexId, int maxNumberRows, string keyword, int listType)
        {
            return dal.GetWordFilterList(startRowIndexId, maxNumberRows, DataSecurity.FilterBadChar(keyword), listType);
        }

        public static string ReplaceInsideLink(string inputText)
        {
            inputText = inputText.Replace("&lt;", "<");
            inputText = inputText.Replace("&gt;", ">");
            IList<WordReplaceInfo> list = SiteCache.Get("CK_Accessories_ReplaceInsideLinkList") as List<WordReplaceInfo>;
            string input = inputText;
            if (list == null)
            {
                list = dal.GetInsideLink(0, 0, "", 2);
                SiteCache.Insert("CK_Accessories_ReplaceInsideLinkList", list);
            }
            foreach (WordReplaceInfo info in list)
            {
                int replaceTimes = 0;
                s_SourceWord = info.SourceWord;
                s_ReplaceTimes = info.ReplaceTimes;
                if (info.OpenType)
                {
                    s_LinkType = "<a class=\"insidelink\" href=\"" + info.TargetWord + "\">" + info.SourceWord + "</a>";
                }
                else
                {
                    s_LinkType = "<a class=\"insidelink\" href=\"" + info.TargetWord + "\" target=\"_blank\">" + info.SourceWord + "</a>";
                }
                if (input.IndexOf("</a>", StringComparison.Ordinal) > 0)
                {
                    Regex regex = new Regex(@"((^|</a>)[\s\S]*?<a|</a>[\s\S]*$)", RegexOptions.IgnoreCase);
                    foreach (Match match in regex.Matches(input))
                    {
                        Regex regex2 = new Regex("(?<!<[^<>]*)" + s_SourceWord + "(?![^<>]*>)", RegexOptions.IgnoreCase);
                        int count = regex2.Matches(match.Value).Count;
                        if (count <= (info.ReplaceTimes - replaceTimes))
                        {
                            input = input.Replace(match.Value, regex2.Replace(match.Value, s_LinkType));
                            replaceTimes += count;
                        }
                        else
                        {
                            input = input.Replace(match.Value, regex2.Replace(match.Value, s_LinkType, (int) (info.ReplaceTimes - replaceTimes)));
                            replaceTimes = info.ReplaceTimes;
                        }
                    }
                    continue;
                }
                Regex regex3 = new Regex("(?<!<[^<>]*)" + s_SourceWord + "(?![^<>]*>)", RegexOptions.IgnoreCase);
                if (regex3.Matches(input).Count <= (info.ReplaceTimes - replaceTimes))
                {
                    input = input.Replace(input, regex3.Replace(input, s_LinkType));
                }
                else
                {
                    input = input.Replace(input, regex3.Replace(input, s_LinkType, (int) (info.ReplaceTimes - replaceTimes)));
                    replaceTimes = info.ReplaceTimes;
                }
            }
            return input.Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public static string ReplaceText(string inputText)
        {
            IList<WordReplaceInfo> wordFilterList = SiteCache.Get("CK_Accessories_WordReplaceInfoList") as List<WordReplaceInfo>;
            StringBuilder builder = new StringBuilder(inputText);
            if (wordFilterList == null)
            {
                wordFilterList = GetWordFilterList();
                SiteCache.Insert("CK_Accessories_WordReplaceInfoList", wordFilterList);
            }
            foreach (WordReplaceInfo info in wordFilterList)
            {
                builder.Replace(info.SourceWord, info.TargetWord);
            }
            return builder.ToString();
        }

        public static bool Update(WordReplaceInfo wordReplaceInfo)
        {
            return dal.Update(wordReplaceInfo);
        }
    }
}

