using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;

namespace WebApplication2.Entity
{
    public class Book : BaseEntity
    {
        [Required]
        public string BookName { get; set; }

        public string Definition { get; set; }

        [Required] //MUTLAKA OLMALI MANTIĞI
        public string Writer { get; set; }

        [Required]
        [Range(10,5000)] //10 tl den başlasın 5000 tl kadar gitsin 
        public  double Price { get; set; }

        [ValidateNever]
       public int TypeOfBookId { get; set; }
        [ForeignKey("TypeOfBookId")]
        [ValidateNever]
        public TypeOfBook  TypeOfBook { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}
