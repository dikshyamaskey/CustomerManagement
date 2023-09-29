using CustomerManagement.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Core.Entities
{
    public class Customer:Entity<Guid>
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Membership")]
        public Guid MembershipId { get; set; }
        public bool IsActive { get; set; }
        public User User{ get; set; }
        public Membership Membership{ get; set; }
    }
}
