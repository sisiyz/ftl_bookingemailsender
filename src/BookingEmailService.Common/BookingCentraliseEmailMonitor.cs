using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEmailService.Common
{
    public class BookingCentraliseEmailMonitor
    {
        public int booking_centralise_email_monitor_id =0;
        public string confirmation_no ="";
        public string company ="";
        public string email_type ="New";
        public string email_format ="HTML";
        public string acct_send_flag ="N";
        public string vip_send_flag ="N";
        public string acct_admin_send_flag ="N";

        public int Booking_Centralise_Email_Monitor_Id
        {
            get { return booking_centralise_email_monitor_id; }
            set { booking_centralise_email_monitor_id = value; }
        }
        public string Confirmation_No
        {
            get { return confirmation_no; }
            set { confirmation_no = value; }
        }
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        public string Email_Type
        {
            get { return email_type; }
            set { email_type = value; }
        }
        public string Email_Format
        {
            get { return email_format; }
            set { email_format = value; }
        }
        public string Acct_Send_Flag
        {
            get { return acct_send_flag; }
            set { acct_send_flag = value; }
        }
        public string Vip_Send_Flag
        {
            get { return vip_send_flag; }
            set { vip_send_flag = value; }
        }
        public string Acct_Admin_Send_Flag
        {
            get { return acct_admin_send_flag; }
            set { acct_admin_send_flag = value; }
        }
    }
}
