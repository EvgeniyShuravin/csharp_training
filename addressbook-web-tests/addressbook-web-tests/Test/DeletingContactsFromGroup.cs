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
            applicationManager.Contact.CheckingForContacts();
            applicationManager.Groups.CheckingForGroups();

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

            if(selectGroup.Id == null) 
            {
                ContactData contacts = ContactData.GetAll().First();
                selectGroup = groups.First();
                applicationManager.Contact.AddContactToGroup(contacts, selectGroup);

            }

            List<ContactData> oldList = selectGroup.GetContacts();
            ContactData contact = ContactData.GetAll().Intersect(selectGroup.GetContacts()).First();  //Получение первого контакта, который входит в group

            applicationManager.Contact.DeletingContactsFromGroup(contact, selectGroup);

            List<ContactData> newList = selectGroup.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
