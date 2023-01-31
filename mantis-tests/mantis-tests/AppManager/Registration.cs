using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Security.Principal;

namespace mantis_tests
{
    public class Registration : HelperBase
    {
        public Registration(ApplicationManager manager) : base(manager)
        {

        }

        public void RegisterAccount(AccountData account)
        {
            OpenMainPage();
            OpenRegForm();
            FillRegForm(account);
            SubmitReg();
            String url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        private string GetConfirmationUrl(AccountData account)
        {
            String message = applicationManager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Id("realname")).SendKeys(account.Name);
            driver.FindElement(By.Id("password")).SendKeys(account.Password);
            driver.FindElement(By.Id("password-confirm")).SendKeys(account.Password);
        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.XPath("//button/span")).Click();
        }

        private void OpenRegForm()
        {
            driver.FindElement(By.XPath("//a[contains(@href, 'signup_page.php')]")).Click();
        }

        private void SubmitReg()
        {
            driver.FindElement(By.XPath("//input[2]")).Click();
        }

        private void FillRegForm(AccountData account)
        {
            driver.FindElement(By.Id("username")).SendKeys(account.Name);
            driver.FindElement(By.Id("email-field")).SendKeys(account.Email);
        }

        private void OpenMainPage()
        {
            applicationManager.Driver.Url = "http://localhost/mantisbt-2.25.4/login_page.php";
        }
    }
}
