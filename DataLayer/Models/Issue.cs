using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS2Data.Models
{
    public class Issue
    {
        public Issue()
        {
            ReviewEntity = new ReviewEntity();
        }

        [Key]
        public int IssueID { get; set; }
        public string IssueIdentity { get; set; }
        public string IssueProvider { get; set; }
        public string URL { get; set; }

        public ReviewEntity ReviewEntity { get; set; }
    }
}
