using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            applicationManager.Groups.CheckingForGroups();

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toModif = oldGroups[0];

            GroupData newData = new GroupData("1");
            newData.Header = null;
            newData.Footer = null;

            applicationManager.Groups.Modify(toModif, newData);

            Assert.AreEqual(oldGroups.Count, applicationManager.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toModif.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
