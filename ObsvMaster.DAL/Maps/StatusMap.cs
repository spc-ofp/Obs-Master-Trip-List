using FluentNHibernate.Mapping;
using ObsvMaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.DAL.Maps
{
    public class StatusMap : ClassMap<Status>
    {
        public StatusMap()
        {
            ReadOnly();
            Schema("ref");
            Table("status");
            Id(x => x.Code).Column("status_code");
            Map(x => x.Label).Column("label");
        }
    }
}
