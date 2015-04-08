using System;
using System.Collections.Generic;
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

            if (_ctx.Systems.Any() )
            {
                return;
            }

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
            new DRSSystem{Name="Windows Server Update Services",Description=""},
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
            _ctx.SaveChanges();
        }
    }
}
