using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactTestBase: AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> fromUI = applicationManager.Contact.GetContactList();
                List<ContactData> fromDB = ContactData.GetAll();
                fromDB.Sort();
                fromUI.Sort();
                Assert.AreEqual(fromDB, fromUI);
            }
        }
    }
}
