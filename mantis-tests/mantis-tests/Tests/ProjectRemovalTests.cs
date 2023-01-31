using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            if(applicationManager.Project.GetProjectCount() <1) 
            {
                ProjectData project = new ProjectData()
                {
                    Name = GenerateRandomString(30),
                };
                applicationManager.Project.Add(project);
            }
            int oldCount = applicationManager.Project.GetProjectCount();

            applicationManager.Project.Remove(1);
            int newCount = applicationManager.Project.GetProjectCount();
            Assert.AreEqual(oldCount - 1, newCount);

        }
    }
}
