using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InterScOM.Areas.Admin.Models;

namespace InterScOM.Areas.Staff.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FathersName { get; set; }

        public string MothersName { get; set; }

        [Required]
        [DataType("Date")]
        [DisplayName("Date of Birth")]
        public DateTime Dob { get; set; }

        [Required]
        public float Percentage { get; set; }

        [Required]
        public float AnnualIncome { get; set; }

        [Required]
        public int InternetRating { get; set; }

        public int Fees { get; set; }

        public string Email { get; set; }

        [DataType("Date")]
        public DateTime ApplicationDate { get; set; }

        public string Status { get; set; }

        public Fee Fee { get; set; }
    }
}
