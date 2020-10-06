using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace InterScOM.Areas.Forum.Models
{
    public class Thread
    {
        public string UserName { get; set; }

        public string Question { get; set; }

        public List<Answer> Answers { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }
    }
}
