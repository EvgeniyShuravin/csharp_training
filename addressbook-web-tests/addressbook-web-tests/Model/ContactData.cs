using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string fullName;
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
        public string NickName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Home { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Mobile { get; set; }
        public string Work { get; set; }
        public string Fax { get; set; }
        public string Id { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                    return allPhones;
                else
                    return (CleanUp(Home) + CleanUp(Mobile) + CleanUp(Work)).Trim();
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
                    return FirstName  + MiddleName + LastName.Remove(LastName.Length-1)+"\r";
            }
            set { fullName = value; }
        }
        public string AllEmail
        {
            get
            {
                if (allEmail != null)
                    return allEmail;
                else
                    return Email + "\r\n" + Email2 + "\r\n" + Email3;
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
    }
}
