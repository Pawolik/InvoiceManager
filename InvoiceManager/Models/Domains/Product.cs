using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Services.Description;

namespace InvoiceManager.Models.Domains
{
    public class Product
    {
        public Product()
        {
            InvoicePositions = new Collection<InvoicePosition>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = " Pole Nazwa produktu jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Nazwa produktu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wartość musi być większa niż 0")]
        [Display(Name = "Wartość produktu")]
        public decimal Value { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ICollection<InvoicePosition> InvoicePositions { get; set; }
        public ApplicationUser User { get; set; }
    }
}