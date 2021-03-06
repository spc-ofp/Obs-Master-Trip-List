﻿using FluentNHibernate.Mapping;
using ObsvMaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.DAL.Maps
{
    public class MasterObsTripHistoryMap : ClassMap<MasterObsTripHistory>
    {
        public MasterObsTripHistoryMap()
        {
            ReadOnly();
            Schema("dbo");
            Table("master_obsv_trip_full");
            CompositeId().KeyProperty(x => x.Id, "master_obsv_trip_id").KeyProperty(x => x.LastModifiedDate, "last_modified_date");
            References(x => x.Vessel, "vessel_id");
            Map(x => x.StartDate).Column("start_date");
            Map(x => x.EndDate).Column("end_date");
            References(x => x.StartPort, "start_port_id");
            References(x => x.EndPort, "end_port_id");
            Map(x => x.ObsvCode).Column("obsv_code");
            Map(x => x.ObsvTripCode).Column("obsv_trip_code");
            //Map(x => x.ObsvProgCode).Column("obsv_prog_code");
            References(x => x.ObsvProg, "obsv_prog_code");
            Map(x => x.LastModifiedDate).Column("last_modified_date");
            Map(x => x.LastModifiedBy).Column("last_modified_by");
            References(x => x.Status, "status_code");
            Map(x => x.IsActive).Column("is_active");
        }
    }
}
