﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests 
{
    public class GroupTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<GroupData> fromUI = applicationManager.Groups.GetGroupList();
                List<GroupData> fromDB = GroupData.GetAll();
                fromDB.Sort();
                fromUI.Sort();
                Assert.AreEqual(fromDB, fromUI);
            }
        }
    }
}
