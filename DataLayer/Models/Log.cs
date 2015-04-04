using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS2Data.Models
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }
        public string Message { get; set; }
        public int? Severity { get; set; }
        public User User { get; set; }

    }
}
