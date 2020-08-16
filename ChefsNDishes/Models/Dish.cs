using System;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
        public class Dish
        {
// Primary Key
        [Key]
        public int DishId { get; set; }

// Dish Name
        [Display(Name = "Dish Name")]
        [Required(ErrorMessage = "Please enter a dish name.")]
        [MaxLength(25)]
        public string Name { get; set; }

// Tastiness
        [Required(ErrorMessage = "Select a tastiness level.")]
        [Range(1, 6)]
        public int Tastiness { get; set; }

// Calories
        [Required(ErrorMessage = "Please enter the amount of calories.")]
        [Range(1, 3000)]
        public int Calories { get; set; }

// Description
        [Required(ErrorMessage = "Please provide a description.")]
        [MinLength(5)]
        [MaxLength(255)]
        public string Description { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Foreign Key - How to uniquely relate to the Chef Model
        [Required]
        public int ChefId { get; set; }

// Nav Prop (A dish is created by a Chef)
        public Chef Creator  { get; set; }
        }
}