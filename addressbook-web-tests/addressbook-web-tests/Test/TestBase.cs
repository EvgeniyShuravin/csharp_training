using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager applicationManager;

        [SetUp]
        public void SetupTest()
        {
            applicationManager = new ApplicationManager();
        }

        [TearDown]
        public void TeardownTest()
        {
            applicationManager.Stop();
        }
    }
}
