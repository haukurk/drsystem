using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRS2Data.Models;

namespace DRS2Web.Models
{
    public class IssueModel
    {
        public int Id { get; set; }
        public ReviewEntityModel ReviewEntity { get; set; }
        public string IssueIdentity { get; set; }
        public string IssueUrl { get; set; }

        public string Url { get; set; }
    }
}