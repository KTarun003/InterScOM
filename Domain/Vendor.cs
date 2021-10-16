using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        public string VendorName { get; set; }

        public int OrdersPlaced { get; set; }
    }
}
