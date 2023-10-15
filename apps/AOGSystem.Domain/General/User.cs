using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.General
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAT { get; set; }
        public DateTime? UpdatedAT { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();


        public User(string firstName, string lastName, string userName, string phoneNumber, string email)
        {
            FirstName = firstName; 
            LastName = lastName;
            UserName = userName;
            PhoneNumber = phoneNumber;
            Email = email;
        }


        
    }
}
