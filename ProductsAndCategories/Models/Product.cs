using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models
{
    public class Product
    {
// Primary Key
        [Key]
        public int ProductId { get; set; }
// Name
        [Required]
        public string Name { get; set; }
// Description
        [MinLength(10)]
        public string Description { get; set; }
// Price
        [Required]
        public decimal Price { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Nav Prop
        public List<Relationship> RelatedCategories { get; set; }
    }
}