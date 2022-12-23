using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ContactIsNotNull();
            SelectContact(index);
            InitContactRemove();
            driver.SwitchTo().Alert().Accept();
            applicationManager.Navigatot.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData contactData, int index)
        {
            ContactIsNotNull();
            OpenToModify(index);
            FillContactForm(contactData);
            SubmitContactModify();
            applicationManager.Navigatot.GoToHomePage();
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
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
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + index + "]/td/input")).Click();
            return this;
        }
        public ContactHelper InitContactRemove()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper OpenToModify(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + index + "]/td[8]/a/img")).Click();
            return this;
        }
        public ContactHelper SubmitContactModify()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[22]")).Click();
            return this;
        }
        public ContactHelper ContactIsNotNull()
        {
            if (!IsElementPresent(By.XPath("//table[@id='maintable']/tbody/tr[1]/td/input")))
            {
                ContactData contact = new ContactData("def", "def");
                Create(contact);
            }
            return this;
        }
    }
}

