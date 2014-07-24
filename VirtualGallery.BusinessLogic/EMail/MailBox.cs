using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using VirtualGallery.BusinessLogic.Configuration;
using VirtualGallery.BusinessLogic.EMail.Interfaces;
using VirtualGallery.BusinessLogic.EMail.Messages;
using VirtualGallery.Infrastructure.Logging;

namespace VirtualGallery.BusinessLogic.EMail
{
    public class MailBox : IMailBox
    {
        private readonly string _from;

        private readonly IMessageQueue _messageQueue;

        private readonly ISmtpClientFactory _smtpClienFactory;

        public MailBox(IMessageQueue messageQueue, ISmtpClientFactory smtpClientFactory)
        {
            _messageQueue = messageQueue;

            _smtpClienFactory = smtpClientFactory;

            _from = AppSettings.MailFrom;
        }

        public bool Send(Message message)
        {
            QueueMessage(message);
            SendQueuedMessages();
            return true;
        }

        private void SendQueuedMessages()
        {
            new TaskFactory().StartNew(SendQueuedMessagesTask);
        }
         
        private MailMessage CreateMailMessage(Message message)
        {
            return CreateMailMessage(message.To, 
                string.IsNullOrEmpty(message.From) ? _from : message.From, 
                message.Subject, message.Body);
        }

        private MailMessage CreateMailMessage(string to, string from, string subject, string body)
        {
            return new MailMessage(new MailAddress(from), new MailAddress(to))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
        }

        private void QueueMessage(Message message)
        {
            _messageQueue.Enqueue(message);
        }

        private bool SendMessage(Message message, SmtpClient smtpClient)
        {
            try
            {
                //MailMessage mailMessage = CreateMailMessage(message);
                //smtpClient.Send(mailMessage);

                SendMail("smtp.gmail.com",
                        465,
                        "dmitryshelemov@gmail.com",
                        "xw30qxw30q",
                        "dmitryshelemov@gmail.com",
                        message.To,
                        message.Subject,
                        message.Body,
                        true);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(string.Format("Failed to send Email to {0}", message.To), ex, LogLevel.Error);

                return false;
            }

            Logger.Instance.WriteLogFormat(LogLevel.Debug, "Message to {0} sent successfully", message.To);

            return true;
        }

        public static void SendMail(string sHost, int nPort, string sUserName, string sPassword,
        string sFromEmail, string sToEmail, string sHeader, string sMessage, bool fSSL)
        {
            System.Web.Mail.MailMessage Mail = new System.Web.Mail.MailMessage();
            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = sHost;
            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = 2;

            Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] =
         nPort.ToString();
            if (fSSL)
                Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpusessl"] = "true";

            if (sUserName.Length == 0)
            {
                //Ingen auth
            }
            else
            {
                Mail.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
                Mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] =
         sUserName;
                Mail.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] =
         sPassword;
            }

            Mail.To = sToEmail;
            Mail.From = sFromEmail;
            Mail.Subject = sHeader;
            Mail.Body = sMessage;
            Logger.Instance.WriteLogFormat(LogLevel.Debug, "Message to {0} sent successfully", sMessage);
            Mail.BodyFormat = System.Web.Mail.MailFormat.Html;

            System.Web.Mail.SmtpMail.SmtpServer = sHost;
            System.Web.Mail.SmtpMail.Send(Mail);
        }

        private void SendQueuedMessagesTask()
        {
            Logger.Instance.WriteLog("SendQueuedMessagesTask", LogLevel.Error);
            var queued = _messageQueue.DequeueAll().ToList();
            if (!queued.Any())
            {
                return;
            }

            Logger.Instance.WriteLog("SendQueuedMessagesTask try" + queued.Count, LogLevel.Error);
            SmtpClient smtpClient;
            try
            {
                smtpClient = _smtpClienFactory.CreateSmtpClient();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("Failed to create smtp client", ex, LogLevel.Error);

                return;
            }

            foreach (var message in queued)
            {
                SendMessage(message, smtpClient);
            }

            smtpClient.Dispose();
        }
    }
}