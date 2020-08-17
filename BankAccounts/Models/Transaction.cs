using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Transaction
    {
// Primary Key
        [Key]
        public int TransactionId { get; set; }

// Amount
        [Required]
        public decimal Amount { get; set; }

// Created At
        public DateTime CreatedAt { get; set; } = DateTime.Now;

// Foreign Key - How to uniquely relate to the related Model
        [Required]
        public int UserId { get; set; }

// Nav Prop (A dish is created by a Chef)
        public User RelatedUser  { get; set; }
    }
}