using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            applicationManager.Groups.CheckingForGroups();
            List<GroupData> oldGroups = applicationManager.Groups.GetGroupList();

            GroupData newData = new GroupData("1");
            newData.Header = null;
            newData.Footer = null;

            applicationManager.Groups.Modify(0, newData);

            List<GroupData> newGroups = applicationManager.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();   
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
