namespace EasyOne.SqlServerDal.Contents
{
    using EasyOne.Common;
    using EasyOne.Enumerations;
    using EasyOne.Model.CommonModel;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;
    using System.Text;

    public sealed class Query
    {
        private Query()
        {
        }

        public static string GetAddColumnToTableSql(FieldInfo fieldInfo, string tableName)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append("ALTER TABLE [");
            sqlText.Append(tableName);
            sqlText.Append("] ADD [");
            sqlText.Append(fieldInfo.FieldName);
            sqlText.Append("] ");
            GetFieldType(fieldInfo.FieldType, sqlText);
            return sqlText.ToString();
        }

        public static string GetAlterColumnToTableSql(FieldInfo fieldInfo, string tableName)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append(" ALTER TABLE [");
            sqlText.Append(tableName);
            sqlText.Append("] ALTER COLUMN [");
            sqlText.Append(fieldInfo.FieldName);
            sqlText.Append("]");
            GetFieldType(fieldInfo.FieldType, sqlText);
            return sqlText.ToString();
        }

        public static DataRow[] GetDataRows(DataTable contentData, string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return contentData.Select(filter);
            }
            return contentData.Select();
        }

        public static string GetDeleteColumnFromTableSql(string fieldName, string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ALTER TABLE [");
            builder.Append(tableName);
            builder.Append("] DROP COLUMN [");
            builder.Append(fieldName);
            builder.Append("] ");
            return builder.ToString();
        }

        public static DbType GetFieldParameType(FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.TextType:
                case FieldType.MultipleTextType:
                case FieldType.MultipleHtmlTextType:
                case FieldType.ListBoxType:
                case FieldType.LookType:
                case FieldType.LinkType:
                case FieldType.CountType:
                case FieldType.PictureType:
                case FieldType.FileType:
                case FieldType.ColorType:
                case FieldType.TemplateType:
                case FieldType.AuthorType:
                case FieldType.SourceType:
                case FieldType.KeywordType:
                case FieldType.OperatingType:
                case FieldType.DownServerType:
                case FieldType.Producer:
                case FieldType.Trademark:
                case FieldType.ContentType:
                case FieldType.TitleType:
                case FieldType.MultiplePhotoType:
                    return DbType.String;

                case FieldType.NumberType:
                    return DbType.Double;

                case FieldType.MoneyType:
                    return DbType.Currency;

                case FieldType.DateTimeType:
                    return DbType.DateTime;

                case FieldType.BoolType:
                    return DbType.Boolean;
            }
            return DbType.Int32;
        }

        private static void GetFieldType(FieldType fieldType, StringBuilder sqlText)
        {
            switch (fieldType)
            {
                case FieldType.TextType:
                case FieldType.ListBoxType:
                case FieldType.LookType:
                case FieldType.CountType:
                case FieldType.ColorType:
                case FieldType.TemplateType:
                case FieldType.AuthorType:
                case FieldType.SourceType:
                case FieldType.KeywordType:
                case FieldType.OperatingType:
                case FieldType.Producer:
                case FieldType.Trademark:
                case FieldType.TitleType:
                    sqlText.Append("[nvarchar] (255)");
                    return;

                case FieldType.MultipleTextType:
                case FieldType.MultipleHtmlTextType:
                case FieldType.LinkType:
                case FieldType.PictureType:
                case FieldType.FileType:
                case FieldType.DownServerType:
                case FieldType.ContentType:
                case FieldType.MultiplePhotoType:
                    sqlText.Append("[ntext]");
                    return;

                case FieldType.NumberType:
                    sqlText.Append(" [float] ");
                    return;

                case FieldType.MoneyType:
                    sqlText.Append(" [money] ");
                    return;

                case FieldType.DateTimeType:
                    sqlText.Append("[datetime]");
                    return;

                case FieldType.BoolType:
                    sqlText.Append("[bit]");
                    return;
            }
            sqlText.Append("[Int]");
        }

        public static object GetFieldValue(FieldType fieldType, object fieldValue)
        {
            switch (fieldType)
            {
                case FieldType.NumberType:
                case FieldType.MoneyType:
                    if (string.IsNullOrEmpty(fieldValue.ToString()))
                    {
                        fieldValue = DBNull.Value;
                    }
                    return fieldValue;

                case FieldType.DateTimeType:
                    if (!string.IsNullOrEmpty(fieldValue.ToString()))
                    {
                        fieldValue = DataConverter.CDate(fieldValue.ToString());
                        return fieldValue;
                    }
                    fieldValue = DBNull.Value;
                    return fieldValue;

                case FieldType.LookType:
                case FieldType.LinkType:
                    return fieldValue;

                case FieldType.BoolType:
                    fieldValue = DataConverter.CBoolean(fieldValue.ToString());
                    return fieldValue;
            }
            return fieldValue;
        }

        public static string GetFiledSting(DataRow[] rows)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in rows)
            {
                builder.Append("[");
                builder.Append(row["FieldName"]);
                builder.Append("] ");
                builder.Append(",");
            }
            if (builder.Length > 1)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        public static string GetInsertTableSql(string tableName, DataTable contentData, string filter)
        {
            DataRow[] dataRows = GetDataRows(contentData, filter);
            string filedSting = GetFiledSting(dataRows);
            string parametersString = GetParametersString(dataRows);
            return GetInsertTableSql(tableName, filedSting, parametersString);
        }

        public static string GetInsertTableSql(string tableName, string strField, string paramters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO ");
            builder.Append(tableName);
            builder.Append("(");
            builder.Append(strField);
            builder.Append(")");
            builder.Append("VALUES");
            builder.Append("(");
            builder.Append(paramters);
            builder.Append(")");
            return builder.ToString();
        }

        public static Parameters GetParameters(DataTable contentData, string filter)
        {
            Parameters parameters = new Parameters();
            foreach (DataRow row in GetDataRows(contentData, filter))
            {
                object fieldValue = row["FieldValue"];
                FieldType fieldType = (FieldType) Enum.Parse(typeof(FieldType), row["FieldType"].ToString());
                parameters.AddInParameter("@" + row["FieldName"], GetFieldParameType(fieldType), GetFieldValue(fieldType, fieldValue));
            }
            return parameters;
        }

        public static string GetParametersString(DataRow[] rows)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in rows)
            {
                builder.Append("@");
                builder.Append(row["FieldName"]);
                builder.Append(",");
            }
            if (builder.Length > 1)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        public static string GetUpdataSql(string tableName, DataTable contentData, string filter, string where)
        {
            string updateFieldList = GetUpdateFieldList(GetDataRows(contentData, filter));
            return GetUpdateSql(tableName, updateFieldList, where);
        }

        public static string GetUpdateFieldList(DataRow[] rows)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in rows)
            {
                builder.Append(" [");
                builder.Append(row["FieldName"]);
                builder.Append("]");
                builder.Append(" = ");
                builder.Append("@");
                builder.Append(row["FieldName"]);
                builder.Append(",");
            }
            if (builder.Length > 1)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        public static string GetUpdateSql(string tableName, string strUpdateField, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE ");
            builder.Append(tableName);
            builder.Append(" SET ");
            builder.Append(strUpdateField);
            builder.Append(" WHERE ");
            builder.Append(where);
            return builder.ToString();
        }
    }
}

