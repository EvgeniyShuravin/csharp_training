using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]

        public void TestContactInformation()
        {
            {
                ContactData fromTable = applicationManager.Contact.GetContactInformationFromTable(0);
                ContactData fromForm = applicationManager.Contact.GetContactInformationFromEditForm(0);

                Assert.AreEqual(fromTable, fromForm);
                Assert.AreEqual(fromTable.Address, fromForm.Address);
                Assert.AreEqual(fromForm.AllPhones, fromForm.AllPhones);
            }
        }

        [Test]

        public void TestContactInformationDetails()
        {
            {
                ContactData fromDetails = applicationManager.Contact.GetContactInformationFromDetails(0);
                ContactData fromForm = applicationManager.Contact.GetContactInformationFromEditForm(0);

                Assert.AreEqual(fromDetails.FullName, fromForm.FullName);
                Assert.AreEqual(fromDetails.AllPhones, fromForm.AllPhones);
            }
        }
    }
}
