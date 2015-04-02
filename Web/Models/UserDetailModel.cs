using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRS2Web.Models
{
    public class UserDetailModel : UserBaseModel
    {
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public List<LogModel> Logs { get; set; }
        public List<ReviewEntityModel> ReviewEntities { get; set; }

    }
}