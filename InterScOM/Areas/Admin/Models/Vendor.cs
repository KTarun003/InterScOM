using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        public string VendorName { get; set; }
        public int Ordersplaced { get; set; }
    }
}
