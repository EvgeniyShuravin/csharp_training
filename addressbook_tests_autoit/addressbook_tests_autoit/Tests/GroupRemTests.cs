﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemTests : TestBase
    {
        [Test]
        public void GroupRemTest()
        {
            if (app.Groups.GetCountGroup() < 2) 
            {
                GroupData group = new GroupData()
                {
                    Name = "def"
                };
                app.Groups.Add(group);
            }
            int old = app.Groups.GetCountGroup();
            app.Groups.Rem(1);

            int newGroups = app.Groups.GetCountGroup();
            Assert.AreEqual(old - 1, newGroups);
        }
    }
}
