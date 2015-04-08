using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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
            HasKey(c => c.Id);
            Property(c => c.Id).IsRequired();
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Name
            Property(c => c.Name).IsRequired().HasMaxLength(100)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                    new IndexAttribute("IX_SystemName", 1) { IsUnique = true }));

            // Description
            Property(c => c.Description).IsOptional();
            Property(c => c.Description).HasMaxLength(1000);


        }
    }
}
