using MinimalisticTelnet;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }

        public void Add(ProjectData project)
        {
            OpenFormCreate();
            FillFormCreate(project);
            SubmitCreateForm();
        }
        public void Remove(int index)
        {
            applicationManager.Navigate.GoProjectManagePage();
            OpenToModify(index);
            DeletingProject();
        }

        private void DeletingProject()
        {
            driver.FindElement(By.XPath("//input[3]")).Click();
            driver.FindElement(By.XPath("//input[4]")).Click();
        }

        private void OpenToModify(int index)
        {
            if (index == 1)
                driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr/td/a")).Click();
            else
                driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr["+ index +"]/td/a")).Click();
        }

        private void SubmitCreateForm()
        {
            driver.FindElement(By.XPath("//div[3]/input")).Click();
        }

        private void FillFormCreate(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
        }

        private void OpenFormCreate()
        {
            applicationManager.Navigate.GoProjectManagePage();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }
        public int GetProjectCount()
        {
            applicationManager.Navigate.GoProjectManagePage();
            return driver.FindElements(By.CssSelector("i[class*='fa fa-check fa-lg']")).Count();
        }
    }
}
