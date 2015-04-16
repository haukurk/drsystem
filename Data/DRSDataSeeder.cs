using System;
using System.Collections.Generic;
using System.IdentityModel.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data
{
    class DRSDataSeeder
    {
        private DRSContext _ctx;
        public DRSDataSeeder(DRSContext ctx)
        {
            _ctx = ctx;
        }

        public void Seed()
        {

            if (_ctx.Systems.Any() || _ctx.Users.Any() || _ctx.ReviewEntities.Any() )
            {
                return;
            }


            // We store oasswords in BCrypt.
            const string pwdToHash = "passw0rd^Y8~JJ"; // ^Y8~JJ is my hard-coded salt
            string hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(pwdToHash, BCrypt.Net.BCrypt.GenerateSalt());
            var user = new User()
                            {
                                DateOfBirth = new DateTime(1984, 4, 8),
                                Email = "haukur@hauxi.is",
                                FirstName = "Administrator",
                                LastName = "DRS",
                                LastLoginDate = null,
                                Password = hashToStoreInDatabase,
                                RegistrationDate = DateTime.Now,
                                UserName = "admin"
                        };
            _ctx.Users.Add(user);

            var logs = new Log()
                       {
                           User = user,
                           Created = DateTime.Now,
                           Message = "Initialized the App.",
                           Severity = 1

                       };
            _ctx.Logs.Add(logs);

            var systems = new List<DRSSystem>
            {
            new DRSSystem{Name="Doris",Description="Doris Container System"},
            new DRSSystem{Name="GFS",Description="Reefer Container System"},
            new DRSSystem{Name="WMOS",Description=""},
            new DRSSystem{Name="Qlikview",Description=""},
            new DRSSystem{Name="WAN",Description=""},
            new DRSSystem{Name="LAN",Description=""},
            new DRSSystem{Name="Concorde",Description=""},
            new DRSSystem{Name="Websense",Description=""},
            new DRSSystem{Name="SAP",Description=""},
            new DRSSystem{Name="Active Directory",Description=""},
            new DRSSystem{Name="AIX-VIOS - IBM Power",Description=""},
            new DRSSystem{Name="Blade Center",Description=""},
            new DRSSystem{Name="Netscaler",Description=""},
            new DRSSystem{Name="Data Center",Description=""},
            new DRSSystem{Name="DNS",Description=""},
            new DRSSystem{Name="Firewall",Description=""},
            new DRSSystem{Name="IP Telephony",Description=""},
            new DRSSystem{Name="iSeries",Description=""},
            new DRSSystem{Name="Local Area Network",Description=""},
            new DRSSystem{Name="MS Windows Server Recovery",Description=""},
            new DRSSystem{Name="Nagios",Description=""},
            new DRSSystem{Name="Oracle",Description=""},
            new DRSSystem{Name="Storage Area Network",Description=""},
            new DRSSystem{Name="Tivoli Identity Manager",Description=""},
            new DRSSystem{Name="VMware",Description=""},
            new DRSSystem{Name="Wide Area Network",Description=""},
            new DRSSystem{Name="Windows Server Uspdate Services",Description=""},
            new DRSSystem{Name="Wireless Infrastructure ",Description=""},
            new DRSSystem{Name="Jira & Wiki",Description=""},
            new DRSSystem{Name="Tivoli Storage Manager",Description=""},
            new DRSSystem{Name="Intel - Linux & Windows",Description=""},
            new DRSSystem{Name="VEEAM",Description=""},
            new DRSSystem{Name="Citrix",Description=""},
            new DRSSystem{Name="GOS",Description=""},
            new DRSSystem{Name="ServiceWeb",Description=""},
            new DRSSystem{Name="Remote Office",Description=""},
            new DRSSystem{Name="MS Exchange",Description=""}
            };

            systems.ForEach(s => _ctx.Systems.Add(s));


            var reviews = new List<ReviewEntity>
                          {
                              new ReviewEntity()
                              {
                                  User = user,
                                  System = systems[5],
                                  Description = "Demo review for a system.",
                                  IdentityString = Guid.NewGuid().ToString("N"),
                                  NotificationPeriod = 128,
                                  Responsible = "Some Guy in IT",
                                  ResponsibleEmail = "haukur@hauxi.is",
                                  URL = "http://someurl.com/jira/ticket"
                              },
                                                            new ReviewEntity()
                              {
                                  User = user,
                                  System = systems[6],
                                  Description = "Demo review for a system.",
                                  IdentityString = Guid.NewGuid().ToString("N"),
                                  NotificationPeriod = 256,
                                  Responsible = "Some Guy in IT",
                                  ResponsibleEmail = "haukur@hauxi.is",
                                  URL = "http://someurl.com/jira/ticket"
                              }
                          };

            reviews.ForEach(s => _ctx.ReviewEntities.Add(s));

            var issues = new List<Issue>
                         {
                             new Issue()
                             {
                                 ForcedByUser = user,
                                 IssueIdentity = "JIRA-342",
                                 IssueProvider = "JIRA",
                                 ReviewEntity = reviews[0]
                             },
                             new Issue()
                             {
                                 ForcedByUser = user,
                                 IssueIdentity = "JIRA-32342",
                                 IssueProvider = "JIRA",
                                 ReviewEntity = reviews[1]
                             },
                         };

            issues.ForEach(s => _ctx.Issues.Add(s));

            _ctx.SaveChanges();
        }
    }
}
