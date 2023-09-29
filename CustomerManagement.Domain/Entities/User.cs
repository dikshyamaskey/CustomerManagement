
using CustomerManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Core.Entities
{
    public class User:Entity<Guid>
    {
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        [StringLength(50)]
        [Required]
        public string EmailAddress { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
    }
}
