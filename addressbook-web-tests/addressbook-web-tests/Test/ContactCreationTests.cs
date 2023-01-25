using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using System.Xml.Serialization;
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

        public static IEnumerable<ContactData> ContactDataDataFromCsvFile()
        {
            List<ContactData> groups = new List<ContactData>();

            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new ContactData(parts[0], parts[1])
                {
                    Address = parts[3],
                    MiddleName = parts[2]
                });
            }
            return groups;
        }
        public static IEnumerable<ContactData> ContactDataDataFromXmlFile()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }
        public static IEnumerable<ContactData> ContactDataDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataDataFromXmlFile")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContact = ContactData.GetAll();

            applicationManager.Contact.Create(contact);

            Assert.AreEqual(oldContact.Count + 1, applicationManager.Contact.GetContactCount());

            List<ContactData> newContact = ContactData.GetAll();
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

            List<ContactData> oldContact = ContactData.GetAll();

            applicationManager.Contact.Create(contact);

            Assert.AreEqual(oldContact.Count + 1, applicationManager.Contact.GetContactCount());

            List<ContactData> newContact = ContactData.GetAll();
            oldContact.Add(contact);
            oldContact.Sort();
            newContact.Sort();
            Assert.AreEqual(oldContact, newContact);
        }
    }
}
