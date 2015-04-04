using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS2Data
{
    class DRSContextMigrationConfiguration : DbMigrationsConfiguration<DRSContext>
    {
        public DRSContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

#if DEBUG
        protected override void Seed(DRSContext context)
        {
            new DRSDataSeeder(context).Seed();
        }
#endif
    }
}
