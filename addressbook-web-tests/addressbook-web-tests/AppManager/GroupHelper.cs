using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager applicationManager) : base(applicationManager) { }

        public GroupHelper Create(GroupData group)
        {
            applicationManager.Navigatot.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper Remove(int id)
        {
            applicationManager.Navigatot.GoToGroupsPage();
            SelectGroup(id);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper Remove(GroupData group)
        {
            applicationManager.Navigatot.GoToGroupsPage();
            SelectGroup(group.Id);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper Modify(int id, GroupData groupData)
        {
            applicationManager.Navigatot.GoToGroupsPage();
            SelectGroup(id);
            InitGroupModify();
            FillGroupForm(groupData);
            SubmitGroupModify();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper Modify(GroupData toBeRemoved, GroupData groupData)
        {
            applicationManager.Navigatot.GoToGroupsPage();
            SelectGroup(toBeRemoved.Id);
            InitGroupModify();
            FillGroupForm(groupData);
            SubmitGroupModify();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/span[" + (index + 1) + "]/input")).Click();
            return this;
        }

        public GroupHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+id+"'])")).Click();
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
        public GroupHelper CheckingForGroups()
        {
            applicationManager.Navigatot.GoToGroupsPage();
            if (!IsElementPresent(By.Name("selected[]")))
            {
                GroupData group = new GroupData("def");
                Create(group);
            }
            return this;
        }
        public GroupHelper InitGroupModify()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
        public GroupHelper SubmitGroupModify()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }

        private List<GroupData> groupCache = null;

        public int GetGroupCount()
        {
            applicationManager.Navigatot.GoToGroupsPage();
            return driver.FindElements(By.CssSelector("span.group")).Count();
        }

        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                applicationManager.Navigatot.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<GroupData>(groupCache);
        }
    }
}
