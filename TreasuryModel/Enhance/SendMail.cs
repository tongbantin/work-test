using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using log4net;

namespace TreasuryModel.Enhance
{
    public class SendMail
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(SendMail));
        public static void Send(string From, string To, string Cc, string Subject, string Message, List<Attachment> Attach, string SMTP, int Port)
        {
            try
            {
                //Parameter
                string to = To;
                bool isHtml = true;
                string message = Message;

                MailMessage mail = new MailMessage();
                // Set the to and from addresses.
                // The from address must be your GMail account
                mail.From = new MailAddress(From);
                string[] tto = to.Split(',');
                for (int i = 0; i < tto.Count(); i++)
                {
                    mail.To.Add(new MailAddress(tto[i].Replace("\n", "")));
                }
                if (!Cc.Trim().Equals(""))
                {
                    string[] ccto = Cc.Split(',');
                    for (int j = 0; j < ccto.Count(); j++)
                    {
                        mail.CC.Add(new MailAddress(ccto[j].Replace("\n", "")));
                    }
                }
                // Define the message
                mail.Subject = Subject;
                mail.IsBodyHtml = isHtml;
                mail.Body = message;
                foreach (Attachment a in Attach)
                {
                    mail.Attachments.Add(a);
                }

                // Create a new Smpt Client using Google's servers
                var mailclient = new SmtpClient();
                //mailclient.Host = "smtp.gmail.com";//ForGmail
                mailclient.Host = SMTP;
                mailclient.Port = Port;

                // This is the critical part, you must enable SSL
                //mailclient.EnableSsl = true;//ForGmail
                mailclient.EnableSsl = false;
                mailclient.UseDefaultCredentials = true;

                // Specify your authentication details
                //mailclient.Credentials = new System.Net.NetworkCredential("SomeGmailAccount@gmail.com", "PaswordXYX123");//ForGmail
                //mailclient.Credentials = new System.Net.NetworkCredential("noreply@gmail.com", "PaSsWaRd");

                mailclient.Send(mail);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }
    }
}
