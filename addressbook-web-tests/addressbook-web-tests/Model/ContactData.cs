using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string fullName;
        private string fullInfo;
        private string allEmail;
        public ContactData(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
        public ContactData()
        {
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string NickName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }


        public string Home { get; set; }
        public string Mobile { get; set; }
        public string Work { get; set; }
        public string Fax { get; set; }

        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string HomePage { get; set; }

        public string Address2 { get; set; }
        public string Phone2 { get; set; }
        public string Notes { get; set; }


        public string Id { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                    return allPhones;
                else
                    return (CleanUp(Home) + CleanUp(Mobile) + CleanUp(Work) + CleanUp(Phone2)).Trim();
            }
            set
            {
                allPhones = value;
            }
        }
        public string FullName
        {
            get
            {
                if (fullName != null)
                    return fullName;
                else
                    return FirstName + MiddleName + LastName.Remove(LastName.Length - 1) + "\r";
            }
            set { fullName = value; }
        }

        public string FullInfo
        {
            get
            {
                if (fullInfo != null)
                    return fullInfo;
                else
                {
                    if (Home != null && Home != "")
                        Home = "\r\nH: " + Home;
                    if (Mobile != null && Mobile != "")
                        Mobile = "\r\nM: " + Mobile;
                    if (Work != null && Work != "")
                        Work = "\r\nW: " + Work;
                    if (Fax != null && Fax != "")
                        Fax = "\r\nF: " + Fax;
                    if (HomePage != null && HomePage != "")
                        HomePage = "Homepage:\r\n" + HomePage;
                    if (Address2 != null && Address2 != "")
                        Address2 = "\r\n\r\n\r\n" + Address2;
                    if (Phone2 != null && Phone2 != "")
                        Phone2 = "\r\n\r\nP: " + Phone2;
                    if (Notes != null && Notes != "")
                        Notes = "\r\n\r\n" + Notes;
                    return FirstName + MiddleName + LastName.Remove(LastName.Length - 1) +
                        NotNullStr(NickName, Title, Company, Address) + ReturnFullInfo(NickName) + ReturnFullInfo(Title) + ReturnFullInfo(Company) + ReturnFullInfo(Address) +
                        Home + Mobile + Work + Fax +
                        NotNullStr(Email, Email2, Email3, HomePage) + NotNullStr(Email, Email2, Email3, HomePage) + ReturnFullInfo(Email) + ReturnFullInfo(Email2) + ReturnFullInfo(Email3) + HomePage +
                        Address2 + Phone2 + Notes;
                }
            }
            set { fullInfo = value; }
        }
        public string AllEmail
        {
            get
            {
                if (allEmail != null)
                    return allEmail;
                else
                    return ReturnFullInfo(Email) + ReturnFullInfo(Email2) + ReturnFullInfo(Email3);
            }
            set
            {
                allEmail = value;
            }
        }
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(this, other)) return true;
            if (Object.ReferenceEquals(other, null)) return false;

            return FirstName == other.FirstName;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode();
        }

        public override string ToString()
        {
            return "FirstName=" + FirstName + "\nlastName=" + LastName + "\naddress=" + Address + "\ncompany" + Company;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return FirstName.CompareTo(other.FirstName);
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
            //return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        private string ReturnFullInfo(string str)
        {
            if (str == null || str == "")
                return "";
            return str + "\r\n";
        }

        private string NotNullStr(string str1, string str2, string str3, string str4)
        {
            if ((str1 != null && str1 != "") || (str2 != null && str2 != "") || (str3 != null && str3 != "") || (str4 != null && str4 != ""))
            {
                return "\r\n";
            }
            return "";
        }
    }
}
