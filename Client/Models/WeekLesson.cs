﻿using System;
using System.Collections.Generic;

namespace Client.Models
{
    public partial class WeekLesson
    {
        public WeekLesson()
        {
            Assignments = new HashSet<Assignment>();
        }

        public int Id { get; set; }
        public int? CourseId { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
