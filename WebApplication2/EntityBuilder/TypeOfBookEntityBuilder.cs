using Microsoft.EntityFrameworkCore;
using WebApplication2.Entity;

using WebApplication2.Infrastructure.Interfaces;

namespace WebApplication2.EntityBuilder
{
    public class TypeOfBookEntityBuilder : IEntityBuilder 
    {
        public void Builder(ModelBuilder builder)
        {
            builder.Entity<TypeOfBook>(entity =>
            {
                entity.ToTable("TypeOfBooks");
              
            });
        }
    }
}
