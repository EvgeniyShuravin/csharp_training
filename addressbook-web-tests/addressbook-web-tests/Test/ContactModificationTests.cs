using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            applicationManager.Contact.CheckingForContacts();

            List<ContactData> oldGroups = ContactData.GetAll();
            ContactData toModif = oldGroups[0];

            ContactData newData = new ContactData("New", "New");
            newData.MiddleName = "NewNew";
            newData.Home = "132";
            newData.Email = "tetstNew@gmail.com";
            newData.Address = "VN";

            List<ContactData> oldContact = ContactData.GetAll();
            ContactData oldData = oldContact[0];

            applicationManager.Contact.Modify(newData, toModif);

            Assert.AreEqual(oldContact.Count, applicationManager.Contact.GetContactCount());

            List<ContactData> newContact = ContactData.GetAll();
            oldContact[0].FirstName = newData.FirstName;
            oldContact[0].LastName = newData.LastName;
            oldContact.Sort();
            newContact.Sort();
            Assert.AreEqual(oldContact, newContact);

            foreach (ContactData contact in newContact)
            {
                if (newData.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.FirstName, contact.FirstName);
                }
            }
        }
    }
}