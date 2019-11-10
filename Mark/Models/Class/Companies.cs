using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mark.Models.Class
{
    public class Companies
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int OrganizationNumber { get; set; }

        public string Note { get; set; }

        public List<Stores> Stores { get; set; }

        //public virtual ICollection<Stores> Stores { get; set; }

    }
}
