using System;
using System.ComponentModel.DataAnnotations;

namespace InterScOM.Areas.Admin.Models
{
    public class VendorOrders
    {
        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string OrderName { get; set; }
        public int OrderRefNumber { get; set; }
        public string OrderClass { get; set; } //books stationary software furniture hardware
        public DateTime DatePlaced { get; set; }
        public DateTime DateReceived { get; set; }

        public int Quantity { get; set; }


        public string Status { get; set; } // placed shipping received
    }
}
