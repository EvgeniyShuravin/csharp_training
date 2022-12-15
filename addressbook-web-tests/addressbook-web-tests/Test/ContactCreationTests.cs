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
            applicationManager.Navigatot.GoToHomePage();
            applicationManager.Auth.Login(new AccountData("admin", "secret"));
            applicationManager.Contact.InitContactCreation();
            ContactData contact = new ContactData("Evg", "Sh");
            contact.MiddleName = "Ser";
            contact.Home = "dsa";
            contact.Email = "tetst@gmail.com";
            contact.Address = "SPB";
            applicationManager.Contact.FillContactForm(contact);
            applicationManager.Contact.SubmitContactCreation();
            applicationManager.Navigatot.ReturnToHomePage();
        }
    }
}
