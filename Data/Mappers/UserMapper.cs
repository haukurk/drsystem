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
    class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("Users", schemaName: "samskipdrs");

            HasKey(u => u.Id);
            Property(u => u.Id).IsRequired();
            Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.RegistrationDate).IsRequired();

            Property(u => u.FirstName).IsRequired();

            Property(u => u.LastName).IsRequired();

            Property(u => u.Password).IsRequired();

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName, 
                    new IndexAnnotation(
                    new IndexAttribute("IX_UserName", 1) { IsUnique = true }));

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                    new IndexAttribute("IX_Email", 1) { IsUnique = true }));

            Property(u => u.RegistrationDate).IsOptional();

            Property(u => u.LastLoginDate).IsOptional();

            Property(u => u.DateOfBirth).IsOptional();

        }
    }
}
