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
    class ReviewEntityMapper : EntityTypeConfiguration<ReviewEntity>
    {
        public ReviewEntityMapper()
        {
            ToTable("ReviewEntities", schemaName: "samskipdrs");

            HasKey(r => r.Id);
            Property(r => r.Id).IsRequired();
            Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(r => r.Description).IsOptional();

            Property(r => r.Responsible).IsRequired();

            Property(r => r.ResponsibleEmail).IsRequired();

            Property(r => r.NotificationPeriod).IsRequired();

            Property(r => r.LastNotified).IsOptional();

            Property(r => r.IdentityString).IsRequired();

            Property(r => r.URL).IsOptional();

            //relationship  
            HasRequired(c => c.User).WithMany(u => u.ReviewEntities).HasForeignKey(x => x.UserID);

        }
    }
}
