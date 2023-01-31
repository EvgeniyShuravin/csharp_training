using MySql.Data.MySqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager applicationManager) : base(applicationManager) { }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            applicationManager.Navigatot.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            SelectContact(index);
            InitContactRemove();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            applicationManager.Navigatot.GoToHomePage();
            return this;
        }
        public ContactHelper Remove(ContactData contactData)
        {
            SelectContact(contactData.Id);
            InitContactRemove();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            applicationManager.Navigatot.GoToHomePage();
            return this;
        }
        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            applicationManager.Navigatot.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Id);
            CommitAddingContactToGroup();
            new WebDriverWait(driver,TimeSpan.FromSeconds(10)).Until(driver=>driver.FindElements(By.CssSelector("div.msgbox")).Count > 0); 
            return this;
        }
        public ContactHelper DeletingContactsFromGroup(ContactData contact, GroupData group)
        {
            applicationManager.Navigatot.GoToHomePage();
            SelectGroupFilter(group.Id);
            SelectContact(contact.Id);
            //SelectGroupToAdd(group.Id);
            CommitDeletingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(driver => driver.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public ContactHelper CommitDeletingContactToGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
            return this;
        }

        public ContactHelper SelectGroupFilter(string id)
        {
            driver.FindElement(By.Name("group")).Click();
            driver.FindElement(By.XPath("//option[@value='"+ id +"']")).Click();
            return this;
        }

        public ContactHelper Modify(ContactData contactData, int index)
        {
            OpenToModify(index);
            FillContactForm(contactData);
            SubmitContactModify();
            applicationManager.Navigatot.GoToHomePage();
            return this;
        }
        public ContactHelper Modify(ContactData contactData, ContactData contact)
        {
            OpenToModify(contact.Id);
            FillContactForm(contactData);
            SubmitContactModify();
            applicationManager.Navigatot.GoToHomePage();
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.NickName);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            return this;
        }
        public ContactHelper CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
            return this;
        }

        public ContactHelper SelectGroupToAdd(string id)
        {
            driver.FindElement(By.CssSelector("select[name=\"to_group\"]")).FindElement(By.XPath("option[@value='" + id + "']")).Click();
            return this;
        }

        public ContactHelper ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
            return this;
        }
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 2) + "]/td/input")).Click();
            return this;
        }
        public ContactHelper SelectContact(object id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
            return this;
        }
        public ContactHelper InitContactRemove()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper OpenToModify(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 2) + "]/td[8]/a/img")).Click();
            return this;
        }
        public ContactHelper OpenToModify(string id)
        {

            applicationManager.Navigatot.GoToUrlCast("/edit.php?id="+id);
            return this;
        }
        public ContactHelper SubmitContactModify()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[22]")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper CheckingForContacts()
        {
            applicationManager.Navigatot.GoToHomePage();
            if (!IsElementPresent(By.XPath("//tr[@name='entry']")))
            {
                ContactData contact = new ContactData("def", "def");
                Create(contact);
            }
            return this;
        }

        public ContactHelper OpenDetails(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (2 + index) + "]/td[7]/a/img")).Click();
            return this;
        }

        private List<ContactData> contactCache = null;
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                applicationManager.Navigatot.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name*='entry']"));
                foreach (IWebElement element in elements)
                {
                    contactCache.Add(new ContactData(element.Text.Split()[1], element.Text.Split()[0])
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    }); ;
                }
            }
            return new List<ContactData>(contactCache);
        }
        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name*='entry']")).Count();
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            applicationManager.Navigatot.GoToHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmail = cells[4].Text;
            string allPhone = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhone,
                AllEmail = allEmail
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            applicationManager.Navigatot.GoToHomePage();
            OpenToModify(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string address2 = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                MiddleName = middlename,
                Address = address,
                Home = homePhone,
                NickName = nickname,
                Company = company,
                Title = title,
                Mobile = mobilePhone,
                Work = workPhone,
                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePage = homepage,
                Address2 = address2,
                Phone2 = phone2,
                Notes = notes
            };
        }
        public ContactData GetContactInformationFromDetails(int index)
        {
            applicationManager.Navigatot.GoToHomePage();
            OpenDetails(index);

            IList<IWebElement> cells = driver.FindElements(By.Id("content"));

            string[] str = cells[0].Text.Split('\n');
            string fullInfo = null;
            string home = null;
            string mobile = null;
            string mobile2 = null;
            string work = null;
            for (int i = 0; i < str.Length; i++)
            {
                if (Regex.IsMatch(str[i], "\\bH: \\b"))
                    home = str[i].Split()[1];
                if (Regex.IsMatch(str[i], "\\bW: \\b"))
                    work = str[i].Split()[1];
                if (Regex.IsMatch(str[i], "\\bM: \\b"))
                    mobile = str[i].Split()[1];
                if (Regex.IsMatch(str[i], "\\bP: \\b"))
                    mobile2 = str[i].Split()[1];
                if (i == 0)
                {
                    fullInfo += str[i]+ "\r";
                }
                else
                    fullInfo += "\n" + str[i];
            }

            return new ContactData()
            {
                FullInfo = fullInfo,
                Home = home,
                Mobile = mobile,
                Phone2 = mobile2,
                Work = work
            };
        }
    }
}

