using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class VendorOrders
    {
        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }

        public int OrderId { get; set; }

        public DateTime DatePlaced { get; set; }

        public DateTime DateReceived { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; } // placed shipping received

    }

    public enum OrdersStatus
    {
        Placed,
        Shipping,
        Received
    }

}
