using System;
using System.Net;
using System.Net.Mail;
using Twilio;

namespace Zuma
{
    public class Complex2
    {
        private class MessageObject
        {
            public string Message { get; private set; }
            public string ShortMessage { get; private set; }
            public string Subject { get; private set; }

            public static MessageObject Create(string messageType)
            {
                return new MessageObject
                {
                    Message = Utils.GetMsg(messageType),
                    ShortMessage = Utils.GetMsg2(messageType),
                    Subject = Utils.GetSub(messageType)
                };
            }
        }

        public void SendMessage(string username, string messageType, bool useEmail)
        {
            var info = User.Fetch(username);
            var messageObj = MessageObject.Create(messageType);

            if (useEmail && IsAcmeEmail(info.Email))
            {
                if (IsPremiumUser(info))
                {
                    SendSMTPMessage(username, messageObj);
                }
                else
                {
                    SendInAppMessage(username, messageObj);
                }

                LogMessage(messageObj.Message);

            }
            else
            {
                SendSmsMessage(username, messageObj);
            }
        }

        private void SendSmsMessage(string username, MessageObject messageObj)
        {
            const string senderNumber = "+17245658130";
            var twilioRestClient = new TwilioRestClient("AC47c7253af8c6fae4066c7fe3dbe4433c", "[AuthToken]");
            var recipientNumber = Utils.GetNum(username);
            twilioRestClient.SendMessage(senderNumber, recipientNumber, messageObj.ShortMessage, MessageCallback);
        }

        private void SendInAppMessage(string username, MessageObject messageObj)
        {
            new AppInbox(username).Send(messageObj.Subject, messageObj.Message);
        }

        private void SendSMTPMessage(string username, MessageObject messageObj)
        {
            var mailClient = new SmtpClient("smtp.acme.net");
            mailClient.Credentials = new NetworkCredential("no-reply@acme.com", "[Password]");

            var name = Utils.GetDisplayName(username).Trim();
            var address = Utils.GetEmailAddress(username);
            var addr2 = new MailAddress(address, name);

            var mailAddress = new MailAddress("no-reply@acme.com", "Acme MailRoom");
            var msg = new MailMessage(mailAddress, addr2);

            msg.Subject = messageObj.Subject;
            msg.Body = messageObj.Message;

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