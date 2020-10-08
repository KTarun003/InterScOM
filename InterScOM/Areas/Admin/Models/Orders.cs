using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterScOM.Areas.Admin.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public string VendorName { get; set; }

        public string ItemName { get; set; }

        public string Status { get; set; }
    }
}
