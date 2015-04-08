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
        public ReviewEntity()
        {
            System = new DRSSystem();
            Issues = new List<Issue>();
            User = new User();
        }

        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Responsible { get; set; }
        public string ResponsibleEmail { get; set; }
        public int NotificationPeriod { get; set; }
        public DateTime? LastNotified { get; set; }
        public string IdentityString { get; set; }
        public string URL { get; set; }

        public User User { get; set; }
        public DRSSystem System { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}
