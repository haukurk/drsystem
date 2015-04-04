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
    class IssueMapper : EntityTypeConfiguration<Issue>
    {
        public IssueMapper()
        {
            ToTable("Issues", schemaName: "samskipdrs");

            HasKey(i => i.IssueID);
            Property(i => i.IssueID).IsRequired();
            Property(i => i.IssueID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(i => i.IssueIdentity).IsRequired();
            Property(i => i.IssueProvider).IsRequired();
            Property(i => i.URL).IsOptional();

            // Relationships
            HasRequired(c => c.ReviewEntity).WithMany().Map(s => s.MapKey("ReviewID"));
        }
    }
}
