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
    class IssueMapper : EntityTypeConfiguration<Issue>
    {
        public IssueMapper()
        {
            ToTable("Issues", schemaName: "samskipdrs");

            HasKey(i => i.Id);
            Property(i => i.Id).IsRequired();
            Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(i => i.IssueIdentity).IsRequired();
            Property(i => i.IssueProvider).IsRequired();
            Property(i => i.URL).IsOptional();

            // Relationships
            HasRequired(c => c.ReviewEntity).WithMany().Map(s => s.MapKey("ReviewID"));
        }
    }
}
