using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models
{
        public class Category
        {
// Primary Key
        [Key]
        public int CategoryId { get; set; }

// Name
        [Required]
        public string Name { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Nav Prop
        public List<Relationship> RelatedProducts { get; set; }
        }
}