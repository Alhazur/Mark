

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mark.Models.Class
{
    public class Stores
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Companies")]
        public int CompanyId { get; set; }//prop Companies Companies

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public Companies Companies { get; set; }

    }
}
