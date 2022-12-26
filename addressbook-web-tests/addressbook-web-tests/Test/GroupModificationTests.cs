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

            GroupData newData = new GroupData("1");
            newData.Header = null;
            newData.Footer = null;

            applicationManager.Groups.Modify(1, newData);
        }
    }
}
