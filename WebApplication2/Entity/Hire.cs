using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApplication2.Entity
{
    public class Hire : BaseEntity
    {
        [Required]
        public int StudentId { get; set;  }

        [ValidateNever]
        public int BookId { get; set;  }
        [ForeignKey("BookId")]

        [ValidateNever]
        public Book Book { get;  set;  }


}
}
