//using System;
//using System.Net;
//using System.Net.Mail;

//namespace Xplorium.Core.Utilities.Loggers
//{
//    public class EmailLogger : Logger
//    {
//        public string MailServer { get; set; }
//        public string From { get; set; }
//        public string To { get; set; }

//        public override void LogError(string error)
//        {
//            MailMessage message = new MailMessage(From, To, "Unhandled exception report", error);
//            SmtpClient client = new SmtpClient(MailServer);
//            client.Credentials = CredentialCache.DefaultNetworkCredentials;
//            client.Send(message);
//        }
//    }
//}

