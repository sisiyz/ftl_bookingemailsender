using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEmailService.Common
{
    public class EmailOrderDeatils
    {
        public string acct_ID { get; set; }
        public string passenger { get; set; }
        public string company_name { get; set; }
        public string vendor_name { get; set; }
        public string company_address_1 { get; set; }
        public string company_address_2 { get; set; }
        public string acct_address_3 { get; set; }

        public string confirmation_no { get; set; }
        public string company_phone { get; set; }
        public string phone { get; set; }
        public DateTime trip_date_time { get; set; }
        public string agent_id { get; set; }
        public string car_type { get; set; }
        public string no_passenger { get; set; }
        public string reservation_type { get; set; }

        public string pu_address { get; set; }
        public string dest_address { get; set; }
        public string stop_address_1 { get; set; }
        public string stop_address_2 { get; set; }
        public string stop_address_3 { get; set; }
        public string stop_address_4 { get; set; }
        public string stop_address_5 { get; set; }
        public string stop_address_6 { get; set; }
        public string stop_address_7 { get; set; }
        public string stop_address_8 { get; set; }
        public string stop_address_9 { get; set; }

        public string comment { get; set; }
        public string pu_meet_instructions { get; set; }
        public string dest_meet_instructions { get; set; }
    }
}
