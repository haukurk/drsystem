using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRS2Data.Models;

namespace DRS2Data.Mappers
{
    class LogMapper : EntityTypeConfiguration<Log>
    {
        public LogMapper()
        {
            ToTable("Logs", schemaName: "samskipdrs");

            HasKey(l => l.LogID);
            Property(l => l.LogID).IsRequired();
            Property(l => l.LogID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(l => l.Message).IsRequired();
            Property(l => l.Severity).IsOptional();

            //relationship  
            HasOptional(c => c.User).WithMany().Map(s => s.MapKey("UserID"));

        }
    }
}
