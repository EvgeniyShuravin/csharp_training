using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public Registration Registration { get; set; }
        public FtpHelrep Ftp { get; set; }
        public JamesHelper James { get; set; }
        public MailHelper Mail { get; set; }
        public LoginHelper Login { get; set; }
        public ProjectHelper Project { get; set; }
        public NavigationHelper Navigate { get; set; }
        public AdminHelper Admin { get; set; }
        public APIHelper API { get; set; }

        private static ThreadLocal<ApplicationManager> applicationManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-2.25.4";
            Registration = new Registration(this);
            Ftp = new FtpHelrep(this);
            James =  new JamesHelper(this);
            Mail = new MailHelper(this);
            Login = new LoginHelper(this);
            Project = new ProjectHelper(this);
            Navigate = new NavigationHelper(this,baseURL);
            Admin = new AdminHelper(this,baseURL);
            API = new APIHelper(this);
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
                newInstanse.driver.Url = "http://localhost/mantisbt-2.25.4/login_page.php";
                applicationManager.Value = newInstanse;
            }
            return applicationManager.Value;
        }

        public IWebDriver Driver { get { return driver; } }
    }
}
