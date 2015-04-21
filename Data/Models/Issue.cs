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
        public int Id { get; set; }
        public string IssueIdentity { get; set; }
        public string IssueProvider { get; set; }
        public string URL { get; set; }
        public DateTime? Created { get; set; }
        public virtual User ForcedByUser { get; set; }
        public virtual ReviewEntity ReviewEntity { get; set; }

        // Foreign Keys
        public int? ForcedUserID { get; set; }
        public int ReviewID { get; set; }
    }
}
