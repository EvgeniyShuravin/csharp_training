﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string firstname;
        private string lastname;
        private string middlename = "";
        private string nickname = "";
        private string title = "";
        private string company = "";
        private string address = "";
        private string home = "";
        private string mobile = "";
        private string work = "";
        private string fax = "";
        private string email = "";
        private string email2 = "";
        private string email3 = "";

        public ContactData(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }
        public string FirstName
        { get { return firstname; } set { firstname = value; } }
        public string LastName
        { get { return lastname; } set { lastname = value; } }
        public string MiddleName { get { return middlename; } set { middlename = value; } }
        public string NickName { get { return nickname; } set { nickname = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Company { get { return company; } set { company = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string Home { get { return home; } set { home = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Email2 { get { return email2; } set { email2 = value; } }
        public string Email3 { get { return email3; } set { email3 = value; } }
        public string Mobile { get { return mobile; } set { mobile = value; } }
        public string Work { get { return work; } set { work = value; } }
        public string Fax { get { return fax; } set { fax = value; } }
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
            return FirstName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return FirstName.CompareTo(other.FirstName);
        }
    }
}
