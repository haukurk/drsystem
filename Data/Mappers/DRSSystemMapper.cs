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
    class DRSSystemMapper : EntityTypeConfiguration<DRSSystem>
    {

        public DRSSystemMapper()
        {
            ToTable("Systems", schemaName: "samskipdrs");

            // System ID
            HasKey(c => c.DRSSystemID);
            Property(c => c.DRSSystemID).IsRequired();
            Property(c => c.DRSSystemID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Name
            Property(c => c.Name).IsRequired();

            // Description
            Property(c => c.Description).IsOptional();
            Property(c => c.Description).HasMaxLength(1000);


        }
    }
}
