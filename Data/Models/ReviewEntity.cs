using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ReviewEntity
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Responsible { get; set; }
        public string ResponsibleEmail { get; set; }
        public int NotificationPeriod { get; set; }
        public DateTime? LastNotified { get; set; }
        public string IdentityString { get; set; }
        public string URL { get; set; }

        public virtual User User { get; set; }
        public virtual DRSSystem System { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
