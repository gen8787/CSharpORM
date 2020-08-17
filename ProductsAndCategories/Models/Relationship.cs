using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models
{
    public class Relationship
    {
// Primary Key
        [Key]
        public int RelationshipId { get; set; }

// MtoM Relationship
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

// Nav Props
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}