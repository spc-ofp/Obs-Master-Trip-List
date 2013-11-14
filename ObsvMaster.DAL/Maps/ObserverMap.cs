using FluentNHibernate.Mapping;
using ObsvMaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.DAL.Maps
{
    public class ObserverMap : ClassMap<Observer>
    {
        public ObserverMap()
        {
            ReadOnly();
            Schema("REFERENCE.ref");
            Table("field_staff");
            Id(x => x.Id).Column("staff_id");
            Map(x => x.Code).Column("staff_code");
            Map(x => x.FamilyName).Column("family_name");
            Map(x => x.FirstName).Column("first_name");
        }
    }
}
