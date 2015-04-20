using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Mappers
{
    class LogMapper : EntityTypeConfiguration<Log>
    {
        public LogMapper()
        {
            ToTable("Logs", schemaName: "samskipdrs");

            HasKey(l => l.Id);
            Property(l => l.Id).IsRequired();
            Property(l => l.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(l => l.Message).IsRequired();
            Property(l => l.Severity).IsOptional();

            //relationship  
            HasOptional(c => c.User).WithMany(u => u.Logs).HasForeignKey(c => c.UserID);

        }
    }
}
