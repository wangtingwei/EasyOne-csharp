namespace EasyOne.Components
{
    using System;

    public sealed class PEEvents
    {
        private PEEvents()
        {
        }

        public static void PEException(CustomException ex)
        {
            PEApplication.Instance().ExecutePEExcetion(ex);
        }
    }
}

