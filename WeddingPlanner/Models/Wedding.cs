using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
        public class Wedding
        {
// Primary Key
        [Key]
        public int WeddingId { get; set; }

// Wedder One
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter your full name.")]
        [Display(Name = "Full Name")]
        public string WedderOne { get; set; }

// Wedder Two
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter your full name.")]
        [Display(Name = "Full Name")]
        public string WedderTwo { get; set; }

// Date
        [Required(ErrorMessage = "Please enter your wedding date.")]
        public DateTime Date { get; set; }

// Address
        [Required(ErrorMessage = "Please enter your wedding address.")]
        public string Address { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Foreign Keys
        public int UserId { get; set; }

// Nav Prop - One wedding can be organized by only one User, but can be attended by many Users.
        public User WeddingOrganizer { get; set; }
        public List<Relationship> RelatedUsers { get; set; }
        }
}