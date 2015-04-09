using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Log
    {

        public Log()
        {
            User = new User();
            Created = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public int? Severity { get; set; }
        public DateTime Created { get; set; }
        public User User { get; set; }

    }
}
