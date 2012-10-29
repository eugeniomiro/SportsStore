using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public String   MailToAddress = "orders@example.com";
        public String   MailFromAddress = "sportsstore@example.com";
        public bool     UseSsl = true;
        public string   Username = "MySmtpUsername";
        public string   Password = "MySmtpPassword";
        public string   ServerName = "smtp.example.com";
        public int      ServerPort = 587;
        public bool     WriteAsFile = false;
        public string   FileLocation = @"c:\sports_store_emails";
    }
}
