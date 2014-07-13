﻿using System;
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
            Logger.Instance.WriteLog("Send " + message.From, LogLevel.Error);
            QueueMessage(message);
            SendQueuedMessages();
            return true;
        }

        private void SendQueuedMessages()
        {
            Logger.Instance.WriteLog("SendQueuedMessages", LogLevel.Error);
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
            Logger.Instance.WriteLog("QueueMessage " + message.From, LogLevel.Error);
            _messageQueue.Enqueue(message);
        }

        private bool SendMessage(Message message, SmtpClient smtpClient)
        {
            try
            {
                MailMessage mailMessage = CreateMailMessage(message);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(string.Format("Failed to send Email to {0}", message.To), ex, LogLevel.Error);

                return false;
            }

            Logger.Instance.WriteLogFormat(LogLevel.Debug, "Message to {0} sent successfully", message.To);

            return true;
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