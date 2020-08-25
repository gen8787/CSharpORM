using System;
using System.ComponentModel.DataAnnotations;

namespace MapProject.Models
{
    public class Leg
    {
// Primary Key
        [Key]
        public int LegId { get; set; }

// Distance
        public decimal Distance { get; set; }

// Vertical
        public int Vertical { get; set; }

// Munter Rate
        public decimal MunterRate { get; set; }

// Time
        public decimal Time { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Foreign Keys
        public int UserId { get; set; }

// Nav Props
        public User LegCreator { get; set; }
    }
}