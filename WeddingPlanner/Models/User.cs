using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
// Primary Key
        [Key]
        public int UserId { get; set; }

// First Name
        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your first name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

// Last Name
        [MinLength(2)]
        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your last name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

// Email
        [Required]
        [EmailAddress]
        public string Email { get; set; }

// Password
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer.")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

// Confirm Password - [NotMapped]
        [NotMapped]
        [Compare("Password", ErrorMessage="Passwords do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Nav Prop - List<Relationship> (One user can attend many weddings, so a list of weddings.)
        public List<Relationship> RelatedWeddings { get; set; }
    }
}