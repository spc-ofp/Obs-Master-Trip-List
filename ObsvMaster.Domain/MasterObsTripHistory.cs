using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.Domain
{
    public class MasterObsTripHistory //: MasterObsTrip
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
        public virtual bool IsActive { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as MasterObsTripHistory;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Id == other.Id &&
                this.LastModifiedDate == other.LastModifiedDate;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ Id.GetHashCode();
                hash = (hash * 31) ^ LastModifiedDate.GetHashCode();

                return hash;
            }
        }
    }
}
