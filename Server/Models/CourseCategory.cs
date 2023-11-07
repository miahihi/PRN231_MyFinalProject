using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public partial class CourseCategory
    {
        public CourseCategory()
        {
            Courses = new HashSet<Course>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
