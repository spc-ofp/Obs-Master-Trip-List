using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsvMaster.Domain
{
    public class Observer
    {
        public virtual int Id { get; set; }
        public virtual String Code { get; set; }
        public virtual String FirstName { get; set; }
        public virtual String FamilyName { get; set; }
    }
}
