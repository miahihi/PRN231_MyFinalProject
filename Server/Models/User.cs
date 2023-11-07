using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public partial class User
    {
        public User()
        {
            Enrollments = new HashSet<Enrollment>();
            Submissions = new HashSet<Submission>();
        }

        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public int? Type { get; set; }
        [JsonIgnore]

        public string? Fullname { get; set; }
        [JsonIgnore]

        public string? Mssv { get; set; }
        [JsonIgnore]


        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [JsonIgnore]

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
