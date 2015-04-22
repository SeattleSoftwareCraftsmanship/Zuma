using System;
using System.Net;
using System.Net.Mail;
using Twilio;

/*************************************************************************
 * filename: LowRisk.cs
 * namespace: ZumaLuma
 * created on: 4/1/2001
 * created by: t.howard
 * description: <include a detailed description of what this class does>
 ************************************************************************/

namespace Zuma
{
    public class LowRisk
    {
        public void SendMessage(string client, string messageType, bool email)
        {
            // if we want to send the message by email then 
            // we need to create a new client, provide our
            // credentials, then we need to use get the message
            // type subjedt and body based on messageType provided,
            // then send the email, otherwise we send an SMS

            var info = User.Fetch(client);

            // To send an email we the client needs to be in the US
            // and have an email address
            // and has to have been active since May 10, 2011
            if (email)
            {
                if (info.Email.EndsWith("acme.com") || info.Email.EndsWith("acme.org"))
                {
                    if (info.Start > new DateTime(2011, 5, 10) && info.Country.Code == "USA")
                    {
                        var addr = new MailAddress("no-reply@acme.com", "Acme MailRoom");

                        var mailClient = new SmtpClient("smtp.acme.net");
                            mailClient.Credentials = new NetworkCredential("no-reply@acme.com", "[Password]");
                            var name = Utils.GetDisplayName(client);
                                var trimmedName = name.Trim();
                                var address = Utils.GetEmailAddress(client);
                            var addr2 = new MailAddress(address, trimmedName);
                            var msg = new MailMessage(addr, addr2);
                        msg.Subject = Utils.GetSub(messageType);

                        // Modified by a.krasowskis on 4/12/2001
                        msg.Body = Utils.GetMsg(messageType);
                        mailClient.Send(msg);

                        // log that we sent the message
                        var logger = Logging.CreateLogger();
                        logger.CreateNewOrOpenExistingLogAndWriteToLog(typeof (MessageLog), msg.Body);
                        logger.Close();
                    }
                    else
                    {
                        // legacy clients & clients outside of USA receive notifications
                        // in their web inbox

                        var msg = Utils.GetMsg(messageType);
                        var sub = Utils.GetSub(messageType);
                        var appInbox = new AppInbox(client);
                        appInbox.Send(sub, msg);

                        // log that we sent the message
                        var logger = Logging.CreateLogger();
                        logger.CreateNewOrOpenExistingLogAndWriteToLog(typeof(MessageLog), msg);
                        logger.Close();
                    }
                }
            }
/*
            else if (customMessenger)
            {
                var c = new customMessengerClient("4c16ca7e-a612-4aab-b22a-2aeb8723f702", "ALFKI", null);
                var msg = Utils.GetMsg3(messageType);
                var r = Utils.GetNum2(client);
                c.Send(msg, r);

            }
*/
            else
            {
                // create a new TwilioRestClient instance
                // first parameter is our Twilio account SID, second is our auth token
                var c = new TwilioRestClient("AC47c7253af8c6fae4066c7fe3dbe4433c", "[AuthToken]");

                // we need the users phone number
                var n = Utils.GetNum(client);

                // we need the message type
                var m = Utils.GetMsg2(messageType);

                // this is our phone number
                var n2 = "+17245658130";

                // send the text message
                // first parameter is our phone number
                c.SendMessage(n2, n, m, cb);
            }


        }

        public void cb(Object message)
        {
            string text;
            if (message is Message)
            {
                text = ((Message) message).Body;
            }
            else // is string
            {
                text = message as String;
            }
            // log that we sent the message
            var logger = Logging.CreateLogger();
            logger.CreateNewOrOpenExistingLogAndWriteToLog(typeof (MessageLog), text);
            logger.Close();
        }
    }
}