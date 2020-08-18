using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Relationship
    {
// Primary Key
        [Key]
        public int RelationshipId { get; set; }

// MtoM Relationship
        public int UserId { get; set; }
        public int WeddingId { get; set; }

// Nav Props
        public User Users { get; set; }
        public Wedding Weddings { get; set; }
    }
}