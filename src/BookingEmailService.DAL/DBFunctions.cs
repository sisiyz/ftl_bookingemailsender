using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using BookingEmailService.Common;

namespace BookingEmailService.DAL
{
    public class DBFunctions
    { 
        private static readonly ILog log = LogManager.GetLogger("log");
        private static string _ConnStr = "server=192.168.0.217;database=limo;uid=appuser;pwd=NJLimo888"; //ServiceConfig.DbConnStr;
        private SqlConnection _Context;

        public DBFunctions()
        {
            _Context = new SqlConnection(_ConnStr);
            _Context.Open();
        }

        public List<BookingCentraliseEmailMonitor> GetConfirmationNoFormats()
        {
           List<BookingCentraliseEmailMonitor> objBookingCentraliseEmailMonitors = new List<BookingCentraliseEmailMonitor>();
            string sqlStr = @"booking_centralise_email_get";
                SqlCommand cmd = new SqlCommand(sqlStr, _Context);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookingCentraliseEmailMonitor objBookingCentraliseEmailMonitor = new BookingCentraliseEmailMonitor();
                        if (reader["booking_centralise_email_monitor_id"] != DBNull.Value)
                        {
                            objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id = Convert.ToInt32(reader["booking_centralise_email_monitor_id"]);
                        }
                        if (reader["confirmation_no"] != DBNull.Value && reader["confirmation_no"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.confirmation_no = reader["confirmation_no"].ToString().Trim();
                        }
                        if (reader["company"] != DBNull.Value && reader["company"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.company = reader["company"].ToString().Trim();
                        }
                        if (reader["email_type"] != DBNull.Value && reader["email_type"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.email_type = reader["email_type"].ToString().Trim();
                        }
                        if (reader["acct_send_flag"] != DBNull.Value && reader["acct_send_flag"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.acct_send_flag = reader["acct_send_flag"].ToString().Trim();
                        }
                        if (reader["email_format"] != DBNull.Value && reader["email_format"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.email_format = reader["email_format"].ToString().Trim();
                        }
                        if (reader["vip_send_flag"] != DBNull.Value && reader["vip_send_flag"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.vip_send_flag = reader["vip_send_flag"].ToString().Trim();
                        }
                        if (reader["acct_admin_send_flag"] != DBNull.Value && reader["acct_admin_send_flag"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.acct_admin_send_flag = reader["acct_admin_send_flag"].ToString().Trim();
                        }
                        objBookingCentraliseEmailMonitors.Add(objBookingCentraliseEmailMonitor);
                    }
                }
            }
            catch (Exception ex)
            {
                //UpdateProcessingStatus(objBookingCentraliseEmailMonitor.confirmation_no, objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id, "Application Error");
            }
            return objBookingCentraliseEmailMonitors;
        }

        public BookingCentraliseEmailMonitor GetConfirmationNoFormat()
        {
            BookingCentraliseEmailMonitor objBookingCentraliseEmailMonitor = new BookingCentraliseEmailMonitor();

            string sqlStr = @"booking_centralise_email_get";
            SqlCommand cmd = new SqlCommand(sqlStr, _Context);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["booking_centralise_email_monitor_id"] != DBNull.Value)
                        {
                            objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id = Convert.ToInt32(reader["booking_centralise_email_monitor_id"]);
                        }
                        if (reader["confirmation_no"] != DBNull.Value && reader["confirmation_no"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.confirmation_no = reader["confirmation_no"].ToString().Trim();
                        }
                        if (reader["company"] != DBNull.Value && reader["company"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.company = reader["company"].ToString().Trim();
                        }
                        if (reader["email_type"] != DBNull.Value && reader["email_type"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.email_type = reader["email_type"].ToString().Trim();
                        }
                        if (reader["acct_send_flag"] != DBNull.Value && reader["acct_send_flag"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.acct_send_flag = reader["acct_send_flag"].ToString().Trim();
                        }
                        if (reader["email_format"] != DBNull.Value && reader["email_format"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.email_format = reader["email_format"].ToString().Trim();
                        }
                        if (reader["vip_send_flag"] != DBNull.Value && reader["vip_send_flag"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.vip_send_flag = reader["vip_send_flag"].ToString().Trim();
                        }
                        if (reader["acct_admin_send_flag"] != DBNull.Value && reader["acct_admin_send_flag"].ToString().Trim() != "")
                        {
                            objBookingCentraliseEmailMonitor.acct_admin_send_flag = reader["acct_admin_send_flag"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateProcessingStatus(objBookingCentraliseEmailMonitor.confirmation_no, objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id, "Application Error");
            }
            return objBookingCentraliseEmailMonitor;
        }

        public OrderDeatils GetEmailOrderDetails(BookingCentraliseEmailMonitor objBookingCentraliseEmailMonitor, string email_type)
        {
            OrderDeatils objOrderDetails = new OrderDeatils();
            
            string sqlStr = @"booking_centralise_email_orderdetail_get";
            SqlCommand cmd = new SqlCommand(sqlStr, _Context);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@conf", objBookingCentraliseEmailMonitor.confirmation_no);
            cmd.Parameters.AddWithValue("@email_type", email_type);

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["confirmation_no"] != DBNull.Value && reader["confirmation_no"].ToString().Trim() != "")
                        {
                            objOrderDetails.confirmation_no = reader["confirmation_no"].ToString().Trim();
                        }
                        if (reader["acct_ID"] != DBNull.Value && reader["acct_ID"].ToString().Trim() != "")
                        {
                            objOrderDetails.acct_ID = reader["acct_ID"].ToString().Trim();
                        }
                        if (reader["passenger_name"] != DBNull.Value && reader["passenger_name"].ToString().Trim() != "")
                        {
                            objOrderDetails.passenger = reader["passenger_name"].ToString().Trim();
                        }
                        if (reader["company_name"] != DBNull.Value && reader["company_name"].ToString().Trim() != "")
                        {
                            objOrderDetails.company_name = reader["company_name"].ToString().Trim();
                        }
                        if (reader["company_address_1"] != DBNull.Value && reader["company_address_1"].ToString().Trim() != "")
                        {
                            objOrderDetails.company_address_1 = reader["company_address_1"].ToString().Trim();
                        }
                        if (reader["company_address_2"] != DBNull.Value && reader["company_address_2"].ToString().Trim() != "")
                        {
                            objOrderDetails.company_address_2 = reader["company_address_2"].ToString().Trim();
                        }
                        if (reader["company_phone"] != DBNull.Value && reader["company_phone"].ToString().Trim() != "")
                        {
                            objOrderDetails.company_phone = reader["company_phone"].ToString().Trim();
                        }
                        if (reader["acct_address_3"] != DBNull.Value && reader["acct_address_3"].ToString().Trim() != "")
                        {
                            objOrderDetails.acct_address_3 = reader["acct_address_3"].ToString().Trim();
                        }
                        if (reader["trip_date_time"] != DBNull.Value)
                        {
                            objOrderDetails.trip_date_time = Convert.ToDateTime(reader["trip_date_time"]);
                        }
                        if (reader["AgentID"] != DBNull.Value && reader["AgentID"].ToString().Trim() != "")
                        {
                            objOrderDetails.agent_id = reader["AgentID"].ToString().Trim();
                        }
                        if (reader["car_type"] != DBNull.Value && reader["car_type"].ToString().Trim() != "")
                        {
                            objOrderDetails.car_type = reader["car_type"].ToString().Trim();
                        }
                        if (reader["field_14"] != DBNull.Value && reader["field_14"].ToString().Trim() != "")
                        {
                            objOrderDetails.no_passenger = reader["field_14"].ToString().Trim();
                        }
                        if (reader["field_11"] != DBNull.Value && reader["field_11"].ToString().Trim() != "")
                        {
                            objOrderDetails.reservation_type = reader["field_11"].ToString().Trim();
                        }
                        if (reader["pu_address"] != DBNull.Value && reader["pu_address"].ToString().Trim() != "")
                        {
                            objOrderDetails.pu_address = reader["pu_address"].ToString().Trim();
                        }
                        if (reader["dest_address"] != DBNull.Value && reader["dest_address"].ToString().Trim() != "")
                        {
                            objOrderDetails.dest_address = reader["dest_address"].ToString().Trim();
                        }
                        if (reader["stop_address_1"] != DBNull.Value && reader["stop_address_1"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_1 = reader["stop_address_1"].ToString().Trim();
                        }
                        if (reader["stop_address_2"] != DBNull.Value && reader["stop_address_2"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_2 = reader["stop_address_2"].ToString().Trim();
                        }
                        if (reader["stop_address_3"] != DBNull.Value && reader["stop_address_3"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_3 = reader["stop_address_3"].ToString().Trim();
                        }
                        if (reader["stop_address_4"] != DBNull.Value && reader["stop_address_4"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_4 = reader["stop_address_4"].ToString().Trim();
                        }
                        if (reader["stop_address_5"] != DBNull.Value && reader["stop_address_5"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_5 = reader["stop_address_5"].ToString().Trim();
                        }
                        if (reader["stop_address_6"] != DBNull.Value && reader["stop_address_6"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_6 = reader["stop_address_6"].ToString().Trim();
                        }
                        if (reader["stop_address_7"] != DBNull.Value && reader["stop_address_7"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_7 = reader["stop_address_7"].ToString().Trim();
                        }
                        if (reader["stop_address_8"] != DBNull.Value && reader["stop_address_8"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_8 = reader["stop_address_8"].ToString().Trim();
                        }
                        if (reader["stop_address_9"] != DBNull.Value && reader["stop_address_9"].ToString().Trim() != "")
                        {
                            objOrderDetails.stop_address_9 = reader["stop_address_9"].ToString().Trim();
                        }
                        if (reader["comment"] != DBNull.Value && reader["comment"].ToString().Trim() != "")
                        {
                            objOrderDetails.comment = reader["comment"].ToString().Trim();
                        }
                        if (reader["field_8"] != DBNull.Value && reader["field_8"].ToString().Trim() != "")
                        {
                            objOrderDetails.pu_meet_instructions = reader["field_8"].ToString().Trim();
                        }
                        if (reader["field_9"] != DBNull.Value && reader["field_9"].ToString().Trim() != "")
                        {
                            objOrderDetails.dest_meet_instructions = reader["field_9"].ToString().Trim();
                        }
                        if (reader["field_15"] != DBNull.Value && reader["field_15"].ToString().Trim() != "")
                        {
                            objOrderDetails.vendor_name = reader["field_15"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateProcessingStatus(objBookingCentraliseEmailMonitor.confirmation_no, objBookingCentraliseEmailMonitor.booking_centralise_email_monitor_id, "Application Error");
            }

            return objOrderDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="confirmation_no"></param>
        /// <param name="is_transfer"></param>
        /// <param name="booking_centralise_email_monitor_id"></param>
        public void Updateistransfer(string confirmation_no, bool is_transfer, int booking_centralise_email_monitor_id)
        {
            string sqlStr = @"booking_centralise_email_update_istransfer";
            SqlCommand cmd = new SqlCommand(sqlStr, _Context);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@conf", confirmation_no);
            cmd.Parameters.AddWithValue("@is_transfer", is_transfer);
            cmd.Parameters.AddWithValue("@booking_centralise_email_monitor_id", booking_centralise_email_monitor_id);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                UpdateProcessingStatus(confirmation_no, booking_centralise_email_monitor_id, "Application Error");
            }
        }

        public void UpdateProcessingStatus(string confirmation_no, int booking_centralise_email_monitor_id, string booking_centralise_email_status)
        {
            string sqlStr = @"booking_centralise_email_update_status";
            SqlCommand cmd = new SqlCommand(sqlStr, _Context);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@conf", confirmation_no);
            cmd.Parameters.AddWithValue("@booking_centralise_email_monitor_id", booking_centralise_email_monitor_id);
            cmd.Parameters.AddWithValue("@booking_centralise_email_status", booking_centralise_email_status);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

        public string GetDirectoryPath( string company)
        {
            string directory = "";
            object result;
            string sqlStr = @"select email_directory from system_parameter_dispatch where company='" + company +"'";
            SqlCommand cmd = new SqlCommand(sqlStr, _Context);
            cmd.CommandType = CommandType.Text;

            try
            {
                result=cmd.ExecuteScalar();
                if (result != null)
                {
                    directory = result.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
            }

            return directory;
        }

        public string GetEmailAddress(string confirmation_no)
        {
            string email_address = "";
            object result;
            string sqlStr = @"select isnull(email_add,'') as email_add from operator  where confirmation_no='" + confirmation_no + "'";
            SqlCommand cmd = new SqlCommand(sqlStr, _Context);
            cmd.CommandType = CommandType.Text;

            try
            {
                result = cmd.ExecuteScalar();
                if (result != null)
                {
                    email_address = result.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
            }

            return email_address;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    _Context.Close();
                    _Context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
