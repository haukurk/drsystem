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
    class ReviewEntityMapper : EntityTypeConfiguration<ReviewEntity>
    {
        public ReviewEntityMapper()
        {
            ToTable("ReviewEntities", schemaName: "samskipdrs");

            HasKey(r => r.ReviewID);
            Property(r => r.ReviewID).IsRequired();
            Property(r => r.ReviewID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
