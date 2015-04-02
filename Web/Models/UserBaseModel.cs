using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRS2Web.Models
{
    public class UserBaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public string Url { get; set; }
    }
}