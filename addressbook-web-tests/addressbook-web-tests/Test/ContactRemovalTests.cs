using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            applicationManager.Contact.CheckingForContacts();

            List<ContactData> oldContact = ContactData.GetAll();
            ContactData toRemove = oldContact[0];

            applicationManager.Contact.Remove(toRemove);

            Assert.AreEqual(oldContact.Count - 1, applicationManager.Contact.GetContactCount());

            List<ContactData> newContact = ContactData.GetAll();

            ContactData toBeRemoved = oldContact[0];
            oldContact.RemoveAt(0);
            Assert.AreEqual(oldContact, newContact);

            foreach (ContactData contact in newContact)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
