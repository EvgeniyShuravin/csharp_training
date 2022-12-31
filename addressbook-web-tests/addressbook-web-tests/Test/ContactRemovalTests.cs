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

            List<ContactData> oldContact = applicationManager.Contact.GetContactList();

            applicationManager.Contact.Remove(0);

            Assert.AreEqual(oldContact.Count - 1, applicationManager.Contact.GetContactCount());

            List<ContactData> newContact = applicationManager.Contact.GetContactList();

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
