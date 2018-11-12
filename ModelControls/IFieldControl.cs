namespace EasyOne.ModelControls
{
    using EasyOne.Enumerations;
    using System;
    using System.Collections.ObjectModel;

    public interface IFieldControl
    {
        void InitFieldControl();

        FieldType ControlType { get; set; }

        string Description { get; set; }

        bool EnableNull { get; set; }

        string FieldAlias { get; set; }

        int FieldLevel { get; set; }

        string FieldName { get; set; }

        string FieldValue { get; set; }

        bool IsAdminManage { get; set; }

        Collection<string> Settings { get; set; }

        string Tips { get; set; }
    }
}

