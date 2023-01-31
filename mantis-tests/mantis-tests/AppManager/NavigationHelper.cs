using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        public string baseURL;
        public NavigationHelper(ApplicationManager applicationManager, string baseURL) : base(applicationManager)
        {
            this.baseURL = baseURL;
        }
        public void GoToHomePage()
        {
            if (driver.Url == baseURL + "/my_view_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/my_view_page.php/");
        }

        public void GoProjectManagePage()
        {
            if (driver.Url == baseURL + "/manage_proj_page.php")
            {
                return;
            }
            driver.FindElement(By.CssSelector("i.fa.fa-gears.menu-icon")).Click();
            driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/ul/li[3]/a")).Click();
        }
    }
}
