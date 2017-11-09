using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingEmailService.Common;
using BookingEmailService.DAL;
using System.Net;
using System.Configuration;
using System.IO;
using log4net;
using NReco.PdfGenerator;

namespace BookingEmailService.BL
{
    public class Email
    {
        private static readonly ILog log = LogManager.GetLogger("log");
        private string builder = "";
        //string ftp_username = ConfigurationManager.AppSettings["ftp_username"];
        //string ftp_password = ConfigurationManager.AppSettings["ftp_password"];

        string local_email_path = ConfigurationManager.AppSettings["local_email_path"];

        #region GETHTML
        public string GetReportHtml(OrderDeatils objOrdertails)
        {
            GetHtmlHeader();
            GetReportHeader(objOrdertails);
            GetReportSummary(objOrdertails);
            GetReportItinerary_Farm(objOrdertails);
            GetReportTripInstruction(objOrdertails);

            return builder;
        }

        private void GetHtmlHeader()
        {
            builder = builder + ("<!DOCTYPE html>");
            builder = builder + ("<html xmlns='http://www.w3.org/1999/xhtml'>");
            builder = builder + ("<head>");
            builder = builder + ("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
            builder = builder + ("<style type='text/css'>");
            builder = builder + ("* { font-family: Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif; font-size: 14px; }");
            builder = builder + (".shadow { -moz-box-shadow: 3px 3px 4px #000; -webkit-box-shadow: 3px 3px 4px #000; box-shadow: 3px 3px 4px #000; /* For IE 8 */ -ms-filter: 'progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color='#000000')'; /* For IE 5.5 - 7 */ filter: progid:DXImageTransform.Microsoft.Shadow(Strength=4, Direction=135, Color='#000000'); }");
            builder = builder + (".cell_title { font-weight: bold; width: 120px; padding: 5px; }");
            builder = builder + (".cell_title_1 { font-weight: bold; width: 60px; padding: 5px; }");
            builder = builder + (".cell_data { width: 250px; padding: 5px; }");
            builder = builder + (".cell_data_1 { padding: 5px; }");
            builder = builder + (".cell_data_2 { padding: 5px; font-weight: bold; }");
            builder = builder + (".sub_header { text-align: center; text-decoration: underline; font-size: 16px; font-weight: bold; margin-top: 20px; margin-bottom: 20px; }");
            builder = builder + ("</style>");
            builder = builder + ("<title>  </title>");
            builder = builder + ("</head>");
            builder = builder + ("<body>");
        }

        private void GetReportHeader(OrderDeatils objOrdertails)
        {
            builder = builder + ("<div style='margin-left: auto; margin-right: auto; width: 760px;'>");
            builder = builder + ("<div style='height: 20px;'></div>");
            builder = builder + ("<table cellpadding='0' cellspacing='0' border='0' style='width: 100%'>");
            builder = builder + ("<tr>");
            builder = builder + ("<td>");
            builder = builder + ("<div style='font-size: 22px; font-weight: bold; text-align: center; margin-bottom: 10px;'>");
            builder = builder + ("" + objOrdertails.company_name);
            builder = builder + ("</div>");
            builder = builder + ("<div style='font-weight: bold; text-align: center; display: block; margin-bottom: 8px;'>");
            builder = builder + ("" + objOrdertails.company_address_1);
            builder = builder + ("<div style='font-weight: bold; text-align: center; display: block; margin-bottom: 8px;'>");
            builder = builder + ("" + objOrdertails.company_address_2);
            builder = builder + ("<div style='font-weight: bold; text-align: center; display: block; margin-bottom: 8px;'>");
            builder = builder + ("" + objOrdertails.company_phone);
            builder = builder + ("</td>");
            builder = builder + ("</tr></table>");

            builder = builder + ("<div style='height: 20px;'></div>");
            builder = builder + ("<table align='center' style='text-align: center; margin-left: 40px; width: 680px; height: 35px; border: solid 1px #000; padding-top: 5px;' class='shadow'>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style='font-size: 16px; font-weight: bold; margin-left: 0px;'>CONFIRMATION OF &nbsp;&nbsp;</TD>");
            builder = builder + ("<td style='font-size: 24px; font-weight: bold; color: red;'>" + ("" + objOrdertails.reservation_type) + "</td>");
            builder = builder + ("<td style='font-size: 16px; font-weight: bold;'>&nbsp;&nbsp;RESERVATION</td>");
            builder = builder + ("</tr>"); ;
            builder = builder + ("</table>");

            builder = builder + ("<div style='clear: both; height: 30px;'></div>");
            builder = builder + ("<div style='text-align: center; margin-top: 10px;'>");
            builder = builder + ("Thank you for choosing " + objOrdertails.company_name + " for your travel needs.  PLEASE REVIEW THE ITINERARY BELOW.");
            builder = builder + ("<br />");
            builder = builder + ("Please contact us with any questions.");
            builder = builder + ("</div>");
        }

        private void GetReportSummary(OrderDeatils objOrdertails)
        {
            builder = builder + ("<hr style=' margin-top: 10px; margin-bottom: 10px; border-bottom: solid 3px #000;'/>");
            builder = builder + ("<table align='center' cellpadding='0' cellspacing='0' style='width: 760px; border: none;'>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 120px; padding: 5px;'>Passenger</td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + ("" + objOrdertails.passenger) + "</td>");
            builder = builder + ("<td style=' font-weight: bold; width: 130px; padding: 5px;'>Confirmation #</td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + objOrdertails.confirmation_no.ToString().Substring(4) + "</td>");
            builder = builder + ("</tr>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 120px; padding: 5px;'>Phone</td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + ("" + objOrdertails.acct_address_3) + "</td>");
            builder = builder + ("<td style=' font-weight: bold; width: 130px; padding: 5px;'>Trip Date</td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + objOrdertails.trip_date_time.Date.ToShortDateString().ToString() + "</td>");
            builder = builder + ("</tr>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 120px; padding: 5px;'>Agent</td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + ("" + objOrdertails.agent_id) + "</td>");
            builder = builder + ("<td style=' font-weight: bold; width: 130px; padding: 5px;'>Trip Time</td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + objOrdertails.trip_date_time.ToLongTimeString() + "</td>");

            builder = builder + ("</tr>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 120px; padding: 5px;'> </td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + " " + "</td>");
            builder = builder + ("<td style=' font-weight: bold; width: 130px; padding: 5px;'>Car Type</td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + ("" + objOrdertails.car_type) + "</td>");
            builder = builder + ("<td style=' font-weight: bold; width: 120px; padding: 5px;'>Passengers #</td>");
            builder = builder + ("<td style=' width: 250px; padding: 5px;'>" + ("" + objOrdertails.no_passenger) + "</td>");

            builder = builder + ("</tr>");
            builder = builder + ("</table>");
        }

        private void GetReportItinerary_Farm(OrderDeatils objOrdertails)
        {
            builder = builder + ("<div style=' text-align: center; text-decoration: underline; font-size: 16px; font-weight: bold; margin-top: 20px; margin-bottom: 20px;'>ITINERARY</div>");

            builder = builder + ("<table  align='center' cellpadding='0' cellspacing='0' style='width: 760px;'>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Pickup</td>");
            builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.pu_address.Trim()) + "</td>");
            builder = builder + ("</tr>");

            if (objOrdertails.stop_address_2.Trim() != "")
            {
                builder = builder + ("<tr>");
                builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Stop1</td>");
                builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.stop_address_2.Trim()) + "</td>");
                builder = builder + ("</tr>");
            }

            if (objOrdertails.stop_address_3.Trim() != "")
            {
                builder = builder + ("<tr>");
                builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Stop2</td>");
                builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.stop_address_3.Trim()) + "</td>");
                builder = builder + ("</tr>");
            }

            if (objOrdertails.stop_address_4.Trim() != "")
            {
                builder = builder + ("<tr>");
                builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Stop3</td>");
                builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.stop_address_4.Trim()) + "</td>");
                builder = builder + ("</tr>");
            }

            if (objOrdertails.stop_address_5.Trim() != "")
            {
                builder = builder + ("<tr>");
                builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Stop4</td>");
                builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.stop_address_5.Trim()) + "</td>");
                builder = builder + ("</tr>");
            }

