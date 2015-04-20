using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        // Navigation Properties
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<ReviewEntity> ReviewEntities { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " (" + UserName + ")";
        }
    }
}
