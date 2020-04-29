using System;
using WebApplication.Validation;

namespace WebApplication.Contracts
{  
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
    }
}
