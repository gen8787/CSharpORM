using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
        public class Chef
        {
// Primary Key
        [Key]
        public int ChefId { get; set; }

// First Name
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter the Chef's first name.")]
        [MinLength(2)]
        [MaxLength(25)]
        public string FirstName { get; set; }

// Last Name
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter the Chef's last name.")]
        [MinLength(2)]
        [MaxLength(25)]
        public string LastName { get; set; }

// DOB
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB {get;set;}

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Nav Prop (One Chef has many dishes, so here's a list of dishes)
        public List<Dish> CreatedDishes { get; set; }
        }
}