namespace EasyOne.Web.HttpModule
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidSessionException : Exception
    {
        public InvalidSessionException()
        {
        }

        public InvalidSessionException(string message) : base(message)
        {
        }

        protected InvalidSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidSessionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

