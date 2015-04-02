using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRS2Web.Models
{
    public class ReviewEntityModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Responsible { get; set; }
        public string ResponsibleEmail { get; set; }
        public int NotificationPeriod { get; set; }
        public DateTime LastNotified { get; set; }
        public string IdentityString { get; set; }
        public string PermanentUrl { get; set; }
        public SystemModel System { get; set; }
        public List<IssueModel> Issues { get; set; }

        public string Url { get; set; }
    }
}