using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;
        public NavigationHelper(ApplicationManager applicationManager, string baseURL) : base(applicationManager)
        {
            this.baseURL = baseURL;
        }
        public void GoToHomePage()
        {
            if(driver.Url == baseURL + "/addressbook")
            {
                return;
            }
                driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }

        public void GoToGroupsPage()
        {
            if (driver.Url == baseURL + "/addressbook/group.php" && IsElementPresent(By.Name("new")))
            {
                return;
            }
            driver.FindElement(By.LinkText("groups")).Click();
        }
    }
}
