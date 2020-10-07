using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace InterScOM.Areas.Forum.Models
{
    public class Query
    {
        public Query()
        {
            Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Question { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }
    }
}
