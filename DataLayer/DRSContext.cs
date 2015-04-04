using DRS2Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRS2Data.Mappers;

namespace DRS2Data
{
    public class DRSContext : DbContext
    {

        public DRSContext() : base("DRSConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DRSContext, DRSContextMigrationConfiguration>());
        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ReviewEntity> ReviewEntities { get; set; }
        public DbSet<DRSSystem> Systems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new DRSSystemMapper());
            modelBuilder.Configurations.Add(new IssueMapper());
            modelBuilder.Configurations.Add(new LogMapper());
            modelBuilder.Configurations.Add(new ReviewEntityMapper());
            modelBuilder.Configurations.Add(new UserMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}
