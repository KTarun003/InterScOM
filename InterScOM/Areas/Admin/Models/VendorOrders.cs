using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
