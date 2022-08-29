using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;
using Asb404.Models;
using System.Configuration;

namespace Asb404.Models
{

    public class Validate
    {
        DBContexter _db = new DBContexter();
      
    
        public  bool IsValidEmailId(string InputEmail)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(InputEmail);
            if (match.Success)
                return true;
            else
                return false;
        }
        public  bool IsValidUSPhoneNumber(string strPhone)
        {
            Regex regex = new Regex(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$");
            Match match = regex.Match(strPhone);
            if (match.Success)
                return true;
            else
                return false;
        }
        public bool IsLength(string word,Int16 TextLength)
        {
            if (word.Length >= TextLength)
                return true;
            else
                return false;
        }
        public bool IsEnValid(String str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
         
        }
        // Function which is used in IsValidUSPhoneNumber function

       public bool MailForget(string mail,string Username, string Password)
        {
          
            try
            {
                string path = HostingEnvironment.MapPath("~/App_Data/WelcomeEmail.txt");
                string body;
                //Read template file from the App_Data folder
                using (var sr = new StreamReader(path))
                {
                    body = sr.ReadToEnd();
                }








                //htmlview = "<body style=\"font-family:'Century Gothic'\">";
                //htmlview += "<h1 style=\"text-align:center;\"> " + /*Session["[Global]Groups"].ToString*/"" + " & VPN " + "</h1>";
                //htmlview += "<h2 style=\"font-size:14px;\">";
                //if (Session["[Global]Groups"].ToString().ToUpper() == "Cisco".ToUpper())


                //htmlview += "User   : " + Username + "<br />";
                //htmlview += "Password  : " + Password + "<br />";
                //htmlview += "Active Date : " + Session["[Global]EffectiveFrom"].ToString() + "<br />";
                //htmlview += "Expire Date : " + Session["[Global]Expires"].ToString() + "<br /><br />";

                //htmlview += "PM9 & SoftEther Server : 192.30.85.5 <br /><br />";


                //if ((Session["[Global]Role"].ToString().ToUpper() == "User".ToUpper()) || (Session["[Global]Role"].ToString().ToUpper() == "manager".ToUpper()))

                //    htmlview += "Adress Update : <a href=" + "192.30.85.5" + ">" + "192.30.85.5" + "</a></h2></body>";
                //else
                string username = HttpUtility.UrlEncode(Username);
                string password = HttpUtility.UrlEncode(Password);


                string messageBody = string.Format(body, username, username, password);

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(mail.ToString());
                message.From = new System.Net.Mail.MailAddress("info@hooman.co");
                message.Body = messageBody;
                message.Subject = "Contact Us Name : " + Username+ " Mail : " + mail;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
        
                    //message.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath("~/Downloads/anyconnect-win.msi")));
                    //message.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath("~/Downloads/anyconnect-Android.apk")));
              

                string sendEmailsFrom = "info@hooman.co";
                string sendEmailsFromPassword = "79030712";
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("hooman.co", 25);
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword);
                // smtp.Timeout = 20000;
                smtp.Send(message);

                //MessageBox.Show("mail Send");
                return true;

            }
            catch (Exception)
            {

                return false;

            }

        }
        public bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
        public bool ContactMail(string email, string subject, string memo)
        {

            try
            {
             
             








                //htmlview = "<body style=\"font-family:'Century Gothic'\">";
                //htmlview += "<h1 style=\"text-align:center;\"> " + /*Session["[Global]Groups"].ToString*/"" + " & VPN " + "</h1>";
                //htmlview += "<h2 style=\"font-size:14px;\">";
                //if (Session["[Global]Groups"].ToString().ToUpper() == "Cisco".ToUpper())


                //htmlview += "User   : " + Username + "<br />";
                //htmlview += "Password  : " + Password + "<br />";
                //htmlview += "Active Date : " + Session["[Global]EffectiveFrom"].ToString() + "<br />";
                //htmlview += "Expire Date : " + Session["[Global]Expires"].ToString() + "<br /><br />";

                //htmlview += "PM9 & SoftEther Server : 192.30.85.5 <br /><br />";


                //if ((Session["[Global]Role"].ToString().ToUpper() == "User".ToUpper()) || (Session["[Global]Role"].ToString().ToUpper() == "manager".ToUpper()))

                //    htmlview += "Adress Update : <a href=" + "192.30.85.5" + ">" + "192.30.85.5" + "</a></h2></body>";
                //else
      


        

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(ConfigurationManager.AppSettings["mailadmin"]);
                message.From = new System.Net.Mail.MailAddress("info@asb404.ir");
                message.Body = memo.ToString()+"<br/> Email : "+email.ToString();
                message.Subject = "Subject : " + subject + " asb404.ir ";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                //message.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath("~/Downloads/anyconnect-win.msi")));
                //message.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath("~/Downloads/anyconnect-Android.apk")));


                string sendEmailsFrom = "info@asb404.ir";
                string sendEmailsFromPassword = "79030712";
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("asb404.ir", 25);
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword);
                // smtp.Timeout = 20000;
                smtp.Send(message);

                //MessageBox.Show("mail Send");
                return true;

            }
            catch (Exception)
            {

                return false;

            }

        }
    }
}