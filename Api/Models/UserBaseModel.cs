using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class UserBaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get { return String.Format("{0} {1}", this.FirstName, this.LastName); } }
        public DateTime? RegistrationDate { get; set; }

        public string Url { get; set; }
    }
}