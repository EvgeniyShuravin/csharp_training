using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
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

        public ContactHelper Modify(ContactData contactData, int index)
        {
            OpenToModify(index);
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
        public ContactHelper InitContactRemove()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper OpenToModify(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + index + 2 + "]/td[8]/a/img")).Click();
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
            if (!IsElementPresent(By.XPath("//table[@id='maintable']/tbody/tr[2]/td[8]/a/img")))
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

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                MiddleName = middlename,
                Address = address,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Email = email, 
                Email2= email2,
                Email3= email3
            };
        }
        public ContactData GetContactInformationFromDetails(int index)
        {
            applicationManager.Navigatot.GoToHomePage();
            OpenDetails(index);

            IList<IWebElement> cells = driver.FindElements(By.Id("content"));

            string[] str = cells[0].Text.Split('\n');

            string lastName = str[0].Split()[2];
            string midleName = str[0].Split()[1];   
            string firstName = str[0].Split()[0];
            string fullName = str[0];
            string home = "";
            string mobile = "";
            string work = "";
            foreach (string str2 in str)
            {
                if (Regex.IsMatch(str2, "\\bH: \\b"))
                    home = str2.Split()[1];
                if (Regex.IsMatch(str2, "\\bW: \\b"))
                    work = str2.Split()[1];
                if (Regex.IsMatch(str2, "\\bM: \\b"))
                    mobile = str2.Split()[1];
            }

            return new ContactData(firstName, lastName)
            {
                MiddleName = midleName, 
                FullName = fullName,
                Home = home,
                Mobile = mobile,
                Work = work
            };
        }
    }
}

