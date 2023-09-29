using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Core.Common
{
    public class Entity<TKey>
    {

        [Key]
        public TKey Id { get; set; }
    }
}
