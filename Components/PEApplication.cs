namespace EasyOne.Components
{
    using System;
    using System.ComponentModel;

    public class PEApplication : IDisposable
    {
        private EventHandlerList m_Events = new EventHandlerList();
        private static object s_EventUnhandledException = new object();

        public event PEExceptionEventHandler CustomException
        {
            add
            {
                this.m_Events.AddHandler(s_EventUnhandledException, value);
            }
            remove
            {
                this.m_Events.RemoveHandler(s_EventUnhandledException, value);
            }
        }

        private PEApplication()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_Events.Dispose();
            }
        }

        internal void ExecutePEExcetion(EasyOne.Components.CustomException ex)
        {
            PEExceptionEventHandler handler = this.m_Events[s_EventUnhandledException] as PEExceptionEventHandler;
            if (handler != null)
            {
                handler(ex, new EventArgs());
            }
        }

        internal static PEApplication Instance()
        {
            PEApplication pea = new PEApplication();
            (Activator.CreateInstance(Type.GetType("EasyOne.Components.PEExceptionModule")) as IPEModule).Init(pea);
            return pea;
        }
    }
}

