using CustomerManagement.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Core.Entities
{
    public class Employee:Entity<int>
    {
        public Guid UserId { get; set; }
        [StringLength(50)]
        public string JobTitle { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
