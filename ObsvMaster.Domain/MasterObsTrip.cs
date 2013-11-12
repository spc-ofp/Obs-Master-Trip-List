using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.Domain
{
    public class MasterObsTrip
    {
        public virtual int Id { get; set; }
        public virtual Vessel Vessel { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual Port StartPort { get; set; }
        public virtual Port EndPort { get; set; }
        public virtual String ObsvCode { get; set; }
        public virtual String ObsvTripCode { get; set; }
        public virtual Program ObsvProg { get; set; }
        public virtual Status Status { get; set; }
        public virtual DateTime LastModifiedDate { get; set; }
        public virtual String LastModifiedBy { get; set; }
    }
}
