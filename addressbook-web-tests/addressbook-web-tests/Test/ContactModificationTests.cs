﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests :TestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            ContactData contact = new ContactData("New", "New");
            contact.MiddleName = "NewNew";
            contact.Home = "132";
            contact.Email = "tetstNew@gmail.com";
            contact.Address = "VN";
            applicationManager.Contact.Modify(contact,3);
        }
    }
}