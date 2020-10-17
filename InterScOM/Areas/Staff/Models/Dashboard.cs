﻿using System.Collections.Generic;

namespace InterScOM.Areas.Staff.Models
{
    public class Dashboard
    {
        public Dashboard()
        {
            PendingApplications = new HashSet<Application>();
        }
        public int Applications { get; set; }

        public int Accepted { get; set; }

        public int Rejected { get; set; }

        public ICollection<Application> PendingApplications { get; set; }
    }
}
