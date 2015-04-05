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
    class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("Users", schemaName: "samskipdrs");

            HasKey(u => u.UserId);
            Property(u => u.UserId).IsRequired();
            Property(u => u.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
