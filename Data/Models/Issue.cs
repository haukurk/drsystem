using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }
        public string IssueIdentity { get; set; }
        public string IssueProvider { get; set; }
        public string URL { get; set; }
        public virtual User ForcedByUser { get; set; }
        public virtual ReviewEntity ReviewEntity { get; set; }
    }
}
