namespace EasyOne.Components
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Web;

    public class WebSystemDiagnostics
    {
        private Process currentProcess = Process.GetCurrentProcess();

        protected static string TimeSwitch(TimeSpan variable)
        {
            string str = "";
            return (((((str + variable.Days.ToString() + " 天 ") + variable.Hours.ToString() + " 时 ") + variable.Minutes.ToString("d2") + "分 ") + variable.Seconds.ToString("d2") + "秒 ") + variable.Milliseconds.ToString("d3") + "毫秒");
        }

        protected static string UnitsSwitch(long variable)
        {
            double num;
            if (variable > 0x40000000L)
            {
                num = ((double) variable) / 1073741824.0;
                return (num.ToString("N2") + "G");
            }
            if (variable > 0x100000L)
            {
                num = ((double) variable) / 1048576.0;
                return (num.ToString("N2") + "M");
            }
            num = ((double) variable) / 1024.0;
            return (num.ToString("N2") + "K");
        }

        public static string Caches
        {
            get
            {
                return HttpRuntime.Cache.Count.ToString();
            }
        }

        public string CpuUseRatio
        {
            get
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.currentProcess.StartTime);
                double num = ((this.currentProcess.TotalProcessorTime.TotalSeconds / span.TotalSeconds) * 100.0) / double.Parse(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
                return (num.ToString("N2") + "%");
            }
        }

        public static string GCTotalMemory
        {
            get
            {
                return UnitsSwitch(GC.GetTotalMemory(false));
            }
        }

        public string HandleCount
        {
            get
            {
                return this.currentProcess.HandleCount.ToString();
            }
        }

        public string MaxVirtualMemory
        {
            get
            {
                return UnitsSwitch(this.currentProcess.PeakVirtualMemorySize64);
            }
        }

        public string Memory
        {
            get
            {
                return UnitsSwitch(this.currentProcess.WorkingSet64);
            }
        }

        public string MixMemory
        {
            get
            {
                return UnitsSwitch(this.currentProcess.PeakWorkingSet64);
            }
        }

        public string NoPaginationMemory
        {
            get
            {
                return UnitsSwitch(this.currentProcess.NonpagedSystemMemorySize64);
            }
        }

        public string PaginationMemory
        {
            get
            {
                return UnitsSwitch(this.currentProcess.PagedMemorySize64);
            }
        }

        public string PrivilegedCpuTime
        {
            get
            {
                return TimeSwitch(this.currentProcess.PrivilegedProcessorTime);
            }
        }

        public string RunThreads
        {
            get
            {
                return this.currentProcess.Threads.Count.ToString();
            }
        }

        public string Runtime
        {
            get
            {
                TimeSpan variable = (TimeSpan) (DateTime.Now - this.currentProcess.StartTime);
                return TimeSwitch(variable);
            }
        }

        public static string ScriptTimeOut
        {
            get
            {
                if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
                {
                    int num = HttpContext.Current.Server.ScriptTimeout / 0x3e8;
                    return (num.ToString() + "秒");
                }
                return "0秒";
            }
        }

        public static string SessionCount
        {
            get
            {
                if ((HttpContext.Current != null) && (HttpContext.Current.Session != null))
                {
                    return HttpContext.Current.Session.Contents.Count.ToString();
                }
                return "0";
            }
        }

        public string StartTime
        {
            get
            {
                DateTime startTime = this.currentProcess.StartTime;
                return (startTime.Year.ToString("d4") + "年 " + startTime.Month.ToString("d2") + "月" + startTime.Day.ToString("d2") + "日 " + startTime.Hour.ToString("d2") + "时" + startTime.Minute.ToString("d2") + "分");
            }
        }

        public static IList<int> ThreadPoolData
        {
            get
            {
                List<int> list = new List<int>();
                int workerThreads = 0;
                int completionPortThreads = 0;
                int num3 = 0;
                int num4 = 0;
                ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
                ThreadPool.GetAvailableThreads(out num3, out num4);
                list.Add(workerThreads);
                list.Add(completionPortThreads);
                list.Add(num3);
                list.Add(num4);
                return list;
            }
        }

        public static string ThreadState
        {
            get
            {
                return Thread.CurrentThread.ThreadState.ToString();
            }
        }

        public string TotalCpuTime
        {
            get
            {
                return TimeSwitch(this.currentProcess.TotalProcessorTime);
            }
        }

        public string UserCpuTime
        {
            get
            {
                return TimeSwitch(this.currentProcess.UserProcessorTime);
            }
        }

        public string VirtualMemory
        {
            get
            {
                return UnitsSwitch(this.currentProcess.VirtualMemorySize64);
            }
        }
    }
}

