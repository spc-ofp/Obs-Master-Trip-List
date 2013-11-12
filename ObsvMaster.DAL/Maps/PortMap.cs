using FluentNHibernate.Mapping;
using ObsvMaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.DAL.Maps
{
    public class PortMap : ClassMap<Port>
    {
        public PortMap()
        {
            ReadOnly();
            Schema("ref");
            Table("ports");
            Id(x => x.Id).Column("port_id");
            Map(x => x.Name).Column("port_name");
            Map(x => x.CountryCode).Column("cty_code");
            Map(x => x.LocationCode).Column("location_code");
            Map(x => x.AlsoCalled).Column("also_called");
            Map(x => x.Latitude).Column("port_latd");
            Map(x => x.Longitude).Column("port_lond");
            Map(x => x.Active).Column("active");
        }
    }
}
