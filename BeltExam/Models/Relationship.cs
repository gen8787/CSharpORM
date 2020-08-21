using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
        public class Relationship
        {
// Primary Key
        [Key]
        public int RelationshipId { get; set; }

// Foreign Keys
        public int UserId { get; set; }
        public int AnActivityId { get; set; }

// Nav Props
        public User Users { get; set; }
        public AnActivity AnActivities { get; set; }
        }
}