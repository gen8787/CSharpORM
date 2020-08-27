using System;
using System.ComponentModel.DataAnnotations;

namespace MapProject.Models
{
        public class Leg
        {
// Primary Key
        [Key]
        public int LegId { get; set; }

// Leg #
        public int LegNumber { get; set; }

// Distance
        public double Distance { get; set; }

// Vertical
        public double Vertical { get; set; }

// Munter Rate
        public double MunterRate { get; set; }

// Time
        public double Time { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Foreign Keys
        public int UserId { get; set; }
        public int TourId { get; set; }

// Nav Props
        public User LegCreator { get; set; }
        public Tour RelatedTour { get; set; }
        }
}