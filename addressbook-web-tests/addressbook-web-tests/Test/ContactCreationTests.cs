using System;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Evg", "Sh");
            contact.MiddleName = "Ser";
            contact.Home = "dsa";
            contact.Email = "tetst@gmail.com";
            contact.Address = "SPB";
            applicationManager.Contact.Create(contact);
        }
    }
}
