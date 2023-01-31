using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : ContactTestBase
    {
        [Test]

        public void TestContactInformation()
        {
            {
                ContactData fromTable = applicationManager.Contact.GetContactInformationFromTable(0);
                ContactData fromForm = applicationManager.Contact.GetContactInformationFromEditForm(0);

                Assert.AreEqual(fromTable.Address, fromForm.Address);
                Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
                Assert.AreEqual(fromTable.AllEmail, fromForm.AllEmail);
            }
        }

        [Test]

        public void TestContactInformationDetails()
        {
            {
                ContactData fromDetails = applicationManager.Contact.GetContactInformationFromDetails(0);
                ContactData fromForm = applicationManager.Contact.GetContactInformationFromEditForm(0);

                Assert.AreEqual(fromDetails.FullInfo, fromForm.FullInfo);


            }
        }
    }
}
