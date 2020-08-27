using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MapProject.Models
{
        public class Tour
        {
// Primary Key
        [Key]
        public int TourId { get; set; }

// Tour Name
        public string TourName { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Foreign Keys
        public int UserId { get; set; }

        // public int LegId { get; set; } // May not be needed - accessed through List<Leg> Legs ?

// Nav Props
        public User TourPlanner { get; set; }
        public List<Leg> Legs { get; set; }

        // public List<User> UsersGoing { get; set; } - Throws error when migrating
        }
}