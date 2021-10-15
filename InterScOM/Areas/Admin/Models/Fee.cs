using System.ComponentModel.DataAnnotations;
using Web.Areas.Staff.Models;

namespace Web.Areas.Admin.Models
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
