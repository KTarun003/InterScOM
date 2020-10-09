using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterScOM.Areas.Admin.Models
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
