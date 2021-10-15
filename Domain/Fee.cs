using System.ComponentModel.DataAnnotations;

namespace Domain
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
