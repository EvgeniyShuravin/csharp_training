﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            applicationManager.Contact.CheckingForContacts();

            ContactData contact = new ContactData("New", "New");
            contact.MiddleName = "NewNew";
            contact.Home = "132";
            contact.Email = "tetstNew@gmail.com";
            contact.Address = "VN";

            List<ContactData> oldContact = applicationManager.Contact.GetContactList();

            applicationManager.Contact.Modify(contact,0);

            List<ContactData> newContact = applicationManager.Contact.GetContactList();
            oldContact[0].FirstName= contact.FirstName;
            oldContact[0].LastName = contact.LastName;
            oldContact.Sort();
            newContact.Sort();
            Assert.AreEqual(oldContact, newContact);
        }
/*
        [Test]
        public void BadNameContactModifyTest()
        {
            applicationManager.Contact.CheckingForContacts();
            ContactData contact = new ContactData("a'a", "New");
            contact.MiddleName = "NewNew";
            contact.Home = "132";
            contact.Email = "tetstNew@gmail.com";
            contact.Address = "VN";
            List<ContactData> oldContact = applicationManager.Contact.GetContactList();
            applicationManager.Contact.Modify(contact, 0);
            List<ContactData> newContact = applicationManager.Contact.GetContactList();
            oldContact[0].FirstName = contact.FirstName;
            oldContact[0].LastName = contact.LastName;
            oldContact.Sort();
            newContact.Sort();
            Assert.AreEqual(oldContact, newContact);
        }
*/

    }
}