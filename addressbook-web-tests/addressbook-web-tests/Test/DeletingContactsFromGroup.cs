using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class DeletingContactsFromGroup: AuthTestBase
    {
        [Test]
        public void TestDeletingContactsFromGroup()
        {
            GroupData selectGroup = new GroupData();
            List<GroupData> groups = GroupData.GetAll();
            foreach(GroupData group in groups)
            {
                if (group.GetContacts().Count()>0)
                {
                    selectGroup = group;
                    break;
                }
            }
            List<ContactData> oldList = selectGroup.GetContacts();
            ContactData contact = ContactData.GetAll().Intersect(selectGroup.GetContacts()).First();  //Получение первого контакта, который входит в group

            applicationManager.Contact.DeletingContactsFromGroup(contact, selectGroup);

            List<ContactData> newList = selectGroup.GetContacts();
            oldList.RemoveAt(0);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
