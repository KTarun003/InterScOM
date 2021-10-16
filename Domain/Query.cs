using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Query
    {
        public Query()
        {
            Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; }

        public string UserName { get; set; }

        [Required]
        public string Question { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }
    }

    public enum Topics
    {
        General,
        Admission,
        Fees
    }
}
