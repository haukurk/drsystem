using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class DRSSystem
    {
        public DRSSystem()
        {
            ReviewEntities = new List<ReviewEntity>();
        }

        [Key]
        public int DRSSystemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ReviewEntity> ReviewEntities { get; set; }
    }
}
