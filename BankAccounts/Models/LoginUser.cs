using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccounts.Models
{
    [NotMapped] //Not Mapped as User is already in db.
    public class LoginUser
    {
// Email
        [EmailAddress]
        [Required(ErrorMessage = "Please enter your email address.")]
        [Display(Name="Email")]
        public string LoginEmail { get; set; }

// Password
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string LoginPassword { get; set; }
    }
}