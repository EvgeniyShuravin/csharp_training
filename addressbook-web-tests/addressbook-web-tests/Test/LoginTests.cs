using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            applicationManager.Auth.Logout();

            AccountData account = new AccountData("admin", "secret");
            applicationManager.Auth.Login(account);

            Assert.IsTrue(applicationManager.Auth.IsLoggedIn(account));
        }
        [Test]
        public void LoginWithInvalidCredentials()
        {
            applicationManager.Auth.Logout();

            AccountData account = new AccountData("admi", "secret");
            applicationManager.Auth.Login(account);

            Assert.IsFalse(applicationManager.Auth.IsLoggedIn(account));
        }
    }
}
