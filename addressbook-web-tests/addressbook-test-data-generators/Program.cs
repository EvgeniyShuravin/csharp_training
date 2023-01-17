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
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[2];

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
            else if (format=="json")
                writeGroupsToJsonFile(groups, writer);
            else
                System.Console.Out.WriteLine("Unrecognized format " + format);
            writer.Close();

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
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer,groups);
        }
        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups,Formatting.Indented));
        }
    }
}
