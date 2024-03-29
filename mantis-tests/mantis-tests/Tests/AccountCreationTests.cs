﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace mantis_tests
{

    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void setUpConfig()
        {
            applicationManager.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                applicationManager.Ftp.Upload("/config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "user12",
                Password = "password",
                Email = "user12@localhost.localdomain"
            };

            List<AccountData> accounts = applicationManager.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(x =>x.Name == account.Name);

            if (existingAccount != null) 
            {
                applicationManager.Admin.DeleteAccount(existingAccount);
            }

            applicationManager.James.Delete(account);
            applicationManager.James.Add(account);  
            applicationManager.Registration.RegisterAccount(account); 
        }

        [TestFixtureTearDown]
        public void restoreConfig()
        {
            applicationManager.Ftp.RestoreFileBackupFile("/config_inc.php");
        }
    }
}
