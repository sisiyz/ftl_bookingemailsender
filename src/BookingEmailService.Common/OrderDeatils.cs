using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingEmailService.Common
{
    public class OrderDeatils
    {
        public string acct_ID = "";
        public string passenger = "";
        public string company_name = "";
        public string company = "";
        public string vendor_name = "";
        public string company_address_1 = "";
        public string company_address_2 = "";
        public string acct_address_3 = "";

        public string confirmation_no = "";
        public string company_phone = "";
        public string phone = "";
        public DateTime trip_date_time;
        public string agent_id = "";
        public string car_type = "";
        public string no_passenger = "";
        public string reservation_type = "";

        public string pu_address = "";
        public string dest_address = "";
        public string stop_address_1 = "";
        public string stop_address_2 = "";
        public string stop_address_3 = "";
        public string stop_address_4 = "";
        public string stop_address_5 = "";
        public string stop_address_6 = "";
        public string stop_address_7 = "";
        public string stop_address_8 = "";
        public string stop_address_9 = "";

        public string comment = "";
        public string pu_meet_instructions = "";
        public string dest_meet_instructions = "";
        public string email_type = "";


        public string Acct_ID
        {
            get { return acct_ID; }
            set { acct_ID = value; }
        }

        public string Passenger
        {
            get { return passenger; }
            set { passenger = value; }
        }

        public string Company_Name
        {
            get { return company_name; }
            set { company_name = value; }
        }

        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        public string Vendor_Name
        {
            get { return vendor_name; }
            set { vendor_name = value; }
        }

        public string Company_Address_1
        {
            get { return company_address_1; }
            set { company_address_1 = value; }
        }

        public string Company_Address_2
        {
            get { return company_address_2; }
            set { company_address_2 = value; }
        }

        public string Acct_Address_3
        {
            get { return acct_address_3; }
            set { acct_address_3 = value; }
        }

        public string Confirmation_No
        {
            get { return confirmation_no; }
            set { confirmation_no = value; }
        }

        public string Company_Phone
        {
            get { return company_phone; }
            set { company_phone = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public DateTime Trip_Date_Time
        {
            get { return trip_date_time; }
            set { trip_date_time = value; }
        }

        public string Agent_Id
        {
            get { return agent_id; }
            set { agent_id = value; }
        }

        public string Car_Type
        {
            get { return car_type; }
            set { car_type = value; }
        }

        public string No_Passenger
        {
            get { return no_passenger; }
            set { no_passenger = value; }
        }

        public string Reservation_Type
        {
            get { return reservation_type; }
            set { reservation_type = value; }
        }

        public string Pu_Address
        {
            get { return pu_address; }
            set { pu_address = value; }
        }

        public string Dest_Address
        {
            get { return dest_address; }
            set { dest_address = value; }
        }
        public string Stop_Address_1
        {
            get { return stop_address_1; }
            set { stop_address_1 = value; }
        }
        public string STOP_ADDRESS_2
        {
            get { return stop_address_2; }
            set { stop_address_2 = value; }
        }
        public string Stop_Address_3
        {
            get { return stop_address_3; }
            set { stop_address_3 = value; }
        }
        public string Stop_Address_4
        {
            get { return stop_address_4; }
            set { stop_address_4 = value; }
        }
        public string Stop_Address_5
        {
            get { return stop_address_5; }
            set { stop_address_5 = value; }
        }
        public string Stop_Address_6
        {
            get { return stop_address_6; }
            set { stop_address_6 = value; }
        }
        public string Stop_Address_7
        {
            get { return stop_address_7; }
            set { stop_address_7 = value; }
        }
        public string Stop_Address_8
        {
            get { return stop_address_8; }
            set { stop_address_8 = value; }
        }
        public string Stop_Address_9
        {
            get { return stop_address_9; }
            set { stop_address_9 = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public string Pu_Meet_Instructions
        {
            get { return pu_meet_instructions; }
            set { pu_meet_instructions = value; }
        }
        public string Dest_Meet_Instructions
        {
            get { return dest_meet_instructions; }
            set { dest_meet_instructions = value; }
        }
        public string Email_Type
        {
            get { return email_type; }
            set { email_type = value; }
        }

    }
}
