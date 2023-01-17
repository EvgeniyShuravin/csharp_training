using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebAddressbookTests;
using Newtonsoft.Json;

namespace addressbook_test_data_generators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];
            string type = args[0];
            if (type == "groups")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(20),
                        Footer = TestBase.GenerateRandomString(20),
                    });
                }
                if (format == "csv")
                    writeGroupsToCsvFile(groups, writer);
                else if (format == "xml")
                    writeGroupsToHtmlFile(groups, writer);
                else if (format == "json")
                    writeGroupsToJsonFile(groups, writer);
                else
                    System.Console.Out.WriteLine("Unrecognized format " + format);
                writer.Close();
            }
            else if (type == "contacts")
            {
                List<ContactData> contact = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contact.Add(new ContactData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10))
                    {
                        Address = TestBase.GenerateRandomString(20),
                        MiddleName = TestBase.GenerateRandomString(20)
                    });
                }
                if (format == "csv")
                    writeContactsToCsvFile(contact, writer);
                else if (format == "xml")
                    writeContactsToHtmlFile(contact, writer);
                else if (format == "json")
                    writeContactsToJsonFile(contact, writer);
                else
                    System.Console.Out.WriteLine("Unrecognized format " + format);
                writer.Close();
            }
            else
            {
                System.Console.Out.WriteLine("Unrecognized type " + type);
            }
        }
        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}", group.Name, group.Header, group.Footer));
            }
        }
        static void writeGroupsToHtmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }
        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
        }
        static void writeContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1},${2}", contact.FirstName, contact.MiddleName, contact.NickName));
            }
        }
        static void writeContactsToHtmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }
        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Formatting.Indented));
        }
    }
}
