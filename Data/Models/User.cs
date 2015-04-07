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
        public User()
        {
            Logs = new List<Log>();
            ReviewEntities = new List<ReviewEntity>();
        }

        [Key]
        public int UserId { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public ICollection<Log> Logs { get; set; }
        public ICollection<ReviewEntity> ReviewEntities { get; set; }

    }
}
