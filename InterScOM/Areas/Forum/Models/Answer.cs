using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterScOM.Areas.Forum.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int QueryId { get; set; }

        public string UserName { get; set; }

        public string ThreadAnswer { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }
    }
}
