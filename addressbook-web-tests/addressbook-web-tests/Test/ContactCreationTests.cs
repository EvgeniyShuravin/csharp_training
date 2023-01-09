using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contact = new List<ContactData>();

            for (int i = 0; i < 5; i++)
            {
                contact.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Address = GenerateRandomString(30),
                    Company = GenerateRandomString(40),
                    MiddleName = GenerateRandomString(100)
                });
            }
            return contact;
        }
        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContact = applicationManager.Contact.GetContactList();

            applicationManager.Contact.Create(contact);

            Assert.AreEqual(oldContact.Count + 1, applicationManager.Contact.GetContactCount());

            List<ContactData> newContact = applicationManager.Contact.GetContactList();
            oldContact.Add(contact);
            oldContact.Sort();
            newContact.Sort();
            Assert.AreEqual(oldContact, newContact);
        }
        [Test]
        public void BadNameContactCreationTest()
        {
            ContactData contact = new ContactData("a'a", "Sh");
            contact.MiddleName = "Ser";
            contact.Home = "dsa";
            contact.Email = "tetst@gmail.com";
            contact.Address = "SPB";

            List<ContactData> oldContact = applicationManager.Contact.GetContactList();

            applicationManager.Contact.Create(contact);

            Assert.AreEqual(oldContact.Count + 1, applicationManager.Contact.GetContactCount());

            List<ContactData> newContact = applicationManager.Contact.GetContactList();
            oldContact.Add(contact);
            oldContact.Sort();
            newContact.Sort();
            Assert.AreEqual(oldContact, newContact);
        }
    }
}
