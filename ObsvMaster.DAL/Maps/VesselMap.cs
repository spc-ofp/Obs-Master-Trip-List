using FluentNHibernate.Mapping;
using ObsvMaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.DAL.Maps
{
    public class VesselMap : ClassMap<Vessel>
    {
        public VesselMap()
        {
            ReadOnly();
            Schema("ref");
            Table("vessels");
            Id(x => x.Id).Column("vessel_id");
            Map(x => x.Name).Column("vessel_name");
            Map(x => x.Ircs).Column("vessel_ircs");
            Map(x => x.Flag).Column("vessel_flag");
            Map(x => x.Type).Column("vessel_type");
            Map(x => x.NormalizedName).Column("normalised_name");
            Map(x => x.Uvi).Column("vessel_uvi");
        }
    }
}
