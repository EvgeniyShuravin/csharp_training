using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssueTest : TestBase
    {
        [Test]
        public void AddNewIssue()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            ProjectData projectData = new ProjectData()
            {
                Id = "0",
                Name = "General"
            };
            IssueData issue = new IssueData()
            {
                Summary = "some short test",
                Description = "some long test",
                Category = "General"
            };

            applicationManager.API.CreateNewIssue(account,projectData, issue);
        }
    }
}
