using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public partial class Enrollment
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime? EnrollTime { get; set; }
        [JsonIgnore]
        public virtual Course? Course { get; set; } 
        [JsonIgnore]

        public virtual User? User { get; set; }
    }
}
