using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace mantis_tests
{

    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
      [Test]
        public void CreateProjectTest()
        {
            ProjectData project = new ProjectData()
            {
                Name = GenerateRandomString(30),
            };

            int oldCount = applicationManager.Project.GetProjectCount();
            applicationManager.Project.Add(project);
            int newCount = applicationManager.Project.GetProjectCount();
            Assert.AreEqual(oldCount + 1, newCount);
        }
    }
}
