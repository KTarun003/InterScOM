using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Supplies
    {
        [Key]
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string Itemname { get; set; }

        public string ItemClass { get; set; }

        public int ItemQuantity { get; set; }
    }
}
