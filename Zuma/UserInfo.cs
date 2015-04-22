using System;

namespace Zuma
{
    public class UserInfo
    {
        public DateTime Start { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }

        public bool HasAcmeEmail
        {
            get { return Email.EndsWith("acme.com") || Email.EndsWith("acme.org"); }

        }

        public bool IsPremiumUser
        {
            get
            {
                return Start > new DateTime(2011, 5, 10) && Country.Code == "USA";
            }
        }
    }
}