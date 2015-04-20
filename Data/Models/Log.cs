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
        public int Id { get; set; }
        public string Message { get; set; }
        public int? Severity { get; set; }
        public DateTime Created { get; set; }
        public virtual User User { get; set; }

        // Foreign KEys
        public int? UserID { get; set; }

    }
}
