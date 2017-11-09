using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Threading;
using BookingEmailService.BL;

namespace BookingEmailSender
{
    public partial class BookingEmailService : ServiceBase
    {
        //private static readonly ILog info_log = LogManager.GetLogger("info-log");
        //private static readonly ILog error_log = LogManager.GetLogger("error-log");
        private static readonly ILog log = LogManager.GetLogger("log");
        private int interval = Convert.ToInt32(ConfigurationManager.AppSettings["BookingEmailServiceInterval"]) == 0 ? 5000 : Convert.ToInt32(ConfigurationManager.AppSettings["BookingEmailServiceInterval"]);
        private System.Threading.Timer workTimer;
        private static bool _processing = false;
        private static readonly int _taskTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["TaskTimeout"]);

        public BookingEmailService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
#if DEBUG
                Debugger.Launch();
#endif

                log.Info("OnStart");
                workTimer = new System.Threading.Timer(new TimerCallback(ProcessMessages), null, 5000, interval); //wait 5 sec before starting
            }
            catch (Exception ex)
            {
                log.Error("Exception in OnStart. Exception : '{0}'", ex);
            }
        }

        protected override void OnStop()
        {
            workTimer.Dispose();
        }

        private void ProcessMessages(object state)
        {
            bool result = false;
            try
            {
                if (!_processing) //dont execute until processing flag is false
                {
                    _processing = true;

                    ProcessMonitor pMonitor = new ProcessMonitor();
                    pMonitor.Process();

                    _processing = false;
                }
            }
            catch
            {
            }
        }
    }
}
