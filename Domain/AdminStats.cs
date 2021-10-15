using System.Collections.Generic;

namespace Domain
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
        // supplies
        public int Totalsupplies { get; set; }
        // fee
        public int TotalFees { get; set; }
    }
}
