using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
        public class AnActivity
        {
// Primary Key
        [Key]
        public int AnActivityId { get; set; }

// Title
        [Required(ErrorMessage = "Please enter the activity name.")]
        [Display(Name = "Activity Name")]
        public string Title { get; set; }

// Date
        [Required(ErrorMessage = "Please enter the activity date.")]
        public DateTime Date { get; set; }

// Time
        public DateTime Time { get; set; }

// Duration Length
        public int DurationLength { get; set; }

// Duration HrsMins
        public string DurationHrsMins { get; set; }

// Description
        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

// Created At / Updated At
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

// Foreign Keys
        public int UserId { get; set; }

// Nav Props -> 
        public User AnActivityCreator { get; set; }
        public List<Relationship> RelatedParticipants { get; set; }
        }
}