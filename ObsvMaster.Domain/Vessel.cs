using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.Domain
{
    public class Vessel
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String Ircs { get; set; }
        public virtual String Flag { get; set; }
        public virtual String Type { get; set; }
        public virtual String NormalizedName { get; set; }
        public virtual int Uvi { get; set; }
    }
}
