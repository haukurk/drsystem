using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRS2Web.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int? Severity { get; set; }
        public string User { get; set; }

        public string Url { get; set; }
    }
}