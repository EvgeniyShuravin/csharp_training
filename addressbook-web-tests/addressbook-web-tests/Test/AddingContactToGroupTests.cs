using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            applicationManager.Contact.CheckingForContacts();
            applicationManager.Groups.CheckingForGroups();

            GroupData selectGroup = new GroupData();
            ContactData selectContact = new ContactData();
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> contacts = ContactData.GetAll();

            foreach (GroupData group in groups)
            {
                try
                {
                    ContactData test = contacts.Except(group.GetContacts()).First();
                    if (test != null)
                    {
                        selectGroup = group;
                        selectContact = test;
                        break;
                    }
                }
                catch
                {
                }
            }

            if (selectGroup.Id == null || selectContact.Id == null)
            {
                GroupData groupNew = new GroupData("def");
                applicationManager.Groups.Create(groupNew);
                groups = GroupData.GetAll();
                contacts = ContactData.GetAll();

                foreach (GroupData group in groups)
                {
                    try
                    {
                        ContactData test = contacts.Except(group.GetContacts()).First();
                        if (test != null)
                        {
                            selectGroup = group;
                            selectContact = test;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }
            }

            List<ContactData> oldList = selectGroup.GetContacts();
            applicationManager.Contact.AddContactToGroup(selectContact, selectGroup);

            List<ContactData> newList = selectGroup.GetContacts();
            oldList.Add(selectContact);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }

    }
}
