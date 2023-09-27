using CustomerManagement.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Core.Entities
{
    public class Employee:Entity
    {
        public Guid UserId { get; set; }
        [StringLength(50)]
        public string JobTitle { get; set; }
    }
}
