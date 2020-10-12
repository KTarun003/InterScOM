using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterScOM.Areas.Admin.Models
{
    public class AdminStats
    {

        // A list of orders for Sending it to the page
        public AdminStats()
        {
            OrdersShipping = new HashSet<VendorOrders>();
        }


        // order attributes
        public int Totalorders { get; set; }
        public int OrdersPlaced { get; set; }
        public int OrdersReceived { get; set; }

        // list of orders
        public ICollection<VendorOrders> OrdersShipping { get; set; }

        // Vendors
        public int TotalVendors { get; set; }
    }
}
