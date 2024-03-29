﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpaqueMail.Net;


namespace mantis_tests
{
    public class MailHelper : HelperBase
    {
        public MailHelper(ApplicationManager manager) : base(manager) { }

        public String GetLastMail(AccountData account) 
        {

            for (int i = 0; i < 20 ; i++) 
            {
                Pop3Client pop3Client = new Pop3Client("localhost", 110, account.Name, account.Password, false);
                pop3Client.Connect();
                pop3Client.Authenticate();

                if (pop3Client.GetMessageCount() > 0)
                {
                    ReadOnlyMailMessage message = pop3Client.GetMessage(1);
                    string body = message.Body;
                    pop3Client.DeleteMessage(1);
                    pop3Client.LogOut();
                    return body;
                }
                else
                    System.Threading.Thread.Sleep(3000);     
            }
            return null; 
        }
    }
}
