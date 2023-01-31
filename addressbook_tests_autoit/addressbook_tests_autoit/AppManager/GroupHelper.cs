using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string GROUPDELETE = "Delete group";
        public GroupHelper(ApplicationManager manager) : base(manager)
        {

        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialog();
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialog();
        }
        internal void Rem(int index)
        {
            OpenGroupsDialog();
            SelectGroup(index.ToString());
            RemGroup();
        }

        private void RemGroup()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            aux.WinWait(GROUPDELETE);
            aux.ControlClick(GROUPDELETE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
        }

        private void SelectGroup(string index)
        {
            aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "Select", "#0|#"+ index, "");
        }

        public int GetCountGroup()
        {
            OpenGroupsDialog();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetItemCount", "#0", "");
            Convert.ToInt32(count);
            return Convert.ToInt32(count);
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupsDialog();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetText", "#0|#"+i, "");
                list.Add(new GroupData()
                {
                    Name = item
                });
            }
            CloseGroupsDialog();
            return list;
        }

        private void CloseGroupsDialog()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        private void OpenGroupsDialog()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GROUPWINTITLE);
            aux.WinWaitActive(GROUPWINTITLE);
        }

    }
}
