using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigationHelper;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;

        private static ThreadLocal<ApplicationManager> applicationManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost";

            loginHelper = new LoginHelper(this);
            navigationHelper = new NavigationHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
        public static ApplicationManager GetInstance()
        {
            if (! applicationManager.IsValueCreated)
            {
                ApplicationManager newInstanse = new ApplicationManager(); 
                newInstanse.Navigatot.GoToHomePage();   
                applicationManager.Value = newInstanse;
            }
            return applicationManager.Value;
        }

        public IWebDriver Driver { get { return driver; } }

        public LoginHelper Auth
        { get { return loginHelper; } }
        public NavigationHelper Navigatot
        { get { return navigationHelper; } }
        public GroupHelper Groups
        { get { return groupHelper; } }
        public ContactHelper Contact
        { get { return contactHelper; } }
    }
}
