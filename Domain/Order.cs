using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string ReferenceId { get; set; }

        public int ItemQuantity { get; set; }
    }

    public enum OrdersType
    {
        Books,
        Stationary,
        Software,
        Furniture,
        Hardware
    }
}
