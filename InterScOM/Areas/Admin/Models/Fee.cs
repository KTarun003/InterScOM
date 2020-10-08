using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterScOM.Areas.Admin.Models
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }

        public string ParentName { get; set; }

        public string FeeStatus { get; set; }

    }
}
