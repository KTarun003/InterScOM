using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using InterScOM.Areas.Staff.Models;

namespace InterScOM.Areas.Admin.Models
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }

        public string ParentName { get; set; }

        public string FeeStatus { get; set; }

        public int ApplicationId { get; set; }

        public Application Application { get; set; }
    }
}
