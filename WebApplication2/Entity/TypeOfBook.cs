using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace WebApplication2.Entity
{
    public class TypeOfBook : BaseEntity
    {

    
        [MaxLength(25)]
        [DisplayName("Kitap Türü Adı")]
        [Required(ErrorMessage = "Kitap Türü Adı Boş Bırakılmaz")] //not null
        public string Name { get; set; }
 

    }
}