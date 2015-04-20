using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Models;

namespace Api.Models
{
    public class IssueModel
    {
        public int Id { get; set; }
        public ReviewEntityBasicModel ReviewEntity { get; set; }
        public string IssueIdentity { get; set; }
        public string IssueUrl { get; set; }
        public string Url { get; set; }
    }
}