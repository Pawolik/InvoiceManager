using InvoiceManager.Models.Domains;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i hasło potwierdzające nie są zgodne.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
