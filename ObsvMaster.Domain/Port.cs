using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.Domain
{
    public class Port
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String CountryCode { get; set; }
        public virtual String LocationCode { get; set; }
        public virtual String AlsoCalled { get; set; }
        public virtual Decimal Latitude { get; set; }
        public virtual Decimal Longitude { get; set; }
        public virtual Boolean Active { get; set; }
    }
}