            if (objOrdertails.stop_address_6.Trim() != "")
            {
                builder = builder + ("<tr>");
                builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Stop5</td>");
                builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.stop_address_6.Trim()) + "</td>");
                builder = builder + ("</tr>");
            }

            if (objOrdertails.stop_address_7.Trim() != "")
            {
                builder = builder + ("<tr>");
                builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Stop6</td>");
                builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.stop_address_7.Trim()) + "</td>");
                builder = builder + ("</tr>");
            }

            if (objOrdertails.stop_address_8.Trim() != "")
            {
                builder = builder + ("<tr>");
                builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Stop7</td>");
                builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.stop_address_8.Trim()) + "</td>");
                builder = builder + ("</tr>");
            }

            if (objOrdertails.stop_address_9.Trim() != "")
            {
                builder = builder + ("<tr>");
                builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Stop8</td>");
                builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.stop_address_9.Trim()) + "</td>");
                builder = builder + ("</tr>");
            }

            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 60px; padding: 5px;'>Destination</td>");
            builder = builder + ("<td style=' font-size :11px; padding: 5px;'>" + ("" + objOrdertails.dest_address) + "</td>");
            builder = builder + ("</tr>");
            builder = builder + ("</table>");

            builder = builder + ("<td valign='top'>");
            builder = builder + ("<table cellpadding='0' cellspacing='0' style='width: 280px; border: none'>");
            builder = builder + ("</table>");
            builder = builder + ("</td>");
        }

        private void GetReportTripInstruction(OrderDeatils objOrdertails)
        {
            builder = builder + ("<table align='center' cellpadding='0' cellspacing='0' style='width: 480px;'>");
            builder = builder + ("<div style='height: 20px'></div>");
            builder = builder + ("</table>");
            builder = builder + ("<div style=' text-align: center; text-decoration: underline; font-size: 16px; font-weight: bold; margin-top: 20px; margin-bottom: 20px;'>TRIP INSTRUCTION</div>");
            builder = builder + ("<table align='center' cellpadding='0' cellspacing='0' style='width: 760px; border: none;'>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 120px; padding: 5px;'>Comments :</td>");
            builder = builder + ("<td style=' padding: 5px; font-weight: bold; color: red;'>" + ("" + objOrdertails.comment) + "</td>");
            builder = builder + ("</tr>");
            builder = builder + ("</table>");
            builder = builder + ("<div style='height: 20px'></div>");
            builder = builder + ("<table align='center' cellpadding='0' cellspacing='0' style='width: 760px; border: none;'>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 120px; padding: 5px;'>Pickup Meet Instructions :</td>");
            builder = builder + ("<td style=' padding: 5px; font-weight: bold;'>" + ("" + objOrdertails.pu_meet_instructions) + "</td>");
            builder = builder + ("</tr>");
            builder = builder + ("<tr>");
            builder = builder + ("<td style=' font-weight: bold; width: 120px; padding: 5px;'>Destination Meet Instructions :</td>");
            builder = builder + ("<td style=' padding: 5px; font-weight: bold;'>" + ("" + objOrdertails.dest_meet_instructions) + "</td>");
            builder = builder + ("</tr>");
            builder = builder + ("</table>");
        }

        #endregion

        #region TransferEmail

        public void TransferEmail(OrderDeatils objOrderDetails, string builder, string email_format, int booking_centralise_email_monitor_id)
        {
            WebClient client = new WebClient();
            DBFunctions objDBFunctions = new DBFunctions();
            string directory = "";
            string sSubject = "";
            string sSendInfo = "";
            //string strFileName_attach = "";
            string ftp_name_path = "";

            string local_email_path = ConfigurationManager.AppSettings["local_email_path"];
            local_email_path = email_format == "HTML" ? local_email_path + ".html" : local_email_path + ".pdf";

            try
            {
                directory = objDBFunctions.GetDirectoryPath(objOrderDetails.company);
                sSubject = objDBFunctions.GetEmailSubject(objOrderDetails);
                sSendInfo = objDBFunctions.GetEmailAddress(objOrderDetails.confirmation_no);

                ftp_name_path = email_format == "HTML" ? directory + "TT_" + objOrderDetails.confirmation_no + ".html" : directory + "TT_" + objOrderDetails.confirmation_no + ".pdf";
                sSendInfo = "sisi.han@tandemttsi.com";
                //email_format = "HTML";

                if (email_format == "HTML")
                {
                    //write HTML file to local
                    File.WriteAllText(local_email_path, builder.ToString());
                }
                else
                {
                    //write PDF file to local
                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                    var pdfBytes = htmlToPdf.GeneratePdf(builder);
                    File.WriteAllBytes(local_email_path, pdfBytes);
                }

                //FTP Password
                //client.Credentials = new NetworkCredential(ftp_username, ftp_password);
                client.Credentials = CredentialCache.DefaultCredentials;

                //upload file to ftp
                client.UploadFile(ftp_name_path, "STOR", local_email_path);

                //ftp_name_path = "\\\\cluster-1fs\\Aleph_share\\EmailConf\\FT_1700458258.pdf";

                //Insert to Email Table
                objDBFunctions.InsertToEmailTable(objOrderDetails.confirmation_no, booking_centralise_email_monitor_id, ftp_name_path, sSendInfo, ftp_name_path, sSubject, objOrderDetails.company);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}
