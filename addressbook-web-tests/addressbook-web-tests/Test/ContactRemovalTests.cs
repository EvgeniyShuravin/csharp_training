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

            List<ContactData> newContact = applicationManager.Contact.GetContactList();
            oldContact.RemoveAt(0);
            Assert.AreEqual(oldContact, newContact);
        }
    }
}
