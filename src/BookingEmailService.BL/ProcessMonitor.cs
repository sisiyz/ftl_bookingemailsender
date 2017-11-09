using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingEmailService.DAL;
using BookingEmailService.Common;
using log4net;

namespace BookingEmailService.BL
{
    public class ProcessMonitor
    {
        private static readonly ILog log = LogManager.GetLogger("log");
        DBFunctions objDBFunction = new DBFunctions();

        public void Process()
        {
            try
            {
                Identifier();
            }
            catch (Exception ex)
            {
            }
        }

        private void Identifier()
        {
            DBFunctions objDBFunctions = new DBFunctions();
            OrderDeatils objOrderDeatils = new OrderDeatils();
            Email objEmail = new Email();

            string builder = "";

            List<BookingCentraliseEmailMonitor> objBookingCentraliseEmailMonitors = new List<BookingCentraliseEmailMonitor>();

            try
            {
                log.Info("EmailSender Indentifier Started on: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                //ParallelOptions parallelOptions = new ParallelOptions();
                //parallelOptions.MaxDegreeOfParallelism = ServiceConfig.MaxDegreeOfParallelism == 0 ? (System.Environment.ProcessorCount / 2) : ServiceConfig.MaxDegreeOfParallelism;

                //Get Confs,Format,account send flag etc
                objBookingCentraliseEmailMonitors = objDBFunction.GetConfirmationNoFormats();

                foreach (BookingCentraliseEmailMonitor objBookingCentraliseEmailMonitor in objBookingCentraliseEmailMonitors)
                {
                    //Check email send flag
                    if (objBookingCentraliseEmailMonitor.acct_send_flag == "N" || (objBookingCentraliseEmailMonitor.vip_send_flag == "N" && objBookingCentraliseEmailMonitor.acct_admin_send_flag == "N"))
                    {
                        //update acct_check_ts to checked, but not send email
                        objDBFunction.Updateistransfer(objBookingCentraliseEmailMonitor.confirmation_no, false, objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id);
                    }
                    else
                    {
                        //update acct_check_ts to checked, send email, and status to processing
                        objDBFunction.Updateistransfer(objBookingCentraliseEmailMonitor.confirmation_no, true, objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id);

                        //GetEmailDetails
                        objOrderDeatils = objDBFunction.GetEmailOrderDetails(objBookingCentraliseEmailMonitor, objBookingCentraliseEmailMonitor.email_type);
                        objOrderDeatils.email_type = objBookingCentraliseEmailMonitor.email_type;
                        objOrderDeatils.company = objBookingCentraliseEmailMonitor.company;

                        //Transfer(Upload) Email
                        builder = objEmail.GetReportHtml(objOrderDeatils);
                        objEmail.TransferEmail(objOrderDeatils, builder,objBookingCentraliseEmailMonitor.email_format, objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id);

                        //Update status to transferred
                        objDBFunction.UpdateProcessingStatus(objBookingCentraliseEmailMonitor.confirmation_no, objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id, "Transferred");
                    }


                    //Generate HTML or PDF


                }
            }
            catch(Exception ex)
            {
                //Update status to transferred
                //objDBFunction.UpdateProcessingStatus(objBookingCentraliseEmailMonitor.confirmation_no, objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id, "Application Error");
            }
        }
    }
}
