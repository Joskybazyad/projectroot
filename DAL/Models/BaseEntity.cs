using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BaseEntity
    {
        public int ID { get; set; } //Pk
        public int CreatedBy { get; set; } // User ID
        public DateTime CreatedOn { get; set; } // Time Of Creation
        public int LastModifiedBy { get; set; } // User ID
        public DateTime LastModifiedOn { get; set; } // Time Of Creation Automaticaly calculated
        public bool IsDeleted { get; set; }
    }
}
