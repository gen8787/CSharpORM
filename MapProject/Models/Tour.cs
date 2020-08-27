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

// Nav Props
        public User TourPlanner { get; set; }
        public List<Leg> Legs { get; set; }

// Methods
        // Total Distance
        public double TotalDist()
        {
                double dist = 0;
                try
                {
                        foreach (Leg d in Legs)
                        {
                                dist = dist + d.Distance;
                        }
                }
                catch
                {
                        dist = 0;
                }
                return dist;
        }

        // Total Vertical
        public double TotalVert()
        {
                double vert = 0;
                try
                {
                        foreach (Leg v in Legs)
                        {
                                vert = vert + v.Vertical;
                        }
                }
                catch
                {
                        vert = 0;
                }
                return vert;
        }

        // Total Est. Hours
        public double TotalEstHours()
        {
                double estHours = 0;
                try
                {
                        foreach (Leg t in Legs)
                        {
                                estHours = estHours + t.Time;
                        }
                }
                catch
                {
                        estHours = 0;
                }
                return Math.Floor(estHours);
        }

        // Total Est. Mins
        public double TotalEstMins()
        {
                double estMins = 0;
                double estHours = 0;
                try
                {
                        foreach (Leg t in Legs)
                        {
                                estMins = estMins + t.Time;
                                estHours = estHours + t.Time;
                        }
                }
                catch
                {
                        estMins = 0;
                        estHours = 0;
                }

                return Math.Round((estMins - Math.Floor(estHours)) * 60);
        }
        } // End of Class
}