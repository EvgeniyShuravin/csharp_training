using LinqToDB.Mapping;
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
    [Table(Name = "addressbook")]
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
        [Column(Name = "firstname")]
        public string FirstName { get; set; }
        [Column(Name = "lastname")]
        public string LastName { get; set; }
        [Column(Name = "middlename")]
        public string MiddleName { get; set; }
        [Column(Name = "address")]
        public string Address { get; set; }
        [Column(Name = "nickname")]
        public string NickName { get; set; }
        [Column(Name = "company")]
        public string Company { get; set; }
        [Column(Name = "title")]
        public string Title { get; set; }
        [Column(Name = "home")]
        public string Home { get; set; }
        [Column(Name = "mobile")]
        public string Mobile { get; set; }
        [Column(Name = "work")]
        public string Work { get; set; }
        [Column(Name = "fax")]
        public string Fax { get; set; }
        [Column(Name = "email")]
        public string Email { get; set; }
        [Column(Name = "email2")]
        public string Email2 { get; set; }
        [Column(Name = "email3")]
        public string Email3 { get; set; }
        [Column(Name = "homepage")]
        public string HomePage { get; set; }
        [Column(Name = "address2")]
        public string Address2 { get; set; }
        [Column(Name = "phone2")]
        public string Phone2 { get; set; }
        [Column(Name = "notes")]
        public string Notes { get; set; }
        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

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
                        HomePage = "\r\nHomepage:\r\n" + HomePage;
                    if (Address2 != null && Address2 != "")
                        Address2 = "\r\n\r\n" + Address2;
                    if (Phone2 != null && Phone2 != "")
                        Phone2 = "\r\n\r\nP: " + Phone2;
                    if (Notes != null && Notes != "")
                        Notes = "\r\n\r\n" + Notes;
                    if ((MiddleName != null && MiddleName != "") || (LastName != null && LastName != ""))
                        FirstName = Returnname(FirstName);
                    if ((LastName != null && LastName != ""))
                        MiddleName = Returnname(MiddleName);



                    return FirstName + MiddleName + LastName + "\r" +
                    ReturnFullInfo(NickName) + ReturnFullInfo(Title) + ReturnFullInfo(Company) + ReturnFullInfo(Address) + NotNullStr(Home, Mobile, Work, Fax) +
                    Home + Mobile + Work + Fax +
                    NotNullStr(Email, Email2, Email3, HomePage) + ReturnFullInfo(Email) + ReturnFullInfo(Email2) + ReturnFullInfo(Email3) + HomePage +
                    NotNullStr(Address2, Phone2, Phone2, Notes) + NullStr(Address2, Phone2, Notes) + Address2 + Phone2 + Notes;
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
                {
                    allEmail = ReturnFullInfo(Email) + ReturnFullInfo(Email2) + ReturnFullInfo(Email3);
                    if (allEmail != null && allEmail != "" && allEmail.Length > 2)
                        allEmail = allEmail.Remove(0, 2);
                    return allEmail;
                }
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
            return "\r\n" + str;
        }
        private string Returnname(string str)
        {
            if (str == null || str == "")
                return "";
            return str + " ";
        }
        private string NotNullStr(string str1, string str2, string str3, string str4)
        {
            if ((str1 != null && str1 != "") || (str2 != null && str2 != "") || (str3 != null && str3 != "") || (str4 != null && str4 != ""))
            {
                return "\r\n";
            }
            return "";
        }
        private string NullStr(string str1, string str2, string str3)
        {

            if ((str1 == null || str1 == "") && (str3 == null || str3 == "") && (str2 == null || str2 == ""))
            {
                return "";
            }
            else if ((str1 == null || str1 == "") && (str3 == null || str3 == "") && (str2 != null && str2 != ""))
                return "\r\n";
            else if ((str1 == null || str1 == "") && (str3 != null && str3 != "") && (str2 != null && str2 != ""))
            {
                return "\r\n";
            }
            else if ((str1 != null && str1 != "") && (str3 == null && str3 == "") && (str2 != null && str2 != ""))
            {
                return "\r\n";
            }

            return "";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x=> x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}
