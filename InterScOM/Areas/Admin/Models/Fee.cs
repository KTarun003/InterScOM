using InterScOM.Areas.Staff.Models;
using System.ComponentModel.DataAnnotations;

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
