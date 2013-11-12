using FluentNHibernate.Mapping;
using ObsvMaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.DAL.Maps
{
    public class ProgramMap : ClassMap<Program>
    {
        public ProgramMap()
        {
            ReadOnly();
            Schema("ref");
            Table("programs");
            Id(x => x.Code).Column("obsprg_code");
            Map(x => x.Comments).Column("comments");
            Map(x => x.Description).Column("description");
        }
    }
}
