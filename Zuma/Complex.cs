using System;
using System.Net;
using System.Net.Mail;
using Twilio;

namespace Zuma
{
    public class Complex
    {
        public void SendMessage(string username, string messageType, bool useEmail)
        {
            var info = User.Fetch(username);
            if (useEmail && IsAcmeEmail(info.Email))
            {
                var message = Utils.GetMsg(messageType);
                var subject = Utils.GetSub(messageType);

                if (IsPremiumUser(info))
                {
                    SendSMTPMessage(username, subject, message);
                }
                else
                {
                    SendAppUserMessage(username, subject, message);
                }

                LogMessage(message);

            }
            else
            {
                SendSmsMessage(username, messageType);
            }
        }

        private void SendSmsMessage(string username, string messageType)
        {
            var twilioRestClient = new TwilioRestClient("AC47c7253af8c6fae4066c7fe3dbe4433c", "[AuthToken]");
            var recipientNumber = Utils.GetNum(username);
            var smsMessage = Utils.GetMsg2(messageType);
            const string senderNumber = "+17245658130";
            twilioRestClient.SendMessage(senderNumber, recipientNumber, smsMessage, MessageCallback);
        }

        private static void SendAppUserMessage(string username, string subject, string message)
        {
            new AppInbox(username).Send(subject, message);
        }

        private static void SendSMTPMessage(string username, string subject, string message)
        {
            var mailClient = new SmtpClient("smtp.acme.net");
            mailClient.Credentials = new NetworkCredential("no-reply@acme.com", "[Password]");

            var name = Utils.GetDisplayName(username).Trim();
            var address = Utils.GetEmailAddress(username);
            var addr2 = new MailAddress(address, name);

            var mailAddress = new MailAddress("no-reply@acme.com", "Acme MailRoom");
            var msg = new MailMessage(mailAddress, addr2);

            msg.Subject = subject;
            msg.Body = message;

            mailClient.Send(msg);
        }

        private static void LogMessage(string messageBody)
        {
            var logger = Logging.CreateLogger();
            logger.CreateNewOrOpenExistingLogAndWriteToLog(typeof(MessageLog), messageBody);
            logger.Close();
        }

        private static bool IsPremiumUser(UserInfo info)
        {
            return info.Start > new DateTime(2011, 5, 10) && info.Country.Code == "USA";
        }

        private bool IsAcmeEmail(string email)
        {
            return email.EndsWith("acme.com") || email.EndsWith("acme.org");
        }

        private void MessageCallback(Message message)
        {
            LogMessage(message.Body);
        }
    }
}