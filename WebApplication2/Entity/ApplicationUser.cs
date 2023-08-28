using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace WebApplication2.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [Required] 
        public int StudentNo  { get; set; }  
        public string? Adress { get; set; }
        public string? Faculty { get; set; }
        public string? Section { get; set; }


    }
}
