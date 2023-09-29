using CustomerManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Core.Entities
{
    public class Membership:Entity<Guid>
    {
        public string MembershipName { get; set; }
        public string Description { get; set; }
        public int DiscountTypeId { get; set; }
    }
}
