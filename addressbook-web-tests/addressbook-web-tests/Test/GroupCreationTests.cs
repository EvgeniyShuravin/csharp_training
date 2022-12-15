using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            applicationManager.Navigatot.GoToHomePage();
            applicationManager.Auth.Login(new AccountData("admin", "secret"));
            applicationManager.Navigatot.GoToGroupsPage();
            applicationManager.Groups.InitGroupCreation();
            GroupData group = new GroupData("dsa");
            group.Header = "dsad";
            // group.Footer = "cxz";
            applicationManager.Groups.FillGroupForm(group);
            applicationManager.Groups.SubmitGroupCreation();
            applicationManager.Groups.ReturnToGroupsPage();
        }
    }
}
