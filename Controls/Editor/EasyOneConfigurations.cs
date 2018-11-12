namespace EasyOne.Controls.Editor
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;

    [Serializable]
    public class EasyOneConfigurations : ISerializable
    {
        private Hashtable _Configs;

        internal EasyOneConfigurations()
        {
            this._Configs = new Hashtable();
        }

        protected EasyOneConfigurations(SerializationInfo info, StreamingContext context)
        {
            this._Configs = (Hashtable) info.GetValue("ConfigTable", typeof(Hashtable));
        }

        private static string EncodeConfig(string valueToEncode)
        {
            return valueToEncode.Replace("&", "%26").Replace("=", "%3D").Replace("\"", "%22");
        }

        internal string GetHiddenFieldString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (DictionaryEntry entry in this._Configs)
            {
                if (builder.Length > 0)
                {
                    builder.Append("&amp;");
                }
                builder.AppendFormat("{0}={1}", EncodeConfig(entry.Key.ToString()), EncodeConfig(entry.Value.ToString()));
            }
            return builder.ToString();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ConfigTable", this._Configs);
        }

        public string this[string configurationName]
        {
            get
            {
                if (this._Configs.ContainsKey(configurationName))
                {
                    return (string) this._Configs[configurationName];
                }
                return null;
            }
            set
            {
                this._Configs[configurationName] = value;
            }
        }
    }
}

