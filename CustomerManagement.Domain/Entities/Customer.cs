using CustomerManagement.Core.Common;

namespace CustomerManagement.Core.Entities
{
    public class Customer:Entity
    {
     
        public Guid UserId { get; set; }
        public Guid MembershipId { get; set; }
        public bool IsActive { get; set; }
    }
}
